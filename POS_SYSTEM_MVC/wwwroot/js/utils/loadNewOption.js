export default function loadNewOption(data, context) {
    const option = new Option(data.name, data.id, true, true);
    context.appendChild(option);
    context.dispatchEvent(new Event('change'));
}