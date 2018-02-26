import BucketPage from "../pages/orders/pages/bucket/bucket";
import OrdersPage from "../pages/orders/pages/orders/orders";
import MyOrdersPage from "../pages/orders/pages/orders/my-orders";

export default [
    {
        path: "/bucket",
        component: BucketPage
    },
    {
        path: "/orders",
        component: OrdersPage
    },
    {
        path: "/my-orders",
        component: MyOrdersPage
    }
];
