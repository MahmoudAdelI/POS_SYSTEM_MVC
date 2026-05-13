export default function toggleVariantsSection() {
    const hasVariants = document.querySelectorAll('#variantsTable tbody tr').length > 0;
    document.getElementById('variantsSection').classList.toggle('d-none', !hasVariants);
}