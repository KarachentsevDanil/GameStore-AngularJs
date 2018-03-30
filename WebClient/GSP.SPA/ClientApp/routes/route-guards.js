let validateToken = () => {
    let tokenExpirationDate = localStorage.tokenExpirationDate;
    let isTokenExpire = !tokenExpirationDate;

    if (!isTokenExpire) {
        let expirationDate = new Date(localStorage.tokenExpirationDate);
        isTokenExpire = expirationDate < new Date();
    }

    if (isTokenExpire) {
        localStorage.user = "";
        localStorage.token = "";
    }

    return !isTokenExpire;
};

export const validateAdminRoute = (to, from, next) => {
    let isTokenValid = validateToken();

    if (isTokenValid) {
        let user = !localStorage.user ? "" : $.parseJSON(localStorage.user);

        if (user && user.Role === "Admin") {
            next();
        } else {
            next("/login");
        }
    } else {
        next("/login");
    }
};

export const validateUserRoute = (to, from, next) => {
    let isTokenValid = validateToken();

    if (isTokenValid) {
        let user = !localStorage.user ? "" : $.parseJSON(localStorage.user);

        if (user && user.Role === "User") {
            next();
        } else if (user && user.Role === "Admin") {
            next("/edit-games");
        } else {
            next("/login");
        }
    } else {
        next("/login");
    }
};

export const redirectToHomePage = (to, from, next) => {
    let user = !localStorage.user ? "" : $.parseJSON(localStorage.user);

    if (user && user.Role === "User") {
        next("/games");
    } else if (user && user.Role === "Admin") {
        next("/edit-games");
    } else {
        next();
    }
};
