<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SERPI.UI.WebForms_C.Login" %>

<html lang="pt-br" class="translated-ltr">
    <head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SAPIENS :: Sistema de Apoio Integrado ao Ensino</title>

    <link rel="SHORTCUT ICON" href="img/formatura.png"" type="image/x-icon">

    <!-- Bootstrap Core CSS -->
    <link href="Content/bootstrap.css" rel="stylesheet" />

    <link href="Content/home.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="Content/metisMenu.min.css" rel="stylesheet" />

    <!-- Timeline CSS -->
    <link href="Content/timeline.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="Content/sb-admin-2.css" rel="stylesheet" />

    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">

    <!-- Custom Fonts -->
<%--    <link href="Content/font-awesome.min.css" rel="stylesheet" type="text/css" />--%>
          <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css"/>

    <link href="Content/animate.min.css" rel="stylesheet" />
</head>

<body onkeydown="teclaEnter();">
                   
    <form id="FORM" name="FORM" method="post" runat="server">
        <style>
            .carousel-indicators li { 
                background-color: #cdcdcd;
            }
            .carousel-indicators .active {
                background-color: #646464;
            }
        </style>
        <style>
            .banner {
                /*background-image: url('img/estudante.jpg');*/
                background-image: url('img/Capa.png');
                height: 70vh;
                /*background-repeat: no-repeat;
                background-position: center;
                overflow: hidden;
                background-size: cover;*/
                background-position: bottom;
                background-repeat: no-repeat;
                background-attachment: fixed;
                background-size: cover;
                position: relative;
                min-height: 550px;
            }

            .banner_desenvolvimento {
                background: url('img/biblioteca.jpg');
                background-repeat: no-repeat;
                background-position: center;
                overflow: hidden;
                background-size: cover;
            }

            .banner_verde {
                background-color: #01A2A6;
                background-repeat: no-repeat;
                background-position: center;
                overflow: hidden;
                background-size: cover;
            }

            .font_4 {
                font: normal normal normal 45px/1.4em Spinnaker,sans-serif;
                color: #060605;
            }

            .font_8 {
                font: normal normal normal 40px/1.4em Spinnaker,sans-serif;
                color: #01A2A6;
            }

            .color_13 {
                color: #0FBC71;
            }

            .color_branco {
                color: #FFFFFF;
            }
        </style>
        
        <script>
            function AbreModalMensagem(qClass) {
                $("#divCabecalho").removeClass("alert-warning");
                $("#divCabecalho").removeClass("alert-primary");
                $('#divCabecalho').removeClass('alert-danger');
                $('#divCabecalho').removeClass('alert-success');
                $('#divCabecalho').addClass(qClass);
                $('#divMensagemModal').modal();
            }
        </script>


        <header>

            <nav id="desktop" class="navbar navbar-default navbar-fixed-top" role="navigation">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button id="responsivo" type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#login" aria-expanded="false">Entrar <span class="glyphicon glyphicon-chevron-down"></span></button>
                    <a class="navbar-brand" href="#" style="padding-top: 20px;"><img src="img/formatura.png" />&nbsp;SAPIENS - <small>Sistema de Apoio Integrado ao Ensino <asp:Label ID="lblDesenvolvimento" runat="server" Text="" ForeColor="Red"></asp:Label></small></a>
                </div>
                <div class ="hidden-lg hidden-md"><br /></div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="navbar-collapse" id="login" style="height: auto;">

                    <div class="navbar-form navbar-right">
                        <div class="form-group">
                            <input runat="server" class="form-control input-sm"  placeholder="Login" id="txtLogin1" name="txtLogin1" type="text" value="" maxlength="15" size="12" autocomplete="off"/>
                        </div>
                        <div class="form-group">
                            <input runat="server" class="form-control input-sm"  placeholder="Senha" id="txtSenha1" name="txtSenha1" type="password" value="" size="8" maxlength="10" autocomplete="off" />
                        </div>
                        <div class="form-group">
                            <button runat="server" id="btnLogin" name="btnLogin" onServerClick="btnLogin_Click" class="btn btn-outline btn-primary" href="#" onclick="javascript:document.FORM.Ok.value='C'; Valida(document.FORM);">
                                <i class="fa fa-sign-in"></i>&nbsp;Entrar</button>
                             
                        </div>
                        <div class="form-group">
                            <ul class="nav navbar-nav" class="panel-primary">
                                <li><a id="dificuldadeConectar" class="panel-primary" data-toggle="modal" href="#Dificuldade"><i class="fa fa-question-circle"></i>&nbsp;Dificuldade em se conectar</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </nav>

        </header>

        <section id="section" class="banner" >
            <br /><br /><br />
            <div class ="container">
                <br /><br />
                <div class="hidden-sm hidden-xs">
                    <br /><br /><br /><br /><br />
                </div>
                <div class ="row">
                    <div class ="hidden-lg hidden-md">
                       <br />
                    </div>

                    <div class ="col-md-1 ">
                        <br />
                    </div>

                    <div class="col-md-6 pull-left wow fadeInLeft animated animated"  data-wow-duration="3s"  style="visibility: visible; animation-name: fadeInLeft;">
                        <br /><br />
                        <h4 style="line-height: 1em;" class="font_4"><font color="#FFFFFF">Comece a Mudar o Seu  <p>Futuro</p></font></h4>
                        <div class ="row ">
                            <div class ="col-md-9">
                                <h4 style="line-height: 1.2em;"><font><font color="#FFFFFF"><p>Qualquer carreira de sucesso começa com boa educação. Juntamente conosco, você terá um conhecimento mais profundo dos assuntos que serão especialmente úteis para você ao subir na carreira.</p></font></h4>
                            </div>
                        </div>
                    </div>
                    <div class ="col-md-1 ">
                        <br /><br /><br /><br />
                    </div>

                </div>
                <br /><br />
                <div class="hidden-sm hidden-xs">
                    <br /><br /><br /><br /><br />
                </div>
            </div>
        </section>

        <div class="">
            <div id="carousel-Sobre" class="carousel slide" data-ride="carousel">
                <!-- Indicators -->
                <div class="pricing-slide-heading ">
                    <br /><br /><br />
                    <div class ="row" >
                        <div class="col-md-2 ">
                            
                        </div>
                        <div class="col-md-10 ">
                            <p class="font_8"><strong ><span>Sobre nós</span></strong></p>
                        </div>
                    </div>

                        
                </div>
                
                <br /><br />

                <ol class="carousel-indicators">
                    <li data-target="#carousel-Sobre" data-slide-to="0" class="active"></li>
                    <li data-target="#carousel-Sobre" data-slide-to="1"></li>
                    <li data-target="#carousel-Sobre" data-slide-to="2"></li>
                </ol>
                <!-- Wrapper for slides -->
                <div class="carousel-inner" role="listbox">
                    <div class="item active">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class =""><font><font class="text-muted">Doutor em engenharia química pela Universidade Federal de São Carlos em 2003 e Pesquisador do IPT desde 1981. Professor das cadeiras de Processos de Separação e Tecnologias de controle de poluição do ar nesse mestrado. Publicou 23 artigos em periódicos especializados e 55 trabalhos em anais de eventos. Possui 3 processos / técnicas registrados e outros 150 itens de produção técnica. É membro de 3 equipes premiadas. Em suas atividades profissionais interagiu com 100 colaboradores em coautorias de trabalhos científicos. Em seu curriculum lattes os termos mais frequentes na contextualização da produção científica, tecnológica e artístico-cultural são: cristalização industrial, precipitação, acido adipico, cristalização de produtos orgânicos, sulfato de cobre, aglomeração, pó metálico, hidroxiapatita e cinéticas de cristalização.
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Silas Derenzo.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font class="text-muted"><strong><i>Silas Derenzo</i></strong></font></font></h5>
                                        <h5><font><font class="text-muted">Coordenador do Mestrado Profissional em Processos Industrias do IPT desde 2013. É Bolsista de Produtividade Desen. Tec. e Extensão Inovadora do CNPq - Nível 2</font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="50" height="340" alt="" style="width:50px;height:340px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font class="text-muted">Possui graduação em Engenharia Civil pela Universidade de São Paulo (1980), mestrado em Engenharia Civil pela Universidade de São Paulo (1988) e doutorado em Engenharia Civil pela Universidade de São Paulo (1998). Atualmente é pesquisador senior do Laboratório de Componentes e Sistemas Construtivos do IPT, especialista em sistemas construtivos e desempenho de edificações habitacionais. É docente e coordenador do Mestrado Profissional em Habitação: Planejamento e Tecnologia, do Instituto de Pesquisas Tecnológicas do Estado de São Paulo, responsável pelas disciplinas: Sistemas construtivos: inovação e desempenho; e Qualidade na construção civil. Tem experiência na área de Construção Civil, com ênfase em Processos e Sistemas Construtivos, Avaliação de desempenho de sistemas construtivos, e Desempenho de edificações habitacionais.
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Claudio Vicente Mitidieri Filho.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font class="text-muted"><strong><i>Cláudio Vicente Mitidieri Filho</i></strong></font></font></h5>
                                        <h5><font><font class="text-muted">Coordenador do Mestrado Profissional em Habitação: Planejamento e Tecnologia</font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="50" height="340" alt="" style="width:50px;height:340px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class =""><font><font class="text-muted">Economista e Doutor em Teoria Econômica pela Universidade de São Paulo. Engenheiro Civil pela Escola Politécnica da Universidade de São Paulo. Visiting scholar na Universidade de Wageningen, Holanda. Pós-doutorado no Centro Brasileiro de Análise e Planejamento. Foi Diretor do Núcleo de Economia e Administração da Tecnologia (2006-2009), Diretor da Gerência de Gestão Tecnológica (2009-2011) no Instituto de Pesquisas Tecnológicas. Coordenador no Curso de Ciências Econômicas (2013-2015) Coordenador do Programa de Mestrado em Economia e Desenvolvimento (2017-2018) na Universidade Federal de São Paulo. Atualmente é Pró-reitor de Pós Graduação, Coordenador do Mestrado Profissional em Engenharia de Computação e Professor Permanente do Mestrado em Processos Industriais no Instituto de Pesquisas Tecnológicas e Professor Adjunto IV da Universidade Federal de São Paulo. Linhas de pesquisa em Organização Industrial, Regulação, Economia do Meio Ambiente, Nova Economia Institucional, Transformação Digital e Indústria 4.0 e Economia e Direito.
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Eduardo Luiz Machado.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font class="text-muted"><strong><i>Eduardo Luiz Machado</i></strong> </font></font></h5>
                                        <h5><font><font class="text-muted">Diretor Técnico do Mestrado Profissional em Engenharia de Computação</font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="50" height="340" alt="" style="width:50px;height:340px">
                        </div>
                    </div>

                </div>
                <a class="left carousel-control hidden-xs hidden-sm" href="#carousel-Sobre" role="button" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                    <span class="sr-only">Anterior</span>
                </a>
                <a class="right carousel-control hidden-xs hidden-sm" href="#carousel-Sobre" role="button" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                    <span class="sr-only">Próximo</span>
                </a>
            </div>
        </div>

        <section >
            <div class ="row">
                <div class ="col-md-2"></div>
                <div class ="col-md-5">
                    <br /><br />
                    <div class="hidden-sm hidden-xs">
                        <br />
                    </div>

                    <div class ="row" >

                        <div class ="col-md-12 wow zoomIn animated animated pull-left " data-wow-duration="3s" " >
                            <div class="embed-responsive embed-responsive-16by9">
                                <iframe width="560" height="315" src="https://www.youtube.com/embed/rMKIarQBG9k" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                <p class="text-muted"><strong><span><br />Prof. Dr. Eduardo Luiz Machado</span></strong></p>
                                <br />
                                <i style="color:black" class="fa fa-graduation-cap fa-5x"></i>
                            </div>

                        </div>
                    </div>
                    <br /><br />
                </div>
                <div class ="hidden-lg hidden-md">
                    <br />
                </div>

                <div class ="col-md-5">
                    <br /><br />
                    <div class="hidden-sm hidden-xs">
                        <br />
                    </div>

                    <div class ="row" >

                        <div class ="col-md-12 wow zoomIn animated animated pull-left " data-wow-duration="3s" " >
                            <div class="embed-responsive embed-responsive-16by9">
                                <iframe width="560" height="315" src="https://www.youtube.com/embed/Y5z8E82AiPE" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                <p class="text-muted"><strong><span><br />Prof. Dr. Wilson Shoji Iyomasa</span></strong></p>
                            </div>

                        </div>
                    </div>
                    <br /><br />
                </div>

                <br /><br />
            </div>
        </section>
        <br /><br />


        <section class="banner_verde">
            <div class ="container">
                <br />
                <div class="hidden-sm hidden-xs">
                    <br /><br /><br /><br /><br />
                </div>
                <div class ="row">
                    <br />
                    <div class ="hidden-lg hidden-md">
                       <br />
                    </div>
                    <div class="col-md-7 pull-right wow fadeInRight animated animated" data-wow-duration="3s" data-wow-offset="50">
                         <br /> <br />
                        <img alt="" src="img/professor.jpg"" class="img-responsive">
                    </div>
                    <div class ="col-md-1 ">
                        <br />
                    </div>

                    <div class="col-md-5 pull-left ">
                        <br />
                        <div class ="row ">
                            <div class ="col-md-10 wow fadeInLeft animated animated" data-wow-duration="3s" data-wow-offset="50" >
                                <h5 style="line-height: 1em;" class="font_4"><font color="#FFFFFF">CORPO DOCENTE QUALIFICADO</font></h5>
                            </div>
                        </div>

                        <div class ="row ">
                            <div class ="col-md-10 text-justify wow fadeInLeft animated animated" data-wow-duration="3s" data-wow-offset="50" >
                                <h4><font><font color="#FFFFFF"><p>O Corpo Docente do Mestrado do IPT é formado por Doutores qualificados em diversas áreas...</p></font></h4>
                            </div>
                        </div>

                        <br />

                    </div>
                    
                </div>
                <br /><br />
                    <div class="hidden-sm hidden-xs">
                        <br /><br /><br /><br />
                    </div>
            </div>
        </section>

        <%--<section class="banner_desenvolvimento">
            <div class="container">
                <div class="row">
                    <div class="pricing-slide-heading text-center">
                        <br />
                        <h4><font><font color ="#FFFFFF">Depoimentos</font></font></h4>
                       <!-- <h2><font><font color ="#FFFFFF">Pricings &amp; Planos</font></font></h2>-->
                        <!-- <img class="img=responsive" src="img/daag.png" alt="">-->
                    </div>
                    <br /><br />
                    <div class="the-pricing-slider">
                        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel" data-interval="7000">
                            <!-- Indicators -->
                            <ol class="carousel-indicators">
                                <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                                <li data-target="#carousel-example-generic" data-slide-to="1" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="2" class=""></li>
                            </ol>
                            <!-- Wrapper for slides -->
                            <div class="carousel-inner" role="listbox">
                                <div class="item active">
                                    <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                        <h4><p><font><font color="#FFFFFF">Cursou o Mestrado Profissional em Engenharia de Computação no IPT, entre 2007 e 2010, atuando no campo de Redes. Trabalhou 22 anos como gerente de programação da Donatelli Tecidos até 2017. Atualmente é pesquisador bolsista do programa de pós-doutorado da Escola Politécnica da USP e ministra aulas para graduação na UNIP, para pós-graduação no SENAC e para o curso de Mestrado do próprio IPT.
                                            Anderson destaca os principais impactos do Mestrado em sua carreira. <br /><br /> “Comecei a dar aulas assim que terminei o Mestrado no fim de 2010. Também ingressei no doutorado por meio dos contatos que fiz durante a defesa do mestrado. Posso dizer que o mestrado do IPT foi o fator preponderante para eu ingressar no meio acadêmico e trabalhar com pesquisas. Creio que o Mestrado Profissional é o caminho ideal para o profissional que quer ingressar na área acadêmica e de pesquisas, sem deixar de lado o legado que acumulou no mercado de trabalho. Costumo recomendá-lo enfatizando justamente este aspecto.”
                                            </font></font></p></h4>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-1">
                                                <img class="img-circle img-responsive" src="img/Anderson Aparecido Alves da Silva.jpg" width="200" height="80" alt="" style="width:55px;height:55px">
                                            </div>
                                            <div class="col-md-11">
                                                <h5><font><font color="#FFFFFF">Anderson Aparecido Alves da Silva </font></font></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="3s" style="visibility: visible; animation-name: zoomIn;">
                                        <h4>
                                            <p><font><font color="#FFFFFF">Quando eu pesquisei os cursos disponíveis eu percebi que o IPT estava oferecendo exatamente o tipo de Mestrado Profissional que me interessava.</font></font></p>
                                        </h4>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-1">
                                                <img class="img-circle img-responsive" src="img/eu.png" width="70" height="20" alt="">
                                            </div>
                                            <div class="col-md-11">
                                                <h5><font><font color="#FFFFFF">Kelsey M. Melo</font></font></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-md-12 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="3s" style="visibility: visible; animation-name: zoomIn;">
                                        <h4>
                                            <p><font><font color="#FFFFFF">"O aumento do conhecimento é como uma esfera dilatando-se no espaço:</font></font></p>
                                            <p><font><font color="#FFFFFF">quanto maior a nossa compreensão, maior o nosso contato com o desconhecido." <i>Blaise Pascal (1623-1662)</i></font></font></p>
                                            <p class="hidden-lg hidden-md "><font><font color="#FFFFFF"> &nbsp;</font></font></p>
                                        </h4>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-1">
                                                <img class="img-circle img-responsive" src="img/eu2.jpg" width="70" height="20" alt="">
                                            </div>
                                            <div class="col-md-11">
                                                <h5><font><font color="#FFFFFF">Kelsey M. Melo</font></font></h5>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                                
                            </div>
                            
                        </div>
                        <br /><br /><br /><br /><br />
                    </div>
                </div>
            </div>
            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        </section>--%>

        <div class="banner_desenvolvimento">
            <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                <!-- Indicators -->
                <div class="pricing-slide-heading text-center">
                    <br />
                    <h4><font><font color ="#FFFFFF">Depoimentos</font></font></h4>

                        
                </div>
                    <br /><br />

                <ol class="carousel-indicators">
                    <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="3"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="4"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="5"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="6"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="7"></li>
                    <li data-target="#carousel-example-generic" data-slide-to="8"></li>
                </ol>
                <!-- Wrapper for slides -->
                <div class="carousel-inner" role="listbox">
                    <div class="item active">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">Cursou o Mestrado Profissional em Engenharia de Computação no IPT, entre 2007 e 2010, atuando no campo de Redes. Trabalhou 22 anos como gerente de programação da Donatelli Tecidos até 2017. Atualmente é pesquisador bolsista do programa de pós-doutorado da Escola Politécnica da USP e ministra aulas para graduação na UNIP, para pós-graduação no SENAC e para o curso de Mestrado do próprio IPT.
                                Anderson destaca os principais impactos do Mestrado em sua carreira. <br /><br /> “Comecei a dar aulas assim que terminei o Mestrado no fim de 2010. Também ingressei no doutorado por meio dos contatos que fiz durante a defesa do mestrado. Posso dizer que o mestrado do IPT foi o fator preponderante para eu ingressar no meio acadêmico e trabalhar com pesquisas. Creio que o Mestrado Profissional é o caminho ideal para o profissional que quer ingressar na área acadêmica e de pesquisas, sem deixar de lado o legado que acumulou no mercado de trabalho. Costumo recomendá-lo enfatizando justamente este aspecto.”
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Anderson Aparecido Alves da Silva.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Anderson Aparecido Alves da Silva</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">Foi aluno do Mestrado Profissional do IPT em Processos Industriais, completado em 2009, tendo como orientador o professor Eduardo Machado. É professor na FGV (MBAs).
Para Flavio, o Mestrado do IPT impactou positivamente sua carreira. <br /><br />“Fortaleceu meu relacionamento com a Fundação (FGV), além de melhorar significativamente as avaliações dos alunos. Sou professor de Marketing e Orientador nos TCCs nos MBAs da FGV desde 2004, além de responsável por treinamentos em Negociações Estratégicas. Sou profissional com experiência na indústria petroquímica com atuação de 38 anos. Decidi dedicar-me exclusivamente à vida acadêmica e de conteúdos no último ano. Neste momento, estou em Lisboa pesquisando doutorado na área de Gestão em Marketing, onde atuo.” <br /><br /> Recomenda o Mestrado do IPT a profissionais de sua área. “Sem a menor sombra de dúvida. Sempre destaco isso em meus relacionamentos.”
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Flavio Ricardo Rodrigues.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Flavio Ricardo Rodrigues</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">É diretor técnico da Pro-Tech Engenharia Ltda. Cursou o mestrado Profissional do IPT em Processo Industriais, no período de 2007 a 2009.
Para Carlos o Mestrado do IPT agregou valor à sua carreira. <br /><br /> “Adicionou credibilidade junto aos clientes, uma vez que atuo em consultoria. Além disso, o Mestrado Profissional despertou meu interesse pela área acadêmica. Em 2011 iniciei um doutorado em Engenharia Química na Escola Politécnica, graças ao incentivo do saudoso professor Marco Giulietti. Finalizei o doutorado em 2015, tendo como orientador o professor Marcelo Seckler, que também conheci no Mestrado Profissional no IPT.” <br /><br />Perguntado se recomendaria o curso, foi sucinto: “Sem dúvida!”

                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Carlos Eduardo Pantoja.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Carlos Eduardo Pantoja</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">Cursou o Mestrado Profissional do IPT na área de Processos Industriais, no período de 2012 a 2015. Hoje é gerente geral na Damm Produtos Alimentícios Ltda.
