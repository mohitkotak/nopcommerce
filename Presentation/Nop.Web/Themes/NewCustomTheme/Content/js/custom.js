/*--- Use only for Custom Javascript  ---*/
$(document).ready(function() {
	
	"use strict";
		
	//To show searchBox
	$("#search_box a").on("click",function(){
		$("#search_box .search-form").slideToggle();
	});
	if ($(".jsSelect-box").length){
		$('.styled').customSelect();
	}
	$(".search-form .icon").on("click",function(){
		$("#search_box .search-form").slideToggle();
	});
	$(".step2 .check-slide .label_check").on("click",function(){
		if($(this).hasClass("c_on"))
		{
			$(".billing-address").hide();
		}
		else
		{
			$(".billing-address").show();	
		}
	});
	
	// tab menu js start 
	$(".tab-menu .tab-nav li a").on("click",function(){
		var curent_id = $(this).attr("class");
		var open_id = curent_id.split("_");
		$(".tab-menu .tab-nav li").removeClass("active");
		$(this).parent("li").addClass("active");
		$(".tab-menu .tab-content").stop(true).hide();
		$("." + open_id[0]).fadeIn();
	});

	// checkout payment opction 
	$(".save-card .card-box .title .card-name").each(function() {
        $(this).on("click",function(){
			$(".save-card .card-box").removeClass("active");
			$(this).parent(".title").parent(".card-box").addClass("active");
		});
    });
	
	// descount box
	$(".total-info .descount-box a").on("click",function(){
		$(".descount-input").slideToggle();
	});
	
	// check box and readio button js
	$('.label_check, .label_radio').on("click",function(){
		setupLabel();
	});
	setupLabel();
	
	// To shoe mobile main menu
	$(".mobile-btn").on("click",function(){
		$(".navMenu").slideToggle();	
	});
	
	// To show submenu
	$("nav .megamenu").before('<i class="fa fa-angle-down"></i>');
	$("nav ul > li .fa").on("click",function(){
		if($(this).next(".megamenu").is(":visible")){
			$(this).next(".megamenu").slideUp();
			$(this).removeClass("fa-angle-up").addClass("fa-angle-down");
		}
		else
		{
			$("nav ul > li .fa").removeClass("fa-angle-up").addClass("fa-angle-down");
			$("nav .megamenu").slideUp();
			$(this).removeClass("fa-angle-down").addClass("fa-angle-up");
			$(this).next(".megamenu").slideDown();
			
		}
	});
	
	// Range Slider 
	if ($(".rangeSlider").length) {
		$( ".rangeSlider #slider" ).slider({
			range: true,
			values: [ 17, 67 ]
		});
	}
	
	// Tooltip
    $( "#tooltip" ).tooltip();
	
	//main banner
	if ($("#banner-slider").length) {
		$('#banner-slider').owlCarousel({
			loop:true,
			margin:0,
			/*nav:true,
			navText:['',''],*/
			dots:true,
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				1000:{
					items:1
				}
			}
		});
		// To set banner 
		var window_height = $(window).height();
		$("#banner-slider .owl-item .item").css ("height",window_height + "px");
		
		$("#banner-slider .owl-item").each(function () {
			var img_src = $(this).children(".item").children("img").attr('src');
			$(this).children(".item").css("background-image", 'url(' + img_src + ')');
			$(this).children(".item").children("img").hide();
		});
	}
	//featured products slider
	if ($("#featured-products-slider").length) {
		$('#featured-products-slider').owlCarousel({
			loop:true,
			margin:30,
			nav:true,
			navText:['<img src="images/prev-slide.png" alt="prev-arrow" />','<img src="images/next-slide.png" alt="next-arrow" />'],
			responsive:{
				0:{
					items:1
				},
				768:{
					items:3
				},
				1000:{
					items:4
				}
			}
		});
	}
	
	//bestseller slider
	if ($("#bestseller-slider").length) {
		$('#bestseller-slider').owlCarousel({
			loop:true,
			margin:30,
			nav:true,
			navText:['<img src="images/prev-slide.png" alt="prev-arrow" />','<img src="images/next-slide.png" alt="next-arrow" />'],
			responsive:{
				0:{
					items:1
				},
				768:{
					items:3
				},
				1000:{
					items:4
				}
			}
		});
	}
	// product detail slider
	if ($("#sync1").length) {
		var sync1 = $("#sync1");
		var sync2 = $("#sync2");
		sync1.owlCarouselOld({
			singleItem : true,
			slideSpeed : 1000,
			navigation: false,
			pagination:false,
			afterAction : syncPosition,
			responsiveRefreshRate : 200,
		});
		
		sync2.owlCarouselOld({
			items : 3,
			itemsDesktop      : [1199,3],
			itemsTablet       : [768,3],
			itemsMobile       : [479,2],
			navigation: true,
			navigationText: ['<i class="fa fa-arrow-left"></i>','<i class="fa fa-arrow-right"></i>'],
			pagination:false,
			responsiveRefreshRate : 100,
			afterInit : function(el){
				el.find(".owl-item").eq(0).addClass("synced");
			}
		});
		
		
		
		$("#sync2").on("click", ".owl-item", function(e){
			e.preventDefault();
			var number = $(this).data("owlItem");
			sync1.trigger("owl.goTo",number);
		});
		
		
	}
	if ($(".homeAnimation").length) {
		//homeAnimation();
	}
	
	// body scroll js start
	$("#goTop").on("click",function(e) {
		$("html, body").animate({ scrollTop: 0 }, 1000);
	});
	
	// product list grid view 
	$(".customize-product .product-view li a").on("click",function() {
		var curant_id = $(this).attr("class");
		var all_list = $(".customize-product .product-view li");
		var list_li = $(".customize-product .product-view li .listItem");
		var product_listing = $(".products-list .products")
		
		if(curant_id == 'listItem')
		{
			all_list.removeClass("active");
			list_li.parent("li").addClass("active");
			product_listing.addClass("list-view");
		}
		else
		{
			all_list.addClass("active");
			list_li.parent("li").removeClass("active");
			product_listing.removeClass("list-view");
		}
	});
});
$(window).resize(function() {
	
	// banner height set 
    var window_height = $(window).height();
	$("#banner-slider .owl-stage-outer .owl-item .item").css ("height",window_height + "px"); 
	
	// To show submenu
	if($(window).width() > 767 ){
		$("nav ul > li .fa").removeClass("fa-angle-up").addClass("fa-angle-down");
	}
	else
	{
		$("nav ul > li").each(function() {
        	if($(this).children(".megamenu").is(":visible")){    
				$(this).children(".fa").removeClass("fa-angle-down").addClass("fa-angle-up");
			}
        });
	}
});

