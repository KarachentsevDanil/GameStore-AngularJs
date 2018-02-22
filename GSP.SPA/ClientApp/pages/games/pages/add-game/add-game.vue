<template>
    <div class="container">
        <div class="row">
            <div class="col-lg-10 col-lg-offset-1 authorization-block">
                <v-card class="form-sign-block">
                    <div class="form-header deep-purple darken-1">
                        <v-card-title class="white--text deep-purple darken-1">
                            <span class="text-xs-center">
                                Add Game
                            </span>
                            <v-spacer></v-spacer>
                            <v-btn color="primary" @click.native.stop="dialog = true"> Add Category </v-btn>
                        </v-card-title>
                    </div>
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
                            <hr>
                            <div>
                                <p class="bold">
                                    Upload main photo:
                                </p>
                                <vue-dropzone @vdropzone-success="mainfileSuccessfullyAdded" id="mainDropzone" :options="mainDropzoneOptions">
                                </vue-dropzone>
                            </div>
                            <hr>

                        </div>
                        <div class="col-lg-6 right-column">
                            <div>
                                <p class="bold">
                                    Upload icon photo:
                                </p>
                                <vue-dropzone @vdropzone-success="iconfileSuccessfullyAdded" id="iconDropzone" :options="iconDropzoneOptions">
                                </vue-dropzone>
                            </div>

                            <hr>
                            <div>
                                <p class="bold">
                                    Upload photos for galery:
                                </p>
                                <vue-dropzone @vdropzone-success="galleryFileSuccessfullyAdded" id="galleryDropzone" :options="galeryDropzoneOptions">
                                </vue-dropzone>
                            </div>
                            <hr>
                        </div>
                        <div class="row">
                            <v-btn class="add-game-btn" color="primary" @click="addGame">Add Game</v-btn>
                        </div>
                    </div>
                </v-card>
            </div>
        </div>
        <v-dialog v-model="dialog" max-width="400">
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
                    <v-btn @click.native="dialog = false">Close</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>

<script>
import { mapGetters } from "vuex";
import { validationMixin } from "vuelidate";
import { required, minLength, email } from "vuelidate/lib/validators";

import * as gameService from "../../api/game-service";
import * as categoryService from "../../api/category-service";

const baseDropzoneOptions = {
  url: "https://httpbin.org/post",
  thumbnailWidth: 200,
  addRemoveLinks: true
};

export default {
  mixins: [validationMixin],

  validations: {
    category: {
      name: { required }
    },
    game: {
      name: { required },
      price: { required },
      description: { required }
    }
  },
  data: () => ({
    dialog: false,
    category: {
      name: ""
    },
    game: {
      name: "",
      categoryId: 0,
      price: 0,
      description: ""
    },
    categories: [],
    files: {
      mainFile: "",
      iconFile: "",
      galleryFiles: []
    },
    mainDropzoneOptions: {
      ...baseDropzoneOptions,
      maxFiles: 1,
      dictDefaultMessage:
        "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Upload main photo</span>"
    },
    iconDropzoneOptions: {
      ...baseDropzoneOptions,
      maxFiles: 1,
      dictDefaultMessage:
        "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Upload icon photo</span>"
    },
    galeryDropzoneOptions: {
      ...baseDropzoneOptions,
      dictDefaultMessage:
        "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Upload photo's for gallery</span>"
    }
  }),
  async beforeMount() {
    this.categories = (await categoryService.getCategories()).data;
  },
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
    async addCategory() {
      let newCategory = {
        Name: this.category.name
      };

      await categoryService.addCategory(newCategory);
      this.categories = (await categoryService.getCategories()).data;
      this.clearCategory();
      this.dialog = false;
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
    },
    clearCategory() {
      this.category.name = "";
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
    categoryNameErrors() {
      const errors = [];
      if (!this.$v.category.name.$dirty) return errors;
      !this.$v.category.name.required && errors.push("Name is required.");
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
