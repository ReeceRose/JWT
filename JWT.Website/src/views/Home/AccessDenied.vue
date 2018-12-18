<template>
    <div>
        <h1 class="text-danger">Access Denied</h1>
        <p class="text-center">
            You do not have access to this page. <router-link :to=" {name: 'home' }">Return home</router-link>            
        </p>
    </div>
</template>