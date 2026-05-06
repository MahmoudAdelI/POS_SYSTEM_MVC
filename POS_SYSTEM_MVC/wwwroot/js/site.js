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
        e.preventDefault();
        var url = $(this).attr('action');
        var query = $('#productSearchInput').val();

        // Construct URL with query string
        var fullUrl = url + (url.indexOf('?') > -1 ? '&' : '?') + 'searchQuery=' + encodeURIComponent(query);

        loadContent(fullUrl, true);
    });

    $(document).on('click', '.ajax-link', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');

        if (url === '#' || !url) return;

        // Don't do AJAX for logout or identity pages
        if (url.includes('Logout') || url.includes('/Identity/')) {
            window.location.href = url;
            return;
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

