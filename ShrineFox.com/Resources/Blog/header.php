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

	<!--ShrineFox Styles-->
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

	<!--ShrineFox NavBar-->
	
    <!--ShrineFox Header-->

	<div class="navipath" style="margin-left:2.5em;">
		<a href="https://shrinefox.com/"><i class="fa fa-home"></i> ShrineFox.com</a>
		<i class="fa fa-angle-right"></i> <a href="https://shrinefox.com/articles">Articles</a> 
		<i class="fa fa-angle-right"></i> <a href="<?php echo get_bloginfo( 'url' ); ?>"><?php echo get_bloginfo( 'name' ); ?></a>
	</div>
		<div id="content" class="site-content">
