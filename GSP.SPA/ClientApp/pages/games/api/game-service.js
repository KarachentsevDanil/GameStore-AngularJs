import * as httpService from "../../../api/http-service";

const baseGameUrl = 'api/game/';

const getGamesUrl = baseGameUrl + 'GetGamesByParams';
const getGameByIdUrl = baseGameUrl + 'GetGameById';
const getRecomendedGamesByIdUrl = baseGameUrl + 'GetRecomendedGames';
const addGameUrl = baseGameUrl + 'CreateGame';

export const addGame = data => {
    let params = {
        url: addGameUrl,
        data: data
    };

    return httpService.postData(params);
};

export const getGames = data => {
    let params = {
        url: getGamesUrl,
        data: data
    };

    return httpService.postData(params);
};

export const getRecomendedGamesById = id => {
    let params = {
        url: getRecomendedGamesByIdUrl.concat(`/${id}`)
    };

    return httpService.getData(params);
};

export const getGameById = id => {
    let params = {
        url: getGameByIdUrl.concat(`/${id}`)
    };

    return httpService.getData(params);
};