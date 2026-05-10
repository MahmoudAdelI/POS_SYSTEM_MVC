export const MODAL_CONFIGS = {
    brand: {
        title: 'Add New Brand',
        fields: [
            { name: 'name', label: 'Brand Name', required: true },
        ],
        endpoint: '/api/brand',
        targetSelect: 'brandSelect',
    },
    category: {
        title: 'Add New Category',
        fields: [
            { name: 'name', label: 'Category Name', required: true },
        ],
        endpoint: '/api/category',
        targetSelect: 'categorySelect',
    },
    subcategory: {
        title: 'Add New Subcategory',
        fields: [
            { name: 'name', label: 'Subcategory Name', required: true },
        ],
        endpoint: '/api/subcategory',
        targetSelect: 'subcategorySelect',
        // extra hidden data to send with the request
        extraData: () => ({ categoryId: document.getElementById('categorySelect').value }),
    },
    unit: {
        title: 'Add New Unit',
        fields: [
            { name: 'name', label: 'Unit Name', required: true }
        ],
        endpoint: '/api/unit',
        targetSelect: 'unitSelect',
    },
};