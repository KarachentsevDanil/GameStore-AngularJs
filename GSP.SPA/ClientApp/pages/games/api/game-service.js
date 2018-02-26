import * as httpService from "../../../api/http-service";
import * as resources from '../resources/resources';

export const addGame = data => {
    let params = {
        url: resources.gameUrls.addGameUrl,
        data: data
    };

    return httpService.postData(params);
};

export const editGame = data => {
    let params = {
        url: resources.gameUrls.editGameUrl,
        data: data
    };

    return httpService.postData(params);
};

export const getGames = data => {
    let params = {
        url: resources.gameUrls.getGamesUrl,
        data: data
    };

    return httpService.postData(params);
};

export const getRecomendedGamesById = id => {
    let params = {
        url: resources.gameUrls.getRecomendedGamesByIdUrl.concat(`/${id}`)
    };

    return httpService.getData(params);
};

export const getGameById = id => {
    let params = {
        url: resources.gameUrls.getGameByIdUrl.concat(`/${id}`)
    };

    return httpService.getData(params);
};
