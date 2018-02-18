<template>
    <div class="nav-menu">
        <v-navigation-drawer :disable-route-watcher="disableRouteWatcher" fixed clipped app v-model="drawer">
            <v-list>
                <v-list-tile>
                    <v-list-tile-content>
                        <v-list-tile-title>
                            <span>Menu</span>
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
        <v-toolbar class="indigo" dark app clipped-left fixed color="indigo darken-4">
            <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
            <v-toolbar-title>PALMS BEACH FACILITY</v-toolbar-title>
            <v-spacer></v-spacer>
            <div class="notifications-bar">
                <i class="fa fa-bell"></i><span class="notification-count">3</span>
            </div>
        </v-toolbar>
    </div>
</template>

<script>
import * as routes from "../../routes";
import * as authGetters from "../auth/store/types/getter-types";
import * as authResources from "../auth/store/resources";
import { mapGetters } from "vuex";

export default {
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
.nav-menu .toolbar-username {
  display: inline-block;
  margin-left: 20px;
}

.nav-menu .notifications-bar {
  position: relative;
}

.nav-menu a:hover{
    text-decoration: none;
    color:black;
}

.nav-menu a:active{
    text-decoration: none;
}

.nav-menu .notifications-bar .notification-count {
  position: absolute;
  top: -5px;
  left: 10px;
  display: inline-block;
  width: 13px;
  text-align: center;
  height: 13px;
  background: red;
  font-size: 11px;
  border-radius: 50%;
  cursor: pointer;
}

.nav-menu i {
  font-size: 20px;
  cursor: pointer;
}

.nav-menu .toolbar-username i {
  margin-right: 15px;
}
</style>
