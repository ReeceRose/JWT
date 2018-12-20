import Vue from 'vue'
import Vuex from 'vuex'
import global from '@/store/modules/global.js'
import authentication from '@/store/modules/authentication.js'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        authentication,
        global
    }
})