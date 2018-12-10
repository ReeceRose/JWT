<template>
    <DisplayCard title="Email Confirmation">
        <div slot="card-content" class="text-center">
            <p v-if="confirmed">Congragulations! Your email has been confirmed. Login <router-link :to="{ name: 'login' }">here</router-link></p>
            <p v-if="error">Unfortunately your email cannot be confirmed. <router-link :to="{ name: 'resendConfirmation' }">Click here to resend confirmation email</router-link></p>
            <p v-if="!(error) && !(confirmed)">Trying to confirm email...</p>
        </div>
    </DisplayCard>
</template>

<script>
import DisplayCard from '@/components/UI/Card/DisplayCard.vue'

export default {
    name: 'confirmEmail',
    data() {
        return {
            userId: this.$route.params.userId,
            token: this.$route.params.token,
            confirmed: false,
            error: false
        }
    },
    components: {
        DisplayCard
    },
    methods: {
        confirmEmail() {
            // Send code to API and verify it
        }
    },
    created() {
        this.confirmEmail()
    }
}
</script>
