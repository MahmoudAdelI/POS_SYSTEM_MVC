import onSubcategoryChange from "./handlers/onSubcategoryChange.js";
import onAddProductSubmit from "./handlers/onAddProductSubmit.js";
import onAddAttribute from "./handlers/onAddAttribute.js";
import renderVariants from "./renderVariants.js";
import openModal from "./components/modal/openModal.js";
import onModalSave from "./handlers/onModalSave.js";

const addAttributeBtn = document.getElementById("addAttributeBtn");
addAttributeBtn.addEventListener("click", onAddAttribute);


document.getElementById("subcategorySelect").addEventListener("change", onSubcategoryChange);

const attributeContainer = document.getElementById("attributesContainer");
attributeContainer.addEventListener("change", renderVariants);

document.querySelector("form").addEventListener("reset", () => {
    document.getElementById("variantsSection").innerHTML = "";
});

const token = document.querySelector("[name='__RequestVerificationToken']").value;
document.querySelector("form").addEventListener("submit", (e) => {
    onAddProductSubmit(e, token);
});

document.querySelector("form").addEventListener("click", (e) => {
    const btn = e.target.closest('[data-modal]');
    if (!btn) return;

    const key = btn.dataset.modal;
    const targetId = btn.dataset.target;
    const context = document.getElementById(targetId);

    // each button declares what extra data it needs via data attributes
    const extraData = btn.dataset.parentId
        ? { [btn.dataset.parentKey]: Number(document.getElementById(btn.dataset.parentId).value) }
        : null;

    openModal(key, context, extraData);
})

document.getElementById("genericModalSave").addEventListener("click", (activeConfig) => {
    onModalSave();
});