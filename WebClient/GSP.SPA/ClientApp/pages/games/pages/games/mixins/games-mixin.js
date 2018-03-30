export const gamesMixin = {
    methods: {
        getFilterParams() {
            return {
                PageSize: 12,
                PageNumber: 1,
                CategoriesIds: this.filters.selectedCategories,
                Term: this.filters.title,
                OutputMode: this.filters.outputMode,
                StartPrice: Math.min(...this.filters.priceRange),
                EndPrice: Math.max(...this.filters.priceRange)
            };
        }
    }
};
