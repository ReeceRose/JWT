<template>
    <div class="row">
        <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
            <div class="card custom-card my-5">
                <div class="custom-card-body">
                    <h5 class="custom-card-title text-center">{{ title }}</h5>
                    <slot name="card-information">

                    </slot>
                    <form class="custom-form" @submit.prevent="submit">
                        <slot name="card-content">

                        </slot>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: ['title', 'submit'],
    methods: {
        submitForm() {
            // eslint-disable-next-line
            submit()
        }
    }

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

.custom-form {
    width: 100%;

    .btn {
        font-size: 80%;
        border-radius: 5rem;
        letter-spacing: .1rem;
        font-weight: bold;
        padding: 1rem;
        transition: all 0.2s;
    }

    .form-label-group {
        position: relative;
        margin-bottom: 1rem;

        input {
            border-radius: 2rem;
        }
    }
}
</style>