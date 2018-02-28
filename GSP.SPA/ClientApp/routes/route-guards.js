export default {
    validateAdminRoute(to, from, next) {
        let user = !localStorage.user ? "" : $.parseJSON(localStorage.user);

        if (user && user.Role === "Admin") {
            next();
        } else {
            next("/login");
        }
    },
    validateUserRoute(to, from, next) {
        let user = !localStorage.user ? "" : $.parseJSON(localStorage.user);

        if (user && user.Role === "User") {
            next();
        } else if (user && user.Role === "Admin") {
            next("/edit-games");
        } else {
            next("/login");
        }
    },

    redirectToHomePage(to, from, next) {
        let user = !localStorage.user ? "" : $.parseJSON(localStorage.user);

        if (user && user.Role === "User") {
            next("/games");
        } else if (user && user.Role === "Admin") {
            next("/edit-games");
        } else {
            next();
        }
    }
};
