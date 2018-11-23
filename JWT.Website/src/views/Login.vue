<template>
    <div class="form">
        <h2>Login</h2>
        <label for="email">Email: </label>
        <input type="text" name="email" v-model="email">
        <br>
        <label for="password">Password: </label>
        <input type="password" name="pasword" v-model="password">
        <br>
        <button @click.prevent="submit">Submit</button>
    </div>
</template>

<script>
import axios from '@/axios.js'
export default {
    name: 'Login',
    data() {
        return {
            email: '',
            password: ''
        }
    },
    methods: {
        submit() {
            axios.put('admin/')
                .then(response => {
                    console.log(response)
                })
            // this.name
            // this.password
            // axios
            //     .put('https://localhost:44301/api/authentication/register', {
            //         "Email": this.email,
            //         "Password": this.password
            //     })
            //     .then(response => console.log(response))
            //     .error(error => console.log(error))
        }
    }
}
</script>
