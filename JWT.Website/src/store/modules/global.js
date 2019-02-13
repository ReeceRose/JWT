import utilities from "@/utilities.js"

const global = {
    namespaced: true,
    state: {
        token: null,
        loading: false
    },
    getters: {
        // TOKEN
        getToken: state => state.token,
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
        removeCookie: () => {
            window.$cookies.remove("token")
        },
        // LOADING
        setLoading: (state, isLoading) => state.loading = isLoading
    },
    actions: {
        // TOKEN
        updateToken: ({ commit }, token) => {
            commit("setToken", token)
        },
        loadToken: ({ dispatch, commit }) => {
            commit("setLoading", true)
            let cookieToken = window.$cookies.get("token")
            if (cookieToken && cookieToken.token) {
                let token = cookieToken.token
                let parsedToken = utilities.parseJwt(JSON.stringify(token))
                var result = dispatch("checkExpiration", parsedToken.exp)
                if (result) {
                    commit("setToken", token)
                } else {
                    dispatch("authentication/logout", null, { root: true })
                }
            }
            commit("setLoading", false)
        },
        checkExpiration: (expiration) => {
            let date = Date(expiration)
            if (date > Date.now()) {
                return false
            } else {
                return true
            }
        },
        updateCookie: ({ commit }, token) => {
            if (token === null ){
                commit("removeCookie")
            } else {
                commit("setCookie", token)
            }
        },
        // LOADING
        updateLoading({ commit }, isLoading) {
            commit("setLoading", isLoading)
        },
    }
}

export default global