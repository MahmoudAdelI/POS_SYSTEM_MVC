// modalState.js
let activeConfig = null;

export function setActiveConfig(config) {
    activeConfig = config;
}

export function getActiveConfig() {
    return activeConfig;
}