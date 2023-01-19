<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadCursoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadCursoGestao" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li4Cursos" />
    <input type="hidden" id ="hCodigotxtDescicaoHomePage"  name="hCodigotxtDescicaoHomePage" value="value" />
    <input type="hidden" id ="hCodigotxtCorpoDocente"  name="hCodigotxtCorpoDocente" value="value" />

    <script src="Scripts/jquery.mask.min.js"></script>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
    
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="value" />
    <%--<input type="hidden" id ="hTituloPagina"  name="hTituloPagina" value="Aluno (Listagem)" />--%>

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/minimal/blue.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>

    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet" />

    <!-- summernote -->
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.6.10/summernote.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.6.10/summernote.min.js"></script>--%>

    <link href="https://cdn.jsdelivr.net/npm/summernote@0.8.15/dist/summernote.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/summernote@0.8.15/dist/summernote.min.js"></script>
    <script src="Scripts/summernote-image-attributes.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.11/lang/summernote-pt-BR.js"></script>


    
    
      
    <style type="text/css">
        /*td.details-control {
        background: url('../resources/details_open.png') no-repeat center center;
        cursor: pointer;
        }
        tr.shown td.details-control {
            background: url('../resources/details_close.png') no-repeat center center;
        }*/

        .bannerRosto_interno {
            background: url('img/homepage/cursos/mestrado.jpg');
            background-repeat: no-repeat;
            background-position: right -190px;
            overflow: hidden;
            background-size: cover;
            height: 30vh;
            /*background-attachment: fixed;*/
            animation-name:img-ani;
            animation-duration: 1s;   
            animation-timing-function: ease-in;  
        }

        .select2 {
            width: 100% !important;
        }

        label.opt {
            cursor: pointer;
        }

        caption {
            color: white;
            background-color: #507CD1;
            font-weight: bold;
            text-align: center;
        }

        .movedown {
            position:absolute;
            opacity:0;
            top:0;
            left:0;
            width:100%;
            height:100%;
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

            input[type=checkbox] {
                transform: scale(0.8);
            }

            input[type=checkbox] {
                width: 20px;
                height: 20px;
                margin-right: 8px;
                cursor: pointer;
                font-size: 18px; /*Tamanho do check interno*/
                visibility: hidden;
                margin-top:-12px;
            }

            input[type=checkbox]:hover:after {
                border-color: #0E76A8;
            }

            input[type=checkbox]:after {
                content: " ";
                background-color: #fff;
                display: inline-block;
                margin-left: 10px;
                padding-bottom: 5px;
                color: #0E76A8;
                width: 22px;
                height: 25px;
                visibility: visible;
                border: 1px solid #D2D6DE;
                padding-left: 3px;
                
                /*border-radius: 5px;*/
            }

            input[type=checkbox]:checked:after {
                content: "\2714";
                padding: -5px;
                font-weight: bold;
            }

            .nicEdit-main {
            overflow: auto !important;
            height: 5.5em;
        }

    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".icon-input-btn").each(function () {
                var btnFont = $(this).find(".btn").css("font-size");
                var btnColor = $(this).find(".btn").css("color");
                $(this).find(".glyphicon").css("font-size", btnFont);
                $(this).find(".glyphicon").css("color", btnColor);
                if ($(this).find(".btn-xs").length) {
                    $(this).find(".glyphicon").css("top", "24%");
                }
            });
        });
    </script>
    <asp:UpdateProgress ID="UpdateProgress2" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel2"  >
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

    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
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

    <div class="row"> 
        <div class="col-md-6">
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Curso</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label><asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            <button type="button" runat="server" id="btnInativar" class="btn btn-danger" href="#" onclick="fModalAtivaInativa('Inativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-off"></i> Inativar Curso
            </button>
            <button type="button" runat="server" id="btnAtivar" class="btn btn-info" href="#" onclick="fModalAtivaInativa('Ativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-on"></i> Ativar Curso
            </button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button"  runat="server" id="btnCriarCurso" name="btnCriarCurso" onserverclick="btnCriarCurso_Click" class="btn btn-primary" href="#" onclick=""  > <%--onserverclick="btnCriarCurso_Click"--%>
                    <i class="fa fa-magic"></i>&nbsp;Criar Curso</button>

        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button" runat="server" id="bntSalvar2" name="bntSalvar2" class="btn btn-success pull-right hidden" href="#" onclick="if (ShowProgress()) return;" onserverclick="btnSalvar_ServerClick1">
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
        </div>

    </div>
    <br />

    <div class="container-fluid">
        <div class="tab-content">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div runat="server" id="divEdicao">
                                <div class="row">
                                    <div class="col-md-2 ">
                                        <span>Data de Cadastro</span><br />
                                        <input class="form-control input-sm" runat="server" id="txtDataCadastro" type="text" readonly="true"/>
                                    </div>
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>

                                    <div class="col-md-2 ">
                                        <span>Status</span><br />
                                        <input class="form-control input-sm" runat="server" id="txtStatus" type="text" readonly="true"/>
                                    </div>
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>

                                    <div class="col-md-2 ">
                                        <span>Última Alteração</span><br />
                                        <input class="form-control input-sm" runat="server" id="txtDataAlteracao" type="text" readonly="true"/>
                                    </div>
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>

                                    <div class="col-md-3 ">
                                        <span>Responsável</span><br />
                                        <input class="form-control input-sm" runat="server" id="txtResponsavel" type="text" readonly="true"/>
                                    </div>

                                </div>
                                <br />
                            </div>

                            <div class="row">
                                <div class="col-md-2 ">
                                    <span>Código </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCodigoCurso" type="text" value="" maxlength="10"/>
                                    <input class="form-control input-sm hidden" runat="server" id="txtIdCurso" type="text" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-5 ">
                                    <span>Nome </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNomeCurso" type="text" value="" maxlength="350"/>
                                </div>

                                <div id="divNomeEnglish" runat="server" class="col-md-5 ">
                                    <div class="hidden-lg hidden-md hidden">
                                        <br />
                                    </div>
                                    <span>Nome (in english)</span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNomeCurso_en" type="text" value="" maxlength="350" />
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-3 ">
                                    <span>Tipo do Curso </span><span style="color:red;">*</span><br />
                                    <asp:DropDownList runat="server" ID="ddlTipoCurso" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Carga Horária </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCargaHorariaCurso" type="number" value="" min="1" max="999"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Créditos </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCreditosCurso" type="number" value="" min="1" max="99"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>N.º Máx Disciplinas</span><span id="spanAsterisco_txtNumeroMaxDisciplinaCurso" runat="server" style="color:red;display:none">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNumeroMaxDisciplinaCurso" type="number" value="" min="1" max="99"/>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-2 ">
                                    <span>Portaria MEC </span><span id="spanAsterisco_txtPortatiaMEC_Curso" runat="server" style="color:red;display:none">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtPortatiaMEC_Curso" type="number" value="" min="1" max="99999999"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Data Portaria MEC </span><span id="spanAsterisco_txtDataPortatiaMEC_Curso" runat="server" style="color:red;display:none">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDataPortatiaMEC_Curso" type="date" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Data Diário Oficial </span><span id="spanAsterisco_txtDataDiarioOficialCurso" runat="server" style="color:red;display:none">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDataDiarioOficialCurso" type="date" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Conceito na CAPES </span><span id="spanAsterisco_txtConceitoCapesCurso" runat="server" style="color:red;display:none">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtConceitoCapesCurso" type="number" value="" min="1" max="99"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Número na CAPES </span><span id="spanAsterisco_txtNumeroCapesCurso" runat="server" style="color:red;display:none">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNumeroCapesCurso" type="text" value="" maxlength="50"/>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-10">
                                    <span>Observações </span><br />
                                    <textarea style ="resize:vertical" runat ="server" class="form-control input-sm" rows="2" id="txtObservacaoCurso" maxlength="150"></textarea>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <button type="button" class="btn btn-success pull-right" onclick="fSalvarDados()">
                                        <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados
                                    </button>
                                </div>

                            </div>

                            <br />

                            <div class="row" runat="server" id="divQRCode" style="display:block">
                                                                                                                <div class="col-md-3">
                                                                                                                    <span>QR Code</span><br />
                                                                                                                    <img id="imageQRCode" class="img-responsive text-center center-block" src="img/Homepage/tipocursos/Gerando QRCode.png"/><br />
                                                                                                                    <button type="button" class="btn btn-purple text-center center-block" onclick="fForceDownload()" >
                                                                                                                            <i class="fa fa-download"></i>&nbsp;Download QR Code
                                                                                                                    </button>

                                                                                                                </div>
                                                                                                                <div class="hidden-lg hidden-md">
                                                                                                                    <br />
                                                                                                                </div>
                                                                                                                <div class="col-md-3">
                                                                                                                    <span>Link para o Tipo de Curso</span><br />
                                                                                                                    <input class="form-control input-sm" runat="server" id="txtEnderecoQRCode" type="text" readonly="true" style="display:block"/>
                                    
                                                                                                                </div>
                                                                                                                <div class="hidden-lg hidden-md">
                                                                                                                    <br />
                                                                                                                </div>
                                                                                                                <div class="col-md-3">
                                                                                                                    <br />
                                                                                                                    <button type="button" id="btnCopyURL" class="btn btn-default" onclick="fCopyUrl()">
                                                                                                                            <i class="fa fa-copy"></i>&nbsp;Copiar Link
                                                                                                                    </button>
                                                                                                                </div>

                                                                                                            </div>

                            <br />

                            <div class="box box-solid">
                                <!-- /.box-header -->
                                <div class="box-body">
                                  <div class="box-group" id="accordion">
                                    <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                                    <div class="panel box box-primary">
                                      <div class="box-header with-border">
                                        <h4 class="box-title">
                                          <a data-toggle="collapse" data-parent="#accordion" href="#tabHomePage" aria-expanded="false" class="collapsed">
                                               <i class="fa fa-globe fa-lg"></i> HomePage
                                          </a>
                                        </h4>
                                      </div>
                                      <div id="tabHomePage" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                          <div class="nav-tabs-custom">
                                              <ul class="nav nav-tabs">
                                                  <li id="tabPaginaPreview" class="active"><a type="button" href="#tab_PaginaPreview" id="atab_PaginaPreview" data-toggle="tab"><strong>Página <em>Preview</em></strong></a></li>
                                                  <li id="tabPaginaAprovada" class="" runat="server"><a type="button" href="#tab_PaginaAprovada" id="atab_PaginaAprovada" data-toggle="tab"><strong>Página Aprovada</strong></a></li>
                                                  <%--                    <li id="HomePage" class="" runat="server"><a href="#tab_HomePageCursoTurma" id="atab_HomePageCursoTurma"  data-toggle="tab"><strong>HomePage do Curso</strong></a></li>--%>
                                              </ul>

                                              <br />

                                              <div class="tab-content">
                                                  <div class="tab-pane active" id="tab_PaginaPreview">
                                                      <%--                                        <b>How to use:</b>--%>
                                                      <div class="box box-primary">
                                                          <div class="box-header hidden">
                                                              <div class="row">
                                                                  <div class="col-md-6">
                                                                      <h3 class="box-title">Página <em>Preview</em></h3>
                                                                  </div>
                                                              </div>
                                                          </div>

                                                          <div class="box-body">
                                                              <!-- Sessão Coordenador -->
                                                              <div class="tab-content">
                                                                  <div class="panel panel-default">
                                                                      <div class="panel-body">
                                                                          <div class="row">
                                                                              <div class="col-md-12">
                                                                                  <h5 class="box-title text-bold">Página <em>Preview</em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label id="lblSituacaoPaginaPreview" runat="server"></label></h5>
                                                                                  <div class="row">
                                                                                      <div class="col-md-12 ">
                                                                                          <div class="box-body">
                                                                                              <div class="tab-content">

                                                                                                  <div class="panel panel-default">
                                                                                                      <div class="panel-body">

                                                                                                          <div class="row">
                                                                                                              <div class="col-md-5">
                                                                                                                  <span>Mostrar na HomePage</span> <label id="lblAlteradoMostrarHome" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                                  <div class="row center-block btn-default form-group">
                                                                                                                      <div class="col-md-4">
                                                                                                                          <asp:RadioButton GroupName="GrupoStatusHomePage" ID="optStatusHomePageSim" runat="server" />
                                                                                                                          &nbsp;
                                                                                                                            <label class="opt" for="<%=optStatusHomePageSim.ClientID %>">Sim</label>
                                                                                                                      </div>

                                                                                                                      <div class="col-md-4">
                                                                                                                          <asp:RadioButton GroupName="GrupoStatusHomePage" ID="optStatusHomePageNao" runat="server" Checked="true" />
                                                                                                                          &nbsp;
                                                                                                                            <label class="opt" for="<%=optStatusHomePageNao.ClientID %>">Não</label>
                                                                                                                      </div>

                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <div class="hidden-lg hidden-md">
                                                                                                                  <br />
                                                                                                              </div>

                                                                                                              <div class="col-md-5">
                                                                                                                  <span>Mostrar botão "Corpo Docente"</span> <label id="lblMostarBotaoCorpoAlterado" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                                  <div class="row center-block btn-default form-group">
                                                                                                                      <div class="col-md-4">
                                                                                                                          <asp:RadioButton GroupName="GrupoStatusBotoes" ID="optStatusBotoesSim" runat="server" />
                                                                                                                          &nbsp;
                                                                                                                            <label class="opt" for="<%=optStatusBotoesSim.ClientID %>">Sim</label>
                                                                                                                      </div>

                                                                                                                      <div class="col-md-4">
                                                                                                                          <asp:RadioButton GroupName="GrupoStatusBotoes" ID="optStatusBotoesNao" runat="server" Checked="true" />
                                                                                                                          &nbsp;
                                                                                                                          <label class="opt" for="<%=optStatusBotoesNao.ClientID %>">Não</label>
                                                                                                                      </div>

                                                                                                                  </div>

                                                                                                              </div>
                                                                                                          </div>
                                                                                                          <br />

                                                                                                          <div class="row">
                                                                                                              <div class="col-md-12">
                                                                                                                  <span>Texto da HomePage</span> <label id="lblTextoHomeAlterado" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                                  <textarea style="resize: vertical; font-size: 14px" id="txtDescricaoHomePage" name="txtDescricaoHomePage" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                              </div>
                                                                                                          </div>
                                                                                                          <br />

                                                                                                          <div class="row">
                                                                                                              <div class="col-md-5 ">
                                                                                                                  <span>Botão Corpo docente</span> <label id="lblTextoBotaoCorpoAlterado" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                                  <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCorpoDocente" name="txtBotaoCorpoDocente" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                              </div>
                                                                                                          </div>
                                                                                                          
                                                                                                          <div id="divEnglish" runat="server">
                                                                                                              <br />
                                                                                                              <hr />
                                                                                                              <h3>(in english)</h3>
                                                                                                              <br />
                                                                                                              <div class="row">
                                                                                                                  <div class="col-md-12">
                                                                                                                      <span>Texto da HomePage (in english)</span> <label id="lblTextoHomeAlterado_en" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                                      <textarea style="resize: vertical; font-size: 14px" id="txtDescricaoHomePage_en" name="txtDescricaoHomePage_en" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <br />

                                                                                                              <div class="row">
                                                                                                                  <div class="col-md-5">
                                                                                                                      <span>Mostrar botão "Faculty" (in english)</span> <label id="lblMostarBotaoCorpoAlterado_en" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                                      <div class="row center-block btn-default form-group">
                                                                                                                          <div class="col-md-4">
                                                                                                                              <asp:RadioButton GroupName="GrupoStatusBotoes_en" ID="optStatusBotoesSim_en" runat="server" />
                                                                                                                              &nbsp;
                                                                                                                                <label class="opt" for="<%=optStatusBotoesSim_en.ClientID %>">Sim</label>
                                                                                                                          </div>

                                                                                                                          <div class="col-md-4">
                                                                                                                              <asp:RadioButton GroupName="GrupoStatusBotoes_en" ID="optStatusBotoesNao_en" runat="server" Checked="true" />
                                                                                                                              &nbsp;
                                                                                                                              <label class="opt" for="<%=optStatusBotoesNao_en.ClientID %>">Não</label>
                                                                                                                          </div>

                                                                                                                      </div>

                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <br />

                                                                                                              <div class="row">
                                                                                                                  <div class="col-md-5 ">
                                                                                                                      <span>Botão Corpo docente (in english)</span> <label id="lblTextoBotaoCorpoAlterado_en" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                                      <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCorpoDocente_en" name="txtBotaoCorpoDocente_en" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <br />
                                                                                                              <hr />
                                                                                                          </div>

                                                                                                          <br />
                                                                                                          <div class="row">
                                                                                                              <div class="col-md-12 ">
                                                                                                                  <span>Imagem da Página</span> <label id="lblImagemAlterada" runat="server"><small class="text-red">(alterada)</small></label><br />
                                                                                                                  <span>(Sugestão para o uso do site "Pixabay" com mais de 1 milhão de imagens, fotos e vídeos gratuitos.)</span><br />
                                                                                                                  <a target="_blank" href="https://pixabay.com/pt/">Clique aqui</a> para o site Pixabay. <i class="fa fa-info-circle fa-lg" style="color: blueviolet" data-toggle="tooltip" title="Escolha uma imagem e faça o download (selecione um tamanho próximo à 1200 x 800). Procure não escolher uma imagem muito 'pesada' acima de 1 Mb. Depois de salvar a imagem utilize o botão abaixo 'Trocar Imagem' para realizar a alteração."></i>
                                                                                                                  <br />
                                                                                                                  <section id="sectionBanner" runat="server" class="bannerRosto_interno">
                                                                                                                      <div id="texto-img" class="text-center">
                                                                                                                      </div>

                                                                                                                  </section>
                                                                                                                  <br />

                                                                                                                  <div id="divBotaoBotao" runat="server">
                                                                                                                      <button type="button" id="btnTrocaImagemUnidade" name="btnVoltar" class="btn btn-primary center-block" onclick="fAbreModalImagem()">
                                                                                                                          <i class="fa fa-camera"></i>&nbsp;Trocar Imagem</button>
                                                                                                                  </div>
                                                                                                              </div>
                                                                                                          </div>
                                                                                                          <br />

                                                                                                          <div class="row">
                                                                                                              <div class="col-md-10">
                                                                                                                  <span>Observações sobre a alteração</span><br />
                                                                                                                  <textarea style="resize: vertical; font-size: 14px" id="txtObsAlteracao" name="txtObsAlteracao" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                              </div>
                                                                                                          </div>

                                                                                                          

                                                                                                          <br />

                                                                                                          <div class="row" id="divbtnAprovarHome" runat="server">
                                                                                                              <div class="col-md-6 center-block">
                                                                                                                  <button type="button" id="btnReprovarHomeOff" name="btnReprovarHomeOff" class="btn btn-danger center-block" onclick="fReprovaHome()">
                                                                                                                        <i class="fa fa-thumbs-o-down"></i>&nbsp;Reprovar alteração dados Homepage</button>
                                                                                                                  <button type="button" id="btnReprovarHome" name="btnReprovarHome" runat="server" class="btn btn-danger center-block hidden" onserverclick="btnReprovarHome_Click">
                                                                                                                        <i class="fa fa-thumbs-o-down"></i>&nbsp;Reprovar alteração dados Homepage</button>

                                                                                                              </div>
                                                                                                              
                                                                                                              <div class="hidden-lg hidden-md">
                                                                                                                  <br />
                                                                                                              </div>

                                                                                                              <div class="col-md-6 center-block">
                                                                                                                  <button type="button" id="btnAprovarHomeOff" name="btnAprovarHomeOff" class="btn btn-success center-block" onclick="fAprovaHome()">
                                                                                                                        <i class="fa fa-thumbs-o-up"></i>&nbsp;Aprovar alteração dados Homepage</button>
                                                                                                                  <button type="button" id="btnAprovarHome" name="btnAprovarHome" runat="server" class="btn btn-success center-block hidden" onserverclick="btnAprovarHome_Click">
                                                                                                                        <i class="fa fa-thumbs-o-up"></i>&nbsp;Aprovar alteração dados Homepage</button>
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

                                                          <div class="box-footer hidden">
                                                          </div>

                                                      </div>
                                                  </div>

                                                  <div class="tab-pane" id="tab_PaginaAprovada">
                                                      <%--                                        <b>How to use:</b>--%>
                                                      <div class="box box-primary">
                                                          <div class="box-header hidden">
                                                              <div class="row">
                                                                  <div class="col-md-6">
                                                                      <h3 class="box-title">Página Aprovada</h3>
                                                                  </div>
                                                              </div>
                                                          </div>

                                                          <div class="box-body">
                                                              <!-- Sessão Coordenador -->
                                                              <div class="tab-content">
                                                                  <div class="panel panel-default">
                                                                      <div class="panel-body">
                                                                          <div class="row">
                                                                              <div class="col-md-12">
                                                                                  <h5 class="box-title text-bold">Página Aprovada&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label id="lblSituacaoPaginaAprovada" runat="server"></label></h5>
                                                                                  <div class="row">
                                                                                      <div class="col-md-12 ">
                                                                                          <div class="box-body">
                                                                                              <div class="tab-content">

                                                                                                  <div class="panel panel-default">
                                                                                                      <div class="panel-body">

                                                                                                          <div class="row">
                                                                                                              <div class="col-md-5">
                                                                                                                  <span>Mostrar na HomePage</span><br />
                                                                                                                  <div class="row center-block btn-default form-group">
                                                                                                                      <div class="col-md-12">
                                                                                                                          <label id="lblMostrarHomeAprovado" runat="server">(não definido)</label>
                                                                                                                      </div>

                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <div class="hidden-lg hidden-md">
                                                                                                                  <br />
                                                                                                              </div>

                                                                                                              <div class="col-md-5">
                                                                                                                  <span>Mostrar botão "Corpo Docente"</span><br />
                                                                                                                  <div class="row center-block btn-default form-group">
                                                                                                                      <div class="col-md-4">
                                                                                                                          <label id="lblBotaoCorpoDocente" runat="server">(não definido)</label>
                                                                                                                      </div>
                                                                                                                  </div>
                                                                                                              </div>
                                                                                                          </div>
                                                                                                          <br />

                                                                                                          <div class="row">
                                                                                                              <div class="col-md-10">
                                                                                                                  <span>Texto da HomePage</span><br />
                                                                                                                  <textarea style="resize: vertical; font-size: 14px" id="txtDescricaoHomePageAprovado" name="txtDescricaoHomePageAprovado" runat="server" class="form-control input-block-level" rows="25" readonly="readonly"></textarea>
                                                                                                              </div>
                                                                                                          </div>
                                                                                                          <br />

                                                                                                          <div class="row">
                                                                                                              <div class="col-md-5 ">
                                                                                                                  <span>Botão Corpo docente</span><br />
                                                                                                                  <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCorpoDocenteAprovado" name="txtBotaoCorpoDocente" runat="server" class="form-control input-block-level" rows="15" readonly="readonly"></textarea>
                                                                                                              </div>
                                                                                                          </div>
                                                                                                          
                                                                                                          <div id="divEnglishProducao" runat="server">
                                                                                                              <br />
                                                                                                              <hr />
                                                                                                              <h3>(in english)</h3>
                                                                                                              <br />

                                                                                                              <div class="row">
                                                                                                                  <div class="col-md-10">
                                                                                                                      <span>Texto da HomePage (in english)</span><br />
                                                                                                                      <textarea style="resize: vertical; font-size: 14px" id="txtDescricaoHomePageAprovado_en" runat="server" class="form-control input-block-level" rows="25" readonly="readonly"></textarea>
                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <br />
                                                                                                              
                                                                                                              <div class="row">
                                                                                                                  <div class="col-md-5">
                                                                                                                      <span>Mostrar botão "Faculty"  (in english)</span><br />
                                                                                                                      <div class="row center-block btn-default form-group">
                                                                                                                          <div class="col-md-4">
                                                                                                                              <label id="lblBotaoCorpoDocente_en" runat="server">(não definido)</label>
                                                                                                                          </div>
                                                                                                                      </div>
                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <br />

                                                                                                              <div class="row">
                                                                                                                  <div class="col-md-5 ">
                                                                                                                      <span>Botão Corpo docente (in english)</span><br />
                                                                                                                      <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCorpoDocenteAprovado_en" runat="server" class="form-control input-block-level" rows="15" readonly="readonly"></textarea>
                                                                                                                  </div>
                                                                                                              </div>
                                                                                                              <br />
                                                                                                              <hr />
                                                                                                          </div>
                                                                                                          
                                                                                                          <br />
                                                                                                          <div class="row">
                                                                                                              <div class="col-md-12 ">
                                                                                                                  <span>Imagem da Página</span><br />
                                                                                                                  <br />
                                                                                                                  <section id="sectionBannerAprovado" runat="server" class="bannerRosto_interno">
                                                                                                                      <div id="texto-imgAprovado" class="text-center">
                                                                                                                      </div>

                                                                                                                  </section>

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

                                                          <div class="box-footer hidden">
                                                          </div>

                                                      </div>
                                                  </div>
                                              </div>

                                          </div>

                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <!-- /.box-body -->
                              </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divComRegistro" runat="server">
            <br />
            <hr />
            <br />
            <br />
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li id="tabCoordenadoresCurso" class="active"><a href="#tab_CoordenadoresCurso" id="atab_CoordenadoresCurso" data-toggle="tab"><strong>Coordenadores do Curso</strong></a></li>
                    <li id="tabDisciplinaCurso" runat="server"><a href="#tab_DisciplinaCurso" id="atab_DisciplinaCurso"  data-toggle="tab"><strong>Disciplinas do Curso</strong></a></li>
