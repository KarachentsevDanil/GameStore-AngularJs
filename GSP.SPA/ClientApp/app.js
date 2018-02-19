import Vue from 'vue'
import Vuetify from 'vuetify'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from './pages/layout/app-root'
import pagination from './assets/vue-pagination/vue-pagination'

Vue.prototype.$http = axios;

sync(store, router)

Vue.use(Vuetify);

Vue.component('pagination', pagination)

const app = new Vue({
    store,
    router,
    ...App
})

export {
    app,
    router,
    store
}
