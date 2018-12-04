import Vue from 'vue'
import Vuelidate from 'vuelidate'

import App from '@/App.vue'
import store from '@/store/store.js'

store.dispatch("authentication/loadToken") 

import router from '@/router.js'

Vue.use(Vuelidate)

Vue.config.productionTip = false

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')