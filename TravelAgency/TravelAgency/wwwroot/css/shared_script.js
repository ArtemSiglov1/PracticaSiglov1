
document.addEventListener('DOMContentLoaded', function () {
    window.addEventListener('scroll', function () {
        var header = this.document.getElementById('header-top');
        var scrollTop = window.scrollY;
        var maxScroll = 250;

        var copacity = Math.min(scrollTop / maxScroll, 1);
        header.style.backgroundColor = `rgba(255,165,0,${copacity})`;
    });
});

    