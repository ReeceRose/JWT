import Vue from 'vue'
Vue.config.productionTip = false

import Vuelidate from 'vuelidate'
Vue.use(Vuelidate)

import App from '@/App.vue'

import store from '@/store/store.js'
store.dispatch("global/loadToken") 

import router from '@/router.js'


new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')