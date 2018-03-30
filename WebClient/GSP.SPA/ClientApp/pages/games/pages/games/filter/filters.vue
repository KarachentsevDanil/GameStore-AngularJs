<template>
    <v-card class="form-sign-block">
        <div class="form-header deep-purple darken-1">
            <v-card-title class="white--text deep-purple darken-1">
                <span class="text-xs-center sub-title">
                    {{labels.headers.filtersLabel}}
                </span>
                <v-btn v-if="hasFilter" small color="primary" class="pull-right" @click="applyFilters">Apply</v-btn>
                <v-btn v-if="filters.isApply" small color="error" class="pull-right" @click="clearFilters">Clear</v-btn>
            </v-card-title>
        </div>
        <div class="form-body">
            <div class="filter-section" v-if="!hideModeFilter">
                <p class="sub-title title-filter">
                    {{labels.properties.filterSortLabel}}
                </p>
                <v-select :items="modes"
                          v-model="filters.outputMode"
                          label="Select Mode"
                          single-line
                          item-text="text"
                          item-value="id"></v-select>
            </div>
            <hr v-if="!hideModeFilter">
            <div class="filter-section">
                <p class="sub-title title-filter">
                    {{labels.properties.filterTitleLabel}}
                </p>
                <v-text-field v-model="filters.title" :label="labels.properties.fitlerTitleSearchLabel"></v-text-field>
            </div>
            <hr>
            <div class="filter-section">
                <p class="sub-title">
                    {{labels.properties.filterCategoriesLabel}}
                </p>
                <div class="category" v-for="category in categories" :key="category.CategoryId">
                    <v-checkbox :label="category.Name" v-model="filters.selectedCategories" :value="category.CategoryId"></v-checkbox>
                </div>
            </div>
            <hr>
            <div class="filter-section">
                <p class="sub-title title-filter">
                    {{labels.properties.filterPriceRangeLabel}}
                </p>
                <div class="price-range">
                    <vue-slider :min="min" :max="max" v-model="filters.priceRange"></vue-slider>
                </div>
            </div>
        </div>
    </v-card>
</template>

<script>
    import * as resources from "../../../resources/resources";

    export default {
        props: {
            filters: {
                type: Object,
                required: true
            },
            categories: {
                type: Array
            },
            loadGamesFunc: {
                type: Function
            },
            hideModeFilter: {
                type: Boolean,
                default: false
            },
            customerId: {
                type: String
            }
        },
        data() {
            return {
                modes: [
                    {
                        id: 0,
                        text: "By name"
                    },
                    {
                        id: 1,
                        text: "By number of sales"
                    },
                    {
                        id: 2,
                        text: "By rating"
                    },
                    {
                        id: 3,
                        text: "By price: high to low"
                    },
                    {
                        id: 4,
                        text: "By price: low to high"
                    },
                ],
                min: 0,
                max: 200,
                labels: resources.gameLabels
            };
        },
        computed: {
            hasFilter() {
                let filterParams = this.getFilterParams();

                let hasFilter =
                    filterParams.CategoriesIds.length > 0 ||
                    filterParams.Term != "" ||
                    filterParams.StartPrice > 0 ||
                    filterParams.EndPrice < 200 ||
                    filterParams.SortMode != 0;

                return hasFilter;
            }
        },
        methods: {
            applyFilters() {
                this.filters.isApply = true;
                let params = this.getFilterParams();
                this.loadGamesFunc(params);
            },
            clearFilters() {
                this.filters.priceRange = [0, 200];
                this.filters.title = "";
                this.filters.selectedCategories = [];
                this.filters.isApply = false;
                this.filters.outputMode = 0;

                let params = this.getFilterParams();
                this.loadGamesFunc(params);
            },
            getFilterParams() {
                return {
                    PageSize: 12,
                    PageNumber: 1,
                    CategoriesIds: this.filters.selectedCategories,
                    Term: this.filters.title,
                    SortMode: this.filters.outputMode,
                    StartPrice: Math.min(...this.filters.priceRange),
                    EndPrice: Math.max(...this.filters.priceRange),
                    CustomerId: this.customerId
                };
            }
        }
    };
</script>
