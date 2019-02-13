<template>
    <NarrowCard :title="title">
        <div slot="card-content">
            <slot name="card-information">

            </slot>
            <form class="custom-form" @submit.prevent="submit">
                <slot name="card-content">

                </slot>
            </form>
        </div>
        <div slot="card-content-below" class="custom-form">
            <slot class="custom-form" name="below-form">

            </slot>
        </div>
    </NarrowCard>
</template>

<script>
import NarrowCard from '@/components/UI/Card/NarrowCard.vue'
export default {
    name: 'FormNarrowCard',
    components: {
        NarrowCard  
    },
    props: {
        title: String,
        submit: Function
    },
    methods: {
        submitForm() {
            // eslint-disable-next-line
            submit()
        }
    }

}
</script>

<style lang="scss">
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