import Vue from 'vue'
import App from '@/App.vue'
import router from '@/router'
import store from '@/store/store.js'
// import axios from '@/axios.js'

// axios
// axios.
Vue.config.productionTip = false

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')