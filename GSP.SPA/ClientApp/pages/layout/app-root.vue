<template>
    <v-app id="app" light>
        <nav-menu params="route: route"></nav-menu>
        <main>
            <v-fade-transition mode="out-in">
                <router-view></router-view>
            </v-fade-transition>
        </main>
    </v-app>
</template>
<script>
    import Vue from 'vue';
    import CounterExample from './counter-example';
    import FetchData from './fetch-data';
    import HomePage from './home-page';
    import NavMenu from './nav-menu';
    import 'vuetify/dist/vuetify.css';

    Vue.component('counter-example', CounterExample);
    Vue.component('fetch-data', FetchData);
    Vue.component('home-page', HomePage);
    Vue.component('nav-menu', NavMenu);

    export default {
        data() {
            return {
            }
        }
    }
</script>
<style>
    .btn {
        padding: 0px;
    }
</style>
