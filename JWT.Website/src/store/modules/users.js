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
                    url: 'admin/users/count',
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
                    url: 'admin/users',
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