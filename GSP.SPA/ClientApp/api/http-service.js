import axios from "axios";

let getHeaders = () => {
    let token = sessionStorage.getItem("token");
    let headers = {};

    if (token) {
        headers = {
            headers: { Authorization: `Bearer ${sessionStorage.token}` }
        };
    }

    return headers;
};

export const getData = params => {
    const headers = getHeaders();
    return axios.get(params.url, headers);
};

export const postData = params => {
    const headers = getHeaders();
    return axios.post(params.url, params.data, headers);
};
