import * as httpService from "../../../api/http-service";

const baseGameUrl = 'api/category/';
const getCategoriesUrl = baseGameUrl + 'GetCategories';

export const getCategories = () => {
    let params = {
        url: getCategoriesUrl
    };

    return httpService.getData(params);
};