import * as httpService from "../../../api/http-service";
import * as resources from '../resources/resources';

export const addRateToGame = data => {
    let params = {
        url: resources.rateUrls.addRateToGameUrl,
        data: data
    };

    return httpService.postData(params);
};

export const getGameRatesById = id => {
    let params = {
        url: resources.rateUrls.getGameRatesByIdUrl.concat(`/${id}`)
    };

    return httpService.getData(params);
};