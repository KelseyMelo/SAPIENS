<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadAlunoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadAlunoGestao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
<%--    <input type="hidden" id="hdivDDLEstadoResidenciaAlu<asp:Literal runat="server"></asp:Literal>no" name="hdivDDLEstadoResidenciaAluno" value="Ola"/>--%>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="Scripts/jquery.validate.js"></script>
    <script src="Scripts/jquery.mask.min.js"></script>
    <script src="https://cdn.rawgit.com/plentz/jquery-maskmoney/master/dist/jquery.maskMoney.min.js"></script>

    <input type="hidden" id ="hCodigo"  name="hCodigo" value="" />

    <input type="hidden" id ="hEscrita" name="hEscrita" value="0" runat="server" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css"/>
    
    <link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />

    <script src="Scripts/moment-with-locales.js"></script>
    <script src="Scripts/bootstrap-datetimepicker.js"></script>
    
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <%--<script src="Scripts/locales/bootstrap-datetimepicker.pt-BR.js"></script>--%>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/minimal/blue.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>
     
    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet" />
<%--    <link href="plugins/select2/Select2Bootstrap.css" rel="stylesheet" />--%>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/webcamjs/1.0.26/webcam.min.js"></script>
  
    <asp:Literal ID="litInputCodigoEvidencia" runat="server"></asp:Literal>
    <style type="text/css">
        
        /*CheckBox Inicio*/
        label.checkbox input[type="checkbox"] {display:none;}
        label.checkbox span {
            cursor: pointer;
            display:inline-block;
            border:2px solid #BBB;
            /*border-radius:10px;*/ /*arredondar a caixa*/
            width:20px;
            height:20px;
            /*background:#C33;*/ /*cor de fundo quando está checado*/
            vertical-align:middle;
            margin:3px;
            position: relative;
            transition:width 0.1s, height 0.1s, margin 0.1s;
        }
        label.checkbox :hover + span:after {
            border-color: #0E76A8;
        }
        label.checkbox :hover + span {
            border-color: #0E76A8;
        }
        label.checkbox :checked + span {
            /*background:#6F6;*/ /*cor de fundo quando está checado*/
            width:22px;
            height:22px;
            /*margin: 6px;*/
        }
        label.checkbox :checked + span:after {
            content: '\2714';
            font-size: 20px;
            position: absolute;
            top: -6px;
            left: 1px;
            color: #0E76A8; /*cor da seta*/
        }

        label.checkbox_only input[type="checkbox"] {display:none;}
        label.checkbox_only span {
            cursor: pointer;
            display:inline-block;
            border:2px solid #BBB;
            /*border-radius:10px;*/ /*arredondar a caixa*/
            width:20px;
            height:20px;
            /*background:#C33;*/ /*cor de fundo quando está checado*/
            vertical-align:middle;
            margin:3px;
            position: relative;
            transition:width 0.1s, height 0.1s, margin 0.1s;
        }
        label.checkbox_only :hover + span:after {
            border-color: #0E76A8;
        }
        label.checkbox_only :hover + span {
            border-color: #0E76A8;
        }
        label.checkbox_only :checked + span {
            /*background:#6F6;*/ /*cor de fundo quando está checado*/
            width:22px;
            height:22px;
            /*margin: 6px;*/
        }
        label.checkbox_only :checked + span:after {
            content: '\2714';
            font-size: 20px;
            position: absolute;
            top: 2px;
            left: 1px;
            color: #0E76A8; /*cor da seta*/
        }
            

        /*CheckBox Fim*/
        
        td.highlight {
            font-weight: bold;
            color: red;
        }

        .select2 {
            width: 100% !important;
        }

        label.opt {
            cursor: pointer;
        }

        .mao {
            cursor: pointer;
        }

        .error {
            color: red;
            font-size: 80%;
        }

        .corTitulo1 {
            background-color: #FBFBFB;
            border-top: 1px solid #ccc;
        }

        .corTitulo2 {
            background-color: #F0F0F0;
            border-top: 1px solid #ccc;
        }

        .corCorpo1 {
            background-color: #FBFBFB;
            border-top: 1px solid #ccc;
        }

        .corCorpo2 {
            background-color: #F0F0F0;
            border-top: 1px solid #ccc;
        }


        caption {
            color: white;
            background-color: #507CD1;
            font-weight: bold;
            text-align: center;
        }


        .icon-input-btn {
            display: inline-block;
            position: relative;
        }

        .icon-input-btn input[type="submit"] {
            padding-left: 2em;
        }

        .icon-input-btn .glyphicon {
            display: inline-block;
            position: absolute;
            left: 0.65em;
            top: 30%;
        }

        .tab-content {
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            padding: 10px;
        }

        .nav-tabs {
            margin-bottom: 0;
        }

        #grdProrrogacaoCPG td.centralizarTH {
                vertical-align: middle;  
            }

        #results {
            float: right;
            margin: 20px;
            padding: 20px;
            border: 1px solid;
            background: #0E76A8;
        }

        .a_faq {
            color:dimgrey !important;
            text-decoration:none !important;
            transition:all 0.5s;
        }

        .a_faq:hover {
            color:#3588CC !important;
            text-decoration:none !important;
            transition:all 0.5s;
        }

        .rotate{
            -moz-transition: all 0.5s linear;
            -webkit-transition: all 0.5s linear;
            transition: all 0.5s linear;
        }

        .rotate.down{
            -moz-transform:rotate(-90deg);
            -webkit-transform:rotate(-90deg);
            transform:rotate(-90deg);
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function(){
            $(".icon-input-btn").each(function(){
                var btnFont = $(this).find(".btn").css("font-size");
                var btnColor = $(this).find(".btn").css("color");
                $(this).find(".glyphicon").css("font-size", btnFont);
                $(this).find(".glyphicon").css("color", btnColor);
                if($(this).find(".btn-xs").length){
                    $(this).find(".glyphicon").css("top", "24%");
                }
            }); 
        });
    </script>
    <%--<asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
        <ProgressTemplate>
            <div class="progress ">
                <div class="row text-center center-block">
                    <div class="col-md-12 text-center center-block">
                    Processando... <br />Por favor, aguarde.
                    <br />
                    <img src="img/loader.gif" class="text-center center-block" width="42" height="42" alt="" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <div class="container-fluid">
        <input type="hidden" id ="hEscolheuFoto"  name="hEscolheuFoto" value="false" />
        
        <div class="row"> 
            <div class="col-md-7">
                <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Aluno</strong></h3>
            </div>
            <div class="hidden-lg hidden-md">
                <br /> <br /> 
            </div>

            <br />
            <div class="col-md-2">
                <br />
                <button type="button" runat="server"  id="Button1" name="btnVoltar" class="btn btn-default" onserverclick="btnVoltar_ServerClick" > <%--onclick="window.history.back()"--%>
                            <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>
            <div class="hidden-lg hidden-md">
                <br /> <br /> 
            </div>

            <br />
            <div class="col-md-2 pull-right">
                <button type="button" runat="server" id="btnCriarUsuario" name="btnCriarUsuario" class="btn btn-primary" href="#" onclick="" onserverclick="btnNovoAluno_Click">
                    <i class="fa fa-magic"></i>&nbsp;Cadastrar novo Aluno</button>
            </div>

        </div>
        <br />
        <hr />
        <br />

        <div class="row">
            <div class="col-md-1">
                <a href='javascript:fExibeImagem()'>
                    <img runat="server" id="imgAluno" src="img/pessoas/avatarunissex.jpg" class="img-rounded center-block" alt="Imagem do Aluno" style="width: 80px; height: 80px;"/>
                </a>
                <hr style="height:3pt; visibility:hidden;" />

                <div id="divBotaoFoto" runat="server">
                    <button id="btnAlterarImagem2" type="button" class="btn btn-primary btn-xs center-block" onclick="AbreModalDadosFoto()"><i class="fa fa-camera"></i>&nbsp;Trocar foto</button>
                </div>
                <div id="divTextoBotaoFoto" runat="server">
                    <br /><br />
                    <p>Crie o aluno e depois altere a foto.</p>
                </div>
            </div>
            <div class="hidden-lg hidden-md">
                <br />
            </div>

            <div class="col-md-1">
                <h3 class="">
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloMatricula" runat="server" Text="Matrícula"></asp:Label><br /> </span>&nbsp;<asp:Label ID="lblNumeroMatricula" runat="server" Text="0000"></asp:Label>
                </h3>
            </div>

            <div class="col-md-4">
                <h3 class="">
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloAlunoAluna" runat="server" Text="Aluno"></asp:Label> </span><br />&nbsp;<asp:Label ID="lblTituloNomeAluno" runat="server" Text="Label"></asp:Label>
                </h3>
            </div>
            <%--<div class="col-md-1">
                
            </div>--%>
            
            <div class="col-md-5">
                <h3 class="">
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloAlteradoPor" runat="server" Text="Alterado por"></asp:Label> </span>&nbsp;<asp:Label ID="lblAlteradoPor" runat="server" Text="0000" CssClass ="small"></asp:Label>
                    <br />
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloAlteradoEm" runat="server" Text="Alterado em"></asp:Label> </span>&nbsp;<asp:Label ID="lblAlteradoEm" runat="server" Text="0000" CssClass ="small" ></asp:Label>
                </h3>
            </div>

        </div>
        <%--<div class="row">
            &nbsp;<button id="btnAlterarImagem2" type="submit" class="btn btn-primary btn-xs text-right" onclick="document.getElementById('frmMaster').h_IdRegistro.value = '2';document.getElementById('frmMaster').h_idCondominio.value = '1'; document.getElementById('frmMaster').qmenu.value = 'Visitantes'; document.getElementById('frmMaster').qpagina.value = 'PaginaVisitantes';  document.getElementById('frmMaster').action='AlteraImagem.aspx'; document.getElementById('frmMaster').submit();"><i class="fa fa-camera"></i>&nbsp;Trocar foto</button>
        </div>--%>
    </div>

    <asp:Label ID="lblPagina" runat="server" Text="Label" Visible="false"></asp:Label>
    <br />

    <div class="container-fluid">
        <div class="tab-content">

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="nav-tabs-custom">

                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="modal fade" id="divModalAlteraDadosFoto" tabindex="-1" role="dialog"
                                        aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">

                                        <div class="modal-dialog modal-lg">
                                            <div class="modal-content">

                                                <div class="modal-header alert-info">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                        ×
                                                    </button>

                                                    <h4 class="modal-title" id="myModalLabel"><i class="fa fa-camera"></i>&nbsp;Trocar Foto
                                                    </h4>
                                                </div>

                                                <div class="modal-body">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <div class="row">
                                                                <%--<div class="col-md-1"></div>--%>
                                                                <div class="col-md-6">
                                                                    <strong>Foto atual</strong><hr />
                                                                    <img id="imgFotoOriginal" alt="Foto atual" runat="server" class="img-responsive" title="Foto atual" src="#" style="max-height: 500px" />
                                                                    <hr />
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <strong>Nova foto</strong><hr />
                                                                    <div id="fileUpload" style="float: left; vertical-align: top">
                                                                        <h5 id="errorBlock" class="file-error-message" style="display: none;"></h5>
                                                                        <span class="file-input file-input-new">
                                                                            <div class="row ">
                                                                                <div class="col-md-12">
                                                                                    <div id="divImgPrw" class="file-preview " style="display: none">
                                                                                        <div class="" id="divImgPreview">
                                                                                            <img id="imgprw" alt="" runat="server" src="" class="img-responsive" style="max-height: 500px" />
                                                                                        </div>
                                                                                        <br />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row " id="divBntLocalizar" style="display: block">
                                                                                <div class="col-md-6 text-center">
                                                                                    <br />
                                                                                    <button type="button" id="btnLocalizar" class="btn btn-primary btn-md"
                                                                                        onclick="javascript:document.getElementById('<%=fileArquivoParaGravar.ClientID%>').click();">
                                                                                        <i class="fa fa-folder-open fa-lg"></i>&nbsp;&nbsp;&nbsp;Localizar nova Foto …
                                                                                    </button>
                                                                                </div>
                                                                                <div class="hidden-lg hidden-md">
                                                                                    <br />
                                                                                </div>

                                                                                <div class="col-md-6 text-center">
                                                                                    <br />
                                                                                    <button type="button" id="btnAbrirCamera" runat="server" visible="false" class="btn btn-purple btn-md"
                                                                                        onclick="fAbrirCamera()">
                                                                                        <i class="fa fa-camera-retro fa-lg"></i>&nbsp;&nbsp;&nbsp;Abrir Câmera
                                                                                    </button>
                                                                                </div>

                                                                            </div>

                                                                            <div class="row " id="divBotoes" style="display: none">
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-3"></div>
                                                                                    <div class="col-md-6">
                                                                                        <button type="button" id="btnLimparArquivo" title="Excluir foto selecionada" onclick="LimparArquivo()" class="btn btn-default fileinput-remove fileinput-remove-button">
                                                                                            <i class="glyphicon glyphicon-trash"></i>&nbsp;Excluir
                                                                                        </button>
                                                                                    </div>
                                                                                    <div class="col-md-3"></div>
                                                                                </div>
                                                                            </div>

                                                                        </span>
                                                                        <br />
                                                                        <div id="divMensagens">

                                                                            <small><i class="fa fa-check"></i> Imagens com tamanho máximo de 1 Mb!</small><br />
                                                                            <small><i class="fa fa-check"></i> Somente imagens com extenção "JPG" ou "JEPG" ou "PNG"!</small><br />
                                                                            <small><i class="fa fa-check"></i> Evite nome de arquivo com caracteres especiais!</small><br />
                                                                        </div>
                                                                        <hr />
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="modal-footer">

                                                    <div class="pull-left">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">
                                                            <i class="fa fa-close"></i>&nbsp;Fechar
                                                        </button>
                                                    </div>

                                                    <div class="pull-right" id="divBotaoSalvar" style="display: none">
                                                        <button type="button" id="bntSalvarDadosFotoOffLine" title="Salvar foto selecionada" onclick="SalvarDadosFoto();" class="btn btn-success"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                            <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;Salvar Dados da Foto
                                                        </button>
                                                    </div>

                                                </div>

                                            </div>
                                            <!-- /.modal-content -->
                                        </div>
                                        <!-- /.modal-dialog -->

                                    </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                

                                <ul class="nav nav-tabs">
                                    <li id="tabDadosAluno" class="active"><a href="#tab_DadosAluno" id="atab_DadosAluno" data-toggle="tab"><strong>Cadastro</strong></a></li>
                                    <li id="tabSituacaoAcademica" runat="server" class="hidden"><a href="#tab_SituacaoAcademica" id="atab_SituacaoAcademica"  data-toggle="tab"><strong>Situação Acadêmica Old</strong></a></li>
                                    <li id="tabSituacaoAcademicaNew" runat="server" class=""><a href="#tab_SituacaoAcademicaNew" id="atab_SituacaoAcademicaNew"  data-toggle="tab"><strong>Situação Acadêmica</strong></a></li>
                                </ul>

                                <br />

                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_DadosAluno">
                                        <%--                                        <b>How to use:</b>--%>
                                        <div class="box box-primary">
                                            <div class="box-header">
                                                <div class="row">
                                                    <div class="col-md-3 pull-right">
                                                        <button type="button" runat="server" id="bntSalvarAlunoAcima_2" title="Salvar dados na Turma" onclick="if (fProcessando()) return;" class="btn btn-success pull-right" onserverclick="bntSalvarAluno_ServerClick"> 
                                                            <i class="fa fa-save"></i>&nbsp;Salvar Dados do Aluno
                                                        </button>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br /> 
                                                    </div>

                                                    <div class="col-md-9">
                                                        <h3 class="box-title">Dados Pessoais</h3>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            <div class="box-body">
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Dados Pessoais</h5>
                                                    <div class="row">
                                                        <div class="col-md-4 ">
                                                            <span class ="text-red text-bold">* </span><span>Nome</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNomeAluno" type="text" value="" maxlength="150" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Matrícula</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtMatriculaAluno" type="text" value="" readonly="readonly"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Data Cadastro</span><br />
                                                            
                                                                <input class="form-control input-sm" runat="server" id="txtDataCadastroAluno" type="text" value="" readonly="readonly"/>
                                                            
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span class ="text-red text-bold">* </span><span>CPF</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCPFAluno" type="text" value=""/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2">
                                                            <span>Estrangeiro</span><br />
                                                            <%--<div class="row center-block btn-default form-group">
                                                                <asp:RadioButton GroupName="Estrangeiro" ID="optEstrangeiroNao" runat="server"/>
                                                                &nbsp;
                                                                <label class="opt" for="<%=optEstrangeiroNao.ClientID %>">Não</label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    
                                                                <asp:RadioButton GroupName="Estrangeiro" ID="optEstrangeiroSim" runat="server" />
                                                                &nbsp;
                                                                <label class="opt" for="<%=optEstrangeiroSim.ClientID %>">Sim</label>
                                                            </div>--%>
                                                            <asp:DropDownList runat="server" ID="ddlEstrangeiro" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false" onchange="fEstrangeiro()">
                                                                <asp:ListItem Text="Não" Value="Não" />
                                                                <asp:ListItem Text="Sim" Value="Sim" />
                                                            </asp:DropDownList>

                                                        </div>
                                                        
                                                    </div>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <span>Gênero</span><br />
                                                            <%--<div class="row center-block btn-default form-group">
                                                                <asp:RadioButton GroupName="Sexo" ID="optSexoMasculino" runat="server"/>
                                                                &nbsp;
                                                                <label class="opt" for="<%=optSexoMasculino.ClientID %>">Masculino</label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    
                                                                <asp:RadioButton GroupName="Sexo" ID="optSexoFeminino" runat="server" />
                                                                &nbsp;
                                                                <label class="opt" for="<%=optSexoFeminino.ClientID %>">Feminino</label>
                                                            </div>--%>
                                                            <asp:DropDownList runat="server" ID="ddlSexoAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                                <asp:ListItem Text="Masculino" Value="m" />
                                                                <asp:ListItem Text="Feminino" Value="f" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Convênio</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtConvenioAluno" type="text" value="" maxlength="100" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Linha Pesquisa</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtLinhaPesquisaAluno" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Data Últ. Alt.</span><br />
                                                            <%--<div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>--%>
                                                                <input class="form-control input-sm" runat="server" id="txtDataUltimaAlteracao" type="text" value="" readonly="readonly"/>
                                                            <%--</div>--%>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <span>Data Nascimento</span><br />
                                                           <%-- <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>--%>
                                                                <input class="form-control input-sm" runat="server" id="txtDataNascimentoAluno" type="date" value=""/>
                                                            <%--</div>--%>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Nacionalidade</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlNacionalidadeAluno" ClientIDMode="Static" class="form-control select2 input-sm " AutoPostBack="false" onchange="fEstrangeiro2()">
                                                            </asp:DropDownList>
                                                            <input class="form-control input-sm" runat="server" id="txtNacionalidadeAlunoAnt" type="text" value="" maxlength="50" style="display:block;font-size:x-large" readonly="readonly" />

                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                            <ProgressTemplate>
                                                                <div class="modal">
                                                                    <div class="center">
                                                                        Em processamento...
                                                                    </div>
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>

                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlEstadoNasctoAluno" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                            

                                                                <div class="col-md-3">
                                                                    <span>Estado</span><br />
                                                                    <div runat="server" id ="divDDLEstadoNasctoAluno">
                                                                        <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlEstadoNasctoAluno_SelectedIndexChanged" ID="ddlEstadoNasctoAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div runat="server" id ="divTXTEstadoNasctoAluno">
                                                                        <input class="form-control input-sm" runat="server" id="txtEstadoNasctoAluno" type="text" value="" maxlength="50" />
                                                                    </div>
                                                                </div>
                                                                <div class="hidden-lg hidden-md">
                                                                    <br />
                                                                </div>
                                                        
                                                                <div class="col-md-3">
                                                                    <span>Cidade</span><br />
                                                                    <div runat="server" id ="divDDLCidadeNasctoAluno">
                                                                        <asp:DropDownList runat="server" ID="ddlCidadeNasctoAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                                                        </asp:DropDownList>
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeNasctoAlunoAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly"/>
                                                                    </div>
                                                                    <div runat="server" id ="divTXTCidadeNasctoAluno">
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeNasctoAluno" type="text" value="" maxlength="50"/>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </div>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <span class ="text-red text-bold">* </span><span>Email (principal)</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-at"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtEmail1Aluno" type="text" value="" maxlength="100" />
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Email (secundário)</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-at"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtEmail2Aluno" type="text" value="" maxlength="100" />
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3">
                                                            <span>Telefone</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-phone"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtTelefoneAluno" type="text" value="" maxlength="15" onkeypress="return ehNumeroOuTRaco(event)" />
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        
                                                        <div class="col-md-3">
                                                            <span>Celular</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-mobile"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtCelularAluno" type="text" value="" maxlength="15" onkeypress="return ehNumeroOuTRaco(event)" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-5 ">
                                                            <span>Palavras-chave <span style="font-size:small">(quando houver)</span></span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtPalavraChaveAluno" type="text" maxlength="2000"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-5 ">
                                                            <span>Profissão </span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtProfissaoAluno" type="text" maxlength="200"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2">
                                                            <span>Estado Civil</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlEstadoCivilAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                                <asp:ListItem Text="Selecionar o Estado Civil" Value="" />
                                                                <asp:ListItem Text="Indefinido" Value="Indefinido" />
                                                                <asp:ListItem Text="Solteiro" Value="Solteiro" />
                                                                <asp:ListItem Text="Casado" Value="Casado" />
                                                                <asp:ListItem Text="Separado" Value="Separado" />
                                                                <asp:ListItem Text="Divorciado" Value="Divorciado" />
                                                                <asp:ListItem Text="Viuvo" Value="Viuvo" />
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                    
                                                </div>
                                                <div id="divCoordenadores" runat="server">
                                                <%--Documento de identificação--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Documento de identificação</h5>
                                                    <div class="row">
                                                        <div class="col-md-3 ">
                                                            <span>Tipo Documento</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlTipoDoctoAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                                <asp:ListItem Text="Selecione Tipo de Docto" Value="" />
                                                                <asp:ListItem Text="Registro Geral (Cédula de Identidade)" Value="Registro Geral (Cédula de Identidade)" />
                                                                <asp:ListItem Text="Registro Geral de Estrangeiro" Value="Registro Geral de Estrangeiro" />
                                                                <asp:ListItem Text="Protocolo do Registro Nacional de Estrangeiro" Value="Protocolo do Registro Nacional de Estrangeiro" />
                                                                <asp:ListItem Text="Carteira de Identidade Militar" Value="Carteira de Identidade Militar" />
                                                                <asp:ListItem Text="Passaporte" Value="Passaporte" />
                                                                <asp:ListItem Text="Carteira de Identidade Especial p/ Estrangeiros" Value="Carteira de Identidade Especial p/ Estrangeiros" />
                                                                <asp:ListItem Text="Certidão de Nascimento" Value="Certidão de Nascimento" />
                                                                <asp:ListItem Text="Termo de Guarda e Responsabilidade" Value="Termo de Guarda e Responsabilidade" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Número</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroDoctoAluno" type="text" value="" maxlength="20"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-1 ">
                                                            <span>Dígito</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtDigitoDoctoAluno" type="text" value="" maxlength="1"  onkeypress="return soNumeroeX(event)"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Orgão Expedidor</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtOrgaoExpeditorAluno" type="text" value="" maxlength="10"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Data Expedição</span><br />
                                                            <%--<div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>--%>
                                                                <input class="form-control input-sm" runat="server" id="txtDataExpedicaoAluno" type="date" value=""/>
                                                            <%--</div>--%>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Data Validade</span><br />
                                                            <%--<div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>--%>
                                                                <input class="form-control input-sm" runat="server" id="txtDataValidadeDoctoAluno" type="date" value=""/>
                                                            <%--</div>--%>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>                     
                                                    </div>                                                    
                                                </div>

                                                <%--Residência--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Residência</h5>
                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>CEP</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCepResidenciaAluno" type="text" value="" maxlength="9"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Logradouro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtLogradouroResidenciaAluno" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        <div class="col-md-2 ">
                                                            <span>Número</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroResidenciaAluno" type="number" value="" maxlength="20" min="1" onkeypress="return soNumero(event)"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Complemento</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtComplementoResidenciaAluno" type="text" value="" maxlength="50"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Bairro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtBairroResidenciaAluno" type="text" value="" maxlength="50"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        
                                                        <div class="col-md-2 ">
                                                            <span>País</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlPaisResidenciaAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false" onchange="fPaisResidencia()">
                                                            </asp:DropDownList>
                                                            <input class="form-control input-sm" runat="server" id="txtPaisResidenciaAlunoAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                            <ProgressTemplate>
                                                                <div class="modal">
                                                                    <div class="center">
                                                                        Em processamento...
                                                                    </div>
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>

                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlEstadoResidenciaAluno" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>

                                                                <div class="col-md-2 ">
                                                                    <span>Estado</span><br />
                                                                    <div runat="server" id ="divDDLEstadoResidenciaAluno">
                                                                        <asp:DropDownList runat="server" ID="ddlEstadoResidenciaAluno" OnSelectedIndexChanged="ddlEstadoResidenciaAluno_SelectedIndexChanged" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <input class="form-control input-sm" runat="server" id="txtEstadoResidenciaAlunoAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly" />
                                                                    </div>
                                                                    <div runat="server" id ="divTXTEstadoResidenciaAluno">
                                                                        <input class="form-control input-sm" runat="server" id="txtEstadoResidenciaAluno" type="text" value="" maxlength="2"/>
                                                                    </div>
                                                                </div>
                                                                <div class="hidden-lg hidden-md">
                                                                    <br />
                                                                </div>

                                                                <div class="col-md-4 ">
                                                                    <span>Cidade</span><br />
                                                                    <div runat="server" id ="divDDLCidadeResidenciaAluno">
                                                                        <asp:DropDownList runat="server" ID="ddlCidadeResidenciaAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                                                        </asp:DropDownList>
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeResidenciaAlunoAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly"/>
                                                                    </div>
                                                                    <div runat="server" id ="divTXTCidadeResidenciaAluno">
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeResidenciaAluno" type="text" value="" maxlength="50"/>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </div>                                                  
                                                </div>

                                                <%--Formação--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Formação</h5>
                                                    <div class="row">
                                                        <div class="col-md-3 ">
                                                            <span>Formação</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtFormacaoAluno" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Instituição</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtInstituicaoAluno" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Ano de Graduação</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtAnoFormacaoAluno" type="number" value="" maxlength="4" min="0"/>
                                                        </div>
                                                    </div>  
                                                </div>

                                                <%--Empresa--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Empresa</h5>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <span>Empresa</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtEmpresaAluno" type="text" value="" maxlength="200"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Nome Fantasia</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNomeFantasiaAluno" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>CNPJ</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCNPJEmpresaAluno" type="text" value="" maxlength="20" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>IE</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtIEEmpresaAluno" type="text" value="" maxlength="50"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-4 ">
                                                            <span>Nome de Contato</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNomeContato" type="text" value="" maxlength="150"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Email do Contato</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-at"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtEmailContato" type="text" value="" maxlength="100"/>
                                                            </div>
                                                        </div>
                                                    </div>  
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>CEP</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCEPEmpresaAluno" type="text" value="" maxlength="9"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Logradouro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtLogradouroEmpresaAluno" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        <div class="col-md-2 ">
                                                            <span>Número</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroEmpresaAluno" type="number" value="" maxlength="20" min="1"  onkeypress="return soNumero(event)"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Complemento</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtComplementoEmpresaAluno" type="text" value="" maxlength="50"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Bairro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtBairroEmpresaAluno" type="text" value="" maxlength="50"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        
                                                        <div class="col-md-2 ">
                                                            <span>País</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlPaisEmpresaAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false" onchange="fPaisEmpresa()">
                                                            </asp:DropDownList>
                                                            <input class="form-control input-sm" runat="server" id="txtPaisEmpresaAlunoAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                            <ProgressTemplate>
                                                                <div class="modal">
                                                                    <div class="center">
                                                                        Em processamento...
                                                                    </div>
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>

                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlEstadoEmpresaAluno" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>

                                                                <div class="col-md-2 ">
                                                                    <span>Estado</span><br />
                                                                    <div runat="server" id ="divDDLEstadoEmpresaAluno">
                                                                        <asp:DropDownList runat="server" ID="ddlEstadoEmpresaAluno" OnSelectedIndexChanged="ddlEstadoEmpresaAluno_SelectedIndexChanged" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    <input class="form-control input-sm" runat="server" id="txtEstadoEmpresaAlunoAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly" />
                                                                    </div>
                                                                    <div runat="server" id ="divTXTEstadoEmpresaAluno">
                                                                        <input class="form-control input-sm" runat="server" id="txtEstadoEmpresaAluno" type="text" value="" maxlength="2"/>
                                                                    </div>
                                                                </div>
                                                                <div class="hidden-lg hidden-md">
                                                                    <br />
                                                                </div>

                                                                <div class="col-md-4 ">
                                                                    <span>Cidade</span><br />
                                                                    <div runat="server" id ="divDDLCidadeEmpresaAluno">
                                                                        <asp:DropDownList runat="server" ID="ddlCidadeEmpresaAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                                                        </asp:DropDownList>
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeEmpresaAlunoAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly"/>
                                                                    </div>
                                                                    <div runat="server" id ="divTXTCidadeEmpresaAluno">
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeEmpresaAluno" type="text" value="" maxlength="50"/>
                                                                    </div>
                                                                </div>

                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div> 
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-4 ">
                                                            <span>Cargo</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCargoAluno" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Telefone</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-phone"></i>
                                                                </div>
                                                                <input class="form-control input-sm " runat="server" id="txtTelefoneEmpresaAluno" type="text" value="" maxlength="15"  onkeypress="return ehNumeroOuTRaco(event)"/>
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        <div class="col-md-2 ">
                                                            <span>Ramal</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtRamalEmpresaAluno" type="text" value="" maxlength="15"/>
                                                        </div>
                                                    </div>                                                  
                                                </div>

                                                <%--Doc. Entregues--%>
                                                <div class="row well" id="divDocumentosEntregues" runat="server" style="display:none">
                                                    <h5 class="box-title text-bold">Documentos Entregues</h5>
                                                    <div class="row hidden">
                                                        <div class="col-md-3">
                                                            <asp:CheckBox ID="chkRG" runat="server"/>
                                                            &nbsp;
                                                            <label class="opt" for="<%=chkRG.ClientID %>">RG</label>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3">
                                                            <asp:CheckBox ID="chkCPF" runat="server"/>
                                                            &nbsp;
                                                            <label class="opt" for="<%=chkCPF.ClientID %>">CPF</label>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3">
                                                            <asp:CheckBox ID="chkHistoricoEscolar" runat="server"/>
                                                            &nbsp;
                                                            <label class="opt" for="<%=chkHistoricoEscolar.ClientID %>">Histórico Escolar</label>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                     
                                                        <div class="col-md-3">
                                                                <asp:CheckBox ID="chkDiploma" runat="server"/>
                                                                &nbsp;
                                                                <label class="opt" for="<%=chkDiploma.ClientID %>">Diploma</label>
                                                        </div>
                                                        
                                                    </div>
                                                    <br />

                                                    <div class ="row hidden">
                                                        <div class="col-md-3">
                                                                <asp:CheckBox ID="chkComprovanteEndereco" runat="server"/>
                                                                &nbsp;
                                                                <label class="opt" for="<%=chkComprovanteEndereco.ClientID %>">Comprovante de Endereço</label>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                     
                                                        <div class="col-md-3">
                                                                <asp:CheckBox ID="chkFoto" runat="server"/>
                                                                &nbsp;
                                                                <label class="opt" for="<%=chkFoto.ClientID %>">2 Fotos 2x2</label>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                     
                                                        <div class="col-md-3">
                                                                <asp:CheckBox ID="chkCertidaoNascimento" runat="server"/>
                                                                &nbsp;
                                                                <label class="opt" for="<%=chkCertidaoNascimento.ClientID %>">Certidão de Nascimento/Casamento</label>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                     
                                                        <div class="col-md-3">
                                                                <asp:CheckBox ID="chkContratoAssinado" runat="server"/>
                                                                &nbsp;
                                                                <label class="opt" for="<%=chkContratoAssinado.ClientID %>">Contrato Assinado</label>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="nav-tabs-custom">
                                                        <ul class="nav nav-tabs">
                                                            <li class="active"><a href="#tab_DocumentosObrigatorios" data-toggle="tab" aria-expanded="true">Obrigatórios</a></li>
                                                            <li class=""><a href="#tab_DocumentosOutros" data-toggle="tab" aria-expanded="false">Outros</a></li>
                                                        </ul>
                                                        <div class="tab-content">
                                                            <div class="tab-pane active" id="tab_DocumentosObrigatorios">
                                                                

                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                <div class="grid-content">
                                                                                    <div id="divgrdDocumentosObrigatorios" class="table-responsive">
                                                                                        <div class="scroll">
                                                                                            <table id="grdDocumentosObrigatorios" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                    <tr>
                                                                                                
                                                                                                    </tr>
                                                                                                </thead>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>  
                                                                    </div>
                                                                </div>



                                                            </div>
                                                            <!-- /.tab-pane -->
                                                            <div class="tab-pane" id="tab_DocumentosOutros">
                                                                

                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                
                                                                                    
                                                                                <div class="grid-content">
                                                                                    <div id="msgSemResultadosgrdDocumentosOutros" style="display:block">
                                                                                        <div class="alert bg-gray">
                                                                                            <asp:Label runat="server" ID="Label17" Text="Nenhum Documento Não-Obrigatório encontrado." />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div id="divgrdDocumentosOutros" class="table-responsive" style="display:none">
                                                                                        <div class="scroll">
                                                                                            <table id="grdDocumentosOutros" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                    <tr>
                                                                                                
                                                                                                    </tr>
                                                                                                </thead>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="row">
                                                                                        <div class="col-md-4 pull-right">
                                                                                            <button type="button" id="btnIncluirDocuemntoNaoObrigatorio" runat="server" name="btnIncluirDocuemntoNaoObrigatorio" class="btn btn-info pull-right" onclick="javascript:fEditarDocumentoNaoObrigatorio('0','1','','')">
                                                                                                <i class="fa fa-file-pdf-o fa-lg"></i>&nbsp;Incluir Documento não-obrigatório</button>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                    
                                                                                
                                                                            </div>
                                                                        </div>  
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <!-- /.tab-content -->
                                                    </div>
                                                </div>

                                                <%--Data Limite--%>
                                                <div id="divDataLimiteDocumento" class="row well" runat="server">
                                                    <h5 class="box-title text-bold">Data Limite para entrega de Documentos</h5>
                                                    <div class="row hidden">
                                                        <div class="col-md-2 ">
                                                            <span>Data de Cadastro</span><br />
                                                            <input class="form-control input-sm" id="txtDataCadastroDataLimiteDcumentacao" type="text" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>cadastrado/alterado por</span><br />
                                                            <input class="form-control input-sm" id="txtResponsavelDataLimiteDcumentacao" type="text" readonly="true"/>
                                                        </div>

                                                    </div>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <small class="text-danger">Caso haja <strong>Documentos Pendentes</strong> após a <strong>Data Limite</strong> o aluno não poderá ser matriculado em nenhum oferecimento.</small><br />
                                                            <span><strong>Data Limite</strong></span>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <input class="form-control" runat="server" id="txtDataLimiteDocumento" type="text" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-6">
                                                            <button type="button" id="btnAlterarDataLimiteDocumentacao" title="Alterar Data Limite Documentação" onclick="fModalIncluirDataLimiteDcumentacao()" class="btn btn-primary" style="vertical-align:bottom"> 
                                                                <i class="fa fa-calendar-check-o"></i>&nbsp;Alterar Data Limite Documentação
                                                            </button>
                                                        </div>
                                                    </div>     
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <span>Observações</span><br />
                                                            <div class="grid-content">
                                                                <div id="divgrdDataLimiteDocumentacao" class="table-responsive">
                                                                    <div class="scroll">
                                                                        <table id="grdDataLimiteDocumentacao" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                            <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                <tr>
                                                                                                
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--Refazer Prova--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Refazer Provas</h5>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:CheckBox ID="chkRefazerProvaProficienciaIngles" runat="server"/>
                                                            &nbsp;
                                                            <label class="opt" for="<%=chkRefazerProvaProficienciaIngles.ClientID %>">Refazer prova de Proficiência em Inglês</label>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-6">
                                                            <asp:CheckBox ID="chkRefazerProvaPortugues" runat="server"/>
                                                            &nbsp;
                                                            <label class="opt" for="<%=chkRefazerProvaPortugues.ClientID %>">Refazer prova de Português</label>
                                                        </div>
                                                    </div>                                                    
                                                </div>

                                                <%--Ocorrências--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Ocorrências</h5>
                                                    <div class="row">
                                                        <div class="col-md-12 ">
                                                            <textarea style ="resize:vertical;font-size:14px" runat ="server" class="form-control input-sm" rows="5" id="txtOcorrenciaAluno"></textarea>
                                                        </div>
                                                    </div>                                                    
                                                </div>

                                                </div>
                                            </div>
                                            <div class="box-footer">
                                                <div class="pull-right">
                                                    <div class="col-md-12">
                                                        <button type="button" runat="server" id="bntSalvarAluno_2" title="Salvar dados na Turma" onclick="if (fProcessando()) return;" class="btn btn-success pull-right" onserverclick="bntSalvarAluno_ServerClick"> 
                                                            <i class="fa fa-save"></i>&nbsp;Salvar Dados do Aluno
                                                        </button>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

