export default {
    setToken(state, token) {
        localStorage.setItem("token", token);
        state.token = token;
    },
    setUser(state, user) {
        localStorage.setItem("user", JSON.stringify(user));
        state.user = user;
    },
    userLogout(state) {
        localStorage.clear();
        state.user = {};
        state.token = "";
    }
};
