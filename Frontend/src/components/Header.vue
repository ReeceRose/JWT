<template>
    <nav>
        <router-link to="/" class="link">Home</router-link>
        <!-- TODO: Make these dynamic -->
        <router-link to="/Register" class="link">Register</router-link>
        <router-link to="/Login" class="link">Login</router-link>
        <router-link to="/Dashboard" class="link">Admin dashboard</router-link>
    </nav>
</template>


<style lang="scss" scoped>
nav {
    padding-top: 20px;
    text-align: center;
}
.link {
    padding: 15px;
    background-color: #404040;
    text-decoration: none;
    color: #fff;
}
</style>
