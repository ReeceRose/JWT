import Vue from 'vue'
import Vuex from 'vuex'
import authentication from '@/store/modules/authentication'
import general from '@/store/modules/general'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        authentication,
        general
    }
})