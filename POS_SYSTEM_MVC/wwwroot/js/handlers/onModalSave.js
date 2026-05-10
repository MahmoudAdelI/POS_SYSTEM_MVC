import { genericModal, getModalState } from "../components/modal/modalState.js";

export default async function onModalSave() {
    const { activeConfig, context, extraData } = getModalState();
    if (!activeConfig) return;

    const payload = {};

    const body = document.getElementById("genericModal");
    body.querySelectorAll("input")
        .forEach(input => { payload[input.name] = input.value.trim(); });

    // Merge any extra data (like categoryId for subcategory)
    if (extraData) Object.assign(payload, extraData);


    // Get the anti-forgery token from the hidden field Razor injects
    const token = document.querySelector('[name=__RequestVerificationToken]').value;
    const baseURL = "http://localhost:5050";

    const res = await fetch(baseURL + activeConfig.endpoint, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': token,
        },
        body: JSON.stringify(payload),
    });

    if (!res.ok) return; // handle error as needed

    const data = await res.json(); // expects { id, name }

    activeConfig.onSuccess(data, context);

    genericModal.hide();
}