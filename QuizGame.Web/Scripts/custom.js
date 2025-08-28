// Hero section typing effect
const heroTitles = [
    'समाज में <span class="text-yellow-400">सकारात्मक बदलाव</span>',
    'हर व्यक्ति के लिए <span class="text-yellow-400">सशक्त भविष्य</span>',
    'आपसी सहयोग से <span class="text-yellow-400">नई उम्मीदें</span>',
    'आइए साथ मिलकर <span class="text-yellow-400">जिंदगी बदलें</span>'
];
let idx = 0, ch = 0, deleting = false;
const heroEl = document.getElementById('heroTitle');
function typeHero() {
    const raw = heroTitles[idx];
    const plain = raw.replace(/<[^>]*>/g, '');
    if (!deleting) {
        ch++;
        heroEl.innerHTML = raw.slice(0, ch + (raw.length - plain.length));
        if (ch >= plain.length) { deleting = true; setTimeout(typeHero, 2000); }
        else setTimeout(typeHero, 70);
    } else {
        ch--;
        heroEl.innerHTML = raw.slice(0, ch + (raw.length - plain.length));
        if (ch <= 0) { deleting = false; idx = (idx + 1) % heroTitles.length; setTimeout(typeHero, 600); }
        else setTimeout(typeHero, 40);
    }
}
if (heroEl) typeHero();

// Hero section image slider
const imgs = [
    'https://img.freepik.com/free-photo/diverse-people-refugee-camps_23-2151561484.jpg?uid=R39734901&ga=GA1.1.528027500.1727091885&semt=ais_hybrid&w=740',
    'https://img.freepik.com/premium-photo/international-day-eradication-poverty-empowering-communities_1198884-21736.jpg?uid=R39734901&ga=GA1.1.528027500.1727091885&semt=ais_hybrid&w=740',
    'https://img.freepik.com/premium-photo/acts-charity-prevalent-makar-sankranti-with-volunteers-distributing-warm-clothes-food-s_950002-603901.jpg?uid=R39734901&ga=GA1.1.528027500.1727091885&semt=ais_hybrid&w=740'
];
let curImg = 0;
const imgEl = document.getElementById('heroImage');
setInterval(() => {
    curImg = (curImg + 1) % imgs.length;
    imgEl.style.opacity = 0;
    setTimeout(() => { imgEl.src = imgs[curImg]; imgEl.style.opacity = 1; }, 400);
}, 4000);

// Mega menu toggle
function toggleMegaMenu() {
    const megaMenu = document.getElementById("megaMenu");
    const overlay = document.getElementById("megaMenuOverlay");
    const icon = document.getElementById("megaMenuIcon");
    megaMenu.classList.toggle("active");
    overlay.classList.toggle("active");
    icon.classList.toggle("rotate-180");
}

// Close mega menu
function closeMegaMenu() {
    document.getElementById("megaMenu").classList.remove("active");
    document.getElementById("megaMenuOverlay").classList.remove("active");
    document.getElementById("megaMenuIcon").classList.remove("rotate-180");
}

// Mobile menu toggle
function toggleMobileMenu() {
    const menu = document.getElementById("mobileMenu");
    menu.classList.toggle("active");
    closeMegaMenu();
}

// Mobile menu accordion toggle
function toggleMobileAccordion() {
    const content = document.getElementById("mobileAccordionContent");
    const icon = document.getElementById("mobileAccordionIcon");
    content.classList.toggle("active");
    icon.classList.toggle("rotate-180");
}

// Media modal open
function openMediaModal(src, type) {
    const modal = document.getElementById('media-modal');
    const content = document.getElementById('media-content');
    const container = document.getElementById('media-container');
    content.innerHTML = '';
    if (type === 'image') {
        const img = document.createElement('img');
        img.src = src;
        img.className = 'max-h-full max-w-full object-contain rounded-lg shadow-2xl';
        content.appendChild(img);
    } else if (type === 'video') {
        const video = document.createElement('video');
        video.src = src;
        video.controls = true;
        video.autoplay = true;
        video.className = 'max-h-full max-w-full object-contain rounded-lg shadow-2xl';
        content.appendChild(video);
    }
    modal.classList.remove('hidden');
    modal.classList.add('flex');
    document.body.style.overflow = 'hidden';
    setTimeout(() => {
        container.classList.add('scale-100', 'opacity-100');
        container.classList.remove('scale-95', 'opacity-0');
    }, 50);
}

