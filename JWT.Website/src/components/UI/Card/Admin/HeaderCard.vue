<template>
    <div @click="click" class="col-sm-4 col-md-4 col-lg-4 mx-auto header-container">
        <div class="card custom-card my-5">
            <div class="custom-card-body">
                <slot name="card-icon">

                </slot>
                <h5 class="custom-card-title text-center mt-3">{{ title }}</h5>
                <slot name="card-content">

                </slot>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'HeaderCard',
    props: {
        title: String,
        click: Function
    },
}
</script>

<style lang="scss" scoped>
.header-container {
    cursor: pointer;
    .custom-card {
        border: 0;
        border-radius: 1rem;
        box-shadow: 0 0.5rem 1rem 0 rgba(0, 0, 0, 0.1);

        .custom-card-title {
            margin-bottom: 0.5rem;
            font-weight: 300;
            font-size: 1.5rem;
        }

        .custom-card-body {
            padding: 0.75rem;
        }
    }
}
</style>