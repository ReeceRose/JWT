<template>
    <div class="form-label-group">
        <input
            v-model="password"
            @blur="validator.$touch()"
            :class="{ 'is-invalid': validator.$error }"                                
            type="password" 
            id="inputPassword" 
            class="form-control" 
            placeholder="Password"
        >
        <p v-if="validator.$error" class="text-danger text-center">Password must be at least 6 characters long, contain one upper and lowercase letter, and one special character</p>
    </div>
</template>

<script>
export default {
    name: 'Password',
    props: {
        password: String,
        validator: Object
    }
}
</script>
