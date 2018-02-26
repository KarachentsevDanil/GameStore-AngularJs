import Vue from "vue";
import VueRouter from "vue-router";
import LoginPage from "./pages/auth/pages/login";

import authorizationRoutes from "./routes/authorizationRoutes";
import gameRoutes from "./routes/gameRoutes";
import orderRoutes from "./routes/orderRoutes";

Vue.use(VueRouter);

const routes = [
    ...authorizationRoutes,
    ...gameRoutes,
    ...orderRoutes,
    {
        path: "*",
        component: LoginPage
    }
];

let router = new VueRouter({
    mode: "history",
    routes
});

export default router;