<%--                    <li id="HomePage" class="" runat="server"><a href="#tab_HomePageCursoTurma" id="atab_HomePageCursoTurma"  data-toggle="tab"><strong>HomePage do Curso</strong></a></li>--%>
                </ul>

                <br />
                
                <div class="tab-content">
                    <div class="tab-pane active" id="tab_CoordenadoresCurso">
                        <%--                                        <b>How to use:</b>--%>
                        <div class="box box-primary">
                            <div class="box-header">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h3 class="box-title">Coordenadores</h3>
                                    </div>
                                </div>
                            </div>

                            <div class="box-body">
                                <!-- Sessão Coordenador -->
                                <div class="tab-content" id="divCoordenadores" runat ="server">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h5 class="box-title text-bold">Coordenadores</h5>
                                                    <div class="row">
                                                        <div class="col-md-12 ">
                                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="grid-content">
                                                 
                                                        <div id="msgSemResultadosCoordenador" style="display:block">
                                                            <div class="alert bg-gray"> 
                                                                <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Coordenador associado." />
                                                            </div>
                                                        </div>
                                                        <div id="divgrdCoordenador" class="table-responsive" style="display:none">
                                                            <div class="scroll">
                                                                <table id="grdCoordenador" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                    <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                        <tr>
                                                                            <th>id</th>
                                                                            <th>CPF</th>
                                                                            <th>Nome</th>
                                                                            <th>Tipo</th>
                                                                            <th>Excluir</th>
                                                                            <th>Ordem</th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <br />

                                                        <div class="col-md-3 pull-right">
                                                            <button type="button" id="btnAssociarCoordenador" name="btnAssociarCoordenador" class="btn btn-warning pull-right" href="#" onclick="fModalAssociarCoordenador()">
                                                                <i class="fa fa-user-plus"></i>&nbsp;Incluir Coordenador</button>
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

                            <div class="box-footer">

                            </div>

                        </div>
                    </div>

                    <div class="tab-pane" id="tab_DisciplinaCurso">
                        <%--                                        <b>How to use:</b>--%>
                        <div class="box box-primary">
                            <div class="box-header">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h3 class="box-title">Disciplinas</h3>
                                    </div>
                                </div>
                            </div>

                            <div class="box-body">
                                <!-- Sessão Disciplina -->
                                <div class="tab-content" id="divDisciplinas" runat ="server">

                                    <div class="panel panel-default">

                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h5 class="box-title text-bold">Disciplinas</h5>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12 ">
                                                            <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="grid-content">
                                                 
                                                                            <div id="msgSemResultadosDisciplina" style="display:block">
                                                                                <div class="alert bg-gray"> 
                                                                                    <asp:Label runat="server" ID="Label9" Text="Nenhuma Disciplina associada." />
                                                                                </div>
                                                                            </div>
                                                                            <div id="divgrdDisciplina" class="table-responsive" style="display:none">
                                                                                <div class="scroll">
                                                                                    <table id="grdDisciplina" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                                        <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                            
                                                                                        </thead>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <br />

                                                                            <div class="col-md-3 pull-right">
                                                                                <button type="button" id="btnAssociarDisciplina" name="btnAssociarDisciplina" class="btn btn-info pull-right" href="#" onclick="fModalAssociarDisciplina()">
                                                                                    <i class="fa fa-plus-square"></i>&nbsp;Incluir Disciplina</button>
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

                            <div class="box-footer">

                            </div>

                        </div>
                    </div>

                    <%--<div class="tab-pane" id="tab_HomePageCursoTurma">
                        
                    </div>--%>

                </div>
                
            </div>
        </div>


        <!-- Sessão Coordenador -->
        <%--<div class="tab-content" id="divCoordenadores" runat ="server">

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="box-title text-bold">Coordenadores</h5>
                            <div class="row">
                                <div class="col-md-12 ">
                                    
                                </div>
                            </div>                                                    
                                                                                
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="tab-content" id="divCoordenadoresAdicionados" runat ="server" visible="false">
            <div class="tab-content">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4">
                                <h5 class="box-title text-bold">Coordenadores Adicionados</h5>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                                                    
                            <div class="col-md-8">
                                <button type="button" id="btnAdicionarCoordenador" class="btn btn-warning" href="#" onclick="fModalAdicionarCoordenador()"> 
                                    <i class="fa fa-user-plus"></i>&nbsp;Adicionar Coordenadores</button>
                            </div>
                        </div>
                        <br />

                        <div class="tab-content">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <div class="tab-content">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="grid-content">
                                                                        <div runat="server" id="msgSemResultadogrdCoordenadorAdicionado">
                                                                            <div class="alert bg-gray">
                                                                                <asp:Label runat="server" ID="Label1" Text="Não existem Coordenadores para este Curso." />
                                                                            </div>
                                                                        </div>
                                                                        <div class="table-responsive ">

                                                                            <asp:GridView ID="grdCoordenadorAdicionado" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_Professor"
                                                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                <Columns>

                                                                                    <asp:BoundField DataField="id_Professor" HeaderText="id_Professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                    <asp:BoundField DataField="cpf" HeaderText="CPF" ItemStyle-HorizontalAlign="Left" />

                                                                                    <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                    <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <span style="position: relative;">
                                                                                                <i class="fa fa-close btn btn-danger btn-circle"></i>
                                                                                                <asp:Button OnClientClick="if (fMostrarProgresso3()) return;" HorizontalAlign="Center" ToolTip="Desassociar Coordenador" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdCoordenadorAdicionado_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                            </span>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>

                                                                            </asp:GridView>
                                                                                       
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlCodigoCursoArea" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNomeCursoArea" EventName="SelectedIndexChanged" />--%>
                                                    </Triggers>
                                            </asp:UpdatePanel>
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

        <!-- Sessão Disciplina -->
        <%--<div class="tab-content" id="divDisciplinas" runat ="server">

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="box-title text-bold">Disciplinas</h5>
                            <br />
                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="row">
                                            <div class="col-md-12">
                                                <div class="grid-content">
                                                 
                                                    <div id="msgSemResultadosDisciplina" style="display:block">
                                                        <div class="alert bg-gray"> 
                                                            <asp:Label runat="server" ID="Label6" Text="Nenhuma Disciplina associada." />
                                                        </div>
                                                    </div>
                                                    <div id="divgrdDisciplina" class="table-responsive" style="display:none">
                                                        <div class="scroll">
                                                            <table id="grdDisciplina" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                    <tr>
                                                                        <th>id</th>
                                                                        <th>Código</th>
                                                                        <th>Nome</th>
                                                                        <th>Excluir</th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="col-md-3 pull-right">
                                                        <button type="button" id="btnAssociarDisciplina" name="btnAssociarDisciplina" class="btn btn-warning pull-right" href="#" onclick="fModalAssociarDisciplina()">
                                                            <i class="fa fa-plus-square"></i>&nbsp;Incluir Disciplina</button>
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
        </div>--%>

        <div class="tab-content" id="divDisciplinasAdicionadas" runat ="server" visible="false">
            <div class="tab-content">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4">
                                <h5 class="box-title text-bold">Disciplinas Adicionadas</h5>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                                                    
                            <div class="col-md-8">
                                <button type="button" id="btnAdicionarDisciplina" class="btn btn-warning" href="#" onclick="fModalAdicionarDisciplina()"> 
                                    <i class="fa fa-user-plus"></i>&nbsp;Adicionar Disciplina</button>
                            </div>
                        </div>
                        <br />

                        <div class="tab-content">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <div class="tab-content">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="grid-content">
                                                                        <div runat="server" id="msgSemResultadogrdDisciplinaAdicionada">
                                                                            <div class="alert bg-gray">
                                                                                <asp:Label runat="server" ID="Label3" Text="Não existem Disciplinas para este Curso." />
                                                                            </div>
                                                                        </div>
                                                                        <div class="table-responsive ">

                                                                            <asp:GridView ID="grdDisciplinaAdicionada" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_Disciplina"
                                                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                <Columns>

                                                                                    <asp:BoundField DataField="id_Disciplina" HeaderText="id_Disciplina" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                    <asp:BoundField DataField="disciplinas.codigo" HeaderText="Código" ItemStyle-HorizontalAlign="Left" />

                                                                                    <asp:BoundField DataField="disciplinas.nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                    <%--<asp:BoundField DataField="CursoCodigo" HeaderText="Curso" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />--%>

                                                                                    <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <span style="position: relative;">
                                                                                                <i class="fa fa-close btn btn-danger btn-circle"></i>
                                                                                                <asp:Button OnClientClick="if (fMostrarProgresso4()) return;" HorizontalAlign="Center" CssClass="movedown" ToolTip="Desassociar Disciplina" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdDisciplinaAdicionada_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                            </span>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                

                                                                                </Columns>

                                                                            </asp:GridView>
                                                                                       
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlCodigoCursoArea" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNomeCursoArea" EventName="SelectedIndexChanged" />--%>
                                                    </Triggers>
                                            </asp:UpdatePanel>
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

            <div class="col-xs-2">
                <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

        </div>
    </div>

    <!-- Modal para Associar Coordenador -->
    <div class="modal fade" id="divModalAssociarCoordenador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Associar Coordenador</h4>
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
                                            <input class="form-control input-sm" id="txtCPF" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeCoordenador" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaCoordenadorDisponivelCurso()" >
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
                                            <div id="msgSemResultadosgrdCoordenadorDisponivel" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label5" Text="Nenhum Coordenador encontrado" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdCoordenadorDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdCoordenadorDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para Excluir Coordenador -->
    <div class="modal fade" id="divModalExcluirCoordenador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Coordenador</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir o Coordenador: <label id="lblNomeCoordenador"></label> - CPF: <label id="lblCPFCoordenador"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirCoordenador" type="button" name="bntExcluirCoordenador" title="" class="btn btn-success" onclick="fExcluiCoordenadorCurso()" >
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

    <!-- Modal para Associar Disciplina -->
    <div class="modal fade" id="divModalAssociarDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus-square"></i>&nbsp;Associar Disciplina</h4>
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
                                            <span>Código</span><br />
                                            <input class="form-control input-sm" id="txtCodigoDisciplina" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeDisciplina" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaDisciplinaDisponivelCurso()" >
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
                                            <div id="msgSemResultadosgrdDisciplinaDisponivel" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label7" Text="Nenhuma Disciplina encontrada" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdDisciplinaDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdDisciplinaDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para Excluir Disciplina -->
    <div class="modal fade" id="divModalExcluirDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Disciplina</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir a Disciplina: <label id="lblNomeDisciplina"></label> - Código: <label id="lblCodigoDisciplina"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirDisciplina" type="button" name="bntExcluirDisciplina" title="" class="btn btn-success" onclick="fExcluiDisciplinaCurso()" >
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

    <!-- ====================== -->

    <!-- Modal para Adicionar Coordenador -->
    <div class="modal fade" id="divModalAdicionarCoordenador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-user-plus"></i>&nbsp;Adicionar Coordenador</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-filter"></i>&nbsp;Filtro</b><br />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-body">
                                            <div class="row">
                                    
                                                <div class="col-md-2">
                                                    <span>CPF</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtCPFPerquisaCoordenador" type="text" value="" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-8">
                                                    <span>Nome</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtNomePerquisaCoordenador" type="text" value="" maxlength="70" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-1">
                                                    <div class="hidden-xs hidden-sm">
                                                        <br />
                                                    </div>

                                                    <button type="button" onclick="if (fMostrarProgresso3()) return;" id="bntPerquisaCoordenador" runat="server" name="bntPerquisaCoordenador" title="" class="btn btn-success" onserverclick="bntPerquisaCoordenador_Click" >
                                                        <i class="fa fa-check"></i>&nbsp;OK</button>
                                                </div>

                                            </div>
                                            <br />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <br />

                        <div class="row" >
                            <div class="tab-content">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="tab-content" runat="server" id="divResultadoListaCoordenadorDisponivel" style="display:none">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="grid-content">
                                                                            <div runat="server" id="divgrdCoordenadoresDisponiveis" style="display:none">
                                                                                <div class="alert bg-gray">
                                                                                    <asp:Label runat="server" ID="Label2" Text="Não existem Coordenadores disponíveis." />
                                                                                </div>
                                                                            </div>
                                                                            <div class="table-responsive ">

                                                                                <asp:GridView ID="grdCoordenadoresDisponiveis" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                    AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_Professor"
                                                                                    SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                    <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                    <Columns>

                                                                                        <asp:BoundField DataField="id_Professor" HeaderText="id_Professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                        <asp:BoundField DataField="cpf" HeaderText="CPF" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:TemplateField HeaderText="Incluir" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <span style="position: relative;">
                                                                                                    <i class="fa fa-check btn btn-success btn-circle"></i>
                                                                                                    <asp:Button HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnClientClick="if (fMostrarProgresso3()) return;" OnCommand="grdCoordenadoresDisponiveis_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                

                                                                                    </Columns>

                                                                                </asp:GridView>
                                                                                       
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <%--<asp:AsyncPostBackTrigger ControlID="ddlCodigoCursoArea" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlNomeCursoArea" EventName="SelectedIndexChanged" />--%>
                                                        </Triggers>
                                                </asp:UpdatePanel>
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
                        <button type="button" class="btn btn-default" data-dismiss="modal" onclick="fAtiva_grdDisciplina()">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Adicionar Disciplina -->
    <div class="modal fade" id="divModalAdicionarDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-user-plus"></i>&nbsp;Adicionar Disciplina</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-filter"></i>&nbsp;Filtro</b><br />
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-body">
                                            <div class="row">
                                    
                                                <div class="col-md-2">
                                                    <span>Código</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtCodigoPerquisaDisciplina" type="text" value="" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-8">
                                                    <span>Nome</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtNomePerquisaDisciplina" type="text" value="" maxlength="70" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-1">
                                                    <div class="hidden-xs hidden-sm">
                                                        <br />
                                                    </div>

                                                    <button type="button" onclick="if (fMostrarProgresso4()) return;" id="bntPerquisaDisciplina" runat="server" name="bntPerquisaDisciplina" title="" class="btn btn-success" onserverclick="bntPerquisaDisciplina_Click" >
                                                        <i class="fa fa-check"></i>&nbsp;OK</button>
                                                </div>

                                            </div>
                                            <br />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <br />

                        <div class="row" >
                            <div class="tab-content">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <div class="tab-content">
                                                                <div class="row" runat="server" id="divResultadoListaDisciplinaDisponivel" style="display:none">
                                                                    <div class="col-md-12">
                                                                        <div class="grid-content">
                                                                            <div runat="server" id="divgrdDisciplinaDisponiveis" style="display:none">
                                                                                <div class="alert bg-gray">
                                                                                    <asp:Label runat="server" ID="Label4" Text="Não existem Disciplinas disponíveis." />
                                                                                </div>
                                                                            </div>
                                                                            <div class="table-responsive ">

                                                                                <asp:GridView ID="grdDisciplinaDisponiveis" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                    AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_Disciplina"
                                                                                    SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                    <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                    <Columns>

                                                                                        <asp:BoundField DataField="id_Disciplina" HeaderText="id_Disciplina" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                        <asp:BoundField DataField="codigo" HeaderText="Código" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:TemplateField HeaderText="Incluir" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <span style="position: relative;">
                                                                                                    <i class="fa fa-check btn btn-success btn-circle"></i>
                                                                                                    <asp:Button OnClientClick="if (fMostrarProgresso4()) return;" HorizontalAlign="Center" CssClass="movedown" HeaderText="Incluir" ID="btnStart" runat="server" Text="" OnCommand="grdDisciplinaDisponiveis_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                

                                                                                    </Columns>

                                                                                </asp:GridView>
                                                                                       
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <%--<asp:AsyncPostBackTrigger ControlID="ddlCodigoCursoArea" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlNomeCursoArea" EventName="SelectedIndexChanged" />--%>
                                                        </Triggers>
                                                </asp:UpdatePanel>
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
                        <button type="button" class="btn btn-default" data-dismiss="modal" onclick="fAtiva_grdDisciplina()">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para setar se Coordenador é Geral ou de Área Curso -->
    <div class="modal fade" id="divModalCoordenadorGeralArea" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <h4 class="modal-title"><label></label> Selecione o Tipo do Coordenador</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <label id="lblNomeCoordenadorTipo"></label>
                                <br /><br />

                                <asp:DropDownList runat="server" ID="ddlTipoCoordenador" ClientIDMode="Static" class="form-control select2 SemPesquisa input-sm " AutoPostBack="false" onchange="fHabilitaBotao()">
                                </asp:DropDownList>
                                <input id ="txtIdCoordenadorTipo" class="hidden" />

                            </div>
                        </div>

                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-xs-6">
                            <button type="button" class="btn btn-default pull-left hidden" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                        </div>
                        <div class="col-xs-6">
                            <button id="btnConfirmaTipoCoordenador" type="button" class="btn btn-success pull-right" onclick="fIncluiCoordenadorCurso2()" style="display:none">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Ativar/Inativar Curso -->
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarCurso('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarCurso('Inativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Aprovação de Home -->
    <div class="modal fade" id="divModalAprocacaoHome" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        <i class="fa fa-thumbs-o-up"></i> Aprovar Alteração da HomePage do Curso
                    </h4>
                </div>
                <div class="modal-body">
                    <span> Deseja aprovar a(s) alteração(ões) da HomePage desse Curso </span>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                    <div class="pull-right">
                        <button type="button" class="btn btn-success" onclick="fConfirmaAprovacao()" >
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Reprovação de Home -->
    <div class="modal fade" id="divModalReprovacaoHome" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        <i class="fa fa-thumbs-o-down"></i> Reprovar Alteração da HomePage do Curso
                    </h4>
                </div>
                <div class="modal-body">
                    <span> Deseja Reprovar a(s) alteração(ões) da HomePage desse Curso ?</span>
                    <br /><br />
                    <span><em>Não se esqueça de descrever o motivo da reprovação no campo <strong>'Observações sobre a alteração'</strong></em> </span>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                    <div class="pull-right">
                        <button type="button" class="btn btn-success" onclick="fConfirmaReprovacao()" >
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
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
    
    <asp:FileUpload ID="fileArquivoParaGravar" runat="server" CssClass="btn btn-primary btn-file hidden" accept=".png,.jpg,.jpeg" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:fTrocaImagem(this);" />

    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

     <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>


    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>
        var vRowIndex_grdCoordenador;
        var vRowIndex_grdDisciplina;

        $('#<%=txtCPFPerquisaCoordenador.ClientID%>').mask('999.999.999-99');
        $('#txtCPF').mask('999.999.999-99');

        $(document).ready(function () {
            if (document.getElementById("<%=txtEnderecoQRCode.ClientID%>").value != "") {
                var conteudo = document.getElementById("<%=txtEnderecoQRCode.ClientID%>").value;
                var GoogleCharts = 'https://chart.googleapis.com/chart?chs=200x200&cht=qr&chl=';
                var imagemQRCode = GoogleCharts + conteudo;
                $('#imageQRCode').attr('src', imagemQRCode);
                //$('#aQRCode').attr('href', imagemQRCode);
                //$('#aQRCode').attr('download', "QR Code");
            }

            fPreencheCoordenadorCurso();
            fPreencheDisciplinaCurso();

            //$('#<%=txtDescricaoHomePage.ClientID%>').code('');
            //$('#<%=txtBotaoCorpoDocente.ClientID%>').code('');

            fSelect2();
        });

        //=======================================

        function fSelect2() {
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

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });
        }

        //========================================

        function fHabilitaBotao() {
            if ($("#<%=ddlTipoCoordenador.ClientID%> option:selected").val() != "") {
                document.getElementById('btnConfirmaTipoCoordenador').style.display = 'block';
            }
            else {
                document.getElementById('btnConfirmaTipoCoordenador').style.display = 'none';
            }
        }

        //============================================================================

        function fForceDownload() {
            //Função para salvar imagem de outra URL
            var url = document.getElementById("imageQRCode").src;
            fileName = "QR Code - " + document.getElementById('<%=txtNomeCurso.ClientID%>').value + ".png";
            var xhr = new XMLHttpRequest();
            xhr.open("GET", url, true);
            xhr.responseType = "blob";
            xhr.onload = function(){
                var urlCreator = window.URL || window.webkitURL;
                var imageUrl = urlCreator.createObjectURL(this.response);
                var tag = document.createElement('a');
                tag.href = imageUrl;
                tag.download = fileName;
                document.body.appendChild(tag);
                tag.click();
                document.body.removeChild(tag);
            }
            xhr.send();
        }

        //============================================================================

    //================================================================================
        function fCopyUrl() {
            /* Get the text field */
            var copyText = document.getElementById("<%=txtEnderecoQRCode.ClientID%>");

            /* Select the text field */
            copyText.select();

            /* Copy the text inside the text field */
            document.execCommand("copy");

            /* Alert the copied text */
            $.notify({
                icon: 'fa fa-check',
                title: '<strong>Atenção! </strong><br /><br />',
                message: 'O Link foi copiado com êxito.<br />',
            
            }, {
                type: 'info',
                delay: 1000,
                timer: 1000,
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

        //================================================================================

        //============================================================================

        function fAbreModalImagem() {
            document.getElementById("<%=fileArquivoParaGravar.ClientID%>").click();
        }


        //============================================================================

        function fTrocaImagem(input) {
                
            var vFileExt = input.value.split('.').pop();
            if (vFileExt.toUpperCase() == "JPEG" || vFileExt.toUpperCase() == "JPG" || vFileExt.toUpperCase() == "PNG") {

                document.getElementById('divBotoesMaster').style.display = 'block';
                document.getElementById('divBotaoSalvarMaster').style.display = 'block';

                if (input.files && input.files[0]) {
                    $('#<%=sectionBanner.ClientID%>').css('background-image', 'url(' + window.URL.createObjectURL(input.files[0]) + ')');
                }


                document.getElementById('divImgPrwMaster').style.display = 'block';
                document.getElementById('divMensagensMaster').style.display = 'none';
                document.getElementById('divBntLocalizarMaster').style.display = 'none';

                } else {
                    document.getElementById('divExtencaoMaster').style.display = 'none';
                    document.getElementById('divTamanhoMaster').style.display = 'block';
                    $('#divModalFotoMaster').modal('show');
                }

        }

        //============================================================================

        function fSalvarDados() {
            //alert(document.getElementById(<%=txtDescricaoHomePage.ClientID%>).value);
            //document.getElementById('hCodigotxtDescicaoHomePage').value = $('#<%=txtDescricaoHomePage.ClientID%>').code();
            //document.getElementById('hCodigotxtCorpoDocente').value = $('#<%=txtBotaoCorpoDocente.ClientID%>').code();
            document.getElementById("<%=bntSalvar2.ClientID%>").click();
        }

        //SUMMERNOTE =========================================================================================================
        var $summernoteDes;

        $(function () {
            function retira_acentos(palavra) {
                com_acento = 'áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇ';
                sem_acento = 'aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC';
                nova = '';
                for (i = 0; i < palavra.length; i++) {
                    if (palavra.substr(i, 1) == ".") {
                        nova += ".";
                    }
                    else if (com_acento.search(palavra.substr(i, 1)) >= 0) {
                        nova += sem_acento.substr(com_acento.search(palavra.substr(i, 1)), 1);
                    }
                    else {
                        nova += palavra.substr(i, 1);
                    }
                }
                return nova;
            }

            var $summernote = $('#<%=txtDescricaoHomePage.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['fontname', ['fontname']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video', 'hr']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 600, minHeight: 600, maxHeight: 1600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                callbacks: {
                    onImageUpload: function (files, editor, welEditable) {
                        formData = new FormData();
                        formData.append('file', files[0], files[0].name);
                        formData.append('qTipo', 'cursos');
                        formData.append('qId', document.getElementById('<%=txtIdCurso.ClientID%>').value);

                        $.ajax({
                            method: 'POST',
                            url: "wsSapiens.asmx/fSalvaImagemSummer",
                            contentType: false,
                            cache: false,
                            processData: false,
                            data: formData,
                            success: function (json) {
                                if (json[0].P0 == "ok") {
                                    //alert(json[0].P2);
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", json[0].P2, function ($image) {
                                        // after image load but image is not inserted yet.
                                        $image.attr('class', 'img-responsive');})
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.error(textStatus + " " + errorThrown);
                            }
                        });
                    }
                },

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

        //=======================================

        function fSalvaImagemSummer(file, editor, welEditable, qElemento) {
            formData = new FormData();
            formData.append('file', file, file.name);
            formData.append('qTipo', 'cursos');
            formData.append('qId', document.getElementById('<%=txtIdCurso.ClientID%>').value);

            $.ajax({
                method: 'POST',
                url: "wsSapiens.asmx/fSalvaImagemSummer",
                contentType: false,
                cache: false,
                processData: false,
                data: formData,
                success: function (json) {
                    if (json[0].P0 == "ok") {
                        //alert(json[0].P2);
                        //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                        qElemento.summernote("insertImage", json[0].P2, function ($image) {
                            // after image load but image is not inserted yet.
                            $image.attr('class', 'center-block img-responsive');
                        })
                    }
                    else {
                        alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                        return false;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error(textStatus + " " + errorThrown);
                }
            });
        }

        //SUMMERNOTE =========================================================================================================
        var $summernoteDes;

        $(function () {
            function retira_acentos(palavra) {
                com_acento = 'áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇ';
                sem_acento = 'aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC';
                nova = '';
                for (i = 0; i < palavra.length; i++) {
                    if (palavra.substr(i, 1) == ".") {
                        nova += ".";
                    }
                    else if (com_acento.search(palavra.substr(i, 1)) >= 0) {
                        nova += sem_acento.substr(com_acento.search(palavra.substr(i, 1)), 1);
                    }
                    else {
                        nova += palavra.substr(i, 1);
                    }
                }
                return nova;
            }

            var $summernote = $('#<%=txtDescricaoHomePage_en.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['fontname', ['fontname']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video', 'hr']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 600, minHeight: 600, maxHeight: 1600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                callbacks: {
                    onImageUpload: function (files, editor, welEditable) {
                        formData = new FormData();
                        formData.append('file', files[0], files[0].name);
                        formData.append('qTipo', 'cursos');
                        formData.append('qId', document.getElementById('<%=txtIdCurso.ClientID%>').value);

                        $.ajax({
                            method: 'POST',
                            url: "wsSapiens.asmx/fSalvaImagemSummer",
                            contentType: false,
                            cache: false,
                            processData: false,
                            data: formData,
                            success: function (json) {
                                if (json[0].P0 == "ok") {
                                    //alert(json[0].P2);
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", json[0].P2, function ($image) {
                                        // after image load but image is not inserted yet.
                                        $image.attr('class', 'img-responsive');})
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.error(textStatus + " " + errorThrown);
                            }
                        });
                    }
                },

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

        //=======================================

        function fSalvaImagemSummer(file, editor, welEditable, qElemento) {
            formData = new FormData();
            formData.append('file', file, file.name);
            formData.append('qTipo', 'cursos');
            formData.append('qId', document.getElementById('<%=txtIdCurso.ClientID%>').value);

            $.ajax({
                method: 'POST',
                url: "wsSapiens.asmx/fSalvaImagemSummer",
                contentType: false,
                cache: false,
                processData: false,
                data: formData,
                success: function (json) {
                    if (json[0].P0 == "ok") {
                        //alert(json[0].P2);
                        //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                        qElemento.summernote("insertImage", json[0].P2, function ($image) {
                            // after image load but image is not inserted yet.
                            $image.attr('class', 'center-block img-responsive');
                        })
                    }
                    else {
                        alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                        return false;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error(textStatus + " " + errorThrown);
                }
            });
        }

        //SUMMERNOTE =========================================================================================================
        $(function () {
            $summernote = $('#<%=txtDescricaoHomePageAprovado.ClientID%>');
            $summernote.summernote('disable', {
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 600, minHeight: 300, maxHeight: 600,         // set maximum height of editor

            });
        });

        //=======================================

         //SUMMERNOTE =========================================================================================================
        $(function () {
            $summernote = $('#<%=txtDescricaoHomePageAprovado_en.ClientID%>');
            $summernote.summernote('disable', {
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 600, minHeight: 300, maxHeight: 600,         // set maximum height of editor

            });
        });

        //=======================================

         //SUMMERNOTE =========================================================================================================
        $(function () {
            $summernote = $('#<%=txtBotaoCorpoDocenteAprovado.ClientID%>');
            $summernote.summernote('disable',{
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor

            });
        });

        //=======================================

         //SUMMERNOTE =========================================================================================================
        $(function () {
            $summernote = $('#<%=txtBotaoCorpoDocenteAprovado_en.ClientID%>');
            $summernote.summernote('disable',{
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor

            });
        });

        //=======================================

        //SUMMERNOTE =========================================================================================================
        $(function () {
            function retira_acentos(palavra) {
                com_acento = 'áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇ';
                sem_acento = 'aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC';
                nova = '';
                for (i = 0; i < palavra.length; i++) {
                    if (palavra.substr(i, 1) == ".") {
                        nova += ".";
                    }
                    else if (com_acento.search(palavra.substr(i, 1)) >= 0) {
                        nova += sem_acento.substr(com_acento.search(palavra.substr(i, 1)), 1);
                    }
                    else {
                        nova += palavra.substr(i, 1);
                    }
                }
                return nova;
            }

            var $summernote = $('#<%=txtBotaoCorpoDocente.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['fontname', ['fontname']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'hr']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                callbacks: {
                    onImageUpload: function (files, editor, welEditable) {
                        formData = new FormData();
                        formData.append('file', files[0], files[0].name);
                        formData.append('qTipo', 'cursos');
                        formData.append('qId', document.getElementById('<%=txtIdCurso.ClientID%>').value);

                        $.ajax({
                            method: 'POST',
                            url: "wsSapiens.asmx/fSalvaImagemSummer",
                            contentType: false,
                            cache: false,
                            processData: false,
                            data: formData,
                            success: function (json) {
                                if (json[0].P0 == "ok") {
                                    //alert(json[0].P2);
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", json[0].P2, function ($image) {
                                        // after image load but image is not inserted yet.
                                        $image.attr('class', 'center-block img-responsive');})
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.error(textStatus + " " + errorThrown);
                            }
                        });
                    }
                },
                

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

        //=======================================

        //SUMMERNOTE =========================================================================================================
        $(function () {
            function retira_acentos(palavra) {
                com_acento = 'áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇ';
                sem_acento = 'aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC';
                nova = '';
                for (i = 0; i < palavra.length; i++) {
                    if (palavra.substr(i, 1) == ".") {
                        nova += ".";
                    }
                    else if (com_acento.search(palavra.substr(i, 1)) >= 0) {
                        nova += sem_acento.substr(com_acento.search(palavra.substr(i, 1)), 1);
                    }
                    else {
                        nova += palavra.substr(i, 1);
                    }
                }
                return nova;
            }

            var $summernote = $('#<%=txtBotaoCorpoDocente_en.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['fontname', ['fontname']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'hr']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                callbacks: {
                    onImageUpload: function (files, editor, welEditable) {
                        formData = new FormData();
                        formData.append('file', files[0], files[0].name);
                        formData.append('qTipo', 'cursos');
                        formData.append('qId', document.getElementById('<%=txtIdCurso.ClientID%>').value);

                        $.ajax({
                            method: 'POST',
                            url: "wsSapiens.asmx/fSalvaImagemSummer",
                            contentType: false,
                            cache: false,
                            processData: false,
                            data: formData,
                            success: function (json) {
                                if (json[0].P0 == "ok") {
                                    //alert(json[0].P2);
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", json[0].P2, function ($image) {
                                        // after image load but image is not inserted yet.
                                        $image.attr('class', 'center-block img-responsive');})
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.error(textStatus + " " + errorThrown);
                            }
                        });
                    }
                },
                

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

        //=======================================

        function fMostrarProgresso2()
        {
            document.getElementById('<%=UpdateProgress2.ClientID%>').style.display = "block";
        }

        function fMostrarProgresso3()
        {
            document.getElementById('<%=UpdateProgress3.ClientID%>').style.display = "block";
        }

        function fMostrarProgresso4()
        {
            document.getElementById('<%=UpdateProgress4.ClientID%>').style.display = "block";
        }

        //================================================================================

        function fAprovaHome() {
            $('#divModalAprocacaoHome').modal();
        }

        //================================================================================

        function fReprovaHome() {
            $('#divModalReprovacaoHome').modal();
            
        }

        //================================================================================

        function fConfirmaAprovacao() {
            document.getElementById('<%=btnAprovarHome.ClientID%>').click();
        }

        //================================================================================

        function fConfirmaReprovacao() {
            document.getElementById('<%=btnReprovarHome.ClientID%>').click();
        }

        //================================================================================

        function fPreencheCoordenadorCurso() {
            var dt = $('#grdCoordenador').DataTable({
                processing: true,
                serverSide: false,
                destroy: true,
                async: false,
                error: function (xhr, error, thrown) {
                    alert( 'Não está logado' );
                },
                searching: false, //Pesquisar
                bPaginate: false, // Paginação
                bInfo: false, //mostrando 1 de x registros
                fnInitComplete: function (oSettings, json) {
                    //alert('Passou');
                    //CallBackReq(oSettings.fnRecordsTotal());
                    //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                    //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                    //    alert(json[i].Item);
                    //} 
                    //alert('Retorno json: ' + json);
                        

                    if(oSettings.fnRecordsTotal() == 0){
                        document.getElementById("divgrdCoordenador").style.display = "none";
                        document.getElementById("msgSemResultadosCoordenador").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdCoordenador").style.display = "none";
                            document.getElementById("msgSemResultadosCoordenador").style.display = "block";
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
                            document.getElementById("divgrdCoordenador").style.display = "block";
                            document.getElementById("msgSemResultadosCoordenador").style.display = "none";

                            var table_grdCoordenador = $('#grdCoordenador').DataTable();

                            $('#grdCoordenador').on("click", "tr", function(){
                                vRowIndex_grdCoordenador = table_grdCoordenador.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheCoordenadorCurso",
                    "type": "POST",
                    "dataSrc": "",
                    error: function (xhr, error, thrown) {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Houve um erro no processamento.<br/> <br/>Descrição do Erro: " + JSON.stringify(xhr, null, 2);
                        $('#divModalAssociarTamanho').modal('hide');
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                        //alert("Get JSON error");
                        //alert("xhr: " + xhr);
                        //alert("error: " + error);
                        //alert("thrown: " + thrown);
                        //console.log("AJAX error in request: " + JSON.stringify(xhr, null, 2));
                        //alert("AJAX error in request: " + JSON.stringify(xhr, null, 2));
                    }
                },
                columns: [
                    {
                        "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                    },
                    {
                        "data": "P1", "title": "CPF", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Nome", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P3", "title": "Tipo", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P4", "title": "Excluir", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P5", "title": "Ordem", "orderable": false, "className": "hidden"
                    }
                ],
                order: [[5, 'asc']],
                dom: 'Blfrtip',
                lengthMenu: [[20, 40, 60, -1], [20, 40, 60, "Todos"]],
                buttons: [

                ],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
                },
                fixedHeader: true
            });
        }


        //================================================================================

        function fPerquisaCoordenadorDisponivelCurso() {
            fProcessando();
            try {
                var qCPF = document.getElementById('txtCPF').value;
                var qNome = document.getElementById('txtNomeCoordenador').value;
                var dt = $('#grdCoordenadorDisponivel').DataTable({
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
                            document.getElementById("divgrdCoordenadorDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdCoordenadorDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdCoordenadorDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdCoordenadorDisponivel").style.display = "block";
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
                                document.getElementById("divgrdCoordenadorDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdCoordenadorDisponivel").style.display = "none";

                                var table_grdCoordenadorDisponivel = $('#grdCoordenadorDisponivel').DataTable();

                                $('#grdCoordenadorDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdCoordenadorDisponivel = table_grdCoordenadorDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaCoordenadorDisponivelCurso?qCPF=" + qCPF + "&qNome=" + qNome,
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
            finally {
                
            }
        }

        //=======================================
        function fIncluiCoordenadorCurso(qId, qCPF, qNome) {
            document.getElementById('txtIdCoordenadorTipo').value = qId;
            document.getElementById('lblNomeCoordenadorTipo').innerHTML = qNome;
            document.getElementById('btnConfirmaTipoCoordenador').style.display = 'none';
            $('#divModalCoordenadorGeralArea').modal();
        }

        //=======================================

        function fIncluiCoordenadorCurso2() {
            $('#divModalCoordenadorGeralArea').modal('hide');

            var qId = document.getElementById('txtIdCoordenadorTipo').value;
            var qNome = document.getElementById('lblNomeCoordenadorTipo').innerHTML;
            var qTipo = $("#<%=ddlTipoCoordenador.ClientID%> option:selected").val();

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiCoordenadorCurso?qId=" + qId + "&qTipo=" + qTipo,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do Coordenador: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheCoordenadorCurso();
                        //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.';
                        //$("#divCabecalho").removeClass("alert-warning");
                        //$("#divCabecalho").removeClass("alert-danger");
                        //$('#divCabecalho').addClass('alert-success');
                        //$('#divMensagemModal').modal();
                        $('#grdCoordenadorDisponivel').DataTable().row(vRowIndex_grdCoordenadorDisponivel).remove().draw();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Coordenador</strong><br /><br />',
                            message: 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.<br />',

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
                    alert("Houve um erro na inclusão do Coordenador. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão do Coordenador. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }


        //=======================================

        function fExcluiCoordenadorCurso() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiCoordenadorCurso?qId=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Coordenador';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do Coordenador: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            $('#grdCoordenador').DataTable().row(vRowIndex_grdCoordenador).remove().draw();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Coordenador</strong><br /><br />',
                                message: 'Exclusão do Coordenador realizado com sucesso.<br />',

                            }, {
                                type: 'danger',
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

                            //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Coordenador';
                            //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Exclusão do Coordenador realizado com sucesso.';
                            //$("#divCabecalho").removeClass("alert-warning");
                            //$("#divCabecalho").removeClass("alert-danger");
                            //$('#divCabecalho').addClass('alert-primary');
                            //$('#divMensagemModal').modal();
                        
                        }
                        fFechaProcessando();
                        $('#divModalExcluirCoordenador').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão do Coordenador. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão do Coordenador. Por favor tente novamente!");
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //================================================================================

        function fPreencheDisciplinaCurso() {
            var dt = $('#grdDisciplina').DataTable({
                processing: true,
                serverSide: false,
                destroy: true,
                async: false,
                error: function (xhr, error, thrown) {
                    alert( 'Não está logado' );
                },
                fnInitComplete: function (oSettings, json) {
                    //alert('Passou');
                    //CallBackReq(oSettings.fnRecordsTotal());
                    //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                    //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                    //    alert(json[i].Item);
                    //} 
                    //alert('Retorno json: ' + json);

                    if(oSettings.fnRecordsTotal() == 0){
                        document.getElementById("divgrdDisciplina").style.display = "none";
                        document.getElementById("msgSemResultadosDisciplina").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdDisciplina").style.display = "none";
                            document.getElementById("msgSemResultadosDisciplina").style.display = "block";
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
                            document.getElementById("divgrdDisciplina").style.display = "block";
                            document.getElementById("msgSemResultadosDisciplina").style.display = "none";

                            var table_grdDisciplina = $('#grdDisciplina').DataTable();
                            if (json[0].P4 != "nMestrado") {
                                table_grdDisciplina.columns([3]).visible(true);
                            }
                            else {
                                table_grdDisciplina.columns([3]).visible(false);
                            }
                            

                            $('#grdDisciplina').on("click", "tr", function () {
                                vRowIndex_grdDisciplina = table_grdDisciplina.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheDisciplinaCurso",
                    "type": "POST",
                    "dataSrc": "",
                    error: function (xhr, error, thrown) {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Houve um erro no processamento.<br/> <br/>Descrição do Erro: " + JSON.stringify(xhr, null, 2);
                        $('#divModalAssociarTamanho').modal('hide');
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                        //alert("Get JSON error");
                        //alert("xhr: " + xhr);
                        //alert("error: " + error);
                        //alert("thrown: " + thrown);
                        //console.log("AJAX error in request: " + JSON.stringify(xhr, null, 2));
                        //alert("AJAX error in request: " + JSON.stringify(xhr, null, 2));
                    }
                },
                columns: [
                    {
                        "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                    },
                    {
                        "data": "P1", "title": "Código", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Nome", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P4", "title": "Obrigatória", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Excluir", "orderable": false, "className": "text-center"
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

        //================================================================================

        function fCheckObrigatoria(elemento) {
            var sAux = (elemento.name).split("_");
            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fCheckObrigatoria?qIdCurso=" + sAux[1] + "&qIdDiscuplina=" + sAux[2] + "&qSituacao=" + elemento.checked,
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Obrigatoriedade';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração de obrigatoriedade desse curso. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {

                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Alteração de Obrigatoriedade</strong><br /><br />',
                                message: 'Alteração de Obrigatoriedade realizada com sucesso.<br />',

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

                            //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Coordenador';
                            //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Exclusão do Coordenador realizado com sucesso.';
                            //$("#divCabecalho").removeClass("alert-warning");
                            //$("#divCabecalho").removeClass("alert-danger");
                            //$('#divCabecalho').addClass('alert-primary');
                            //$('#divMensagemModal').modal();

                        }
                        fFechaProcessando();
                    },
                    error: function (xhr) {
                        alert("Houve um erro na alteração de presença do : " + sAux[3] + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na alteração de presença do : " + sAux[3] + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //================================================================================

        function fPerquisaDisciplinaDisponivelCurso() {
            fProcessando();
            try {
                var qCodigo = document.getElementById('txtCodigoDisciplina').value;
                var qNome = document.getElementById('txtNomeDisciplina').value;
                var dt = $('#grdDisciplinaDisponivel').DataTable({
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
                            document.getElementById("divgrdDisciplinaDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdDisciplinaDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdDisciplinaDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdDisciplinaDisponivel").style.display = "block";
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
                                document.getElementById("divgrdDisciplinaDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdDisciplinaDisponivel").style.display = "none";

                                var table_grdDisciplinaDisponivel = $('#grdDisciplinaDisponivel').DataTable();

                                $('#grdDisciplinaDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdDisciplinaDisponivel = table_grdDisciplinaDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaDisciplinaDisponivelCurso?qCodigo=" + qCodigo + "&qNome=" + qNome,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                        },
                        {
                            "data": "P2", "title": "Código", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left"
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
            finally {
                
            }
        }

        //============================================================================

        function fIncluiDisciplinaCurso(qId, qCodigo, qNome) {
            
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiDisciplinaCurso?qId=" + qId,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Disciplina';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão da Disciplina: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheDisciplinaCurso();
                        //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.';
                        //$("#divCabecalho").removeClass("alert-warning");
                        //$("#divCabecalho").removeClass("alert-danger");
                        //$('#divCabecalho').addClass('alert-success');
                        //$('#divMensagemModal').modal();
                        $('#grdDisciplinaDisponivel').DataTable().row(vRowIndex_grdDisciplinaDisponivel).remove().draw();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Disciplina</strong><br /><br />',
                            message: 'Inclusão da Disciplina <strong>' + qNome + '</strong> realizada com sucesso.<br />',

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
                    alert("Houve um erro na inclusão da Disciplina. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão da Disciplina. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function fExcluiDisciplinaCurso() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiDisciplinaCurso?qId=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Disciplina';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão da Disciplina: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            $('#grdDisciplina').DataTable().row(vRowIndex_grdDisciplina).remove().draw();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Disciplina</strong><br /><br />',
                                message: 'Exclusão da Disciplina realizada com sucesso.<br />',

                            }, {
                                type: 'danger',
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

                            //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Coordenador';
                            //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Exclusão do Coordenador realizado com sucesso.';
                            //$("#divCabecalho").removeClass("alert-warning");
                            //$("#divCabecalho").removeClass("alert-danger");
                            //$('#divCabecalho').addClass('alert-primary');
                            //$('#divMensagemModal').modal();
                        
                        }
                        fFechaProcessando();
                        $('#divModalExcluirDisciplina').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão da Disciplina. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão da Disciplina. Por favor tente novamente!");
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //============================================================================

        function fModalAtivaInativa(qOperacao) {
            if (qOperacao == 'Ativa') {
                $("#divCabecAtiva").removeClass("bg-danger");
                $('#divCabecAtiva').addClass('bg-info');
                document.getElementById("btnConfirmaAtivar").style.display = 'block';
                document.getElementById("btnConfirmaInativar").style.display = 'none';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Curso';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar o curso <strong>' + document.getElementById("<%=txtNomeCurso.ClientID%>").value + '</strong>?' ;
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Curso';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar o curso <strong>' + document.getElementById("<%=txtNomeCurso.ClientID%>").value + '</strong>?' ;
            }
            $('#divModalAtivaInativa').modal();
        }

        //===============================================================

        //=======================================

        function fAtivarInativarCurso(qOperacao){
            //alert(qOperacao);
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAtivarInativarCurso",
                contentType: 'application/json; charset=utf-8',
                data: "{qOperacao:'" + qOperacao + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var vTitulo = '';
                    var vBg = '';
                    var vIcon = '';
                    if (qOperacao == "Ativar") {
                        vTitulo = "Curso Ativado com sucesso";
                        vBg = "info";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    else {
                        vTitulo = "Curso Inativado com sucesso"
                        vBg = "danger";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    //alert('sucesso');
                    //Tratando o retorno com parseJSON
                    var json = $.parseJSON(data.d);
                    //alert(itens[0].NomeEmpresa);
                    if (json[0].Retorno == 'ok') {
                        $.notify({
                            icon: vIcon,
                            title: '<br /><br /><strong> Atenção! </strong><br /><br />',
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

                        $('#divModalAtivaInativa').modal('hide');
                    }
                    else if (json[0].Retorno == "deslogado") {
                        window.location.href = "index.html";
                    }
                    else {
                        $.notify({
                            icon: 'fa fa-check',
                            title: '<strong>Atenção! </strong><br /><br />',
                            message: 'Houve um problema na Ativação/Inativação do professor.<br />' + itens[0].Resposta,
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

        function fAtiva_grdCoordenadoresDisponiveis() {
            $('#<%=grdCoordenadoresDisponiveis.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
        }

        function fAtiva_grdDisciplinaDisponiveis() {
            if ( ! $.fn.DataTable.isDataTable( '#<%=grdDisciplinaDisponiveis.ClientID%>' ) ) {
                $('#<%=grdDisciplinaDisponiveis.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
            }
            if ( ! $.fn.DataTable.isDataTable( '#<%=grdDisciplinaAdicionada.ClientID%>' ) ) {
                $('#<%=grdDisciplinaAdicionada.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
            }
        }

        function fAtiva_grdDisciplina() {
            if ( ! $.fn.DataTable.isDataTable( '#<%=grdDisciplinaAdicionada.ClientID%>' ) ) {
                $('#<%=grdDisciplinaAdicionada.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
            }
            if ( ! $.fn.DataTable.isDataTable( '#<%=grdDisciplinaDisponiveis.ClientID%>' ) ) {
                $('#<%=grdDisciplinaDisponiveis.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
            }
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

        $('#<%=ddlTipoCurso.ClientID%>').on("select2:select", function(e) { 
            //alert($(this).val());
            if ($(this).val() == "1") {
                document.getElementById('<%=spanAsterisco_txtConceitoCapesCurso.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtDataDiarioOficialCurso.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtDataPortatiaMEC_Curso.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtNumeroCapesCurso.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtPortatiaMEC_Curso.ClientID%>').style.display = "inline-block";
                $('#<%=divNomeEnglish.ClientID%>').show();
                $('#<%=divEnglish.ClientID%>').show();
                $('#<%=divEnglishProducao.ClientID%>').show();
            }
            else {
                document.getElementById('<%=spanAsterisco_txtConceitoCapesCurso.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtDataDiarioOficialCurso.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtDataPortatiaMEC_Curso.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtNumeroCapesCurso.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtPortatiaMEC_Curso.ClientID%>').style.display = "none";
                $('#<%=divNomeEnglish.ClientID%>').hide();
                $('#<%=divEnglish.ClientID%>').hide();
                $('#<%=divEnglishProducao.ClientID%>').hide();
            }

        });

        function fModalAdicionarCoordenador() {
            document.getElementById('<%=divResultadoListaCoordenadorDisponivel.ClientID%>').style.display = "none";
            $('#divModalAdicionarCoordenador').modal();
        }

        function fModalAdicionarDisciplina() {
            document.getElementById('<%=divResultadoListaDisciplinaDisponivel.ClientID%>').style.display = "none";
            $('#divModalAdicionarDisciplina').modal();
        }

        function fMostrarProgresso()
        {
            document.getElementById('<%=UpdateProgress2.ClientID%>').style.display = "block";
        }

        //function fCheckObrigatoria(obj) {
        //    if (obj.checked) {
        //        var id = obj.getAttribute("id").split("_");
        //        document.getElementById("ContentPlaceHolderBody_grdCursos_chkObrigatoria_" + id[3]).checked = true;
        //    }
        //}

        function fCheckAssociar(obj) {
            if (!obj.checked) {
                var id = obj.getAttribute("id").split("_");
                document.getElementById("ContentPlaceHolderBody_grdCursos_chkAssociar_" + id[3]).checked = false;
            }
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalAdicionarCoordenador').is(':visible')) {
                    document.getElementById("<%=bntPerquisaCoordenador.ClientID%>").click();
                }
                else if ($('#divModalAdicionarDisciplina').is(':visible')) {
                    document.getElementById("<%=bntPerquisaDisciplina.ClientID%>").click();
                }
                else if ($('#divModalAssociarCoordenador').is(':visible')) {
                    fPerquisaCoordenadorDisponivelCurso();
                }
                else if ($('#divModalAssociarDisciplina').is(':visible')) {
                    fPerquisaDisciplinaDisponivelCurso();
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

        function fModalAssociarCoordenador() {
            document.getElementById("divgrdCoordenadorDisponivel").style.display = "none";
            $('#divModalAssociarCoordenador').modal();
        }

        function fModalAssociarDisciplina() {
            document.getElementById("divgrdDisciplinaDisponivel").style.display = "none";
            $('#divModalAssociarDisciplina').modal();
        }

        function AbreModalApagarCoordenador(qId, qCPF, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeCoordenador').innerHTML = qNome;
            document.getElementById('lblCPFCoordenador').innerHTML = qCPF;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirCoordenador').modal();
        }

        function AbreModalApagarDisciplina(qId, qCodigo, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeDisciplina').innerHTML = qNome;
            document.getElementById('lblCodigoDisciplina').innerHTML = qCodigo;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirDisciplina').modal();
        }

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        

        

    </script>

</asp:Content>
