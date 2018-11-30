<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <router-link to="/" class="navbar-brand">JWT Project Example</router-link>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse text-center" id="navigationBar">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <router-link to="/" class="btn btn-outline-primary">Home</router-link>
                    </li>
                    <li class="nav-item" v-if="!loggedIn">
                        <router-link to="/login" class="btn btn-outline-primary">Login</router-link>
                    </li>
                    <li class="nav-item" v-if="!loggedIn">
                        <router-link to="/register" class="btn btn-outline-primary">Register</router-link>
                    </li>
                    <li class="nav-item" v-if="loggedIn">
                        <button class="btn btn-outline-primary" @click="logout">Logout</button>
                    </li>
                    <li class="nav-item" v-if="loggedIn">
                        <router-link to="/dashboard" class="btn btn-outline-primary">Dashboard</router-link>
                    </li>
                </ul>
        </div>
    </nav>
</template>

<script>
export default {
    methods: {
        logout() {
            this.$store.dispatch("authentication/logout")
        }
    },
    computed: {
        loggedIn() {
            return this.$store.getters['authentication/getToken']
        }
    }
    // watch: {
    //     loggedIn() {
    //         this.isLoggedIn()
    //     }
    // }
}
</script>



<style lang="scss" scoped>
.btn {
    width: 125px;
}
.nav-item {
    padding: 5px;
}
</style>
