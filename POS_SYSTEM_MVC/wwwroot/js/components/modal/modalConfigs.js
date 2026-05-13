import loadNewOption from "../../utils/loadNewOption.js";
import loadNewCheckBox from "../../utils/loadNewCheckBox.js";

export const MODAL_CONFIGS = {
    brand: {
        title: 'Add New Brand',
        fields: [
            { name: 'name', label: 'Brand Name', required: true },
        ],
        endpoint: '/api/brand',
        onSuccess: loadNewOption
    },
    category: {
        title: 'Add New Category',
        fields: [
            { name: 'name', label: 'Category Name', required: true },
        ],
        endpoint: '/api/category',
        onSuccess: loadNewOption
    },
    subcategory: {
        title: 'Add New Subcategory',
        fields: [
            { name: 'name', label: 'Subcategory Name', required: true },
        ],
        endpoint: '/api/subcategory',
        onSuccess: loadNewOption,
    },
    unit: {
        title: 'Add New Unit',
        fields: [
            { name: 'name', label: 'Unit Name', required: true }
        ],
        endpoint: '/api/unit',
        onSuccess: loadNewOption
    },
    attribute: {
        title: 'Add New Attribute',
        fields: [{ name: 'name', label: 'Attribute Name', required: true }],
        endpoint: '/api/attribute',
        onSuccess: loadNewOption
    },
    attributeValue: {
        title: 'Add New Value',
        fields: [{ name: 'value', label: 'Value', required: true }],
        endpoint: '/api/attribute/value',
        onSuccess: loadNewCheckBox
    }
};