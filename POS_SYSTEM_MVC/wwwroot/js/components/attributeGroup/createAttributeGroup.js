export default function createAttributeGroup() {
    const el = document.createElement("div");
    el.classList.add("bg-light", "rounded", "p-3", "mb-3", "attribute-group");
    el.innerHTML = `
        <div class="d-flex align-items-end gap-3 mb-3">
            <div class="flex-grow-1">
                <label class="form-label small text-muted">Attribute Type</label>
                <div class="d-flex gap-2">
                    <select class="form-select attribute-type">
                        <option value="">Select attribute...</option>
                    </select>
                    <button type="button" class="btn btn-outline-secondary btn-sm new-attribute-btn">
                        <i class="fa-solid fa-plus"></i>
                    </button>
                </div>
            </div>
            <button type="button" class="btn btn-outline-danger btn-md remove-attribute" title="Remove">
                <i class="fa-regular fa-trash-can"></i>
            </button>
        </div>
        <div>
            <label class="form-label small text-muted">Select Values</label>
            <div class="d-flex flex-wrap gap-2 align-items-center value-checkboxes"></div>
            <button type="button"
                class="btn btn-outline-secondary btn-sm mt-2 new-value-btn"
                disabled>
                <i class="fa-solid fa-plus"></i>
            </button>
        </div>
    `;
    return el;
}