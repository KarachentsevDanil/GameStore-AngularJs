<template>
    <div class="container game-container">
        <div class="col-lg-3 col-sm-12">
            <v-list two-line>
                <template v-for="(order, index) in orders">
                    <v-list-tile avatar
                                 ripple
                                 @click="selectOrder(order)"
                                 :key="order.OrderId">

                        <v-list-tile-content>
                            <v-list-tile-title> Order #{{ order.OrderId }} </v-list-tile-title>
                            <v-list-tile-title>
                                <p>
                                    <span class="bold"> Customer:</span> {{order.CustomerName}}
                                </p>
                            </v-list-tile-title>
                            <v-list-tile-sub-title class="text--primary">
                                <p>
                                    <span class="bold"> Count Games:</span> {{order.Games.length}}
                                </p>
                            </v-list-tile-sub-title>
                            <v-list-tile-sub-title class="text--primary">
                                <p>
                                    <span class="bold"> Total Price:</span> {{getOrderTotalPrice(order.Games)}}
                                </p>
                            </v-list-tile-sub-title>
                        </v-list-tile-content>

                    </v-list-tile>
                    <v-divider v-if="index + 1 < order.length" :key="index"></v-divider>
                </template>
            </v-list>
        </div>
        <div class="col-lg-9 col-sm-12" v-if="hasSelectedOrder">
            <div class="col-lg-6 col-sm-12 game-block" v-for="game in currentOrder.Games" :key="game.GameId">
                <div class="row">
                    <div class="col-lg-3 col-md-12 image-block">
                        <img class="rounded-photo" :src="game.Photo" height="300" width="220">
                    </div>
                    <div class="game-details col-lg-7 col-lg-offset-2 col-md-12">
                        <h3>
                            {{game.Name}}
                        </h3>
                        <p>
                            <span class="bold"> Category:</span> {{game.CategoryName}}
                        </p>
                        <p>
                            <span class="bold"> Price:</span> {{game.Price}} USD
                        </p>
                        <p>
                            <span class="bold"> Description:</span> {{game.Description}}
                        </p>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
    import * as orderService from "../../api/order-service";

    export default {
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
                }
            };
        },
        props: {
            customerId: {
                type: String
            }
        },
        async beforeMount() {
            let params = {
                PageSize: 12,
                PageNumber: 1,
                CustomerId: this.customerId
            };

            let ordersResponse = (await orderService.getOrdersByParams(params)).data;

            this.orders = ordersResponse.Collection;
            this.filters.pagination.total = ordersResponse.TotalCount;
        },
        methods: {
            selectOrder(order) {
                this.currentOrder = order;
            },
            getOrderTotalPrice(games) {
                let total = _.reduce(games, (sum, game) => {
                    return sum + game.Price;
                }, 0);

                return total;
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
    .game-container .col-lg-6 {
        width: 48%;
    }

    .game-block {
        background: white;
        margin-bottom: 20px;
        border: 3px solid white;
        border-radius: 2px;
        box-shadow: 0px 2px 2px 0 rgba(34, 36, 38, 0.15);
        margin-right: 20px;
    }

        .game-block .image-block {
            padding-top: 5px;
            padding-bottom: 5px;
        }

            .game-block .image-block .rounded-photo {
                border-radius: 5px;
            }

        .game-block h3 {
            margin-top: 5px;
        }

        .game-block .game-details {
            padding-left: 5px;
        }

    .order-block .text-md-center {
        font-weight: bold;
        font-size: 18px;
    }

    .order-block .order-details-block {
        padding: 10px;
    }

    p span.bold {
        font-weight: bold;
    }

    p {
        font-size: 16px;
    }
</style>
