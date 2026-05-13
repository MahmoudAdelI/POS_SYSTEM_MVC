// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    // Sidebar Toggle
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        // Toggle the margin on content area for fixed sidebar transition
        if ($('#sidebar').hasClass('active')) {
            $('#content').css('margin-left', '0');
        } else {
            if ($(window).width() > 768) {
                $('#content').css('margin-left', '250px');
            }
        }
    });

    // Handle window resize to fix margins
    $(window).resize(function () {
        if ($(window).width() <= 768) {
            $('#content').css('margin-left', '0');
        } else if (!$('#sidebar').hasClass('active')) {
            $('#content').css('margin-left', '250px');
        }
    });

    // Handle Search Form Submission
    $(document).on('submit', '#productSearchForm', function (e) {
        if ($(this).hasClass('no-ajax')) return;
        e.preventDefault();
        var url = $(this).attr('action');
        var query = $('#productSearchInput').val();

        // Construct URL with query string
        var fullUrl = url + (url.indexOf('?') > -1 ? '&' : '?') + 'searchQuery=' + encodeURIComponent(query);

        loadContent(fullUrl, true);
    });

    $(document).on('click', '.ajax-link', function (e) {
        if ($(this).hasClass('no-ajax')) return;
        e.preventDefault();
        var url = $(this).attr('href');

        if (url === '#' || !url) return;

        // Don't do AJAX for logout or identity pages
        if (url.includes('Logout') || url.includes('/Identity/')) {
            window.location.href = url;
            return;
        }

        // If navigating to Cashier Index, preserve current filters
        if (url.includes('/Cashier')) {
            const stockFilter = $('.stock-radio-filter:checked').val();
            const searchTerm = $('#searchInput').val();
            const pageSize = $('#pageSizeSelect').val();

            const urlObj = new URL(url, window.location.origin);
            if (stockFilter && stockFilter !== 'all') {
                urlObj.searchParams.set('stockFilter', stockFilter);
            }
            if (searchTerm) {
                urlObj.searchParams.set('searchTerm', searchTerm);
            }
            if (pageSize) {
                urlObj.searchParams.set('pageSize', pageSize);
            }
            url = urlObj.pathname + urlObj.search;
        }

        loadContent(url, true);

        // Update active class in sidebar
        $('.components li').removeClass('active');
        $(this).closest('li').addClass('active');

        // Collapse sidebar on mobile after click
        if ($(window).width() <= 768) {
            $('#sidebar').addClass('active');
        }
    });

    // Handle Browser Back/Forward buttons
    window.onpopstate = function (e) {
        if (e.state && e.state.path) {
            loadContent(e.state.path, false);
        }
    };

    function loadContent(url, pushState) {
        // Prevent recursive or unnecessary reloads
        if (window.location.href === url) return;

        $('#ajax-loader').fadeIn(100);

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                // Extract only the <main> content from the returned HTML
                var $tempDom = $('<div/>').append($.parseHTML(data));
                var $newContent = $tempDom.find('main[role="main"]');

                if ($newContent.length > 0) {
                    $('main[role="main"]').html($newContent.html());
                } else {
                    // Fallback to updating the whole container if <main> is missing
                    $('main[role="main"]').html(data);
                }

                if (pushState) {
                    history.pushState({ path: url }, '', url);
                }

                // Trigger an event that the page has changed, useful for re-initializing components
                $(document).trigger('content-updated');

                $('#ajax-loader').fadeOut(100);
            },
            error: function () {
                $('#ajax-loader').fadeOut(100);
                // alert('Error loading page.');
                window.location.href = url; // Fallback to normal navigation
            }
        });
    }
});

// POS System Global State
window.posCart = window.posCart || [];
window.activeDiscountRules = window.activeDiscountRules || [];
let isCartVisible = false;
let isDiscountRulesLoading = false;

// Global POS Functions
function openProductDetails(id) {
    $.get('/Cashier/GetProductDetails/' + id, function (data) {
        let $container = $('#modalContainer');
        if ($container.length === 0) {
            $('body').append('<div id="modalContainer"></div>');
            $container = $('#modalContainer');
        }
        $container.html(data);
        $('#productDetailsModal').modal('show');
    });
}

function toggleCart(show) {
    isCartVisible = show;
    const $sidebar = $('#cartSidebar');
    const $floatingBtn = $('#floatingCartBtn');

    if ($sidebar.length === 0) return;

    if (show) {
        $sidebar.removeClass('hidden');
        $floatingBtn.addClass('d-none');
    } else {
        $sidebar.addClass('hidden');
        if (window.posCart.length > 0) {
            $floatingBtn.removeClass('d-none');
        }
    }
}

