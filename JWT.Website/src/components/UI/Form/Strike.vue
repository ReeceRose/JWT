<template>
    <div class="my-4 strike">
        <span>{{ text }}</span>
    </div>
</template>

<script>
export default {
    name: 'Strike',
    props: {
        text: String
    }
}
</script>


<style lang="scss" scoped>
.strike {
    display: block;
    text-align: center;
    overflow: hidden;
    white-space: nowrap;

    span {
        position: relative;
        display: inline-block;

        &:before, &:after {
            content: "";
            position: absolute;
            top: 50%;
            width: 5000px;
            height: 1px;
            background-color: #000;
        }

        &:before {
            right: 100%;
            margin-right: 15px;
        }

        &:after {
            left: 100%;
            margin-left: 15px;
        }
    }
}
</style>