import { state } from "../state.js";
import fetchAttributes  from "../services/fetchAttributes.js";
import loadAttributesOptions  from "../utils/loadAttributesOptions.js";
import renderAttributeValues from "../components/attributeGroup/renderAttributeValues.js";
import syncAttributeSelects from "../components/attributeGroup/syncAttributeSelects.js";
import renderVariants from "../renderVariants.js";

export default async function onSubcategoryChange(e) {
    const subCategoryId = Number(e.target.value);
    state.attributes = await fetchAttributes(subCategoryId);

    // rebuild each existing group against the new attribute list
    document.querySelectorAll('.attribute-group').forEach(group => {
        const attributeSelect = group.querySelector('.attribute-type');
        const valueContainer = group.querySelector('.value-checkboxes');
        const newValueBtn = group.querySelector('.new-value-btn');

        const prevId = Number(attributeSelect.value);

        // rebuild options
        attributeSelect.innerHTML = '<option value="">Select attribute...</option>';
        loadAttributesOptions(attributeSelect, state.attributes);

        // restore previous selection if it still exists
        const stillExists = state.attributes.find(a => a.id === prevId);
        if (stillExists) {
            attributeSelect.value = String(prevId);
            renderAttributeValues(stillExists, valueContainer);
        } else {
            attributeSelect.value = '';
            valueContainer.innerHTML = '';
            newValueBtn.disabled = true;
        }
    });

    syncAttributeSelects();
    renderVariants();
}