<template>
    <v-card class="form-sign-block">
        <div class="form-header deep-purple darken-1">
            <v-card-title class="white--text deep-purple darken-1">
                <span class="text-xs-center sub-title">
                    Filters
                </span>
                <v-btn v-if="hasFilter" small color="primary" class="pull-right" @click="applyFilters">Apply</v-btn>
                <v-btn v-if="filters.isApply" small color="error" class="pull-right" @click="clearFilters">Clear</v-btn>
            </v-card-title>
        </div>
        <div class="form-body">
            <div class="filter-section" v-if="!hideModeFilter">
                <p class="sub-title title-filter">
                    Mode
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
                    Title
                </p>
                <v-text-field v-model="filters.title" label="Search..."></v-text-field>
            </div>
            <hr>
            <div class="filter-section">
                <p class="sub-title">
                    Categories
                </p>
                <div class="category" v-for="category in categories" :key="category.CategoryId">
                    <v-checkbox :label="category.Name" v-model="filters.selectedCategories" :value="category.CategoryId"></v-checkbox>
                </div>
            </div>
            <hr>
            <div class="filter-section">
                <p class="sub-title title-filter">
                    Price Range
                </p>
                <div class="price-range">
                    <vue-slider :min="min" :max="max" v-model="filters.priceRange"></vue-slider>
                </div>
            </div>
        </div>
    </v-card>
</template>

<script>
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
    hideModeFilter:{
      type: Boolean,
      default: false
    },
    customerId: {
      type: String
    },
  },
  data() {
    return {
      modes: [
        {
          id: 0,
          text: "All Games"
        },
        {
          id: 1,
          text: "Top Sell Games"
        },
        {
          id: 2,
          text: "Top Rate Games"
        }
      ],
      min: 0,
      max: 200
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
        filterParams.OutputMode != 0;

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
        OutputMode: this.filters.outputMode,
        StartPrice: Math.min(...this.filters.priceRange),
        EndPrice: Math.max(...this.filters.priceRange),
        CustomerId: this.customerId
      };
    }
  }
};
</script>
