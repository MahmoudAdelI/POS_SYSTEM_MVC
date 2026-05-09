import collectFormData from "../utils/collectFormData.js";

export default async function onAddProductSubmit(e) {
    e.preventDefault();

    const payload = collectFormData();
    console.log(payload);

    // Basic guard
    if (!payload.variants.length) {
        alert("Please configure at least one variant.");
        return;
    }

    //const res = await fetch("/Product/Add", {
    //    method: "POST",
    //    headers: { "Content-Type": "application/json" },
    //    body: JSON.stringify(payload)
    //});

    //if (res.ok) {
    //    window.location.href = "/Product";
    //} else {
    //    console.error("Failed", await res.text());
    //}
}