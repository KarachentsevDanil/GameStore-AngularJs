export default {
    getToken: state => {
        let token = "";

        if (state.token) {
            return state.token;
        }

        if (sessionStorage.token) {
            state.token = sessionStorage.token;
            return state.token;
        }

        return token;
    },
    getUser: state => {
        let user = {};

        if (!_.isEmpty(state.user) && state.user) {
            return state.user;
        }

        if (!_.isEmpty(sessionStorage.user) && sessionStorage.user) {
            state.user = $.parseJSON(sessionStorage.user);
            return state.user;
        }

        return user;
    }
};
