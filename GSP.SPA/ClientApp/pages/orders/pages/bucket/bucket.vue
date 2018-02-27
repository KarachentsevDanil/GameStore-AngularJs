<template>
    <div class="container game-container">
        <div class="bucket-games" v-if="haveGamesInBucket">
            <div class="col-lg-9 col-sm-12">
                <order-game v-for="game in getGamesInBucket" :game="game" :deleteGameFromBucket="deleteGameFromBucket" :key="game.GameId"></order-game>
            </div>
            <div class="col-lg-3 col-sm-12">
                <v-card class="order-block">
                    <div class="deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-md-center">
                                {{resources.headers.orderDetailsLabel}}
                            </span>
                        </v-card-title>
                    </div>
                    <div class="order-details-block">
                        <p>
                            <span class="bold"> {{resources.properties.countGamesLabel}}</span> {{getCountGames}}
                        </p>
                        <p>
                            <span class="bold"> {{resources.properties.totalPriceLabel}}</span> {{getOrderTotal}} {{resources.properties.moneyLabel}}
                        </p>
                    </div>
                    <div class="order-complete-block">
                        <v-btn block color="primary" dark @click="completeOrder">{{resources.commands.confirmOrderLabel}} <v-icon right dark>shopping_cart</v-icon> </v-btn>
                    </div>
                </v-card>
            </div>
        </div>
        <div class="col-lg-6 col-lg-offset-3 col-xs-12 text-center">
            <p class="bold text-center">
                Bucket is empty.
            </p>
            <v-btn :to="'/games'">
                Go to game
            </v-btn>
        </div>
    </div>
</template>

<script>
import * as ordersGetters from "../../store/types/getter-types";
import * as ordersActions from "../../store/types/action-types";
import * as ordersResources from "../../store/resources";
import * as resources from "../../resources/resources";

import * as authResources from "../../../auth/store/resources";
import * as authGetters from "../../../auth/store/types/getter-types";
import * as orderService from "../../../orders/api/order-service";

import { mapGetters } from "vuex";

import orderGameComponent from "../order-game/order-game";

export default {
  data() {
    return {
      resources: {
        ...resources.lables
      }
    };
  },
  components: {
    orderGame: orderGameComponent
  },
  methods: {
    ...mapGetters({
      getGames: ordersResources.ORDERS_STORE_NAMESPACE.concat(
        ordersGetters.GET_GAMES_FROM_BUCKET_GETTER
      ),
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      )
    }),
    async completeOrder() {
      let user = this.getUser();

      let completeOrderDto = {
        CustomerId: user.CustomerId
      };

      await orderService.completeOrder(completeOrderDto);

      this.$store.dispatch(
        ordersResources.ORDERS_STORE_NAMESPACE.concat(
          ordersActions.GET_GAMES_FROM_BUCKET_ACTION
        ),
        user
      );

      this.$router.push("/my-orders");
    },
    async deleteGameFromBucket(gameId) {
      let user = this.getUser();

      let completeOrderDto = {
        CustomerId: user.CustomerId,
        GameId: gameId
      };

      try {
        await orderService.deleteGameFromOrder(completeOrderDto);
        this.$noty.success(resources.popupMessages.gameDeletedFromBucket);
      } catch (e) {
        this.$noty.error(resources.popupMessages.gameDeletedFromBucketError);
      }

      this.$store.dispatch(
        ordersResources.ORDERS_STORE_NAMESPACE.concat(
          ordersActions.GET_GAMES_FROM_BUCKET_ACTION
        ),
        user
      );
    }
  },
  beforeMount() {
    let user = this.getUser();

    this.$store.dispatch(
      ordersResources.ORDERS_STORE_NAMESPACE.concat(
        ordersActions.GET_GAMES_FROM_BUCKET_ACTION
      ),
      user
    );
  },
  computed: {
    getOrderTotal() {
      let gamesInBucket = this.getGames();
      let total = _.reduce(
        gamesInBucket,
        function(sum, game) {
          return sum + game.Price;
        },
        0
      );

      return total;
    },
    getGamesInBucket() {
      return this.getGames();
    },
    getCountGames() {
      return this.getGames().length;
    },
    haveGamesInBucket() {
      return this.getGames().length > 0;
    }
  }
};
</script>

<style scoped>
.order-block .text-md-center {
  font-weight: bold;
  font-size: 18px;
}

.order-block .order-details-block {
  padding: 10px;
}

div.text-center {
  text-align: center;
}

p span.bold {
  font-weight: bold;
}

p {
  font-size: 16px;
}

p.bold.text-center {
  text-align: center;
  font-size: 28px;
  font-weight: 500;
}
</style>