$(window).scroll(function() {
	
	if ($(".homeAnimation").length) {
		//homeAnimation();
	}
	//to set header bg
    var scroll_height = $("header").outerHeight();
	if ($(window).width() > 767){
		if($(window).scrollTop() > scroll_height)
		{
			$("header").addClass("fixHeader");
		}
		else
		{
			$("header").removeClass("fixHeader");
		}
	}
	
});

function setupLabel() {
	
	// check box and readio button js
	if ($('.label_check input').length) {
		$('.label_check').each(function(){ 
			$(this).removeClass('c_on');
		});
		$('.label_check input:checked').each(function(){ 
			$(this).parent('label').addClass('c_on');
		});                
	};
	if ($('.label_radio input').length) {
		$('.label_radio').each(function(){ 
			$(this).removeClass('r_on');
		});
		$('.label_radio input:checked').each(function(){ 
			$(this).parent('label').addClass('r_on');
		});
	};
};

function syncPosition(el){
	var current = this.currentItem;
	$("#sync2")
	.find(".owl-item")
	.removeClass("synced")
	.eq(current)
	.addClass("synced")
	if($("#sync2").data("owlCarousel") !== undefined){
		center(current)
	}
}

function center(number){
	var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
	var num = number;
	var found = false;
	for(var i in sync2visible){
		if(num === sync2visible[i]){
			var found = true;
		}
	}
	if(found===false){
		if(num>sync2visible[sync2visible.length-1]){
			sync2.trigger("owl.goTo", num - sync2visible.length+2)
		}else{
			if(num - 1 === -1){
			num = 0;
		}
		sync2.trigger("owl.goTo", num);
	}
	} else if(num === sync2visible[sync2visible.length-1]){
		sync2.trigger("owl.goTo", sync2visible[1])
	} else if(num === sync2visible[0]){
		sync2.trigger("owl.goTo", num-1)
	}
}

function homeAnimation() {

	var window_scroll_top = $(window).scrollTop();
	console.log(window_scroll_top);
	var window_height = $(window).height();
	
	
	// Featured Products animation
	var functionality_top1 = $(".featured-products").offset().top;
	functionality_top1 = functionality_top1 - (window_height / 2); 
	var featured_productBox = $(".featured-products .productBox")
	if(window_scroll_top >= functionality_top1){
		featured_productBox.addClass("visible");
	}
	else
	{
		featured_productBox.removeClass("visible");
	}
	
	// bestseller Products animation
	var functionality_top2 = $(".bestseller").offset().top;
	functionality_top2 = functionality_top2 - (window_height / 2); 
	var bestseller_productBox = $(".bestseller .productBox")
	if(window_scroll_top >= functionality_top2){
		bestseller_productBox.addClass("visible");
	}
	else
	{
		bestseller_productBox.removeClass("visible");
	}
}