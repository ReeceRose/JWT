<template>
    <FormCard title="Resend confirmation email" :submit="submit">
        <div slot="card-information">
            <p v-if="sent" class="text-success text-center mb-3">A confirmation email has been sent.</p>
            <p v-if="error" class="text-danger text-center mb-3">A confirmation email cannot be sent.</p>
        </div>

        <div slot="card-content">
            <FormEmail v-model="email" :validator="$v.email"/>
            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Resend link</button>
        </div>
    </FormCard>
</template>

<script>
import FormCard from '@/components/UI/Card/FormCard.vue'
import FormEmail from '@/components/UI/Form/Email.vue'

import axios from '@/axios.js'
import { required, email } from 'vuelidate/lib/validators'

export default {
    name: 'resendConfirmation',
    data() {
        return {
            email: null,
            sent: false,
            error: false
        }
    },
    components: {
        FormCard,
        FormEmail
    },
    validations: {
        email: {
            required,
            email
        }
    },
    methods: {
        submit() {
            this.error = false
            // this.$store.dispatch('general/setIsLoading', true)
            axios({
                method: 'post',
                url: 'authentication/generateConfirmationEmail',
                data: { email: this.email },
            })
            .then(() => {
                this.sent = true
            })
            .catch(() => {
                this.error = true
            })
            .finally(() => {
                // this.$store.dispatch('general/setIsLoading', false)
            })
        }
    }
}
</script>