// Media modal close
function closeMediaModal() {
    const modal = document.getElementById('media-modal');
    const container = document.getElementById('media-container');
    container.classList.remove('scale-100', 'opacity-100');
    container.classList.add('scale-95', 'opacity-0');
    document.body.style.overflow = 'auto';
    setTimeout(() => {
        modal.classList.add('hidden');
        modal.classList.remove('flex');
        document.getElementById('media-content').innerHTML = '';
    }, 300);
}

// Close modal on overlay click
document.getElementById('media-modal').addEventListener('click', function (e) {
    if (e.target === this) closeMediaModal();
});

// Close modal on Escape key
document.addEventListener('keydown', function (e) {
    if (e.key === 'Escape') closeMediaModal();
});

// Gallery fade-in animation on scroll
document.addEventListener('DOMContentLoaded', function () {
    const galleryItems = document.querySelectorAll('.gallery-item');
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    });
    galleryItems.forEach(item => {
        observer.observe(item);
    });
    // Show first tab by default
    showTab('education');
});

// Tab logic with active styling
function showTab(id) {
    document.querySelectorAll('.tab-content').forEach(el => el.classList.add('hidden'));
    document.getElementById(id).classList.remove('hidden');
    document.querySelectorAll('.tab-btn').forEach(btn => {
        btn.classList.remove('bg-teal-50', 'shadow-lg', 'text-teal-700');
        btn.classList.add('text-gray-700');
    });
    const tabButtons = document.querySelectorAll('.tab-btn');
    tabButtons.forEach(btn => {
        if (btn.getAttribute('onclick').includes(id)) {
            btn.classList.add('bg-teal-50', 'shadow-lg', 'text-teal-700');
            btn.classList.remove('text-gray-700');
        }
    });
}

// Video Modal Functions
function openVideoModal(videoId) {
    const modal = document.getElementById('videoModal');
    const videoFrame = document.getElementById('videoFrame');

    // Set the YouTube embed URL with autoplay
    videoFrame.src = `https://www.youtube.com/embed/${videoId}?autoplay=1&rel=0`;

    // Show modal
    modal.classList.remove('hidden');
    modal.classList.add('flex');

    // Prevent body scroll
    document.body.style.overflow = 'hidden';
}

function closeVideoModal() {
    const modal = document.getElementById('videoModal');
    const videoFrame = document.getElementById('videoFrame');

    // Stop video by clearing src
    videoFrame.src = '';

    // Hide modal
    modal.classList.add('hidden');
    modal.classList.remove('flex');

    // Restore body scroll
    document.body.style.overflow = 'auto';
}

// Close video modal when clicking outside
document.addEventListener('DOMContentLoaded', function() {
    const videoModal = document.getElementById('videoModal');
    if (videoModal) {
        videoModal.addEventListener('click', function(e) {
            if (e.target === this) {
                closeVideoModal();
            }
        });
    }
});

// Close video modal on Escape key
document.addEventListener('keydown', function(e) {
    if (e.key === 'Escape') {
        const modal = document.getElementById('videoModal');
        if (modal && !modal.classList.contains('hidden')) {
            closeVideoModal();
        }
    }
});

// Hero Slider Functionality
let currentSlide = 0;
const totalSlides = 4;
let slideInterval;

function showSlide(index) {
    // Hide all slides
    document.querySelectorAll('.hero-slide').forEach(slide => {
        slide.classList.remove('active');
    });
    document.querySelectorAll('.content-slide').forEach(slide => {
        slide.classList.remove('active');
    });
    document.querySelectorAll('.slider-dot').forEach(dot => {
        dot.classList.remove('active');
    });

    // Show current slide
    document.querySelectorAll('.hero-slide')[index].classList.add('active');
    document.querySelectorAll('.content-slide')[index].classList.add('active');
    document.querySelectorAll('.slider-dot')[index].classList.add('active');

    currentSlide = index;
}

function nextSlide() {
    currentSlide = (currentSlide + 1) % totalSlides;
    showSlide(currentSlide);
}

function prevSlide() {
    currentSlide = (currentSlide - 1 + totalSlides) % totalSlides;
    showSlide(currentSlide);
}

function startSlideShow() {
    slideInterval = setInterval(nextSlide, 5000); // Change slide every 5 seconds
}

function stopSlideShow() {
    clearInterval(slideInterval);
}

// Scroll to Donation Section
function scrollToDonation() {
    const donationSection = document.getElementById('donation-section');
    if (donationSection) {
        donationSection.scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        });
    }
}

