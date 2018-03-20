<template>
    <div class="container">
        <div class="col-lg-3 col-sm-12">
            <filters :filters="filters" :categories="categories" :loadGamesFunc="loadGamesByParams"></filters>

            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
            <div class="games-info">
                <div v-for="(game) in games" :class="{'game-details' : true, active: isActive(game.GameId)}" :key="game.GameId" @click="selectGame(game)">
                    <h4>
                        {{labels.properties.gameNumberLabel}} {{ game.GameId }}
                    </h4>
                    <p>
                        <span class="bold"> {{labels.properties.gameOutputNameLabel}} </span> {{ game.Name }}
                    </p>
                    <p>
                        <span class="bold"> {{labels.properties.gameOutputCategoryLabel}} </span> {{game.CategoryName}}
                    </p>
                    <p>
                        <span class="bold"> {{labels.properties.gameOutputPriceLabel}} </span> {{ game.Price }} {{labels.properties.gameMoney}}
                    </p>
                </div>
            </div>
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
        </div>
        <div class="col-lg-9 col-sm-12">
            <edit-game-layout ref="gameLayout" :refreshCategories="refreshCategories" :categories="categories" v-if="currentGame.GameId" :refreshGameAfterUpdate="refreshGameAfterUpdate" :game="currentGame"></edit-game-layout>
        </div>
    </div>
</template>

<script>
    import * as gameService from "../../api/game-service";
    import * as categoryService from "../../api/category-service";
    import * as mainStoreActions from "../../../../store/types/action-types";
    import * as resources from "../../resources/resources";

    import editGameLayoutComponent from "./edit/edit-game-layout";
    import filtersComponent from "./filters/filters";

    export default {
        components: {
            editGameLayout: editGameLayoutComponent,
            filters: filtersComponent
        },
        data() {
            return {
                labels: resources.gameLabels,
                games: [],
                currentGame: {},
                categories: [],
                filters: {
                    isApply: false,
                    selectedCategories: [],
                    title: "",
                    outputMode: 0,
                    priceRange: [0, 200],
                    pagination: {
                        total: 0,
                        pageSize: 12,
                        currentPage: 1,
                        paginationOptions: {
                            offset: 3,
                            previousText: "Prev",
                            nextText: "Next",
                            alwaysShowPrevNext: true
                        }
                    }
                }
            };
        },
        async beforeCreate() {
            this.$store.dispatch(
                mainStoreActions.START_LOADING_ACTION,
                "Games are loading ..."
            );

            let params = {
                PageSize: 12,
                PageNumber: 1
            };

            let gamesResponse = (await gameService.getGames(params)).data.Data;

            this.games = gamesResponse.Collection;
            this.filters.pagination.total = gamesResponse.TotalCount;
            this.categories = (await categoryService.getCategories()).data.Data;

            this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
        },
        methods: {
            selectGame(game) {
                this.currentGame = {...game};

                if (this.$refs.gameLayout) {
                    this.$refs.gameLayout.$refs.editGame.clearGame();
                }
            },
            getFilterParams() {
                return {
                    PageSize: 12,
                    PageNumber: 1,
                    CategoriesIds: this.filters.selectedCategories,
                    Term: this.filters.title,
                    OutputMode: this.filters.outputMode,
                    StartPrice: Math.min(...this.filters.priceRange),
                    EndPrice: Math.max(...this.filters.priceRange),
                    CustomerId: this.customerId
                };
            },
            async refreshCategories() {
                this.categories = (await categoryService.getCategories()).data.Data;
            },
            async loadGamesByParams(params) {
                this.$store.dispatch(
                    mainStoreActions.START_LOADING_ACTION,
                    "Games are loading ..."
                );

                let gamesResponse = (await gameService.getGames(params)).data.Data;

                this.games = gamesResponse.Collection;
                this.filters.pagination.total = gamesResponse.TotalCount;

                this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
            },
            pageChanged(page) {
                this.filters.pagination.currentPage = page;

                let params = this.getFilterParams();
                params.PageNumber = page;

                this.loadGamesByParams(params);
            },
            refreshGameAfterUpdate() {
                let params = this.getFilterParams();
                this.currentGame = {};

                params.PageNumber = this.filters.pagination.currentPage;
                this.loadGamesByParams(params);
            },
            isActive(gameId) {
                return this.currentGame.GameId && this.currentGame.GameId === gameId;
            }
        }
    };
</script>

<style scoped>
    .games-info {
        margin-top: 10px;
        margin-bottom: 10px;
    }

    .game-details {
        background: white;
        border-bottom: 2px solid whitesmoke;
        padding: 10px;
        box-shadow: 0px 2px 2px 0 rgba(34, 36, 38, 0.15);
    }

        .game-details:hover {
            background-color: whitesmoke;
            cursor: pointer;
        }

        .game-details.active {
            background-color: whitesmoke;
        }

        .game-details p {
            margin-bottom: 2px;
        }

    p span.bold {
        font-weight: bold;
    }

    p {
        font-size: 14px;
    }
</style>
