<template>
    <FormCard title="Login" :submit="submit">
        <div slot="card-information">
            <p v-if="this.$route.params.redirect" class="text-danger text-center">You must be logged in to view this. Please login below.</p>
            <p v-if="error" class="text-danger text-center">An error has occured, please check your credentials</p>
        </div>

        <div slot="card-content">
            <div class="form-label-group">
                <input 
                    v-model="email" 
                    @blur="$v.email.$touch()" 
                    :class="{'is-invalid': $v.email.$error }"
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
                    :class="{'is-invalid': $v.password.$error}"
                    type="password"
                    id="inputPassword" 
                    class="form-control"
                    placeholder="Password"
                >
                <p v-if="$v.password.$error" class="text-danger text-center">Password must be at least 6 characters</p>
            </div>

            <div class="custom-control custom-checkbox mb-3">
                <input v-model="rememberMe" type="checkbox" class="custom-control-input" id="inputRememberMe">
                <label class="custom-control-label" for="inputRememberMe">Remember password</label>
            </div>

            <div class="mb-3">
                <router-link :to="{ name: 'resetPassword' }">Forgot your password?</router-link>
            </div>
            
            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Login</button>
            
            <div class="my-4 strike"><span>OR</span></div>
            <h5 class="card-title text-center">Login With</h5>
            
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

import { required, minLength, email } from 'vuelidate/lib/validators'

export default {
    components: {
        FormCard
    },
    data() {
        return {
            email: '',
            password: '',
            rememberMe: true
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
        }
    },
    computed: {
        error() {
            return this.$store.getters['authentication/getError']
        }
    },
    methods: {
        submit() {
            this.$v.$touch()
            if (this.$v.$invalid) {
                return
            }
            this.$store.dispatch('authentication/login', { email: this.email, password: this.password, rememberMe: this.rememberMe })
        }
    },
    destroyed() {
        this.$store.commit('authentication/resetError')
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