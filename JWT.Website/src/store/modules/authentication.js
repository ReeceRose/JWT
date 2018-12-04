import utilities from '@/utilities.js'
import router from '../../router';
// import router from '@/'

const authentication = {
    namespaced: true,
    // VAR
    state: {
        token: null,
        loading: false
    },
    // GET
    getters: {
        getToken(state) {
            return state.token
        },
        isAdmin(state) {
            return state.token ? utilities.parseJwt(state.token).hasOwnProperty("Administrator") : false
        },
        isLoading(state) {
            return state.loading
        }
    },
    // SET
    mutations: {
        setStateToken(state, token) {
            state.token = token
        },
        setSessionToken(state, token) {
            state.token = token
            sessionStorage.setItem("token", JSON.stringify(token))
        },
        setLocalStorageToken(token) {
            localStorage.setItem("token", JSON.stringify(token))
        },
        removeToken(state) {
            state.token = ''
            localStorage.removeItem("token")
            sessionStorage.removeItem("token")
        },
        setLoading(state, loadingState) {
            state.loading = loadingState
        }
    },
    // METHOD
    actions: {
        signIn({ commit }, payload) {
            commit("setSessionToken", payload.token)
            if (payload.rememberMe) {
                commit("setLocalStorageToken", payload.token)
            }
        },
        logout({ commit }) {
            commit("removeToken")
            router.push('/')
        },
        loadToken({ commit }) {
            commit("setLoading", true)
            if (localStorage.getItem("token")) {
                commit("setStateToken", JSON.parse(localStorage.getItem("token")).token)
            } else if (sessionStorage.getItem("token")) {
                commit("setSessionToken", JSON.parse(sessionStorage.getItem("token")))
            }
            commit("setLoading", false)
        }
    }
}

export default authentication