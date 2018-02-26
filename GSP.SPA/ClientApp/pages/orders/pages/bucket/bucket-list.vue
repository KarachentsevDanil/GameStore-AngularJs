<template>
    <v-menu id="bucketMenu" offset-y :close-on-content-click="false">
        <div slot="activator" class="notifications-bar">
            <v-badge color="cyan" right v-if="haveGamesInBucket">
                <span slot="badge"> {{getCountGames}} </span>
                <v-icon large color="grey lighten-1">shopping_cart</v-icon>
            </v-badge>
        </div>
        <div class="main-bucket-block">
            <v-list>
                <template v-for="(game) in getGamesInBucket">
                    <v-list-tile avatar :key="game.GameId">
                        <v-list-tile-avatar>
                            <img :src="game.Icon">
                        </v-list-tile-avatar>
                        <v-list-tile-content>
                            <v-list-tile-title> {{game.Name}} </v-list-tile-title>
                            <v-list-tile-sub-title>
                                <p>
                                    <span class="bold"> {{resources.properties.categoryLabel}} </span> {{game.CategoryName}}
                                </p>
                                <p>
                                    <span class="bold"> {{resources.properties.priceLabel}} </span> {{game.Price}} {{resources.properties.moneyLabel}}
                                </p>
                            </v-list-tile-sub-title>
                        </v-list-tile-content>
                    </v-list-tile>
                </template>
            </v-list>
            <hr>
            <div class="total-block">
                <p>
                    <span class="bold">{{resources.properties.countGamesLabel}} {{getCountGames}} </span>
                </p>
                <p>
                    <span class="bold">{{resources.properties.totalPriceLabel}} {{getOrderTotal}} {{resources.properties.moneyLabel}} </span>
                </p>
            </div>
            <div class="checkout-block">
                <v-btn block color="primary" to="/bucket" dark> {{resources.commands.checkoutOrderLabel}} <v-icon right dark>shopping_cart</v-icon> </v-btn>
            </div>
        </div>
    </v-menu>
</template>

<script>
    import * as ordersGetters from "../../store/types/getter-types";
    import * as ordersActions from "../../store/types/action-types";
    import * as ordersResources from "../../store/resources";
    import * as resources from "../../resources/resources";

    import * as authResources from "../../../auth/store/resources";
    import * as authGetters from "../../../auth/store/types/getter-types";

    import { mapGetters } from "vuex";

    export default {
        data() {
            return {
                resources: {
                    ...resources.lables
                }
            };
        },
        methods: {
            ...mapGetters({
                getGames: ordersResources.ORDERS_STORE_NAMESPACE.concat(
                    ordersGetters.GET_GAMES_FROM_BUCKET_GETTER
                ),
                getUser: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                )
            })
        },
        beforeMount() {
            let user = this.getUser();

            this.$store.dispatch(
                ordersResources.ORDERS_STORE_NAMESPACE.concat(
                    ordersActions.GET_GAMES_FROM_BUCKET_ACTION
                ),
                user
            );
        },
        computed: {
            getOrderTotal() {
                let gamesInBucket = this.getGames();
                let total = _.reduce(
                    gamesInBucket,
                    function (sum, game) {
                        return sum + game.Price;
                    },
                    0
                );

                return total;
            },
            getGamesInBucket() {
                return this.getGames();
            },
            getCountGames() {
                return this.getGames().length;
            },
            haveGamesInBucket() {
                return this.getGames().length > 0;
            }
        }
    };
</script>

<style scoped>
    .menu__content ul li {
        width: 250px;
        margin-bottom: 10px;
    }

    .menu__content ul {
        margin-bottom: 0px;
    }

    .list__tile__sub-title p {
        margin-bottom: 0px;
        margin: 0px;
    }

    .main-bucket-block {
        background-color: white;
    }

        .main-bucket-block .total-block {
            padding: 10px;
        }

        .main-bucket-block hr {
            margin-top: 0px;
            margin-bottom: 0px;
        }

        .main-bucket-block .total-block p {
            margin: 0px;
        }

        .main-bucket-block .checkout-block {
            padding-left: 5px;
            padding-right: 5px;
            padding-bottom: 2px;
        }

    p span.bold {
        font-weight: bold;
    }
</style>
