<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SERPI.UI.WebForms_C.index" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .bannerRosto_interno {
            /*background: url('img/estudante.jpg');*/
            background: url('img/Capa.png');
            background-repeat: no-repeat;
            background-position: right -80px;
            overflow: hidden;
            background-size: cover;
            height: 55vh;
            background-attachment: fixed;
            animation-name:img-ani;
            animation-duration: 1s;   
            animation-timing-function: ease-in;
        }

        .bannerCarrocel_0 {
            /*padding-top:210px;*/
            /*background: url('img/estudante.jpg?data=01/03/2019');*/
            background: url('img/Capa.png?data=24/07/2021');
            background-repeat: no-repeat;
            background-position: right -80px;
            overflow: hidden;
            background-size: cover;
            height: 50vh;
        }

        .bannerCarrocel_1 {
            /*padding-top:210px;*/
            background: url('img/biblioteca.jpg?data=01/03/2019');
            background-repeat: no-repeat;
            background-position: right -80px;
            overflow: hidden;
            background-size: cover;
            height: 50vh;
        }


        /* Carousel Fading slide */
        .carousel-fade .carousel-inner {
            /*background: #000;*/
        }

        .carousel-fade .carousel-control {
            z-index: 2;
        }

        .carousel-fade .item {
            opacity: 0;
            -webkit-transition-property: opacity;
            -moz-transition-property: opacity;
            -o-transition-property: opacity;
            transition-property: opacity;
        }

            .carousel-fade .next.left,
            .carousel-fade .prev.right,
            .carousel-fade .item.active {
                opacity: 1;
            }

        .carousel-fade .active.left,
        .carousel-fade .active.right {
            left: 0;
            opacity: 0;
            z-index: 1;
        }


        /* Safari Fix */
        @media all and (transform-3d), (-webkit-transform-3d) {
            .carousel-fade .carousel-inner > .item.next,
            .carousel-fade .carousel-inner > .item.active.right {
                opacity: 0;
                -webkit-transform: translate3d(0, 0, 0);
                transform: translate3d(0, 0, 0);
            }

            .carousel-fade .carousel-inner > .item.prev,
            .carousel-fade .carousel-inner > .item.active.left {
                opacity: 0;
                -webkit-transform: translate3d(0, 0, 0);
                transform: translate3d(0, 0, 0);
            }

                .carousel-fade .carousel-inner > .item.next.left,
                .carousel-fade .carousel-inner > .item.prev.right,
                .carousel-fade .carousel-inner > .item.active {
                    opacity: 1;
                    -webkit-transform: translate3d(0, 0, 0);
                    transform: translate3d(0, 0, 0);
                }
        }




        /* Carousel Control custom */
        .carousel-control .control-icon {
            font-size: 48px;
            height: 30px;
            margin-top: -15px;
            width: 30px;
            display: inline-block;
            position: absolute;
            top: 50%;
            z-index: 5;
        }

        .carousel-control .prev {
            margin-left: -15px;
            left: 50%;
        }
        /* Prev */
        .carousel-control .next {
            margin-right: -15px;
            right: 50%;
        }
        /* Next */


        /* Removing BS background */
        .carousel .control-box {
            opacity: 0;
        }

        a.carousel-control.left {
            left: 0;
            background: none;
            border: 0;
        }

        a.carousel-control.right {
            right: 0;
            background: none;
            border: 0;
        }


        /* Animation */
        .control-box, a.carousel-control, .carousel-indicators li {
            -webkit-transition: all 250ms ease;
            -moz-transition: all 250ms ease;
            -ms-transition: all 250ms ease;
            -o-transition: all 250ms ease;
            transition: all 250ms ease;
            /* hardware acceleration causes Bootstrap carousel controlbox margin error in webkit */
            /* Assigning animation to indicator li will make slides flicker */
        }


        /* Hover animation */
        .carousel:hover .control-box {
            opacity: 1;
        }

        .carousel:hover a.carousel-control.left {
            left: 15px;
        }

        .carousel:hover a.carousel-control.right {
            right: 15px;
        }


        /* Carouse Indicator */
        .carousel-indicators li.active,
        .carousel-indicators li {
            border: 0;
            border-radius: 25%;
        }

        .carousel-indicators li {
            background: #666;
            margin: 0 3px;
            width: 12px;
            height: 12px;
        }

        .carousel-indicators li.active {
            background: #016699;
            margin: 0 3px;
        }

        /*=======*/

        blockquote {
          height: 220px;
          display: flex;
          align-items: center;
          justify-content: center;
          width: 100%;
          margin: 1.5em 10px;
          padding: 0.5em 10px;
        }

        /* Extra small devices (phones, 600px and down) */
        @media only screen and (max-width: 600px) {
            #container_principal {
                margin-top:-50px;
            }

          .bannerRosto_interno {
                height: 42vh;
                margin-top:65px;
                background-attachment:unset;
                background-position: center;

            }

            #texto-img {
                height: 42vh;
            }
        }

        /* Small devices (portrait tablets and large phones, 600px and up) */
        @media only screen and (min-width: 600px) {
          .bannerRosto_interno {
                height: 42vh;
                margin-top:65px;
                background-attachment:unset;
                background-position: center;
            }

            #texto-img {
                height: 42vh;
            }
        }
        
        /* Medium devices (landscape tablets, 768px and up) */
        @media only screen and (min-width: 768px) {
          .bannerRosto_interno {
                height: 55vh;
            }

            #texto-img {
                height: 55vh;
            }
        } 

        /* Large devices (laptops/desktops, 992px and up) */
        @media only screen and (min-width: 992px) {
          .bannerRosto_interno {
                height: 55vh;
            }

            #texto-img {
                height: 55vh;
            }
        } 

        /* Extra large devices (large laptops and desktops, 1200px and up) */
        @media only screen and (min-width: 1200px) {
          .bannerRosto_interno {
                height: 55vh;
                /*margin-top:90px;*/
            }

            #texto-img {
                height: 55vh;
            }
        }

        .a_listaCurso {
            color:rgb(51, 51, 51) !important;
            transition:all 0.5s;
            }

        .a_listaCurso:hover {
            color:#3588CC !important;
            text-decoration:none;
            transition:all 0.5s;
        }

    </style>


    <!-- LayerSlider stylesheet -->
    <link rel="stylesheet" href="Content/Homepage/layerslider.css" type="text/css"/>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <section class="bannerRosto_interno hidden-sm hidden-md hidden-lg">
                
                <div id="texto-img" class="text-center">
                    <br /><br /><br /><br /><br /><br />
                    <h3 class="text-center wow zoomIn animated" data-wow-duration="2s"><strong><label style="color:white;text-shadow: 2px 2px grey;">
                                    Pós-Graduação IPT <br /><br /> Conheça nossos cursos 
                                </label></strong></h3>
                </div>
                   
            </section>

            <section class ="hidden-xs">
                <div id="layerslider" class="ls-wp-container fitvidsignore" style="width: 1180px; height: 400px; z-index: -1; margin-top: 139px;">

                    <div class="ls-slide" data-ls="duration:10000;transition2d:70;timeshift:-3200;kenburnsscale:1.2;">
                        <%--<img src="img/estudante2.jpg" class="bannerRosto_interno ls-bg" alt="slide-1" />--%>
                        <img src="img/Capa.png" class="bannerRosto_interno ls-bg" alt="slide-1" />

                        <img width="1610" height="400" src="img/Homepage/slider-overlay-01.png" class="ls-l" alt="" style="top: 0; left: -600px; text-align: initial; font-weight: 400; font-style: normal; text-decoration: none; word-wrap: normal; opacity: 1;" data-ls="showinfo:1;offsetxin:-100lw;durationin:1500;delayin:200;easingin:easeInOutBack;fadein:false;clipin:0 100% 0 0;offsetxout:-100lw;durationout:900000000000000;startatout:transitioninend + 5100;easingout:easeInBack;fadeout:false;position:fixed;" />

                        <img width="1570" height="400" src="img/Homepage/slider-overlay-03.png" class="ls-l" alt="" style="top: 0; left: -600px; text-align: initial; font-weight: 400; font-style: normal; text-decoration: none; word-wrap: normal; opacity: 1;"
                            data-ls="showinfo:1;offsetxin:-100lw;durationin:1500;easingin:easeInOutBack;fadein:false;clipin:0 100% 0 0;offsetxout:-100lw;durationout:900000000000000;startatout:transitioninend + 5000;easingout:easeInBack;fadeout:false;position:fixed;" />

                        <h2 style="top: 70%; left: 30%; text-align: initial; font-weight: 400; font-style: normal; text-decoration: none; word-wrap: normal; opacity: 1; font-family: Gudea; font-size: 30px; color: #ffffff;" class="ls-l hidden" data-ls="showinfo:1;offsetxin:-100lw;durationin:800;delayin:1700;easingin:easeInOutBack;offsetxout:-100lw;durationout:900000000000000;startatout:transitioninend + 4400;easingout:easeInBack;clipout:0 100% 0 0;position:absolute;"><span style="font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif">Pós-Graduação IPT - conheça nossos cursos </span><%--ls-hide-phone--%>
                        </h2>

                        <h2 style="top: 70%; left: 30%; text-align: initial; font-weight: 400; font-style: normal; text-decoration: none; word-wrap: normal; opacity: 1; font-family: Gudea; font-size: 30px; color: #ffffff;" class="ls-l" ><span style="font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif">Pós-Graduação IPT - conheça nossos cursos </span><%--ls-hide-phone--%>
                        </h2>
                    </div>

                </div>
            </section>


            <br /><br /><br />
            <div class="container" id="O_que_e">
                <div>
                    <h2 class="widget-title">Pós-Graduação IPT</h2>
                </div>

                <div class="row">
                    <div class="col-md-12" style="font-family: sans-serif; text-align: justify; font-size: 1.8rem;">
                        
                        <p>
                            O IPT oferece diversas modalidades de cursos para o público externo (graduados e não graduados), tanto na sede do IPT quanto in company. As atividades de ensino são compostas por diferentes formatos de cursos e atividades, a saber:
                        <br /><br />
                            <i class="fa fa-angle-double-right" style="color:#016699"></i><a class="aMestrado a_listaCurso" href="#"> Mestrado Profissional</a><br />
                            <i class="fa fa-angle-double-right" style="color:#016699"></i><a class="aEspecializacao a_listaCurso" href="#"> Especialização</a><br />
                            <i class="fa fa-angle-double-right" style="color:#016699"></i><a class="aCurtaDuracao a_listaCurso" href="#"> Curta Duração</a><br />
                            <i class="fa fa-angle-double-right" style="color:#016699"></i><a class="aInCompany a_listaCurso" href="#"> Educação Corporativa</a><br />
                            <i class="fa fa-angle-double-right" style="color:#016699"></i><a class="aMBAInternacional a_listaCurso" href="#"> MBA Internacional</a><br />

                        <br />
                           
                        </p>
                        <button type="button" class="btn btn-primary btn-lg center-block bSobre" href="#" onclick="">
                            Saiba mais
                        </button>
                    </div>
                    <div class="hidden-lg hiddden-md">
                        <br />
                    </div>

                    <div class="col-md-4" style="display:none">
                        <a target="_blank" href="http://revista.ipt.br/index.php/revistaIPT">
                            <img class="img-rounded img-responsive" src="img/Homepage/revista_ipt.png" /> 
                        </a>
                         <%--amostra/home_2.jpg--%>
                        <div>
                            <br /><br />
                        </div>
                    </div>

                </div>

            </div>

            <br /><br /><br />

            <div style="background-color:#f8f8f8">
                <br /><br /><br />
            </div>

            <br /><br /><br />

            <div class="container">
                <div>
                    <h2 class="widget-title">Depoimentos</h2>
                </div>

                <div id="myCarousel" class="carousel slide carousel-fade" data-interval="6000" data-ride="carousel" style="z-index:0">
                    <!-- Carousel indicators -->
                    <ol class="carousel-indicators" style="margin-bottom:-10px">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                        <li data-target="#myCarousel" data-slide-to="3"></li>
                        <li data-target="#myCarousel" data-slide-to="4"></li>
                        <li data-target="#myCarousel" data-slide-to="5"></li>
                        <li data-target="#myCarousel" data-slide-to="6"></li>
                        <li data-target="#myCarousel" data-slide-to="7"></li>
                    </ol>
                    <!-- Carousel items -->
                    <div class="carousel-inner">
                        <div class="item active">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>O mestrado me possibilitou iniciar uma carreira complementar. Desde o ano de 2009 leciono como professora convidada na disciplina ‘PGP-009 Análise de custos e de viabilidade de construções’. 
                                                <br /><br />
                                                Recomendo o mestrado porque, além das disciplinas oferecidas e o estudo aprofundado em um tema durante o desenvolvimento da dissertação, os contatos e trocas de conhecimento com professores e colegas de curso são enriquecedores.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;"></i>
                                        </span>
                                        
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/e69a0-sandra-haruna-hashizume.jpg" alt="" class="img-rounded" style="display: inline-block" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Sandra Haruna Hashizume</strong></cite>

                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>
                        
                        <div class="item">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>Acredito que o principal impacto do mestrado foi me tornar mais questionador em busca do saber. O ponto de você questionar sobre tópicos que não fazem parte do seu domínio tem uma consequência significativa: o aumento constante do conhecimento. Aprendi a defender a ideia do filósofo Sócrates: ‘Quanto mais sei, mais sei que nada sei’. Levo isto como um grande aprendizado que trago do mestrado e consigo aplicar tanto nas minhas atividades na empresa quanto docentes. 
                                                <br /><br />
                                                Recomendo o curso no IPT principalmente se existe uma busca insaciável por resolver problemas no dia a dia de trabalho que, muitas vezes, devido à pressão de entregas, não conseguimos parar e buscar soluções em pesquisas acadêmicas. Agradeço sempre ao professor Paulo Sergio Muniz Silva por ter estimulado esse lado.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;padding-right:0 !important"></i>
                                        </span>
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/34309-ricardo-tardelli-pessoa.jpg" alt="" class="img-rounded" style="display: inline-block" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Ricardo Tardelli Pessoa</strong></cite>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>

                        <div class="item">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>Por causa do mestrado no IPT pude pesquisar algo que realmente acreditava que faria uma diferença. Fiz algumas apresentações na empresa em que trabalhava, mas a mudança foi quando apresentei um projeto paralelo a minha área de pesquisa no Congresso Internacional de Tecnologia da Informação para a Construção. Conheci pesquisadores do mundo inteiro no evento e fui, por causa disso, cursar um doutorado no exterior. 
                                                <br /><br />
                                                Vim para os Estados Unidos logo após o fim do mestrado, onde pude continuar com a pesquisa aplicada. Agora, como professora da Purdue University, dou aulas e pesquiso na área de gestão de obras e projetos. Recomendo o mestrado por dois diferenciais: o primeiro foi ter aulas no período noturno e poder continuar trabalhando enquanto estudava e fazia a pesquisa; o segundo é o foco na pesquisa aplicada.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;padding-right:0 !important"></i>
                                        </span>
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/8321d-luciana-de-cresce-el-debs.jpg" alt="" class="img-rounded" style="display: inline-block" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Luciana de Cresce El Debs</strong></cite>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>

                        <div class="item">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>
                                                O mestrado ofereceu a mim uma atualização quanto ao processo de desenvolvimento de software e tecnologias da informação, assim como uma melhoria na abordagem de conceitos de tecnologia da informação em sala de aula e preparação para o doutorado.
                                                <br /><br />
                                                Tenho recomendado o curso de POS-Graduação do IPT a alunos e colegas sempre que abordado sobre o tema.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;padding-right:0 !important"></i>
                                        </span>
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/482c2-gianni-ricciardi.jpg" alt="" class="img-rounded" style="display: inline-block" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Gianni Ricciardi</strong></cite>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>

                        <div class="item">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>
                                                O mestrado fortaleceu meu relacionamento com a FGV, além de ter melhorado significativamente as avaliações dos alunos. 
                                                <br />
                                                Sou professor de Marketing e orientador de TCCs nos MBAs da FGV desde 2004, além de ministrar treinamentos em Negociações Estratégicas. 
                                                <br /><br />
                                                Sou profissional com experiência na indústria petroquímica - 38 anos - e decidi me dedicar exclusivamente à vida acadêmica e de conteúdos no último ano. Neste momento, estou pesquisando doutorado na área de Gestão em Marketing, em que atuo atualmente.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;padding-right:0 !important"></i>
                                        </span>
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/b2d85-flavio-ricardo-rodrigues.jpg" alt="" class="img-rounded" style="display: inline-block" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Flavio Ricardo Rodrigues</strong></cite>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>

                        <div class="item">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>
                                                Tinha intenção em migrar da carreira executiva para a acadêmica e o mestrado me preparou, com excelência, em pesquisa. Depois me permitiu cursar o doutorado. 
                                                <br />
                                                Atualmente sou professor no mestrado do IPT, o que me dá muito orgulho.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;padding-right:0 !important"></i>
                                        </span>
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/403e3-claudio-luis-carvalho-larieira.jpg" alt="" class="img-rounded" style="display: inline-block" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Claudio Luis Carvalho Larieira</strong></cite>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>

                        <div class="item">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>
                                                Apresentei minha dissertação de mestrado em uma quinta-feira; no sábado, embarquei para trabalhar dois anos e meio em um escritório de engenharia de grande porte na cidade de Nova York, onde integrei a equipe de design computacional. Atualmente estou morando na Alemanha. Acredito que a experiência em Nova York, resultado da minha dissertação, foi fundamental para conquistar minha atual posição. 
                                                <br /><br />
                                                Para mim o mestrado e os anos no IPT foram muito válidos e vieram no momento certo da minha carreira. Complementei minha formação de arquiteta com o lado construtivo e prático da profissão, justamente o que eu procurava. No IPT, além da técnica, aprendi a escrever relatórios técnicos e a definir planos de pesquisa. Estas capacitações me ajudam muito no meu dia a dia.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;padding-right:0 !important"></i>
                                        </span>
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/Anne Waelkens.jpg" alt="" class="img-rounded" style="display: inline-block;width:65px !important;height:65px !important" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Anne Catherine Waelkens</strong></cite>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>

                        <div class="item">
                            <section style="background-color: white">
                                <div class="swiper-slide">
                                    <blockquote class="blockquote blockquote--style-1">
                                        <span>
                                            <i class="fa fa-quote-left" style="color:#016699;">&nbsp;</i>
                                            <em>
                                                Comecei a dar aulas assim que terminei o mestrado no fim de 2010. Também ingressei no doutorado por meio dos contatos feitos durante a defesa do trabalho. 
                                                <br /><br />
                                                Posso dizer que o mestrado do IPT foi o fator preponderante para eu ingressar no meio acadêmico e trabalhar com pesquisas: creio que ele é o caminho ideal para o profissional que quer ingressar na área acadêmica e de pesquisas, sem deixar de lado o legado que acumulou no mercado de trabalho. Costumo recomendá-lo enfatizando justamente este aspecto.
                                            </em>
                                            <i class="fa fa-quote-right" style="color:#016699;padding-right:0 !important"></i>
                                        </span>
                                    </blockquote>
                                    <div class="blockquote-author" style="padding-left:20px;">
                                        <img src="img/Homepage/Anderson Aparecido Alves da Silva.jpg" alt="" class="img-rounded" style="display: inline-block;width:65px !important;height:65px !important" />
                                        <div class="blockquote-author__info" style="display: inline-block">
                                            <cite class="blockquote-author__name" style="font-size:medium;color:#016699"><strong> Anderson Aparecido Alves da Silva</strong></cite>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <div class="carousel-caption">
                                <%--<h3>Primeiro slide</h3>
                        <p>Lorem ipsum dolor sit amet...</p>--%>
                            </div>
                        </div>

                    </div>

                    <!-- Left and right controls -->
                      <%--<a class="left carousel-control" href="#myCarousel" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        <span class="sr-only">Previous</span>
                      </a>
                      <a class="right carousel-control" href="#myCarousel" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                        <span class="sr-only">Next</span>
                      </a>--%>
                </div>

            </div>

            <br /><br /><br /><br /><br />



        </div>

