import axios from '@/axios.js'
import utilities from '@/utilities.js'
import global from '@/store/modules/global.js'
import '@/facebook/init.js'
import '@/google/init.js'
// For reference
// headers: { Authorization: `Bearer ${getters['uthentication/getToken'] || ''}`}
const authentication = {
    namespaced: true,
    getters: {
        isAdmin: () => utilities.parseJwt(global.state.token).hasOwnProperty("Administrator")
    },
    actions: {
        login: ({ commit, dispatch }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/login',
                    data: { email: payload.email, password: payload.password },
                })
                    .then((response) => {
                        const token = response.data.token
                        dispatch("global/updateToken", token, { root: true })
                        if (payload.rememberMe) {
                            // Store cookie
                        }
                        resolve()
                    })
                    .catch(error => {
                        reject(error)
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        facebookLogin: ({ commit, dispatch }) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                // eslint-disable-next-line
                FB.login(
                    loginResponse => {
                        // handle the response
                        if (loginResponse.status === "connected") {
                            // eslint-disable-next-line
                            FB.api("/me?fields=email", meResponse => {
                                axios({
                                    method: 'post',
                                    url: 'authentication/externalLogin',
                                    data: { email: meResponse.email, accessToken: loginResponse.authResponse.accessToken },
                                })
                                    .then((response) => {
                                        const token = response.data.token
                                        dispatch("global/updateToken", token, { root: true })
                                        resolve()
                                    })
                                    .catch(error => {
                                        reject(error)
                                    })
                                    .finally(() => {
                                        commit('global/setLoading', false, { root: true })
                                    })
                            })
                        // Facebook login error
                        } else {
                            reject();
                        }
                    },
                    {
                        scope: "email",
                        return_scopes: true
                    }
                );
            })
        },
        googleLogin: ({ commit, dispatch }) => {
            return new Promise((resolve, reject) => {    
                let googleAuth = window.gapi.auth2.getAuthInstance()
                googleAuth.signIn().then(googleUser => {
                    axios({
                        method: 'post',
                        url: 'authentication/externalLogin',
                        data: { email: googleUser.w3.U3, accessToken: googleUser.Zi.id_token },
                    })
                        .then((response) => {
                            const token = response.data.token
                            dispatch("global/updateToken", token, { root: true })
                            resolve()
                        })
                        .catch(() => {
                            reject()
                        })
                        .finally(() => {
                            commit('global/setLoading', false, { root: true })
                        })
                    resolve()
                })
                .catch(() => {
                    reject()
                })
            })
        },
        logout: ({ commit }) => {
            commit("global/removeToken", null, { root: true })
            try {
                let googleAuth = window.gapi.auth2.getAuthInstance()
                googleAuth.signOut()
                // eslint-disable-next-line
                FB.logout(() => {})
            // eslint-disable-next-line
            } catch (error) {
                // If not signed in it will throw an error
            }

        },
        register: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/register',
                    data: { email: payload.email, password: payload.password }
                })
                    .then(response => {
                        resolve(response)
                    })
                    .catch((error) => {
                        reject(error)
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        confirmEmail: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/confirmEmail',
                    data: { userId: payload.userId, token: payload.token },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        regenerateConfirmationEmail: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/generateConfirmationEmail',
                    data: { email: payload.email },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        resetPassword: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/resetPassword',
                    data: { token: payload.token, email: payload.email, password: payload.password },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        generateResetPasswordEmail: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/generateResetPasswordEmail',
                    data: { email: payload.email },
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        }
    }
}

export default authentication