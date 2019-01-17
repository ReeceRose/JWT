<template>
    <div v-if="this.$route.name === 'userDashboard' " class="pt-3 table-responsive">
        <h2 class="text-center pb-4">Users</h2>
        <table border="1" class="table table-bordered table-hover text-center">
            <thead class="thead-dark">
                <tr>
                    <th class="header">Email</th>
                    <th class="">Date Joined</th>
                    <th class="">Email Confirmed</th>
                    <th class="header">Account Enabled</th>
                    <th class="header">Management</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(user, index) in users" :key="index">
                    <td>{{ user.email }}</td>
                    <td>{{ user.dateJoined }}</td>
                    <td>{{ user.accountEnabled }}</td>
                    <td>{{ user.emailConfirmed }}</td>
                    <td>Edit | Delete</td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
        <router-view></router-view>
    </div>
</template>

<script>
export default {
    name: 'UserDashboard',
    data() {
        return {
            users: [
                {
                    email: 'email here',
                    dateJoined: 'date here',
                    accountEnabled: false,
                    emailConfirmed: false
                },
                {
                    email: 'email here 2',
                    dateJoined: 'date here 2',
                    accountEnabled: false,
                    emailConfirmed: false
                },
            ]
        }
    }
}
</script>
