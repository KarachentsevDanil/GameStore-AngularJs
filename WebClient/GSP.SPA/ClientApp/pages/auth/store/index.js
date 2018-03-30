import getters from "./getters";
import mutations from "./mutators";
import actions from "./actions";

const state = {
    user: {},
    token: "",
    tokenExpirationDate: ""
};

const store = {
    namespaced: true,
    state,
    mutations: mutations,
    actions: actions,
    getters: getters
};

export default store;
