<template>
    <div>
        <ApiHealth/>
        <!-- <div v-if="this.$route.name === 'dashboard' && verifyIsAdmin()" > -->
        <div v-if="this.$route.name === 'dashboard'" >
            <h1 class="text-left pt-3">
                Admin Dashboard
            </h1>
            <div class="row">
                <HeaderCard title="Users" class="text-center" :click="userClick">
                    <div slot="card-icon">
                        <i class="fas fa fa-4x fa-users"></i>
                    </div>
                    <div slot="card-content">
                        <p>User Count: {{ userCount }}</p>
                    </div>
                </HeaderCard>
            </div>
        </div>
        <div v-else>
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
import ApiHealth from '@/components/UI/Dashboard/ApiHealth.vue'
import HeaderCard from '@/components/UI/Card/Admin/HeaderCard.vue'

export default {
    name: 'Dashboard',
    components: {
        ApiHealth,
        HeaderCard
    },
    data() {
        return {
            userCount: 0,
        }
    },
    methods: {
        userClick() {
            this.$router.push({ name: 'userDashboard' })
        },
        verifyIsAdmin() {
            this.$store.dispatch("authentication/verifyIsAdmin")
                .then(() => {
                    // Nothing
                })
                .catch(() => {
                    // This will clean up the tokens
                    this.$store.dispatch("authentication/logout")
                    this.$router.push({ name: 'home' })
                })
            // Won't be reached if not true    
            return true
        },
        getUserCount() {
            this.$store.dispatch("users/userCount")
                .then((userCount) => {
                    this.userCount = userCount
                })
                .catch(() => {
                    this.userCount = 'Failed to load'
                })
        }
    },
    created() {
        this.verifyIsAdmin()
        this.getUserCount()
    }
}
</script>