import * as orderService from "../api/order-service";
import * as mutations from "./types/mutators-types";
import * as resources from "../resources/resources";

export default {
    async addGameToBucket(context, data) {

        try {
            let isSuccess = await orderService.addGameToBucket(data);
            if (isSuccess.status === 200) {
                let games = (await orderService.getGamesFromBucket(data.CustomerId))
                    .data;

                data.notify.success(resources.popupMessages.gameAddedToBucket);
                context.commit(mutations.ADD_GAME_TO_BUCKET_ACTION, games);
            }
        } catch (error) {
            data.notify.warning(resources.popupMessages.gameAlreadyBought);
        }
    },
    async getGamesFromBucket(context, data) {
        let games = (await orderService.getGamesFromBucket(data.CustomerId))
            .data;

        context.commit(mutations.ADD_GAME_TO_BUCKET_ACTION, games);
    }
};
