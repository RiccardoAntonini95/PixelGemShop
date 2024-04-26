var avgRatingInput = document.querySelectorAll('.average-rating');
var starContainers = document.querySelectorAll('.stars-container');
var numStars = [];


// Calcola il numero di stelle piene arrotondate per ogni input di media
for (let i = 0; i < avgRatingInput.length; i++) {
    var value = avgRatingInput[i].value.replace(',', '.');
    numStars[i] = Math.round(parseFloat(value));
}

// Itera su ogni container di stelle e colora le stelle corrispondenti
starContainers.forEach(function (container, index) {
    var stars = container.querySelectorAll('.star-avg-rating');

    // Colora le prime numStars stelle nel container corrente
    for (var i = 0; i < numStars[index]; i++) {
        stars[i].querySelector('path').setAttribute('fill', 'gold');
    }
});
