<template>
    <BaseSocialButton>
        <i class="fab fa-google fixed-width"></i>
        <span>Google</span>
    </BaseSocialButton>
</template>

<script>
import BaseSocialButton from '@/components/UI/Button/Social/BaseSocialButton.vue'

export default {
    name: 'GoogleButton',
    components: {
        BaseSocialButton
    }
}
</script>


<style lang="scss" scoped>
.btn-social {
    background-color: #ea4335;
}
</style>
