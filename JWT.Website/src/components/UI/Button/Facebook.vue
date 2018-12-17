<template>
    <button class="btn btn-facebook btn-block">
        <i class="fab fa-facebook-f fixed-width"></i>
        <span>Facebook</span>
    </button>
</template>

<script>
export default {
    name: 'FacebookButton'
}
</script>

<style lang="scss" scoped>
.btn-facebook {
    color: white;
    background-color: #3b5998;
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
        float: left;
        margin-top: 5px;
    }
}
</style>
