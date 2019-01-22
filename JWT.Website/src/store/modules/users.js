import axios from '@/axios.js'
const users = {
    namespaced: true,
    getters: {

    },
    mutations: {

    },
    actions: {
        getCount: ({ rootGetters }) => {
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
        getUsers: ({ rootGetters }) => {
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
        getUser: ({ rootGetters }, userId) => {
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
                    .catch((error) => {
                        console.log(error)
                        reject()
                    })
            })
        },
        enable: ({ rootGetters }, userId) => {
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
        disable: ({ rootGetters }, userId) => {
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
        }
    }
}

export default users