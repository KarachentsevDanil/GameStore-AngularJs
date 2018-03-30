<template>
    <div class="edit-filters-block">
        <v-expansion-panel>
            <v-expansion-panel-content>
                <div slot="header">
                    <span class="filter-header">
                        {{labels.headers.filtersLabel}}
                    </span>
                    <v-btn v-if="hasFilter" small color="primary" class="pull-right" @click="applyFilters">
                        {{labels.commands.applyFilterLabel}}
                    </v-btn>
                    <v-btn v-if="filters.isApply" small color="error" class="pull-right" @click="clearFilters">
                        {{labels.commands.clearFilterLabel}}
                    </v-btn>
                </div>
                <v-card>
                    <div class="form-body">
                        <div class="filter-section">
                            <p class="sub-title title-filter">
                                {{labels.properties.filterTitleLabel}}
                            </p>
                            <v-text-field v-model="filters.title" :label="labels.properties.fitlerTitleSearchLabel"></v-text-field>
                        </div>
                        <hr>
                        <div class="filter-section">
                            <p class="sub-title title-filter">
                                {{labels.properties.filterCategoriesLabel}}
                            </p>
                            <div class="category" v-for="category in categories" :key="category.CategoryId">
                                <v-checkbox :label="category.Name" v-model="filters.selectedCategories" :value="category.CategoryId"></v-checkbox>
                            </div>
                        </div>
                        <hr>
                    </div>

                </v-card>
            </v-expansion-panel-content>
        </v-expansion-panel>
    </div>
</template>

<script>
    import * as resources from "../../../resources/resources";

    export default {
        data() {
            return {
                labels: resources.gameLabels
            };
        },
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
        computed: {
            hasFilter() {
                let filterParams = this.getFilterParams();

                let hasFilter =
                    filterParams.CategoriesIds.length > 0 || filterParams.Term != "";

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
                this.filters.title = "";
                this.filters.selectedCategories = [];
                this.filters.isApply = false;

                let params = this.getFilterParams();
                this.loadGamesFunc(params);
            },
            getFilterParams() {
                return {
                    PageSize: 12,
                    PageNumber: 1,
                    CategoriesIds: this.filters.selectedCategories,
                    Term: this.filters.title
                };
            }
        }
    };
</script>

<style>
    .edit-filters-block .expansion-panel__header {
        background-color: #5e35b1 !important;
    }

    div.input-group__details {
        min-height: 0px !important;
    }

    .edit-filters-block .expansion-panel__header span.filter-header {
        font-size: 18px;
        font-weight: bold;
        color: white;
        padding-top: 8px;
        display: inline-block;
    }

    .edit-filters-block .filter-section {
        padding: 15px;
    }

        .edit-filters-block .filter-section .input-group__details {
            min-height: 0px;
        }

        .edit-filters-block .filter-section .sub-title.title-filter {
            font-size: 16px;
            font-weight: bold;
        }

        .edit-filters-block .filter-section p.sub-title.title-filter {
            font-size: 16px;
            font-weight: bold;
            margin-bottom: 0px;
        }

    .edit-filters-block hr {
        margin-top: 0px;
        margin-bottom: 0px;
    }

    .price-range {
        margin-top: 35px;
    }
</style>
