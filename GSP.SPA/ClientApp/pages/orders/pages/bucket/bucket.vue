<template>
    <div class="container game-container">
        <div class="col-lg-9 col-sm-12" v-if="haveGamesInBucket">
            <order-game v-for="game in getGamesInBucket" :game="game" :deleteGameFromBucket="deleteGameFromBucket" :key="game.GameId"></order-game>
        </div>
        <div class="col-lg-3 col-sm-12">
            <v-card class="order-block">
                <div class="deep-purple darken-1">
                    <v-card-title class="white--text deep-purple darken-1">
                        <span class="text-md-center">
                            Order details
                        </span>
                    </v-card-title>
                </div>
                <div class="order-details-block">
                    <p>
                        <span class="bold"> Count Games:</span> {{getCountGames}}
                    </p>
                    <p>
                        <span class="bold"> Total Price:</span> {{getOrderTotal}} USD
                    </p>
                </div>
                <div class="order-complete-block">
                    <v-btn block color="primary" dark @click="completeOrder">Confirm Order <v-icon right dark>shopping_cart</v-icon> </v-btn>
                </div>
            </v-card>
        </div>
    </div>
</template>

<script>
    import * as ordersGetters from "../../store/types/getter-types";
    import * as ordersActions from "../../store/types/action-types";
    import * as ordersResources from "../../store/resources";

    import * as authResources from "../../../auth/store/resources";
    import * as authGetters from "../../../auth/store/types/getter-types";
    import * as orderService from "../../../orders/api/order-service";

    import { mapGetters } from "vuex";

    import orderGameComponent from "../order-game/order-game";

    export default {
        components: {
            orderGame: orderGameComponent
        },
        methods: {
            ...mapGetters({
                getGames: ordersResources.ORDERS_STORE_NAMESPACE.concat(
                    ordersGetters.GET_GAMES_FROM_BUCKET_GETTER
                ),
                getUser: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                )
            }),
            async completeOrder() {
                let user = this.getUser();

                let completeOrderDto = {
                    CustomerId: user.CustomerId
                };

                await orderService.completeOrder(completeOrderDto);

                this.$store.dispatch(
                    ordersResources.ORDERS_STORE_NAMESPACE.concat(
                        ordersActions.GET_GAMES_FROM_BUCKET_ACTION
                    ),
                    user
                );

                this.$router.push("/orders");
            },
            async deleteGameFromBucket(gameId) {
                let user = this.getUser();

                let completeOrderDto = {
                    CustomerId: user.CustomerId,
                    GameId: gameId
                };

                await orderService.deleteGameFromOrder(completeOrderDto);

                this.$store.dispatch(
                    ordersResources.ORDERS_STORE_NAMESPACE.concat(
                        ordersActions.GET_GAMES_FROM_BUCKET_ACTION
                    ),
                    user
                );
            }
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
    .order-block .text-md-center {
        font-weight: bold;
        font-size: 18px;
    }

    .order-block .order-details-block {
        padding: 10px;
    }

    p span.bold {
        font-weight: bold;
    }

    p {
        font-size: 16px;
    }
</style>