function renderCart() {
    const $cartItems = $('#cartItems');
    if ($cartItems.length === 0) return; // Not on cashier page

    const $cartCount = $('#cartCount');
    const $floatingCartCount = $('#floatingCartCount');
    const $emptyMsg = $('#emptyCartMessage');

    if (window.posCart.length === 0) {
        $cartItems.find('.cart-item').remove();
        $emptyMsg.show();
        $cartCount.text(0);
        $floatingCartCount.text(0);
        $('#checkoutBtn').prop('disabled', true);
        updateTotals({
            subtotal: 0,
            lineDiscountTotal: 0,
            orderDiscount: 0,
            totalDiscount: 0,
            total: 0
        });

        if (isCartVisible) {
            toggleCart(false);
        }
        $('#floatingCartBtn').addClass('d-none');
    } else {
        $emptyMsg.hide();
        $cartItems.find('.cart-item').remove();

        let subtotal = 0;
        let count = 0;

        window.posCart.forEach(item => {
            subtotal += item.unitPrice * item.quantity;
            count += item.quantity;

            const attrText = Object.entries(item.attributes).map(([k, v]) => `${k}: ${v}`).join(' - ');
            const safeName = encodeURIComponent(item.name || '?');
            const placeholder = 'https://placehold.co/45x45/eeeeee/555555?text=' + safeName;
            const src = item.imageUrl || placeholder;

            const itemHtml = `
                <div class="cart-item p-2 mb-2 rounded-3 border bg-white shadow-sm" data-variant-id="${item.variantId}">
                    <div class="d-flex gap-2">
                            <img src="${src}" class="rounded" style="width: 45px; height: 45px; object-fit: cover;" 
                                onerror="this.onerror=null; this.src='${placeholder}';">
                        <div class="flex-grow-1 min-width-0">
                            <div class="d-flex justify-content-between align-items-start">
                                <h6 class="mb-0 fw-bold text-truncate" style="font-size: 0.75rem; max-width: 140px;">${item.name}</h6>
                                <button class="btn btn-link text-danger p-0" onclick="removeFromCart(${item.variantId})">
                                    <i class="fas fa-trash-alt" style="font-size: 0.7rem;"></i>
                                </button>
                            </div>
                            <p class="text-muted mb-1 text-truncate" style="font-size: 0.7rem;">${attrText}</p>
                            <div class="d-flex justify-content-between align-items-center">
                                <div class="input-group input-group-sm" style="width: 80px;">
                                    <button class="btn btn-outline-secondary py-0 px-1" style="font-size: 0.7rem;" onclick="updateQty(${item.variantId}, -1)">-</button>
                                    <input type="text" class="form-control text-center py-0 px-1 bg-white border-secondary border-start-0 border-end-0" style="font-size: 0.75rem;" value="${item.quantity}" readonly>
                                    <button class="btn btn-outline-secondary py-0 px-1" style="font-size: 0.7rem;" onclick="updateQty(${item.variantId}, 1)">+</button>
                                </div>
                                <span class="fw-bold text-primary" style="font-size: 0.75rem;">$${(item.unitPrice * item.quantity).toFixed(2)}</span>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            $cartItems.append(itemHtml);
        });

        $cartCount.text(count);
        $floatingCartCount.text(count);
        $('#checkoutBtn').prop('disabled', false);

        const totals = calculateCartTotals(subtotal);
        updateTotals(totals);

        if (!isCartVisible) {
            toggleCart(true);
        }
    }
}

function updateTotals(totals) {
    $('#cartSubtotal').text('$' + totals.subtotal.toFixed(2));
    $('#cartLineDiscount').text('-$' + totals.lineDiscountTotal.toFixed(2));
    $('#cartOrderDiscount').text('-$' + totals.orderDiscount.toFixed(2));
    $('#cartTotalDiscount').text('-$' + totals.totalDiscount.toFixed(2));
    $('#cartTotal').text('$' + totals.total.toFixed(2));
}

function calculateCartTotals(subtotal) {
    const lineDiscountTotal = calculateLineDiscountTotal();
    const subtotalAfterLineDiscount = Math.max(0, subtotal - lineDiscountTotal);
    const orderDiscount = calculateOrderDiscount(subtotalAfterLineDiscount);
    const totalDiscount = lineDiscountTotal + orderDiscount;
    const total = Math.max(0, subtotal - totalDiscount);

    return {
        subtotal: subtotal,
        lineDiscountTotal: lineDiscountTotal,
        orderDiscount: orderDiscount,
        totalDiscount: totalDiscount,
        total: total
    };
}

function calculateLineDiscountTotal() {
    let lineDiscountTotal = 0;

    window.posCart.forEach(item => {
        const lineSubtotal = item.unitPrice * item.quantity;
        const lineRule = getLineDiscountRule(item);
        const lineDiscount = calculateDiscountAmount(lineRule, lineSubtotal);
        lineDiscountTotal += lineDiscount;
    });

    return roundMoney(lineDiscountTotal);
}

function calculateOrderDiscount(subtotalAfterLineDiscount) {
    const orderRule = getOrderDiscountRule(subtotalAfterLineDiscount);
    return calculateDiscountAmount(orderRule, subtotalAfterLineDiscount);
}

function getLineDiscountRule(item) {
    const variantRule = window.activeDiscountRules
        .filter(r => !hasThreshold(r) && r.productVariantId === item.variantId)
        .sort((a, b) => getRuleTimestamp(b) - getRuleTimestamp(a))[0];

    if (variantRule) {
        return variantRule;
    }

    return window.activeDiscountRules
        .filter(r => !hasThreshold(r) && !r.productVariantId && r.productId === item.productId)
        .sort((a, b) => getRuleTimestamp(b) - getRuleTimestamp(a))[0] || null;
}

function getOrderDiscountRule(subtotalAfterLineDiscount) {
    return window.activeDiscountRules
        .filter(r => hasThreshold(r) && subtotalAfterLineDiscount >= Number(r.saleTotalThreshold || 0))
        .sort((a, b) => getRuleTimestamp(b) - getRuleTimestamp(a))[0] || null;
}

function calculateDiscountAmount(rule, amountBase) {
    if (!rule || amountBase <= 0) {
        return 0;
    }

    let discountAmount = 0;
    if (rule.type === 'Fixed') {
        discountAmount = Number(rule.value || 0);
    } else if (rule.type === 'Percentage') {
        discountAmount = amountBase * (Number(rule.value || 0) / 100);
    }

    if (discountAmount < 0) {
        discountAmount = 0;
    }

    if (discountAmount > amountBase) {
        discountAmount = amountBase;
    }

    return roundMoney(discountAmount);
}

function hasThreshold(rule) {
    return rule && rule.saleTotalThreshold !== null && rule.saleTotalThreshold !== undefined;
}

function getRuleTimestamp(rule) {
    if (!rule || !rule.createdAt) {
        return 0;
    }

    const ts = new Date(rule.createdAt).getTime();
    return Number.isNaN(ts) ? 0 : ts;
}

function roundMoney(value) {
    return Math.round((Number(value) + Number.EPSILON) * 100) / 100;
}

function loadActiveDiscountRules() {
    if (isDiscountRulesLoading) {
        return;
    }

    isDiscountRulesLoading = true;

    $.ajax({
        url: '/Cashier/GetActiveDiscountRules',
        type: 'GET',
        success: function (response) {
            if (response && response.success && Array.isArray(response.discounts)) {
                window.activeDiscountRules = response.discounts;
                renderCart();
            }
        },
        complete: function () {
            isDiscountRulesLoading = false;
        }
    });
}

function updateQty(variantId, delta) {
    const item = window.posCart.find(i => i.variantId === variantId);
    if (item) {
        item.quantity += delta;
        if (item.quantity <= 0) {
            removeFromCart(variantId);
        } else {
            renderCart();
        }
    }
}

function removeFromCart(variantId) {
    window.posCart = window.posCart.filter(i => i.variantId !== variantId);
    renderCart();
}

function clearCart() {
    if (window.posCart.length > 0 && confirm('Clear all items from cart?')) {
        window.posCart = [];
        renderCart();
    }
}

function checkout() {
    if (window.posCart.length === 0) return;

    const $btn = $('#checkoutBtn');
    const originalText = $btn.text();
    $btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm me-1"></span>');

    const payload = {
        items: window.posCart.map(i => ({

            productVariantId: i.variantId,
            quantity: i.quantity,
            unitPrice: i.unitPrice
        }))
    };

    $.ajax({
        url: '/Cashier/Checkout',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(payload),
        success: function (response) {
            if (response.success) {
                printReceipt(response.receiptData);
                window.posCart = [];
                renderCart();
                alert('Order completed successfully!');
            } else {
                alert('Error: ' + response.message);
            }
        },
        error: function () {
            alert('An error occurred during checkout.');
        },
        complete: function () {
            $btn.prop('disabled', false).text(originalText);
        }
    });
}

function printReceipt(data) {
    let iframe = document.getElementById('receiptPrinter');
    if (!iframe) {
        iframe = document.createElement('iframe');
        iframe.id = 'receiptPrinter';
        iframe.style.display = 'none';
        document.body.appendChild(iframe);
    }

    let itemsHtml = '';
    data.items.forEach(item => {
        itemsHtml += `
            <tr>
                <td style="padding: 5px 0;">${item.name}<br><small>${item.variantInfo}</small></td>
                <td style="text-align: center;">${item.quantity}</td>
                <td style="text-align: right;">$${Number(item.lineTotal).toFixed(2)}</td>
            </tr>
        `;
    });

    const html = `
        <html>
        <head>
            <title>Receipt #${data.saleId}</title>
            <style>
                body { font-family: 'Courier New', monospace; font-size: 14px; line-height: 1.2; padding: 20px; }
                .text-center { text-align: center; }
                .border-top { border-top: 1px dashed #000; margin: 10px 0; padding-top: 10px; }
                table { width: 100%; border-collapse: collapse; }
            </style>
        </head>
        <body onload="window.print();">
            <div class="text-center">
                <h3>POS SYSTEM</h3>
                <p>Receipt #${data.saleId}<br>${data.date}</p>
            </div>
            <div class="border-top">
                <table>
                    <thead>
                        <tr>
                            <th style="text-align: left;">Item</th>
                            <th>Qty</th>
                            <th style="text-align: right;">Total</th>
                        </tr>
                    </thead>
                    <tbody>${itemsHtml}</tbody>
                </table>
            </div>
            <div class="border-top">
                <div style="display: flex; justify-content: space-between;"><span>Subtotal:</span><span>$${data.subtotal.toFixed(2)}</span></div>
                <div style="display: flex; justify-content: space-between;"><span>Item Discounts:</span><span>-$${data.lineDiscountTotal.toFixed(2)}</span></div>
                <div style="display: flex; justify-content: space-between;"><span>Order Discount:</span><span>-$${data.orderDiscount.toFixed(2)}</span></div>
                <div style="display: flex; justify-content: space-between;"><span>Total Discounts:</span><span>-$${data.totalDiscount.toFixed(2)}</span></div>
                <div style="display: flex; justify-content: space-between; font-weight: bold; font-size: 16px; margin-top: 5px;">
                    <span>TOTAL:</span><span>$${data.total.toFixed(2)}</span>
                </div>
            </div>
            <div class="text-center border-top" style="margin-top: 20px;">
                <p>Thank you for your purchase!</p>
            </div>
        </body>
        </html>
    `;

    const doc = iframe.contentWindow.document;
    doc.open();
    doc.write(html);
    doc.close();
}

// Product Loading Logic
function loadProducts(page = 1) {
    const $searchInput = $('#searchInput');
    const $productListContainer = $('#productListContainer');
    if ($productListContainer.length === 0) return;

    const searchTerm = $searchInput.val() || '';
    const stockFilter = $('.stock-radio-filter:checked').val() || 'all';
    const categoryId = $('#currentCategoryId').val();
    const subCategoryId = $('#currentSubCategoryId').val();
    const pageSize = $('#pageSizeSelect').val() || 12;

    $productListContainer.css('opacity', '0.6');

    $.ajax({
        url: '/Cashier/Index',
        data: {
            searchTerm: searchTerm,
            stockFilter: stockFilter,
            categoryId: categoryId,
            subCategoryId: subCategoryId,
            page: page,
            pageSize: pageSize,
            partial: true
        },
        success: function (data) {
            $productListContainer.html(data);
        },
        error: function () {
            alert('Error loading products.');
        },
        complete: function () {
            $productListContainer.css('opacity', '1');
        }
    });
}

// Event Handlers
$(document).on('input', '#searchInput', function () {
    clearTimeout(window.searchTimer);
    window.searchTimer = setTimeout(function () {
        loadProducts(1);
    }, 400);
});

$(document).on('change', '.stock-radio-filter', function () {
    loadProducts(1);
});

$(document).on('change', '#pageSizeSelect', function () {
    loadProducts(1);
});

$(document).on('click', '.pagination-link', function (e) {
    e.preventDefault();
    const page = $(this).data('page');
    if (page) {
        loadProducts(page);
    }
});

// Re-initialize on content update
$(document).on('content-updated', function () {
    loadActiveDiscountRules();
    renderCart();
});

// Initial load
$(document).ready(function () {
    loadActiveDiscountRules();
    renderCart();
});

