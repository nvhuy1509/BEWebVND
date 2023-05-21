$(document).ready(function () {
  $(".slide-list").slick({
    autoplay: true,
    autoplaySpeed: 3500,
    slidesToShow: 3,
    slidesToScroll: 1,
    arrows: false,
    dots: true,
    responsive: [
      {
        breakpoint: 1200,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 1008,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 800,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
    ],
  });

  $('.story-list').slick({
      infinite: false,
      slidesToShow: 2,
      slidesToScroll: 1,
      responsive: [
      {
        breakpoint: 1200,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 1008,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 800,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
    ],
  });
  

  $(window).on("load resize orientationchange", function () {
    $(".mission-list").each(function () {
      var $mission = $(this);
      if ($(window).width() > 767) {
        if ($mission.hasClass("slick-initialized")) {
          $mission.slick("unslick");
        }
      } else {
        if (!$mission.hasClass("slick-initialized")) {
            $mission.slick({
              slidesToShow: 1,
              autoplay: true,
              autoplaySpeed: 3500,
              slidesToScroll: 1,
              mobileFirst: true,
              arrows: false,
              dots: true,
          });
        }
      }
    });
  });

  $(window).on("load resize orientationchange", function () {
    $(".getting-list").each(function () {
      var $getting = $(this);
      if ($(window).width() > 767) {
        if ($getting.hasClass("slick-initialized")) {
          $getting.slick("unslick");
        }
      } else {
        if (!$getting.hasClass("slick-initialized")) {
          $getting.slick({
            autoplay: true,
            autoplaySpeed: 3500,
            slidesToShow: 1,
            slidesToScroll: 1,
            mobileFirst: true,
            arrows: false,
            dots: true,
          });
        }
      }
    });
  });

  $(window).on("load resize orientationchange", function () {
    $(".step-list").each(function () {
      var $step = $(this);
      if ($(window).width() > 1025) {
        if ($step.hasClass("slick-initialized")) {
          $step.slick("unslick");
        }
      } else if ($(window).width() > 768) {
        if (!$step.hasClass("slick-initialized")) {
          $step.slick({
            autoplay: true,
            autoplaySpeed: 3500,
            slidesToShow: 3,
            slidesToScroll: 1,
            mobileFirst: true,
            arrows: false,
            dots: true,
          });
        }
      } else {
        if (!$step.hasClass("slick-initialized")) {
          $step.slick({
            autoplay: true,
            autoplaySpeed: 3500,
            slidesToShow: 1,
            slidesToScroll: 1,
            mobileFirst: true,
            arrows: false,
            dots: true,
          });
        }
      }
    });
  });

  $(window).on("load resize orientationchange", function () {
    $(".products-list").each(function () {
      var $products = $(this);
      if ($(window).width() > 767) {
        if ($products.hasClass("slick-initialized")) {
          $products.slick("unslick");
        }
      } else {
        if (!$products.hasClass("slick-initialized")) {
          $products.slick({
            slidesToShow: 1,
            autoplay: true,
            autoplaySpeed: 3500,
            slidesToScroll: 1,
            mobileFirst: true,
            arrows: false,
            dots: true,
          });
        }
      }
    });
  });
  $(".owl-carousel").owlCarousel({
    loop: true,
    autoplay: false,
    autoplayTimeout: 3500,
    nav:true,
    navText : ['<img src="./images/PC_index/btn-prev-slide-team.svg" alt="">','<img src="./images/PC_index/btn-next-slide-team.svg" alt="">'],
    autoplayHoverPause: true,
    responsive: {
      0: {
        items: 1,
      },
      600: {
        margin: 15,
        items: 2,
      },
      1000: {
        margin: 30,
        items: 3,
      },
    },
  });
    
});


$('.slider-nav').slick({
    slidesToShow: 4,
    slidesToScroll: 1,
    dots: false,
    arrows: true,
    prevArrow: '<img src="../../images/pc_blog/slide-arrow-back.svg" class="slick-prev" alt="">',
    nextArrow: '<img src="../../images/pc_blog/slide-arrow-next.svg" class="slick-next" alt="">',
    focusOnSelect: true,
    responsive: [
        {
            breakpoint: 1200,
            settings: {
                slidesToShow: 4,
                slidesToScroll: 1,
            },
        },
        {
            breakpoint: 1008,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            },
        },
        {
            breakpoint: 800,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
            },
        },
    ],
});
