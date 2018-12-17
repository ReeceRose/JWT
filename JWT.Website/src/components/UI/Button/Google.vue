<template>
    <button class="btn btn-google btn-block">
        <i class="fab fa-google fixed-width"></i>
        <span>Google</span>
    </button>
</template>

<script>
export default {
    name: 'GoogleButton'
}
</script>


<style lang="scss" scoped>
.btn-google {
    color: white;
    background-color: #ea4335;
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
