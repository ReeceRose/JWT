<template>
    <div>
        <DisplayCard>

        </DisplayCard>
        <h2>Email confirmation</h2>
    </div>
</template>

<script>
import DisplayCard from '@/components/UI/Card/DisplayCard.vue'

export default {
    name: 'confirmEmail',
    components: {
        DisplayCard
    },
    data() {
        return {
            code: this.$route.params.code
        }
    },
    methods: {
        confirmEmail() {
            // Send code to API and verify it
        }
    },
    created() {
        this.confirmEmail()
    }
}
</script>
