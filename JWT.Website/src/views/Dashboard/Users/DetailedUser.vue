<template>
    <div class="pt-3">
        <div class="row">
            <div class="col">
                <h2 class="text-center pb-4">User</h2>
                <p v-if="error" class="text-danger text-center">Failed to load user</p>

                <p v-if="accountDisabled" class="text-success text-center">Account has been disabled</p>
                <p v-if="accountEnabledError" class="text-danger text-center">Failed to enable user</p>

                <p v-if="accountEnabled" class="text-success text-center">Account has been enabled</p>
                <p v-if="accountDisabledError" class="text-danger text-center">Failed to disable user</p>

                <p v-if="emailSent" class="text-success text-center">Email has been sent</p>
                <p v-if="emailNotSent" class="text-danger text-center">Email cannot be sent</p>

                <p v-if="emailedConfirmed" class="text-success text-center">Email has been confirmed</p>
                <p v-if="emailedConfirmedError" class="text-danger text-center">Email cannot be confirmed</p>
            </div>
        </div>
        <WideCard :title="user.email" v-if="user">
            <div slot="card-content" class="text-center">
                <div class="col-12">
                    <ul>
                        <li><span class="item" v-if="user.dateJoined">Date Joined: {{ user.dateJoined.substr(0, 10) }}</span></li>
                        <li>
                            <span class="item" v-if="user.emailConfirmed">Email Confirmed</span>
                            <span class="item" v-else>
                                <button class="btn btn-primary" @click="sendConfirmationEmail(user.email)">Send Confirmation Email</button>
                                <button class="btn btn-primary" @click="forceEmailConfirmaiton(user.id)">Force Email Confirmation</button>
                            </span>
                        </li>
                        <li>
                            <span class="item" v-if="user.accountEnabled"><button class="btn btn-primary" @click="disableAccount(user.id)">Disable Account</button></span>
                            <span class="item" v-else><button class="btn btn-primary" @click="enableAccount(user.id)">Enable Account</button></span>
                        </li>
                    </ul>
                </div>
            </div>
        </WideCard>
    </div>
</template>

<script>
import WideCard from '@/components/UI/Card/WideCard.vue'

export default {
    name: 'DetailedUser',
    components: {
        WideCard
    },
    data() {
        return {
            user: false,
            error: false,
            accountDisabled: false,
            accountDisabledError: false,
            accountEnabled: false,
            accountEnabledError: false,
            emailSent: false,
            emailNotSent: false,
            emailedConfirmed: false,
            emailedConfirmedError: false
        }
    },
    methods: {
        getUser(userId) {
            this.$store.dispatch("users/userById", userId)
                .then((user) => {
                    this.user = user
                })
                .catch(() => {
                    this.error = true
                })
        },
        sendConfirmationEmail(email) {
            this.$store.dispatch("authentication/regenerateConfirmationEmail", { email: email })
                .then(() => {
                    this.user.accountEnabled = true
                    this.emailSent = true
                })
                .catch(() => {
                    this.emailNotSent = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.emailSent = false 
                        this.emailNotSent = false
                    }, 3000)
                })
        },
        forceEmailConfirmaiton(userId) {
            this.$store.dispatch("users/forceEmailConfirmation", userId)
                .then(() => {
                    this.user.emailConfirmed = true
                    this.emailedConfirmed = true
                })
                .catch(() => {
                    this.emailedConfirmedError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.emailedConfirmed = false 
                        this.emailedConfirmedError = false
                    }, 3000)
                })
        },
        enableAccount(userId) {
            this.$store.dispatch("users/enableAccount", userId)
                .then(() => {
                    this.user.accountEnabled = true
                    this.accountEnabled = true
                })
                .catch(() => {
                    this.accountEnabledError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.accountEnabled = false 
                        this.accountEnabledError = false
                    }, 3000)
                })
        },
        disableAccount(userId) {
            this.$store.dispatch("users/disableAccount", userId)
                .then(() => {
                    this.user.accountEnabled = false
                    this.accountDisabled = true
                })
                .catch(() => {
                    this.accountDisabledError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.accountDisabled = false 
                        this.accountDisabledError = false
                    }, 3000)
                })
        }
    },
    created() {
        this.getUser(this.$route.params.id)
    }
}
</script>

<style lang="scss" scoped>
ul {
    list-style: none;

    .item {
        font-size: 1.2rem;

        .btn {
            margin: 5px 5px;
        }
    }
}
</style>
