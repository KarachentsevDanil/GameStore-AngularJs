import Vue from 'vue'
import Vuex from 'vuex'
import authenticationStore from '../pages/auth/store/index';
// import gamesStore from '../pages/games/store/index';
import ordersStore from '../pages/orders/store/index';

Vue.use(Vuex)

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER'

// STATE
const state = {
    counter: 0
}

// MUTATIONS
const mutations = {
    [MAIN_SET_COUNTER](state, obj) {
        state.counter = obj.counter
    }
}

// ACTIONS
const actions = ({
    setCounter({ commit }, obj) {
        commit(MAIN_SET_COUNTER, obj)
    }
})


const store = new Vuex.Store({
    state,
    mutations,
    actions,
    modules: {
        authStore: authenticationStore,
        ordersStore: ordersStore
    }
});

export default store;
