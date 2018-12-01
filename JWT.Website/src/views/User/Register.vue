<template>
    <div class="row">
        <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
            <div class="card card-signin my-5">
                <div class="card-body">
                    <h5 class="card-title text-center">Register</h5>
                    <p v-if="error" class="text-danger text-center">An error has occured, make sure your passwords match and your email is unique</p>
                    <form class="form-signin" @submit.prevent="submit">
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
                            <p v-if="$v.password.$error" class="text-danger text-center">Password must be at least 6 characters</p>
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
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from '@/axios.js'

import { required, minLength, email, sameAs } from 'vuelidate/lib/validators'

export default {
    name: 'Register',
    data() {
        return {
            email: '',
            password: '',
            confirmationPassword: '',
            error: ''
        }
    },
    validations: {
        email: {
            required,
            email
        },
        password: {
            required,
            minLength: minLength(6)
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
        }
    }
}
</script>

<style lang="scss" scoped>
.card-signin {
    border: 0;
    border-radius: 1rem;
    box-shadow: 0 0.5rem 1rem 0 rgba(0, 0, 0, 0.1);

    .card-title {
        margin-bottom: 2rem;
        font-weight: 300;
        font-size: 1.5rem;
    }

    .card-body {
        padding: 2rem;
    }
}

.form-signin {
    width: 100%;

    .btn {
        font-size: 80%;
        border-radius: 5rem;
        letter-spacing: .1rem;
        font-weight: bold;
        padding: 1rem;
        transition: all 0.2s;
    }

    .form-label-group {
        position: relative;
        margin-bottom: 1rem;

        input {
            border-radius: 2rem;
        }
    }

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
