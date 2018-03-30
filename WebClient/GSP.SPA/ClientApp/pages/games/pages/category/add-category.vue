<template>
    <v-dialog v-model="dialog.showDialog" max-width="400">
        <v-card>
            <v-card-title class="headline"> {{labels.headers.addCategoryLabel}} </v-card-title>
            <v-card-text>
                <v-text-field :label="labels.properties.categoryNameLabel"
                              v-model="category.name"
                              :error-messages="categoryNameErrors"
                              @input="$v.category.name.$touch()"
                              @blur="$v.category.name.$touch()"
                              required></v-text-field>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="addCategory" :disabled="isInvaild">
                    {{labels.commands.addCategoryLabel}}
                </v-btn>
                <v-btn @click.native="dialog.showDialog = false">
                    {{labels.commands.closeLabel}}
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
    import { validationMixin } from "vuelidate";
    import { required } from "vuelidate/lib/validators";

    import * as categoryService from "../../api/category-service";
    import * as resources from "../../resources/resources";

    export default {
        props: {
            dialog: {
                type: Object,
                required: true
            },
            refreshCategories: {
                type: Function
            }
        },
        mixins: [validationMixin],

        validations: {
            category: {
                name: { required }
            }
        },
        data() {
            return {
                category: {
                    name: ""
                },
                labels: resources.categoryLabels
            };
        },
        computed: {
            isInvaild() {
                return this.$v.$invalid;
            },
            categoryNameErrors() {
                const errors = [];
                if (!this.$v.category.name.$dirty) return errors;
                !this.$v.category.name.required && errors.push(resources.categoryLabels.validationMessages.categoryNameRequired);
                return errors;
            }
        },
        methods: {
            async addCategory() {
                let newCategory = {
                    Name: this.category.name
                };

                await categoryService.addCategory(newCategory);
                this.categories = (await categoryService.getCategories()).data.Data;

                this.clearCategory();
                this.refreshCategories();

                this.dialog.showDialog = false;
                this.$noty.success(resources.popupMessages.categoryCreatedMessage);
            },
            clearCategory() {
                this.category.name = "";
                this.$v.$reset();
            }
        }
    };
</script>
