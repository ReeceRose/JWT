<template>
    <div class="background">
        <div class="spinner"></div>
    </div>
</template>

<style lang="scss" scoped>
.background {
    background-color: rgba(0, 0, 0, 0.5);
    position: fixed;
    left: 0;
    width: 100vw;
    height:100vh;
    display: flex;
    align-items: center;
    top: 0;
    z-index: 1;

    .spinner {
        display: flex;
        margin: 0 auto;
        height:60px;
        width:60px;
        animation: rotation .6s infinite linear;
        border-left:6px solid rgba(0,174,239,.15);
        border-right:6px solid rgba(0,174,239,.15);
        border-bottom:6px solid rgba(0,174,239,.15);
        border-top:6px solid rgba(0,174,239,.8);
        border-radius:100%;
    }
    
    @keyframes rotation {
        from {transform: rotate(0deg);}
        to {transform: rotate(359deg);}
    }
}
</style>
