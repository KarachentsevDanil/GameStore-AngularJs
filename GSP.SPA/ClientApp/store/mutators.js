export default {
    startLoadingProccess(state, message) {
        state.options.isLoading = true;
        state.options.message = message;
    },
    stopLoadingProccess(state) {
        state.options.isLoading = false;
    }
};
