<template>
    <div class="form-body">
        <div class="col-lg-6 left-column">
            <form>
                <v-text-field label="Name"
                              v-model="game.name"
                              :error-messages="nameErrors"
                              @input="$v.game.name.$touch()"
                              @blur="$v.game.name.$touch()"
                              required></v-text-field>

                <v-select :items="categories"
                          v-model="game.categoryId"
                          label="Category"
                          single-line
                          bottom
                          item-text="Name"
                          item-value="CategoryId"
                          required></v-select>

                <v-text-field label="Price"
                              v-model="game.price"
                              :error-messages="priceErrors"
                              @input="$v.game.price.$touch()"
                              @blur="$v.game.price.$touch()"
                              prefix="$"
                              required></v-text-field>

                <v-text-field v-model="game.description"
                              label="Description"
                              :error-messages="descriptionErrors"
                              @input="$v.game.description.$touch()"
                              @blur="$v.game.description.$touch()"
                              required
                              multi-line></v-text-field>
            </form>
            <main-file-uploader ref="mainUploader" :baseOptions="baseDropzoneOptions" :mainfileSuccessfullyAdded="mainfileSuccessfullyAdded"></main-file-uploader>
        </div>
        <div class="col-lg-6 right-column">
            <other-file-uploaders ref="otherUploaders" :baseOptions="baseDropzoneOptions" :iconfileSuccessfullyAdded="iconfileSuccessfullyAdded" :galleryFileSuccessfullyAdded="galleryFileSuccessfullyAdded"> </other-file-uploaders>
        </div>
        <div class="row">
            <v-btn class="add-game-btn" color="primary" @click="addGame">Add Game</v-btn>
        </div>
    </div>
</template>

<script>
    import { validationMixin } from "vuelidate";
    import { required } from "vuelidate/lib/validators";

    import * as gameService from "../../api/game-service";

    import mainFileUploaderComponent from "./file-uploader/main-file-uploader";
    import otherFilesUploaderComponent from "./file-uploader/other-file-uploaders";

    export default {
        components: {
            "main-file-uploader": mainFileUploaderComponent,
            "other-file-uploaders": otherFilesUploaderComponent
        },
        props: {
            categories: {
                type: Array,
                required
            }
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
            game: {
                name: "",
                categoryId: 0,
                price: 0,
                description: ""
            },
            files: {
                mainFile: "",
                iconFile: "",
                galleryFiles: []
            }
        }),
        methods: {
            getFileData(file) {
                let fileData = file.dataURL.split("base64,")[1];
                return fileData;
            },
            mainfileSuccessfullyAdded(file, response) {
                this.files.mainFile = this.getFileData(file);
            },
            iconfileSuccessfullyAdded(file, response) {
                this.files.iconFile = this.getFileData(file);
            },
            galleryFileSuccessfullyAdded(file, response) {
                this.files.galleryFiles.push(this.getFileData(file));
            },
            async addGame() {
                let newGame = {
                    Name: this.game.name,
                    CategoryId: this.game.categoryId,
                    Description: this.game.description,
                    Price: this.game.price,
                    Photo: this.files.mainFile,
                    Icon: this.files.iconFile,
                    Photos: this.files.galleryFiles.map(content => {
                        return {
                            Photo: content
                        };
                    })
                };

                await gameService.addGame(newGame);
                
                this.clearGame();
                this.$noty.success("Game was successfylly added.");
            },
            clearGame() {
                this.game.name = "";
                this.game.categoryId = 0;
                this.game.price = 0;
                this.game.description = "";

                this.$refs.mainUploader.$refs.mainDropzoe.removeAllFiles();
                this.$refs.otherUploaders.$refs.iconDropzoe.removeAllFiles();
                this.$refs.otherUploaders.$refs.galleryDropzoe.removeAllFiles();

                this.$v.$reset();
            }
        },
        computed: {
            nameErrors() {
                const errors = [];
                if (!this.$v.game.name.$dirty) return errors;
                !this.$v.game.name.required && errors.push("Name is required.");
                return errors;
            },
            priceErrors() {
                const errors = [];
                if (!this.$v.game.price.$dirty) return errors;
                !this.$v.game.price.required && errors.push("Price is required");
                return errors;
            },
            descriptionErrors() {
                const errors = [];
                if (!this.$v.game.description.$dirty) return errors;
                !this.$v.game.description.required &&
                    errors.push("Description is required");
                return errors;
            }
        }
    };
</script>
