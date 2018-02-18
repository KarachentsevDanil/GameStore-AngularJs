import * as httpService from "../../../api/http-service";

const baseAccountUrl = 'api/account/';
const loginUrl = baseAccountUrl + 'login';
const registrationUrl = baseAccountUrl + 'register';

export const login = data => {
    let params = {
        url: loginUrl,
        data: data
    };

    return httpService.postData(params);
};

export const registr = data => {
    let params = {
        url: registrationUrl,
        data: data
    };

    return httpService.postData(params);
};
