<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepass.aspx.cs" Inherits="FE.changepass" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<form id="form1" runat="server">
<!--[if gt IE 8]><!--> <html class="no-js"> <!--<![endif]-->
	<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<title>Adviters</title>
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta name="description" content="Adviters Consultoría Estratégica" />
  	<meta name="keywords" content="Consultoría, Coaching, IT, Estudio, Sistemas" />
  	<meta name="author" content="Adviters.com" />

  	<!-- Facebook and Twitter integration -->
	<meta property="og:title" content=""/>
	<meta property="og:image" content=""/>
	<meta property="og:url" content=""/>
	<meta property="og:site_name" content=""/>
	<meta property="og:description" content=""/>
	<meta name="twitter:title" content="" />
	<meta name="twitter:image" content="" />
	<meta name="twitter:url" content="" />
	<meta name="twitter:card" content="" />

  	<!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
    <link rel="icon" href="images/favicon.ico" type="image/x-icon" /> 
  	<link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />

  	<!-- Google Webfont -->
	<link href='http://fonts.googleapis.com/css?family=Lato:300,400|Crimson+Text' rel='stylesheet' type='text/css'>
	<!-- Themify Icons -->
	<link rel="stylesheet" href="css/themify-icons.css">
	<!-- Bootstrap -->
	<link rel="stylesheet" href="css/bootstrap.css">
	<!-- Owl Carousel -->
	<link rel="stylesheet" href="css/owl.carousel.min.css">
	<link rel="stylesheet" href="css/owl.theme.default.min.css">
	<!-- Magnific Popup -->
	<link rel="stylesheet" href="css/magnific-popup.css">
	<!-- Superfish -->
	<link rel="stylesheet" href="css/superfish.css">
	<!-- Easy Responsive Tabs -->
	<link rel="stylesheet" href="css/easy-responsive-tabs.css">

	

	<!-- Theme Style -->
	<link rel="stylesheet" href="css/style.css">

	
	<!-- FOR IE9 below -->
	<!--[if lt IE 9]>
	<script src="js/modernizr-2.6.2.min.js"></script>
	<script src="js/respond.min.js"></script>
	<![endif]-->

	</head>
	<body>

			<!-- START #fh5co-header -->
			<header id="fh5co-header-section" role="header" class="" >
				<div class="container">
					
					<!-- START #fh5co-logo -->
					<h1 id="fh5co-logo" class="pull-left"><a href="index.html">Adviters</a></h1>
					
					<!-- START #fh5co-menu-wrap -->
					<nav id="fh5co-menu-wrap" role="navigation">
						<ul class="sf-menu" id="fh5co-primary-menu">
							<!--<li class="active"><a href="index.html">Adviters</a></li>-->
							<li><a href="institucional.html">Institucional</a></li>
                            <li><a href="servicios.html">Servicios</a></li>
							<li><a href="contacto.html">Contacto</a></li>
                            <li><a href="login.aspx">Ingresar</a></li>
						</ul>
					</nav>

				</div>
			</header>
			
			<!-- START #fh5co-hero -->
			<aside id="fh5co-hero" style="background-image: url(images/hero4.jpg);">
				<div class="container">
					<div class="row">
						<div class="col-md-8 col-md-offset-2">
							<div class="fh5co-hero-wrap">
								<div class="fh5co-hero-intro-login">
										<h2>Reestablecer Contraseña<span></span></h2>
                                        <p></p>
                                        <div class="row fh5co-feature-2-login">
                                            <div class="col-md-5 fh5co-feature-item">
                                                <p class="text-right fh5co-feature-description"><asp:Label ID="lblclave" runat="server" Text="Ingrese su Contraseña:"></asp:Label></p>
								                <p class="text-right fh5co-feature-description"><asp:Label ID="lblclave2" runat="server" Text="Repita su Contraseña:"></asp:Label></p>
							                </div>
							                <div class="col-md-5 fh5co-feature-item">
								                <p class="text-left fh5co-feature-textbox"><asp:TextBox ID="txtclave" runat="server" TextMode="Password"></asp:TextBox> </p>
                                                <p class="text-left fh5co-feature-textbox"><asp:TextBox ID="txtclave2" runat="server" TextMode="Password"></asp:TextBox> </p>
							                </div>
                                            
                                        </div>
                                        <asp:Label ID="lblLogin" runat="server" Text=""></asp:Label>
                                        <br />
                                        <asp:LinkButton ID="btnAceptar" runat="server" CssClass="btn btn-outline btn-lg" Text="Aceptar" OnClick="btnAceptar_Click"><span class="glyphicon glyphicon-ok"></span>&nbsp;Aceptar</asp:LinkButton>
                                        <br />
								</div>
							</div>
						</div>
					</div>
				</div>
			</aside>

			<div id="fh5co-main-login">

<%--				<div class="container">
					<div class="row">
						<div class="col-md-8 col-md-offset-2 text-center fh5co-lead-wrap">

						</div>
					</div>
				</div>--%>

				
				
				<footer id="fh5co-footer">
					<div class="container">
						
						<ul class="fh5co-social-icons">
							<li><a href="https://www.twitter.com/@Adviters"><i class="ti-twitter-alt"></i></a></li>
							<li><a href="https://www.facebook.com/Adviters"><i class="ti-facebook"></i></a></li>
							<li><a href="https://www.linkedin.com/company/Adviters"><i class="ti-linkedin"></i></a></li>
						</ul>
						<p class="text-muted fh5co-no-margin-bottom text-center"><small>&copy; 2017 <a href="http://adviters.com">Adviters</a></small> All rights reserved.</p>

					</div>
				</footer>
			
		
			</div>
			
			
		<!-- jQuery -->
		<script src="js/jquery-1.10.2.min.js"></script>
		<!-- jQuery Easing -->
		<script src="js/jquery.easing.1.3.js"></script>
		<!-- Bootstrap -->
		<script src="js/bootstrap.js"></script>
		<!-- Owl carousel -->
		<script src="js/owl.carousel.min.js"></script>
		<!-- Magnific Popup -->
		<script src="js/jquery.magnific-popup.min.js"></script>
		<!-- Superfish -->
		<script src="js/hoverIntent.js"></script>
		<script src="js/superfish.js"></script>
		<!-- Easy Responsive Tabs -->
		<script src="js/easyResponsiveTabs.js"></script>
		<!-- FastClick for Mobile/Tablets -->
		<script src="js/fastclick.js"></script>
		<!-- Main JS -->
		<script src="js/main.js"></script>

	</body>
</html>
</form>
