
(function () {

})();

document.addEventListener("DOMContentLoaded", function () {
    let btn = document.querySelector(".login-submit-btn");

    btn.addEventListener("click", fetchPost);
});

async function fetchPost() {
    const nav = navigator;
    const appName = nav.userAgent;
    const formData = new FormData();

    const login = document.querySelector(".login").value;
    const pass = document.querySelector(".pass").value;
    const url = document.querySelector(".url").value;

    formData.append("User", login);
    formData.append("Password", pass);
    formData.append("ReturnUrl", url);

    const response = await fetch("http://localhost:5000/auth/login", {
        credentials: "include",
        method: "POST",
        headers: {
            "Accept": "application/json",
            //"Content-Type": "multipart/form-data",      //"application/x-www-form-urlencoded",
        },
        body: formData
    });

    const redirectUrl = response["url"];

    if (response.ok === true) {
        if (appName.includes("AppleWebKit")) {
            window.location.replace(url);
		}
        
    } else {
        console.log("Error: ", response.status, data.erorrText);
    }

    window.location.replace(redirectUrl);    
}