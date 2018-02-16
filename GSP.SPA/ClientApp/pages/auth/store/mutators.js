export default {
    addToken(state, response) {
        state.username = response.data.username;
        state.acessToken = response.data.access_token;
    }
};
