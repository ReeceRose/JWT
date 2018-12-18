<template>
    <BaseSocialButton>
        <i class="fab fa-facebook-f fixed-width"></i>
        <span>Facebook</span>
    </BaseSocialButton>
</template>

<script>
import BaseSocialButton from '@/components/UI/Button/Social/BaseSocialButton.vue'

export default {
    name: 'FacebookButton',
    components: {
        BaseSocialButton
    }
}
</script>

<style lang="scss" scoped>
.btn-social {
    background-color: #3b5998;
}
</style>
