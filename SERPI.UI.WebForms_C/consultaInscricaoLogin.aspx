<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="consultaInscricaoLogin.aspx.cs" Inherits="SERPI.UI.WebForms_C.consultaInscricaoLogin" %>

<html lang="pt-br" class="translated-ltr">
    <head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Ensino Tecnológico</title>

    <link rel="SHORTCUT ICON" href="img/ipt_ico.gif"" type="image/x-icon">

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
        <input type="hidden" id ="hOrigem"  name="hOrigem" value="" runat="server" />

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
                position:absolute;
                opacity:0;
                top:0;
                left:0;
                width:100%;
                height:100%;
            }
        </style>

        <header>

        </header>

        <section id="section" class="banner" >
            <div class ="container">
                <div class="hidden-sm hidden-xs">
                    <br /><br />
                </div>
                <div class ="row">
                    <div class ="hidden-lg hidden-md">
                       <br />
                    </div>

                    <div class="col-md-8 pull-left wow fadeInLeft animated animated"  data-wow-duration="3s"  style="visibility: visible; animation-name: fadeInLeft;">
                        <h2 style="line-height: 1em;" ><font color="#FFFFFF">Ensino Tecnológico - IPT</font></h2>

                        <h3 style="line-height: 1em;" ><font color="#FFFFFF">Comece a Mudar o Seu  Futuro</font></h3>
                        <div class ="row ">
                            <div class ="col-md-9">
                                <h4 style="line-height: 1.2em;"><font color="#FFFFFF"><p>Qualquer carreira de sucesso começa com boa educação. Juntamente conosco, você terá um conhecimento mais profundo dos assuntos que serão especialmente úteis para você subir na carreira.</p></font></h4>
                            </div>
                        </div>
                    </div>
                    <div class ="col-md-1 ">
                    </div>

                </div>
                <br /><br />
                <div class="hidden-sm hidden-xs">
                    <br /><br />
                </div>
            </div>
        </section>
        <br /><br />

        <section >
            <div class="container">
                <div class ="row">
                    <div class ="col-md-12">
                        <h2 style="line-height: 1em"><font color="#7e7e7e">Acesso à Consulta de Inscrições</font></h2>
                    </div>
                </div>
                <br />

                <div class ="row">
                    <div class ="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <b><i class="fa fa-key"></i>&nbsp;Login</b><br />
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <span>Digite a Senha</span><br />
                                        <input runat="server" class="form-control input-sm" id="txtSenha" type="password" value="" maxlength="20" />
                                    </div>
                                </div>
                                <br />
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-md-6">
                                        <button type="button" id="btnConfirma" runat="server" class="btn btn-success pull-right" onserverclick="btnConfirma_Click">
                                        <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                                    </div>
                                </div>
                                <br />
                            </div>

                        </div>
                    </div>
                </div>

            </div>
            
        </section>
        <br /><br />

        <footer class="main-footer">
                <!-- To the right -->
            <div class="row">
                <div class="col-xs-1">

                </div>
                <div class="col-xs-10">
                    
                    <!-- Default to the left -->
                    <strong><a href="https://www.ipt.br">Instituto de Pesquisas Tecnológicas - ipt - &copy; 2020</a></strong> 


                </div>
            </div>
                
        </footer>

        <!-- Modal para Logar no sistema -->
        <div class="modal fade" id="divModalFalhaLogin" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-danger">
                        <h4 class="modal-title"><i class="fa fa-close"></i> Erro</h4>
                    </div>
                    <div class="modal-body">

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-6">
                                    <span>Senha não confere!</span><br />
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-xs-6">
                                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">
                                <i class="fa fa-close"></i>&nbsp;Fechar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>


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
        
        <script>

            function fModalFalhaLogin() {
                $('#divModalFalhaLogin').modal();
                //alert("Hello world");
            }

            new WOW().init();

            function teclaEnter() {
             if (event.keyCode == "13") {
                document.getElementById("<%=btnConfirma.ClientID%>").click();
             }
         }

         function register(e) {
             if (!e) e = window.event;
             var keyInfo = e.keyCode;
         }

        </script>

    </form> 
         
</body>

</html>
