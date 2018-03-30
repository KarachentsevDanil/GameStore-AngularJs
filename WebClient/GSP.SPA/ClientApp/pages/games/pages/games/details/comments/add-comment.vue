<template>
    <div class="add-comment-block">
        <form>
            <div class="text">
                <span class="bold"> {{labels.properties.commentRatingLabel}} </span> <star-rating v-model="comment.rating" :show-rating="false" :increment="0.5" :star-size="40"></star-rating>
            </div>

            <v-text-field v-model="comment.message"
                          :label="labels.properties.commentMessageLabel"
                          :error-messages="messageErrors"
                          @input="$v.comment.message.$touch()"
                          @blur="$v.comment.message.$touch()"
                          required
                          multi-line></v-text-field>

            <v-btn class="form-button" @click="addComment" color="info">{{labels.commands.addCommentLabel}}</v-btn>
        </form>
    </div>
</template>

<script>
    import * as authActions from "../../../../../auth/store/types/action-types";
    import * as authGetters from "../../../../../auth/store/types/getter-types";
    import * as authResources from "../../../../../auth/store/resources";
    import * as resources from "../../../../resources/resources";

    import { mapGetters } from "vuex";
    import { validationMixin } from "vuelidate";
    import { required } from "vuelidate/lib/validators";

    export default {
        mixins: [validationMixin],

        validations: {
            comment: {
                message: { required }
            }
        },
        props: {
            gameId: {
                required: true
            },
            addCommentFunc: {
                type: Function,
                required: true
            }
        },
        data() {
            return {
                comment: {
                    message: "",
                    rating: 0
                },
                labels: resources.rateLabels
            };
        },
        computed: {
            ...mapGetters({
                currentUser: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                )
            }),
            messageErrors() {
                const errors = [];
                if (!this.$v.comment.message.$dirty) return errors;
                !this.$v.comment.message.required && errors.push(resources.rateLabels.validationMessages.commentMessageRequiredMessage);
                return errors;
            }
        },
        methods: {
            addComment() {
                let newComment = {
                    CustomerId: this.currentUser.CustomerId,
                    GameId: this.gameId,
                    Comment: this.comment.message,
                    Rating: this.comment.rating
                };

                this.addCommentFunc(newComment);
                this.clear();
            },
            clear() {
                this.$v.$reset();
                this.comment.message = "";
                this.comment.rating = 0;
            }
        }
    };
</script>
