export default {
    getToken: state => {
        let token = "";

        if (state.token) {
            return state.token;
        }

        if (localStorage.token) {
            state.token = localStorage.token;
            return state.token;
        }

        return token;
    },
    getUser: state => {
        let user = {};

        if (!_.isEmpty(state.user) && state.user) {
            return state.user;
        }

        if (!_.isEmpty(localStorage.user) && localStorage.user) {
            state.user = $.parseJSON(localStorage.user);
            return state.user;
        }

        return user;
    }
};
