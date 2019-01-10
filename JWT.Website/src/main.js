import Vue from 'vue'
Vue.config.productionTip = false

import VueCookies from 'vue-cookies'
Vue.use(VueCookies)
VueCookies.config('1d')

import Vuelidate from 'vuelidate'
Vue.use(Vuelidate)

import App from '@/App.vue'

import store from '@/store/store.js'
store.dispatch("global/loadToken", { root: true }) 

import router from '@/router.js'

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')