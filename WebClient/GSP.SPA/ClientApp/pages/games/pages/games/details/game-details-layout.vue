<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-12 breadcrumb-bar">
                <ol class="breadcrumb breadcrumb-nav-bar">
                    <li>
                        <router-link to="/games">{{labels.headers.gamesLabel}}</router-link>
                    </li>
                    <li class="active">{{game.Name}}</li>
                </ol>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-sm-12">
                <game-details :game="game" :buyGame="addGameToBucket"></game-details>
            </div>
            <div class="col-lg-5 col-sm-12">
                <comments :gameId="id"></comments>
            </div>
            <div class="col-lg-4 col-sm-12">
                <recommendedGames :gameId="id" :buyGame="addGameToBucket"></recommendedGames>
            </div>
        </div>
    </div>
</template>

<script>
    import commentsComponent from "./comments/comments";
    import gameDetailsComponent from "./game-details";
    import recommendedGamesComponent from "./recomended-games";

    import * as gameService from "../../../api/game-service";
    import * as authGetters from "../../../../auth/store/types/getter-types";
    import * as authResources from "../../../../auth/store/resources";
    import * as resources from "../../../resources/resources";

    import * as ordersActions from "../../../../orders/store/types/action-types";
    import * as ordersResources from "../../../../orders/store/resources";

    import { mapGetters } from "vuex";

    export default {
        components: {
            gameDetails: gameDetailsComponent,
            comments: commentsComponent,
            recommendedGames: recommendedGamesComponent
        },
        data() {
            return {
                game: {},
                labels: resources.gameLabels
            };
        },
        props: {
            id: {
                required: true
            }
        },
        async beforeMount() {
            this.game = (await gameService.getGameById(this.id)).data.Data;
        },
        methods: {
            ...mapGetters({
                getCurrentUser: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                )
            }),
            addGameToBucket(gameId) {
                let addGame = {
                    GameId: gameId,
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

<style>
    .container .breadcrumb-bar {
        padding-left: 30px;
    }

    .container .breadcrumb.breadcrumb-nav-bar {
        background-color: white;
        border: 1px solid whitesmoke;
        box-shadow: 0px 2px 2px 0 rgba(34, 36, 38, 0.15);
    }

    .game-details-block hr {
        margin: 0px;
    }

    .header-text {
        font-weight: bold;
        font-size: 18px;
    }

    .add-comment-block {
        padding: 10px;
    }

    .game-block .vue-star-rating {
        display: inline-block !important;
    }

    .game-block .text {
        margin-bottom: 10px;
        font-size: 18px;
    }

    button.flat-button {
        width: 47%;
    }

    a.flat-button {
        width: 47%;
    }

    .game-block .card__title.card__title--primary {
        padding-top: 0px;
        margin-bottom: 6px;
    }

    .game-block .bold {
        margin-top: 3px;
        font-weight: bold;
    }
</style>
