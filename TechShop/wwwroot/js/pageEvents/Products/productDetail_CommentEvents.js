$(document).ready(function ()
{
    const review_link = $(".review-link")
    const product_tab = $("#product-tab")
    review_link.on("click", function (event) {
        event.preventDefault();
        let tab_nav_review = product_tab.find(".tab-nav li")
        tab_nav_review.removeClass("active")
        tab_nav_review.find("a").attr("aria-expanded", false)
        tab_nav_review.eq(2).addClass("active")
        tab_nav_review.eq(2).find("a").attr("aria-expanded", true)

        let tab_content = product_tab.find(".tab-content .tab-pane")
        console.log(tab_content)
        tab_content.removeClass("active").removeClass("in")
        tab_content.eq(2).addClass("active").addClass("in")

        $([document.documentElement, document.body]).animate({
            scrollTop: tab_nav_review.eq(2).offset().top
        }, 300);
    })
})