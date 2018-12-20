<template>
    <div id="App">
        <Spinner v-if="loading"/>
        <TheHeader/>
        <div class="container">
            <router-view/>
        </div>
    </div>
</template>

<script>
import TheHeader from '@/components/UI/Navigation/Navigation.vue'
import Spinner from '@/components/UI/Spinner.vue'

export default {
    name: 'App',
    components: {
        TheHeader,
        Spinner
    },
    computed: {
        loading() {
            return this.$store.getters['global/isLoading']
        }
    }
}
</script>


<style lang="scss">
* {
    padding: 0;
    margin: 0;
    font-family: 'Montserrat', sans-serif;
}
h1, .form {
    padding-top: 75px;
    text-align: center;
}
</style>
