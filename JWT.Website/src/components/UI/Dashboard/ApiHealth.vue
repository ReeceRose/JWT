<template>
    <div>
        API Status: 
            <span v-if="result === 'Healthy'" class="badge badge-success">{{ result }}</span>
            <span v-if="result === 'Not Available'" class="badge badge-danger">{{ result }}</span>
    </div>
</template>

<script>
import axios from 'axios'
export default {
    data() {
        return {
            result: 'Not available'
        }
    },
    beforeCreate() {
        axios.get('https://localhost:5001/health')
        // axios.get('http://159.203.18.146/health')
        .then((response) => {
            this.result = response.data
        })
        .catch(() => {
            this.result = 'Not Available'
        })
    }
}
</script>
