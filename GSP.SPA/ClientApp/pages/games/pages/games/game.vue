<template>
    <div class="col-sm-12 game-block">
        <v-card>
            <v-card-media :src="game.Photo" height="420px">
            </v-card-media>
            <v-card-title primary-title>
                <div>
                    <h3 class="headline mb-0">{{ game.Name }}</h3>
                    <div>
                        <span class="bold"> {{labels.properties.gameOutputCategoryLabel}} </span> {{ game.CategoryName }}
                    </div>
                    <div>
                        <span class="bold"> {{labels.properties.gameOutputPriceLabel}} </span> {{ game.Price }} {{labels.properties.gameMoney}}
                    </div>
                    <div>
                        <span class="bold"> {{labels.properties.gameOutputRatingLabel}} </span> <star-rating v-model="game.AverageRate" :show-rating="false" :read-only="true" :increment="0.01" :star-size="16"></star-rating> {{ game.AverageRate }} / 5
                    </div>
                </div>
            </v-card-title>
            <v-card-actions>
                <v-btn class="flat-button" color="primary" @click="addGameToBucket">
                    {{labels.commands.buyGameLabel}} <v-icon right dark>shopping_cart</v-icon>
                </v-btn>
                <v-btn v-if="!isDetailsPage" class="flat-button" :to="'/game-details/' + game.GameId">
                    {{labels.commands.gameDetailsLabel}}
                </v-btn>
            </v-card-actions>
        </v-card>
    </div>
</template>

<script>
    import * as authGetters from "../../../auth/store/types/getter-types";
    import * as authResources from "../../../auth/store/resources";
    import * as resources from "../../resources/resources";

    import * as ordersActions from "../../../orders/store/types/action-types";
    import * as ordersResources from "../../../orders/store/resources";

    import { mapGetters } from "vuex";

    export default {
        data() {
            return {
                labels: resources.gameLabels
            };
        },
        props: {
            game: {
                type: Object,
                required: true
            },
            isDetailsPage: {
                type: Boolean,
                required: false
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
                    CustomerId: this.getCurrentUser().CustomerId,
                    notify: this.$noty
                };

                this.$store.dispatch(
                    ordersResources.ORDERS_STORE_NAMESPACE.concat(
                        ordersActions.ADD_GAME_TO_BUCKET_ACTION
                    ),
                    addGame
                );
            }
        }
    };
</script>

<style scoped>
    @media (min-width: 1600px) {
        div.game-block {
            width: 25% !important;
        }
    }

    @media (min-width: 1300px) {
        div.game-block {
            width: 33.3%;
        }
    }

    .game-block .headline {
        font-size: 18px;
        font-weight: bold;
    }

    .game-block .vue-star-rating {
        display: inline-block !important;
    }

    .game-block .card__title.card__title--primary {
        padding-top: 0px;
        margin-bottom: 0px;
        padding-bottom: 4px;
    }

    button.flat-button {
        width: 47%;
    }

    a.flat-button {
        width: 47%;
    }

    .game-block {
        margin-bottom: 25px;
    }

        .game-block .bold {
            margin-top: 3px;
            font-weight: bold;
        }
</style>
