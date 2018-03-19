import GamesPage from "../pages/games/pages/games/games";
import MyGamesPage from "../pages/games/pages/games/my-games";
import GameDetails from "../pages/games/pages/games/details/game-details-layout";

import AddGamePage from "../pages/games/pages/add-game/add-game-layout";
import EditGamesPage from "../pages/games/pages/edit-games/edit-games";

import * as routeGuards from "./route-guards";

export default [
    {
        path: "/add-game",
        component: AddGamePage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateAdminRoute(to, from, next);
        }
    },
    {
        path: "/edit-games",
        component: EditGamesPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateAdminRoute(to, from, next);
        }
    },
    {
        path: "/games",
        name: "games",
        component: GamesPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateUserRoute(to, from, next);
        }
    },
    {
        path: "/game-details/:id",
        component: GameDetails,
        props: true,
        beforeEnter: (to, from, next) => {
            routeGuards.validateUserRoute(to, from, next);
        }
    },
    {
        path: "/my-games",
        component: MyGamesPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateUserRoute(to, from, next);
        }
    }
];
