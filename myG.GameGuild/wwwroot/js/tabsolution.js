
const scrollContainer = document.querySelector(".thumb-list");
console.log(scrollContainer.scrollWidth);

scrollContainer.addEventListener("wheel", (evt) => {
    evt.preventDefault();
    scrollContainer.scrollLeft += evt.deltaY;
    if(scrollContainer.scrollLeft > 0){
        scrollContainer.classList.add("scrolling");
    }else{
        scrollContainer.classList.remove("scrolling");
    }
});

$(document).ready(function(){
    var itemBanners = $('.banner-item');
    var itemThumbs = $('.thumb-item');
    var tabSidebar = $('.tab-item');
    var currIdx = 0;
    var preIdx = 0;
    var widthThumbSmall = $(itemThumbs[1]).width();
    var scrollLeft = 0;

    itemThumbs.each(function(index){
        $(this).on('click', function(){
            itemThumbs.removeClass('active');
            $(this).addClass('active');
            itemBanners.removeClass('active');
            $(itemBanners[index]).addClass('active');
            tabSidebar.removeClass('active');
            $(tabSidebar[index]).addClass('active');
            if(index === 0){
                $('.thumb-list').removeClass('scrolling');
            }else if(index === itemThumbs.length - 1){
                $('.thumb-list').addClass('scrolling');
            }

            currIdx = index;
            var newScrollLeft = widthThumbSmall*(currIdx - preIdx);
            if(preIdx < currIdx){
                if(index < itemThumbs.length - 1){
                    scrollLeft += newScrollLeft;
                }
                $('.thumb-list').animate({scrollLeft: scrollLeft}, "slow");
            }else if(preIdx > currIdx){
            var newScrollRight = widthThumbSmall*(-currIdx + preIdx);
                scrollLeft -= newScrollRight;
                if(scrollLeft < 0){
                    scrollLeft = 0;
                }
                // console.log(scrollLeft);
                $('.thumb-list').animate({scrollLeft: scrollLeft}, "slow");
            }
            preIdx = currIdx;
        })
    })

    tabSidebar.each(function(index){
        $(this).on('click', function(){
            tabSidebar.removeClass('active');
            $(this).addClass('active');
            itemBanners.removeClass('active');
            $(itemBanners[index]).addClass('active');
            itemThumbs.removeClass('active');
            $(itemThumbs[index]).addClass('active');
        })
    })

    if($(window).width() < 600 && itemBanners.length > 1){
        $('.banner-list').slick({
                items: 1,
                dots: true,
                autoplay: false,
                autoplayTimeout: 3500,
                nav:true,
                arrows: false,
            }
        );

        var itemDot = $('.banner-list .slick-dots li button');
        tabSidebar.each(function(index){
            var text = $(this).text();
            $(itemDot[index]).html(`<span>${text}</span>`);
        })
    }  
});

