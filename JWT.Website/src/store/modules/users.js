import axios from '@/axios.js'
const users = {
    namespaced: true,
    actions: {
        userCount: ({ commit, rootGetters }) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: 'users/count',
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        users: ({ commit, rootGetters }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'users/',
                    data: {
                        CurrentPage: payload.currentPage,
                        PageSize: payload.pageSize
                    },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        userById: ({ commit, rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: `users/details/id/${userId}`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        userByEmail: ({ commit, rootGetters }, email) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: `users/details/email/${email}`,
                    data: { email: email },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        forceEmailConfirmation: ({ commit, rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: `users/forceEmailConfirmation/${userId}`,
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        sendEmailConfirmation: ({ commit, rootGetters }, email) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: 'authentication/GenerateConfirmationEmail',
                    data: { email: email },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        enableAccount: ({ commit, rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: `users/enable/${userId}`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        disableAccount: ({ commit, rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: `users/disable/${userId}`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        confirmEmail: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'users/ConfirmEmail/',
                    data: { userId: payload.userId, token: payload.token },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        regenerateConfirmationEmail: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'users/generateConfirmationEmail',
                    data: { email: payload.email },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        resetPassword: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'users/resetPassword',
                    data: { token: payload.token, email: payload.email, password: payload.password },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        generateResetPasswordEmail: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'users/generateResetPasswordEmail',
                    data: { email: payload.email },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        deleteUser: ({ commit, rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: `users/delete/${userId}`,
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
    }
}

export default users