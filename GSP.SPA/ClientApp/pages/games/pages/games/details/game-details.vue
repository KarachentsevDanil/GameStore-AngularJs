<template>
    <div class="col-lg-12 details-container game-block">
        <div class="game-details-block">
            <div class="photo-parent-block">
                <div class="photo-block" :style="{ backgroundImage: `url('${game.Photo}')` }">
                </div>
            </div>
            <hr>
            <div class="game-details">
                <div class="text">
                    <h3 class="headline mb-0">{{ game.Name }}</h3>
                </div>
                <div class="text">
                    <span class="bold"> Category: </span> {{ game.CategoryName }}
                </div>
                <div class="text">
                    <span class="bold"> Price: </span> {{ game.Price }} USD
                </div>
                <div class="text">
                    <span class="bold"> Description: </span> {{ game.Description }}
                </div>
                <div class="text">
                    <span class="bold">Rating: </span> <star-rating v-model="game.AverageRate" :show-rating="false" :read-only="true" :increment="0.01" :star-size="22"></star-rating> {{ game.AverageRate }} / 5
                </div>
                <div class="text">
                    <span class="bold">Count Of Feedback's: </span> {{ getRatesCount }}
                </div>
            </div>
            <hr>
            <div class="game-actions">
                <v-btn block color="primary" @click="buyGame(game.GameId)">Buy <v-icon right dark>shopping_cart</v-icon> </v-btn>
            </div>
        </div>
    </div>
</template>

<script>
export default {
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
  computed: {
    getRatesCount() {
      let countRates = 0;

      if (this.game && this.game.Rates) {
        countRates = this.game.Rates.length;
      }

      return countRates;
    }
  }
};
</script>

<style>
.details-container .game-details-block {
  background: white;
  margin-bottom: 20px;
  border: 3px solid white;
  border-radius: 2px;
  box-shadow: 0px 2px 2px 0 rgba(34, 36, 38, 0.15);
  margin-right: 20px;
}

.details-container .game-details-block .photo-parent-block {
  width: auto;
  height: 420px;
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
  -webkit-transition: all 0.5s;
  -moz-transition: all 0.5s;
  -o-transition: all 0.5s;
  transition: all 0.5s;
}

.game-details-block hr {
  margin: 0px;
}
.game-details-block .game-details{
    padding: 10px;
}

.game-details-block .game-details h3{
    margin-top: 2px;
}
</style>
