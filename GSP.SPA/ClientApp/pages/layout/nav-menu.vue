<template>
    <div class="nav-menu">
        <v-navigation-drawer :disable-route-watcher="disableRouteWatcher" fixed clipped app v-model="drawer">
            <v-list>
                <v-list-tile>
                    <v-list-tile-content>
                        <v-list-tile-title>
                            <span>Hi, {{ getUser.FullName }}</span>
                        </v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
                <v-divider></v-divider>
                <template v-for="(item, index) in getUserPages" v-if="getUser.Role == 'User'">
                    <v-list-tile :to="item.path" :key="index">
                        <v-list-tile-action>
                            <v-icon light v-html="item.icon"></v-icon>
                        </v-list-tile-action>
                        <v-list-tile-content>
                            <v-list-tile-title v-html="item.display"></v-list-tile-title>
                        </v-list-tile-content>
                    </v-list-tile>
                </template>
                    <template v-for="(item, index) in getAdminPages" v-if="getUser.Role == 'Admin'">
                    <v-list-tile :to="item.path" :key="index">
                        <v-list-tile-action>
                            <v-icon light v-html="item.icon"></v-icon>
                        </v-list-tile-action>
                        <v-list-tile-content>
                            <v-list-tile-title v-html="item.display"></v-list-tile-title>
                        </v-list-tile-content>
                    </v-list-tile>
                </template>
            </v-list>
        </v-navigation-drawer>
        <v-toolbar class="indigo nav-menu-bar" dark app clipped-left fixed color="deep-purple darken-1">
            <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
            <v-toolbar-title>Game Store</v-toolbar-title>
            <v-spacer></v-spacer>
            <bucketItems></bucketItems>
        </v-toolbar>
    </div>
</template>

<script>
import * as routes from "../../routes";
import * as authGetters from "../auth/store/types/getter-types";
import * as authResources from "../auth/store/resources";
import { mapGetters } from "vuex";
import bucketItems from '../orders/pages/bucket/bucket-list';

export default {
  components:{
    bucketItems
  },
  data() {
    return {
      drawer: false,
      disableRouteWatcher: true
    };
  },
  computed: {
    ...mapGetters({
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      ),
      getToken: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_TOKEN_GETTER
      )
    }),
    getAdminPages() {
      return routes.adminRoutes;
    },
    getUserPages() {
      return routes.userRoutes;
    }
  }
};
</script>
<style>
.nav-menu .toolbar.nav-menu-bar {
  background-color: #5e35b1 !important;
  z-index: 20;
}

.nav-menu .toolbar.nav-menu-bar .badge {
  margin-top: 10px;
  margin-right: 25px !important;
  background-color: #5e35b1;
}

.nav-menu .toolbar.nav-menu-bar .badge .badge__badge.cyan {
  right: -10px;
}

.nav-menu .toolbar.nav-menu-bar .badge .icon.material-icons {
  font-size: 30px;
  color: white !important;
}

.nav-menu .toolbar-username {
  display: inline-block;
  margin-left: 20px;
}

.nav-menu .notifications-bar {
  position: relative;
}

.nav-menu a:hover {
  text-decoration: none;
  color: black;
}

.nav-menu a:active {
  text-decoration: none;
}

.nav-menu i {
  font-size: 20px;
  cursor: pointer;
}

.nav-menu .toolbar-username i {
  margin-right: 15px;
}

.navigation-drawer.navigation-drawer--clipped.navigation-drawer--open{
  z-index: 10;
}
</style>
