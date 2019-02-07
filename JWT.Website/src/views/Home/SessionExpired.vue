<template>
    <div>
        <h1 class="text-danger">Session Expired</h1>
        <p class="text-center">
            Your session has expired. <router-link :to=" {name: 'login' }">Login here</router-link>            
        </p>
    </div>
</template>