<!-- LayerSlider script files -->

	    <!-- External libraries: jQuery & GreenSock -->
	    <script src="Scripts/Homepage/layerslider/js/greensock.js"></script>

	    <!-- LayerSlider script files -->
	    <script src="Scripts/Homepage/layerslider/js/layerslider.transitions.js"></script>
	    <script src="Scripts/Homepage/layerslider/js/layerslider.kreaturamedia.jquery.js"></script>

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

		    $(document).ready(function () {
		        document.getElementById('layerslider').style.display = 'block';
		        
		        $('#myCarousel').carousel({
		            interval: 15000
		        });

		        $(this).scrollTop(0);

		        $(".bSobre").click(function () {
		            $('#result').load('index_sobre.aspx');
		        });

		        $(".aMestrado").click(function () {
		            $('#result').load('index_area.aspx?qIdTipo=1');
		        });

		        $(".aEspecializacao").click(function () {
		            $('#result').load('index_area.aspx?qIdTipo=3');
		        });

		        $(".aCurtaDuracao").click(function () {
		            $('#result').load('index_area.aspx?qIdTipo=4');
		        });

		        $(".aInCompany").click(function () {
		            $('#result').load('index_area.aspx?qIdTipo=5');
		        });

		        $(".aMBAInternacional").click(function () {
		            $('#result').load('index_area.aspx?qIdTipo=2');
		        });

		    });
	    </script>

        <!-- Global site tag (gtag.js) - Google Analytics -->
        <script async src="https://www.googletagmanager.com/gtag/js?id=UA-154434342-1"></script>
        <script>
          window.dataLayer = window.dataLayer || [];
          function gtag(){dataLayer.push(arguments);}
          gtag('js', new Date());

          gtag('config', 'UA-154434342-1', {
              'page_title': 'Página Home',
              'page_path': '/index.aspx'
          });
        </script>


    </form>
    
</body>
</html>
