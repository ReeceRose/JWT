<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <router-link to="/" class="navbar-brand">JWT Project Example</router-link>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse text-center" id="navigationBar">
                <!-- Add conditonal rendering -->
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <router-link to="/" class="btn btn-outline-primary">Home</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/login" class="btn btn-outline-primary">Login</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/register" class="btn btn-outline-primary">Register</router-link>
                    </li>
                    <li class="nav-item">
                        <button class="btn btn-outline-primary">Logout</button>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard" class="btn btn-outline-primary">Dashboard</router-link>
                    </li>
                </ul>
        </div>
    </nav>
</template>


<style lang="scss" scoped>
.btn {
    width: 125px;
}
.nav-item {
    padding: 5px;
}
</style>
