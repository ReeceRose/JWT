<template>
    <FormCard title="Resend confirmation email" :submit="submit">
        <div slot="card-information">
            <p v-if="success" class="text-success text-center mb-3">A confirmation email has been sent.</p>
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
import FormCard from '@/components/UI/Card/FormCard.vue'

import { required, email } from 'vuelidate/lib/validators'

export default {
    name: 'resendConfirmation',
    data() {
        return {
            email: null
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
            // Resend confirmation linkk
        }
    }
}
</script>
