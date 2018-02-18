import axios from 'axios'
axios.defaults.baseURL = "http://localhost:56624/"

export const getRequest = (params) => {
    return axios.get(params.url);
}

export const postData = (params) => {
    return axios.post(params.url, params.data);
}
