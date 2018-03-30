import getters from "./getters";
import mutations from "./mutators";
import actions from "./actions";

const state = {
    games: []
};

const store = {
    namespaced: true,
    state,
    mutations: mutations,
    actions: actions,
    getters: getters
};

export default store;