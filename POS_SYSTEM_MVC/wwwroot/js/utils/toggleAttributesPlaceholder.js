export default function toggleAttributesPlaceholder() {
    const hasGroups = document.querySelectorAll('.attribute-group').length > 0;
    document.getElementById('emptyAttributesPlaceholder').classList.toggle('d-none', hasGroups);
    document.getElementById('attributesContainer').classList.toggle('d-none', !hasGroups);
}