import collectFormData from "../utils/collectFormData.js";
import syncUI from "../utils/syncUI.js";

export default async function onAddProductSubmit(e, token) {
    e.preventDefault();

    const payload = collectFormData();

    // Basic guard
    if (!payload.variants.length) {
        alert("Please configure at least one variant.");
        return;
    }

    const res = await fetch("/products/create", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(payload)
    });

    if (res.ok) {
        document.querySelector("form").reset();
        syncUI();
    } else {
        console.error("Failed", await res.text());
    }
}