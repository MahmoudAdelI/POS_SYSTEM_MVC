import fileToBase64 from "./fileToBase64.js";
export default async function collectFormData() {

    const name = document.getElementById("productName").value.trim();
    const brandId = Number(document.getElementById("brandSelect").value);
    const basePrice = Number(document.getElementById("BasePriceSelect").value);
    const subcategoryId = Number(document.getElementById("subcategorySelect").value);
    const unitId = Number(document.getElementById("unitSelect").value);
    const imageFile = document.getElementById("imageInput").files[0];
    const image = imageFile ? {
        data: await fileToBase64(imageFile),
        mimeType: imageFile.type,      // "image/jpeg", "image/png" etc.
        fileName: imageFile.name
    } : null;
    const variants = [];

    document.querySelectorAll("#variantsTable tbody tr").forEach(row => {
        const attributeValues = JSON.parse(row.dataset.combo);  // [3, 7]
        const price = Number(row.querySelector(".variant-price").value) || basePrice;
        const stock = Number(row.querySelector(".variant-stock").value);

        variants.push({ price, stock, attributeValues });
    });

    return {
        name,
        brandId,
        basePrice,
        subcategoryId,
        unitId,
        image,
        variants
    };
}