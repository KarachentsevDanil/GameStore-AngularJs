import * as httpService from "../../../api/http-service";
import * as orderResources from '../resources/resources';

export const getOrdersByParams = ordersParams => {
    let params = {
        url: orderResources.ordersUrls.getOrdersByParamsUrl,
        data: ordersParams
    };

    return httpService.postData(params);
};

export const getGamesFromBucket = customerId => {
    let params = {
        url: orderResources.ordersUrls.getGamesFromBucketUrl,
        data: {
            CustomerId: customerId
        }
    };

    return httpService.postData(params);
};

export const deleteGameFromOrder = data => {
    let params = {
        url: orderResources.ordersUrls.deleteGameFromOrderUrl,
        data: data
    };

    return httpService.postData(params);
};

export const addGameToBucket = data => {
    let params = {
        url: orderResources.ordersUrls.addGameToBucketUrl,
        data: data
    };

    return httpService.postData(params);
};

export const completeOrder = data => {
    let params = {
        url: orderResources.ordersUrls.completeOrderUrl,
        data: data
    };

    return httpService.postData(params);
};
