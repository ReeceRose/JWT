window.fbAsyncInit = function() {
    // eslint-disable-next-line
    FB.init({
        appId: "780843438933865",
        autoLogAppEvents: false,
        xfbml: false,
        version: "v3.2"
    });
};

(function(d, s, id) {
    var js,
        fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {
        return;
    }
    js = d.createElement(s);
    js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
})(document, "script", "facebook-jssdk");