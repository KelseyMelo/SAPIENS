<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadTipoCursoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadTipoCursoGestao" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li4TipoCursos" />

    <script src="Scripts/jquery.mask.min.js"></script>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
    
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="value" />
    <input type="hidden" id ="hCodigotxtDescicaoHomePage"  name="hCodigotxtDescicaoHomePage" value="value" />
    <input type="hidden" id ="hCodigotxtBotaoCalendario"  name="hCodigotxtBotaoCalendario" value="value" />
    <input type="hidden" id ="hCodigotxtBotaoProcesso"  name="hCodigotxtBotaoProcesso" value="value" />
    <%--<input type="hidden" id ="hTituloPagina"  name="hTituloPagina" value="Aluno (Listagem)" />--%>

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/minimal/blue.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>

    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet" />

    <!-- summernote -->
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.6.9/summernote.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.6.9/summernote.min.js"></script>--%>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote.css" rel="stylesheet"/>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote.js"></script>

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

    <div class="row"> 
        <div class="col-md-6">
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Tipo de Curso</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label><%--<asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label>--%></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 hidden">
            <br />
            <button type="button" runat="server" id="btnInativar" class="btn btn-danger" href="#" onclick="fModalAtivaInativa('Inativa')">
                <i class="fa fa-toggle-off"></i> Inativar Tipo de Curso
            </button>
            <button type="button" runat="server" id="btnAtivar" class="btn btn-info" href="#" onclick="fModalAtivaInativa('Ativa')">
                <i class="fa fa-toggle-on"></i> Ativar Tipo de Curso
            </button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 hidden">
            <br />
            <button type="button"  runat="server" id="btnCriarTipoCurso" name="btnCriarTipoCurso" class="btn btn-primary" href="#" onclick="">
                    <i class="fa fa-magic"></i>&nbsp;Criar Tipo de Curso</button>

        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button" class="btn btn-success pull-right" onclick="fSalvarDados()" >
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
                                <div class="col-md-4 ">
                                    <span>Tipo do Curso </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtTipoCurso" type="text" value="" maxlength="50" />
                                    <input class="form-control input-sm hidden" runat="server" id="txtIdTipoCurso" type="text" value="" />
                                </div>
                                <div class="hidden-lg hidden-md hidden">
                                    <br />
                                </div>

                                <div id="divNomeEnglish" runat="server" class="col-md-4 ">
                                    <span>Tipo do Curso (in english)</span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtTipoCurso_en" type="text" value="" maxlength="70" />
                                </div>
                                <div class="hidden-lg hidden-md hidden">
                                    <br />
                                </div>

                                <div class="col-md-3 hidden">
                                    <span>Mostrar na HomePage</span><br />
                                    <div class="row center-block btn-default form-group">
                                        <div class="col-md-4">
                                        <asp:RadioButton GroupName="GrupoStatusHomePage" ID="optStatusHomePageSim" runat="server" Checked="true"/>
                                        &nbsp;
                                        <label class="opt" for="<%=optStatusHomePageSim.ClientID %>">Sim</label>
                                        </div>
                                
                                        <div class="col-md-4">                    
                                        <asp:RadioButton GroupName="GrupoStatusHomePage" ID="optStatusHomePageNao" runat="server" />
                                        &nbsp;
                                        <label class="opt" for="<%=optStatusHomePageNao.ClientID %>">Não</label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row" runat ="server" id="divQRCode" style="display:block">
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

                        </div>
                    </div>
                </div>
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
                                    <i class="fa fa-globe fa-lg"></i>  HomePage
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
                                                                                                <div class="col-md-5 hidden">
                                                                                                    <span>Mostrar na HomePage</span>
                                                                                                    <label id="lblAlteradoMostrarHome" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                    <div class="row center-block btn-default form-group">
                                                                                                        <div class="col-md-4">
                                                                                                            <asp:RadioButton GroupName="GrupoStatusHomePage" ID="RadioButton1" runat="server" />
                                                                                                            &nbsp;
                                                                                                                            <label class="opt" for="<%=optStatusHomePageSim.ClientID %>">Sim</label>
                                                                                                        </div>

                                                                                                        <div class="col-md-4">
                                                                                                            <asp:RadioButton GroupName="GrupoStatusHomePage" ID="RadioButton2" runat="server" Checked="true" />
                                                                                                            &nbsp;
                                                                                                                            <label class="opt" for="<%=optStatusHomePageNao.ClientID %>">Não</label>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="hidden-lg hidden-md hidden">
                                                                                                    <br />
                                                                                                </div>

                                                                                                <div class="col-md-5">
                                                                                                    <span>Mostrar botões "Calendário e Processo Seletivo"</span>
                                                                                                    <label id="lblMostarBotoeProcessoCalendario" runat="server"><small class="text-red">(alterado)</small></label><br />
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
                                                                                                    <span>Texto da HomePage</span>
                                                                                                    <label id="lblTextoHomeAlterado" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                    <textarea style="resize: vertical; font-size: 14px" id="txtDescricaoHomePage" name="txtDescricaoHomePage" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />

                                                                                            <div class="row">
                                                                                                <div class="col-md-5 ">
                                                                                                    <span>Botão Processo Seletivo</span>
                                                                                                    <label id="lblTextoBotaoProcesso" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                    <textarea style="resize: vertical; font-size: 14px" id="txtBotaoProcesso" name="txtBotaoProcesso" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                </div>
                                                                                                <div class="hidden-lg hidden-md">
                                                                                                    <br />
                                                                                                </div>

                                                                                                <div class="col-md-5 ">
                                                                                                    <span>Botão Calendário</span>
                                                                                                    <label id="lblTextoBotaoCalendario" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                    <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCalendario" name="txtBotaoCalendario" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                </div>

                                                                                            </div>

                                                                                            <div id="divEnglish" runat="server">
                                                                                                <br />
                                                                                                <hr />
                                                                                                <h3>(in english)</h3>
                                                                                                <br />
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <span>Texto da HomePage (in english)</span>
                                                                                                        <label id="lblTextoHomeAlterado_en" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                        <textarea style="resize: vertical; font-size: 14px" id="txtDescricaoHomePage_en" name="txtDescricaoHomePage_en" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <br />

                                                                                                <div class="row">
                                                                                                    <div class="col-md-5 ">
                                                                                                        <span>Botão Processo Seletivo (in english)</span>
                                                                                                        <label id="lblTextoBotaoProcesso_en" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                        <textarea style="resize: vertical; font-size: 14px" id="txtBotaoProcesso_en" name="txtBotaoProcesso_en" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                    </div>
                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                        <br />
                                                                                                    </div>

                                                                                                    <div class="col-md-5 ">
                                                                                                        <span>Botão Calendário (in english)</span>
                                                                                                        <label id="lblTextoBotaoCalendario_en" runat="server"><small class="text-red">(alterado)</small></label><br />
                                                                                                        <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCalendario_en" name="txtBotaoCalendario_en" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                    </div>

                                                                                                </div>
                                                                                                <br />
                                                                                                <hr />
                                                                                            </div>

                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-md-12 ">
                                                                                                    <span>Imagem da Página</span>
                                                                                                    <label id="lblImagemAlterada" runat="server"><small class="text-red">(alterada)</small></label><br />
                                                                                                    <span>(Sugestão para o uso do site "Pixabay" com mais de 1 milhão de imagens, fotos e vídeos gratuitos.)</span><br />
                                                                                                    <a target="_blank" href="https://pixabay.com/pt/">Clique aqui</a> para o site Pixabay. <i class="fa fa-info-circle fa-lg" style="color: blueviolet" data-toggle="tooltip" title="Escolha uma imagem e faça o download (selecione um tamanho próximo à 1200 x 800). Procure não escolher uma imagem muito 'pesada' acima de 1 Mb. Depois de salvar a imagem utilize o botão abaixo 'Trocar Imagem' para realizar a alteração."></i>
                                                                                                    <br />
                                                                                                    <section id="sectionBanner" runat="server" class="bannerRosto_interno">
                                                                                                        <div id="texto-img" class="text-center">
                                                                                                        </div>

                                                                                                    </section>
                                                                                                    <br />

                                                                                                    <div id="div1" runat="server">
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
                                                                                                    <button type="button" id="btnReprovarHome" name="btnReprovarHome" runat="server" class="btn btn-danger center-block hidden" onserverclick="btnReprovarHome_Click">  <%----%>
                                                                                                        <i class="fa fa-thumbs-o-down"></i>&nbsp;Reprovar alteração dados Homepage</button>

                                                                                                </div>

                                                                                                <div class="hidden-lg hidden-md">
                                                                                                    <br />
                                                                                                </div>

                                                                                                <div class="col-md-6 center-block">
                                                                                                    <button type="button" id="btnAprovarHomeOff" name="btnAprovarHomeOff" class="btn btn-success center-block" onclick="fAprovaHome()">
                                                                                                        <i class="fa fa-thumbs-o-up"></i>&nbsp;Aprovar alteração dados Homepage</button>
                                                                                                    <button type="button" id="btnAprovarHome" name="btnAprovarHome" runat="server" class="btn btn-success center-block hidden" onserverclick="btnAprovarHome_Click"> <%----%>
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
                                                                                                    <span>Mostrar botões "Processo Seletivo e Calendário"</span><br />
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
                                                                                                    <span>Botão Processo Seletivo</span>
                                                                                                    <textarea style="resize: vertical; font-size: 14px" id="txtBotaoProcessoAprovado" name="txtBotaoProcessoAprovado" runat="server" class="form-control input-block-level" rows="5"></textarea>
                                                                                                </div>
                                                                                                <div class="hidden-lg hidden-md">
                                                                                                    <br />
                                                                                                </div>

                                                                                                <div class="col-md-5 ">
                                                                                                    <span>Botão Calendário</span>
                                                                                                    <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCalendarioAprovado" name="txtBotaoCalendarioAprovado" runat="server" class="form-control input-block-level" rows="5"></textarea>
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
                                                                                                        <textarea style="resize: vertical; font-size: 14px" id="txtDescricaoHomePageAprovado_en" name="txtDescricaoHomePageAprovado_en" runat="server" class="form-control input-block-level" rows="25" readonly="readonly"></textarea>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <br />

                                                                                                <div class="row">
                                                                                                    <div class="col-md-5 ">
                                                                                                        <span>Botão Processo Seletivo (in english)</span>
                                                                                                        <textarea style="resize: vertical; font-size: 14px" id="txtBotaoProcessoAprovado_en" name="txtBotaoProcessoAprovado_en" runat="server" class="form-control input-block-level" rows="5" readonly="readonly"></textarea>
                                                                                                    </div>
                                                                                                    <div class="hidden-lg hidden-md">
                                                                                                        <br />
                                                                                                    </div>

                                                                                                    <div class="col-md-5 ">
                                                                                                        <span>Botão Calendário (in english)</span>
                                                                                                        <textarea style="resize: vertical; font-size: 14px" id="txtBotaoCalendarioAprovado_en" name="txtBotaoCalendarioAprovado_en" runat="server" class="form-control input-block-level" rows="5" readonly="readonly"></textarea>
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

        <br />
        

        <div class="row">

            <div class="col-xs-2">
                <button type="button" runat="server" id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

            <button type="button" runat="server" id="bntSalvarNoticia" name="bntSalvarNoticia" class="btn btn-success pull-right hidden" href="#" onclick="if (ShowProgress()) return;" onserverclick="btnSalvar_ServerClick">
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>

            <button type="button" class="btn btn-success pull-right" onclick="fSalvarDados()" >
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>

        </div>
    </div>

    <!-- Modal para Ativar/Inativar Periodo -->
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarPeriodo('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarPeriodo('Inativar')">
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
                        <i class="fa fa-thumbs-o-up"></i> Aprovar Alteração da HomePage do Tipo de Curso
                    </h4>
                </div>
                <div class="modal-body">
                    <span> Deseja aprovar a(s) alteração(ões) da HomePage desse Tipo de Curso </span>

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
                        <i class="fa fa-thumbs-o-down"></i> Reprovar Alteração da HomePage do Tipo de Curso
                    </h4>
                </div>
                <div class="modal-body">
                    <span> Deseja Reprovar a(s) alteração(ões) da HomePage desse Tipo de Curso ?</span>
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
    
    <input class="note-image-input hidden" runat="server" type="file" id="fileArquivoPagina" name="fileArquivoPagina" accept="image/jpg,image/jpeg,image/png" onchange="javascript:fTrocaImagem(this);" />
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

        $(document).ready(function () {
           <%-- $('#<%=txtDescricaoHomePage.ClientID%>').code('');
            $('#<%=txtBotaoCalendario.ClientID%>').code('');
            $('#<%=txtBotaoProcesso.ClientID%>').code('');--%>
            fSelect2();

            if (document.getElementById("<%=txtEnderecoQRCode.ClientID%>").value != "") {
                var conteudo = document.getElementById("<%=txtEnderecoQRCode.ClientID%>").value;
                var GoogleCharts = 'https://chart.googleapis.com/chart?chs=200x200&cht=qr&chl=';
                var imagemQRCode = GoogleCharts + conteudo;
                $('#imageQRCode').attr('src', imagemQRCode);
                //$('#aQRCode').attr('href', imagemQRCode);
                //$('#aQRCode').attr('download', "QR Code");
            }
        });

//============================================================================

        function fForceDownload() {
            //Função para salvar imagem de outra URL
            var url = document.getElementById("imageQRCode").src;
            fileName = "QR Code - " + document.getElementById('<%=txtTipoCurso.ClientID%>').value + ".png";
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
            <%--document.getElementById('hCodigotxtDescicaoHomePage').value = $('#<%=txtDescricaoHomePage.ClientID%>').code();
            document.getElementById('hCodigotxtBotaoCalendario').value = $('#<%=txtBotaoCalendario.ClientID%>').code();
            document.getElementById('hCodigotxtBotaoProcesso').value = $('#<%=txtBotaoProcesso.ClientID%>').code();--%>
            document.getElementById("<%=bntSalvarNoticia.ClientID%>").click();
        }

        //============================================================================

        function fModalAtivaInativa(qOperacao) {
            if (qOperacao == 'Ativa') {
                $("#divCabecAtiva").removeClass("bg-danger");
                $('#divCabecAtiva').addClass('bg-info');
                document.getElementById("btnConfirmaAtivar").style.display = 'block';
                document.getElementById("btnConfirmaInativar").style.display = 'none';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Tipo de Curso';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar o Tipo de Curso <strong>' + document.getElementById("<%=lblTituloPagina.ClientID%>").innerHTML + '</strong>?';
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Tipo de Curso';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar o Tipo de Curso <strong>' + document.getElementById("<%=lblTituloPagina.ClientID%>").innerHTML + '</strong>?';
            }
            $('#divModalAtivaInativa').modal();
        }

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

        function teclaEnter() {
            if (event.keyCode == "13") {
                
            }
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        //SUMMERNOTE =========================================================================================================
        var $summernote;

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

            $summernote = $('#<%=txtDescricaoHomePage.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['insert', ['link', 'picture', 'video', 'hr']],
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
                        fSalvaImagemSummer(files[0], editor, welEditable);
                    }
                }

            });

        });

        function fSalvaImagemSummer(file, editor, welEditable) {
            formData = new FormData();
            formData.append('file', file, file.name);
            formData.append('qTipo', 'tipocursos');
            formData.append('qId', document.getElementById('<%=txtIdTipoCurso.ClientID%>').value);

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
        var $summernote;

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

            $summernote = $('#<%=txtDescricaoHomePage_en.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['insert', ['link', 'picture', 'video', 'hr']],
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
                        fSalvaImagemSummer(files[0], editor, welEditable);
                    }
                }

            });

        });

        function fSalvaImagemSummer(file, editor, welEditable) {
            formData = new FormData();
            formData.append('file', file, file.name);
            formData.append('qTipo', 'tipocursos');
            formData.append('qId', document.getElementById('<%=txtIdTipoCurso.ClientID%>').value);

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

            $summernote = $('#<%=txtBotaoCalendario.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['insert', ['link']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                onImageUpload: function (files) {
                    var qQtdArquivo = files.length;
                    //alert('qQtdArquivo: ' + qQtdArquivo);
                    $.each(files, function (idx, file) {
                        //alert('oi');
                        var pasta_web = $('#pasta_web').val();
                        var pasta_to = $('#pasta_to').val();
                        var formData = new FormData();
                        var fileArq = file.name;
                        fileArq = retira_acentos(fileArq.replace(/ /g, ''));
                        //fileArq = fileArq.replace(/ /g, '');
                        formData.append("qArquivo", file);
                        //formData.append("file_data", file);
                        //formData.append("pasta_to", pasta_to);
                        //formData.append("arquivo", file);
                        //formData.append("arquivo2", file);
                        pasta_web = pasta_web + fileArq;
                        $.ajax({
                            url: "wsSapiens.asmx/fSalvaImagem",
                            data: formData,
                            type: 'POST',
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (json) {
                                //var aux = window.location.href;
                                //alert('aux: ' + aux);

                                //var auxSemHashTag = aux.split("#");
                                //alert('auxSemHashTag: ' + auxSemHashTag[0]);

                                //var auxPaht = window.location.pathname;
                                //alert('auxPaht: ' + auxPaht);

                                var aux3 = window.location.hostname;

                                if (aux3.indexOf("donald") != -1) {
                                    aux3 = aux3 + "/Sapiens";
                                }

                                if (json[0].P0 == "ok") {
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (err) {
                                var myJSON = JSON.stringify(err);

                                alert(myJSON + "\n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                return false;
                            }
                        });

                    });
                },

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

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

            $summernote = $('#<%=txtBotaoCalendario_en.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['insert', ['link']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                onImageUpload: function (files) {
                    var qQtdArquivo = files.length;
                    //alert('qQtdArquivo: ' + qQtdArquivo);
                    $.each(files, function (idx, file) {
                        //alert('oi');
                        var pasta_web = $('#pasta_web').val();
                        var pasta_to = $('#pasta_to').val();
                        var formData = new FormData();
                        var fileArq = file.name;
                        fileArq = retira_acentos(fileArq.replace(/ /g, ''));
                        //fileArq = fileArq.replace(/ /g, '');
                        formData.append("qArquivo", file);
                        //formData.append("file_data", file);
                        //formData.append("pasta_to", pasta_to);
                        //formData.append("arquivo", file);
                        //formData.append("arquivo2", file);
                        pasta_web = pasta_web + fileArq;
                        $.ajax({
                            url: "wsSapiens.asmx/fSalvaImagem",
                            data: formData,
                            type: 'POST',
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (json) {
                                //var aux = window.location.href;
                                //alert('aux: ' + aux);

                                //var auxSemHashTag = aux.split("#");
                                //alert('auxSemHashTag: ' + auxSemHashTag[0]);

                                //var auxPaht = window.location.pathname;
                                //alert('auxPaht: ' + auxPaht);

                                var aux3 = window.location.hostname;

                                if (aux3.indexOf("donald") != -1) {
                                    aux3 = aux3 + "/Sapiens";
                                }

                                if (json[0].P0 == "ok") {
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (err) {
                                var myJSON = JSON.stringify(err);

                                alert(myJSON + "\n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                return false;
                            }
                        });

                    });
                },

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

        //SUMMERNOTE =========================================================================================================
        $(function () {
            $summernote = $('#<%=txtBotaoCalendarioAprovado.ClientID%>');
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
            $summernote = $('#<%=txtBotaoCalendarioAprovado_en.ClientID%>');
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

            $summernote = $('#<%=txtBotaoProcesso.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['insert', ['link']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                onImageUpload: function (files) {
                    var qQtdArquivo = files.length;
                    //alert('qQtdArquivo: ' + qQtdArquivo);
                    $.each(files, function (idx, file) {
                        //alert('oi');
                        var pasta_web = $('#pasta_web').val();
                        var pasta_to = $('#pasta_to').val();
                        var formData = new FormData();
                        var fileArq = file.name;
                        fileArq = retira_acentos(fileArq.replace(/ /g, ''));
                        //fileArq = fileArq.replace(/ /g, '');
                        formData.append("qArquivo", file);
                        //formData.append("file_data", file);
                        //formData.append("pasta_to", pasta_to);
                        //formData.append("arquivo", file);
                        //formData.append("arquivo2", file);
                        pasta_web = pasta_web + fileArq;
                        $.ajax({
                            url: "wsSapiens.asmx/fSalvaImagem",
                            data: formData,
                            type: 'POST',
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (json) {
                                //var aux = window.location.href;
                                //alert('aux: ' + aux);

                                //var auxSemHashTag = aux.split("#");
                                //alert('auxSemHashTag: ' + auxSemHashTag[0]);

                                //var auxPaht = window.location.pathname;
                                //alert('auxPaht: ' + auxPaht);

                                var aux3 = window.location.hostname;

                                if (aux3.indexOf("donald") != -1) {
                                    aux3 = aux3 + "/Sapiens";
                                }

                                if (json[0].P0 == "ok") {
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (err) {
                                var myJSON = JSON.stringify(err);

                                alert(myJSON + "\n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                return false;
                            }
                        });

                    });
                },

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

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

            $summernote = $('#<%=txtBotaoProcesso_en.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['insert', ['link']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor
                image: {
                    resizeFull: '540px',
                    selectFromFiles: 'Selecione a imagem',
                },

                onImageUpload: function (files) {
                    var qQtdArquivo = files.length;
                    //alert('qQtdArquivo: ' + qQtdArquivo);
                    $.each(files, function (idx, file) {
                        //alert('oi');
                        var pasta_web = $('#pasta_web').val();
                        var pasta_to = $('#pasta_to').val();
                        var formData = new FormData();
                        var fileArq = file.name;
                        fileArq = retira_acentos(fileArq.replace(/ /g, ''));
                        //fileArq = fileArq.replace(/ /g, '');
                        formData.append("qArquivo", file);
                        //formData.append("file_data", file);
                        //formData.append("pasta_to", pasta_to);
                        //formData.append("arquivo", file);
                        //formData.append("arquivo2", file);
                        pasta_web = pasta_web + fileArq;
                        $.ajax({
                            url: "wsSapiens.asmx/fSalvaImagem",
                            data: formData,
                            type: 'POST',
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (json) {
                                //var aux = window.location.href;
                                //alert('aux: ' + aux);

                                //var auxSemHashTag = aux.split("#");
                                //alert('auxSemHashTag: ' + auxSemHashTag[0]);

                                //var auxPaht = window.location.pathname;
                                //alert('auxPaht: ' + auxPaht);

                                var aux3 = window.location.hostname;

                                if (aux3.indexOf("donald") != -1) {
                                    aux3 = aux3 + "/Sapiens";
                                }

                                if (json[0].P0 == "ok") {
                                    //alert('url: ' + window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                    $summernote.summernote("insertImage", window.location.protocol + "//" + aux3 + "/Templates/emails/imagens/" + file.name);
                                }
                                else {
                                    alert(json[0].P1 + " \n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                    return false;
                                }
                            },
                            error: function (err) {
                                var myJSON = JSON.stringify(err);

                                alert(myJSON + "\n Não foi possível realizar a inclusão da imagem, por favor tente novamente.")
                                return false;
                            }
                        });

                    });
                },

                onChange: function (contents, $editable) {
                    // $editable.find(".note-editable div p img").addClass("bilola");
                    //                    $(".note-editable img").attr("style", "max-width:550px !important; width:550px !important");
                    //                    $(".note-editable img").attr("width", "550");
                    //                    return true;
                },

            });

        });

        //SUMMERNOTE =========================================================================================================
        $(function () {
            $summernote = $('#<%=txtBotaoProcessoAprovado.ClientID%>');
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
            $summernote = $('#<%=txtBotaoProcessoAprovado_en.ClientID%>');
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

        //=======================================

    </script>

</asp:Content>
