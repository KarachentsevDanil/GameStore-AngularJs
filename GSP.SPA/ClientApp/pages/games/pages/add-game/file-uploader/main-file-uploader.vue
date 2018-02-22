<template>
    <div>
        <hr>
        <div>
            <p class="bold">
                Upload main photo:
            </p>
            <vue-dropzone ref="mainDropzoe" @vdropzone-success="mainfileSuccessfullyAdded" id="mainDropzone" :options="mainDropzoneOptions">
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
      }
    };
  },
  methods:{
      clearPhoto(){
          this.$refs.mainDropzoe.removeAllFiles();
      }
  }
};
</script>
