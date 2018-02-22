import * as httpService from "../../../api/http-service";

const baseGameUrl = 'api/category/';
const getCategoriesUrl = baseGameUrl + 'GetCategories';
const addCategoryUrl = baseGameUrl + 'AddCategory';

export const getCategories = () => {
    let params = {
        url: getCategoriesUrl
    };

    return httpService.getData(params);
};

export const addCategory = (data) => {
    let params = {
        url: addCategoryUrl,
        data: data
    };

    return httpService.postData(params);
};