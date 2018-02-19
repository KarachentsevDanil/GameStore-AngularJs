import * as httpService from "../../../api/http-service";

const baseOrderUrl = 'api/order/';

const addGameToBucketUrl = baseOrderUrl + 'AddToBucket';
const getGamesFromBucketUrl = baseOrderUrl + 'GetGamesFromBucket';
const completeOrderUrl = baseOrderUrl + 'ConfirmOrder';
const deleteGameFromOrderUrl = baseOrderUrl + 'DeleteGameFromBucket';
const getOrdersByParamsUrl = baseOrderUrl + 'GetOrdersByParams';

export const getOrdersByParams = ordersParams => {
    let params = {
        url: getOrdersByParamsUrl,
        data: ordersParams
    };

    return httpService.postData(params);
};

export const getGamesFromBucket = customerId => {
    let params = {
        url: getGamesFromBucketUrl,
        data: {
            CustomerId: customerId
        }
    };

    return httpService.postData(params);
};

export const deleteGameFromOrder = data => {
    let params = {
        url: deleteGameFromOrderUrl,
        data: data
    };
    
    return httpService.postData(params);
};

export const addGameToBucket = data => {
    let params = {
        url: addGameToBucketUrl,
        data: data
    };

    return httpService.postData(params);
};

export const completeOrder = data => {
    let params = {
        url: completeOrderUrl,
        data: data
    };
    
    return httpService.postData(params);
};