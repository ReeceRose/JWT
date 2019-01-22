<template>
    <div class="pt-3">
        <div class="row">
            <div class="col">
                <h2 class="text-center pb-4">User</h2>
                <p v-if="error" class="text-danger text-center">Failed to load user</p>
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
                                <button class="btn btn-primary" @click="sendConfirmationEmail(user.id)">Send Confirmation Email</button>
                                <button class="btn btn-primary" @click="forceEmailConfirmaiton(user.id)">Force Email Confirmation</button>
                            </span>
                        </li>
                        <li>
                            <span class="item" v-if="user.lockoutEnabled"><button class="btn btn-primary" @click="disableAccount(user.id)">Disable Account</button></span>
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
            error: false
        }
    },
    methods: {
        getUser(userId) {
            this.$store.dispatch("users/getUser", userId)
                .then((user) => {
                    console.log(user)
                    this.user = user
                })
                .catch(() => {
                    this.error = true
                })
        },
        sendConfirmationEmail(userId) {
            console.log(userId)
        },
        forceEmailConfirmaiton(userId) {

        },
        enableAccount(userId) {
            this.$store.dispatch("users/enable", userId)
                .then((response) => {
                    console.log(response)
                })
                .catch(() => {
                    console.log('cannot enable account')
                })
        },
        disableAccount(userId) {
            this.$store.dispatch("users/disable", userId)
                .then((response) => {
                    console.log(response)
                })
                .catch(() => {
                    console.log('cannot disable account')
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
            margin: 5px 0;
        }
    }
}
</style>