Carlos vê impactos importantes do Mestrado em sua atividade profissional.<br /><br /> “Propiciou o aprofundamento nas questões técnicas. Sobretudo nos debates voltados para os aspectos práticos do mercado e da operação, que me habilitaram para assumir novas responsabilidades como um profissional estratégico.  A análise das restrições e o estudo de viabilidade das alternativas resultaram na criação de uma solução inovadora para a cadeia de suprimento da empresa, tornando possível a redução de 51% dos custos de transporte da principal matéria-prima. Após o Mestrado feito no IPT fui promovido a Gerencia Geral da Organização.”<br /><br /> Carlos também recomenda o curso do IPT. “Recomendo o Mestrado Profissional em Processos Industriais do IPT para profissionais do agronegócio e da indústria de alimentos.  O ambiente proporciona discussões de elevado nível técnico, com  foco no resultado prático.”
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Carlos Vieira Leite.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Carlos Vieira Leite</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">É professor na FIAP, FGV e IPT. Cursou o mestrado Profissional do IPT na área de Engenharia de Software e concluiu em 2012.
<br /><br /> Sobre os principais impactos do curso em sua carreira, Gianni destaca alguns pontos. “Atualização quanto ao processo de desenvolvimento de software e tecnologias da informação. Melhoria na abordagem de conceitos de tecnologia da informação em sala de aula e preparação para o doutorado.”<br /><br /> Recomendaria o mestrado do IPT a profissionais de sua área de atuação? “Sim, tenho recomendado a alunos e colegas sempre que abordado sobre o tema.

                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Gianni Ricciardi.png" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Gianni Ricciardi</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">Atualmente é responsável pela área de Processos, Qualidade e Auditoria dentro do Delivery na T-Systems do Brasil e professor de temas relacionados ao desenvolvimento de software na FIAP. Concluiu o Mestrado Profissional do IPT na área de Engenharia de Computação em 2016.
