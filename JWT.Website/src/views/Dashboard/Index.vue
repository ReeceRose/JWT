<template>
    <div>
        <h1>
            Admin Dashboard
        </h1>
        <div class="row">
            <HeaderCard title="Users">
                <div slot="card-content">
                    <p><i class="fas fa fa-users"></i>  User Count: X</p>
                </div>
            </HeaderCard>
            <HeaderCard title="TBD">
                <div slot="card-content">

                </div>
            </HeaderCard>
            <HeaderCard title="TBD">
                <div slot="card-content">

                </div>
            </HeaderCard>
        </div>
    </div>
</template>

<script>
import HeaderCard from '@/components/UI/Card/Admin/HeaderCard.vue'

export default {
    name: 'Dashboard',
    components: {
        HeaderCard
    }
}
</script>