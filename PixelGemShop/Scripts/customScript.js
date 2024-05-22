const containerLoader = document.querySelector(".loader-container");
const lightModeBtn = document.querySelector("#lightModeBtn");
const body = document.body;

window.addEventListener("load", () => {
    containerLoader.classList.add("hidden")
})

var alertDiv = document.getElementById('alertDiv');

setTimeout(function () {
    alertDiv.style.display = 'none'; 
}, 3000); 

lightModeBtn.addEventListener("click", () => {
    console.log("clic")
    body.style.color = "red"
})