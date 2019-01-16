<template>
    <div>
        <div v-if="result === 'Healthy' ">
            API Status: <span class="badge badge-success">{{ result }}</span>
        </div>
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
        axios.get('https://localhost:5001/ready')
        .then((response) => {
            this.result = response.data
        })
    }
}
</script>
