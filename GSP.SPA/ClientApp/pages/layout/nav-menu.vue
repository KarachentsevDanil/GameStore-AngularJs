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
                <template v-for="(item, index) in items">
                    <v-list-tile :to="item.path" :key="index">
                        <v-list-tile-action>
                            <v-icon light v-html="item.style"></v-icon>
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

    export default {
        data() {
            return {
                items: [
                    {
                        path: "/",
                        display: "Home",
                        style: "glyphicon glyphicon-home"
                    },
                    {
                        path: "/counter",
                        display: "Counter",
                        style: "glyphicon glyphicon-education"
                    },
                    {
                        path: "/fetch-data",
                        display: "Fetch data",
                        style: "glyphicon glyphicon-th-list"
                    },
                    {
                        path: "/login",
                        display: "Sign in",
                        style: "glyphicon glyphicon-th-list"
                    }
                ],
                drawer: false,
                disableRouteWatcher: true
            };
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
