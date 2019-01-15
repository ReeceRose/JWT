<template>
    <div v-if="this.$route.name === 'dashboard' ">
        <h1 class="text-left">
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
</template>

<script>
import HeaderCard from '@/components/UI/Card/Admin/HeaderCard.vue'

export default {
    name: 'Dashboard',
    components: {
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
        }
    }
}
</script>