<template>
    <div class="row">
        <div class="col-lg-12 authorization-block">
            <v-card class="form-sign-block">
                <div class="form-header deep-purple darken-1">
                    <v-card-title class="white--text deep-purple darken-1">
                        <span class="text-xs-center">
                            {{labels.headers.editGameLabel}}
                        </span>
                        <v-spacer></v-spacer>
                        <v-btn color="primary" @click.native.stop="dialog.showDialog = true">
                            {{labels.commands.addCategoryLabel}}
                        </v-btn>
                    </v-card-title>
                </div>

                <edit-game :game="game" ref="editGame" :refreshGameAfterUpdate="refreshGameAfterUpdate" :categories="categories"></edit-game>
                <add-category :dialog="dialog" :refreshCategories="refreshCategories"></add-category>
            </v-card>
        </div>
    </div>
</template>

<script>
    import addCategoryComponent from "../../category/add-category";
    import editGameComponent from "./edit-game";
    import * as resources from "../../../resources/resources";
    import * as categoryService from '../../../api/category-service';

    export default {
        components: {
            editGame: editGameComponent,
            addCategory: addCategoryComponent
        },
        props: {
            game: {
                type: Object,
                required: true
            },
            categories: {
                type: Array
            },
            refreshGameAfterUpdate: {
                type: Function
            },
            refreshCategories: {
                type: Function
            }
        },
        data: () => ({
            dialog: {
                showDialog: false
            },
            labels: {
                ...resources.gameLabels
            }
        })
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
