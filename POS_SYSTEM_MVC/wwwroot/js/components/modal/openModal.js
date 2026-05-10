import { MODAL_CONFIGS } from "./modalConfigs.js";
import { setActiveConfig } from "../modal/modalState.js";
export default function openModal(key) {
    const config = MODAL_CONFIGS[key];
    if (!config) return;

    setActiveConfig(config);

    document.getElementById('genericModalTitle').textContent = config.title;

    // Render fields dynamically
    document.getElementById('genericModalBody').innerHTML = config.fields.map(f => `
    <div class="mb-3">
      <label class="form-label">${f.label}</label>
      <input type="text" class="form-control" name="${f.name}" ${f.required ? 'required' : ''} />
    </div>
  `).join('');

    new bootstrap.Modal(document.getElementById('genericModal')).show();
}
