<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tela.aspx.cs" Inherits="SERPI.UI.WebForms_C.tela" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml" lang="pt-br" class="translated-ltr">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>SAPIENS :: Sistema de Apoio Integrado ao Ensino</title>

    <link rel="SHORTCUT ICON" href="img/formatura.png" type="image/x-icon" />

    <%-- <!-- Bootstrap Core CSS -->
    <link href="Content/bootstrap.css" rel="stylesheet" />

<%--    <link href="Content/datepicker.css" rel="stylesheet"  type="text/css"/>--%>

    <%--<link href="Content/home.css" rel="stylesheet"  type="text/css" />

    <!-- Timeline CSS -->
    <link href="Content/timeline.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="Content/sb-admin-2.css" rel="stylesheet" />
    <link href="Content/metisMenu.min.css" rel="stylesheet" type="text/css" />

    <!-- Morris Charts CSS -->
    <link href="Content/morris.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="Content/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <link href="Content/EasyCon.css" rel="stylesheet" />

    <link type="text/css" rel="stylesheet" charset="UTF-8" href="https://translate.googleapis.com/translate_static/css/translateelement.css"/>--%>

    <!-- Bootstrap 3.3.7 -->
    <link href="Content/AdminLTE/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" type="text/css" rel="stylesheet"/>--%>


    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

    <%--https://use.fontawesome.com/releases/v5.0.2/js/all.js--%>

    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />

    <!-- Theme style AdminLTE AdminLTE v2.3.7 -->
    <link href="Content/AdminLTE/AdminLTE.min.css" rel="stylesheet" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
    <link href="Content/AdminLTE/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="Content/animate.min.css" rel="stylesheet" />

    <script src="Scripts/AdminLTE/jQuery/jquery-2.2.3.min.js"></script>
    <%--<script src="Scripts/AdminLTE/jQuery/jquery-2.2.3.min.js"></script>--%>
    <%--    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>--%>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"></script>

    <!-- Bootstrap 3.3.7 -->
    <script src="Scripts/AdminLTE/Bootstrap/bootstrap.min.js"></script>
    <%--    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>--%>


    <!-- AdminLTE App -->
<%--    <script src="Scripts/AdminLTE/app.min.js"></script>--%>

    <script src="Scripts/bootstrap-notify.min.js"></script>

    <style>
        .piscante {

            animation:1s blinker linear infinite;
            -webkit-animation:2s blinker linear infinite;
            -moz-animation:1s blinker linear infinite;
            }

            @-moz-keyframes blinker {  
             0% { opacity: 1.0; }
             50% { opacity: 0.0; }
             100% { opacity: 1.0; }
             }

            @-webkit-keyframes blinker {  
             0% { opacity: 1.0; }
             50% { opacity: 0.0; }
             100% { opacity: 1.0; }
             }

            @keyframes blinker {  
             0% { opacity: 1.0; }
             50% { opacity: 0.0; }
             100% { opacity: 1.0; }
        }
    </style>

    <style>
            .cssCabecalho {
                /*background-image: url("./img/estudante.jpg");*/
                background-image: url("./img/Capa.png");
                height: 115vh;
                background-position: bottom;
                background-repeat: no-repeat;
                background-attachment: fixed;
                background-size: cover;
                position: relative;
                min-height: 550px;
            }

            .cssCabecalho2 {
                background-color:#D2D6DE;
                height: 115vh;
                background-position: bottom;
                background-repeat: no-repeat;
                background-attachment: fixed;
                background-size: cover;
                position: relative;
                min-height: 550px;
            }

            .descricao-box {
                background-color: rgba(255,255,255, 0.8);
                padding: 30px 30px;
                font-size: 20px;
                width: 100%;
                margin: auto;
                text-align: left;
                border-radius: 10px;
            }

            .borda_texto{
            -webkit-text-stroke-width: 2px;
            -webkit-text-stroke-color: #000;
            font-size: 3em; color: dimgrey;
            }

            #countdown {
              position: relative;
              margin: auto;
              margin-top: 100px;
              height: 40px;
              width: 40px;
              text-align: center;
            }

            #countdown-number {
              color: orangered;
              display: inline-block;
              line-height: 40px;
            }

            svg {
              position: absolute;
              top: 0;
              right: 0;
              width: 40px;
              height: 40px;
              transform: rotateY(-180deg) rotateZ(-90deg);
            }

            svg circle {
              stroke-dasharray: 113px;
              stroke-dashoffset: 0px;
              stroke-linecap: round;
              stroke-width: 2px;
              stroke: orangered;
              fill: none;
              animation: countdown 10s linear infinite forwards;
            }

            @keyframes countdown {
              from {
                stroke-dashoffset: 0px;
              }
              to {
                stroke-dashoffset: 113px;
              }
            }

        </style>

