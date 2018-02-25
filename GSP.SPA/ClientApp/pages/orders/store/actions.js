import * as orderService from "../api/order-service";
import * as mutations from "./types/mutators-types";

export default {
    async addGameToBucket(context, data) {

        try {
            let isSuccess = await orderService.addGameToBucket(data);
            if (isSuccess.status === 200) {
                let games = (await orderService.getGamesFromBucket(data.CustomerId))
                    .data;

                data.notify.success('Game was successfully added to bucket.');
                context.commit(mutations.ADD_GAME_TO_BUCKET_ACTION, games);
            }
        } catch (error) {
            data.notify.warning('Game already bought.');
        }
    },
    async getGamesFromBucket(context, data) {
        let games = (await orderService.getGamesFromBucket(data.CustomerId))
            .data;

        context.commit(mutations.ADD_GAME_TO_BUCKET_ACTION, games);
    }
};
