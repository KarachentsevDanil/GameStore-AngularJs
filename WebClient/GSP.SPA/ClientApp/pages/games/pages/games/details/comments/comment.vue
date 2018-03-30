<template>
    <div class="comment">
        <p>
            <span class="bold">
                {{labels.properties.commentCustomerLabel}}
            </span>
            {{comment.CustomerName}}
        </p>
        <p>
            <span class="bold">
                {{labels.properties.commentMessageLabel}}:
            </span>
            {{comment.Comment}}
        </p>
        <p>
            <span class="bold">
                {{labels.properties.commentRatingLabel}}
            </span>
            <star-rating v-model="comment.Rating" :show-rating="false" :increment="0.5" :read-only="true" :star-size="18"></star-rating> {{comment.Rating}} / 5
        </p>
        <p>
            <span class="bold">
                {{labels.properties.commentCreatedOnLabel}}
            </span>
            {{comment.CreatedOn}}
        </p>
    </div>
</template>

<script>
    import * as resources from "../../../../resources/resources";
    export default {
        data() {
            return {
                labels: resources.rateLabels
            };
        },
        props: {
            comment: {
                type: Object,
                required: true
            }
        }
    };
</script>
