<template>
    <v-dialog v-model="dialog.showDialog" max-width="400">
        <v-card>
            <v-card-title class="headline">Add Category</v-card-title>
            <v-card-text>
                <v-text-field label="Name"
                              v-model="category.name"
                              :error-messages="categoryNameErrors"
                              @input="$v.category.name.$touch()"
                              @blur="$v.category.name.$touch()"
                              required></v-text-field>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="addCategory">Add</v-btn>
                <v-btn @click.native="dialog.showDialog = false">Close</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
    import { validationMixin } from "vuelidate";
    import { required } from "vuelidate/lib/validators";

    import * as categoryService from "../../../api/category-service";

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
                }
            };
        },
        computed: {
            categoryNameErrors() {
                const errors = [];
                if (!this.$v.category.name.$dirty) return errors;
                !this.$v.category.name.required && errors.push("Name is required.");
                return errors;
            }
        },
        methods: {
            async addCategory() {
                let newCategory = {
                    Name: this.category.name
                };

                await categoryService.addCategory(newCategory);
                this.categories = (await categoryService.getCategories()).data;

                this.clearCategory();
                this.refreshCategories();

                this.dialog.showDialog = false;
                this.$noty.success("Category was successfylly added.");
            },
            clearCategory() {
                this.category.name = "";
                this.$v.$reset();
            }
        }
    };
</script>
