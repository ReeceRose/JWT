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
        enable: ({ rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `users/${userId}/enable`,
                    data: { userId: userId },
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
        disable: ({ rootGetters }, userId) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `users/${userId}/disable`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
            })
        }
    }
}

export default users