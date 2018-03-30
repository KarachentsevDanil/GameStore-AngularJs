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
                        <confirm-order-popup :customerPayment="customerPayment" :updatePaymentInfo="updatePaymentInfo" :completeOrder="completeOrder" :games="getGamesInBucket"></confirm-order-popup>
                    </div>
                </v-card>
            </div>
        </div>
        <div class="col-lg-6 col-lg-offset-3 col-xs-12 text-center" v-else>
            <p class="bold text-center">
                {{resources.properties.bucketEmptyLabel}}
            </p>
            <v-btn :to="'/games'">
                {{resources.properties.goToGamesLabel}}
            </v-btn>
        </div>
    </div>
</template>

<script>
import * as ordersGetters from "../../store/types/getter-types";
import * as ordersActions from "../../store/types/action-types";
import * as ordersResources from "../../store/resources";
import * as resources from "../../resources/resources";
import * as mainStoreActions from "../../../../store/types/action-types";

import * as authResources from "../../../auth/store/resources";
import * as authGetters from "../../../auth/store/types/getter-types";
import * as orderService from "../../../orders/api/order-service";
import * as paymentService from "../../api/payment-service";

import { mapGetters } from "vuex";

import orderGameComponent from "../order-game/order-game";
import confirmOrderPopupComponent from "./confirm-order-popup";
import confirmOrderPopupVue from "./confirm-order-popup.vue";

export default {
  data() {
    return {
      resources: {
        ...resources.lables
      },
      customerPayment: {}
    };
  },
  components: {
    orderGame: orderGameComponent,
    confirmOrderPopup: confirmOrderPopupComponent
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
    updatePaymentInfo(customerPayment) {
      this.customerPayment = customerPayment;
    },
    async completeOrder(paymentInfo) {
      this.$store.dispatch(
        mainStoreActions.START_LOADING_ACTION,
        "Order is completing ..."
      );

      let user = this.getUser();

      let completeOrderDto = {
        CustomerId: user.CustomerId
      };

      if (this.customerPayment) {
        completeOrderDto.PaymentId = this.customerPayment.PaymentId;
      }

      if (paymentInfo.creditCard) {
        let newPayment = {
          CustomerId: user.CustomerId,
          CreditCard: paymentInfo.creditCard,
          CvvCode: paymentInfo.cvvCode,
          ExpirationMonth: paymentInfo.expirationMonth,
          ExpirationYear: paymentInfo.expirationYear,
          Country: paymentInfo.country,
          FullName: paymentInfo.fullName
        };

        let paymentId = (await paymentService.addPayment(newPayment)).data.Data;
        completeOrderDto.PaymentId = paymentId;
      }

      await orderService.completeOrder(completeOrderDto);

      this.$store.dispatch(
        ordersResources.ORDERS_STORE_NAMESPACE.concat(
          ordersActions.GET_GAMES_FROM_BUCKET_ACTION
        ),
        user
      );

      this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);

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
      let games = this.getGames();

      return games;
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
