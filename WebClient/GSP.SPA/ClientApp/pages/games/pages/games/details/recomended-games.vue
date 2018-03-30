<template>
    <v-card class="game-details-block">
        <div class="form-header deep-purple darken-1">
            <v-card-title class="white--text deep-purple darken-1">
                <span class="text-xs-center header-text">
                    {{labels.headers.recomendedGamesLabel}}
                </span>
            </v-card-title>
        </div>
        <div class="form-body">
            <div class="recomended-game-block">
                <div class="col-lg-12 game-block recomender-dame" v-for="game in recomendedGames" :key="game.GameId">
                    <div class="row">
                        <div class="col-lg-3 col-md-12 image-block">
                            <div class="parent-photo-div">
                                <div class="photo-block" :style="{ backgroundImage: `url('${game.Photo}')` }">
                                </div>
                            </div>
                        </div>
                        <div class="game-details col-lg-8 col-md-12">
                            <h3>
                                {{game.Name}}
                            </h3>
                            <p>
                                <span class="bold"> {{labels.properties.gameOutputCategoryLabel}}</span> {{game.CategoryName}}
                            </p>
                            <p>
                                <span class="bold"> {{labels.properties.gameOutputPriceLabel}}</span> {{game.Price}} {{labels.properties.gameMoney}}
                            </p>
                            <p>
                                <span class="bold">{{labels.properties.gameOutputDescriptionLabel}} </span> {{game.Description}}
                            </p>
                            <v-btn right fab dark color="primary" class="pull-right" @click="addGameToBucket(game.GameId)">
                                <i class="icon theme--dark far fa-cart-arrow-down"></i>
                            </v-btn>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </v-card>
</template>

<script>
    import * as gameService from "../../../api/game-service";
    import * as resources from "../../../resources/resources";

    export default {
        props: {
            gameId: {
                required: true
            },
            buyGame: {
                type: Function,
                required: true
            }
        },
        data() {
            return {
                recomendedGames: [],
                labels: resources.gameLabels
            };
        },
        async beforeMount() {
            this.recomendedGames = (await gameService.getRecomendedGamesById(
                this.gameId
            )).data.Data;
        }
    };
</script>

<style>
    .parent-photo-div {
        width: 150px;
        height: 210px;
        overflow: hidden;
    }

        .parent-photo-div:hover .photo-block {
            transform: scale(1.05);
            -webkit-transform: scale(1.05);
            -moz-transform: scale(1.05);
            -o-transform: scale(1.05);
        }

    .photo-block {
        width: 100%;
        height: 100%;
        background-position: center;
        background-size: cover;
        -webkit-transition: all 0.5s;
        -moz-transition: all 0.5s;
        -o-transition: all 0.5s;
        transition: all 0.5s;
    }

    .recomender-dame {
        background-color: white;
        border-bottom: whitesmoke 1px solid;
    }

    .recomender-dame {
        padding-top: 10px;
        padding-bottom: 5px;
    }

        .recomender-dame .game-details {
            margin-left: 5%;
        }
</style>
