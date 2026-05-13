export default function renderAttributeValues(attr, container) {
    container.innerHTML = attr?.values?.map(v => `
        <input type="checkbox" class="btn-check" id="val-${v.id}" value="${v.id}">
        <label class="btn btn-outline-secondary btn-sm" for="val-${v.id}">${v.value}</label>
    `).join('') ?? '';
}