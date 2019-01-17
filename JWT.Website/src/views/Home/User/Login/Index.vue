<template>
	<FormNarrowCard title="Login" :submit="submit" v-if="this.$route.name === 'login'">
		<div slot="card-information">
			<p v-if="redirect" class="text-danger text-center mb-3">You must be logged in to view this. Please login below.</p>
            <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
		</div>

		<div slot="card-content">
			<FormEmail v-model="email" :validator="$v.email"/>
			<FormPassword v-model="password" :validator="$v.password"/>

			<div class="custom-control custom-checkbox mb-3">
				<input v-model="rememberMe" type="checkbox" class="custom-control-input" id="inputRememberMe">
				<label class="custom-control-label" for="inputRememberMe">Remember password</label>
			</div>
			<div class="mb-3">
				<router-link :to="{ name: 'resetPassword' }">Forgot your password?</router-link>
			</div>

			<button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Login</button>
			<Strike text="OR" />

			<h5 class="card-title text-center">Login With</h5>
		</div>
		<div slot="below-form">
			<div class="text-center social-btn">
				<FacebookButton :submit="facebook" />
				<GoogleButton :submit="google" />
			</div>
		</div>
	</FormNarrowCard>
	<div v-else>
		<router-view></router-view>
	</div>
</template>

<script>
import FormNarrowCard from '@/components/UI/Card/Form/FormNarrowCard.vue'
import FormEmail from "@/components/UI/Form/Email.vue"
import FormPassword from "@/components/UI/Form/Password.vue"
import Strike from "@/components/UI/Form/Strike.vue"
import FacebookButton from "@/components/UI/Button/Social/Facebook.vue"
import GoogleButton from "@/components/UI/Button/Social/Google.vue"

import { required, minLength, email } from "vuelidate/lib/validators"

export default {
	name: "LoginIndex",
	components: {
		FormNarrowCard,
		FormEmail,
		FormPassword,
		Strike,
		FacebookButton,
		GoogleButton
	},
	data() {
		return {
			email: "",
			password: "",
			rememberMe: true,
			redirect: this.$route.params.redirect,
			error: null,
			errorMessage: "Failed to login. Please try again"
		};
	},
	validations: {
		email: {
			required,
			email
		},
		password: {
			required,
			minLength: minLength(6)
		}
	},
	methods: {
		submit() {
			this.$v.$touch();
			if (this.$v.$invalid) {
				return;
			}
			this.$store
				.dispatch("authentication/login", {
					email: this.email,
					password: this.password,
					rememberMe: this.rememberMe
				})
				.then(() => {
					this.error = false
					this.$router.push({ name: "home" });
				})
				.catch(error => {
					if (error.response) {
						if (
							String(error.response.data.error[0])
								.toLowerCase()
								.includes("email not confirmed")
						) {
							this.$router.push({ name: "confirmEmail" });
						}
						this.errorMessage = error.response.data.error[0];
					}
					this.error = true;
				});
		},
		facebook() {
			this.$store
				.dispatch("authentication/facebookLogin")
				.then(() => {
					this.$router.push({ name: "home" });
				})
				.catch(() => {
					this.error = true;
					this.errorMessage = "Failed to login with Facebook";
				});
		},
		google() {
            this.$store
				.dispatch("authentication/googleLogin")
				.then(() => {
                    this.$router.push({ name: "home" });
				})
				.catch(() => {
					this.error = true;
					this.errorMessage = "Failed to login with Google";
				});
		}
	}
};
</script>

<style lang="scss" scoped>
</style>