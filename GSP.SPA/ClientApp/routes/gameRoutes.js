import GamesPage from "../pages/games/pages/games/games";
import MyGamesPage from "../pages/games/pages/games/my-games";
import GameDetails from "../pages/games/pages/games/details/game-details-layout";

import AddGamePage from "../pages/games/pages/add-game/add-game-layout";
import EditGamesPage from "../pages/games/pages/edit-games/edit-games";

export default [
    {
        path: "/add-game",
        component: AddGamePage
    },
    {
        path: "/edit-games",
        component: EditGamesPage
    },
    {
        path: "/games",
        name: "games",
        component: GamesPage
    },
    {
        path: "/game-details/:id",
        component: GameDetails,
        props: true
    },
    {
        path: "/my-games",
        component: MyGamesPage
    }
];
