import * as authenticationService from '../api/authentication-service';
import * as mutations from './types/mutators-types';

export default {
    async login(context, data) {
        try {
            let userDate = await authenticationService.login(data.user);

            if (userDate.data) {
                context.commit(mutations.SET_TOKEN_MUTATOR, userDate.data.token);
                context.commit(mutations.SET_USER_MUTATOR, userDate.data.user);
                data.router.push('/games');
            }
            
            notification.error('Email or password was incorrect. Try again.');
        } catch (error) {
            notification.error('Email or password was incorrect. Try again.');
        }
    }
}
