import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'
import utilities from '@/utilities.js'

// Lazy load all imports
const Home = () => import('@/views/Home/Index.vue')
const Dashboard = () => import('@/views/Dashboard/Index.vue')

const Login  = () => import('@/views/Home/User/Login.vue')
const Register = () => import('@/views/Home/User/Register.vue')
const AccessDenied = () => import('@/views/Home/User/AccessDenied.vue')

Vue.use(Router)

const AdminProtected = {
    beforeEnter: (to, from, next) => {
        const token = store.getters['authentication/getToken']
        if (token) {
            const parsedToken = utilities.parseJwt(token)
            if (parsedToken.hasOwnProperty("Administrator")) {
                next()
            } else {
                // TODO: ADD UNAUTHRIZED
                next('/AccessDenied')
            }
        } else {
            // TODO: ADD UNATHENTICATED PAGE
            next({ name: 'login', params: { redirect: to.fullPath }})
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
            path: '*',
            component: Home
        }
    ]
})