</head>

<body onkeydown="teclaEnter();">

    <form id="FORM" name="FORM" method="post" runat="server">
        <input type="hidden" id="hOrigem" name="hOrigem" value="" runat="server" />

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
                /*background: url('img/estudante2.jpg');*/
                background: url('img/Capa.png');
                background-repeat: no-repeat;
                background-position: center;
                overflow: hidden;
                background-size: cover;
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

            #grdCursoDisponivel td.centralizarTH {
                vertical-align: middle;
            }

            .movedown {
                position: absolute;
                opacity: 0;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }

            .modal {
                text-align: center;
                padding: 0 !important;
            }

                .modal:before {
                    content: '';
                    display: inline-block;
                    height: 100%;
                    vertical-align: middle;
                    margin-right: -4px; /* Adjusts for spacing */
                }

            .modal-dialog {
                display: inline-block;
                text-align: left;
                vertical-align: middle;
            }
        </style>

        <header>
        </header>

<%--        <section id="section" class="">
            <div class="container">
                <h1>MODELO 0</h1>
            </div>
        </section>
        <br />
        <br />

        <section id="sectionPrincipal" class="content-wrapper" style="margin: 0">
            <div class="container-fluid">
                <br />
                <div class="cssdivPost" style="display: block">
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <h1 style="font-size: 50px"><strong class="text-danger">ENCONTRE AQUI A SUA SALA</strong></h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <br />
                            <br />
                            <div class="box box-primary" style="height: 800px">

                                <div class="box-header with-border">
                                    <h2 class="text-center text-muted"><strong>INFORMATIVO (<label class="cssPost1"></label>)</strong></h2>
                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em"><strong><i class="fa fa-book margin-r-5 text-orange"></i>Evento</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssEvento1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 class="clearfix" style="line-height: 1em"><strong><i class="fa fa-map-marker margin-r-5 text-red"></i>Local</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssLocal1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em"><strong><i class="fa fa-calendar margin-r-5 text-blue"></i>Data</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssData1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em"><strong><i class="fa fa-clock-o margin-r-5 text-green"></i>Horário</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssHorario1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em"><strong><i class="fa fa-pencil margin-r-5 text-fuchsia"></i>Responsável</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssResponsavel1"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-5">
                            <br />
                            <br />
                            <div class="box box-primary" style="height: 800px">

                                <div class="box-header with-border">
                                    <h2 class="text-center text-muted"><strong>INFORMATIVO (<label id="lblPost2" class="cssPost2"></label>)</strong></h2>
                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em" class="text-orange"><strong><i class="fa fa-book margin-r-5"></i>Evento</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssEvento2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-red"><strong><i class="fa fa-map-marker margin-r-5"></i>Local</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssLocal2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-blue"><strong><i class="fa fa-calendar margin-r-5"></i>Data</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssData2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-green"><strong><i class="fa fa-clock-o margin-r-5"></i>Horário</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssHorario2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em" class="text-fuchsia"><strong><i class="fa fa-pencil margin-r-5"></i>Responsável</strong></h2>

                                        <h2 class="text-muted" style="line-height: 1em">
                                            <label class="cssResponsavel2"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-1"></div>

                    </div>
                </div>

                <div class="cssdivVideo" style="display: none">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <video class="cssVideo" width="1424" height="900" controls loop>
                                <source src="./videos/Pós-Graduação em Gestão da Inovação Tecnológica e Negócio.mp4" type="video/mp4">
                                <source src="./videos/Vídeo institucional do IPT 2014.mp4" type="video/mp4"/>
                                Your browser does not support the video tag.
                            </video>
                        </div>

                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-md-12">
                        <marquee style="background-color: blueviolet; color: white" scrollamount="10" scrolldelay="60"><h1><strong><label class="cssLetreiro"></label></strong></h1></marquee>
                    </div>
                </div>
            </div>

        </section>
        <br />
        <br />

        <section class="main-footer">
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-5">
                    <button id="btnVoltar" runat="server" type="button" class="btn btn-default center-block" onserverclick="btnVoltar_Click">
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
                </div>

                <div class="col-xs-5">
                    <button id="btnConfirmaAtivar" type="button" class="btn btn-success center-block aclick">
                        <i class="fa fa-window-maximize"></i>&nbsp;Full Screen Modelo 1</button>
                </div>
                <div class="col-xs-1">
                </div>
            </div>

        </section>

        <br />
        <br />
        <br />
        <hr />
        <br />
        <br />
        <br />

        <section class="">
            <div class="container">
                <h1>MODELO 1</h1>
            </div>
        </section>
        <br />
        <br />

        <section id="sectionPrincipal_1" class="content-wrapper bg-gray" style="margin: 0">
            <div class="container-fluid">
                <br />
                <div class="cssdivPost" style="display: block">
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <h1 style="font-size: 50px"><strong class="text-red">ENCONTRE AQUI A SUA SALA</strong></h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <br />
                            <br />
                            <div class="box box-primary" style="height: 800px">

                                <div class="box-header with-border">
                                    <img src="./img/ipt_ico.gif" style="width:10%;display:inline-block;margin-top:-20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h2 style="display:inline-block" class="text-center text-black"><strong>INFORMATIVO (<label class="cssPost1"></label>)</strong></h2>
                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em"><strong class="text-red"><i class="fa fa-book margin-r-5"></i>EVENTO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssEvento1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 class="clearfix" style="line-height: 1em"><strong class="text-orange"><i class="fa fa-map-marker margin-r-5"></i>LOCAL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssLocal1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em"><strong class="text-blue"><i class="fa fa-calendar margin-r-5"></i>DATA</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssData1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em"><strong class="text-green"><i class="fa fa-clock-o margin-r-5"></i>HORÁRIO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssHorario1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em"><strong class="text-fuchsia"><i class="fa fa-pencil margin-r-5"></i>RESPONSÁVEL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssResponsavel1"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-5">
                            <br />
                            <br />
                            <div class="box box-primary" style="height: 800px">

                                <div class="box-header with-border">
                                    <img src="./img/ipt_ico.gif" style="width:10%;display:inline-block;margin-top:-20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h2 style="display:inline-block" class="text-center text-black"><strong>INFORMATIVO (<label class="cssPost2"></label>)</strong></h2>
                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em" class="text-red"><strong><i class="fa fa-book margin-r-5"></i>Evento</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssEvento2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-orange"><strong><i class="fa fa-map-marker margin-r-5"></i>Local</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssLocal2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-blue"><strong><i class="fa fa-calendar margin-r-5"></i>Data</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssData2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-green"><strong><i class="fa fa-clock-o margin-r-5"></i>Horário</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssHorario2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em" class="text-fuchsia"><strong><i class="fa fa-pencil margin-r-5"></i>Responsável</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssResponsavel2"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-1"></div>

                    </div>
                </div>

                <div class="cssdivVideo" style="display: none">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <video class="cssVideo" width="1424" height="900" controls autoplay loop>
                                <source src="./videos/Vídeo institucional do IPT 2014.mp4" type="video/mp4"/>
                                <source src="./videos/Pós-Graduação em Gestão da Inovação Tecnológica e Negócio.mp4" type="video/mp4"/>
                                
                                Your browser does not support the video tag.
                            </video>
                        </div>

                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-md-12">
                        <marquee style="background-color: blueviolet; color: white" scrollamount="10" scrolldelay="60"><h1><strong><label class="cssLetreiro">A T E N Ç Ã O - Dia 26/07/2019 (sexta-feira) não haverá expediênte na secretaria devido ao feriado prolongado.</label></strong></h1></marquee>
                    </div>
                </div>
            </div>

        </section>
        <br />
        <br />

        <section class="main-footer">
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-5">
                    <button id="Button1" runat="server" type="button" class="btn btn-default center-block" onserverclick="btnVoltar_Click">
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
                </div>

                <div class="col-xs-5">
                    <button type="button" class="btn btn-success center-block aclick_1">
                        <i class="fa fa-window-maximize"></i>&nbsp;Full Screen</button>
                </div>
                <div class="col-xs-1">
                </div>
            </div>

        </section>


        <br />
        <br />
        <br />
        <hr />
        <br />
        <br />
        <br />

        <section class="">
            <div class="container">
                <h1>MODELO 2</h1>
            </div>
        </section>
        <br />
        <br />

        <section id="sectionPrincipal_2" class="content-wrapper bg-gray" style="margin: 0">
            <div class="container-fluid">
                <br />
                <div class="cssdivPost" style="display: block">
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <h1 style="font-size: 50px"><strong class="text-red piscante">ENCONTRE AQUI A SUA SALA</strong></h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <br />
                            <br />
                            <div class="box box-primary" style="height: 820px">

                                <div class="box-header with-border">
                                    <img src="./img/ipt_ico.gif" style="width:10%;display:inline-block;margin-top:-20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h2 style="display:inline-block" class="text-center text-black"><strong>INFORMATIVO (<label class="cssPost1"></label>)</strong></h2>
                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em;font-size:32px"><strong class="text-red"><i class="fa fa-book margin-r-5"></i>EVENTO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:32px">
                                            <label class="cssEvento1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 class="clearfix" style="line-height: 1em;font-size:32px"><strong class="text-orange"><i class="fa fa-map-marker margin-r-5"></i>LOCAL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:32px">
                                            <label class="cssLocal1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em;font-size:32px"><strong class="text-blue"><i class="fa fa-calendar margin-r-5"></i>DATA</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:32px">
                                            <label class="cssData1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em;font-size:32px"><strong class="text-green"><i class="fa fa-clock-o margin-r-5"></i>HORÁRIO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:32px">
                                            <label class="cssHorario1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em;font-size:32px"><strong class="text-fuchsia"><i class="fa fa-pencil margin-r-5"></i>RESPONSÁVEL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:32px">
                                            <label class="cssResponsavel1"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-6">
                            <br />
                            <br />
                            <div class="box box-primary" style="height: 820px">

                                <div class="box-header with-border">
                                    <img src="./img/ipt_ico.gif" style="width:10%;display:inline-block;margin-top:-20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h2 style="display:inline-block"  class="text-center text-black"><strong>INFORMATIVO (<label class="cssPost2"></label>)</strong></h2>
                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em;font-size:32px" class="text-red"><strong><i class="fa fa-book margin-r-5"></i>EVENTO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:32px">
                                            <label class="cssEvento2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-orange"><strong><i class="fa fa-map-marker margin-r-5"></i>LOCAL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssLocal2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-blue"><strong><i class="fa fa-calendar margin-r-5"></i>DATA</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssData2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em" class="text-green"><strong><i class="fa fa-clock-o margin-r-5"></i>HORÁRIO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssHorario2"></label>
                                        </h2>
                                    </div>

                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em" class="text-fuchsia"><strong><i class="fa fa-pencil margin-r-5"></i>RESPONSÁVEL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em">
                                            <label class="cssResponsavel2"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

                <div class="cssdivVideo" style="display: none">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <video class="cssVideo" width="1424" height="900" controls autoplay loop>
                                <source src="./videos/Pós-Graduação em Gestão da Inovação Tecnológica e Negócio.mp4" type="video/mp4">
                                Your browser does not support the video tag.
                            </video>
                        </div>

                    </div>
                </div>


                <div class="row">
                    <div class="col-md-12">
                        <marquee style="background-color: blueviolet; color: white" scrollamount="10" scrolldelay="60"><h1><strong><label class="cssLetreiro">A T E N Ç Ã O - Dia 26/07/2019 (sexta-feira) não haverá expediênte na secretaria devido ao feriado prolongado.</label></strong></h1></marquee>
                    </div>
                </div>
            </div>

        </section>
        <br />
        <br />

        <section class="main-footer">
            <div class="row">
                
                <div class="col-xs-6">
                    <button id="Button2" runat="server" type="button" class="btn btn-default center-block" onserverclick="btnVoltar_Click">
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
                </div>

                <div class="col-xs-6">
                    <button type="button" class="btn btn-success center-block aclick_2">
                        <i class="fa fa-window-maximize"></i>&nbsp;Full Screen</button>
                </div>

            </div>

        </section>

        <br />
        <br />
        <br />
        <hr />
        <br />
        <br />
        <br />--%>

        <%--<section class="hidden">
            <div class="container">
                <h1><i class="fa fa-television text-red fa-lg"></i> Modelo 3</h1>
            </div>
        </section>
        <br />
        <br />

        <section id="sectionPrincipal_3" class="content-wrapper bg-gray hidden" style="margin: 0">
            <div class="container-fluid">
                <br />
                <div class="cssdivPost" style="display: block">
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <h1 style="font-size: 50px"><strong class="text-red">ENCONTRE AQUI A SUA SALA</strong></h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <br />
                            <br />
                            <div class="box box-primary" style="height: 820px">

                                <div class="box-header with-border">
                                    <img src="./img/ipt_ico.gif" style="width:8%;display:inline-block;margin-top:-15px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</img><h2 style="display:inline-block; font-size:40px" class="text-center text-black"><strong>INFORMATIVO (<label class="cssPost1"></label>)</strong></h2>
                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em;font-size:40px"><strong class="text-red"><i class="fa fa-book margin-r-5"></i>EVENTO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px">
                                            <label class="cssEvento1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 class="clearfix" style="line-height: 1em;font-size:40px; display:inline-block"><strong class="text-orange"><i class="fa fa-map-marker margin-r-5"></i>LOCAL </strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px; display:inline-block">
                                            &nbsp;&nbsp;&nbsp;<label class="cssLocal1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em;font-size:40px; display:inline-block"><strong class="text-blue"><i class="fa fa-calendar margin-r-5"></i>DATA</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px; display:inline-block">
                                            &nbsp;&nbsp;&nbsp;<label class="cssData1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em;font-size:40px; display:inline-block"><strong class="text-green"><i class="fa fa-clock-o margin-r-5"></i>HORÁRIO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px; display:inline-block">
                                            &nbsp;&nbsp;&nbsp;<label class="cssHorario1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em;font-size:40px"><strong class="text-fuchsia"><i class="fa fa-pencil margin-r-5"></i>RESPONSÁVEL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px">
                                            <label class="cssResponsavel1"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

                <div class="cssdivVideo" style="display: none">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <video id="idVideo2" width="1424" height="900">

                            </video>
                        </div>

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">

                        <marquee style="background-color: blueviolet; color: white" scrollamount="10" scrolldelay="60"><h1><strong><label class="cssLetreiro"></label></strong></h1></marquee>
                    </div>
                </div>
            </div>

        </section>
        <br />
        <br />

        <section class="main-footer hidden">
            <!-- To the right -->
            <div class="row">
                
                <div class="col-xs-6">
                    <button id="Button3" runat="server" type="button" class="btn btn-default center-block" onserverclick="btnVoltar_Click">
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
                </div>

                <div class="col-xs-6">
                    <button type="button" class="btn btn-success center-block aclick_3">
                        <i class="fa fa-window-maximize"></i>&nbsp;Full Screen</button>
                </div>

            </div>

        </section>


        <br />
        <br />
        <br />
        <hr />
        <br />
        <br />--%>


        <section class="">
            <div class="container">
                <h1><i class="fa fa-television text-red fa-lg"></i> Apresentação Monitor</h1>
            </div>
        </section>
        <br />
        <br />

        <section id="sectionPrincipal_4" class="container-fluid cssCabecalho " style="margin: 0">
            <div class="container-fluid">
                <br />
                <div class="cssdivPost" style="display: block">
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <h1 style="font-size: 20px"><strong class="borda_texto" style="color:white">ENCONTRE AQUI A SUA SALA</strong></h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <br />
                            <div class="box box-primary descricao-box" style="height: 820px">

                                <div class="box-header with-border" style="margin-top:-15px">
                                    <div class="row"  style="margin-top:-10px">
                                        <div class="col-xs-2 pull-left">
                                            <img src="./img/ipt_ico_transparente.gif" style="width:60%"/>
                                        </div>
                                        <div class="col-xs-8 text-center">
                                            <h2 style="font-size:40px" class="text-center center-block text-black"><strong>INFORMATIVO (<label class="cssPost1"></label>)</strong></h2>
                                        </div>
                                        <div id="divRegressiva" class="col-xs-2 pull-right" style="margin-top:-80px">
                                            <div id="countdown">
                                                <div id="countdown-number"></div>
                                                <svg>
                                                <circle r="18" cx="20" cy="20"></circle>
                                                </svg>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="box-body">
                                    <div style="height: 130px">
                                        <h2 style="line-height: 1em;font-size:40px"><strong class="text-red"><i class="fa fa-book margin-r-5"></i>EVENTO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px">
                                            <label class="cssEvento1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 class="clearfix" style="line-height: 1em;font-size:40px; display:inline-block"><strong class="text-orange"><i class="fa fa-map-marker margin-r-5"></i>LOCAL </strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px; display:inline-block">
                                            &nbsp;&nbsp;&nbsp;<label class="cssLocal1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em;font-size:40px; display:inline-block"><strong class="text-blue"><i class="fa fa-calendar margin-r-5"></i>DATA</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px; display:inline-block">
                                            &nbsp;&nbsp;&nbsp;<label class="cssData1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div>
                                        <h2 style="line-height: 1em;font-size:40px; display:inline-block"><strong class="text-green"><i class="fa fa-clock-o margin-r-5"></i>HORÁRIO</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px; display:inline-block">
                                            &nbsp;&nbsp;&nbsp;<label class="cssHorario1"></label>
                                        </h2>
                                    </div>
                                    <hr />

                                    <div style="height: 100px">
                                        <h2 style="line-height: 1em;font-size:40px"><strong class="text-fuchsia"><i class="fa fa-pencil margin-r-5"></i>RESPONSÁVEL</strong></h2>

                                        <h2 class="text-black" style="line-height: 1em;font-size:40px">
                                            <label class="cssResponsavel1"></label>
                                        </h2>
                                    </div>
                                </div>

                            </div>
                            
                        </div>

                    </div>
                </div>
                <div class="cssdivVideo" style="display: none">
                    <div class="row" >
                        <div class="col-md-12 text-center" >

                            <video id="idVideo" width="1700" height="900" controls ><%--controls autoplay--%>
