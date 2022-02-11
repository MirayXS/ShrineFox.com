<?php
/**
 * The template for displaying the header.
 *
 * Displays all of the head element and everything up until the "site-content" div.
 *
 * @package Primer
 * @since   1.0.0
 */

?><!DOCTYPE html>

<html <?php language_attributes(); ?>>

<head>
	
	<meta charset="<?php bloginfo( 'charset' ); ?>">

	<meta name="viewport" content="width=device-width, initial-scale=1">

	<link rel="profile" href="http://gmpg.org/xfn/11">

	<link rel="pingback" href="<?php bloginfo( 'pingback_url' ); ?>">

	<?php wp_head(); ?>

	
    <link rel="stylesheet" href="https://shrinefox.com/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/spectre.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/spectre-icons.css" />
    <!--ShrineFox Styles-->
    <link rel="stylesheet" href="https://shrinefox.com/css/ytv.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/theme/theme.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/theme/notice.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/theme/tags.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/theme/scrollbar.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/theme/header.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/theme/google.css" />
    <link rel="stylesheet" href="https://shrinefox.com/css/theme/feedek.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
    <script src="https://shrinefox.com/js/jquery.min.js" type="text/javascript"></script>
    <script src="https://shrinefox.com/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <script src="https://shrinefox.com/js/jscolor.js" type="text/javascript"></script>
    <script src="https://shrinefox.com/js/theme.js" type="text/javascript"></script>
    <script src="https://shrinefox.com/js/ytv.js" type="text/javascript"></script>
    <script src="https://shrinefox.com/js/FeedEk.min.js" type="text/javascript"></script>
    
</head>

