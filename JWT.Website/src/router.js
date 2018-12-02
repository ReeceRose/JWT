import Vue from 'vue'
import Router from 'vue-router'
// Lazy load all imports
const Home = () => import('@/views/Home/Index.vue')
const Login  = () => import('@/views/Home/Login.vue')
const Register = () => import('@/views/Home/Register.vue')
const Dashboard = () => import('@/views/Dashboard/Index.vue')

Vue.use(Router)

export default new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/Login',
            name: 'login',
            component: Login
        },
        {
            path: '/Register',
            name: 'register',
            component: Register
        },
        {
            path: '/Dashboard',
            name: 'dashboard',
            component: Dashboard
        },
        {
            path: '*',
            component: Home
        }
    ]
})
