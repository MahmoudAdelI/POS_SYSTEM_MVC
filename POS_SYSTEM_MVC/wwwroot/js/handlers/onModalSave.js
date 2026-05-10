import { getActiveConfig } from "../components/modal/modalState.js";

export default async function onModalSave() {
    const activeConfig = getActiveConfig();
    
    if (!activeConfig) return;


    const body = document.getElementById("genericModal");

    const payload = {};

    body.querySelectorAll("input").forEach(input => {
        payload[input.name] = input.value.trim();
    });

    // Merge any extra data (like categoryId for subcategory)
    if (activeConfig.extraData) {
        Object.assign(payload, activeConfig.extraData());
    }

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

    // Append to the correct dropdown and auto-select it
    const select = document.getElementById(activeConfig.targetSelect);
    const option = new Option(data.name, data.id, true, true);
    select.appendChild(option);

    // Manually fire the change event
    select.dispatchEvent(new Event('change'));

    bootstrap.Modal.getInstance(document.getElementById('genericModal')).hide();
}