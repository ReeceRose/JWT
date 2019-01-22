<template>
    <div class="pt-3">
        <div class="row">
            <div class="col">
                <h2 class="text-center pb-4">User</h2>
            </div>
        </div>
        <WideCard :title="user.email || ''">

            <div slot="card-content">
                <p v-if="error" class="text-danger">Failed to load user</p>
                <div v-else class="col-12">
                    <ul>
                        <li><span class="item">Date Joined: {{ user.dateJoined.substr(0, 10) }}</span></li>
                        <li>
                            <span class="item" v-if="user.emailConfirmed">Email Confirmed</span>
                            <span class="item" v-else>
                                <button class="btn btn-primary">Send Confirmation Email</button>
                                <button class="btn btn-primary">Force Email Confirmation</button>
                            </span>
                        </li>
                        <li>
                            <span class="item" v-if="user.lockoutEnabled"><button class="btn btn-primary">Disable Account</button></span>
                            <span class="item" v-else><button class="btn btn-primary">Enable Account</button></span>
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
                    this.user = user
                })
                .catch(() => {
                    this.error = true
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
