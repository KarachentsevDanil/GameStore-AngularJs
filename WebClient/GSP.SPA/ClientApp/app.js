import Vue from 'vue'
import Vuetify from 'vuetify'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import pagination from './assets/vue-pagination/vue-pagination'
import vueSlider from 'vue-slider-component';
import BlockUI from 'vue-blockui'
import StarRating from 'vue-star-rating'
import vue2Dropzone from 'vue2-dropzone'
import VueNoty from 'vuejs-noty'

import App from './pages/layout/app-root'

import 'vue2-dropzone/dist/vue2Dropzone.css'
import 'vuejs-noty/dist/vuejs-noty.css'

Vue.prototype.$http = axios;

sync(store, router);

Vue.use(Vuetify);
Vue.use(VueNoty);
Vue.use(BlockUI);

Vue.component('pagination', pagination);
Vue.component('vueDropzone', vue2Dropzone);
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
