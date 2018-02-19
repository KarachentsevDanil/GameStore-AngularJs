<template>
    <div class="container">
        <div class="col-lg-3 col-sm-12 filter-block">
                <v-card class="form-sign-block">
                    <div class="form-header deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-xs-center sub-title">
                                Filters
                            </span>
                        </v-card-title>
                    </div>
                    <div class="form-body">
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
                    </div>
                </v-card>
        </div>
        <div class="col-lg-9 col-sm-12 games-block" v-if="games.length > 0">
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
            <div v-for="game in games" :key="game.GameId" class="col-lg-4 col-sm-12 game-block">
                <v-card>
                    <v-card-media :src="game.Photo" height="320px">
                    </v-card-media>
                    <v-card-title primary-title>
                        <div>
                            <h3 class="headline mb-0">{{ game.Name }}</h3>
                            <div>
                               <span class="bold"> Category: </span> {{ game.CategoryName }}
                            </div>
                            <div>
                               <span class="bold"> Price: </span> {{ game.Price }}
                            </div>
                        </div>
                    </v-card-title>
                    <v-card-actions>
                        <v-btn class="flat-button" color="primary">Buy</v-btn>
                        <v-btn class="flat-button">Details</v-btn>
                    </v-card-actions>
                </v-card>
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

export default {
  data() {
    return {
      games: [],
      categories: [],
      filters: {
        selectedCategories: [],
        title: "",
        pagination: {
          total: 0,
          pageSize: 12,
          currentPage: 2,
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
    let params = {
      PageSize: 12,
      PageNumber: 1
    };

    let gamesResponse = await gameService.getGames(params);

    this.games = gamesResponse.data.Collection;
    this.filters.pagination.total = gamesResponse.data.TotalCount;

    let categoriesResponse = await categoryService.getCategories();
    this.categories = categoriesResponse.data;
  },
  methods: {
    pageChanged(page) {
      this.filters.pagination.currentPage = page;
    }
  }
};
</script>

<style>
.filter-block .form-body {
  padding: 10px;
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

button.flat-button {
  width: 47%;
}

.games-block .padding-10 {
  margin-bottom: 10px;
  margin-left: 15px;
}

.game-block {
  margin-bottom: 25px;
}
.games-block .card__title.card__title--primary {
  padding-top: 6px;
  margin-bottom: 6px;
}
.games-block .headline.mb-0 {
  margin-top: 4px;
}

.game-block .bold {
  margin-top: 3px;
  font-weight: bold;
}
</style>

