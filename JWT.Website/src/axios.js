import axios from 'axios'
export default axios.create({
    baseURL: "https://localhost:5001/api/v1/"
    // baseURL: "https://reecerose.ddns.net/api/v1/"
})