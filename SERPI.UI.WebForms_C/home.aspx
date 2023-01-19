<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="SERPI.UI.WebForms_C.home" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
    
<head runat="server" lang="pt-br">
    <meta charset="utf-8"/>
	<meta name="apple-mobile-web-app-capable" content="yes"/>
	<meta http-equiv="x-ua-compatible" content="ie=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>

		    <title>Pós-graduação IPT - Instituto de Pesquisa Tecnológicas</title>
	
	<!-- Page Title -->
	        <meta name="description" content="Pós-graduação IPT - Instituto de Pesquisa Tecnológicas"/>
    
	<!-- FONTS -->
	<link href="https://fonts.googleapis.com/css?family=Gudea:400,400i,700%7CPT+Serif:400,400i,700,700i%7CSlabo+27px&amp;subset=cyrillic,cyrillic-ext,latin-ext" rel="stylesheet"/>
	<link href="https://fonts.googleapis.com/css?family=Cabin:600" rel="stylesheet"/>
	<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.1/css/all.css" integrity="sha384-O8whS3fhG2OnA5Kas0Y9l3cfpmYjapjI0E4theH4iuMD+pLhbf6JI0jIMfYcK3yZ" crossorigin="anonymous"/>
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <!-- STYLES -->
	<!-- libs -->

	<link rel="stylesheet" href="Content/Homepage/fontello.css"/>
	<link rel="stylesheet" href="Content/Homepage/foundation.css"/>
	<link rel="stylesheet" href="Content/Homepage/swiper.min.css"/>
	<link rel="stylesheet" href="Content/Homepage/magnific-popup.css"/>
	<!-- custom -->
	<link rel="stylesheet" href="Content/Homepage/main.min.css"/>
	<link rel="stylesheet" href="Content/Homepage/style.css"/>
	<link rel="stylesheet" href="Content/Homepage/steps.css"/>
	<!-- LayerSlider stylesheet -->
	<link rel="stylesheet" href="Content/Homepage/layerslider.css" type="text/css"/>

	<link rel="shortcut icon" href="img/Homepage/favicon.ico">
	<meta name="theme-color" content="#ffffff">

    <!-- Bootstrap 3.3.7 -->
    <link href="Content/AdminLTE/bootstrap.min.css" rel="stylesheet" />

    
    
