<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadProfessorGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadProfessorGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li2Professores" />

    <input type="hidden" runat="server" id ="hCPF_Passaporte"  name="hCPF_Passaporte" value="CPF" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="Scripts/jquery.validate.js"></script>
    <script src="Scripts/jquery.mask.min.js"></script>

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
  
    <asp:Literal ID="litInputCodigoEvidencia" runat="server"></asp:Literal>
    <style type="text/css">
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
    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
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
    </asp:UpdateProgress>

    <div class="container-fluid">
        <input type="hidden" id ="hEscolheuFoto"  name="hEscolheuFoto" value="false" />
        <div class="row"> 
            <div class="col-md-8">
                <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong>Professor</strong><asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label> </h3>
            </div>
            <div class="hidden-lg hidden-md">
                <br /> <br /> 
            </div>

            <br />
            <div class="col-md-2 pull-right">
                <button type="button" runat="server" id="btnNovoProfessor" class="btn btn-primary" href="#" onclick="" onserverclick="btnNovoProfessor_Click"> <%--onserverclick="btnNovoAluno_Click"--%>
                    <i class="fa fa-magic"></i>&nbsp;Cadastrar novo Professor</button>
            </div>

        </div>
        <br />
        <hr />
        <br />

        <div class="row">
            <div class="col-md-1 center-block">
                <a href='javascript:fExibeImagem()'>
                    <img runat="server" id="imgProfessor" src="img/pessoas/avatarunissex.jpg" class="img-rounded center-block" alt="Imagem do Professor" style="width: 80px; height: 80px;" />
                </a>
                <hr style="height:3pt; visibility:hidden;" />

                <div id="divBotaoFoto" runat="server">
                    <button id="btnAlterarImagem2" type="button" class="btn btn-primary btn-xs center-block" onclick="AbreModalDadosFoto()"><i class="fa fa-camera"></i>&nbsp;Trocar foto</button>
                </div>
                <div id="divTextoBotaoFoto" runat="server">
                    <p>Crie o Professor e depois altere a foto.</p>
                </div>
            </div>
            <div class="hidden-lg hidden-md">
                <br />
            </div>

            <div class="col-md-1">
                <h3 class="">
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloCodigo" runat="server" Text="Código"></asp:Label><br /> </span>&nbsp;<asp:Label ID="lblNumeroCodigo" runat="server" Text="0000"></asp:Label>
                </h3>
            </div>

            <div class="col-md-4">
                <h3 class="">
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloProfessor_a" runat="server" Text="Professor"></asp:Label> </span><br />&nbsp;<asp:Label ID="lblTituloNomeProfessor" runat="server" Text="Label"></asp:Label>
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

                                                <div class="modal-header bg-info">
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
                                                                    <strong>Foto atual</strong><hr /> <br />
                                                                    <img id="imgFotoOriginal" alt="Foto atual" runat="server" class="img-responsive center-block" title="Foto atual" src="#" style="max-height: 500px" />
                                                                    <br /><hr />
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <strong>Nova foto</strong><hr /> <br />
                                                                    <div id="fileUpload" style="float: left; vertical-align: top">
                                                                        <h5 id="errorBlock" class="file-error-message" style="display: none;"></h5>
                                                                        <span class="file-input file-input-new">
                                                                            <div class="row ">
                                                                                <div class="col-md-12">
                                                                                    <div id="divImgPrw" class="file-preview " style="display: none">
                                                                                        <div class="" id="divImgPreview">
                                                                                            <img id="imgprw" alt="" runat="server" src="img/pessoas/avatarunissex.jpg" class="img-responsive center-block" style="max-height: 500px" />
                                                                                        </div>
                                                                                        <br />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row " id="divBntLocalizar" style="display: block">
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-3"></div>
                                                                                    <div class="col-md-6">
                                                                                        <button type="button" id="btnLocalizar" class="btn btn-primary btn-md"
                                                                                            onclick="javascript:document.getElementById('<%=fileArquivoParaGravar.ClientID%>').click();">
                                                                                            <i class="glyphicon glyphicon-folder-open"></i>&nbsp;&nbsp;Localizar nova Foto …
                                                                                        </button>
                                                                                    </div>
                                                                                    <div class="col-md-3"></div>
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
                                                                            <small><i class="fa fa-check"></i> Imagens com tamanho máximo de 60 Kb!</small><br />
                                                                            <small><i class="fa fa-check"></i> Somente imagens com extenção "JPG" ou "JEPG"!</small><br />
                                                                            <small><i class="fa fa-check"></i> Evite nome de arquivo com caracteres especiais!</small><br />
                                                                        </div>
                                                                        <br /><hr />
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
                                    <li id="tabDadosProfessor" class="active"><a href="#tab_DadosProfessor" id="atab_DadosProfessor" data-toggle="tab"><strong>Cadastro</strong></a></li>
                                    <li id="tabSituacaoAcademica" runat="server" class="hidden"><a href="#tab_SituacaoAcademica" id="atab_SituacaoAcademica"  data-toggle="tab"><strong>Outros</strong></a></li>
                                </ul>

                                <br />

                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_DadosProfessor">
                                        <%--                                        <b>How to use:</b>--%>
                                        <div class="box box-primary">
                                            <div class="box-header">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h3 class="box-title">Dados Pessoais</h3>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br /> 
                                                    </div>

                                                    <div class="col-md-2">
                                                        <button type="button" runat="server" id="btnInativar" class="btn btn-danger" href="#" onclick="fModalAtivaInativa('Inativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                                                            <i class="fa fa-toggle-off"></i> Inativar Professor
                                                        </button>
                                                        <button type="button" runat="server" id="btnAtivar" class="btn btn-info" href="#" onclick="fModalAtivaInativa('Ativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                                                            <i class="fa fa-toggle-on"></i> Ativar Professor
                                                        </button>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br /> 
                                                    </div>

                                                    <div class="col-md-2 ">
                                                        <button type="button" runat="server" id="btnImprimirCadastro" class="btn btn-warning" href="#" onclick="" onserverclick="btnImprimirCadastro_Click"> <%--onserverclick="btnNovoAluno_Click"--%>
                                                            <i class="fa fa-print"></i>&nbsp;Imprimir Cadastro</button>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br /> <br /> 
                                                    </div>

                                                    <div class="col-md-2 pull-right">
                                                        <button type="button" runat="server" id="bntSalvarProfessor2" class="btn btn-success" href="#" onclick="if (fProcessando()) return;" onserverclick="bntSalvarProfessor_Click"> <%--onserverclick="btnNovoAluno_Click"--%>
                                                            <i class="fa fa-save"></i> Salvar Dados
                                                        </button>
                                                    </div>


                                                </div>
                                                
                                            </div>

                                            <div class="box-body">
                                                <%--Dados Pessoais - Início--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Dados Pessoais</h5>
                                                    <div class="row">
                                                        <div class="col-md-4 ">
                                                            <span class ="text-red text-bold">* </span><span>Nome</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNomeProfessor" type="text" value="" maxlength="150" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        
                                                        <div class="col-md-3" style="margin-top:-5px">
                                                            <span class ="text-red text-bold">* </span><%--<span id="lblCPF_Passaporte" runat="server">CPF</span> <a href="javascript:fAlteraCPF_Passaporte();" style="display:inline-block" title="CPF ou Passaporte"><i runat="server" id="iCPF_Passaporte" class="fa fa-toggle-on"></i></a> --%>
                                                            <asp:RadioButton GroupName="GrupoCPF_Passaporte" ID="optCPF" runat="server" Checked="true"/>
                                                            &nbsp;
                                                            <label class="opt" for="<%=optCPF.ClientID %>">CPF</label>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
                                                            <asp:RadioButton GroupName="GrupoCPF_Passaporte" ID="optPassaporte" runat="server" />
                                                            &nbsp;
                                                            <label class="opt" for="<%=optPassaporte.ClientID %>">Passaporte</label>
                                                            <br />
                                                            <input class="form-control input-sm" runat="server" id="txtCPFProfessor" type="text" value=""/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div> 

                                                        <div class="col-md-2">
                                                            <span>Sexo</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlSexoProfessor" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                                <asp:ListItem Text="Masculino" Value="m" />
                                                                <asp:ListItem Text="Feminino" Value="f" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3">
                                                            <span>Data Nascimento</span><br />
                                                            <%--<div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>--%>
                                                                <input class="form-control input-sm" runat="server" id="txtDataNascimentoProfessor" type="date" value=""/>
                                                            <%--</div>--%>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 hidden">
                                                            <span>Data Cadastro</span><br />
                                                            <%--<div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>--%>
                                                                <input class="form-control input-sm" runat="server" id="txtDataCadastroProfessor" type="text" value="" readonly="readonly"/>
                                                            <%--</div>--%>
                                                        </div>
                                                        
                                                    </div>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <span>Tipo Documento</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlTipoDoctoProfessor" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                                <asp:ListItem Text="RG" Value="RG" />
                                                                <asp:ListItem Text="RNE" Value="RNE *" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Número</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroDoctoProfessor" type="text" value="" maxlength="20"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2">
                                                            <span>Nacionalidade</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNacionalidadeProfessor" type="text" value="" maxlength="50" />

                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-6 ">
                                                            <span>Url CV Lattes</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtUrlLatesProfessor" type="text" value="" maxlength="200" />
                                                        </div>
                                                        
                                                    </div>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <span class ="text-red text-bold">* </span><span>Email principal</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-at"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtEmail1Professor" type="text" value="" maxlength="70" />
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div id="divConfirnacaoEmail" runat="server" visible="false">
                                                            <div class="col-md-3">
                                                                <div id="divEmailConfirmado" runat="server">
                                                                    <br />
                                                                    <span class="alert-success">&nbsp;&nbsp;<i class="fa fa-check-circle-o"></i> Email principal Confirmado&nbsp;&nbsp; </span>
                                                                </div>
                                                                <div id="divEmailNaoConfirmado" runat="server">
                                                                    <span class="alert-danger">&nbsp;&nbsp;<i class="fa fa-times-circle-o"></i> Email principal Não Confirmado&nbsp;&nbsp;</span><br />
                                                                    <button id="btnEnvirConfirmacaoEmail" type="button" runat="server" class="btn btn-danger btn-xs" onserverclick="btnbtnEnvirConfirmacaoEmail_Click"> 
                                                                        <i class="fa fa-share-square-o"></i> Reenviar E-mail
                                                                    </button>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Email secundário</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-at"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtEmail2Professor" type="text" value="" maxlength="100" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <span>Telefone</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-phone"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtTelefoneProfessor" type="text" value="" maxlength="15" onkeypress="return ehNumeroOuTRaco(event)" />
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Celular</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-phone"></i>
                                                                </div>
                                                                <input class="form-control input-sm" runat="server" id="txtCelularProfessor" type="text" value="" maxlength="15" onkeypress="return ehNumeroOuTRaco(event)" />
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                    
                                                </div>
                                                <%--Dados Pessoais - Fim--%>

                                                <%--Residência - Início--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Residência</h5>
                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>CEP</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCepResidenciaProfessor" type="text" value="" maxlength="9"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-5 ">
                                                            <span>Logradouro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtLogradouroResidenciaProfessor" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        <div class="col-md-1 ">
                                                            <span>Número</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroResidenciaProfessor" type="number" value="" maxlength="20" min="1" onkeypress="return soNumero(event)"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Complemento</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtComplementoResidenciaProfessor" type="text" value="" maxlength="50"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Bairro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtBairroResidenciaProfessor" type="text" value="" maxlength="50"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlEstadoResidenciaProfessor" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <div class="col-md-2 ">
                                                                    <span>Estado</span><br />
                                                                    <asp:DropDownList runat="server" ID="ddlEstadoResidenciaProfessor" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoResidenciaProfessor_SelectedIndexChanged"> <%--OnSelectedIndexChanged="ddlEstadoResidenciaProfessor_SelectedIndexChanged" --%>
                                                                    </asp:DropDownList>
                                                                    <input class="form-control input-sm" runat="server" id="txtEstadoResidenciaProfessorAnt" type="text" value="" maxlength="50" style="display:none" readonly="readonly" />
                                                                </div>
                                                                <div class="hidden-lg hidden-md">
                                                                    <br />
                                                                </div>

                                                                <div class="col-md-4 ">
                                                                    <span>Cidade</span><br />
                                                                    <asp:DropDownList runat="server" ID="ddlCidadeResidenciaProfessor" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                                                    </asp:DropDownList>
                                                                    <input class="form-control input-sm" runat="server" id="txtCidadeResidenciaProfessorAnt" type="text" value="" maxlength="50" style="display:none" readonly="readonly"/>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Placa Veículo</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtPlacaProfessor" type="text" value="" maxlength="20"/>
                                                        </div>

                                                    </div>                                                  
                                                </div>
                                                <%--Residência - Fim--%>

                                                <%--Tìtulo Acadêmico - Início--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Título Acadêmico</h5>
                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>Título</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlTituloProfessor" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Ano de Obtenção</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtAnoObtencaoProfessor" type="number" value="" maxlength="4" min="0" onkeypress="javascript: if (this.value.length > 3) return false;"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Local</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtLocalTituloProfessor" type="text" value="" maxlength="100"/>
                                                        </div>
                                                    </div>  
                                                </div>
                                                <%--Tìtulo Acadêmico - Fim--%>

                                                <%--Dados Bancários - Início--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Dados Bancários Pessoa Física</h5>
                                                    <div class="row">
                                                        <div class="col-md-3 ">
                                                            <span>Nome do Banco</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNomeBancoProfessor" type="text" value="" maxlength="25"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>N.º do Banco</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroBancoProfessor" type="text" value="" maxlength="10"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>N.º da Agência</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtAgenciaProfessor" type="text" value="" maxlength="25"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>N.º da Conta</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroContaProfessor" type="text" value="" maxlength="50"/>
                                                        </div>
                                                    </div>  
                                                </div>
                                                <%--Dados Bancários - Fim--%>

                                                <%--Dados Comerciais - Início--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Dados Comerciais</h5>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <span>Empresa</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtEmpresaProfessor" type="text" value="" maxlength="200"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>CEP</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCEPEmpresaProfessor" type="text" value="" maxlength="9"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-5 ">
                                                            <span>Logradouro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtLogradouroEmpresaProfessor" type="text" value="" maxlength="100"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        <div class="col-md-1 ">
                                                            <span>Número</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroEmpresaProfessor" type="number" value="" maxlength="20" min="1"  onkeypress="return soNumero(event)"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Complemento</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtComplementoEmpresaProfessor" type="text" value="" maxlength="50"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Bairro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtBairroEmpresaProfessor" type="text" value="" maxlength="50"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>País</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlPaisEmpresaProfessor" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false" onchange="fPaisEmpresa()">
                                                            </asp:DropDownList>
                                                            <input class="form-control input-sm" runat="server" id="txtPaisEmpresaProfessorAnt" type="text" value="" maxlength="50" style="display:block" readonly="readonly" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlEstadoEmpresaProfessor" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>

                                                                <div class="col-md-2 ">
                                                                    <span>Estado</span><br />
                                                                    <div runat="server" id ="divDDLEstadoEmpresaProfessor">
                                                                        <asp:DropDownList runat="server" ID="ddlEstadoEmpresaProfessor"  ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoEmpresaProfessor_SelectedIndexChanged"> <%--OnSelectedIndexChanged="ddlEstadoEmpresaProfessor_SelectedIndexChanged"--%>
                                                                        </asp:DropDownList>
                                                                    <input class="form-control input-sm" runat="server" id="txtEstadoEmpresaProfessorAnt" type="text" value="" maxlength="50" style="display:none" readonly="readonly" />
                                                                    </div>
                                                                    <div runat="server" id ="divTXTEstadoEmpresaProfessor">
                                                                        <input class="form-control input-sm" runat="server" id="txtEstadoEmpresaProfessor" type="text" value="" maxlength="2"/>
                                                                    </div>
                                                                </div>
                                                                <div class="hidden-lg hidden-md">
                                                                    <br />
                                                                </div>

                                                                <div class="col-md-4 ">
                                                                    <span>Cidade</span><br />
                                                                    <div runat="server" id ="divDDLCidadeEmpresaProfessor">
                                                                        <asp:DropDownList runat="server" ID="ddlCidadeEmpresaProfessor" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                                                        </asp:DropDownList>
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeEmpresaProfessorAnt" type="text" value="" maxlength="50" style="display:none" readonly="readonly"/>
                                                                    </div>
                                                                    <div runat="server" id ="divTXTCidadeEmpresaProfessor">
                                                                        <input class="form-control input-sm" runat="server" id="txtCidadeEmpresaProfessor" type="text" value="" maxlength="50"/>
                                                                    </div>
                                                                </div>

                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div> 
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-4 ">
                                                            <span>Cargo</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCargoProfessor" type="text" value="" maxlength="100"/>
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
                                                                <input class="form-control input-sm " runat="server" id="txtTelefoneEmpresaProfessor" type="text" value="" maxlength="15"  onkeypress="return ehNumeroOuTRaco(event)"/>
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        <div class="col-md-2 ">
                                                            <span>Ramal</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtRamalEmpresaProfessor" type="text" value="" maxlength="15"/>
                                                        </div>
                                                    </div>                                                  
                                                </div>
                                                <%--Dados Comerciais - Fim--%>

                                                <%--Formas de Recebimento - Início--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Formas de Recebimento</h5>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <span>Horas - aula</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlHorasAula" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                                <asp:ListItem Text="Não Recebe" Value="1" />
                                                                <asp:ListItem Text="CLT" Value="2"/>
                                                                <asp:ListItem Text="CPTI 1" Value="3" disabled="disabled" />
                                                                <asp:ListItem Text="CPTI 2" Value="4" disabled="disabled" />
                                                                <asp:ListItem Text="Pessoa Jurídica 1" Value="5" />
                                                                <asp:ListItem Text="Pessoa Jurídica 2" Value="12" />
                                                                <asp:ListItem Text="Pessoa Jurídica 3" Value="13" />
                                                                <asp:ListItem Text="Pessoa Jurídica 4" Value="14" />
                                                                <asp:ListItem Text="Recibo Técnico" Value="6" />
                                                                <asp:ListItem Text="Recibo-240 - (Fixo)" Value="7" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="divHorasPermitidas" runat="server" style="display:none">
                                                            <div class="hidden-lg hidden-md">
                                                                <br />
                                                            </div>

                                                            <div class="col-md-4">
                                                                <span>Horas Permitidas</span><br />
                                                                <input class="form-control input-sm" runat="server" id="txtHorasAulaCLT" type="number" value="0" maxlength="3"/>
                                                            </div>
                                                            <div class="hidden-lg hidden-md">
                                                                <br />
                                                            </div>

                                                            <div class="col-md-4 ">
                                                                <span>Pagamento Horas Adicionais</span><br />
                                                                <asp:DropDownList runat="server" ID="ddlHorasAulaAdicional" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false" >
                                                                    <asp:ListItem Text="Não Recebe" Value="1" />
                                                                    <asp:ListItem Text="CLT" Value="2" />
                                                                    <asp:ListItem Text="CPTI 1" Value="3" />
                                                                    <asp:ListItem Text="CPTI 2" Value="4" />
                                                                    <asp:ListItem Text="Pessoa Jurídica" Value="5" />
                                                                    <asp:ListItem Text="Recibo Técnico" Value="6" />
                                                                    <asp:ListItem Text="Recibo-240 - (Fixo)" Value="7" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class ="row">
                                                        <div class="col-md-4 ">
                                                            <span>Orientação</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlOrientacao" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false" >
                                                                <asp:ListItem Text="Não Recebe" Value="1" />
                                                                <asp:ListItem Text="CLT" Value="2" />
                                                                <asp:ListItem Text="CPTI 1" Value="3" />
                                                                <asp:ListItem Text="CPTI 2" Value="4" />
                                                                <asp:ListItem Text="Pessoa Jurídica" Value="5" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-4 ">
                                                            <span>Banca</span><br />
                                                            <asp:DropDownList runat="server" ID="ddlBanca" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                                <asp:ListItem Text="Não Recebe" Value="1" />
                                                                <asp:ListItem Text="Recibo Banca - São Paulo" Value="8" />
                                                                <asp:ListItem Text="Recibo Banca - SP Interior - Outros Estados" Value="9" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>                                                    
                                                </div>
                                                <%--Formas de Recebimento - Fim--%>

                                                <%--Dados da Empresa - Início--%>
                                                <div class="row well" id="divDadosEmpresa" runat="server" style="display:none">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <h5 class="box-title text-bold">Pessoa Jurídica Para Recebimento </h5>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                    
                                                        <div class="col-md-8">
                                                            <button type="button" id="btnSelecionaEmpresa2" class="btn btn-warning" href="#" onclick="fModalAssociarEmpresa()" > <%--onserverclick="btnNovoAluno_Click"--%>
                                                                <i class="fa fa-building"></i>&nbsp;Selecionar Empresa</button>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-5 ">
                                                            <span>Empresa</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtIdEmpresa" type="text" value="" style="display:none"/>
                                                            <input class="form-control input-sm" runat="server" id="txtNomeEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>CNPJ</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCNPJEmpresa" type="text" value=""  readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Inscrição Estadual</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtInscricaoEstadualEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Cargo</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCargoEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>CEP</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCEPEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-5 ">
                                                            <span>Logradouro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtLogradouroEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>
                                                        <div class="col-md-1 ">
                                                            <span>Número</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroEmpresa" type="number" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Complemento</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtComplementoEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Bairro</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtBairroEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                    </div>  
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-4 ">
                                                            <span>Cidade</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCidadeEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Estado</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtEstadoEmpresa" type="text" value="" readonly="true"/>
                                                        </div>
                                                    </div> 
                                                    <br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-3 ">
                                                            <span>Telefone</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-phone"></i>
                                                                </div>
                                                                <input class="form-control input-sm " runat="server" id="txtTelefoneEmpresa" type="text" value="" readonly="true"/>
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Celular</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-mobile"></i>
                                                                </div>
                                                                <input class="form-control input-sm " runat="server" id="txtCelularEmpresa" type="text" value="" readonly="true"/>
                                                            </div>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-3 ">
                                                            <span>Fax</span><br />
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-fax"></i>
                                                                </div>
                                                                <input class="form-control input-sm " runat="server" id="txtFaxEmpresa" type="text" value="" readonly="true"/>
                                                            </div>
                                                        </div>
                                                    </div>                                                    
                                                </div>
                                                <%--Dados da Empresa - Fim--%>

                                                <%--Observações - Início--%>
                                                <div class="row well">
                                                    <h5 class="box-title text-bold">Observações</h5>
                                                    <div class="row">
                                                        <div class="col-md-12 ">
                                                            <textarea style ="resize:vertical;font-size:14px" runat ="server" class="form-control input-sm" rows="5" id="txtObservacaoProfessor"></textarea>
                                                        </div>
                                                    </div>                                                    
                                                </div>
                                                <%--Observações - Fim--%>

                                            </div>
                                            <div class="box-footer">
                                                <div class="pull-right">
                                                    <div class="col-md-12">
                                                        <button type="button" runat="server" id="bntSalvarProfessor1" class="btn btn-success" href="#" onclick="if (fProcessando()) return;" onserverclick="bntSalvarProfessor_Click"> <%--onserverclick="btnNovoAluno_Click"--%>
                                                            <i class="fa fa-save"></i>&nbsp;Salvar Dados
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

