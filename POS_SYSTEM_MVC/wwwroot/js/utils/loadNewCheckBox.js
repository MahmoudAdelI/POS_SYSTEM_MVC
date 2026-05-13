export default function loadNewCheckBox(data, context) {
    context.insertAdjacentHTML('beforeend', `
            <input type="checkbox" class="btn-check" id="val-${data.id}" value="${data.id}" checked>
            <label class="btn btn-outline-secondary btn-sm" for="val-${data.id}">${data.value}</label>
        `);
}