<template>
    <div class="row">
        <div class="col-sm-10 col-md-9 col-lg-8 mx-auto">
            <div class="card custom-card my-5">
                <div class="custom-card-body">
                    <h5 class="custom-card-title text-center">{{ title }}</h5>
                    <slot name="card-content">

                    </slot>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'WideCard',
    props: {
        title: String
    },
}
</script>

<style lang="scss" scoped>
.custom-card {
    border: 0;
    border-radius: 1rem;
    box-shadow: 0 0.5rem 1rem 0 rgba(0, 0, 0, 0.1);

    .custom-card-title {
        margin-bottom: 2rem;
        font-weight: 300;
        font-size: 1.5rem;
    }

    .custom-card-body {
        padding: 2rem;
    }
}
</style>