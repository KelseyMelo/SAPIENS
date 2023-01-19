<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fichaInscricao.aspx.cs" Inherits="SERPI.UI.WebForms_C.fichaInscricao" %>

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
        
        <style>
            .banner {
                /*background: url('img/estudante2.jpg');*/
                background: url('img/Capa.png');
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

        <section >
            <div class="container">
                <div class ="row">
                    <div class ="col-md-12">
                        <h2 style="line-height: 1em"><font color="#7e7e7e">Ficha de Inscrição</font></h2>
                    </div>
                </div>
                <br />

                <div class ="row">
                    <div class ="col-md-12">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-pencil"></i>&nbsp;Dados do Curso</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-2">
                                            <span>Tipo do Curso</span><br />
                                            <input runat="server" class="form-control input-sm" id="txtTipoCurso" type="text" value="" readonly="true" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-6">
                                            <span>Curso</span><br />
                                            <input runat="server" class="form-control input-sm" id="txtNomeCurso" type="text" value="" readonly="true" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div id="divAreaConcentracao" runat="server" class="col-md-4">
                                            <span>Área de Concentração </span><span class="text-danger"><strong>*</strong></span><br />
                                            <asp:DropDownList runat="server" ID="ddlAreaConcentracao" ClientIDMode="Static" class="form-control input-sm" AutoPostBack="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                </div>

                            </div>

                        </div>
                        <br />

                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-user"></i>&nbsp;Dados Pessoais</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <span>CPF</span><span class="text-danger"><strong> *</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtCPFAluno" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Nome </span><span class="text-danger"><strong>*</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtNomeAluno" type="text" value="" maxlength="350" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Data Nasc. </span><span class="text-danger"><strong>*</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtNascimentoAluno" type="date" value="" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Sexo </span><span class="text-danger"><strong>*</strong></span><br />
                                            <asp:DropDownList runat="server" ID="ddlSexoAluno" ClientIDMode="Static" class="form-control input-sm" AutoPostBack="false">
                                                <asp:ListItem Text="Masculino" Value="M" />
                                                <asp:ListItem Text="Feminino" Value="F" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2">
                                            <span>CEP</span><span class="text-danger"><strong> *</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtCEPAluno" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Endereço </span><span class="text-danger"><strong>*</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtEnderecoAluno" type="text" value="" maxlength="350" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Número </span><span class="text-danger"><strong>*</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtEnderecoNumeroAluno" type="text" value="" maxlength="10" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Complemento </span><br />
                                            <input runat="server" class="form-control input-sm" id="txtEnderecoComplementoAluno" type="text" value="" maxlength="20" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2">
                                            <span>Bairro</span><span class="text-danger"><strong> *</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtBairroAluno" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Cidade </span><span class="text-danger"><strong>*</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtCidadeAluno" type="text" value="" maxlength="350" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Estado </span><span class="text-danger"><strong>*</strong></span><br />
                                            <asp:DropDownList runat="server" ID="ddlEstado" ClientIDMode="Static" class="form-control input-sm" AutoPostBack="false">
                                                
                                            </asp:DropDownList>



                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Email </span><span class="text-danger"><strong>*</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtEmailAluno" type="text" value="" maxlength="150" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-3">
                                            <table align="center" width="100%" cellpadding="0" cellspacing="0" border="0"> 
                                                <tr>
                                                    <td><span style="font-size:14px">Rg </span><span class="text-danger"><strong>*</strong></span><br /><input runat="server" class="form-control input-sm" id="txtRg" type="text" value="" onkeypress="return fValidaRG(event)" /></td>
                                                    <td><span style="font-size:14px">Dígito </span><br /><input runat="server" class="form-control input-sm" id="txtDigitoRg" type="text" onkeyup="this.value = this.value.toUpperCase()" value="" maxlength="1" size="1" onkeypress="return fValidaDigito(event)" /></td>
                                                </tr> 
                                            </table>

                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span>Telefone</span><br />
                                            <input runat="server" class="form-control input-sm" id="txtTelefone" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span>Celular </span><span class="text-danger"><strong>*</strong></span><br />
                                            <input runat="server" class="form-control input-sm" id="txtCelular" type="text" value=""/>
                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>
                        <br />

                        <div class="row" id="divPesquisa" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-edit"></i>&nbsp;Como tomou conhecimento do Curso</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optIndEmpresa" value="Indicação - EMPRESA" onMouseOver="this.style.cursor='pointer'"> Indicação - EMPRESA </label>
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optIndProfessor" value="Indicação - PROFESSOR IPT" onMouseOver="this.style.cursor='pointer'"> Indicação - PROFESSOR IPT </label>
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optIndAluno" value="Indicação - ALUNO IPT" onMouseOver="this.style.cursor='pointer'"> Indicação - ALUNO IPT </label>

                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optIndExAluno" value="Indicação - EX-ALUNO IPT" onMouseOver="this.style.cursor='pointer'"> Indicação - EX-ALUNO IPT </label>
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optBuscaINternet" value="Busca Internet" onMouseOver="this.style.cursor='pointer'"> Busca Internet </label>
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optEmailIPT" value="Linkedin" onMouseOver="this.style.cursor='pointer'"> E-mail encaminhado pelo IPT </label>

                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optFacebook" value="Facebook" onMouseOver="this.style.cursor='pointer'"> Facebook </label>
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optTwitter" value="Twitter" onMouseOver="this.style.cursor='pointer'"> Twitter </label>
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optLinkedin" value="Linkedin" onMouseOver="this.style.cursor='pointer'"> Linkedin </label>
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <label class="radio" style="font-weight:normal !important" onMouseOver="this.style.cursor='pointer'"> <input type="radio" name="grupoPesquisa" runat="server" id="optOutros" value="Outros" onMouseOver="this.style.cursor='pointer'"> Outros </label>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-6">
                                            <span>Qual(is) outro(s)?</span><br />
                                            <input runat="server" class="form-control input-sm" id="txtOutros" type="text" value="" maxlength="1000" />
                                        </div>
                                    </div>
                                    
                                </div>

                            </div>

                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-2 pull-left">
                                <button type="button" runat="server" id="btnModalSegundaVia" class="btn btn-success" onclick="fAbreModalSegundaVia()">
                                    <i class="fa fa-print"></i>&nbsp;Segunda Via Boleto</button>
                            </div>
                            <div class="col-md-2 pull-right">
                                <button type="button" runat="server" id="btnEnviarInscricao" class="btn btn-primary" href="#" onclick="" onserverclick="btnEnviarInscricao_Click">
                                    <i class="fa fa-check"></i>&nbsp;Enviar</button>
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
                    <strong><a href="https://www.ipt.br">Instituto de Pesquisas Tecnológicas - ipt - &copy; 2019</a></strong> 


                </div>
            </div>
                
        </footer>

        <!-- Modal para Confirmação de Inscrição -->
        <div class="modal fade" id="divModalConfirmacaoInscricao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-success">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><i class="fa fa-check-circle-o"></i> Confirmação de Inscrição</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <span>Sua inscrição foi realizada com sucesso.</span><br /><br />
                                <span>Para emitir boleto bancário com o valor da taxa de inscrição clique no botão abaixo.</span><br />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-3">

                            </div>
                            <div class="col-md-6 center-block text-center">
                                <button type="button" id="btnEmitirBoleto" class="btn btn-success center-block text-center" href="#" onclick="fEmitirBoleto()" >
                                        <i class="fa fa-print"></i>&nbsp;Emitir Boleto</button>
                            </div>
                            <div class="col-md-3">

                            </div>
                        </div>
                        <br />

                        <hr />

                        <div class="row">
                            <div class="col-md-12">
                                <div id="divProvaComTaxa" runat="server">
                                    <span><strong>Local do processo seletivo:</strong></span><br /><br />

                                    <span>IPT – Instituto de Pesquisas Tecnológicas do Est. de SP.</span><br />
                                    <span>Av. Professor Almeida Prado, 532 - Butantã – S. Paulo – SP.</span><br />
                                    <span>Prédio 56.</span><br /><br />

                                    <span><strong>Dia da Prova:</strong> <asp:Label ID="lblDiaProvaComTaxa" runat="server" Text="Label"></asp:Label> </span><br /><br />
                                    <span>O Aluno deverá chegar com no mínimo 30 minutos de antecedência para verificar a sala onde será aplicada a prova.</span><br />
                                    <span>É proibido qualquer tipo de consulta a materiais.</span><br /><br />
                                </div>

                                <span>Enviamos essas informações para o email cadastrado.</span><br />
                            </div>
                        </div>
                    </div>
                    
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <!-- Modal para Confirmação de Inscrição Sem Taxa -->
        <div class="modal fade" id="divModalConfirmacaoInscricaoSemTaxa" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-success">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><i class="fa fa-check-circle-o"></i> Confirmação de Inscrição</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <span>Sua inscrição foi realizada com sucesso.</span><br /><br />
                            </div>
                        </div>
                        <br />

                        <hr />

                        <div class="row">
                            <div class="col-md-12">
                                <div id="divProvaSemTaxa" runat="server">
                                    <span><strong>Local do processo seletivo:</strong></span><br /><br />

                                    <span>IPT – Instituto de Pesquisas Tecnológicas do Est. de SP.</span><br />
                                    <span>Av. Professor Almeida Prado, 532 - Butantã – S. Paulo – SP.</span><br />
                                    <span>Prédio 56.</span><br /><br />

                                    <span><strong>Dia da Prova:</strong> <asp:Label ID="lblDiaProvaSemTaxa" runat="server" Text="Label"></asp:Label> </span><br /><br />
                                    <span>O Aluno deverá chegar com no mínimo 30 minutos de antecedência para verificar a sala onde será aplicada a prova.</span><br />
                                    <span>É proibido qualquer tipo de consulta a materiais.</span><br /><br />
                                </div>

                                <span>Enviamos essas informações para o email cadastrado.</span><br />
                            </div>
                        </div>
                    </div>
                    
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <!-- Modal para Segunda Via -->
        <div class="modal fade" id="divModalSegundaVia" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-success">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><i class="fa fa-print"></i> Emissão de Segunda Via de Boleto</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <span>Informe o CPF</span><br />
                                <input runat="server" class="form-control input-sm" id="txtCPFSegundaVia" type="text" value="" />
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
                                <div class="col-md-2 pull-right">
                                    <button type="button" runat="server" id="btnSegundaVia" class="btn btn-success pull-right" href="#" onclick="" onserverclick="btnSegundaVia_Click"><%--onserverclick="btnEnviarInscricao_Click"--%>
                                        <i class="fa fa-check"></i>&nbsp;Enviar</button>
                                </div>
                            </div>
                        </div>

                        
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>



        <!-- Modal para mensagens diversas -->
    <div class="modal fade" id="divMensagemModal" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="divCabecalho" class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="CabecalhoMsg">
                        <asp:Label runat="server" ID="lblTituloMensagem" Text="" /></h4>
                </div>
                <div id="CorpoMsg" class="modal-body">
                    <asp:Label runat="server" ID="lblMensagem" Text="" />

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

        <script src="Scripts/jquery.mask.min.js"></script>
        
        <script>

            function fAbreModalSegundaVia() {
                $('#divModalSegundaVia').modal();
            }

            function fAbreModalConfirmacaoInscricao() {
                $('#divModalConfirmacaoInscricao').modal();
            }

            function fAbreModalConfirmacaoInscricaoSemTaxa() {
                $('#divModalConfirmacaoInscricaoSemTaxa').modal();
            }

            function fValidaRG(qTecla) {
                var charCode = (qTecla.which) ? qTecla.which : qTecla.keyCode;
                if ((charCode >= 48 && charCode <= 57) || charCode == 46)
                {
                    //alert('válido: ' + charCode);
                    return true
                }
                else {
                    //alert('Inválido: ' + charCode);
                    return false
                }
            }

            function fValidaDigito(qTecla) {
                var charCode = (qTecla.which) ? qTecla.which : qTecla.keyCode;
                if ((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122)) {
                    //alert('válido: ' + charCode);
                    return true
                }
                else {
                    //alert('Inválido: ' + charCode);
                    return false
                }
            }

            new WOW().init();

            //============================================

            function AbreModalMensagem(qClass) {
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').removeClass("alert-danger");
                $('#divCabecalho').removeClass("alert-info");
                $('#divCabecalho').addClass(qClass);
                $('#divMensagemModal').modal();
            }

            function fEmitirBoleto() {
                $('#divModalConfirmacaoInscricao').modal('hide');
                window.open("boletoBB.aspx");
            }

            //============================================

        </script>

    </form> 
         
</body>

     <script>

         $(document).ready(function () {
             $('#<%=txtCPFAluno.ClientID%>').mask('999.999.999-99');
             $('#<%=txtCPFSegundaVia.ClientID%>').mask('999.999.999-99');
             $('#<%=txtCEPAluno.ClientID%>').mask('99999-999');
             $('#<%=txtTelefone.ClientID%>').mask('(99) 9999-9999');
             $('#<%=txtCelular.ClientID%>').mask('(99) 99999-9999');
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

        </script>

        <asp:Literal ID="litGoogle" runat="server"></asp:Literal>

</html>
