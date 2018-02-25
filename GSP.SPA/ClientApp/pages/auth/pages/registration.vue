<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-6 col-lg-offset-3 col-sm-12 authorization-block">
                <v-card class="form-sign-block">
                    <div class="form-header deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-xs-center">
                                Resitration
                            </span>
                        </v-card-title>
                    </div>
                    <div class="form-body">
                        <form>
                            <v-text-field label="Full Name"
                                          v-model="user.fullName"
                                          :error-messages="fullNameErrors"
                                          @input="$v.user.fullName.$touch()"
                                          @blur="$v.user.fullName.$touch()"
                                          required></v-text-field>

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

                            <v-btn class="form-button" @click="submit" color="info">Registr</v-btn>
                        </form>
                    </div>
                </v-card>

                <div class="registr-link-block">
                    Have account? <router-link to="/login" active-class="active" exact><a>Sign In</a></router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import * as authenticationService from "../api/authentication-service";
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

      try {
        let response = await authenticationService.registr(data);
        this.$noty.success("Account was created.");
        this.$router.push("/login");
      } catch (error) {
        this.$noty.error("Error occure when registr user.");
      }
    }
  },
  computed: {
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
    },
    fullNameErrors() {
      const errors = [];
      if (!this.$v.user.fullName.$dirty) return errors;
      !this.$v.user.fullName.required && errors.push("Full Name is required");
      return errors;
    }
  }
};
</script>
