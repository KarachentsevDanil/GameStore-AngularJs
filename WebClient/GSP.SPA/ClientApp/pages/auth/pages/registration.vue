<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-6 col-lg-offset-3 col-sm-12 authorization-block">
                <v-card class="form-sign-block">
                    <div class="form-header deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-xs-center">
                                {{ labels.headers.resitrationLabel }}
                            </span>
                        </v-card-title>
                    </div>
                    <div class="form-body">
                        <form>
                            <v-text-field :label="labels.properties.fullNameLabel"
                                          v-model="user.fullName"
                                          :error-messages="fullNameErrors"
                                          @input="$v.user.fullName.$touch()"
                                          @blur="$v.user.fullName.$touch()"
                                          required></v-text-field>

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

                            <v-menu ref="menu"
                                    lazy
                                    :close-on-content-click="false"
                                    v-model="menu"
                                    transition="scale-transition"
                                    offset-y
                                    full-width
                                    :nudge-right="40"
                                    min-width="290px"
                                    :return-value.sync="user.birthday">
                                <v-text-field slot="activator"
                                              label="Picker in menu"
                                              v-model="user.birthday"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="user.birthday" no-title scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn flat color="primary" @click="menu = false">Cancel</v-btn>
                                    <v-btn flat color="primary" @click="$refs.menu.save(user.birthday)">OK</v-btn>
                                </v-date-picker>
                            </v-menu>

                            <v-btn class="form-button" :disabled="isInvaild" @click="submit" color="info">{{labels.commands.registrLabel}}</v-btn>
                        </form>
                    </div>
                </v-card>

                <div class="registr-link-block">
                    {{labels.commands.haveAccountLabel}} <router-link to="/login" active-class="active" exact><a>{{labels.commands.signInLabel}}</a></router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import * as authenticationService from "../api/authentication-service";
    import * as authTextResources from "../resources/resources";
    import * as mainStoreActions from "../../../store/types/action-types";

    import { mapGetters } from "vuex";
    import { validationMixin } from "vuelidate";
    import { required, minLength, email } from "vuelidate/lib/validators";

    export default {
        mixins: [validationMixin],

        validations: {
            user: {
                email: { required, email },
                password: { required, minLength: minLength(6) },
                fullName: { required }
            }
        },
        data: () => ({
            user: {
                email: "",
                password: "",
                fullName: "",
                birthday: null
            },
            labels: {
                ...authTextResources.lables
            },
            menu: false
        }),
        methods: {
            async submit() {
                let data = {
                    Email: this.user.email,
                    Password: this.user.password,
                    FullName: this.user.fullName,
                    DateOfBirthsday: this.user.birthday
                };

                this.$store.dispatch(mainStoreActions.START_LOADING_ACTION, "Account is creating ...");

                let response = await authenticationService.registr(data);

                if (response.data.IsSuccess) {
                    this.$noty.success(authTextResources.popupMessages.accountCreatedMessage);
                    this.$router.push("/login");
                } else {
                    this.$noty.error(authTextResources.popupMessages.accountCreationFailedMessage);
                }

                this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
            }
        },
        computed: {
            isInvaild() {
                return this.$v.$invalid;
            },
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
            },
            fullNameErrors() {
                const errors = [];
                if (!this.$v.user.fullName.$dirty) return errors;
                !this.$v.user.fullName.required && errors.push(authTextResources.lables.validationMessages.fullNameRequiredMessage);
                return errors;
            }
        }
    };
</script>
