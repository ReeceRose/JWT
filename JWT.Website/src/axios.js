import axios from 'axios'
export default axios.create({
    baseURL: "https://localhost:5001/api/v1/"
    // baseURL: "http://159.203.18.146/api/v1/"
})