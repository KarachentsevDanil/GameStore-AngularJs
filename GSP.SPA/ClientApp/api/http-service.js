import axios from "axios";
import router from "../router";

axios.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        if (error.response.status === 401) {
            router.push("/login");
        }

        return error;
    }
);

let getHeaders = () => {
    let token = localStorage.getItem("token");
    let headers = {};

    if (token) {
        headers = {
            headers: { Authorization: `Bearer ${localStorage.token}` }
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
