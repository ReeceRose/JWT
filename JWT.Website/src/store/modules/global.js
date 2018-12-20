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
        // TOKN
        setToken: (state, token) => state.token = token,
        // LOADING
        setLoading: (state, isLoading) => state.loading = isLoading
    },
    actions: {
        // TOKEN
        updateToken({ commit }, token) {
            commit("setToken", token)
        },
        loadToken({ commit }) {
            commit("setLoading", true)
            if (localStorage.getItem("token")) {
                commit("setToken", JSON.parse(localStorage.getItem("token")).token)
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