<template>
    <div id="App">
        <TheHeader/>
        <router-view/>
    </div>
</template>

<script>
import TheHeader from '@/components/Header.vue'
export default {
    name: 'App',
    components: {
        TheHeader
    }
}
</script>


<style lang="scss">
* {
    padding: 0;
    margin: 0;
}
h1, .form {
    padding-top: 75px;
    text-align: center;
}
</style>