<br /><br /> Na carreira profissional e acadêmica Ricardo enfatiza o aspecto crítico. “Acredito que o principal impacto do Mestrado foi me tornar mais questionador em busca do saber. O ponto de você questionar sobre tópicos que não fazem parte do seu domínio tem uma consequência significativa: o aumento constante do seu conhecimento. Aprendi a defender a ideia do filosofo Sócrates: ‘quanto mais sei, mais sei que nada sei’. Levo isto como um grande aprendizado que trago do mestrado e consigo aplicar tanto nas minhas atividades na T-Systems do Brasil, quanto na FIAP.” 
<br /><br />Ricardo recomenda o curso do IPT. “Principalmente se existe uma busca insaciável por resolver problemas que acontecem no dia-dia de trabalho que, muitas vezes, devido à pressão de entregas não conseguimos parar e buscar soluções em pesquisas acadêmicas. Agradeço sempre ao professor Paulo Sergio Muniz Silva por ter estimulado esse lado que, embora tivesse dificuldade, sempre foi um suporte durante todo o curso.”
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Ricardo Tardelli Pessoa.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Ricardo Tardelli Pessoa</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">Atualmente é professora assistente na Purdue University, Estados Unidos.  Cursou o Mestrado Profissional no IPT entre 2010 e 2013, na área de Habitação – Planejamento e Tecnologia. 
<br /><br /> A busca de diferenciais no curso fez a diferença na carreira de Luciana. “Por causa do Mestrado no IPT pude pesquisar algo que realmente acreditava que faria uma diferença. Fiz algumas apresentações na empresa em que trabalhava, mas foi apresentando um projeto paralelo a minha área de pesquisa para o grupo CIB W78 (Congresso Internacional de Tecnologia da Informação para a Construção) que fez a diferença. Durante o congresso pude conhecer pesquisadores do mundo inteiro e acabei, por causa disso, indo para um Doutorado no exterior. Fui aceita e vim para os Estados Unidos logo após o fim do Mestrado, onde pude continuar com a pesquisa aplicada. Agora, como professora da Purdue University, dou aulas e pesquiso na área de gestão de obras e projetos.”
<br /><br />Perguntada se recomendaria o Mestrado do IPT, Luciana é direta. “Sim, por dois diferenciais. O primeiro foi ter aulas à noite a poder continuar trabalhando enquanto estudava e fazia a pesquisa. O outro diferencial é o foco na pesquisa aplicada, que é o que eu realmente gosto de fazer!”
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Luciana Debs.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Luciana de Cresce El Debs</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">Atua e é proprietária da HRS Engenharia e Custos. Concluiu seu Mestrado Profissional no IPT em 2006, na área de Habitação – Planejamento e Tecnologia.
<br /><br /> Considera que o curso do IPT impactou positivamente sua carreira. “O Mestrado me possibilitou iniciar uma carreira complementar. Desde o ano de 2009 leciono como professora convidada na disciplina ‘PGP-009 Análise de custos e de viabilidade de construções’.” 
<br /><br /> Sandra também recomenda o Mestrado do IPT. “Recomendo, pois além das disciplinas oferecidas e o estudo aprofundado em um tema durante o desenvolvimento da dissertação, os contatos e trocas de conhecimento com professores e colegas de curso também são muito enriquecedores.”
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Sandra Haruna Hashizume.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Sandra Haruna Hashizume</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>

                    <div class="item">
                        <div class="col-md-3 pull-left">
                        </div>

                        <div class="col-md-7">                            
                            <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12 zoomIn animated" data-wow-duration="1s" style="visibility: visible; animation-name: zoomIn;">
                                <h4><p class ="text-justify"><font><font color="#FFFFFF">É consultora na Turner & Townsend GmbH em Munique, Alemanha. Concluiu o Mestrado Profissional no IPT em 2012, em Habitação – Planejamento e Tecnologia.
