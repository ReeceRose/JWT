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
                        console.log(response)
                        resolve(response.data.count)
                    })
                    .catch((error) => {
                        console.log(error)
                        reject()
                    })
            })
        },
    }
}

export default users