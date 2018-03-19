<template>
    <v-layout row justify-center>
        <v-btn color="primary" @click.native.stop="dialogInfo.isDialogOpen = true" dark>{{resources.commands.confirmOrderLabel}} <v-icon right dark>shopping_cart</v-icon> </v-btn>
        <v-dialog v-model="dialogInfo.isDialogOpen" max-width="800">
            <v-card>
                <div class="deep-purple darken-1">
                    <v-card-title class="white--text deep-purple darken-1">
                        <span class="text-md-center payment-title">
                            {{resources.headers.orderReviewLabel}}
                        </span>
                    </v-card-title>
                </div>
                <v-stepper v-model="dialogInfo.currentTab">
                    <v-stepper-header>
                        <v-stepper-step step="1" :complete="dialogInfo.currentTab > 1"> {{resources.properties.paymentDetailsLabel}} </v-stepper-step>
                        <v-divider></v-divider>
                        <v-stepper-step step="2" :complete="dialogInfo.currentTab > 2"> {{resources.properties.confirmOrderLabel}} </v-stepper-step>
                    </v-stepper-header>
                    <v-stepper-items>
                        <v-stepper-content step="1">
                            <payment-info :updatePaymentInfo="updatePaymentInfo" :paymentInfo="paymentInfo" :dialogInfo="dialogInfo"></payment-info>
                        </v-stepper-content>
                        <v-stepper-content step="2">
                            <order-summary :customerPayment="customerPayment" :games="games" :paymentInfo="paymentInfo" :dialogInfo="dialogInfo" :completeOrder="completeOrder"></order-summary>
                        </v-stepper-content>
                    </v-stepper-items>
                </v-stepper>
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
import * as resources from "../../resources/resources";
import paymentInfoComponent from "./confirm-order/payment-info";
import orderSummaryComponent from "./confirm-order/order-summary";

const currentDate = new Date();

export default {
  components: {
    paymentInfo: paymentInfoComponent,
    orderSummary: orderSummaryComponent
  },
  props: {
    customerPayment: {
      type: Object
    },
    games: {
      type: Array
    },
    completeOrder: {
      type: Function
    },
    updatePaymentInfo: {
      type: Function
    }
  },
  data() {
    return {
      dialogInfo: {
        currentTab: 0,
        isDialogOpen: false
      },
      paymentInfo: {
        creditCard: "",
        cvvCode: "",
        expirationMonth: currentDate.getMonth(),
        expirationYear: currentDate.getFullYear(),
        country: "Ukraine",
        fullName: ""
      },
      resources: {
        ...resources.lables
      }
    };
  }
};
</script>
