import * as httpService from "../../../api/http-service";
import * as resources from '../resources/resources';

export const getCategories = () => {
    let params = {
        url: resources.categoryUrls.getCategoriesUrl
    };

    return httpService.getData(params);
};

export const addCategory = (data) => {
    let params = {
        url: resources.categoryUrls.addCategoryUrl,
        data: data
    };

    return httpService.postData(params);
};