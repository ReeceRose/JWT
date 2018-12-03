import axios from 'axios'
import store from '@/store/store.js'
axios.defaults.headers.common['Authorization'] = `Bearer ${store.getters['authentication/getToken']}`
export default axios.create({
    baseURL: "https://localhost:5001/api/v1/"
})