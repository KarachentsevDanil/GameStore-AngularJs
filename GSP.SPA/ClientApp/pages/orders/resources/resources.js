import * as resources from "../../../resources/resources";

const baseOrderUrl = resources.urls.baseUrl + "api/order/";
const basePaymentUrl = resources.urls.baseUrl + "api/payment/";

export const ordersUrls = {
    addGameToBucketUrl: baseOrderUrl + "AddToBucket",
    getGamesFromBucketUrl: baseOrderUrl + "GetGamesFromBucket",
    completeOrderUrl: baseOrderUrl + "ConfirmOrder",
    deleteGameFromOrderUrl: baseOrderUrl + "DeleteGameFromBucket",
    getOrdersByParamsUrl: baseOrderUrl + "GetOrdersByParams"
};

export const paymentsUrls = {
    addPaymentUrl: basePaymentUrl + "AddPayment",
    getCustomerPayments: basePaymentUrl + "GetCustomerPayments"
};

export const lables = {
    headers: {
        orderDetailsLabel: "Order details",
        orderReviewLabel: "Review Order",
        paymentMethodLabel: "Payment Method"
    },
    properties: {
        bucketEmptyLabel: "Bucket is empty.",
        goToGamesLabel: "Go to games",
        paymentDetailsLabel: "Payment Details",
        confirmOrderLabel: "Confirm Order",
        countGamesLabel: "Count Games:",
        totalPriceLabel: "Total Price:",
        categoryLabel: "Category:",
        descriptionLabel: "Description:",
        priceLabel: "Price:",
        moneyLabel: "USD",
        orderNumber: "Order #",
        customerLabel: "Customer:",
        creditCardNumberLabel: "Credit Card Number",
        cvvCodeLabel: "CVV Code",
        expirationMonthLabel: "Expiration Month",
        expirationYearLabel: "Expiration Year",
        countryLabel: "Country",
        fullNameLabel: "Full Name",
        paymentTypeLabel: "Payment Type:",
        creditCardTypeLabel: "Credit Card",
        creditCardOwnerLabel: "Credit Card Owner:"
    },
    commands: {
        confirmOrderLabel: "Confirm Order",
        checkoutOrderLabel: "Checkout",
        proccedReviewOrderLabel: "Proceed to Review Order",
        payNowLabel: "Pay Now",
        backLabel: "Back",
        cancelLabel: "Cancel",
        addNewPaymentLabel: "Add New Payment"
    },
    validationMessages: {
        creditCardRequiredMessage: "Credit Card is required.",
        cvvCodeRequiredMessage: "CVV Code is required.",
        cvvCodeMinLengthMessage: "CVV Code must be at least 3 characters long.",
        cvvCodeMaxLengthMessage: "CVV Code must be at most 4 characters long.",
        expirationMonthRequiredMessage: "Expiration Month is required.",
        expirationYearRequiredMessage: "Expiration Year is required.",
        fullNameRequiredMessage: "Full Name is required.",
        countryRequiredMessage: "Country is required."
    }
};

export const popupMessages = {
    gameAddedToBucket: "Game was successfully added to bucket.",
    gameAlreadyBought: "Game already bought.",
    gameDeletedFromBucket: "Game was successfully deleted from bucket.",
    gameDeletedFromBucketError: "Error occure when deleting game from bucket."
};
