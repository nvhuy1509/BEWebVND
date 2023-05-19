$(document).ready(function () {
  $(".menu-item-wrapper").each(function (idx, el) {
    $(this).click(function (e) {
      if (e.target.matches(".visible-s")) {
        console.log(e.target);
        e.target.classList.toggle("active");
      } else {
        e.target.querySelector(".visible-s").classList.toggle("active");
      }
      $(this).next().slideToggle(200);
    });
  });
});



const header = document.querySelector(".header-top");
const backToTop = document.querySelector(".back-to-top");
window.addEventListener("scroll", (e) => {
  const srollY = window.pageYOffset;
  const headerHeight = header.offsetHeight;
  if (srollY >= headerHeight) {
    backToTop.classList.add("show");
  } else {
    backToTop.classList.remove("show");
  }
});


const headerBar = document.querySelector(".header-bar img");
const headerFixed = document.querySelector(".header-fixed");
const menuItem = document.querySelectorAll(".menu-link");
let logo = document.getElementById("logoHeader");
let bar = document.getElementById("imgBarHeader");

headerBar.addEventListener("click", () => {
  headerFixed.classList.add("show");
  menuItem.forEach((element) => {
    element.classList.add("menu-itemShow");
  });
  logo.style.display = "none";
  bar.style.display = "none";
  header.style.background = "transparent"
 
});

const iconCloseHeaderFixed = document.querySelector(".close-header-fixed");
iconCloseHeaderFixed.addEventListener("click", () => {
  headerFixed.classList.remove("show");
  menuItem.forEach((element) => {
    element.classList.remove("menu-itemShow");
  });
  logo.style.display = "block";
  bar.style.display = "block";
  header.style.background = "#000000"
  
});


const roadmapOverlay = document.querySelectorAll(".roadmap-overlay");
roadmapOverlay.forEach((item) =>
  item.addEventListener("click", (e) => {
    e.target.nextElementSibling.nextElementSibling.nextElementSibling.classList.toggle(
      "active"
    );
  })
);

const roadmapVerticalOverLay = document.querySelectorAll(
  ".roadmap-vertical-overlay"
);
roadmapVerticalOverLay.forEach((item) =>
  item.addEventListener("click", (e) => {
    e.target.parentNode.nextElementSibling.classList.toggle("show");
  })
);

const dropdowns = document.querySelectorAll(".faqs-dropdown");

dropdowns.forEach((el) => {
  const button = el.querySelector(".faqs-dropdown-btn");

  button.addEventListener("click", () => {
    // Close all
    [...dropdowns]
      .filter((x) => x != el)
      .forEach((el) => el.classList.remove("is-open"));
    // Toggle one
    el.classList.toggle("is-open");
  });
});

//number animation
// function animateValue(obj, start, end, duration) {
//   let startTimestamp = null;
//   const step = (timestamp) => {
//     if (!startTimestamp) startTimestamp = timestamp;
//     const progress = Math.min((timestamp - startTimestamp) / duration, 1);
//     obj.innerHTML = Math.floor(progress * (end - start) + start);
//     if (progress < 1) {
//       window.requestAnimationFrame(step);
//     }
//   };
//   window.requestAnimationFrame(step);
// }

const obj1 = document.getElementById("value-1");
const obj2 = document.getElementById("value-2");
const obj3 = document.getElementById("value-3");


//scroll-view-display

function reveal() {
  var reveals = document.querySelectorAll(".reveal");

  for (var i = 0; i < reveals.length; i++) {
    var windowHeight = window.innerHeight;
    var elementTop = reveals[i].getBoundingClientRect().top;
    var elementVisible = 150;

    if (elementTop < windowHeight - elementVisible) {
      reveals[i].classList.add("active");
      // window.addEventListener("scroll", animateValue(obj1, 0, 125, 2000));
      // window.addEventListener("scroll", animateValue(obj2, 0, 20, 1000));
      // window.addEventListener("scroll", animateValue(obj3, 0, 20, 1000));
    } else {
      reveals[i].classList.remove("active");
    }
  }
}

window.addEventListener("scroll", reveal);

//change language
function changeLanguage() {
  var x = document.getElementById("language");
  if (x.innerHTML === "EN") {
    x.innerHTML = "VN";
  } else {
    x.innerHTML = "EN";
  }
}

// page solution

$(document).ready(function(){
  var itemBanners = $('.banner-item');
  var itemThumbs = $('.thumb-item');
  console.log($(body).height());

  itemThumbs.each(function(index){
      $(this).on('click', function(){
          itemThumbs.removeClass('active');
          $(this).addClass('active');
          itemBanners.removeClass('active');
          $(itemBanners[index]).addClass('active');
      })
  })
})