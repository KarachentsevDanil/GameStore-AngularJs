<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-6 col-lg-offset-3">
                <v-form v-model="valid">
                    <v-text-field label="E-mail"
                                  v-model="email"
                                  :rules="emailRules"
                                  required></v-text-field>

                    <v-text-field label="Password"
                                  v-model="password"
                                  :rules="passwordRules"
                                  :type="'password'"
                                  required></v-text-field>

                    <v-btn @click="submit"
                           :disabled="!valid">
                        submit
                    </v-btn>
                    <v-btn @click="clear">clear</v-btn>
                </v-form>
                <p>
                    Username: {{ getUsername }} Toke: {{ getToken }}
                </p>
            </div>
        </div>
    </div>
</template>

<script>
    import * as authActions from "../store/types/action-types";
    import * as authGetters from "../store/types/getter-types";
    import * as authResources from "../store/resources";
    import { mapGetters } from "vuex";

    export default {
        data: () => ({
            valid: false,
            email: "",
            emailRules: [
                v => !!v || "E-mail is required",
                v =>
                    /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
                    "E-mail must be valid"
            ],
            password: "",
            passwordRules: [v => !!v || "Password is required"]
        }),
        methods: {
            submit() {
                let data = {
                    email: this.email,
                    password: this.password
                };

                this.$store.dispatch(
                    authResources.AUTH_STORE_NAMESPACE.concat(authActions.LOGIN_ACTION),
                    data
                );
            },
            clear() {
                this.$refs.form.reset();
            }
        },
        computed: {
            ...mapGetters({
                getUsername: authResources.AUTH_STORE_NAMESPACE.concat(authGetters.GET_USERNAME_GETTER),
                getToken: authResources.AUTH_STORE_NAMESPACE.concat(authGetters.GET_TOKEN_GETTER)
            })
        }
    };
</script>

<style scoped>
    div.container {
        margin: 60px 0px 0px 0px;
    }
</style>

