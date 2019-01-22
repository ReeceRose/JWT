import axios from '@/axios.js'
const users = {
    namespaced: true,
    actions: {
        userCount: ({ rootGetters }) => {
            return new Promise((resolve, reject) => {
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
            })
        },
        users: ({ rootGetters }) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: 'users/',
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
            })
        },
        userById: ({ rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `users/${userId}`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject(error)
                    })
            })
        },
        forceEmailConfirmation: ({ rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `users/${userId}/ForceEmailConfirmation`,
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
            })
        },
        sendEmailConfirmation: ({ rootGetters }, email) => {
            return new Promise((resolve, reject) => {
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
            })
        },
        enableAccount: ({ rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `users/${userId}/enable`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
            })
        },
        disableAccount: ({ rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `users/${userId}/disable`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
            })
        },
        confirmEmail: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'users/confirmEmail',
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
    }
}

export default users