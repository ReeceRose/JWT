import Vue from 'vue'
import Vuelidate from 'vuelidate'

import App from '@/App.vue'
import router from '@/router.js'
import store from '@/store/store.js'

Vue.use(Vuelidate)
Vue.config.productionTip = false

function parseJwt (token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(window.atob(base64));
};

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')