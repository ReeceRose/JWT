import utilities from '@/utilities.js'
import router from '@/router.js'
import axios from '@/axios.js'
// For reference
// headers: { Authorization: `Bearer ${getters['uthentication/getToken'] || ''}`}
const authentication = {
    namespaced: true,
    // VAR
    state: {
        token: null,
        loading: false,
        error: false,
        detailedError: '',
        status: false
    },
    // GET
    getters: {
        // TOKEN
        getToken(state) { return state.token },
        isAdmin(state) {
            return state.token ? utilities.parseJwt(state.token).hasOwnProperty("Administrator") : false
        },
        // LOADING
        isLoading(state) { return state.loading },
        // ERROR
        getError(state) { return state.error },
        getDetailedError(state) { return state.detailedError }, // For debugging
        getStatus(state) { return state.status }
    },
    // SET
    mutations: {
        // TOKEN
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
        // LOADING
        setLoading(state, loadingState) { state.loading = loadingState },
        // ERROR
        setError(state, error) { state.error = error },
        setDetailedError(state, error) { state.detailedError = error },
        resetError(state) {
            state.error = ''
            state.detailedError = ''
        },
        setStatus(state, status) { state.status = status }
    },
    // METHOD
    actions: {
        login({ commit, dispatch }, payload) {
            dispatch('general/setIsLoading', true, {root: true})
            commit("setError", false)
            axios({
                method: 'post',
                url: 'authentication/login',
                data: { email: payload.email, password: payload.password},
            })
                .then((response) => {
                    const token = response.data.token
                    commit("setSessionToken", token)
                    if (payload.rememberMe) {
                        commit("setLocalStorageToken", token)
                    }
                    router.push('/')
                })
                .catch(error => {
                    commit("setError", true)
                    commit("setDetailedError", JSON.stringify(error))
                })
                .finally(() => {
                    dispatch('general/setIsLoading', false, {root: true})
                })
        },
        logout({ commit }) {
            commit("removeToken")
            router.push('/')
        },
        register({ commit, dispatch }, payload) {
            dispatch('general/setIsLoading', true, {root: true})
            commit("setError", false)
            axios({
                method: 'post',
                url: 'authentication/register',
                data: { email: payload.email, password: payload.password}
            })
                .then(response => {
                    if (response.data.result) {
                        commit("setStatus", true)
                        setTimeout(() => {
                            router.push('/Login')
                        }, 3000)
                    }
                    else {
                        commit("setError", true)
                    }
                })
                .catch(() => {
                    commit("setError", true)
                })
                .finally(() => {
                    dispatch('general/setIsLoading', false, { root: true })
                })
        },
        loadToken({ commit }) {
            commit("setLoading", true)
            if (localStorage.getItem("token")) {
                commit("setStateToken", JSON.parse(localStorage.getItem("token")).token)
            } else if (sessionStorage.getItem("token")) {
                commit("setSessionToken", JSON.parse(sessionStorage.getItem("token")))
            }
            commit("setLoading", false)
        },
    }
}

export default authentication