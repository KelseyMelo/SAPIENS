<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="outCertificadoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.outCertificadoGestao" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liMenuOutrosGrupo" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liOutrosCertificado" />

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
        <div class="col-md-10">
            <h3 class=""><i class="fa fa-circle-o text-purple"></i>&nbsp;<strong >Certificado</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(novo)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            <button type="button"  runat="server" id="btnCriarCertificado" name="btnCriarCertificado" class="btn btn-primary " href="#" onclick="" onserverclick="btnCriarCertificado_Click"> <%--onserverclick="btnCriarCertificado_Click"--%> 
                    <i class="fa fa-magic"></i>&nbsp;Criar Certificado</button>

        </div>

    </div>
    <br />

    <div class="container-fluid">
        <div class="tab-content">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div id="divLog" runat="server" visible ="false">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-2 ">
                                                <span>Data de Cadastro</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtDataCadastro" type="text" readonly="true" />
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <%--<div class="col-md-2">
                                                <span>Status</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtStatus" type="text" readonly="true" />
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>--%>

                                            <div class="col-md-2 ">
                                                <span>Última Alteração</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtDataAlteracao" type="text" readonly="true" />
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-3 ">
                                                <span>Responsável</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtResponsavel" type="text" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        
                            <br />
                        </div>                        
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <span style="font-size: 14px">Linha 1 </span><%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <span style="font-size: 14px"><small>Data do Evento</small></span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtDataEvento" type="date"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span style="font-size: 14px"><small>Número Inicial dos Certificados</small></span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtNumeroSequencial" type="number" value="0" min="0"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span style="font-size: 14px"><small>Ano Referência </small></span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtAnoReferencia" type="number" value="0" min="2000"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        
                        <br />

                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <span style="font-size: 14px">Linha 2 </span>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <span style="font-size: 14px"><small>Exemplo:</small> PELO PRESENTE CERTIFICAMOS QUE:</span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtCampo1a" type="text" value="PELO PRESENTE CERTIFICAMOS QUE:" maxlength="40" />
                                        </div>
                                        <div class="col-md-6">
                                            <span style="font-size: 14px"><small>Aqui será preenchido com o nome do participante</small></span>
                                            <input class="form-control input-sm alteracao" type="text" value="NOME DO PARTICIPANTE" readonly="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <br />

                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <span style="font-size: 14px">Linha 3 </span><%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <span style="font-size: 14px"><small>Exemplo:</small> PARTICIPOU DO CURSO:</span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtCampo2a" type="text" value="PARTICIPOU DO CURSO:" maxlength="30" />
                                        </div>
                                        <div class="col-md-6">
                                            <span style="font-size: 14px"><small>Nome do Evento (curso)</small></span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtNomeEvento" type="text" value="" maxlength="200" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <br />

                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <span style="font-size: 14px">Linha 4 </span><%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <span style="font-size: 14px"><small>Exemplo:</small> Data:</span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtCampo3a" type="text" value="PARTICIPOU DO CURSO:" maxlength="30" />
                                        </div>
                                        <div class="col-md-3">
                                            <span style="font-size: 14px"><small>Exemplo:</small> 12 DE JUNHO DE 2020</span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtCampo3b" type="text" value="" maxlength="50" />
                                        </div>
                                        <div class="col-md-3">
                                            <span style="font-size: 14px"><small>Exemplo:</small> Carga horária:</span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtCampo3c" type="text" value="PARTICIPOU DO CURSO:" maxlength="30" />
                                        </div>
                                        <div class="col-md-3">
                                            <span style="font-size: 14px"><small>Exemplo:</small> 3h</span>
                                            <input class="form-control input-sm alteracao" runat="server" id="txtCampo3d" type="text" value="" maxlength="30" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <br />

                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-5">
                                    <span>Palestante</span><br />
                                    <div class="row center-block btn-default form-group">
                                        <div class="col-md-6">
                                            <asp:RadioButton GroupName="GrupoPalestrante" ID="optPalestranteSim" runat="server" />
                                            &nbsp;
                                    <label class="opt" for="<%=optPalestranteSim.ClientID %>">Sim</label>
                                        </div>

                                        <div class="col-md-6">
                                            <asp:RadioButton GroupName="GrupoPalestrante" ID="optPalestranteNao" runat="server" />
                                            &nbsp;
                                    <label class="opt" for="<%=optPalestranteNao.ClientID %>">Não</label>
                                        </div>
                                    </div>
                                    <%--                            <asp:DropDownList runat="server" ID="ddlSexoAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                    <asp:ListItem Text="Masculino" Value="m" />
                                    <asp:ListItem Text="Feminino" Value="f" />
                                </asp:DropDownList>--%>
                                </div>
                            </div>
                        </div>
                        <br /><br />

                        <div class="col-md-12" >
                            <div id="divAssinatura" class="row" runat="server" style="display:block" >
                                <div class="col-md-12">
                                    <span style="font-size: 14px">Linha 5 (Assinaturas) </span><%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <span style="font-size: 14px"><small>Assinatura 1:</small></span>
                                                    <img id="imgAssinatura1" src="#" runat="server" class="img-responsive"/>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <input class="form-control input-sm alteracao" runat="server" id="txtImagemAssinatura1" type="text" value="" maxlength="70" readonly="true"/>
                                                </div>

                                                <div class="col-md-6">
                                                    <button type="button" id="btnLocalizarAssinatura" name="btnLocalizarAssinatura" class="btn btn-primary pull-right" onclick="javascript:fLocalizaArquivo()">
                                                        <i class="fa fa-search"></i>&nbsp;Localizar Assinatura 1
                                                    </button>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <span style="font-size: 14px"><small>Nome Assinatura 1 (exemplo):</small> Luciana Alves</span>
                                                    <input class="form-control input-sm alteracao" runat="server" id="txtNomeAssinatura1" type="text" value="" maxlength="70" />
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <span style="font-size: 14px"><small>Cargo Assinatura 1 (exemplo):</small> Coordenadora de Gestão de Pessoas</span>
                                                    <input class="form-control input-sm alteracao" runat="server" id="txtCargoAssinatura1" type="text" value="PARTICIPOU DO CURSO:" maxlength="70" />
                                                </div>
                                            </div>
                                        
                                        </div>

                                        
                                    </div>
                                </div>

                            </div>
                            <br />

                            <hr />
                            <div class="row">
                                
                                    <br />
                                    <div class="col-md-3">
                                        <span>Tipo Curso/Palestra</span><br />
                                        <asp:DropDownList runat="server" ID="ddlTipoCursoCertificado" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>
                                    <div runat="server" id="divColInformacoesAdicionais" class="col-md-5" >
                                        <span>Informações Adicionais <i class="fa fa-info-circle" style="color:blueviolet" data-toggle="tooltip" title="" data-original-title="Informações adicionais como, por exemplo, o nome da empresa"></i></span><br />
                                        <input class="form-control input-sm" runat="server" id="txtInformacoesAdicionais" type="text" value="" maxlength="500" />
                                    </div>
                               
                            </div>
                            <br />
                            <hr />
                            <br />

                            <div class="row">
                                <div class="col-md-12">
                                    <span style="font-size: 14px">Verso <small> (segunda página)</small></span>
                                    <textarea style="resize: none;font-size:14px" runat="server" class="form-control input-sm" rows="4" id="txtObsFolha2"></textarea>
                                    
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-12">
                                    <span style="font-size: 14px">Tipo de Certificado</span>
                                </div>
                                <br />

                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoTipoCertificado" ID="optTipoCertificado_1" runat="server"/>
                                    &nbsp;
                                    <label class="opt" for="<%=optTipoCertificado_1.ClientID %>">
                                        <span>Padrão</span><img src="Certificados/certificado_1.png"  class="img-responsive"/>
                                    </label>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoTipoCertificado" ID="optTipoCertificado_2" runat="server"/>
                                    &nbsp;
                                    <label class="opt" for="<%=optTipoCertificado_2.ClientID %>">
                                        <span>Plataforma Municípios</span><img src="Certificados/certificado_2.png" class="img-responsive" />
                                    </label>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoTipoCertificado" ID="optTipoCertificado_3" runat="server"/>
                                    &nbsp;
                                    <label class="opt" for="<%=optTipoCertificado_3.ClientID %>">
                                        <span>Opção 3</span><img src="Certificados/certificado_3.png" class="img-responsive" />
                                    </label>
                                </div>
                                
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoTipoCertificado" ID="optTipoCertificado_4" runat="server"/>
                                    &nbsp;
                                    <label class="opt" for="<%=optTipoCertificado_4.ClientID %>">
                                        <span>Café com Tecnologia</span><img src="Certificados/certificado_4.png" class="img-responsive" />
                                    </label>
                                </div>

                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoTipoCertificado" ID="optTipoCertificado_5" runat="server"/>
                                    &nbsp;
                                    <label class="opt" for="<%=optTipoCertificado_5.ClientID %>">
                                        <span>Bionanomanufatura</span><img src="Certificados/certificado_5.png" class="img-responsive" />
                                    </label>
                                </div>

                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-1 pull-right">
                                    <br />
                                    <button runat="server" type="button" id="btnSalvarCertificado" name="btnSalvarCertificado" class="btn btn-success pull-right" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvarCertificado_Click">
                                        <%----%>
                                        <i class="fa fa-floppy-o"></i>&nbsp;Salvar</button>
                                </div>
                            </div>
                        </div>
                        <br />

                    </div>
                </div>
            </div>
        </div>
    </div>

    <br />
    <div id="divPreview" runat ="server" visible="true">
        <div class="panel panel-default">

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <h5 class="box-title text-bold">Preview  <i class="fa fa-info-circle fa-lg" style="color:blueviolet" data-toggle="tooltip" title="Clique no botão para a criação de uma amostra dos certificados que serão gerados."></i></h5>
                        <br />
                        <div class="row">
                            <div class="col-md-12 center-block">
                                <%--fa fa-plus-square--%>
                                <button type="button" id="btnPreviewCertificado" name="btnPreviewCertificado" runat="server" class="btn btn-purple center-block" onclick="if (fProcessando()) return;" onserverclick="btnPreviewCertificado_Click">
                                    <i class="fa fa-download"></i>&nbsp;Preview do Certificado</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <a id="aLinkPreview" name="aLinkPreview" target="_blank" class="btn btn-purple btn-circle fa fa-download hidden" href="#"></a>

    <br />

    <div class="anchor" id="divAncora"></div>
    <a id="aAncora" href ="#divAncora" class="hidden" ></a>

    <!-- Sessão Grupo -->
    <div class="tab-content" id="divTurma" runat ="server" visible="false">

        <div class="panel panel-default">

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <h5 class="box-title text-bold">Turma Participante  <i class="fa fa-info-circle fa-lg" style="color:blueviolet" data-toggle="tooltip" title="Turma associada a esse Certificado."></i></h5>
                        <br />
                        <div class="row">
                            <div class="col-md-12 ">
                                <div class="row">
                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                 
                                                <div id="msgSemResultadosTurma" style="display:block">
                                                    <div class="alert bg-gray"> 
                                                        <asp:Label runat="server" ID="Label6" Text="Nenhum Participante associado." />
                                                    </div>
                                                </div>
                                                <div id="divgrdTurma" class="table-responsive" style="display:none">
                                                    <div class="scroll">
                                                        <table id="grdTurma" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                            <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                <tr>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                </div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-md-3 center-block">
                                                        <button type="button" id="btnTurma" name="btnTurma" class="btn btn-warning" onclick="fLocalizaArquivoExcel()">
                                                            <i class="fa fa-upload"></i>&nbsp;Importar Turma</button>
                                                    </div>

                                                    <div class="col-md-3 center-block">
                                                        <%--fa fa-plus-square--%>
                                                        <button type="button" runat="server" id="btnGerarRelatorio" name="btnGerarRelatorio" class="btn btn-success" onclick="if (fProcessando()) return;" onserverclick="btnGerarRelatorio_Click">
                                                            <i class="fa fa-print"></i>&nbsp;Gerar Certificados</button>
                                                    </div>

                                                    <div class="col-md-3 center-block">
                                                        <%--fa fa-plus-square--%>
                                                        <button type="button" id="btnBaixarCertificados" name="btnBaixarCertificados" class="btn btn-purple" onclick="fBaixarPdfCertificado()">
                                                            <i class="fa fa-download"></i>&nbsp;Baixar Certificados</button>
                                                    </div>

                                                    <div class="col-md-3 center-block">
                                                        <%--fa fa-plus-square--%>
                                                        <button type="button" id="btnEnviarEmailLote" name="btnEnviarEmailLote" class="btn btn-info" onclick="fAbrirModalEnviarEmailLote('Todos')">
                                                            <i class="fa fa-envelope-o"></i>&nbsp;Enviar e-mail em lote</button>
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

    <div class="row">

        <div class="col-xs-2">
            <button runat="server" type="button" id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click"> <%----%>
                    <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
        </div>

    </div>

    <!-- Modal Enviar Email -->
    <div class="modal fade" id="divModalEnviarEmail" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-envelope"></i>&nbsp;&nbsp;E-Mail</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-3">
                                <span >De</span><br />
                                <input class="form-control input-sm" runat="server" id="txtDeEmail" type="text" readonly="readonly" value="suporte@ipt.br" />
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                            <div class="col-md-6">
                                <span >Para</span><br />
                                <textarea style="resize: none;font-size:14px" runat="server" class="form-control input-sm" rows="1" id="txtParaEmail" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span >Assunto</span><br />
                                <input class="form-control input-sm" runat="server" id="txtAssuntoEmail" type="text" maxlength="100" value="Chegou seu Certificado - IPT" readonly="readonly" />
                            </div>
                        </div>
                        <br />

                        <div class="row ">
                            <div class="col-md-12">
                                <span>Mensagem</span><br />
                                <textarea runat="server" style="resize: vertical" id="txtCorpoEmail" name="txtCorpoEmail" class="form-control input-block-level" rows="5"></textarea>
                            </div>
                        </div>
                         <br />

                        <div class="row ">
                            <div class="col-md-12">
                                <button type="button" class="btn btn-primary btn-sm hidden"><i class="fa fa-send"></i>&nbsp;ENVIAR</button>
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
                            <button type="button" id="btnEnviarEmail" class="btn btn-primary pull-right hidden" onclick="if (fProcessando()) return;">  <%--onserverclick="btnEnviarEmail_Click"--%>
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                            <button type="button" class="btn btn-primary pull-right" onclick="fEnviaEmailCertificado()" >
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!-- Modal para Associar Grupo -->
    <div class="modal fade" id="divModalAssociarGrupo" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-yellow">
                    <h4 class="modal-title"><i class="fa fa-plus-square"></i>&nbsp;Associar Grupo</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <b><i class="fa fa-filter"></i>&nbsp;Filtro</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                    
                                        <div class="col-md-2">
                                            <span>Código</span><br />
                                            <input class="form-control input-sm" id="txtCodigoGrupo" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeGrupo" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaGrupoDisponivel()" >
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
                                            <div id="msgSemResultadosgrdTurmaDisponivel" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label7" Text="Nenhuma Turma encontrada" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdTurmaDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdTurmaDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para Editar Grupo -->
    <div class="modal fade" id="divModalEditarGrupo" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="divCabecGrupo" class="modal-header ">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i id="iconeGrupo" class="fa fa-edit"></i> <label id="lblTituloGrupo"></label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <span>Certificado</span>
                            <input class="form-control input-sm" type="text" id="txtNomeCertificadoEditar" readonly="true" />

                        </div>
                    </div>
                    <br />
                    
                    <div class="row">
                        <div class="col-md-12">
                            <span>Grupo</span>
                            <input class="form-control input-sm" type="text" id="txtNomeGrupoEditar" readonly="true" />

                        </div>
                    </div>
                    <br />


                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntEditarGrupo" type="button" name="bntEditarGrupo" title="" class="btn btn-success" onclick="fEditarGrupo()" >
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;Confirmar</button>
                            <button id="bntAssociarGrupo" type="button" name="bntAssociarGrupo" title="" class="btn btn-success" onclick="fAssociarGrupo()" >
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;Confirmar</button>
                            <button id="bntApagarGrupo" type="button" name="bntApagarGrupo" title="" class="btn btn-success" onclick="fApagarGrupo()" >
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

    <!-- Modal para Confirmação de Importação de Turma -->
    <div class="modal fade" id="divModalConfirmacaoImportacaoTurma" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header alert-warning">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-info-circle"></i> ATENÇÃO</h4>
                </div>
                <div class="modal-body">
                    O processo de uma "nova" importação irá recriar <strong>todos os registros</strong> dos participantes.<br /><br />
                    Deseja realmente realizar uma <strong>nova importação</strong>?
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button type="button" title="" class="btn btn-success" onclick="fLocalizaArquivoExcel2()" >
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

    <!-- Modal para Excluir Grupo -->
    <div class="modal fade" id="divModalExcluirGrupo" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Grupo</h4>
                </div>
                <div class="modal-body">
                    Grupo: <label class="negrito" id="lblCodigoGrupo"></label> - <label class="negrito" id="lblNomeGrupo"></label><br />Vagas: <label class="negrito" id="lblVagasGrupo"></label><br />Dia: <label class="negrito" id="lblDiaGrupo"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirGrupo" type="button" name="bntExcluirGrupo" title="" class="btn btn-success" onclick="fExcluiGrupoPeriodoMatricula()" >
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

    <!-- Modal para Excluir Certificado -->
    <div class="modal fade" id="divModalExcluirPeriodoMatricula" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
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
    
    <div class="hidden">
        <asp:FileUpload ID="fileArquivoParaGravar" runat="server" accept=".jpg,.jpeg,.png" onchange="javascript:fSelecionouArquivo(this);"  />
        <asp:FileUpload ID="fileArquivoParaImportar" runat="server" accept=".xls,.xlsx" onchange="javascript:fSelecionouExcelCertificado(this);"  />
        <a id="aBaixarPDF" target="_blank" href="#"></a>
    </div>

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
        var vRowIndex_grdTurma;

        $(document).ready(function () {
            fSelect2();

            if ($('#<%=divTurma.ClientID%>').length) {
                fPreencheTurmaCertificado();
                //fSelect2();
            }
            //else {
               
            //}

            $('#aAncora').click(function () {
                    $('html, body').animate({
                        scrollTop: $($(this).attr('href')).offset().top
                    }, 'slow');
                    return false;
            });
             
            //alert($("#divColInformacoesAdicionais option:selected").val());
            ////alert($("#divColInformacoesAdicionais option:selected").text());
            //if ($("#divColInformacoesAdicionais option:selected").val() == 2) {
            //    //Tipo In Company
            //    document.getElementById('divColInformacoesAdicionais').style.display = 'block';
            //}
            //else {
            //    document.getElementById('divColInformacoesAdicionais').style.display = 'none';
            //}

        });

        //=============================================================================

        function fLocalizaArquivo() {
            document.getElementById("<%=fileArquivoParaGravar.ClientID%>").click();
        }

        //=============================================================================

        function fAncora() {
            document.getElementById("aAncora").click();
        }

        //=============================================================================


        function fDownloadPreview(qArquivo) {
            document.getElementById("aLinkPreview").href = qArquivo;
            document.getElementById("aLinkPreview").click();
        }

        //=============================================================================

        $("#<%=optPalestranteSim.ClientID%>").on('ifChecked', function (e) {
            document.getElementById('<%=divAssinatura.ClientID%>').style.display = 'none';
        });

        $("#<%=optPalestranteSim.ClientID%>").on('ifUnchecked', function (e) {
            document.getElementById('<%=divAssinatura.ClientID%>').style.display = 'block';
        });

        //=============================================================================

        $('#<%=ddlTipoCursoCertificado.ClientID%>').on("select2:select", function (e) { 
            //alert($(this).val());
            if ($(this).val() == 2) {
                //Tipo In Company
                document.getElementById('<%=divColInformacoesAdicionais.ClientID%>').style.display = 'block';
            }
            else {
                document.getElementById('<%=divColInformacoesAdicionais.ClientID%>').style.display = 'none';
            }
            //$("#<%=ddlTipoCursoCertificado.ClientID%>").val($(this).val()).trigger("change");
            //$('#<%=ddlTipoCursoCertificado.ClientID%>').hide();
        });

        //=============================================================================
        function fSelecionouArquivo(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "PNG" || vFileExt.toUpperCase() == "JPG" || vFileExt.toUpperCase() == "JPEG") {

                if (idFile.files && idFile.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        document.getElementById('<%=txtImagemAssinatura1.ClientID%>').value = idFile.files[0].name;
                        $('#<%=imgAssinatura1.ClientID%>').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(idFile.files[0]);
                }

            } else {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "O arquivo deve ser dos tipos JPG ou JPEG ou PNG";
                $('#divModalAssociarTamanho').modal('hide');
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
            }
        }

        //=============================================================================

        function fLocalizaArquivoExcel() {
            if (document.getElementById("<%=btnGerarRelatorio.ClientID%>").style.display == "block") {
                $('#divModalConfirmacaoImportacaoTurma').modal();
            }
            else {
                fLocalizaArquivoExcel2();
            }
        }

        function fLocalizaArquivoExcel2() {
            $('#divModalConfirmacaoImportacaoTurma').modal('hide');
            document.getElementById("<%=fileArquivoParaImportar.ClientID%>").value = null;
            document.getElementById("<%=fileArquivoParaImportar.ClientID%>").click();
        }

        //=============================================================================

        function fSelecionouExcelCertificado(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "XLS" || vFileExt.toUpperCase() == "XLSX") {

                if (idFile.files && idFile.files[0]) {
                    fProcessando();
                    try {
                        var files = idFile.files;
                        //$.each(idFile, function (idx, file) {
                        var formData = new FormData();
                        formData.append("qArquivo", idFile.files[0]);

                            $.ajax({
                                url: "wsSapiens.asmx/fSelecionouExcelCertificado",
                                data: formData,
                                type: 'POST',
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (json) {
                    
                                    if (json[0].P0 == "ok") {
                                        //$('#divModalAlteraDadosFotoMaster').modal('hide');
                                        //fLimparArquivoMaster();
                                        //$('#imgLogin1').attr('src', json[0].P2);
                                        //$('#imgLogin2').attr('src', json[0].P2);
                                        //$('#imgLogin3').attr('src', json[0].P2);
                                        //$('#ContentPlaceHolderBody_imgAluno').attr('src', json[0].P2);
                            
                                        //if (document.getElementById("ContentPlaceHolderBody_imgAluno")) {
                                        //    document.getElementById("ContentPlaceHolderBody_imgAluno").src = json[0].P2;
                                        //}
                                        fPreencheTurmaCertificado();
                                        fFechaProcessando();

                                        $.notify({
                                            icon: 'fa fa-thumbs-o-up fa-2x',
                                            title: '<strong>Importação realizada</strong><br /><br />',
                                            message: "Importação realizada com sucesso",

                                        }, {
                                            type: 'info',
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
                                    else {
                                        fPreencheTurmaCertificado();
                                        fFechaProcessando();
                                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                        $("#divCabecalho").removeClass("alert-info");
                                        $("#divCabecalho").removeClass("alert-success");
                                        $("#divCabecalho").removeClass("alert-danger");
                                        $("#divCabecalho").removeClass("alert-warning");
                                        $('#divCabecalho').addClass('alert-warning');
                                        $('#divMensagemModal').modal();
                                        return false;
                                    }
                                },
                                error: function (err) {
                                    var myJSON = JSON.stringify(err);

                                    alert(myJSON + "\n Não foi possível realizar a Importação da planilha, por favor tente novamente.")
                                    fFechaProcessando();
                                    return false;
                                }
                            });
                            //});
                        } catch (e) {
                            fFechaProcessando();
                        }
                }

            } else {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "O arquivo deve ser do tipo Excel (.xls ou . xlsx)";
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
            }
        }

        //=============================================================================


        function fSelect2() {
            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });

            $(".select2").select2({
                theme: "bootstrap",
                language: "pt-BR",
            });

            $(".SemPesquisa").select2({
                theme: "bootstrap",
                minimumResultsForSearch: Infinity
            });
        }

        //=============================================================================

        function fModalAssociarGrupo() {
            $('#divModalAssociarGrupo').modal();
        }

        function fPreencheTurmaCertificado() {
            var dt = $('#grdTurma').DataTable({
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

                    document.getElementById("btnTurma").style.display = "block";
                    document.getElementById("<%=btnGerarRelatorio.ClientID%>").style.display = "none";
                    document.getElementById("btnBaixarCertificados").style.display = "none";
                    document.getElementById("btnEnviarEmailLote").style.display = "none";

                    if (oSettings.fnRecordsTotal() == 0) {
                        document.getElementById("divgrdTurma").style.display = "block";
                        document.getElementById("msgSemResultadosTurma").style.display = "block";
                        document.getElementById("btnTurma").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdTurma").style.display = "none";
                            document.getElementById("msgSemResultadosTurma").style.display = "block";
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
                            document.getElementById("divgrdTurma").style.display = "block";
                            document.getElementById("msgSemResultadosTurma").style.display = "none";
                            document.getElementById("<%=btnGerarRelatorio.ClientID%>").style.display = "block";

                            if (json[0].P3 != "") {
                                document.getElementById("btnBaixarCertificados").style.display = "block";
                                document.getElementById("<%=btnGerarRelatorio.ClientID%>").style.display = "block";

                                json.forEach(function(elemento){
                                if (elemento.P6 != "" && elemento.P6 != null) {
                                    //alert(elemento.P6);
                                    document.getElementById("btnEnviarEmailLote").style.display = "block";
                                }
                            });
                            }

                            //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {

                            //}
                            
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheTurmaCertificado",
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
                        "data": "P0", "title": "N.º Seq.", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P1", "title": "Participante", "orderable": true, "className": "text-left", "width": "50%"
                    },
                    {
                        "data": "P2", "title": "Int/Ext", "orderable": true, "className": "text-center", "width": "10px"
                    },
                    {
                        "data": "P6", "title": "E-mail", "orderable": true, "className": "text-left", "width": "10px"
                    },
                    {
                        "data": "P8", "title": "Data Envio de e-mail", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "PDF", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P7", "title": "Envio e-mail", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Editar", "orderable": false, "className": "text-center hidden"
                    },
                    {
                        "data": "P5", "title": "Excluir", "orderable": false, "className": "text-center hidden"
                    }
                ],
                order: [[0, 'asc']],
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

        function encodeImageFileAsURL(element) {
            var file = element.files[0];
            var reader = new FileReader();
            reader.onloadend = function () {
                return reader.result;
             }
            reader.readAsDataURL(file);
        }

         //================================================================================

        function fAbrirModalEnviarEmailLote(qPara) {
            document.getElementById('<%=txtParaEmail.ClientID%>').value = qPara;
            $('#divModalEnviarEmail').modal();
        }

        //================================================================================

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

            var $summernote = $('#<%=txtCorpoEmail.ClientID%>');
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
                height: 1000, minHeight: 1000, maxHeight: 1000,         // set maximum height of editor
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

        //===============================================================

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

            var $summernote = $('#<%=txtObsFolha2.ClientID%>');
            $summernote.summernote({
                airMode: false, focus: false,
                toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['style2', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                //['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                //['height', ['height']],
                //['insert', ['link']],
                ['view', ['fullscreen', 'codeview', 'help']],
                ],
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 200, minHeight: 100, maxHeight: 700,         // set maximum height of editor
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

        //===============================================================

        function fPerquisaGrupoDisponivel() {
            fProcessando();
            try {
                var qIdGrupo = document.getElementById('txtCodigoGrupo').value;
                var qNome = document.getElementById('txtNomeGrupo').value;
                var dt = $('#grdTurmaDisponivel').DataTable({
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
                            document.getElementById("divgrdTurmaDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdTurmaDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdTurmaDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdTurmaDisponivel").style.display = "block";
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
                                document.getElementById("divgrdTurmaDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdTurmaDisponivel").style.display = "none";
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaGrupoDisponivel?qIdGrupo=" + qIdGrupo + "&qNome=" + qNome,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": true, "className": "hidden"
                        },
                        {
                            "data": "P1", "title": "Grupo", "orderable": true, "className": "text-left"
                        },
                        {
                            "data": "P2", "title": "Adicionar", "orderable": false, "className": "text-center"
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
        
        function fBaixarPdfCertificado() {
            fProcessando();
            try {
                var qPermissao;
                
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fBaixarPdfCertificado",
                    dataType: "json",
                    success: function(json)
                    {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Edição do Tipo de Acesso';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro do Tipo de Acesso: ' + qNomeGrupo + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //$('#divModalEditarGrupo').modal('hide');
                            //fPreencheTurmaCertificado();
                            //alert(json[0].P1);
                            document.getElementById('aBaixarPDF').href = json[0].P1;
                            document.getElementById('aBaixarPDF').click();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Download realizado</strong><br /><br />',
                                message: 'Download realizado com sucesso.<br />',

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
                        alert("Houve um erro no download dos certificados. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro no download dos certificados. Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }


//============================================================================
        
        function fEnviaEmailCertificado() {
            fProcessando();
            try {
                
                var formData = new FormData();
                formData.append("qDestinatario", document.getElementById('<%=txtParaEmail.ClientID%>').value);
                formData.append("qAssunto", document.getElementById('<%=txtAssuntoEmail.ClientID%>').value);
                formData.append("qCorpo", document.getElementById('<%=txtCorpoEmail.ClientID%>').value);
                //formData.append("pasta_to", pasta_to);
                //formData.append("arquivo", file);
               
                $.ajax({
                url: "wsSapiens.asmx/fEnviaEmailCertificado",
                data: formData,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                success: function(json)
                {
                    if (json[0].P0 == "deslogado") {
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Envio de e-mail de Certificados';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro no envio de e-mail de Certificados. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    } else if (json[0].P0 == "nok") {
                        $('#divModalEnviarEmail').modal('hide');
                        fPreencheTurmaCertificado();
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Envio de e-mail de Certificados';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Problema no envio de e-mail.<br><br>Pessoa(s) com e-mail(s) não enviado(s):<br><strong>' + json[0].P1 + '</strong>';
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $("#divCabecalho").removeClass("alert-danger");
                        $('#divCabecalho').addClass('alert-warning');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalEnviarEmail').modal('hide');
                        fPreencheTurmaCertificado();
                        //alert(json[0].P1);
                           
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-2x',
                            title: '<strong> E-mail enviado</strong><br /><br />',
                            message: 'E-mail enviado com sucesso.<br />',

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
                    alert("Houve um erro no envio do E-mail dos certificados. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando();
                },
                failure: function () 
                {
                    alert("Houve um erro no envio do E-mail dos certificados. Por favor tente novamente!");
                    fFechaProcessando();
                }
            });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //============================================================================
        
        function fEditarGrupo(qNomeGrupo) {
            fProcessando();
            try {
                var qPermissao;
                
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fEditarGrupo?qIdGrupo=" + document.getElementById('hCodigo').value + "&qPermissao=" + qPermissao,
                    dataType: "json",
                    success: function(json)
                    {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Edição do Tipo de Acesso';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro do Tipo de Acesso: ' + qNomeGrupo + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            $('#divModalEditarGrupo').modal('hide');
                            fPreencheTurmaCertificado();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Edição do Tipo de Acesso</strong><br /><br />',
                                message: 'Edição do Tipo de Acesso ' + qNomeGrupo + ' realizada com sucesso.<br />',

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
                        $('#divModalEditarGrupo').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na edição do Grupo. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalEditarGrupo').modal('hide');
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na edição da Grupo. Por favor tente novamente!");
                        $('#divModalEditarGrupo').modal('hide');
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //============================================================================

        function fAssociarGrupo() {
            if (!document.getElementById('optEscritaCertificado').checked) {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Associação de Grupo';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Deve-se selecionar o Tipo de Acesso.';
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            var qPermissao;
            
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fAssociarGrupo?qIdGrupo=" + document.getElementById('hCodigo').value + "&qPermissao=" + qPermissao,
                    dataType: "json",
                    success: function(json)
                    {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Associação de Grupo';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na associação do Grupo: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            $('#divModalEditarGrupo').modal('hide');
                            $('#divModalAssociarGrupo').modal('hide');
                            fPreencheTurmaCertificado();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Associação de Grupo</strong><br /><br />',
                                message: 'Associação da Grupo realizada com sucesso.<br />',

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
                        $('#divModalExcluirGrupo').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão da Grupo. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão da Grupo. Por favor tente novamente!");
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }


        //============================================================================

        function fApagarGrupo() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fApagarGrupo?qIdGrupo=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Desassociação de Grupo';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na desassociação do Grupo: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            $('#divModalEditarGrupo').modal('hide');
                            fPreencheTurmaCertificado();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Desassociação de Grupo</strong><br /><br />',
                                message: 'Desassociação do Grupo realizada com sucesso.<br />',

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
                        $('#divModalEditarGrupo').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na desassociação do Grupo. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalEditarGrupo').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na desassociação da Grupo. Por favor tente novamente!");
                        $('#divModalEditarGrupo').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }


        //============================================================================

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalAssociarGrupo').is(':visible')) {
                    fPerquisaGrupoDisponivel();
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

        function fModalEditarGrupo(qNomeGrupo, qIdGrupo, qTipoAcesso) {
            $("#divCabecGrupo").removeClass("bg-red");
            $("#divCabecGrupo").removeClass("bg-yellow");
            $("#divCabecGrupo").removeClass("bg-primary");
            $('#divCabecGrupo').addClass('bg-primary');

            $("#iconeGrupo").removeClass("fa-edit");
            $("#iconeGrupo").removeClass("fa-plus-square");
            $("#iconeGrupo").removeClass("fa-eraser");
            $('#iconeGrupo').addClass('fa-edit');

            document.getElementById('bntEditarGrupo').style.display = "block"
            document.getElementById('bntAssociarGrupo').style.display = "none"
            document.getElementById('bntApagarGrupo').style.display = "none"

            document.getElementById('lblTituloGrupo').innerHTML = "Edição do Tipo de Acesso";

            document.getElementById('txtNomeGrupoEditar').value = qNomeGrupo;
            document.getElementById('hCodigo').value = qIdGrupo;
            
            fSelect2();
            document.getElementById('bntEditarGrupo').onclick = function(){fEditarGrupo(qNomeGrupo)};
            $('#divModalEditarGrupo').modal();
        }

        function fAbreModalIncluiGrupo(qIdGrupo, qNome) {
            $("#divCabecGrupo").removeClass("bg-red");
            $("#divCabecGrupo").removeClass("bg-yellow");
            $("#divCabecGrupo").removeClass("bg-primary");
            $('#divCabecGrupo').addClass('bg-yellow');

            $("#iconeGrupo").removeClass("fa-edit");
            $("#iconeGrupo").removeClass("fa-plus-square");
            $("#iconeGrupo").removeClass("fa-eraser");
            $('#iconeGrupo').addClass('fa-plus-square');

            document.getElementById('bntEditarGrupo').style.display = "none"
            document.getElementById('bntAssociarGrupo').style.display = "block"
            document.getElementById('bntApagarGrupo').style.display = "none"

            document.getElementById('lblTituloGrupo').innerHTML = "Associação de Grupo";

            document.getElementById('txtNomeGrupoEditar').value = qNome;
            document.getElementById('hCodigo').value = qIdGrupo;
            fSelect2();
            $('#divModalEditarGrupo').modal();
        }

        function fModalExcluirGrupo(qNomeGrupo, qIdGrupo, qTipoAcesso) {
            $("#divCabecGrupo").removeClass("bg-red");
            $("#divCabecGrupo").removeClass("bg-yellow");
            $("#divCabecGrupo").removeClass("bg-primary");
            $('#divCabecGrupo').addClass('bg-red');

            $("#iconeGrupo").removeClass("fa-edit");
            $("#iconeGrupo").removeClass("fa-plus-square");
            $("#iconeGrupo").removeClass("fa-eraser");
            $('#iconeGrupo').addClass('fa-eraser');

            document.getElementById('bntEditarGrupo').style.display = "none"
            document.getElementById('bntAssociarGrupo').style.display = "none"
            document.getElementById('bntApagarGrupo').style.display = "block"

            document.getElementById('lblTituloGrupo').innerHTML = "Desassociação de Grupo";

            document.getElementById('txtNomeGrupoEditar').value = qNomeGrupo;
            document.getElementById('hCodigo').value = qIdGrupo;
            
            fSelect2();
            $('#divModalEditarGrupo').modal();
        }

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
