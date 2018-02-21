import Vue from 'vue'
import Vuetify from 'vuetify'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from './pages/layout/app-root'
import pagination from './assets/vue-pagination/vue-pagination'
import vueSlider from 'vue-slider-component';
import BlockUI from 'vue-blockui'
import StarRating from 'vue-star-rating'

Vue.prototype.$http = axios;

sync(store, router);

Vue.use(Vuetify);
Vue.use(BlockUI);

Vue.component('pagination', pagination);
Vue.component('vueSlider', vueSlider);
Vue.component('starRating', StarRating);

const app = new Vue({
    store,
    router,
    ...App
});

export {
    app,
    router,
    store
}
