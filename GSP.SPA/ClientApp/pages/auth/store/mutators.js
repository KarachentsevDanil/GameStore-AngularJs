export default {
    setToken(state, token) {
        sessionStorage.setItem('token', token);
        state.token = token;
    },
    setUser(state, user) {
        sessionStorage.setItem('user', JSON.stringify(user));
        state.user = user;
    }
};
