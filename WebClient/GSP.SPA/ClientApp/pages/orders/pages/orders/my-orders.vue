<template>
    <div>
        <orders :customerId="getCurrentUser.CustomerId"></orders>
    </div>
</template>

<script>
    import * as authResources from "../../../auth/store/resources";
    import * as authGetters from "../../../auth/store/types/getter-types";

    import { mapGetters } from "vuex";

    import ordersComponent from "./orders.vue";

    export default {
        components: {
            orders: ordersComponent
        },
        methods: {
            ...mapGetters({
                getUser: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                )
            })
        },
        computed: {
            getCurrentUser() {
                return this.getUser();
            }
        }
    };
</script>
