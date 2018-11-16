<template>
    <div class="form">
        <h1>Register</h1>
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
    name: 'Register',
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
