<template>
    <FormCard title="Resend confirmation email" :submit="submit">
        <div slot="card-information">
            <p v-if="sent" class="text-success text-center mb-3">A confirmation email has been sent.</p>
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

            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Resend link</button>
        </div>
    </FormCard>
</template>

<script>
import axios from '@/axios.js'
import { required, email } from 'vuelidate/lib/validators'

import FormCard from '@/components/UI/Card/FormCard.vue'

export default {
    name: 'resendConfirmation',
    data() {
        return {
            email: null,
            sent: false
        }
    },
    components: {
        FormCard
    },
    validations: {
        email: {
            required,
            email
        }
    },
    methods: {
        submit() {
            // TOOD: MOVE LOGIC INTO STORE. SET IS LOADING
            axios({
                method: 'post',
                url: 'authentication/regenerateConfirmationEmail',
                data: { email: this.email },
            })
                .then(() => {
                    this.confirmed = true
                })
                .catch(() => {
                    this.error = true
                })
        }
    }
}
</script>
