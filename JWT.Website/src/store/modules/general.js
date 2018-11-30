const general = {
    namespaced: true,
    // VAR
    state: {
        loading: false
    },
    // GET
    getters: {
        isLoading(state) {
            return state.loading
        }
    },
    // SET
    mutations: {
        // could do a toggleLoading but I wasn this to be explicit
        setLoading(state, isLoading) {
            state.loading = isLoading
        }
    },
    // METHOD
    actions: {
        setIsLoading({ commit }, isLoading) {
            commit("setLoading", isLoading)
        }
    }
}

export default general