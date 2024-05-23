const containerLoader = document.querySelector(".loader-container");

window.addEventListener("load", () => {
    containerLoader.classList.add("hidden")
})

var alertDiv = document.getElementById('alertDiv');

setTimeout(function () {
    alertDiv.style.display = 'none'; 
}, 3000); 
