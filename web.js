
window.addEventListener('scroll', function() {
    const header = document.querySelector('header');
    const content = document.querySelector('.content');
    content.style.marginTop = header.offsetHeight + 'px';
});