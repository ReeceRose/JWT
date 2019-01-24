<template>
    <div v-if="this.$route.name === 'userDashboard' " class="pt-3 table-responsive">
        <div class="text-right">
            <h2 class="text-center">Users</h2>
            <i class="fas fa-sync-alt pointer" @click="getAllUsers"></i>
        </div>

        <div>
            <h5 v-if="error" class="text-danger">Failed to load users</h5>
        </div>

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
                <tr v-for="user in users" :key="user.id" @click="viewDetailedUser(user.id)" class="pointer">
                    <td>{{ user.email }}</td>
                    <td>{{ user.dateJoined.substr(0, 10) }}</td>
                    <td class="upper">{{ user.emailConfirmed }}</td>
                    <td class="upper">{{ user.accountEnabled }}</td>
                    <td><button class="btn btn-primary" @click="viewDetailedUser(user.id)">Edit</button></td>
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
            users: [],
            error: false
        }
    },
    methods: {
        viewDetailedUser(id) {
            this.$router.push({ name: 'detailedUserDashboard', params: { id: id } })
        },
        getAllUsers() {
            this.$store.dispatch("users/users", { currentPage: 2, pageSize: 10})
                .then((users) => {
                    this.users = users
                })
                .catch(() => {
                    this.error = true
                })
        }
    },
    created() {
        this.getAllUsers()
    }
}
</script>

<style lang="scss" scoped>
.pointer {
    cursor: pointer;
}
.upper::first-letter {
    text-transform: capitalize;
}
</style>