<body <?php body_class(); ?>>

	<?php

	/**
	 * Fires inside the `<body>` element.
	 *
	 * @since 1.0.0
	 */
	do_action( 'primer_body' );

	?>

	<div id="page" class="hfeed site">

		<a class="skip-link screen-reader-text" href="#content"><?php esc_html_e( 'Skip to content', 'primer' ); ?></a>

		<?php

		/**
		 * Fires before the `<header>` element.
		 *
		 * @since 1.0.0
		 */
		do_action( 'primer_before_header' );

		$masthead_classes = array( 'site-header' );

		if ( has_header_video() && primer_is_amp() ) {

			$masthead_classes[] = 'video-header';

		}
		?>

	
    <header>
        <section class="navbar-section desktop-menu">
            <nav class="navbar dropmenu animated navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3">
                <div class="container accent">
                    <a class="navbar-brand" href="#" onclick="window.location.reload(true);">
                        <!--Logo-->
                        <svg version="1.0" class="amicitialogo" xmlns="http://www.w3.org/2000/svg"
                             width="500.000000pt" height="500.000000pt" viewBox="0 0 500.000000 500.000000"
                             preserveAspectRatio="xMidYMid meet">
                            <metadata>
                            </metadata>
                            <g transform="translate(0.000000,500.000000) scale(0.100000,-0.100000)"
                               fill="#ffffff" stroke="none">
                                <path d="M2256 4489 c-926 -119 -1651 -850 -1766 -1779 -15 -125 -12 -405 5
                                        -529 65 -450 258 -836 579 -1157 67 -66 164 -152 216 -191 136 -100 310 -203
                                        343 -203 14 0 48 114 141 481 l43 166 -143 246 c-78 136 -141 247 -139 247 15
                                        0 424 113 442 122 12 6 20 14 18 18 -2 4 30 132 71 286 41 153 120 445 174
                                        647 l99 369 -143 246 c-121 208 -141 248 -126 254 9 3 113 32 232 63 210 56
                                        215 58 215 84 1 14 37 160 82 324 44 165 78 303 75 308 -7 11 -325 9 -418 -2z" />
                                <path d="M3135 3840 c-4 -16 -4 -34 -1 -39 3 -4 -39 -249 -94 -542 -55 -294
                                        -100 -546 -100 -561 l0 -28 210 0 210 0 0 28 c0 15 -45 267 -100 561 -55 293
                                        -97 538 -94 542 8 13 -6 69 -16 69 -5 0 -11 -13 -15 -30z" />
                                <path d="M2806 2008 c-180 -966 -256 -1368 -261 -1390 l-7 -28 606 0 606 0 0
                                        23 c0 13 -56 324 -125 693 -69 368 -128 684 -131 702 l-6 32 -338 0 -338 0 -6
                                        -32z" />
                            </g>
                        </svg>
                    </a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"><i class="fas fa-bars"></i></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <!--Home-->
                                <a class="nav-link text-dark home" href="https://shrinefox.com/">Home</a>
                            </li>
                            <li class="nav-item">
                                <!--Browse-->
                                <a class="nav-link text-dark browse" href="https://shrinefox.com/Browse">Browse</a>
                            </li>
                            <li class="nav-item">
                                <!--Apps-->
                                <a class="nav-link text-dark apps" style="margin-right:20px;">
                                    Apps
                                </a>
                                <ul>
                                    <li class="nav-item">
                                        <!--PCSX2 PNACH Creator-->
                                        <a href="https://shrinefox.com/apps/PNACHCreator" class="nav-link text-dark pnachcreator">
                                            PCSX2 PNACH Creator
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <!--RPCS3 Patch Creator-->
                                        <a href="https://shrinefox.com/apps/PatchCreator" class="nav-link text-dark patchcreator">
                                            RPCS3 Patch Creator
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <!--PS4 Update Creator-->
                                        <a href="https://shrinefox.com/apps/UpdateCreator" class="nav-link text-dark updatecreator">
                                            PS4 Update Creator
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <!--Text Search-->
                                        <a href="https://shrinefox.com/apps/TextSearch" class="nav-link text-dark textsearch">
                                            Text Search
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <!--Files-->
                                        <a href="https://shrinefox.com/apps/Files" class="nav-link text-dark files">
                                            Files
                                        </a>
                                    </li>
                                </ul>
                            </li>
                            <li class="nav-item">
                                <!--Forum-->
                                <a class="nav-link text-dark forum" href="https://shrinefox.com/Forum">Forum</a>
                            </li>
                            <li class="nav-item">
                                <!--Wiki-->
                                <a class="nav-link text-dark wiki" href="https://shrinefox.com/Wiki">Wiki</a>
                            </li>
                            <li class="nav-item">
                                <!--FAQs-->
                                <a class="nav-link text-dark faqs" href="https://shrinefox.com/Forum/app.php/faqpage">FAQs</a>
                            </li>
                            <li class="nav-item">
                                <!--Articles-->
                                <a class="nav-link text-dark articles" style="margin-right:20px;">
                                    Articles
                                </a>
                                <ul>
                                    <li class="nav-item">
                                        <!--News-->
                                        <a href="https://shrinefox.com/news" class="nav-link text-dark news">
                                            News
                                        </a>
                                        <ul id="news-feed"></ul>
                                    </li>
                                    <li class="nav-item">
                                        <!--Guides-->
                                        <a href="https://shrinefox.com/guides" class="nav-link text-dark guides">
                                            Guides
                                        </a>
                                        <ul id="guides-feed"></ul>
                                    </li>
                                    <li class="nav-item">
                                        <!--Blog-->
                                        <a href="https://shrinefox.com/blog" class="nav-link text-dark blog">
                                            Blog
                                        </a>
                                        <ul id="blog-feed"></ul>
                                    </li>
                                </ul>
                            </li>
                            <li class="nav-item">
                                <!--About-->
                                <a class="nav-link text-dark about" href="https://shrinefox.com/about">About</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </section>
        <script>
            $('#news-feed').FeedEk({
                FeedUrl: 'https://shrinefox.com/news/feed',
                MaxCount: 3,
                ShowDesc: false,
                ShowPubDate: true,
                DescCharacterLimit: 0,
                TitleLinkTarget: '_blank',
                DateFormat: 'MMM d',
                DateFormatLang: 'en'
            });

            $('#guides-feed').FeedEk({
                FeedUrl: 'https://shrinefox.com/guides/feed',
                MaxCount: 3,
                ShowDesc: false,
                ShowPubDate: true,
                DescCharacterLimit: 0,
                TitleLinkTarget: '_blank',
                DateFormat: 'MMM d',
                DateFormatLang: 'en'
            });

            $('#blog-feed').FeedEk({
                FeedUrl: 'https://shrinefox.com/blog/feed',
                MaxCount: 3,
                ShowDesc: false,
                ShowPubDate: true,
                DescCharacterLimit: 0,
                TitleLinkTarget: '_blank',
                DateFormat: 'MMM d',
                DateFormatLang: 'en'
            });
        </script>
    </header>
    
	
    
    <!--Video Background-->
    <div class="videocontainer">
        <video autoplay muted loop class="videobg">
            <source src="https://shrinefox.com/images/waves.mp4" type="video/mp4">
        </video>
    </div>
    <!--Search-->
    <div class="googlesearchcontainer">
        <div class="googlesearch">
            <script async src="https://cse.google.com/cse.js?cx=38ad766f435ddfed4"></script>
            <div class="gcse-search"></div>
        </div>
    </div>
    

	<div class="navipath" style="margin-left:3em;">
		<a href="https://shrinefox.com/"><i class="fa fa-home"></i> ShrineFox.com</a>
		<i class="fa fa-angle-right"></i> <a href="https://shrinefox.com/articles">Articles</a> 
		<i class="fa fa-angle-right"></i> <a href="<?php echo get_bloginfo( 'url' ); ?>"><?php echo get_bloginfo( 'name' ); ?></a>
	</div>
		<div id="content" class="site-content">
