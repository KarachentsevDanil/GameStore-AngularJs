import LoginPage from "../pages/auth/pages/login";
import RegistrationPage from "../pages/auth/pages/registration";
import * as routeGuards from './route-guards';
export default [
    {
        path: "/login",
        component: LoginPage,
        beforeEnter: (to, from, next) => {
            routeGuards.redirectToHomePage(to, from, next);
        }
    },
    {
        path: "/registr",
        component: RegistrationPage
    }
];
