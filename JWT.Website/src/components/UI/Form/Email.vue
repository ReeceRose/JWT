<template>
    <div class="form-label-group">
        <input 
            v-model="input"
                @blur="validator.$touch()"
                :class="{ 'is-invalid': validator.$error }"
                type="text" 
                id="inputEmail" 
                class="form-control" 
                placeholder="Email address" 
                autofocus
            >
            <p v-if="validator.$error" class="text-danger text-center">Not a valid email address</p>
    </div>
</template>

<script>
export default {
    name: 'Email',
    data() {
        return {
            input: ''
        }
    },
    props: {
        email: String,
        validator: Object
    }
}
</script>
