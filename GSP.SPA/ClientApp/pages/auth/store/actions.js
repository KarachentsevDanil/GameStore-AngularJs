import * as authenticationService from '../api/authentication-service';
import * as mutations from './types/mutators-types';

export default {
    async login(context, data) {
        let token = await authenticationService.getToken(data);
        context.commit(mutations.ADD_TOKEN_MUTATOR, token);
    }
}