export default function loadAttributesOptions(inputItem, src) {
    src.forEach(attr => {
        const option = document.createElement("option");
        option.value = attr.id;
        option.textContent = attr.name;
        inputItem.appendChild(option);
    });
}