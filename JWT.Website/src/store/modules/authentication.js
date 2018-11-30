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
        setStateToken(state, token) {
            state.token = token
        },
        setLocalStorageToken(token) {
            localStorage.setItem("token", JSON.stringify(token))
        },
        removeToken(state) {
            state.token = ''
            localStorage.removeItem("token")
        }
    },
    // METHOD
    actions: {
        signIn({ commit }, payload) {
            // console.log(payload);
            commit("setStateToken", payload.token)
            if (payload.rememberMe) {
                commit("setLocalStorageToken", payload.token)
            }
        },
        logout({ commit }) {
            commit("removeToken")
        },
        loadToken({ commit }) {
            if (localStorage.getItem("token")) {
                commit("setStateToken", JSON.parse(localStorage.getItem("token")).token)
            }
        }
    }
}

export default authentication