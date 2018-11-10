<template>
    <div class="form">
        <h2>Login</h2>
        <label for="name">Name: </label>
        <input type="text" name="name" v-model="name">
        <br>
        <label for="password">Password: </label>
        <input type="password" name="pasword" v-model="password">
        <br>
        <button @click.prevent="submit">Submit</button>
    </div>
</template>

<script>
export default {
    name: 'Login',
    data() {
        return {
            name: '',
            password: ''
        }
    },
    methods: {
        submit() {
            // this.name
            // this.password
        }
    }
}
</script>
