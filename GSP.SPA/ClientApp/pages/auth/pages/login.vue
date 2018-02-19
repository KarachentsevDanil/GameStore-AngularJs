<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-6 col-lg-offset-3 col-sm-12 authorization-block">
                <v-card class="form-sign-block">
                    <div class="form-header deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-xs-center">
                                Login
                            </span>
                        </v-card-title>
                    </div>
                    <div class="form-body">
                        <form>
                            <v-text-field label="E-mail"
                                          v-model="user.email"
                                          :error-messages="emailErrors"
                                          @input="$v.user.email.$touch()"
                                          @blur="$v.user.email.$touch()"
                                          required></v-text-field>

                            <v-text-field v-model="user.password"
                                          :error-messages="passwordErrors"
                                          label="Password"
                                          hint="At least 6 characters"
                                          min="6"
                                          @input="$v.user.password.$touch()"
                                          @blur="$v.user.password.$touch()"
                                          :type="'password'"
                                          required></v-text-field>

                            <v-checkbox label="Remember me ?" v-model="user.rememberMe"></v-checkbox>

                            <v-btn class="form-button" @click="submit" color="info">Sign In</v-btn>
                        </form>
                    </div>
                </v-card>

                <div class="registr-link-block">
                    New to us? <router-link to="/registr" active-class="active" exact><a>Sign Up</a></router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import * as authActions from "../store/types/action-types";
import * as authGetters from "../store/types/getter-types";
import * as authResources from "../store/resources";
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
        router: this.$router
      };

      this.$store.dispatch(
        authResources.AUTH_STORE_NAMESPACE.concat(authActions.LOGIN_ACTION),
        data
      );
    }
  },
  computed: {
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
        errors.push("Password must be at least 6 characters long");
      !this.$v.user.password.required && errors.push("Password is required.");
      return errors;
    },
    emailErrors() {
      const errors = [];
      if (!this.$v.user.email.$dirty) return errors;
      !this.$v.user.email.email && errors.push("Must be valid e-mail");
      !this.$v.user.email.required && errors.push("E-mail is required");
      return errors;
    }
  }
};
</script>
