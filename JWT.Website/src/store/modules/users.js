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
                console.log(userId)
                axios({
                    method: 'get',
                    url: `users/${userId}`,
                    data: { userId: userId },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch((error) => {
                        reject(error)
                    })
            })
        }
    }
}

export default users