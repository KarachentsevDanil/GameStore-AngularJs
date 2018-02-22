<template>
  <div class="container">
      <div class="col-lg-3 col-sm-12">
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
            <div class="games-info">
                <div v-for="(game) in games" class="game-details" :key="game.GameId" @click="selectGame(game)">
                    <h4>
                        Game Id #{{ game.GameId }}
                    </h4>
                    <p>
                        <span class="bold"> Name:</span> {{ game.Name }}
                    </p>
                    <p>
                        <span class="bold"> Category:</span> {{game.CategoryName}}
                    </p>
                    <p>
                        <span class="bold"> Price:</span> {{ game.Price }}
                    </p>
                </div>
            </div>
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
      </div>
      <div class="col-lg-9 col-sm-12">

      </div>
  </div>
</template>

<script>
import * as gameService from "../../api/game-service";

export default {
  data() {
    return {
      games: [],
      currentGame: null,
      filters: {
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
    let params = {
      PageSize: 12,
      PageNumber: 1
    };

    let gamesResponse = (await gameService.getGames(params)).data;

    this.games = gamesResponse.Collection;
    this.filters.pagination.total = gamesResponse.TotalCount;
  },
  methods: {
      selectGame(game){
          this.currentGame = game;
      }
  }
};
</script>

<style scoped>

.games-info{
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
