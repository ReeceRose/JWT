import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'

// Lazy load all imports
const Home = () => import('@/views/Home/Index.vue')
const Dashboard = () => import('@/views/Dashboard/Index.vue')

// USER
const Login  = () => import('@/views/Home/User/Login.vue')
const Register = () => import('@/views/Home/User/Register.vue')
const AccessDenied = () => import('@/views/Home/User/AccessDenied.vue')
const ResetPassword = () => import('@/views/Home/User/ResetPassword.vue')
const ConfirmEmail = () => import('@/views/Home/User/ConfirmEmail.vue')
const ResendConfirmation = () => import('@/views/Home/User/ResendConfirmation.vue')

Vue.use(Router)

const AdminProtected = {
    beforeEnter: (to, from, next) => {
        const redirect = () => {
            const token = store.getters['authentication/getToken']
            if (token) {
                if (store.getters['authentication/isAdmin']) {
                    next()
                } else {
                    next('/AccessDenied')
                }
            } else {
                next({ name: 'login', params: { redirect: to.fullPath }})
            }
        }
        if (store.getters['authentication/isLoading']) {
            store.watch(
                (getters) => {
                    getters['authentication/isLoading']
                },
                () => {
                    redirect()
                }
            )
        } else {
            redirect()
        }
    }
}
const NotLoggedIn = {
    beforeEnter: (to, from, next) => {
        const token = store.getters['authentication/getToken']
        if (token) {
            next(false)
        }
        else {
            next()
        }
    }
}

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
            path: '/Login/:redirect?',
            name: 'login',
            component: Login,
            ...NotLoggedIn
        },
        {
            path: '/Register',
            name: 'register',
            component: Register,
            ...NotLoggedIn
        },
        {
            path: '/Dashboard',
            name: 'dashboard',
            component: Dashboard,
            ...AdminProtected
        },
        {
            path: '/AccessDenied',
            name: 'accessDenied',
            component: AccessDenied
        },
        {
            path: '/ResetPassword',
            name: 'resetPassword',
            component: ResetPassword
        },
        {
            path: '/ConfirmEmail',
            name: 'confirmEmail',
            component: ConfirmEmail
        },
        {
            path: '/ResendConfirmation',
            name: 'resendConfirmation',
            component: ResendConfirmation
        },
        {
            path: '*',
            component: Home
        }
    ]
})
