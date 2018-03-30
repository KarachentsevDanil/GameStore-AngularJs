<template>
    <div>
        <hr>
        <div>
            <p class="bold">
                {{labels.properties.uploadMainPhotoLabel}}*
            </p>
            <vue-dropzone ref="mainDropzoe" @vdropzone-success="mainfileSuccessfullyAdded" id="mainDropzone" :options="mainDropzoneOptions">
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
                labels: resources.gameLabels
            };
        }
    };
</script>
