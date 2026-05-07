import { cache } from "./addProduct.js";
import updateUI from "./updateUI.js";

export default function renderNewAttribute(e) {
    const container = document.getElementById("attributesContainer");
    const element = document.createElement("div");
    element.classList.add("bg-light", "rounded", "p-3", "mb-3", "attribute-group");
    element.innerHTML = `
        <div class="d-flex align-items-end gap-3 mb-3">
            <div class="flex-grow-1">
                <label class="form-label small text-muted">Attribute Type</label>
                <select class="form-select attribute-type">
                    <option value="">Select attribute...</option>
                    
                </select>
            </div>
            <button type="button" class="btn btn-outline-danger btn-md mt-4 remove-attribute" title="Remove">
                <!-- trash icon -->
                <i class="fa-regular fa-trash-can"></i>
            </button>
        </div>
        <div>
            <label class="form-label small text-muted">Select Values</label>
            <div class="d-flex flex-wrap gap-2 value-checkboxes">
                <!-- dynamically filled by JS -->
                <!-- ... -->
            </div>
        </div>
    `
    container.appendChild(element);
    updateUI();

    const typeSelect = element.querySelector(".attribute-type");
    cache.attributes.forEach(attr => {
        const option = document.createElement("option");
        option.value = attr.id;
        option.textContent = attr.name;
        typeSelect.appendChild(option);
    });


    // When attribute type changes, show its values
    typeSelect.addEventListener("change", (e) => {
        const selectedAttrId = Number(e.target.value);
        const selectedAttr = cache.attributes.find(a => a.id === selectedAttrId);
        const valueContainer = element.querySelector(".value-checkboxes");

        if (selectedAttr && selectedAttr.values) {
            valueContainer.innerHTML = selectedAttr.values.map(v => `
                <input type="checkbox" class="btn-check" id="val-${v.id}" value="${v.id}">
                <label class="btn btn-outline-secondary btn-sm" for="val-${v.id}">${v.value}</label>
            `).join("");
        } else {
            valueContainer.innerHTML = "";
        }
    });



    const removeBtn = element.querySelector("button");
    removeBtn.addEventListener("click", () => {
        element.remove()
        updateUI();
    });

    
}