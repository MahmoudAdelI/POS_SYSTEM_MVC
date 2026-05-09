import loadAttributesOptions  from "../utils/loadAttributesOptions.js";
import fetchAttributes  from "../services/fetchAttributes.js";
import syncUI from "../utils/syncUI.js";
import renderVariants from "../renderVariants.js";
import { state } from "../state.js";

export default async function onSubcategoryChange(e) {
    const subCategoryId = Number(e.target.value);
    state.attributes = await fetchAttributes(subCategoryId);

    // Refresh any existing attribute groups to reflect the new cached attributes
    const attributeGroups = document.querySelectorAll('.attribute-group');
    attributeGroups.forEach(group => {
        const attributeSelectInput = group.querySelector('.attribute-type');
        const valueContainer = group.querySelector('.value-checkboxes');

        // Remember previous selection
        const prevSelected = Number(attributeSelectInput.value);

        // Rebuild options from cache (keep default placeholder)
        attributeSelectInput.innerHTML = '<option value="">Select attribute...</option>';
        loadAttributesOptions(attributeSelectInput, state.attributes);

        // If the previously selected attribute still exists, restore it and refill its values
        const stillExists = state.attributes.find(a => a.id === prevSelected);
        if (stillExists) {
            attributeSelectInput.value = String(prevSelected);
            if (stillExists.values) {
                valueContainer.innerHTML = stillExists.values.map(v => `
                    <input type="checkbox" class="btn-check" id="val-${v.id}" value="${v.id}">
                    <label class="btn btn-outline-secondary btn-sm" for="val-${v.id}">${v.value}</label>
                `).join("");
            } else {
                valueContainer.innerHTML = '';
            }
        } else {
            // Reset selection and clear values
            attributeSelectInput.value = '';
            valueContainer.innerHTML = '';
        }
    });

    // Clear variants table body so variants are regenerated later when you implement that logic
    const variantsTbody = document.querySelector('#variantsTable tbody');
    if (variantsTbody) variantsTbody.innerHTML = '';

    syncUI();
    renderVariants(); // Trigger re-render to update variants based on new attributes
}