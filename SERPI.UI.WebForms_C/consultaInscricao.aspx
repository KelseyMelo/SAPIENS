<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="consultaInscricao.aspx.cs" Inherits="SERPI.UI.WebForms_C.consultaInscricao" %>

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
                        <h2 style="line-height: 1em"><font color="#7e7e7e">Consulta de Inscrições</font></h2>
                    </div>
                </div>

                <div class="row">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b><i class="fa fa-filter"></i>&nbsp;Filtro</b>&nbsp;&nbsp;&nbsp;<span id="divProcessando" style="display:none"><b><i class="fa fa-spinner fa-pulse"></i>&nbsp;processando...</b></span> <br />
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-md-3">
                                    <span>Data Início</span><br />
                                    <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtDataInicio" type="date" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span>Data Fim</span><br />
                                    <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtDataFim" type="date" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4" runat="server" id="divOrigem">
                                    <span>Origem</span><br />
                                    <div class="row center-block btn-default form-group">
                                        <div class="col-md-4">
                                        <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoTodos" runat="server"/>
                                        &nbsp;
                                        <label class="opt" for="<%=optSituacaoTodos.ClientID %>">Todos</label>
                                        </div>
                                
                                        <div class="col-md-4">                    
                                        <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoPhorte" runat="server" Checked="true"/>
                                        &nbsp;
                                        <label class="opt" for="<%=optSituacaoPhorte.ClientID %>">Phorte</label>
                                        </div>

                                        <div class="col-md-4">
                                        <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoIpt" runat="server" />
                                        &nbsp;
                                        <label class="opt" for="<%=optSituacaoIpt.ClientID %>">IPT</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-1">
                                    <%--<span>&nbsp;</span><br />--%>

                                    <div class="hidden-xs hidden-sm">
                                        <span>&nbsp;</span><br />
                                    </div>
                            
                                    <a id="aBntPerquisa" runat ="server" onclick="ShowProgress()" onserverclick="btnPerquisa_Click"  href="#" class ="btn btn-success pull-right"><i class="fa fa-check"></i><span>&nbsp;OK</span></a> 
                                </div>

                                <div class="col-md-1">
                                    <%--<span>&nbsp;</span><br />--%>

                                    <div class="hidden-xs hidden-sm">
                                        <span>&nbsp;</span><br />
                                    </div>
                            
                                    <a id="aBntSair" runat ="server" onclick="ShowProgress()" onserverclick="btnSair_Click"  href="#" class ="btn btn-default pull-right"><i class="fa fa-sign-out"></i><span>&nbsp;Sair</span></a> 
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class ="row">
                    <div class ="col-md-12">
                        <div class="grid-content">
                            <div runat="server" id="msgSemResultados" visible="false">
                                <div class="alert bg-gray">
                                    <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum resultado disponível." />
                                </div>
                            </div>
                            <div class="table-responsive ">
                                <asp:GridView ID="grdCandidato" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                    AllowPaging="True" PageSize="1000000" AllowSorting="true" 
                                    SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                    <Columns>

                                        <asp:BoundField DataField="P0" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                        <asp:BoundField DataField="P1" HeaderText="CPF" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                        <asp:BoundField DataField="P2" HeaderText="Data Inscrição" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                        <asp:BoundField DataField="P11" HeaderText="Curso"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                        <asp:BoundField DataField="P3" HeaderText="Endereço" ItemStyle-CssClass="centralizarTH hidden" HeaderStyle-CssClass="centralizarTH hidden"/>

                                        <asp:BoundField DataField="P4" HeaderText="Bairro" ItemStyle-CssClass="centralizarTH hidden" HeaderStyle-CssClass="centralizarTH hidden"/>

                                        <asp:BoundField DataField="P5" HeaderText="Cidade" ItemStyle-CssClass="centralizarTH hidden" HeaderStyle-CssClass="centralizarTH hidden"/>

                                        <asp:BoundField DataField="P6" HeaderText="Estado"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH hidden" HeaderStyle-CssClass="centralizarTH hidden"/>

                                        <asp:BoundField DataField="P7" HeaderText="CEP"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH hidden" HeaderStyle-CssClass="centralizarTH hidden"/>

                                        <asp:BoundField DataField="P8" HeaderText="Celular"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                        <asp:BoundField DataField="P12" HeaderText="Email"  ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                        <asp:BoundField DataField="P9" HeaderText="Situação Inscrição"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HtmlEncode="false"/>

                                        <asp:BoundField DataField="P10" HeaderText="Origem"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                    </Columns>

                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                </asp:GridView>

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
        <div class="modal fade" id="divModalLogin" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div id="divCabecAtiva" class="modal-header bg-primary">
                        <h4 class="modal-title"><i class="fa fa-key"></i> Login</h4>
                    </div>
                    <div class="modal-body">

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-6">
                                    <span>Digite a senha</span><br />
                                    <input runat="server" class="form-control input-sm" id="txtSenha" type="text" maxlength="20" />
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-xs-6">
                                <button type="button" class="btn btn-default pull-left" data-dismiss="modal">
                                <i class="fa fa-close"></i>&nbsp;Fechar</button>
                            </div>
                            <div class="col-xs-6">
                                <button type="button" id="btnConfirma" runat="server" class="btn btn-success pull-right" onserverclick="btnPerquisa_Click">
                                <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

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

            function fModalLogin() {
                $('#divModalLogin').modal();
                //alert("Hello world");
            }

            function fModalFalhaLogin() {
                $('#divModalFalhaLogin').modal();
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
                document.getElementById("<%=aBntPerquisa.ClientID%>").click();
             }
         }

         function register(e) {
             if (!e) e = window.event;
             var keyInfo = e.keyCode;
         }

         function ShowProgress() {
             document.getElementById('divProcessando').style.display = "block";
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