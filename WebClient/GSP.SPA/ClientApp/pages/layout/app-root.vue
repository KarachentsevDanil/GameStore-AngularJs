<template>
    <v-app id="app" light v-cloak>
        <nav-menu params="route: route" v-if="getToken"></nav-menu>
        <BlockUI v-if="blockUiOptions && blockUiOptions.isLoading" :message="blockUiOptions.message" :html="blockUiOptions.icon"></BlockUI>
        <main class="main-block">
            <v-fade-transition mode="out-in">
                <router-view></router-view>
            </v-fade-transition>
        </main>
    </v-app>
</template>
<script>
    import Vue from "vue";
    import NavMenu from "./nav-menu";
    import "vuetify/dist/vuetify.css";

    import * as authGetters from "../auth/store/types/getter-types";
    import * as authResources from "../auth/store/resources";

    import * as mainStoreGetters from '../../store/types/action-types';

    import { mapGetters } from "vuex";

    Vue.component("nav-menu", NavMenu);

    export default {
        data() {
            return {};
        },
        computed: {
            ...mapGetters({
                getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                ),
                getToken: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_TOKEN_GETTER
                ),
                blockUiOptions: 'getLoaderOptions'
            })
        }
    };
</script>
<style>
    .btn {
        padding: 0px;
    }

    .main-block {
        margin-top: 60px;
    }
</style>
