import { state } from "../state.js";
import renderVariants from "../renderVariants.js";
import syncUI from "../utils/syncUI.js";
import syncAttributeSelects from "../utils/syncAttributeSelects.js";
import loadAttributesOptions from "../utils/loadAttributesOptions.js";

export default function onAddAttribute(e) {
    const container = document.getElementById("attributesContainer");
    const element = document.createElement("div");
    element.classList.add("bg-light", "rounded", "p-3", "mb-3", "attribute-group");
    element.innerHTML = `
    <div class="d-flex align-items-end gap-3 mb-3">
        <div class="flex-grow-1">
            <label class="form-label small text-muted">Attribute Type</label>
            <div class="d-flex gap-2">
                <select class="form-select attribute-type">
                    <option value="">Select attribute...</option>
                </select>
                <button type="button" class="btn btn-outline-secondary btn-sm new-attribute-btn">+ New</button>
            </div>
        </div>
        <button type="button" class="btn btn-outline-danger btn-md remove-attribute" title="Remove">
            <i class="fa-regular fa-trash-can"></i>
        </button>
    </div>
    <div>
        <label class="form-label small text-muted">Select Values</label>
        <div class="d-flex flex-wrap gap-2 align-items-center value-checkboxes">
            <!-- dynamically filled by JS -->
        </div>
        <button type="button" class="btn btn-outline-secondary btn-sm mt-2 new-value-btn" disabled>
            + New Value
        </button>
    </div>
`;
    container.appendChild(element);

    // update attribute select input
    const attributeSelectInput = element.querySelector(".attribute-type");
    loadAttributesOptions(attributeSelectInput, state.attributes);
    syncAttributeSelects(); // disable already-picked options in the new select
    syncUI();

    // load attribute values on attribute select change
    attributeSelectInput.addEventListener("change", (e) => {
        const selectedAttrId = Number(e.target.value);
        const selectedAttr = state.attributes.find(a => a.id === selectedAttrId);
        const valueContainer = element.querySelector(".value-checkboxes");

        valueContainer.innerHTML = selectedAttr?.values?.map(v => `
            <input type="checkbox" class="btn-check" id="val-${v.id}" value="${v.id}">
            <label class="btn btn-outline-secondary btn-sm" for="val-${v.id}">${v.value}</label>
        `).join("") ?? "";

        syncAttributeSelects();
    });


    // remove attribute group
    const removeBtn = element.querySelector("button");
    removeBtn.addEventListener("click", () => {
        element.remove()
        syncAttributeSelects();
        syncUI();
        renderVariants();
    });
}