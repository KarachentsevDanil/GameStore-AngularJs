<template>
    <div class="comment">
        <p>
            <span class="bold">
                Customer:
            </span>
            {{comment.CustomerName}}
        </p>
        <p>
            <span class="bold">
                Comment:
            </span>
            {{comment.Comment}}
        </p>
        <p>
            <span class="bold">
                Rating:
            </span>
            <star-rating v-model="comment.Rating" :show-rating="false" :increment="0.5" :read-only="true" :star-size="18"></star-rating> {{comment.Rating}} / 5
        </p>
        <p>
            <span class="bold">
                Date:
            </span>
            {{comment.CreatedOn}}
        </p>
    </div>
</template>

<script>
    export default {
        props: {
            comment: {
                type: Object,
                required: true
            }
        }
    };
</script>
