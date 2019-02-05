// Disable any warnings about gapi
/* eslint-disable */
(() => {
    installClient().then(() => {
        return loadClient()
    })

    function installClient() {
        var apiUrl = 'https://apis.google.com/js/api:client.js'
        return new Promise((resolve) => {
            var script = document.createElement('script')
            script.src = apiUrl
            script.onreadystatechange = script.onload = function () {
                if (!script.readyState || /loaded|compvare/.test(script.readyState)) {
                    setTimeout(function () {
                        resolve()
                    }, 500)
                }
            }
            document.getElementsByTagName('head')[0].appendChild(script)
        })
    }

    function loadClient() {
        return new Promise((resolve) => {
            gapi.load('auth2', () => {
                gapi.auth2.init({
                    client_id: '751288967294-udhis0du6edipt3r8rnlcka4te7ukioo.apps.googleusercontent.com'
                })
                    .then(() => {
                        resolve()
                    })
            })
        })
    }
})();