<template>
    <div>
        <div>
            <p class="bold">
                Upload icon photo:
            </p>
            <vue-dropzone ref="iconDropzoe" @vdropzone-success="iconfileSuccessfullyAdded" id="iconDropzone" :options="iconDropzoneOptions">
            </vue-dropzone>
        </div>

        <hr>
        <div>
            <p class="bold">
                Upload photos for galery:
            </p>
            <vue-dropzone ref="galleryDropzoe" @vdropzone-success="galleryFileSuccessfullyAdded" id="galleryDropzone" :options="galeryDropzoneOptions">
            </vue-dropzone>
        </div>
        <hr>
    </div>
</template>

<script>
export default {
  props: {
    baseOptions: {
      type: Object,
      required: true
    },
    iconfileSuccessfullyAdded: {
      type: Function
    },
    galleryFileSuccessfullyAdded: {
      type: Function
    }
  },
  data() {
    return {
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
      }
    };
  },
  clearPhotos() {
    this.$refs.iconDropzoe.removeAllFiles();
    this.$refs.galleryDropzoe.removeAllFiles();
  }
};
</script>
