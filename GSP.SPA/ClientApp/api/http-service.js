import axios from "axios";

export const getData = params => {
    return axios.get(params.url);
};

export const postData = params => {
    return axios.post(params.url, params.data);
};
