<template>
    <FormNarrowCard title="Resend confirmation email" :submit="submit">
        <div slot="card-information">
            <p v-if="sent" class="text-success text-center mb-3">A confirmation email has been sent.</p>
            <p v-if="error" class="text-danger text-center mb-3">A confirmation email cannot be sent.</p>
        </div>

        <div slot="card-content">
            <FormEmail v-model="email" :validator="$v.email"/>
            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Resend link</button>
        </div>
    </FormNarrowCard>
</template>

<script>
import FormNarrowCard from '@/components/UI/Card/Form/FormNarrowCard.vue'
import FormEmail from '@/components/UI/Form/Email.vue'

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
        FormNarrowCard,
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
             this.$store.dispatch('users/regenerateConfirmationEmail', { email: this.email })
                .then(() => {
                    this.sent = true
                    this.error = false
                })
                .catch(() => {
                    this.error = true
                    this.sent = false
                })
        }
    }
}
</script>