// Initialize Hero Slider
document.addEventListener('DOMContentLoaded', function() {
    // Start automatic slideshow
    startSlideShow();

    // Navigation arrows
    const nextBtn = document.getElementById('nextSlide');
    const prevBtn = document.getElementById('prevSlide');

    if (nextBtn) {
        nextBtn.addEventListener('click', () => {
            stopSlideShow();
            nextSlide();
            startSlideShow();
        });
    }

    if (prevBtn) {
        prevBtn.addEventListener('click', () => {
            stopSlideShow();
            prevSlide();
            startSlideShow();
        });
    }

    // Dot navigation
    document.querySelectorAll('.slider-dot').forEach((dot, index) => {
        dot.addEventListener('click', () => {
            stopSlideShow();
            showSlide(index);
            startSlideShow();
        });
    });

    // Pause on hover
    const heroSection = document.querySelector('.hero-slider');
    if (heroSection) {
        heroSection.addEventListener('mouseenter', stopSlideShow);
        heroSection.addEventListener('mouseleave', startSlideShow);
    }

    // Initialize volunteers slider if it exists
    if (typeof $ !== 'undefined' && $('.volunteers-slider').length) {
        $('.volunteers-slider').owlCarousel({
            loop: true,
            margin: 30,
            nav: true,
            dots: true,
            autoplay: true,
            autoplayTimeout: 5000,
            autoplayHoverPause: true,
            navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
            responsive: {
                0: {
                    items: 1,
                    margin: 15
                },
                768: {
                    items: 2,
                    margin: 20
                },
                1024: {
                    items: 3,
                    margin: 30
                }
            }
        });
    }
});

// Events & Blog Slider Functionality
let currentSliderIndex = 0;

function showSlide(index) {
    const slider = document.getElementById('slider');
    const buttons = document.querySelectorAll('.slider-btn');
    
    if (slider) {
        slider.style.transform = `translateX(-${index * 100}%)`;
        currentSliderIndex = index;
        
        // Update button styles
        buttons.forEach((btn, i) => {
            if (i === index) {
                btn.classList.remove('bg-gray-300', 'text-gray-700');
                btn.classList.add('bg-purple-600', 'text-white');
                if (i === 1) {
                    btn.classList.remove('bg-purple-600');
                    btn.classList.add('bg-emerald-600');
                }
            } else {
                btn.classList.remove('bg-purple-600', 'bg-emerald-600', 'text-white');
                btn.classList.add('bg-gray-300', 'text-gray-700');
            }
        });
    }
}

// Auto-slide functionality
let sliderInterval;

function startAutoSlide() {
    sliderInterval = setInterval(() => {
        const nextIndex = (currentSliderIndex + 1) % 2;
        showSlide(nextIndex);
    }, 8000);
}

function stopAutoSlide() {
    clearInterval(sliderInterval);
}

// Initialize slider on page load
window.addEventListener('load', function() {
    startAutoSlide();
    
    const sliderSection = document.querySelector('.slider-wrapper');
    if (sliderSection) {
        sliderSection.addEventListener('mouseenter', stopAutoSlide);
        sliderSection.addEventListener('mouseleave', startAutoSlide);
    }
});



    // $('.testimonials').owlCarousel({ ... }); // Old, incorrect selector

    $('.testimonials').owlCarousel({
        loop: true,
        margin: 20,
        nav: true,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 3
            },
            1000: {
                items: 4
            }
        }
    });

    // Events Slider
    $('.events-slider').owlCarousel({
        loop: true,
        margin: 30,
        nav: true,
        dots: true,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
        responsive: {
            0: {
                items: 1,
                margin: 15
            },
            768: {
                items: 2,
                margin: 20
            },
            1024: {
                items: 3,
                margin: 30
            }
        }
    });

    // Blog Slider
    $('.blog-slider').owlCarousel({
        loop: true,
        margin: 30,
        nav: true,
        dots: true,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
        responsive: {
            0: {
                items: 1,
                margin: 15
            },
            768: {
                items: 2,
                margin: 20
            },
            1024: {
                items: 3,
                margin: 30
            }
        }
    });

    function openDonatForm() {
        document.getElementById('popup').classList.remove('hidden');
    }

    function closePopup() {
        document.getElementById('popup').classList.add('hidden');
    }

    // Close popup when clicking outside
    document.getElementById('popup').addEventListener('click', function (e) {
        if (e.target === this) {
            closePopup();
        }
    });

    // Close popup with Escape key
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            closePopup();
        }
    });
