export default {
    getToken: state => !state.token ? sessionStorage.token : state.token,
    getUser: state => !state.user ?
             !sessionStorage.user ?
               '' : $.parseJSON(sessionStorage.user)
                  : state.user
}
