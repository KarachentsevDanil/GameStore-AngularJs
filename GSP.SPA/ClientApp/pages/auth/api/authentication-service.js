import * as httpService from "../../../api/http-service";

export const getToken = data => {
    let params = {
        url: "token",
        data: data
    };

    return httpService.postData(params);
};
