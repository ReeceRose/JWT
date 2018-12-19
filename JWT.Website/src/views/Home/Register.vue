<template>
    <FormCard title="Register" :submit="submit">
        <div slot="card-information">
            <p v-if="status" class="text-success text-center mb-3">A confirmation email has been sent.</p>
            <p v-if="error" class="text-danger text-center mb-3">An error has occured, make sure your passwords match and your email is unique</p>
        </div>

        <div slot="card-content">
            <FormEmail v-model="email" :validator="$v.email"/>
            <FormPassword v-model="password" :validator="$v.password"/>
            <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>

            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Register</button>
            <Strike text="OR"/>

            <h5 class="card-title text-center">Register With</h5>
            <div class="text-center social-btn">
                <FacebookButton/>
                <GoogleButton/>
            </div>
        </div>
    </FormCard>
</template>

<script>
import FormCard from '@/components/UI/Card/FormCard.vue'
import FormEmail from '@/components/UI/Form/Email.vue'
import FormPassword from '@/components/UI/Form/Password.vue'
import Strike from '@/components/UI/Form/Strike.vue'
import FacebookButton from '@/components/UI/Button/Social/Facebook.vue'
import GoogleButton from '@/components/UI/Button/Social/Google.vue'

import { required, minLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)

export default {
    components: {
        FormCard,
        FormEmail,
        FormPassword,
        Strike,
        FacebookButton,
        GoogleButton
    },
    data() {
        return {
            email: '',
            password: '',
            confirmationPassword: ''
        }
    },
    validations: {
        email: {
            required,
            email
        },
        password: {
            required,
            minLength: minLength(6),
            passwordRegex
        },
        confirmationPassword: {
            required,
            sameAsPassword: sameAs('password')
        }
    },
    computed: {
        error() {
            return this.$store.getters['authentication/getError']
        },
        status() {
            return this.$store.getters['authentication/getStatus']
        }
    },
    methods: {
        submit() {
            this.$v.$touch()
            if (this.$v.$invalid) {
                return
            }
            this.$store.dispatch('authentication/register', { email: this.email, password: this.password })
        }
    },
    destroyed() {
        this.$store.commit('authentication/setStatus', false)
    }
}
</script>

<style lang="scss" scoped>

</style>