<%--                                <source src="https://www.w3schools.com/html/mov_bbb.mp4" type="video/mp4"/>
                                <source src="./videos/Vídeo institucional do IPT 2014.mp4" type="video/mp4"/>--%>
                                <%--<source src="./videos/Pós-Graduação em Gestão da Inovação Tecnológica e Negócio.mp4" type="video/mp4"/>--%>
                                <%--Your browser does not support the video tag.--%>
                            </video>

                        </div>

                    </div>
                </div>

                <br /><br />
                <div class="row">
                    <div class="col-md-12">
                        <marquee style="background-color: blueviolet; color: white" scrollamount="10" scrolldelay="60"><h1><strong><label class="cssLetreiro">A T E N Ç Ã O - Dia 26/07/2019 (sexta-feira) não haverá expediênte na secretaria devido ao feriado prolongado.</label></strong></h1></marquee>
                    </div>
                </div>
            </div>

        </section>
        <br />
        <br />

        <section class="main-footer">
            <!-- To the right -->
            <div class="row">
                
                <div class="col-xs-6">
                    <button id="Button1" runat="server" type="button" class="btn btn-default center-block" onserverclick="btnVoltar_Click">
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
                </div>

                <div class="col-xs-6">
                    <button type="button" class="btn btn-success center-block aclick_4">
                        <i class="fa fa-window-maximize"></i>&nbsp;Full Screen</button>
                </div>

            </div>

        </section>


        <!-- jQuery -->
        <%--<script src="../bower_components/jquery/dist/jquery.min.js"></script>--%>
        <script src="Scripts/jquery-2.1.0.min.js"></script>
        <%-- <script src="Scripts/jquery-ui.min.js"></script>--%>
        <script src="Scripts/jquery-ui.min.js"></script>

        <script src="https://cdnjs.cloudflare.com/ajax/libs/screenfull.js/4.2.0/screenfull.min.js"></script>

        <%--<script src="https://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>--%>



        <!-- Bootstrap Core JavaScript -->
        <script src="Scripts/bootstrap.min.js"></script>

        <!-- Metis Menu Plugin JavaScript -->
        <script src="Scripts/metisMenu.min.js"></script>

        <!-- Custom Theme JavaScript -->
        <script src="Scripts/sb-admin-2.js"></script>

        <script src="Scripts/wow.js"></script>

        <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false" >
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12 text-center">
                                Processando...
                                <br />
                                Por favor, aguarde.<br />
                                <br />
                                <img src="img/loader.gif" width="42" height="42" alt="" />
                                <a id="divbtnFechaProcessando" href="#" data-dismiss="modal" class="btn hidden">Fechar</a>
                            </div>
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
                        <h4 class="modal-title" id="CabecalhoMsg">
                            <asp:Label runat="server" ID="lblTituloMensagem" Text="" /></h4>
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
            var countdownNumberEl = document.getElementById('countdown-number');
            var countdown = 10;

            countdownNumberEl.textContent = countdown;

            setInterval(function () {
                countdown = --countdown <= 0 ? 10 : countdown;
                countdownNumberEl.textContent = countdown;
            }, 1000);

            new WOW().init();
            var qIdPost = 0;
            var retorno;
            var Interval = 0;

            var playerVideo = document.getElementById("idVideo");
            var current = 0;
            var videos = [];
            var qDataVideo;

            $(document).ready(function () {
                fPreencheMonitor();
            });

            setInterval("fPreencheMonitor()", 100000);

            function fTrocaPost() {
                //alert('oi');
                //document.getElementById('lblPost1').innerHTML = retorno[qIdPost].P1;
                //document.getElementById('lblEvento1').innerHTML = retorno[qIdPost].P2;
                //document.getElementById('lblLocal1').innerHTML = retorno[qIdPost].P3;
                //document.getElementById('lblData1').innerHTML = retorno[qIdPost].P4;
                //document.getElementById('lblHorario1').innerHTML = retorno[qIdPost].P5;
                //document.getElementById('lblResponsavel1').innerHTML = retorno[qIdPost].P6;

                $('.cssPost1').html(retorno[qIdPost].P1);
                $('.cssEvento1').html(retorno[qIdPost].P2);
                $('.cssLocal1').html(retorno[qIdPost].P3);
                $('.cssData1').html(retorno[qIdPost].P4);
                $('.cssHorario1').html(retorno[qIdPost].P5);
                $('.cssResponsavel1').html(retorno[qIdPost].P6);

                qIdPost = qIdPost + 1;
                if (retorno.length == qIdPost || retorno.length < qIdPost) {
                    qIdPost = 0;
                }

                //alert(qIdPost);

                //document.getElementById('lblPost2').innerHTML = retorno[qIdPost].P1;
                //document.getElementById('lblEvento2').innerHTML = retorno[qIdPost].P2;
                //document.getElementById('lblLocal2').innerHTML = retorno[qIdPost].P3;
                //document.getElementById('lblData2').innerHTML = retorno[qIdPost].P4;
                //document.getElementById('lblHorario2').innerHTML = retorno[qIdPost].P5;
                //document.getElementById('lblResponsavel2').innerHTML = retorno[qIdPost].P6;

                //$('.cssPost2').html(retorno[qIdPost].P1);
                //$('.cssEvento2').html(retorno[qIdPost].P2);
                //$('.cssLocal2').html(retorno[qIdPost].P3);
                //$('.cssData2').html(retorno[qIdPost].P4);
                //$('.cssHorario2').html(retorno[qIdPost].P5);
                //$('.cssResponsavel2').html(retorno[qIdPost].P6);

                //qIdPost = qIdPost + 1;
                //if (retorno.length == qIdPost) {
                //    qIdPost = 0;
                //}
            }


            //============================================================================

            function fPreencheMonitor() {

                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fPreencheMonitor",
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            $(".cssdivPost").css("display", "none");
                            $(".cssdivVideo").css("display", "block");
                            //document.getElementById('divPost').style.display = 'none';
                            //document.getElementById('divVideo').style.display = 'block';

                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Disciplina';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração da Disciplina: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            $(".cssLetreiro").html(json[0].P7);
                            if (json[0].P0 == "avisos") {
                                $("#sectionPrincipal_4").removeClass("bg-gray");
                                $('#sectionPrincipal_4').addClass('cssCabecalho');
                                retorno = json;
                                if (json.length > 1) {
                                    document.getElementById("divRegressiva").style.display = "block";
                                }
                                else {
                                    document.getElementById("divRegressiva").style.display = "none";
                                }
                                qIdPost = 0;
                                fTrocaPost();
                                clearInterval(Interval);
                                Interval = setInterval("fTrocaPost()", 10000);
                                $(".cssdivPost").css("display", "block");
                                $(".cssdivVideo").css("display", "none");
                                //document.getElementById('divPost').style.display = 'block';
                                //document.getElementById('divVideo').style.display = 'none';
                                //document.getElementById('idVideo').pause();
                                $('.cssVideo').trigger('pause');
                                if (playerVideo != null) {
                                    playerVideo.pause();
                                    playerVideo = null;
                                }
                                qDataVideo = "";
                            }
                            else {
                                $("#sectionPrincipal_4").removeClass("cssCabecalho");
                                $('#sectionPrincipal_4').addClass('bg-gray');
                                $(".cssdivPost").css("display", "none");
                                $(".cssdivVideo").css("display", "block");
                                clearInterval(Interval);
                                //document.getElementById('divPost').style.display = 'none';
                                //document.getElementById('divVideo').style.display = 'block';
                                //document.getElementById('idVideo').play();
                                //$('.cssVideo').trigger('play');

                                if (json[0].P2 != qDataVideo) {
                                    qDataVideo = json[0].P2;

                                    playerVideo = document.getElementById("idVideo");

                                    videos = null;
                                    videos = [];

                                    for (var i = 0; i < json.length; i++) {
                                        videos.push(json[i].P1);
                                    }
                                    //videos.push("https://www.w3schools.com/html/movie.mp4");
                                    //videos.push("https://www.w3schools.com/html/mov_bbb.mp4");
                                    current = 0;

                                    function nextVideo() {
                                        playerVideo.src = videos[current];
                                        current++;
                                        playerVideo.play();
                                        if (current >= videos.length) {
                                            current = 0;
                                        }
                                    }

                                    playerVideo.addEventListener("ended", nextVideo);

                                    nextVideo();
                                }
                            }

                        }
                    },
                    error: function (xhr) {
                        alert("Houve um erro na Alteração da Disciplina. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando()
                    },
                    failure: function () {
                        alert("Houve um erro na Alteração da Disciplina. Por favor tente novamente!");
                        fFechaProcessando()
                    }
                });
        }

        //============================================================================




        </script>

    </form>

</body>

<script>

    //const el = document.getElementById('sectionPrincipal');

    //const el_1 = document.getElementById('sectionPrincipal_1');

    //const el_2 = document.getElementById('sectionPrincipal_2');

    //const el_3 = document.getElementById('sectionPrincipal_3');

    const el_4 = document.getElementById('sectionPrincipal_4');


    //$('.aclick').on('click', () => {
    //    if (screenfull.enabled) {
    //        screenfull.request(el);
    //    }
    //});

    //$('.aclick_1').on('click', () => {
    //    if (screenfull.enabled) {
    //        screenfull.request(el_1);
    //    }
    //});

    //$('.aclick_2').on('click', () => {
    //    if (screenfull.enabled) {
    //        screenfull.request(el_2);
    //    }
    //});

    $('.aclick_3').on('click', () => {
        if (screenfull.enabled) {
            screenfull.request(el_3);
        }
    });


    $('.aclick_4').on('click', () => {
        if (screenfull.enabled) {
            screenfull.request(el_4);
        }
    });


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

    function ShowProgress() {
        $('#myModal').modal();
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


    //});

</script>
</html>

