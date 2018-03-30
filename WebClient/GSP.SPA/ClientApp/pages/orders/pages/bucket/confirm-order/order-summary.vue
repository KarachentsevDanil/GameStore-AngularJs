<template>
    <div>
        <div class="row">
            <div class="col-lg-8 col-sm-8 col-xs-12">
                <div class="game-details col-lg-12 col-xs-12" v-for="game in games" :key="game.GameId">
                    <div class="col-lg-3 col-xs-3">
                        <img :src="game.Photo" height="120" width="80">
                    </div>
                    <div class="col-lg-5 col-xs-5">
                        <p class="bold game-title">
                            {{game.Name}}
                        </p>
                        <p class="bold game-category">
                            {{resources.properties.categoryLabel }} {{game.CategoryName}}
                        </p>
                    </div>
                    <div class="col-lg-4 col-xs-4">
                        <p class="bold game-price">
                            {{game.Price}} {{resources.properties.moneyLabel }}.
                        </p>
                    </div>
                </div>
                <div class="col-lg-12">
                    <p class="order-total">
                        {{resources.properties.totalPriceLabel }} {{getOrderTotal}} {{resources.properties.moneyLabel }}.
                    </p>
                </div>
            </div>
            <div class="col-lg-4 col-sm-4 col-xs-12">
                <v-card class="order-block">
                    <div class="deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-md-center payment-title">
                                {{resources.headers.paymentMethodLabel }}
                            </span>
                        </v-card-title>
                    </div>
                    <div class="order-details-block">
                        <div>
                            <div class="bold"> {{resources.properties.paymentTypeLabel }} </div>
                            <div class="payment-info">
                                {{resources.properties.creditCardTypeLabel }}
                            </div>
                        </div>
                        <div>
                            <div class="bold"> {{resources.properties.creditCardNumberLabel }}: </div>
                            <div class="payment-info">
                                {{creditCardFormat(getPaymentInfo.creditCard)}}
                            </div>
                        </div>
                        <div>
                            <div class="bold"> {{resources.properties.creditCardOwnerLabel }} </div>
                            <div class="payment-info">
                                {{`${getPaymentInfo.fullName}`}}
                            </div>
                        </div>
                    </div>
                </v-card>
            </div>
        </div>
        <v-btn color="primary" @click.native="completeOrderMethod">{{resources.commands.payNowLabel }}</v-btn>
        <v-btn flat @click.native="dialogInfo.currentTab = 1">{{resources.commands.backLabel }}</v-btn>
    </div>
</template>

<script>
import * as resources from "../../../resources/resources";

export default {
  props: {
    dialogInfo: {
      type: Object
    },
    games: {
      type: Array
    },
    paymentInfo: {
      type: Object
    },
    customerPayment: {
      type: Object
    },
    completeOrder: {
      type: Function
    }
  },
  data() {
    return {
      resources: {
        ...resources.lables
      }
    };
  },
  methods: {
    creditCardFormat(creditCard) {
      let number = String(creditCard);

      if (number != "") {
        return `${number.slice(0, 4)}-${number.slice(4, 8)}-${number.slice(
          8,
          12
        )}-${number.slice(12, 16)}`;
      }

      return "";
    },
    completeOrderMethod() {
      this.completeOrder(this.paymentInfo);
      this.dialogInfo.isDialogOpen = false;
    }
  },
  computed: {
    getOrderTotal() {
      let gamesInBucket = this.games;

      let total = _.reduce(
        gamesInBucket,
        function(sum, game) {
          return sum + game.Price;
        },
        0
      );

      return total;
    },
    getPaymentInfo() {
      let payment = {};

      if (this.customerPayment) {
        payment = {
          creditCard: this.customerPayment.CreditCard,
          fullName: this.customerPayment.FullName
        };
      }

      if (this.paymentInfo.creditCard) {
        payment = this.paymentInfo;
      }

      return payment;
    }
  }
};
</script>

<style>
    div.col-lg-12.game-details {
        margin-bottom: 10px;
        padding-bottom: 10px;
        border-bottom: 1px solid whitesmoke;
    }

    p.bold {
        font-weight: 500;
    }

    .game-title {
        font-size: 20px;
    }

    .game-price {
        padding-top: 50px;
        font-size: 16px;
        text-align: right;
    }

    .game-category {
        font-size: 16px;
    }

    .order-total {
        text-align: right;
        font-size: 24px;
        font-weight: 600;
    }

    .payment-title {
        font-size: 20px;
        font-weight: 500;
    }

    .order-details-block {
        padding: 10px;
    }

        .order-details-block div.bold {
            font-size: 16px;
        }

        .order-details-block .payment-info {
            font-size: 16px;
            margin-bottom: 10px;
        }

    div.bold {
        font-weight: 500;
        padding-left: 5px;
    }
</style>

