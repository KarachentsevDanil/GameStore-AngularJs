import * as orderService from "../api/order-service";
import * as mutations from "./types/mutators-types";
import * as resources from "../resources/resources";

export default {
    async addGameToBucket(context, data) {
        let isSuccess = await orderService.addGameToBucket(data);
        if (isSuccess.data.IsSuccess) {
            let games = (await orderService.getGamesFromBucket(data.CustomerId))
                .data.Data;

            data.notify.success(resources.popupMessages.gameAddedToBucket);
            context.commit(mutations.ADD_GAME_TO_BUCKET_ACTION, games);
        } else {
            data.notify.warning(isSuccess.data.ErrorMessage);
        }
    },
    async getGamesFromBucket(context, data) {
        let games = (await orderService.getGamesFromBucket(data.CustomerId))
            .data.Data;

        context.commit(mutations.ADD_GAME_TO_BUCKET_ACTION, games);
    }
};
