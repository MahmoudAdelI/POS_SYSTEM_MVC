import fetchAttributes from "./fetchAttributes.js";
import renderNewAttribute from "./renderNewAttribute.js";
import updateUI from "./updateUI.js";

export const cache = {
    attributes: []
}
document.getElementById("addAttributeBtn").addEventListener("click", renderNewAttribute);


document.getElementById("subcategorySelect").addEventListener("change", async (e) => {
    const subCategoryId = Number(e.target.value);
    cache.attributes = await fetchAttributes(subCategoryId);
    console.log(cache.attributes);

    // Refresh any existing attribute groups to reflect the new cached attributes
    const attributeGroups = document.querySelectorAll('.attribute-group');
    attributeGroups.forEach(group => {
        const typeSelect = group.querySelector('.attribute-type');
        const valueContainer = group.querySelector('.value-checkboxes');

        // Remember previous selection
        const prevSelected = Number(typeSelect.value);

        // Rebuild options from cache (keep default placeholder)
        typeSelect.innerHTML = '<option value="">Select attribute...</option>';
        cache.attributes.forEach(attr => {
            const option = document.createElement('option');
            option.value = attr.id;
            option.textContent = attr.name;
            typeSelect.appendChild(option);
        });

        // If the previously selected attribute still exists, restore it and refill its values
        const stillExists = cache.attributes.find(a => a.id === prevSelected);
        if (stillExists) {
            typeSelect.value = String(prevSelected);
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
            typeSelect.value = '';
            valueContainer.innerHTML = '';
        }
    });

    // Clear variants table body so variants are regenerated later when you implement that logic
    const variantsTbody = document.querySelector('#variantsTable tbody');
    if (variantsTbody) variantsTbody.innerHTML = '';

    updateUI();
})