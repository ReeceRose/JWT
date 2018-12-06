<template>
    <FormCard title="Register" :submit="submit">
        <div slot="card-information">
            <p v-if="status" class="text-success text-center">Registered successfully. Redirecting...</p>
            <p v-if="error" class="text-danger text-center">An error has occured, make sure your passwords match and your email is unique</p>
        </div>

        <div slot="card-content">
            <div class="form-label-group">
                <input 
                    v-model="email"
                    @blur="$v.email.$touch()"
                    :class="{ 'is-invalid': $v.email.$error }"
                    type="text" 
                    id="inputEmail" 
                    class="form-control" 
                    placeholder="Email address" 
                    autofocus
                >
                <p v-if="$v.email.$error" class="text-danger text-center">Not a valid email address</p>
            </div>

            <div class="form-label-group">
                <input
                    v-model="password"
                    @blur="$v.password.$touch()"
                    :class="{ 'is-invalid': $v.password.$error }"                                
                    type="password" 
                    id="inputPassword" 
                    class="form-control" 
                    placeholder="Password"
                >
                <p v-if="$v.password.$error" class="text-danger text-center">Password must be at least 6 characters long, contain one upper and lowercase letter, and one special character</p>
            </div>

            <div class="form-label-group">
                <input 
                    v-model="confirmationPassword"
                    @blur="$v.confirmationPassword.$touch()"
                    :class="{ 'is-invalid': $v.confirmationPassword.$error }"
                    type="password" 
                    id="inputPasswordConfirmation" 
                    class="form-control" 
                    placeholder="Password confirmation"
                >
                <p v-if="$v.confirmationPassword.$error" class="text-danger text-center">Passwords must match</p>
            </div>

            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Register</button>
            
            <div class="my-4 strike">
                <span>OR</span>
            </div>

            <h5 class="card-title text-center">Register With</h5>

            <div class="text-center social-btn">
                <button class="btn btn-facebook btn-block">
                    <i class="fab fa-facebook-f fixed-width"></i>
                    <span>Facebook</span>
                </button>
                <button class="btn btn-google btn-block">
                    <i class="fab fa-google fixed-width"></i>
                    <span>Google</span>
                </button>
            </div>
        </div>
    </FormCard>
</template>

<script>
import FormCard from '@/components/UI/Card/FormCard.vue'

import { required, minLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)

export default {
    components: {
        FormCard
    },
    data() {
        return {
            email: '',
            password: '',
            confirmationPassword: '',
            success: false
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
    }
}
</script>

<style lang="scss" scoped>
.custom-form {
     .btn-google {
        color: white;
        background-color: #ea4335;
        margin: 5px;
    }

    .btn-facebook {
        color: white;
        background-color: #3b5998;
        margin: 5px;
    }

    .social-btn .btn {
        margin: 10px 0;
        font-size: 15px;
        text-align: center;
        line-height: 24px;    

        &:hover {
            opacity: 0.9;
        }

        .fixed-width {
            width: 0;
            margin-left: 10px;
            float: left;
            margin-top: 5px;
        }
    }
}

.strike {
    display: block;
    text-align: center;
    overflow: hidden;
    white-space: nowrap;

    span {
        position: relative;
        display: inline-block;

        &:before, &:after {
            content: "";
            position: absolute;
            top: 50%;
            width: 5000px;
            height: 1px;
            background-color: #000;
        }

        &:before {
            right: 100%;
            margin-right: 15px;
        }

        &:after {
            left: 100%;
            margin-left: 15px;
        }
    }
}
</style>