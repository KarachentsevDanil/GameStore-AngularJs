import BucketPage from "../pages/orders/pages/bucket/bucket";
import OrdersPage from "../pages/orders/pages/orders/orders";
import MyOrdersPage from "../pages/orders/pages/orders/my-orders";

import * as routeGuards from "./route-guards";

export default [
    {
        path: "/bucket",
        component: BucketPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateUserRoute(to, from, next);
        }
    },
    {
        path: "/orders",
        component: OrdersPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateAdminRoute(to, from, next);
        }
    },
    {
        path: "/my-orders",
        component: MyOrdersPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateUserRoute(to, from, next);
        }
    }
];