</head>
<body>
    <form id="form1" runat="server">
        <!-- HEADER -->
	    <header class="main-header">
		    <div class="row-fluid" id="js-menu-sticky-anchor">
			    <div class="row align-justify align-middle row-logo">
				    <div class="columns small-12 medium-6">
					    <div class="logo">
						    <a href="https://www.ipt.br" target="_blank">
							    <img src="img/Homepage/logo.jpg" style="height:80px;" alt="Logo do IPT"/>
						    </a>
					    </div>
				    </div>
				    <div class="columns small-12 shrink">
					    <div class="contact-phone">
						    <i class="fa fa-phone"></i> Telefone para contato: <span>  (11) 3767-4226</span>
					    </div>
					    <div class="contact-phone">
						    <i class="fab fa-whatsapp"></i> Whatsapp para contato: <span> (11) 95994-7506</span>
					    </div>
				    </div>
			    </div>
		    </div>
		    <div class="sticky-container">
			    <div class="row-fluid row-fluid--menu js-sticky">
				    <div class="row align-middle main-navigation">

					    <div class="columns small-order-2 large-order-1 menu-col">
						    <nav>
							    <ul class="menu main-menu">
								    <li class="menu-item ">
									    <a href="./home.aspx">Home</a>
								    </li>
                                    <li class="menu-item ">
									    <a onclick="javascript:fAbreModal()" href="#">Mestrado</a>
								    </li>
                                    <li class="menu-item ">
									    <a href="/mestrado">Especialização</a>
								    </li>
                                    <li class="menu-item ">
									    <a href="/mestrado">Curta Duração</a>
								    </li>
                                    <li class="menu-item ">
									    <a href="/palestras">Sobre</a>
								    </li>
								    <li class="menu-item ">
									    <a href="/sobre">FAQ</a>
								    </li>
								    <li class="menu-item ">
									    <a href="/contato">Contato</a>
								    </li>
							    </ul>
						    </nav>
					    </div>
					    <div class="columns large-1 small-order-1 large-order-2 small-12">
						    <div class="row align-middle menu-search-row">
							    <div class="columns">
								    <div class="nav-menu-icon"><i></i></div>
							    </div>
							    <div class="columns">
								    <div class="row small-collapse align-right">
									    <div class="columns menu-search-col">
										    <form class="search-form">
											    <input type="search" placeholder="Search">
										    </form>
									    </div>
									    <div class="column small-12 shrink">
				                            <a href="/reimprimir-inscricao" class="btn get-a-quote__link"><i class="fa fa-sign-in">&nbsp;&nbsp;<span style="font-family:Arial">Entrar</span></i> </a>
			                            </div>
								    </div>
							    </div>
						    </div>
					    </div>

				    </div>
			    </div>
		    </div>
	    </header>

	    <!-- BANER SLIDER -->
        <div class="slider-1">
            <div id="layerslider" class="ls-wp-container fitvidsignore" style="width: 1180px; height: 400px; margin: 0 auto; margin-bottom: 0; z-index: 5;">

                <div class="ls-slide" data-ls="duration:10000;transition2d:70;timeshift:-3200;kenburnsscale:1.2;">
                    <img src="http://painel.posipt.com.br/assets/uploads/128/8b0ec-banner_principal.jpg" class="ls-bg" alt="slide-1">

                    <img width="1610" height="400" src="img/Homepage/slider-overlay-01.png" class="ls-l" alt="" style="top: 0; left: -600px; text-align: initial; font-weight: 400; font-style: normal; text-decoration: none; word-wrap: normal; opacity: 1;" data-ls="showinfo:1;offsetxin:-100lw;durationin:1500;delayin:200;easingin:easeInOutBack;fadein:false;clipin:0 100% 0 0;offsetxout:-100lw;durationout:900000000000000;startatout:transitioninend + 5100;easingout:easeInBack;fadeout:false;position:fixed;">

                    <img width="1570" height="400" src="img/Homepage/slider-overlay-03.png" class="ls-l" alt="" style="top: 0; left: -600px; text-align: initial; font-weight: 400; font-style: normal; text-decoration: none; word-wrap: normal; opacity: 1;"
                        data-ls="showinfo:1;offsetxin:-100lw;durationin:1500;easingin:easeInOutBack;fadein:false;clipin:0 100% 0 0;offsetxout:-100lw;durationout:900000000000000;startatout:transitioninend + 5000;easingout:easeInBack;fadeout:false;position:fixed;">

                    <h2 style="top: 70%; left: 20%; text-align: initial; font-weight: 400; font-style: normal; text-decoration: none; word-wrap: normal; opacity: 1; font-family: Gudea; font-size: 30px; color: #ffffff;" class="ls-l ls-hide-phone" data-ls="showinfo:1;offsetxin:-100lw;durationin:800;delayin:1700;easingin:easeInOutBack;offsetxout:-100lw;durationout:900000000000000;startatout:transitioninend + 4400;easingout:easeInBack;clipout:0 100% 0 0;position:absolute;">Pós-Graduação IPT - conheça os cursos 
                    </h2>
                </div>
            </div>
        </div>

	    <!-- SERVICES and QUOTE --> <%--Inibido--%>
	    <div class="row-fluid services-links-row hidden">
		    <div class="row">
			    <div class="columns small-12">
				    <div class="row large-collapse services-links">
					    <div class="columns small-12 large-4">
						    <a href="/solucoes-tecnologicas">
							    <div class="services-links__column">
								    <i class="fa fa-globe fa-2x">&nbsp;&nbsp;&nbsp;</i>
								    <div class="services-links__item">
									    <h2 class="services-links__title">Áreas de Conhecimento</h2>
									    <p class="services-links__item-content">Áreas de Conhecimento</p>
								    </div>
							    </div>
						    </a>
					    </div>
					    <div class="columns small-12 large-4">
						    <a href="/cursos-incompany">
							    <div class="services-links__column">
								    <i class="fa fa-file-text-o fa-2x">&nbsp;&nbsp;&nbsp;</i>
								    <div class="services-links__item">
									    <h2 class="services-links__title">Documentos / Boletos</h2>
									    <p class="services-links__item-content">Documentos / Boletos</p>
								    </div>
							    </div>
						    </a>
					    </div>
					    <div class="columns small-12 large-4">
						    <a href="/centro-tecnologicos">
							    <div class="services-links__column">
								    <i class="fa fa-question-circle fa-2x">&nbsp;&nbsp;&nbsp;</i>
								    <div class="services-links__item">
									    <h2 class="services-links__title">FAQ</h2>
									    <p class="services-links__item-content">Perguntas Freqüentes</p>
								    </div>
							    </div>
						    </a>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>

        <!-- Sobre Inicio-->
        <br />
        <div class="row">
		        <!-- BLOG -->
		    <div class="columns small-12 large-9">
			    <h3>Expertise em ensino</h3>
			            
	            <div class="content">
	            
	                <p>
	            &nbsp;</p>
            <p>
	            <span style="font-size:x-large;">IPT oferece cursos para profissionais diplomados em cursos superiores de graduação (bacharelado, licenciatura e tecnológico) como formação complementar, dividindo-se em stricto sensu e lato sensu.</span></p>

	                    </div>
		    </div>

		    <div class="columns small-12 large-3">
			    <div class="widget widget-find-lawyer">
	                <!-- Practice Areas -->
	                <section class="widget widget_categories">
		                <h3 class="widget-title">LINKS GERAIS</h3>
		                <div class="image">
	            	        <img src="http://painel.posipt.com.br/assets/uploads/128/6e41d-20160119100401_ipt-predio.jpg"/>
	            	    </div>
	                </section>
	                <!-- Sectors -->
	
                </div>
		    </div>
	    </div>
        <!-- Sobre Fim -->

	    <!-- EXPERIENCE -->
	    <div class="row">
		    <div class="column">
			    <h2 class="banner-title">Nossos Cursos</h2>
		    </div>
	    </div>
        <div class="row">

            <div class="columns small-12 medium-6 large-3">
                <div class="service-item">
                    <a href="/pos-graduacao/seguranca-de-barragens/8/1">
                        <figure class="effect-apollo">
                            <img src="img/Homepage/68417-seguranca_em_barragens.jpg" title="Pós em Segurança de Barragens" alt="Pós em Segurança de Barragens" />
                            <div class="effect-apollo__overlay"></div>
                        </figure>
                    </a>
                    <h4 class="service-item__title titulo-cursos-mais-procurados"><a href="/pos-graduacao/seguranca-de-barragens/8/1">Pós em Segurança de Barragens</a></h4>

                </div>
            </div>
            <div class="columns small-12 medium-6 large-3">
                <div class="service-item">
                    <a href="/pos-graduacao/investigacao-do-subsolo-geotecnia-e-meio-ambiente/9/1">
                        <figure class="effect-apollo">
                            <img src="img/Homepage/4b899-investigacao_de_subsolo.jpg" title="Pós em Investigação do Subsolo" alt="Pós em Investigação do Subsolo" />
                            <div class="effect-apollo__overlay"></div>
                        </figure>
                    </a>
                    <h4 class="service-item__title titulo-cursos-mais-procurados"><a href="/pos-graduacao/investigacao-do-subsolo-geotecnia-e-meio-ambiente/9/1">Pós em Investigação do Subsolo</a></h4>

                </div>
            </div>
            <div class="columns small-12 medium-6 large-3">
                <div class="service-item">
                    <a href="/pos-graduacao/gestao-da-inovacao-tecnologica-e-negocios/7/2">
                        <figure class="effect-apollo">
                            <img src="img/Homepage/50da3-gestao_da_inovacao.jpg" title="Pós em Pós-Graduação em Gestão da Inovação Tecnológica e Negócio" alt="Pós em Pós-Graduação em Gestão da Inovação Tecnológica e Negócio" />
                            <div class="effect-apollo__overlay"></div>
                        </figure>
                    </a>
                    <h4 class="service-item__title titulo-cursos-mais-procurados"><a href="/pos-graduacao/gestao-da-inovacao-tecnologica-e-negocios/7/2">Pós em Pós-Graduação em Gestão da Inovação Tecnológica e Negócio</a></h4>

                </div>
            </div>
            <div class="columns small-12 medium-6 large-3">
                <div class="service-item">
                    <a href="/pos-graduacao/master-technological-administration-mta-em-tecnologias-digitais-estrategicas/12/3">
                        <figure class="effect-apollo">
                            <img src="img/Homepage/bf096-curso-dr.-alessandro-ipt.jpg" title="Master Technological Administration (MTA) em Tecnologias Digitais Estratégica" alt="Master Technological Administration (MTA) em Tecnologias Digitais Estratégica" />
                            <div class="effect-apollo__overlay"></div>
                        </figure>
                    </a>
                    <h4 class="service-item__title titulo-cursos-mais-procurados"><a href="/pos-graduacao/master-technological-administration-mta-em-tecnologias-digitais-estrategicas/12/3">Master Technological Administration (MTA) em Tecnologias Digitais Estratégica</a></h4>

                </div>
            </div>
        </div>

	    <!-- FIND PEOPLE - MAP -->
	    <div class="find-people-map">
		    <div class="row">
			    <div class="column">
				    <h2 class="banner-title"></h2>
			    </div>
		    </div>
		    <div class="row find-people-map-inner">
			    <div class="small-12 large-12 column">
				
			    </div>
			
		    </div>
	    </div>

	    <!-- LATEST POSTS -->
	    <div class="row latest-posts-wrapper">
		    <div class="small-12 medium-6 large-4 column widget widget-latest-posts-thumb">
			    <h2 class="widget-title alinhamento-esquerda">
				    Últimas notícias -
				    <a href="/noticias"><small>[Ler todas]</small></a>
			    </h2>
			    <ul>
									                            <li>
							    <a href="/ipt-lanca-cursos-de-pos-graduacao-nas-areas-de-engenharia-gestao-e-tecnologia/noticia/4" class="widget-latest-posts-thumb__thumb effect-apollo">
								                                        <img src="img/Homepage/a3b77-linkedin.jpg" alt="Imagem da Notícia IPT lança cursos de Pós-graduação nas áreas de Engenharia, Gestão e Tecnologia" />
                                								    <div class="effect-apollo__overlay"></div>
							    </a>
							    <div class="widget-latest-posts-thumb__item-meta alinhamento-esquerda">
								    <a class="widget__date" href="/ipt-lanca-cursos-de-pos-graduacao-nas-areas-de-engenharia-gestao-e-tecnologia/noticia/4">30/01/2019</a>
								    <h4 class="alinhamento-esquerda"><a href="/ipt-lanca-cursos-de-pos-graduacao-nas-areas-de-engenharia-gestao-e-tecnologia/noticia/4">IPT lança cursos de Pós-graduação nas áreas de Engenharia, Gestão e Tecnologia</a></h4>
							    </div>
						    </li>
					                					                            <li>
							    <a href="/inovacao-tecnologica-e-a-lideranca-feminina-nos-negocios/noticia/1" class="widget-latest-posts-thumb__thumb effect-apollo">
								                                        <img src="img/Homepage/259d4-untitled-9.jpg" alt="Imagem da Notícia Inovação tecnológica e a liderança feminina nos negócios " />
                                								    <div class="effect-apollo__overlay"></div>
							    </a>
							    <div class="widget-latest-posts-thumb__item-meta alinhamento-esquerda">
								    <a class="widget__date" href="/inovacao-tecnologica-e-a-lideranca-feminina-nos-negocios/noticia/1">07/08/2018</a>
								    <h4 class="alinhamento-esquerda"><a href="/inovacao-tecnologica-e-a-lideranca-feminina-nos-negocios/noticia/1">Inovação tecnológica e a liderança feminina nos negócios </a></h4>
							    </div>
						    </li>
					                					                            <li>
							    <a href="/investigacao-do-subsolo-e-sua-aplicacao-na-engenharia/noticia/2" class="widget-latest-posts-thumb__thumb effect-apollo">
								                                        <img src="img/Homepage/940af-untitled-7.jpg" alt="Imagem da Notícia Investigação do subsolo e sua aplicação na Engenharia " />
                                								    <div class="effect-apollo__overlay"></div>
							    </a>
							    <div class="widget-latest-posts-thumb__item-meta alinhamento-esquerda">
								    <a class="widget__date" href="/investigacao-do-subsolo-e-sua-aplicacao-na-engenharia/noticia/2">07/08/2018</a>
								    <h4 class="alinhamento-esquerda"><a href="/investigacao-do-subsolo-e-sua-aplicacao-na-engenharia/noticia/2">Investigação do subsolo e sua aplicação na Engenharia </a></h4>
							    </div>
						    </li>
					                					                			    </ul>
		    </div>
		    <div class="small-12 medium-6 large-4 column widget widget-latest-posts-thumb">
			    <h2 class="widget-title">&nbsp;</h2>
			    <ul>
									                					                					                					                            <li>
							    <a href="/seguranca-de-barragens-autorizacao-e-fiscalizacao-sao-necessarias/noticia/3" class="widget-latest-posts-thumb__thumb effect-apollo">
								                                        <img src="img/Homepage/d7ac8-untitled-8.jpg" alt="Imagem da Notícia Segurança de barragens: autorização e fiscalização são necessárias" />
                                								    <div class="effect-apollo__overlay"></div>
							    </a>
							    <div class="widget-latest-posts-thumb__item-meta alinhamento-esquerda">
								    <a class="widget__date" href="/seguranca-de-barragens-autorizacao-e-fiscalizacao-sao-necessarias/noticia/3">28/07/2018</a>
								    <h4 class="alinhamento-esquerda"><a href="/seguranca-de-barragens-autorizacao-e-fiscalizacao-sao-necessarias/noticia/3">Segurança de barragens: autorização e fiscalização são necessárias</a></h4>
							    </div>
						    </li>
					                			    </ul>
		    </div>
		    <div class="small-12 medium-6 large-4 column widget widget-latest-posts-thumb">
			    <h2 class="widget-title">Depoimentos - <a href="/depoimentos"><small>[Ler todas]</small></a></h2>
			    <section class="widget widget-blockquote">
				    <div class="swiper-container" data-slides-per-view="1" data-loop="true" data-autoplay="3000" data-speed="3000" data-space-between="70" data-slide-effect="fade">
					    <!-- Additional required wrapper -->
					    <div class="swiper-wrapper">
						    <!-- Slides -->
									                    <div class="swiper-slide">
								    <blockquote class="blockquote blockquote--style-1">
									    &ldquo;O mestrado me possibilitou iniciar uma carreira complementar. Desde o ano de 2009 leciono como professora convidada na disciplina &lsquo;PGP-009 An&aacute;lise de custos e de viabilidade de constru&ccedil;&otilde;es&rsquo;.&nbsp;&#8230;
								    </blockquote>
								    <div class="blockquote-author">
									    <img src="img/Homepage/e69a0-sandra-haruna-hashizume.jpg" alt="" class="blockquote-author__photo">
									    <div class="blockquote-author__info">
										    <cite class="blockquote-author__name">Sandra Haruna Hashizume</cite>
										
									    </div>
								    </div>
							    </div>
			            			                    <div class="swiper-slide">
								    <blockquote class="blockquote blockquote--style-1">
									    &ldquo;Acredito que o principal impacto do mestrado foi me tornar mais questionador em busca do saber. O ponto de voc&ecirc; questionar sobre t&oacute;picos que n&atilde;o fazem parte do seu dom&iacute;nio&#8230;
								    </blockquote>
								    <div class="blockquote-author">
									    <img src="img/Homepage/34309-ricardo-tardelli-pessoa.jpg" alt="" class="blockquote-author__photo">
									    <div class="blockquote-author__info">
										    <cite class="blockquote-author__name">Ricardo Tardelli Pessoa</cite>
										
									    </div>
								    </div>
							    </div>
			            			                    <div class="swiper-slide">
								    <blockquote class="blockquote blockquote--style-1">
									    &ldquo;Por causa do mestrado no IPT pude pesquisar algo que realmente acreditava que faria uma diferen&ccedil;a. Fiz algumas apresenta&ccedil;&otilde;es na empresa em que trabalhava, mas a mudan&ccedil;a&#8230;
								    </blockquote>
								    <div class="blockquote-author">
									    <img src="img/Homepage/8321d-luciana-de-cresce-el-debs.jpg" alt="" class="blockquote-author__photo">
									    <div class="blockquote-author__info">
										    <cite class="blockquote-author__name">Luciana de Cresce El Debs</cite>
										
									    </div>
								    </div>
							    </div>
			            			                    <div class="swiper-slide">
								    <blockquote class="blockquote blockquote--style-1">
									    &ldquo;O mestrado ofereceu a mim uma atualiza&ccedil;&atilde;o quanto ao processo de desenvolvimento de software e tecnologias da informa&ccedil;&atilde;o, assim como uma melhoria na abordagem de conceitos&#8230;
								    </blockquote>
								    <div class="blockquote-author">
									    <img src="img/Homepage/482c2-gianni-ricciardi.jpg" alt="" class="blockquote-author__photo">
									    <div class="blockquote-author__info">
										    <cite class="blockquote-author__name">Gianni Ricciardi</cite>
										
									    </div>
								    </div>
							    </div>
			            			                    <div class="swiper-slide">
								    <blockquote class="blockquote blockquote--style-1">
									    &ldquo;O mestrado fortaleceu meu relacionamento com a FGV, al&eacute;m de ter melhorado significativamente as avalia&ccedil;&otilde;es dos alunos.&nbsp; Sou professor de Marketing e orientador de TCCs&#8230;
								    </blockquote>
								    <div class="blockquote-author">
									    <img src="img/Homepage/b2d85-flavio-ricardo-rodrigues.jpg" alt="" class="blockquote-author__photo">
									    <div class="blockquote-author__info">
										    <cite class="blockquote-author__name">Flavio Ricardo Rodrigues</cite>
										
									    </div>
								    </div>
							    </div>
			            			                    <div class="swiper-slide">
								    <blockquote class="blockquote blockquote--style-1">
									    &ldquo;Tinha inten&ccedil;&atilde;o em migrar da carreira executiva para a acad&ecirc;mica e o mestrado me preparou, com excel&ecirc;ncia, em pesquisa. Depois me permitiu cursar o doutorado. Atualmente&#8230;
								    </blockquote>
								    <div class="blockquote-author">
									    <img src="img/Homepage/403e3-claudio-luis-carvalho-larieira.jpg" alt="" class="blockquote-author__photo">
									    <div class="blockquote-author__info">
										    <cite class="blockquote-author__name">Claudio Luis Carvalho Larieira</cite>
										
									    </div>
								    </div>
							    </div>
			            					    </div>
				    </div>
			    </section>
		    </div>
	    </div>


    <!-- GET A QUOTE -->
	    <div class="get-a-quote">
		    <div class="row align-center">
			    <div class="column small-12 medium-expand">
				    <h3 class="get-a-quote__text">Faça sua inscrição.</h3>
			    </div>
			    <div class="column small-12 shrink">
				    <a href="./homeInscricoes.aspx" target="_blank" class="btn get-a-quote__link"><i class="fa fa-pencil"></i> Inscreva-se</a>
			    </div>
		    </div>
	    </div>

	    <!-- FOOTER -->
	    <footer class="main-footer dark-section">
		    <div class="row row-widgets">
			    <div class="columns small-12 medium-6 large-3">
				    <section class="widget widget-address">
					    <h3 class="widget-title">
	    P&oacute;s-Gradua&ccedil;&atilde;o IPT</h3>
    <p>
	    Instituto de Pesquisas Tecnol&oacute;gicas &ndash; IPT<br />
	    Avenida Professor Almeida Prado, 532<br />
	    Cidade Universit&aacute;ria, Butant&atilde;, S&atilde;o Paulo - SP<br />
	    Coordenadoria de Ensino Tecnol&oacute;gico - Pr&eacute;dio 56 &ndash; t&eacute;rreo<br />
	    <br />
	    Tel.: (11) 3767-4068 | 3767-4058</p>

				    </section>
			    </div>
			    <div class="columns small-12 medium-6 large-3">
				    <section class="widget widget_categories">
					    <h3 class="widget-title">ÁREAS DA PÓS PRESENCIAL</h3>
					    <ul>
						                                                                                        <li>
                                	    <a href="/pos-graduacao/engenharias/1">ENGENHARIAS</a>
                               	    </li>
                                                                                                                                                    <li>
                                	    <a href="/pos-graduacao/gestao/2">GESTÃO</a>
                               	    </li>
                                                                                                                                                    <li>
                                	    <a href="/pos-graduacao/tecnologia/3">TECNOLOGIA</a>
                               	    </li>
                                                                                    					    </ul>
				    </section>
			    </div>
			    <div class="columns small-12 medium-6 large-3">
				    <section class="widget widget_pages">
					    <h3 class="widget-title">LINKS</h3>
					    <ul>
                            <li><a href="/sobre">Sobre o IPT</a></li>
                            <li><a href="/mestrado">Mestrado</a></li>
                            <li><a href="/solucoes-tecnologicas">Soluções Tecnológicas</a></li>
                            <li><a href="/trabalhe-ipt">Trabalhe no IPT</a></li>
                            <li><a href="/centro-tecnologicos">Centro Tecnológicos</a></li>
					    </ul>
				    </section>
			    </div>
			    <div class="columns small-12 medium-6 large-3">
				    <section class="widget widget-latest-posts">
					    <h3 class="widget-title">REDES SOCIAIS</h3>
					    <ul>
						    <li><a target="_blank" href="https://www.facebook.com/iptsp"><i class="fab fa-facebook-f"></i> Facebook</a></li>
						    <li><a target="_blank" href="https://twitter.com/iptsp"><i class="fab fa-twitter"></i> Twitter</a></li>
						    <li><a target="_blank" href="https://www.linkedin.com/company/insitituto-de-pesquisas-tecnol-gicas"><i class="fab fa-linkedin"></i> Linkedin</a></li>
						    <li><a target="_blank" href="https://www.youtube.com/user/IPTbr?reload=9"><i class="fab fa-youtube"></i> Youtube</a></li>
					    </ul>
				    </section>
			    </div>
		    </div>
		    <div class="row" style="border-top:1px dashed rgba(255,255,255,0.1);">
			    <div class="columns small-12">
				    <div class="row align-justify align-middle copyright small-collapse">
					    <div class="columns small-12 medium-expand">
						    Copyright © 2019. <strong>Pós IPT</strong>. Todos direitos reservados.
					    </div>
					    <div class="columns small-12 shrink">
						    Desenvolvido por <a target="_blank" href="/">Grupo Instituto Phorte</a>
					    </div>
				    </div>
			    </div>
		    </div>
	    </footer>

        <div class="modal fade" id="Dificuldade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-warning">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><i class="fa fa-question-circle"></i> Dificuldade em se Conectar</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <p>
                                    <b>Prezado Usuário,</b>
                                    <br>
                                    <br>
                                </p>
                                <p>
                                    Se este é seu PRIMEIRO ACESSO ou ainda caso tenha esquecido sua <b>Senha</b>, por gentileza, digite seu <strong>Login</strong> e <strong>email</strong> e será enviado um email com uma nova Senha.
                                    <br />
                                </p>
                            </div>
                        </div>
                        <br />

                        
                        
                        <hr size="100" width="100%" noshade color="#9D9D9D" />
                        
                        <div class="row">
                            <div class="col-lg-6 pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                            </div>

                            
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

	    <script src="Scripts/Homepage/jquery.js"></script>
	    <script src="Scripts/Homepage/swiper.jquery.min.js"></script>
	    <script src="Scripts/Homepage/imagesloaded.pkgd.min.js"></script>
	    <script src="Scripts/Homepage/isotope.pkgd.min.js"></script>
	    <script src="Scripts/Homepage/jquery.matchHeight.js"></script>
	    <script src="Scripts/Homepage/jquery.magnific-popup.min.js"></script>
	    <script src="Scripts/Homepage/main.js"></script>

	    <!-- External libraries: jQuery & GreenSock -->
	    <script src="Scripts/Homepage/layerslider/js/jquery.js"></script>
	    <script src="Scripts/Homepage/layerslider/js/greensock.js"></script>

	    <!-- LayerSlider script files -->
	    <script src="Scripts/Homepage/layerslider/js/layerslider.transitions.js"></script>
	    <script src="Scripts/Homepage/layerslider/js/layerslider.kreaturamedia.jquery.js"></script>
	    <!-- Initializing the slider -->

        <!-- jQuery 2.2.3 -->
        <%--<script src="Scripts/AdminLTE/jQuery/jquery-2.2.3.min.js"></script>--%>
        

        <!-- Bootstrap 3.3.7 -->
        <script src="Scripts/AdminLTE/Bootstrap/bootstrap.min.js"></script>

	    <script>
		    jQuery("#layerslider").layerSlider({
			    type: 'fullwidth',
			    allowFullscreen: false,
			    cycles: 3,
			    navStartStop: false,
			    navButtons: false,
			    popupWidth: 640,
			    popupHeight: 360,
			    skinsPath: 'Content/Homepage/layerslider/skins/',
			    hideOnMobile: true
		    });
	    </script>

	    <script type="text/javascript">
            $(document).ready(function() {
                $("#layerslider").removeClass('ls-forcehide');

                document.getElementById('layerslider').style.display = 'block';

                var element = document.getElementById("layerslider");
                element.classList.remove("ls-forcehide");

                $("#filtro_uf").change(function () {
                    estado_sigla = $(this).val();
                    pegaCidade(estado_sigla);
                });
                $("#filtro_cidade").change(function(){
                    pegaArea();
                });
                $("#filtro_modalidade").change(function(){
                    if($(this).val() == 0){
                        $("#filtro_uf").css('display', "inline");
                        $("#filtro_cidade").css('display', "inline");
                    } else{
                        $("#filtro_uf").css('display', "none");
                        $("#filtro_cidade").css('display', "none");
                    }
                    pegaArea();
                });
            });
            function pegaCidade(estado_sigla){
                $("#filtro_areas").html("<option value=''>AREAS</option>");
                $.get("/pega-cidades-filtro?estado_sigla="+estado_sigla, function(data){
                    $("#filtro_cidade").html("<option value=''>CIDADES</option>"+data);

                }).done(function() {
                                });
            }
            function pegaArea(){
                $.get("/pega-areas-filtro", function(data){
                        $("#filtro_areas").html("<option value=''>AREAS</option>"+data);
                }).done(function () {
                                });
            }

            function fAbreModal() {
                $("#Dificuldade").modal();
            }
        </script>
	    <script src="Scripts/Homepage/library.js"></script>
	    <script type="text/javascript" src="Scripts/Homepage/jquery.mask.min.js"></script>
    </form>
</body>
</html>
