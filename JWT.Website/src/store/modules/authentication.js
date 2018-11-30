const authentication = {
    namespaced: true,
    // VAR
    state: {
        token: null
    },
    // GET
    getters: {
        getToken(state) {
            return state.token
        }
    },
    // SET
    mutations: {
        setToken(state, token) {
            state.token = token
            localStorage.setItem("token", token)
        },
        removeToken(state) {
            state.token = ''
            localStorage.removeItem("token")
        }
    },
    // METHOD
    actions: {
        signIn({ commit }, payload) {
            commit("setToken", payload)
        },
        logout({ commit }) {
            commit("removeToken")
        },
        loadToken({ commit }) {
            if (localStorage.getItem("token")) {
                commit("setToken", localStorage.getItem("token"))
            }
        }
    }
}

export default authentication