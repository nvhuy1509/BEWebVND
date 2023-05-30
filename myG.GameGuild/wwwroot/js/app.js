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

const headerBar = document.querySelector(".header-bar img");
const headerFixed = document.querySelector(".header-fixed");
headerBar.addEventListener("click", () => {
    headerFixed.classList.add("show");
});

const iconCloseHeaderFixed = document.querySelector(".close-header-fixed");
iconCloseHeaderFixed.addEventListener("click", () => {
    headerFixed.classList.remove("show");
});

const header = document.querySelector("header");
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

function onActiveBtnSlick(element) {
    console.log(element)
    var btnPrev = document.getElementById("btnPrev");
    var btnNext = document.getElementById("btnNext");

    if (element.classList.contains("slick-prev")) {
        element.src = "/images/PC_blog/slide-prev-yellow.svg";
        btnNext.src = "/images/PC_blog/slide-next-black.svg";
    }
    if (element.classList.contains("slick-next")) {
        element.src = "/images/PC_blog/slide-next-yellow.svg";
        console.log(element)
        btnPrev.src = "/images/PC_blog/slide-prev-black.svg";
    }

}


function onActiveBtnCarou(element) {
    console.log(element)
    var btnPrev = document.getElementById("carouPrev");
    var btnNext = document.getElementById("carouNext");

    if (element.classList.contains("carousel-control-prev")) {
        btnPrev.classList.add("btn-slide-active");
        btnNext.classList.remove("btn-slide-active");
    }
    if (element.classList.contains("carousel-control-next")) {
        btnNext.classList.add("btn-slide-active");
        btnPrev.classList.remove("btn-slide-active");
    }

}
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
