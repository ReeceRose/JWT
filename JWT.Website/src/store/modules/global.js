import axios from "../../axios";

const global = {
    namespaced: true,
    state: {
        token: null,
        crsfToken: null,
        loading: false,
    },
    getters: {
        // TOKEN
        getToken: state => state.token,
        getCrsfToken: state => state.crsfToken,
        // LOADING
        isLoading: state => state.loading
    },
    mutations: {
        // TOKEN
        setToken: (state, token) => {
            state.token = token
        },
        removeToken: (state) => {
            state.token = null
        },
        setCookie: (token) => {
            window.$cookies.set("token", JSON.stringify(token))
        },
        setCrsfToken: (state, token) => {
            state.crsfToken = token
        },
        removeCookie: () => {
            window.$cookies.remove("token")
        },
        // LOADING
        setLoading: (state, isLoading) => state.loading = isLoading
    },
    actions: {
        // TOKEN
        updateToken({ commit }, token) {
            commit("setToken", token)
            localStorage.setItem("token", JSON.stringify(token))
        },
        loadToken({ commit }) {
            commit("setLoading", true)
            if (window.$cookies.get("token")) {
                commit("setToken", window.$cookies.get("token").token)
            }
            commit("setLoading", false)
        },
        updateCookie({ commit }, token) {
            if (token === null ){
                commit("removeCookie")
            } else {
                commit("setCookie", token)
            }
        },
        // LOADING
        updateLoading({ commit }, isLoading) {
            commit("setLoading", isLoading)
        }
    }
}

export default global