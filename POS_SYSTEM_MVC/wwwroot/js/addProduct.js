import onSubcategoryChange from "./handlers/onSubcategoryChange.js";
import onAddProductSubmit from "./handlers/onAddProductSubmit.js";
import onAddAttribute from "./handlers/onAddAttribute.js";
import renderVariants from "./renderVariants.js";

const addAttributeBtn = document.getElementById("addAttributeBtn");
addAttributeBtn.addEventListener("click", onAddAttribute);


document.getElementById("subcategorySelect").addEventListener("change", onSubcategoryChange);

const attributeContainer = document.getElementById("attributesContainer");
attributeContainer.addEventListener("change", renderVariants);

document.querySelector("form").addEventListener("reset", () => {
    document.getElementById("variantsSection").innerHTML = "";
});

document.querySelector("form").addEventListener("submit", onAddProductSubmit);