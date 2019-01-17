<template>
    <div class="pt-3">
        <div class="row">
            <div class="col">
                <h2 class="text-center pb-4">User</h2>
            </div>
        </div>
        <WideCard :title="user.email">
            <div slot="card-content">
                <div class="col-12">
                    <ul>
                        <li><span class="item">Date Joined: {{ user.dateJoined }}</span></li>
                        <li>
                            <span class="item" v-if="user.emailConfirmed">Email Confirmed</span>
                            <span class="item" v-else>
                                <button class="btn btn-primary">Send Confirmation Email</button>
                                <button class="btn btn-primary">Force Email Confirmation</button>
                            </span>
                        </li>
                        <li>
                            <span class="item" v-if="user.accountEnabled"><button class="btn btn-primary">Disable Account</button></span>
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
            user: {
                    id: '123123',
                    email: 'test@gmail.com',
                    dateJoined: '12/12/2018',
                    accountEnabled: true,
                    emailConfirmed: true
                }
        }
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