<br /><br /> O Mestrado no IPT impactou positivamente a carreira de Anne. <br />“Apresentei minha dissertação de Mestrado em uma quinta-feira, no sábado embarquei para trabalhar dois anos e meio em um escritório de engenharia de grande porte na cidade de Nova York, onde integrei a equipe de design computacional. Atualmente estou morando na Alemanha. Acredito que a experiência em Nova York, resultado da minha tese no IPT, foi fundamental para conquistar minha atual posição”, disse ela.
<br /><br />Embora confesse não ter maiores informações sobre outros cursos de mestrado profissional no Brasil, Anne considera importante a experiência adquirida. “Para mim o mestrado e os anos no IPT foram muito válidos e vieram no momento certo da minha carreira. Complementei minha formação de arquiteta com o lado construtivo e prático da profissão, justamente o que eu procurava. No IPT, além da técnica, aprendi a escrever relatórios técnicos e a definir planos de pesquisa. Estas capacidades me ajudam muito no meu dia a dia.”
                                </font></font></p></h4>
                                <br />

                                <div class="row">
                                    <div class="col-md-2">
                                        <img class="img-circle img-responsive" src="img/Anne Waelkens.jpg" width="100" height="80" alt="" style="width:100px;height:100px">
                                    </div>
                                    <div class="col-md-10 pull-left ">
                                        <br />
                                        <h5><font><font color="#FFFFFF"><i>Anne Catherine Waelkens</i> </font></font></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right hidden-xs hidden-sm">
                            <img src="img/BarraTransparente.png" width="100" height="500" alt="" style="width:100px;height:500px">
                        </div>
                    </div>


                </div>
                <a class="left carousel-control hidden-xs hidden-sm" href="#carousel-example-generic" role="button" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                    <span class="sr-only">Anterior</span>
                </a>
                <a class="right carousel-control hidden-xs hidden-sm" href="#carousel-example-generic" role="button" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                    <span class="sr-only">Próximo</span>
                </a>
            </div>
        </div>

        <section class="">
            <br />
        </section>


        <!-- jQuery -->
        <%--<script src="../bower_components/jquery/dist/jquery.min.js"></script>--%>
        <script src="Scripts/jquery-2.1.0.min.js"></script>
        <%-- <script src="Scripts/jquery-ui.min.js"></script>--%>
        <script src="Scripts/jquery-ui.min.js"></script>

        <%--<script src="https://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>--%>



        <!-- Bootstrap Core JavaScript -->
        <script src="Scripts/bootstrap.min.js"></script>

        <!-- Metis Menu Plugin JavaScript -->
        <script src="Scripts/metisMenu.min.js"></script>

        <!-- Custom Theme JavaScript -->
        <script src="Scripts/sb-admin-2.js"></script>

        <script src="Scripts/wow.js"></script>
        
        
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

                        <div class="row">
                            <div class="col-lg-5">
                                <span style="font-size:14px">Login</span><br />
                                <input class="form-control input-sm" runat="server" id="txtLoginSenha" type="text" value="" maxlength="50"/>
                            </div>

                            <div class="col-lg-7">
                                <span style="font-size:14px">Email</span><br />
                                <input class="form-control input-sm" runat="server" id="txtEmailSenha" type="email" value="" maxlength="50"/>
                            </div>
                            
                        </div>
                        
                        <hr size="100" width="100%" noshade color="#9D9D9D" />
                        
                        <div class="row">
                            <div class="col-lg-6 pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                            </div>

                            <div class="col-lg-6 pull-right">
                                    <button type="button" runat="server" id="btnLoginLembrar" name="btnLoginLembrar" class="btn btn-outline btn-primary pull-right" onserverclick="btnLoginLembrar_Click">
                                        <i class="fa fa-sign-in"></i>&nbsp;Lembrar Senha</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <div class="modal fade" id="divMensagemModal" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div id="divCabecalho" class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title" id="CabecalhoMsg2">
                            <asp:Label runat="server" ID="lblTituloMensagem2" Text="" /></h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="lblMensagem2" Text="" />
                    </div>
                    <div class="modal-footer">
                        <div class="pull-right">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                <i class="fa fa-close"></i>&nbsp;Fechar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="divErroLogin" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-danger">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title" id="CabecalhoMsg"><asp:Label runat="server" ID="lblTituloMensagem" Text="" /></h4>
                    </div>
                    <div id="CorpoMsg" class="modal-body">
                        <asp:Label runat="server" ID="lblMensagem" Text="Usuário ou senha inválidos" />
                    </div>
                    <div class="modal-footer">
                        <div class="pull-right">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                <i class="fa fa-close"></i>&nbsp;Fechar</button>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <script>

            function LoginFail() {
                $('#divErroLogin').modal();
                //alert("Hello world");
            }

            new WOW().init();

        </script>

    </form> 
         
