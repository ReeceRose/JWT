<template>
    <div class="form-label-group">
        <input
            v-model="input"
            @blur="validator.$touch()"
            :class="{ 'is-invalid': validator.$error }"                                
            type="password" 
            :id="confirmationPassword == 'true' ? 'inputPassword' : 'confirmationPassword'" 
            class="form-control" 
            :placeholder="confirmationPassword == 'true' ? 'Confirmation password' : 'Password'"
        >
        <p v-if="validator.$error && !confirmationPassword" class="text-danger text-center">Password must be at least 6 characters long, contain one upper and lowercase letter, and one special character</p>
        <p v-if="validator.$error && confirmationPassword" class="text-danger text-center">Password must match the password above</p>
    </div>
</template>

<script>
export default {
    name: 'Password',
    props: {
        value: {
            type: String,
            required: false,
            default: ""
        },
        validator: {
            type: Object,
            required: true  
        },
        confirmationPassword: {
            type: String,
            required: false,
            default: ""
        }
    },
    computed: {
        input: {
            get() {
                return this.value
            },
            set(value) {
                this.$emit("input", value)
            }
        }
    }
}
</script>
