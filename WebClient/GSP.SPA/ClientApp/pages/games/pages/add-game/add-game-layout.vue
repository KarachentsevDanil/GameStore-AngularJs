<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-10 col-lg-offset-1 authorization-block">
                <v-card class="form-sign-block">
                    <div class="form-header deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-xs-center">
                                {{labels.headers.addGameLabel}}
                            </span>
                            <v-spacer></v-spacer>
                            <v-btn color="primary" @click.native.stop="dialog.showDialog = true"> {{labels.commands.addCategoryLabel}} </v-btn>
                        </v-card-title>
                    </div>

                    <add-game :categories="categories"></add-game>
                    <add-category :dialog="dialog" :refreshCategories="refreshCategories"></add-category>
                </v-card>
            </div>
        </div>
    </div>
</template>

<script>
    import * as categoryService from "../../api/category-service";
    import * as resources from "../../resources/resources";

    import addCategoryComponent from "../category/add-category";
    import addGameComponent from "./add-game";

    export default {
        components: {
            addGame: addGameComponent,
            addCategory: addCategoryComponent
        },
        data: () => ({
            dialog: {
                showDialog: false
            },
            categories: [],
            labels: {
                ...resources.gameLabels
            }
        }),
        async beforeMount() {
            this.categories = (await categoryService.getCategories()).data.Data;
        },
        methods: {
            async refreshCategories() {
                this.categories = (await categoryService.getCategories()).data.Data;
            }
        }
    };
</script>

<style>
    .upload-text {
        font-size: 26px;
    }

    .right-column {
        border-left: 2px solid whitesmoke;
    }

    p.bold {
        font-weight: bold;
        text-align: left;
    }

    .add-game-btn {
        width: 95%;
        margin-top: 10px;
    }
</style>
