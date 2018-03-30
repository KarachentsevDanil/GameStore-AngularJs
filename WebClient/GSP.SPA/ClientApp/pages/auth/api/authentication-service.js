import * as httpService from "../../../api/http-service";
import * as authResources from "../resources/resources";

export const login = data => {
    let params = {
        url: authResources.accountUrls.loginUrl,
        data: data
    };

    return httpService.postData(params);
};

export const registr = data => {
    let params = {
        url: authResources.accountUrls.registrationUrl,
        data: data
    };

    return httpService.postData(params);
};
