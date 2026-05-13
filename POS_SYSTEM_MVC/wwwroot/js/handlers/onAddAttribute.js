import createAttributeGroup from "../components/attributeGroup/createAttributeGroup.js";
import bindAttributeGroup from "../components/attributeGroup/bindAttributeGroup.js";
import syncAttributeSelects from "../components/attributeGroup/syncAttributeSelects.js";
import toggleAttributesPlaceholder from "../utils/toggleAttributesPlaceholder.js";

export default function onAddAttribute(e) {
    const container = document.getElementById("attributesContainer");
    const el = createAttributeGroup();
    container.appendChild(el);
    bindAttributeGroup(el);
    syncAttributeSelects();
    toggleAttributesPlaceholder();
}