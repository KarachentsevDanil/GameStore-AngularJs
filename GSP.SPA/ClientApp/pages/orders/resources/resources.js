import * as resources from "../../../resources/resources";

const baseOrderUrl = resources.urls.baseUrl + "api/order/";

export const ordersUrls = {
    addGameToBucketUrl: baseOrderUrl + "AddToBucket",
    getGamesFromBucketUrl: baseOrderUrl + "GetGamesFromBucket",
    completeOrderUrl: baseOrderUrl + "ConfirmOrder",
    deleteGameFromOrderUrl: baseOrderUrl + "DeleteGameFromBucket",
    getOrdersByParamsUrl: baseOrderUrl + "GetOrdersByParams"
};

export const lables = {
    headers: {
        orderDetailsLabel: "Order details"
    },
    properties: {
        countGamesLabel: "Count Games:",
        totalPriceLabel: "Total Price:",
        categoryLabel: "Category:",
        descriptionLabel: "Description:",
        priceLabel: "Price:",
        moneyLabel: "USD",
        orderNumber: "Order #",
        customerLabel: "Customer:"
    },
    commands: {
        confirmOrderLabel: "Confirm Order",
        checkoutOrderLabel: "Checkout"
    }
};

export const popupMessages = {
    gameAddedToBucket: "Game was successfully added to bucket.",
    gameAlreadyBought: "Game already bought.",
    gameDeletedFromBucket: "Game was successfully deleted from bucket.",
    gameDeletedFromBucketError: "Error occure when deleting game from bucket."
};