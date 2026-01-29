/**
 * TERMIZ MTU - Main JavaScript
 * Railway Website Functionality
 */

document.addEventListener('DOMContentLoaded', function () {
    // Initialize all components
    initMobileMenu();
    initStickyHeader();
    initHeroSlider();
    initStatsCounter();
    initSmoothScroll();
    initContactForm();
});

/**
 * Mobile Menu Toggle
 */
function initMobileMenu() {
    const menuBtn = document.getElementById('mobileMenuBtn');
    const mobileMenu = document.getElementById('mobileMenu');
    const mobileLinks = document.querySelectorAll('.mobile-nav-link');

    if (menuBtn && mobileMenu) {
        menuBtn.addEventListener('click', function () {
            menuBtn.classList.toggle('active');
            mobileMenu.classList.toggle('active');
        });

        // Close menu when clicking a link (but not dropdown toggles)
        mobileLinks.forEach(link => {
            if (!link.classList.contains('mobile-dropdown-toggle')) {
                link.addEventListener('click', function () {
                    menuBtn.classList.remove('active');
                    mobileMenu.classList.remove('active');
                });
            }
        });

        // Mobile dropdown toggle
        const mobileDropdownToggles = document.querySelectorAll('.mobile-dropdown-toggle');
        mobileDropdownToggles.forEach(toggle => {
            toggle.addEventListener('click', function (e) {
                e.preventDefault();
                const dropdown = this.parentElement;
                dropdown.classList.toggle('active');
            });
        });

        // Close menu when clicking outside
        document.addEventListener('click', function (e) {
            if (!menuBtn.contains(e.target) && !mobileMenu.contains(e.target)) {
                menuBtn.classList.remove('active');
                mobileMenu.classList.remove('active');
            }
        });
    }
}

/**
 * Sticky Header with shadow on scroll
 */
function initStickyHeader() {
    const header = document.getElementById('header');

    if (header) {
        window.addEventListener('scroll', function () {
            if (window.scrollY > 100) {
                header.classList.add('scrolled');
            } else {
                header.classList.remove('scrolled');
            }
        });
    }
}

/**
 * Hero Section Image Slider
 */
function initHeroSlider() {
    const slides = document.querySelectorAll('.hero-slide');
    const navBtns = document.querySelectorAll('.hero-nav-btn');
    let currentSlide = 0;
    let slideInterval;

    if (slides.length === 0) return;

    function showSlide(index) {
        // Remove active class from all slides and buttons
        slides.forEach(slide => slide.classList.remove('active'));
        navBtns.forEach(btn => btn.classList.remove('active'));

        // Add active class to current slide and button
        slides[index].classList.add('active');
        if (navBtns[index]) {
            navBtns[index].classList.add('active');
        }

        currentSlide = index;
    }

    function nextSlide() {
        const next = (currentSlide + 1) % slides.length;
        showSlide(next);
    }

    function startAutoSlide() {
        slideInterval = setInterval(nextSlide, 6000);
    }

    function stopAutoSlide() {
        clearInterval(slideInterval);
    }

    // Add click events to navigation buttons
    navBtns.forEach((btn, index) => {
        btn.addEventListener('click', function () {
            stopAutoSlide();
            showSlide(index);
            startAutoSlide();
        });
    });

    // Start auto-sliding
    startAutoSlide();

    // Pause on hover
    const heroSection = document.querySelector('.hero');
    if (heroSection) {
        heroSection.addEventListener('mouseenter', stopAutoSlide);
        heroSection.addEventListener('mouseleave', startAutoSlide);
    }
}

/**
 * Animated Statistics Counter
 */
