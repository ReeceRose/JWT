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
            localStorage.removeItem("token")
        },
        setCookie: (token) => {
            this.$cookies.set("token", token)
        },
        removeCookie: () => {
            this.$cookies.set("token", null)
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
            if (localStorage.getItem("token")) {
                commit("setToken", JSON.parse(localStorage.getItem("token")))
            }
            commit("setLoading", false)
        },
        // LOADING
        updateLoading({ commit }, isLoading) {
            commit("setLoading", isLoading)
        }
    }
}

export default global