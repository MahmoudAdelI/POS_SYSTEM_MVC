export default async function fetchAttributes(subCategoryId) {
    const res = await fetch("http://localhost:5050/api/subcategory/attributes/" + subCategoryId);
    const data = await res.json();
    return data;
}