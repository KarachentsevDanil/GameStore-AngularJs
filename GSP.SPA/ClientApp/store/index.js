import Vue from "vue";
import Vuex from "vuex";
import authenticationStore from "../pages/auth/store/index";
import ordersStore from "../pages/orders/store/index";

import actions from "./actions";
import getters from "./getters";
import mutations from "./mutators";

Vue.use(Vuex);

const state = {
    options: {
        message: "Please wait ...",
        icon: '<i class="fa fa-cog fa-spin fa-3x fa-fw"></i>',
        isLoading: false
    }
};

const store = new Vuex.Store({
    state: state,
    actions,
    getters,
    mutations,
    modules: {
        authStore: authenticationStore,
        ordersStore: ordersStore
    }
});

export default store;
