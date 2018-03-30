<template>
    <div class="col-lg-6 col-sm-12 game-block">
        <div class="row">
            <div class="col-lg-3 col-md-12 image-block">
                <img class="rounded-photo" :src="game.Photo" height="300" width="220">
            </div>
            <div class="game-details col-lg-7 col-lg-offset-2 col-md-12">
                <h3>
                    {{game.Name}}
                </h3>
                <p>
                    <span class="bold"> {{resources.properties.categoryLabel}}</span> {{game.CategoryName}}
                </p>
                <p>
                    <span class="bold"> {{resources.properties.priceLabel}}</span> {{game.Price}} {{resources.properties.moneyLabel}}
                </p>
                <p>
                    <span class="bold"> {{resources.properties.descriptionLabel}}</span> {{game.Description}}
                </p>
                <v-btn v-if="!hideDeleteAction" right fab dark small color="red" class="pull-right" @click="deleteGameFromBucket(game.GameId)">
                    <i class="icon theme--dark far fa-trash-alt"></i>
                </v-btn>
            </div>
        </div>
    </div>
</template>

<script>
    import * as resources from "../../resources/resources";

    export default {
        data() {
            return {
                resources: {
                    ...resources.lables
                }
            };
        },
        props: {
            game: {
                type: Object,
                required: true
            },
            deleteGameFromBucket: {
                type: Function
            },
            hideDeleteAction: {
                type: Boolean,
                default: false
            }
        }
    };
</script>

<style scoped>
    .game-container .col-lg-6 {
        width: 47%;
    }

    .game-block {
        background: white;
        margin-bottom: 20px;
        border: 3px solid white;
        border-radius: 2px;
        box-shadow: 0px 2px 2px 0 rgba(34, 36, 38, 0.15);
        margin-right: 20px;
    }

        .game-block .image-block {
            padding-top: 5px;
            padding-bottom: 5px;
        }

    @media (min-width: 1600px) {
        .game-block .image-block img {
            height: 300px !important;
            width: 220px !important;
        }
    }

    @media (min-width: 1360px) {
        .game-block .image-block img {
            height: 180px;
            width: 130px;
        }
    }

    .game-block .image-block .rounded-photo {
        border-radius: 5px;
    }

    .game-block h3 {
        margin-top: 5px;
    }

    .game-block .game-details {
        padding-left: 5px;
    }

    p span.bold{
        font-weight: bold;
    }
</style>

