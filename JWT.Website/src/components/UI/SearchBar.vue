<template>
    <form @submit.prevent="submit">
        <div class="d-flex justify-content-center h-100">
            <div class="search-box">
                <input class="search-input" type="text" placeholder="Enter an email" :value="search" aria-label="Enter an emaik">
                <button type="submit" class="search-button" aria-label="Search for an email"><i class="fas text-primary fa-search"></i></button>
            </div>
        </div>
    </form>
</template>

<script>
export default {
    data() {
        return {
            search: ''
        }
    },
    props: {
        submit: Function
    }
};
</script>


<style lang="scss" scoped>
.search-box {
	margin-bottom: auto;
	margin-top: auto;
	height: 60px;
	background-color: #22292f;
	border-radius: 30px;
	padding: 10px;
	color: #fff;

	.search-input {
		// text-align: center;
		color: white;
		border: 0;
		outline: 0;
		background: none;
		width: 0;
		line-height: 40px;
		transition: width 0.4s linear;
	}

	.search-button {
		border: none;
		height: 40px;
		background-color: #22292f;
		width: 40px;
		float: right;
		display: flex;
		justify-content: center;
		align-items: center;
		border-radius: 50%;
	}

	&:hover .search-input,
	&:focus-within .search-input {
		width: 40vw;
		transition: width 0.4s linear;
	}

	&:hover .search-button,
	&:focus-within .search-button {
		background-color: #fff;
	}

	&:not(:hover) .search-input {
		transition-delay: 1s;
	}
}
</style>