function initStatsCounter() {
    const statNumbers = document.querySelectorAll('.stat-number');
    let hasAnimated = false;

    function animateNumber(element, target) {
        const duration = 2000;
        const increment = target / (duration / 16);
        let current = 0;

        function updateNumber() {
            current += increment;
            if (current < target) {
                element.textContent = Math.floor(current);
                requestAnimationFrame(updateNumber);
            } else {
                element.textContent = target;
            }
        }

        updateNumber();
    }

    function checkScroll() {
        if (hasAnimated) return;

        const statsSection = document.querySelector('.stats');
        if (!statsSection) return;

        const rect = statsSection.getBoundingClientRect();
        const windowHeight = window.innerHeight;

        if (rect.top < windowHeight * 0.8) {
            hasAnimated = true;
            statNumbers.forEach(stat => {
                const target = parseInt(stat.getAttribute('data-target'));
                if (target) {
                    animateNumber(stat, target);
                }
            });
        }
    }

    window.addEventListener('scroll', checkScroll);
    checkScroll(); // Check on initial load
}

/**
 * Smooth Scroll for Navigation Links
 */
function initSmoothScroll() {
    const links = document.querySelectorAll('a[href^="#"]');

    links.forEach(link => {
        link.addEventListener('click', function (e) {
            const href = this.getAttribute('href');
            if (href === '#') return;

            const target = document.querySelector(href);
            if (target) {
                e.preventDefault();

                const headerHeight = document.querySelector('.header').offsetHeight;
                const targetPosition = target.getBoundingClientRect().top + window.scrollY - headerHeight;

                window.scrollTo({
                    top: targetPosition,
                    behavior: 'smooth'
                });
            }
        });
    });
}

/**
 * Contact Form Submission Handler
 */
function initContactForm() {
    const form = document.getElementById('contactForm');

    if (form) {
        form.addEventListener('submit', function (e) {
            e.preventDefault();

            // Get form data
            const formData = new FormData(form);
            const data = {};
            formData.forEach((value, key) => {
                data[key] = value;
            });

            // Validate form
            if (!data.name || !data.phone || !data.message) {
                showNotification('Iltimos, barcha majburiy maydonlarni to\'ldiring.', 'error');
                return;
            }

            // Show loading state
            const submitBtn = form.querySelector('button[type="submit"]');
            const originalText = submitBtn.innerHTML;
            submitBtn.innerHTML = 'Yuborilmoqda...';
            submitBtn.disabled = true;

            // Simulate form submission (replace with actual API call)
            setTimeout(function () {
                showNotification('Xabaringiz muvaffaqiyatli yuborildi!', 'success');
                form.reset();
                submitBtn.innerHTML = originalText;
                submitBtn.disabled = false;
            }, 1500);
        });
    }
}

/**
 * Show Notification Message
 */
function showNotification(message, type) {
    // Remove existing notifications
    const existing = document.querySelector('.notification');
    if (existing) {
        existing.remove();
    }

    // Create notification element
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <span>${message}</span>
        <button class="notification-close">&times;</button>
    `;

    // Add styles
    notification.style.cssText = `
        position: fixed;
        bottom: 20px;
        right: 20px;
        padding: 16px 24px;
        border-radius: 12px;
        font-size: 15px;
        font-weight: 500;
        display: flex;
        align-items: center;
        gap: 16px;
        z-index: 1000;
        animation: slideIn 0.3s ease;
        box-shadow: 0 10px 25px -5px rgba(0, 0, 0, 0.2);
    `;

    if (type === 'success') {
        notification.style.backgroundColor = '#10B981';
        notification.style.color = 'white';
    } else {
        notification.style.backgroundColor = '#EF4444';
        notification.style.color = 'white';
    }

    // Add to DOM
    document.body.appendChild(notification);

    // Close button functionality
    const closeBtn = notification.querySelector('.notification-close');
    closeBtn.style.cssText = `
        background: none;
        border: none;
        color: white;
        font-size: 20px;
        cursor: pointer;
        padding: 0;
        line-height: 1;
    `;
    closeBtn.addEventListener('click', function () {
        notification.remove();
    });

    // Auto-remove after 5 seconds
    setTimeout(function () {
        if (notification.parentNode) {
            notification.remove();
        }
    }, 5000);
}

// Add notification animation
const style = document.createElement('style');
style.textContent = `
    @keyframes slideIn {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
`;
document.head.appendChild(style);