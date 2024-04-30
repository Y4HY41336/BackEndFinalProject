const loadMore = document.getElementById("loadMoreBtn");
const productBox = document.getElementById("productBox");

let skip = 6;
loadMore.addEventListener("click", function () {
    let url = `/Shop/LoadMore?skip=${skip}`;
    console.log(url);
    fetch(url).then(response => response.text())
        .then(data => productBox.innerHTML += data);

    skip += 3;

});