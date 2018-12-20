const utilities = {
    parseJwt: (token) => {
        // const token = String(t)
        var base64Url = token.split('.')[1]
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
        return JSON.parse(window.atob(base64))
    }
}
export default utilities