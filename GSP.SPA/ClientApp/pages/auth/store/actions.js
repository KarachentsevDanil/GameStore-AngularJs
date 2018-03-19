import * as authenticationService from '../api/authentication-service';
import * as mutations from './types/mutators-types';
import * as authResources from '../resources/resources';

export default {
    async login(context, data) {
        let userDate = await authenticationService.login(data.user);

        if (!userDate.data.ErrorMessage) {
            context.commit(mutations.SET_TOKEN_MUTATOR, userDate.data.token);
            context.commit(mutations.SET_USER_MUTATOR, userDate.data.user);
            data.router.push('/games');
        } else {
            data.notification.error(authResources.popupMessages.loginFailedMessage);
        }
    },
    logout(context) {
        context.commit(mutations.USER_LOGOUT_MUTATOR);
    }
}
