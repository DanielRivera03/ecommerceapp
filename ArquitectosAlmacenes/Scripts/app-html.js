/*-----------------------------------------------------------------------------------
 Theme Name: Foxic eCommerce Template
 Author: BigSteps
 Author URI: http://themeforest.net/user/bigsteps
 -----------------------------------------------------------------------------------*/
"use strict";

window.THEME = {};

// Youtube API
function onYouTubeIframeAPIReady() {
	THEME.Video.loadVideos();
}

(function ($) {
	var calcScrollWidth = function calcScrollWidth() {
		var html = $('<div style="width:100px;height:100px;overflow:scroll;visibility: hidden;"><div style="height:200px;"></div>');
		$body.append(html);
		var w = html[0].offsetWidth - html[0].clientWidth;
		$(html).remove();
		return w;
	};
	var setVH = function setVH() {
		var vh = window.innerHeight * 0.01;
		$('html').css('--vh', vh + 'px');
		$('html').css('--scrollW', scrollWidth + 'px');
	};
	var random = function random(min, max) {
		return Math.floor(Math.random() * (max - min + 1) + min);
	};
	THEME.Video = function () {
		var mobileBreikpoint = 575;
		var iosMobileAutoplay = true;
		var autoplayCheckComplete = false;
		var playOnClickChecked = false;
		var playOnClick = false;
		var youtubeLoaded = false;
		var videos = {};
		var videoPlayers = [];
		var videoOptions = {
			ratio: 16 / 9,
			scrollAnimationDuration: 400,
			playerVars: {
				iv_load_policy: 3,
				modestbranding: 1,
				autoplay: 0,
				controls: 0,
				wmode: 'opaque',
				branding: 0,
				autohide: 0,
				rel: 0,
				playsinline: 1
			},
			events: {
				onReady: onPlayerReady,
				onStateChange: onPlayerChange
			}
		};
		var classes = {
			playing: 'video-is-playing',
			paused: 'video-is-paused',
			loading: 'video-is-loading',
			loaded: 'video-is-loaded',
			backgroundVideoWrapper: 'video-background-wrapper',
			videoWithImage: 'video--image_with_play',
			backgroundVideo: 'video--background',
			userPaused: 'is-paused',
			supportsAutoplay: 'autoplay',
			supportsNoAutoplay: 'no-autoplay',
			wrapperMinHeight: 'video-section-wrapper--min-height'
		};

		var selectors = {
			section: '.video-section',
			videoWrapper: '.video-section-wrapper',
			playVideoBtn: '.video-control__play',
			closeVideoBtn: '.video-control__close-wrapper',
			pauseVideoBtn: '.video-control__pause'
		};

		function keepScale(slider) {
			var $bnsliderNoScale = slider;
			var wW = $(window).width();
			if ($bnsliderNoScale.hasClass("keep-scale") && !$bnsliderNoScale.hasClass("bnslider--fullheight")) {
				$bnsliderNoScale.css({
					'height': '',
					'min-height': ''
				});
				var bnrH;
				if (wW <= mobileBreikpoint && parseInt($bnsliderNoScale.attr('data-start-mwidth')) > 0 && parseInt($bnsliderNoScale.attr('data-start-mheight')) > 0) {
					var bnrH = parseInt($bnsliderNoScale.attr('data-start-mheight'), 10) / parseInt($bnsliderNoScale.attr('data-start-mwidth'), 10) * wW;
					$bnsliderNoScale.css({
						'height': bnrH + 'px',
						'min-height': bnrH + 'px'
					});
				} else if (parseInt($bnsliderNoScale.attr('data-start-width')) > 0 && parseInt($bnsliderNoScale.attr('data-start-height')) > 0) {
					var bnrH = parseInt($bnsliderNoScale.attr('data-start-height'), 10) / parseInt($bnsliderNoScale.attr('data-start-width'), 10) * wW;
					if ($bnsliderNoScale.closest("div[class^='col-'],div[class*=' col-']").length) {
						var colW = $bnsliderNoScale.closest("div[class^='col-'],div[class*=' col-']").width() * 100 / wW;
						bnrH = bnrH * colW / 100;
					} else if ($bnsliderNoScale.closest(".holder.boxed").length) {
						var colW = $bnsliderNoScale.closest(".container").width() * 100 / wW;
						bnrH = bnrH * colW / 100;
					}
					$bnsliderNoScale.css({
						'height': bnrH + 'px',
						'min-height': bnrH + 'px'
					});
				} else return false;
			}
		}

		function init($video) {
			if (!$video.length) {
				return;
			}
			$video.closest(selectors.videoWrapper).find('.bnslider-text').each(function () {
				var $this = $(this),
				    thisInlineStyle = '';
				var thisStyle = $this.data();
				for (var data in thisStyle) {
					if (data == 'fontcolor') {
						thisInlineStyle += 'color:' + $this.data('fontcolor') + ';';
					}
					if (data == 'fontfamily') {
						thisInlineStyle += 'font-family:' + $this.data('fontfamily') + ';';
					}
					if (data == 'fontsize') {
						thisInlineStyle += 'font-size:' + $this.data('fontsize') + 'px;';
					}
					if (data == 'fontline') {
						thisInlineStyle += 'line-height:' + $this.data('fontline') + 'em;';
					}
					if (data == 'fontweight') {
						thisInlineStyle += 'font-weight:' + $this.data('fontweight') + ';';
					}
					if (data == 'bgcolor') {
						thisInlineStyle += 'background-color:' + $this.data('bgcolor') + ';';
					}
					if (data == 'otherstyle') {
						thisInlineStyle += $this.data("otherstyle") + ';';
					}
				}
				if (thisInlineStyle.length > 0) {
					$this.attr('style', thisInlineStyle).addClass('data-ini');
				}
			});

			$video.closest(selectors.videoWrapper).each(function (index, el) {
				keepScale($(el));
			});

			videos[$video.attr('id')] = {
				id: $video.attr('id'),
				videoId: $video.data('id'),
				type: $video.data('type'),
				status: $video.data('type') === 'image_with_play' ? 'closed' : 'background',
				$video: $video,
				$videoWrapper: $video.closest(selectors.videoWrapper),
				$section: $video.closest(selectors.section),
				controls: $video.data('type') === 'background' ? 0 : 1
			};

			if (!youtubeLoaded) {
				var tag = document.createElement('script');
				tag.src = 'https://www.youtube.com/iframe_api';
				var firstScriptTag = document.getElementsByTagName('script')[0];
				firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
			}

			playOnClickCheck();
		}

		function customPlayVideo(playerId) {
			if (!playOnClickChecked && !playOnClick) {
				return;
			}
			if (playerId && typeof videoPlayers[playerId].playVideo === 'function') {
				privatePlayVideo(playerId);
			}
		}

		function pauseVideo(playerId) {
			if (videoPlayers[playerId] && typeof videoPlayers[playerId].pauseVideo === 'function') {
				videoPlayers[playerId].pauseVideo();
			}
		}

		function loadVideos() {
			for (var key in videos) {
				if (videos.hasOwnProperty(key)) {
					createPlayer(key);
				}
			}

			initEvents();
			youtubeLoaded = true;
		}

		function editorLoadVideo(key) {
			if (!youtubeLoaded) {
				return;
			}
			createPlayer(key);

			initEvents();
		}

		function privatePlayVideo(id, clicked) {
			var videoData = videos[id];
			var player = videoPlayers[id];
			var $videoWrapper = videoData.$videoWrapper;

			if (playOnClick) {
				setAsPlaying(videoData);
			} else if (clicked || autoplayCheckComplete) {
				$videoWrapper.removeClass(classes.loading);
				setAsPlaying(videoData);
				player.playVideo();
				return;
			} else {
				player.playVideo();
			}
		}

		function setAutoplaySupport(supported) {
			var supportClass = supported ? classes.supportsAutoplay : classes.supportsNoAutoplay;
			$(document.documentElement).removeClass(classes.supportsAutoplay).removeClass(classes.supportsNoAutoplay).addClass(supportClass);

			if (!supported) {
				playOnClick = true;
			}

			autoplayCheckComplete = true;
		}

		function playOnClickCheck() {
			if (playOnClickChecked) {
				return;
			}
			if (isMobile()) {
				playOnClick = true;
			}
			if (playOnClick) {
				setAutoplaySupport(false);
			}
			playOnClickChecked = true;
		}

		function onPlayerReady(evt) {
			evt.target.setPlaybackQuality('hd1080');
			var videoData = getVideoOptions(evt);
			playOnClickCheck();
			$('#' + videoData.id).attr('tabindex', '-1');
			sizeBackgroundVideos();
			if ($('#' + videoData.id).attr('data-mute') == 'true') {
				evt.target.mute();
			}
			if (videoData.type === 'background') {
				privatePlayVideo(videoData.id);
			}
			videoData.$videoWrapper.addClass(classes.loaded);
		}

		function onPlayerChange(evt) {
			var videoData = getVideoOptions(evt);
			videoData.$videoWrapper.find('.video__loader').addClass('d-none');
			if (videoData.status === 'background' && !isMobile() && !autoplayCheckComplete && (evt.data === YT.PlayerState.PLAYING || evt.data === YT.PlayerState.BUFFERING)) {
				setAutoplaySupport(true);
				autoplayCheckComplete = true;
				videoData.$videoWrapper.removeClass(classes.loading);
			}
			switch (evt.data) {
				case YT.PlayerState.ENDED:
					setAsFinished(videoData);
					break;
				case YT.PlayerState.PAUSED:
					setTimeout(function () {
						if (evt.target.getPlayerState() === YT.PlayerState.PAUSED) {
							setAsPaused(videoData);
						}
					}, 200);
					break;
			}
		}

		function setAsFinished(videoData) {
			switch (videoData.type) {
				case 'background':
					videoPlayers[videoData.id].seekTo(0);
					break;
				case 'image_with_play':
					closeVideo(videoData.id);
					toggleExpandVideo(videoData.id, false);
					break;
			}
		}

		function setAsPlaying(videoData) {
			var $videoWrapper = videoData.$videoWrapper;
			var $pauseButton = $videoWrapper.find(selectors.pauseVideoBtn);

			$videoWrapper.removeClass(classes.loading);

			if ($pauseButton.hasClass(classes.userPaused)) {
				$pauseButton.removeClass(classes.userPaused);
			}

			if (videoData.status === 'background') {
				return;
			}

			$('#' + videoData.id).attr('tabindex', '0');

			if (videoData.type === 'image_with_play') {
				$videoWrapper.removeClass(classes.paused).addClass(classes.playing);
			}

			setTimeout(function () {
				$videoWrapper.find(selectors.closeVideoBtn).focus();
			}, videoOptions.scrollAnimationDuration);
		}

		function setAsPaused(videoData) {
			var $videoWrapper = videoData.$videoWrapper;

			if (videoData.type === 'image_with_play') {
				if (videoData.status === 'closed') {
					$videoWrapper.removeClass(classes.paused);
				} else {
					$videoWrapper.addClass(classes.paused);
				}
			}

			$videoWrapper.removeClass(classes.playing);
		}

		function closeVideo(playerId) {
			var videoData = videos[playerId];
			var $videoWrapper = videoData.$videoWrapper;
			var classesToRemove = [classes.paused, classes.playing].join(' ');

			if (isMobile()) {
				$videoWrapper.removeAttr('style');
			}

			$('#' + videoData.id).attr('tabindex', '-1');

			videoData.status = 'closed';

			switch (videoData.type) {
				case 'image_with_play':
					videoPlayers[playerId].stopVideo();
					setAsPaused(videoData);
					break;
				case 'background':
					videoPlayers[playerId].mute();
					setBackgroundVideo(playerId);
					break;
			}

			$videoWrapper.removeClass(classesToRemove);
		}

		function getVideoOptions(evt) {
			var id = evt.target.getIframe().id;
			return videos[id];
		}

		function toggleExpandVideo(playerId, expand) {
			var video = videos[playerId];
			var elementTop = video.$videoWrapper.offset().top;
			var $playButton = video.$videoWrapper.find(selectors.playVideoBtn);
			var offset = 0;
			var newHeight = 0;

			if (isMobile()) {
				video.$videoWrapper.parent().toggleClass('page-width', !expand);
			}

			if (expand) {
				if (isMobile()) {
					newHeight = $(window).width() / videoOptions.ratio;
				} else {
					newHeight = video.$videoWrapper.width() / videoOptions.ratio;
				}
				offset = ($(window).height() - newHeight) / 2;

				video.$videoWrapper.removeClass(classes.wrapperMinHeight).animate({ height: newHeight }, 600);

				if (!(isMobile() && Shopify.designMode)) {
					$('html, body').animate({
						scrollTop: elementTop - offset
					}, videoOptions.scrollAnimationDuration);
				}
			} else {
				if (isMobile()) {
					newHeight = video.$videoWrapper.data('mobile-height');
				} else {
					newHeight = video.$videoWrapper.data('desktop-height');
				}

				video.$videoWrapper.height(video.$videoWrapper.width() / videoOptions.ratio).animate({ height: newHeight }, 600);
				setTimeout(function () {
					video.$videoWrapper.addClass(classes.wrapperMinHeight);
				}, 600);
				$playButton.focus();
			}
		}

		function togglePause(playerId) {
			var $pauseButton = videos[playerId].$videoWrapper.find(selectors.pauseVideoBtn);
			var paused = $pauseButton.hasClass(classes.userPaused);
			if (paused) {
				$pauseButton.removeClass(classes.userPaused);
				customPlayVideo(playerId);
			} else {
				$pauseButton.addClass(classes.userPaused);
				pauseVideo(playerId);
			}
			$pauseButton.attr('aria-pressed', !paused);
		}

		function startVideoOnClick(playerId) {
			var video = videos[playerId];
			video.$videoWrapper.addClass(classes.loading);
			video.status = 'open';
			switch (video.type) {
				case 'image_with_play':
					privatePlayVideo(playerId, true);
					break;
				case 'background':
					unsetBackgroundVideo(playerId, video);
					videoPlayers[playerId].unMute();
					privatePlayVideo(playerId, true);
					break;
			}
		}

		function sizeBackgroundVideos() {
			$('.video-section-wrapper').each(function (index, el) {
				keepScale($(el));
			});
			$('[data-type="background"]').each(function (index, el) {
				sizeBackgroundVideo($(el));
			});
		}

		function sizeBackgroundVideo($videoPlayer) {
			if (!youtubeLoaded) {
				return;
			}
			if (isMobile()) {
				$videoPlayer.removeAttr('style');
			} else {
				var $videoWrapper = $videoPlayer.closest(selectors.videoWrapper);
				var videoWidth = $videoWrapper.width();
				var playerWidth = $videoPlayer.width();
				var desktopHeight = $videoWrapper.height();
				var desktopHeightMax = Math.ceil(videoWidth / videoOptions.ratio);
				if (videoWidth / videoOptions.ratio < desktopHeight) {
					playerWidth = Math.ceil(desktopHeight * videoOptions.ratio);
					$videoPlayer.width(playerWidth).height(desktopHeight).css({
						left: (videoWidth - playerWidth) / 2,
						top: 0
					});
				} else {
					$videoPlayer.width(videoWidth).height(desktopHeightMax).css({
						left: 0,
						top: (desktopHeight - desktopHeightMax) / 2
					});
				}

				$videoPlayer.prepareTransition();
				$videoWrapper.addClass(classes.loaded);
			}
		}

		function unsetBackgroundVideo(playerId) {
			$('#' + playerId).removeClass(classes.backgroundVideo).addClass(classes.videoWithImage);

			videos[playerId].$videoWrapper.removeClass(classes.backgroundVideoWrapper).addClass(classes.playing);

			videos[playerId].status = 'open';
		}

		function setBackgroundVideo(playerId) {
			$('#' + playerId).removeClass(classes.videoWithImage).addClass(classes.backgroundVideo);

			videos[playerId].$videoWrapper.addClass(classes.backgroundVideoWrapper);

			videos[playerId].status = 'background';
			sizeBackgroundVideo($('#' + playerId));
		}

		function isMobile() {
			var iOS = !!navigator.platform && /iPad|iPhone|iPod/.test(navigator.platform);
			return iOS;
		}

		function resizeVideo() {
			if (!youtubeLoaded) return;
			var key;
			var fullscreen = window.innerHeight === screen.height;

			sizeBackgroundVideos();

			if (isMobile() && !iosMobileAutoplay) {
				for (key in videos) {
					if (videos.hasOwnProperty(key)) {
						if (videos[key].$videoWrapper.hasClass(classes.playing)) {
							if (!fullscreen) {
								pauseVideo(key);
								setAsPaused(videos[key]);
							}
						}
						videos[key].$videoWrapper.height($(document).width() / videoOptions.ratio);
					}
				}
				setAutoplaySupport(false);
			} else {
				setAutoplaySupport(true);
				for (key in videos) {
					if (videos[key].$videoWrapper.find('.' + classes.videoWithImage).length) {
						continue;
					}
					videoPlayers[key].playVideo();
					setAsPlaying(videos[key]);
				}
			}
		}

		function initEvents() {
			$(document).on('click.videoPlayer', selectors.playVideoBtn, function (evt) {
				var playerId = $(evt.currentTarget).data('controls');
				var player = videoPlayers[playerId];
				startVideoOnClick(playerId);
				player.playVideo();
				evt.preventDefault();
				if ($(evt.currentTarget).hasClass('is-started')) {
					$(evt.currentTarget).removeClass('is-started');
				}
			});
			$(document).on('click.videoPlayer', '.video-control__pause-mobile', function (evt) {
				var playerId = $(evt.currentTarget).data('controls');
				var player = videoPlayers[playerId];
				player.pauseVideo();
				evt.preventDefault();
			});

			$(document).on('click.videoPlayer', selectors.pauseVideoBtn, function (evt) {
				var playerId = $(evt.currentTarget).data('controls');
				togglePause(playerId);
			});
		}

		function createPlayer(key) {
			var args = $.extend({}, videoOptions, videos[key]);
			args.playerVars.controls = args.controls;
			args.playerVars.start = $('#' + videos[key].id).data('start');
			videoPlayers[key] = new YT.Player(key, args);
		}

		function removeEvents() {
			$(document).off('.videoPlayer');
			$(window).off('.videoPlayer');
		}

		return {
			init: init,
			editorLoadVideo: editorLoadVideo,
			loadVideos: loadVideos,
			playVideo: customPlayVideo,
			pauseVideo: pauseVideo,
			removeEvents: removeEvents,
			resizeVideo: resizeVideo
		};
	}();
	THEME.special = {
		init: function init() {
			this.simulateEventsDemo();
			this.setLanguage();
			this.scrollNextSection();
			if ($('.js-circle-loader-ajax').length) this.collectionLoader();
		},
		simulateEventsDemo: function simulateEventsDemo() {
			$(document).on('click', '.js-prd-setdata', function () {
				THEME.checkOutModal.setData($(this).data('product'));
			});
			$(document).on('click', '.js-prd-seterror', function () {
				THEME.checkOutModal.setError($(this).data('error'));
			});
			$(document).on('click', '.js-select-adddata', function () {
				THEME.selectModal.setData($(this).data('product'));
			});
			$(document).on('click', '.js-select-seterror', function () {
				THEME.selectModal.setError($(this).data('error'));
			});
			$(document).on('click', '.js-select-status', function () {
				alert(THEME.selectModal.getStatus());
			});
		},
		collectionLoader: function collectionLoader() {
			$(document).on('lazyloaded', '.has-infinite-scroll .js-circle-loader-ajax', function () {
				$('.js-circle-loader-ajax').trigger('click');
			});
		},
		setLanguage: function setLanguage() {
			$('.js-currencies').find('a').on('click', function (e) {
				var $this = $(this),
				    selected = $this.attr('data-value');
				$this.closest('li').siblings().removeClass('active').end().closest('li').addClass('active');
				$this.closest('.dropdn').find('.js-dropdn-select-current').html(selected);
				$this.closest('form').find("input[name='currency']").val(selected);
				$this.closest('form').submit();
				e.preventDefault();
			});
		},
		scrollNextSection: function scrollNextSection() {
			function goToByScroll($section) {
				var top = $section.offset().top,
				    h = $('.hdr-content-sticky').length ? $('.hdr-content-sticky').height() : 0;
				if ($('.bnr-wrap', $section).length) {
					top = $('.bnr-wrap', $section).offset().top;
				}
				$('html,body').animate({
					scrollTop: top - h - 20
				}, 300, function () {
					if (!$body.hasClass('has-sticky')) {
						$('html,body').animate({
							scrollTop: top - 20
						}, 50);
					}
				});
			}

			$('.js-scroll-to-next-section').on('click', function (e) {
				if ($(this).attr('href') == '#') {
					e.preventDefault();
					$(this).blur();
					var $next = $(this).closest('.shopify-section').next('.shopify-section');
					if ($next.length) goToByScroll($next);
				}
			});
		}
	};
	THEME.initialization = {
		init: function init() {
			this.removePreloader(1000);
			this.checkDevice();
			this.animations();
			this.parallaxImage();
			this.wishlist();
			if ($('iframe').length) this.responsiveVideo();
			if ($('table').length) this.tableAddClass();
			this.removeEmptyParent('.prd-block_info_item > .two-column');
			this.removeEmptyLinked('.colorswatch-label', 'ul');
			if ($('.bnslider').length) this.mainSlider('.bnslider');
			if ($('.bnslider .video').length) this.videoBannerInit('.bnslider .video');
			if ($('.mobilemenu-content').length) this.hideBeforeLoad('.mobilemenu-content', 1000);
			if ($('.js-filter-col').length) this.hideEmptyFilters('.js-filter-col', '.sidebar-block_content', '.filter-toggle');
			if ($('.js-filter-col-horizontal').length) this.hideEmptyFiltersH('.js-filter-col-horizontal', '.sidebar-block_content', '.filter-toggle');
			if ($('.product-tab').length) this.productTab('.product-tab');
			this.accordionSetActive();
			if ($('.collapsed-mobile').length) this.footerCollapse('.collapsed-mobile');
			if ($('.qty-input').length) this.qtyInput();
			if ($('[data-toggle="tooltip"]').length) this.tooltipIni('[data-toggle="tooltip"]', '.prd-block [data-toggle="tooltip"]');
			if ($('.js-countdown').length) this.countdown('.js-countdown');
			if ($('.js-newslettermodal').length) this.newsletterModal('.js-newslettermodal', '#newsLetterCheckBox');
			if ($('.js-popupPromo').length) this.promoPopup('.js-popupPromo');
			if ($('.page-content').length) this.setFullHeight('.page-content');
			if ($('.bnslider--fullheight').length) this.setFullHeightSlider('.bnslider--fullheight');
			if ($('#productAccordion').length) this.openAccordion('#productAccordion');
			if ($('.js-simple-filter').length) this.simpleFilters('.js-simple-filter');
			if ($('.fixed-scroll').length) this.compensateScrollBar('.fixed-scroll');
			if ($('[data-hover-bgcolor]').length) this.hoverColor();
			if ($('.js-scroll-to').length) this.scrollToTarget('.js-scroll-to');
			this.agreementCheckbox();
			if ($('.prd-block_accordion').length) this.tabScroll('.prd-block_accordion');
			if ($('.js-video-content').length) this.videoContent('.js-video-content');
			if ($('[data-toggle=popover]').length) this.popoverHtml('[data-toggle=popover]');
			if ($('.brand-grid').length) this.brandsShowMore();
			if ($('.js-color-hover-brand-grid').length) this.brandsColorHover();
			if ($('.parallaxed').length) this.parallaxImage();
		},
		wishlist: function wishlist() {
			var WishlistPage = {
				options: {
					wishlistGrid: '[data-wishlist-grid]'
				},
				init: function init(options) {
					$.extend(this.options, options);
					this._handlers(this);
				},
				_handlers: function _handlers(that) {
					$(document).on('click', '.js-add-wishlist', function (e) {
						$(this).closest('.prd, .prd-block_info').addClass('prd--in-wishlist');
						e.preventDefault();
					});
					$(document).on('click', '.js-remove-wishlist', function (e) {
						var $product = $(this).closest('.prd, .prd-block_info'),
						    $grid = $('.prd-grid--wishlist');
						if ($grid.length) {
							$grid.addClass('disable-actions');
							anime({
								targets: $product[0],
								opacity: 0,
								duration: 300,
								easing: 'linear',
								complete: function complete() {
									$product.remove();
									$product.removeClass('prd--in-wishlist');
									$grid.removeClass('disable-actions');
									if ($grid.children().length == 0) {
										var $empty = $('.js-empty-wishlist');
										if ($empty.length) {
											$empty.removeClass('d-none');
											anime({
												targets: $empty[0],
												opacity: 1,
												duration: 300,
												easing: 'linear'
											});
										}
									}
								}
							});
						} else {
							$product.removeClass('prd--in-wishlist');
						}
						e.preventDefault();
					});
				}
			};
			var wishlistPage = Object.create(WishlistPage);
			wishlistPage.init();
		},
		parallaxImage: function parallaxImage() {
			function parallaxed(image, e) {
				var amountMovedX = e.clientX * -0.3 / 8,
				    amountMovedY = e.clientY * -0.3 / 8,
				    x = $(image);
				for (var i = 0; i < x.length; i++) {
					x[i].style.transform = 'translate(' + amountMovedX + 'px,' + amountMovedY + 'px)';
				}
			}

			$('.parallaxed').on('mousemove', function (event) {
				parallaxed(this, event);
			});
		},
		brandsColorHover: function brandsColorHover() {
			var $grid = $('.js-color-hover-brand-grid');
			$grid.on('mouseenter', 'a', function (e) {
				$(this).closest('.js-color-hover-brand-grid').find('img').addClass('grayscale');
				$(this).find('img').removeClass('grayscale');
			}).on('mouseleave', 'a', function (e) {
				$(this).closest('.js-color-hover-brand-grid').find('img').removeClass('grayscale');
			});
		},
		brandsShowMore: function brandsShowMore() {
			var $btn = $('.js-brands-show-more');
			if (isMobile & !$btn.hasClass('showLess')) {
				$('.brand-grid .js-hidden').hide().removeClass('visuallyhidden');
			} else {
				$('.brand-grid .js-hidden').show().removeClass('visuallyhidden');
			}
			if (!$btn.hasClass('is-init')) {
				$('.js-brands-show-more').addClass('is-init');
				$('.js-brands-show-more').on('click', function (e) {
					$(this).closest('.holder').find('.js-hidden').fadeToggle(250);
					$(this).toggleClass('showLess').blur();
					e.preventDefault();
				});
			}
		},
		popoverHtml: function popoverHtml(popover) {
			$document.on('click', function (event) {
				var $target = $(event.target);
				if ($target.closest(popover).length) {
					var $btn = $target.closest(popover);
					$(popover).not($btn).popover('hide').removeClass('active');
				} else if ($target.closest('.popover').length) {
					event.stopPropagation();
				} else {
					$(popover).popover('hide').removeClass('active');
				}
			});
			$(popover).on('click', function () {
				$(this).toggleClass('active');
			});
			$(popover).each(function () {
				var $parent = $(this).closest('.lookbook-bnr');
				if ($parent.length) {
					$(this).popover({
						html: true,
						content: function content() {
							return $(this).next('.js-popover-content').html();
						},
						animation: true,
						trigger: 'click',
						container: $parent,
						sanitize: false,
						delay: { "show": 100, "hide": 300 },
						template: '<div class="popover"><div class="arrow"></div><div class="popover-body"></div></div>'
					});
				}
			});
		},
		videoBannerInit: function videoBannerInit(video) {
			$(video).each(function () {
				var $el = $(this);
				THEME.Video.init($el);
				THEME.Video.editorLoadVideo($el.attr('id'));
			});
		},
		videoContent: function videoContent(video) {
			var $videoStopBtn = $('.js-video-stop'),
			    $videoPlayBtn = $('.js-video-play');
			$videoPlayBtn.on('click', function () {
				var $video = $(this).closest('.js-video-wrap').find(video);
				$video.get(0).play();
				$video.parent().addClass('is-playing').removeClass('is-paused');
			});
			$videoStopBtn.on('click', function () {
				var $video = $(this).closest('.js-video-wrap').find(video);
				$video.get(0).pause();
				$video.parent().addClass('is-paused').removeClass('is-playing');
			});
		},
		tabScroll: function tabScroll(accordion) {
			$('.collapse', $(accordion)).on('show.bs.collapse', function (e) {
				var $panel = $(this).closest('.panel');
				var $open = $($(this).data('parent')).find('.collapse.show');
				var additionalOffset = 0;
				var top = $body.hasClass('has-hdr_sticky') ? $('.hdr').outerHeight() : 0;
				var bottomOffset = h - ($panel.offset().top - $window.scrollTop() + $(this).height());
				var scroll = $window.scrollTop() - bottomOffset + top;
				if ($(this).height() > h - top) {
					scroll = $panel.offset().top - additionalOffset - top;
				}
				if ($panel.prevAll().filter($open.closest('.panel')).length !== 0) {
					additionalOffset = $open.height();
				}
				if (bottomOffset < 0) {
					$('html,body').animate({
						scrollTop: scroll
					}, 500);
				}
			});
		},
		agreementCheckbox: function agreementCheckbox() {
			function checkAgreement($obj) {
				var $this = $obj;
				if ($this.is(':checked')) {
					$($this.data('button')).each(function () {
						$(this).removeClass('disabled').addClass('enabled');
						$(this).prop('disabled', false);
					});
				} else {
					$($this.data('button')).each(function () {
						$(this).addClass('disabled').removeClass('enabled');
						$(this).prop('disabled', true);
					});
				}
			}

			$('.js-agreement-checkbox').each(function () {
				checkAgreement($(this));
			});
			$('.js-agreement-checkbox').on('change', function () {
				checkAgreement($(this));
			});
		},
		mainSlider: function mainSlider(slider) {
			var mainSlider = function mainSlider(el) {
				var MainSlider = {
					options: {
						slider: el,
						arrowsplace: '.bnslider-arrows > div',
						dotsplace: '.bnslider-dots',
						wrapper: '.bnslider-wrapper',
						videoStopBtn: '.js-video-slider-stop',
						videoPlayBtn: '.js-video-slider-play'
					},
					init: function init() {
						this._setHeight();
						this._sizeBackgroundVideos();
						this._animate();
					},
					reinit: function reinit() {
						this._setHeight();
						this._sizeBackgroundVideos();
					},
					_animate: function _animate() {
						var that = this,
						    $slider = $(that.options.slider),
						    media = $slider.find(that.options.media),
						    videoStopBtn = that.options.videoStopBtn,
						    videoPlayBtn = that.options.videoPlayBtn,
						    $wrapper = $slider.closest(that.options.wrapper),
						    arrowsplace = $wrapper.find(that.options.arrowsplace),
						    dotsplace = $wrapper.find(that.options.dotsplace),
						    animatedText = "[class^='bnslider-text'],[class*=' bnslider-text'],.btn-wrap",
						    $textBlock = $(animatedText, $slider);
						$textBlock.each(function () {
							var $this = $(this),
							    thisInlineStyle = '';
							var thisStyle = $this.data();
							for (var data in thisStyle) {
								if (data == 'fontcolor') {
									thisInlineStyle += 'color:' + $this.data('fontcolor') + ';';
								}
								if (data == 'fontfamily') {
									thisInlineStyle += 'font-family:' + $this.data('fontfamily') + ';';
								}
								if (data == 'fontsize') {
									thisInlineStyle += 'font-size:' + $this.data('fontsize') + 'px;';
								}
								if (data == 'fontline') {
									thisInlineStyle += 'line-height:' + $this.data('fontline') + 'em;';
								}
								if (data == 'fontweight') {
									thisInlineStyle += 'font-weight:' + $this.data('fontweight') + ';';
								}
								if (data == 'bgcolor') {
									thisInlineStyle += 'background-color:' + $this.data('bgcolor') + ';';
								}
								if (data == 'otherstyle') {
									thisInlineStyle += $this.data("otherstyle") + ';';
								}
							}
							if (thisInlineStyle.length > 0) {
								$this.attr('style', thisInlineStyle).addClass('data-ini');
							}
						});

						function splitLetter(text) {
							var $textWrapper = $(text);
							$textWrapper.addClass('text-nowrap');
							if ($textWrapper.find('span').length) {
								$textWrapper.find('span').each(function () {
									var $this = $(this),
									    style = $this.attr('style');
									$this.html($this.html().replace(/([^\x00-\x80]|\w)/g, "<span class='letter' style='" + style + "'>$&</span>"));
									$this.contents().unwrap();
								});
							} else $textWrapper.html($textWrapper.html().replace(/([^\x00-\x80]|\w)/g, "<span class='letter'>$&</span>"));
						}

						function doAnimations(elements, first) {
							if (first) {
								$('.bnslider-first-slide').removeClass('bnslider-first-slide');
								return false;
							}
							elements.each(function () {
								var $this = $(this);
								var animationDelay = $this.data('animation-delay');
								var animationType = $this.data('animation');
								if (animationType == 'fadeInDown') {
									anime({
										targets: this,
										opacity: [0, 1],
										translateY: ['-200%', '0'],
										easing: 'easeInOutSine',
										duration: 500,
										delay: animationDelay
									});
								} else if (animationType == 'fadeInUp') {
									anime({
										targets: this,
										opacity: [0, 1],
										translateY: ['200%', '0'],
										easing: 'easeInOutSine',
										duration: 500,
										delay: animationDelay
									});
								} else if (animationType == 'fadeInLeft') {
									anime({
										targets: this,
										opacity: [0, 1],
										translateX: ['-200%', '0'],
										easing: 'easeInOutSine',
										duration: 600,
										delay: animationDelay
									});
								} else if (animationType == 'fadeInRight') {
									anime({
										targets: this,
										opacity: [0, 1],
										translateX: ['200%', '0'],
										easing: 'easeInOutSine',
										duration: 600,
										delay: animationDelay
									});
								} else if (animationType == 'fadeIn') {
									anime({
										targets: this,
										opacity: [0, 1],
										easing: 'easeInOutSine',
										duration: 600,
										delay: animationDelay
									});
								} else if (animationType == 'zoomIn') {
									anime({
										targets: this,
										opacity: [0, 1],
										scale: [.5, 1],
										easing: 'easeInOutQuad',
										duration: 400,
										delay: animationDelay
									});
								} else if (animationType == 'fromLeftRubber') {
									anime({
										targets: [this.getElementsByClassName('circle_decor')],
										opacity: [0, .75],
										translateX: ['-200%', '0'],
										rotate: 400,
										duration: 4000,
										delay: animationDelay
									});
								} else if (animationType == 'fromRightRubber') {
									anime({
										targets: [this.getElementsByClassName('circle_decor')],
										opacity: [0, .75],
										translateX: ['200%', '0'],
										rotate: 400,
										duration: 4000,
										delay: animationDelay
									});
								} else if (animationType == 'flipY') {
									anime({
										targets: this,
										opacity: [0, 1],
										rotateY: [180, 0],
										easing: 'easeInOutQuad',
										duration: 500,
										delay: animationDelay
									});
								} else if (animationType == 'flipX') {
									anime({
										targets: this,
										opacity: [0, 1],
										rotateX: [180, 0],
										easing: 'easeInOutQuad',
										duration: 500,
										delay: animationDelay
									});
								} else if (animationType == 'fx1') {
									splitLetter(this);
									anime.timeline({ loop: false }).add({
										targets: [this.getElementsByClassName('letter')],
										translateY: [-100, 0],
										opacity: [.5, 1],
										easing: "easeOutExpo",
										duration: 650,
										delay: function delay(el, i) {
											return animationDelay + 60 * i;
										}
									});
								} else if (animationType == 'fx2') {
									splitLetter(this);
									anime.timeline({ loop: false }).add({
										targets: [this.getElementsByClassName('letter')],
										translateX: [40, 0],
										translateZ: 0,
										opacity: [0, 1],
										duration: 800,
										delay: function delay(el, i) {
											return animationDelay + 60 * i;
										}
									});
								} else if (animationType == 'fx3') {
									splitLetter(this);
									anime.timeline({ loop: false }).add({
										targets: [this.getElementsByClassName('letter')],
										translateY: ["1.1em", 0],
										translateZ: 0,
										duration: 1000,
										delay: function delay(el, i) {
											return animationDelay + 60 * i;
										}
									});
								} else if (animationType == 'fx4') {
									splitLetter(this);
									anime.timeline({ loop: false }).add({
										targets: [this.getElementsByClassName('letter')],
										opacity: [0, 1],
										easing: "easeInOutQuad",
										duration: 1400,
										delay: function delay(el, i) {
											return animationDelay + 100 * (i + 1);
										}
									});
								} else if (animationType == 'fx5') {
									splitLetter(this);
									anime.timeline({ loop: false }).add({
										targets: [this.getElementsByClassName('letter')],
										easing: 'easeOutElastic',
										duration: 1200,
										translateY: function translateY(el, i) {
											return i % 2 === 0 ? ['-100%', '0%'] : ['100%', '0%'];
										},
										delay: function delay(el, i) {
											return animationDelay + 50 * (i + 1);
										}
									});
								} else if (animationType == 'fx6') {
									splitLetter(this);
									anime.timeline({ loop: false }).add({
										targets: [this.getElementsByClassName('letter')],
										rotateY: [-90, 0],
										duration: 1000,
										delay: function delay(el, i) {
											return animationDelay + 50 * (i + 1);
										}
									});
								} else if (animationType == 'fx7') {
									var textWrapper = this.getElementsByClassName('bnslider-text-inside')[0];
									var bg = [this.getElementsByClassName('bnslider-text-bg')];
									splitLetter(textWrapper);
									anime.timeline({ loop: false }).add({
										targets: [this.getElementsByClassName('letter')],
										duration: 650,
										easing: 'easeOutQuint',
										opacity: [0, 1],
										translateX: function translateX(el, i) {
											return [-1 * el.offsetLeft, 0];
										},
										delay: function delay(el, i) {
											return animationDelay + 450 + (el.parentNode.children.length - i - 1) * 30;
										}
									});
									anime.timeline({ loop: false }).add({
										targets: bg,
										translateX: ['0'],
										duration: 0
									}).add({
										targets: bg,
										easing: 'easeInOutCubic',
										scaleX: [0, 1],
										duration: 400,
										delay: animationDelay
									}).add({
										targets: bg,
										easing: 'easeInOutCubic',
										translateX: ['0', '110%'],
										duration: 400
									});
								}
							});
						}

						function startInit($firstAnimatingElements) {
							setTimeout(function () {
								$('[data-animation]', $slider).css({ 'opacity': 1 });
								doAnimations($firstAnimatingElements, true);
								if ($slider.data('autoplay') == true) {
									$slider.slick('slickPlay');
								}
							}, 1000);
						}

						$('.bnslider-slide', $slider).each(function () {
							if ($(this).data('autoplay') == true) {
								$(this).addClass('is-playing');
							}
						});
						$slider.on('init', function (slick) {
							slick = $(slick.currentTarget);
							var $currentSlide = slick.find('.slick-current'),
							    $firstAnimatingElements = $('[data-animation]', $currentSlide);
							if (isMobile) {
								if (!$('.bnslider-image-mobile', $currentSlide).find('img').length) {
									startInit($firstAnimatingElements);
								} else {
									$('.bnslider-image-mobile', $currentSlide).one('lazyloaded', function () {
										startInit($firstAnimatingElements);
									});
								}
							} else {
								if (!$('.bnslider-image', $currentSlide).find('img').length) {
									startInit($firstAnimatingElements);
								} else {
									$('.bnslider-image', $currentSlide).one('lazyloaded', function () {
										startInit($firstAnimatingElements);
									});
								}
							}
							var status = $currentSlide.data('autoplay') == true ? "play" : "pause";
							if ($currentSlide.find('video').length) {
								setTimeout(function () {
									if (status == "play") {
										var playPromise = $currentSlide.find('video')[0].play();
										if (playPromise !== undefined) {
											playPromise.then(function () {
												controlVideo(slick, status);
											}).catch(function (error) {
												console.info('autoplay blocked');
												controlVideo(slick, 'pause');
											});
										}
									} else {
										controlVideo(slick, 'pause');
									}
								}, 100);
							} else if ($currentSlide.find('iframe').length) {
								if (status == "play") {
									controlVideo($slider, "play", true);
								}
							}
						});
						$slider.on('afterChange', function (e, slick) {
							slick = $(slick.$slider);
							var $currentSlide = slick.find('.slick-current'),
							    status = $currentSlide.data('autoplay') == true && !$currentSlide.hasClass('is-custom-paused') || $currentSlide.hasClass('is-custom-playing') ? "play" : "pause",
							    $animatingElements = $currentSlide.find('[data-animation]');
							controlVideo(slick, status);
						});
						$slider.on('beforeChange', function (event, slick, currentSlide, nextSlide) {
							slick = $(slick.$slider);
							var $nextSlide = $slider.find('.bnslider-slide[data-slick-index="' + nextSlide + '"]'),
							    $animatingElements = $nextSlide.find('[data-animation]');
							$slider.slick('slickPlay');
							controlVideo(slick, "pause");
							doAnimations($animatingElements);
							$slider.find('svg').css({ 'opacity': 0, 'transform': '' });
						});
						$slider.slick({
							appendArrows: arrowsplace,
							appendDots: dotsplace,
							accessibility: true,
							arrows: true,
							dots: true,
							fade: true,
							draggable: true,
							touchThreshold: 20,
							autoplay: false,
							autoplaySpeed: $slider.data('speed')
						});

						function playerSend(player, command) {
							if (player == null || command == null) return;
							player.contentWindow.postMessage(JSON.stringify(command), "*");
						}

						function controlVideo(slick, control, button) {
							var video = void 0,
							    currentSlide = slick.find(".slick-current"),
							    slideType = currentSlide.data("video-type"),
							    player = currentSlide.find("iframe").get(0),
							    userConrol = button;
							if (userConrol) currentSlide.addClass('userControl');
							if (slideType === "youtube") {
								switch (control) {
									case "play":
										playerSend(player, {
											"event": "command",
											"func": "mute"
										});
										playerSend(player, {
											"event": "command",
											"func": "playVideo"
										});
										currentSlide.addClass('is-playing').removeClass('is-paused');
										if (userConrol) currentSlide.addClass('is-custom-playing').removeClass('is-custom-paused');
										break;
									case "pause":
										playerSend(player, {
											"event": "command",
											"func": "pauseVideo"
										});
										currentSlide.removeClass('is-playing').addClass('is-paused');
										if (userConrol) currentSlide.removeClass('is-custom-playing').addClass('is-custom-paused');
										break;
								}
							} else if (slideType === "video") {
								video = currentSlide.find("video").get(0);
								if (video != null) {
									if (control === "play") {
										video.play();
										currentSlide.addClass('is-playing').removeClass('is-paused');
										if (userConrol) currentSlide.addClass('is-custom-playing').removeClass('is-custom-paused');
									} else {
										video.pause();
										currentSlide.removeClass('is-playing').addClass('is-paused');
										if (userConrol) currentSlide.removeClass('is-custom-playing').addClass('is-custom-paused');
									}
								}
							}
						}

						$(videoPlayBtn).on('click', function () {
							var $this = $(this);
							if ($this.hasClass('btn')) {
								if (!$this.hasClass('is-play')) {
									controlVideo($slider, "play", true);
									$this.addClass('is-play');
								} else {
									controlVideo($slider, "pause", true);
									$this.removeClass('is-play');
								}
							} else {
								controlVideo($slider, "play", true);
							}
						});
						$(videoStopBtn).on('click', function () {
							controlVideo($slider, "pause", true);
						});
					},
					_sizeBackgroundVideos: function _sizeBackgroundVideos() {
						function sizeBackgroundVideo($slide) {
							if ($slide.hasClass('slick-cloned')) {
								return;
							}
							var $player = $slide.find('iframe'),
							    videoOptionsRatio = 16 / 9,
							    slideWidth = $slide.width(),
							    playerWidth = $player.width(),
							    playerHeight = $slide.height();
							if (slideWidth / videoOptionsRatio < playerHeight) {
								playerWidth = Math.ceil(playerHeight * videoOptionsRatio);
								if ($('body').hasClass('rtl')) {
									$player.width(playerWidth).height(playerHeight).css({
										right: (slideWidth - playerWidth) / 2,
										top: 0
									});
								} else {
									$player.width(playerWidth).height(playerHeight).css({
										left: (slideWidth - playerWidth) / 2,
										top: 0
									});
								}
							} else {
								playerHeight = Math.ceil(slideWidth / videoOptionsRatio);
								$player.width(slideWidth).height(playerHeight).css({
									left: 0,
									right: '',
									top: (playerHeight - playerHeight) / 2
								});
							}
						}

						$(this.options.slider).find('[data-video-type="youtube"]').each(function (index, el) {
							sizeBackgroundVideo($(el));
						});
					},
					_setHeight: function _setHeight() {
						var $slider = $(this.options.slider);
						keepScale($slider);

						function keepScale(slider) {
							var $bnsliderNoScale = slider;
							var wW = $(window).width();
							var mobileBreikpoint = 575;
							if ($bnsliderNoScale.hasClass("keep-scale") && !$bnsliderNoScale.hasClass("bnslider--fullheight")) {
								$bnsliderNoScale.css({
									'height': '',
									'min-height': ''
								});
								var bnrH;
								if (wW <= mobileBreikpoint && parseInt($bnsliderNoScale.attr('data-start-mwidth')) > 0 && parseInt($bnsliderNoScale.attr('data-start-mheight')) > 0) {
									var bnrH = parseInt($bnsliderNoScale.attr('data-start-mheight'), 10) / parseInt($bnsliderNoScale.attr('data-start-mwidth'), 10) * wW;
									$bnsliderNoScale.css({
										'height': bnrH + 'px',
										'min-height': bnrH + 'px'
									});
								} else if (parseInt($bnsliderNoScale.attr('data-start-width')) > 0 && parseInt($bnsliderNoScale.attr('data-start-height')) > 0) {
									var bnrH = parseInt($bnsliderNoScale.attr('data-start-height'), 10) / parseInt($bnsliderNoScale.attr('data-start-width'), 10) * wW;
									if ($bnsliderNoScale.closest("div[class^='col-'],div[class*=' col-']").length) {
										var colW = $bnsliderNoScale.closest("div[class^='col-'],div[class*=' col-']").width() * 100 / wW;
										bnrH = bnrH * colW / 100;
									} else if ($bnsliderNoScale.closest(".holder.boxed").length) {
										var colW = $bnsliderNoScale.closest(".container").width() * 100 / wW;
										bnrH = bnrH * colW / 100;
									}
									$bnsliderNoScale.css({
										'height': bnrH + 'px',
										'min-height': bnrH + 'px'
									});
								} else return false;
							}
						}
					}
				};
				THEME.mainslider[el] = Object.create(MainSlider);
				THEME.mainslider[el].init();
			};
			THEME.mainslider = {};
			$(slider).each(function () {
				var $el = $(this);
				if ($el.hasClass('video-section-wrapper')) return false;
				var bnslider = "#" + $el.attr('id');
				mainSlider(bnslider);
			});
		},
		initDelay: function initDelay() {
			if ($('[data-fontratio]').length) this.flowtype();
			if ($('.bnslider').length) this.sliderTextTopShift();
			if ($('.js-back-to-top').length) this.backToTop('.js-back-to-top');
		},
		resizeSvgRect: function resizeSvgRect() {
			$('svg > rect').each(function () {
				var $rect = $(this),
				    $container = $rect.closest('.form-control-wrap');
				$rect.attr('width', $container.width());
			});
		},
		removePreloader: function removePreloader(delay) {
			setTimeout(function () {
				$body.addClass('no-loader').removeClass('ready');
			}, delay);
			setTimeout(function () {
				$('.body-loader').remove();
			}, delay + 1000);
		},
		checkDevice: function checkDevice() {
			var isTouchDevice = 'ontouchstart' in window || navigator.msMaxTouchPoints;
			if (navigator.userAgent.indexOf('Windows') > 0) {
				$body.addClass('win');
				isTouchDevice = false;
			}
			if (isTouchDevice) {
				$body.addClass('touch');
				$('html').addClass('touch');
				swipemode = true;
			}
			if (navigator.userAgent.indexOf('Mac') > 0) {
				$body.addClass('mac');
			}
			if (navigator.userAgent.match(/Android/)) {
				$body.addClass('android');
			}
			if (navigator.userAgent.indexOf('MSIE') !== -1 || navigator.appVersion.indexOf('Trident/') > -1) {
				$body.addClass('ie');
				$('[data-srcset]').each(function () {
					var img = $(this).attr('data-srcset');
					$(this).attr('data-src', img);
				});
				$('[data-bgset]').each(function () {
					var img = $(this).attr('data-bgset');
					$(this).css('backgroundImage', 'url(' + img + ')');
				});
			}
		},
		animations: function animations() {
			if ($('#morphing').length) {
				var morphing = anime({
					targets: '#morphing .p',
					d: [{ value: 'M102.415 0.213527C130.863 2.47687 147.861 29.1238 165.824 51.3646C184.061 73.9452 208.291 96.5524 203.349 125.186C198.223 154.882 170.474 174.504 142.619 185.81C117.091 196.172 89.4629 192.662 64.2006 181.665C37.3147 169.962 10.5522 153.227 2.70559 124.905C-5.49916 95.2906 5.82269 64.1385 24.9965 40.1606C44.0991 16.2717 71.9772 -2.20802 102.415 0.213527Z' }, { value: 'M85.5803 0.0257094C113.027 0.686367 135.69 17.7373 153.924 38.3289C173.847 60.8277 195.211 87.2464 188.86 116.659C182.461 146.295 151.822 161.123 124.576 174.25C97.3846 187.35 66.591 202.297 39.7448 188.498C13.3189 174.916 8.32449 141.215 3.2131 111.87C-1.45393 85.0772 -3.11404 56.8126 12.9963 34.9367C29.7713 12.1583 57.3593 -0.653592 85.5803 0.0257094Z' }, { value: 'M93.5441 2.30824C127.414 -1.02781 167.142 -4.63212 188.625 21.7114C210.22 48.1931 199.088 86.5178 188.761 119.068C179.736 147.517 162.617 171.844 136.426 186.243C108.079 201.828 73.804 212.713 44.915 198.152C16.4428 183.802 6.66731 149.747 1.64848 118.312C-2.87856 89.9563 1.56309 60.9032 19.4066 38.3787C37.3451 15.7342 64.7587 5.14348 93.5441 2.30824Z' }],
					easing: 'easeInOutSine',
					duration: 10000,
					direction: 'alternate',
					loop: true
				});
			}
		},
		hoverColor: function hoverColor() {
			if (!$body.hasClass('touch')) {
				$document.on('mouseenter', '[data-hover-bgcolor]', function (e) {
					var $this = $(e.target),
					    color = $this.attr('data-hover-bgcolor') ? $this.attr('data-hover-bgcolor') : '';
					$this.css({
						'background-color': color
					});
				}).on('mouseleave', '[data-hover-bgcolor]', function (e) {
					var $this = $(e.target),
					    color = $this.attr('data-bgcolor') ? $this.attr('data-bgcolor') : '';
					$this.css({
						'background-color': color
					});
				});
				$document.on('mouseenter', '[data-hover-color]', function (e) {
					var $this = $(e.target),
					    color = $this.attr('data-hover-color') ? $this.attr('data-hover-color') : '';
					$this.css({
						'color': color
					});
				}).on('mouseleave', '[data-color]', function (e) {
					var $this = $(e.target),
					    color = $this.attr('data-color') ? $this.attr('data-color') : '';
					$this.css({
						'color': color
					});
				});
				$document.on('mouseenter', '.bnr-wrap', function (e) {
					if ($(e.target).hasClass('.bnr-wrap')) {
						$(e.target).find('.btn').addClass('hovered');
					} else {
						$(e.target).closest('.bnr-wrap').find('.btn').addClass('hovered');
					}
				}).on('mouseleave', '.bnr-wrap', function (e) {
					if ($(e.target).hasClass('.bnr-wrap')) {
						$(e.target).find('.btn').removeClass('hovered');
					} else {
						$(e.target).closest('.bnr-wrap').find('.btn').removeClass('hovered');
					}
				});
			}
		},
		tableAddClass: function tableAddClass() {
			$('table').each(function () {
				if (!$(this).parent().hasClass('table-responsive')) {
					$(this).addClass('table').wrap('<div class="table-responsive"></div>');
				}
			});
		},
		responsiveVideo: function responsiveVideo() {
			$('iframe').each(function () {
				if (!$(this).closest('.bnslider').length && !$(this).parent().hasClass('embed-responsive') && this.src.indexOf('youtube') != -1) {
					$(this).wrap('<div class="embed-responsive embed-responsive-16by9"></div>');
				}
			});
		},
		compensateScrollBar: function compensateScrollBar(el) {
			$(el).css({
				width: 'calc(100% + ' + scrollWidth + 'px)'
			});
		},
		scrollOnLoad: function scrollOnLoad() {
			var $elem = $($(location).attr('href').split('#')[1]);
			if ($elem.length) {
				setTimeout(function () {
					var speed = $body.height() / 3 > 500 ? $body.height() / 3 : 500;
					var wHeight = $window.height() < $elem.height() * 2 ? 0 : $window.height() - $elem.height() * 2,
					    offsetTop = $elem.offset().top - wHeight;
					$('html,body').animate({
						scrollTop: offsetTop
					}, speed);
				}, 500);
			}
		},
		simpleFilters: function simpleFilters(el) {
			if ($(el).length) {
				THEME.simplefilters = {
					default: {
						gallery: '.js-simple-filter',
						galleryItem: '.js-simple-filter-item',
						filterLabel: '.js-simple-filter-label'
					},
					init: function init(options) {
						$.extend(this.default, options);
						var that = this,
						    $gallery = $(this.default.gallery);
						$gallery.each(function () {
							var $gallery = $(this),
							    $galleryItem = $(that.default.galleryItem, $gallery),
							    $filterLabel = $(that.default.filterLabel, $gallery),
							    activeStart = void 0;
							that._handlers($filterLabel, $galleryItem, $gallery);
							$filterLabel.each(function () {
								var $this = $(this),
								    selectedCategory = $this.attr('data-filter'),
								    count = '<span>' + $gallery.find(selectedCategory).length + '</span>';
								$this.append(count);
								if ($this.hasClass('active')) {
									$galleryItem.filter(selectedCategory).fadeIn(0).addClass('isvisible');
									activeStart = true;
								} else {
									$galleryItem.fadeIn(0).addClass('isvisible');
								}
							});
							if (!activeStart) $filterLabel.first().trigger('click');
							that._clickFirst($gallery);
						});
					},
					_clickFirst: function _clickFirst($gallery) {
						if ($('.faq-item', $gallery).length) {
							$('.panel-heading.active', $gallery).find('.panel-title').trigger('click');
							$('.faq-item.isvisible', $gallery).first().find('.panel-title').trigger('click');
						}
					},
					_handlers: function _handlers($filterLabel, $galleryItem, $gallery) {
						var that = this;
						$filterLabel.on('click', function (e) {
							var $this = $(this),
							    selectedCategory = $this.attr('data-filter');
							if ($this.hasClass('active')) {
								return false;
							} else {
								$this.siblings().removeClass('active');
								$this.addClass('active');
							}
							if (!selectedCategory) {
								$galleryItem.fadeIn(0).addClass('isvisible');
							} else {
								$galleryItem.filter(':not(' + selectedCategory + ')').fadeOut(0).removeClass('isvisible');
								$galleryItem.filter(selectedCategory).fadeIn(0).addClass('isvisible');
							}
							that._clickFirst($gallery);
							e.preventDefault();
						});
					},
					reinit: function reinit() {
						this.init();
						return this;
					}
				};
				THEME.simplefilters.init();
			}
		},
		imageLoadedProductPage: function imageLoadedProductPage(image) {
			$(image).each(function () {
				var $this = $(this);
				if ($this.closest('.prd')) $this.find('img').css({
					opacity: 0
				});
				$this.imagesLoaded(function () {
					$this.addClass('loaded');
					$this.find('img').animate({
						opacity: 1
					}, 200);
				});
			});
		},
		imageLoaded: function imageLoaded(image, carousel) {
			var $imageL = $(image);
			if (carousel) {
				$imageL = image;
			}
			$imageL.each(function () {
				var $this = $(this);
				if ($('.prd-img-area', $this).length) {
					$('.prd-img-area', $this).imagesLoaded(function () {
						$this.addClass('loaded');
					});
				} else {
					$this.imagesLoaded(function () {
						$this.addClass('loaded');
					});
				}
			});
		},
		removeEmpty: function removeEmpty(selector) {
			$(selector).each(function () {
				var $this = $(this);
				if (!$.trim($this.html()).length) $this.remove();
			});
		},
		removeEmptyParent: function removeEmptyParent(selector) {
			$(selector).each(function () {
				var $this = $(this);
				if (!$.trim($this.html()).length) {
					$this.parent().remove();
				} else $this.parent().removeClass('d-none');
			});
		},
		removeEmptyLinked: function removeEmptyLinked(target, empty) {
			$(target).each(function () {
				var $this = $(this),
				    $empty = $this.find(empty);
				if (!$.trim($empty.html()).length) $this.remove();
			});
		},
		setFullHeight: function setFullHeight(el) {
			if ($(el).length) {
				THEME.setfullheight = {
					default: {
						holder: '.page-content, .page404-bg',
						header: '.hdr-wrap',
						footer: '.page-footer'
					},
					init: function init(options) {
						$.extend(this.default, options);
						var that = this;
						$(that.default.holder).each(function () {
							var $this = $(this),
							    wh = $window.height();
							if ($(that.default.header).length && $(that.default.footer).length) {
								$this.css({
									'min-height': wh - $(that.default.header).outerHeight() - $(that.default.footer).outerHeight() - parseInt($(that.default.footer).css('marginTop')) + 'px'
								});
							} else if ($(that.default.header).length) {
								$this.css({
									'min-height': wh - $(that.default.header).outerHeight() + 'px'
								});
							} else if ($(that.default.footer).length) {
								$this.css({
									'min-height': wh - $(that.default.footer).outerHeight() - parseInt($(that.default.footer).css('marginTop')) + 'px'
								});
							}
						});
					},
					reinit: function reinit() {
						this.init();
						return this;
					}
				};
				THEME.setfullheight.init();
			}
		},
		setFullHeightSlider: function setFullHeightSlider(el) {
			if ($(el).length) {
				THEME.setfullheightslider = {
					default: {
						slider: '.bnslider--fullheight',
						header: '.hdr',
						footer: '.footer-style-5'
					},
					init: function init(options) {
						$.extend(this.default, options);
						var that = this;
						$(that.default.slider).each(function () {
							var $this = $(this),
							    wh = $window.height(),
							    $header = $(that.default.header),
							    $footer = $(that.default.footer);
							if ($header.length) {
								wh = wh - $header.outerHeight();
							}
							if ($footer.length) {
								wh = wh - $footer.outerHeight();
							}
							$this.css({
								'min-height': wh + 'px'
							});
						});
					},
					reinit: function reinit() {
						this.init();
						return this;
					}
				};
				THEME.setfullheightslider.init();
			}
		},
		backToTop: function backToTop(button) {
			function scrollToTop() {
				$body.addClass('blockSticky');
				$('html').addClass('scroll-auto');
				var speed = $window.scrollTop() / 5 > 500 ? $window.scrollTop() / 5 : 500;
				if (isMobile) {
					speed = speed / 2;
				}
				$('html, body').animate({
					scrollTop: 0
				}, speed, function () {
					$body.removeClass('blockSticky');
					$('html').removeClass('scroll-auto');
				});
				$body.hasClass('has-hdr_sticky') ? THEME.stickyheader.destroySticky() : false;
			}

			if ($(button).length) {
				var $button = $(button),
				    windowH = $window.height();
				if ($window.scrollTop() > windowH / 2) {
					$button.addClass('is-visible');
				}
				$window.on('scroll', function () {
					if ($(this).scrollTop() > h / 2) {
						$button.addClass('is-visible');
					} else {
						$button.removeClass('is-visible');
					}
				});
				$button.on('click', function (e) {
					scrollToTop();
					e.preventDefault();
				});
			}
		},
		promoPopup: function promoPopup(modal) {

			!function (t) {
				var e = {};!function () {
					var o,
					    n = t.requestAnimationFrame || t.webkitRequestAnimationFrame || t.mozRequestAnimationFrame || t.oRequestAnimationFrame || t.msRequestAnimationFrame || function (e) {
						t.setTimeout(e, 1e3 / 60);
					},
					    i = { particleCount: 50, angle: 90, spread: 45, startVelocity: 45, decay: .9, ticks: 200, x: .5, y: .5, zIndex: 100, colors: ["#26ccff", "#a25afd", "#ff5e7e", "#88ff5a", "#fcff42", "#ffa62d", "#ff36ff"] };function r() {}function l(t, e, o) {
						return n = t && null != t[e] ? t[e] : i[e], (r = o) ? r(n) : n;var n, r;
					}function a(t) {
						return parseInt(t, 16);
					}function c(t) {
						t.width = document.documentElement.clientWidth, t.height = document.documentElement.clientHeight;
					}function s(t) {
						var e,
						    o,
						    n = t.angle * (Math.PI / 180),
						    i = t.spread * (Math.PI / 180);return { x: t.x, y: t.y, wobble: 10 * Math.random(), velocity: .5 * t.startVelocity + Math.random() * t.startVelocity, angle2D: -n + (.5 * i - Math.random() * i), tiltAngle: Math.random() * Math.PI, color: (e = t.color, o = String(e).replace(/[^0-9a-f]/gi, ""), o.length < 6 && (o = o[0] + o[0] + o[1] + o[1] + o[2] + o[2]), { r: a(o.substring(0, 2)), g: a(o.substring(2, 4)), b: a(o.substring(4, 6)) }), tick: 0, totalTicks: t.ticks, decay: t.decay, random: Math.random() + 5, tiltSin: 0, tiltCos: 0, wobbleX: 0, wobbleY: 0 };
					}function u(o, i, l) {
						var a = i.slice(),
						    s = o.getContext("2d"),
						    u = o.width,
						    d = o.height;function m() {
							u = d = null;
						}var f,
						    h = (f = function f(e) {
							n(function i() {
								u || d || (c(o), u = o.width, d = o.height), s.clearRect(0, 0, u, d), (a = a.filter(function (t) {
									return function (t, e) {
										e.x += Math.cos(e.angle2D) * e.velocity, e.y += Math.sin(e.angle2D) * e.velocity + 3, e.wobble += .1, e.velocity *= e.decay, e.tiltAngle += .1, e.tiltSin = Math.sin(e.tiltAngle), e.tiltCos = Math.cos(e.tiltAngle), e.random = Math.random() + 5, e.wobbleX = e.x + 10 * Math.cos(e.wobble), e.wobbleY = e.y + 10 * Math.sin(e.wobble);var o = e.tick++ / e.totalTicks,
										    n = e.x + e.random * e.tiltCos,
										    i = e.y + e.random * e.tiltSin,
										    r = e.wobbleX + e.random * e.tiltCos,
										    l = e.wobbleY + e.random * e.tiltSin;return t.fillStyle = "rgba(" + e.color.r + ", " + e.color.g + ", " + e.color.b + ", " + (1 - o) + ")", t.beginPath(), t.moveTo(Math.floor(e.x), Math.floor(e.y)), t.lineTo(Math.floor(e.wobbleX), Math.floor(i)), t.lineTo(Math.floor(r), Math.floor(l)), t.lineTo(Math.floor(n), Math.floor(e.wobbleY)), t.closePath(), t.fill(), e.tick < e.totalTicks;
									}(s, t);
								})).length ? n(i) : (t.removeEventListener("resize", m), l(), e());
							});
						}, e.exports.Promise ? new e.exports.Promise(f) : (f(r), null));return t.addEventListener("resize", m, !1), { addFettis: function addFettis(t) {
								return a = a.concat(t), h;
							}, canvas: o, promise: h };
					}e.exports = function (t) {
						for (var e, n, i, r = l(t, "particleCount", Math.floor), a = l(t, "angle", Number), d = l(t, "spread", Number), m = l(t, "startVelocity", Number), f = l(t, "decay", Number), h = l(t, "colors"), b = l(t, "ticks", Number), y = l(t, "zIndex", Number), g = ((e = l(t, "origin", Object)).x = l(e, "x", Number), e.y = l(e, "y", Number), e), M = r, p = [], x = o ? o.canvas : (n = y, c(i = document.createElement("canvas")), i.style.position = "fixed", i.style.top = "0px", i.style.left = "0px", i.style.pointerEvents = "none", i.style.zIndex = n, i), v = x.width * g.x, w = x.height * g.y; M--;) {
							p.push(s({ x: v, y: w, angle: a, spread: d, startVelocity: m, color: h[M % h.length], ticks: b, decay: f }));
						}return o ? o.addFettis(p) : (document.body.appendChild(x), (o = u(x, p, function () {
							o = null, document.body.removeChild(x);
						})).promise);
					}, e.exports.Promise = t.Promise || null;
				}(), t.confetti = e.exports;
			}(window);

			var $promoPopup = $(modal);

			function checkCookie() {
				if ($.cookie('foxicPromoPopup') != 'yes' || $promoPopup.attr('data-expires') == '0' || $('body').hasClass('demo')) {
					openPromoPopup();
				}
			}

			function openPromoPopup() {
				var pause = $promoPopup.attr('data-pause') > 0 ? $promoPopup.attr('data-pause') : 2000,
				    src = $promoPopup.attr('data-src');
				setTimeout(function () {
					$.fancybox.open([{
						src: src,
						type: 'ajax',
						btnTpl: {
							smallBtn: '<div data-fancybox-close class="fancybox-close-small js-popup-promo-close"><i class="icon-close"></i></div>'
						},
						baseTpl: '<div class="fancybox-container">' + '<div class="fancybox-bg"></div>' + '<div class="fancybox-inner">' + '<div class="fancybox-stage"></div>' + "</div>" + "</div>"
					}], {
						buttons: ['close'],
						touch: false,
						afterShow: function afterShow() {
							var effect = $promoPopup.attr('data-effect');
							if (effect == 'confetti') {
								var i;

								(function () {
									var colors = ['#f0771a', '#ffffff', '#a83ae5'];
									for (i = 0; i < 3; i++) {
										setTimeout(function () {
											confetti({
												particleCount: 100,
												spread: 50 + i * 15,
												colors: colors,
												zIndex: 1000
											});
										}, 500 * i);
									}
								})();
							} else if (effect == 'snow') {
								var duration = 7 * 1000;
								var animationEnd = Date.now() + duration;
								var skew = 1;
								(function frame() {
									var timeLeft = animationEnd - Date.now();
									var ticks = Math.max(200, 500 * (timeLeft / duration));
									skew = Math.max(0.8, skew - 0.001);
									confetti({
										particleCount: 1,
										startVelocity: 0,
										ticks: ticks,
										gravity: 0.5,
										zIndex: 1000,
										origin: {
											x: Math.random(),
											y: Math.random() * skew - 0.2
										},
										colors: ['#ffffff'],
										shapes: ['circle']
									});
									if (timeLeft > 0) {
										requestAnimationFrame(frame);
									}
								})();
							} else if (effect == 'fireworks') {
								var end = Date.now() + 5 * 1000;
								var colors = ['#ffffff', '#f83f81'];
								(function frame() {
									confetti({
										particleCount: 2,
										angle: 60,
										spread: 55,
										origin: {
											x: 0
										},
										colors: colors,
										zIndex: 1000
									});
									confetti({
										particleCount: 2,
										angle: 120,
										spread: 55,
										origin: {
											x: 1
										},
										colors: colors,
										zIndex: 1000
									});
									if (Date.now() < end) {
										requestAnimationFrame(frame);
									}
								})();
							}
						}
					});
				}, pause);
			}
			$(document).on('click', '.js-popup-promo-close', function () {
				$.cookie('foxicPromoPopup', 'yes', {
					expires: parseInt($promoPopup.attr('data-expires'), 10)
				});
				parent.jQuery.fancybox.getInstance().close();
			});
			checkCookie();
		},
		newsletterModal: function newsletterModal(modal, checkbox) {
			if ($('body').hasClass('newslettermodal-error')) {
				setTimeout(function () {
					$.fancybox.open([{
						src: '.js-newslettermodal',
						type: 'inline',
						btnTpl: {
							smallBtn: '<div data-fancybox-close class="fancybox-close-small modal-close"><i class="icon-close"></i></div>'
						}
					}], {
						buttons: ['close'],
						touch: false,
						afterClose: function afterClose() {
							$.cookie('foxicNewsLetter', 'yes', {
								expires: parseInt($newsletter.attr('data-expires'), 10)
							});
						}
					});
				}, 1000);
			}
			var $newsletter = $(modal),
			    $checkBox = $(checkbox);
			if ($newsletter.attr('data-only-index') == 'true' && !$('body[class*="page-index"]').length) {
				return false;
			}

			function checkCookie() {
				if ($.cookie('foxicNewsLetter') != 'yes' || $newsletter.attr('data-expires') == '0' || $('body').hasClass('demo')) {
					openNewsletterPopup();
				}
			}

			function openNewsletterPopup() {
				var pause = $newsletter.attr('data-pause') > 0 ? $newsletter.attr('data-pause') : 2000;
				setTimeout(function () {
					if ($newsletter.hasClass('modal-info-content')) {
						$.fancybox.open([{
							src: modal,
							type: 'inline',
							btnTpl: {
								smallBtn: '<div data-fancybox-close class="fancybox-close-small modal-close"><i class="icon-close"></i></div>'
							}
						}], {
							buttons: ['close'],
							touch: false,
							afterClose: function afterClose() {
								$.cookie('foxicNewsLetter', 'yes', {
									expires: parseInt($newsletter.attr('data-expires'), 10)
								});
							}
						});
					} else {
						$newsletter.removeClass('d-none').addClass('opened');
						anime({
							targets: modal,
							translateY: ['100%', '0%'],
							easing: 'spring(1, 80, 10, 0)',
							duration: 300,
							complete: function complete() {
								if ($newsletter.find('svg').length) THEME.initialization.resizeSvgRect();
							}
						});
					}
				}, pause);
				setTimeout(function () {
					if ($('body').hasClass('newslettermodal-off')) {
						$.cookie('foxicNewsLetter', 'yes', {
							expires: parseInt($newsletter.attr('data-expires'), 10)
						});
					}
				}, pause * 2);
			}

			$document.on('click', '.js-popup-newsletter-close', function () {
				$.cookie('foxicNewsLetter', 'yes', {
					expires: parseInt($newsletter.attr('data-expires'), 10)
				});
				$newsletter.removeClass('opened');
				anime({
					targets: modal,
					translateY: ['0%', '100%'],
					easing: 'spring(1, 80, 10, 0)',
					duration: 300
				});
			});
			var uri = window.location.toString();
			if (uri.indexOf("?customer_posted=true") > 0) {
				$.cookie('foxicNewsLetter', 'yes', {
					expires: parseInt($newsletter.attr('data-expires'), 10)
				});
			} else {
				checkCookie();
			}
		},
		hideEmptyFilters: function hideEmptyFilters(columnFilter, filter, mobFilter) {
			if (!$(columnFilter).find(filter).length) {
				$(columnFilter).addClass('d-none');
				$(mobFilter).addClass('d-none');
			}
		},
		hideEmptyFiltersH: function hideEmptyFiltersH(columnFilter, filter, mobFilter) {
			if (!$(columnFilter).find(filter).length) {
				$(columnFilter).addClass('d-none');
				$(mobFilter).addClass('d-none');
			}
		},
		sliderTextTopShift: function sliderTextTopShift() {
			THEME.slidertexttopshift = {
				default: {
					header: '.hdr-wrap .hdr',
					text: '.bnslider-text-content-flex'
				},
				init: function init(options) {
					$.extend(this.default, options);
					if (!isMobile && !$('.aside').length) {
						if ($('.hdr-transparent').length) {
							$(this.default.header).addClass('visible');
							$(this.default.text).css({
								'padding-top': $(this.default.header).outerHeight() * .85
							});
						}
					} else {
						$(this.default.text).css({
							'padding-top': ''
						});
					}
					return this;
				},
				reinit: function reinit() {
					this.init();
					return this;
				}
			};
			THEME.slidertexttopshift.init();
		},
		hideBeforeLoad: function hideBeforeLoad(el, timeOut) {
			$(el).css('visibility', 'hidden');
			setTimeout(function () {
				$(el).css('visibility', '').addClass('loaded');
			}, timeOut);
		},
		countdown: function countdown(_countdown) {
			function removeCountdown($countdown) {
				if ($countdown.closest('.js-countdown-wrap').length) {
					$countdown.closest('.js-countdown-wrap').remove();
				} else $countdown.remove();
			}

			$(_countdown).each(function () {
				var $countdown = $(this),
				    promoperiod = void 0,
				    isActual = false,
				    timeLocale = ['DAYS', 'HRS', 'MIN', 'SEC'];
				if ($body.data('time-locale')) {
					timeLocale = $body.data('time-locale').split('/');
				}
				if ($countdown.attr('data-promoperiod')) {
					promoperiod = parseInt($countdown.attr('data-promoperiod'), 10);
					isActual = promoperiod > 0;
					promoperiod = new Date().getTime() + promoperiod;
				}
				if ($countdown.attr('data-countdown')) {
					promoperiod = $countdown.attr('data-countdown');
					isActual = Date.parse(promoperiod) - Date.parse(new Date()) > 0;
				}
				if (isActual) {
					$countdown.countdown(promoperiod, function (event) {
						$countdown.html(event.strftime('<span><span>%D</span>' + timeLocale[0] + '</span>' + '<span><span>%H</span>' + timeLocale[1] + '</span>' + '<span><span>%M</span>' + timeLocale[2] + '</span>' + '<span><span>%S</span>' + timeLocale[3] + '</span>'));
					}).on('finish.countdown', function () {
						removeCountdown($countdown);
					});
				} else {
					removeCountdown($countdown);
				}
				if ($countdown.closest('.js-countdown-wrap').length) {
					$countdown.closest('.js-countdown-wrap').addClass('countdown-init');
				}
			});
		},
		productTab: function productTab(tab) {
			var $tabs = $(tab),
			    setCurrent = false;
			$tabs.tabCollapse({
				accordion: true,
				tabsClass: 'd-none d-lg-flex',
				accordionClass: 'panel-group--style2 d-lg-none'
			});
			$('.panel-title').each(function () {
				if ($(this).children().length < 2) {
					$(this).append('<div class="toggle-arrow"><span></span><span></span></div>');
				}
			});
			$('a', $tabs).each(function () {
				var $this = $(this);
				if ($this.parent('li').is('.active')) {
					var curTab = $this.attr('href');
					$(curTab).addClass('active');
					setCurrent = true;
				}
			});
			if (!setCurrent) {
				$('li:first-child a', $tabs).tab('show');
			}
		},
		accordionSetActive: function accordionSetActive() {
			$body.on('show.bs.collapse', '.panel-collapse', function (e) {
				$(e.currentTarget).siblings('.panel-heading').addClass('active');
			}).on('hide.bs.collapse', '.panel-collapse', function (e) {
				$(e.currentTarget).siblings('.panel-heading').removeClass('active');
			});
			if (w < maxLG) {
				setTimeout(function () {
					$('.panel-group').each(function () {
						if ($(this).closest('.holder').data('open-mobile')) {
							var $first = $(this).find('.panel').first();
							$first.find('.panel-heading').addClass('active');
							$first.find('.panel-heading a').removeClass('collapsed');
							$first.find('.panel-collapse').addClass('show');
						}
					});
				}, 1000);
			}
		},
		openAccordion: function openAccordion(accordion) {
			if ($(accordion).find('.panel-heading.active')) return false;
			$(accordion).find('.panel-body').each(function () {
				var $this = $(this);
				if (!$.trim($this.html()).length) $this.closest('.panel').remove();
			}).promise().done($(accordion).find('.panel:first-child').find('.panel-title > a').trigger('click'));
		},
		flowtype: function flowtype() {
			THEME.flowtype = {
				default: {
					maximum: 9999,
					minimum: 1,
					maxFont: 9999,
					minFont: 1
				},
				init: function init(bnr) {
					var that = this;
					$(bnr).each(function () {
						var $this = $(this);
						$this.imagesLoaded(function () {
							var fontratio = Math.round($this.attr('data-fontratio') * 100) / 100;
							if (fontratio > 0) {
								that._changes($this, fontratio);
							}
							if ($this.find('.js-bnr-caption-carousel').length) {
								$this.find('.js-bnr-caption-carousel').each(function () {
									$(this).slick({
										autoplay: true,
										arrows: false,
										dots: false,
										slidesToShow: 1,
										draggable: false,
										infinite: true,
										pauseOnHover: false,
										swipe: false,
										touchMove: false,
										vertical: true,
										speed: 1000,
										autoplaySpeed: 2000,
										useTransform: true,
										cssEase: 'cubic-bezier(0.645, 0.045, 0.355, 1.000)'
									});
								});
							}
						});
					});
				},
				hide: function hide(bnr) {
					var that = this;
					$(bnr).each(function () {
						$(this).removeClass('fontratio-calc');
					});
				},
				hideCaption: function hideCaption(bnr) {
					$(bnr).each(function () {
						$(this).find('.bnr-caption').css({ 'opacity': 0 });
					});
				},
				showCaption: function showCaption(bnr) {
					$(bnr).each(function () {
						$(this).find('.bnr-caption').css({ 'opacity': 1 });
					});
				},
				reinit: function reinit(bnr) {
					var that = this;
					$(bnr).each(function () {
						var $this = $(this),
						    fontratio = Math.round($this.attr('data-fontratio') * 100) / 100;
						$this.removeClass('fontratio-calc');
						if (fontratio > 0) {
							that._changes($this, fontratio);
						}
					});
				},
				_changes: function _changes(el, fontRatio) {
					var $el = $(el),
					    elw = $el.width(),
					    width = elw > this.default.maximum ? this.default.maximum : elw < this.default.minimum ? this.default.minimum : elw,
					    fontBase = width / fontRatio,
					    fontSize = fontBase > this.default.maxFont ? this.default.maxFont : fontBase < this.default.minFont ? this.default.minFont : fontBase;
					setTimeout(function () {
						$el.css('font-size', fontSize + 'px').addClass('fontratio-calc');
					}, 100);
				}
			};
			THEME.flowtype.init('.bnr[data-fontratio]');
		},
		footerCollapse: function footerCollapse(el) {
			$('.footer-block').each(function () {
				if (!$(this).hasClass('collapsed-mobile')) {
					$(this).parent().addClass('col-no-collapsed');
				}
			});
			$.fn.footerCollapse = function () {
				var $collapsed = this;
				$('.title', $collapsed).on('click', function (e) {
					e.preventDefault;
					$(this).closest('.collapsed-mobile').toggleClass('open');
				});
			};
			$(el).footerCollapse();
		},
		qtyInput: function qtyInput() {
			$.fn.textWidth = function (text, font) {
				if (!$.fn.textWidth.fakeEl) $.fn.textWidth.fakeEl = $('<span>').hide().appendTo(document.body);
				$.fn.textWidth.fakeEl.text(text || this.val() || this.text() || this.attr('placeholder')).css('font', font || this.css('font'));
				return $.fn.textWidth.fakeEl.width();
			};
			$document.on('click', '.js-qty-button', function (e) {
				var $this = $(e.target),
				    input = $this.parent().find('.qty-input'),
				    v = $this.hasClass('decrease') ? input.val() - 1 : input.val() * 1 + 1,
				    min = input.attr('data-min') ? input.attr('data-min') : 1,
				    max = input.attr('data-max') ? input.attr('data-max') : false,
				    inputWidth = void 0;
				if (v >= min) {
					if (!max == false && v > max) {
						return false;
					} else input.val(v);
				}
				inputWidth = input.textWidth();
				input.css({ width: inputWidth + 10 });
				e.preventDefault();
			});
			$document.on('input', '.qty-input', function (e) {
				var input = $(e.target),
				    min = input.attr('data-min') ? input.attr('data-min') : 1,
				    max = input.attr('data-max') ? input.attr('data-max') : 1000,
				    v = Math.round(input.val()),
				    inputWidth = 0;
				if (v > max) {
					input.val(max);
				} else if (v < min) {
					input.val(min);
				}
				inputWidth = input.textWidth();
				input.css({ width: inputWidth + 10 });
			});
		},
		tooltipIni: function tooltipIni(tooltip) {
			if (!isMobile) {
				$(tooltip).each(function () {
					var $this = $(this);
					$this.tooltip({
						animation: false,
						trigger: 'hover',
						placement: $this.attr('data-placement')
					});
				});
				$('[data-toggle="tooltip"]').on('shown.bs.tooltip', function () {
					if ($('.tooltip').hasClass('bs-tooltip-right')) {
						$('.tooltip').addClass('tooltipright');
					} else if ($('.tooltip').hasClass('bs-tooltip-left')) {
						$('.tooltip').addClass('tooltipleft');
					} else if ($('.tooltip').hasClass('bs-tooltip-top')) {
						$('.tooltip').addClass('tooltiptop');
					} else if ($('.tooltip').hasClass('bs-tooltip-bottom')) {
						$('.tooltip').addClass('tooltipbottom');
					}
				});
			}
		},
		modalCountDown: function modalCountDown(modal) {
			var $modal = $(modal);
			if ($modal.length) {
				var counter = void 0;
				$modal.on('hidden.bs.modal', function () {
					var $modal = $(this);
					if ($modal.attr('data-interval') > 0) {
						$('.count', $modal).html('').fadeOut();
						clearInterval(counter);
					}
				});
				$modal.on('shown.bs.modal', function () {
					var interval = 0,
					    $modal = $(this);
					if ($modal.attr('data-interval') > 0) {
						interval = $modal.attr('data-interval');
					}
					var count = interval / 1000;
					if (count > 0) {
						$('.modal--countdown', $modal).show();
						$('.count', $modal).html(count).fadeIn();
						counter = setInterval(function modalCount() {
							if (count > 0) {
								count -= 1;
								$('.count', $modal).html(count);
							} else {
								$modal.modal('hide').removeData('bs.modal');
								clearInterval(counter);
							}
						}, 1000);
					}
				});
			}
		},
		scrollToDiv: function scrollToDiv(el, speed) {
			var $header = $('.hdr'),
			    $el = $(el),
			    speedScroll = speed ? speed : 300;
			if ($el.length) {
				$('html,body').animate({
					scrollTop: $el.offset().top - $header.height()
				}, speedScroll);
			}
		},
		scrollToTarget: function scrollToTarget(link) {
			var header = '.hdr';

			function goToByScroll(id) {
				id = id.replace('link", "');
				$('html,body').animate({
					scrollTop: $(id).offset().top - $(header).height()
				}, 500);
			}

			$(link).on('click', function (e) {
				var $this = $(this);
				e.preventDefault();
				$this.blur();
				if ($this.closest('.mmenu').length) {
					$this.parent().siblings().find('.active').removeClass('active');
					$this.addClass('active');
				}
				goToByScroll($(this).attr('href'));
			});
		}
	};
	THEME.header = {
		init: function init() {
			this.headerDrop();
			this.megaMenu();
			this.mmobilePush();
			this.searchAutoFill('.js-search-autofill', 'a', '.search-input');
			this.searchInput('.search-input');
			this.dropdnSelect('.js-dropdn-select');
		},
		landingMenu: function landingMenu() {
			function onScroll() {
				var scrollPos = $document.scrollTop() + 50;
				$('.hdr-landing-mmenu .mmenu a.js-scroll-to').each(function () {
					var $currLink = $(this),
					    $refElement = $($currLink.attr('href'));
					if ($refElement.position().top <= scrollPos && $refElement.position().top + $refElement.height() > scrollPos) {
						$currLink.parent().siblings().find('.active').removeClass('active');
						$currLink.addClass('active');
					} else {
						$currLink.removeClass('active');
					}
				});
			}
			$document.on('scroll', onScroll);
		},
		dropdnSelect: function dropdnSelect(link) {
			$(link).on('click', 'a', function (e) {
				var selected = $(this).text();
				$(this).closest('li').siblings().removeClass('active').end().closest('li').addClass('active');
				$(this).closest('.dropdn').find('.js-dropdn-select-current').html(selected);
				e.preventDefault();
			});
		},
		promoTopline: function promoTopline(topline, close) {
			var $topline = $(topline),
			    $close = $(close),
			    speed = 300,
			    timeout = 1000;
			if ($('.hdr-mobile-style2').length && isMobile) {
				speed = 100;
				timeout = 0;
			}

			function checkCookie() {
				if ($.cookie('THEMEPromoTopLine') != 'yes') {
					setTimeout(function () {
						$topline.slideDown(speed, function () {
							promoToplineHeight = $topline.outerHeight();
							if ($body.hasClass('has-hdr_sticky')) THEME.stickyheader.setHeaderHeight();
						});
					}, timeout);
				} else {
					$topline.slideUp(0);
					promoToplineHeight = 0;
					if ($body.hasClass('has-hdr_sticky')) THEME.stickyheader.setHeaderHeight();
				}
			}

			$close.on('click', function () {
				if ($body.hasClass('demo')) {
					$topline.slideUp(speed);
					promoToplineHeight = 0;
					if ($body.hasClass('has-hdr_sticky')) THEME.stickyheader.setHeaderHeight();
				} else {
					$.cookie('THEMEPromoTopLine', 'yes', {
						expires: parseInt($topline.attr('data-expires'), 10)
					});
					checkCookie();
				}
			});
			checkCookie();
		},
		searchAutoFill: function searchAutoFill(parent, link, target) {
			$(parent).find(link).on('click', function (e) {
				if ($(target).val() == $(this).html()) {
					return false;
				}
				$(target).val($(this).html()).focus().trigger('keyup');
				e.preventDefault();
			});
		},
		searchInput: function searchInput(input) {
			$(input).on('input', function () {
				!$(this).val() ? $(this).addClass('input-empty') : $(this).removeClass('input-empty');
			});
		},
		megaMenu: function megaMenu() {
			THEME.megamenu = {
				defaults: {
					header: '.hdr',
					menu: '.mmenu-js',
					submenu: '.mmenu-submenu',
					toggleMenu: '.toggleMenu',
					simpleDropdn: '.mmenu-item--simple',
					megaDropdn: '.mmenu-item--mega',
					headerCart: '.minicart-js',
					headerCartToggleBtn: '.minicart-link',
					headerCartDropdn: '.minicart-drop',
					dropdn: '.dropdn',
					titleHeight: 50
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					if ($(this.defaults.menu).length) {
						this._handlers(this);
						if ($(this.defaults.menu).find('.menu-label').length) {
							$(this.defaults.menu).addClass('mmenu--withlabels');
						}
					}
				},
				_handlers: function _handlers(menu) {
					function setMaxHeight(wHeight, submenu) {
						if ($menu.hasClass('mmenu--vertical')) return false;
						if (submenu.length) {
							var maxH = $body.hasClass('has-sticky') ? wHeight - $('.hdr-content-sticky').outerHeight() : wHeight - submenu.prev().offset().top - submenu.prev().outerHeight();
							submenu.children(':first').css({
								'max-height': maxH + 'px'
							});
						}
					}

					function clearMaxHeight() {
						$submenu.each(function () {
							var $this = $(this);
							$this.css({
								'max-height': ''
							});
						});
					}

					var $menu = $(menu.defaults.menu),
					    submenu = menu.defaults.submenu,
					    $submenu = $(menu.defaults.submenu, $menu),
					    $header = $(menu.defaults.header),
					    $toggleMenu = $(menu.defaults.toggleMenu),
					    megaDropdnClass = menu.defaults.megaDropdn,
					    simpleDropdnClass = menu.defaults.simpleDropdn,
					    vertical = menu.defaults.vertical,
					    $headerCart = $(menu.defaults.headerCart),
					    $headerCartToggleBtn = $headerCart.find(menu.defaults.headerCartToggleBtn),
					    $headerCartDropdn = $headerCart.find(menu.defaults.headerCartDropdn),
					    $dropdn = $(menu.defaults.dropdn, $header),
					    timer = void 0;
					$menu.on('mouseenter.mmenu', megaDropdnClass + '> a,' + simpleDropdnClass + '> a', function () {
						var $this = $(this);
						timer = setTimeout(function () {
							$submenu = $this.next(submenu);
							setMaxHeight($window.height(), $submenu);
							$submenu.scrollTop(0);
							$this.parent('li').addClass('hovered');
							$submenu.find('.mmenu-col').each(function (i) {
								var $this = $(this);
								anime({
									targets: $this[0],
									opacity: [0, 1],
									translateY: ['80px', '0'],
									translateZ: 0,
									translateX: 0,
									easing: 'cubicBezier(0.165, 0.84, 0.44, 1)',
									duration: 650,
									delay: i * 100,
									complete: function complete() {
										$this.css({ 'transform': 'none' });
									}
								});
							});
							if ($headerCartDropdn.hasClass('opened')) {
								$headerCartToggleBtn.trigger('click');
							}
							$dropdn.each(function () {
								var $this = $(this);
								if ($this.hasClass('is-hovered')) {
									$('>a', $this).trigger('click');
								}
							});
						}, 200);
					}).on('mouseleave.mmenu', megaDropdnClass + ',' + simpleDropdnClass, function () {
						var $this = $(this);
						clearTimeout(timer);
						clearMaxHeight();
						$this.removeClass('hovered');
					});
					$toggleMenu.on('click', function (e) {
						var $this = this;
						$header.toggleClass('open');
						$this.toggleClass('open');
						$menu.addClass('disable').delay(1000).queue(function () {
							$this.removeClass('disable').dequeue();
						});
						e.preventDefault();
					});
					$('li', $submenu).on('mouseenter', function () {
						var $this = $(this).addClass('hovered');
						if ($('> a .mmenu-preview', $this).length) {
							var $ul = $this.closest('ul'),
							    $img = $('.mmenu-preview', $this);
							$ul.css({
								'min-width': '',
								'overflow': ''
							});
							$ul.css({
								'min-width': 454,
								'overflow': 'hidden'
							});
							$ul.append($img.clone());
						}
						if ($('> .submenu-link-image', $this).length) {
							var $elm = $('> .submenu-link-image', $this),
							    isXvisible = $body.hasClass('rtl') ? $elm.offset().left >= 0 : $elm.offset().left + $elm.width() <= w,
							    isYvisible = h + $window.scrollTop() - ($elm.offset().top + $elm.outerHeight());
							if (!isXvisible) {
								$elm.addClass('to-right');
							} else {
								$elm.removeClass('to-right');
							}
							if (isYvisible < 0) {
								$elm.css({
									'margin-top': isYvisible + 'px'
								});
							}
						} else if ($('ul', $this).length) {
							var _$elm = $('.mmenu-submenu', $this).length ? $('.mmenu-submenu', $this) : $('ul:first', $this),
							    _isXvisible = 0,
							    _isYvisible = 0;
							if ($this.closest('.mmenu-item--mega').length && $this.parent().hasClass('submenu-list')) {
								if (!$body.hasClass('rtl')) {
									_$elm.css({
										top: $this.offset().top - $this.closest('.mmenu-submenu').offset().top,
										left: $this.offset().left + $this.outerWidth()
									});
								} else {
									_$elm.css({
										top: $this.offset().top - $this.closest('.mmenu-submenu').offset().top,
										left: $this.offset().left - _$elm.outerWidth()
									});
								}
							}
							_isXvisible = $body.hasClass('rtl') ? _$elm.offset().left >= 0 : _$elm.offset().left + _$elm.width() <= w;
							_isYvisible = h + $window.scrollTop() - (_$elm.offset().top + _$elm.outerHeight());
							if (!_isXvisible) {
								$this.addClass('to-right');
							} else {
								$this.removeClass('to-right');
							}
							if (_isYvisible < 0) {
								_$elm.css({
									'margin-top': _isYvisible + 'px'
								});
							}
						}
					}).on('mouseleave', function () {
						var $elm = $('.mmenu-submenu', this).length ? $('.mmenu-submenu', this) : $('ul:first', this);
						var $this = $(this).removeClass('to-right').removeClass('hovered');
						$('.submenu-link-image', $(this)).removeClass('to-right');
						if ($('> a .mmenu-preview', $this).length) {
							var $ul = $this.closest('ul');
							$ul.css({
								'min-width': '',
								'overflow': ''
							});
							$ul.find('>.mmenu-preview').remove();
						}
						$elm.css({
							'margin-top': ''
						});
						if (!$this.closest('.sub-level').length) {
							$elm.closest('.mmenu-submenu').removeClass('mmenu--not-hide').css({
								'padding-right': ''
							});
						}
						if ($('.submenu-link-image', $this).length) {
							$('.submenu-link-image', $this).css({
								'margin-top': ''
							});
						}
					});
				}
			};
			THEME.megamenu.init();
		},
		mobileMenu: function mobileMenu() {
			THEME.mobilemenu = {
				defaults: {
					mobilemenu: '.mobilemenu',
					toggleMenu: '.mobilemenu-toggle',
					search: '.search_container_desktop',
					searchOpenMobile: '.search_container_mobile',
					mobileMenuScroll: '.mobilemenu-scroll'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					var obj = {
						$mobilemenu: $(this.defaults.mobilemenu),
						$toggleMenu: $(this.defaults.toggleMenu),
						$lang: $(this.defaults.lang),
						$currency: $(this.defaults.currency),
						$langMobile: $(this.defaults.langMobile),
						$currencyMobile: $(this.defaults.currencyMobile),
						$search: $(this.defaults.search),
						$searchOpenMobile: $(this.defaults.searchOpenMobile)
					};
					$.extend(this.defaults, obj);
					if (this.defaults.$mobilemenu.length) {
						this._handlers(this);
					}
				},
				_handlers: function _handlers() {
					var _ = this.defaults,
					    $scroll = $(_.$mobilemenu);
					$(_.mobileMenuScroll).perfectScrollbar();
					_.$toggleMenu.on('click.mobileMenu', function () {
						_.$mobilemenu.toggleClass('active');
						_.$toggleMenu.toggleClass('active');
						$body.toggleClass('slidemenu-open');
						if (isMobile) {
							if ($body.hasClass('slidemenu-open')) {
								bodyScrollLock.disableBodyScroll($scroll);
							} else {
								bodyScrollLock.clearAllBodyScrollLocks();
							}
						}
						return false;
					});
					_.$mobilemenu.on('click.mobileMenu', function (e) {
						if ($(e.target).is(_.$mobilemenu)) {
							_.$mobilemenu.toggleClass('active');
							_.$toggleMenu.toggleClass('active');
							$body.toggleClass('slidemenu-open');
							if (isMobile) bodyScrollLock.clearAllBodyScrollLocks();
							e.preventDefault();
						}
					});
				}
			};
			THEME.mobilemenu.init();
		},
		mmobilePush: function mmobilePush() {
			THEME.mobilemenupush = {
				defaults: {
					initElem: ".mobilemenu",
					menuTitle: "Menu",
					curItem: 0,
					curLevel: 0
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					var that = this,
					    $mmenu = $(that.defaults.initElem);
					if ($mmenu.length && isMobile) {
						$mmenu.data('init', true);
						that._clickHandlers();
						that._updateMenuTitle();
						$('.nav-wrapper').css({
							"height": $('.nav-wrapper ul.nav').outerHeight()
						});
					}
				},
				reinit: function reinit() {
					if (!$(this.defaults.initElem).data('init') == true) {
						this.init();
					} else {
						this._setHeigth();
					}
				},
				_setHeigth: function _setHeigth() {
					$('.nav-wrapper').css({
						"height": $('mmenu-submenu-active .nav-level-' + (this.defaults.curLevel + 1)).outerHeight()
					});
				},
				_clickHandlers: function _clickHandlers() {
					var that = this,
					    $mmenu = $(that.defaults.initElem);
					$mmenu.on('click', 'a', function (e) {
						if ($(e.target).parent('li').find('ul').length) {
							e.preventDefault();
							that.defaults.curItem = $(this).parent();
							that._updateActiveMenu();
						}
					});
					$mmenu.on('click', '.nav-toggle', function () {
						that._updateActiveMenu('back');
					});
					$mmenu.on('click', '.nav-viewall', function (e) {
						e.stopPropagation();
						location.href = $(this).attr('href');
					});
				},
				_updateActiveMenu: function _updateActiveMenu(direction) {
					var that = this;
					that._slideMenu(direction);
					if (direction === 'back') {
						var curItem = that.defaults.curItem;
						setTimeout(function () {
							curItem.removeClass('mmenu-submenu-open mmenu-submenu-active');
						}, 300);
						that.defaults.curItem = that.defaults.curItem.parent().closest('li');
						curItem.addClass('mmenu-submenu-open mmenu-submenu-active');
						that._updateMenuTitle();
					} else {
						that.defaults.curItem.addClass('mmenu-submenu-open mmenu-submenu-active');
						that._updateMenuTitle();
					}
				},
				_updateMenuTitle: function _updateMenuTitle() {
					var that = this,
					    title = that.defaults.menuTitle,
					    $mmenu = $(that.defaults.initElem),
					    link = '';
					if (that.defaults.curLevel > 0) {
						title = that.defaults.curItem.children('a').html();
						link = that.defaults.curItem.children('a').attr('href') ? that.defaults.curItem.children('a').attr('href') : '';
						$mmenu.find('.nav-toggle').addClass('back-visible');
					} else {
						$mmenu.find('.nav-toggle').removeClass('back-visible');
					}
					$('.nav-title').html(title);
					$('.nav-viewall').attr('href', link);
				},
				_updateHeight: function _updateHeight() {
					var that = this,
					    $mmenu = $(that.defaults.initElem);
					if (that.defaults.curLevel > 0) {
						that.defaults.curItem.children('ul').css({
							"padding-top": $mmenu.find('.nav-toggle').outerHeight()
						});
						$('.nav-wrapper').css({
							"height": that.defaults.curItem.children('ul').outerHeight()
						});
					} else {
						$('.nav-wrapper').css({
							"height": $('.nav-wrapper .nav-level-1').outerHeight()
						});
					}
				},
				_slideMenu: function _slideMenu(direction) {
					var that = this,
					    $mmenu = $(that.defaults.initElem);
					if (direction === 'back') {
						that.defaults.curLevel = that.defaults.curLevel > 0 ? that.defaults.curLevel - 1 : 0;
						setTimeout(function () {
							that._updateHeight();
						}, 300);
					} else {
						that.defaults.curLevel += 1;
						setTimeout(function () {
							that._updateHeight();
						}, 100);
					}
					if (!$('body').hasClass('rtl')) {
						$mmenu.children('ul').css({
							"transform": "translateX(-" + that.defaults.curLevel * 100 + "%)"
						});
					} else {
						$mmenu.children('ul').css({
							"transform": "translateX(" + that.defaults.curLevel * 100 + "%)"
						});
					}
				}
			};
			THEME.mobilemenupush.init({
				initElem: ".js-push-mbmenu .nav-wrapper"
			});
		},
		headerDrop: function headerDrop() {
			THEME.headerdrop = {
				defaults: {
					header: '.hdr',
					dropLink: '.js-dropdn-link, .js-open-drop',
					dropLinkParent: '.dropdn',
					dropClose: '.js-dropdn-close',
					dropContent: '.dropdn-content',
					dropContentScroll: '.js-dropdn-content-scroll',
					overlay: '.drop-overlay',
					popupCheckCart: '.popup-addedtocart_actions',
					stickyOffset: 10
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					this.reinitHeight();
					this._handlers();
				},
				reinit: function reinit() {
					this.reinitHeight();
					this._handlers();
					return this;
				},
				_handlers: function _handlers() {
					var that = this,
					    $dropLink = $(that.defaults.dropLink),
					    $dropLinkParent = $dropLink.closest(that.defaults.dropLinkParent),
					    $dropClose = $(that.defaults.dropClose),
					    $scroll = $(that.defaults.dropContentScroll),
					    $popupCheckCart = $(that.defaults.popupCheckCart);
					$(that.defaults.dropContentScroll).perfectScrollbar();
					if (isMobile) {
						if (!$dropLink.data('mclick')) {
							$dropClose.off('.dropdn');
							$dropLink.off('.dropdn');
							$dropLinkParent.off('.dropdn');
							$document.off('.dropdn');
							$window.off('.dropdn');
							$dropLink.on('click.dropdn', function (e) {
								bodyScrollLock.disableBodyScroll($scroll);
								var $this = $(this),
								    $panel = $($this.data('panel')).length ? $($this.data('panel')) : $this.next();
								if ($this.closest('.js-popupAddToCart').length) {
									THEME.selectModal && THEME.selectModal.ifOpened() && THEME.selectModal.close();
									THEME.checkOutModal && THEME.checkOutModal.ifOpened() && THEME.checkOutModal.close();
									THEME.selectSticky && THEME.selectSticky.hide();
									$.fancybox.close();
								}
								if ($this.closest('.mobilemenu').length) {
									$this.parent().toggleClass('is-hovered');
								} else if ($panel.length) {
									if ($this.parent().hasClass('is-hovered')) {
										$this.parent().removeClass('is-hovered');
										$panel.removeClass('is-opened');
										$body.removeClass('blockSticky');
									} else {
										$dropLink.parent().removeClass('is-hovered');
										$this.parent().addClass('is-hovered');
										$panel.addClass('is-opened');
										$body.addClass('blockSticky');
									}
									if ($this.parent().hasClass('dropdn_search')) {
										setTimeout(function () {
											$this.next().find('.search-input').focus();
										}, 100);
									}
								}
								e.preventDefault();
								e.stopPropagation();
							});
							$dropLinkParent.on('click.dropdn', function (e) {
								var $this = $(e.target);
								if (!$this.closest('.dropdn-content').length && $('.header-side-panel').find('.is-opened').length && !$this.closest('.mobilemenu').length) {
									$dropLinkParent.removeClass('is-hovered');
									$('.header-side-panel').find('.is-opened').removeClass('is-opened');
									$body.removeClass('blockSticky');
									e.preventDefault();
								}
							});
							$dropClose.on('click.dropdn', function (e) {
								if (!$(this).closest('.mobilemenu').length) {
									$dropLink.parent().removeClass('is-hovered');
									$('.header-side-panel').find('.is-opened').removeClass('is-opened');
									$body.removeClass('blockSticky');
									bodyScrollLock.clearAllBodyScrollLocks();
								}
								e.preventDefault();
							});
							$dropLink.data('mclick', true);
							$dropLink.removeData('click');
						}
					} else {
						if ($dropLinkParent.closest('.dropdn_fullheight')) {
							$dropLinkParent.find('.dropdn-content-block').perfectScrollbar();
						}
						if (!$dropLink.data('click')) {
							$dropClose.off('.dropdn');
							$dropLink.off('.dropdn');
							$dropLinkParent.on('.dropdn');
							$dropLinkParent.off('.dropdn');
							$dropLink.on('click.dropdn', function (e) {
								var $this = $(this),
								    $panel = $($this.data('panel')).length ? $($this.data('panel')) : $this.next();
								if ($this.closest('.js-popupAddToCart').length) {
									THEME.selectModal && THEME.selectModal.ifOpened() && THEME.selectModal.close();
									THEME.checkOutModal && THEME.checkOutModal.ifOpened() && THEME.checkOutModal.close();
									THEME.selectSticky && THEME.selectSticky.hide();
									$.fancybox.close();
								}
								if ($panel.length) {
									if ($('.header-side-panel').find('.is-opened').length) {
										$('.header-side-panel').find('.is-opened').removeClass('is-opened');
									}
									if ($this.parent().hasClass('is-hovered')) {
										$this.parent().removeClass('is-hovered');
										$panel.removeClass('is-opened');
										setTimeout(function () {
											$this.next().find('.search-input').focus().val('');
										}, 500);
									} else {
										$dropLink.parent().removeClass('is-hovered');
										$this.parent().addClass('is-hovered');
										$panel.addClass('is-opened');
										$this.next().css({
											'max-height': that._getDropHeight($this) + 'px',
											'top': that._getDropPos($this) + 'px'
										});
										if ($this.closest('.dropdn_fullheight').length) {
											$this.next().css({
												'height': that._getDropHeight($this) + 'px'
											});
										}
										$this.next().find('.search-input').focus();
									}
									e.preventDefault();
									e.stopPropagation();
								}
							});
							$document.on('click.dropdn', function (e) {
								var $this = $(e.target);
								if (!$this.closest('.dropdn-content').length) {
									if ($popupCheckCart.hasClass('is-hovered')) $popupCheckCart.removeClass('is-hovered');
									if ($('.header-side-panel').find('.is-opened').length) {
										$dropLinkParent.removeClass('is-hovered');
										$('.header-side-panel').find('.is-opened').removeClass('is-opened');
									} else if ($dropLinkParent.hasClass('is-hovered')) {
										$dropLinkParent.removeClass('is-hovered');
										$dropLink.next().css({
											'height': '',
											'max-height': ''
										});
										setTimeout(function () {
											if ($this.next().find('.search-input').length) {
												$this.next().find('.search-input').val('');
											}
										}, 500);
									}
								}
							});
							$dropClose.on('click.dropdn', function (e) {
								var $this = $(e.target);
								$dropLink.parent().removeClass('is-hovered');
								$('.header-side-panel').find('.is-opened').removeClass('is-opened');
								if ($popupCheckCart.hasClass('is-hovered')) $popupCheckCart.removeClass('is-hovered');
								$dropLink.next().css({
									'height': '',
									'max-height': ''
								});
								setTimeout(function () {
									$this.next().find('.search-input').val('');
								}, 500);
								THEME.selectSticky && THEME.selectSticky.show();
								e.preventDefault();
							});
							$dropLink.data('click', true);
							$dropLink.removeData('mclick');
						}
					}
				},
				_getDropHeight: function _getDropHeight(dropdn) {
					var dropH = void 0,
					    yPos = void 0,
					    $parent = dropdn.closest('.container');
					if ($parent.length && dropdn.closest('.dropdn_fullheight,.dropdn_fullwidth').length) {
						if (!$('.hdr-content-sticky').length) {
							yPos = $parent.outerHeight() + $parent.offset().top - $window.scrollTop();
							if (yPos < 0) {
								yPos = 0;
							}
						} else {
							yPos = 0;
						}
					}
					dropH = h - yPos;
					return dropH;
				},
				_getDropPos: function _getDropPos(dropdn) {
					var that = this,
					    $header = $(that.defaults.header),
					    $parent = dropdn.closest('.container'),
					    yPos = void 0;
					if ($parent.length) {
						if (dropdn.closest('.dropdn_fullheight').length) {
							yPos = 0;
						} else if (dropdn.closest('.dropdn_fullwidth').length) {
							if (dropdn.closest('.hdr-content-sticky').length) {
								yPos = $('.hdr-content-sticky').outerHeight();
							} else if (dropdn.closest('.hdr-navline').length) {
								yPos = $parent.outerHeight() + $parent.offset().top;
							} else if ($header.hasClass('hdr-style4') || $header.hasClass('hdr-style7')) {
								yPos = $parent.outerHeight() + $('.hdr-navline').outerHeight();
							} else yPos = $parent.outerHeight();
						}
					}
					return yPos;
				},
				reinitHeight: function reinitHeight() {
					var that = this,
					    $this = $(that.defaults.dropLinkParent);
					$this.each(function () {
						var _ = $(this).find(that.defaults.dropContent);
						if (!isMobile) {
							if (!$('.hdr-content-sticky').length) {
								_.css({
									'max-height': that._getDropHeight(_.prev()) + 'px',
									'top': that._getDropPos(_.prev()) + 'px'
								});
								if (_.closest('.dropdn_fullheight').length) {
									_.css({
										'height': that._getDropHeight(_.prev()) + 'px'
									});
								}
							} else {
								_.css({
									'max-height': that._getDropHeight(_.prev()) + 'px',
									'top': that._getDropPos(_.prev()) + 'px'
								});
								if (_.closest('.dropdn_fullheight').length) {
									_.css({
										'height': that._getDropHeight(_.prev()) + 'px'
									});
								}
							}
						}
					});
				}
			};
			THEME.headerdrop.init();
		},
		stickyHeaderInit: function stickyHeaderInit(el) {
			if ($(el).length) {
				THEME.stickyheader = {
					defaults: {
						header: '.hdr',
						headerSticky: '.hdr-content-sticky',
						headerM: '.hdr-mobile',
						headerD: '.hdr-desktop',
						hdrNav: '.nav-holder',
						stickyNav: '.nav-holder-s',
						mobileMenu: '.mmenu',
						promoTopline: '.promo-topline',
						drops: '.hdr-content-sticky .dropdn-content',
						stickyCollision: '.js-sticky-collision',
						offset: 500
					},
					init: function init(options) {
						$.extend(this.defaults, options);
						if ($(this.defaults.headerSticky).length) {
							$body.addClass('has-hdr_sticky');
						}
						if (!isMobile && !$body.hasClass('has-sticky')) {
							this._setHeigth();
						}
						this._setScroll();
						if ($(this.defaults.stickyNav).length) {
							$(this.defaults.hdrNav).children().clone(true).appendTo(this.defaults.stickyNav);
						}
						if (!isMobile) {
							this._multirow();
							this._multirowS();
						}
						return this;
					},
					reinit: function reinit() {
						if (!$(this.defaults.header).length) return false;
						$window.off('scroll.stickyHeader');
						if (!isMobile && !$body.hasClass('has-sticky')) {
							this._setHeigth();
						}
						if (!isMobile) {
							this._multirow();
							this._multirowS();
						}
						this._setScroll();
						return this;
					},
					_multirow: function _multirow() {
						if ($(this.defaults.hdrNav).height() > 60) {
							$(this.defaults.header).addClass('mmenu-multirow');
						} else $(this.defaults.header).removeClass('mmenu-multirow');
					},
					_multirowS: function _multirowS() {
						if ($body.hasClass('has-hdr_sticky')) {
							if ($(this.defaults.stickyNav).height() > 60) {
								$(this.defaults.headerSticky).addClass('mmenu-multirow-s');
							} else $(this.defaults.headerSticky).removeClass('mmenu-multirow-s');
						}
					},
					destroySticky: function destroySticky() {
						this._removeSticky();
					},
					_setScroll: function _setScroll() {
						var that = this,
						    offset = that.defaults.offset + $(that.defaults.headerSticky).outerHeight(),
						    checkColission = !isMobile || $(that.defaults.stickyCollision).length ? true : false;

						function isCollapsed(el1, el2) {
							if (!$body.hasClass('has-sticky')) return false;
							if (el1.offset().top < el2.offset().top + el2.height()) {
								return true;
							} else return false;
						}

						function scrollEvents() {
							if ($body.hasClass('blockSticky')) return false;
							var st = $window.scrollTop();
							if (st > offset) {
								if (!$body.hasClass('has-sticky')) {
									that._setSticky();
								}
							} else {
								if ($body.hasClass('has-sticky')) {
									that._removeSticky();
								}
							}
						}

						$window.on('scroll.stickyHeader', function () {
							if ($body.hasClass('blockSticky')) return false;
							scrollEvents();
							if (isMobile) return false;
							if (checkColission) {
								$(that.defaults.stickyCollision).each(function () {
									var $this = $(this),
									    h = parseInt($(that.defaults.headerSticky).outerHeight(), 10);
									if (isCollapsed($this, $(that.defaults.headerSticky))) {
										$this.addClass('is-collision');
										if ($this.hasClass('js-filter-col')) {
											h = h + 40;
											$this.css({
												"-webkit-transform": "translate3d(0," + h + "px,0)",
												"-ms-transform": "translate3d(0," + h + "px,0)",
												"transform": "translate3d(0," + h + "px,0)",
												"padding-bottom": h + "px"
											});
										} else {
											$this.children().css({
												"-webkit-transform": "translate3d(0," + h + "px,0)",
												"-ms-transform": "translate3d(0," + h + "px,0)",
												"transform": "translate3d(0," + h + "px,0)",
												"padding-bottom": h + "px"
											});
										}
									} else {
										$this.removeClass('is-collision');
										if ($this.hasClass('js-filter-col')) {
											$this.css({
												"-webkit-transform": "translate3d(0,0,0)",
												"-ms-transform": "translate3d(0,0,0)",
												"transform": "translate3d(0,0,0)",
												"padding-bottom": ""
											});
										} else {
											$this.children().css({
												"-webkit-transform": "translate3d(0,0,0)",
												"-ms-transform": "translate3d(0,0,0)",
												"transform": "translate3d(0,0,0)",
												"padding-bottom": ""
											});
										}
									}
								});
							}
						});
						scrollEvents();
						return this;
					},
					_setTop: function _setTop() {
						var that = this,
						    top = $('.js-hdr-top:visible').outerHeight();
						$(that.defaults.header).css({
							'top': 0 - top
						});
						$(that.defaults.drops).css({
							'top': top
						});
					},
					_setSticky: function _setSticky() {
						var that = this;
						$body.addClass('has-sticky');
						$(that.defaults.header).find('.is-hovered .dropdn-link').each(function () {
							if (!$(this).closest('.dropdn_fullheight').length) {
								$(this).trigger('click');
							}
						});
					},
					_removeSticky: function _removeSticky() {
						$body.removeClass('has-sticky');
					},
					_setHeigth: function _setHeigth() {
						var $header = $(this.defaults.header),
						    $hdrNav = $(this.defaults.hdrNav);
						$hdrNav.css({
							'height': ''
						});
						$header.css({
							'height': ''
						});
						if (!$body.hasClass('has-sticky')) {
							$hdrNav.css({
								'height': $hdrNav.outerHeight()
							});
						} else {
							$body.removeClass('has-sticky');
						}
						return this;
					}
				};
				THEME.stickyheader.init();
			} else {
				THEME.stickyheader = {
					defaults: {
						header: '.hdr',
						hdrNav: '.hdr-nav'
					},
					init: function init(options) {
						$.extend(this.defaults, options);
						this._multirow();
						return this;
					},
					reinit: function reinit() {
						this._multirow();
						return this;
					},
					_multirow: function _multirow() {
						if (isMobile) return false;
						if ($(this.defaults.hdrNav).outerHeight() > 60) {
							$(this.defaults.header).addClass('mmenu-multirow');
						} else $(this.defaults.header).removeClass('mmenu-multirow');
					}
				};
				THEME.stickyheader.init();
			}
		}
	};
	THEME.modals = {
		init: function init() {
			this.quickView();
			this.checkOut();
			this.addToWishlist('.js-label-wishlist');
			this.selectModal();
			this.formNotifications();
			this.infoModals();
			this.loaderHorizontal();
			this.loaderHorizontalSm();
			this.loaderCategory();
			this.loaderTab();
		},
		infoModals: function infoModals() {
			$('.modal-info-link').fancybox({
				type: 'inline',
				buttons: ['close'],
				touch: false,
				backFocus: false,
				btnTpl: {
					smallBtn: '<div data-fancybox-close class="fancybox-close-small modal-close"><i class="icon-close"></i></div>'
				}
			});
		},
		formNotifications: function formNotifications() {
			setTimeout(function () {
				if ($('.js-form-notification').length) {
					$.fancybox.open([{
						src: '.js-form-notification',
						type: 'inline',
						btnTpl: {
							smallBtn: '<div data-fancybox-close class="fancybox-close-small modal-close"><i class="icon-close"></i></div>'
						}
					}], {
						buttons: ['close'],
						touch: false,
						afterClose: function afterClose() {
							var uri = window.location.toString();
							if (uri.indexOf("?") > 0) {
								var clean_uri = uri.substring(0, uri.indexOf("?"));
								window.history.replaceState({}, document.title, clean_uri);
							}
							if ($('.js-form-notification .js-scroll-to').length) {
								THEME.initialization.scrollToTarget('.js-scroll-to');
								$('.js-form-notification .js-scroll-to').trigger('click');
							}
						}
					});
				}
				if ($('.js-form-contact-notification').length) {
					$.fancybox.open([{
						src: '.js-form-contact-notification',
						type: 'inline',
						btnTpl: {
							smallBtn: '<div data-fancybox-close class="fancybox-close-small modal-close"><i class="icon-close"></i></div>'
						}
					}], {
						buttons: ['close'],
						touch: false,
						afterClose: function afterClose() {
							var uri = window.location.toString();
							if (uri.indexOf("?") > 0) {
								var clean_uri = uri.substring(0, uri.indexOf("?"));
								window.history.replaceState({}, document.title, clean_uri);
							}
						}
					});
				}
			}, 1000);
		},
		quickView: function quickView() {
			THEME.quickView = {
				init: function init(options) {
					var that = this,
					    $modalQuickView = $('#modalQuickView');
					var $mainCarousel = $('.js-product-main-carousel-qw'),
					    $prwCarousel = $('.js-product-previews-carousel-qw');
					$document.on('click', '.js-prd-quickview', function (e) {
						var src = $(this).data('src');
						$.fancybox.open({
							src: src,
							backFocus: false,
							type: 'ajax',
							image: {
								preload: true
							},
							afterShow: function afterShow() {
								THEME.product.productPageGallery(true);
								THEME.product.swatchToggle('#modalQuickView .prd-color.swatches');
								setTimeout(function () {
									$('.quickview-gallery').css({ 'opacity': '1' });
								}, 500);

								if ($('.js-product-previews-carousel-qw').length) {
									$('.js-product-previews-carousel-qw').slick('setPosition');
								}
								THEME.initialization.countdown('#modalQuickView .js-countdown');
								THEME.productPage.visitorsNow();
								THEME.initialization.tooltipIni($('#modalQuickView [data-toggle="tooltip"]'));
								that._setScroll();
								if (isMobile) bodyScrollLock.disableBodyScroll($modalQuickView);
							},
							beforeShow: function beforeShow() {
								THEME.selectModal && THEME.selectModal.ifOpened() && THEME.selectModal.close();
								THEME.checkOutModal && THEME.checkOutModal.ifOpened() && THEME.checkOutModal.close();
								THEME.selectSticky && THEME.selectSticky.hide();
								$body.addClass('fancybox--under-modals');
							},
							beforeClose: function beforeClose() {
								$('.zoom-container').remove();
								THEME.productpagegallery_qw.destroy();
								delete THEME.productpagegallery_qw;
							},
							afterClose: function afterClose() {
								THEME.selectSticky ? THEME.selectSticky.show() : false;
								THEME.productpagegallery ? THEME.productpagegallery.reinitZoom() : false;
								$body.removeClass('fancybox--under-modals');
								if (isMobile) bodyScrollLock.enableBodyScroll($modalQuickView);
							},

							touch: false,
							btnTpl: {
								smallBtn: '<div data-fancybox-close class="fancybox-close-small modal-close"><i class="icon-close"></i></div>'
							},
							baseTpl: '<div class="fancybox-container">' + '<div class="fancybox-bg"></div>' + '<div class="fancybox-inner">' + '<div class="fancybox-stage"></div>' + "</div>" + "</div>"
						});
						e.preventDefault();
					});
				},
				reinit: function reinit() {
					if (this.ifOpened()) {
						$('#modalQuickView .quickview-info').perfectScrollbar('destroy');
						$('#modalQuickView .prd-block').perfectScrollbar('destroy');
						$('#modalQuickView .quickview-info').css({ 'height': '' });
						$('#modalQuickView .prd-block').css({ 'height': '' });
						this._setScroll();
					}
				},
				ifOpened: function ifOpened() {
					if ($('#modalQuickView').length && $('#modalQuickView').closest('.fancybox-is-open').length) {
						return true;
					} else {
						return false;
					}
				},
				_setScroll: function _setScroll() {
					if (w > maxMD) {
						if ($('.modal-quickview--classic').length) {
							var h = $('#modalQuickView .quickview-info')[0].scrollHeight;
							$('#modalQuickView .quickview-info').css({ 'min-height': h, 'height': h });
						} else {
							$('#modalQuickView .quickview-info').css({ 'height': $('.modal--quickview').height() });
						}
						$('#modalQuickView .quickview-info').perfectScrollbar();
					} else {
						$('#modalQuickView .prd-block').css({ 'height': $('.modal--quickview').height() });
						$('#modalQuickView .prd-block').perfectScrollbar();
					}
					$('.js-product-previews-carousel-qw').slick('setPosition');
				}
			};
			THEME.quickView.init();
		},
		checkOut: function checkOut() {
			THEME.checkOutModal = {
				defaults: {
					popup: '.js-popupAddToCart',
					popupAlt: ['.js-stickyAddToCart', '.js-popupSelectOptions'],
					button: '.js-prd-addtocart',
					closePopup: '.js-popupAddToCart-close',
					title: '.popup-addedtocart_title',
					imageWrap: '.popup-addedtocart_image',
					image: '.popup-addedtocart_image > img, .popup-addedtocart_image > span > img',
					error: '.popup-addedtocart_error_message'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					var that = this,
					    $popup = $(that.defaults.popup);
					if ($popup.length) {
						that._handlers();
					}
				},
				reinit: function reinit() {
					if (!$(this.defaults.popup).hasClass('closed')) {
						this._setHeigth();
					}
				},
				setData: function setData(data) {
					var that = this,
					    $popup = $(this.defaults.popup);
					$popup.removeClass('has-error');
					$(this.defaults.title, $popup).empty().html('<b>' + data.name + '</b>');
					$(this.defaults.title, $popup).attr('href', data.url);
					$(this.defaults.imageWrap, $popup).attr('href', data.url);
					$(this.defaults.image, $popup).fadeTo(0, 0).attr('src', '');
					if (data.path) {
						if (data.path.indexOf('no-image') == -1) {
							$(this.defaults.image, $popup).attr('src', data.path);
						} else {
							$(this.defaults.image, $popup).attr('src', data.main_image);
						}
					}
					if (data.aspect_ratio > 0) {
						$(this.defaults.imageWrap, $popup).find('.image-container').css({
							'padding-bottom': 1 / data.aspect_ratio * 100 + "%"
						});
					}
					$(this.defaults.imageWrap, $popup).imagesLoaded(function () {
						anime({
							targets: that.defaults.image,
							opacity: 1,
							duration: 1000
						});
					});
					$(this.defaults.image, $popup).parent().attr('href', data.url);
					$(this.defaults.error, $popup).empty().html(data.error_message);
					if (!$popup.hasClass('closed')) {
						this.open();
					}
				},
				setError: function setError(error) {
					var $popup = $(this.defaults.popup);
					$popup.addClass('has-error');
					$(this.defaults.error, $popup).empty().html(error);
					if (!$popup.hasClass('closed')) {
						this.open();
					}
				},
				ifOpened: function ifOpened() {
					return $(this.defaults.popup).hasClass('closed') ? false : true;
				},
				_correctHeight: function _correctHeight() {
					var that = this,
					    h = $(that.defaults.popup).find('.container').outerHeight();
					anime({
						targets: that.defaults.popup,
						height: {
							value: h + 'px'
						},
						duration: 100,
						easing: 'linear'
					});
				},
				_setHeigth: function _setHeigth() {
					var $popup = $(this.defaults.popup),
					    popupAlt = this.defaults.popupAlt,
					    h = $popup.find('.container').outerHeight();
					for (var i = 0; i < popupAlt.length; ++i) {
						if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
							h += $(popupAlt[i]).find('.container').outerHeight();
						}
					}
					$popup.removeClass('closed');
					$('.js-pn').css({
						'margin-bottom': h + 20 + 'px'
					});
				},
				_handlers: function _handlers() {
					var that = this,
					    $popup = $(that.defaults.popup);
					$(document).on('click', that.defaults.button, function (e) {
						var product_data = $(this).data('product');
						if (!$popup.hasClass('closed')) {
							that.close();
							setTimeout(function () {
								that.setData(product_data);
								that.open();
							}, 500);
						} else {
							that.setData(product_data);
							that.open();
						}
						e.preventDefault();
					});
					$(document).on('click', that.defaults.closePopup, function (e) {
						that.close();
						e.preventDefault();
					});
				},
				open: function open() {
					var that = this,
					    $popup = $(that.defaults.popup),
					    $qty = $popup.find('.qty-input'),
					    h = $popup.find('.container').outerHeight(),
					    duration = 300;
					$popup.removeClass('closed');
					$qty.val($qty.data('min'));
					that._setHeigth();
					anime({
						targets: that.defaults.popup,
						easing: 'linear',
						duration: duration,
						delay: 500,
						height: {
							value: h + 'px'
						}
					});
					if ($popup.attr('data-close')) {
						that.timer = setTimeout(function () {
							that.close();
						}, $popup.attr('data-close'));
					}
				},
				close: function close() {
					var that = this,
					    $popup = $(that.defaults.popup),
					    popupAlt = $(that.defaults.popupAlt),
					    duration = 300,
					    h = 0;
					for (var i = 0; i < popupAlt.length; ++i) {
						if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
							h += $(popupAlt[i]).find('.container').outerHeight();
						}
					}
					$popup.addClass('closed');
					$popup.data('variant-id', '');
					$('.js-pn').css({
						'margin-bottom': h + 20 + 'px'
					});
					clearTimeout(that.timer);
					anime({
						targets: that.defaults.popup,
						easing: 'linear',
						duration: duration,
						height: {
							value: '0px'
						},
						complete: function complete() {
							$(that.defaults.imageWrap, $popup).find('.image-container').css({
								'padding-bottom': ''
							});
						}
					});
				}
			};
			THEME.checkOutModal.init();
		},
		selectModal: function selectModal() {
			function createSelect(el) {
				var SelectModal = {
					defaults: {
						popup: '.js-popupSelectOptions',
						popupAlt: ['.js-stickyAddToCart', '.js-popupAddToCart'],
						button: '.js-select-add-to-cart',
						closePopup: '.js-popupSelectOptions-close',
						title: '.sticky-addcart_title',
						imageWrap: '.sticky-addcart_image',
						image: '.sticky-addcart_image img',
						imageLink: '.sticky-addcart_image a',
						imageZoom: '.js-popupSelectOptions-zoom a',
						error: '.sticky-addcart_error_message',
						options: '.sticky-addcart_options_select',
						priceActual: '.sticky-addcart_price--actual',
						priceOld: '.sticky-addcart_price--old',
						sticky: false
					},
					init: function init(options) {
						$.extend(this.defaults, options);
						var that = this,
						    $popup = $(that.defaults.popup);
						if ($popup.length) {
							that._handlers();
						}
					},
					reinit: function reinit() {
						if (!$(this.defaults.popup).hasClass('closed')) {
							this._setHeigth();
						}
					},
					_setProduct: function _setProduct(data) {
						var that = this,
						    $popup = $(this.defaults.popup);
						$popup.removeClass('has-error');
						if (data) {
							$(that.defaults.title, $popup).find('a').empty().html('<b>' + data.name + '</b>');
							$(that.defaults.title, $popup).find('a').attr('href', data.url);
							if (data.path.indexOf('no-image') == -1) {
								$(that.defaults.image, $popup).attr('src', data.path);
							} else {
								$(that.defaults.image, $popup).attr('src', data.main_image);
							}
							if ($(that.defaults.imageZoom).length) {
								if (data.path_image_big.indexOf('no-image') == -1) {
									$(that.defaults.imageZoom, $popup).attr('href', data.path_image_big);
								} else {
									$(that.defaults.imageZoom, $popup).attr('href', data.main_image_big);
								}
								$(that.defaults.imageZoom, $popup).data('caption', data.name);
							} else $(that.defaults.imageLink, $popup).attr('href', data.url);
							$(that.defaults.error, $popup).empty().html(data.error_message);
							$(that.defaults.priceActual, $popup).empty().html(data.price_actual);
							if (data.price_old && parseInt(data.price_old.replace(/[^0-9]/g, '')) > parseInt(data.price_actual.replace(/[^0-9]/g, ''))) {
								$(that.defaults.priceOld, $popup).empty().html(data.price_old);
							} else $(that.defaults.priceOld, $popup).empty();
						}
					},
					_setFormData: function _setFormData(data) {
						var that = this,
						    $popup = $(this.defaults.popup);
						if (data) {
							var $btn = $('.btn', $popup);
							$btn.data('variant-id', data.id);
							$btn.data('product', data);
							if (that.defaults.sticky) {
								$btn.html(js_helper.strings.addToCart);
								$btn.attr('aria-disabled', false);
							}
						}
					},
					setData: function setData(data) {
						var that = this,
						    $popup = $(this.defaults.popup);
						that._setProduct(data);
						$(that.defaults.options, $popup).find('option').remove();
						$.each(data.variants, function (key, value) {
							var option_status = '';
							if (!value.disabled) option_status = 'disabled';
							$(that.defaults.options, $popup).append($('<option ' + option_status + '>', { value: value.id }).text(key + value.info));
							$(that.defaults.options, $popup).find('option').last().data('variant', value);
						});
						if (data.aspect_ratio > 0) {
							$(this.defaults.imageWrap, $popup).find('.image-container').css({
								'padding-bottom': 1 / data.aspect_ratio * 100 + "%"
							});
						}
						that._setFormData(data);
					},
					setError: function setError(error) {
						var $popup = $(this.defaults.popup);
						$popup.addClass('has-error');
						$(this.defaults.error, $popup).empty().html(error);
						if (!$popup.hasClass('closed')) {
							this.open();
						}
					},
					ifOpened: function ifOpened() {
						return $(this.defaults.popup).hasClass('closed') ? false : true;
					},
					_correctHeight: function _correctHeight() {
						var that = this,
						    h = $(that.defaults.popup).find('.container').outerHeight();
						anime({
							targets: that.defaults.popup,
							height: {
								value: h + 'px'
							},
							duration: 100,
							easing: 'linear'
						});
					},
					_setHeigth: function _setHeigth() {
						var $popup = $(this.defaults.popup),
						    popupAlt = this.defaults.popupAlt,
						    h = $popup.find('.container').outerHeight();
						for (var i = 0; i < popupAlt.length; ++i) {
							if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
								h += $(popupAlt[i]).find('.container').outerHeight();
							}
						}
						$popup.removeClass('closed');
						$('.js-pn').css({
							'margin-bottom': h + 20 + 'px'
						});
					},
					_handlers: function _handlers() {
						var that = this,
						    $popup = $(that.defaults.popup);
						$(that.defaults.options, $popup).on('change', function () {
							that._setProduct($('option:selected', this).data('variant'));
							that._setFormData($('option:selected', this).data('variant'));
							that.setTimer();
						});
						$(document).on('click', that.defaults.imageZoom, function (e) {
							var $this = $(this);
							if ($body.hasClass('fancybox--under-modals')) {
								$popup.data('secondClick', true);
								$.fancybox.close();
							} else $popup.data('secondClick', false);
							$.fancybox.open([{
								src: $this.attr('href'),
								opts: {
									caption: $this.data('caption')
								}
							}], {
								buttons: ['close'],
								touch: false,
								beforeShow: function beforeShow() {
									THEME.checkOutModal && THEME.checkOutModal.close();
									$('.fancybox-container').last().addClass('fancybox--light');
									$body.addClass('fancybox--under-modals');
									if (that.defaults.sticky) {
										$body.addClass('fancybox--from-sticky');
									}
									that.setTimer(60000);
								},
								afterClose: function afterClose() {
									if (!$popup.data('secondClick')) {
										$body.removeClass('fancybox--under-modals fancybox--from-sticky');
									} else {
										$popup.data('secondClick', false);
									}
									that.setTimer();
								}
							});
							e.preventDefault();
						});
						$(document).on('click', that.defaults.closePopup, function (e) {
							that.close(true);
							e.preventDefault();
						});
					},
					setTimer: function setTimer(pause) {
						var that = this,
						    $popup = $(that.defaults.popup);
						clearTimeout(that.timer);
						if (pause) {
							that.timer = setTimeout(function () {
								that.close();
							}, pause);
						} else if ($popup.attr('data-close')) {
							that.timer = setTimeout(function () {
								that.close();
							}, $popup.attr('data-close'));
						}
					},
					open: function open() {
						var that = this,
						    $popup = $(that.defaults.popup),
						    $qty = $popup.find('.qty-input'),
						    h = $popup.find('.container').outerHeight(),
						    duration = 300;
						$popup.removeClass('closed');
						$qty.val($qty.data('min'));
						that._setHeigth();
						anime({
							targets: that.defaults.popup,
							easing: 'linear',
							duration: duration,
							height: {
								value: h + 'px'
							}
						});
						that.setTimer();
					},
					close: function close(all) {
						var that = this,
						    $popup = $(that.defaults.popup),
						    popupAlt = $(that.defaults.popupAlt),
						    duration = 300,
						    h = 0;
						for (var i = 0; i < popupAlt.length; ++i) {
							if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
								h += $(popupAlt[i]).find('.container').outerHeight();
							}
						}
						$popup.addClass('closed');
						$('.js-pn').css({
							'margin-bottom': h + 20 + 'px'
						});
						clearTimeout(that.timer);
						anime({
							targets: that.defaults.popup,
							easing: 'linear',
							duration: duration,
							height: {
								value: '0px'
							},
							complete: function complete() {
								$(that.defaults.imageWrap, $popup).find('.image-container').css({
									'padding-bottom': ''
								});
								if (all) {
									$('.js-stickyAddToCart').remove();
								}
								if (that.defaults.sticky) {
									$popup.remove();
								}
								THEME.stickyaddtocart ? THEME.stickyaddtocart.destroy() : false;
							}
						});
					},
					hide: function hide() {
						var that = this,
						    $popup = $(that.defaults.popup),
						    popupAlt = $(that.defaults.popupAlt),
						    duration = 200,
						    h = 0;
						for (var i = 0; i < popupAlt.length; ++i) {
							if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
								h += $(popupAlt[i]).find('.container').outerHeight();
							}
						}
						$('.js-pn').css({
							'margin-bottom': h + 20 + 'px'
						});
						$popup.data('height', $popup.height());
						if (that.defaults.sticky && $popup.hasClass('closed')) {
							return false;
						}
						$(that.defaults.popup).addClass('modal-hidden');
						anime({
							targets: that.defaults.popup,
							easing: 'linear',
							duration: duration,
							opacity: 0,
							height: {
								value: '0px'
							}
						});
					},
					show: function show() {
						var that = this,
						    $popup = $(that.defaults.popup),
						    popupAlt = $(that.defaults.popupAlt),
						    hPopup = $popup.data('height') ? $popup.data('height') : $popup.find('.container').outerHeight(),
						    duration = 200,
						    h = 0;
						if (that.defaults.sticky && $popup.hasClass('closed')) {
							return false;
						}
						for (var i = 0; i < popupAlt.length; ++i) {
							if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
								h += $(popupAlt[i]).find('.container').outerHeight();
							}
						}
						$('.js-pn').css({
							'margin-bottom': h + hPopup + 20 + 'px'
						});
						$(that.defaults.popup).removeClass('modal-hidden');
						anime({
							targets: that.defaults.popup,
							easing: 'linear',
							duration: duration,
							opacity: 1,
							height: {
								value: hPopup + 'px'
							}
						});
					}
				};
				if (el == 'selectModal') {
					THEME.selectModal = Object.create(SelectModal);
					THEME.selectModal.init();
				} else if (el == 'selectSticky') {
					THEME.selectSticky = Object.create(SelectModal);
					THEME.selectSticky.init({
						popup: '.js-stickyAddToCart',
						imageZoom: '.js-stickyAddcart-zoom a',
						sticky: true,
						closePopup: '.js-stickyAddcart-close',
						popupAlt: ['.js-popupSelectOptions', '.js-popupAddToCart']
					});
				}
			}

			createSelect('selectModal');
			if ($('.js-stickyAddToCart').length) createSelect('selectSticky');
		},
		loaderHorizontal: function loaderHorizontal() {
			THEME.loaderHorizontal = {
				defaults: {
					loader: '.js-loader-horizontal',
					height: 30,
					popupAlt: ['.js-popupAddToCart', '.js-stickyAddToCart', '.js-popupSelectOptions']
				},
				init: function init(options) {
					$.extend(this.defaults, options);
				},
				reinit: function reinit() {
					if (!$(this.defaults.loader).hasClass('closed')) {
						this._setHeigth();
					}
				},
				_setHeigth: function _setHeigth() {
					var $loader = $(this.defaults.loader),
					    popupAlt = $(this.defaults.popupAlt),
					    h = this.defaults.height;
					if (isMobile) h = 50;
					for (var i = 0; i < popupAlt.length; ++i) {
						if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
							h += $(popupAlt[i]).find('.container').outerHeight();
						}
					}
					$loader.removeClass('closed');
					$('.js-pn').css({
						'margin-bottom': h + 20 + 'px'
					});
				},
				open: function open() {
					var that = this,
					    $loader = $(that.defaults.loader),
					    h = this.defaults.height,
					    duration = 200;
					if (isMobile) h = 50;
					$loader.removeClass('closed');
					that._setHeigth();
					THEME.selectModal && THEME.selectModal.ifOpened() && THEME.selectModal.close();
					THEME.checkOutModal && THEME.checkOutModal.ifOpened() && THEME.checkOutModal.close();
					THEME.selectSticky && THEME.selectSticky.hide();
					anime({
						targets: that.defaults.loader,
						easing: 'linear',
						duration: duration,
						height: {
							value: h + 'px'
						}
					});
				},
				close: function close() {
					var that = this,
					    $loader = $(that.defaults.loader),
					    popupAlt = $(that.defaults.popupAlt),
					    duration = 200,
					    h = 0;
					for (var i = 0; i < popupAlt.length; ++i) {
						if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
							h += $(popupAlt[i]).find('.container').outerHeight();
						}
					}
					$loader.addClass('closed');
					$('.js-pn').css({
						'margin-bottom': h + 20 + 'px'
					});
					THEME.selectSticky && THEME.selectSticky.show();
					anime({
						targets: that.defaults.loader,
						easing: 'linear',
						duration: duration,
						height: {
							value: '0px'
						}
					});
				}
			};
			THEME.loaderHorizontal.init();
		},
		loaderHorizontalSm: function loaderHorizontalSm() {
			THEME.loaderHorizontalSm = {
				defaults: {
					loader: '.js-loader-horizontal-sm'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
				},
				open: function open() {
					var that = this,
					    $loader = $(that.defaults.loader),
					    duration = 500;
					$loader.removeClass('d-none');
					anime({
						targets: that.defaults.loader,
						easing: 'linear',
						duration: duration,
						opacity: [0, 1]
					});
				},
				close: function close() {
					var that = this,
					    $loader = $(that.defaults.loader),
					    duration = 500;
					anime({
						targets: that.defaults.loader,
						easing: 'linear',
						duration: duration,
						opacity: [1, 0],
						complete: function complete() {
							$loader.addClass('d-none');
						}
					});
				}
			};
			THEME.loaderHorizontalSm.init();
		},
		loaderCategory: function loaderCategory() {
			THEME.loaderCategory = {
				defaults: {
					grid: '.js-category-grid',
					loader: '.js-loader-horizontal-sm-fixed'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
				},
				open: function open() {
					var that = this,
					    $grid = $(that.defaults.grid),
					    $loader = $grid.next(that.defaults.loader),
					    duration = 500;
					$grid.addClass('disable-actions');
					$loader.removeClass('d-none').addClass('is-centered');
					anime({
						targets: that.defaults.grid,
						easing: 'linear',
						duration: duration,
						opacity: [1, 0.5]
					});
				},
				close: function close() {
					var that = this,
					    $grid = $(that.defaults.grid),
					    $loader = $grid.next(that.defaults.loader),
					    duration = 500;
					anime({
						targets: that.defaults.grid,
						easing: 'linear',
						duration: duration,
						opacity: [0.5, 1],
						complete: function complete() {
							$grid.removeClass('disable-actions');
							$loader.removeClass('is-centered').addClass('d-none');
						}
					});
				}
			};
			THEME.loaderCategory.init();
		},
		loaderTab: function loaderTab() {
			THEME.loaderTab = {
				defaults: {
					grid: '[data-grid-tab-content]',
					loader: '.js-loader-horizontal-sm'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
				},
				open: function open(sectionID, button) {
					var that = this,
					    $grid = $(that.defaults.grid, $('#' + sectionID)),
					    $loader = $grid.find(that.defaults.loader),
					    duration = 500;
					if (!button) {
						$loader = $grid.next(that.defaults.loader);
						$loader.removeClass('d-none');
						$grid.addClass('is-loading');
						$grid.addClass('disable-actions');
						anime({
							targets: '#' + sectionID + ' ' + that.defaults.grid,
							easing: 'linear',
							duration: duration,
							opacity: [1, 0.5]
						});
					} else {
						$loader.removeClass('d-none');
					}
					anime({
						targets: '#' + sectionID + ' ' + that.defaults.loader,
						easing: 'linear',
						duration: duration,
						opacity: [0, 1]
					});
				},
				close: function close(sectionID, button) {
					var that = this,
					    $grid = $(that.defaults.grid, $('#' + sectionID)),
					    $loader = $grid.find(that.defaults.loader),
					    duration = 500;
					if (!button) {
						$loader = $grid.next(that.defaults.loader);
					}
					anime({
						targets: '#' + sectionID + ' ' + that.defaults.loader,
						easing: 'linear',
						duration: duration,
						opacity: [1, 0],
						complete: function complete() {
							$loader.addClass('d-none');
						}
					});
					if (!button) {
						anime({
							targets: '#' + sectionID + ' ' + that.defaults.grid,
							easing: 'linear',
							duration: duration,
							opacity: [0.5, 1],
							complete: function complete() {
								$grid.removeClass('disable-actions');
								$grid.removeClass('is-loading');
							}
						});
					}
				}
			};
			THEME.loaderTab.init();
		},
		addToWishlist: function addToWishlist(link) {
			var $link = $(link),
			    $modalAdd = $('#modalWishlistAdd'),
			    $modalRemoved = $('#modalWishlistRemove');
			$link.on('click', function (e) {
				var $this = $(this),
				    $modal = $this.hasClass('active') ? $modalRemoved : $modalAdd;
				$.fancybox.open($modal, {
					animationDuration: 350,
					touch: false
				});
				$this.toggleClass('active');
				e.preventDefault();
			});
		}
	};
	THEME.product = {
		init: function init() {
			this.productPageGallery(false);
			this.productHoverHeight('.prd');
			this.colorToggle();
			this.actionPrd();
			this.swatchToSelect();
			this.swatchToggle('.prd-color.swatches');
		},
		swatchToSelect: function swatchToSelect() {
			var swatch = '.js-size-list a';
			$document.on('click', swatch, function (e) {
				var $this = $(this),
				    $select = $($this.closest('.swatches').find('select'));
				if (!$this.parent('li').is('.active')) {
					$this.closest('.js-size-list').find('li').removeClass('active');
					$this.parent('li').addClass('active');
					$select.val($this.data('value'));
					$select.trigger('change');
				}
				e.preventDefault();
			});
		},
		swatchToggle: function swatchToggle(option) {
			$(option).each(function () {
				var $option = $(this),
				    $optionlist = $('ul', $option),
				    $optionbtn = $('a', $optionlist),
				    $optionselect = $('select', $option),
				    $productblock = $option.closest('.prd-block'),
				    $carousel = $productblock.find('.product-main-carousel'),
				    $previewsCarousel = $productblock.find('.product-previews-carousel');
				if (!$optionlist.hasClass('js-list-filter')) {
					$optionlist.find("a[data-value='" + $optionselect.val() + "']").parent().addClass('active');
				}
				$optionbtn.on('click touchstart', function (e) {
					var $this = $(this),
					    currentSelect = $this.attr('data-value');
					if (!$optionlist.hasClass('js-list-filter')) {
						if (currentSelect) {
							$carousel.find('.slick-slide').each(function (i) {
								if ($(this).attr('data-value') == currentSelect) {
									$carousel.slick('slickGoTo', i);
									return false;
								}
							});
						}
					} else {
						$carousel.slick('slickUnfilter');
						$previewsCarousel.slick('slickUnfilter');
						$carousel.slick('slickFilter', 'div[data-value="' + currentSelect + '"]');
						$previewsCarousel.slick('slickFilter', 'a[data-value="' + currentSelect + '"]');
						$carousel.slick('refresh');
						$previewsCarousel.slick('refresh');
						if ($previewsCarousel.find('.slick-arrow:not(.slick-disabled)').length) {
							$previewsCarousel.removeClass('fixedOnSelect');
						} else {
							$previewsCarousel.addClass('fixedOnSelect');
						}
					}
					if (!$this.parent('li').is('.active')) {
						$optionselect.val($this.attr('data-value'));
						$this.closest('ul').find('li').removeClass('active');
						$this.parent('li').addClass('active');
						var text = $this.attr('data-original-title') ? $this.attr('data-original-title') : currentSelect;
						$this.closest('.prd-block_options').find('.prd-block_options_selected').html(text);
					}
					e.preventDefault();
				});
			});
		},
		postAjaxProduct: function postAjaxProduct() {
			THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
			THEME.initialization.countdown('.prd .js-countdown');
			THEME.initialization.removeEmptyLinked('.colorswatch-label', 'ul');
			THEME.initialization.tooltipIni('.prd [data-toggle="tooltip"]');
			if ($('.js-circle-loader').length) THEME.catalog.setPersentCircle();
		},
		colorToggle: function colorToggle() {
			THEME.colortoggle = {
				defaults: {
					toggle: '.js-color-toggle',
					image: '.js-prd-img',
					colorswatch: '.color-swatch',
					product: '.prd, .prd-hor, .prd-single'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					this._handlers();
				},
				_handlers: function _handlers() {
					var that = this;
					$document.on('click', that.defaults.toggle, function (e) {
						e.preventDefault();
					}).on('mouseenter', that.defaults.toggle, function (e) {
						var $el = $(this).parent('li');
						if ($el.hasClass('active')) return false;
						if ($el.data('image')) {
							var $prd = $el.closest(that.defaults.product),
							    $image = $prd.find(that.defaults.image),
							    imgSrc = $el.data('image'),
							    widthAuto = false;
							if ($image.length > 1) {
								$image.first().remove();
							}
							if ($el.data('aspect') && !$prd.data('firstAspect')) {
								$prd.data('globalAspect', $el.closest(that.defaults.colorswatch).find('li').first().data('aspect'));
								$prd.data('firstAspect', true);
							}
							if ($el.data('aspect') && $prd.data('globalAspect') != $el.data('aspect')) {
								$prd.data('globalAspect', $el.data('aspect'));
								var currentAspect = ($image.width() / $image.height()).toFixed(2);
								if (currentAspect > $el.data('aspect')) {
									widthAuto = true;
								}
								$image.hide();
							}
							if (!$el.hasClass('loaded')) {
								$prd.addClass('prd-loading');
							}
							$prd.find('li.active').removeClass('active');
							$el.addClass('active');
							var newImg = document.createElement('img');
							newImg.src = $el.data('image');
							newImg.onload = function () {
								$image.attr('src', imgSrc);
								if ($image.attr('srcset')) {
									$image.attr('srcset', imgSrc);
								}
								widthAuto ? $image.css({ 'width': 'auto' }) : $image.css({ 'width': '' });
								$image.show();
								$prd.removeClass('prd-loading');
								$el.addClass('loaded');
							};
						}
						e.preventDefault();
					});
				}
			};
			THEME.colortoggle.init();
		},
		productPageGallery: function productPageGallery(quickview) {
			var ProductPageGallery = {
				defaults: {
					mainMedia: '.page-content .product-main-carousel',
					carousel: '.js-product-main-carousel',
					previews: '.js-product-previews-carousel',
					zoomOptions: {
						zoomType: 'inner',
						zoomContainerAppendTo: '#prdMainImage',
						zIndex: 149,
						zoomWindowFadeIn: 500,
						zoomWindowFadeOut: 500,
						lensFadeIn: 500,
						lensFadeOut: 500,
						imageCrossfade: true,
						responsive: true,
						cursor: 'crosshair'
					},
					prdBlock: '.page-content .prd-block',
					zoomLink: '.page-content .prd-block_zoom-link',
					imageHolder: '.page-content .prd-block_main-image-holder',
					zoomImg: '.zoom',
					verticalSelector: '.prd-block--prv-left, .prd-block--prv-right',
					doubleSelector: '.prd-block--prv-double',
					quickView: false
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					this._handlers();
					this._galleryBuild();
					this._zoomLinkEvent();
				},
				reinit: function reinit() {
					if (isMobile) {
						$('.ZoomContainer').length && $('.ZoomContainer').remove();
					} else {
						this.reinitZoom();
					}
				},
				_galleryBuild: function _galleryBuild() {
					var that = this,
					    $carousel = $(that.defaults.carousel),
					    _galleryObj = [];
					$carousel.find('img').each(function () {
						var $this = $(this),
						    src = $this.attr('data-zoom-image'),
						    image = {};
						image["src"] = src;
						image["opts"] = {
							thumb: src,
							caption: $this.attr('alt')
						};
						_galleryObj.push(image);
					});
					that.galleryObj = _galleryObj;
				},
				_getActiveIndex: function _getActiveIndex($carousel) {
					var current = 0;
					if ($carousel.find('.slick-active').length) {
						current = $('.slick-active', $carousel).index();
					}
					return current;
				},
				_zoomLinkEvent: function _zoomLinkEvent() {
					var that = this,
					    $zoomLink = $(that.defaults.zoomLink),
					    $carousel = $(that.defaults.carousel);
					if ($zoomLink.closest('.prd-block').find('.product-previews-carousel').length) {
						$zoomLink.on('click', function (e) {
							e.preventDefault();
							var activeIndex = that._getActiveIndex($carousel),
							    items = that.galleryObj;
							$.fancybox.open(items, {
								loop: false,
								animationEffect: 'zoom',
								touch: false,
								buttons: ['close'],
								thumbs: {
									autoStart: true,
									axis: 'x'
								},
								arrows: false,
								beforeShow: function beforeShow(instance, slide) {
									$('.fancybox-container').last().addClass('fancybox--light');
								}
							});
							$.fancybox.getInstance('jumpTo', activeIndex);
						});
					} else {
						$zoomLink.on('click', function (e) {
							var $this = $(this);
							$.fancybox.open([{
								src: $this.attr('href'),
								opts: {
									caption: $this.data('caption')
								}
							}], {
								beforeShow: function beforeShow(instance, slide) {
									$(".fancybox-container").last().addClass("fancybox--light");
								}
							});
							e.preventDefault();
						});
					}
				},

				reinitZoom: function reinit() {
					if (!isMobile) {
						var that = this,
						    $carousel = $(this.defaults.carousel),
						    $currentSlide = $carousel.find('.slick-current');
						that._addZoom($carousel, $currentSlide);
					}
				},
				_addZoom: function _addZoom($slider, $slide) {
					var that = this;
					$slide.find('.elzoom').each(function () {
						var $this = $(this);
						if ($this.data('image-width')) {
							if ($this.data('image-width') > $this.width()) {
								$this.ezPlus(that.defaults.zoomOptions);
							}
						} else $this.ezPlus(that.defaults.zoomOptions);
					});
				},
				destroy: function destroy() {
					var that = this,
					    $carousel = $(that.defaults.carousel),
					    $previews = that.defaults.quickView ? $(that.defaults.previews) : $carousel.closest('.prd-block').find(that.defaults.previews);
					if ($previews.length) {
						$document.off('click', that.defaults.previews + ' a');
					}
				},
				_handlers: function _handlers() {
					var that = this,
					    $carousel = $(that.defaults.carousel),
					    $mainMedia = $(that.defaults.mainMedia),
					    $previews = that.defaults.quickView ? $(that.defaults.previews) : $carousel.closest('.prd-block').find(that.defaults.previews),
					    zoomPosition = $mainMedia.data('zoom-position') ? $mainMedia.data('zoom-position') : false,
					    fade = false,
					    asNavForCarousel = false,
					    asNavForPreview = false,
					    rows = $previews.closest(that.defaults.doubleSelector).length ? 2 : 1,
					    slidesToShow = $previews.closest(that.defaults.doubleSelector).length && $previews.closest(that.defaults.verticalSelector).length ? 3 : $previews.closest(that.defaults.verticalSelector).length ? 6 : 4,
					    slidesToShowT = $previews.closest(that.defaults.doubleSelector).length ? slidesToShow : $previews.closest(that.defaults.verticalSelector).length ? slidesToShow - 1 : slidesToShow;
					if ($previews.data('desktop')) {
						slidesToShow = $previews.data('desktop');
					}
					if ($previews.data('tablet')) {
						slidesToShowT = $previews.data('tablet');
					}
					if (!$previews.closest(that.defaults.doubleSelector).length) {
						asNavForPreview = that.defaults.carousel;
						asNavForCarousel = that.defaults.previews;
					}
					var startSlide = 0,
					    startSlidePreviews = 0;
					if ($previews.length) {
						startSlide = $previews.find('.active').length ? $previews.find('.active').index() : 0;
						if ($previews.closest(that.defaults.doubleSelector).length) {
							startSlidePreviews = parseInt(startSlide / 2);
							if (slidesToShow * 2 >= $previews.children().length) {
								startSlidePreviews = 0;
							}
						} else {
							startSlidePreviews = startSlide;
						}
					}
					that.defaults.zoomOptions.zoomType = zoomPosition;
					var slickCarouselParams = void 0;
					if ($previews.length) {
						slickCarouselParams = {
							slidesToShow: 1,
							arrows: true,
							infinite: false,
							fade: fade,
							speed: 500,
							initialSlide: startSlide,
							asNavFor: asNavForCarousel,
							adaptiveHeight: true
						};
					} else {
						slickCarouselParams = {
							slidesToShow: 1,
							arrows: true,
							infinite: false,
							fade: fade,
							speed: 500,
							initialSlide: startSlide,
							adaptiveHeight: true
						};
					}
					if ($carousel.length) {
						$carousel.on('init afterChange', function (event, slick) {
							slick = $(slick.$slider);
							var $currentSlide = slick.find('.slick-current');
							if (!$currentSlide.find('img').hasClass('lazyloaded')) {
								$currentSlide.find('img').addClass('lazyload');
								if (!isMobile) {
									$currentSlide.find('img').on('lazyloaded', function () {
										that._addZoom(slick, $currentSlide);
									});
								}
							} else {
								if (!isMobile) {
									that._addZoom(slick, $currentSlide);
								}
							}
							if (!$currentSlide.next().find('img').hasClass('lazyloaded')) {
								$currentSlide.next().find('img').addClass('lazyload');
							}
							if (!$currentSlide.prev().find('img').hasClass('lazyloaded')) {
								$currentSlide.prev().find('img').addClass('lazyload');
							}
						});
						$carousel.on('afterChange', function (event, slick, currentSlide) {
							slick = $(slick.$slider);
							var $currentSlide = slick.find('.slick-current');
							$('iframe', $currentSlide).each(function () {
								this.contentWindow.postMessage('{"event":"command","func":"playVideo","args":""}', '*');
							});
							$('video', $currentSlide).each(function () {
								this.play();
							});
							if (slick.closest(that.defaults.doubleSelector).length) {
								var previewsRow = currentSlide < 2 ? 0 : Math.floor(currentSlide / 2),
								    previewsColumn = currentSlide % 2 + 1;
								$previews.find('.active').removeClass('active');
								$previews.slick('slickGoTo', previewsRow);
								$previews.find('.slick-slide[data-slick-index="' + previewsRow + '"] > div:nth-child(' + previewsColumn + ') > a').addClass('active');
							} else {
								$previews.find('.active').removeClass('active');
								$previews.find('.slick-slide[data-slick-index="' + currentSlide + '"]').addClass('active');
							}
						});
						$carousel.on('beforeChange', function (event, slick) {
							if (!isMobile) {
								$('.ZoomContainer').length && $('.ZoomContainer').remove();
							}
							slick = $(slick.$slider);
							$('iframe', slick).each(function () {
								this.contentWindow.postMessage('{"event":"command","func":"pauseVideo","args":""}', '*');
							});
							$('video', slick).each(function () {
								this.pause();
							});
						});
						$carousel.slick(slickCarouselParams);
					} else if (that.defaults.quickView && !isMobile) {
						var $this = $mainMedia.find('img.elzoom');
						if ($this.data('image-width') > $this.width()) {
							$this.ezPlus(that.defaults.zoomOptions);
						}
					} else if (!isMobile) {
						$mainMedia.find('img.elzoom').on('lazyloaded', function () {
							var $this = $(this);
							if ($this.data('image-width') > $this.width()) {
								$this.ezPlus(that.defaults.zoomOptions);
							}
						});
					}
					if ($previews.length) {
						$previews.on('init', function () {
							var $this = $(this);
							if ($this.find('[data-aspectratio]').length) {
								$('[data-aspectratio]', $this).each(function () {
									$(this).height($(this).width() / $(this).data('aspectratio'));
								});
								$('[data-aspectratio]', $this).on('lazyloaded', function () {
									$(this).height('');
								});
							}
							if (!$this.find('.slick-arrow:not(.slick-disabled)').length) {
								$this.addClass('fixedOnSelect');
							}
						});
						if (that.defaults.quickView) {
							var vertical = $('.modal-quickview--classic').length ? false : true;
							$previews.slick({
								slidesToShow: 4,
								slidesToScroll: 1,
								dots: false,
								vertical: vertical,
								swipe: swipemode,
								focusOnSelect: true,
								infinite: false,
								rows: rows,
								initialSlide: startSlidePreviews,
								responsive: [{
									breakpoint: maxSM,
									settings: {
										slidesToShow: 4
									}
								}, {
									breakpoint: maxXS,
									settings: {
										slidesToShow: 3
									}
								}]
							});
						} else {
							$previews.slick({
								slidesToShow: slidesToShow,
								slidesToScroll: 1,
								dots: false,
								vertical: $previews.closest(that.defaults.verticalSelector).length ? true : false,
								swipe: swipemode,
								focusOnSelect: true,
								infinite: false,
								rows: rows,
								initialSlide: startSlidePreviews,
								responsive: [{
									breakpoint: 1400,
									settings: {
										vertical: $previews.closest(that.defaults.verticalSelector).length ? true : false,
										slidesToShow: slidesToShowT
									}
								}, {
									breakpoint: maxLG,
									settings: {
										vertical: $previews.closest(that.defaults.verticalSelector).length ? true : false,
										slidesToShow: slidesToShowT
									}
								}, {
									breakpoint: productPreviewHorBreikpoint,
									settings: {
										vertical: false,
										slidesToShow: 3,
										centerMode: false
									}
								}]
							});
						}
						$document.on('click', that.defaults.previews + ' a', function (e) {
							var $this = $(this);
							if ($this.hasClass('active')) {
								return false;
							} else {
								$previews.find('.active').removeClass('active');
								$this.addClass('active');
								$carousel.slick('slickSetOption', 'fade', true);
								$carousel.slick('slickSetOption', 'speed', 100);
								if ($this.closest(that.defaults.doubleSelector).length) {
									var slidenumber = $this.closest('.slick-slide').data('slick-index'),
									    allCount = $previews.find('.slick-slide').length,
									    showCount = $previews.find('.slick-active').length;
									$carousel.slick('slickGoTo', $this.closest('.slick-slide').data('slick-index') * 2 + $this.parent().index(), false);
									if (slidenumber > allCount - showCount) {
										$previews.slick('slickGoTo', allCount - showCount, false);
									}
								} else {
									$carousel.slick('slickGoTo', $this.data('slick-index'), false);
								}
								$carousel.slick('slickSetOption', 'fade', false);
								$carousel.slick('slickSetOption', 'speed', 500);
								$carousel.find('.slick-slide').css({ 'opacity': 1 });
								e.stopPropagation();
							}
							e.preventDefault();
						});
					}
				},
				switchImage: function switchImage(id) {
					var that = this,
					    $carousel = $(that.defaults.carousel),
					    $previews = that.defaults.quickView ? $(that.defaults.previews) : $carousel.closest('.prd-block').find(that.defaults.previews),
					    $slide = void 0;
					if ($previews.length) {
						$slide = $previews.find('[data-image-id=' + id + ']');
						$slide.trigger('click');
					} else {
						$slide = $carousel.find('[data-image-id=' + id + ']');
						$carousel.slick('slickSetOption', 'fade', true);
						$carousel.slick('slickSetOption', 'speed', 100);
						$carousel.slick('slickGoTo', $slide.data('slick-index'), false);
						$carousel.slick('slickSetOption', 'fade', false);
						$carousel.slick('slickSetOption', 'speed', 500);
						$carousel.find('.slick-slide').css({ 'opacity': 1 });
					}
				}
			};
			if (quickview) {
				THEME.productpagegallery_qw = Object.create(ProductPageGallery);
				THEME.productpagegallery_qw.init({
					mainMedia: '.modal--quickview .product-main-carousel',
					carousel: '.js-product-main-carousel-qw',
					previews: '.js-product-previews-carousel-qw',
					quickView: true,
					zoomOptions: {
						zoomType: 'inner',
						zoomContainerAppendTo: '.js-product-main-zoom-container',
						zIndex: 149,
						zoomWindowFadeIn: 500,
						zoomWindowFadeOut: 500,
						lensFadeIn: 500,
						lensFadeOut: 500,
						imageCrossfade: true,
						responsive: true,
						cursor: 'crosshair'
					},
					prdBlock: '.modal--quickview .prd-block',
					zoomLink: '.modal--quickview .prd-block_zoom-link',
					imageHolder: '.modal--quickview .prd-block_main-image-holder'
				});
			} else {
				THEME.productpagegallery = Object.create(ProductPageGallery);
				THEME.productpagegallery.init();
			}
		},
		productHoverHeight: function productHoverHeight(product) {
			var speed = 180;
			$document.on('mouseenter', product, function (e) {
				if (w < maxMD) return false;
				var $this = $(this),
				    $slick = $this.closest('.slick-list');
				if ($this.closest('.single-prd-carousel').length || $this.closest('.prd-listview').length) return false;
				if (!$(this).hasClass('prd--action-off')) {
					$this.css({
						'height': $(this)[0].getBoundingClientRect().height + 'px'
					});
				}
				if (!$this.hasClass('hovered')) {
					$this.addClass('hovered');
				}
				$slick.addClass('slick-list--offset');
				$slick.parent().addClass('prd-hovered');
			}).on('mouseleave', product, function () {
				if (w < maxMD) return false;
				var $this = $(this);
				var $slick = $this.closest('.slick-list');
				if ($this.closest('.single-prd-carousel').length || $this.closest('.prd-listview').length) return false;
				$this.removeClass('hovered');
				$slick.removeClass('slick-list--offset');
				$slick.parent().removeClass('prd-hovered');
				$this.css({
					'height': ''
				});
			});
			$('.prd-carousel [data-fancybox]').fancybox({
				backFocus: false
			});
		},
		actionPrd: function actionPrd() {
			THEME.productAction = {
				defaults: {
					link: '.js-product-remove',
					empty: '.js-minicart-empty',
					minicart: '.minicart-drop-content',
					minicartProduct: '.minicart-prd',
					prdWrap: '.minicart-list-prd'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					this._handlers();
					this.numberLine();
				},
				numberLine: function numberLine() {
					var that = this;
					$(that.defaults.minicart).find(that.defaults.minicartProduct).each(function (i) {
						$(this).find('[data-line-number]').attr('data-line-number', i + 1);
					});
				},
				clearAllAnimation: function clearAllAnimation() {
					var that = this,
					    $empty = $(that.defaults.empty),
					    $prd = $(that.defaults.minicartProduct),
					    $prdWrap = $(that.defaults.prdWrap);
					$empty.removeClass('d-none');
					var h = $empty[0].scrollHeight;
					var showEmpty = anime.timeline({
						easing: 'linear'
					});
					showEmpty.add({
						targets: $prdWrap[0],
						height: '0',
						opacity: 0,
						duration: 200 + $prd.length * 50,
						complete: function complete() {
							$prd.remove();
							$prdWrap.css({ 'height': '', 'opacity': '' });
						}
					}).add({
						targets: $(that.defaults.empty)[0],
						opacity: [0, 1],
						height: {
							value: h + 'px'
						},
						duration: 450
					});
					$('.js-minicart-drop-total, .js-hide-empty').addClass('d-none');
					$('.minicart-link').addClass('only-icon');
				},
				removeAnimation: function removeAnimation($link) {
					var that = this,
					    $minicart = $(that.defaults.minicart),
					    $prd = $link.closest('.minicart-prd');
					$minicart.addClass('disable-actions');
					anime({
						targets: $prd[0],
						height: '0',
						padding: '0',
						opacity: 0,
						duration: 300,
						easing: 'linear',
						complete: function complete() {
							$prd[0].remove();
							if (!$('.minicart-prd').length) {
								$(that.defaults.empty).removeClass('d-none').css({ 'opacity': 0, 'height': 0 });
								that.removeAnimationAll();
							}
							that.numberLine();
							setTimeout(function () {
								$minicart.removeClass('disable-actions');
							}, 1000);
						}
					});
				},
				removeAnimationAll: function removeAnimationAll() {
					var that = this,
					    $empty = $(that.defaults.empty),
					    h = $empty[0].scrollHeight;
					var showEmpty = anime.timeline({
						easing: 'linear'
					});
					showEmpty.add({
						targets: $('.js-minicart-drop-total')[0],
						opacity: 0,
						height: '0',
						padding: '0',
						margin: '0',
						duration: 100,
						complete: function complete() {
							$('.js-minicart-drop-total').addClass('d-none').css({
								'height': '',
								'padding': '',
								'margin': ''
							});
							$('.js-hide-empty').addClass('d-none');
						}
					}).add({
						targets: $(this.defaults.empty)[0],
						opacity: [0, 1],
						height: {
							value: h + 'px'
						},
						duration: 450
					});
				},
				_handlers: function _handlers() {
					var that = this;
					$(document).on('click', that.defaults.link, function (e) {
						that.removeAnimation($(this));
						e.preventDefault();
					});
				}
			};
			THEME.productAction.init();
		},
		productWidth: function productWidth(product) {
			if (w < maxLG) {
				$('.prd-hor').addClass('prd-hor-temp').removeClass('prd-hor');
			} else {
				$('.prd-hor-temp').addClass('prd-hor').removeClass('prd-hor-temp');
			}
			$(product).each(function () {
				var $this = $(this);
				$this.removeClass('prd-w-xl prd-w-lg prd-w-md prd-w-sm prd-w-xs prd-w-xxs');
				var w = $this.find('.prd-img-area').width(),
				    wClass = 'prd-w-xl';
				if (w >= 250 && w < 300) {
					wClass = 'prd-w-lg';
				} else if (w >= 220 && w < 250) {
					wClass = 'prd-w-md';
				} else if (w >= 190 && w < 220) {
					wClass = 'prd-w-sm';
				} else if (w >= 160 && w < 190) {
					wClass = 'prd-w-xs';
				} else if (w < 160) {
					wClass = 'prd-w-xxs';
				}
				$this.addClass(wClass);
			});
		},
		countdownWidth: function countdownWidth(countdown) {
			$(countdown).each(function () {
				var $this = $(this);
				$this.removeClass('w-md w-sm');
				var w = $this.width();
				if (w < 500) {
					$this.addClass('w-sm');
				} else if (w < 600) {
					$this.addClass('w-md');
				}
			});
		}
	};
	THEME.productPage = {
		init: function init() {
			this.simpleFancyGallery('.prd-block [data-fancybox="galleryQW"], .prd-block [data-fancybox="gallery"]');
			this.shiftTitle();
			if ($('.js-trigger-addtocart').length) this.stickyAddToCart();
			if ($('.prd-progress-bar').length) this.progressBar();
			this.visitorsNow();
			this.productSold();
			this.countdownCircle();
			this.scrollToReview('.js-reviews-link', '#productReviews');
		},
		countdownCircle: function countdownCircle() {

			var $countdownCircleTimer = $('.countdownCircleTimer'),
			    $shippingDate = $('.js-prd-shipping-date'),
			    $deliveryDate = $('.js-prd-delivery-date');
			if (!$countdownCircleTimer.length) return false;
			var dataTime = $countdownCircleTimer.data('time').split(',');
			var time1 = dataTime[0],
			    time2 = dataTime[1],
			    time3 = dataTime[2];
			var dt = new Date();
			var date = dt.getFullYear() + "/" + (dt.getMonth() + 1) + "/" + dt.getDate();
			var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
			var array = [new Date(date + " " + time1), new Date(date + " " + time2), new Date(date + " " + time3)];

			function closestTime(array, num) {
				var i = 0,
				    ans = void 0;
				for (i in array) {
					var m = array[i] - num;
					if (m > 0) {
						ans = array[i];
						break;
					}
				}
				return ans;
			}

			function getDayOfWeek(date, weekdays) {
				var dayOfWeek = new Date(date).getDay();
				weekdays = weekdays ? weekdays : ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
				return isNaN(dayOfWeek) ? null : weekdays[dayOfWeek];
			}

			var closesTime = closestTime(array, dt),
			    timeBreikpoint = closesTime.getHours() + ":" + closesTime.getMinutes() + ":" + closesTime.getSeconds(),
			    leftTime = Math.abs(closesTime - dt) / 1000,
			    format = '<span><span>%H</span>:</span><span><span>%M</span>:</span><span><span>%S</span></span>';
			$countdownCircleTimer.countdown(date + ' ' + timeBreikpoint, function (event) {
				$countdownCircleTimer.html(event.strftime(format));
			}).on('finish.countdown', function () {
				dt = new Date();
				closesTime = closestTime(array, dt);
				timeBreikpoint = closesTime.getHours() + ":" + closesTime.getMinutes() + ":" + closesTime.getSeconds();
				leftTime = Math.abs(closesTime - dt) / 1000;
				$countdownCircleTimer.countdown(date + ' ' + timeBreikpoint, function (event) {
					$countdownCircleTimer.html(event.strftime(format));
				});
			});
			$shippingDate.html(("0" + Math.floor(leftTime / 3600) % 24).slice(-2) + ':' + ("0" + Math.floor(leftTime / 60) % 60).slice(-2) + ':' + ("0" + Math.floor(leftTime % 60)).slice(-2));
			var deliveryDate = new Date();
			deliveryDate.setDate(dt.getDate() + 3);
			$deliveryDate.html(getDayOfWeek(deliveryDate, $deliveryDate.data('weekday')) + " " + ("0" + (deliveryDate.getMonth() + 1)).slice(-2) + "/" + ("0" + deliveryDate.getDate()).slice(-2) + "/" + deliveryDate.getFullYear());
		},
		visitorsNow: function visitorsNow() {
			var $count = $('.js-visitors-now'),
			    vMax = $count.data('vmax') ? $count.data('vmax') : 40,
			    vMin = $count.data('vmin') ? $count.data('vmin') : 10,
			    initial = random(vMin, vMax),
			    count = initial,
			    speed = 12000;

			function setVisitors() {
				var variation = random(-2, 2),
				    current_count = count + variation;
				if (current_count <= vMax && current_count >= vMin) {
					count = current_count;
					$count.html(count);
				}
			}

			setVisitors();
			setInterval(function () {
				setVisitors();
			}, speed);
		},
		productSold: function productSold() {
			var $count = $('.js-prd-last-sold-count'),
			    soldMax = $count.data('soldmax') ? $count.data('soldmax') : 25,
			    soldMin = $count.data('soldmin') ? $count.data('soldmin') : 10;
			$count.html(random(soldMin, soldMax));
		},
		progressBar: function progressBar() {
			THEME.progressbar = {
				data: {
					progress: '.prd-progress-bar',
					leftCount: '.js-stock-left'
				},
				init: function init(options) {
					$.extend(this.data, options);
					var that = this;
					var $progress = $(that.data.progress),
					    $leftCount = $(that.data.leftCount),
					    dataStock = $progress.data('stock').split(',');
					that.data.stockMax = parseInt(dataStock[0]);
					that.data.stockMin = parseInt(dataStock[1]);
					that.data.stockLeftMax = parseInt(dataStock[2]);
					that.data.stockLeftMin = parseInt(dataStock[3]);
					that.data.stockLeft = Math.floor(Math.random() * (that.data.stockLeftMax - that.data.stockLeftMin + 1) + that.data.stockLeftMin);
					that.data.firstDownInterval = parseInt(dataStock[4]);
					that.data.downInterval = parseInt(dataStock[5]);
					if (!$progress.length) return false;
					$progress.css({
						width: that.data.stockLeft * 100 / that.data.stockMax + '%'
					});
					$leftCount.html(that.data.stockLeft);
					var timerStart = setInterval(function () {
						$progress.css({
							width: that.data.stockLeft * 100 / that.data.stockMax - 1 + '%'
						});
						$leftCount.html(that.data.stockLeft - 1);
						clearInterval(timerStart);
						that._stockDown(that.data.downInterval, that.data.stockLeft - 1, that.data.stockMin);
					}, that.data.firstDownInterval);
				},
				setNull: function setNull(text) {
					var that = this;
					$(that.data.progress).addClass('prd-progress--null');
					$('.prd-progress-text').hide();
					$('.prd-progress-text-null').show();
					$('.prd-progress-text-null').html(text);
				},
				resetNull: function resetNull() {
					var that = this;
					$(that.data.progress).removeClass('prd-progress--null');
					$('.prd-progress-text').show();
					$('.prd-progress-text-null').hide();
					$('.prd-progress-text-null').html('');
				},
				_stockDown: function _stockDown(downinterval, stockleft, stockmin) {
					var that = this,
					    $progress = $(that.data.progress);
					var _stockleft = stockleft,
					    _stockmin = stockmin;
					var timer = setInterval(function () {
						_stockleft--;
						$progress.css({
							width: _stockleft * 100 / that.data.stockMax + '%'
						});
						$(that.data.leftCount).html(_stockleft);
						if (_stockleft <= _stockmin) {
							clearInterval(timer);
							that._startCount(downinterval, stockleft + 1, stockmin);
						}
					}, downinterval);
				},
				_startCount: function _startCount(downinterval, stockleft, stockmin) {
					var that = this;
					that._stockDown(downinterval, stockleft, stockmin);
				}
			};
			THEME.progressbar.init();
		},
		stickyAddToCart: function stickyAddToCart() {
			THEME.stickyaddtocart = {
				data: {
					stickyAddToCart: '.js-stickyAddToCart',
					button: '.js-trigger-addtocart',
					popupAlt: ['.js-popupAddToCart', '.js-popupSelectOptions']
				},
				init: function init(options) {
					$.extend(this.data, options);
					var that = this,
					    $stickyAddToCart = $(that.data.stickyAddToCart),
					    $button = $(that.data.button);
					if ($stickyAddToCart.length && $button.length) {
						$window.on('load.stickyAddToCart scroll.stickyAddToCart', function () {
							that._scrollEvents($stickyAddToCart, $button);
						});
						setTimeout(function () {
							that._scrollEvents($stickyAddToCart, $button);
						}, 500);
					}
				},
				destroy: function destroy() {
					$('body').css({ 'margin-bottom': '' });
					$window.off('scroll.stickyAddToCart');
				},
				reinit: function reinit() {
					var $stickyAddToCart = $(this.data.stickyAddToCart);
					if (!$stickyAddToCart.hasClass('closed')) {
						this._setHeigth($(this.data.stickyAddToCart));
					}
				},
				_setHeigth: function _setHeigth($stickyAddToCart) {
					var popupAlt = this.data.popupAlt,
					    h = $stickyAddToCart.find('.container').outerHeight();
					$stickyAddToCart.removeClass('closed');
					$('body').css({
						'margin-bottom': h + 'px'
					});
					for (var i = 0; i < popupAlt.length; ++i) {
						if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
							h += $(popupAlt[i]).find('.container').outerHeight();
						}
					}
					$('.js-pn').css({
						'margin-bottom': h + 20 + 'px'
					});
				},
				_scrollEvents: function _scrollEvents($stickyAddToCart, $button) {
					var st = $window.scrollTop();
					if (st < $button.offset().top) {
						if (!$stickyAddToCart.hasClass('closed')) {
							var popupAlt = this.data.popupAlt,
							    _h = 0;
							$stickyAddToCart.addClass('closed');
							$('body').css({
								'margin-bottom': ''
							});
							for (var i = 0; i < popupAlt.length; ++i) {
								if (!$(popupAlt[i]).hasClass('closed') && $(popupAlt[i]).length) {
									_h += $(popupAlt[i]).find('.container').outerHeight();
								}
							}
							$('.js-pn').css({
								'margin-bottom': _h + 20 + 'px'
							});
							anime({
								targets: '.js-stickyAddToCart',
								easing: 'linear',
								duration: 300,
								height: {
									value: '0px'
								}
							});
						}
					} else {
						if ($stickyAddToCart.hasClass('closed')) {
							var _h2 = $stickyAddToCart.find('.container').outerHeight();
							$stickyAddToCart.removeClass('closed');
							this._setHeigth($stickyAddToCart);
							anime({
								targets: '.js-stickyAddToCart',
								easing: 'linear',
								duration: 300,
								height: {
									value: _h2 + 'px'
								}
							});
						}
					}
				}
			};
			THEME.stickyaddtocart.init();
		},
		shiftTitle: function shiftTitle() {
			if ($('.prd-block-under').length && $('.product-previews-wrapper').length) {
				var shift = $('.product-previews-wrapper').outerWidth() + 15;
				if (w < productPreviewHorBreikpoint) {
					shift = 15;
				}
				if ($('.prd-block-under').hasClass('prd-block--prv-left')) {
					if (!$('body').hasClass('rtl')) {
						$('.prd-block-under').find('.col').css({
							'padding-left': shift + 'px'
						});
					} else {
						$('.prd-block-under').find('.col').css({
							'padding-right': shift + 'px'
						});
					}
				}
			}
		},
		productTitleReposition: function productTitleReposition() {
			THEME.prdrepos = {
				init: function init(options) {
					this.default = options;
					if ($(this.default.mobile).closest('.prd-block--mobile-image-first').length) return false;
					this._reposBlock(w < this.default.reposBreakpoint);
					return this;
				},
				reinit: function reinit(w) {
					if ($(this.default.mobile).closest('.prd-block--mobile-image-first').length) return false;
					this._reposBlock(w < this.default.reposBreakpoint);
					return this;
				},
				_reposBlock: function _reposBlock(isMobile) {
					var $prdInfoDesktop = $(this.default.desktop),
					    $prdInfoMobile = $(this.default.mobile);
					if (isMobile) {
						if ($body.hasClass('prd-mob')) return false;
						$prdInfoDesktop.hide();
						if ($prdInfoDesktop.length) {
							$prdInfoDesktop.children().detach().appendTo($prdInfoMobile);
							$prdInfoMobile.show();
							$body.addClass('prd-mob').removeClass('prd-dsc');
						}
					} else {
						if ($body.hasClass('prd-dsc')) return false;
						$prdInfoMobile.hide();
						if ($prdInfoMobile.length) {
							$prdInfoMobile.children().detach().appendTo($prdInfoDesktop);
							$prdInfoDesktop.show();
							$body.addClass('prd-dsc').removeClass('prd-mob');
						}
					}
				}
			};
			THEME.prdrepos.init({
				desktop: '.prd-block .js-prd-d-holder',
				mobile: '.prd-block .js-prd-m-holder',
				reposBreakpoint: maxSM
			});
		},
		scrollToReview: function scrollToReview(link, reviewID) {
			$document.on('click', link, function (e) {
				var $panReview = $(reviewID),
				    tabNavs = '.nav-tabs',
				    tabPane = '.tab-pane',
				    tabPaneM = '.panel',
				    header = '.hdr';
				if ($panReview.length) {
					if ($panReview.closest(tabPaneM).length) {
						var $reviewTab = $panReview.closest(tabPaneM).find('.panel-title > a');
						if (!$reviewTab.closest('.panel-heading').hasClass('active')) $reviewTab.trigger('click');
						setTimeout(function () {
							$('html,body').animate({
								scrollTop: $reviewTab.offset().top - $(header).height()
							}, 500);
						}, 500);
					} else if ($panReview.closest(tabPane).length) {
						var tabReviewID = $panReview.closest(tabPane).attr('id'),
						    reviewTabNum = $('#' + tabReviewID).index(),
						    _$reviewTab = $(tabNavs).find('li').eq(reviewTabNum).find('a');
						_$reviewTab.trigger('click');
						$('html,body').animate({
							scrollTop: $(tabNavs).offset().top - $(header).height()
						}, 500);
					} else {
						$('html,body').animate({
							scrollTop: $panReview.offset().top - $(header).height()
						}, 500);
					}
				}
				e.preventDefault();
			});
		},
		simpleFancyGallery: function simpleFancyGallery(link) {
			$(link).fancybox({
				loop: false,
				animationEffect: "zoom",
				buttons: ["close"],
				thumbs: {
					autoStart: true,
					axis: 'x'
				},
				arrows: false,
				touch: false,
				beforeShow: function beforeShow(instance, slide) {
					$('.fancybox-container').last().addClass('fancybox--light');
				}
			});
		}
	};
	THEME.productFeatures = {
		init: function init() {
			if ($('.payment-notification').length) this.paymentNotification();
		},
		paymentNotification: function paymentNotification(options) {
			THEME.paymentnotification = {
				defaults: {
					productArray: '',
					maxAgoMin: 10,
					minAgoMin: 1,
					paymentNotification: '.payment-notification',
					timelineHide: false,
					timelineShow: false
				},
				init: function init(options) {
					var that = this;
					$.extend(that.defaults, options);
					var $paymentNotification = $(that.defaults.paymentNotification);
					that.defaults.fromArray = $paymentNotification.parent().data('from').split(",");
					that.defaults.productArray = $paymentNotification.parent().data('products');
					that.defaults.paymentNotificationVisible = $paymentNotification.data('visible-time') ? $paymentNotification.data('visible-time') : 10000; // min 2000
					that.defaults.paymentNotificationHidden = $paymentNotification.data('hidden-time') ? $paymentNotification.data('hidden-time') : 10000; // min 2000
					that.defaults.paymentNotificationDelay = $paymentNotification.data('delay') ? $paymentNotification.data('delay') : 5000;
					var delay = that.defaults.paymentNotificationDelay > that.defaults.paymentNotificationHidden ? that.defaults.paymentNotificationDelay - that.defaults.paymentNotificationHidden : that.defaults.paymentNotificationHidden;
					setTimeout(function () {
						that._showPaymentNotification(Math.floor(Math.random() * that.defaults.productArray.length));
					}, delay);
					$('.payment-notification-close').on('click', function () {
						that._hidePaymentNotification();
						if ($paymentNotification.data('close')) {
							that.destroy();
						}
					});
				},
				destroy: function destroy() {
					$(this.defaults.paymentNotification).remove();
				},
				_hidePaymentNotification: function _hidePaymentNotification() {
					var that = this;
					that.defaults.timelineShow.pause();
					that.defaults.timelineHide = anime.timeline();
					that.defaults.timelineHide.add({
						targets: that.defaults.paymentNotification,
						scale: '1',
						translateY: ['0%', '-50%'],
						opacity: '0',
						easing: 'easeInOutSine',
						duration: 350
					});
					setTimeout(function () {
						that._setData(Math.floor(Math.random() * that.defaults.productArray.length));
						that.defaults.timelineShow.restart();
					}, 500);
				},
				setCities: function setCities(list) {
					this.defaults.fromArray = list;
				},
				_setData: function _setData(i) {
					var that = this;
					$('.js-pn-name').html(that.defaults.productArray[i].productname);
					$('.js-pn-link').attr('href', that.defaults.productArray[i].productlink);
					$('.js-pn-link').attr('title', that.defaults.productArray[i].productname);
					$('.js-pn-link-quickview').data('src', that.defaults.productArray[i].productlink + '?view=quick-view&output=embed');
					$('.js-pn-image').attr('src', that.defaults.productArray[i].productimage);
					$('.js-pn-from').html(that.defaults.fromArray[random(0, that.defaults.fromArray.length)]);
					$('.js-pn-time').html(random(that.defaults.minAgoMin, that.defaults.maxAgoMin));
				},
				_showPaymentNotification: function _showPaymentNotification(i) {
					var that = this;
					that._setData(i);
					that.defaults.timelineShow = anime.timeline({
						loop: true,
						delay: that.defaults.paymentNotificationHidden
					});
					that.defaults.timelineShow.add({
						targets: that.defaults.paymentNotification,
						translateY: ['120%', '0%'],
						opacity: ['0', '1'],
						easing: 'easeInOutSine',
						duration: 500
					}).add({
						targets: that.defaults.paymentNotification,
						scale: '1.2',
						translateY: ['0%', '-50%'],
						opacity: '0',
						easing: 'easeInOutSine',
						duration: 500,
						delay: that.defaults.paymentNotificationVisible,
						complete: function complete() {
							that._setData(Math.floor(Math.random() * that.defaults.productArray.length));
							that.defaults.timelineShow.restart();
						}
					});
				}
			};
			if ($.cookie('foxicPromoProduct') != 'yes' || $('.payment-notification').attr('data-expires') == '0' || $('body').hasClass('demo')) {
				THEME.paymentnotification.init(options);
			}
		}
	};
	THEME.catalog = {
		init: function init() {
			this.blockCollapse('.sidebar-block');
			this.categoryCollapse();
			this.viewMode('.view-mode'); // product view mode toggle
			this.sortOptions('ul[data-sort]');
			this.checkBoxes('.category-list a');
			if ($('.js-circle-loader').length) this.loadMoreAjax('.js-circle-loader');
		},
		loadMoreAjax: function loadMoreAjax(button) {
			function updateCircleLoader($button) {
				var start = $button.data('loaded') * 100 / $button.data('total'),
				    to = ($button.data('loaded') + $button.data('load')) * 100 / $button.data('total');
				$button.attr('data-start', start);
				$button.attr('data-to', to);
				$('.js-circle-loader-start', $button).html($button.data('loaded'));
				$('.js-circle-loader-end', $button).html($button.data('total'));
			}

			updateCircleLoader($(button));
			THEME.catalog.setPersentCircle();
			$(button).on('click', function (e) {
				var $this = $(this),
				    $container = $('.js-category-grid');
				THEME.loaderHorizontalSm ? THEME.loaderHorizontalSm.open() : false;
				if ($this.data('loaded') == $this.data('total')) return false;
				$.getJSON($this.attr('href'), function (data) {
					var html = '',
					    i = 0,
					    max = Math.min(parseInt($this.data('total'), 10), $this.data('load') + $this.data('loaded')) - 1,
					    min = parseInt($this.data('loaded'), 10) - 1;
					$.each(data, function (key, val) {
						if (i > min && i <= max) {
							html += val.product;
							setTimeout(function () {
								THEME.product.postAjaxProduct();
							}, 500);
						}
						i++;
					});
					$container.append(html);
					$this.data('loaded', $this.data('loaded') + $this.data('load'));
					if ($this.data('loaded') >= $this.data('total')) {
						$this.closest('.circle-loader-wrap').addClass('d-none');
					}
					updateCircleLoader($this);
					THEME.setPersentCircle ? THEME.setPersentCircle.reinit() : false;
					THEME.loaderHorizontalSm ? THEME.loaderHorizontalSm.close() : false;
				});
				e.preventDefault();
			});
		},
		setPersentCircle: function setPersentCircle() {
			THEME.setPersentCircle = {
				defaults: {
					loaderWrap: '.js-circle-loader',
					loader: '.js-circle-loader svg',
					bar: '.js-circle-bar',
					pagination: '.js-loader-pagination',
					paginationList: '.js-loader-pagination ul'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					this._handlers();
					this.reinit();
				},
				_handlers: function _handlers() {
					var that = this,
					    $loader = $(that.defaults.loaderWrap),
					    $pagination = $(that.defaults.pagination);
					$document.on('mouseenter', this.defaults.loaderWrap, function () {
						var $this = $(this);
						$(that.defaults.bar).removeClass('off-animation');
						that.setPersent($this.attr('data-to'), $this);
					}).on('mouseleave', this.defaults.loaderWrap, function () {
						var $this = $(this);
						that.setPersent($this.attr('data-start'), $this);
					});
					$document.on('mouseenter', this.defaults.pagination, function () {
						that.setPersent($loader.attr('data-start'), $loader);
					}).on('mouseleave', this.defaults.paginationList, function () {
						if ($pagination.length) that.setStartPage(200);
					});
					var paginationLeftPos = 0;
					$pagination.find('li').on('mouseenter', function () {
						paginationLeftPos = $(this).prop('offsetLeft');
						$('.js-pagination-hover-overlay').css({ 'left': paginationLeftPos });
					});
				},
				reinit: function reinit() {
					var that = this;
					$(this.defaults.loaderWrap).each(function () {
						var val = $(this).attr('data-start');
						that.setPersent(val, $(this));
					});
					if ($(that.defaults.pagination).length) that.setStartPage(0);
				},
				setStartPage: function setStartPage(duration) {
					var that = this,
					    $loader = $(that.defaults.loaderWrap),
					    $pagination = $(that.defaults.pagination),
					    page = $loader.attr('data-page'),
					    paginationLeftPos = 0;
					$pagination.find('li').each(function () {
						if (page == $(this).text()) {
							paginationLeftPos = $(this).prop('offsetLeft');
							return false;
						}
					});
					$('.js-pagination-hover-overlay').css({ 'left': paginationLeftPos });
				},
				setPersent: function setPersent(val, $wrap) {
					var $circle = $(this.defaults.bar, $wrap);
					$circle.each(function () {
						var $this = $(this),
						    r = parseInt($this.attr('r'), 10),
						    l = Math.PI * (r * 2);
						val < 0 ? val = 0 : val > 100 ? val = 100 : false;
						var str = (100 - val) / 100 * l;
						$this.css({ strokeDashoffset: str });
					});
				},
				setData: function setData(from, to) {
					var that = this;
					if (!from) {
						$(that.defaults.bar).addClass('off-animation');
						that.setPersent(from);
					}
					if (!to) {
						setTimeout(function () {
							$(that.defaults.bar).removeClass('off-animation');
							that.setPersent(to);
						}, 700);
					}
				}
			};
			THEME.setPersentCircle.init();
		},
		checkBoxes: function checkBoxes(link) {
			$document.on('click', link, function (e) {
				var $this = $(this),
				    $list = $this.closest('.category-list');
				$list.find('.active').removeClass('active');
				$this.closest('li').addClass('active');
			});
		},
		postAjaxCatalog: function postAjaxCatalog() {
			THEME.flowtype ? THEME.flowtype.hideCaption('.bnr[data-fontratio]') : false;
			THEME.catalog.blockCollapse('.sidebar-block');
			THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
			THEME.initialization.countdown('.prd .js-countdown');
			THEME.initialization.removeEmptyLinked('.colorswatch-label', 'ul');
			THEME.flowtype ? THEME.flowtype.reinit('.bnr[data-fontratio]') : false;
			THEME.initialization.tooltipIni('.prd [data-toggle="tooltip"]');
			THEME.mobileFilter ? THEME.mobileFilter.setShift(0) : false;
			setTimeout(function () {
				THEME.flowtype ? THEME.flowtype.showCaption('.bnr[data-fontratio]') : false;
				$('.js-category-grid').find('.lazyload').addClass('lazypreload');
			}, 500);
		},
		sortOptions: function sortOptions(obj) {
			$(obj).each(function () {
				var $sizeList = $(this),
				    defaultSort = ["xs", "s", "m", "l", "xl", "36", "38", "40", "42"],
				    sortData = $sizeList.data('sort') ? $sizeList.data('sort') : defaultSort,
				    unsortedArray = [];

				function intersect(a, b) {
					var t = void 0;
					if (b.length > a.length) t = b, b = a, a = t;
					return a.filter(function (e) {
						return b.indexOf(e) > -1;
					});
				}

				function sort_li(a, b) {
					return $(b).data('position') < $(a).data('position') ? 1 : -1;
				}

				$('li', $sizeList).each(function () {
					unsortedArray.push($(this).data('value'));
				});
				var sortedArray = intersect(sortData, unsortedArray);
				$('li', $sizeList).each(function (i) {
					var $this = $(this);
					$this.data('position', sortedArray.indexOf($this.data('value')));
				});
				$('li', $sizeList).sort(sort_li).appendTo($sizeList);
			});
		},
		emptyCategoryDisable: function emptyCategoryDisable() {
			THEME.emptycategorydisable = {
				defaults: {
					emptyBlock: '.empty-category',
					filterRow: '.filter-row',
					disableClass: 'disable'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					this.reinit();
				},
				reinit: function reinit() {
					if ($(this.defaults.emptyBlock).length) {
						$(this.defaults.filterRow).addClass(this.defaults.disableClass);
					} else {
						$(this.defaults.filterRow).removeClass(this.defaults.disableClass);
					}
				}
			};
			THEME.emptycategorydisable.init();
		},
		markSelectedFilter: function markSelectedFilter() {
			THEME.markselectedfilter = {
				defaults: {
					block: '.sidebar-block',
					marker: 'selected',
					active: '.active'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					this.reinit();
				},
				reinit: function reinit() {
					var that = this;
					$(that.defaults.block).each(function () {
						var $this = $(this);
						if ($this.find(that.defaults.active).length) {
							$this.addClass(that.defaults.marker);
						} else {
							$this.removeClass(that.defaults.marker);
						}
					});
				}
			};
			THEME.markselectedfilter.init();
		},
		blockCollapse: function blockCollapse(el) {
			var collapsed = el,
			    slidespeed = 250;
			$('.sidebar-block_content', collapsed).each(function () {
				if ($(this).parent().is('.open')) {
					$(this).slideDown(0);
				}
			});
			$('.sidebar-block_title', collapsed).on('click.blockCollapse', function (e) {
				e.preventDefault;
				var $this = $(this),
				    $thiscontent = $this.next('.sidebar-block_content');
				if ($this.parent().is('.open')) {
					$this.parent().removeClass('open');
					$thiscontent.slideUp(slidespeed);
				} else {
					$this.parent().addClass('open');
					$thiscontent.slideDown(slidespeed);
				}
			});
		},
		categoryCollapse: function categoryCollapse() {
			var slidespeed = 150;
			$(document).on('click', '.js-toggle-category', function (e) {
				e.preventDefault;
				e.stopPropagation();
				var $this = $(this),
				    $thiscontent = $this.next('ul');
				if ($this.parent().find('a').is('.open')) {
					$this.parent().find('a').removeClass('open');
					$thiscontent.slideUp(slidespeed);
				} else {
					$this.parent().find('a').addClass('open');
					$thiscontent.slideDown(slidespeed);
				}
			});
		},
		
		mobileFilterHorizontal: function mobileFilterHorizontal(el) {
			if ($(el).length) {
				THEME.mobileFilter = {
					options: {
						mobileFilter: '.js-filter-col-horizontal',
						toggleFilter: '.js-filter-toggle-horizontal, .js-filter-toggle',
						toggleButton: '.js-filter-btn',
						grid: '.js-category-grid'
					},
					init: function init(options) {
						$.extend(this.options, options);
						this.setShift(500);
						this._handlers();
					},
					reinit: function reinit() {
						this.setShift(500);
						return this;
					},
					setShift: function setShift(delay) {
						var that = this,
						    $button = $(that.options.toggleFilter),
						    left = $(that.options.grid).offset().left;
						if (isMobile) {
							$button.css({ 'margin-left': '' });
							$('.footer-sticky').css({ 'bottom': $button.outerHeight() });
						} else {
							$button.css({ 'margin-left': -left + 'px' });
							$('.footer-sticky').css({ 'bottom': '' });
						}
						setTimeout(function () {
							$(that.options.mobileFilter).addClass('filter-col--init');
						}, delay);
					},
					_handlers: function _handlers() {
						var $mobileFilter = $(this.options.mobileFilter),
						    toggleButton = this.options.toggleButton;
						$document.on('click', toggleButton, function (e) {
							var $this = $(this);
							if (!$body.hasClass('is-filters-opened')) {
								if (isMobile) bodyScrollLock.disableBodyScroll();
								$body.addClass('is-fixed is-filters-opened');
							} else {
								if (isMobile) bodyScrollLock.enableBodyScroll();
								$body.removeClass('is-fixed is-filters-opened');
							}
							if ($this.data('scroll-to')) {
								$('html,body').animate({
									scrollTop: $($this.data('scroll-to')).offset().top - $('.hdr').height()
								}, 250);
							}
							if ($mobileFilter.hasClass('filter-col--opened')) {
								var _h3 = $mobileFilter.find('.filter-row-content').outerHeight();
								$mobileFilter.css({ 'height': _h3 + 'px' });
								$mobileFilter.removeClass('filter-col--opened');
								anime({
									targets: $mobileFilter[0],
									height: {
										value: 0
									},
									duration: 500,
									easing: 'easeOutQuad'
								});
							} else {
								var _h4 = $mobileFilter.find('.filter-row-content').outerHeight();
								anime({
									targets: $mobileFilter[0],
									height: {
										value: _h4 + 'px'
									},
									duration: 550,
									easing: 'easeInOutQuad',
									complete: function complete() {
										$mobileFilter.css({ 'height': 'auto' });
									}
								});
								$mobileFilter.addClass('filter-col--opened');
							}
							e.preventDefault();
						});
					}
				};
				THEME.mobileFilter.init();
			}
		},

		viewMode: function viewMode(viewmode) {
			var $grid = $('.js-gridview', $(viewmode)),
			    $list = $('.js-listview', $(viewmode)),
			    $horGrid = $('.js-horview', $(viewmode)),
			    $products = $('.js-category-grid'),
			    listingClass = 'prd-listview',
			    horizontalClass = 'prd-horgrid',
			    gridClass = 'prd-grid';
			if ($products.hasClass(listingClass)) {
				$list.addClass('active');
			} else if ($products.hasClass(gridClass)) {
				$grid.addClass('active');
			} else $horGrid.addClass('active');
			THEME.viewMode = {
				options: {},
				init: function init(options) {
					$.extend(this.options, options);
					this._handlers();
					this.reinit();
				},
				reinit: function reinit() {
					if (w < maxMD) {
						if ($products.hasClass(horizontalClass)) {
							$list.trigger('click');
							$products.addClass('js-temp-horview-d');
						}
					} else {
						if ($products.hasClass('js-temp-horview-d')) {
							$horGrid.trigger('click');
							$products.removeClass('js-temp-horview-d');
						}
					}
				},
				_handlers: function _handlers() {
					$horGrid.on('click', function (e) {
						var $this = $(this);
						if (!$this.is('.active')) {
							$products.addClass('off-animation');
							$list.removeClass('active');
							$grid.removeClass('active');
							$this.addClass('active');
							$products.removeClass(listingClass);
							$products.removeClass(gridClass);
							$products.addClass(horizontalClass);
							$('.list-options [data-toggle="tooltip"]', $products).attr('data-placement', 'right').tooltip('dispose');
							$('.prd-circle-labels [data-toggle="tooltip"]', $products).attr('data-placement', 'left').tooltip('dispose');
							THEME.initialization.tooltipIni($('[data-toggle="tooltip"]', $products));
							THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
							setTimeout(function () {
								$products.removeClass('off-animation');
							}, 1000);
						}
						$products.removeClass('js-temp-horview-d');
						e.preventDefault();
					});
					$grid.on('click', function (e) {
						var $this = $(this);
						if (!$this.is('.active')) {
							$products.addClass('off-animation');
							$list.removeClass('active');
							$horGrid.removeClass('active');
							$this.addClass('active');
							$products.removeClass(listingClass);
							$products.removeClass(horizontalClass);
							$products.addClass(gridClass);
							$('.list-options [data-toggle="tooltip"]', $products).attr('data-placement', 'right').tooltip('dispose');
							$('.prd-circle-labels [data-toggle="tooltip"]', $products).attr('data-placement', 'left').tooltip('dispose');
							THEME.initialization.tooltipIni($('[data-toggle="tooltip"]', $products));
							THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
							setTimeout(function () {
								$products.removeClass('off-animation');
							}, 1000);
						}
						$products.removeClass('js-temp-horview-d');
						e.preventDefault();
					});
					$list.on('click', function (e) {
						var $this = $(this);
						if (!$this.is('.active')) {
							$products.addClass('off-animation');
							$grid.removeClass('active');
							$horGrid.removeClass('active');
							$this.addClass('active');
							$products.removeClass(gridClass);
							$products.removeClass(horizontalClass);
							$products.addClass(listingClass);
							$('[data-toggle="tooltip"]', $products).attr('data-placement', 'left').tooltip('dispose');
							THEME.initialization.tooltipIni($('[data-toggle="tooltip"]', $products));
							THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
							setTimeout(function () {
								$products.removeClass('off-animation');
							}, 1000);
						}
						$products.removeClass('js-temp-horview-d');
						e.preventDefault();
					});
				}
			};
			THEME.viewMode.init();
		}
	};
	THEME.forms = {
		init: function init() {
			this.checkoutDisable();
			this.checkoutTabs();
			this.checkoutAccordion();
			this.showRecoverPasswordForm();
			this.showForm();
			this.contactForm('#contactForm');
		},
		checkoutDisable: function checkoutDisable() {
			$(document).on('change', '[data-checkbox-btn]', function (e) {
				var $this = $(e.target);
				$this.is(':checked') ? $($this.attr('data-checkbox-btn')).removeClass('btn-disable') : $($this.attr('data-checkbox-btn')).addClass('btn-disable');
			});
		},
		contactForm: function contactForm(form) {
			var $contactForm = $(form);
			$contactForm.validate({
				rules: {
					username: {
						required: true,
						minlength: 2
					},
					email: {
						required: true,
						email: true
					},
					message: "required"
				},
				messages: {
					username: {
						required: "Please enter a username",
						minlength: "Your username must consist of at least 2 characters"
					},
					email: "Please enter a valid email address",
					message: "Please enter a message"
				},
				errorPlacement: function errorPlacement(error, element) {
					error.addClass("invalid-feedback");
					if (element.prop("type") === "checkbox") {
						error.insertAfter(element.next("label"));
					} else {
						error.insertAfter(element);
					}
				},
				highlight: function highlight(element, errorClass, validClass) {
					$(element).addClass("is-invalid").removeClass("is-valid");
				},
				unhighlight: function unhighlight(element, errorClass, validClass) {
					$(element).addClass("is-valid").removeClass("is-invalid");
				},

				submitHandler: function submitHandler(form) {
					$contactForm.ajaxSubmit({
						type: "POST",
						data: $contactForm.serialize(),
						url: "php/process-contact.php",
						success: function success() {
							$('.success-confirm', $contactForm).fadeIn();
							$contactForm.get(0).reset();
						},
						error: function error() {
							$('.error-confirm', $contactForm).fadeIn();
						}
					});
				}
			});
		},
		checkoutTabs: function checkoutTabs() {
			$('.step-next').on('click', function () {
				var nextId = $(this).closest('.tab-pane').next().attr('id');
				$('[href="#' + nextId + '"]').tab('show');
				return false;
			});
			$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
				var step = $(e.target).data('step');
				var percent = parseInt(step) / 4 * 100;
				$('.progress-bar').css({ width: percent + '%' });
				e.relatedTarget;
			});
		},
		checkoutAccordion: function checkoutAccordion() {
			$('.step-next-accordion').on('click', function () {
				var $nextPanel = $(this).closest('.panel').next('.panel');
				if ($nextPanel) $nextPanel.find('.panel-title > a').trigger('click');
				return false;
			});
		},
		showRecoverPasswordForm: function showRecoverPasswordForm() {
			var $link = $('.js-toggle-forms'),
			    $form1 = $('#loginForm'),
			    $form2 = $('#recoverPasswordForm');
			$link.on('click', function (e) {
				$form1.toggleClass('d-none');
				$form2.toggleClass('d-none');
				e.preventDefault();
			});
		},
		showForm: function showForm() {
			var $linkShow = $('.js-show-form'),
			    $linkClose = $('.js-close-form');
			$linkShow.on('click', function (e) {
				$($(this).data('form')).removeClass('d-none');
				e.preventDefault();
			});
			$linkClose.on('click', function (e) {
				$($(this).data('form')).addClass('d-none');
				e.preventDefault();
			});
		}
	};
	THEME.sections = {
		init: function init() {
			if ($('[data-grid-tab-title]').length) this.productTabs();
			this.slickCarousels();
			if ($('.js-prd-carousel-tab').length) this.carouselTab();
			if ($('.js-prd-carousel').length) this.prdCarousel();
			if ($('.js-prd-carousel-dots').length) this.prdCarouselDots();
			if ($('.js-instagram-feed').length) this.instaFeed('.js-instagram-feed');
			if ($('.js-gallery-isotope').length) this.galleryIsotope('.js-gallery-isotope');
			if ($('.js-product-isotope').length) this.productIsotope('.js-product-isotope');
			if ($('.js-product-isotope-sm').length) this.productIsotopeSm('.js-product-isotope-sm');
			if ($('.compare-table').length) this.compareTable();
			if ($('[data-count]').length) this.countTo('[data-count]', 5000);
		},
		productTabs: function productTabs() {
			$('.js-circle-loader').on('click', function (e) {
				var $this = $(this),
				    $container = $this.closest('.holder').find('.prd-grid');
				if ($this.data('loaded') == $this.data('total')) return false;
				$.getJSON($this.attr('href'), function (data) {
					var html = '',
					    i = 0,
					    max = Math.min(parseInt($this.data('total'), 10), $this.data('load') + $this.data('loaded')) - 1,
					    min = parseInt($this.data('loaded'), 10) - 1;
					$.each(data, function (key, val) {
						if (i > min && i <= max) {
							html += val.product;
							setTimeout(function () {
								THEME.product.postAjaxProduct();
							}, 500);
						}
						i++;
					});
					$container.append(html);
					$this.data('loaded', $this.data('loaded') + $this.data('load'));
					if ($this.data('loaded') == $this.data('total')) {
						$this.closest('.circle-loader-wrap').addClass('d-none');
					}
					$('.js-circle-loader-start', $this).html($this.data('loaded'));
					$('.js-circle-loader-end', $this).html($this.data('total'));

					var start = $this.data('loaded') * 100 / $this.data('total'),
					    to = ($this.data('loaded') + $this.data('load')) * 100 / $this.data('total');
					$this.attr('data-start', start);
					$this.attr('data-to', to);
					THEME.setPersentCircle ? THEME.setPersentCircle.reinit() : false;
				});
				e.preventDefault();
			});
			$('[data-grid-tab-title]').on('click', function (e) {
				var $this = $(this),
				    $container = $this.closest('.holder').find('.prd-grid');
				if ($this.parent().hasClass('active')) return false;
				THEME.loaderHorizontalSm ? THEME.loaderHorizontalSm.open() : false;
				THEME.loaderTab ? THEME.loaderTab.open('productsGrid01') : false;
				if ($this.data('loaded') < $this.data('total')) {
					$('.circle-loader-wrap').removeClass('d-none');
				} else $('.circle-loader-wrap').addClass('d-none');
				$this.parent().siblings().removeClass('active');
				$this.parent().addClass('active');

				var $loader = $this.closest('.holder').find('.js-circle-loader'),
				    start = $this.data('loaded') * 100 / $this.data('total'),
				    to = ($this.data('loaded') + $('.js-circle-loader').data('load')) * 100 / $this.data('total');
				$loader.attr('href', $this.attr('href'));
				$loader.attr('data-start', start);
				$loader.attr('data-to', to);
				$loader.data('total', $this.data('total'));
				$loader.data('loaded', $this.data('loaded'));
				THEME.setPersentCircle ? THEME.setPersentCircle.reinit() : false;

				$('.js-circle-loader-start', $loader).html($this.data('loaded'));
				$('.js-circle-loader-end', $loader).html($this.data('total'));

				$.getJSON($this.attr('href'), function (data) {
					var html = '',
					    i = 0,
					    max = parseInt($this.data('loaded'), 10);
					$.each(data, function (key, val) {
						if (i < max) {
							html += val.product;
							setTimeout(function () {
								THEME.product.postAjaxProduct();
							}, 500);
						}
						i++;
					});
					$container.html(html);
					THEME.loaderTab ? THEME.loaderTab.close('productsGrid01') : false;
					THEME.loaderHorizontalSm ? THEME.loaderHorizontalSm.close() : false;
				});
				e.preventDefault();
			});
			$('.js-title-tabs').each(function () {
				var $this = $(this);
				$this.perfectScrollbar();
				if ($this.find('.active').length) {
					$this.find('.active > [data-grid-tab-title]').trigger('click');
				} else {
					$this.find('[data-grid-tab-title]').first().trigger('click');
				}
			});
		},
		postWidth: function postWidth(post) {
			$(post).each(function () {
				var $this = $(this);
				$this.removeClass('post-prw--hor');
				if ($this.width() < 570) {
					$this.addClass('post-prw--hor');
				}
			});
		},
		countTo: function countTo(obj, duration) {
			function counting() {
				$(obj).each(function () {
					var $this = $(this),
					    countTo = $this.attr('data-count');
					$({
						countNum: $this.text()
					}).animate({
						countNum: countTo
					}, {
						duration: duration,
						easing: 'linear',
						step: function step() {
							$this.text(Math.floor(this.countNum), 10);
						},
						complete: function complete() {
							$this.text(this.countNum);
						}
					});
				});
			}

			if ($(obj).closest('.lazyloaded').length) {
				counting();
			} else {
				$(obj).closest('.lazyload').on('lazyloaded', function () {
					counting();
				});
			}
		},
		compareTable: function compareTable(table) {
			$(table).perfectScrollbar();
			$(document).on('click', '.js-delete-from-compare', function (e) {
				var $this = $(e.target),
				    i = $this.closest('.compare-table-cell').index() + 1;
				$('.compare-table-row').find('.compare-table-cell:nth-child(' + i + ')').remove();
				$this.closest('.compare-table-cell').remove();
				if ($('.compare-table-head-row').children().length < 2) {
					$('.compare-table').hide();
					$('.js-compare-empty').removeClass('d-none');
				}
				e.preventDefault();
			});
			$(document).on('click', '.js-delete-all-compare', function (e) {
				$('.compare-table').hide();
				$('.js-compare-empty').removeClass('d-none');
				e.preventDefault();
			});
		},
		arrowCenter: function arrowCenter(carousel, arrow, image, delay) {
			var $carousel = carousel;
			image = $carousel.attr('data-center') ? $carousel.attr('data-center') : image;
			delay = delay ? delay : 1000;
			setTimeout(function () {
				$(arrow, $carousel).css({
					'top': $(image, $carousel).height() * 0.5,
					'opacity': 1
				});
			}, delay);
			$carousel.addClass('js-arrowCenter').attr('data-center', image);
		},
		creativeSlider: function creativeSlider(el) {
			var $slider = $(el),
			    $sliderWrap = $('.creative-product-carousel-wrap'),
			    tabs = '.js-creative-product-carousel-tabs';

			function doAnimations(elements, type, delay) {
				var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
				elements.each(function (i) {
					var $this = $(this),
					    $animationDelay = i * 200 + delay,
					    $animationType = type;
					$this.css({
						'animation-delay': $animationDelay + 'ms',
						'-webkit-animation-delay': $animationDelay + 'ms'
					});
					$this.addClass('animated ' + $animationType).one(animationEndEvents, function () {
						$this.removeClass($animationType);
					});
				});
			}

			function doAnimationsEnd(elements, type, delay) {
				var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
				elements.each(function (i) {
					var $this = $(this),
					    $animationDelay = i * 200 + delay,
					    $animationType = type;
					$this.css({
						'animation-delay': $animationDelay + 'ms',
						'-webkit-animation-delay': $animationDelay + 'ms'
					});
					$this.addClass($animationType).one(animationEndEvents, function () {
						$this.removeClass($animationType).removeClass('animated');
					});
				});
			}

			$slider.on('init', function (slick) {
				var $currentSlide = $('.slick-current', $slider);
				setTimeout(function () {
					$('[data-src]', $currentSlide).each(function () {
						var $this = $(this);
						if ($this.closest('.col').is(':visible')) {
							var img = $this.attr('data-src');
							$this.attr('src', img).addClass('lazyloaded');
						}
					});
					doAnimations($currentSlide.find('.col:not(.prd-single-col)'), 'fadeInRight', 1000);
					doAnimations($currentSlide.find('.js-creative-image'), 'driveInLeft', 1000);
					doAnimations($currentSlide.find('.js-creative-info'), 'fadeIn', 1000);
					doAnimations($currentSlide.find('.creative-product-carousel-text:not(.creative-prd-single-text)'), 'fadeInRightBig', 800);
					doAnimations($currentSlide.find('.creative-prd-single-text'), 'fadeIn', 800);
				}, 100);
			});
			$slider.on('afterChange', function (e, slick) {
				slick = $(slick.$slider);
				var $currentSlide = slick.find('.slick-current'),
				    index = parseInt($currentSlide.attr('data-slick-index'), 10) + 1;
				doAnimations($currentSlide.find('.col:not(.prd-single-col)'), 'fadeInRight', 200);
				doAnimations($currentSlide.find('.js-creative-image'), 'driveInLeft', 200);
				doAnimations($currentSlide.find('.js-creative-info'), 'fadeIn', 200);
				doAnimations($currentSlide.find('.creative-product-carousel-text:not(.creative-prd-single-text)'), 'fadeInRightBig', 0);
				doAnimations($currentSlide.find('.creative-prd-single-text'), 'fadeIn', 0);
				slick.next(tabs).find('.js-collection-grid-nav').removeClass('active');
				slick.next(tabs).find('.js-collection-grid-nav:nth-child(' + index + ')').addClass('active');
				setTimeout(function () {
					slick.next(tabs).find('.js-collection-grid-nav').removeClass('disable');
				}, 1000);
			});
			$slider.on('beforeChange', function (e, slick) {
				slick = $(slick.$slider);
				var $currentSlide = slick.find('.slick-current');
				doAnimationsEnd($currentSlide.find('.col:not(.prd-single-col)'), 'fadeOutLeft', 0);
				doAnimationsEnd($currentSlide.find('.js-creative-image'), 'fadeOutRight', 0);
				doAnimationsEnd($currentSlide.find('.js-creative-info'), 'fadeOut', 0);
				doAnimationsEnd($currentSlide.find('.creative-product-carousel-text:not(.creative-prd-single-text)'), 'fadeOutLeft', 0);
				doAnimationsEnd($currentSlide.find('.creative-prd-single-text'), 'fadeOut', 0);
				slick.next(tabs).find('.js-collection-grid-nav').addClass('disable');
			});
			$slider.each(function () {
				var $slider = $(this);
				$slider.slick({
					arrows: false,
					dots: false,
					slidesToShow: 1,
					infinite: true,
					cssEase: 'linear',
					speed: 500,
					fade: true,
					draggable: false,
					touchMove: false,
					swipe: false,
					autoplay: $slider.data('autoplay'),
					autoplaySpeed: $slider.data('speed')
				});
				// for ajax loading
				$slider.next(tabs).find('.js-collection-grid-nav').on('click', function (e) {
					var $this = $(this),
					    $slide = $slider.find('[data-slick-index=' + $this.index() + ']');
					var url = document.location.origin + $this.find('a').attr('href');
					if ($this.hasClass('disable')) return false;
					$this.siblings().removeClass('active').end().addClass('active');
					if ($this.find('a').attr('href') == "#") {
						$slider.slick('slickGoTo', $this.index());
					} else if (!$this.hasClass('tab-loaded')) {
						$sliderWrap.addClass('loading');
						$.ajax({
							url: url,
							success: function success(data) {
								$this.addClass('tab-loaded');
								$slide.find('.row').html(data);
								$slider.slick('slickGoTo', $this.index());
								$sliderWrap.removeClass('loading');
							}
						});
					} else {
						$slider.slick('slickGoTo', $this.index());
					}
					e.preventDefault();
				});
			});
		},
		slickCarousels: function slickCarousels() {
			if ($('.data-slick').length) {
				$('.data-slick').each(function () {
					var $this = $(this),
					    arrowsplace = $this.parent().find('.carousel-arrows').length ? $this.parent().find('.carousel-arrows') : $this;
					if ($this.hasClass('slick-initialized')) return false;
					$this.on('init', function () {
						$this.find('.slick-arrow').css({ 'opacity': 0 });
					});
					$this.imagesLoaded(function () {
						$this.on('init', function () {
							THEME.product.productWidth('.prd', $this);
						});
						$this.slick({
							appendArrows: arrowsplace,
							swipe: swipemode,
							infinite: false,
							draggable: false
						});
						if ($this.hasClass('collection-carousel-2')) {
							if ($this.find('.lazyload').length) {
								$this.find('.lazyload').first().on('lazyloaded', function () {
									THEME.sections.arrowCenter($this, '.slick-arrow', '.collection-carousel-2-img > img', 10);
								});
							} else THEME.sections.arrowCenter($this, '.slick-arrow', '.collection-carousel-2-img > img', 10);
						}
					});
				});
			}
			if ($('.js-custom-text-carousel').length) {
				$('.js-custom-text-carousel').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					$this.slick({
						autoplay: true,
						arrows: false,
						dots: false,
						slidesToShow: 1,
						draggable: false,
						infinite: true,
						pauseOnHover: false,
						swipe: false,
						touchMove: false,
						vertical: true,
						speed: 1000,
						autoplaySpeed: 2000,
						useTransform: true,
						cssEase: 'cubic-bezier(0.645, 0.045, 0.355, 1.000)'
					});
				});
			}
			if ($('.js-prevnext-carousel').length) {
				$('.js-prevnext-carousel').each(function () {
					$(this).slick({
						arrows: true,
						dots: false,
						slidesToShow: 3,
						infinite: true,
						draggable: false
					});
				});
			}
			if ($('.js-bigcarousel').length) {
				$('.js-bigcarousel').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					if ($this.children().length > 2 && !$this.closest('.aside').length) {
						$this.slick({
							arrows: true,
							dots: false,
							slidesToShow: 1,
							centerMode: true,
							centerPadding: '150px',
							swipe: swipemode,
							draggable: false,
							responsive: [{
								breakpoint: maxMD,
								settings: {
									centerPadding: '0'
								}
							}, {
								breakpoint: maxXS,
								settings: {
									dots: true,
									arrows: false,
									centerPadding: '0'
								}
							}]
						});
					} else {
						$this.slick({
							arrows: true,
							dots: false,
							slidesToShow: 1,
							swipe: swipemode,
							draggable: false,
							responsive: [{
								breakpoint: maxXS,
								settings: {
									dots: true,
									arrows: false
								}
							}]
						});
					}
				});
			}
			if ($('.js-prd-promo-carousel').length) {
				$('.js-prd-promo-carousel').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					var slidesToShow = 3;
					if ($this.hasClass('data-to-show-4')) {
						slidesToShow = 4;
					}
					$this.slick({
						arrows: false,
						dots: true,
						slidesToShow: slidesToShow,
						slidesToScroll: 1,
						adaptiveHeight: true,
						swipe: swipemode,
						infinite: false,
						draggable: false,
						responsive: [{
							breakpoint: maxMD,
							settings: {
								slidesToShow: 3
							}
						}, {
							breakpoint: maxSM,
							settings: {
								slidesToShow: 2
							}
						}, {
							breakpoint: maxXS,
							settings: {
								slidesToShow: 1
							}
						}]
					});
				});
			}
			if ($('.js-post-prws-carousel').length) {
				$('.js-post-prws-carousel').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					var arrowsplace = $this.parent().find('.carousel-arrows');
					$this.on('init', function () {
						if ($('.js-post-prw-adaptive').length) THEME.sections.postWidth('.js-post-prw-adaptive');
					});
					$this.slick({
						arrows: true,
						dots: false,
						slidesToShow: 2,
						appendArrows: arrowsplace,
						swipe: swipemode,
						infinite: false,
						draggable: false
					});
				});
			}
			if ($('.js-testimonials-carousel').length) {
				$('.js-testimonials-carousel').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					var arrowsplace = $this.parent().parent().find('.carousel-arrows');
					$this.slick({
						arrows: true,
						dots: false,
						slidesToShow: 2,
						appendArrows: arrowsplace,
						swipe: swipemode,
						infinite: false,
						draggable: false
					});
				});
			}
			if ($('.js-testimonials-carousel-single').length) {
				$('.js-testimonials-carousel-single').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					$this.slick({
						arrows: true,
						dots: false,
						slidesToShow: 1,
						swipe: swipemode,
						infinite: true,
						draggable: false
					});
				});
			}
			if ($('.js-brand-carousel').length) {
				$('.js-brand-carousel').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					var arrowsplace = $this.parent().find('.carousel-arrows').length ? $this.parent().find('.carousel-arrows') : $this,
					    slidesToShow_lg = 5,
					    slidesToScroll_lg = 1,
					    slidesToShow_md = 4,
					    slidesToScroll_md = 1,
					    slickTimer = void 0;
					if ($this.closest('.aside ').length) {
						slidesToShow_lg = 4;
						slidesToScroll_lg = 1;
						slidesToShow_md = 3;
						slidesToScroll_md = 1;
					}
					$this.slick({
						arrows: false,
						dots: false,
						slidesToShow: slidesToShow_lg,
						slidesToScroll: slidesToScroll_lg,
						appendArrows: arrowsplace,
						swipe: swipemode,
						infinite: true,
						draggable: false,
						speed: 750,
						autoplay: true,
						autoplaySpeed: 3000,
						cssEase: 'cubic-bezier(0.645, 0.045, 0.355, 1.000)',
						//waitForAnimate: true,
						responsive: [{
							breakpoint: maxMD,
							settings: {
								speed: 1000,
								slidesToShow: slidesToShow_md,
								slidesToScroll: slidesToScroll_md
							}
						}, {
							breakpoint: maxXS,
							settings: {
								speed: 500,
								slidesToShow: 2,
								slidesToScroll: 1
							}
						}]
					});
				});
			}
			if ($('.js-prd-carousel-vert').length) {
				$('.js-prd-carousel-vert').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					var arrowsplace = $this.parent().find('.carousel-arrows').length ? $this.parent().find('.carousel-arrows') : $this;
					$this.slick({
						slidesToShow: 2,
						slidesToScroll: 1,
						arrows: true,
						vertical: true,
						appendArrows: arrowsplace,
						swipe: swipemode,
						speed: 300,
						infinite: false,
						draggable: false,
						responsive: [{
							breakpoint: maxLG,
							settings: {
								vertical: false,
								slidesToShow: 1,
								slidesToScroll: 1
							}
						}, {
							breakpoint: maxXS,
							settings: {
								vertical: false,
								slidesToShow: 2,
								slidesToScroll: 1
							}
						}]
					});
				});
			}
			if ($('.js-prd-carousel-vert-2').length) {
				$('.js-prd-carousel-vert-2').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					var arrowsplace = $this.parent().parent().find('.js-prd-carousel-vert-arrows').length ? $this.parent().parent().find('.js-prd-carousel-vert-arrows') : $this.parent().find('.js-prd-carousel-vert-arrows').length ? $this.parent().find('.js-prd-carousel-vert-arrows') : $this;
					$this.slick({
						slidesToShow: 2,
						slidesToScroll: 1,
						arrows: true,
						vertical: true,
						appendArrows: arrowsplace,
						swipe: swipemode,
						speed: 300,
						infinite: false,
						draggable: false,
						responsive: [{
							breakpoint: maxLG,
							settings: {
								slidesToShow: 1,
								slidesToScroll: 1
							}
						}]
					});
				});
			}
			if ($('.js-prd-carousel-single').length) {
				$('.js-prd-carousel-single').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					var arrowsplace = $this.parent().find('.carousel-arrows').length ? $this.parent().find('.carousel-arrows') : $this;
					$this.slick({
						slidesToShow: 1,
						slidesToScroll: 1,
						arrows: true,
						appendArrows: arrowsplace,
						swipe: swipemode,
						speed: 300,
						infinite: false,
						draggable: false,
						responsive: [{
							breakpoint: maxMD,
							settings: {
								slidesToShow: 1,
								slidesToScroll: 1
							}
						}, {
							breakpoint: maxSM,
							settings: {
								slidesToShow: 2,
								slidesToScroll: 1
							}
						}, {
							breakpoint: maxXS,
							settings: {
								slidesToShow: 2,
								slidesToScroll: 1
							}
						}]
					});
				});
			}
			if ($('.js-prd-carousel-modal-checkout').length) {
				$('.js-prd-carousel-modal-checkout').each(function () {
					var $this = $(this);
					if ($this.hasClass('slick-initialized')) return false;
					$this.slick({
						arrows: true,
						dots: false,
						slidesToShow: 4,
						infinite: true,
						draggable: false,
						responsive: [{
							breakpoint: maxMD,
							settings: {
								slidesToShow: 3
							}
						}, {
							breakpoint: maxSM,
							settings: {
								slidesToShow: 2
							}
						}, {
							breakpoint: maxXS,
							settings: {
								slidesToShow: 1
							}
						}]
					});
				});
			}
		},
		prdCarousel: function prdCarousel() {
			THEME.prdcarousel = {
				defaults: {
					carousel: '.js-prd-carousel'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					if (w < maxSM && $(this.defaults.carousel).hasClass('js-product-grid-sm')) {
						return false;
					}
					this.reinit();
				},
				reinit: function reinit() {
					if (w < maxSM && $(this.defaults.carousel).hasClass('js-product-grid-sm')) {
						if ($(this.defaults.carousel).hasClass('slick-initialized')) {
							$(this.defaults.carousel).css({
								'height': ''
							}).slick('unslick');
						}
						return false;
					} else if ($(this.defaults.carousel).hasClass('slick-initialized')) {
						return false;
					}
					$(this.defaults.carousel).each(function () {
						var $this = $(this),
						    arrowsplace = void 0;
						if ($this.parent().find('.carousel-arrows').length) {
							arrowsplace = $this.parent().find('.carousel-arrows');
						} else if ($this.closest('.holder').find('.carousel-arrows').length) {
							arrowsplace = $this.closest('.holder').find('.carousel-arrows');
						}
						$this.on('beforeChange', function () {
							$this.find('.color-swatch').each(function () {
								$(this).find('.js-color-toggle').first().trigger('click');
							});
						});
						$this.on('init', function () {
							THEME.product.productWidth('.prd', $this);
						});
						var slidesToShow = 4,
						    slidesToShowLG = 4,
						    slidesToShowMD = 3,
						    slidesToShowSM = 2,
						    slidesToShowXS = 2,
						    speed = 500;
						if ($this.hasClass('data-to-show-6')) {
							slidesToShow = 6;
							speed = 300;
						} else if ($this.hasClass('data-to-show-5')) {
							slidesToShow = 5;
							speed = 300;
						} else if ($this.hasClass('data-to-show-3')) {
							slidesToShow = 3;
							speed = 300;
						} else if ($this.hasClass('data-to-show-2')) {
							slidesToShow = 2;
							speed = 200;
						} else if ($this.hasClass('data-to-show-1')) {
							slidesToShow = 1;
							speed = 200;
						}
						if ($this.hasClass('data-to-show-lg-6')) {
							slidesToShowLG = 6;
						} else if ($this.hasClass('data-to-show-lg-5')) {
							slidesToShowLG = 5;
						} else if ($this.hasClass('data-to-show-lg-3')) {
							slidesToShowLG = 3;
						} else if ($this.hasClass('data-to-show-lg-2')) {
							slidesToShowLG = 2;
						} else if ($this.hasClass('data-to-show-lg-1')) {
							slidesToShowLG = 1;
						}
						if ($this.hasClass('data-to-show-md-6')) {
							slidesToShowMD = 6;
						} else if ($this.hasClass('data-to-show-md-5')) {
							slidesToShowMD = 5;
						} else if ($this.hasClass('data-to-show-md-3')) {
							slidesToShowMD = 3;
						} else if ($this.hasClass('data-to-show-md-2')) {
							slidesToShowMD = 2;
						} else if ($this.hasClass('data-to-show-md-1')) {
							slidesToShowMD = 1;
						}
						if ($this.hasClass('data-to-show-sm-6')) {
							slidesToShowSM = 6;
						} else if ($this.hasClass('data-to-show-sm-5')) {
							slidesToShowSM = 5;
						} else if ($this.hasClass('data-to-show-sm-3')) {
							slidesToShowSM = 3;
						} else if ($this.hasClass('data-to-show-sm-2')) {
							slidesToShowSM = 2;
						} else if ($this.hasClass('data-to-show-sm-1')) {
							slidesToShowSM = 1;
						}
						if ($this.hasClass('data-to-show-xs-6')) {
							slidesToShowXS = 6;
						} else if ($this.hasClass('data-to-show-xs-5')) {
							slidesToShowXS = 5;
						} else if ($this.hasClass('data-to-show-xs-3')) {
							slidesToShowXS = 3;
						} else if ($this.hasClass('data-to-show-xs-2')) {
							slidesToShowXS = 2;
						} else if ($this.hasClass('data-to-show-xs-1')) {
							slidesToShowXS = 1;
						}
						$this.slick({
							slidesToShow: slidesToShow,
							slidesToScroll: slidesToShow,
							arrows: true,
							appendArrows: arrowsplace,
							adaptiveHeight: true,
							swipe: swipemode,
							speed: speed,
							infinite: false,
							draggable: false,
							responsive: [{
								breakpoint: maxLG,
								settings: {
									slidesToShow: slidesToShowLG,
									slidesToScroll: slidesToShowLG
								}
							}, {
								breakpoint: maxMD,
								settings: {
									slidesToShow: slidesToShowMD,
									slidesToScroll: 1
								}
							}, {
								breakpoint: maxSM,
								settings: {
									slidesToShow: slidesToShowSM,
									slidesToScroll: 1
								}
							}, {
								breakpoint: maxXS,
								settings: {
									slidesToShow: slidesToShowXS,
									slidesToScroll: 1
								}
							}]
						});
					});
				}
			};
			THEME.prdcarousel.init();
		},
		prdCarouselDots: function prdCarouselDots() {
			THEME.prdcarousel = {
				defaults: {
					carousel: '.js-prd-carousel-dots'
				},
				init: function init(options) {
					this.reinit();
				},
				reinit: function reinit() {
					if ($(this.defaults.carousel).hasClass('slick-initialized')) {
						return false;
					}
					$(this.defaults.carousel).each(function () {
						var $this = $(this),
						    arrowsplace = void 0;
						if ($this.parent().find('.carousel-arrows').length) {
							arrowsplace = $this.parent().find('.carousel-arrows');
						} else if ($this.closest('.holder').find('.carousel-arrows').length) {
							arrowsplace = $this.closest('.holder').find('.carousel-arrows');
						}
						$this.on('beforeChange', function () {
							$this.find('.color-swatch').each(function () {
								$(this).find('.js-color-toggle').first().trigger('click');
							});
						});
						$this.on('init', function () {
							THEME.product.productWidth('.prd', $this);
						});
						var slidesToShow = 4,
						    slidesToShowLG = 4,
						    slidesToShowMD = 3,
						    slidesToShowSM = 2,
						    slidesToShowXS = 2,
						    speed = 500;
						if ($this.hasClass('data-to-show-6')) {
							slidesToShow = 6;
							speed = 300;
						} else if ($this.hasClass('data-to-show-5')) {
							slidesToShow = 5;
							speed = 300;
						} else if ($this.hasClass('data-to-show-3')) {
							slidesToShow = 3;
							speed = 300;
						} else if ($this.hasClass('data-to-show-2')) {
							slidesToShow = 2;
							speed = 200;
						} else if ($this.hasClass('data-to-show-1')) {
							slidesToShow = 1;
							speed = 200;
						}
						if ($this.hasClass('data-to-show-lg-6')) {
							slidesToShowLG = 6;
						} else if ($this.hasClass('data-to-show-lg-5')) {
							slidesToShowLG = 5;
						} else if ($this.hasClass('data-to-show-lg-3')) {
							slidesToShowLG = 3;
						} else if ($this.hasClass('data-to-show-lg-2')) {
							slidesToShowLG = 2;
						} else if ($this.hasClass('data-to-show-lg-1')) {
							slidesToShowLG = 1;
						}
						if ($this.hasClass('data-to-show-md-6')) {
							slidesToShowMD = 6;
						} else if ($this.hasClass('data-to-show-md-5')) {
							slidesToShowMD = 5;
						} else if ($this.hasClass('data-to-show-md-3')) {
							slidesToShowMD = 3;
						} else if ($this.hasClass('data-to-show-md-2')) {
							slidesToShowMD = 2;
						} else if ($this.hasClass('data-to-show-md-1')) {
							slidesToShowMD = 1;
						}
						if ($this.hasClass('data-to-show-sm-6')) {
							slidesToShowSM = 6;
						} else if ($this.hasClass('data-to-show-sm-5')) {
							slidesToShowSM = 5;
						} else if ($this.hasClass('data-to-show-sm-3')) {
							slidesToShowSM = 3;
						} else if ($this.hasClass('data-to-show-sm-2')) {
							slidesToShowSM = 2;
						} else if ($this.hasClass('data-to-show-sm-1')) {
							slidesToShowSM = 1;
						}
						if ($this.hasClass('data-to-show-xs-6')) {
							slidesToShowXS = 6;
						} else if ($this.hasClass('data-to-show-xs-5')) {
							slidesToShowXS = 5;
						} else if ($this.hasClass('data-to-show-xs-3')) {
							slidesToShowXS = 3;
						} else if ($this.hasClass('data-to-show-xs-2')) {
							slidesToShowXS = 2;
						} else if ($this.hasClass('data-to-show-xs-1')) {
							slidesToShowXS = 1;
						}
						$this.slick({
							slidesToShow: slidesToShow,
							slidesToScroll: slidesToShow,
							arrows: false,
							dots: true,
							appendArrows: arrowsplace,
							adaptiveHeight: true,
							swipe: swipemode,
							speed: speed,
							infinite: false,
							draggable: false,
							responsive: [{
								breakpoint: maxLG,
								settings: {
									slidesToShow: slidesToShowLG,
									slidesToScroll: slidesToShowLG
								}
							}, {
								breakpoint: maxMD,
								settings: {
									slidesToShow: slidesToShowMD,
									slidesToScroll: 1
								}
							}, {
								breakpoint: maxSM,
								settings: {
									slidesToShow: slidesToShowSM,
									slidesToScroll: 1
								}
							}, {
								breakpoint: maxXS,
								settings: {
									slidesToShow: slidesToShowXS,
									slidesToScroll: 1
								}
							}]
						});
					});
				}
			};
			THEME.prdcarousel.init();
		},
		productIsotopeSm: function productIsotopeSm(el) {
			if ($(el).length) {
				THEME.productisotopeSM = {
					defaults: {
						gallery: '.js-product-isotope-sm',
						galleryItem: '.prd',
						filtersList: '.js-filters-prd-sm',
						filtersLabel: '.filters-label',
						filtersCount: '.filters-label-count',
						activeClass: 'active',
						dataAttr: 'data-filter',
						layoutMode: 'fitRows',
						currentFilter: ''
					},
					init: function init(options) {
						$.extend(this.defaults, options);
						var that = this;
						$(that.defaults.gallery).each(function () {
							var $gallery = $(this),
							    $filtersList = $(that.defaults.filtersList, $gallery.closest('.holder'));
							if ($filtersList.length) {
								if (w >= maxSM) {
									if ($gallery.data('isotope')) {
										$gallery.isotope('destroy');
									}
								} else if (!$(that.defaults.gallery).data('isotope')) {
									that._galleryInit($gallery);
								}
							}
						});
					},
					reinit: function reinit() {
						var that = this;
						$(that.defaults.gallery).each(function () {
							var $gallery = $(this),
							    $filtersList = $(that.defaults.filtersList, $gallery.closest('.holder'));
							if ($filtersList.length) {
								if (w >= maxSM) {
									if ($gallery.data('isotope')) {
										$gallery.isotope('destroy');
									}
								} else if (!$(that.defaults.gallery).data('isotope')) {
									that._galleryInit($gallery);
								}
							}
						});
						return this;
					},
					_galleryInit: function _galleryInit(gallery) {
						var $gallery = gallery,
						    that = this,
						    $filtersList = $(that.defaults.filtersList, $gallery.closest('.holder')),
						    ltr = $body.is('.rtl') ? false : true;
						if ($filtersList.length) {
							$gallery.imagesLoaded(function () {
								$gallery.isotope({
									isOriginLeft: ltr,
									itemSelector: that.defaults.galleryItem,
									layoutMode: that.defaults.layoutMode,
									percentPosition: true,
									filter: function filter() {
										var filterResult = that.defaults.currentFilter ? $(this).is(that.defaults.currentFilter) : true;
										return filterResult;
									}
								});
							});
							that._filters(this);
						}
						return this;
					},
					_filters: function _filters(obj) {
						var activeStart = void 0,
						    $gallery = $(obj.defaults.gallery),
						    $filtersList = $(obj.defaults.filtersList, $gallery.closest('.holder')),
						    $filtersLabel = $(obj.defaults.filtersList + ' ' + obj.defaults.filtersLabel),
						    activeClass = obj.defaults.activeClass,
						    dataAttr = obj.defaults.dataAttr;
						$filtersLabel.each(function () {
							var $this = $(this),
							    $gallery = $(obj.defaults.gallery, $this.closest('.holder'));
							var filtered = $this.attr(dataAttr),
							    count = filtered == null ? $gallery.find(obj.defaults.galleryItem).length : $gallery.find(filtered).length;
							$this.find(obj.defaults.filtersCount).html(count);
							if ($this.hasClass(activeClass)) {
								activeStart = true;
								obj.defaults.currentFilter = $this.attr(dataAttr);
								$gallery.isotope();
							}
						});
						if (!activeStart) $(obj.defaults.filtersList + ' ' + obj.defaults.filtersLabel + ':first-child').addClass(activeClass);
						$filtersLabel.on('click', function (e) {
							e.preventDefault();
							var $this = $(this),
							    $gallery = $(obj.defaults.gallery, $this.closest('.holder'));
							if ($this.hasClass(activeClass)) {
								return false;
							} else {
								$filtersLabel.removeClass(activeClass);
								$this.addClass(activeClass);
							}
							obj.defaults.currentFilter = $this.attr(dataAttr);
							$gallery.isotope();
						});
					}
				};
				THEME.productisotopeSM.init();
			}
		},
		productIsotope: function productIsotope(el) {
			if ($(el).length) {
				THEME.productisotope = {
					defaults: {
						gallery: '.js-product-isotope',
						galleryItem: '.prd',
						filtersList: '.js-filters-prd',
						filtersLabel: '.filters-label',
						filtersCount: '.filters-label-count',
						activeClass: 'active',
						dataAttr: 'data-filter',
						layoutMode: 'fitRows',
						currentFilter: ''
					},
					init: function init(options) {
						$.extend(this.defaults, options);
						var $gallery = $(this.defaults.gallery),
						    $filtersList = $(this.defaults.filtersList, $gallery.closest('.holder'));
						if ($filtersList.length) {
							$.extend(this.defaults, options);
							if (!$(this.defaults.gallery).data('isotope')) {
								this._galleryInit(this);
							}
						} else return false;
					},
					reinit: function reinit() {
						var $gallery = $(this.defaults.gallery),
						    $filtersList = $(this.defaults.filtersList, $gallery.closest('.holder'));
						if ($filtersList.length) {
							if (!$(this.defaults.gallery).data('isotope')) {
								this._galleryInit(this);
							}
							var _$gallery = $(this.defaults.gallery);
							var ltr = $body.is('.rtl') ? false : true;
							_$gallery.isotope({
								isOriginLeft: ltr
							});
							return this;
						} else return false;
					},
					_galleryInit: function _galleryInit(obj) {
						var $gallery = $(obj.defaults.gallery),
						    $filtersList = $(obj.defaults.filtersList, $gallery.closest('.holder'));
						if ($filtersList.length) {
							var ltr = $body.is('.rtl') ? false : true;
							$gallery.imagesLoaded(function () {
								$gallery.isotope({
									isOriginLeft: ltr,
									itemSelector: obj.defaults.galleryItem,
									layoutMode: obj.defaults.layoutMode,
									percentPosition: true,
									filter: function filter() {
										var filterResult = obj.defaults.currentFilter ? $(this).is(obj.defaults.currentFilter) : true;
										return filterResult;
									}
								});
							});
							this._filters(this);
						}
						return this;
					},
					_filters: function _filters(obj) {
						var activeStart = void 0,
						    $gallery = $(obj.defaults.gallery),
						    $filtersList = $(obj.defaults.filtersList, $gallery.closest('.holder')),
						    $filtersLabel = $(obj.defaults.filtersList + ' ' + obj.defaults.filtersLabel),
						    activeClass = obj.defaults.activeClass,
						    dataAttr = obj.defaults.dataAttr;
						$filtersLabel.each(function () {
							var $this = $(this),
							    $gallery = $(obj.defaults.gallery, $this.closest('.holder'));
							var filtered = $this.attr(dataAttr),
							    count = filtered == null ? $gallery.find(obj.defaults.galleryItem).length : $gallery.find(filtered).length;
							$this.find(obj.defaults.filtersCount).html(count);
							if ($this.hasClass(activeClass)) {
								activeStart = true;
								obj.defaults.currentFilter = $this.attr(dataAttr);
								$gallery.isotope();
							}
						});
						if (!activeStart) $(obj.defaults.filtersList + ' ' + obj.defaults.filtersLabel + ':first-child').addClass(activeClass);
						$filtersLabel.on('click', function (e) {
							e.preventDefault();
							var $this = $(this),
							    $gallery = $(obj.defaults.gallery, $this.closest('.holder'));
							if ($this.hasClass(activeClass)) {
								return false;
							} else {
								$filtersLabel.removeClass(activeClass);
								$this.addClass(activeClass);
							}
							obj.defaults.currentFilter = $this.attr(dataAttr);
							$gallery.isotope();
						});
					}
				};
				THEME.productisotope.init();
			}
		},
		galleryIsotope: function galleryIsotope(el) {
			if ($(el).length) {
				THEME.gallery = {
					defaults: {
						gallery: '.js-gallery-isotope',
						galleryItem: '.gallery-item',
						filtersList: '.js-filters-gallery',
						filtersLabel: '.filters-label',
						filtersCount: '.filters-label-count',
						activeClass: 'active',
						dataAttr: 'data-filter',
						layoutMode: 'fitRows',
						popupImage: '[data-fancybox="gallery"]',
						currentFilter: ''
					},
					init: function init(options) {
						$.extend(this.defaults, options);
						this._galleryInit(this);
						this._filters(this);
						this._popup(this);
					},
					reinit: function reinit() {
						var $gallery = $(obj.defaults.gallery);
						$gallery.isotope();
						return this;
					},
					_galleryInit: function _galleryInit(obj) {
						var $gallery = $(obj.defaults.gallery);
						$gallery.imagesLoaded(function () {
							$gallery.isotope({
								itemSelector: obj.defaults.galleryItem,
								layoutMode: obj.defaults.layoutMode,
								percentPosition: true,
								filter: function filter() {
									var filterResult = obj.defaults.currentFilter ? $(this).is(obj.defaults.currentFilter) : true;
									return filterResult;
								}
							});
							$gallery.isotope();
						});
						return this;
					},
					_popup: function _popup(obj) {
						$('[data-fancybox]').fancybox({
							touch: false,
							backFocus: false,
							buttons: ["close"]
						});
						var $popupImage = $(obj.defaults.gallery + ' ' + obj.defaults.popupImage);
						if ($popupImage.length) {
							$popupImage.fancybox({
								touch: false,
								buttons: ["close"]
							});
						}
						return this;
					},
					_filters: function _filters(obj) {
						var activeStart = void 0,
						    $gallery = $(obj.defaults.gallery),
						    $filtersList = $(obj.defaults.filtersList, $gallery.closest('.holder')),
						    $filtersLabel = $(obj.defaults.filtersList + ' ' + obj.defaults.filtersLabel),
						    activeClass = obj.defaults.activeClass,
						    dataAttr = obj.defaults.dataAttr;
						$filtersLabel.each(function () {
							var $this = $(this);
							var filtered = $this.attr(dataAttr),
							    count = filtered == null ? $gallery.find(obj.defaults.galleryItem).length : $gallery.find(filtered).length;
							$this.find(obj.defaults.filtersCount).html(count);
							if ($this.hasClass(activeClass)) {
								activeStart = true;
								obj.defaults.currentFilter = $this.attr(dataAttr);
								$gallery.isotope();
							}
						});
						if (!activeStart) $(obj.defaults.filtersList + ' ' + obj.defaults.filtersLabel + ':first-child').addClass(activeClass);
						$filtersLabel.on('click', function (e) {
							e.preventDefault();
							var $this = $(this);
							if ($this.hasClass(activeClass)) return false;else {
								$filtersLabel.removeClass(activeClass);
								$this.addClass(activeClass);
							}
							obj.defaults.currentFilter = $this.attr(dataAttr);
							$gallery.isotope();
						});
					}
				};
				THEME.gallery.init();
			}
		},
		carouselTab: function carouselTab() {
			THEME.carouseltab = {
				defaults: {
					carousel: '.js-prd-carousel-tab',
					tabs: '.js-filters-prd',
					tab: '.js-filters-prd [data-filter]'
				},
				init: function init(options) {
					$.extend(this.defaults, options);
					if (w < maxSM) return false;
					this.reinit();
				},
				hide: function hide() {
					if (!$(this.defaults.carousel).length) {
						return false;
					}
					$(this.defaults.carousel).each(function () {
						var $this = $(this);
						if ($this.hasClass('slick-initialized')) {
							$this.removeClass('slick-initialized');
							$this.slick('slickUnfilter');
							$this.slick('unslick');
						}
					});
				},
				reinit: function reinit() {
					if (w < maxSM) return false;
					var that = this;
					that._handler();
					$(that.defaults.carousel).each(function () {
						var $this = $(this);
						var arrowsplace = void 0;
						if ($this.parent().find('.carousel-arrows').length) {
							arrowsplace = $this.parent().find('.carousel-arrows');
						} else if ($this.closest('.holder').find('.carousel-arrows').length) {
							arrowsplace = $this.closest('.holder').find('.carousel-arrows');
						}
						$this.on('beforeChange', function () {
							$this.find('.color-swatch').each(function () {
								$(this).find('.js-color-toggle').first().trigger('click');
							});
						});
						$this.on('init', function () {
							THEME.product.productWidth('.prd', $this);
						});
						var slidesToShow = parseInt($this.attr('data-to-show'), 10);
						if (w < maxMD) {
							slidesToShow = 3;
						}
						$this.slick({
							slidesToShow: slidesToShow,
							slidesToScroll: slidesToShow,
							arrows: true,
							appendArrows: arrowsplace,
							adaptiveHeight: true,
							swipe: swipemode,
							speed: 400,
							infinite: false
						});
					});
					$(that.defaults.tabs).find('.active').trigger('click');
				},
				_handler: function _handler() {
					var that = this;
					if ($(that.defaults.carousel).hasClass('.slick-initialized')) {
						return false;
					}
					$(that.defaults.tab, $(that.defaults.carousel).closest('.holder')).on('click', function (e) {
						var $this = $(this),
						    $carousel = $('#' + $this.parent().attr('data-grid')),
						    filtername = $this.attr('data-filter');
						$this.siblings().removeClass('active');
						$this.addClass('active');
						$carousel.slick('slickUnfilter');
						$carousel.slick('slickFilter', '.' + filtername);
						e.preventDefault();
					});
				}
			};
			THEME.carouseltab.init();
		},
		instaFeed: function instaFeed(el) {
			$(el).each(function () {
				var $el = $(this),
				    tag = $el.data('tag'),
				    token = $el.data('token'),
				    limit = $el.data('limit'),
				    id = $el.attr('id');

				var userFeed = new Instafeed({
					get: 'user',
					target: id,
					accessToken: token,
					resolution: 'low_resolution',
					template: '<a href={{link}} target="_blank" id={{id}}><span><img src={{image}} /></span></a>',
					links: false,
					limit: limit,
					filter: function filter(item) {
						return item.model.caption.indexOf(tag) >= 0;
					}
				});
				userFeed.run();
				function startInstagramCarousel(carousel) {
					var $carousel = $(carousel);
					if ($carousel.hasClass('slick-initialized')) {
						$carousel.slick('setPosition');
						return false;
					}
					$carousel.find('a').each(function () {
						$(this).attr('target', '_blank');
					});
					var arrowsplace = $carousel.closest('.holder').find('.carousel-arrows');
					$carousel.slick({
						speed: 500,
						slidesToShow: 7,
						slidesToScroll: 2,
						arrows: true,
						appendArrows: arrowsplace,
						swipe: swipemode,
						infinite: false,
						responsive: [{
							breakpoint: maxMD,
							settings: {
								slidesToShow: 5,
								slidesToScroll: 2
							}
						}, {
							breakpoint: maxSM,
							settings: {
								slidesToShow: 4,
								slidesToScroll: 2
							}
						}, {
							breakpoint: maxXS,
							settings: {
								slidesToShow: 3,
								slidesToScroll: 1
							}
						}]
					});
				}

				function doStuff(timer) {
					if ($el.find('img').length) {
						clearInterval(timer);
						$el.find('img').each(function () {
							$(this).removeAttr('style').wrap('<span></span>');
						});
						if ($el.closest('.instagram-carousel').length) {
							startInstagramCarousel($el);
						}
					}
				}

				var timer = setInterval(function () {
					doStuff(timer);
				}, 100);
			});
		}
	};
	THEME.beforeReady = {
		init: function init() {
			THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
		}
	};
	THEME.documentReady = {
		init: function init() {
			$body.addClass('ready');
			THEME.header.mobileMenu('.mobilemenu');
			THEME.initialization.init();
			THEME.header.init();
			THEME.forms.init();
			THEME.product.init();
			if (catalogPage) THEME.catalog.init();
			if (productPage) THEME.productPage.productTitleReposition();
			THEME.initialization.initDelay();
			THEME.special.init();
			THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
			THEME.product.countdownWidth('.countdown-box-full-text');
			if ($('.js-creative-product-carousel').length) THEME.sections.creativeSlider('.js-creative-product-carousel');
		}
	};
	THEME.documentLoad = {
		init: function init() {
			$body.addClass('documentLoad');
			w = window.innerWidth || $window.width();
			h = window.innerHeight || $window.height();
			THEME.sections.init();
			if (THEME.sidefixed) THEME.sidefixed.reinit();
			THEME.initialization.scrollOnLoad();
			THEME.product.postAjaxProduct();
			$('.slick-initialized').slick('setPosition');
			THEME.header.stickyHeaderInit('.hdr-content-sticky');
			if ($('.hdr-landing-mmenu').length) THEME.header.landingMenu();
			THEME.catalog.mobileFilterHorizontal('.js-filter-col-horizontal');
			THEME.modals.init();
			if (productPage) THEME.productPage.init();
			THEME.productFeatures.init();
		}
	};
	THEME.documentResize = {
		init: function init() {
			$body.addClass('is-resizing');
			setVH();
			clearTimeout(resizeTimer);

			function allResize() {
				scrollWidth = calcScrollWidth();
				w = window.innerWidth || $window.width();
				h = window.innerHeight || $window.height();
				setVH();
				isMobile = w < mobileMenuBreikpoint;
				THEME.initialization.compensateScrollBar();
				THEME.mobilemenupush ? THEME.mobilemenupush.reinit() : false;
				THEME.slidertexttopshift ? THEME.slidertexttopshift.reinit() : false;
				THEME.setfullheight ? THEME.setfullheight.reinit() : false;
				THEME.setfullheightslider ? THEME.setfullheightslider.reinit() : false;
				THEME.sidefixed ? THEME.sidefixed.reinit() : false;
				THEME.headerdrop ? THEME.headerdrop.reinit() : false;
				THEME.quickView ? THEME.quickView.reinit() : false;
				THEME.setPersentCircle ? THEME.setPersentCircle.reinit() : false;
				THEME.productPage.shiftTitle();
				if (THEME.mainslider) {
					$.each(THEME.mainslider, function (key) {
						THEME.mainslider[key].reinit();
					});
				}
				THEME.viewMode ? THEME.viewMode.reinit() : false;
				if ($('.brand-grid').length) THEME.initialization.brandsShowMore();
				$body.removeClass('is-resizing');
			};
			if ((window.innerWidth || $window.width()) != w) {
				THEME.carouseltab ? THEME.carouseltab.hide() : false;
				THEME.flowtype ? THEME.flowtype.hide('.bnr[data-fontratio]') : false;
				THEME.viewMode ? THEME.viewMode.reinit() : false;
			}
			resizeTimer = setTimeout(function () {
				if ((window.innerWidth || $window.width()) == w) {
					allResize();
				} else {
					allResize();
					THEME.initialization.animations();
					THEME.prdrepos ? THEME.prdrepos.reinit() : false;
					THEME.stickyheader ? THEME.stickyheader.reinit() : false;
					THEME.prdcarousel ? THEME.prdcarousel.reinit() : false;
					THEME.mobileFilter ? THEME.mobileFilter.reinit() : false;
					THEME.productisotopeSM ? THEME.productisotopeSM.reinit() : false;
					THEME.carouseltab ? THEME.carouseltab.reinit() : false;
					THEME.product.productWidth('.prd, .prd-hor, .prd-promo');
					THEME.product.countdownWidth('.countdown-box-full-text');
					THEME.popup ? THEME.popup.reinit() : false;
					THEME.mobilemenupush ? THEME.mobilemenupush.reinit() : false;
					THEME.flowtype ? THEME.flowtype.reinit('.bnr[data-fontratio]') : false;
					THEME.productpagegallery ? THEME.productpagegallery.reinit() : false;
					THEME.productpagegallery_qw ? THEME.productpagegallery_qw.reinit() : false;
					if ($('.js-post-prw-adaptive').length) THEME.sections.postWidth('.js-post-prw-adaptive');
					if ($('.bnslider .video').length) THEME.Video.resizeVideo();
					$('.slick-initialized').slick('setPosition');
					$('.js-arrowCenter').each(function () {
						THEME.sections.arrowCenter($(this), '.slick-arrow', 'img', 100);
					});
					if ($('svg > rect').length) THEME.initialization.resizeSvgRect();
				}
			}, 500);
		}
	};
	var $body = $('body'),
	    $window = $(window),
	    $document = $(document),
	    w = window.innerWidth || $window.width(),
	    h = window.innerHeight || $window.height(),
	    resizeTimer = void 0,
	    scrollWidth = calcScrollWidth(),
	    promoToplineHeight = 0,
	    swipemode = false,
	    maxXS = 480,
	    maxSM = 768,
	    maxMD = 992,
	    maxLG = 1200,
	    mobileMenuBreikpoint = 1025,
	    productPreviewHorBreikpoint = 1025,
	    isMobile = w < mobileMenuBreikpoint,
	    productPage = $body.hasClass('template-product'),
	    catalogPage = $body.hasClass('template-collection');
	setVH();
	THEME.beforeReady.init();
	THEME.documentReady.init();
	$window.on('load', THEME.documentLoad.init);
	$window.on('resize', THEME.documentResize.init);
})(jQuery);