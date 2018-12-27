import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'

// Lazy load all imports
const Home = () => import('@/views/Home/Index.vue')
const Dashboard = () => import('@/views/Dashboard/Index.vue')

// USER
const UserIndex = () => import('@/views/Home/User/Index.vue')
const LoginIndex  = () => import('@/views/Home/User/Login/Index.vue')
const FacebookLogin  = () => import('@/views/Home/User/Login/Facebook.vue')
const GoogleLogin  = () => import('@/views/Home/User/Login/Google.vue')

const Register = () => import('@/views/Home/User/Register/Index.vue')

const AccessDenied = () => import('@/views/Home/AccessDenied.vue')
const ResetPassword = () => import('@/views/Home/ResetPassword.vue')
const ConfirmEmail = () => import('@/views/Home/ConfirmEmail.vue')
const RegenerateConfirmationEmail = () => import('@/views/Home/RegenerateConfirmationEmail.vue')

Vue.use(Router)

const AdminProtected = {
    beforeEnter: (to, from, next) => {
        const redirect = () => {
            const token = store.getters['global/getToken']
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
        if (store.getters['global/isLoading']) {
            store.watch(
                (getters) => {
                    getters['global/isLoading']
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
        const token = store.getters['global/getToken']
        if (token) {
            next({ name: 'home' })
        }
        else {
            next()
        }
    }
}

export default new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    beforeEach: (to, from, next) => {
        store.commit('global/setLoading', true)
        next()
    },
    afterEach: () => {
        store.commit('global/setLoading', false)
    },
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/User',
            name: 'user',
            component: UserIndex,
            children: [
                {
                    path: 'Login/:redirect?',
                    name: 'login',
                    component: LoginIndex,
                    ...NotLoggedIn,
                    children: [
                        {
                            path: 'Google',
                            name: 'googleLogin',
                            component: GoogleLogin
                        },
                        {
                            path: 'Facebook',
                            name: 'facebookLogin',
                            component: FacebookLogin
                        }
                    ]
                },
                {
                    path: 'Register',
                    name: 'register',
                    component: Register,
                    ...NotLoggedIn
                },
                {
                    path: 'ResetPassword',
                    name: 'resetPassword',
                    component: ResetPassword
                },
                {
                    path: '/ConfirmEmail',
                    name: 'confirmEmail',
                    component: ConfirmEmail
                },
                {
                    path: '/RegenerateConfirmationEmail',
                    name: 'regenerateConfirmationEmail',
                    component: RegenerateConfirmationEmail
                },
            ]
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
            path: '*',
            component: Home
        }
    ]
})