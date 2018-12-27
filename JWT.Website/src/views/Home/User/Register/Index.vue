<template>
    <FormCard title="Register" :submit="submit">
        <div slot="card-information">
            <p v-if="success" class="text-success text-center mb-3">A confirmation email has been sent.</p>
            <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
        </div>

        <div slot="card-content">
            <FormEmail v-model="email" :validator="$v.email"/>
            <FormPassword v-model="password" :validator="$v.password"/>
            <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>

            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Register</button>
            <Strike text="OR"/>

            <h5 class="card-title text-center">Register With</h5>
        </div>
        <div slot="below-form" class="text-center social-btn">
            <FacebookButton :submit="facebook"/>
            <GoogleButton :submit="google"/>
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
            confirmationPassword: '',
            success: null,
            error: null,
            errorMessage: 'An error has occured, make sure your passwords match and your email is unique'
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
    methods: {
        submit() {
            this.$v.$touch()
            if (this.$v.$invalid) {
                return
            }
            this.$store.dispatch('authentication/register', { email: this.email, password: this.password, isAdmin: false })
                .then(() => {
                    this.success = true
                })
                .catch((error) => {
                    if (error.response) {
                        this.errorMessage = error.response.data.error[0]
                    }
                    this.error = true
                })
        },
        facebook() {
            this.$router.push({ name: 'facebookLogin' })
        },
        google() {
            this.$router.push({ name: 'googleLogin' })
        }
    }
}
</script>

<style lang="scss" scoped>

</style>