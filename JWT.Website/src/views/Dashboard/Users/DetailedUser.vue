<template>
    <div class="pt-3">
        <div class="row">
            <div class="col">
                <h2 class="text-center pb-4">User</h2>
            </div>
        </div>
        <NarrowCard>

        </NarrowCard>
        <div class="row">
            <div class="col text-center">
                <h5>Email: {{ user.email }}</h5>
            </div>
        </div>
        <div class="row">
            <h5 class="">
                Email: {{ user.email }}<br>
                Date Joined: {{ user.dateJoined }}<br>
                Account Enabled: {{ user.accountEnabled }}<br>
                Email Confirmed: {{ user.emailConfirmed }}
            </h5>
        </div>
    </div>
</template>

<script>
import NarrowCard from '@/components/UI/Card/NarrowCard.vue'

export default {
    name: 'DetailedUser',
    components: {
        NarrowCard
    },
    data() {
        return {
            user: {
                    id: '123123',
                    email: 'email here',
                    dateJoined: 'date here',
                    accountEnabled: true,
                    emailConfirmed: true
                }
        }
    }
}
</script>