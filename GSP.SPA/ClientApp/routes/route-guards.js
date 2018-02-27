export default {
    validateAdminRoute(to, from, next) {
        let user = !sessionStorage.user ? "" : $.parseJSON(sessionStorage.user);

        if (user && user.Role === "Admin") {
            next();
        } else {
            next("/login");
        }
    },
    validateUserRoute(to, from, next) {
        let user = !sessionStorage.user ? "" : $.parseJSON(sessionStorage.user);

        if (user && user.Role === "User") {
            next();
        } else if (user && user.Role === "Admin") {
            next("/edit-games");
        } else {
            next("/login");
        }
    }
};
