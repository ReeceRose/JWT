<template>
    <div v-if="this.$route.name === 'userDashboard' ">
        Users
    </div>
    <div v-else>
        <router-view></router-view>
    </div>
</template>
