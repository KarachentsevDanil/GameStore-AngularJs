<template>
    <div class="container">
        <div class="row">
            <div v-if="!createNew" class="col-lg-12">
                <div class="cards-block col-lg-10 col-lg-offset-1">
                    <h4 class="text-center">
                        Customer Payments
                    </h4>
                    <div class="col-xs-6" v-for="payment in customerPayments" :key="payment.PaymentId" @click="selectPayment(payment)">
                        <div :class="{'payment-block' : true, active: isActive(payment.PaymentId)}">
                            <p>
                                <span class="bold">
                                    {{resources.properties.creditCardNumberLabel}}:
                                </span>
                                <span class="value">
                                    {{payment.CreditCard}}
                                </span>
                            </p>
                            <p>
                                <span class="bold">
                                    {{resources.properties.fullNameLabel}}:
                                </span>
                                <span class="value">
                                    {{payment.FullName}}
                                </span>
                            </p>
                            <p>
                                <span class="bold">
                                    {{resources.properties.countryLabel}}:
                                </span>
                                <span class="value">
                                    {{payment.Country}}
                                </span>
                            </p>
                        </div>
                    </div>
                </div>

                <v-btn class="pull-right" color="primary" @click="createNew = true"> {{resources.commands.addNewPaymentLabel }} </v-btn>
                <v-btn class="pull-right" color="primary" :disabled="!isPaymentSelected" @click.native="dialogInfo.currentTab = 2"> {{resources.commands.proccedReviewOrderLabel }} </v-btn>
            </div>
            <div v-else class="col-lg-12">
                <add-payment :paymentInfo="paymentInfo" :dialogInfo="dialogInfo" :back="back">
                </add-payment>
            </div>
        </div>
    </div>
</template>

<script>
    import addPaymentComponent from "./payment/add-payment";
    import * as resources from "../../../resources/resources";
    import * as paymentService from "../../../api/payment-service";

    import * as authGetters from "../../../../auth/store/types/getter-types";
    import * as authResources from "../../../../auth/store/resources";

    import { mapGetters } from "vuex";

    export default {
        components: {
            addPayment: addPaymentComponent
        },
        data() {
            return {
                currentPayment: {},
                customerPayments: [],
                createNew: false,
                isPaymentSelected: false,
                resources: {
                    ...resources.lables
                }
            };
        },
        async beforeMount() {
            let customerId = this.getUsername.CustomerId;
            let paymentsResponse = await paymentService.getCustomerPayments(customerId);
            this.customerPayments = paymentsResponse.data.Data;
        },
        methods: {
            selectPayment(payment) {
                this.currentPayment = payment;
                this.updatePaymentInfo(payment);

                this.isPaymentSelected = true;
            },
            back() {
                this.isPaymentSelected = false;
                this.createNew = false;
                this.currentPayment = {};
            },
            isActive(paymentId) {
                return this.currentPayment && this.currentPayment.PaymentId == paymentId;
            }
        },
        computed: {
            ...mapGetters({
                getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                ),
                getToken: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_TOKEN_GETTER
                ),
                blockUiOptions: "getLoaderOptions"
            })
        },
        props: {
            dialogInfo: {
                type: Object
            },
            paymentInfo: {
                type: Object
            },
            updatePaymentInfo: {
                type: Function
            }
        }
    };
</script>

<style>
    .payment-block {
        cursor: pointer;
        border: 1px solid whitesmoke;
        padding: 15px;
        margin-bottom: 15px;
        border-radius: 3px;
    }

        .payment-block.active {
            background-color: whitesmoke;
        }

        .payment-block > p .bold {
            font-size: 16px;
            font-weight: bold;
        }

        .payment-block > p .value {
            font-size: 15px;
        }
</style>
