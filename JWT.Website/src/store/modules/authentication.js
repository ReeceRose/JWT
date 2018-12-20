import axios from '@/axios.js'
// For reference
// headers: { Authorization: `Bearer ${getters['uthentication/getToken'] || ''}`}
const authentication = {
    namespaced: true,
    actions: {
        login: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/login',
                    data: { email: payload.email, password: payload.password },
                })
                    .then((response) => {
                        const token = response.data.token
                        commit("global/updateToken", token, { root: true })
                        if (payload.rememberMe) {
                            // commit("setLocalStorageToken", token)
                            // Store cookie
                        }
                        resolve()
                    })
                    .catch(error => {
                        reject(error)
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        }
    }
    // state: {
    //     token: null,
    //     loading: false,
    //     error: false,
    //     detailedError: '',
    //     status: false
    // },
    // getters: {
    //     // TOKEN
    //     getToken(state) { return state.token },
    //     isAdmin(state) {
    //         return state.token ? utilities.parseJwt(state.token).hasOwnProperty("Administrator") : false
    //     },
    //     // LOADING
    //     isLoading(state) { return state.loading },
    //     // ERROR
    //     getError(state) { return state.error },
    //     getDetailedError(state) { return state.detailedError }, // For debugging
    //     getStatus(state) { return state.status }
    // },
    // mutations: {
    //     setStateToken(state, token) {
    //         state.token = token
    //     },
    //     setSessionToken(state, token) {
    //         state.token = token
    //         sessionStorage.setItem("token", JSON.stringify(token))
    //     },
    //     setLocalStorageToken(token) {
    //         localStorage.setItem("token", JSON.stringify(token))
    //     },
    //     removeToken(state) {
    //         state.token = ''
    //         localStorage.removeItem("token")
    //         sessionStorage.removeItem("token")
    //     },
    //     setLoading(state, loadingState) { state.loading = loadingState },
    //     setError(state, error) { state.error = error },
    //     setDetailedError(state, error) { state.detailedError = error },
    //     resetError(state) {
    //         state.error = ''
    //         state.detailedError = ''
    //     },
    //     setStatus(state, status) { state.status = status }
    // },
    // actions: {
    //     login({ commit, dispatch }, payload) {
    //         dispatch('general/setIsLoading', true, {root: true})
    //         commit("setError", false)
    //         axios({
    //             method: 'post',
    //             url: 'authentication/login',
    //             data: { email: payload.email, password: payload.password},
    //         })
    //             .then((response) => {
    //                 const token = response.data.token
    //                 commit("setSessionToken", token)
    //                 if (payload.rememberMe) {
    //                     commit("setLocalStorageToken", token)
    //                 }
    //                 router.push('/')
    //             })
    //             .catch(error => {
    //                 if (error.response) {
    //                     if (error.response.data.error[0] == "Email not confirmed") {
    //                         router.push({ name: 'confirmEmail' })
    //                     }
    //                 }
    //                 commit("setError", true)
    //                 commit("setDetailedError", JSON.stringify(error))
    //             })
    //             .finally(() => {
    //                 dispatch('general/setIsLoading', false, {root: true})
    //             })
    //     },
    //     logout({ commit }) {
    //         commit("removeToken")
    //         router.push('/')
    //     },
    //     register({ commit, dispatch }, payload) {
    //         dispatch('general/setIsLoading', true, {root: true})
    //         commit("setError", false)
    //         axios({
    //             method: 'post',
    //             url: 'authentication/register',
    //             data: { email: payload.email, password: payload.password}
    //         })
    //             .then(response => {
    //                 if (response.data.result) {
    //                     commit("setStatus", true)
    //                 }
    //                 else {
    //                     commit("setError", true)
    //                 }
    //             })
    //             .catch(() => {
    //                 commit("setError", true)
    //             })
    //             .finally(() => {
    //                 dispatch('general/setIsLoading', false, { root: true })
    //             })
    //     },
    //     loadToken({ commit }) {
    //         commit("setLoading", true)
    //         if (localStorage.getItem("token")) {
    //             commit("setStateToken", JSON.parse(localStorage.getItem("token")).token)
    //         } else if (sessionStorage.getItem("token")) {
    //             commit("setSessionToken", JSON.parse(sessionStorage.getItem("token")))
    //         }
    //         commit("setLoading", false)
    //     },
    // }
}

export default authentication