<template>
    <div>
        <games :customerId="getUser.CustomerId" :hideModeFilter="hideModeFilter">
        </games>
    </div>
</template>

<script>
    import gamesComponent from "./games";

    import * as authGetters from "../../../auth/store/types/getter-types";
    import * as authResources from "../../../auth/store/resources";

    import { mapGetters } from "vuex";

    export default {
        components: {
            games: gamesComponent
        },
        data() {
            return {
                hideModeFilter: true
            };
        },
        methods: {
            ...mapGetters({
                getCurrentUser: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                )
            })
        },
        computed: {
            getUser() {
                return this.getCurrentUser();
            }
        }
    };
</script>
