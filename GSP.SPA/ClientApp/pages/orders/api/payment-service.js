import * as httpService from "../../../api/http-service";
import * as orderResources from '../resources/resources';

export const addPayment = payment => {
    let params = {
        url: orderResources.paymentsUrls.addPaymentUrl,
        data: payment
    };

    return httpService.postData(params);
};

export const getCustomerPayments = customerId => {
    let params = {
        url: `${orderResources.paymentsUrls.getCustomerPayments}/${customerId}`
    };

    return httpService.getData(params);
};
