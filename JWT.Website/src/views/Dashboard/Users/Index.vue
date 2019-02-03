<template>
    <div v-if="this.$route.name === 'userDashboard' " class="pt-3 table-responsive">
        <h2 class="text-center">Users</h2 >

        <SearchBar class="col-12" :submit="searchEmail"/>

        <div class="text-right col-1 offset-11">
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
        <ul class="pagination">
            <li class="page-item" :class="c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(c-1)">Previous</span>
            </li>
            <li class="page-item" :class="c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(1)">First</span>
            </li>
            <li v-for="(page, index) in  pageCount" :key="index" class="page-item" :class="page == c ? 'active' : ''">
                <span class="page-link" @click="setPage(page)">{{ page }}</span>
            </li>
            <li class="page-item" :class="c == pageCount || c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(pageCount)">Last</span>
            </li>
            <li class="page-item" :class="c == pageCount || c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(c+1)">Next</span>
            </li>
        </ul>
    </div>
    <div v-else>
        <router-view></router-view>
    </div>
</template>

<script>
import SearchBar from '@/components/UI/SearchBar.vue'

export default {
    name: 'UserDashboard',
    components: {
        SearchBar
    },
    data() {
        return {
            users: [],
            error: false,
            currentPage: 1,
            pageCount: 1
        }
    },
    computed: {
        c() {
            return this.currentPage
        }
    },
    methods: {
        searchEmail(event) {
            let email = event.target[0].value
            if (email === '' || email === null) {
                this.getAllUsers()
                return
            }
            this.$store.dispatch("users/userByEmail", email)
                .then((result) => {
                    this.users = result.users
                    this.pageCount = result.paginationModel.totalPages
                    this.error = false
                })
                .catch(() => {
                    this.error = true
                })
        },
        viewDetailedUser(id) {
            this.$router.push({ name: 'detailedUserDashboard', params: { id: id } })
        },
        getAllUsers() {
            this.$store.dispatch("users/users", { currentPage: this.currentPage, pageSize: 10})
                .then((result) => {
                    this.users = result.users
                    this.pageCount = result.paginationModel.totalPages
                    this.error = false
                })
                .catch(() => {
                    this.error = true
                })
        },
        setPage(pageIndex) {
            if (pageIndex <= 0 || pageIndex > this.pageCount) return
            this.currentPage = pageIndex
            this.getAllUsers()
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
