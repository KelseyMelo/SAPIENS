;(function($, window, document, undefined) {
	"use strict";
	/*===============*/
	/* COMMON VARS */
	/*==============*/
	var $window = $( window );
	function setWindowWidth() {
		$windowWidth = $( window ).width();
		return $windowWidth;
	}
	var $windowWidth = setWindowWidth();

	// At least Safari 3+: "[object HTMLElementConstructor]"
	var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
	var isChrome = !!window.chrome && !!window.chrome.webstore;
	var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0 || !isChrome && !isOpera && window.webkitAudioContext !== undefined;

	var standalone = window.navigator.standalone,
	userAgent = window.navigator.userAgent.toLowerCase(),
	safari = /safari/.test( userAgent ),
	ios = /iphone|ipod|ipad/.test( userAgent );

	if( ios ||  isSafari) {
		$('.team-person').matchHeight();
		$('.tile').matchHeight();
	}

	/*------------------*/
	/* BACKGROUND IMAGE */
	/*------------------*/
	function addImgBg( img_sel, parent_sel){

		if (!img_sel) {
			console.info('no img selector');
			return false;
		}

		var $parent, _this;

		$(img_sel).each(function(){
			_this = $(this);
			$parent = _this.closest( parent_sel );
			$parent = $parent.length ? $parent : _this.parent().addClass('s-back-switch');
			$parent.css( 'background-image' , 'url(' + this.src + ')' );
			_this.hide()
		});
	}

	addImgBg('.s-img-switch', '.s-back-switch');

	/*---------------*/
	/* SWIPER SLIDER */
	/*---------------*/
	var swipers = {};
	var attrsToSize = {
		'data-md-slides' : '1023',
		'data-sm-slides' : '768',
		'data-xs-slides' : '599'
	};

	function parseSlidesAttrValue(value) {
		var parts = value.split(',');
		return {
			slidesPerView: parseInt(parts[0],10),
			spaceBetween: parseInt(parts[1],10)
		}
	}

	function createBreakpoints(container, attrsToSize) {
		var breakpointsObj = {};
		$.each(attrsToSize, function(key ,value) {
			if (container.attr(key)) {
				breakpointsObj[value] = parseSlidesAttrValue(container.attr(key));
			}
		});

		return breakpointsObj;
	}

	$('.swiper-container').each(function(index){
		var $t = $(this);
		var sliderIndex = 'swiper-unique-id-'+ index;
		$t.addClass(sliderIndex + ' initialized').attr('id', sliderIndex);
		$t.find('.swiper-pagination').addClass('pagination-'+ sliderIndex);

		var autoPlayVar = $t.attr('data-autoplay');
		var mode = $t.attr('data-mode');

		var loopVar = $t.attr('data-loop');
		var speedVar = parseInt($t.attr('data-speed'),10);
		var centerVar = $t.attr('data-center');
		var spaceBetweenVar = parseInt($t.attr('data-space-between'),10);
		var slideEffect = $t.attr('data-slide-effect');
		//console.log(centerVar);


		var slidesPerViewVar = parseInt($t.attr('data-slides-per-view'),10);
		if (isNaN(slidesPerViewVar)) {
			slidesPerViewVar = 'auto';
		}

		swipers[sliderIndex] = new Swiper('.' + sliderIndex,{
			loop: loopVar || false,
			autoplay: autoPlayVar || false,
			autoplayDisableOnInteraction: false,
			speed: speedVar || 300,
			slidesPerView: slidesPerViewVar || 1,
			spaceBetween: spaceBetweenVar,
			pagination: '.pagination-' + sliderIndex,
			paginationClickable: true,
			centeredSlides: centerVar || false,
			mode: mode || 'horizontal',
			grabCursor: true,
			keyboardControl: true,
			breakpoints: createBreakpoints($t, attrsToSize),
			setWrapperSize: true,
			effect: slideEffect || 'slide',
			fade: {
				crossFade: true
			}
		});
	});

	/* Toggle menu search form */
	$(".btn-search").on('click', function(event){
		var $searchForm = $(this).parent().parent().find(".menu-search-col");
		var ANIMATION_DURATION = 400;

		if ($searchForm.hasClass('menu-search-col--active')) {
			$searchForm.fadeToggle(ANIMATION_DURATION);

			setTimeout(function () {
				$searchForm.removeClass('menu-search-col--active');
			}, ANIMATION_DURATION);

		} else {
			$searchForm.fadeToggle(ANIMATION_DURATION).addClass('menu-search-col--active');
		}
		event.stopPropagation();
	});

	/* Close search form when click outside it */
	$("body").on('click', function(){
		var $searchForm = $(".btn-search").parent().parent().find(".menu-search-col");

		if ($searchForm.hasClass('menu-search-col--active')) {
			$searchForm.fadeToggle(400);

			setTimeout(function () {
				$searchForm.removeClass('menu-search-col--active');
			}, 400);
		}
	});

	$(".menu-search-col .search-form").on('click', function(event){
		event.stopPropagation();
	});

	/*-----------*/
	/* ACCORDION */
	/*-----------*/
	var accordRemoveClass = function( el, _class ){
		if (el.classList)
			el.classList.remove( _class ? _class : 'active' );
		else
			el.className = panel.className.replace(new RegExp('(^|\\b)' + className.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');
	};
	$('.accordion').on('click', '.accordion-title', function(){

		var panel_parent = this.parentNode,
		panel_container = panel_parent.parentNode,
		panels_wrap = panel_container.querySelectorAll('.accordion-item');

		Array.prototype.forEach.call(panels_wrap, function(panel, i){
			if(panel !== panel_parent) {
				accordRemoveClass(panel);
			}
		});

		if ( -1 !== this.parentNode.className.indexOf( 'active' ) ) {
			accordRemoveClass(panel_parent);
		} else {
			panel_parent.className += ' active';
		}
	});

	/*------*/
	/* TABS */
	/*------*/
	$('.tabs-header').on('click', 'li', function(e) {
		e.preventDefault();
		if (!$(this).hasClass('active')) {
			var index_el = $(this).index();
			$(this).addClass('active').siblings().removeClass('active');
			$(this).closest('.tabs').find('.tabs-item').removeClass('active').eq(index_el).addClass('active');
		}
	});

	/*--------------*/
	/* CONTACT FORM */
	/*--------------*/
	$('.js-contact-form').submit(function(e){

		$('.ajax-loader').show();

		var url = 'mail.php',
		form = this;

		$(form).find('[name="fields[code]"]').remove();

		function result(class_key, data){
			setTimeout(function(){
				$('.ajax-loader').hide();
				$('.ajax-result').find(class_key).show().text(data);
			},500);
		}

		$.ajax({
			type: "POST",
			url: url,
			data: $(form).serialize(),
		})
		.done(function(data) {
			result('.success', data);
		}).error(function(data){
			result('.error', data);
		});

		e.preventDefault();

	});

	/*-----------*/
	/* MAIN MENU */
	/*-----------*/
	var $stickyElement = $('.js-sticky');

	$('.nav-menu-icon').click(function(e) {
		$(this).toggleClass('active');
		$('.main-navigation').toggleClass('active');
		$stickyElement.toggleClass('sticky-menu').toggleClass('sticky-menu--active');
		$('body').toggleClass('no-scroll');
	});

	// sticky menu
	var stickyTopOffset = $stickyElement.offset().top;
	$stickyElement.before('<div class="sticky-plug"></div>');
	var stickyHeight = $stickyElement.height();
	//var $stickyContainer = $('.sticky-container').height(stickyHeight + 1);

	$window.on('resize', function() {
		stickyHeight = $stickyElement.height();
		//$stickyContainer = $('.sticky-container').height(stickyHeight + 1);
		setWindowWidth();
	});

	$window.on('scroll', function() {
		if ($(this).scrollTop() >= stickyTopOffset) {
			$stickyElement.addClass('sticky-menu');
		} else {
			$stickyElement.removeClass('sticky-menu');
		}
	});

	// add button to open submenu

	$('.menu-item-has-children > a').append('<span class="icon-left-open-3 toggle-menu-item"></span>');

	$('.toggle-menu-item').on('click', function(e) {
		e.preventDefault();
		var $menuItem = $(this).closest('.menu-item-has-children');

		if ($menuItem.hasClass('menu-item--active')) {
			$menuItem.removeClass('menu-item--active').children('.sub-menu, .mega-sub-menu').slideUp();
		} else {
			$menuItem.addClass('menu-item--active').children('.sub-menu, .mega-sub-menu').slideDown();
		}
	});

	$(".menu-item").hover(
		function() {
			if ($windowWidth >= 1024) {
				clearTimeout($.data(this, 'timer'));
				$(this).children('.sub-menu, .mega-sub-menu').stop(true, true).slideDown(200);
			}
		},
		function(){
			if ($windowWidth >= 1024) {
				$.data(this, 'timer', setTimeout($.proxy(function() {
					$(this).children('.sub-menu, .mega-sub-menu').stop(true, true).slideUp(200);
				},this), 200));
			}
		}
		);

	/*-------------------*/
	/* ISOTOPE PORTFOLIO */
	/*-------------------*/
	var $grid = $('.isotope-container').isotope({
		itemSelector: '.gallery-item',
		percentPosition: true,
		masonry: {
			columnWidth: '.grid-sizer',
			gutter: '.gutter-sizer'
		}
	});

	$grid.imagesLoaded().progress( function() {
		$grid.isotope('layout');
	});

	$('.filter-wrap').on( 'click', 'button', function() {
		$(this).siblings().removeClass('active');
		$(this).addClass('active');
		var $thisIsotope = $(this).parent().siblings('.isotope-container');
		var filterValue = $(this).attr('data-filter');
		$thisIsotope.isotope({ filter: filterValue });
	});

	/*--------------*/
	/* VIDEO BANNER */
	/*--------------*/
	$('.video-banner').each(function(){
		var videoWrap = $(this),
		videoPopUp = videoWrap.find('.video-popup'),
		buttonPlay = videoWrap.find('.play-btn'),
		videoIframe = videoPopUp.find('iframe'),
		iframeSrc = videoIframe.attr('src'),
		iframeDataSrc = videoIframe.attr('data-src'),
		closePlayButton = videoPopUp.find('.close-btn');

		buttonPlay.on('click', function(e){
			e.preventDefault();
			videoPopUp.addClass('active');
			videoIframe.attr('src', iframeDataSrc);
		});

		closePlayButton.on('click', function(){
			videoPopUp.removeClass('active');
			videoIframe.attr('src', iframeSrc);
		});
	});

	/*------------*/
	/* GOOGLE MAP */
	/*------------*/

	(function () {

		function initMap(element, addresses) {

			var infowindow, currAddress, coords = [], markers = [];
			var grayscaleStyle = [
				{
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#f5f5f5"
						}
					]
				},
				{
					"elementType": "labels.icon",
					"stylers": [
						{
							"visibility": "off"
						}
					]
				},
				{
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#616161"
						}
					]
				},
				{
					"elementType": "labels.text.stroke",
					"stylers": [
						{
							"color": "#f5f5f5"
						}
					]
				},
				{
					"featureType": "administrative.country",
					"elementType": "geometry.fill",
					"stylers": [
						{
							"color": "#f5f5f5"
						},
						{
							"visibility": "on"
						}
					]
				},
				{
					"featureType": "administrative.country",
					"elementType": "geometry.stroke",
					"stylers": [
						{
							"color": "#d5d5d5"
						},
						{
							"visibility": "on"
						}
					]
				},
				{
					"featureType": "administrative.land_parcel",
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#bdbdbd"
						}
					]
				},
				{
					"featureType": "landscape",
					"stylers": [
						{
							"visibility": "off"
						}
					]
				},
				{
					"featureType": "poi",
					"stylers": [
						{
							"visibility": "off"
						}
					]
				},
				{
					"featureType": "poi",
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#eeeeee"
						}
					]
				},
				{
					"featureType": "poi",
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#757575"
						}
					]
				},
				{
					"featureType": "poi.park",
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#f5f5f5"
						}
					]
				},
				{
					"featureType": "poi.park",
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#9e9e9e"
						}
					]
				},
				{
					"featureType": "road",
					"stylers": [
						{
							"visibility": "off"
						}
					]
				},
				{
					"featureType": "road",
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#ffffff"
						}
					]
				},
				{
					"featureType": "road.arterial",
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#757575"
						}
					]
				},
				{
					"featureType": "road.highway",
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#dadada"
						}
					]
				},
				{
					"featureType": "road.highway",
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#616161"
						}
					]
				},
				{
					"featureType": "road.local",
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#9e9e9e"
						}
					]
				},
				{
					"featureType": "transit.line",
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#f5f5f5"
						}
					]
				},
				{
					"featureType": "transit.station",
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#eeeeee"
						}
					]
				},
				{
					"featureType": "water",
					"stylers": [
						{
							"color": "#ffffff"
						},
						{
							"visibility": "on"
						}
					]
				},
				{
					"featureType": "water",
					"elementType": "geometry",
					"stylers": [
						{
							"color": "#ffffff"
						},
						{
							"visibility": "on"
						}
					]
				},
				{
					"featureType": "water",
					"elementType": "labels.text.fill",
					"stylers": [
						{
							"color": "#9e9e9e"
						}
					]
				}
			];
			var mapZoom = locations[0].zoom || 12;
			var bullet = window.location.protocol + '//' + window.location.host + window.location.pathname + '/../images/bullet.png';
			var latlng = new google.maps.LatLng(52.40, -3.61);
			var geocoder = new google.maps.Geocoder();
			var bounds = new google.maps.LatLngBounds(); // Creating a LatLngBounds object
			var mapOptions = {
				zoom: mapZoom,
				center: latlng,
				zoomControl: true,
				draggable: true,
				scrollwheel: false,
				styles: locations[0].style ? grayscaleStyle : false
			};

			// Creating the map
			var map = new google.maps.Map(document.getElementById(element), mapOptions);

			for(var i = 0; i < addresses.length; i++) {
				currAddress = addresses[i].address;

				//console.log(currAddress);

				geocoder.geocode({'address':currAddress}, function (results, status) {
					if (status == google.maps.GeocoderStatus.OK) {
						var addr = results[0].formatted_address;
						coords = results[0].geometry.location;
						var marker = new google.maps.Marker({
							position: coords,
							icon: geoAddress[0].defaultMarker || bullet,
							map: map,
							title: addr
						});
						markers.push(marker);
						google.maps.event.addListener(marker, 'click', function() {
							if (!infowindow) {
								infowindow = new google.maps.InfoWindow();
							}

							// Setting the content of the InfoWindow
							infowindow.setContent(addr);

							// Tying the InfoWindow to the marker
							infowindow.open(map, marker);
						});

						// Extending the bounds object with all coordinates
						bounds.extend(coords);

						// Adjusting the map to new bounding box
						map.fitBounds(bounds);

						if(locations.length == 1) {
							map.setZoom(mapZoom);
						}
					}
					else {
						throw('No results found: ' + status);
					}
				});
			}
		}

		if ($("div").is("#js-map")) {

			var locations = [];
			locations = geoAddress;

			initMap('js-map', locations);

		}
	}());



	$( ".gallery-container" ).last().addClass( "last-child" );

	/*---------------*/
	/* POPUP GALLERY */
	/*---------------*/
	$('.isotope-container').each(function() { // the containers for all your galleries
		$(this).magnificPopup({
			delegate: '.gallery-item a',
			type: 'image',
			gallery: {
				enabled: true
			},
			mainClass: 'mfp-fade',
			fixedContentPos: false
		});
	});


})(jQuery, window, document);
