function initializeIntersectionObserver() {
    const animatedElements = document.querySelectorAll('.animate-slide-up:not(.visible)');

    if (animatedElements.length === 0) return;

    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                observer.unobserve(entry.target);
            }
        });
    }, {
        threshold: 0.1
    });

    animatedElements.forEach(el => observer.observe(el));
}

function scrollToTop() {
    requestAnimationFrame(() => {
        window.scrollTo(0, 0);
    });
}
