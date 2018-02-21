import * as httpService from "../../../api/http-service";

const baseRateUrl = 'api/rate/';

const getGameRatesByIdUrl = baseRateUrl + 'GetGameRates';
const addRateToGameUrl = baseRateUrl + 'CreateFeed';

export const addRateToGame = data => {
    let params = {
        url: addRateToGameUrl,
        data: data
    };

    return httpService.postData(params);
};

export const getGameRatesById = id => {
    let params = {
        url: getGameRatesByIdUrl.concat(`/${id}`)
    };

    return httpService.getData(params);
};