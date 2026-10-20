// Progressive enhancement for the CV page: the page works fully without this script.
document.documentElement.classList.add('js');

// Mobile menu: toggles the navigation, closes on Escape (returning focus) and on link choice
(function setUpMenu() {
  const button = document.querySelector('.menu-toggle');
  if (!button) return;

  const list = document.getElementById(button.getAttribute('aria-controls'));

  function setOpen(open) {
    button.setAttribute('aria-expanded', String(open));
    button.textContent = open ? 'Close' : 'Menu';
    list.classList.toggle('open', open);
  }

  button.addEventListener('click', () => setOpen(button.getAttribute('aria-expanded') !== 'true'));

  list.addEventListener('click', (event) => {
    if (event.target.closest('.nav-link')) setOpen(false);
  });

  document.addEventListener('keydown', (event) => {
    if (event.key !== 'Escape' || button.getAttribute('aria-expanded') !== 'true') return;

    setOpen(false);
    button.focus();
  });
})();

// One-time scroll reveal; nothing is hidden without IntersectionObserver or with reduced motion
(function setUpReveal() {
  if (!('IntersectionObserver' in window)) return;
  if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) return;

  const observer = new IntersectionObserver((entries) => {
    for (const entry of entries) {
      if (!entry.isIntersecting) continue;

      entry.target.classList.replace('reveal-pending', 'reveal-visible');
      observer.unobserve(entry.target);
    }
  }, { rootMargin: '0px 0px -10% 0px' });

  for (const element of document.querySelectorAll('[data-reveal]')) {
    element.style.setProperty('--reveal-delay', `${Number(element.dataset.reveal) || 0}ms`);
    element.classList.add('reveal-pending');
    observer.observe(element);
  }
})();
