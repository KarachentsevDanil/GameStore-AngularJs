import Vue from 'vue'
import VueRouter from 'vue-router'
import LoginPage from './pages/auth/pages/login'
import RegistrationPage from './pages/auth/pages/registration'

import GamesPage from './pages/games/pages/games/games'
import MyGamesPage from './pages/games/pages/games/my-games'
import GameDetails from './pages/games/pages/games/details/game-details-layout'

import AddGamePage from './pages/games/pages/add-game/add-game';

import BucketPage from './pages/orders/pages/bucket/bucket'
import OrdersPage from './pages/orders/pages/orders/orders'
import MyOrdersPage from './pages/orders/pages/orders/my-orders'

Vue.use(VueRouter);

const routes = [{
        path: '/login',
        component: LoginPage
    },
    {
        path: '/registr',
        component: RegistrationPage
    },
    {
        path: '/add-game',
        component: AddGamePage
    },
    {
        path: '/games',
        component: GamesPage
    },
    {
        path: '/game-details/:id',
        component: GameDetails,
        props: true
    },
    {
        path: '/my-games',
        component: MyGamesPage
    },
    {
        path: '/bucket',
        component: BucketPage
    },
    {
        path: '/orders',
        component: OrdersPage
    },
    {
        path: '/my-orders',
        component: MyOrdersPage
    },
    {
        path: '*',
        component: LoginPage
    }
];

let router = new VueRouter({
    mode: 'history',
    routes
});

export default router
