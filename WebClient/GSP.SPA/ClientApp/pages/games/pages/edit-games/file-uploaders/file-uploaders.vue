<template>
    <div>
        <div>
            <p class="bold">
                {{labels.properties.updateMainPhotoLabel}}
            </p>
            <vue-dropzone ref="mainDropzoe" @vdropzone-success="mainfileSuccessfullyAdded" id="mainDropzone" :options="mainDropzoneOptions">
            </vue-dropzone>
        </div>
        <hr>
        <div>
            <p class="bold">
                {{labels.properties.updateIconPhotoLabel}}
            </p>
            <vue-dropzone ref="iconDropzoe" @vdropzone-success="iconfileSuccessfullyAdded" id="iconDropzone" :options="iconDropzoneOptions">
            </vue-dropzone>
        </div>

        <hr>
        <div>
            <p class="bold">
                {{labels.properties.uploadExtraPhotosLabel}}
            </p>
            <vue-dropzone ref="galleryDropzoe" @vdropzone-success="galleryFileSuccessfullyAdded" id="galleryDropzone" :options="galeryDropzoneOptions">
            </vue-dropzone>
        </div>
        <hr>
        <div>
            <p class="bold">
                {{labels.properties.updateGameFileLabel}}
            </p>
            <vue-dropzone ref="gameFileDropzoe" @vdropzone-success="gameFileSuccessfullyAdded" id="gameFileDropzone" :options="gameFileDropzoneOptions">
            </vue-dropzone>
        </div>
        <hr>
    </div>
</template>

<script>
    import * as resources from "../../../resources/resources";

    export default {
        props: {
            baseOptions: {
                type: Object,
                required: true
            },
            mainfileSuccessfullyAdded: {
                type: Function
            },
            iconfileSuccessfullyAdded: {
                type: Function
            },
            galleryFileSuccessfullyAdded: {
                type: Function
            },
            gameFileSuccessfullyAdded:{
                type:Function
            }
        },
        data() {
            return {
                mainDropzoneOptions: {
                    ...this.baseOptions,
                    maxFiles: 1,
                    dictDefaultMessage:
                    "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Upload main photo</span>"
                },
                iconDropzoneOptions: {
                    ...this.baseOptions,
                    maxFiles: 1,
                    dictDefaultMessage:
                    "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Upload icon photo</span>"
                },
                galeryDropzoneOptions: {
                    ...this.baseOptions,
                    dictDefaultMessage:
                    "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Upload photo's for gallery</span>"
                },
                gameFileDropzoneOptions: {
                    ...this.baseOptions,
                    dictDefaultMessage:
                    "<span class='upload-text'><i class='fal fa-cloud-upload'></i> Update file for game download</span>"
                },
                labels: resources.gameLabels
            };
        }
    };
</script>
