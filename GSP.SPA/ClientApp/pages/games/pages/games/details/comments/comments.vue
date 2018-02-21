<template>
    <v-card class="game-details-block">
        <div class="form-header deep-purple darken-1">
            <v-card-title class="white--text deep-purple darken-1">
                <span class="text-xs-center header-text">
                    Comments
                </span>
            </v-card-title>
        </div>
        <div class="form-body">
            <add-comment :gameId="gameId" :addCommentFunc="addComment"></add-comment>
            <hr>
            <div class="comments-block">
                <comment v-for="comment in comments" :key="comment.RateId" :comment="comment"></comment>
            </div>
        </div>
    </v-card>
</template>

<script>
import * as rateService from "../../../../api/rate-service";

import commentComponent from "./comment";
import addCommentComponent from "./add-comment";

export default {
  components: {
    comment: commentComponent,
    addComment: addCommentComponent
  },
  data() {
    return {
      comments: []
    };
  },
  props: {
    gameId: {
      required: true
    }
  },
  async beforeMount() {
    this.comments = (await rateService.getGameRatesById(this.gameId)).data;
  },
  methods: {
    async addComment(comment) {
      await rateService.addRateToGame(comment);
      this.comments = (await rateService.getGameRatesById(this.gameId)).data;
    }
  }
};
</script>

<style>
.form-body hr {
  margin-top: 0px;
  margin-bottom: 0px;
}

.add-comment-block .text {
  font-size: 18px;
}

.comments-block .vue-star-rating {
  display: inline-block !important;
}

.comments-block .comment {
  padding: 10px;
  border-bottom: whitesmoke solid 1px;
  font-size: 14px;
}

.comments-block .comment p {
  margin-bottom: 4px;
}

.comments-block .comment .bold {
  font-weight: bold;
  font-size: 16px;
}
</style>
