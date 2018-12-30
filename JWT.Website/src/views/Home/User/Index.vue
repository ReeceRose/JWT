<template>
    <router-view></router-view>
</template>

<script>
export default {
    beforeCreate() {
        if (this.$route.name === 'user') {
            this.$router.push({ name: 'home' })
        }
    }
}
</script>

