<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-6 col-lg-offset-3 col-sm-12 authorization-block">
                <v-card class="form-sign-block">
                    <div class="form-header deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-xs-center">
                                {{labels.headers.loginLabel}}
                            </span>
                        </v-card-title>
                    </div>
                    <div class="form-body">
                        <form>
                            <v-text-field :label="labels.properties.emailLable"
                                          v-model="user.email"
                                          :error-messages="emailErrors"
                                          @input="$v.user.email.$touch()"
                                          @blur="$v.user.email.$touch()"
                                          required></v-text-field>

                            <v-text-field v-model="user.password"
                                          :error-messages="passwordErrors"
                                          :label="labels.properties.passwordLabel"
                                          hint="At least 6 characters"
                                          min="6"
                                          @input="$v.user.password.$touch()"
                                          @blur="$v.user.password.$touch()"
                                          :type="'password'"
                                          required></v-text-field>

                            <v-checkbox :label="labels.properties.remeberMeLabel" v-model="user.rememberMe"></v-checkbox>

                            <v-btn class="form-button" @click="submit" color="info" :disabled="isInvaild">{{labels.commands.signInLabel}}</v-btn>
                        </form>
                    </div>
                </v-card>

                <div class="registr-link-block">
                    {{labels.commands.newToUsLabel}} <router-link to="/registr" active-class="active" exact><a> {{labels.commands.signUpLabel}} </a></router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import * as authActions from "../store/types/action-types";
    import * as authGetters from "../store/types/getter-types";
    import * as authResources from "../store/resources";
    import * as authTextResources from "../resources/resources";
    import * as routeGuards from '../../../routes/route-guards';

    import { mapGetters } from "vuex";
    import { validationMixin } from "vuelidate";
    import { required, minLength, email } from "vuelidate/lib/validators";

    export default {
        mixins: [validationMixin],

        validations: {
            user: {
                email: { required, email },
                password: { required, minLength: minLength(6) }
            }
        },
        data: () => ({
            user: {
                email: "",
                password: "",
                rememberMe: false
            },
            labels: {
                ...authTextResources.lables
            }
        }),
        methods: {
            submit() {
                let data = {
                    user: {
                        Email: this.user.email,
                        Password: this.user.password,
                        RememberMe: this.user.rememberMe
                    },
                    router: this.$router,
                    notification: this.$noty
                };

                this.$store.dispatch(
                    authResources.AUTH_STORE_NAMESPACE.concat(authActions.LOGIN_ACTION),
                    data
                );
            }
        },
        computed: {
            isInvaild() {
                return this.$v.$invalid;
            },
            ...mapGetters({
                getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                ),
                getToken: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_TOKEN_GETTER
                )
            }),
            passwordErrors() {
                const errors = [];
                if (!this.$v.user.password.$dirty) return errors;
                !this.$v.user.password.minLength &&
                    errors.push(authTextResources.lables.validationMessages.passwordLengthMessage);
                !this.$v.user.password.required && errors.push(authTextResources.lables.validationMessages.passwordRequiredMessage);
                return errors;
            },
            emailErrors() {
                const errors = [];
                if (!this.$v.user.email.$dirty) return errors;
                !this.$v.user.email.email && errors.push(authTextResources.lables.validationMessages.emailMessage);
                !this.$v.user.email.required && errors.push(authTextResources.lables.validationMessages.emailRequiredMessage);
                return errors;
            }
        },
        beforeRouteEnter: (to, from, next) => {
            routeGuards.redirectToHomePage(to, from, next);
        }
    };
</script>
