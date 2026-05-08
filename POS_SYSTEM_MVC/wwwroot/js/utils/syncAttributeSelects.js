import { state } from "../state.js";

export default function syncAttributeSelects() {
    const allSelects = [...document.querySelectorAll(".attribute-type")];
    const selectedValues = new Set(
        allSelects.map(s => s.value).filter(v => v !== "")
    );

    allSelects.forEach(select => {
        [...select.options].forEach(option => {
            if (option.value === "") return; // skip placeholder
            // disable if selected elsewhere (but not in this select itself)
            option.disabled = selectedValues.has(option.value) && option.value !== select.value;
        });
    });

    // Toggle add button
    const addBtn = document.getElementById("addAttributeBtn");
    const allUsed = allSelects.length >= state.attributes.length;
    addBtn.classList.toggle("disabled", allUsed);
}