<template>
    <div class="form-body">
        <div class="col-lg-6 left-column">
            <form>
                <v-text-field :label="labels.properties.gameNameLabel"
                              v-model="game.Name"
                              :error-messages="nameErrors"
                              @input="$v.game.name.$touch()"
                              @blur="$v.game.name.$touch()"
                              required></v-text-field>

                <v-select :items="categories"
                          v-model="game.CategoryId"
                          :label="labels.properties.gameCategoryLabel"
                          single-line
                          bottom
                          item-text="Name"
                          item-value="CategoryId"
                          required></v-select>

                <v-text-field :label="labels.properties.gamePriceLabel"
                              v-model="game.Price"
                              :error-messages="priceErrors"
                              @input="$v.game.price.$touch()"
                              @blur="$v.game.price.$touch()"
                              prefix="$"
                              required></v-text-field>

                <v-text-field v-model="game.Description"
                              :label="labels.properties.gameDescriptionLabel"
                              :error-messages="descriptionErrors"
                              @input="$v.game.description.$touch()"
                              @blur="$v.game.description.$touch()"
                              required
                              multi-line></v-text-field>
            </form>
            <hr>
            <div class="current-photo-block">
                <h4>
                    {{labels.properties.currentPhotosLabel}}
                </h4>
                <div class="col-lg-6">
                    <p>
                        {{labels.properties.mainPhotoLabel}}
                    </p>
                    <img :src="game.Photo" width="130" height="180">
                </div>
                <div class="col-lg-6">
                    <p>
                        {{labels.properties.iconPhotoLabel}}
                    </p>
                    <img :src="game.Icon" width="150" height="150">
                </div>

            </div>
        </div>
        <div class="col-lg-6 right-column">
            <file-uploaders ref="fileUploaders" :baseOptions="baseDropzoneOptions" :mainfileSuccessfullyAdded="mainfileSuccessfullyAdded" :iconfileSuccessfullyAdded="iconfileSuccessfullyAdded" :galleryFileSuccessfullyAdded="galleryFileSuccessfullyAdded">
            </file-uploaders>
        </div>
        <div class="row">
            <v-btn class="add-game-btn" color="primary" @click="editGame" :disabled="isInvaild">
                {{labels.commands.updateGameLabel}}
            </v-btn>
        </div>
    </div>
</template>

<script>
    import { validationMixin } from "vuelidate";
    import { required } from "vuelidate/lib/validators";

    import * as gameService from "../../../api/game-service";
    import * as resources from "../../../resources/resources";
    import * as mainStoreActions from "../../../../../store/types/action-types";

    import fileUploadersComponent from "../file-uploaders/file-uploaders";

    export default {
        props: {
            game: {
                type: Object,
                required: true
            },
            categories: {
                type: Array,
                required
            },
            refreshGameAfterUpdate: {
                type: Function
            }
        },
        components: {
            fileUploaders: fileUploadersComponent
        },
        mixins: [validationMixin],
        validations: {
            game: {
                name: { required },
                price: { required },
                description: { required }
            }
        },
        data: () => ({
            baseDropzoneOptions: {
                url: "https://httpbin.org/post",
                thumbnailWidth: 200,
                addRemoveLinks: true
            },
            files: {
                mainFile: "",
                iconFile: "",
                galleryFiles: []
            },
            labels: resources.gameLabels
        }),
        methods: {
            getFileData(file) {
                let fileData = file.dataURL.split("base64,")[1];
                return fileData;
            },
            mainfileSuccessfullyAdded(file, response) {
                this.game.Photo = file.dataURL;
                this.game.PhotoContent = this.getFileData(file);
            },
            iconfileSuccessfullyAdded(file, response) {
                this.game.Icon = file.dataURL;
                this.game.IconContent = this.getFileData(file);
            },
            galleryFileSuccessfullyAdded(file, response) {
                this.game.Photos.push({
                    Content: this.getFileData(file),
                    GameId: this.game.GameId,
                    Photo: file.dataURL
                });
            },
            async editGame() {
                try {
                    this.$store.dispatch(mainStoreActions.START_LOADING_ACTION, "Game is updating ...");

                    await gameService.editGame(this.game);

                    this.clearGame();
                    this.refreshGameAfterUpdate();

                    this.$noty.success(resources.popupMessages.gameUpdatedMessage);

                    this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
                } catch (error) {
                    this.$noty.success(resources.popupMessages.gameUpdatedErrorMessage);
                }
            },
            clearGame() {
                this.$refs.fileUploaders.$refs.mainDropzoe.removeAllFiles();
                this.$refs.fileUploaders.$refs.iconDropzoe.removeAllFiles();
                this.$refs.fileUploaders.$refs.galleryDropzoe.removeAllFiles();

                this.$v.$reset();
            }
        },
        computed: {
            isInvaild() {
                return this.$v.$invalid || !this.game.PhotoContent || this.game.IconContent;
            },
            nameErrors() {
                const errors = [];
                if (!this.$v.game.name.$dirty) return errors;
                !this.$v.game.name.required && errors.push(resources.gameLabels.validationMessages.gameNameRequiredMessage);
                return errors;
            },
            priceErrors() {
                const errors = [];
                if (!this.$v.game.price.$dirty) return errors;
                !this.$v.game.price.required && errors.push(resources.gameLabels.validationMessages.gamePriceRequiredMessage);
                return errors;
            },
            descriptionErrors() {
                const errors = [];
                if (!this.$v.game.description.$dirty) return errors;
                !this.$v.game.description.required &&
                    errors.push(resources.gameLabels.validationMessages.gameDescriptionRequiredMessage);
                return errors;
            }
        }
    };
</script>

<style>
    .current-photo-block p {
        font-weight: bold;
        font-size: 16px;
    }
</style>
