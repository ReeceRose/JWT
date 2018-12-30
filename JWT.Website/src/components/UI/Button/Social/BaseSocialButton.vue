<template>
    <button class="btn btn-social btn-social btn-block" @click="submit">
        <slot>

        </slot>
    </button>
</template>

<script>
export default {
    name: 'SocialButton',
    props: {
        submit: {
            type: Function,
            required: true
        }
    }
}
</script>

<style lang="scss" scoped>
.btn-social {
    color: white;
    margin: 10px 0;
    font-size: 15px;
    text-align: center;
    line-height: 24px;    

    &:hover {
        opacity: 0.9;
    }

    .fixed-width {
        width: 0;
        margin-left: 10px;
        margin-right: -15px;
        float: left;
        margin-top: 5px;
    }
}
</style>
