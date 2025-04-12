function selectColor(imgId, colorNameId, newSrc, colorName) {
    const img = document.getElementById(imgId);
    if (img) {
        img.classList.remove('fade-in');
        void img.offsetWidth; // trigger reflow
        img.src = newSrc;
        img.classList.add('fade-in');
    }

    const label = document.getElementById(colorNameId);
    if (label) label.textContent = colorName;
}

function scrollCarousel(direction) {
    const container = document.getElementById("carousel");
    const track = container.querySelector(".carousel-track");
    const cards = track.querySelectorAll(".product-card");

    const cardWidth = cards[0].offsetWidth + 20; // 20: gap değeri
    const maxScroll = track.scrollWidth - container.clientWidth;
    const currentScroll = container.scrollLeft;

    let newScroll = currentScroll + direction * cardWidth;

    // Başa dön
    if (newScroll > maxScroll) {
        newScroll = 0;
    }

    // Sona dön
    if (newScroll < 0) {
        newScroll = maxScroll;
    }

    container.scrollTo({ left: newScroll, behavior: "smooth" });
}
