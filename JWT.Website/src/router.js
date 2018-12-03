import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'
// Lazy load all imports
const Home = () => import('@/views/Home/Index.vue')
const Login  = () => import('@/views/Home/Login.vue')
const Register = () => import('@/views/Home/Register.vue')
const Dashboard = () => import('@/views/Dashboard/Index.vue')

Vue.use(Router)

function parseJwt (token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(window.atob(base64));
};

const AdminProtected = {
    beforeEnter: (to, from, next) => {
        const token = store.getters['authentication/getToken']
        if (token) {
            if (parseJwt(token).has("Administrator")) {
                next()
                // console.log('here')
            }
            // TODO: ADD UNAUTHRIZED
            next('/')
        } else {
            // TODO: ADD UNATHENTICATED PAGE
            next('/Login')
        }
    }
}
const NotLoggedIn = {
    beforeEnter: (to, from, next) => {
        const token = store.getters['authentication/getToken']
        console.log(token)
        if (store.getters['authentication/getToken']) {
            console.log('here')
            next(false)
        }
        else {
            console.log('her2e')
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
            path: '/Login',
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
            path: '*',
            component: Home
        }
    ]
})
