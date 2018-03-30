<template>
    <form>
        <div>
            <div class="row">
                <div class="col-lg-9 col-sm-12">

                    <v-text-field :label="resources.properties.creditCardNumberLabel"
                                  v-model="paymentInfo.creditCard"
                                  :error-messages="creditCardErrors"
                                  :counter="16"
                                  :mask="'credit-card'"
                                  @input="$v.paymentInfo.creditCard.$touch()"
                                  @blur="$v.paymentInfo.creditCard.$touch()"
                                  required></v-text-field>
                </div>
                <div class="col-lg-3 col-sm-12">
                    <v-text-field :label="resources.properties.cvvCodeLabel"
                                  v-model="paymentInfo.cvvCode"
                                  :error-messages="cvvCodeErrors"
                                  :counter="4"
                                  @input="$v.paymentInfo.cvvCode.$touch()"
                                  @blur="$v.paymentInfo.cvvCode.$touch()"
                                  required></v-text-field>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-sm-12">
                    <v-select :label="resources.properties.expirationMonthLabel"
                              v-model="paymentInfo.expirationMonth"
                              :items="months"
                              item-text="name"
                              item-value="id"
                              :error-messages="expirationMonthErrors"
                              @change="$v.paymentInfo.expirationMonth.$touch()"
                              @blur="$v.paymentInfo.expirationMonth.$touch()"
                              required></v-select>
                </div>
                <div class="col-lg-6 col-sm-12">
                    <v-select :label="resources.properties.expirationYearLabel"
                              v-model="paymentInfo.expirationYear"
                              :items="years"
                              :error-messages="expirationYearErrors"
                              @change="$v.paymentInfo.expirationYear.$touch()"
                              @blur="$v.paymentInfo.expirationYear.$touch()"
                              required></v-select>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12">
                    <v-select :label="resources.properties.countryLabel"
                              v-model="paymentInfo.country"
                              :items="contries"
                              :error-messages="countryErrors"
                              @change="$v.paymentInfo.country.$touch()"
                              @blur="$v.paymentInfo.country.$touch()"
                              required></v-select>
                </div>

                <div class="col-lg-12 col-sm-12">

                    <v-text-field :label="resources.properties.fullNameLabel"
                                  v-model="paymentInfo.fullName"
                                  :error-messages="fullNameErrors"
                                  @input="$v.paymentInfo.fullName.$touch()"
                                  @blur="$v.paymentInfo.fullName.$touch()"
                                  required></v-text-field>
                </div>
            </div>

            <v-btn class="pull-right" color="primary" @click.native="back"> {{resources.commands.cancelLabel}} </v-btn>
            <v-btn class="pull-right" color="primary" :disabled="isInvaild" @click.native="dialogInfo.currentTab = 2"> {{resources.commands.proccedReviewOrderLabel }} </v-btn>
        </div>

    </form>
</template>

<script>
    import * as resources from "../../../../resources/resources";

    import { validationMixin } from "vuelidate";
    import { required, maxLength, minLength } from "vuelidate/lib/validators";
    const rangeFunc = (start, end) =>
        Array.from({ length: end - start }, (v, k) => k + start);

    export default {
        mixins: [validationMixin],

        validations: {
            paymentInfo: {
                creditCard: { required },
                cvvCode: { required, maxLength: maxLength(4), minLength: minLength(3) },
                expirationMonth: { required },
                expirationYear: { required },
                fullName: { required },
                country: { required }
            }
        },
        data() {
            return {
                resources: {
                    ...resources.lables
                },
                contries: ["Ukraine", "USA", "Canada"],
                months: [
                    {
                        id: 0,
                        name: "January"
                    },
                    {
                        id: 1,
                        name: "February"
                    },
                    {
                        id: 2,
                        name: "March"
                    },
                    {
                        id: 3,
                        name: "April"
                    },
                    {
                        id: 4,
                        name: "May"
                    },
                    {
                        id: 5,
                        name: "June"
                    },
                    {
                        id: 6,
                        name: "July"
                    },
                    {
                        id: 7,
                        name: "August"
                    },
                    {
                        id: 8,
                        name: "September"
                    },
                    {
                        id: 9,
                        name: "October"
                    },
                    {
                        id: 10,
                        name: "November"
                    },
                    {
                        id: 11,
                        name: "December"
                    }
                ],
                years: rangeFunc(
                    this.paymentInfo.expirationYear,
                    this.paymentInfo.expirationYear + 10
                )
            };
        },
        props: {
            dialogInfo: {
                type: Object
            },
            paymentInfo: {
                type: Object
            },
            back: {
                type: Function
            }
        },
        computed: {
            isInvaild() {
                return this.$v.$invalid;
            },
            creditCardErrors() {
                const errors = [];
                if (!this.$v.paymentInfo.creditCard.$dirty) return errors;
                !this.$v.paymentInfo.creditCard.required &&
                    errors.push(
                        resources.lables.validationMessages.creditCardRequiredMessage
                    );
                return errors;
            },
            cvvCodeErrors() {
                const errors = [];
                if (!this.$v.paymentInfo.cvvCode.$dirty) return errors;
                !this.$v.paymentInfo.cvvCode.required &&
                    errors.push(resources.lables.validationMessages.cvvCodeRequiredMessage);
                !this.$v.paymentInfo.cvvCode.minLength &&
                    errors.push(
                        resources.lables.validationMessages.cvvCodeMinLengthMessage
                    );
                !this.$v.paymentInfo.cvvCode.maxLength &&
                    errors.push(
                        resources.lables.validationMessages.cvvCodeMaxLengthMessage
                    );
                return errors;
            },
            expirationMonthErrors() {
                const errors = [];
                if (!this.$v.paymentInfo.expirationMonth.$dirty) return errors;
                !this.$v.paymentInfo.expirationMonth.required &&
                    errors.push(
                        resources.lables.validationMessages.expirationMonthRequiredMessage
                    );
                return errors;
            },
            expirationYearErrors() {
                const errors = [];
                if (!this.$v.paymentInfo.expirationYear.$dirty) return errors;
                !this.$v.paymentInfo.expirationYear.required &&
                    errors.push(
                        resources.lables.validationMessages.expirationYearRequiredMessage
                    );
                return errors;
            },
            fullNameErrors() {
                const errors = [];
                if (!this.$v.paymentInfo.fullName.$dirty) return errors;
                !this.$v.paymentInfo.fullName.required &&
                    errors.push(
                        resources.lables.validationMessages.fullNameRequiredMessage
                    );
                return errors;
            },
            countryErrors() {
                const errors = [];
                if (!this.$v.paymentInfo.country.$dirty) return errors;
                !this.$v.paymentInfo.country.required &&
                    errors.push(resources.lables.validationMessages.countryRequiredMessage);
                return errors;
            }
        }
    };
</script>
