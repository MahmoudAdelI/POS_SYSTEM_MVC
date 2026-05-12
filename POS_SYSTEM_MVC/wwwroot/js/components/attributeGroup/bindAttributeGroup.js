import { state } from "../../state.js";
import loadAttributesOptions from "../../utils/loadAttributesOptions.js";
import openModal from "../modal/openModal.js";
import syncAttributeSelects from "./syncAttributeSelects.js";
import renderVariants from "../../renderVariants.js";
import renderAttributeValues from "./renderAttributeValues.js";
import toggleAttributesPlaceholder from "../../utils/toggleAttributesPlaceholder.js";
import toggleVariantsSection from "../../utils/toggleVariantsSection.js";
export default function bindAttributeGroup(el) {
    const attributeSelect = el.querySelector('.attribute-type');
    const valueContainer = el.querySelector('.value-checkboxes');
    const newAttributeBtn = el.querySelector('.new-attribute-btn');
    const newValueBtn = el.querySelector('.new-value-btn');
    const removeBtn = el.querySelector('.remove-attribute');

    // populate select from cached state
    loadAttributesOptions(attributeSelect, state.attributes);

    // ── New Attribute ──
    // context is this group's select — onSuccess appends option and fires change
    newAttributeBtn.addEventListener('click', () => {
        openModal('attribute', attributeSelect);
    });

    // ── Attribute select change ──
    attributeSelect.addEventListener('change', () => {
        const attrId = Number(attributeSelect.value);
        const attr = state.attributes.find(a => a.id === attrId);

        renderAttributeValues(attr, valueContainer); // fills checkboxes
        newValueBtn.disabled = !attrId;

        syncAttributeSelects();
        renderVariants();
    });

    // ── New Value ──
    // context is this group's valueContainer — onSuccess appends checkbox
    // extraData computed at click time from the closure
    newValueBtn.addEventListener('click', () => {
        openModal('attributeValue', valueContainer, {
            attributeId: Number(attributeSelect.value),
            SubcategoryId: Number(document.getElementById("subcategorySelect").value)
        });
    });

    // ── Remove ──
    removeBtn.addEventListener('click', () => {
        el.remove();
        syncAttributeSelects();
        renderVariants();
        toggleAttributesPlaceholder();
        toggleVariantsSection();
    });
}