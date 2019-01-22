<template>
    <WideCard title="Email Confirmation">
        <div slot="card-content" class="text-center">
            <div v-if="userId && token">
                <p v-if="confirmed">Congragulations! Your email has been confirmed. <br>Login <router-link :to="{ name: 'login' }">here</router-link></p>
                <p v-if="error">Unfortunately your email cannot be confirmed. <br><router-link :to="{ name: 'regenerateConfirmationEmail' }">Click here to resend confirmation email</router-link></p>
                <p v-if="!(error) && !(confirmed)">Trying to confirm email...</p>
            </div>
            <div v-else>
                <p>In order to login you must confirm your email.<br>
                Resend confirmation email <router-link :to="{ name: 'regenerateConfirmationEmail' }">here</router-link></p>
            </div>
        </div>
    </WideCard>
</template>

<script>
import WideCard from '@/components/UI/Card/WideCard.vue'

export default {
    name: 'confirmEmail',
    data() {
        return {
            userId: this.$route.query.userId,
            token: this.$route.query.token,
            confirmed: false,
            error: false
        }
    },
    components: {
        WideCard
    },
    methods: {
        confirmEmail() {
            if (this.userId && this.token) {
                this.$store.dispatch('users/confirmEmail', { userId: this.userId, token: this.token })
                .then(() => {
                    this.confirmed = true
                    setTimeout(() => {
                        this.$router.push({ name: 'login' })
                    }, 3000)
                })
                .catch(() => {
                    this.error = true
                })
            }
        }
    },
    created() {
        this.confirmEmail()
    }
}
</script>
