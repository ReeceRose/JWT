<template>
    <div v-if="this.$route.name === 'userDashboard' " class="pt-3">
        Users
        <table border="1">
            <thead>
                <tr>
                    <td>Email</td>
                    <td>Username</td>
                    <td>Date Joined</td>
                    <td>Email Confirmed</td>
                    <td>Management</td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(user, index) in users" :key="index">
                    <td>{{ user.username }}</td>
                    <td>{{ user.email }}</td>
                    <td>{{ user.dateJoined }}</td>
                    <td>{{ user.email }}</td>
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
                    username: 'username here',
                    email: 'email here',
                    dateJoined: 'date here',
                    emailConfirmed: false
                },
                {
                    username: 'username here 2',
                    email: 'email here 2',
                    dateJoined: 'date here 2',
                    emailConfirmed: false
                },
            ]
        }
    }
}
</script>
