import cartesian from "./utils/cartesian.js";
import syncUI from "./utils/syncUI.js";

export default function renderVariants() {
    const groups = document.querySelectorAll(".attribute-group");
    const selected = [];

    groups.forEach(group => {
        const attributeSelectInput = group.querySelector(".attribute-type");
        const attrId = Number(attributeSelectInput.value);
        const attrName = attributeSelectInput.options[attributeSelectInput.selectedIndex]?.text;

        if (!attrId) {
            return; // skip if no attribute selected
        }

        const checkedBoxes = [...group.querySelectorAll(".btn-check:checked")]; // spread to convert to array not NodeList
        if (checkedBoxes.length === 0) {
            return; // skip if no values checked
        }


        selected.push({
            attrId,
            attrName,
            values: checkedBoxes.map(cb => ({
                id: Number(cb.value),
                name: document.querySelector(`label[for="${cb.id}"]`)?.textContent ?? cb.value
            }))
        });
    });

    if (selected.length === 0) {
        clearVariantsTable();
        return;
    }

    const combos = cartesian(selected.map(a => a.values.map(v => ({
        attrId: a.attrId,
        attrName: a.attrName,
        valueId: v.id,
        valueName: v.name
    }))));

    renderTable(combos);
    syncUI();
}


function renderTable(combos) {
    const container = document.querySelector("#variantsSection");
    container.innerHTML = `
    <h5 class="mb-3">Generated Variants</h5>
    <div class="table-responsive bg-white border rounded">
        <table class="table table-hover mb-0" id="variantsTable">
                <thead class="table-light">
                <tr>
                    <th>Combination</th>
                    <th style="width:160px">Price</th>
                    <th style="width:130px">Stock</th>
                </tr>
            </thead>
            <tbody>
                ${combos.map((combo, i) => `
                    <tr data-combo='${JSON.stringify(combo.map(c => c.valueId))}'>
                        <td>${combo.map(c => `<span class="badge bg-secondary me-1">${c.attrName}: ${c.valueName}</span>`).join("")}</td>
                        <td>
                            <input type="number" class="form-control variant-price" 
                                   min="0" step="0.01" placeholder="0.00" 
                                   data-index="${i}">
                        </td>
                        <td>
                            <input type="number" class="form-control variant-stock" 
                                   min="0" step="1" placeholder="0" 
                                   data-index="${i}">
                        </td>
                    </tr>
                `).join("")}
            </tbody>
        </table>
        </div>
        <p class="text-muted small mt-3">
            💡 Tip: Leave Unit Price empty to use the base price for all variants.
        </p>
    </div>
    `;
}

function clearVariantsTable() {
    const table = document.querySelector("#variantsSection");
    if (table) table.innerHTML = "";
}