import * as mutations from './types/mutators-types';

export default {
    startLoadingProccess(context, message) {
        context.commit(mutations.START_LOADING_PROCCESS_MUTATION, message);
    },
    stopLoadingProccess(context) {
        context.commit(mutations.STOP_LOADING_PROCCESS_MUTATION);
    }
};
