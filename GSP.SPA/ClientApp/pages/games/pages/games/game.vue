<template>
    <div class="col-lg-3 col-md-4 col-sm-12 game-block">
        <v-card>
            <v-card-media :src="game.Photo" height="320px">
            </v-card-media>
            <v-card-title primary-title>
                <div>
                    <h3 class="headline mb-0">{{ game.Name }}</h3>
                    <div>
                        <span class="bold"> Category: </span> {{ game.CategoryName }}
                    </div>
                    <div>
                        <span class="bold"> Price: </span> {{ game.Price }} USD
                    </div>
                </div>
            </v-card-title>
            <v-card-actions>
                <v-btn class="flat-button" color="primary" @click="addGameToBucket">Buy <v-icon right dark>shopping_cart</v-icon> </v-btn>
                <v-btn class="flat-button">Details</v-btn>
            </v-card-actions>
        </v-card>
    </div>
</template>

<script>
    import * as authGetters from "../../../auth/store/types/getter-types";
    import * as authResources from "../../../auth/store/resources";

    import * as ordersActions from "../../../orders/store/types/action-types";
    import * as ordersResources from "../../../orders/store/resources";

    import { mapGetters } from "vuex";

    export default {
        props: {
            game: {
                type: Object,
                required: true
            }
        },
        methods: {
            ...mapGetters({
                getCurrentUser: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                )
            }),
            addGameToBucket() {
                let addGame = {
                    GameId: this.game.GameId,
                    CustomerId: this.getCurrentUser().CustomerId
                };

                this.$store.dispatch(
                    ordersResources.ORDERS_STORE_NAMESPACE.concat(ordersActions.ADD_GAME_TO_BUCKET_ACTION),
                    addGame
                );
            }
        }
    };
</script>