</body>

     <script>

         //function LoginFail() {
         //    //$('#Dificuldade').modal();
         //    alert("Hello world");
         //} 

         function teclaEnter() {
             if (event.keyCode == "13") {
                 document.FORM.Ok.value = 'C';
                 Valida(document.FORM);
             }
         }

         function register(e) {
             if (!e) e = window.event;
             var keyInfo = e.keyCode;
         }

         //function isMobile(){
         //    if( navigator.userAgent.match(/Android/i)
         //         || navigator.userAgent.match(/webOS/i)
         //         || navigator.userAgent.match(/iPhone/i)
         //         || navigator.userAgent.match(/iPad/i)
         //         || navigator.userAgent.match(/iPod/i)
         //         || navigator.userAgent.match(/BlackBerry/i)
         //         || navigator.userAgent.match(/Windows Phone/i)
         //    ){
         //        return true;
         //    }
         //    else {
         //        return false;
         //    }	
         //}

         //$(function () {

         //    var mobile = isMobile();

         //    if(mobile == true){
         //        window.location('http://exemplo.asp');
         //    }


         //});

         function Valida(pform) {
             var erro = "";
             var titleMensagem = "";
             var titulo = "";
             var Url = "../valida_login.asp";

             if (pform.Ok.value == "C") {
                 titulo = "<span style='color: #F00'>Alerta!</span>";
                 var campos = "";

                 if (pform.Login_txt.value == "") {
                     if (campos != "") { campos = campos + ', ' }
                     campos = campos + " Digite seu LOGIN";
                     erro = "1";
                 }

                 if (pform.Senha_txt.value == "") {
                     if (campos != "") { campos = campos + " e " } else { campos = campos + " digite sua " }
                     campos = campos + " SENHA";
                     erro = "1";
                 }
                 if (erro == "1") {
                     campos = campos + " para conectar no sistema. ";
                 }
             }
             else {
                 titleMensagem = "";
                 titulo = "Lembrar Senha!"
                 if (pform.flgTorre.value == "S") {
                     if (pform.Predio_Id.value == "") {
                         var campos = "Para enviarmos sua senha para o seu E-MAIL, digite seu LOGIN, e clique no botão LEMBRAR SENHA.";
                         erro = "1";
                     }
                 }

                 if (pform.Login_txt.value == "") {
                     var campos = "Para enviarmos sua senha para o seu E-MAIL, digite seu LOGIN, e clique no botão LEMBRAR SENHA.";
                     erro = "1";
                 }

                 // se estiver com a senha preenchida, tenta fazer a conexao
                 if (pform.Senha_txt.value == "")
                 { var Url = "index.asp" }

                 pform.Ok.value = "S"
             }

             //alert(pform.Ok.value);

             if (pform.Ok.value == "L") {
                 titulo = "Lembrar Senha!";
             }

             if (erro == "") {
                 pform.action = Url;
                 pform.submit();
             }
             else {

                 if (titleMensagem == "") {
                     titleMensagem = titulo;
                 }
                 Mensagem(titleMensagem, "Prezado usuário, <br /><br />" + campos);
             }
         }


        </script>
</html>