<%--==========================================================================================================================================--%>
                                    <input runat="server" id="txtIdTurma" type="text" value="" visible="false" />
                                    <div class="tab-pane" id="tab_SituacaoAcademicaNew">
                                        <div class="box box-primary">
                                            <div class="box-header">
                                                <h3 class="box-title">Situação Acadêmica</h3>
                                            </div>
                                            <div class="box-body">
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Turma</h5>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div id="divTurmaDiversasNew" style="display:none">
                                                                <div class="row">
                                                                    <div class="col-md-8">
                                                                        <span class="piscante" style="color:darkblue"><b>Outras Turmas</b></span><br />
                                                                        <select id="ddlTurmaAlunoNew" class="form-control input-sm select2 SemPesquisa">
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                            </div>

                                                            <div id="divTurmaTemNew" style="display:none">
                                                                <div class="row">
                                                                    <div class="col-md-2">
                                                                        <span>Código Turma</span><br />
                                                                        <input class="form-control input-sm" runat="server" id="txtCodTurmaAlunoNew" type="text" value="" readonly="readonly" />
                                                                        <input class="form-control input-sm" runat="server" id="txtIdTurmaAlunoNew" type="text" value="" readonly="readonly" style="display:none" />
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <span>Período</span><br />
                                                                        <input class="form-control input-sm" runat="server" id="txtQuadrimestreAlunoNew" type="text" value="" readonly="readonly" />
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-2 ">
                                                                        <span>Tipo Curso</span><br />
                                                                        <input class="form-control input-sm" runat="server" id="txtTipoCursoAlunoNew" type="text" value="" readonly="readonly" />
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-2 ">
                                                                        <span>Data Início</span><br />
                                                                        <div class="input-group">
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-calendar"></i>
                                                                            </div>
                                                                            <input class="form-control input-sm" runat="server" id="txtDataInicioCursoAlunoNew" type="text" value="" readonly="readonly" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-2 ">
                                                                        <span>Data Fim</span><br />
                                                                        <div class="input-group">
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-calendar"></i>
                                                                            </div>
                                                                            <input class="form-control input-sm" runat="server" id="txtDataFimCursoAlunoNew" type="text" value="" readonly="readonly" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-2 ">
                                                                        <span>Data de Término <i class="fa fa-info-circle" style="color:blueviolet" data-toggle="tooltip" title="O cálculo da 'Data de Término' é feito somando-se à 'Data Fim' original da Turma, a diferença entre a 'Data Início' e a 'Data Fim' de todos os 'Trancamentos' e 'Prorrogações CPG'."></i></span><br />
                                                                        <div class="input-group">
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-calendar"></i>
                                                                            </div>
                                                                            <input class="form-control input-sm" runat="server" id="txtDataTerminoCursoAlunoNew" type="text" value="" readonly="readonly" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />

                                                                <div class="row">
                                                                    <div class="col-md-4 ">
                                                                        <span>Curso</span><br />
                                                                        <input class="form-control input-sm" runat="server" id="txtCursoAlunoNew" type="text" value="" readonly="readonly" />
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-4 ">
                                                                        <span>Área de Concentração</span><br />
                                                                        <input class="form-control input-sm" runat="server" id="txtAreaConcentracaoAlunoNew" type="text" value="" readonly="readonly" />
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <span>Situação <small>(data fim)</small> </span><br />
                                                                        <input class="form-control input-sm" runat="server" id="txtSituacaoAlunoNew" type="text" value="" readonly="readonly" />
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <hr />
                                                                <br />

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="nav-tabs-custom">
                                                                            <ul class="nav nav-tabs">
                                                                                <li class="active"><a href="#tab_1" data-toggle="tab">Contrato</a></li>
                                                                                <li><a href="#tab_2" data-toggle="tab">Artigo</a></li>
                                                                            </ul>
                                                                            <div class="tab-content">
                                                                                <div class="tab-pane active" id="tab_1">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <h5 class="box-title text-bold">Contrato Assinado</h5>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-4">
                                                                                            <span>Contrato</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtNomeContrato" type="text" value="" readonly="readonly" />
                                                                                            <input class="form-control input-sm" runat="server" id="txtIdAlunoArquivo" type="text" value="" readonly="readonly" style="display:none" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div class="col-md-2">
                                                                                            <span>Data Upload</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtDataUploadContrato" type="text" value="" readonly="readonly" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div class="col-md-2">
                                                                                            <span>Usuário</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtUsuarioContrato" type="text" value="" readonly="readonly" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div id="divBotaoDowloadContrato">
                                                                                            <div class="col-md-1">
                                                                                                <span></span><br />
                                                                                                <div title="Download Contrato">
                                                                                                    <a runat="server" id="aDownLoadContrato" class="btn btn-purple btn-circle fa fa-cloud-download" download="" target="_blank" href="Arquivo\40708\RG-Kelsey.pdf"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="hidden-lg hidden-md">
                                                                                                <br />
                                                                                            </div>
                                                                                        </div>

                                                                                        <div id="divBotaoSalvarContrato">
                                                                                            <div class="col-md-2">
                                                                                                <span></span><br />
                                                                                                <button type="button" runat="server" id="btnSalvarContrato" title="Salvar Contrato" onclick="fSalvarContrato();" class="btn btn-success"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                                                                <i class="fa fa-save"></i>&nbsp;Salvar Contrato
                                                                                            </button>
                                                                                            </div>
                                                                                            <div class="hidden-lg hidden-md">
                                                                                                <br />
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-md-2">
                                                                                            <span></span><br />
                                                                                            <button type="button" runat="server" id="btnLocalizarContrato" title="Localizar Contrato" onclick="fLocalizarContrato();" class="btn btn-info"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                                                                <i class="fa fa-search"></i>&nbsp;Localizar Contrato
                                                                                            </button>
                                                                                            <button type="button" runat="server" id="btnCancelarContrato" title="Cancelar alteração Contrato" onclick="fCancelarContrato();" class="btn btn-danger"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                                                                <i class="fa fa-close"></i>&nbsp;Cancelar Alteração
                                                                                            </button>
                                                                                        </div>
                                                                                    </div>
                                                                                   
                                                                                </div>
                                                                                <!-- /.tab-pane -->
                                                                                <div class="tab-pane" id="tab_2">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <h5 class="box-title text-bold">Entrega do Artigo</h5>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="row">
                                                                                         <div class="col-md-5">
                                                                                            <span>Nome do Artigo</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtNomeArtigo" type="text" value="" maxlength="300" />
                                                                                            <input class="form-control input-sm" runat="server" id="txtIdAlunoArtigo" type="text" value="" readonly="readonly" style="display:none" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div class="col-md-3 ">
                                                                                            <span>Data entrega</span><br />
                                                                                            <input class="form-control input-sm" id="txtDataEntregaArtigoNew" type="date" value="" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                         <div class="col-md-3 ">
                                                                                            <span>Data Aprovação</span><br />
                                                                                            <input class="form-control input-sm" id="txtDataAprovacaoArtigoNew" type="date" value="" />
                                                                                        </div>

                                                                                    </div>
                                                                                    <br /> 

                                                                                    <div class="row">
                                                                                        <div class="col-md-5">
                                                                                            <span>Arquivo do Artigo</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtArquivoArtigo" type="text" value=""  readonly="readonly"  />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div class="col-md-2">
                                                                                            <span>Data Upload</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtDataUploadArtigo" type="text" value="" readonly="readonly" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div class="col-md-2">
                                                                                            <span>Usuário</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtUsuarioArtigo" type="text" value="" readonly="readonly" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div id="divBotaoDowloadArtigo">
                                                                                            <div class="col-md-1">
                                                                                                <span></span><br />
                                                                                                <div title="Download Artigo">
                                                                                                    <a runat="server" id="aDownLoadArtigo" class="btn btn-purple btn-circle fa fa-cloud-download" download="" target="_blank" href="Arquivo\40708\RG-Kelsey.pdf"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="hidden-lg hidden-md">
                                                                                                <br />
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-md-2">
                                                                                            <span></span><br />
                                                                                            <button type="button" runat="server" id="btnLocalizarArtigo" title="Localizar Artigo" onclick="fLocalizarArtigo();" class="btn btn-info"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                                                                <i class="fa fa-search"></i>&nbsp;Localizar Artigo
                                                                                            </button>
                                                                                            <button type="button" runat="server" id="btnCancelarArtigo" title="Cancelar alteração Artigo" onclick="fCancelarArtigo();" class="btn btn-danger"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                                                                <i class="fa fa-close"></i>&nbsp;Cancelar Alteração
                                                                                            </button>
                                                                                        </div>

                                                                                    </div>
                                                                                    <br />

                                                                                    <div class="row">
                                                                                        <div class="col-md-5">
                                                                                            <span>Nome do Orientador do Artigo</span><br />
                                                                                            <input class="form-control input-sm" runat="server" id="txtOrientadorArtigo" type="text" value="" maxlength="100" />
                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div id="divBotaoSalvarArtigo">
                                                                                            <div class="col-md-2">
                                                                                                <span></span><br />
                                                                                                <button type="button" runat="server" id="bntSalvarDadosTurmaNew" title="Salvar dados no Artigo" onclick="fSalvarDadosArtigo();" class="btn btn-success"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                                                                    <i class="fa fa-save"></i>&nbsp;Salvar Artigo
                                                                                                </button>
                                                                                            </div>
                                                                        
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <!-- /.tab-pane -->
                                                                            </div>
                                                                            <!-- /.tab-content -->
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                               
                                                            </div>

                                                            <div id="divTurmaNaoTemNew">
                                                                <div class="row">
                                                                    <div class="col-md-2 ">
                                                                        <div class="alert bg-gray">
                                                                            <asp:Label runat="server" ID="lblMsgSemResultadosNew" Text="Sem Turma associada" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row well">
                                                    <div class="nav-tabs-custom">

                                                        <ul class="nav nav-tabs">
                                                            <li id="tabHistoricoMatriculaNew" runat="server" class="active"><a href="#<%=tab_HistoricoMatriculaNew.ClientID %>" data-toggle="tab"><strong>Situação da Matrícula</strong></a></li>
                                                            <li id="tabHistoricoAlunoNew" runat="server" class=""><a href="#<%=tab_HistoricoAlunoNew.ClientID %>" data-toggle="tab"><strong>Histórico Escolar</strong></a></li>
                                                            <li id="tabOrientacaoAlunoNew" runat="server" class=""><a href="#<%=tab_OrientacaoAlunoNew.ClientID %>" data-toggle="tab"><strong>Orientação</strong></a></li>
                                                            <li id="tabBancaAlunoNew" runat="server" class=""><a href="#<%=tab_Banca.ClientID %>" data-toggle="tab"><strong>Bancas</strong></a></li>
                                                            <li id="tabProrrogacaoCPG" runat="server" class=""><a href="#<%=tab_ProrrogacaoCPG.ClientID %>" data-toggle="tab"><strong>Reuniões CPG</strong></a></li>
                                                            <li id="tabContrato" runat="server" class=""><a href="#<%=tab_Contrato.ClientID %>" data-toggle="tab"><strong>Contrato</strong></a></li>
                                                            <li id="tabCertificado" runat="server" class=""><a href="#<%=tab_Certificado.ClientID %>" data-toggle="tab"><strong>Certificado de Titulação</strong></a></li>

                                                        </ul>

                                                        <div class="tab-content">
                                                            <%--Situação da Matrícula--%>
                                                            <div class="tab-pane" runat="server" id="tab_HistoricoMatriculaNew">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="grid-content">
                                                                                            <div id="msgSemResultadosgrdHistoricoMatriculaNew" style="display:block">
                                                                                                <div class="alert bg-gray">
                                                                                                    <asp:Label runat="server" ID="Label2" Text="Nenhum Histórico da Matrícula encontrado." />
                                                                                                </div>
                                                                                            </div>

                                                                                            <div id="divgrdHistoricoMatriculaNew" class="table-responsive" style="display:none">
                                                                                                <div class="scroll">
                                                                                                    <table id="grdHistoricoMatriculaNew" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                        <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                            <tr>
                                                                                                
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>  
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-4 pull-left">
                                                                        <button type="button" runat="server" id="btnImprimirSituacaoMatricula" name="btnImprimirSituacaoMatricula" class="btn btn-warning pull-left" onserverclick="btnImprimirSituacaoMatricula_Click" onclick="if (fPreparaRelatorio('O relatório de Situação da Matrícula está sendo preparado.')) return;" >
                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Situação da Matrícula</button>
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-4 pull-right">
                                                                        <button type="button" id="btnIncluirHistoricoMatricula" runat="server" name="btnIncluirHistoricoMatricula" class="btn btn-info pull-right" onclick="fModalIncluirSituacao()">
                                                                            <i class="fa fa-calendar-plus-o"></i>&nbsp;Incluir Situação da Matrícula</button>
                                                                    </div>
                                                                </div>  

                                                            </div>

                                                            <%--Histórico do Aluno--%>
                                                            <div class="tab-pane" runat="server" id="tab_HistoricoAlunoNew">
                                                                <div class="tab-content">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-body">
                                                                            <div class="row">
                                                                                <div class="col-md-12 ">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="grid-content">
                                                                                                <div id="msgSemResultadosgrdHistoricoAlunoNew" style="display:block">
                                                                                                    <div class="alert bg-gray">
                                                                                                        <asp:Label runat="server" ID="Label4" Text="Nenhum Histórico encontrado" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div id="divgrdHistoricoAlunoNew" class="table-responsive" style="display:none">
                                                                                                    <div class="">
                                                                                                        <table id="grdHistoricoAlunoNew" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                            <caption>RELAÇÃO DE DISCIPLINAS</caption>
                                                                                                            <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                                <tr>
                                                                                                
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>  
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="divBotoesImpressaoHistoricoNew" class="row">
                                                                    <div class="col-md-6">
                                                                        <button type="button" id="btnImprimirHitoricoOffNew" name="btnImprimirHitoricoOff" class="btn btn-warning center-block" onclick="funcClicaImprimirHistorico()">
                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Histórico</button>
                                                                    </div>
                                                                    <div class="hidden-lg hidden-md">
                                                                        <br />
                                                                    </div>

                                                                    <div class="col-md-6 ">
                                                                        <button type="button" id="btnImprimirHitoricoOficialOffNew" name="btnImprimirHitoricoOficialOff" class="btn btn-success center-block" onclick="funcModalImprimirHistoricoOficial()">
                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Histórico Oficial</button>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <%--Cadastro do Orientador--%>
                                                            <div class="tab-pane" runat="server" id="tab_OrientacaoAlunoNew">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="row">
                                                                                    <div class="col-md-2">
                                                                                        <h5 class="box-title text-bold">Orientador</h5>
                                                                                    </div>

                                                                                    <div class="col-md-3">

                                                                                    </div>
                                                                                </div>
                                                                                
                                                                                <div class="row">
                                                                                    <div class="col-md-12 ">
                                                                                        <div class="row">
                                                                                            <div class="col-md-12">
                                                                                                <div id="divOrientadorNaoTem">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-6">
                                                                                                            <div class="alert bg-gray">
                                                                                                                <asp:Label runat="server" ID="Label6" Text="Sem Orientador cadastrado" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="hidden-lg hidden-md">
                                                                                                            <br />
                                                                                                        </div>

                                                                                                        <div class="col-md-3 center-block">
                                                                                                            <br />
                                                                                                            <button type="button" class="btn btn-primary center-block" title="Selecionar Orientador" onclick="funcModalAdicionaOrientador('Selecionar')">
                                                                                                                <i class="fa fa-graduation-cap"></i>&nbsp;Selecionar Orientador</button>
                                                                                                        </div>
                                                                                                    </div>
                                                                                            
                                                                                                </div>
                                                                                                <div id="divOrientadorTem" style="display:none">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-3 ">
                                                                                                            <span>CPF</span><br />
                                                                                                            <input class="form-control input-sm" id="txtIdOrientador" type="text" value="" readonly="true" style="display:none"/>
                                                                                                            <input class="form-control input-sm" id="txtCpfOrientador" type="text" value="" readonly="true"/>
                                                                                                        </div>
                                                                                                        <div class="hidden-lg hidden-md">
                                                                                                            <br />
                                                                                                        </div>

                                                                                                        <div class="col-md-4 ">
                                                                                                            <span>Nome</span><br />
                                                                                                            <input class="form-control input-sm" id="txtNomeOrientador" type="text" value="" readonly="true"/>
                                                                                                        </div>
                                                                                                        <div class="hidden-lg hidden-md">
                                                                                                            <br />
                                                                                                        </div>

                                                                                                        <div class="col-md-3 center-block">
                                                                                                            <br />
                                                                                                            <button type="button" id="btnAlterarOrientadorOrientacao" runat="server" class="btn btn-primary center-block" title="Alterar Orientador" onclick="funcModalAdicionaOrientador('Alterar')">
                                                                                                                <i class="fa fa-graduation-cap"></i>&nbsp;Alterar Orientador</button>
                                                                                                        </div>


                                                                                                    </div> 
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>   
                                                                                    </div>
                                                                                </div>  
                                                                        
                                                                            </div>
                                                                            <br />

                                                                            <div id="divBotoesOrientacao" class="col-md-12" style="display:none">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <br />
                                                                                        <h5 class="box-title text-bold">Título da Orientação</h5>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12 ">
                                                                                                <textarea style ="resize:vertical;font-size:14px" class="form-control input-sm" rows="3" maxlength="500" id="txtTituloOrientacaoNew"></textarea>
                                                                                            </div>
                                                                                        </div>                                                    
                                                                                    </div>
                                                                                </div>
                                                                                <br />
                                                                        
                                                                                <div class="row">
                                                                                    <div class="col-md-6">
                                                                                        <button type="button" id="btnExcluirOrientacao" runat="server" name="btnExcluirOrientacao" class="btn btn-danger pull-left" title="Excluir Dados da Orientação" onclick="fModalApagarDadosOrientacao()">
                                                                                                <i class="fa fa-eraser"></i>&nbsp;Excluir Dados da Orientação</button>
                                                                                    </div>

                                                                                    <div class="col-md-6">
                                                                                        <button type="button" id="btnSalvarOrientacao" runat="server" name="btnSalvarOrientacao" class="btn btn-success pull-right" title="Salvar Dados da Orientação" onclick="fSalvarDadosOrientacao()">
                                                                                                <i class="fa fa-save"></i>&nbsp;Salvar Dados da Orientação</button>
                                                                                    </div>
                                                                                </div>
                                                                        
                                                                            </div>
                                                                    
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                
                                                                <br />
                                                                        
                                                                <div id="divCoorientador" class="panel panel-default" style="display:none">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <h5 class="box-title text-bold">Co-orientador(es)</h5>
                                                                            </div>
                                                                        </div>
                                                                                
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="grid-content">
                                                                                    <div id="msgSemResultadosgrdCoorientadorAlunoNew" style="display:block">
                                                                                        <div class="alert bg-gray">
                                                                                            <asp:Label runat="server" ID="Label7" Text="Nenhum Co-orientador encontrado" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div id="divgrdCoorientadorAlunoNew" class="table-responsive" style="display:none">
                                                                                        <div class="">
                                                                                            <table id="grdCoorientadorAlunoNew" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                    <tr>
                                                                                                
                                                                                                    </tr>
                                                                                                </thead>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                        </div>
                                                                        
                                                                        <div class="btn-group pull-right">
                                                                            <button style="padding-right:2em" type="button" id="btnAdicionarCoOrientacao" runat="server" name="btnAdicionarCoOrientacao" class="btn btn-primary" title="Adicionar Co-Orientador" onclick="funcModalAdicionaOrientador()">
                                                                                    <i class="fa fa-graduation-cap"></i>&nbsp;Adicionar Co-Orientador</button>
                                                                        </div>                                                    
                                                                                
                                                                    </div>
                                                                </div>
        
                                                            </div>

                                                            <%--Cadastro de Bancas--%>
                                                            <div class="tab-pane" runat="server" id="tab_Banca">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="nav-tabs-custom">
                                                                            <ul class="nav nav-tabs">
                                                                                <li id="tabBancaQualificacao" runat="server" class="active"><a href="#<%=tab_BancaQualificacao.ClientID %>" data-toggle="tab"><strong>Qualificação</strong></a></li>
                                                                                <li id="tabBancaDefesa" runat="server" class=""><a href="#<%=tab_BancaDefesa.ClientID %>" data-toggle="tab"><strong>Defesa</strong></a></li>
                                                                            </ul>

                                                                            <div class="tab-content">

                                                                                <div class="tab-pane active" runat="server" id="tab_BancaQualificacao">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="nav-tabs-custom">
                                                                                                <ul class="nav nav-tabs">
                                                                                                    <li id="tabBancaQualificacao_Cadastro" runat="server" class="active"><a href="#<%=tab_BancaQualificacao_Cadastro.ClientID %>" data-toggle="tab"><strong>Cadastro</strong></a></li>
                                                                                                    <li id="tabBancaQualificacao_Orientador" runat="server" class=""><a href="#<%=tab_BancaQualificacao_Orientador.ClientID %>" data-toggle="tab"><strong>Orientador</strong></a></li>
                                                                                                    <li id="tabBancaQualificacao_Coorientador" runat="server" class=""><a href="#<%=tab_BancaQualificacao_Coorientador.ClientID %>" data-toggle="tab"><strong>Co-orientador</strong></a></li>
                                                                                                    <li id="tabBancaQualificacao_Membro" runat="server" class=""><a href="#<%=tab_BancaQualificacao_Membro.ClientID %>" data-toggle="tab"><strong>Membros</strong></a></li>
                                                                                                </ul>

                                                                                                <div class="tab-content">
                                                                                                    <div class="tab-pane active" runat="server" id="tab_BancaQualificacao_Cadastro">
                                                                                                        <%--Cadastro Banca Qualificação--%>
                                                                                                        <div class="panel panel-info">
                                                                                                            <div class="panel-body">
                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-3">
                                                                                                                        <h5 class="box-title text-bold">Cadastro Qualificação</h5>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <br />
                                                                                                                <div id="divEdicaoBancaQualificacao" style="display:none">
                                                                                                                    <div class="row">
                                                                                                                        <div class="col-md-2 ">
                                                                                                                            <span>Data de Cadastro</span><br />
                                                                                                                            <input class="form-control input-sm" id="txtDataCadastroBancaQualificacao" type="text" readonly="true"/>
                                                                                                                        </div>
                                                                                                                        <div class="hidden-lg hidden-md">
                                                                                                                            <br />
                                                                                                                        </div>

                                                                                                                        <div class="col-md-2 ">
                                                                                                                            <span>Última Alteração</span><br />
                                                                                                                            <input class="form-control input-sm" id="txtDataAlteracaoBancaQualificacao" type="text" readonly="true"/>
                                                                                                                        </div>
                                                                                                                        <div class="hidden-lg hidden-md">
                                                                                                                            <br />
                                                                                                                        </div>

                                                                                                                        <div class="col-md-3 ">
                                                                                                                            <span>Alterado por</span><br />
                                                                                                                            <input class="form-control input-sm" id="txtResponsavelBancaQualificacao" type="text" readonly="true"/>
                                                                                                                        </div>

                                                                                                                    </div>
                                                                                                                    <br />
                                                                                                                </div>

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>N.º Banca</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtNumeroBancaQualificacao" type="text" value="" readonly="true"/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-3">
                                                                                                                        <span>Data</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtDataBancaQualificacao" type="date" value=""/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>Hora</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtHoraBancaQualificacao" type="time" value=""/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>
                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>Banca Remota</span><br />
                                                                                                                        <select id="ddlBancaQualificacaoRemota" class="form-control input-sm select2 SemPesquisa">
                                                                                                                            <option selected value="">Indicar se a Banca é remota</option>
                                                                                                                            <option value="1">Sim</option>
                                                                                                                            <option value="0">Não</option>
                                                                                                                        </select>
                                                                                                                    </div>

                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>Resultado</span><br />
                                                                                                                        <select id="ddlResultadoBancaQualificacao" class="form-control input-sm select2 SemPesquisa">
                                                                                                                            <option selected value=""></option>
                                                                                                                            <option value="Aprovado">Aprovado</option>
                                                                                                                            <option value="Reprovado">Reprovado</option>
                                                                                                                        </select>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <br />

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-10">
                                                                                                                        <span>Título</span><br />
                                                                                                                        <textarea style ="resize:vertical;font-size:14px" class="form-control input-sm" rows="3" maxlength="500" id="txtTituloBancaQualificacao"></textarea>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <br />

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-10">
                                                                                                                        <span>Observação</span><br />
                                                                                                                        <textarea style ="resize:vertical;font-size:14px" class="form-control input-sm" rows="3" maxlength="5500" id="txtObdervacaoBancaQualificacao"></textarea>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <br />

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-2 pull-left">
                                                                                                                        <br />
                                                                                                                        <button id="btnImprimirDivulgacaoQualificao" type="button" title="Imprimir Divulgação da Qualificação" onclick="fImprimirDivulgacao('Qualificação');" class="btn btn-warning pull-left"> 
                                                                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Divulgação
                                                                                                                        </button>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-8 center-block">
                                                                                                                        <br />
                                                                                                                        <button id="btnImprimirAtaQualificao" type="button" title="Imprimir Ata da Qualificação" onclick="fAbreModalObsAta('Qualificação');" class="btn btn-purple center-block"> 
                                                                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Ata
                                                                                                                        </button>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-2 pull-right">
                                                                                                                        <br />
                                                                                                                        <button id="btnSalvarBancaQualificao" type="button" title="Salvar Qualificação" onclick="fSalvarDadosQualificacao();" class="btn btn-success pull-right"> 
                                                                                                                            <i class="fa fa-save"></i>&nbsp;Salvar Qualificação
                                                                                                                        </button>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="tab-pane" runat="server" id="tab_BancaQualificacao_Orientador">
                                                                                                        <%--Orientador Banca Qualificação--%>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <div class="panel panel-info">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2">
                                                                                                                                <h5 class="box-title text-bold">Orientador</h5>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-3">

                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2 ">
                                                                                                                                <span>CPF</span><br />
                                                                                                                                <input class="form-control input-sm" id="txtIdOrientadorBancaQualificacao" type="text" value="" readonly="true" style="display:none"/>
                                                                                                                                <input class="form-control input-sm" id="txtCpfOrientadorBancaQualificacao" type="text" value="" readonly="true"/>
                                                                                                                            </div>
                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                <br />
                                                                                                                            </div>

                                                                                                                            <div class="col-md-5 ">
                                                                                                                                <span>Nome</span><br />
                                                                                                                                <input class="form-control input-sm" id="txtNomeOrientadorBancaQualificacao" type="text" value="" readonly="true"/>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <br />
                                                                                                                        <div class="row">
                                                                                                                            <div id="divBotaoAlterarOrientadorBancaQualificacao" style="display:block">
                                                                                                                                <div class="col-md-3 center-block">
                                                                                                                                    <br />
                                                                                                                                    <button type="button" class="btn btn-info center-block" title="Atestado do Orientador" onclick="fImprimirAtestadoPre('Qualificação')">
                                                                                                                                        <i class="fa fa-file-pdf-o"></i>&nbsp;Atestado Orientador</button>
                                                                                                                                </div>

                                                                                                                                <div class="hidden-lg hidden-md">
                                                                                                                                    <br />
                                                                                                                                </div>

                                                                                                                                <div class="col-md-3 center-block">
                                                                                                                                    <br />
                                                                                                                                    <button type="button" class="btn btn-purple center-block" title="Convite do Orientador" onclick="fImprimirConvitePre('Qualificação')">
                                                                                                                                        <i class="fa fa-envelope-o"></i>&nbsp;Convite Orientador</button>
                                                                                                                                </div>
                                                                                                                                <div class="hidden-lg hidden-md">
                                                                                                                                    <br />
                                                                                                                                </div>

                                                                                                                                <div class="col-md-3 center-block">
                                                                                                                                    <br />
                                                                                                                                    <button id="btnAlterarOrientadorQualificacao" type="button" class="btn btn-primary center-block" title="Alterar Orientador" onclick="fModalAdicionaOrientadorBanca('Qualificação','Orientador')">
                                                                                                                                        <i class="fa fa-graduation-cap"></i>&nbsp;Alterar Orientador</button>
                                                                                                                                </div>
                                                                                                                            </div>

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="tab-pane" runat="server" id="tab_BancaQualificacao_Coorientador">
                                                                                                        <%--Co-Orientador Banca Qualificação (Temporário)--%>
                                                                                                        <div class="row" id="divCoorientadorQualificacaoTemporario" style="display:none">
                                                                                                            <div class="col-md-12">
                                                                                                                <div class="panel panel-info">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2">
                                                                                                                                <h5 class="box-title text-bold">Co-orientador</h5>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-3">

                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12 ">
                                                                                                                                <label id="lblCoorientadorQualificacaoTemporario"></label>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <%--Co-orientador(es) Banca Qualificação--%>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                        
                                                                                                                <div id="divCoorientadorBancaQualificacao" class="panel panel-info" style="display:block">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-3">
                                                                                                                                <h5 class="box-title text-bold">Co-orientador(es)</h5>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12">
                                                                                                                                <div class="grid-content">
                                                                                                                                    <div id="msgSemResultadosgrdCoorientadorBancaQualificacao" style="display:block">
                                                                                                                                        <div class="alert bg-gray">
                                                                                                                                            <asp:Label runat="server" ID="Label9" Text="Nenhum Co-orientador encontrado" />
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div id="divgrdCoorientadorBancaQualificacao" class="table-responsive" style="display:none">
                                                                                                                                        <div class="">
                                                                                                                                            <table id="grdCoorientadorBancaQualificacao" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                                                                    <tr>
                                                                                                
                                                                                                                                                    </tr>
                                                                                                                                                </thead>
                                                                                                                                            </table>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <br />
                                                                                                                        </div>
                                                                        
                                                                                                                        <div class="btn-group pull-right">
                                                                                                                            <button style="padding-right:2em" type="button" id="btnAdicionarCoOrientacaoBancaQualificacao" name="btnAdicionarCoOrientacaoBancaQualificacao" class="btn btn-primary" title="Adicionar Co-Orientador" onclick="fModalAdicionaOrientadorBanca('Qualificação','Coorientador')">
                                                                                                                                    <i class="fa fa-graduation-cap"></i>&nbsp;Adicionar Co-Orientador</button>
                                                                                                                        </div>                                                    
                                                                                
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="tab-pane" runat="server" id="tab_BancaQualificacao_Membro">
                                                                                                        <%--Membros Banca Qualificação--%>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                        
                                                                                                                <div id="divMembrosBancaQualificacao" class="panel panel-info" style="display:block">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2">
                                                                                                                                <h5 class="box-title text-bold">Membros</h5>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12">
                                                                                                                                <div class="grid-content">
                                                                                                                                    <div id="msgSemResultadosgrdMembrosBancaQualificacao" style="display:block">
                                                                                                                                        <div class="alert bg-gray">
                                                                                                                                            <asp:Label runat="server" ID="Label10" Text="Nenhum Menbro encontrado" />
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div id="divgrdMembrosBancaQualificacao" class="table-responsive" style="display:none">
                                                                                                                                        <div class="">
                                                                                                                                            <table id="grdMembrosBancaQualificacao" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                                                                    <tr>
                                                                                                
                                                                                                                                                    </tr>
                                                                                                                                                </thead>
                                                                                                                                            </table>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <br />
                                                                                                                        </div>
                                                                        
                                                                                                
                                                                                                
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-9">
                                                                                                                                <button type="button" id="btnAdicionarMembrosBancaQualificacao" name="btnAdicionarMembrosBancaQualificacao" class="btn btn-primary pull-right" title="Adicionar Membros" onclick="fModalAdicionaOrientadorBanca('Qualificação','Membro')">
                                                                                                                                        <i class="fa fa-graduation-cap"></i>&nbsp;Adicionar Membro</button>
                                                                                                        
                                                                                                                            </div>
                                                                                                                            <div class="col-md-3">
                                                                                                                                <button type="button" id="btnAdicionarMembrosSuplentesBancaQualificacao" name="btnAdicionarMembrosSuplentesBancaQualificacao" class="btn btn-info pull-right" title="Adicionar Membros (Suplente)" onclick="fModalAdicionaOrientadorBanca('Qualificação','Suplente')">
                                                                                                                                        <i class="fa fa-graduation-cap"></i>&nbsp;Adicionar Suplente</button>
                                                                                                                            </div>

                                                                                                    

                                                                                                                        </div>   
                                                                                
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="tab-pane" runat="server" id="tab_BancaDefesa">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="nav-tabs-custom">
                                                                                                <ul class="nav nav-tabs">
                                                                                                    <li id="tabBancaDefesa_Cadastro" runat="server" class="active"><a href="#<%=tab_BancaDefesa_Cadastro.ClientID %>" data-toggle="tab"><strong>Cadastro</strong></a></li>
                                                                                                    <li id="tabBancaDefesa_Orientador" runat="server" class=""><a href="#<%=tab_BancaDefesa_Orientador.ClientID %>" data-toggle="tab"><strong>Orientador</strong></a></li>
                                                                                                    <li id="tabBancaDefesa_Coorientador" runat="server" class=""><a href="#<%=tab_BancaDefesa_Coorientador.ClientID %>" data-toggle="tab"><strong>Co-orientador</strong></a></li>
                                                                                                    <li id="tabBancaDefesa_Membro" runat="server" class=""><a href="#<%=tab_BancaDefesa_Membro.ClientID %>" data-toggle="tab"><strong>Membros</strong></a></li>
                                                                                                    <li id="tabBancaDefesa_Dissertacao" runat="server" class=""><a href="#<%=tab_BancaDefesa_Dissertacao.ClientID %>" data-toggle="tab"><strong><label id ="lblDissertacao_TCC_1" class="negrito">Dissertação</label></strong></a></li>
                                                                                                </ul>

                                                                                                <div class="tab-content">
                                                                                                    <div class="tab-pane active" runat="server" id="tab_BancaDefesa_Cadastro">
                                                                                                        <%--Cadastro Banca Defesa--%>
                                                                                                        <div class="panel panel-info">
                                                                                                            <div class="panel-body">
                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-3">
                                                                                                                        <h5 class="box-title text-bold">Cadastro Defesa</h5>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <br />
                                                                                                                <div id="divEdicaoBancaDefesa" style="display:none">
                                                                                                                    <div class="row">
                                                                                                                        <div class="col-md-2 ">
                                                                                                                            <span>Data de Cadastro</span><br />
                                                                                                                            <input class="form-control input-sm" id="txtDataCadastroBancaDefesa" type="text" readonly="true"/>
                                                                                                                        </div>
                                                                                                                        <div class="hidden-lg hidden-md">
                                                                                                                            <br />
                                                                                                                        </div>

                                                                                                                        <div class="col-md-2 ">
                                                                                                                            <span>Última Alteração</span><br />
                                                                                                                            <input class="form-control input-sm" id="txtDataAlteracaoBancaDefesa" type="text" readonly="true"/>
                                                                                                                        </div>
                                                                                                                        <div class="hidden-lg hidden-md">
                                                                                                                            <br />
                                                                                                                        </div>

                                                                                                                        <div class="col-md-3 ">
                                                                                                                            <span>Alterado por</span><br />
                                                                                                                            <input class="form-control input-sm" id="txtResponsavelBancaDefesa" type="text" readonly="true"/>
                                                                                                                        </div>

                                                                                                                    </div>
                                                                                                                    <br />
                                                                                                                </div>

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>N.º Banca</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtNumeroBancaDefesa" type="text" value="" readonly="true"/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-3">
                                                                                                                        <span>Data</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtDataBancaDefesa" type="date" value=""/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>Hora</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtHoraBancaDefesa" type="time" value=""/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>
                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>Banca Remota</span><br />
                                                                                                                        <select id="ddlBancaDefesaRemota" class="form-control input-sm select2 SemPesquisa">
                                                                                                                            <option selected value="">Indicar se a Banca é remota</option>
                                                                                                                            <option value="1">Sim</option>
                                                                                                                            <option value="0">Não</option>
                                                                                                                        </select>
                                                                                                                    </div>

                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>Resultado</span><br />
                                                                                                                        <select id="ddlResultadoBancaDefesa" class="form-control input-sm select2 SemPesquisa">
                                                                                                                            <option selected value=""></option>
                                                                                                                            <option value="Aprovado">Aprovado</option>
                                                                                                                            <option value="Reprovado">Reprovado</option>
                                                                                                                        </select>
                                                                                                                    </div>

                                                                                                                </div>
                                                                                                                <br />

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-2">
                                                                                                                        <span>Nº Portaria MEC</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtNumeroPortariaMecBancaDefesa" type="text" value="" />
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-3">
                                                                                                                        <span>Data Portaria MEC</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtDataPortariaMecBancaDefesa" type="date" value=""/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-3">
                                                                                                                        <span>Data D.O.U.</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtDataDOUBancaDefesa" type="date" value=""/>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-3">
                                                                                                                        <span>Data Aprovação Orientador</span><br />
                                                                                                                        <input class="form-control input-sm" id="txtDataEntregaBancaDefesa" type="date" value=""/>
                                                                                                                    </div>

                                                                                                                </div>
                                                                                                                <br />

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-10">
                                                                                                                        <span>Título</span><br />
                                                                                                                        <textarea style ="resize:vertical;font-size:14px" class="form-control input-sm" rows="3" maxlength="500" id="txtTituloBancaDefesa"></textarea>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <br />

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-10">
                                                                                                                        <span>Observação</span><br />
                                                                                                                        <textarea style ="resize:vertical;font-size:14px" class="form-control input-sm" rows="3" maxlength="5500" id="txtObdervacaoBancaDefesa"></textarea>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <br />

                                                                                                                <div class="row">
                                                                                                                    <div class="col-md-2 pull-left">
                                                                                                                        <br />
                                                                                                                        <button id="btnImprimirDivulgacaoDefesa" type="button" title="Imprimir Divulgação da Defesa" onclick="fImprimirDivulgacao('Defesa');" class="btn btn-warning pull-left"> 
                                                                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Divulgação
                                                                                                                        </button>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-8 center-block">
                                                                                                                        <br />
                                                                                                                        <button id="btnImprimirAtaDefesa" type="button" title="Imprimir Ata da Qualificação" onclick="fAbreModalObsAta('Defesa');" class="btn btn-purple center-block"> 
                                                                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Ata
                                                                                                                        </button>
                                                                                                                    </div>
                                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                                        <br />
                                                                                                                    </div>

                                                                                                                    <div class="col-md-2 pull-right">
                                                                                                                        <br />
                                                                                                                        <button id="btnSalvarBancaDefesa" type="button" title="Salvar Defesa" onclick="fSalvarDadosDefesa();" class="btn btn-success pull-right"> 
                                                                                                                            <i class="fa fa-save"></i>&nbsp;Salvar Defesa
                                                                                                                        </button>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="tab-pane" runat="server" id="tab_BancaDefesa_Orientador">
                                                                                                        <%--Orientador Banca Defesa--%>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <div class="panel panel-info">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2">
                                                                                                                                <h5 class="box-title text-bold">Orientador</h5>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-3">

                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2 ">
                                                                                                                                <span>CPF</span><br />
                                                                                                                                <input class="form-control input-sm" id="txtIdOrientadorBancaDefesa" type="text" value="" readonly="true" style="display:none"/>
                                                                                                                                <input class="form-control input-sm" id="txtCpfOrientadorBancaDefesa" type="text" value="" readonly="true"/>
                                                                                                                            </div>
                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                <br />
                                                                                                                            </div>

                                                                                                                            <div class="col-md-5 ">
                                                                                                                                <span>Nome</span><br />
                                                                                                                                <input class="form-control input-sm" id="txtNomeOrientadorBancaDefesa" type="text" value="" readonly="true"/>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <br />
                                                                                                                        <div class="row">
                                                                                                                            <div id="divBotaoAlterarOrientadorBancaDefesa" style="display:block">
                                                                                                                                <div class="col-md-3 center-block">
                                                                                                                                    
                                                                                                                                    <button type="button" class="btn btn-info center-block" title="Atestado do Orientador" onclick="fImprimirAtestadoPre('Defesa')">
                                                                                                                                        <i class="fa fa-file-pdf-o"></i>&nbsp;Atestado Orientador</button>
                                                                                                                                </div>

                                                                                                                                <div class="hidden-lg hidden-md">
                                                                                                                                    <br />
                                                                                                                                </div>

                                                                                                                                <div class="col-md-3 center-block">
                                                                                                                                    
                                                                                                                                    <button type="button" class="btn btn-purple center-block" title="Convite do Orientador" onclick="fImprimirConvitePre('Defesa')">
                                                                                                                                        <i class="fa fa-envelope-o"></i>&nbsp;Convite Orientador</button>
                                                                                                                                </div>
                                                                                                                                <div class="hidden-lg hidden-md">
                                                                                                                                    <br />
                                                                                                                                </div>

                                                                                                                                <div class="col-md-3 center-block">
                                                                                                                                    
                                                                                                                                    <button id="btnAlterarOrientadorDefesa" type="button" class="btn btn-primary center-block" title="Alterar Orientador" onclick="fModalAdicionaOrientadorBanca('Defesa','Orientador')">
                                                                                                                                        <i class="fa fa-graduation-cap"></i>&nbsp;Alterar Orientador</button>
                                                                                                                                </div>
                                                                                                                            </div>

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="tab-pane" runat="server" id="tab_BancaDefesa_Coorientador">
                                                                                                        <%--Co-Orientador Banca Defesa (Temporário)--%>
                                                                                                        <div class="row" id="divCoorientadorDefesaTemporario" style="display:none">
                                                                                                            <div class="col-md-12">
                                                                                                                <div class="panel panel-info">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2">
                                                                                                                                <h5 class="box-title text-bold">Co-orientador</h5>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-3">

                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12 ">
                                                                                                                                <label id="lblCoorientadorDefesaTemporario"></label>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <%--Co-orientador(es) Banca Defesa--%>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                        
                                                                                                                <div id="divCoorientadorBancaDefesa" class="panel panel-info" style="display:block">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-3">
                                                                                                                                <h5 class="box-title text-bold">Co-orientador(es)</h5>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12">
                                                                                                                                <div class="grid-content">
                                                                                                                                    <div id="msgSemResultadosgrdCoorientadorBancaDefesa" style="display:block">
                                                                                                                                        <div class="alert bg-gray">
                                                                                                                                            <asp:Label runat="server" ID="Label11" Text="Nenhum Co-orientador encontrado" />
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div id="divgrdCoorientadorBancaDefesa" class="table-responsive" style="display:none">
                                                                                                                                        <div class="">
                                                                                                                                            <table id="grdCoorientadorBancaDefesa" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                                                                    <tr>
                                                                                                
                                                                                                                                                    </tr>
                                                                                                                                                </thead>
                                                                                                                                            </table>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <br />
                                                                                                                        </div>
                                                                        
                                                                                                                        <div class="btn-group pull-right">
                                                                                                                            <button style="padding-right:2em" type="button" id="btnAdicionarCoOrientacaoBancaDefesa" name="btnAdicionarCoOrientacaoBancaDefesa" class="btn btn-primary" title="Adicionar Co-Orientador" onclick="fModalAdicionaOrientadorBanca('Defesa','Coorientador')">
                                                                                                                                    <i class="fa fa-graduation-cap"></i>&nbsp;Adicionar Co-Orientador</button>
                                                                                                                        </div>                                                    
                                                                                
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="tab-pane" runat="server" id="tab_BancaDefesa_Membro">
                                                                                                        <%--Membros Banca Defesa (Temporário)--%>
                                                                                                        <div class="row" id="divMembrosDefesaTemporario" style="display:none">
                                                                                                            <div class="col-md-12">
                                                                                                                <div class="panel panel-info">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2">
                                                                                                                                <h5 class="box-title text-bold">Membros</h5>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-3">

                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12 ">
                                                                                                                                <label id="lblMembrosDefesaTemporario"></label>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <%--Membros Banca Defesa--%>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                        
                                                                                                                <div id="divMembrosBancaDefesa" class="panel panel-info" style="display:block">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-2">
                                                                                                                                <h5 class="box-title text-bold">Membros</h5>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12">
                                                                                                                                <div class="grid-content">
                                                                                                                                    <div id="msgSemResultadosgrdMembrosBancaDefesa" style="display:block">
                                                                                                                                        <div class="alert bg-gray">
                                                                                                                                            <asp:Label runat="server" ID="Label12" Text="Nenhum Menbro encontrado" />
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div id="divgrdMembrosBancaDefesa" class="table-responsive" style="display:none">
                                                                                                                                        <div class="">
                                                                                                                                            <table id="grdMembrosBancaDefesa" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                                                                    <tr>
                                                                                                
                                                                                                                                                    </tr>
                                                                                                                                                </thead>
                                                                                                                                            </table>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <br />
                                                                                                                        </div>
                                                                        
                                                                                                
                                                                                                
                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-9">
                                                                                                        
                                                                                                                                <button type="button" id="btnAdicionarMembrosSuplentesBancaDefesa" name="btnAdicionarMembrosSuplentesBancaDefesa" class="btn btn-info pull-right" title="Adicionar Membros (Suplente)" onclick="fModalAdicionaOrientadorBanca('Defesa','Suplente')">
                                                                                                                                        <i class="fa fa-graduation-cap"></i>&nbsp;Adicionar Suplente</button>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-3">
                                                                                                                                <button type="button" id="btnAdicionarMembrosBancaDefesa" name="btnAdicionarMembrosBancaDefesa" class="btn btn-primary pull-right" title="Adicionar Membros" onclick="fModalAdicionaOrientadorBanca('Defesa','Membro')">
                                                                                                                                        <i class="fa fa-graduation-cap"></i>&nbsp;Adicionar Membro</button>
                                                                                                                            </div>

                                                                                                                        </div>   
                                                                                
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="tab-pane" runat="server" id="tab_BancaDefesa_Dissertacao">
                                                                                                        <%--Dissertação Banca Defesa--%>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                        
                                                                                                                <div class="panel panel-info" style="display:block">
                                                                                                                    <div class="panel-body">
                                                                                                                        <div class="row ">
                                                                                                                            <div class="col-md-3">
                                                                                                                                <h5 class="box-title text-bold"><label class="negrito" id ="lblDissertacao_TCC_2">Dissertação</label></h5>
                                                                                                                                <label id="lblDissertacaoInformacao" class="text-red text-center piscante" style="display:none">Alterado (enviar para aprovação)</label>
                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        <div class="row">
                                                                                                                            <div class="col-md-12">

                                                                                                                                <div id="msgSemResultadosDissertacaoBancaDefesa" style="display: block">
                                                                                                                                    <div class="alert bg-gray">
                                                                                                                                        <asp:Label runat="server" ID="Label15" Text="Não há Data de Aprovação do Orientador para a Defesa." />
                                                                                                                                    </div>
                                                                                                                                </div>

                                                                                                                                <div id="divDissertacaoBancaDefesa" style="display: none">
                                                                                                                                    <div id="divEnviar_Aprovar_Reprovar" style="display:none">
                                                                                                                                        <div class ="row">
                                                                                                                                            <div class="col-md-6 text-center">
                                                                                                                                                <button type="button" id="btnEnviarAprovacaoOffLine" name="btnEnviarAprovacaoOffLine" class="btn btn-warning " style="display:none" onclick="fModalEnviarAprovacao('EnviarAprovacao')">
                                                                                                                                                    <i class="fa fa-mail-forward fa-lg"></i>&nbsp;Enviar para Aprovação
                                                                                                                                                </button>
                                                                                                                                                <button type="button" id="btnAprovarOffLine" name="btnAprovarOffLine" class="btn btn-success " style="display:none" onclick="fModalEnviarAprovacao('Aprovar')">
                                                                                                                                                    <i class="fa fa-thumbs-o-up fa-lg"></i>&nbsp;Aprovar
                                                                                                                                                </button>
                                                                                                                                            </div>
                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                <br />
                                                                                                                                            </div>

                                                                                                                                            <div class="col-md-6 text-center">
                                                                                                                                                <button type="button" id="btnReprovarOffLine" name="btnReprovarOffLine" class="btn btn-danger " style="display:none" onclick="fModalEnviarAprovacao('Reprovar')">
                                                                                                                                                    <i class="fa fa-thumbs-o-down fa-lg"></i>&nbsp;Reprovar
                                                                                                                                                </button>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                        <br />
                                                                                                                                    </div>

                                                                                                                                    <div id="divEdicaoDissertacaoBancaDefesa" style="display: none">
                                                                                                                                        <div class="row">
                                                                                                                                            <div class="col-md-2 ">
                                                                                                                                                <span>Data de Cadastro</span><br />
                                                                                                                                                <input class="form-control input-sm" id="txtDataCadastroDissertacaoBancaDefesa" type="text" readonly="true" />
                                                                                                                                            </div>
                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                <br />
                                                                                                                                            </div>

                                                                                                                                            <div class="col-md-2 ">
                                                                                                                                                <span>Última Alteração</span><br />
                                                                                                                                                <input class="form-control input-sm" id="txtDataAlteracaoDissertacaoBancaDefesa" type="text" readonly="true" />
                                                                                                                                            </div>
                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                <br />
                                                                                                                                            </div>

                                                                                                                                            <div class="col-md-3 ">
                                                                                                                                                <span>Alterado por</span><br />
                                                                                                                                                <input class="form-control input-sm" id="txtResponsavelDissertacaoBancaDefesa" type="text" readonly="true" />
                                                                                                                                            </div>
                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                <br />
                                                                                                                                            </div>

                                                                                                                                            <div class="col-md-2">
                                                                                                                                                <span>Qtd visitas</span><br />
                                                                                                                                                <input class="form-control input-sm" id="txtVisitaDissertacaoBancaDefesa" type="number" readonly="true" value="0" min="0" />
                                                                                                                                            </div>
                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                <br />
                                                                                                                                            </div>

                                                                                                                                            <div class="col-md-2">
                                                                                                                                                <span>Qtd <em>downloads</em></span><br />
                                                                                                                                                <input class="form-control input-sm" id="txtDownloadDissertacaoDefesa" type="number" readonly="true" value="0" min="0" />
                                                                                                                                            </div>

                                                                                                                                        </div>
                                                                                                                                        <br />
                                                                                                                                    </div>

                                                                                                                                    <div class="row ">
                                                                                                                                        <div class="col-lg-12">
                                                                                                                                            <div class="nav-tabs-custom">
                                                                                                                                                <ul class="nav nav-tabs">
                                                                                                                                                    <li class="active"><a href="#tab_PreviewDissertacao" data-toggle="tab" aria-expanded="true">Preview</a></li>
                                                                                                                                                    <li id="tabPublicadoDissertacao" runat="server"><a href="#tab_PublicadoDissertacao" data-toggle="tab" aria-expanded="false">Publicado</a></li>
                                                                                                                                                </ul>

                                                                                                                                                <div class="tab-content">
                                                                                                                                                    <div class="tab-pane active" id="tab_PreviewDissertacao">
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-3">
                                                                                                                                                                <h5 class="box-title text-bold">Preview</h5>
                                                                                                                                                            </div>
                                                                                                                                                        </div>

                                                                                                                                                        <%--Palavra Chave e Cod IPT--%>
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-7">
                                                                                                                                                                <span>Palavras-chave</span><br />
                                                                                                                                                                <input class="form-control input-sm" id="txtPalavraChaveDissertacaoDefesa_Preview" type="text" value="" maxlength="2000" />
                                                                                                                                                            </div>
                                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                                <br />
                                                                                                                                                            </div>

                                                                                                                                                            <div class="col-md-4">
                                                                                                                                                                <span>Cod. IPT</span><br />
                                                                                                                                                                <input class="form-control input-sm" id="txtCodIPT_Preview" type="text" value="" maxlength="40" />
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <br />

                                                                                                                                                        <%--Resumo--%>
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-12">
                                                                                                                                                                <span>Resumo</span><br />
                                                                                                                                                                <textarea style="resize: vertical; font-size: 14px" class="form-control input-sm" rows="10" id="txtResumoDissertacaoBancaDefesa_Preview"></textarea>
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <br />

                                                                                                                                                        <%--Arquivo PDF--%>
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-6">
                                                                                                                                                                <span>Arquivo PDF</span><br />
                                                                                                                                                                <input class="form-control input-sm" id="txtArquivoDissertacaoBancaDefesa" type="text" value="" readonly="true" />
                                                                                                                                                            </div>
                                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                                <br />
                                                                                                                                                            </div>

                                                                                                                                                            <div class="col-md-2">
                                                                                                                                                                <span></span>
                                                                                                                                                                <br />
                                                                                                                                                                <button type="button" style="display: none" id="btnLocalizarDissertacaoBancaDefesa" name="btnLocalizarDissertacaoBancaDefesa" runat="server" class="btn btn-info" title="Localizar Dissertação" onclick="javascript:fLocalizarDissertacaoBancaDefesa()">
                                                                                                                                                                    <i class="fa fa-search fa-lg"></i>&nbsp;Localizar PDF
                                                                                                                                                                </button>
                                                                                                                                                            </div>
                                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                                <br />
                                                                                                                                                            </div>

                                                                                                                                                            <%--Botão Salvar Dissertação--%>
                                                                                                                                                            <div class="col-md-3">
                                                                                                                                                                <br />
                                                                                                                                                                <div class="btn-group pull-right">
                                                                                                                                                                    <button type="button" style="display: none" id="btnSalvarDissertacaoBancaDefesa" name="btnSalvarDissertacaoBancaDefesa" runat="server" class="btn btn-success" title="Salvar Dissertação" onclick="fSalvarDissertacaoBancaDefesaAluno()">
                                                                                                                                                                        <i class="fa fa-save fa-lg"></i>&nbsp;<label id="lblBtnSalvarDissertacao"> Salvar Dissertação</label></button>
                                                                                                                                                                </div>
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <br />

                                                                                                                                                        <div id="divHistoricoObservacao" style="display:none" >
                                                                                                                                                            <br />
                                                                                                                                                            <div class="row">
                                                                                                                                                                <div class="col-md-12 ">
                                                                                                                                                                    <span>Histórico de Observações</span><br />

                                                                                                                                                                    <div class="row">
                                                                                                                                                                        <div class="col-md-12">
                                                                                                                                                                            <div class="grid-content">
                                                                                                                                                                                <div id="lblMsgSemResultadosgrdHistoricoDissertacao" style="display:none">
                                                                                                                                                                                    <div class="alert bg-gray">
                                                                                                                                                                                        <label>Nenhuma Observação encontrada</label>
                                                                                                                                                                                    </div>
                                                                                                                                                                                </div>
                                                                                                                                                                                
                                                                                                                                                                                <div id="divgrdHistoricoDissertacao" class="table-responsive" style="display: none">
                                                                                                                                                                                    <div class="scroll">
                                                                                                                                                                                        <table id="grdHistoricoDissertacao" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%">
                                                                                                                                                                                            <thead style="color: White; background-color: #507CD1; font-weight: bold;">
                                                                                                                                                                                                <tr>
                                                                                                                                                                                                </tr>
                                                                                                                                                                                            </thead>
                                                                                                                                                                                        </table>
                                                                                                                                                                                    </div>
                                                                                                                                                                                </div>

                                                                                                                                                                            </div>
                                                                                                                                                                        </div>
                                                                                                                                                                    </div>

                                                                                                                                                                </div>
                                                                                                                                                            </div>
                                                                                                                                                        </div>

                                                                                                                                                        <div id="divDissertacaoPublicacaoPreview" style="display:none">
                                                                                                                                                            <br />
                                                                                                                                                            <hr />
                                                                                                                                                            <br />
                                                                                                                                                            <h5>Exemplo <em>Preview</em></h5>

                                                                                                                                                            <div class="panel panel-success">
                                                                                                                                                                <div class="panel-heading" role="tab">
                                                                                                                                                                    <h5 class="panel-title"><a class="a_faq collapsed" id="cab_0" data-toggle="collapse" href="#res_0" aria-expanded="false">
                                                                                                                                                                        <h6><i class="fa fa-square" style="color: #3588CC"></i> <span id="lblDissertacao_tipocurso_preview">Engenharia de C...</span></h6>
                                                                                                                                                                        <h5 style="line-height: 1.5em"><strong><span id="lblDissertacao_titulo_preview">Metodologia de análise big data aplicada a dados ...</span></strong></h5>
                                                                                                                                                                        <i id="icab_0" style="margin-top: -25px; color: #3588CC" class="fa fa-chevron-left pull-right rotate"></i>
                                                                                                                                                                        <h6>por <strong><span id="lblDissertacao_aluno_preview">NETO, Jayro D...</span></strong></h6>
                                                                                                                                                                    </a></h5>
                                                                                                                                                                </div>
                                                                                                                                                                <div id="res_0" class="panel-collapse collapse" role="tabpanel" aria-labelledby="cab_0" aria-expanded="false" style="height: 0px;">
                                                                                                                                                                    <div class="panel-body">
                                                                                                                                                                        <p style="font-size: 1.3rem; line-height: 1.5em">Orientação: <strong><span id="lblDissertacao_orientador_preview">NETO, adriano C...</span></strong><br />
                                                                                                                                                                            <br />
                                                                                                                                                                        </p>
                                                                                                                                                                        <div class="row">
                                                                                                                                                                            <div class="col-xs-6">Ano: <strong><span id="lblDissertacao_ano_preview">202..</span></strong></div>
                                                                                                                                                                            <div class="col-xs-6 text-right">visualizações: <strong>
                                                                                                                                                                                <label id="lblDissertacao_visualizacoes_preview">2...</label></strong></div>
                                                                                                                                                                            <div class="col-xs-12 text-right"><em>downloads</em>: <strong>
                                                                                                                                                                                <label id="lblDissertacao_downloads_preview">0...</label></strong></div>
                                                                                                                                                                        </div>

                                                                                                                                                                        <br />
                                                                                                                                                                        <span id="lblDissertacao_resumo_preview">Aqui é o texto blá..</span>               
                                                                                                                                                                        <br />
                                                                                                                                                                        <br />
                                                                                                                                                                        <a id="aArquivo_preview" class="btn btn-primary" target="_blank" onclick="fGoToDownload(0,3139)" href="Teses\null"><b>Faça aqui o <em>download</em> da dissertação</b></a>
                                                                                                                                                                        <p></p>
                                                                                                                                                                    </div>
                                                                                                                                                                </div>
                                                                                                                                                            </div>

                                                                                                                                                        </div>
                                                                                                                                                    </div>
                                                                                                                                                    <!-- /.tab-pane -->
                                                                                                                                                    <div class="tab-pane" id="tab_PublicadoDissertacao">
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-3">
                                                                                                                                                                <h5 class="box-title text-bold">Publicado</h5>
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <br />

                                                                                                                                                        <%--Palavra Chave e Cod IPT--%>
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-2 ">
                                                                                                                                                                <span>Data de Aprovação</span><br/>
                                                                                                                                                                <input name="txtDataAprovacaoDissertacao" type="text" id="txtDataAprovacaoDissertacao" class="form-control input-sm" readonly="true" value=""/>
                                                                                                                                                            </div>
                                                                                                                                                            
                                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                                <br/>
                                                                                                                                                            </div>

                                                                                                                                                            <div class="col-md-3 ">
                                                                                                                                                                <span>Responsável</span><br/>
                                                                                                                                                                <input name="txtusuarioAprovacaoDissertacao" type="text" id="txtusuarioAprovacaoDissertacao" class="form-control input-sm" readonly="true" value=""/>
                                                                                                                                                            </div>

                                                                                                                                                        </div>
                                                                                                                                                        
                                                                                                                                                        <br />

                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-6">
                                                                                                                                                                <span>Palavras-chave</span><br />
                                                                                                                                                                <input class="form-control input-sm" id="txtPalavraChaveDissertacaoDefesa" type="text" value="" maxlength="2000" readonly="true" />
                                                                                                                                                            </div>
                                                                                                                                                            <div class="hidden-lg hidden-md">
                                                                                                                                                                <br />
                                                                                                                                                            </div>

                                                                                                                                                            <div class="col-md-2">
                                                                                                                                                                <span>Cod. IPT</span><br />
                                                                                                                                                                <input class="form-control input-sm" id="txtCodIPT" type="text" value="" maxlength="40" readonly="true" />
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <br />

                                                                                                                                                        <%--Resumo--%>
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-12">
                                                                                                                                                                <span>Resumo</span><br />
                                                                                                                                                                <textarea style="resize: vertical; font-size: 14px" class="form-control input-sm" rows="10" id="txtResumoDissertacaoBancaDefesa" readonly="true"></textarea>
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <br />

                                                                                                                                                        <%--Arquivo PDF--%>
                                                                                                                                                        <div class="row">
                                                                                                                                                            <div class="col-md-8">
                                                                                                                                                                <span>Arquivo PDF</span><br />
                                                                                                                                                                <input class="form-control input-sm" id="txtArquivoPDFDissertacaoPublicado" type="text" value="" readonly="true" />
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <br />


                                                                                                                                                        <div id="divDissertacaoPublicacaoPublicado" style="display:none">
                                                                                                                                                            <br />
                                                                                                                                                            <hr />
                                                                                                                                                            <br />
                                                                                                                                                            <h5>Exemplo Publicado</h5>

                                                                                                                                                            <div class="panel panel-success">
                                                                                                                                                                <div class="panel-heading" role="tab">
                                                                                                                                                                    <h5 class="panel-title"><a class="a_faq collapsed" id="cab_1" data-toggle="collapse" href="#res_1" aria-expanded="false">
                                                                                                                                                                        <h6><i class="fa fa-square" style="color: #3588CC"></i> <span id="lblDissertacao_tipocurso_publicado">Engenharia de C...</span></h6>
                                                                                                                                                                        <h5 style="line-height: 1.5em"><strong><span id="lblDissertacao_titulo_publicado">Metodologia de análise big data aplicada a dados ...</span></strong></h5>
                                                                                                                                                                        <i id="icab_1" style="margin-top: -25px; color: #3588CC" class="fa fa-chevron-left pull-right rotate"></i>
                                                                                                                                                                        <h6>por <strong><span id="lblDissertacao_aluno_publicado">NETO, Jayro D...</span></strong></h6>
                                                                                                                                                                    </a></h5>
                                                                                                                                                                </div>
                                                                                                                                                                <div id="res_1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="cab_1" aria-expanded="false" style="height: 0px;">
                                                                                                                                                                    <div class="panel-body">
                                                                                                                                                                        <p style="font-size: 1.3rem; line-height: 1.5em">Orientação: <strong><span id="lblDissertacao_orientador_publicado">NETO, adriano C...</span></strong><br />
                                                                                                                                                                            <br />
                                                                                                                                                                        </p>
                                                                                                                                                                        <div class="row">
                                                                                                                                                                            <div class="col-xs-6">Ano: <strong><span id="lblDissertacao_ano_publicado">202..</span></strong></div>
                                                                                                                                                                            <div class="col-xs-6 text-right">visualizações: <strong>
                                                                                                                                                                                <label id="lblDissertacao_visualizacoes_publicado">2...</label></strong></div>
                                                                                                                                                                            <div class="col-xs-12 text-right"><em>downloads</em>: <strong>
                                                                                                                                                                                <label id="lblDissertacao_downloads_publicado">0...</label></strong></div>
                                                                                                                                                                        </div>

                                                                                                                                                                        <br />
                                                                                                                                                                        <span id="lblDissertacao_resumo_publicado">Aqui é o texto blá..</span>               
                                                                                                                                                                        <br />
                                                                                                                                                                        <br />
                                                                                                                                                                        <a id="aArquivo_publicado" class="btn btn-primary" target="_blank" onclick="fGoToDownload(0,3139)" href="Teses\null"><b>Faça aqui o <em>download</em> da dissertação</b></a>
                                                                                                                                                                        <p></p>
                                                                                                                                                                    </div>
                                                                                                                                                                </div>
                                                                                                                                                            </div>

                                                                                                                                                        </div>
                                                                                                                                                        
                                                                                                                                                    </div>
                                                                                                                                                    <!-- /.tab-pane -->
                                                                                                                                                </div>
                                                                                                                                                <!-- /.tab-content -->
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        
                                                                                                                        
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            </div>
                                                                    </div>
                                                                </div>
        
                                                            </div>

                                                            <%--Reuniões CPG--%>
                                                            <div class="tab-pane" runat="server" id="tab_ProrrogacaoCPG">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="grid-content">
                                                                                            <div id="msgSemResultadosgrdProrrogacaoCPG" style="display:block">
                                                                                                <div class="alert bg-gray">
                                                                                                    <asp:Label runat="server" ID="Label13" Text="Nenhum cadastro de Reunião encontrado." />
                                                                                                </div>
                                                                                            </div>

                                                                                            <div id="divgrdProrrogacaoCPG" class="table-responsive" style="display:none">
                                                                                                <div class="scroll">
                                                                                                    <table id="grdProrrogacaoCPG" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%" >
                                                                                                        <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                                            <tr>
                                                                                                
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>  
                                                                    </div>
                                                                </div>

                                                                <div class="row">

                                                                    <div class="col-md-4 pull-right">
                                                                        <button type="button" id="btnIncluirProrrogacaoCPG" runat="server" name="btnIncluirProrrogacaoCPG" class="btn btn-info pull-right" onclick="fModalIncluirProrrogacaoCPG()">
                                                                            <i class="fa fa-users"></i>&nbsp;Incluir Reunião CPG</button>
                                                                    </div>
                                                                </div>  

                                                            </div>

                                                            <%--Contrato--%>
                                                            <div class="tab-pane" runat="server" id="tab_Contrato">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                <h4>Dados do Contrato</h4>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-4 ">
                                                                                <span>Contrato</span><br />
                                                                                <select id="ddlTipoContrato" name="ddlTipoContrato" class="form-control input-sm select2 SemPesquisa">
                                                                                </select>

                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-3 ">
                                                                                <span>Data</span><br />
                                                                                <input class="form-control input-sm" id="txtDataContrato" name="txtDataContrato" type="date" value=""/>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-2 ">
                                                                                <span>Valor Total</span><br />
                                                                                <input class="form-control input-sm" id="txtValorTotal" name="txtValorTotal" type="text" value=""/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2 ">
                                                                                <span>Valor Disciplina</span><br />
                                                                                <input class="form-control input-sm" id="txtValorDisciplina" name="txtValorDisciplina" type="text" value=""/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2 ">
                                                                                <span>Nº de Parcelas</span><br />
                                                                                <input class="form-control input-sm" id="txtNumeroParcela" name="txtNumeroParcela" type="number" value=""/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2 ">
                                                                                <span>Valor Parcela</span><br />
                                                                                <input class="form-control input-sm" id="txtValorParcela" name="txtValorParcela" type="text" value=""/>
                                                                            </div>

                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-3 ">
                                                                                <span>Início Curso</span><br />
                                                                                <input class="form-control input-sm" id="txtDataInicioCurso" name="txtDataInicioCurso" type="date" value=""/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2 ">
                                                                                <span>Prazo <small>(em meses)</small> </span><br />
                                                                                <input class="form-control input-sm" id="txtPrazo" name="txtPrazo" type="number" value=""/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-5 ">
                                                                                <span>Coordenador</span><br />
                                                                                <input class="form-control input-sm" id="txtCoordenador" name="txtCoordenador" type="text" value="" maxlength="300"/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-5 ">
                                                                                <span>Secretária</span><br />
                                                                                <input class="form-control input-sm" id="txtSecretaria" name="txtSecretaria" type="text" value="" maxlength="300"/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-5 ">
                                                                                <span>Testemunha #1</span><br />
                                                                                <input class="form-control input-sm" id="txtTextemunha1" name="txtTextemunha1" type="text" value="" maxlength="300"/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-3 ">
                                                                                <span>RG #1</span><br />
                                                                                <input class="form-control input-sm" id="txtRGTextemunha1" name="txtRGTextemunha1" type="text" value="" onkeypress="return fValidaRG(event)"/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-5 ">
                                                                                <span>Testemunha #2</span><br />
                                                                                <input class="form-control input-sm" id="txtTextemunha2" name="txtTextemunha2" type="text" value="" maxlength="300"/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-3 ">
                                                                                <span>RG #2</span><br />
                                                                                <input class="form-control input-sm" id="txtRGTextemunha2" name="txtRGTextemunha2" type="text" value="" onkeypress="return fValidaRG(event)"/>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                        </div>
                                                                        <br />

                                                                        <div class ="row">
                                                                            <div class="col-md-10">
                                                                                <span>Parágrafo do Diretor</span><br />
                                                                                <textarea style="resize: vertical;font-size:14px" class="form-control input-sm" rows="4" id="txtParagrafoDiretor" name="txtParagrafoDiretor"></textarea><br />
                                                                                <span>Por exemplo: <strong>seu diretor, Fúlvio Vittorino, brasileiro, casado, Engenheiro Mecânico, Pesquisador, portador de Cédula de Identidade RG nº. 16.978.877, com endereço especial no local acima indicado, doravante designada simplesmente FIPT, na forma, cláusulas e condições abaixo:</strong></span>
                                                                            </div>
                                                                        </div>
                                                                          
                                                                    </div>
                                                                </div>

                                                                <div class="row">

                                                                    <div class="col-md-4 pull-right">
                                                                        <button type="button" class="btn btn-warning pull-right" onclick="fImprimirContrato()">
                                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Contrato</button>
                                                                    </div>
                                                                </div>  

                                                            </div>

                                                            <%--Certificado de Titulação--%>
                                                            <div class="tab-pane" runat="server" id="tab_Certificado">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                <div id="msgSemResultadosCertificado" style="display:block">
                                                                                    <div class="alert bg-gray">
                                                                                        <asp:Label runat="server" ID="Label14" Text="O aluno ainda não titulou." />
                                                                                    </div>
                                                                                </div>

                                                                                <div id="divBotaoCertificado" class="center-block" style="display:block">
                                                                                    <br />
                                                                                    <button type="button" runat="server" id="btnCertificado" name="btnCertificado" class="btn btn-info center-block" onclick="fModalCertificado()" >
                                                                                        <i class="fa fa-print"></i>&nbsp;Imprimir Certificado de Titulação</button>
                                                                                    <br />
                                                                                    <button type="button" runat="server" id="btnCertificadoCurta" name="btnCertificadoCurta" class="btn btn-info center-block" onclick="fModalCertificadoCurta()" >
                                                                                        <i class="fa fa-print"></i>&nbsp;Imprimir Certificado de Curta Duração</button>
                                                                                    <br />
                                                                                    <button type="button" runat="server" id="btnCertificadoEspecializacao" name="btnCertificadoEspecializacao" class="btn btn-info center-block" onclick="fModalCertificadoEspecializacao()" >
                                                                                        <i class="fa fa-print"></i>&nbsp;Imprimir Certificado de Especialização</button>
                                                                                    <br />
                                                                                    <button type="button" runat="server" id="btnCertificadoMBAInternacional" name="btnCertificadoMBAInternacional" class="btn btn-info center-block" onclick="fModalCertificadoMBAInternacional()" >
                                                                                        <i class="fa fa-print"></i>&nbsp;Imprimir Certificado de MBA Internacional</button>
                                                                                    <br />
                                                                                </div>
                                                                            </div>
                                                                        </div>  
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="box-footer">
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>

                </div>
            </div>
                </div>

            <br />

            <div class="row">
                <div class="pull-left">
                    <div class="col-md-12">

                        <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onserverclick="btnVoltar_ServerClick" > <%--onclick="window.history.back()"--%>
                            <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>

                        <button type="button" runat="server" id="btnImprimirHitorico" name="btnImprimirHitorico" class="btn btn-warning hidden " href="#" onclick="" onserverclick="btnImprimir_Click" >
                                                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico</button>

                        <button type ="button" runat="server" id="btnImprimirHitoricoOficial" name="btnImprimirHitorico" class="btn btn-success hidden" onserverclick="btnImprimirOficial_Click">
                                                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico Oficial</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <!-- Modal para Associar Orientador -->
    <div class="modal fade" id="divModalSelecionarOrientador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-graduation-cap"></i>&nbsp;<label id="lblTituloModalSelecionarOrientador"></label> </h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-filter"></i>&nbsp;Filtro</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                    
                                        <div class="col-md-2">
                                            <span>CPF</span><br />
                                            <input class="form-control input-sm" id="txtCPFOrientadorPesquisa" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeOrientadorPesquisa" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button id="btnPerquisaOrientadorDisponivel" style="display:none" type="button" title="" class="btn btn-success" onclick="fPerquisaOrientadorDisponivel()" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaCoorientadorDisponivel" style="display:none" type="button" title="" class="btn btn-success" onclick="fPerquisaCoorientadorDisponivel()" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>
                                        </div>

                                    </div>
                                    <br />
                                </div>

                            </div>

                        </div>
                        <br />

                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                <div id="msgSemResultadosgrdOrientadorDisponivel" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <asp:Label runat="server" ID="Label8" Text="Nenhum Orientador disponível encontrado" />
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdOrientadorDisponivel" >
                                                    <div class="scroll">
                                                        <table id="grdOrientadorDisponivel" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%">
                                                            <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                            
                                                                <tr>
                                                               
                                                                </tr>
                                                            </thead>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

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

    <!-- Modal para Associar Orientador -->
    <div class="modal fade" id="divModalSelecionarBanca" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-graduation-cap"></i>&nbsp;<label id="lblTituloModalSelecionarBanca"></label> </h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-filter"></i>&nbsp;Filtro</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                    
                                        <div class="col-md-2">
                                            <span>CPF</span><br />
                                            <input class="form-control input-sm" id="txtCPFBancaPesquisa" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeBancaPesquisa" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button id="btnPerquisaOrientadorDisponivelBancaQualificacao" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Qualificação','Orientador')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaCoorientadorDisponivelBancaQualificacao" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Qualificação','Co-orientador')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaMembroDisponivelBancaQualificacao" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Qualificação','Membro')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaSuplenteDisponivelBancaQualificacao" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Qualificação','Membro Suplente')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaOrientadorDisponivelBancaDefesa" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Defesa','Orientador')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaCoorientadorDisponivelBancaDefesa" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Defesa','Co-orientador')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaMembroDisponivelBancaDefesa" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Defesa','Membro')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                                            <button id="btnPerquisaSuplenteDisponivelBancaDefesa" style="display:none" type="button" title="" class="btn btn-success" onclick="fPesquisaBancaDisponivel('Defesa','Membro Suplente')" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>
                                        </div>

                                    </div>
                                    <br />
                                </div>

                            </div>

                        </div>
                        <br />

                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                <div id="msgSemResultadosgrdBancaDisponivel" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <asp:Label runat="server" ID="Label1" Text="Nenhum Professor disponível encontrado" />
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdBancaDisponivel" >
                                                    <div class="scroll">
                                                        <table id="grdBancaDisponivel" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%">
                                                            <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                            
                                                                <tr>
                                                               
                                                                </tr>
                                                            </thead>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

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

    <%--Aqui são os botões escondidos do FileUpLoad--%>

    <a id="adivModal" class="preto" data-toggle="modal" href="#divModal" style:"display: none;" ></a>

    <button type="button" runat="server" id="btnImprimirAtestado" class="btn btn-default center-block hidden" href="#"  onserverclick="btnImprimirAtestado_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
        <i class="fa fa-print"></i>&nbsp;Imprimir Atestado
    </button>

    <button type="button" runat="server" id="btnImprimirRecibo" class="btn btn-default center-block hidden" href="#"  onserverclick="btnImprimirRecibo_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
        <i class="fa fa-print"></i>&nbsp;Imprimir Recibo
    </button>

    <button type="button" runat="server" id="btnImprimirConvite" class="btn btn-default center-block hidden" href="#"  onserverclick="btnImprimirConvite_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
        <i class="fa fa-print"></i>&nbsp;Imprimir Convite
    </button>

    <button type="button" runat="server" id="btnImprimirDivulgacao" class="btn btn-default center-block hidden" href="#"  onserverclick="btnImprimirDivulgacao_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
        <i class="fa fa-print"></i>&nbsp;Imprimir Divulgacao
    </button>

    <button type="button" runat="server" id="btnImprimirAta" class="btn btn-default center-block hidden" href="#"  onserverclick="btnImprimirAta_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
        <i class="fa fa-print"></i>&nbsp;Imprimir Ata
    </button>

    <button type="button" runat="server" id="btnImprimirContrato" class="btn btn-default center-block hidden" href="#" onserverclick="btnImprimirContrato_Click">
        <i class="fa fa-print"></i>&nbsp;Imprimir Contrato
    </button>

    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
    
    <!-- Modal para editar documetnos obrigatórios -->
    <div class="modal fade" id="divModalPreencheDocumentosObrigatorios" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header alert-info">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="">
                        <div class="row text-center">
                            <span class="text-center"><label id="lblTituloDocumentosArquivo"></label></span><br />
                        </div>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-9">
                            <span>Documento</span><br />
                            <input class="form-control input-sm" id="txtDescricaoDocumentoObrigatorio" type="text" value="" maxlength="200" readonly/>
                            <input class="form-control input-sm" id="txtIdDocumentoObrigatorio" type="text" value="" style="display:none"/>
                            <input class="form-control input-sm" id="txtIdTipoDocumento" type="text" value="" style="display:none"/>
                        </div>
                        </div>
                        <br />
                    <div class="row">
                        <div class="col-xs-9">
                            <span>Arquivo</span><br />
                            <input class="form-control input-sm" id="txtArquivoDocumentoObrigatorio" type="text" value="" maxlength="200" readonly/>
                        </div>
                        <div class="col-xs-3">
                            <br />
                            <button id="btnArquivoDocumentosObrigatorios" type="button" class="btn btn-success pull-right" onclick="javascript:document.getElementById('<%=fileArquivo.ClientID%>').click();">
                            <i class="fa fa-upload"></i>&nbsp;Selecionar arquivo</button>
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
                            <button id="btnPreencheDocumentosObrigatorios" type="button" class="btn btn-success pull-right" onclick="fSalvarDocumento('1')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnPreencheDocumentosNaoObrigatorios" type="button" class="btn btn-success pull-right" onclick="fSalvarDocumento('2')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->

    <!-- Modal para Excluir dados Orientação -->
    <div class="modal fade" id="divModalApagarDadosOrientacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Excluir dados da Orientação</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir Todos os dados da Orientação do aluno nessa turma?
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button type="button" title="" class="btn btn-success" onclick="fApagarDadosOrientacao()" >
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;Confirmar</button>
                        </div>
                        <div class="hidden-md hidden-lg">
                            <br />
                        </div>

                        <div class="col-md-2 pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    <i class="fa fa-close"></i>&nbsp;Fechar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

     <!-- Modal para Excluir dados Orientação -->
    <div class="modal fade" id="divModalExcluirCoorientador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Excluir Professor Co-orientador</h4>
                </div>
                <div class="modal-body">
                    <label id="lblIdCoorientador" style="display:none"> </label>
                    Deseja excluir o Co-orientador <label id="lblNomeCoorientador" class="negrito"></label>?
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button type="button" title="" class="btn btn-success" onclick="fExcluirCoorientador()" >
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;Confirmar</button>
                        </div>
                        <div class="hidden-md hidden-lg">
                            <br />
                        </div>

                        <div class="col-md-2 pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    <i class="fa fa-close"></i>&nbsp;Fechar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Excluir Professores Banca -->
    <div class="modal fade" id="divModalExcluirMembroBanca" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Excluir Professor <label id="lblTipoProfessorBanca"></label></h4>
                </div>
                <div class="modal-body">
                    <label id="lblIdBanca" style="display:none"> </label>
                    <label id="lblIdProfessorBanca" style="display:none"> </label>
                    Deseja excluir o <label id="lblTipoProfessorBanca2"></label> <label id="lblNomeProfessorBanca" class="negrito"></label> da banca de <label id="lblTipoBanca"></label>?
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button type="button" title="" class="btn btn-success" onclick="fExcluirProfessorBanca()" >
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;Confirmar</button>
                        </div>
                        <div class="hidden-md hidden-lg">
                            <br />
                        </div>

                        <div class="col-md-2 pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    <i class="fa fa-close"></i>&nbsp;Fechar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Inserir Situação -->
    <div class="modal fade" id="divModalEdiarSituacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div id="divCabecSituacao" class="modal-header arrastavel">
                    <h4 class="modal-title"><label id="lblTituloEdiarSituacao"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <input class="form-control input-sm" id="txtIdHIstoricoSituacao" type="text" value="" style="display:none"/>
                        <div class="row">
                            <div class="col-md-3">
                                <span>Status</span><br />
                                <select id="ddlStatusSituacao" class="form-control input-sm select2 SemPesquisa ddlStatusSituacao">
                                    <option value="Especial">Especial</option>
                                    <option selected value="Regular">Regular</option>
                                </select>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-3">
                                <span>Situação</span><br />
                                <input class="form-control input-sm" id="txtNomeSituacao" type="text" value="" readonly="true"/>
                                <div id="divddlNomeSituacao">
                                    <select id="ddlNomeSituacao" class="form-control input-sm select2 SemPesquisa">
                                        <option selected value="">Selecione</option>
                                        <option value="Matriculado">Matriculado</option>
                                        <option value="Trancado">Trancado</option>
                                        <%--<option disabled value="Prorrogação CPG">Prorrogação CPG</option>--%>
                                        <option id="Desligado" value="Desligado">Desligado</option>
                                        <option value="Abandonou">Abandonou</option>
                                        <option value="Qualificação">Qualificação</option>
                                        <option id="Titulado" value="Titulado">Titulado</option>
                                    </select>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>
                            <div id="divDataInicio">
                                <div class="col-md-3">
                                    <span>Data Início</span><br />
                                    <input class="form-control input-sm" id="txtDataInicioSituacao" type="date" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md"> 
                                    <br />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <span>Data Fim</span><br />
                                <input class="form-control input-sm" id="txtDataFimSituacao" type="date" value=""/>
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
                            <button id="btnConfirmaInsereSituacao" type="button" class="btn btn-success pull-right" onclick="fInserirSituacao()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaEditaSituacao" type="button" class="btn btn-success pull-right" onclick="fEditarSituacao()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaApagaSituacao" type="button" class="btn btn-success pull-right" onclick="fApagarSituacao()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <label class="text-red pull-right" id="lblSemBotao"></label>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Inserir Reunião CPG -->
    <div class="modal fade" id="divModalReuniaoCPG" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div id="divCabecReuniaoCPG" class="modal-header">
                    <h4 class="modal-title"><label id="lblTituloReuniaoCPG"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <input class="form-control input-sm" id="txtIdProrrogacao" type="text" value="" style="display:none"/>
                        <div class="row">
                            <div class="col-md-2">
                                <span>Nº Reunião</span><br />
                                <input class="form-control input-sm" id="txtIdReuniao" type="number" value=""/>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-4">
                                <span>Tipo da Reunião</span><br />
                                <select id="ddlTipoReuniaoCPG" class="form-control input-sm select2 SemPesquisa habilitaComboReuniaoCPG">
                                    <option selected value="">Selecione uma Reunião</option>
                                    <option value="1">Prorrogação</option>
                                    <option value="2">Aprovação de Banca (qualificação)</option>
                                    <option value="3">Aprovação de Banca (defesa)</option>
                                    <option value="4">Trancamento Especial</option>
                                    <option value="5">Validação de disciplina</option>
                                </select>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-3">
                                <span>Parecer</span><br />
                                <select id="ddlParecerReuniaoCPG" class="form-control input-sm select2 SemPesquisa habilitaComboReuniaoCPG">
                                    <option selected value="">Sem parecer</option>
                                    <option value="Aceito">Aceito</option>
                                    <option value="Recusado">Recusado</option>
                                </select>
                            </div>
                        </div>
                        <br />

                        <div id="divDatasReuniaoCPG" style="display:none">
                            <div class="row">
                                <div class="col-md-3">
                                    <span>Data Início</span><br />
                                    <input class="form-control input-sm" id="txtDataInicioReuniaoCPG" type="date" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md"> 
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span>Data Fim</span><br />
                                    <input class="form-control input-sm" id="txtDataFimReuniaoCPG" type="date" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md"> 
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span>Data Depósito</span><br />
                                    <input class="form-control input-sm" id="txtDataDepositoReuniaoCPG" type="date" value=""/>
                                </div>
                            </div>
                            <br />
                        </div>

                        <div class="row">
                            <div class="col-md-12 ">
                                <span>Observação</span><br />
                                <textarea style ="resize:vertical;font-size:14px" class="form-control input-sm" rows="5" id="txtObsReuniaoCPG"></textarea>
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
                            <button id="btnConfirmaInsereReuniaoCPG" type="button" class="btn btn-success pull-right" onclick="fInserirReuniaoCPG()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaEditaReuniaoCPG" type="button" class="btn btn-success pull-right" onclick="fEditarReuniaoCPG()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaApagaReuniaoCPG" type="button" class="btn btn-success pull-right" onclick="fApagarReuniaoCPG()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="divModalPresenca" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header alert-info ">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="">
                        <div class="row text-center">
                            <span class="text-center">RELAÇÃO DE PRESENÇA</span><br />
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                            Disciplina: <strong><label id="lblCodigoDisciplinaModalPresenca">&nbsp;</label></strong><br />
                            Nome: <strong><label id="lblNomeDisciplinaModalPresenca">&nbsp;</label></strong>
                            </div>
                        </div>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="Label3" Text="" />
                    <div class="container-fluid text-center">
                        <div class="row text-center">
                            <div class="col-md-12">
                                <table  class="display table table-striped table-bordered table-condensed table-hover" id="tabPresenca" cellspacing="0" width="100%">
                                    <thead>
                                        <tr style="color:White;background-color:#507CD1;font-weight:bold;" >
                                            <th>Nº Aula</th>
                                            <th>Data da Aula</th>
                                            <th>Hora de Início</th>
                                            <th>Hora de Término</th>
                                            <th>Situação</th>
                                        </tr>
                                    </thead>
                                    <%--<tfoot>
                                        <tr>
                                            <th>Name</th>
                                            <th>Position</th>
                                            <th>Office</th>
                                            <th>Extn.</th>
                                            <th>Start date</th>
                                            <th>Salary</th>
                                        </tr>
                                    </tfoot>--%>
                                </table>
                            </div>
                        </div>
                    </div>

                    <%--Prezado(a) Condômino,
                        <br>
                        <br>
                        Por favor, revise os seus dados digitados. 
                        <br>
                        <br>
                        Login e/ou Senha inválido(s).--%>
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

    <div class="modal fade" id="divModalDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header alert-info">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="">
                        <div class="row text-center">
                            <span class="text-center">Dados do Oferecimento da Turma</span><br />
                        </div>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="Label5" Text="" />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Nome</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblNomeDisciplinaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Código</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblCodigoDisciplinaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Quadrimestre</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblQuadrimestreModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>nº do Oferec.</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblNoOferecimentoModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Objetivo</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblObjetivoModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Ementa</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblEmentaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Bibliografia Básica</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblBibliografiaBasicaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Bibliografia Complementar</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblBibliografiaComplementarModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Forma de Avaliação</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblFormaAvaliacaoModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Observação</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblObservacaoModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                        </div>
                    </div>
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

    <div class="modal fade" id="divModalErro" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-red">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><label id="lblErroCabecalho">&nbsp;</label></h4>
                    </div>
                    <div class="modal-body">
                        <label id="lblErroCorpo">&nbsp;</label>
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

    <!-- Modal para recolher dados para imprimir histórico Oficial -->
    <div class="modal fade" id="divModalImprimirHistoricoOficial" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><label><i class ="fa fa-calendar"></i> Data do Diploma</label></h4>
                </div>
                <div class="modal-body">
                    <div class ="row">
                        <div class="col-md-6">
                            <span>Digite a Data do Diploma</span><br />
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input class="form-control input-sm" runat="server" id="txtDataDiploma" type="date" value=""/>
                            </div>
                        </div>
                    </div>
                    <br />

                    <div class ="row">
                        <div class="col-md-12">
                            <span>Observação</span><br />
                            <input class="form-control input-sm hidden" runat="server" id="txtObsHistorico" type="text" size="1000" maxlength="40"/>
                            <textarea style="resize: vertical" runat="server" class="form-control input-sm" rows="5" id="txtObsHistorico2"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-primary" onclick="funcClicaImprimirHistoricoOficial()" data-dismiss="modal">
                            <i class="fa fa-check"></i>&nbsp;OK</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->

    <!-- Modal para recolher dados para observação de Ata -->
    <div class="modal fade" id="divModalObsAta" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><label><i class ="fa fa-info-circle"></i> Observação da Ata</label></h4>
                </div>
                <div class="modal-body">
                    <div class ="row">
                        <div class="col-md-12">
                            <span>Observação</span><br />
                            <textarea style="resize: vertical; font-size:14px" runat="server" class="form-control input-sm" rows="5" id="txtObsAta"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" id="btnModalObsAtaDefesa" class="btn btn-primary" onclick="fImprimirAta('Defesa')" data-dismiss="modal">
                            <i class="fa fa-check"></i>&nbsp;OK</button>
                        <button type="button" id="btnModalObsAtaQualificacao" class="btn btn-primary" onclick="fImprimirAta('Qualificação')" data-dismiss="modal">
                            <i class="fa fa-check"></i>&nbsp;OK</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->

    <!-- Modal para recolher dados para imprimir Certificado de Titulação -->
    <div class="modal fade" id="divModalImprimirCertificado" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><label><i class ="fa fa-edit"></i> Dados do Certificado de Titulação </label></h4>
                </div>
                <div class="modal-body">
                    <div class ="row">
                        <div class="col-md-6">
                            <span>Número da Portaria</span><br />
                            <input class="form-control input-sm" runat="server" id="txtPortariaNumeroOficial" type="text" value=""/>

                        </div>
                    </div>
                    <br />

                    <div class ="row">
                        <div class="col-md-6">
                            <span>Data da Portaria</span><br />
                            <input class="form-control input-sm" runat="server" id="txtPortariaDataOficial" type="text" value=""/>

                        </div>
                    </div>
                    <br />

                    <div class ="row">
                        <div class="col-md-10">
                            <span>DOU</span><br />

                            <input class="form-control input-sm" runat="server" id="txtDouDataOficial" type="text" value=""/>

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-primary" runat="server" onserverclick="btnImprimirCertificado_Click" data-dismiss="modal" onclick="if (fPreparaRelatorio('O Certificado de Titulação está sendo preparado.')) return;">
                            <i class="fa fa-check"></i>&nbsp;OK</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->

    <!-- Modal para recolher dados para imprimir Certificado de Curta -->
    <div class="modal fade" id="divModalImprimirCertificadoCurta" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><label><i class ="fa fa-edit"></i> Dados do Certificado de Curta Duração </label></h4>
                </div>
                <div class="modal-body">
                    <div class ="row">
                        <div class="col-md-6">
                            <span>Número de Registro</span><br />
                            <input class="form-control input-sm" runat="server" id="txtNumeroRegistroCurta" type="text" value=""/>

                        </div>
                    </div>
                    <br />

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-primary" runat="server" onserverclick="btnImprimirCertificadoCurta_Click" data-dismiss="modal" onclick="if (fPreparaRelatorio('O Certificado de Curta Duração está sendo preparado.')) return;">
                            <i class="fa fa-check"></i>&nbsp;OK</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->

    <!-- Modal para recolher dados para imprimir Certificado de Especialização -->
    <div class="modal fade" id="divModalImprimirCertificadoEspecializacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><label><i class ="fa fa-edit"></i> Dados do Certificado de Especialização </label></h4>
                </div>
                <div class="modal-body">
                    <div class ="row">
                        <div class="col-md-6">
                            <span>Número do Certificado</span><br />
                            <input class="form-control input-sm" runat="server" id="txtNumeroRegistroEspecializacao" type="text" value=""/>

                        </div>
                    </div>
                     <br />

                    <div class ="row">
                        <div class="col-xs-5">
                            <span>Carga horária</span><br />
                            <small>Exemplo: </small><span>115</span><br /><%--<small>(se vazio sistema coloca dados da turma)</small> <br />--%>
                            <input class="form-control input-sm" runat="server" id="txtCargaHorariaEspecializacao" type="text" value=""/>
                        </div>

                        <div class="col-xs-7">
                            <span>Data de Conclusão</span><br />
                            <small>Exemplo: </small><span>02 de abril de 2022</span><br /><%-- <small>(se vazio sistema coloca dados da turma)</small> <br />--%>
                            <input class="form-control input-sm" runat="server" id="txtDataEspecializacao" type="text" value=""/>
                        </div>
                        
                    </div>
                    <br />

                    <div class ="row">
                        <div class="col-md-12">
                            <span>Coordenador do Curso</span><br />
                            <small>Exemplo: </small><span> Prof. MSc Leonardo Silva Sampaio </span><br />
                            <input class="form-control input-sm" runat="server" id="txtCoordenadorEspecializacao" type="text" value=""/>

                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <span>Tipo de Certificado</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-6">
                                    <asp:RadioButton GroupName="GrupoTipoCertificado" ID="optCertificadoComMascara" runat="server" />
                                    &nbsp;
                            <label class="opt" for="<%=optCertificadoComMascara.ClientID %>">Impresso (papel com máscara)</label>
                                </div>

                                <div class="col-md-6">
                                    <asp:RadioButton GroupName="GrupoTipoCertificado" ID="optCertificadoSemMascara" runat="server" />
                                    &nbsp;
                            <label class="opt" for="<%=optCertificadoSemMascara.ClientID %>">Digital (papel sem máscara)</label>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-primary" runat="server" onserverclick="btnImprimirCertificadoEspecializacao_Click" data-dismiss="modal" onclick="if (fPreparaRelatorio('O Certificado de Especialização está sendo preparado.')) return;">
                            <i class="fa fa-check"></i>&nbsp;OK</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->

    <!-- Modal para recolher dados para imprimir Certificado de MBA Internacional -->
    <div class="modal fade" id="divModalImprimirCertificadoMBAInternacional" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><label><i class ="fa fa-edit"></i> Dados do Certificado de MBA Internacional </label></h4>
                </div>
                <div class="modal-body">
                    <div class ="row">
                        <div class="col-md-6">
                            <span>Número de Registro</span><br />
                            <input class="form-control input-sm" runat="server" id="Text1" type="text" value=""/>

                        </div>
                    </div>
                    <br />

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-primary" runat="server" onserverclick="btnImprimirCertificadoCurta_Click" data-dismiss="modal" onclick="if (fPreparaRelatorio('O Certificado de MBA Internacional está sendo preparado.')) return;">
                            <i class="fa fa-check"></i>&nbsp;OK</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->

    <!-- Modal para Inserir Data Limite Documentação -->
    <div class="modal fade" id="divModalInserirDataLimiteDocumentacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><label><i class ="fa fa-calendar-check-o"></i> Inserir Nova Data Limte para Documentação</label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <span>Nova Data Limite para entrega da documentação</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <input class="form-control input-sm" id="txtNovaDataLimiteDocumentacao" type="text" value="" readonly="true"/>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span>Observação</span><br />
                                <span><small class="text-danger">(Digite aqui o motivo para a prorrogação da Data Limite)</small></span>
                                <textarea style="resize: vertical; font-size: 14px" id="txtObsDataLimiteDocumentacao" class="form-control" rows="4" maxlength="1000"></textarea>
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
                            <button id="btnConfirmaNovaDataLimiteDocumentacao" type="button" class="btn btn-success pull-right" onclick="fIncluirDataLimiteDocumentacao()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!-- Modal para aprovações -->
    <div class="modal fade" id="divAprovacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="divCabecAprovacao" class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i id="iconCabecEnviar" class="fa fa-mail-forward fa-lg"></i>&nbsp;<label id="lblCabecAprovacao">Associar Grupo</label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 ">
                            <label id="lblCorpoAprovacao">Enviar para Aprovação</label>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12 ">
                            <div id="divLabelObs"><span>Observação </span><span style="color: red;">*</span><br /></div>
                            <textarea style="resize: vertical; font-size: 14px" id="txtObsAprovacaoDissertacao" class="form-control" rows="4" maxlength="1000"></textarea>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-xs-6 ">
                            <div class="pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    <i class="fa fa-close"></i>&nbsp;Fechar</button>
                            </div>
                        </div>
                        <div class="col-xs-6 ">
                            <div class="pull-right">
                                <button type="button" id="btnEnviarAprovacaoDissertacao" name="btnEnviarAprovacaoDissertacao" class="btn btn-warning " onclick="fEnviarAprovacaoDissertacao()">
                                    <i class="fa fa-mail-forward fa-lg"></i>&nbsp;Enviar para Aprovação
                                </button>

                                <button type="button" id="btnAprovarDissertacao" name="btnAprovarDissertacao" class="btn btn-success" onclick="fAprovarDissertacao()" >
                                    <i class="fa fa-thumbs-o-up fa-lg"></i>&nbsp;Aprovar
                                </button>

                                <button type="button" id="btnReprovarDissertacao" name="btnReprovarDissertacao" class="btn btn-danger" onclick="fReprovarDissertacao()">
                                    <i class="fa fa-thumbs-o-down fa-lg"></i>&nbsp;Reprovar
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para exibir foto do Aluno -->
    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header bg-blue">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
            <h4 class="modal-title"><label id="labelNomeExibeImagem">test</label></h4>
          </div>
          <div class="modal-body text-center">
            <img src="" id="imagepreview" class="img-responsive center-block"  /> <%--style="width: 400px; height: 300px;"--%>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal para exibir Câmera -->
    <div class="modal fade" id="divModalCamera" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <div class="modal-header bg-purple">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
            <h4 class="modal-title"><label><i class ="fa fa-camera-retro fa-lg"></i>&nbsp;&nbsp;&nbsp;Tirar Foto </label></h4>
          </div>
          <div class="modal-body text-center">
            <div class="row">
                <div class="col-md-6">
                    <span class="pull-left"><i class="fa fa-camera-retro fa-lg"></i>&nbsp;&nbsp;&nbsp;Câmera</span><br /><br />
                    <div id="my_camera"></div>
                </div>

                <div class="col-md-6">
                    <span><i class="fa fa-photo fa-lg"></i>&nbsp;&nbsp;&nbsp;Imagem capturada</span><br />
                    <div id="results">A imagem capturada aparecerá aqui...</div>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-md-12">
                    <button type="button" class="btn btn-purple text-center" onclick="take_snapshot()"><i class="fa fa-camera fa-lg"></i>&nbsp;&nbsp;&nbsp;<strong> Capturar imagem</strong></button>
                </div>
            </div>

          </div>
          <div class="modal-footer">
              <div class="row">
                <div class="col-md-6">
                    <button type="button" class="btn btn-danger pull-left" data-dismiss="modal" onclick="fFecharCamera()"><i class="fa fa-close fa-lg"></i> Cancelar</button>
                </div>
                <div class="col-md-6">
                    <button type="button" class="btn btn-success pull-right" data-dismiss="modal" onclick="fConfirmarCaptura()"><i class="fa fa-check fa-lg"></i> Confirmar</button>
                </div>
            </div>
            
          </div>
        </div>
      </div>
    </div>

    <!-- Modal para Mensagens -->
    <div class="modal fade" id="divMensagemModal" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="divCabecalho" class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="CabecalhoMsg">
                        <asp:Label runat="server" ID="lblTituloMensagem" Text="" /></h4>
                        
                </div>
                <div id="CorpoMsg" class="modal-body">
                    <asp:Label runat="server" ID="lblMensagem" Text="" />


                    <%--Prezado(a) Condômino,
                        <br>
                        <br>
                        Por favor, revise os seus dados digitados. 
                        <br>
                        <br>
                        Login e/ou Senha inválido(s).--%>
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

    <div class="modal fade" id="divModal" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header alert-warning">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa fa-file-image-o"></i> Problema na escolha da Foto</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Prezado usuário,</b>
                        <br/>
                        <br/>
                    </p>
                       
                    <div class="row" style:"display: none;" >

                        <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Arquivo inválido!</b>
                        <br/>
                        <br/>
                        <p>
                            <div id="divExtencao" class="col-lg-12" style:"display: none;" >
                                Você deve selecionar apenas imagens com extenção: "<b>jpg</b>" ou "<b>jpeg</b>" ou "<b>png</b>".
                            </div>

                            <div id="divTamanho" class="col-lg-12" style:"display: none;" >
                                Você deve selecionar imagens com tamanho máximo de <b>1 Mb</b>.
                            </div>
                                
                        </p>
                        <br/>
                    </div>
                        
                </div>

                <div class="modal-footer">
                    <div class="pull-right">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar
                        </button>
                    </div>
                </div>
                    
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
        
    </div>
   

    <div class="hide"> 
        <asp:FileUpload ID="fileArquivoParaGravar" accept=".jpg,.png,.jpeg"  runat="server" CssClass="btn btn-primary btn-file" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:imagePreview(this);" />
        <asp:Button ID="bntSalvarDadosFoto" runat="server" Text="Button" />

        <asp:FileUpload ID="fileDissertacao" accept=".pdf"  runat="server" CssClass="btn btn-primary btn-file" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:fSelecionouDissertacao(this);" />

        <asp:FileUpload ID="fileArquivo" accept=".pdf,.jpg,.jpeg,.png"  runat="server" CssClass="btn btn-primary btn-file" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:fSelecionouArquivo(this);" />

        <asp:FileUpload ID="fileContrato" accept=".pdf"  runat="server" CssClass="btn btn-primary btn-file" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:fSelecionouContrato(this);" />

        <asp:FileUpload ID="fileArtigo" accept=".pdf"  runat="server" CssClass="btn btn-primary btn-file" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:fSelecionouArtigo(this);" />
    </div>

 <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>
    
    <script>
        //function replaceAll(find, replace, str) {
        //    alert("str: " + str)
        //    while (str.indexOf(find) > -1) {
        //        str = str.replace(find, replace);
        //    }
        //    return str;
        //}

        $(".collapsed").click(function () {
            //alert($(this).get(0).id);
            $("#i" + $(this).get(0).id).toggleClass("down");
        })

        function fModalCertificado() {
            $('#divModalImprimirCertificado').modal();
        }

        function fModalCertificadoCurta() {
            $('#divModalImprimirCertificadoCurta').modal();
        }

        function fModalCertificadoEspecializacao() {
            $('#divModalImprimirCertificadoEspecializacao').modal();
        }

        function fModalCertificadoMBAInternacional() {
            $('#divModalImprimirCertificadoMBAInternacional').modal();
        }

        $(".select2").select2({
            theme: "bootstrap",
            language: "pt-BR",
            //containerCssClass: "form-control input-sm ",
            //dropdownCssClass: "form-control input-sm "
        });

        $(".SemPesquisa").select2({
            theme: "bootstrap",
            minimumResultsForSearch: Infinity
        });

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalSelecionarOrientador').is(':visible')) {
                    if (document.getElementById('btnPerquisaOrientadorDisponivel').style.display == 'block') {
                        fPerquisaOrientadorDisponivel();
                    }
                    else {
                        fPerquisaCoorientadorDisponivel();
                    }
                }
                else if ($('#divModalSelecionarBanca').is(':visible')) {
                    if (document.getElementById('btnPerquisaOrientadorDisponivelBancaQualificacao').style.display == 'block') {
                        fPesquisaBancaDisponivel('Qualificação','Orientador');
                    }
                    else if (document.getElementById('btnPerquisaCoorientadorDisponivelBancaQualificacao').style.display == 'block') {
                        fPesquisaBancaDisponivel('Qualificação','Co-orientador');
                    }
                    else if (document.getElementById('btnPerquisaMembroDisponivelBancaQualificacao').style.display == 'block') {
                        fPesquisaBancaDisponivel('Qualificação','Membro');
                    }
                    else if (document.getElementById('btnPerquisaSuplenteDisponivelBancaQualificacao').style.display == 'block') {
                        fPesquisaBancaDisponivel('Qualificação','Membro Suplente');
                    }
                    else if (document.getElementById('btnPerquisaOrientadorDisponivelBancaDefesa').style.display == 'block') {
                        fPesquisaBancaDisponivel('Defesa','Orientador');
                    }
                    else if (document.getElementById('btnPerquisaCoorientadorDisponivelBancaDefesa').style.display == 'block') {
                        fPesquisaBancaDisponivel('Defesa','Co-orientador');
                    }
                    else if (document.getElementById('btnPerquisaMembroDisponivelBancaDefesa').style.display == 'block') {
                        fPesquisaBancaDisponivel('Defesa','Membro');
                    }
                    else  {
                        fPesquisaBancaDisponivel('Defesa','Membro Suplente');
                    }
                    
                }
                else {
                    //alert('não');
                }
            }
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;

        //==========================================================

        function fSelect2(){
                //alert('fSelect2');
                //Initialize Select2 Elements
                $(".select2").select2({
                    theme: "bootstrap",
                    language: "pt-BR",
                    //containerCssClass: "form-control input-sm ",
                    //dropdownCssClass: "form-control input-sm "
                });

                $(".SemPesquisa").select2({
                    theme: "bootstrap",
                    minimumResultsForSearch: Infinity
                });
            }

        //==========================================================

        function fModalIncluirSituacao() {
            $("#divCabecSituacao").removeClass("bg-warning");
            $("#divCabecSituacao").removeClass("bg-primary");
            $('#divCabecSituacao').removeClass('bg-red');
            $('#divCabecSituacao').removeClass('alert-info');
            $('#divCabecSituacao').addClass("alert-info");
            document.getElementById('lblTituloEdiarSituacao').innerHTML = '<i class="fa fa-calendar-plus-o"></i> Incluir Situação da Matrícula';
            document.getElementById('btnConfirmaInsereSituacao').style.display = 'block';
            document.getElementById('btnConfirmaEditaSituacao').style.display = 'none';
            document.getElementById('btnConfirmaApagaSituacao').style.display = 'none';
            document.getElementById('txtNomeSituacao').style.display = 'none';
            document.getElementById('txtDataInicioSituacao').value = "";
            document.getElementById('txtDataFimSituacao').value = "";
            document.getElementById('divddlNomeSituacao').style.display = 'block';
            document.getElementById('lblSemBotao').style.display = 'none';
            document.getElementById('divDataInicio').style.display = "block";
            $("#Desligado").removeAttr('disabled');
            $("#Titulado").removeAttr('disabled');
            var table2 = $('#grdHistoricoMatriculaNew').DataTable();
            table2.cells().every(function (row, col) {
                //alert('row: ' + row );
                //alert('col: ' + col );
                if (col == 2) {
                    if (this.data() == "Desligado") {
                        $("#Desligado").attr('disabled','disabled');
                        document.getElementById('btnConfirmaInsereSituacao').style.display = 'none';
                        document.getElementById('lblSemBotao').style.display = 'block';
                        document.getElementById('lblSemBotao').innerHTML = "Não é possível inserir nenhum registro pois já há no histórico uma situação " + this.data();
                    }
                    else if (this.data() == "Titulado") {
                        $("#Titulado").attr('disabled','disabled');
                        document.getElementById('btnConfirmaInsereSituacao').style.display = 'none';
                        document.getElementById('lblSemBotao').style.display = 'block';
                        document.getElementById('lblSemBotao').innerHTML = "Não é possível inserir nenhum registro pois já há no histórico uma situação " + this.data();
                    }
                }
            });

            document.getElementById('txtDataInicioSituacao').readOnly = false;
            document.getElementById('txtDataFimSituacao').readOnly = false;
            $('.ddlStatusSituacao').select2({
                disabled: false
            });

            fSelect2();

            $("#ddlNomeSituacao").val('').trigger("change");

            $('#divModalEdiarSituacao').modal();

            $('.modal-dialog').css({
                    top: 0,
                    left: 0
                });
            }
            
        //==========================================================

        function fModalEditarSituacao(qIdHistorico, qNomeSituacao, qDataInicio, qDataFim) {
            $("#divCabecSituacao").removeClass("bg-warning");
            $("#divCabecSituacao").removeClass("bg-primary");
            $('#divCabecSituacao').removeClass('bg-red');
            $('#divCabecSituacao').removeClass('alert-info');
            $('#divCabecSituacao').addClass("bg-primary");
            document.getElementById('lblTituloEdiarSituacao').innerHTML = '<i class="fa fa-edit"></i> Editar Situação da Matrícula';
            document.getElementById('btnConfirmaInsereSituacao').style.display = 'none';
            document.getElementById('btnConfirmaEditaSituacao').style.display = 'block';
            document.getElementById('btnConfirmaApagaSituacao').style.display = 'none';
            document.getElementById('divDataInicio').style.display = "block";
            document.getElementById('txtNomeSituacao').style.display = 'block';
            document.getElementById('divddlNomeSituacao').style.display = 'none';
            document.getElementById('lblSemBotao').style.display = 'none';
            document.getElementById('txtIdHIstoricoSituacao').value = qIdHistorico;
            document.getElementById('txtNomeSituacao').value = qNomeSituacao;
            document.getElementById('txtDataInicioSituacao').value = qDataInicio;
            document.getElementById('txtDataFimSituacao').value = qDataFim;

            if (qNomeSituacao == "Desligado" || qNomeSituacao == "Abandonou" || qNomeSituacao == "Qualificação" || qNomeSituacao == "Titulado") {
                document.getElementById('divDataInicio').style.display = "none";
                document.getElementById('txtDataInicioSituacao').readOnly = true;
            }
            else {
                document.getElementById('divDataInicio').style.display = "block";
                document.getElementById('txtDataInicioSituacao').readOnly = false;
            }

            document.getElementById('txtDataInicioSituacao').readOnly = false;
            document.getElementById('txtDataFimSituacao').readOnly = false;
            $('.ddlStatusSituacao').select2({
                disabled: false
            });
            fSelect2();
            $('#divModalEdiarSituacao').modal();
        }

        //==========================================================

        function fModalApagarSituacao(qIdHistorico, qNomeSituacao, qDataInicio, qDataFim) {
            $("#divCabecSituacao").removeClass("bg-warning");
            $("#divCabecSituacao").removeClass("bg-primary");
            $('#divCabecSituacao').removeClass('bg-red');
            $('#divCabecSituacao').removeClass('alert-info');
            $('#divCabecSituacao').addClass("bg-red");
            document.getElementById('lblTituloEdiarSituacao').innerHTML = '<i class="fa fa-eraser"></i> Apagar Situação da Matrícula';
            document.getElementById('btnConfirmaInsereSituacao').style.display = 'none';
            document.getElementById('btnConfirmaEditaSituacao').style.display = 'none';
            document.getElementById('btnConfirmaApagaSituacao').style.display = 'block';
            document.getElementById('txtNomeSituacao').style.display = 'block';
            document.getElementById('divDataInicio').style.display = "block";
            document.getElementById('divddlNomeSituacao').style.display = 'none';
            document.getElementById('lblSemBotao').style.display = 'none';
            document.getElementById('txtIdHIstoricoSituacao').value = qIdHistorico;
            document.getElementById('txtNomeSituacao').value = qNomeSituacao;
            document.getElementById('txtDataInicioSituacao').value = qDataInicio;
            document.getElementById('txtDataFimSituacao').value = qDataFim;

            document.getElementById('txtDataInicioSituacao').readOnly = true;
            document.getElementById('txtDataFimSituacao').readOnly = true;
            $('.ddlStatusSituacao').select2({
                disabled: true
            });

            $('#divModalEdiarSituacao').modal();
        }

        //==========================================================

        $('#divddlNomeSituacao').on('change', function(){
            var qOpcao = $("#divddlNomeSituacao option:selected").text();
            if (qOpcao == "Desligado" || qOpcao == "Abandonou" || qOpcao == "Qualificação" || qOpcao == "Titulado") {
                document.getElementById('divDataInicio').style.display = "none";
                document.getElementById('txtDataInicioSituacao').readOnly = true;
            }
            else {
                document.getElementById('divDataInicio').style.display = "block";
                document.getElementById('txtDataInicioSituacao').readOnly = false;
            }
        });

        //==========================================================

        function fInserirSituacao() {
            var qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            var qStatus = $("#ddlStatusSituacao option:selected").text();
            var qSituacao = $("#divddlNomeSituacao option:selected").text();
            var qDataInicio = document.getElementById('txtDataInicioSituacao').value;
            var qDataFim = document.getElementById('txtDataFimSituacao').value
            var sAux = "";
            if (qSituacao == "Selecione") {
                sAux = sAux + "Deve-se selecionar uma Situação <br><br>"
            }

            if (qSituacao != "Desligado" && qSituacao != "Abandonou" && qSituacao != "Qualificação" && qSituacao != "Titulado" && document.getElementById('txtDataInicioSituacao').value == "") {
                sAux = sAux + "Deve-se digitar uma Data Início <br><br>"
                
            }
            if (document.getElementById('txtDataFimSituacao').value == "") {
                sAux = sAux + "Deve-se digitar uma Data Fim <br><br>"
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fInserirSituacao?qIdTurma=" + qIdTurma + "&qStatus=" + qStatus + "&qSituacao=" + qSituacao + "&qDataInicio=" + qDataInicio + "&qDataFim=" + qDataFim + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Situação de Matrícula ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão de Situação de Matrícula. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheTurmaAluno(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value);
                        $('#divModalEdiarSituacao').modal('hide');
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Situação de Matrícula</strong><br /><br />',
                            message: 'Inclusão de Situação de Matrícula realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na inclusão de Situação de Matrícula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão de Situação de Matrícula. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //==========================================================

        function fEditarSituacao() {
            var qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            var qIdHistorico = document.getElementById('txtIdHIstoricoSituacao').value;
            var qStatus = $("#ddlStatusSituacao option:selected").text();
            var qSituacao = document.getElementById('txtNomeSituacao').value;
            var qDataInicio = document.getElementById('txtDataInicioSituacao').value;
            var qDataFim = document.getElementById('txtDataFimSituacao').value
            var sAux = "";

            //if (qSituacao == "Selecione") {
            //    sAux = sAux + "Deve-se selecionar uma Situação <br><br>"
            //}

            if (qSituacao != "Desligado" && qSituacao != "Abandonou" && qSituacao != "Qualificação" && qSituacao != "Titulado" && document.getElementById('txtDataInicioSituacao').value == "") {
                sAux = sAux + "Deve-se digitar uma Data Início <br><br>"
                
            }
            if (document.getElementById('txtDataFimSituacao').value == "") {
                sAux = sAux + "Deve-se digitar uma Data Fim <br><br>"
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fEditarSituacao?qIdHistorico=" + qIdHistorico + "&qStatus=" + qStatus + "&qSituacao=" + qSituacao + "&qDataInicio=" + qDataInicio + "&qDataFim=" + qDataFim + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Situação de Matrícula ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Alteração de Situação de Matrícula. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheTurmaAluno(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value);
                        $('#divModalEdiarSituacao').modal('hide');
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração de Situação de Matrícula</strong><br /><br />',
                            message: 'Alteração de Situação de Matrícula realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Alteração de Situação de Matrícula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração de Situação de Matrícula. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //==========================================================

        function fApagarSituacao() {
            var qIdHistorico = document.getElementById('txtIdHIstoricoSituacao').value;

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fApagarSituacao?qIdHistorico=" + qIdHistorico + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Situação de Matrícula ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Exclusão de Situação de Matrícula. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheTurmaAluno(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value);
                        $('#divModalEdiarSituacao').modal('hide');
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão de Situação de Matrícula</strong><br /><br />',
                            message: 'Exclusão de Situação de Matrícula realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Exclusão de Situação de Matrícula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Exclusão de Situação de Matrícula. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //================================================================================

        function fPreencheComboContrato(qIdTurma) {
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheComboContrato?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } 
                    else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'carregamento de Contrato ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de carregamento do Contrato. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //alert("entrou: " + json.length);
                        $("#ddlTipoContrato").empty();
                        $('#ddlTipoContrato').select2({ data: json });
                        $("#ddlTipoContrato").val(json[0].id).trigger("change");
                        fSelect2();
                        fPreencheContrato(qIdTurma, $("#ddlTipoContrato").val());
                        //alert($("#ddlTipoContrato").val());
                        //alert("saiu: " + json.length);
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de Contrato. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de Contrato. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
            
        }

        //================================================================================

        $('#ddlTipoContrato').on("select2:select", function(e) { 
            fPreencheContrato(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value, $(this).val());
        });

        //================================================================================
        //== Inicio Aba Reuniões CPG ==============================================================================

        function fPreencheDataLimiteDocumentacao() {
            try {
                var dt = $('#grdDataLimiteDocumentacao').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById('<%=divDataLimiteDocumento.ClientID%>').style.display = "none";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById('<%=divDataLimiteDocumento.ClientID%>').style.display = "none";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById('<%=divDataLimiteDocumento.ClientID%>').style.display = "block";
                                document.getElementById('txtDataCadastroDataLimiteDcumentacao').value = json[0].P0;
                                document.getElementById('txtResponsavelDataLimiteDcumentacao').value = json[0].P2;
                                document.getElementById('<%=txtDataLimiteDocumento.ClientID%>').value = json[0].P3;
                                if (json[0].P4 == "false") {
                                    document.getElementById('btnAlterarDataLimiteDocumentacao').style.display="none";
                                }
                                else {
                                    document.getElementById('btnAlterarDataLimiteDocumentacao').style.display="block";
                                }
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheDataLimiteDocumentacao?qTab=" + document.getElementById('hQTab').value ,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Data Cadastro", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: "date-euro"
                        },
                        {
                            "data": "P1", "title": "Observação", "orderable": false, "className": "text-left centralizarTH", width: "90%"
                        },
                        {
                            "data": "P2", "title": "Usuário", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P3", "title": "Data Limite", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    order: [ 3, 'desc' ],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

         //==========================================================

        function fModalIncluirDataLimiteDcumentacao() {
            var today = new Date();
            today.setMonth(today.getMonth() + 6);
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();
            document.getElementById('txtNovaDataLimiteDocumentacao').value = dd + "/" + mm + "/" + yyyy;
            document.getElementById('txtObsDataLimiteDocumentacao').value = "";

            $('#divModalInserirDataLimiteDocumentacao').modal();
        }

        //==========================================================

        function fIncluirDataLimiteDocumentacao() {
            var qDataLimite = document.getElementById('txtNovaDataLimiteDocumentacao').value;
            var qObsDataLimite = document.getElementById('txtObsDataLimiteDocumentacao').value;
            var sAux = "";
            if (qObsDataLimite == "" || qObsDataLimite.length < 10) {
                sAux = sAux + "Deve-se digitar uma observação (com o mínimo de 10 caracteres) explicando o motivo da prorrogação da Data Limite para entrega da documentação desse aluno.  <br><br>"
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluirDataLimiteDocumentacao?qDataLimite=" + qDataLimite + "&qObsDataLimite=" + qObsDataLimite + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Situação de Matrícula ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão de Situação de Matrícula. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheDataLimiteDocumentacao();
                        $('#divModalInserirDataLimiteDocumentacao').modal('hide');
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Nova Data Limite</strong><br /><br />',
                            message: 'Inclusão de Nova Data Limite para entrega de Documentação realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na inclusão de Situação de Matrícula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão de Situação de Matrícula. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //================================================================================

        function fPreencheContrato(qIdTurma, qContrato) {
            //alert("qContrato: " + qContrato)
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheContrato?qIdTurma=" + qIdTurma + "&qContrato=" + qContrato + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } 
                    else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'carregamento de Contrato ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de carregamento do Contrato. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //alert(json.length)
                        document.getElementById('txtDataContrato').value = json[0].P0;
                        document.getElementById('txtValorTotal').value = json[0].P1;
                        document.getElementById('txtValorTotal').focus();
                        document.getElementById('txtValorDisciplina').value = json[0].P2;
                        document.getElementById('txtValorDisciplina').focus();
                        document.getElementById('txtNumeroParcela').value = json[0].P3;
                        document.getElementById('txtValorParcela').value = json[0].P4;
                        document.getElementById('txtValorParcela').focus();
                        document.getElementById('txtDataInicioCurso').value = json[0].P5;
                        document.getElementById('txtPrazo').value = json[0].P6;
                        document.getElementById('txtCoordenador').value = json[0].P7;
                        document.getElementById('txtSecretaria').value = json[0].P8;
                        document.getElementById('txtTextemunha1').value = json[0].P9;
                        document.getElementById('txtRGTextemunha1').value = json[0].P10;
                        document.getElementById('txtTextemunha2').value = json[0].P11;
                        document.getElementById('txtRGTextemunha2').value = json[0].P12;
                        document.getElementById('txtParagrafoDiretor').value = json[0].P13;
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de carregamento do grupo da Turma. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de carregamento do grupo da Turma. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
            
        }

        //== Inicio Aba Reuniões CPG ==============================================================================

        function fPreencheProrrogacaoCPG(qIdTurma) {
            try {
                var dt = $('#grdProrrogacaoCPG').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdProrrogacaoCPG").style.display = "none";
                            document.getElementById("msgSemResultadosgrdProrrogacaoCPG").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdProrrogacaoCPG").style.display = "none";
                                document.getElementById("msgSemResultadosgrdProrrogacaoCPG").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdProrrogacaoCPG").style.display = "block";
                                document.getElementById("msgSemResultadosgrdProrrogacaoCPG").style.display = "none";

                                var table_grdProrrogacaoCPG = $('#grdProrrogacaoCPG').DataTable();

                                $('#grdProrrogacaoCPG').on("click", "tr", function () {
                                    vRowIndex_grdProrrogacaoCPG = table_grdProrrogacaoCPG.row(this).index()
                                });

                                if (json[0].P10 == "0") {
                                    dt.columns([8,9] ).visible( false );
                                }
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheProrrogacaoCPG?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "N.º Reunião", "orderable": true, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P9", "title": "Data Cadastro", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: "date-euro"
                        },
                        {
                            "data": "P8", "title": "Tipo Reunião", "orderable": true, "className": "text-center centralizarTH", width: "15%"
                        },
                        {
                            "data": "P1", "title": "Data Início", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: "date-euro"
                        },
                        {
                            "data": "P2", "title": "Data Fim", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: "date-euro"
                        },
                        {
                            "data": "P3", "title": "Resultado", "orderable": true, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P4", "title": "Depósito", "orderable": false, "className": "text-center centralizarTH", width: '10px', type: "date-euro"
                        },
                        {
                            "data": "P5", "title": "Observação", "orderable": false, "className": "centralizarTH"
                        },
                        {
                            "data": "P6", "title": "Editar", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                        },
                        {
                            "data": "P7", "title": "Apagar", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    order: [ 0, 'asc' ],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

        //================================================================================

        function fModalIncluirProrrogacaoCPG() {
            $("#divCabecReuniaoCPG").removeClass("bg-warning");
            $("#divCabecReuniaoCPG").removeClass("bg-primary");
            $('#divCabecReuniaoCPG').removeClass('bg-red');
            $('#divCabecReuniaoCPG').removeClass('alert-info');
            $('#divCabecReuniaoCPG').addClass("alert-info");

            document.getElementById('txtIdReuniao').disabled=false;
            $('.habilitaComboReuniaoCPG').select2({disabled: false});
            document.getElementById('txtDataInicioReuniaoCPG').disabled=false;
            document.getElementById('txtDataFimReuniaoCPG').disabled=false;
            document.getElementById('txtDataDepositoReuniaoCPG').disabled=false;
            document.getElementById('txtObsReuniaoCPG').disabled=false;
            fSelect2();

            document.getElementById('lblTituloReuniaoCPG').innerHTML = '<i class="fa fa-users"></i> Incluir Reunião CPG';
            document.getElementById('btnConfirmaInsereReuniaoCPG').style.display = 'block';
            document.getElementById('btnConfirmaEditaReuniaoCPG').style.display = 'none';
            document.getElementById('btnConfirmaApagaReuniaoCPG').style.display = 'none';
            document.getElementById('txtIdProrrogacao').value = '';
            document.getElementById('txtIdReuniao').value = '';
            $("#ddlTipoReuniaoCPG").val('').trigger("change");
            $("#ddlParecerReuniaoCPG").val('').trigger("change");
            document.getElementById('txtDataInicioReuniaoCPG').value = '';
            document.getElementById('txtDataFimReuniaoCPG').value = '';
            document.getElementById('txtDataDepositoReuniaoCPG').value = '';
            document.getElementById('txtObsReuniaoCPG').value = '';
            document.getElementById('divDatasReuniaoCPG').style.display = 'none';

            $('#divModalReuniaoCPG').modal();
        }

        //==========================================================
        
        function fModalEditarProrrogacaoCPG(qIdProrrogacao, qIdReuniao, qDataInicio, qDataFim, qParecer, qDataDeposito, qObs, qIdTipoReuniaoCPG ) {
            $("#divCabecReuniaoCPG").removeClass("bg-warning");
            $("#divCabecReuniaoCPG").removeClass("bg-primary");
            $('#divCabecReuniaoCPG').removeClass('bg-red');
            $('#divCabecReuniaoCPG').removeClass('alert-info');
            $('#divCabecReuniaoCPG').addClass("bg-primary");

            document.getElementById('txtIdReuniao').disabled=false;
            $('.habilitaComboReuniaoCPG').select2({disabled: false});
            document.getElementById('txtDataInicioReuniaoCPG').disabled=false;
            document.getElementById('txtDataFimReuniaoCPG').disabled=false;
            document.getElementById('txtDataDepositoReuniaoCPG').disabled=false;
            document.getElementById('txtObsReuniaoCPG').disabled=false;
            fSelect2();

            document.getElementById('lblTituloReuniaoCPG').innerHTML = '<i class="fa fa-users"></i> Editar Reunião CPG';
            document.getElementById('btnConfirmaInsereReuniaoCPG').style.display = 'none';
            document.getElementById('btnConfirmaEditaReuniaoCPG').style.display = 'block';
            document.getElementById('btnConfirmaApagaReuniaoCPG').style.display = 'none';
            document.getElementById('txtIdProrrogacao').value = qIdProrrogacao;
            document.getElementById('txtIdReuniao').value = qIdReuniao;
            $("#ddlTipoReuniaoCPG").val(qIdTipoReuniaoCPG).trigger("change");
            $("#ddlParecerReuniaoCPG").val(qParecer).trigger("change");
            document.getElementById('txtDataInicioReuniaoCPG').value = qDataInicio;
            document.getElementById('txtDataFimReuniaoCPG').value = qDataFim;
            document.getElementById('txtDataDepositoReuniaoCPG').value = qDataDeposito;
            document.getElementById('txtObsReuniaoCPG').value = qObs;
            if (qIdTipoReuniaoCPG == "1" || qIdTipoReuniaoCPG == "4") {
                document.getElementById('divDatasReuniaoCPG').style.display = 'block';
            }
            else {
                document.getElementById('divDatasReuniaoCPG').style.display = 'none';
            }

            $('#divModalReuniaoCPG').modal();
        }

        //==========================================================

        function fModalApagarProrrogacaoCPG(qIdProrrogacao, qIdReuniao, qDataInicio, qDataFim, qParecer, qDataDeposito, qObs, qIdTipoReuniaoCPG) {
            $("#divCabecReuniaoCPG").removeClass("bg-warning");
            $("#divCabecReuniaoCPG").removeClass("bg-primary");
            $('#divCabecReuniaoCPG').removeClass('bg-red');
            $('#divCabecReuniaoCPG').removeClass('alert-info');
            $('#divCabecReuniaoCPG').addClass("bg-red");

            document.getElementById('txtIdReuniao').disabled=true;
            $('.habilitaComboReuniaoCPG').select2({disabled: true});
            document.getElementById('txtDataInicioReuniaoCPG').disabled=true;
            document.getElementById('txtDataFimReuniaoCPG').disabled=true;
            document.getElementById('txtDataDepositoReuniaoCPG').disabled=true;
            document.getElementById('txtObsReuniaoCPG').disabled=true;
            fSelect2();

            document.getElementById('lblTituloReuniaoCPG').innerHTML = '<i class="fa fa-users"></i> Apagar Reunião CPG';
            document.getElementById('btnConfirmaInsereReuniaoCPG').style.display = 'none';
            document.getElementById('btnConfirmaEditaReuniaoCPG').style.display = 'none';
            document.getElementById('btnConfirmaApagaReuniaoCPG').style.display = 'block';
            document.getElementById('txtIdProrrogacao').value = qIdProrrogacao;
            document.getElementById('txtIdReuniao').value = qIdReuniao;
            $("#ddlTipoReuniaoCPG").val(qIdTipoReuniaoCPG).trigger("change");
            $("#ddlParecerReuniaoCPG").val(qParecer).trigger("change");
            document.getElementById('txtDataInicioReuniaoCPG').value = qDataInicio;
            document.getElementById('txtDataFimReuniaoCPG').value = qDataFim;
            document.getElementById('txtDataDepositoReuniaoCPG').value = qDataDeposito;
            document.getElementById('txtObsReuniaoCPG').value = qObs;
            if (qIdTipoReuniaoCPG == "1" || qIdTipoReuniaoCPG == "4") {
                document.getElementById('divDatasReuniaoCPG').style.display = 'block';
            }
            else {
                document.getElementById('divDatasReuniaoCPG').style.display = 'none';
            }

            $('#divModalReuniaoCPG').modal();
        }

        
        //==========================================================

        $('#ddlTipoReuniaoCPG').on("select2:select", function(e) { 
            //alert($(this).val());
            var qOpcao = $("#ddlTipoReuniaoCPG option:selected").val();
            //alert(qOpcao);
            if (qOpcao == "1" || qOpcao == "4") {
                document.getElementById('divDatasReuniaoCPG').style.display = 'block';
            }
            else {
                document.getElementById('divDatasReuniaoCPG').style.display = 'none';
            }
        });
       
        //==========================================================
        
        function fInserirReuniaoCPG() {
            var qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            var qIdReuniao = document.getElementById('txtIdReuniao').value;
            var qidTipoReuniao = $("#ddlTipoReuniaoCPG option:selected").val();
            var qParecer = $("#ddlParecerReuniaoCPG option:selected").val();
            var qDataInicio = document.getElementById('txtDataInicioReuniaoCPG').value;
            var qDataFim = document.getElementById('txtDataFimReuniaoCPG').value
            var qDataDeposito = document.getElementById('txtDataDepositoReuniaoCPG').value
            var qObs = document.getElementById('txtObsReuniaoCPG').value;
            
            var sAux = "";
            if (qIdReuniao.trim() == "") {
                sAux = sAux + "Deve-se digitar o nº da reunião <br><br>"
            }

            if (qidTipoReuniao == "") {
                sAux = sAux + "Deve-se selecionar um Tipo de Reunião <br><br>"
                
            }

            if (qidTipoReuniao == "1") {
                if (qDataInicio == "") {
                    sAux = sAux + "Deve-se digitar uma Data Início para a Prorrogação <br><br>"
                
                }
                if (qDataFim == "") {
                    sAux = sAux + "Deve-se digitar uma Data Fim para a Prorrogação<br><br>"
                }
            }

            if (qidTipoReuniao == "4") {
                if (qDataInicio == "") {
                    sAux = sAux + "Deve-se digitar uma Data Início para Trancamento <br><br>"
                
                }
                if (qDataFim == "") {
                    sAux = sAux + "Deve-se digitar uma Data Fim para Trancamento<br><br>"
                }
            }

            if (qObs == "") {
                sAux = sAux + "Deve-se digitar uma Observação para a Reunião <br><br>"
                
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fInserirReuniaoCPG?qIdTurma=" + qIdTurma + "&qIdReuniao=" + qIdReuniao + "&qidTipoReuniao=" + qidTipoReuniao + "&qParecer=" + qParecer + "&qDataInicio=" + qDataInicio + "&qDataFim=" + qDataFim + "&qDataDeposito=" + qDataDeposito + "&qObs=" + qObs + "&qTab=" + document.getElementById('hQTab').value ,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Reunião CPG ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão de Reunião CPG. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalReuniaoCPG').modal('hide');
                        fPreencheTurmaAluno(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value);
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Reunião CPG</strong><br /><br />',
                            message: 'Inclusão de Reunião CPG realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na inclusão de Reunião CPG. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão de Reunião CPG. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }


        //==========================================================
        
        function fEditarReuniaoCPG() {
            var qIdProrrogacao = document.getElementById('txtIdProrrogacao').value;
            var qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            var qIdReuniao = document.getElementById('txtIdReuniao').value;
            var qidTipoReuniao = $("#ddlTipoReuniaoCPG option:selected").val();
            var qParecer = $("#ddlParecerReuniaoCPG option:selected").val();
            var qDataInicio = document.getElementById('txtDataInicioReuniaoCPG').value;
            var qDataFim = document.getElementById('txtDataFimReuniaoCPG').value
            var qDataDeposito = document.getElementById('txtDataDepositoReuniaoCPG').value
            var qObs = document.getElementById('txtObsReuniaoCPG').value;
            
            var sAux = "";
            if (qIdReuniao.trim() == "") {
                sAux = sAux + "Deve-se digitar o nº da reunião <br><br>"
            }

            if (qidTipoReuniao == "") {
                sAux = sAux + "Deve-se selecionar um Tipo de Reunião <br><br>"
                
            }

            if (qidTipoReuniao == "1") {
                if (qDataInicio == "") {
                    sAux = sAux + "Deve-se digitar uma Data Início (ou válida) para a Prorrogação <br><br>"
                }
                if (qDataFim == "") {
                    sAux = sAux + "Deve-se digitar uma Data Fim (ou válida) para a Prorrogação<br><br>"
                }
            }

            if (qidTipoReuniao == "4") {
                if (qDataInicio == "") {
                    sAux = sAux + "Deve-se digitar uma Data Início para Trancamento <br><br>"
                
                }
                if (qDataFim == "") {
                    sAux = sAux + "Deve-se digitar uma Data Fim para Trancamento<br><br>"
                }
            }

            if (qObs == "") {
                sAux = sAux + "Deve-se digitar uma Observação para a Reunião <br><br>"
                
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fEditarReuniaoCPG?qIdProrrogacao=" + qIdProrrogacao + "&qIdTurma=" + qIdTurma + "&qIdReuniao=" + qIdReuniao + "&qidTipoReuniao=" + qidTipoReuniao + "&qParecer=" + qParecer + "&qDataInicio=" + qDataInicio + "&qDataFim=" + qDataFim + "&qDataDeposito=" + qDataDeposito + "&qObs=" + qObs  + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Edição de Reunião CPG ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Edição de Reunião CPG. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalReuniaoCPG').modal('hide');
                        fPreencheTurmaAluno(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value);
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Edição de Reunião CPG</strong><br /><br />',
                            message: 'Edição de Reunião CPG realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Edição de Reunião CPG. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Edição de Reunião CPG. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        function fApagarReuniaoCPG() {
            var qIdProrrogacao = document.getElementById('txtIdProrrogacao').value;
            var qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            var qIdReuniao = document.getElementById('txtIdReuniao').value;
            var qidTipoReuniao = $("#ddlTipoReuniaoCPG option:selected").val();
            var qParecer = $("#ddlParecerReuniaoCPG option:selected").val();
            var qDataInicio = document.getElementById('txtDataInicioReuniaoCPG').value;
            var qDataFim = document.getElementById('txtDataFimReuniaoCPG').value
            var qDataDeposito = document.getElementById('txtDataDepositoReuniaoCPG').value
            var qObs = document.getElementById('txtObsReuniaoCPG').value;
            
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fApagarReuniaoCPG?qIdProrrogacao=" + qIdProrrogacao + "&qIdTurma=" + qIdTurma + "&qIdReuniao=" + qIdReuniao + "&qidTipoReuniao=" + qidTipoReuniao + "&qParecer=" + qParecer + "&qDataInicio=" + qDataInicio + "&qDataFim=" + qDataFim + "&qDataDeposito=" + qDataDeposito + "&qObs=" + qObs + "&qTab=" + document.getElementById('hQTab').value ,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Reunião CPG ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Exclusão de Reunião CPG. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalReuniaoCPG').modal('hide');
                        fPreencheTurmaAluno(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value);
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão de Reunião CPG</strong><br /><br />',
                            message: 'Exclusão de Reunião CPG realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Exclusão de Reunião CPG. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Exclusão de Reunião CPG. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //== Fim Aba Reuniões CPG ========================================================

        //==========================================================

        function fPerquisaOrientadorDisponivel() {
            fProcessando();
            try {
                var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
                var qCPF = document.getElementById('txtCPFOrientadorPesquisa').value;
                var qNome = document.getElementById('txtNomeOrientadorPesquisa').value;
                var dt = $('#grdOrientadorDisponivel').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: true, //Pesquisar
                    bPaginate: true, // Paginação
                    bInfo: true, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdOrientadorDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdOrientadorDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdOrientadorDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdOrientadorDisponivel").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $('#divModalAssociarTamanho').modal('hide');
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdOrientadorDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdOrientadorDisponivel").style.display = "none";

                                var table_grdOrientadorDisponivel = $('#grdOrientadorDisponivel').DataTable();

                                $('#grdOrientadorDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdOrientadorDisponivel = table_grdOrientadorDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaOrientadorDisponivel?qCPF=" + qCPF + "&qNome=" + qNome + "&qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left"
                        },
                        {
                            "data": "P2", "title": "CPF", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P3", "title": "Adicionar", "orderable": false, "className": "text-center"
                        }
                    ],
                    order: [[1, 'asc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                fFechaProcessando();
            }
            
        }

        //============================================

        function fPerquisaCoorientadorDisponivel() {
            fProcessando();
            try {
                var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
                var qCPF = document.getElementById('txtCPFOrientadorPesquisa').value;
                var qNome = document.getElementById('txtNomeOrientadorPesquisa').value;
                var dt = $('#grdOrientadorDisponivel').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: true, //Pesquisar
                    bPaginate: true, // Paginação
                    bInfo: true, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdOrientadorDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdOrientadorDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdOrientadorDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdOrientadorDisponivel").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $('#divModalAssociarTamanho').modal('hide');
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdOrientadorDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdOrientadorDisponivel").style.display = "none";

                                var table_grdOrientadorDisponivel = $('#grdOrientadorDisponivel').DataTable();

                                $('#grdOrientadorDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdOrientadorDisponivel = table_grdOrientadorDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaCoorientadorDisponivel?qCPF=" + qCPF + "&qNome=" + qNome + "&qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left"
                        },
                        {
                            "data": "P2", "title": "CPF", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P3", "title": "Adicionar", "orderable": false, "className": "text-center"
                        }
                    ],
                    order: [[1, 'asc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                fFechaProcessando();
            }
            
        }

        //============================================

        function fIncluiAlteraOrientadorAluno(qId, qCPF, qNome) {
            fProcessando();
            var qIdOrientadorAnterior = document.getElementById('txtIdOrientador').value;
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiAlteraOrientadorAluno?qId=" + qId + "&qIdOrientadorAnterior=" + qIdOrientadorAnterior + "&qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão/Alteração de Orientador';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Inclusão/Alteração do Orientador: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheOrietacaoAluno(qIdTurma); //Na verdade é Coorientação da "Orientação"

                        if (document.getElementById('btnImprimirAtaQualificao').style.display != "block" && document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value != "Especialização" ) {
                            fPreencheBancaQualificacaoAluno(qIdTurma);
                        }
                        else if (document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value == "Especialização" && document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                            fPreencheBancaDefesaAluno(qIdTurma);
                        }

                        $('#divModalSelecionarOrientador').modal('hide');

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão/Alteração de Orientador</strong><br /><br />',
                            message: 'Inclusão/Alteração do Orientador <strong>' + qNome + '</strong> realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert("Houve um erro na Inclusão/Alteração do Orientador. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Inclusão/Alteração do Orientador. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================

        function fIncluiCoorientadorAluno(qId, qCPF, qNome) {
            fProcessando();
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiCoorientadorAluno?qId=" + qId + "&qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Co-orientador';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Inclusão do Co-orientador: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheOrietacaoAluno(qIdTurma); //Na verdade é Coorientação da "Orientação"
                        fPerquisaCoorientadorDisponivel(); //Para atualizar o modal de pesquisa que NÃO fecha após a escolha de um Professor.
                        
                        if (document.getElementById('btnImprimirAtaQualificao').style.display != "block") {
                            fPreencheBancaQualificacaoAluno(qIdTurma);
                        }
                        else if (document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value == "Especialização" && document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                             fPreencheBancaDefesaAluno(qIdTurma);
                        }
                        //$('#divModalSelecionarOrientador').modal('hide');

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Co-orientador</strong><br /><br />',
                            message: 'Inclusão/Inclusão do Co-orientador <strong>' + qNome + '</strong> realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert("Houve um erro na Inclusão de Co-orientador. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Inclusão de Co-orientador. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function fSalvarDadosOrientacao() {
            var qTitulo = document.getElementById('txtTituloOrientacaoNew').value;
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            var sAux = "";

            if (qTitulo == "") {
                sAux = sAux + "Deve-se digitar um Título da Orientação <br><br>"
                
            }
            
            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            fProcessando();
            
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fSalvarDadosOrientacao?qTitulo=" + qTitulo + "&qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Salvar dados da Orientação';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro ao Salvar dados da Orientação. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheOrietacaoAluno(qIdTurma); //Na verdade é Coorientação da "Orientação"

                        if (document.getElementById('btnImprimirAtaQualificao').style.display != "block" && document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value != "Especialização") {
                            fPreencheBancaQualificacaoAluno(qIdTurma);
                        }
                        else if (document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value == "Especialização" && document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                             fPreencheBancaDefesaAluno(qIdTurma);
                        }

                        $('#divModalSelecionarOrientador').modal('hide');

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Salvar dados da Orientação</strong><br /><br />',
                            message: 'Alteração de dados da Orientação realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert("Houve um erro ao Salvar dados da Orientação. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro ao Salvar dados da Orientação. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function fApagarDadosOrientacao() {
            fProcessando();
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fApagarDadosOrientacao?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Apagar dados da Orientação';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro ao Apagar os dados da Orientação. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheOrietacaoAluno(qIdTurma); //Na verdade é Coorientação da "Orientação"
                        if (document.getElementById('btnImprimirAtaQualificao').style.display != "block") {
                            fPreencheBancaQualificacaoAluno(qIdTurma);
                        }
                        else if (document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value == "Especialização" && document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                             fPreencheBancaDefesaAluno(qIdTurma);
                        }

                        $('#divModalApagarDadosOrientacao').modal('hide');

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Apagar dados da Orientação</strong><br /><br />',
                            message: 'Exclusão dos dados da Orientação realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert("Houve um erro ao Apagar os dados da Orientação. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro ao Apagar os dados da Orientação. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function fExcluirCoorientador() {
            fProcessando();
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            var qIdCoorientador = document.getElementById('lblIdCoorientador').innerHTML;
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fExcluirCoorientador?qIdTurma=" + qIdTurma + "&qIdCoorientador=" + qIdCoorientador + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Co-orientador';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Exclusão do Co-orientador. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheOrietacaoAluno(qIdTurma); //Na verdade é Coorientação da "Orientação"
                        
                        if (document.getElementById('btnImprimirAtaQualificao').style.display != "block") {
                            fPreencheBancaQualificacaoAluno(qIdTurma);
                        }
                        else if (document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value == "Especialização" && document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                             fPreencheBancaDefesaAluno(qIdTurma);
                        }

                        $('#divModalExcluirCoorientador').modal('hide');

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão de Co-orientador</strong><br /><br />',
                            message: 'Exclusão de Co-orientador realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert("Houve um erro na Exclusão de Co-orientador. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Exclusão de Co-orientador. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function fExcluirProfessorBanca() {
            fProcessando();
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            var qTipoProfessor = document.getElementById('lblTipoProfessorBanca').innerHTML;
            var qIdBanca = document.getElementById('lblIdBanca').innerHTML;
            var qIdProfessor = document.getElementById('lblIdProfessorBanca').innerHTML;
            var qNomeProfessor = document.getElementById('lblNomeProfessorBanca').innerHTML;
            var qBanca = document.getElementById('lblTipoBanca').innerHTML;

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fExcluirProfessorBanca?qTipoProfessor=" + qTipoProfessor + "&qIdBanca=" + qIdBanca + "&qIdProfessor=" + qIdProfessor + "&qNomeProfessor=" + qNomeProfessor + "&qBanca=" + qBanca + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de ' + qTipoProfessor + ' da banca de ' + qBanca;
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Exclusão do ' + qTipoProfessor + ' <strong>' + qNomeProfessor + '</strong> da banca de ' + qBanca + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        if (qTipoProfessor == "Co-orientador") {
                            fPreencheBancaCoorientador(qIdTurma, qBanca);
                        }
                        else {
                            fPreencheBancaMembro(qIdTurma, qBanca);
                        }

                        if (document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                            fPreencheBancaDefesaAluno(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);
                        }

                        $('#divModalExcluirMembroBanca').modal('hide');

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão de ' + qTipoProfessor + ' da banca de ' + qBanca + '</strong><br /><br />',
                            message: 'Exclusão de ' + qTipoProfessor + ' da banca de ' + qBanca + ' realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert('Houve um erro na Exclusão de ' + qTipoProfessor + ' da banca de ' + qBanca + '. Por favor tente novamente.');
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert('Houve um erro na Exclusão de ' + qTipoProfessor + ' da banca de ' + qBanca + '. Por favor tente novamente!');
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function funcModalAdicionaOrientador(qTipo) {
            if (qTipo == "Selecionar") {
                document.getElementById("lblTituloModalSelecionarOrientador").innerHTML = "Selecionar Orientador";
                document.getElementById("btnPerquisaOrientadorDisponivel").style.display = "block";
                document.getElementById("btnPerquisaCoorientadorDisponivel").style.display = "none";
            }
            else if (qTipo == "Alterar") {
                document.getElementById("lblTituloModalSelecionarOrientador").innerHTML = "Alterar Orientador";
                document.getElementById("btnPerquisaOrientadorDisponivel").style.display = "block";
                document.getElementById("btnPerquisaCoorientadorDisponivel").style.display = "none";
            }
            else {
                document.getElementById("lblTituloModalSelecionarOrientador").innerHTML = "Adicionar Co-orientador";
                document.getElementById("btnPerquisaOrientadorDisponivel").style.display = "none";
                document.getElementById("btnPerquisaCoorientadorDisponivel").style.display = "block";
            }

            document.getElementById("divgrdOrientadorDisponivel").style.display = "none";
            $('#divModalSelecionarOrientador').modal('show');  
        }

        //============================================================================


        function fModalApagarDadosOrientacao() {
            $('#divModalApagarDadosOrientacao').modal('show');  
        }

        //============================================================================

        function fModalExcluirCoorientador(qIdProfessor, qNomeProfessor) {
            document.getElementById('lblIdCoorientador').innerHTML = qIdProfessor;
            document.getElementById('lblNomeCoorientador').innerHTML = qNomeProfessor;
            $('#divModalExcluirCoorientador').modal('show');  
        }

        //============================================================================

        function fModalExcluirMembroBanca(qIdBanca, qIdProfessor, qNomeProfessor, qTipoProfessor, qBanca) {
            document.getElementById('lblTipoProfessorBanca').innerHTML = qTipoProfessor;
            document.getElementById('lblTipoProfessorBanca2').innerHTML = qTipoProfessor;
            document.getElementById('lblIdBanca').innerHTML = qIdBanca;
            document.getElementById('lblIdProfessorBanca').innerHTML = qIdProfessor;
            document.getElementById('lblNomeProfessorBanca').innerHTML = qNomeProfessor;
            document.getElementById('lblTipoBanca').innerHTML = qBanca;
            $('#divModalExcluirMembroBanca').modal('show');  
        }

        //============================================================================

        function fExcluirOrientador(qIdOrientacao, qIdAluno, qTurmaOrientador,qNomeProfessor,qIdProfessor,qPapel) {
            document.getElementById('lblOrientador').value = qIdProfessor;
            document.getElementById('lblOrientador').value = qIdAluno;
            document.getElementById('lblOrientador').value = qTurmaOrientador;
            document.getElementById('lblOrientador').value = qIdOrientacao;
            document.getElementById('lblOrientador').innerHTML = qNomeProfessor;
            $('#divModalApagarOrientador').modal('show'); 
        }

        //============================================================================

        function fConfirmacaoExcluirOrientador(){
            $("tabDadosAluno").removeClass("active");
            $("tabSituacaoAcademica").addClass("active");
            $.notify({
                icon: 'fa fa-check',
                title: '<strong>Atenção! </strong><br /><br />',
                message: 'O orientador foi excluído com êxito.<br />',
            },{
                type: 'success',
                animate: {
                    enter: 'animated flipInY',
                    exit: 'animated flipOutX'
                },
                placement: {
                    from: "top",
                    align: "center"
                }
            });   
            
            document.getElementById("tabHistoricoAluno").classList.remove("active");
            document.getElementById("tabOrientacaoAluno").classList.add("active");
            
            document.getElementById("tab_HistoricoAluno").classList.remove("active");
            document.getElementById("tab_OrientacaoAluno").classList.add("active");

        }       

        //============================================================================

        function funcApagaBotoesHistorico() {
            document.getElementById('btnImprimirHitoricoOff').style.display = 'none'; 
            document.getElementById('btnImprimirHitoricoOficialOff').style.display = 'none'; 
        }

        //============================================================================

        function InvalidoZero(textbox) {
    
            if (textbox.value == 0) {
                textbox.setCustomValidity('Required email address');
            }
            else if(textbox.validity.typeMismatch){
                textbox.setCustomValidity('please enter a valid email addressssss');
            }
            else {
                textbox.setCustomValidity('');
            }
            return true;
        }

        //=========================================================

        function fSalvarDadosArtigo() {
            //return;
            var sAux = "";

            if (document.getElementById('<%=txtNomeArtigo.ClientID%>').value == '') {
                 sAux = sAux + "Deve-se digitar o Nome do Artigo. <br /><br />";
            }

            if (document.getElementById('txtDataEntregaArtigoNew').value == '') {
                 sAux = sAux + "Deve-se digitar a Data de Entrega do Artigo. <br /><br />";
            }

            if (document.getElementById('<%=txtArquivoArtigo.ClientID%>').value =="") {
                sAux = sAux + "Deve-se selecionar o Arquivo do Artigo para o <em>Upload</em> <br /><br />";
            }
            
            if (document.getElementById('<%=txtOrientadorArtigo.ClientID%>').value =="") {
                sAux = sAux + "Deve-se digitar o Nome do Orientador do Artigo <br /><br />";
            }

            if (sAux != '') {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção ';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                return;
            }

            var formData = new FormData();
            formData.append("qIdAlunoArtigo", document.getElementById('<%=txtIdAlunoArtigo.ClientID%>').value);
            formData.append("qIdTurmaAluno", document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);
            formData.append("qDataArtigo", document.getElementById('txtDataEntregaArtigoNew').value);
            formData.append("qNomeArtigo", document.getElementById('<%=txtNomeArtigo.ClientID%>').value);
            formData.append("qDataAprovacaoArtigo", document.getElementById('txtDataAprovacaoArtigoNew').value);
            formData.append("qOrientadorArtigo", document.getElementById('<%=txtOrientadorArtigo.ClientID%>').value);
            formData.append("qTab", document.getElementById('hQTab').value);

            var files = $("#<%=fileArtigo.ClientID%>")[0].files;
            $.each(files, function (idx, file) {
                formData.append("qArquivo",  file);
                formData.append("qOrigem",  "1");
            });

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fSalvarDadosArtigo",//?qId=" + document.getElementById('<%//=txtIdTurmaAlunoNew.ClientID%>').value + "&qDataArtigo=" + document.getElementById('txtDataEntregaArtigoNew').value,
                data: formData,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração Dados do Aluno na Turma ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração de dados do Aluno na Turma. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        document.getElementById('<%=txtIdAlunoArtigo.ClientID%>').value = json[0].P1;
                        document.getElementById('<%=txtDataUploadArtigo.ClientID%>').value = json[0].P2;
                        document.getElementById('<%=txtUsuarioArtigo.ClientID%>').value = json[0].P3;
                        document.getElementById('<%=aDownLoadArtigo.ClientID%>').href = json[0].P4;

                        //document.getElementById('divBotaoSalvarContrato').style.display = 'none'; //
                        document.getElementById('<%=btnLocalizarArtigo.ClientID%>').style.display = 'block';
                        document.getElementById('<%=btnCancelarArtigo.ClientID%>').style.display = 'none'; //
                        document.getElementById('divBotaoDowloadArtigo').style.display = 'block';

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração Entrega Artigo</strong><br /><br />',
                            message: 'Alteração na Entrega do Artigo do Aluno na Turma realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Alteração da entrega do Artigo do Aluno na Turma. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração da entrega do Artigo do Aluno na Turma. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //=========================================================

        function SalvarDadosFoto() {
            var formData = new FormData();
            formData.append("qTab", document.getElementById('hQTab').value)
            if (qOrigem == 1) {
                 var files = $("#<%=fileArquivoParaGravar.ClientID%>")[0].files;
                $.each(files, function (idx, file) {
                    formData.append("qArquivo",  file);
                    formData.append("qOrigem",  "1");
                });
            }
            else {
                formData.append("qArquivo",  WebCam_data_uri_clone);
                formData.append("qOrigem",  "2");
            }

            $.ajax({
                url: "wsSapiens.asmx/AlteraFoto",
                data: formData,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                success: function (json) {
                    
                    if (json[0].P0 == "ok") {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Atenção! </strong><br /><br />',
                            message: 'Foto alterada com sucesso.',
                        },{
                            type: 'success',
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        document.getElementById('<%=imgAluno.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
                        document.getElementById('<%=imgFotoOriginal.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
                        LimparArquivo();
                        $('#divModalAlteraDadosFoto').modal('hide');

                    }
                    else {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Atenção! </strong><br /><br />',
                            message: 'Houve um problema na alteração da foto.<br />' + json[0].P1,
                        },{
                            type: 'danger',
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        //document.getElementById('<%=imgAluno.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
                        //document.getElementById('<%=imgFotoOriginal.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
                        //LimparArquivo();
                        $('#divModalAlteraDadosFoto').modal('hide');
                    }
                },
                error: function (xmlHttpRequest, status, err) {
                    //alert(xmlHttpRequest.status);
                    //alert(JSON.parse(xhr.responseText).Message);
                    document.getElementById('lblErroCabecalho').innerHTML = 'Erro na alteração da foto';
                    document.getElementById('lblErroCorpo').innerHTML = 'Erro na alteração da foto <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;
                    $('#divModalAlteraDadosFoto').modal('hide');
                    $('#divModalErro').modal('show');
                }
            });

        }

        //============================================================================

        function LimparArquivo() {
            document.getElementById('<%=imgprw.ClientID%>').src='abreimagem.aspx';
            document.getElementById('divBotoes').style.display='none'; 
            document.getElementById('divBotaoSalvar').style.display='none'; 
            document.getElementById('divImgPrw').style.display = 'none'; 
            document.getElementById('divMensagens').style.display = 'block'; 
            document.getElementById('divBntLocalizar').style.display = 'block';
            $("#<%=fileArquivoParaGravar.ClientID%>").val(null);
        }

        function AbreModalDadosFoto() {
            $('#divModalAlteraDadosFoto').modal('show');
        }

        function imagePreview(input) {
                
            var vFileExt = input.value.split('.').pop();
            if (vFileExt.toUpperCase() == "JPEG" || vFileExt.toUpperCase() == "JPG" || vFileExt.toUpperCase() == "PNG") {

                if (input.files[0].size < 1048576) {

                    document.getElementById('divBotoes').style.display = 'block';
                    document.getElementById('divBotaoSalvar').style.display = 'block';

                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgprw.ClientID%>').attr('src', e.target.result);
                        }

                        reader.readAsDataURL(input.files[0]);
                        }

                        $("#<%=fileArquivoParaGravar.ClientID%>").change(function () {
                            imagePreview(this);
                        });
                        document.getElementById('divImgPrw').style.display = 'block';
                        document.getElementById('divMensagens').style.display = 'none';
                        document.getElementById('divBntLocalizar').style.display = 'none';
                        document.getElementById('hEscolheuFoto').value = 'true';
                        qOrigem = "1";

                    } else {
                        document.getElementById('divExtencao').style.display = 'none';
                        document.getElementById('divTamanho').style.display = 'block';
                        document.getElementById('hEscolheuFoto').value = 'false';
                        javascript: document.getElementById('adivModal').click();
                    }

                } else {

                    document.getElementById('divExtencao').style.display = 'block';
                    document.getElementById('divTamanho').style.display = 'none';
                    document.getElementById('hEscolheuFoto').value = 'false';
                    javascript: document.getElementById('adivModal').click();
                }

            }

            function fDetalheDisciplina(qDisciplina){

                $.ajax({
                    type: "post",
                    url: "wsSapiens.asmx/DetalheDisciplina",
                    contentType: 'application/json; charset=utf-8',
                    //data: "{idOferecimento:'" + 'SP' + "', n:'" + '5' + "'}",
                    data: "{idOferecimento:'" + qDisciplina + "'}",
                    dataType: 'json',
                    success: function (data, status) {
                        //Tratando o retorno com parseJSON
                        var itens = $.parseJSON(data.d);
                        document.getElementById('lblNomeDisciplinaModalDisciplina').innerHTML = itens[0].NomeDisciplina;
                        document.getElementById('lblCodigoDisciplinaModalDisciplina').innerHTML = itens[0].CodigoDisciplina;
                        document.getElementById('lblQuadrimestreModalDisciplina').innerHTML = itens[0].Quadrimestre;
                        document.getElementById('lblNoOferecimentoModalDisciplina').innerHTML = itens[0].NumeroOferecimento;
                        document.getElementById('lblObjetivoModalDisciplina').innerHTML = itens[0].Objetivo;
                        document.getElementById('lblEmentaModalDisciplina').innerHTML = itens[0].Ementa;
                        document.getElementById('lblBibliografiaBasicaModalDisciplina').innerHTML = itens[0].BibliografiaBasica;
                        document.getElementById('lblBibliografiaComplementarModalDisciplina').innerHTML = itens[0].NomeDisciplina;
                        document.getElementById('lblFormaAvaliacaoModalDisciplina').innerHTML = itens[0].FormaAvaliacao;
                        document.getElementById('lblObservacaoModalDisciplina').innerHTML = itens[0].Observacao;

                        $('#divModalDisciplina').modal('show');
                        ////Alert com o primeiro item
                        //alert(itens[0].NomeDisciplina);
                        ////Respondendo na tela todos os itens
                        //alert(data.d);

                        //alert(status);
                    },
                    error: function (xmlHttpRequest, status, err) {
                        document.getElementById('lblErroCabecalho').innerHTML = 'Erro para exibir Dados do Oferecimento';
                        document.getElementById('lblErroCorpo').innerHTML = 'Erro para exibir Dados do Oferecimento <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;

                        $('#divModalErro').modal('show');
                    }
                });

                
            }

            function fDetalhePresenca(qDisciplina, qAluno){
                $.ajax({
                    type: "post",
                    url: "wsSapiens.asmx/ListaPresenca",
                    contentType: 'application/json; charset=utf-8',
                    //data: "{idOferecimento:'" + 'SP' + "', n:'" + '5' + "'}",
                    data: "{idOferecimento:'" + qDisciplina + "', idAluno:'" + qAluno + "'}",
                    dataType: 'json',
                    async: false,
                    success: function (data, status) {
                        //Tratando o retorno com parseJSON
                        //alert (data);
                        var itens = $.parseJSON(data.d);
                        if (itens == "") {
                            $.notify({
                                icon: 'fa fa-check',
                                title: '<strong>Atenção! </strong><br /><br />',
                                message: 'Não há lista de presença para essa disciplina.<br />',
                            },{
                                type: 'warning',
                                animate: {
                                    enter: 'animated flipInY',
                                    exit: 'animated flipOutX'
                                },
                                placement: {
                                    from: "top",
                                    align: "center"
                                }
                            });    
                        }
                        else {
                            document.getElementById('lblCodigoDisciplinaModalPresenca').innerHTML = itens[0].CodigoDisciplina;
                            document.getElementById('lblNomeDisciplinaModalPresenca').innerHTML = itens[0].NomeDisciplina;
                            $('#tabPresenca').DataTable( {
                                processing : true,
                                destroy: true,
                                "paging": false,
                                "searching": false,
                                "ordering": false,
                                "info":     false,
                                "aaData": itens,
                                "aoColumns": [
                                    { "mDataProp": "NumeroAula" },
                                    { "mDataProp": "DataAula" },
                                    { "mDataProp": "HoraInicio" },
                                    { "mDataProp": "HoraFim" },
                                    { "mDataProp": "Presente" }
                                ],
                                "createdRow": function ( row, data1, index ) {
                                    if ( data1.Presente == 'Ausente') {
                                        $('td', row).eq(4).addClass('highlight');
                                    }
                                }
                            } );
                            $('#divModalPresenca').modal('show');
                        }
                    },
                    error: function (xmlHttpRequest, status, err) {
                        document.getElementById('lblErroCabecalho').innerHTML = 'Erro para exibir Dados de Presença';
                        document.getElementById('lblErroCorpo').innerHTML = 'Erro para exibir dados de Presença do Aluno<br/> Erro: ' + err + '<br/>Status do erro: ' + status ;

                        $('#divModalErro').modal('show');
                    }
                });
                
            }

            function ehNumeroOuTRaco(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && charCode != 45 && charCode != 46 &&  (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            function soNumero(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                //alert(charCode);
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            function soNumeroeX(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                //alert(charCode);
                if (charCode > 31 && charCode != 120 && charCode != 88 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            //function semLetras(inputtxt)  
            //{  
            //    var str = inputtxt.value;
            //    alert(str);
            //    for (var i = 0, len = str.length; i < len; i++) {
            //        if (str[i] > 31 && str[i] != 45 && str[i] != 46 &&  (str[i] < 48 || str[i] > 57)) {
            //            alert('sim ' + str[i]);
            //            break;
            //        }
            //        else {
            //            alert('não ' + str[i].charCodeAt(0));
            //        }
                    
            //    }
            //}  

            
            
        $(document).ready(function () {
            //alert("document.ready");
            //fProcessando();

            fEstrangeiro();
            fEstrangeiro2();
            fPaisEmpresa();
            fPaisResidencia();
            fPreencheDataLimiteDocumentacao();
                
            //fSelect2()

            $('#<%=txtCPFAluno.ClientID%>').mask('999.999.999-99');
            $('#<%=txtCepResidenciaAluno.ClientID%>').mask('99999-999');
            $('#<%=txtCNPJEmpresaAluno.ClientID%>').mask('99.999.999/9999-99');
            $('#<%=txtCEPEmpresaAluno.ClientID%>').mask('99999-999');
            $('#<%=txtTelefoneAluno.ClientID%>').mask('99-9999-9999');
            $('#<%=txtCelularAluno.ClientID%>').mask('99-99999-9999');
            $('#txtCPFOrientadorPesquisa').mask('999.999.999-99');

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });

                
            $("#frmMaster").validate({
                debug:false,
                rules: {
                    <%=txtNomeAluno.UniqueID%>: "required",
                    <%=txtCPFAluno.UniqueID%>: "required",
                    <%=txtEmail1Aluno.UniqueID%>: "required",
                },
                messages: {
                    <%=txtNomeAluno.UniqueID%>: "Digite o nome do Aluno",
                    <%=txtCPFAluno.UniqueID%>: "Digite o CPF do Aluno",
                    <%=txtEmail1Aluno.UniqueID%>: "Digite o Email do Aluno",
                }
                
            });

            if (document.getElementById('<%=tabSituacaoAcademicaNew.ClientID%>').style.display != 'none') {
                document.getElementById('<%=divDocumentosEntregues.ClientID%>').style.display = 'block';
                fPreencheGrupoTurmaAluno();
                fPreencheDocumentosObrigatorios();
                fPreencheDocumentosNaoObrigatorios();
            }

            $("#txtValorTotal").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: false,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });
            
            $("#txtValorDisciplina").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtValorParcela").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });
        });

        //==========================================

        function fValidaRG(qTecla) {
            var charCode = (qTecla.which) ? qTecla.which : qTecla.keyCode;
            if ((charCode >= 48 && charCode <= 57) || charCode == 45 || charCode == 46 || charCode == 88 || charCode == 120)
            {
                //alert('válido: ' + charCode);
                return true
            }
            else {
                //alert('Inválido: ' + charCode);
                return false
            }
        }

        //==========================================
        function isInteger(value) {
            return !isNaN(value) && (function (x) { return (x | 0) === x; })(parseFloat(value))
        }

        //================================================================================

        function fPreencheTurmaAluno(qIdTurma) {
            fProcessando();
            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheTurmaAluno?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                        fFechaProcessando();
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Preenchimnto de dados da Turma ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro no preenchimnto de dados da Turma. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                        fFechaProcessando();
                    }
                    else {
                        //document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value = qIdTurma;
                        document.getElementById('<% =txtCodTurmaAlunoNew.ClientID%>').value = json[0].P0;
                        document.getElementById('<% =txtQuadrimestreAlunoNew.ClientID%>').value = json[0].P1;
                        document.getElementById('<% =txtTipoCursoAlunoNew.ClientID%>').value = json[0].P2;
                        document.getElementById('<% =txtDataInicioCursoAlunoNew.ClientID%>').value = json[0].P3;
                        document.getElementById('<% =txtDataFimCursoAlunoNew.ClientID%>').value = json[0].P4;
                        document.getElementById('<% =txtDataTerminoCursoAlunoNew.ClientID%>').value = json[0].P5;
                        document.getElementById('<% =txtCursoAlunoNew.ClientID%>').value = json[0].P6;
                        document.getElementById('<% =txtAreaConcentracaoAlunoNew.ClientID%>').value = json[0].P7;
                        document.getElementById('<% =txtSituacaoAlunoNew.ClientID%>').value = json[0].P8;
                        document.getElementById('txtDataEntregaArtigoNew').value = json[0].P9;
                        //Novos campos do Artigo
                        document.getElementById('<% =txtNomeArtigo.ClientID%>').value = json[0].P21;
                        document.getElementById('txtDataAprovacaoArtigoNew').value = json[0].P22;
                        document.getElementById('<% =txtOrientadorArtigo.ClientID%>').value = json[0].P23;

                        document.getElementById('divBotaoSalvarContrato').style.display = 'none'; //
                        document.getElementById('<% =btnLocalizarContrato.ClientID%>').style.display = 'block';
                        document.getElementById('<% =btnCancelarContrato.ClientID%>').style.display = 'none'; //

                        if (json[0].P11 != "") {
                           document.getElementById('divBotaoDowloadContrato').style.display = 'block';
                            document.getElementById('<% =txtNomeContrato.ClientID%>').value = json[0].P11;
                            document.getElementById('<% =txtDataUploadContrato.ClientID%>').value = json[0].P12;
                            document.getElementById('<% =txtUsuarioContrato.ClientID%>').value = json[0].P13;
                            document.getElementById('<% =txtIdAlunoArquivo.ClientID%>').value = json[0].P14;
                            document.getElementById('<% =aDownLoadContrato.ClientID%>').href = json[0].P15;
                        }
                        else {
                            document.getElementById('divBotaoDowloadContrato').style.display = 'none';
                            document.getElementById('<% =txtNomeContrato.ClientID%>').value = '';
                            document.getElementById('<% =txtDataUploadContrato.ClientID%>').value = '';
                            document.getElementById('<% =txtUsuarioContrato.ClientID%>').value = '';
                            document.getElementById('<% =txtIdAlunoArquivo.ClientID%>').value = '';
                        }

                        document.getElementById('divBotaoSalvarArtigo').style.display = 'block'; // 'none'
                        document.getElementById('<% =btnLocalizarArtigo.ClientID%>').style.display = 'block';
                        document.getElementById('<% =btnCancelarArtigo.ClientID%>').style.display = 'none'; //

                        if (json[0].P16 != "") {
                           document.getElementById('divBotaoDowloadArtigo').style.display = 'block';
                            document.getElementById('<% =txtArquivoArtigo.ClientID%>').value = json[0].P16;
                            document.getElementById('<% =txtDataUploadArtigo.ClientID%>').value = json[0].P17;
                            document.getElementById('<% =txtUsuarioArtigo.ClientID%>').value = json[0].P18;
                            document.getElementById('<% =txtIdAlunoArtigo.ClientID%>').value = json[0].P19;
                            document.getElementById('<% =aDownLoadArtigo.ClientID%>').href = json[0].P20;
                        }
                        else {
                            document.getElementById('divBotaoDowloadArtigo').style.display = 'none';
                            document.getElementById('<% =txtArquivoArtigo.ClientID%>').value = '';
                            document.getElementById('<% =txtDataUploadArtigo.ClientID%>').value = '';
                            document.getElementById('<% =txtUsuarioArtigo.ClientID%>').value = '';
                            document.getElementById('<% =txtIdAlunoArtigo.ClientID%>').value = '';
                        }
                        
                        $("#<%=tabHistoricoAlunoNew.ClientID%>").addClass("hidden");
                        $("#<%=tabOrientacaoAlunoNew.ClientID%>").addClass("hidden");
                        $("#<%=tabBancaAlunoNew.ClientID%>").addClass("hidden");
                        $("#<%=tabProrrogacaoCPG.ClientID%>").addClass("hidden");
                        $("#<%=tabContrato.ClientID%>").addClass("hidden");
                        $("#<%=tabCertificado.ClientID%>").addClass("hidden");
                        $("#<%=tabBancaQualificacao.ClientID%>").removeClass("hidden");
                        $("#<%=tab_BancaQualificacao.ClientID%>").addClass("active");
                        $("#<%=tab_BancaDefesa.ClientID%>").removeClass("active");
                        document.getElementById('lblDissertacao_TCC_1').innerHTML = "Dissertação";
                        document.getElementById('lblDissertacao_TCC_2').innerHTML = "Dissertação";
                        document.getElementById('lblBtnSalvarDissertacao').innerHTML = "Salvar Dissertação";
                        
                        if (json[0].P10 == "1") { //mestrado
                            $("#<%=tabHistoricoAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabOrientacaoAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabBancaAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabProrrogacaoCPG.ClientID%>").removeClass("hidden");
                            $("#<%=tabContrato.ClientID%>").removeClass("hidden");
                            $("#<%=tabCertificado.ClientID%>").removeClass("hidden");
                        }
                        else if (json[0].P10 == "3") { //especialização
                            $("#<%=tabHistoricoAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabOrientacaoAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabBancaAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabContrato.ClientID%>").removeClass("hidden");
                            $("#<%=tabCertificado.ClientID%>").removeClass("hidden");
                            $("#<%=tabBancaQualificacao.ClientID%>").addClass("hidden");
                            $("#<%=tab_BancaQualificacao.ClientID%>").removeClass("active");
                            $("#<%=tab_BancaDefesa.ClientID%>").addClass("active");
                            document.getElementById('lblDissertacao_TCC_1').innerHTML = "Monografia";
                            document.getElementById('lblDissertacao_TCC_2').innerHTML = "Monografia";
                            document.getElementById('lblBtnSalvarDissertacao').innerHTML = "Monografia";

                        }
                        else if (json[0].P10 == "4") { //Curta Duração
                            $("#<%=tabContrato.ClientID%>").removeClass("hidden");
                            $("#<%=tabCertificado.ClientID%>").removeClass("hidden");
                        }
                        else if (json[0].P10 == "2") { //MBA Internacional
                            $("#<%=tabHistoricoAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabOrientacaoAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabBancaAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabContrato.ClientID%>").removeClass("hidden");
                            $("#<%=tabCertificado.ClientID%>").removeClass("hidden");
                        }
                         else if (json[0].P10 == "5") { //Educação Corporativa
                            $("#<%=tabHistoricoAlunoNew.ClientID%>").removeClass("hidden");
                            $("#<%=tabCertificado.ClientID%>").removeClass("hidden");
                        }

                        fPreencheComboContrato(qIdTurma);
                        //$("#ddlTurmaAlunoNew").val(json[0].id).trigger("change");

                        if (document.getElementById('<%=tabHistoricoMatriculaNew.ClientID%>').style.display != 'none') {
                            //alert("entrou Histórico Matricula");
                            fPreencheHistoricoMatricula(qIdTurma);
                        }
                        if (document.getElementById('<%=tabHistoricoAlunoNew.ClientID%>').style.display != 'none') {
                            //alert("entrou Histórico Aluno");
                            fPreencheHistoricoAluno(qIdTurma);
                        }
                        if (document.getElementById('<%=tabOrientacaoAlunoNew.ClientID%>').style.display != 'none') {
                            //alert("entrou Histórico Aluno");
                            fPreencheOrietacaoAluno(qIdTurma); //Na verdade é Coorientação da "Orientação"
                        }
                        if (document.getElementById('<%=tabBancaAlunoNew.ClientID%>').style.display != 'none') {
                            //alert("entrou Histórico Aluno");
                            fPreencheBancaQualificacaoAluno(qIdTurma);
                            fPreencheBancaDefesaAluno(qIdTurma);
                        }
                        if (document.getElementById('<%=tabProrrogacaoCPG.ClientID%>').style.display != 'none') {
                            //alert("entrou Histórico Aluno");
                            fPreencheProrrogacaoCPG(qIdTurma);
                        }
                        
                        fFechaProcessando();
                    }
                },
                error: function(xhr){
                    alert("Houve um erro no preenchimnto de dados da Turma. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro no preenchimnto de dados da Turma. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
            } catch (e) {
                fFechaProcessando();
            }
        }

        //==========================================

        function fPreencheGrupoTurmaAluno() {

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheGrupoTurmaAluno?qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de carregamento do grupo da Turma ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de carregamento do grupo da Turma. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //alert(json.length)
                        //=== verifica se tem turma passando por querystring
                        var url_string = window.location.href; // www.test.com?qIdTurma=test
                        var url = new URL(url_string);
                        var paramValue = url.searchParams.get("qIdTurma");
                        var qIdTurma = 0;
                        if (paramValue != null) {
                            //alert(paramValue);
                            if (isInteger(paramValue)) {
                                qIdTurma = paramValue;
                                window.history.pushState("object or string", "Title", "/cadAlunoGestao.aspx");
                            }
                        }

                        if (json.length > 1) {
                            document.getElementById('divTurmaDiversasNew').style.display = 'block';
                            document.getElementById('divTurmaTemNew').style.display = 'block';
                            document.getElementById('divTurmaNaoTemNew').style.display = 'none';
                            $("#ddlTurmaAlunoNew").empty();
                            $('#ddlTurmaAlunoNew').select2({ data: json });
                            fSelect2();
                            $("#ddlTurmaAlunoNew").val(json[0].id).trigger("change");
                            fPreencheTurmaAluno(json[0].id);
                            if (qIdTurma != "0") {
                                document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value = qIdTurma;
                            }
                            else {
                                document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value = json[0].id;
                            }
                            
                        }
                        else if (json[0].id != "Nada") {
                            document.getElementById('divTurmaDiversasNew').style.display = 'none';
                            document.getElementById('divTurmaTemNew').style.display = 'block';
                            document.getElementById('divTurmaNaoTemNew').style.display = 'none';
                            fPreencheTurmaAluno(json[0].id);
                            if (qIdTurma != "0") {
                                document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value = qIdTurma;
                            }
                            else {
                                document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value = json[0].id;
                            }
                        }
                        else {
                            document.getElementById('divTurmaDiversasNew').style.display = 'none';
                            document.getElementById('divTurmaTemNew').style.display = 'none';
                            document.getElementById('divTurmaNaoTemNew').style.display = 'block';
                        }
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de carregamento do grupo da Turma. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de carregamento do grupo da Turma. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //================================================================================

        $('#ddlTurmaAlunoNew').on("select2:select", function(e) { 
            fPreencheTurmaAluno($(this).val());
            document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value = $(this).val();
        });

        //================================================================================

        function fPreencheDocumentosObrigatorios() {
            try {
                var dt = $('#grdDocumentosObrigatorios').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);

                        //dt.columns( [5,6] ).visible( false );

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdDocumentosObrigatorios").style.display = "none";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdDocumentosObrigatorios").style.display = "none";
                                //document.getElementById("msgSemResultadosgrdHistoricoMatriculaNew").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheDocumentosObrigatorios?qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Ordem", "orderable": false, "className": "hidden text-center centralizarTH", width: "0px"
                        },
                        {
                            "data": "P1", "title": "Documento", "orderable": false, "className": "text-center centralizarTH", width: "30px"
                        },
                        {
                            "data": "P2", "title": "Situação", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P3", "title": "Data Upload", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: "date-euro"
                        },
                        {
                            "data": "P4", "title": "Usuário", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P5", "title": "Download", "orderable": false, "className": "text-center centralizarTHt", width: '10px'
                        },
                         {
                            "data": "P6", "title": "Editar", "orderable": false, "className": "text-center centralizarTHt", width: '10px'
                        },
                        {
                            "data": "P7", "title": "Apagar", "orderable": false, "className": "hidden text-center centralizarTH", width: "10px"
                        }
                    ],
                    //order: [[ 3, 'asc' ],[ 4, 'asc' ]],
                    order: [[ 0, 'asc' ]],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
        }

        //================================================================================
        function fEditarDocumentoObrigatorio(qIdDocumento, qIdTipoDocumento, qDescricaoDocumento, qArquivo) {
            document.getElementById('lblTituloDocumentosArquivo').innerHTML = 'Editar Documentos Obrigatórios';
            document.getElementById('btnPreencheDocumentosObrigatorios').style.display = 'block';
            document.getElementById('btnPreencheDocumentosNaoObrigatorios').style.display = 'none';
            document.getElementById('txtIdDocumentoObrigatorio').value = qIdDocumento;
            document.getElementById('txtIdTipoDocumento').value = qIdTipoDocumento;
            document.getElementById('txtDescricaoDocumentoObrigatorio').value = qDescricaoDocumento;
            document.getElementById('txtDescricaoDocumentoObrigatorio').readOnly = true;
            document.getElementById('txtArquivoDocumentoObrigatorio').value = qArquivo;
           <%-- if (qIdTipoDocumento != "7") {
                $("#<%=fileArquivo.ClientID%>").attr('accept','.pdf');
            }
            else {
                $("#<%=fileArquivo.ClientID%>").attr("accept", ".jpg,.jpeg,.png");
            }--%>

            $("#<%=fileArquivo.ClientID%>").val(null);
            
            $('#divModalPreencheDocumentosObrigatorios').modal();
        }
        //================================================================================

        function fSalvarDocumento(qOrigem) {
            fProcessando();
            if (qOrigem == 2) {
                var sAux = "";
                if (document.getElementById('txtDescricaoDocumentoObrigatorio').value.trim() == '') {
                    sAux = "Deve-se digitar o Nome do Documento não-obrigatório.<br><br>"
                }
                if (sAux != "") {
                    fFechaProcessando();
                    document.getElementById("btnPreencheDocumentosObrigatorios").disabled = false;
                    document.getElementById("btnPreencheDocumentosNaoObrigatorios").disabled = false;
                    document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'ATENÇÂO ';
                    document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                    $("#divCabecalho").removeClass("alert-success");
                    $("#divCabecalho").removeClass("alert-danger");
                    $('#divCabecalho').addClass('alert-warning');
                    $('#divMensagemModal').modal();
                    return;
                }
            }

            var formData = new FormData();
            formData.append("qIdDocumento", document.getElementById('txtIdDocumentoObrigatorio').value);
            formData.append("qIdTipoDocumento", document.getElementById('txtIdTipoDocumento').value);
            formData.append("qDescricao", document.getElementById('txtDescricaoDocumentoObrigatorio').value);
            formData.append("qTab", document.getElementById('hQTab').value);
            if (document.getElementById('txtArquivoDocumentoObrigatorio').value =="") {
                document.getElementById('lblErroCabecalho').innerHTML = 'Atenção';
                document.getElementById('lblErroCorpo').innerHTML = 'Deve-se selecionar um Arquivo para o <em>Upload</em>' ;
                $('#divModalErro').modal('show');
                return;
            }
            formData.append("qNomeArquivo", document.getElementById('txtArquivoDocumentoObrigatorio').value);

            var files = $("#<%=fileArquivo.ClientID%>")[0].files;
            $.each(files, function (idx, file) {
                formData.append("qArquivo",  file);
                formData.append("qOrigem",  "1");
            });

            $.ajax({
                url: "wsSapiens.asmx/fSalvarDocumento",
                data: formData,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                success: function (json) {
                    
                    if (json[0].P0 == "ok") {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Arquivo criado/alterado! </strong><br /><br />',
                            message: 'Arquivo criado/alterado com sucesso.',
                        },{
                            type: 'success',
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                            });
                        fPreencheDocumentosObrigatorios();
                        fPreencheDocumentosNaoObrigatorios();
                        $("#<%=fileArquivo.ClientID%>").val(null);
                    }
                    else {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Atenção! </strong><br /><br />',
                            message: 'Houve um problema na alteração do Arquivo.<br />' + json[0].P1,
                        },{
                            type: 'danger',
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                    }
                    $('#divModalPreencheDocumentosObrigatorios').modal('hide');
                    fFechaProcessando();
                },
                error: function (xmlHttpRequest, status, err) {
                    fFechaProcessando();
                    //alert(xmlHttpRequest.status);
                    //alert(JSON.parse(xhr.responseText).Message);
                    document.getElementById('lblErroCabecalho').innerHTML = 'Erro na alteração do Documento';
                    document.getElementById('lblErroCorpo').innerHTML = 'Erro na alteração do documento <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;
                    $('#divModalPreencheDocumentosObrigatorios').modal('hide');
                    $('#divModalErro').modal('show');
                }
            });
        }

        //================================================================================

        function fSalvarContrato(qOrigem) {
            var formData = new FormData();
            formData.append("qIdAlunoArquivo", document.getElementById('<%=txtIdAlunoArquivo.ClientID%>').value);
            formData.append("qIdTurmaAluno", document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);
            formData.append("qTab", document.getElementById('hQTab').value);
            if (document.getElementById('<%=txtNomeContrato.ClientID%>').value =="") {
                document.getElementById('lblErroCabecalho').innerHTML = 'Atenção';
                document.getElementById('lblErroCorpo').innerHTML = 'Deve-se selecionar um Arquivo para o <em>Upload</em>' ;
                $('#divModalErro').modal('show');
                return;
            }
            var files = $("#<%=fileContrato.ClientID%>")[0].files;
            $.each(files, function (idx, file) {
                formData.append("qArquivo",  file);
                formData.append("qOrigem",  "1");
            });

            $.ajax({
                url: "wsSapiens.asmx/fSalvarContrato",
                data: formData,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                success: function (json) {
                    
                    if (json[0].P0 == "ok") {
                        document.getElementById('<%=txtIdAlunoArquivo.ClientID%>').value = json[0].P1;
                        document.getElementById('<%=txtDataUploadContrato.ClientID%>').value = json[0].P2;
                        document.getElementById('<%=txtUsuarioContrato.ClientID%>').value = json[0].P3;
                        document.getElementById('<% =aDownLoadContrato.ClientID%>').href = json[0].P4;

                        document.getElementById('divBotaoSalvarContrato').style.display = 'none'; //
                        document.getElementById('<% =btnLocalizarContrato.ClientID%>').style.display = 'block';
                        document.getElementById('<% =btnCancelarContrato.ClientID%>').style.display = 'none'; //
                        document.getElementById('divBotaoDowloadContrato').style.display = 'block';
                        

                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Contrato criado/alterado! </strong><br /><br />',
                            message: 'Contrato criado/alterado com sucesso.',
                        },{
                            type: 'success',
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                            });
                        $("#<%=fileContrato.ClientID%>").val(null);
                    }
                    else {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Atenção! </strong><br /><br />',
                            message: 'Houve um problema na alteração do Arquivo.<br />' + json[0].P1,
                        },{
                            type: 'danger',
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                    }
                    $('#divModalPreencheDocumentosObrigatorios').modal('hide');
                },
                error: function (xmlHttpRequest, status, err) {
                    //alert(xmlHttpRequest.status);
                    //alert(JSON.parse(xhr.responseText).Message);
                    document.getElementById('lblErroCabecalho').innerHTML = 'Erro na alteração do Contrato';
                    document.getElementById('lblErroCorpo').innerHTML = 'Erro na alteração do Contrato <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;
                    $('#divModalAlteraDadosFoto').modal('hide');
                    $('#divModalErro').modal('show');
                }
            });
        }

        //================================================================================

        function fPreencheDocumentosNaoObrigatorios() {
            try {
                var dt = $('#grdDocumentosOutros').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);

                        //dt.columns( [5,6] ).visible( false );

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdDocumentosOutros").style.display = "none";
                            document.getElementById("msgSemResultadosgrdDocumentosOutros").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdDocumentosOutros").style.display = "none";
                                //document.getElementById("msgSemResultadosgrdHistoricoMatriculaNew").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else {
                                document.getElementById("divgrdDocumentosOutros").style.display = "block";
                                document.getElementById("msgSemResultadosgrdDocumentosOutros").style.display = "none";
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheDocumentosNaoObrigatorios?qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Ordem", "orderable": false, "className": "hidden text-center centralizarTH", width: "0px"
                        },
                        {
                            "data": "P1", "title": "Documento", "orderable": false, "className": "text-center centralizarTH", width: "50px"
                        },
                        {
                            "data": "P2", "title": "Data Upload", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: "date-euro"
                        },
                        {
                            "data": "P3", "title": "Usuário", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P4", "title": "Download", "orderable": false, "className": "text-center centralizarTHt", width: '10px'
                        },
                         {
                            "data": "P5", "title": "Editar", "orderable": false, "className": "text-center centralizarTHt", width: '10px'
                        },
                        {
                            "data": "P6", "title": "Apagar", "orderable": false, "className": "hidden text-center centralizarTH", width: "10px"
                        }
                    ],
                    //order: [[ 3, 'asc' ],[ 4, 'asc' ]],
                    order: [[ 0, 'asc' ]],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                        "sEmptyTable": "Nenhum registro encontrado",
                        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                        "sInfoPostFix": "",
                        "sInfoThousands": ".",
                        "sLengthMenu": "Mostrando _MENU_ resultados por página",
                        "sLoadingRecords": "Carregando...",
                        "sProcessing": "Processando...",
                        "sZeroRecords": "Nenhum registro encontrado",
                        "sSearch": "Pesquisar",
                        "oPaginate": {
                            "sNext": ">",
                            "sPrevious": "<",
                            "sFirst": "Primeiro",
                            "sLast": "Último"
                        },
                        "oAria": {
                            "sSortAscending": ": Ordenar colunas de forma ascendente",
                            "sSortDescending": ": Ordenar colunas de forma descendente"
                        }
                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
        }

        //========================================================================================

        function fEditarDocumentoNaoObrigatorio(qIdDocumento, qIdTipoDocumento, qDescricaoDocumento, qArquivo) {
            if (qIdDocumento == "0") {
                document.getElementById('lblTituloDocumentosArquivo').innerHTML = 'Criar Documento Não-Obrigatório';
            }
            else {
                document.getElementById('lblTituloDocumentosArquivo').innerHTML = 'Editar Documento Não-Obrigatório';
            }
            document.getElementById('btnPreencheDocumentosObrigatorios').style.display = 'none';
            document.getElementById('btnPreencheDocumentosNaoObrigatorios').style.display = 'block';
            document.getElementById('txtIdDocumentoObrigatorio').value = qIdDocumento;
            document.getElementById('txtIdTipoDocumento').value = qIdTipoDocumento;
            document.getElementById('txtDescricaoDocumentoObrigatorio').value = qDescricaoDocumento;
            document.getElementById('txtDescricaoDocumentoObrigatorio').readOnly = false;
            document.getElementById('txtArquivoDocumentoObrigatorio').value = qArquivo;
            //$("#<%=fileArquivo.ClientID%>").attr('accept','.pdf');

            $("#<%=fileArquivo.ClientID%>").val(null);
            
            $('#divModalPreencheDocumentosObrigatorios').modal();
        }

        //================================================================================

        function fPreencheHistoricoMatricula(qIdTurma) {
            try {
                var dt = $('#grdHistoricoMatriculaNew').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);

                        //dt.columns( [5,6] ).visible( false );

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdHistoricoMatriculaNew").style.display = "none";
                            document.getElementById("msgSemResultadosgrdHistoricoMatriculaNew").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdHistoricoMatriculaNew").style.display = "none";
                                document.getElementById("msgSemResultadosgrdHistoricoMatriculaNew").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                if (json[0].P7 == "Titulado") {
                                    document.getElementById("<%=btnCertificado.ClientID%>").style.display = "none";
                                    document.getElementById("<%=btnCertificadoCurta.ClientID%>").style.display = "none";
                                    document.getElementById("<%=btnCertificadoEspecializacao.ClientID%>").style.display = "none";
                                    document.getElementById("<%=btnCertificadoMBAInternacional.ClientID%>").style.display = "none";
                                    if (json[0].P12 == 1) {
                                        //Mestrado
                                        document.getElementById("<%=btnCertificado.ClientID%>").style.display = "block";
                                    }
                                    else if (json[0].P12 == 4) {
                                        //Curta
                                        document.getElementById("<%=btnCertificadoCurta.ClientID%>").style.display = "block";
                                    }
                                    else if (json[0].P12 == 3) {
                                        //Especialização
                                        document.getElementById("<%=btnCertificadoEspecializacao.ClientID%>").style.display = "block";
                                    }
                                    else if (json[0].P12 == 2) {
                                        //Especialização
                                        document.getElementById("<%=btnCertificadoMBAInternacional.ClientID%>").style.display = "block";
                                    }

                                    document.getElementById("msgSemResultadosCertificado").style.display = "none";
                                    document.getElementById("divBotaoCertificado").style.display = "block";

                                    document.getElementById('<%=txtPortariaNumeroOficial.ClientID%>').value = json[0].P8;
                                    document.getElementById('<%=txtPortariaDataOficial.ClientID%>').value = json[0].P9;
                                    document.getElementById('<%=txtDouDataOficial.ClientID%>').value = json[0].P10;
                                }
                                else {
                                    document.getElementById("msgSemResultadosCertificado").style.display = "block";
                                    document.getElementById("divBotaoCertificado").style.display = "none";
                                }

                                document.getElementById("divgrdHistoricoMatriculaNew").style.display = "block";
                                document.getElementById("msgSemResultadosgrdHistoricoMatriculaNew").style.display = "none";

                                var table_grdHistoricoMatriculaNew = $('#grdHistoricoMatriculaNew').DataTable();

                                $('#grdHistoricoMatriculaNew').on("click", "tr", function () {
                                    vRowIndex_grdHistoricoMatriculaNew = table_grdHistoricoMatriculaNew.row(this).index()
                                });

                                if (json[0].P11 == "0") {
                                    dt.columns([5,6] ).visible( false );
                                }
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheHistoricoMatricula?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Status", "orderable": false, "className": "hidden text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P1", "title": "Status", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P2", "title": "Situação", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P3", "title": "Data Início", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: "date-euro"
                        },
                        {
                            "data": "P4", "title": "Data Fim", "orderable": false, "className": "text-center centralizarTH", type: "date-euro", width: '10px'
                        },
                        {
                            "data": "P5", "title": "Editar", "orderable": false, "className": "text-center centralizarTHt", width: '10px'
                        },
                        {
                            "data": "P6", "title": "Apagar", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    //order: [[ 3, 'asc' ],[ 4, 'asc' ]],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

        //================================================================================

        function fPreencheHistoricoAluno(qIdTurma) {
            try {
                var dt = $('#grdHistoricoAlunoNew').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdHistoricoAlunoNew").style.display = "none";
                            document.getElementById("msgSemResultadosgrdHistoricoAlunoNew").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdHistoricoAlunoNew").style.display = "none";
                                document.getElementById("msgSemResultadosgrdHistoricoAlunoNew").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdHistoricoAlunoNew").style.display = "block";
                                document.getElementById("msgSemResultadosgrdHistoricoAlunoNew").style.display = "none";

                                var table_grdHistoricoAlunoNew = $('#grdHistoricoAlunoNew').DataTable();

                                $('#grdHistoricoAlunoNew').on("click", "tr", function () {
                                    vRowIndex_grdHistoricoAlunoNew = table_grdHistoricoAlunoNew.row(this).index()
                                });
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheHistoricoAluno?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Início", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: 'date-euro'
                        },
                        {
                            "data": "P1", "title": "Período", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P2", "title": "Disciplina", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P3", "title": "Nome", "orderable": false, "className": "text-left centralizarTH", width: '55%'
                        },
                        {
                            "data": "P4", "title": "Duração", "orderable": false, "className": "text-lef centralizarTHt", width: '10px'
                        },
                        {
                            "data": "P5", "title": "Frequência", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P6", "title": "Conceito", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P7", "title": "Resultado", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P8", "title": "Detalhe Disciplina", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P9", "title": "Presença Disciplina", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    order: [[1, 'asc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

        //================================================================================

        function fPreencheOrietacaoAluno(qIdTurma) {
            try {
                var dt = $('#grdCoorientadorAlunoNew').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divBotoesOrientacao").style.display = "none";
                            document.getElementById("divOrientadorTem").style.display = "none";
                            document.getElementById("divOrientadorNaoTem").style.display = "block";
                            document.getElementById("divCoorientador").style.display = "none";
                            document.getElementById("txtTituloOrientacaoNew").value = "";
                            document.getElementById("txtIdOrientador").value = "";

                            document.getElementById("divgrdCoorientadorAlunoNew").style.display = "none";
                            document.getElementById("msgSemResultadosgrdCoorientadorAlunoNew").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdCoorientadorAlunoNew").style.display = "none";
                                document.getElementById("msgSemResultadosgrdCoorientadorAlunoNew").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("txtCpfOrientador").value = json[0].P3;
                                document.getElementById("txtNomeOrientador").value = json[0].P4;
                                document.getElementById("txtTituloOrientacaoNew").value = json[0].P6;
                                document.getElementById("txtIdOrientador").value = json[0].P8;

                                document.getElementById("divOrientadorTem").style.display = "block";
                                document.getElementById("divOrientadorNaoTem").style.display = "none";
                                document.getElementById("divBotoesOrientacao").style.display = "block";
                                document.getElementById("msgSemResultadosgrdCoorientadorAlunoNew").style.display = "none";
                                document.getElementById("divCoorientador").style.display = "block";

                                if (document.getElementById('<%=hEscrita.ClientID%>').value == "1") {
                                    document.getElementById('btnSalvarBancaQualificao').style.display="block";
                                }

                                //alert(oSettings.fnRecordsTotal());

                                if (json[0].P7 == "sim") {
                                    document.getElementById("divgrdCoorientadorAlunoNew").style.display = "block";
                                    document.getElementById("msgSemResultadosgrdCoorientadorAlunoNew").style.display = "none";

                                    var table_grdCoorientadorAlunoNew = $('#grdCoorientadorAlunoNew').DataTable();

                                    $('#grdCoorientadorAlunoNew').on("click", "tr", function () {
                                        vRowIndex_grdCoorientadorAlunoNew = table_grdCoorientadorAlunoNew.row(this).index()
                                    });
                                }
                                else {
                                    document.getElementById("divgrdCoorientadorAlunoNew").style.display = "none";
                                    document.getElementById("msgSemResultadosgrdCoorientadorAlunoNew").style.display = "block";
                                }

                                if (json[0].P9 == "0") {
                                    dt.columns([2] ).visible( false );
                                }
                                
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheOrietacaoAluno?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "CFP", "orderable": false, "className": "text-center centralizarTH"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left centralizarTH"
                        },
                        {
                            "data": "P2", "title": "Excluir", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    order: [[2, 'asc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

        //==============================

        function fPreencheBancaQualificacaoAluno(qIdTurma) {
            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheBancaQualificacaoAlunoDetalhes?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de carregamento dos dados da Banca de Qualificação ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de carregamento dos dados da Banca de Qualificação. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //alert(json.length)
                        document.getElementById('txtNumeroBancaQualificacao').value = json[0].P0;
                        document.getElementById('txtDataBancaQualificacao').value = json[0].P1;
                        document.getElementById('txtHoraBancaQualificacao').value = json[0].P2;
                        $("#ddlBancaQualificacaoRemota").val(json[0].P14).trigger("change");
                        $("#ddlResultadoBancaQualificacao").val(json[0].P3).trigger("change");
                        //document.getElementById('ddlResultadoBancaQualificacao').value = json[0].P3;
                        document.getElementById('txtTituloBancaQualificacao').value = json[0].P4;
                        document.getElementById('txtObdervacaoBancaQualificacao').value = json[0].P5;
                        document.getElementById('txtIdOrientadorBancaQualificacao').value = json[0].P6;
                        document.getElementById('txtCpfOrientadorBancaQualificacao').value = json[0].P7;
                        document.getElementById('txtNomeOrientadorBancaQualificacao').value = json[0].P8;
                        document.getElementById('divCoorientadorQualificacaoTemporario').style.display="none";

                        if (json[0].P9 == "novo") {
                            document.getElementById('divEdicaoBancaQualificacao').style.display="none";
                            document.getElementById('divBotaoAlterarOrientadorBancaQualificacao').style.display="none";
                            document.getElementById('divCoorientadorBancaQualificacao').style.display="none";
                            document.getElementById('btnImprimirDivulgacaoQualificao').style.display="none";
                            document.getElementById('btnImprimirAtaQualificao').style.display="none";
                            document.getElementById('divMembrosBancaQualificacao').style.display="none";
                            if (document.getElementById('<%=hEscrita.ClientID%>').value == "1") {
                                    document.getElementById('btnSalvarBancaQualificao').style.display="block";
                                }
                            if (json[0].P10 != "") {
                                document.getElementById('divCoorientadorQualificacaoTemporario').style.display="block";
                                document.getElementById('lblCoorientadorQualificacaoTemporario').innerHTML = json[0].P10;
                            }
                            else {
                                document.getElementById('divCoorientadorQualificacaoTemporario').style.display="none";
                            }
                        }
                        else if (json[0].P9 == "sem_Orientação") {
                            document.getElementById('divEdicaoBancaQualificacao').style.display="none";
                            document.getElementById('divBotaoAlterarOrientadorBancaQualificacao').style.display="none";
                            document.getElementById('divCoorientadorBancaQualificacao').style.display="none";
                            document.getElementById('btnImprimirDivulgacaoQualificao').style.display="none";
                            document.getElementById('btnImprimirAtaQualificao').style.display="none";
                            document.getElementById('divMembrosBancaQualificacao').style.display="none";
                            document.getElementById('btnSalvarBancaQualificao').style.display="none";
                        }
                        else {
                            document.getElementById('txtDataCadastroBancaQualificacao').value = json[0].P11;
                            document.getElementById('txtDataAlteracaoBancaQualificacao').value = json[0].P12;
                            document.getElementById('txtResponsavelBancaQualificacao').value = json[0].P13;
                            document.getElementById('divEdicaoBancaQualificacao').style.display="block";
                            document.getElementById('divBotaoAlterarOrientadorBancaQualificacao').style.display="block";
                            document.getElementById('divCoorientadorBancaQualificacao').style.display="block";
                            document.getElementById('btnImprimirDivulgacaoQualificao').style.display="block";
                            document.getElementById('btnImprimirAtaQualificao').style.display="block";
                            document.getElementById('divMembrosBancaQualificacao').style.display="block";
                            if (document.getElementById('<%=hEscrita.ClientID%>').value == "1") {
                                    document.getElementById('btnSalvarBancaQualificao').style.display="block";
                                }
                            fPreencheBancaCoorientador(qIdTurma, "Qualificação");
                            //alert("Passou PreencheBancaCoorientador");
                            fPreencheBancaMembro(qIdTurma, "Qualificação");
                        }

                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de carregamento dos dados da Banca de Qualificação. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de carregamento dos dados da Banca de Qualificação. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });

            } catch (e) {
                fFechaProcessando()
            }
            
        }


        //==============================

        function fPreencheBancaDefesaAluno(qIdTurma) {
            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheBancaDefesaAluno?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de carregamento dos dados da Banca de Defesa ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de carregamento dos dados da Banca de Defesa. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //alert(json.length)
                        document.getElementById('txtNumeroBancaDefesa').value = json[0].P0;
                        document.getElementById('txtDataBancaDefesa').value = json[0].P1;
                        document.getElementById('txtHoraBancaDefesa').value = json[0].P2;
                        $("#ddlBancaDefesaRemota").val(json[0].P19).trigger("change");
                        $("#ddlResultadoBancaDefesa").val(json[0].P3).trigger("change");
                        document.getElementById('txtTituloBancaDefesa').value = json[0].P4;
                        document.getElementById('txtObdervacaoBancaDefesa').value = json[0].P5;
                        document.getElementById('txtIdOrientadorBancaDefesa').value = json[0].P6;
                        document.getElementById('txtCpfOrientadorBancaDefesa').value = json[0].P7;
                        document.getElementById('txtNomeOrientadorBancaDefesa').value = json[0].P8;
                        document.getElementById('txtDataEntregaBancaDefesa').value = json[0].P12;
                        if (json[0].P12 == "") {
                            document.getElementById('msgSemResultadosDissertacaoBancaDefesa').style.display="block";
                            document.getElementById('divDissertacaoBancaDefesa').style.display="none";
                        }
                        else {
                            document.getElementById('msgSemResultadosDissertacaoBancaDefesa').style.display="none";
                            document.getElementById('divDissertacaoBancaDefesa').style.display="block";
                        }
                        document.getElementById('divCoorientadorDefesaTemporario').style.display="none";
                        document.getElementById('divMembrosDefesaTemporario').style.display="none";
                        document.getElementById('txtNumeroPortariaMecBancaDefesa').value = json[0].P13;
                        document.getElementById('txtDataPortariaMecBancaDefesa').value = json[0].P14;
                        document.getElementById('txtDataDOUBancaDefesa').value = json[0].P15;
                        document.getElementById('txtDataCadastroBancaDefesa').value = json[0].P16;
                        document.getElementById('txtDataAlteracaoBancaDefesa').value = json[0].P17;
                        document.getElementById('txtResponsavelBancaDefesa').value = json[0].P18;

                        if (json[0].P9 == "novo") {
                            document.getElementById('divEdicaoBancaDefesa').style.display="none";
                            document.getElementById('divBotaoAlterarOrientadorBancaDefesa').style.display="none";
                            document.getElementById('divCoorientadorBancaDefesa').style.display="none";
                            document.getElementById('btnImprimirDivulgacaoDefesa').style.display="none";
                            document.getElementById('btnImprimirAtaDefesa').style.display="none";
                            document.getElementById('divMembrosBancaDefesa').style.display="none";
                            if (document.getElementById('<%=hEscrita.ClientID%>').value == "1") {
                                document.getElementById('btnSalvarBancaDefesa').style.display="block";
                                }
                            if (json[0].P10 != "") {
                                document.getElementById('divCoorientadorDefesaTemporario').style.display="block";
                                document.getElementById('lblCoorientadorDefesaTemporario').innerHTML = json[0].P10;
                            }
                            else {
                                document.getElementById('divCoorientadorDefesaTemporario').style.display="none";
                            }
                            if (json[0].P11 != "") {
                                document.getElementById('divMembrosDefesaTemporario').style.display="block";
                                document.getElementById('lblMembrosDefesaTemporario').innerHTML = json[0].P11;
                            }
                            else {
                                document.getElementById('divMembrosDefesaTemporario').style.display="none";
                            }
                        }
                        else if (json[0].P9 == "sem_qualificação") {
                            document.getElementById('divEdicaoBancaDefesa').style.display="none";
                            document.getElementById('divBotaoAlterarOrientadorBancaDefesa').style.display="none";
                            document.getElementById('divCoorientadorBancaDefesa').style.display="none";
                            document.getElementById('btnImprimirDivulgacaoDefesa').style.display="none";
                            document.getElementById('btnImprimirAtaDefesa').style.display="none";
                            document.getElementById('divMembrosBancaDefesa').style.display="none";
                            document.getElementById('btnSalvarBancaDefesa').style.display="none";
                        }
                        else {
                            document.getElementById('divEdicaoBancaDefesa').style.display="block";
                            document.getElementById('divBotaoAlterarOrientadorBancaDefesa').style.display="block";
                            document.getElementById('divCoorientadorBancaDefesa').style.display="block";
                            document.getElementById('btnImprimirDivulgacaoDefesa').style.display="block";
                            document.getElementById('btnImprimirAtaDefesa').style.display="block";
                            document.getElementById('divMembrosBancaDefesa').style.display="block";
                            if (document.getElementById('<%=hEscrita.ClientID%>').value == "1") {
                                document.getElementById('btnSalvarBancaDefesa').style.display="block";
                                }
                            fPreencheBancaCoorientador(qIdTurma, "Defesa");
                            fPreencheBancaMembro(qIdTurma, "Defesa");
                            fPreencheDissertacaoBancaDefesaAluno(qIdTurma);
                            fPreencheHistoricoDissertacao(qIdTurma);
                        }

                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de carregamento dos dados da Banca de Defesa. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de carregamento dos dados da Banca de Defesa. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });

            } catch (e) {
                fFechaProcessando()
            }
            
        }

    //==============================

        function fPreencheDissertacaoBancaDefesaAluno(qIdTurma) {
            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheDissertacaoBancaDefesaAluno?qIdTurma=" + qIdTurma  + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de carregamento dos dados da Dissertação do Aluno';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de carregamento dos dados da Dissertação do Aluno. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //alert(json.length)
                        //Não tem registro
                        if (json[0].P0 == null) {
                            document.getElementById('<%=tabPublicadoDissertacao.ClientID%>').style.display = "none";
                            document.getElementById('divEdicaoDissertacaoBancaDefesa').style.display = "none";
                            document.getElementById('txtDataCadastroDissertacaoBancaDefesa').value = "";
                            document.getElementById('txtDataAlteracaoDissertacaoBancaDefesa').value = "";
                            document.getElementById('txtResponsavelDissertacaoBancaDefesa').value = "";
                            document.getElementById('divDissertacaoPublicacaoPreview').style.display = "none";
                        }
                        //tem registro
                        else {
                            document.getElementById('<%=tabPublicadoDissertacao.ClientID%>').style.display = "block";
                            document.getElementById('divEdicaoDissertacaoBancaDefesa').style.display = "block";
                            document.getElementById('txtDataCadastroDissertacaoBancaDefesa').value = json[0].P1;
                            document.getElementById('txtDataAlteracaoDissertacaoBancaDefesa').value = json[0].P2;
                            document.getElementById('txtResponsavelDissertacaoBancaDefesa').value = json[0].P3;
                            document.getElementById('divDissertacaoPublicacaoPreview').style.display = "block";
                        }
                        if (json[0].P21 == "") {
                            document.getElementById('divDissertacaoPublicacaoPublicado').style.display = "none";
                        }
                        else {
                            document.getElementById('divDissertacaoPublicacaoPublicado').style.display = "block";    
                            document.getElementById('lblDissertacao_tipocurso_publicado').innerHTML = json[0].P16;
                            document.getElementById('lblDissertacao_titulo_publicado').innerHTML = json[0].P17;
                            document.getElementById('lblDissertacao_aluno_publicado').innerHTML = json[0].P18;
                            document.getElementById('lblDissertacao_orientador_publicado').innerHTML = json[0].P19;
                            document.getElementById('lblDissertacao_ano_publicado').innerHTML = json[0].P20;
                            document.getElementById('lblDissertacao_visualizacoes_publicado').innerHTML = json[0].P4;
                            document.getElementById('lblDissertacao_downloads_publicado').innerHTML = json[0].P5;
                            document.getElementById('lblDissertacao_resumo_publicado').innerHTML = replaceAll("\n", "<br>", json[0].P7);
                            document.getElementById('aArquivo_publicado').href ='teses/' + json[0].P8;
                        }
                        document.getElementById('txtVisitaDissertacaoBancaDefesa').value = json[0].P4;
                        document.getElementById('txtDownloadDissertacaoDefesa').value = json[0].P5;
                        //document.getElementById('txtPalavraChaveDissertacaoDefesa').value = json[0].P6;
                        //document.getElementById('txtResumoDissertacaoBancaDefesa').value = json[0].P7;
                        //document.getElementById('txtArquivoDissertacaoBancaDefesa').value = json[0].P8;
                        //document.getElementById('txtCodIPT').value = json[0].P9;
                        //======
                        document.getElementById('txtPalavraChaveDissertacaoDefesa_Preview').value = json[0].P10;
                        document.getElementById('txtCodIPT_Preview').value = json[0].P11;
                        document.getElementById('txtResumoDissertacaoBancaDefesa_Preview').value = json[0].P12;
                        document.getElementById('txtArquivoDissertacaoBancaDefesa').value = json[0].P13;

                        document.getElementById('txtPalavraChaveDissertacaoDefesa').value = json[0].P6;
                        document.getElementById('txtCodIPT').value = json[0].P9;
                        document.getElementById('txtResumoDissertacaoBancaDefesa').value = json[0].P7;
                        document.getElementById('txtArquivoPDFDissertacaoPublicado').value = json[0].P8;
                        document.getElementById('txtDataAprovacaoDissertacao').value = json[0].P21;
                        document.getElementById('txtusuarioAprovacaoDissertacao').value = json[0].P22;

                        if (json[0].P14 == "n_Gerente") {
                            document.getElementById('<%=btnLocalizarDissertacaoBancaDefesa.ClientID%>').style.display = 'block';
                            document.getElementById('<%=btnSalvarDissertacaoBancaDefesa.ClientID%>').style.display = 'block';
                            //3 = Alterado (aguardando envio para aprovação)
                            if (json[0].P15 == "3") {
                                document.getElementById('divEnviar_Aprovar_Reprovar').style.display = 'block';
                                document.getElementById('btnEnviarAprovacaoOffLine').style.display = 'block';
                                document.getElementById('btnAprovarOffLine').style.display = 'none';
                                document.getElementById('btnReprovarOffLine').style.display = 'none';
                                document.getElementById('lblDissertacaoInformacao').style.display = 'block';
                                document.getElementById('lblDissertacaoInformacao').innerHTML = "Alterado (enviar para aprovação)";
                            }
                            //0 = Enviado para aprovação (aguardando)
                            else if (json[0].P15 == "0") {
                                document.getElementById('divEnviar_Aprovar_Reprovar').style.display = 'block';
                                document.getElementById('lblDissertacaoInformacao').style.display = 'block';
                                document.getElementById('btnEnviarAprovacaoOffLine').style.display = 'none';
                                document.getElementById('lblDissertacaoInformacao').innerHTML = "Aguardando Aprovação";
                            }
                        }
                        //Gerente
                        else {
                            //0 = Enviado para aprovação (aguardando)
                            if (json[0].P15 == "0") {
                                document.getElementById('btnEnviarAprovacaoOffLine').style.display = 'none';
                                document.getElementById('divEnviar_Aprovar_Reprovar').style.display = 'block';
                                document.getElementById('btnAprovarOffLine').style.display = 'block';
                                document.getElementById('btnReprovarOffLine').style.display = 'block';
                            }
                            else {
                                document.getElementById('divEnviar_Aprovar_Reprovar').style.display = 'none';
                            }
                            document.getElementById('<%=btnLocalizarDissertacaoBancaDefesa.ClientID%>').style.display = 'none';
                            document.getElementById('<%=btnSalvarDissertacaoBancaDefesa.ClientID%>').style.display = 'none';
                        }
                        document.getElementById('lblDissertacao_tipocurso_preview').innerHTML = json[0].P16;
                        document.getElementById('lblDissertacao_titulo_preview').innerHTML = json[0].P17;
                        document.getElementById('lblDissertacao_aluno_preview').innerHTML = json[0].P18;
                        document.getElementById('lblDissertacao_orientador_preview').innerHTML = json[0].P19;
                        document.getElementById('lblDissertacao_ano_preview').innerHTML = json[0].P20;
                        document.getElementById('lblDissertacao_visualizacoes_preview').innerHTML = json[0].P4;
                        document.getElementById('lblDissertacao_downloads_preview').innerHTML = json[0].P5;
                        document.getElementById('lblDissertacao_resumo_preview').innerHTML = replaceAll("\n", "<br>", json[0].P12);
                        document.getElementById('aArquivo_preview').href ='teses/' + json[0].P13;
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de carregamento dos dados da Dissertação do Aluno. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de carregamento dos dados da Dissertação do Aluno. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });

            } catch (e) {
                fFechaProcessando()
            }
            
        }

        //==============================

        function fLocalizarDissertacaoBancaDefesa() {
            document.getElementById("<%=fileDissertacao.ClientID%>").click();
        }

        //==============================
        function fModalEnviarAprovacao(qEvento) {
            document.getElementById('txtObsAprovacaoDissertacao').value = "";
            document.getElementById('txtObsAprovacaoDissertacao').style.display = "none";
            $("#iconCabecEnviar").removeClass("fa-mail-forward");
            $("#iconCabecEnviar").removeClass("fa-thumbs-o-up");
            $("#iconCabecEnviar").removeClass("fa-thumbs-o-down");
            //document.getElementById('lblCorpoAprovacao').style.display = "block";
            document.getElementById('divLabelObs').style.display = "none";
            document.getElementById('btnEnviarAprovacaoDissertacao').style.display = "none";
            document.getElementById('btnAprovarDissertacao').style.display = "none";
            document.getElementById('btnReprovarDissertacao').style.display = "none";
            $("#divCabecAprovacao").removeClass("alert-warning");
            $("#divCabecAprovacao").removeClass("alert-success");
            $("#divCabecAprovacao").removeClass("alert-danger");

            if (qEvento == 'EnviarAprovacao') {
                $('#divCabecAprovacao').addClass("alert-warning");
                $("#iconCabecEnviar").addClass("fa-mail-forward");
                document.getElementById("lblCabecAprovacao").innerHTML = "Enviar para aprovação";
                document.getElementById('lblCorpoAprovacao').innerHTML = 'Preencha a observação para enviar para aprovação. <br> (assim ajuda o "aprovador" a identificar o que foi incluído/alterado)';
                document.getElementById('divLabelObs').style.display = "block";
                document.getElementById('txtObsAprovacaoDissertacao').style.display = "block";
                document.getElementById('btnEnviarAprovacaoDissertacao').style.display = "block";
                $('#divAprovacao').modal();
            }
            else if (qEvento == 'Aprovar') {
                $('#divCabecAprovacao').addClass("alert-success");
                $("#iconCabecEnviar").addClass("fa-thumbs-o-up");
                document.getElementById("lblCabecAprovacao").innerHTML = "Aprovar";
                document.getElementById('lblCorpoAprovacao').innerHTML = 'Deseja Aprovar a publicação do Documento?';
                document.getElementById('btnAprovarDissertacao').style.display = "block";
                $('#divAprovacao').modal();
            }
            else if (qEvento == 'Reprovar') {
                $('#divCabecAprovacao').addClass("alert-danger");
                $("#iconCabecEnviar").addClass("fa-thumbs-o-down");
                document.getElementById("lblCabecAprovacao").innerHTML = "Reprovar";
                document.getElementById('divLabelObs').style.display = "block";
                document.getElementById('lblCorpoAprovacao').innerHTML = 'Preencha a observação para reprovação. <br> (assim ajuda o "usuário" a identificar o porquê foi Reprovado)';
                document.getElementById('txtObsAprovacaoDissertacao').style.display = "block";
                document.getElementById('btnReprovarDissertacao').style.display = "block";
                $('#divAprovacao').modal();
            }
        }

        //==============================

        function fEnviarAprovacaoDissertacao(qIdTurma) {
            qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            qObs = document.getElementById('txtObsAprovacaoDissertacao').value;
            if (document.getElementById('txtObsAprovacaoDissertacao').value == "") {
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass("alert-danger");
                document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = "ATENÇÃO"
                document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = "Deve-se preencher campo 'Observação' para descrever a alteração realizada."
                $('#divMensagemModal').modal();
                return;
            }

            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fEnviarAprovacaoDissertacao?qIdTurma=" + qIdTurma + "&qObs=" + qObs + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de enviar dados da Dissertação para Aprovação';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de enviar dados da Dissertação para Aprovação. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        var qDissertacao;
                        if (document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value == "Especialização") {
                            qDissertacao = "da Monografia";
                        }
                        else {
                            qDissertacao = "da Dissertação";
                        }
                        fPreencheDissertacaoBancaDefesaAluno(qIdTurma);
                        fPreencheHistoricoDissertacao(qIdTurma);
                        $('#divAprovacao').modal('hide');
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Dados ' + qDissertacao + ' para Aprovação';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Dados ' + qDissertacao + ' enviados para aprovação com sucesso';
                        $("#divCabecalho").removeClass("alert-danger");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-success');
                        $('#divMensagemModal').modal();
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de enviar dados da Dissertação para Aprovação. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de enviar dados da Dissertação para Aprovação. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });

            } catch (e) {
                fFechaProcessando()
            }
            
        }

        //==============================

        function fAprovarDissertacao(qIdTurma) {
            qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;

            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fAprovarDissertacao?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de Aprovação da Dissertação';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de Aprovação da Dissertação. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fAprovarHPDissertacao(document.getElementById('<%=txtMatriculaAluno.ClientID%>').value, qIdTurma);
                        //location.reload();
                        <%--fPreencheDissertacaoBancaDefesaAluno(qIdTurma);
                        fPreencheHistoricoDissertacao(qIdTurma);
                        $('#divAprovacao').modal('hide');
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Dissertação Aprovada';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Dissertação aprovada com sucesso';
                        $("#divCabecalho").removeClass("alert-danger");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-success');
                        $('#divMensagemModal').modal();--%>
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de Aprovação da Dissertação. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de Aprovação da Dissertação. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });

            } catch (e) {
                fFechaProcessando()
            }
            
        }
        //================================================================================


        function fReprovarDissertacao(qIdTurma) {
            qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            qObs = document.getElementById('txtObsAprovacaoDissertacao').value;
            if (document.getElementById('txtObsAprovacaoDissertacao').value == "") {
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass("alert-danger");
                document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = "ATENÇÃO"
                document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = "Deve-se preencher campo 'Observação' para descrever a alteração realizada."
                $('#divMensagemModal').modal();
                return;
            }

            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fReprovarDissertacao?qIdTurma=" + qIdTurma + "&qObs=" + qObs + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de Reprovação da Dissertação';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de Reprovação da Dissertação. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fAprovarHPDissertacao(document.getElementById('<%=txtMatriculaAluno.ClientID%>').value, qIdTurma);
                        <%--fPreencheDissertacaoBancaDefesaAluno(qIdTurma);
                        fPreencheHistoricoDissertacao(qIdTurma);
                        $('#divAprovacao').modal('hide');
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Dados da Reprovação da Dissertação';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Dados da Reprovação da Dissertação enviados com sucesso';
                        $("#divCabecalho").removeClass("alert-danger");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-success');
                        $('#divMensagemModal').modal();--%>
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de Reprovação da Dissertação. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de Reprovação da Dissertação. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });

            } catch (e) {
                fFechaProcessando()
            }
            
        }

        //================================================================================

        function fPreencheHistoricoDissertacao(qIdTurma) {
            try {
                var dt = $('#grdHistoricoDissertacao').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdHistoricoDissertacao").style.display = "none";
                            document.getElementById("lblMsgSemResultadosgrdHistoricoDissertacao").style.display = "block";
                            document.getElementById("divHistoricoObservacao").style.display = "none";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdHistoricoDissertacao").style.display = "none";
                                document.getElementById("divHistoricoObservacao").style.display = "none";
                                document.getElementById("lblMsgSemResultadosgrdHistoricoDissertacao").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdHistoricoDissertacao").style.display = "block";
                                document.getElementById("lblMsgSemResultadosgrdHistoricoDissertacao").style.display = "none";
                                document.getElementById("divHistoricoObservacao").style.display = "block";
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheHistoricoDissertacao?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Data/Hora Obs.", "orderable": true, "className": "text-center centralizarTH", width: "10px", type: 'date-euro'
                        },
                        {
                            "data": "P1", "title": "Observação", "orderable": false, "className": "text-center centralizarTH"
                        },
                        {
                            "data": "P2", "title": "Usuário", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    order: [[0, 'desc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

        //================================================================================

        function fLocalizarContrato() {
            $("#<%=fileContrato.ClientID%>").val(null);
            document.getElementById("<%=fileContrato.ClientID%>").click();
        }

        //==============================

        function fCancelarContrato() {
            document.getElementById('<% =btnLocalizarContrato.ClientID%>').style.display = "block";
            document.getElementById('<% =btnCancelarContrato.ClientID%>').style.display = "none";
            document.getElementById('divBotaoSalvarContrato').style.display = 'none'; //
        }

        //==============================

        function fLocalizarArtigo() {
            $("#<%=fileArtigo.ClientID%>").val(null);
            document.getElementById("<%=fileArtigo.ClientID%>").click();
        }

        //==============================

        function fCancelarArtigo() {
            document.getElementById('<% =btnLocalizarArtigo.ClientID%>').style.display = "block";
            document.getElementById('<% =btnCancelarArtigo.ClientID%>').style.display = "none";
            //document.getElementById('divBotaoSalvarArtigo').style.display = 'none'; //
        }
        //==============================

        function fSelecionouArquivo(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "PDF" || vFileExt.toUpperCase() == "JPG" || vFileExt.toUpperCase() == "JPEG" || vFileExt.toUpperCase() == "PNG") {

                if (idFile.files && idFile.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        document.getElementById('txtArquivoDocumentoObrigatorio').value = idFile.files[0].name;
                    }
                    reader.readAsDataURL(idFile.files[0]);
                }
            } else {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Permitido apenas documento em PDF ou JPG ou JPEG ou PNG.';
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
            }

        }

        //==============================

        function fSelecionouContrato(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "PDF" || vFileExt.toUpperCase() == "JPG" || vFileExt.toUpperCase() == "JPEG" || vFileExt.toUpperCase() == "PNG") {

                if (idFile.files && idFile.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        document.getElementById('<% =txtNomeContrato.ClientID%>').value = idFile.files[0].name;
                    }
                    reader.readAsDataURL(idFile.files[0]);
                    document.getElementById('<% =btnLocalizarContrato.ClientID%>').style.display = "none";
                    document.getElementById('<% =btnCancelarContrato.ClientID%>').style.display = "block";
                    document.getElementById('divBotaoSalvarContrato').style.display = 'block'; //
                    document.getElementById('divBotaoDowloadContrato').style.display = 'none'; //
                }

            } else {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Permitido apenas documento em PDF ou JPG ou JPEG ou PNG.';
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
            }

        }

        //==============================

        function fSelecionouArtigo(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "PDF" || vFileExt.toUpperCase() == "JPG" || vFileExt.toUpperCase() == "JPEG" || vFileExt.toUpperCase() == "PNG") {

                if (idFile.files && idFile.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        document.getElementById('<% =txtArquivoArtigo.ClientID%>').value = idFile.files[0].name;
                    }
                    reader.readAsDataURL(idFile.files[0]);
                    document.getElementById('<% =btnLocalizarArtigo.ClientID%>').style.display = "none";
                    document.getElementById('<% =btnCancelarArtigo.ClientID%>').style.display = "block";
                    document.getElementById('divBotaoSalvarArtigo').style.display = 'block'; //
                    document.getElementById('divBotaoDowloadArtigo').style.display = 'none'; //
                }

            } else {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Permitido apenas documento em PDF ou JPG ou JPEG ou PNG.';
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
            }

        }

        //==============================
        function fSelecionouDissertacao(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "PDF") {

                if (idFile.files && idFile.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        document.getElementById('txtArquivoDissertacaoBancaDefesa').value = idFile.files[0].name;
                    }
                    reader.readAsDataURL(idFile.files[0]);
                }

                $("#<%=fileDissertacao.ClientID%>").change(function () {
                    fSelecionouImagem(this);
                });

            } else {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Permitido apenas documento em PDF.';
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
            }

        }

        //==============================

        function fSalvarDissertacaoBancaDefesaAluno(qIdTurma) {
            //return;
            var qIdTurma = document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value;
            var qDissertacao = "da Dissertação";
            if (document.getElementById('<%=txtTipoCursoAlunoNew.ClientID%>').value == "Especialização") {
                qDissertacao = "da Monografia";
            }

            var sAux =  "";
            //if (document.getElementById('txtVisitaDissertacaoBancaDefesa').value.trim() == '') {
            //    sAux = "Deve-se digitar o número de Visitas.<br><br>"
            //}
            //if (document.getElementById('txtDownloadDissertacaoDefesa').value.trim() == '') {
            //    sAux = sAux + "Deve-se digitar o número de <em>Downloads</em>.<br><br>"
            //}
            if (document.getElementById('txtPalavraChaveDissertacaoDefesa_Preview').value.trim() == '') {
                sAux = sAux + "Deve-se digitar as Palavras-chaves.<br><br>"
            }
            if (document.getElementById('txtResumoDissertacaoBancaDefesa_Preview').value.trim() == '') {
                sAux = sAux + "Deve-se digitar o Resumo " + qDissertacao + ".<br><br>"
            }
            if (document.getElementById('txtArquivoDissertacaoBancaDefesa').value.trim() == '') {
                sAux = sAux + "Deve-se selecionar o arquivo em PDF " + qDissertacao + " para <e>Upload</em>.<br><br>"
            }
            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'ATENÇÂO ';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                return;
            }

            try {
                var formData = new FormData();
                var files = $("#<%=fileDissertacao.ClientID%>")[0].files;
                $.each(files, function (idx, file) {
                    formData.append("qArquivo",  file);
                });
                //formData.append("qVititas",  document.getElementById('txtVisitaDissertacaoBancaDefesa').value.trim());
                //formData.append("qDownloads",  document.getElementById('txtDownloadDissertacaoDefesa').value.trim());
                formData.append("qPalavras",  document.getElementById('txtPalavraChaveDissertacaoDefesa_Preview').value.trim());
                formData.append("qResumo",  document.getElementById('txtResumoDissertacaoBancaDefesa_Preview').value.trim());
                formData.append("qNomeArquivo",  document.getElementById('txtArquivoDissertacaoBancaDefesa').value.trim());
                formData.append("qTurma",  document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value.trim());
                formData.append("qBanca",  document.getElementById('txtNumeroBancaDefesa').value.trim());
                formData.append("qCodIPT", document.getElementById('txtCodIPT_Preview').value.trim());
                formData.append("qDissertacao", qDissertacao);
                formData.append("qTab", document.getElementById('hQTab').value);

                $.ajax({
                url: "wsSapiens.asmx/fSalvarDissertacaoBancaDefesaAluno",
                data: formData,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                success: function (json) 
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro salvamento Dissertação do Aluno';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de salvamento dos dados da Dissertação do Aluno. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheDissertacaoBancaDefesaAluno(qIdTurma);
                        fPreencheHistoricoDissertacao(qIdTurma);
                        document.getElementById('divEdicaoDissertacaoBancaDefesa').style.display = "block";
                        document.getElementById('txtDataCadastroDissertacaoBancaDefesa').value = json[0].P1;
                        document.getElementById('txtDataAlteracaoDissertacaoBancaDefesa').value = json[0].P2;
                        document.getElementById('txtResponsavelDissertacaoBancaDefesa').value = json[0].P3;
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Salvamento da Dissertação</strong><br /><br />',
                            message: 'Salvamento da Dissertação do Aluno realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de carregamento dos dados da Dissertação do Aluno. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de carregamento dos dados da Dissertação do Aluno. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });

            } catch (e) {
                fFechaProcessando()
            }
            
        }

        //==============================
        
        function fPreencheBancaCoorientador(qIdTurma, qBanca) {
            try {
                //alert('entrou fPreencheBancaCoorientador');
                var qGrade;
                if (qBanca != "Defesa") {
                    qGrade = "grdCoorientadorBancaQualificacao";
                }
                else {
                    qGrade = "grdCoorientadorBancaDefesa";
                }
                var dt = $('#' + qGrade ).DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("div" + qGrade).style.display = "none";
                            document.getElementById("msgSemResultados" + qGrade).style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("div" + qGrade).style.display = "none";
                                document.getElementById("msgSemResultados" + qGrade).style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("div" + qGrade).style.display = "block";
                                document.getElementById("msgSemResultados" + qGrade).style.display = "none";

                                if (document.getElementById('<%=hEscrita.ClientID%>').value == "0") {
                                    fEscondeColunas(qGrade,6);
                                }

                                //var table_+ qGrade = $('#grd' + qGrade).DataTable();

                                //$('#grdCoorientadorBancaQualificacao').on("click", "tr", function () {
                                //    vRowIndex_grdCoorientadorBancaQualificacao = table_grdCoorientadorBancaQualificacao.row(this).index()
                                //});
                                //alert('Saiu fPreencheBancaCoorientador');
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheBancaCoorientador?qIdTurma=" + qIdTurma + "&qBanca=" + qBanca + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "CPF", "orderable": false, "className": "text-center centralizarTH"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left centralizarTH"
                        },
                        {
                            "data": "P2", "title": "Imprimir", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P3", "title": "Atestado", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P4", "title": "Recibo", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P5", "title": "Convite", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P6", "title": "Excluir", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    order: [[1, 'asc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

        //==============================

        function fPreencheBancaMembro(qIdTurma, qBanca) {
            try {
                //alert('entrou fPreencheBancaMembro');
                var qGrade;
                if (qBanca != "Defesa") {
                    qGrade = "grdMembrosBancaQualificacao";
                }
                else {
                    qGrade = "grdMembrosBancaDefesa";
                }
                var dt = $('#' + qGrade).DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        //alert('saiu fPreencheBancaMembro');

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("div" + qGrade).style.display = "none";
                            document.getElementById("msgSemResultados" + qGrade).style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("div" + qGrade).style.display = "none";
                                document.getElementById("msgSemResultados" + qGrade).style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("div" + qGrade).style.display = "block";
                                document.getElementById("msgSemResultados" + qGrade).style.display = "none";

                                if (document.getElementById('<%=hEscrita.ClientID%>').value == "0") {
                                    fEscondeColunas(qGrade,6);
                                }

                                //var table_grdMembrosBancaQualificacao = $('#grdMembrosBancaQualificacao').DataTable();

                                //$('#grdMembrosBancaQualificacao').on("click", "tr", function () {
                                //    vRowIndex_grdMembrosBancaQualificacao = table_grdMembrosBancaQualificacao.row(this).index()
                                //});

                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheBancaMembro?qIdTurma=" + qIdTurma + "&qBanca=" + qBanca  + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "CPF", "orderable": false, "className": "text-center centralizarTH"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left centralizarTH"
                        },
                        {
                            "data": "P2", "title": "Tipo", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P3", "title": "Atestado", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P4", "title": "Recibo", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P5", "title": "Convite", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P6", "title": "Excluir", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        }
                    ],
                    order: [[2, 'asc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                
            }
            
        }

        //==============================

        //function FechaModalDadosFoto() {
        //    document.getElementById('divModalAlteraDadosFoto').style.display = 'none';
        //}

        function AbreMensagem(qClass) {
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').removeClass('alert-danger');
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        function funcClicaImprimirHistorico() {

            //alert("Hello world");
            fPreparaRelatorio('O relatório do Histórico Oficial está sendo preparado.');
            document.getElementById('<%=btnImprimirHitorico.ClientID%>').click();



            //$.ajax({
            //    type: 'POST',
            //    url: 'cadAlunoGestao.aspx',  //URL solicitada
            //    data: { funcImprimeHistorico: 'sim' },
            //    success: function (data) { //O HTML é retornado em 'data'
            //        alert(data); //Se sucesso um alert com o conteúdo retornado pela URL solicitada será exibido.
            //    },
            //    error: function (xmlHttpRequest, status, err) {
            //        document.getElementById('lblErroCabecalho').innerHTML = 'Erro';
            //        document.getElementById('lblErroCorpo').innerHTML = 'Erro na rotina de impressão do Histórico Escolar <br/> <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;

            //        $('#divModalErro').modal('show');
            //    }


            //});


        }

        function funcModalImprimirHistoricoOficial() {

            //alert("Hello world");
            $('#divModalImprimirHistoricoOficial').modal('show');
        }

        function funcClicaImprimirHistoricoOficial() {

            //alert("Hello world");
            fPreparaRelatorio('O relatório do Histórico Oficial está sendo preparado.');
            document.getElementById('<%=btnImprimirHitoricoOficial.ClientID%>').click();
        }

        function fEstrangeiro() {
            //alert("fEstrangeiro");
            var display = document.getElementById('<%=ddlEstrangeiro.ClientID%>').selectedIndex;
            if(display == "0") //não estrangeiro
            {
                document.getElementById('<%=divDDLEstadoNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divDDLCidadeNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTEstadoNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTCidadeNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=ddlNacionalidadeAluno.ClientID%>').classList.remove("select2-hidden-accessible")
                var element = document.getElementById('<%=ddlNacionalidadeAluno.ClientID%>');
                element.value = 'Brasileira';
                fSelect2();

                //document.getElementById('<%=ddlNacionalidadeAluno.ClientID%>').classList.add("select2-hidden-accessible")

            }   
            else
            { 
                document.getElementById('<%=divDDLEstadoNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divDDLCidadeNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTEstadoNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTCidadeNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=ddlNacionalidadeAluno.ClientID%>').classList.remove("select2-hidden-accessible")
                var element = document.getElementById('<%=ddlNacionalidadeAluno.ClientID%>');
                if (element.value == 'Brasileira') {
                    element.value = '';
                }
                fSelect2();
            }
        }

        function fEstrangeiro2() {
            //alert("fEstrangeiro2");
            var display = document.getElementById('<%=ddlNacionalidadeAluno.ClientID%>').value;
            if(display == "Brasileira") //não estrangeiro
            {
                document.getElementById('<%=divDDLEstadoNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divDDLCidadeNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTEstadoNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTCidadeNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=ddlEstrangeiro.ClientID%>').classList.remove("select2-hidden-accessible")
                document.getElementById('<%=ddlEstrangeiro.ClientID%>').selectedIndex = 0;
                fSelect2();
            }   
            else
            { 
                document.getElementById('<%=divDDLEstadoNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divDDLCidadeNasctoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTEstadoNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTCidadeNasctoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=ddlEstrangeiro.ClientID%>').classList.remove("select2-hidden-accessible")
                document.getElementById('<%=ddlEstrangeiro.ClientID%>').selectedIndex = 1;
                fSelect2();
            }
        }

        function fPaisResidencia() {
            //alert("fPaisResidencia");
            var display = document.getElementById('<%=ddlPaisResidenciaAluno.ClientID%>').value;
            if(display == "Brasil")
            {
                document.getElementById('<%=divDDLEstadoResidenciaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divDDLCidadeResidenciaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTEstadoResidenciaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTCidadeResidenciaAluno.ClientID%>').style.display = 'none';
                //document.getElementById("hdivDDLEstadoResidenciaAluno").value = "sim";
                fSelect2();
            }   
            else
            { 
                document.getElementById('<%=divDDLEstadoResidenciaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divDDLCidadeResidenciaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTEstadoResidenciaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTCidadeResidenciaAluno.ClientID%>').style.display = 'block';
                //document.getElementById("hdivDDLEstadoResidenciaAluno").value = "nao";
                fSelect2();
            }
        }

        function fPaisEmpresa() {
            //alert("fPaisEmpresa");
            var display = document.getElementById('<%=ddlPaisEmpresaAluno.ClientID%>').value;
            if(display == "Brasil")
            {
                document.getElementById('<%=divDDLEstadoEmpresaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divDDLCidadeEmpresaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTEstadoEmpresaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTCidadeEmpresaAluno.ClientID%>').style.display = 'none';
                fSelect2();
            }   
            else
            { 
                document.getElementById('<%=divDDLEstadoEmpresaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divDDLCidadeEmpresaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTEstadoEmpresaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTCidadeEmpresaAluno.ClientID%>').style.display = 'block';
                fSelect2();
            }
        }

        function fExibeImagem() {
            //$('#imagepreview').attr('src', "..\\img\\pessoas\\" + qId); // here asign the image to the modal when the user click the enlarge link
            document.getElementById('imagepreview').src = document.getElementById('<%=imgAluno.ClientID%>').src;
            document.getElementById('labelNomeExibeImagem').innerHTML = document.getElementById('<%=lblTituloNomeAluno.ClientID%>').innerHTML;
            $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
        }

        //=======================================

        function fImprimirCoorientadorBanca(element, qBanca) {
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;

            //alert(element.checked + " " + element.name);
            if (qBanca == "Qualificação") {
                var sAux = element.name.replace("chkImprimirQualificacaoCoorientador_","");
            }
            else {
                var sAux = element.name.replace("chkImprimirDefesaCoorientador_","");
            }
            
            var aAux = sAux.split("_");
            //alert(aAux[0] + " " + aAux[1]);

            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fImprimirCoorientadorBanca?qIdProfessor=" + aAux[0] + "&qIdBanca=" + aAux[1] + "&qImprimir=" + element.checked + "&qBanca=" + qBanca  + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração no status "Imprimir" da ' + qBanca + ' do Aluno ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração no status "Imprimir" da ' + qBanca + ' do Aluno. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fFechaProcessando();
                        fPreencheBancaCoorientador(qIdTurma,qBanca);
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração no status "Imprimir" da ' + qBanca + ' do Aluno</strong><br /><br />',
                            message: 'Alteração no status "Imprimir" da ' + qBanca + ' do Aluno realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert('Houve um erro na Alteração do status "Imprimir" da ' + qBanca + ' do Aluno. Por favor tente novamente.');
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert('Houve um erro na Alteração do status "Imprimir" da ' + qBanca + ' do Aluno. Por favor tente novamente!');
                    fFechaProcessando()
                }
            });

        }

        //=========================================================

        function fSalvarDadosQualificacao() {
            //return;
            var sAux =  "";
            if (document.getElementById('txtDataBancaQualificacao').value.trim() == '') {
                sAux = "Deve-se digitar a Data da Qualificação.<br><br>"
            }
            if (document.getElementById('txtHoraBancaQualificacao').value.trim() == '') {
                sAux = sAux + "Deve-se digitar a Hora da Qualificação.<br><br>"
            }
            if (document.getElementById('txtTituloBancaQualificacao').value.trim() == '') {
                sAux = sAux + "Deve-se digitar o Título da Qualificação.<br><br>"
            }
            if ($("#ddlBancaQualificacaoRemota option:selected").val() == "") {
                sAux = sAux + "Deve-se indicar se a Banca é Remota ou não.<br><br>";
            }
            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'ATENÇÂO ';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                return;
            }

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fSalvarDadosQualificacao?qIdTurma=" + document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value + "&qIdBanca=" + document.getElementById('txtNumeroBancaQualificacao').value + "&qData=" + document.getElementById('txtDataBancaQualificacao').value + "&qHora=" + document.getElementById('txtHoraBancaQualificacao').value + "&qRemota=" + $("#ddlBancaQualificacaoRemota option:selected").val() + "&qResultado=" + $("#ddlResultadoBancaQualificacao option:selected").val() + "&qTitulo=" + document.getElementById('txtTituloBancaQualificacao').value + "&qObservacao=" + document.getElementById('txtObdervacaoBancaQualificacao').value  + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração Dados da Qualificação do Aluno ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração de dados da qualificação do Aluno. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else if (json[0].P0 == "Aviso") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração Dados da Qualificação do Aluno ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = '<strong>Infelizmente NÃO serão realizadas as alterações pretendidas.</strong> <br /><br /><strong>Motivos: </strong><br /><br />' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        if (document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                            //alert("entrou: fPreencheBancaDefesaAluno");
                            fPreencheBancaDefesaAluno(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);
                        }

                        if (json[0].P1 != "0") {
                            document.getElementById('txtNumeroBancaQualificacao').value = json[0].P1;
                        }

                        <%--if (document.getElementById('divCoorientadorQualificacaoTemporario').style.display == "block") {
                            //alert("entrou: fPreencheBancaCoorientador");
                            fPreencheBancaQualificacaoAluno(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value, "Qualificação");
                        }--%>

                        fPreencheBancaQualificacaoAluno(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value, "Qualificação");

                        if (json[0].P2 != "") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração Dados da Qualificação do Aluno ';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = '<strong>Os dados foram salvos mas há as seguintes pendências</strong>:<br><br>' + json[0].P2;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-danger");
                            $('#divCabecalho').addClass('alert-warning');
                            $('#divMensagemModal').modal();
                        }
                        
                        fPreencheHistoricoMatricula(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);
                        //alert(json[0].P1);
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração Dados da Qualificação do Aluno</strong><br /><br />',
                            message: 'Alteração Dados da Qualificação do Aluno realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "right"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Alteração Dados da Qualificação do Aluno. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração Dados da Qualificação do Aluno. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //=========================================================

        function isDate(txtDate)
        {
            var currVal = txtDate;
            if(currVal == '')
                return false;

            var rxDatePattern = /^(\d{4})(\/|-)(\d{1,2})(\/|-)(\d{1,2})$/; //Declare Regex
            var dtArray = currVal.match(rxDatePattern); // is format OK?

            if (dtArray == null) 
                return false;

            //Checks for mm/dd/yyyy format.
            dtMonth = dtArray[3];
            dtDay= dtArray[5];
            dtYear = dtArray[1];        

            if (dtMonth < 1 || dtMonth > 12) 
                return false;
            else if (dtDay < 1 || dtDay> 31) 
                return false;
            else if ((dtMonth==4 || dtMonth==6 || dtMonth==9 || dtMonth==11) && dtDay ==31) 
                return false;
            else if (dtMonth == 2) 
            {
                var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
                if (dtDay> 29 || (dtDay ==29 && !isleap)) 
                    return false;
            }
            return true;
        }


        //=========================================================

        function fSalvarDadosDefesa() {
            //return;
            var sAux =  "";
            if (document.getElementById('txtDataBancaDefesa').value.trim() == '') {
                sAux = "Deve-se digitar a Data da Defesa.<br><br>";
            }
            if (document.getElementById('txtHoraBancaDefesa').value.trim() == '') {
                sAux = sAux + "Deve-se digitar a Hora da Defesa.<br><br>";
            }
            if (document.getElementById('txtTituloBancaDefesa').value.trim() == '') {
                sAux = sAux + "Deve-se digitar o Título da Defesa.<br><br>";
            }
            if ($("#ddlBancaDefesaRemota option:selected").val() == "") {
                sAux = sAux + "Deve-se indicar se a Banca é Remota ou não.<br><br>";
            }
            if (document.getElementById('txtDataEntregaBancaDefesa').value.trim() != '') {
                var date = document.getElementById('txtDataEntregaBancaDefesa').value.trim();
                if (isDate(date)) {
                    //if (document.getElementById('txtDataEntregaArtigoNew').value.trim() == "") {
                    //    sAux = sAux + "Para se incluir uma 'Data Aprovação Orientador' é necessário que antes se inclua a 'Data entrega Artigo'.<br><br>";
                    //}
                    if ($("#ddlResultadoBancaDefesa option:selected").text() != "Aprovado") {
                        sAux = sAux + "Para se incluir uma 'Data Aprovação Orientador' é necessário que o Resultado seja 'Aprovado'.<br><br>";
                    }
                }
                else {
                    sAux = sAux + "Deve-se digitar uma data válida para o campo Data Aprovação Orientador.<br><br>";
                    
                }
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'ATENÇÂO ';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                return;
            }

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fSalvarDadosDefesa?qIdTurma=" + document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value + "&qIdBanca=" + document.getElementById('txtNumeroBancaDefesa').value + "&qData=" + document.getElementById('txtDataBancaDefesa').value + "&qHora=" + document.getElementById('txtHoraBancaDefesa').value + "&qRemota=" + $("#ddlBancaDefesaRemota option:selected").val() + "&qResultado=" + $("#ddlResultadoBancaDefesa option:selected").val() + "&qTitulo=" + document.getElementById('txtTituloBancaDefesa').value + "&qObservacao=" + document.getElementById('txtObdervacaoBancaDefesa').value + "&qDataEntrega=" + document.getElementById('txtDataEntregaBancaDefesa').value + "&qNumeroPortariaMecBancaDefesa=" + document.getElementById('txtNumeroPortariaMecBancaDefesa').value + "&qDataPortariaMecBancaDefesa=" + document.getElementById('txtDataPortariaMecBancaDefesa').value + "&qDataDOUBancaDefesa=" + document.getElementById('txtDataDOUBancaDefesa').value  + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração Dados da Defesa do Aluno ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração de dados da Defesa do Aluno. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else if (json[0].P0 == "Aviso") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração Dados da Defesa do Aluno ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = '<strong>Infelizmente NÃO serão realizadas as alterações pretendidas.</strong> <br /><br /><strong>Motivos: </strong><br /><br />' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }

                    else {
                        if (json[0].P1 != "0") {
                            document.getElementById('txtNumeroBancaDefesa').value = json[0].P1;
                        }

                        if (document.getElementById('divCoorientadorDefesaTemporario').style.display = "block") {
                            fPreencheBancaDefesaAluno(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value, "Defesa");
                        }

                        fPreencheHistoricoMatricula(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração Dados da Defesa do Aluno</strong><br /><br />',
                            message: 'Alteração Dados da Defesa do Aluno realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "right"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Alteração Dados da Defesa do Aluno. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração Dados da Defesa do Aluno. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //=========================================================

        function fImprimirAtestadoPre(qBanca) {
            //return;
            var idProfessor;
            var sNomeProfessor;
            var idBanca;

            if (qBanca == "Qualificação") {
                idProfessor = document.getElementById('txtIdOrientadorBancaQualificacao').value;
                sNomeProfessor = document.getElementById('txtNomeOrientadorBancaQualificacao').value;
                idBanca = document.getElementById('txtNumeroBancaQualificacao').value;
            }
            else {
                idProfessor = document.getElementById('txtIdOrientadorBancaDefesa').value;
                sNomeProfessor = document.getElementById('txtNomeOrientadorBancaDefesa').value;
                idBanca = document.getElementById('txtNumeroBancaDefesa').value;
            }

            fImprimirAtestado(idProfessor, sNomeProfessor, idBanca, qBanca);

        }
        
        //=========================================================

        function fImprimirAtestado(idProfessor, sNomeProfessor, idBanca, qBanca) {
            //return;
            document.getElementById('hCodigo').value = idProfessor + "," + sNomeProfessor + "," + idBanca + "," + qBanca + "," + document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            fPreparaRelatorio('O Atestado do prof. ' + sNomeProfessor + ' está sendo preparado.');
            document.getElementById('<%=btnImprimirAtestado.ClientID%>').click();
        }

        //===========================================

        function fImprimirConvitePre(qBanca) {
            //return;
            var idProfessor;
            var sNomeProfessor;
            var idBanca;

            if (qBanca == "Qualificação") {
                idProfessor = document.getElementById('txtIdOrientadorBancaQualificacao').value;
                sNomeProfessor = document.getElementById('txtNomeOrientadorBancaQualificacao').value;
                idBanca = document.getElementById('txtNumeroBancaQualificacao').value;
            }
            else {
                idProfessor = document.getElementById('txtIdOrientadorBancaDefesa').value;
                sNomeProfessor = document.getElementById('txtNomeOrientadorBancaDefesa').value;
                idBanca = document.getElementById('txtNumeroBancaDefesa').value;
            }

            fImprimirConvite(idProfessor, sNomeProfessor, idBanca, qBanca);

        }
        
        //=========================================================

        function fImprimirConvite(idProfessor, sNomeProfessor, idBanca, qBanca) {
            //return;
            document.getElementById('hCodigo').value = idProfessor + "," + sNomeProfessor + "," + idBanca + "," + qBanca + "," + document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            fPreparaRelatorio('O Convite do prof. ' + sNomeProfessor + ' está sendo preparado.');
            document.getElementById('<%=btnImprimirConvite.ClientID%>').click();
        }

        //===========================================

        function fImprimirRecibo(idProfessor, sNomeProfessor, idBanca, qBanca) {
            //return;
            document.getElementById('hCodigo').value = idProfessor + "," + sNomeProfessor + "," + idBanca + "," + qBanca + "," + document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
            fPreparaRelatorio('O Recibo do prof. ' + sNomeProfessor + ' está sendo preparado.');
            document.getElementById('<%=btnImprimirRecibo.ClientID%>').click();
        }

        //===========================================

        function fImprimirDivulgacao(qBanca) {
            //return;
            
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;

            //if (qBanca == "Qualificação") {
            //    idProfessor = document.getElementById('txtIdOrientadorBancaQualificacao').value;
            //    sNomeProfessor = document.getElementById('txtNomeOrientadorBancaQualificacao').value;
            //    idBanca = document.getElementById('txtNumeroBancaQualificacao').value;
            //}

            document.getElementById('hCodigo').value = qBanca + "," + qIdTurma;
            fPreparaRelatorio('O relatório de Divulgação da banca de ' + qBanca + ' está sendo preparado.');
            document.getElementById('<%=btnImprimirDivulgacao.ClientID%>').click();

        }

        //===========================================
        function fAbreModalObsAta(qBanca) {
            document.getElementById('<%=txtObsAta.ClientID%>').value = "";
            document.getElementById('btnModalObsAtaQualificacao').style.display = 'none';
            document.getElementById('btnModalObsAtaDefesa').style.display = 'none';

            if (qBanca == 'Defesa') {
                document.getElementById('btnModalObsAtaDefesa').style.display = 'block';
            }
            else {
                document.getElementById('btnModalObsAtaQualificacao').style.display = 'block';
            }
            $('#divModalObsAta').modal('show');
        }

        //===========================================

        function fImprimirAta(qBanca) {
            //return;
            
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;

            //if (qBanca == "Qualificação") {
            //    idProfessor = document.getElementById('txtIdOrientadorBancaQualificacao').value;
            //    sNomeProfessor = document.getElementById('txtNomeOrientadorBancaQualificacao').value;
            //    idBanca = document.getElementById('txtNumeroBancaQualificacao').value;
            //}

            document.getElementById('hCodigo').value = qBanca + "," + qIdTurma;
            fPreparaRelatorio('O relatório da Ata da banca de ' + qBanca + ' está sendo preparado.');
            document.getElementById('<%=btnImprimirAta.ClientID%>').click();

        }

        //===========================================

        function fImprimirContrato() {
            //return;
            
            var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;

            //if (qBanca == "Qualificação") {
            //    idProfessor = document.getElementById('txtIdOrientadorBancaQualificacao').value;
            //    sNomeProfessor = document.getElementById('txtNomeOrientadorBancaQualificacao').value;
            //    idBanca = document.getElementById('txtNumeroBancaQualificacao').value;
            //}

            document.getElementById('hCodigo').value = qIdTurma + "," + document.getElementById('<%=txtNomeAluno.ClientID%>').value;
            fPreparaRelatorio('O Contrato do aluno ' + document.getElementById('<%=txtNomeAluno.ClientID%>').value + ' está sendo preparado.');
            document.getElementById('<%=btnImprimirContrato.ClientID%>').click();

        }


        //===========================================

        function fModalAdicionaOrientadorBanca(qBanca, qTipoProfessor) {
            if (qBanca == "Qualificação") {
                if (qTipoProfessor == "Orientador") {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Alterar Orientador";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "block";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "none";

                }
                else if (qTipoProfessor == "Coorientador") {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Adicionar Co-orientador";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "block";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "none";
                }
                else if (qTipoProfessor == "Membro") {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Adicionar Membro";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "block";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "none";
                }
                else {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Adicionar Suplente";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "block";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "none";
                }
            }
            else {
                if (qTipoProfessor == "Orientador") {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Alterar Orientador";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "block";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "none";

                }
                else if (qTipoProfessor == "Coorientador") {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Adicionar Co-orientador";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "block";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "none";
                }
                else if (qTipoProfessor == "Membro") {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Adicionar Membro";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "block";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "none";
                }
                else {
                    document.getElementById("lblTituloModalSelecionarBanca").innerHTML = "Adicionar Suplente";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaQualificacao").style.display = "none";
                    document.getElementById("btnPerquisaOrientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaCoorientadorDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaMembroDisponivelBancaDefesa").style.display = "none";
                    document.getElementById("btnPerquisaSuplenteDisponivelBancaDefesa").style.display = "block";
                }
            }

            document.getElementById("divgrdBancaDisponivel").style.display = "none";
            $('#divModalSelecionarBanca').modal('show');  
        }

        //================================================================================

        function fPesquisaBancaDisponivel(qBanca, qTipoProfessor) {
            fProcessando();
            try {
                var qIdTurma = document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value;
                var qCPF = document.getElementById('txtCPFBancaPesquisa').value;
                var qNome = document.getElementById('txtNomeBancaPesquisa').value;
                var dt = $('#grdBancaDisponivel').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: true, //Pesquisar
                    bPaginate: true, // Paginação
                    bInfo: true, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdBancaDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdBancaDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdBancaDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdBancaDisponivel").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdBancaDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdBancaDisponivel").style.display = "none";

                                var table_grdBancaDisponivel = $('#grdBancaDisponivel').DataTable();

                                $('#grdBancaDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdBancaDisponivel = table_grdBancaDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPesquisaBancaDisponivel?qCPF=" + qCPF + "&qNome=" + qNome + "&qIdTurma=" + qIdTurma + "&qBanca=" + qBanca + "&qTipoProfessor=" + qTipoProfessor + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left"
                        },
                        {
                            "data": "P2", "title": "CPF", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P3", "title": "Adicionar", "orderable": false, "className": "text-center"
                        }
                    ],
                    order: [[1, 'asc']],
                    dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                    lengthMenu: [[10, 20, 40, 60, -1], [10, 20, 40, 60, "Todos"]],
                    buttons: [

                    ],
                    "language": {
                    
                            "sEmptyTable": "Nenhum registro encontrado",
                            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sInfoThousands": ".",
                            "sLengthMenu": "Mostrando _MENU_ resultados por página",
                            "sLoadingRecords": "Carregando...",
                            "sProcessing": "Processando...",
                            "sZeroRecords": "Nenhum registro encontrado",
                            "sSearch": "Pesquisar",
                            "oPaginate": {
                                "sNext": ">",
                                "sPrevious": "<",
                                "sFirst": "Primeiro",
                                "sLast": "Último"
                            },
                            "oAria": {
                                "sSortAscending": ": Ordenar colunas de forma ascendente",
                                "sSortDescending": ": Ordenar colunas de forma descendente"
                            }

                    },
                    fixedHeader: true
                });

            } catch (e) {
                fFechaProcessando();
            }
            
        }

        //============================================

        function fAlterarOrientadorBanca(qIdBanca, qIdProfessor, qCpf, qNome, qBanca ) {
            //return;
           
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fAlterarOrientadorBanca?qIdBanca=" + qIdBanca + "&qIdProfessor=" + qIdProfessor + "&qCpf=" + qCpf + "&qNome=" + qNome + "&qBanca=" + qBanca + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração do Orientador da ' + qBanca + ' do Aluno ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração do Orientador da ' + qBanca + ' do Aluno. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        if (qBanca == "Qualificação") {
                            document.getElementById('txtIdOrientadorBancaQualificacao').value = qIdProfessor;
                            document.getElementById('txtCpfOrientadorBancaQualificacao').value = qCpf;
                            document.getElementById('txtNomeOrientadorBancaQualificacao').value = qNome;
                        }
                        else {
                            document.getElementById('txtIdOrientadorBancaDefesa').value = qIdProfessor;
                            document.getElementById('txtCpfOrientadorBancaDefesa').value = qCpf;
                            document.getElementById('txtNomeOrientadorBancaDefesa').value = qNome;
                        }    
                        $('#divModalSelecionarBanca').modal('hide');

                        if (document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                            fPreencheBancaDefesaAluno(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);
                        }

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração do Orientador da ' + qBanca + ' do Aluno</strong><br /><br />',
                            message: 'Alteração do Orientador da ' + qBanca + ' do Aluno realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Alteração do Orientador da " + qBanca + " do Aluno. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração do Orientador da " + qBanca + " do Aluno. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //=========================================================

        function fIncluirProfessorBanca(qIdBanca, qIdProfessor, qCpf, qNome, qTipoProfessor, qBanca) {
            //return;
           
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluirProfessorBanca?qIdBanca=" + qIdBanca + "&qIdProfessor=" + qIdProfessor + "&qCpf=" + qCpf + "&qNome=" + qNome + "&qTipoProfessor=" + qTipoProfessor + "&qBanca=" + qBanca + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão do ' + qTipoProfessor + ' da ' + qBanca + ' do Aluno ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do ' + qTipoProfessor + ' da ' + qBanca + ' do Aluno. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {    
                        if (qTipoProfessor == "Co-orientador") {
                            fPreencheBancaCoorientador(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value, qBanca);
                        }
                        else {
                            fPreencheBancaMembro(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value, qBanca);
                        }

                        //alert("qNumero: " + document.getElementById('txtNumeroBancaDefesa').value);
                        if (document.getElementById('btnImprimirAtaDefesa').style.display != "block") {
                            fPreencheBancaDefesaAluno(document.getElementById('<%=txtIdTurmaAlunoNew.ClientID%>').value);
                        }
                         
                        // A pedido do Adilson no dia 25/02/2019 não é pra fechar o modal assim que um professor é incluído.
                        //$('#divModalSelecionarBanca').modal('hide');
                        //================================================

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão do ' + qTipoProfessor + ' da ' + qBanca + ' do Aluno</strong><br /><br />',
                            message: 'Inclusão do ' + qTipoProfessor + ' da ' + qBanca + ' do Aluno realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na Inclusão do " + qTipoProfessor + " da " + qBanca + " do Aluno. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Inclusão do " + qTipoProfessor + " da " + qBanca + " do Aluno. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //=========================================================

        function isMobile() {
            if (navigator.userAgent.match(/Android/i)
                    || navigator.userAgent.match(/webOS/i)
                    || navigator.userAgent.match(/iPhone/i)
                    || navigator.userAgent.match(/iPad/i)
                    || navigator.userAgent.match(/iPod/i)
                    || navigator.userAgent.match(/BlackBerry/i)
                    || navigator.userAgent.match(/Windows Phone/i)
            ) {
                return true;
            }
            else {
                return false;
            }
        }

        //function fPreencheCidade() {


        //    $("#profiles-thread").select2({
        //        minimumInputLength: 2,
        //        tags: [],
        //        ajax: {
        //            url: '/wsSapiens.asmx/preencheCidade',
        //            dataType: 'json',
        //            type: "GET",
        //            quietMillis: 50,
        //            data: function (term) {
        //                return {
        //                    term: term
        //                };
        //            },
        //            results: function (data) {
        //                return {
        //                    results: $.map(data, function (item) {
        //                        return {
        //                            text: item.completeName,
        //                            slug: item.slug,
        //                            id: item.id
        //                        }
        //                    })
        //                };
        //            }
        //        }
        //    });


                
        //}

        $(function () {

            var mobile = isMobile();

            if(mobile == true){
                //window.location('http://www/mobile.asp');
                //document.getElementById("<%//=txtNomeUsuario.ClientID%>").className = "form-control input-sm";
                //document.getElementById("<%//=txtNomeUsuario.ClientID%>").className = "form-control input-sm";
                //document.getElementById("<%//=txtNomeUsuario.ClientID%>").className = "form-control input-sm"
            }
        });

        //===============================================================

        function fInibeAlteracoes() {
            document.getElementById('btnSalvarBancaQualificao').style.display='none';
            document.getElementById('btnAlterarOrientadorQualificacao').style.display='none';    
            document.getElementById('btnAdicionarCoOrientacaoBancaQualificacao').style.display='none'; 
            document.getElementById('btnAdicionarMembrosBancaQualificacao').style.display='none'; 
            document.getElementById('btnAdicionarMembrosSuplentesBancaQualificacao').style.display='none'; 

            document.getElementById('btnSalvarBancaDefesa').style.display='none';
            document.getElementById('btnAlterarOrientadorDefesa').style.display='none';    
            document.getElementById('btnAdicionarCoOrientacaoBancaDefesa').style.display='none'; 
            document.getElementById('btnAdicionarMembrosSuplentesBancaDefesa').style.display='none'; 
            document.getElementById('btnAdicionarMembrosBancaDefesa').style.display='none'; 
        }

        //===============================================================

        function fEscondeColunas(qGrade, qColuna) {
            <%--var aColunas = document.getElementById('<%=txtqColunas.ClientID%>').value;
            var qColunas = aColunas.split(";");
            var tbl = $('#<%=grdAluno.ClientID%>');

            for (var i = 0; i < qColunas.length; i++) {
                //alert('i: ' + i + " qColunas[i]: " + qColunas[i]);
                tbl.DataTable().column(qColunas[i]).visible(false);
                //tbl.fnSetColumnVis(qColunas[i], false);
            }--%>
            var tbl = $('#' + qGrade);
            tbl.DataTable().column(qColuna).visible(false);
        }

        //===============================================================

        function fPreparaRelatorio(qRelatorio) {
            $.notify({
                icon: 'fa fa fa-print fa-lg',
                title: '<strong>Preparação de Relatório</strong><br /><br />',
                message: qRelatorio + '<br /><br /> AGUARDE...',

            }, {
                //type: 'info',
                delay: 1500,
                timer: 1500,
                z_index: 5000,
                animate: {
                    enter: 'animated flipInY',
                    exit: 'animated flipOutX'
                },
                placement: {
                    from: "bottom",
                    align: "left"
                }
            });
        } 
        
        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "date-euro-pre": function (a) {
                var x;

                if ($.trim(a) !== '') {
                    var frDatea = $.trim(a).split(' ');
                    var frTimea = (undefined != frDatea[1]) ? frDatea[1].split(':') : [00, 00, 00];
                    var frDatea2 = frDatea[0].split('/');
                    x = (frDatea2[2] + frDatea2[1] + frDatea2[0] + frTimea[0] + frTimea[1] + ((undefined != frTimea[2]) ? frTimea[2] : 0)) * 1;
                }
                else {
                    x = Infinity;
                }

                return x;
            },

            "date-euro-asc": function (a, b) {
                return a - b;
            },

            "date-euro-desc": function (a, b) {
                return b - a;
            }
        });

        //==== Detectar se tem WebCam ================
       <%-- navigator.getMedia = ( navigator.getUserMedia || // use the proper vendor prefix
                       navigator.webkitGetUserMedia ||
                       navigator.mozGetUserMedia ||
                       navigator.msGetUserMedia);

        navigator.getMedia({video: true}, function() {
            // webcam is available
            //alert('Tem webCam');
            document.getElementById('<%=btnAbrirCamera.ClientID%>').style.display='block';
        }, function() {
            // webcam is not available
            document.getElementById('<%=btnAbrirCamera.ClientID%>').style.display='none';
        });--%>
        //=============================================

        function fAbrirCamera() {

            Webcam.set({
                width: 380,
                height: 300,
                dest_width:320,
                dest_height: 240,
                crop_width: 240,
                crop_height:240,
                image_format: 'jpeg',
                jpeg_quality: 90,
                force_flash: false
            });
            Webcam.attach('#my_camera');

            document.getElementById('results').innerHTML =
                  //'<h2>Here is your image:</h2>' +
                  'A imagem capturada aparecerá aqui...';
            $('#divModalCamera').modal();
        }

        var WebCam_data_uri_clone;
        var WebCam_data_uri;
        var qOrigem = "1";

        function fFecharCamera() {
            //$("#my_camera").replaceWith(originalState);
            Webcam.reset();
            //alert("fechou");
        }

        function fConfirmarCaptura() {
            //$("#my_camera").replaceWith(originalState);
            document.getElementById('<%=imgprw.ClientID%>').src = WebCam_data_uri;
            Webcam.reset();
            document.getElementById('divBotoes').style.display = 'block';
            document.getElementById('divBotaoSalvar').style.display = 'block';

            document.getElementById('divImgPrw').style.display = 'block';
            document.getElementById('divMensagens').style.display = 'none';
            document.getElementById('divBntLocalizar').style.display = 'none';
            document.getElementById('hEscolheuFoto').value = 'true';
            qOrigem = "2"

        }

        function take_snapshot() {
            // take snapshot and get image data
            Webcam.snap(function(data_uri) {
                // display results in page
                document.getElementById('results').innerHTML =
                  //'<h2>Here is your image:</h2>' +
                  '<img src="' + data_uri + '"/>';
                //alert(data_uri);
                WebCam_data_uri = data_uri;

                var block = data_uri.split(";");
                // Get the content type of the image
                var contentType = block[0].split(":")[1];// In this case "image/gif"
                // get the real base64 content of the file
                var realData = block[1].split(",")[1];// In this case "R0lGODlhPQBEAPeoAJosM...."

                // Convert it to a blob to upload
                WebCam_data_uri_clone = b64toBlob(realData, contentType);

                //alert(data_uri_clone);
                // Create a FormData and append the file with "image" as parameter name
                //var formDataToUpload = new FormData(form);
                //formDataToUpload.append("image", blob);

            });
        }

        //===================

        function b64toBlob(b64Data, contentType, sliceSize) {
            contentType = contentType || '';
            sliceSize = sliceSize || 512;

            var byteCharacters = atob(b64Data);
            var byteArrays = [];

            for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
                var slice = byteCharacters.slice(offset, offset + sliceSize);

                var byteNumbers = new Array(slice.length);
                for (var i = 0; i < slice.length; i++) {
                    byteNumbers[i] = slice.charCodeAt(i);
                }

                var byteArray = new Uint8Array(byteNumbers);

                byteArrays.push(byteArray);
            }

            var blob = new Blob(byteArrays, {type: contentType});
            return blob;
        }
    </script>
</asp:Content>
