export default {
    setToken(state, token) {
        localStorage.setItem("token", token.value);
        localStorage.setItem("tokenExpirationDate", token.expireData);

        state.token = token.value;
        state.tokenExpirationDate = token.expireData;
    },
    setUser(state, user) {
        localStorage.setItem("user", JSON.stringify(user));
        state.user = user;
    },
    userLogout(state) {
        localStorage.clear();
        state.user = {};
        state.token = "";
        state.tokenExpirationDate = "";
    }
};
