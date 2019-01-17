<template>
    <div class="pt-3">
        <h2 class="text-center pb-4">User</h2>
        <div>
            Email: {{ user.email }}<br>
            Date Joined: {{ user.dateJoined }}<br>
            Account Enabled: {{ user.accountEnabled }}<br>
            Email Confirmed: {{ user.emailConfirmed }}
        </div>
    </div>
</template>

<script>
export default {
    name: 'DetailedUser',
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
