import CounterExample from './pages/layout/counter-example'
import FetchData from './pages/layout/fetch-data'
import HomePage from './pages/layout/home-page'
import LoginPage from './pages/auth/pages/login'

export const routes = [
    { path: '/', component: HomePage, display: 'Home', style: 'glyphicon glyphicon-home' },
    { path: '/counter', component: CounterExample, display: 'Counter', style: 'glyphicon glyphicon-education' },
    { path: '/fetch-data', component: FetchData, display: 'Fetch data', style: 'glyphicon glyphicon-th-list' },
    { path: '/login', component: LoginPage, display: 'Login', style: 'glyphicon glyphicon-th-list' }
]
