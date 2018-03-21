<template>
    <div class="container game-container">
        <div class="col-lg-3 col-sm-12 orders">
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
            <div class="orders-info">
                <div v-for="(order) in orders" :class="{'order-details' : true, active: isActive(order.OrderId)}" :key="order.OrderId" @click="selectOrder(order)">
                    <h4>
                        {{resources.properties.orderNumber}} {{ order.OrderId }}
                    </h4>
                    <p>
                        <span class="bold"> {{resources.properties.customerLabel}}</span> {{order.CustomerName}}
                    </p>
                    <p>
                        <span class="bold"> {{resources.properties.countGamesLabel}}</span> {{order.Games.length}}
                    </p>
                    <p>
                        <span class="bold"> {{resources.properties.totalPriceLabel}}</span> {{getOrderTotalPrice(order.Games)}} {{resources.properties.moneyLabel}}
                    </p>
                    <p>
                        <span class="bold"> {{resources.properties.paymentTypeLabel}}</span> {{resources.properties.creditCardTypeLabel}}
                    </p>
                    <p>
                        <span class="bold"> {{resources.properties.creditCardTypeLabel}}:</span> {{creditCardFormat(order.Payment.CreditCard)}}
                    </p>
                    <p>
                        <span class="bold"> {{resources.properties.creditCardOwnerLabel}}</span> {{order.Payment.FullName}}
                    </p>
                </div>
            </div>
            <div>
                <pagination :currentPage="filters.pagination.currentPage" :total="filters.pagination.total" :page-size="filters.pagination.pageSize" :callback="pageChanged" :options="filters.pagination.paginationOptions" nav-class="padding-10" ul-class="bg-color-red" li-class="txt-color-blue">
                </pagination>
            </div>
        </div>
        <div class="col-lg-9 col-sm-12" v-if="hasSelectedOrder">
            <order-game v-for="game in currentOrder.Games" :key="game.GameId" :game="game" :hideDeleteAction="true"></order-game>
        </div>
    </div>
</template>

<script>
    import * as orderService from "../../api/order-service";
    import * as resources from "../../resources/resources";
    import * as mainStoreActions from "../../../../store/types/action-types";

    import orderGameComponent from "../order-game/order-game";

    export default {
        components: {
            orderGame: orderGameComponent
        },
        data() {
            return {
                orders: [],
                currentOrder: null,
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
                },
                resources: {
                    ...resources.lables
                }
            };
        },
        props: {
            customerId: {
                type: String
            }
        },
        async beforeMount() {
             this.$store.dispatch(
                    mainStoreActions.START_LOADING_ACTION,
                    "Orders are loading ..."
                );

            let params = {
                PageSize: 12,
                PageNumber: 1,
                CustomerId: this.customerId
            };

            let ordersResponse = (await orderService.getOrdersByParams(params)).data.Data;

            this.orders = ordersResponse.Collection;
            this.filters.pagination.total = ordersResponse.TotalCount;

            this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
        },
        methods: {
            selectOrder(order) {
                this.currentOrder = order;
            },
            getOrderTotalPrice(games) {
                let total = _.reduce(
                    games,
                    (sum, game) => {
                        return sum + game.Price;
                    },
                    0
                );

                return total;
            },
            isActive(orderId) {
                return this.currentOrder != null && this.currentOrder.OrderId === orderId;
            },
            async loadOrders(params) {
                let ordersResponse = (await orderService.getOrdersByParams(params)).data.Data;

                this.orders = ordersResponse.Collection;
                this.filters.pagination.total = ordersResponse.TotalCount;
            },
            getFilterParams() {
                let params = {
                    PageSize: 12,
                    PageNumber: 1,
                    CustomerId: this.customerId
                };

                return params;
            },
            pageChanged(page) {
                this.filters.pagination.currentPage = page;

                let params = this.getFilterParams();
                params.PageNumber = page;

                this.loadOrders(params);
            },
            creditCardFormat(creditCard) {
                let number = String(creditCard);

                if (number != "") {
                    return `${number.slice(0, 4)}-${number.slice(4, 8)}-${number.slice(
                    8,
                    12
                    )}-${number.slice(12, 16)}`;
                }

                return "";
            }
        },
        computed: {
            hasSelectedOrder() {
                return this.currentOrder != null;
            }
        }
    };
</script>


<style scoped>
    .orders-info {
        margin-top: 10px;
        margin-bottom: 10px;
    }

    .order-details {
        background: white;
        border-bottom: 2px solid whitesmoke;
        padding: 10px;
        box-shadow: 0px 2px 2px 0 rgba(34, 36, 38, 0.15);
    }

        .order-details:hover {
            background-color: whitesmoke;
            cursor: pointer;
        }

    .active {
        background-color: whitesmoke;
    }

    .order-details p {
        margin-bottom: 2px;
    }

    p span.bold {
        font-weight: bold;
    }

    p {
        font-size: 14px;
    }
</style>
