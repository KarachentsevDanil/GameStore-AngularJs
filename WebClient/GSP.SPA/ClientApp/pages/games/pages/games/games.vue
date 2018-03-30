<template>
    <div class="container">
        <div class="col-lg-3 col-sm-12 filter-block">
            <filtersBlock :filters="filters" :customerId="customerId" :hideModeFilter="hideModeFilter" :categories="categories" :loadGamesFunc="loadGamesByParams"></filtersBlock>
        </div>
        <div class="col-lg-9 col-sm-12 games-block" v-if="games.length > 0">
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
            <div class="col-lg-12 games-div">
                <gameBlock v-for="game in games" :key="game.GameId" :game="game">
                </gameBlock>
            </div>
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
        </div>
    </div>
</template>

<script>
    import * as gameService from "../../api/game-service";
    import * as categoryService from "../../api/category-service";
    import * as mainStoreActions from "../../../../store/types/action-types";
    import * as resources from "../../resources/resources";

    import gameBlockComponent from "./game";
    import filtersBlockComponent from "./filter/filters";
    import gamesMixin from "./mixins/games-mixin";

    export default {
        components: {
            gameBlock: gameBlockComponent,
            filtersBlock: filtersBlockComponent
        },
        props: {
            customerId: {
                type: String
            },
            hideModeFilter: {
                type: Boolean,
                default: false
            }
        },
        data() {
            return {
                labels: resources.gameLabels,
                games: [],
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
        async beforeMount() {
            this.$store.dispatch(
                mainStoreActions.START_LOADING_ACTION,
                "Games are loading ..."
            );

            let params = {
                PageSize: 12,
                PageNumber: 1,
                CustomerId: this.customerId
            };

            let gamesResponse = (await gameService.getGames(params)).data.Data;
            this.games = gamesResponse.Collection;
            this.filters.pagination.total = gamesResponse.TotalCount;

            let categoriesResponse = await categoryService.getCategories();
            this.categories = categoriesResponse.data.Data;

            this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
        },
        methods: {
            pageChanged(page) {
                this.filters.pagination.currentPage = page;

                let params = this.getFilterParams();
                params.PageNumber = page;

                this.loadGamesByParams(params);
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
            async loadGamesByParams(params) {
                this.$store.dispatch(
                    mainStoreActions.START_LOADING_ACTION,
                    "Games are loading ..."
                );

                let gamesResponse = (await gameService.getGames(params)).data.Data;

                this.games = gamesResponse.Collection;
                this.filters.pagination.total = gamesResponse.TotalCount;

                this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
            }
        }
    };
</script>

<style>
    .games-block .padding-10 {
        margin-bottom: 10px;
        margin-left: 15px;
    }

    .games-block .card__title.card__title--primary {
        padding-top: 6px;
        margin-bottom: 6px;
    }

    .games-block .headline.mb-0 {
        margin-top: 4px;
    }

    .games-block .games-div {
        padding-left: 0px;
    }

    .filter-block .form-body {
        padding: 15px;
    }

    .filter-block .filter-section {
        margin-bottom: 5px;
    }

    .filter-block .sub-title {
        font-size: 18px;
        font-weight: bold;
    }

    .filter-block .title-filter {
        margin-bottom: 0px;
    }

    .filter-block .input-group__details {
        min-height: 0px;
    }

    .filter-block .price-range {
        margin-top: 35px;
    }
</style>
