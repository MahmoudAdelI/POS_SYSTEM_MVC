export default function updateUI() {
    const attributeCount = document.querySelectorAll('.attribute-group').length;
    const placeholder = document.getElementById('emptyAttributesPlaceholder');
    const container = document.getElementById('attributesContainer');
    const variantsSection = document.getElementById('variantsSection');

    if (attributeCount === 0) {
        placeholder.classList.remove('d-none');
        container.classList.add('d-none');
        variantsSection.classList.add('d-none');
    } else {
        placeholder.classList.add('d-none');
        container.classList.remove('d-none');
        //generateVariantTable(); // Your function to populate the variants table
        if (document.querySelectorAll('#variantsTable tbody tr').length > 0) {
            variantsSection.classList.remove('d-none');
        }
    }

}