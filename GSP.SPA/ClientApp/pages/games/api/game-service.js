import * as httpService from "../../../api/http-service";

const baseGameUrl = 'api/game/';
const getGamesUrl = baseGameUrl + 'GetGamesByParams';

export const getGames = data => {
    let params = {
        url: getGamesUrl,
        data: data
    };

    return httpService.postData(params);
};