<%--==========================================================================================================================================--%>

                                    <div class="tab-pane" id="tab_SituacaoAcademica">
                                        <%--                                        <b>How to use:</b>--%>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTurmaProfessor" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <input runat="server" id="txtIdTurma" type="text" value="" visible="false" />
                                                <div class="box box-primary">
                                                    <div class="box-header">
                                                        <h3 class="box-title">Situação Acadêmica</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="row well">
                                                            <h5 class="box-title text-bold">Turma</h5>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div runat="server" id="divTurmaDiversas">
                                                                        <div class="row">
                                                                            <div class="col-md-8">
                                                                                <span>Outras Turmas</span><br />

                                                                                <asp:DropDownList runat="server" ID="ddlTurmaProfessor" onChange = "fMostrarProgresso4();"  ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="true" > <%--OnSelectedIndexChanged="ddlTurmaProfessor_SelectedIndexChanged" --%>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                    </div>

                                                                    <div runat="server" id="divTurmaTem">
                                                                        <div class="row">
                                                                            <div class="col-md-2">
                                                                                <span>Código Turma</span><br />
                                                                                <input class="form-control input-sm" runat="server" id="txtCodTurmaProfessor" type="text" value="" readonly="readonly" />
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <span>Quadrimestre</span><br />
                                                                                <input class="form-control input-sm" runat="server" id="txtQuadrimestreProfessor" type="text" value="" readonly="readonly" />
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2 ">
                                                                                <span>Tipo Curso</span><br />
                                                                                <input class="form-control input-sm" runat="server" id="txtTipoCursoProfessor" type="text" value="" readonly="readonly" />
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2 ">
                                                                                <span>Data Inicio</span><br />
                                                                                <div class="input-group">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <input class="form-control input-sm" runat="server" id="txtDataInicioCursoProfessor" type="text" value="" readonly="readonly" />
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
                                                                                    <input class="form-control input-sm" runat="server" id="txtDataFimCursoProfessor" type="text" value="" readonly="readonly" />
                                                                                </div>
                                                                            </div>


                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-4 ">
                                                                                <span>Curso</span><br />
                                                                                <input class="form-control input-sm" runat="server" id="txtCursoProfessor" type="text" value="" readonly="readonly" />
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-4 ">
                                                                                <span>Área de Concentração</span><br />
                                                                                <input class="form-control input-sm" runat="server" id="txtAreaConcentracaoProfessor" type="text" value="" readonly="readonly" />
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-2 ">
                                                                                <span>Situação <small>(data fim)</small> </span><br />
                                                                                <input class="form-control input-sm" runat="server" id="txtSituacaoProfessor" type="text" value="" readonly="readonly" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div runat="server" id="divTurmaNaoTem">
                                                                        <div class="row">
                                                                            <div class="col-md-2 ">
                                                                                <div class="alert alert-warning">
                                                                                    <asp:Label runat="server" ID="lblMsgSemResultados" Text="Sem Turma associada" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <br />

                                                        </div>

                                                        <div class="row well">
                                                            <div class="nav-tabs-custom">

                                                                <ul class="nav nav-tabs">
                                                                    <li id="tabHistoricoProfessor" name="tabHistoricoProfessor" class="active "><a href="#tab_HistoricoProfessor" data-toggle="tab"><strong>Histório Escolar</strong></a></li>
                                                                    <li id="tabOrientacaoProfessor" name="tabOrientacaoProfessor" class="hidden"><a href="#tab_OrientacaoProfessor" data-toggle="tab"><strong>Orientação</strong></a></li>
                                                                </ul>

                                                                <div class="tab-content">
                                                                    <div class="tab-pane active" id="tab_HistoricoProfessor">

                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="grid-content">
                                                                                    <div runat="server" id="msgSemResultadosHistorico" visible="false">
                                                                                        <div class="alert alert-warning">
                                                                                            <asp:Label runat="server" ID="Label1" Text="Nenhum Histórico encontrado" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="table-responsive ">

                                                                                        <asp:GridView ID="grdHistoricoProfessor" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                            AllowPaging="True" PageSize="1000000" AllowSorting="true"
                                                                                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" Caption="RELAÇÃO DE DISCIPLINAS">
                                                                                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                            <Columns>
                                                                                                <%--<asp:TemplateField>
                                                                                                    <HeaderTemplate>
                                                                                                        <th colspan="10" align="center">RELAÇÃO DE DISCIPLINAS</th>

                                                                                                        <b><tr class="header" style="background:#507CD1; color:White"></b>
                                                                                                        <th style="width: 0px"></th>
                                                                                                            <th>Início</th>
                                                                                                            <th>Quadrimestre</th>
                                                                                                            <th>Disciplina</th>
                                                                                                            <th>Nome</th>
                                                                                                            <th>Duração</th>
                                                                                                            <th>Frequência</th>
                                                                                                            <th>Conceito</th>
                                                                                                            <th>Resultado</th>
                                                                                                            <th>Detalhe Disciplina</th>
                                                                                                            <th>Presença Disciplina</th>
                                                                                                        </tr>
                                                                                                    </HeaderTemplate> 
                                                                                                    <ItemTemplate>
                                                                                                        <td align="center" style="width: 40px"><%# Eval("Inicio") %></td>
                                                                                                        <td align="center"><%# Eval("Quadrimestre")%></td>
                                                                                                        <td align="center" style="white-space:nowrap;"><%# Eval("DisciplinaCodigo")%></td>
                                                                                                        <td align="left" ><%# Eval("DisciplinaNome")%></td>
                                                                                                        <td align="center" ><%# Eval("Duracao")%></td>
                                                                                                        <td align="center" ><%# Eval("Frequencia")%></td>
                                                                                                        <td align="center" ><%# Eval("Conceito")%></td>
                                                                                                        <td align="center" ><%# Eval("Resultado")%></td>
                                                                                                        <td align="center" ><a class="mao" title="Visualizar detalhes da Disciplina" onclick="fDetalheDisciplina(<%# DataBinder.Eval(Container.DataItem, "Oferecimento")%>)"><i class="fa fa-search-plus"></i></a></td>
                                                                                                        <td align="center" ><a class="mao" title="Visualizar lista de presença na Disciplina" onclick="fDetalhePresenca(<%# DataBinder.Eval(Container.DataItem, "Oferecimento")%>,<%# DataBinder.Eval(Container.DataItem, "Professor")%>)"><i class="fa fa-calendar-check-o"></i></a></td>
                                                                                                    </ItemTemplate>

                                                                                                </asp:TemplateField>  --%>

                                                                                                <asp:BoundField DataField="Inicio" HeaderText="Início" ItemStyle-HorizontalAlign="Center" />

                                                                                                <asp:BoundField DataField="Quadrimestre" HeaderText="Quadrimestre" ItemStyle-HorizontalAlign="Center" />

                                                                                                <asp:BoundField DataField="DisciplinaCodigo" HeaderText="Disciplina" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />

                                                                                                <asp:BoundField DataField="DisciplinaNome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                                <asp:BoundField DataField="Duracao" HeaderText="Duração" ItemStyle-HorizontalAlign="Center" />

                                                                                                <asp:BoundField DataField="Frequencia" HeaderText="Frequência" ItemStyle-HorizontalAlign="Center" />

                                                                                                <asp:BoundField DataField="Conceito" HeaderText="Conceito" ItemStyle-HorizontalAlign="Center" />

                                                                                                <asp:BoundField DataField="Resultado" HeaderText="Resultado" ItemStyle-HorizontalAlign="Center" />

                                                                                                <asp:TemplateField HeaderText="Detalhe Disciplina" ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <a class="mao btn btn-default btn-circle" title="Visualizar detalhes da Disciplina" onclick="fDetalheDisciplina(<%# DataBinder.Eval(Container.DataItem, "Oferecimento")%>)"><i class="fa fa-search-plus text-blue"></i></a>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>

                                                                                                <asp:TemplateField HeaderText="Presença Disciplina" ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <a class="mao btn btn-default btn-circle" title="Visualizar lista de presença na Disciplina" onclick="fDetalhePresenca(<%# DataBinder.Eval(Container.DataItem, "Oferecimento")%>,<%# DataBinder.Eval(Container.DataItem, "Professor")%>)"><i class="fa fa-calendar-check-o text-blue"></i></a>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>

                                                                                            </Columns>

                                                                                        </asp:GridView>
                                                                                       
                                                                                    </div>
                                                                                    <div id="divBotoesImpressaoHistorico" class="row">
                                                                                        <div class="col-md-3">
                                                                                            <%--<button runat ="server" id="btnImprimirHitorico" name="btnImprimirHitorico" class="btn btn-warning" onserverclick="btnImprimir_Click">
                                                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico</button>--%>

                                                                                            <button type="button" id="btnImprimirHitoricoOff" name="btnImprimirHitoricoOff" class="btn btn-warning" onclick="funcClicaImprimirHistorico()">
                                                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico</button>

                                                                                            

                                                                                        </div>
                                                                                        <div class="hidden-lg hidden-md">
                                                                                            <br />
                                                                                        </div>

                                                                                        <div class="col-md-4 ">
                                                                                            <button type="button" id="btnImprimirHitoricoOficialOff" name="btnImprimirHitoricoOficialOff" class="btn btn-success" onclick="funcModalImprimirHistoricoOficial()">
                                                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico Oficial</button>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                    </div>

                                                                    <div class="tab-pane" id="tab_OrientacaoProfessor">
                                                                        <div class="row well">
                                                                            <div class="col-md-12">
                                                                                <h5 class="box-title text-bold">Em Desenvolvimento</h5>

                                                                                <h5 class="box-title text-bold">Titulo da Orientação</h5>
                                                                                <div class="row">
                                                                                    <div class="col-md-12 ">
                                                                                        <textarea style ="resize:vertical;font-size:14px" runat ="server" class="form-control input-sm" rows="5" id="txtTituloOrientacao"></textarea>
                                                                                    </div>
                                                                                </div>                                                    
                                                                                
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        
                                                                        <div class="row well">
                                                                            <div class="col-md-12">
                                                                                <div class="row">
                                                                                    <div class="col-md-1">
                                                                                        <button type="button" id="btnOrientador" name="btnOrientador" class="btn btn-default" title="Adicionar Orientador" onclick="funcModalAdicionaOrientador()">
                                                                                                <i class="fa fa-plus-circle"></i>&nbsp;</button>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <h5 class="box-title text-bold">Orientador</h5>
                                                                                    </div>
                                                                                </div>
                                                                                
                                                                                <div class="row">
                                                                                    <div class="col-md-12 ">
                                                                                        <div class="row">
                                                                                            <div class="col-md-12 ">
                                                                                                <div class="grid-content">
                                                                                                    <div runat="server" id="divMensagemOrientador" visible="true">
                                                                                                        <div class="alert alert-warning">
                                                                                                            <asp:Label runat="server" ID="lblMensagemOrientador" Text="Nenhum Orientador encontrado" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="table-responsive ">
                                                                                                        <asp:GridView ID="grdOrientador" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                                            AllowPaging="True" PageSize="1000000" AllowSorting="true"
                                                                                                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="IDProfessor" HeaderText="IDProfessor" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  />

                                                                                                                <asp:BoundField DataField="CPF" HeaderText="CPF" ItemStyle-HorizontalAlign="Center" />

                                                                                                                <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Center" />

                                                                                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <a class="mao" title="Excluir Orientador" onclick="fExcluirOrientador(<%# DataBinder.Eval(Container.DataItem, "ApagaLinha")%>)"><i class="fa fa-eraser"></i></a>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>

                                                                                                            </Columns>

                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>   
                                                                                    </div>
                                                                                </div>                                                    
                                                                                
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        
                                                                        <div class="row well">
                                                                            <div class="col-md-12">
                                                                                <div class="row">
                                                                                    <div class="col-md-1">
                                                                                        <button type="button" id="btnCo_orientador" name="btnCo_orientador" class="btn btn-default" title="Adicionar Co-orientador" onclick="funcModalImprimirHistoricoOficial()">
                                                                                                <i class="fa fa-plus-circle"></i>&nbsp;</button>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <h5 class="box-title text-bold">Co-orientador(es)</h5>
                                                                                    </div>
                                                                                </div>
                                                                                
                                                                                <div class="row">
                                                                                    <div class="col-md-12 ">
                                                                                        <div class="row">
                                                                                            <div class="col-md-12 ">
                                                                                                <div class="grid-content">
                                                                                                    <div runat="server" id="divMensagemCo_orientador" visible="true">
                                                                                                        <div class="alert alert-warning">
                                                                                                            <asp:Label runat="server" ID="lblMensagemCo_orientador" Text="Nenhum Co-orientador encontrado" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="table-responsive ">
                                                                                                        <asp:GridView ID="grdCo_orientador" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                                            AllowPaging="True" PageSize="1000000" AllowSorting="true"
                                                                                                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="IDProfessor" HeaderText="IDProfessor" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  />

                                                                                                                <asp:BoundField DataField="CPF" HeaderText="CPF" ItemStyle-HorizontalAlign="Center" />

                                                                                                                <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Center" />

                                                                                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <a class="mao" title="Excluir Co-orientador" onclick="fExcluirCoorientador(<%# DataBinder.Eval(Container.DataItem, "ApagaLinha")%>)"><i class="fa fa-eraser"></i></a>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>

                                                                                                            </Columns>

                                                                                                        </asp:GridView>
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
                                                                            <div class="col-md-12">
                                                                                <button type="button" id="bntSalvarDadosOrientador" title="Salvar dados da Orientação" onclick="SalvarDadosFoto();" class="btn btn-success pull-right"> <%--document.getElementById('<%=bntSalvarDadosFoto.ClientID%>').click()--%>
                                                                                    <i class="glyphicon glyphicon-floppy-disk"></i>&nbsp;Salvar dados da Orientação
                                                                                </button>
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


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

                        <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onserverclick="btnVoltar_Click" > <%--onserverclick="btnVoltar_ServerClick"--%> <%--onclick="window.history.back()"--%>
                            <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>

                        <button type="button" runat="server" id="btnImprimirHitorico" name="btnImprimirHitorico" class="btn btn-warning hidden " href="#" onclick=""  > <%--onserverclick="btnImprimir_Click"--%>
                                                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico</button>

                        <button type ="button" runat="server" id="btnImprimirHitoricoOficial" name="btnImprimirHitorico" class="btn btn-success hidden" > <%--onserverclick="btnImprimirOficial_Click"--%>
                                                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico Oficial</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <div class="modal fade" id="divModalApagarOrientador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-danger">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title" ><i class ="fa fa-eraser"></i> Excluir Orientador</label></h4>
                        </div>
                        <div class="modal-body">
                            Deseja excluir o oriendador: <br /> <label id="lblOrientador"></label>?
                            <input class="hidden" runat="server" id="txtIdProfessorOrientador" type="text" value=""/>
                            <input class="hidden" runat="server" id="txtTurmaOrientador" type="text" value=""/>
                            <input class="hidden" runat="server" id="txtIdOrientador" type="text" value="" />
                            <input class="hidden" runat="server" id="txtIdOrientacao" type="text" value="" />
                        </div>
                        <div class="modal-footer">
                            <div class="pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    <i class="fa fa-close"></i>&nbsp;Fechar</button>
                            </div>

                            <div class="pull-right">
                                <button type="button" runat="server" id="btnApagarOrientador" name="btnApagarOrientador"  class="btn btn-primary" data-dismiss="modal" > <%--onserverclick="btnApagarOrientador_Click"--%>
                                    <i class="fa fa-check"></i>&nbsp;OK</button>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal fade" id="divModalSelecionarOrientador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title" ><i class ="fa fa-graduation-cap"></i> Adicionar/Alterar Orientador</label></h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <span><strong>Filtro</strong></span><br />
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-7">
                                    <span>Nome</span><br />
                                    <input class="form-control input-sm alteracao" runat="server" id="txtNomeOrientadorFiltro" type="text" value="" maxlength="150" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span>CPF</span><br />
                                    <input class="form-control input-sm alteracao" runat="server" id="txtCPFOrientadorFiltro" type="text" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 pull-right">
                                    <span> </span><br />
                                    <button type="button" runat="server" id="btnOkOrientadorFiltro" name="btnOkOrientadorFiltro"  class="btn btn-primary" > <%--onserverclick="btnApagarOrientador_Click"--%>
                                    <i class="fa fa-check"></i>&nbsp;OK</button>
                                </div>
                                <br />
                            </div>
                            <hr>

                            <div class="container-fluid text-center">
                                <div class="row text-center">
                                    <div class="col-md-12">
                                        <table  class="display table table-striped table-bordered table-condensed table-hover" id="tabOrientador" cellspacing="0" width="100%">
                                            <thead>
                                                <tr style="color:White;background-color:#507CD1;font-weight:bold;" >
                                                    <th class="hidden">IDProfessor</th>
                                                    <th>Nome</th>
                                                    <th>CPF</th>
                                                    <th>Selecionar</th>
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

        </ContentTemplate>
    </asp:UpdatePanel>

    <%--Aqui são os botões escondidos do FileUpLoad--%>

    <a id="adivModal" class="preto" data-toggle="modal" href="#divModal" style:"display: none;" ></a>

    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>

    <!-- Modal para Associar Empresa -->
    <div class="modal fade" id="divModalAssociarEmpresa" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Associar Empresa</h4>
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
                                    
                                        <div class="col-md-3">
                                            <span>CNPJ</span><br />
                                            <input class="form-control input-sm" id="txtCnpjFiltro" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtEmpresaFiltro" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button id="bntPerquisaEmpresa" type="button" name="bntPerquisaEmpresa" title="" class="btn btn-success" onclick="fPerquisaEmpresa()" >
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
                                            <div id="msgSemResultadosgrdEmpresaDisponivel" style="display:none">
                                                <div class="alert alert-warning">
                                                    <asp:Label runat="server" ID="Label2" Text="Nenhuma Empresa encontrada" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdEmpresaDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdEmpresaDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para Ativar/Inativar Professor -->
    <div class="modal fade" id="divModalAtivaInativa" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="divCabecAtiva" class="modal-header bg-danger">
                    <h4 class="modal-title"><label id="lblTituloAtiva"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <label id="lblCorpoAtiva"></label>
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarProfessor('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarProfessor('Inativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

     <!-- Modal para exibir foto do Professor -->
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
    <!-- Modal -->

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
                                    Você deve selecionar apenas imagens com extenção: "<b>jpg</b>" ou "<b>jpeg</b>".
                                </div>

                                <div id="divTamanho" class="col-lg-12" style:"display: none;" >
                                    Você deve selecionar imagens com tamanho máximo de <b>60 Kb</b>.
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
        <asp:FileUpload ID="fileArquivoParaGravar" runat="server" CssClass="btn btn-primary btn-file" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:imagePreview(this);" />
        <asp:Button ID="bntSalvarDadosFoto" runat="server" Text="Button" />
    </div>

 <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>
    
    <script>
        $('#<%=ddlHorasAula.ClientID%>').on("select2:select", function(e) { 
            //alert($(this).val());
            if ($(this).val() == 2) {
                //Inibido -- mas é assim que estava no SERPI
                //document.getElementById('<%=txtHorasAulaCLT.ClientID%>').value = 52;
                //$("#<%=ddlHorasAulaAdicional.ClientID%>").select2("val", "5");
                //Inibido -- mas é assim que estava no SERPI
                document.getElementById('<%=divHorasPermitidas.ClientID%>').style.display = 'block';
                document.getElementById('<%=divDadosEmpresa.ClientID%>').style.display = 'none';
                //alert($(this).val());
           }
            else if ($(this).val() == 5 || $(this).val() == 12 || $(this).val() == 13 || $(this).val() == 14) {
               document.getElementById('<%=divHorasPermitidas.ClientID%>').style.display = 'none';
               document.getElementById('<%=divDadosEmpresa.ClientID%>').style.display = 'block';
           }
            else {
                if ($("#<%=ddlOrientacao.ClientID%>").select2("val") != 5 && $("#<%=ddlHorasAulaAdicional.ClientID%>").select2("val") != 5)
                {
                    document.getElementById('<%=divDadosEmpresa.ClientID%>').style.display = 'none';
                }
               document.getElementById('<%=divHorasPermitidas.ClientID%>').style.display = 'none';
           }
        });

        $('#<%=ddlHorasAulaAdicional.ClientID%>').on("select2:select", function(e) { 
            //alert($(this).val());
            if ($(this).val() == 5) {
                document.getElementById('<%=divDadosEmpresa.ClientID%>').style.display = 'block';
            }
            else {
                if ($("#<%=ddlOrientacao.ClientID%>").select2("val") != 5 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 5 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 12 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 13 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 14)
                {
                    document.getElementById('<%=divDadosEmpresa.ClientID%>').style.display = 'none';
                }
           }
        });

        $('#<%=ddlOrientacao.ClientID%>').on("select2:select", function(e) { 
            //alert($(this).val());
            if ($(this).val() == 5) {
                document.getElementById('<%=divDadosEmpresa.ClientID%>').style.display = 'block';
            }
            else {
                if ($("#<%=ddlHorasAulaAdicional.ClientID%>").select2("val") != 5 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 5 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 12 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 13 && $("#<%=ddlHorasAula.ClientID%>").select2("val") != 14)
                {
                    document.getElementById('<%=divDadosEmpresa.ClientID%>').style.display = 'none';
                }
           }
        });

        
        //============================================================================

        $('#<%=txtCPFProfessor.ClientID%>').mask('999.999.999-99');
        $('#txtCnpjFiltro').mask('99.999.999/9999-99');
        $('#<%=txtCepResidenciaProfessor.ClientID%>').mask('99999-999');
        $('#<%=txtCEPEmpresaProfessor.ClientID%>').mask('99999-999');
        $('#<%=txtTelefoneProfessor.ClientID%>').mask('99-9999-9999');
        $('#<%=txtTelefoneEmpresaProfessor.ClientID%>').mask('99-9999-9999');
        $('#<%=txtCelularProfessor.ClientID%>').mask('99-99999-9999');
        $('#<%=txtCPFOrientadorFiltro.ClientID%>').mask('999.999.999-99');

        //============================================================================
        function fModalAssociarEmpresa() {
            document.getElementById("divgrdEmpresaDisponivel").style.display = "none";
            $('#divModalAssociarEmpresa').modal();
        }

        $("#<%=optCPF.ClientID%>").on('ifChecked', function (e) {
            fSetMaskaraCPF();

        });
        $("#<%=optPassaporte.ClientID%>").on('ifChecked', function (e) {
            fUnsetMaskaraCPF();

            });
        //============================================================================
        <%--function fAlteraCPF_Passaporte() {
            if (document.getElementById("<%=hCPF_Passaporte.ClientID%>").value == "CPF")
            {
                document.getElementById("<%=lblCPF_Passaporte.ClientID%>").innerHTML = "Passaporte";
                document.getElementById("<%=hCPF_Passaporte.ClientID%>").value = "Passaporte";
                $('#<%=iCPF_Passaporte.ClientID%>').removeClass('fa-toggle-on');
                $('#<%=iCPF_Passaporte.ClientID%>').addClass('fa-toggle-off');
                $('#<%=txtCPFProfessor.ClientID%>').unmask();
            }
            else {
                document.getElementById("<%=lblCPF_Passaporte.ClientID%>").innerHTML = "CPF";
                document.getElementById("<%=hCPF_Passaporte.ClientID%>").value = "CPF";
                $('#<%=iCPF_Passaporte.ClientID%>').removeClass('fa-toggle-off');
                $('#<%=iCPF_Passaporte.ClientID%>').addClass('fa-toggle-on');
                $('#<%=txtCPFProfessor.ClientID%>').mask('999.999.999-99');
            }
        }--%>

        //============================================================================

        //============================================================================
        function fSetMaskaraCPF() {
            $('#<%=txtCPFProfessor.ClientID%>').mask('999.999.999-99');
        }

        //============================================================================

        function fUnsetMaskaraCPF() {
            $('#<%=txtCPFProfessor.ClientID%>').unmask();
        }

        //============================================================================

        function fModalAtivaInativa(qOperacao) {
            if (qOperacao == 'Ativa') {
                $("#divCabecAtiva").removeClass("bg-danger");
                $('#divCabecAtiva').addClass('bg-info');
                document.getElementById("btnConfirmaAtivar").style.display = 'block';
                document.getElementById("btnConfirmaInativar").style.display = 'none';

                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Professor';
                if ($('#<%=ddlSexoProfessor.ClientID%>').val() == "m") {
                    document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar o professor <strong>' + document.getElementById("<%=txtNomeProfessor.ClientID%>").value + '</strong>?' ;
                }
                else {
                    document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar a professora <strong>' + document.getElementById("<%=txtNomeProfessor.ClientID%>").value + '</strong>?' ;
                }
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';

                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Professor';
                if ($('#<%=ddlSexoProfessor.ClientID%>').val() == "m") {
                    document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar o professor <strong>' + document.getElementById("<%=txtNomeProfessor.ClientID%>").value + '</strong>?' ;
                }
                else {
                    document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar a professora <strong>' + document.getElementById("<%=txtNomeProfessor.ClientID%>").value + '</strong>?' ;
                }
            }
            
            $('#divModalAtivaInativa').modal();
        }


        //============================================================================
        function fPerquisaEmpresa() {
            //alert('oi');
            var qCNPJ = document.getElementById('txtCnpjFiltro').value;
            var qNome = document.getElementById('txtEmpresaFiltro').value;
            var dt = $('#grdEmpresaDisponivel').DataTable({
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
                        document.getElementById("divgrdEmpresaDisponivel").style.display = "none";
                        document.getElementById("msgSemResultadosgrdEmpresaDisponivel").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdEmpresaDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdEmpresaDisponivel").style.display = "block";
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
                            document.getElementById("divgrdEmpresaDisponivel").style.display = "block";
                            document.getElementById("msgSemResultadosgrdEmpresaDisponivel").style.display = "none";

                            var table_grdEmpresaDisponivel = $('#grdEmpresaDisponivel').DataTable();

                            $('#grdEmpresaDisponivel').on("click", "tr", function () {
                                vRowIndex_grdEmpresaDisponivel = table_grdEmpresaDisponivel.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPerquisaEmpresa?qCNPJ=" + qCNPJ + "&qNome=" + qNome,
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
                        "data": "P2", "title": "CNPJ", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Associar", "orderable": false, "className": "text-center"
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
        }

        //=======================================

        function fAssociarEmpresa(qIdEmpresa, qNomeEmpresa, qCNPJEmpresa){

            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAssociarEmpresa",
                contentType: 'application/json; charset=utf-8',
                //data: "{idOferecimento:'" + 'SP' + "', n:'" + '5' + "'}",
                data: "{qIdEmpresa:'" + qIdEmpresa + "'}",
                dataType: 'json',
                success: function (data, status) {
                    //alert('sucesso');
                    //Tratando o retorno com parseJSON
                    var itens = $.parseJSON(data.d);
                    //alert(itens[0].NomeEmpresa);
                    document.getElementById('<%=txtIdEmpresa.ClientID%>').value = itens[0].IdEmpresa;
                    document.getElementById('<%=txtNomeEmpresa.ClientID%>').value = itens[0].NomeEmpresa;
                    document.getElementById('<%=txtCNPJEmpresa.ClientID%>').value = itens[0].CNPJEmpresa;
                    document.getElementById('<%=txtInscricaoEstadualEmpresa.ClientID%>').value = itens[0].IEEmpresa;
                    document.getElementById('<%=txtCargoEmpresa.ClientID%>').value = itens[0].CargoEmpresa;
                    document.getElementById('<%=txtCEPEmpresa.ClientID%>').value = itens[0].CEPEmpresa;
                    document.getElementById('<%=txtLogradouroEmpresa.ClientID%>').value = itens[0].LogradouroEmpresa;
                    document.getElementById('<%=txtNumeroEmpresa.ClientID%>').value = itens[0].NumeroEmpresa;
                    document.getElementById('<%=txtComplementoEmpresa.ClientID%>').value = itens[0].ComplementoEmpresa;
                    document.getElementById('<%=txtBairroEmpresa.ClientID%>').value = itens[0].BairroEmpresa;
                    document.getElementById('<%=txtCidadeEmpresa.ClientID%>').value = itens[0].CidadeEmpresa;
                    document.getElementById('<%=txtEstadoEmpresa.ClientID%>').value = itens[0].EstadoEmpresa;
                    document.getElementById('<%=txtTelefoneEmpresa.ClientID%>').value = itens[0].TelefoneEmpresa;
                    document.getElementById('<%=txtCelularEmpresa.ClientID%>').value = itens[0].CelularEmpresa;
                    document.getElementById('<%=txtFaxEmpresa.ClientID%>').value = itens[0].FaxEmpresa;
                    $('#divModalAssociarEmpresa').modal('hide')

                },
                error: function (xmlHttpRequest, status, err) {
                    //alert('erro');
                    document.getElementById('lblErroCabecalho').innerHTML = 'Erro para associar a Empresa';
                    document.getElementById('lblErroCorpo').innerHTML = 'Erro para associar a Empresa <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;

                    $('#divMensagemModal').modal('show');
                }
            });

                
        }

        //=======================================

        function fAtivarInativarProfessor(qOperacao){
            //alert(qOperacao);
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAtivarInativarProfessor",
                contentType: 'application/json; charset=utf-8',
                data: "{qOperacao:'" + qOperacao + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var vTitulo = '';
                    var vBg = '';
                    var vIcon = '';

                    if (qOperacao == "Ativar") {
                        vTitulo = "Professor Ativado com sucesso";
                        vBg = "info";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    else {
                        vTitulo = "Professor Inativado com sucesso"
                        vBg = "danger";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    //alert('sucesso');
                    //Tratando o retorno com parseJSON
                    var json = $.parseJSON(data.d);
                    //alert(itens[0].NomeEmpresa);
                    if (json[0].Retorno == 'ok') {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<br /><br /><strong>Atenção! </strong><br /><br />',
                            message: vTitulo,
                        },{
                            type: vBg,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });

                        if (qOperacao == "Ativar") {
                            document.getElementById('<%=btnAtivar.ClientID%>').style.display='none';
                            document.getElementById('<%=btnInativar.ClientID%>').style.display='block';
                            document.getElementById('<%=lblInativado.ClientID%>').style.display='none';
                        }
                        else {
                            document.getElementById('<%=btnAtivar.ClientID%>').style.display='block';
                            document.getElementById('<%=btnInativar.ClientID%>').style.display='none';
                            document.getElementById('<%=lblInativado.ClientID%>').style.display='block';
                        }

                        
                        document.getElementById('<%=imgFotoOriginal.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
                        $('#divModalAlteraDadosFoto').modal('hide');
                    }
                    else if (json[0].Retorno == "deslogado") {
                        window.location.href = "index.html";
                    }
                    else {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Atenção! </strong><br /><br />',
                            message: 'Houve um problema na Ativação/Inativação do professor.<br />' + json[0].Resposta,
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
                    $('#divModalAtivaInativa').modal('hide')

                },
                error: function (xmlHttpRequest, status, err) {
                    if (qOperacao == "Ativar") {
                        document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro para Ativar Professor';
                        document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro para ativar o professor <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;
                    }
                    else {
                        document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro para Inativar Professor';
                        document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro para Inativar o professor <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;
                    }
                    
                    $('#divModalAtivaInativa').modal('hide')
                    $('#divMensagemModal').modal('show');
                }
            });

                
        }

        //=======================================

        function fMostrarProgresso4()
        {
            document.getElementById('<%=UpdateProgress4.ClientID%>').style.display = "block";
        }

        function funcModalAdicionaOrientador() {
            $('#divModalSelecionarOrientador').modal('show');  
        }

        function fExcluirOrientador(qIdOrientacao, qIdProfessor, qTurmaOrientador,qNomeProfessor,qIdProfessor,qPapel) {
            document.getElementById('<%=txtIdOrientador.ClientID%>').value = qIdProfessor;
            document.getElementById('<%=txtIdProfessorOrientador.ClientID%>').value = qIdProfessor;
            document.getElementById('<%=txtTurmaOrientador.ClientID%>').value = qTurmaOrientador;
            document.getElementById('<%=txtIdOrientacao.ClientID%>').value = qIdOrientacao;
            document.getElementById('lblOrientador').innerHTML = qNomeProfessor;
            $('#divModalApagarOrientador').modal('show'); 
        }

        function fConfirmacaoExcluirOrientador(){
            $("tabDadosProfessor").removeClass("active");
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
            
            document.getElementById("tabHistoricoProfessor").classList.remove("active");
            document.getElementById("tabOrientacaoProfessor").classList.add("active");
            
            document.getElementById("tab_HistoricoProfessor").classList.remove("active");
            document.getElementById("tab_OrientacaoProfessor").classList.add("active");

        }       


        function funcApagaBotoesHistorico() {
            document.getElementById('btnImprimirHitoricoOff').style.display = 'none'; 
            document.getElementById('btnImprimirHitoricoOficialOff').style.display = 'none'; 
        }

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

        function SalvarDadosFoto() {
            //var fileData = new FormData();  

            var fileUpload = $("#<%=fileArquivoParaGravar.ClientID%>").get(0);  
            var files = fileUpload.files;  

            // Looping over all files and add it to FormData object  
            //for (var i = 0; i < files.length; i++) {  
            //    fileData.append("UploadedFile", files[i]);  
            //    //fileData.append(files[i].name, files[i]);  
            //}  

            // Adding one more key to FormData object  
            //fileData.append('DescricaoArquivo', document.getElementById("<%//=txtMatriculaProfessor.ClientID%>").value);

            //alert('oi');
            //var filesSelected = document.getElementById("#<%=fileArquivoParaGravar.ClientID%>").files;
            //alert('oi2');

            var file = files[0];
            //alert('oi3');
            var reader = new FileReader();
            reader.onload = function() {
                //alert('oi7');
                //alert(reader.result);
                //var comp = reader.result;
                //alert(comp);
                //alert(comp.length);
                //var tamanho = 70000;

               
                //var numChunks = Math.ceil(comp.length / tamanho),
                //        chunks = new Array(numChunks);

                //for(var i = 0, o = 0; i < numChunks; ++i, o += tamanho) {
                //    chunks[i] = comp.substr(o, tamanho);
                //    }

                //    //return chunks;

                //alert(chunks[0]);
                //alert(chunks[1]);



                $.ajax({
                    type: "post",
                    url: "wsSapiens.asmx/fAlteraFotoProfessor",
                    contentType: 'application/json; charset=utf-8',
                    //contentType: false,
                    processData: false, // Not to process data  
                    //data: fileData, 
                    data: "{iFoto:'" + reader.result + "'}",
                    dataType: 'json',
                    async: false,
                    success: function (data, status) {
                        //alert('oi sucesso');
                        //alert(data);
                        //Tratando o retorno com parseJSON
                        var itens = $.parseJSON(data.d);
                        //alert('oi sucesso');
                        //alert(itens[0].Retorno);
                        if (itens[0].Retorno == 'ok') {
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
                            document.getElementById('<%=imgProfessor.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
                                document.getElementById('<%=imgFotoOriginal.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
                            LimparArquivo();
                            $('#divModalAlteraDadosFoto').modal('hide');
                        }
                        else {
                            $.notify({
                                icon: 'fa fa-check',
                                title: '<strong>Atenção! </strong><br /><br />',
                                message: 'Houve um problema na alteração da foto.<br />' + itens[0].Retorno,
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
                            //document.getElementById('<%=imgProfessor.ClientID%>').src=document.getElementById('<%=imgprw.ClientID%>').src;
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
            reader.readAsDataURL(file);

        }

        function LimparArquivo() {
            document.getElementById('<%=imgprw.ClientID%>').src='img/pessoas/avatarunissex.jpg';
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
            if (vFileExt.toUpperCase() == "JPEG" || vFileExt.toUpperCase() == "JPG") {

                //if (input.files[0].size < 1024001) {
                if (input.files[0].size < 62000) {

                    document.getElementById('divBotoes').style.display = 'block';
                    document.getElementById('divBotaoSalvar').style.display = 'block';

                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgprw.ClientID%>').attr('src', e.target.result);
                        }

                        reader.readAsDataURL(input.files[0]);
                        }

                        $("#fileArquivoParaGravar").change(function () {
                            imagePreview(this);
                        });
                        document.getElementById('divImgPrw').style.display = 'block';
                        document.getElementById('divMensagens').style.display = 'none';
                        document.getElementById('divBntLocalizar').style.display = 'none';
                        document.getElementById('hEscolheuFoto').value = 'true';

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

            function fDetalhePresenca(qDisciplina, qProfessor){
                $.ajax({
                    type: "post",
                    url: "wsSapiens.asmx/ListaPresenca",
                    contentType: 'application/json; charset=utf-8',
                    //data: "{idOferecimento:'" + 'SP' + "', n:'" + '5' + "'}",
                    data: "{idOferecimento:'" + qDisciplina + "', idProfessor:'" + qProfessor + "'}",
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
                        document.getElementById('lblErroCorpo').innerHTML = 'Erro para exibir dados de Presença do Professor<br/> Erro: ' + err + '<br/>Status do erro: ' + status ;

                        $('#divModalErro').modal('show');
                    }
                });
                
            }


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

            function ehNumeroOuTRaco(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && charCode != 45 && charCode != 46 && charCode != 40 && charCode != 41 && charCode != 32 &&  (charCode < 48 || charCode > 57)) {
                    return false; //40 = "("; 41 = ")";  32 = " ";  
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
           <%-- $('#<%=ddlTurmaProfessor.ClientID%>').on('change',function()
            {alert('oi2')});--%>

                //alert("document.ready");
                fPaisEmpresa();
                
                //fSelect2()

                

                $('input').iCheck({
                    checkboxClass: 'icheckbox_minimal-blue',
                    radioClass: 'iradio_minimal-blue',
                    increaseArea: '20%' // optional
                });

                
                $("#frmMaster").validate({
                    debug:false,
                    rules: {
                        <%=txtNomeProfessor.UniqueID%>: "required",
                        <%=txtCPFProfessor.UniqueID%>: "required",
                        <%=txtEmail1Professor.UniqueID%>: "required",
                    },
                    messages: {
                        <%=txtNomeProfessor.UniqueID%>: "Digite o nome do Professor",
                        <%=txtCPFProfessor.UniqueID%>: "Digite o CPF do Professor",
                        <%=txtEmail1Professor.UniqueID%>: "Digite o Email do Professor",
                    }
                
                });

            <%--$("#frmMaster").submit(function () {
                alert("entrou na função");
                var teste = "[{nome:" + document.getElementById('<%=txtNomeProfessor.ClientID%>').value + "}]";
                var arForm = $("#frmMaster").serializeArray();
                $.ajax({

                    type: "post",
                    url: "cadProfessorGestao.aspx/SalvaRegistro",
                    contentType: 'application/json; charset=utf-8',
                    data: "{formVars:'" + teste + "'}",
                    //data: { skin: 'hold-transition ' + qTema + ' sidebar-mini' },
                    dataType: 'json',
                    async: true,
                    success: function (data, status) {
                        //Tratando o retorno com parseJSON
                        var itens = $.parseJSON(data.d);
                        //alert('1ok');
                        //alert(itens[0].Retorno);
                    },
                    error: function (msg) {

                        alert('Error calling AddBook: ' + msg);
                    }
                });
                return false;
            });--%>
            
            });

            
        //function FechaModalDadosFoto() {
        //    document.getElementById('divModalAlteraDadosFoto').style.display = 'none';
        //}

        function AbreMensagem(qClass) {
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        function funcClicaImprimirHistorico() {

            //alert("Hello world");
            document.getElementById('<%=btnImprimirHitorico.ClientID%>').click();



            //$.ajax({
            //    type: 'POST',
            //    url: 'cadProfessorGestao.aspx',  //URL solicitada
            //    data: { funcImprimeHistorico: 'sim' },
            //    success: function (data) { //O HTML é retornado em 'data'
            //        alert(data); //Se sucesso um alert com o conteúdo retornado pela URL solicitada será exibido.
            //    },
            //    error: function (xmlHttpRequest, status, err) {
            //        document.getElementById('lblErroCabecalho').innerHTML = 'Erro';
            //        document.getElementById('lblErroCorpo').innerHTML = 'Erro na rotina de impressão do Histório Escolar <br/> <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;

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
            document.getElementById('<%=btnImprimirHitoricoOficial.ClientID%>').click();
        }

        function fPaisEmpresa() {
            //alert("fPaisEmpresa");
            var display = document.getElementById('<%=ddlPaisEmpresaProfessor.ClientID%>').value;
            if(display == "Brasil")
            {
                document.getElementById('<%=divDDLEstadoEmpresaProfessor.ClientID%>').style.display = 'block';
                document.getElementById('<%=divDDLCidadeEmpresaProfessor.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTEstadoEmpresaProfessor.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTCidadeEmpresaProfessor.ClientID%>').style.display = 'none';
                fSelect2();
            }   
            else
            { 
                document.getElementById('<%=divDDLEstadoEmpresaProfessor.ClientID%>').style.display = 'none';
                document.getElementById('<%=divDDLCidadeEmpresaProfessor.ClientID%>').style.display = 'none';
                document.getElementById('<%=divTXTEstadoEmpresaProfessor.ClientID%>').style.display = 'block';
                document.getElementById('<%=divTXTCidadeEmpresaProfessor.ClientID%>').style.display = 'block';
                fSelect2();
            }
        }

        function fExibeImagem() {
            //$('#imagepreview').attr('src', "..\\img\\pessoas\\" + qId); // here asign the image to the modal when the user click the enlarge link
            document.getElementById('imagepreview').src = document.getElementById('<%=imgProfessor.ClientID%>').src;
            document.getElementById('labelNomeExibeImagem').innerHTML = document.getElementById('<%=lblTituloNomeProfessor.ClientID%>').innerHTML;
            $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
        }

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

    </script>
</asp:Content>
