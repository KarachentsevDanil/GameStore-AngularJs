<template>
    <div class="col-lg-12 details-container game-block">
        <div class="game-details-block">

            <div class="photo-parent-block" @click="openGallery">
                <div class="photo-block" :style="{ backgroundImage: `url('${game.Photo}')` }">
                </div>
            </div>

            <div @click="openGallery" class="icons-block">
                <p>
                    <i class="big-icon fal fa-play-circle"></i>
                </p>
            </div>

            <div @click="openGallery" class="text-block">
                {{labels.properties.gameGalleryLabel}}
            </div>

            <hr class="margin-top-5">

            <div class="game-actions">
                <a block v-if="game.IsGameBought" class="download-btn flat-button btn--block btn success btn--router" :href="game.File" :download="getFileName(game.FileExtinction)">
                    <div class="btn__content">
                        {{labels.commands.downloadGameLabel}} <i class="icon download-icon material-icons">cloud_upload</i>
                    </div>
                </a>
                <v-btn v-else block color="primary" @click="buyGame(game.GameId)">
                    {{labels.commands.buyGameLabel}} <v-icon right dark>shopping_cart</v-icon>
                </v-btn>
            </div>
        </div>

        <div class="game-details-block game-details-description">

            <div class="game-details">
                <div class="text">
                    <h2 class="bold">{{ game.Name }}</h2>
                </div>
                <div class="text">
                    <span class="bold"> {{labels.properties.gameOutputCategoryLabel}} </span> {{ game.CategoryName }}
                </div>
                <div class="text">
                    <span class="bold"> {{labels.properties.gameOutputPriceLabel}} </span> {{ game.Price }} {{labels.properties.gameMoney}}
                </div>
                <div class="text">
                    <span class="bold"> {{labels.properties.gameOutputDescriptionLabel}}</span>
                    <p class="text-justify">
                        {{ game.Description }}
                    </p>
                </div>
                <div class="text">
                    <span class="bold"> {{labels.properties.gameOutputRatingLabel}} </span> <star-rating v-model="game.AverageRate" :show-rating="false" :read-only="true" :increment="0.01" :star-size="22"></star-rating> {{ game.AverageRate }} / 5
                </div>
                <div class="text">
                    <span class="bold"> {{labels.properties.gameFeedbacksCountLabel}} </span> {{ getRatesCount }}
                </div>
            </div>
        </div>

        <gallery :images="getPhotos" :index="index" @close="index = null"></gallery>
    </div>
</template>

<script>
    import VueGallery from "vue-gallery";
    import * as resources from "../../../resources/resources";

    export default {
        components: {
            gallery: VueGallery
        },
        data() {
            return {
                index: null,
                labels: resources.gameLabels
            };
        },
        props: {
            game: {
                type: Object,
                required: true
            },
            buyGame: {
                type: Function,
                required: true
            }
        },
        methods: {
            openGallery() {
                this.index = 0;
            },
            getFileName(file){
                let fileExtinction = file.split("/")[1].replace(";","");
                let fileName = `gameFile.${fileExtinction}`;
                return fileName;
            }
        },
        computed: {
            getRatesCount() {
                let countRates = 0;

                if (this.game && this.game.Rates) {
                    countRates = this.game.Rates.length;
                }

                return countRates;
            },
            getPhotos() {
                let photos = [];

                if (this.game && this.game.Photos) {
                    photos = this.game.Photos.map(element => {
                        return element.Photo;
                    });
                }

                return photos;
            }
        }
    };
</script>

<style>
    @media (min-width: 1800px) {
        .details-container.game-block .game-details-block .icons-block {
            left: 170px;
        }

        .details-container.game-block .game-details-block .text-block {
            left: 90px;
        }
    }

    @media (min-width: 1200px) {
        .game-block .game-details-block .icons-block {
            left: 105px;
        }

        .game-block .game-details-block .text-block {
            left: 75px;
        }
    }

    @media (min-width: 768px) {
        .game-details-block .icons-block {
            left: 405px;
        }

        .game-details-block .text-block {
            left: 315px;
        }
    }
    
    .download-btn {
        width: 100% !important; 
    }
    .download-icon {
        margin-left: 3px;
    }

    .margin-top-5 {
        margin-top: 7px !important;
    }

    .game-details-block:hover {
        cursor: pointer;
    }

        .game-details-block:hover .big-icon,
        .game-details-block:hover .text-block {
            color: white;
        }

    .big-icon {
        font-size: 50px;
        color: orange;
    }

    .text-block {
        position: absolute;
        top: 360px;
        left: 90px;
        font-size: 20px;
        color: orange;
    }

    .icons-block {
        position: absolute;
        top: 300px;
        left: 170px;
    }

    .details-container .game-details-block {
        background: white;
        margin-bottom: 20px;
        border: 3px solid white;
        border-radius: 2px;
        box-shadow: 0px 2px 2px 0 rgba(34, 36, 38, 0.15);
        margin-right: 20px;
    }

        .details-container .game-details-block.game-details-description {
            border: none;
            background-color: #3b3b3b;
            font-weight: 400 !important;
            padding-left: 20px;
            padding-top: 20px;
            padding-right: 20px;
        }

            .details-container .game-details-block.game-details-description .text .bold {
                color: #8b8b8b;
            }

        .details-container .game-details-block.game-details-description {
            color: white;
        }

        .details-container .game-details-block .photo-parent-block {
            width: auto;
            height: 460px;
            overflow: hidden;
        }

            .details-container .game-details-block .photo-parent-block:hover .photo-block {
                transform: scale(1.05);
                -webkit-transform: scale(1.05);
                -moz-transform: scale(1.05);
                -o-transform: scale(1.05);
            }

            .details-container .game-details-block .photo-parent-block .photo-block {
                width: 100%;
                height: 100%;
                background-position: center;
                background-size: cover;
                -webkit-transition: all 2s;
                -moz-transition: all 2s;
                -o-transition: all 2s;
                transition: all 2s;
            }

    .game-details-block hr {
        margin: 0px;
    }

    .game-details-block .game-details {
        padding: 10px;
    }

        .game-details-block .game-details h3 {
            margin-top: 2px;
        }
</style>
