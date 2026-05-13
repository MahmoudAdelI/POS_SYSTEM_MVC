export const genericModal = new bootstrap.Modal(
    document.getElementById('genericModal')
);

let activeConfig = null;
let context = null;
let extraData = null;

export const setModalState = ({ config, ctx, extra = null }) => {
    activeConfig = config;
    context = ctx;
    extraData = extra;
};

export const getModalState = () => ({ activeConfig, context, extraData });