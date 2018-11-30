import Vue from 'vue'
import Vuelidate from 'vuelidate'

import App from '@/App.vue'
import router from '@/router.js'
import store from '@/store/store.js'

Vue.use(Vuelidate)
Vue.config.productionTip = false

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')