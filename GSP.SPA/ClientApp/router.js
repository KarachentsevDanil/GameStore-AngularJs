import Vue from 'vue'
import VueRouter from 'vue-router'
import CounterExample from './pages/layout/counter-example'
import FetchData from './pages/layout/fetch-data'
import HomePage from './pages/layout/home-page'
import LoginPage from './pages/auth/pages/login'
import RegistrationPage from './pages/auth/pages/registration'
import GamesPage from './pages/games/pages/games/games'
import MyGamesPage from './pages/games/pages/games/my-games'
import BucketPage from './pages/orders/pages/bucket/bucket'

Vue.use(VueRouter);

const routes = [{
        path: '/',
        component: HomePage
    },
    {
        path: '/counter',
        component: CounterExample
    },
    {
        path: '/fetch-data',
        component: FetchData
    },
    {
        path: '/login',
        component: LoginPage
    },
    {
        path: '/registr',
        component: RegistrationPage
    },
    {
        path: '/games',
        component: GamesPage
    },
    {
        path: '/my-games',
        component: MyGamesPage
    },
    {
        path: '/bucket',
        component: BucketPage
    }
]

let router = new VueRouter({
    mode: 'history',
    routes
})

export default router
