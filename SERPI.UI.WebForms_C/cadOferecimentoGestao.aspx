<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadOferecimentoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadOferecimentoGestao" validateRequest="false" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li8Oferecimentos" />

    <script src="Scripts/jquery.mask.min.js"></script>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
    
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="" />
    <input type="hidden" id ="hCodigoDisciplina"  name="hCodigoDisciplina" value="" />
    <input type="hidden" id ="hMinimoData"  name="hMinimoData" value="" />

    <input type="hidden" id ="htxtObjetivoOferecimento"  name="htxtObjetivoOferecimento" value="" />
    <input type="hidden" id ="htxtJustificativaOferecimento"  name="htxtJustificativaOferecimento" value="" />
    <input type="hidden" id ="htxtEmentaOferecimento"  name="htxtEmentaOferecimento" value="" />
    <input type="hidden" id ="htxtFormaAvaliacaoOferecimento"  name="htxtFormaAvaliacaoOferecimento" value="" />
    <input type="hidden" id ="htxtMaterialUtilizadoOferecimento"  name="htxtMaterialUtilizadoOferecimento" value="" />
    <input type="hidden" id ="htxtMetodologiaOferecimento"  name="htxtMetodologiaOferecimento" value="" />
    <input type="hidden" id ="htxtConhecimentosPreviosOferecimento"  name="htxtConhecimentosPreviosOferecimento" value="" />
    <input type="hidden" id ="htxtProgramaOferecimento"  name="htxtProgramaOferecimento" value="" />
    <input type="hidden" id ="htxtBibliografiaBasicaOferecimento"  name="htxtBibliografiaBasicaOferecimento" value="" />
    <input type="hidden" id ="htxtBibliografiaComplementarOferecimento"  name="htxtBibliografiaComplementarOferecimento" value="" />
    <input type="hidden" id ="htxtObservacaoOferecimento"  name="htxtObservacaoOferecimento" value="" />
    <%--<input type="hidden" id ="hTituloPagina"  name="hTituloPagina" value="Aluno (Listagem)" />--%>

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/minimal/blue.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>

    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet" />
      
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

            .nicEdit-main {
            overflow: auto !important;
            height: 5.5em;
            }

            .negrito
            {
                font-weight: bold !important;
            }

            #grdDatasAula td.centralizarTH {
                vertical-align: middle;  
            }

            #grdAlunoDisponivel td.centralizarTH {
                vertical-align: middle;  
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
    <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel1"  >
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

    <asp:UpdateProgress ID="UpdateProgress5" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel5"  >
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
        <div class="col-md-5">
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong>Oferecimento</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label><asp:Label ID="lblInativadoOferecimento" ForeColor="Red" runat="server" Text=" (Inativada)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-1">
            <br />
            
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarOferecimento" name="btnCriarOferecimento" onserverclick="btnCriarOferecimento_Click" class="btn btn-primary" href="#" onclick=""  > <%--onserverclick="btnCriarOferecimento_Click"--%>
                        <i class="fa fa-magic"></i>&nbsp;Criar Oferecimento</button>
            </div>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            
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

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span>Tipo Curso </span></span><span style="color:red;">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTipoCursoOferecimento" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoOferecimento_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span>Curso </span></span><span style="color:red;">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlCursoOferecimento" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCursoOferecimento_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Período </span></span><span style="color:red;">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlPeriodoOferecimento" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodoOferecimento_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>N.º Oferecimento </span><span style="color:red;">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtNumeroOferecimanto" type="text" value="" readonly="true"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <br />
                                            <button type="button" runat="server" id="btnSelecionarDisciplina" class="btn btn-warning" href="#" onclick="fSelecionarDisciplina()"> 
                                            <i class="fa fa-search"></i>&nbsp;Selecionar Disciplina</button>
                                        </div>

                                    </div>
                                
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <span>Código Disciplina</span><span style="color:red;">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtCodigoDisciplinaOferecimento" type="text" value="" maxlength="7" readonly="true"/>
                                            <input class="form-control input-sm" runat="server" id="txtIdDisciplinaOferecimento" type="text" value="" maxlength="7" readonly="true" style="display:none"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-6 ">
                                            <span>Nome Disciplina</span><span style="color:red;">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtNomeDisciplinaOferecimento" type="text" value="" maxlength="150" readonly="true"/>
                                        </div>

                                        <button style="display:none" type="button" runat="server" id="btnIncluiDisciplina" class="btn btn-warning" href="#" onserverclick="btnIncluiDisciplina_Click"> 
                                            <i class="fa fa-search"></i>&nbsp;Inclui Disciplina</button>

                                    </div>
                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoOferecimento" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCursoOferecimento" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlPeriodoOferecimento" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li id="tabDetalheOferecimento" class="active"><a href="#tab_DetalheOferecimento" id="atab_DetalheOferecimento" data-toggle="tab"><strong>Detalhe</strong></a></li>
                <li id="tabDataAulaOferecimento" runat="server" style="display:none"><a href="#tab_DataAulaOferecimento" id="atab_DataAulaOferecimento" data-toggle="tab"><strong>Datas de Aula <small class="text-primary">(presenças)</small></strong></a></li>
                <li id="tabMatriculaOferecimento" runat="server" style="display:none"><a href="#tab_MatriculaOferecimento" id="atab_MatriculaOferecimento" data-toggle="tab"><strong>Alunos Matriculados <small class="text-primary">(notas)</small></strong></a></li>
            </ul>

            <br />

            <div class="tab-content">
                <div class="tab-pane active" id="tab_DetalheOferecimento">
                    <%--                                        <b>How to use:</b>--%>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-4">
                                    <h3 class="box-title">Detalhe do Oferecimento</h3>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br /> 
                                </div>

                                <div class="col-md-4">
                                    <button type="button" runat="server" id="btnInativar" class="btn btn-danger" href="#" onclick="fModalAtivaInativa('Inativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                                        <i class="fa fa-toggle-off"></i> Inativar Oferecimento
                                    </button>
                                    <button type="button" runat="server" id="btnAtivar" class="btn btn-info" href="#" onclick="fModalAtivaInativa('Ativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                                        <i class="fa fa-toggle-on"></i> Ativar Oferecimento
                                    </button>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br /> 
                                </div>

                                <div class="col-md-4 ">
                                    <button type="button" runat="server" id="btnImprimirEmenta" class="btn btn-warning" href="#" onclick="if (fPreparaRelatorio('O relatório da Ementa do Oferecimento está sendo preparado.')) return;" onserverclick="btnImprimirEmenta_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
                                        <i class="fa fa-print"></i>&nbsp;Imprimir Ementa</button>
                                </div>

                            </div>
                                                
                        </div>

                        <div class="box-body">
                            <%--Dados do Oferecimento - Início--%>
                            <div class="row well">


                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#tab_DadosOferecimento" data-toggle="tab" aria-expanded="true"><strong>Dados do Oferecimento</strong></a></li>
                                        <li class="" id="tabProfessoresOferecimento" runat="server"><a href="#tab_ProfessoresOferecimento" data-toggle="tab" aria-expanded="false"><strong>Professores</strong></a></li>
                                        <li class="" id="tabTecnicoOferecimento" runat="server"><a href="#tab_TecnicoOferecimento" data-toggle="tab" aria-expanded="false"><strong>Técnicos/Monitores</strong></a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_DadosOferecimento">
                                            
                                            <div class="row">
                                                <div class="col-md-4 pull-right">
                                                    <button type="button" runat="server" id="bntSalvar2" name="bntSalvar2" class="btn btn-success pull-right hidden" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvar_ServerClick1">
                                                                    <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
                                                    <button type="button" class="btn btn-success pull-right cBotaoSalvar" onclick="fbtnSalvar()">
                                                                    <i class="fa fa-save"></i>&nbsp;&nbsp;Salvar Dados do Oferecimento</button>
                                                </div>
                                                <div class="hidden-lg hidden-md">
                                                    <br /> 
                                                </div>

                                                <div class="col-md-8">
                                                    <h5 class="box-title text-bold">Dados do Oferecimento</h5>
                                                </div>
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br /> 
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-md-2 ">
                                                            <span>N.º Máx Alunos</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtNumeroMaxAlunosOferecimento" type="number" value="" min="1" max="99" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Créditos </span><span style="color: red;">*</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCreditosOferecimento" type="number" value="" min="1" max="99" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <span>Carga Horária </span><span style="color: red;">*</span><br />
                                                            <input class="form-control input-sm" runat="server" id="txtCargaHorariaOferecimento" type="number" value="" min="1" max="99" />
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                        <div style="line-height: 1.0em;">
                                                            <div class="col-md-1">
                                                                &nbsp;<br />

                                                                <label class="checkbox_only" style="cursor: pointer">
                                                                    <input type="checkbox" id="chkAtivoOferecimento_new" runat="server" />
                                                                    <span></span>
                                                                    Ativo
                                                                </label>
                                                            </div>
                                                        </div>

                                                        <div id="divUrl" runat="server">
                                                            <div class="hidden-lg hidden-md">
                                                                <br />
                                                            </div>

                                                            <div class="col-md-3">
                                                                <span>URL da Ementa</span><br />
                                                                <input class="form-control input-sm" runat="server" id="txtUrlEmenta" type="text" value="" readonly="true" />
                                                            </div>
                                                            <div class="hidden-lg hidden-md">
                                                                <br />
                                                            </div>

                                                            <div class="col-md-2">
                                                                <br />
                                                                <button type="button" id="btnCopyURL" class="btn btn-default center-block" href="#" onclick="fCopyUrl()">
                                                                    <i class="fa fa-copy"></i>&nbsp;Copiar Link</button>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />

                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="tab-content">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">

                                                                        <div class="row">
                                                                            <div class="col-md-6 ">
                                                                                <span>Objetivos</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtObjetivoOferecimento"></textarea>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-6 ">
                                                                                <span>Justificativa</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtJustificativaOferecimento"></textarea>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-6 ">
                                                                                <span>Ementa </span><span style="color: red;">*</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtEmentaOferecimento" htmlencode="true"></textarea>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-6 ">
                                                                                <span>Forma de Avaliação</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtFormaAvaliacaoOferecimento" htmlencode="false"></textarea>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-6 ">
                                                                                <span>Material Utilizado</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtMaterialUtilizadoOferecimento" htmlencode="false"></textarea>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-6 ">
                                                                                <span>Metodologia</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtMetodologiaOferecimento" htmlencode="false"></textarea>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-6 ">
                                                                                <span>Conhecimentos Prévios</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtConhecimentosPreviosOferecimento" htmlencode="false"></textarea>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-6 ">
                                                                                <span>Programa do Oferecimento</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtProgramaOferecimento" htmlencode="false"></textarea>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-6 ">
                                                                                <span>Bibliografia Básica </span><span style="color: red;">*</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtBibliografiaBasicaOferecimento" htmlencode="false"></textarea>
                                                                            </div>
                                                                            <div class="hidden-lg hidden-md">
                                                                                <br />
                                                                            </div>

                                                                            <div class="col-md-6 ">
                                                                                <span>Bibliografia Complementar</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtBibliografiaComplementarOferecimento" htmlencode="false"></textarea>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="row">
                                                                            <div class="col-md-6 ">
                                                                                <span>Observação</span><br />
                                                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtObservacaoOferecimento" htmlencode="false"></textarea>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="pull-right">
                                                        <div class="col-md-12">
                                                            <button type="button" runat="server" id="bntSalvarNoticia" name="bntSalvar" class="btn btn-success pull-right hidden" href="#" onclick="if (fAProcessando()) return;" onserverclick="btnSalvar_ServerClick1">
                                                                            <i class="fa fa-floppy-o fa-lg"></i>&nbsp;Salvar Dados do Oferecimento</button>
                                                            <button type="button" class="btn btn-success pull-right cBotaoSalvar" onclick="fbtnSalvar()">
                                                                            <i class="fa fa-save fa-lg"></i>&nbsp;&nbsp;Salvar Dados do Oferecimento</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane" id="tab_ProfessoresOferecimento">
                                            <!-- Sessão Professor -->
                                            <div class="tab-content" id="divProfessor" runat="server">

                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <h5 class="box-title text-bold">Professores Adicionados</h5>

                                                            <div class="tab-content">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="grid-content">

                                                                                            <div id="msgSemResultadosProfessor" style="display: block">
                                                                                                <div class="alert bg-gray">
                                                                                                    <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Professor associado." />
                                                                                                </div>
                                                                                            </div>

                                                                                            <div id="divgrdProfessor" class="table-responsive" style="display: none">
                                                                                                <div class="scroll">
                                                                                                    <table id="grdProfessor" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-3 pull-right">
                                                                    <button type="button" id="btnAssociarProfessor" name="btnAssociarProfessor" class="btn btn-warning pull-right" href="#" onclick="fModalAssociarProfessor()">
                                                                        <i class="fa fa-user-plus"></i>&nbsp;Incluir Professor</button>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="tab-pane" id="tab_TecnicoOferecimento">
                                            <!-- Sessão Técnico -->
                                            <div class="tab-content" id="divTecnico" runat="server">

                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <h5 class="box-title text-bold">Técnicos/Monitores Adicionados</h5>

                                                            <div class="tab-content">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12 ">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="grid-content">

                                                                                            <div id="msgSemResultadosTecnico" style="display: block">
                                                                                                <div class="alert bg-gray">
                                                                                                    <asp:Label runat="server" ID="Label6" Text="Nenhum Técnico/Monitor associado." />
                                                                                                </div>
                                                                                            </div>
                                                                                            <div id="divgrdTecnico" class="table-responsive" style="display: none">
                                                                                                <div class="scroll">
                                                                                                    <table id="grdTecnico" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-3 pull-right">
                                                                    <button type="button" id="btnAssociarTecnico" name="btnAssociarTecnico" class="btn btn-warning pull-right" onclick="fModalAssociarTecnico()">
                                                                        <i class="fa fa-user-plus"></i>&nbsp;Incluir Técnico/Monitor</button>
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
                            <%--Dados Pessoais - Fim--%>

                        </div>
                    </div>
                </div>

            <%--==========================================================================================================================================--%>

                <div class="tab-pane" id="tab_DataAulaOferecimento">
                    <input runat="server" id="txtIdTurma" type="text" value="" visible="false" />
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-4">
                                    <h3 class="box-title">Datas de Aula</h3>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br /> 
                                </div>

                                <div class="col-md-4 center-block">
                                    <button type="button" runat="server" id="btnImprimirPresencaProfessor" class="btn btn-default center-block" href="#"  onclick="if (fPreparaRelatorio('O relatório de Presença do Professor desse Oferecimento está sendo preparado.')) return;"  onserverclick="btnImprimirPresencaProfessor_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
                                        <i class="fa fa-print"></i>&nbsp;Imprimir Presença Professor
                                    </button>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br /> 
                                </div>

                                <div class="col-md-4 pull-right">
                                    <button type="button" runat="server" id="btnImprimirPresencaAluno" class="btn btn-warning pull-right" href="#"  onclick="if (fPreparaRelatorio('O relatório de Presença dos Alunos desse Oferecimento está sendo preparado.')) return;"  onserverclick="btnImprimirPresencaAluno_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
                                        <i class="fa fa-print"></i>&nbsp;Imprimir Presença Alunos
                                    </button>
                                </div>

                            </div>
                            
                        </div>
                        <div class="box-body">
                            <div class="row well">

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Professor Responsável pelo Oferecimento</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtProfessorResponsavelOferecimento" type="text" value="" readonly="readonly" />
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-3 ">
                                                <span>Duração do Oferecimento</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtDuracaoOferecimento" type="text" value="" readonly="readonly"  />
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-3 ">
                                                <span>Total Horas Aulas</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtTotalHorasAulas" type="text" value="" readonly="readonly" />
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-3 ">
                                                <span>Total Horas Professor</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtHorasProfessor" type="text" value="" readonly="readonly" />
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-3 ">
                                                <span>Total Horas Técnico/Monitor</span><br />
                                                <input class="form-control input-sm" runat="server" id="txtHorasTecnico" type="text" value="" readonly="readonly" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <br />

                                <div class="row">
                                    <div class="col-md-12">
                                        <h5 class="box-title text-bold">Datas de Aula Cadastradas</h5>
                            
                                        <div class="tab-content">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12 ">
                                                            <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="grid-content">
                                                 
                                                                            <div id="msgSemResultadosDatasAula" style="display:block">
                                                                                <div class="alert bg-gray"> 
                                                                                    <asp:Label runat="server" ID="Label2" Text="Nenhuma Data de Aula cadastrada." />
                                                                                </div>
                                                                            </div>
                                                                            <div id="divgrdDatasAula" class="table-responsive" style="display:none">
                                                                                <div class="scroll">
                                                                                    <table id="grdDatasAula" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                                        <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                            <%--<tr>
                                                                                                <th>id</th>
                                                                                                <th>Sequencia</th>
                                                                                                <th>Data</th>
                                                                                                <th>Hora Início</th>
                                                                                                <th>Hora Fim</th>
                                                                                                <th>Sala</th>
                                                                                                <th>Equipe</th>
                                                                                                <th>Presença</th>
                                                                                                <th>Editar</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                            </tr>--%>
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
                            
                                        <div class="row">
                                            <div class="col-md-3 pull-right">
                                                <button type="button" id="btnAdicionarDataAula" name="btnAdicionarDataAula" class="btn btn-warning pull-right" href="#" onclick="fModalAdicionarDataAula()">
                                                    <i class="fa fa-calendar-plus-o"></i>&nbsp;Incluir Data Aula</button>
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

             <%--==========================================================================================================================================--%>

                <div class="tab-pane" id="tab_MatriculaOferecimento">
                    <input runat="server" id="Text1" type="text" value="" visible="false" />
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-9">
                                    <h3 class="box-title">Alunos Matriculados nesse Oferecimento</h3>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br /> 
                                </div>

                                <div class="col-md-3">
                                    <button type="button" runat="server" id="btnImprimirNotasAlunos" class="btn btn-warning" href="#" onclick="if (fPreparaRelatorio('O relatório com a Nota dos Alunos está sendo preparado.')) return;"  onserverclick="btnImprimirNotasAlunos_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
                                        <i class="fa fa-print"></i>&nbsp;Imprimir Nota dos Alunos</button>
                                </div>

                            </div>
                            
                        </div>
                        <div class="box-body">
                            <div class="row well">

                                <div class="row">
                                    <div class="col-md-12">
                            
                                        <div class="tab-content">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12 ">
                                                            <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="grid-content">
                                                 
                                                                            <div id="msgSemResultadosMatriculaOferecimento" style="display:block">
                                                                                <div class="alert bg-gray"> 
                                                                                    <asp:Label runat="server" ID="Label3" Text="Nenhum Aluno matriculado nesse Oferecimento." />
                                                                                </div>
                                                                            </div>
                                                                            <div id="divgrdMatriculaOferecimento" class="table-responsive" style="display:none">
                                                                                <div class="scroll">
                                                                                    <table id="grdMatriculaOferecimento" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                                        <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                            <%--<tr>
                                                                                                <th>id</th>
                                                                                                <th>Sequencia</th>
                                                                                                <th>Data</th>
                                                                                                <th>Hora Início</th>
                                                                                                <th>Hora Fim</th>
                                                                                                <th>Sala</th>
                                                                                                <th>Equipe</th>
                                                                                                <th>Presença</th>
                                                                                                <th>Editar</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                                <th>Excluir</th>
                                                                                            </tr>--%>
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
                            
                                        <div class="row">
                                            <div class="col-md-3 pull-right">
                                                <button type="button" id="btnAdicionarMatriculaOferecimento" name="btnAdicionarMatriculaOferecimento" class="btn btn-warning pull-right" href="#" onclick="fModalAdicionarMatriculaOferecimento()">
                                                    <i class="fa fa-user-plus"></i>&nbsp;Matricular Aluno</button>
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
        

        <div class="row">

            <div class="col-xs-2">
                <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>
        </div>
    </div>

    <!-- Modal para Associar Professor -->
    <div class="modal fade" id="divModalAssociarProfessor" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Associar Professor</h4>
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
                                            <input class="form-control input-sm" id="txtCPFProfessor" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeProfessor" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaProfessorDisponivelOferecimento()" >
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
                                                <div id="msgSemResultadosgrdProfessorDisponivel" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <asp:Label runat="server" ID="Label5" Text="Nenhum Professor disponível encontrado" />
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdProfessorDisponivel" >
                                                    <div class="scroll">
                                                        <table id="grdProfessorDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

        <!-- Modal para Excluir Professor -->
    <div class="modal fade" id="divModalExcluirProfessor" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Professor</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir o Professor: <label id="lblNomeProfessor"></label> - CPF: <label id="lblCPFProfessor"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirProfessor" type="button" name="bntExcluirProfessor" title="" class="btn btn-success" onclick="fExcluiProfessorOferecimento()" >
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

        <!-- Modal para Associar Tecnico -->
    <div class="modal fade" id="divModalAssociarTecnico" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Associar Técnico/Monitor</h4>
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
                                            <input class="form-control input-sm" id="txtCPFTecnico" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeTecnico" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaTecnicoDisponivelOferecimento()" >
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
                                            <div id="msgSemResultadosgrdTecnicoDisponivel" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label7" Text="Nenhum Técnico/Monitor disponível encontrado" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdTecnicoDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdTecnicoDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

        <!-- Modal para Excluir Tecnico -->
    <div class="modal fade" id="divModalExcluirTecnico" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Técnico/Monitor</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir o Técnico/Monitor: <label id="lblNomeTecnico"></label> - CPF: <label id="lblCPFTecnico"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirTecnico" type="button" name="bntExcluirTecnico" title="" class="btn btn-success" onclick="fExcluiTecnicoOferecimento()" >
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

    <!-- Modal para Adicionar Disciplina -->
    <div class="modal fade" id="divModalAssociarDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-hand-pointer-o"></i>&nbsp;Selecionar Disciplina</h4>
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

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaDisciplinaDisponivelOferecimento()" >
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
                                                    <asp:Label runat="server" ID="Label1" Text="Nenhuma Disciplina encontrada" />
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

    <!-- Modal para Ativar/Inativar Oferecimento -->
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarOferecimento('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarOferecimento('Inativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Editar/Excluir Aula -->
    <div class="modal fade" id="divModalEditarAula" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div id="divCabecModalEditarAula" class="modal-header">
                    <h4 class="modal-title"><label id="lblTituloModalEditarAula"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        
                        <div id="divComPresenca">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color:red">ATENÇÃO: Existem presenças de alunos cadastradas nessa aula. Caso se confirme a exclusão dessa aula essas presenças de alunos serão excluídas também.</h4>
                                </div>
                            </div>
                            <br />
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <span>Data Aula</span><br />
                                <input class="form-control input-sm" id="txtDataAulaModalEditarAula" type="date" value="" />
                                <input class="form-control input-sm" id="txtIdAula" type="text" value="" style="display:none" />
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-2">
                                <span>Hora Início</span><br />
                                <div style="display:inline-block">
                                <select id="ddlHoraInicioAulaModalEditarAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="01">01</option>
                                    <option value="02">02</option>
                                    <option value="03">03</option>
                                    <option value="04">04</option>
                                    <option value="05">05</option>
                                    <option value="06">06</option>
                                    <option value="07">07</option>
                                    <option value="08">08</option>
                                    <option value="09">09</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option value="23">23</option>
                                </select>
                                </div>
                                <div style="display:inline-block">
                                <select id="ddlMinutoInicioAulaModalEditarAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="05">05</option>
                                    <option value="10">10</option>
                                    <option value="15">15</option>
                                    <option value="20">20</option>
                                    <option value="25">25</option>
                                    <option value="30">30</option>
                                    <option value="35">35</option>
                                    <option value="40">40</option>
                                    <option value="45">45</option>
                                    <option value="50">50</option>
                                    <option value="55">55</option>
                                </select>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-2">
                                <span>Hora Fim</span><br />
                                <div style="display:inline-block">
                                <select id="ddlHoraFimAulaModalEditarAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="01">01</option>
                                    <option value="02">02</option>
                                    <option value="03">03</option>
                                    <option value="04">04</option>
                                    <option value="05">05</option>
                                    <option value="06">06</option>
                                    <option value="07">07</option>
                                    <option value="08">08</option>
                                    <option value="09">09</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option value="23">23</option>
                                </select>
                                </div>
                                <div style="display:inline-block">
                                <select id="ddlMinutoFimAulaModalEditarAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="05">05</option>
                                    <option value="10">10</option>
                                    <option value="15">15</option>
                                    <option value="20">20</option>
                                    <option value="25">25</option>
                                    <option value="30">30</option>
                                    <option value="35">35</option>
                                    <option value="40">40</option>
                                    <option value="45">45</option>
                                    <option value="50">50</option>
                                    <option value="55">55</option>
                                </select>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-5">
                                <span>Sala</span><br />
                                <asp:DropDownList runat="server" ID="ddlSalaAula" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                </asp:DropDownList>
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
                            <button id="btnConfirmaAlteracaoModalEditarAula" type="button" class="btn btn-success pull-right" onclick="fEditarAula()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaExclusaoModalEditarAula" type="button" class="btn btn-success pull-right" onclick="fExcluirAula()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Incluir Aula -->
    <div class="modal fade" id="divModalIncluirAula" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div id="divCabecModalIncluirAula" class="modal-header bg-yellow">
                    <h4 class="modal-title"><i class="fa fa-calendar-plus-o"></i> Incluir Aula</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-3">
                                <span>Data Aula</span><br />
                                <input class="form-control input-sm" id="txtDataAulaModalIncluirAula" type="date" value="" />
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-2">
                                <span>Hora Início</span><br />
                                <div style="display:inline-block">
                                <select id="ddlHoraInicioAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="01">01</option>
                                    <option value="02">02</option>
                                    <option value="03">03</option>
                                    <option value="04">04</option>
                                    <option value="05">05</option>
                                    <option value="06">06</option>
                                    <option value="07">07</option>
                                    <option value="08">08</option>
                                    <option value="09">09</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option selected value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option value="23">23</option>
                                </select>
                                </div>
                                <div style="display:inline-block">
                                <select id="ddlMinutoInicioAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option selected="selected" value="00">00</option>
                                    <option value="05">05</option>
                                    <option value="10">10</option>
                                    <option value="15">15</option>
                                    <option value="20">20</option>
                                    <option value="25">25</option>
                                    <option value="30">30</option>
                                    <option value="35">35</option>
                                    <option value="40">40</option>
                                    <option value="45">45</option>
                                    <option value="50">50</option>
                                    <option value="55">55</option>
                                </select>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-2">
                                <span>Hora Fim</span><br />
                                <div style="display:inline-block">
                                <select id="ddlHoraFimAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="01">01</option>
                                    <option value="02">02</option>
                                    <option value="03">03</option>
                                    <option value="04">04</option>
                                    <option value="05">05</option>
                                    <option value="06">06</option>
                                    <option value="07">07</option>
                                    <option value="08">08</option>
                                    <option value="09">09</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option selected="selected" value="23">23</option>
                                </select>
                                </div>
                                <div style="display:inline-block">
                                <select id="ddlMinutoFimAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option selected value="00">00</option>
                                    <option value="05">05</option>
                                    <option value="10">10</option>
                                    <option value="15">15</option>
                                    <option value="20">20</option>
                                    <option value="25">25</option>
                                    <option value="30">30</option>
                                    <option value="35">35</option>
                                    <option value="40">40</option>
                                    <option value="45">45</option>
                                    <option value="50">50</option>
                                    <option value="55">55</option>
                                </select>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-5">
                                <span>Sala</span><br />
                                <asp:DropDownList runat="server" ID="ddlSalaAulaIncluirAula" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-8">
                                <span>Professor</span><br />
                                <asp:DropDownList runat="server" ID="ddlProfessorIncluirAula" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-4">
                                <span>Horas gastas</span><br />
                                <div style="display:inline-block">
                                <select id="ddlHoraProfessorIncluirAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="01">01</option>
                                    <option value="02">02</option>
                                    <option value="03">03</option>
                                    <option selected value="04">04</option>
                                    <option value="05">05</option>
                                    <option value="06">06</option>
                                    <option value="07">07</option>
                                    <option value="08">08</option>
                                    <option value="09">09</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option value="23">23</option>
                                </select>
                                </div>
                                <div style="display:inline-block">
                                <select id="ddlMinutoProfessorIncluirAula" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option selected value="00">00</option>
                                    <option value="05">05</option>
                                    <option value="10">10</option>
                                    <option value="15">15</option>
                                    <option value="20">20</option>
                                    <option value="25">25</option>
                                    <option value="30">30</option>
                                    <option value="35">35</option>
                                    <option value="40">40</option>
                                    <option value="45">45</option>
                                    <option value="50">50</option>
                                    <option value="55">55</option>
                                </select>
                                </div>
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
                            <button id="btnConfirmaModalIncluirAula" type="button" class="btn btn-success pull-right" onclick="fIncluirAula()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Editar/Apagar Professor -->
    <div class="modal fade" id="divModalEquipe" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div id="divCabecModalEquipe" class="modal-header bg-yellow">
                    <h4 class="modal-title"><label id="lblTituloEquipe"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-8">
                                <span><label id="lblProfessorTecnicoEquipe"></label></span><br />
                                <input class="form-control input-sm" id="txtNomeEquipe" type="text" value="" readonly="readonly"/>
                                <input class="form-control input-sm" id="txtIdProfessorEquipe" type="text" value="" readonly="readonly" style="display:none"/>
                                <input class="form-control input-sm" id="txtIdAulaEquipe" type="text" value="" readonly="readonly" style="display:none"/>
                                <input class="form-control input-sm" id="txtTipoEquipe" type="text" value="" readonly="readonly" style="display:none"/>
                                <asp:DropDownList runat="server" ID="ddlNomeEquipe" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-4">
                                <span>Horas gastas</span><br />
                                <div style="display:inline-block">
                                <select id="ddlHoraEquipe" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option value="00">00</option>
                                    <option value="01">01</option>
                                    <option value="02">02</option>
                                    <option value="03">03</option>
                                    <option selected value="04">04</option>
                                    <option value="05">05</option>
                                    <option value="06">06</option>
                                    <option value="07">07</option>
                                    <option value="08">08</option>
                                    <option value="09">09</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option value="23">23</option>
                                </select>
                                </div>
                                <div style="display:inline-block">
                                <select id="ddlMinutoEquipe" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                    <option selected value="00">00</option>
                                    <option value="05">05</option>
                                    <option value="10">10</option>
                                    <option value="15">15</option>
                                    <option value="20">20</option>
                                    <option value="25">25</option>
                                    <option value="30">30</option>
                                    <option value="35">35</option>
                                    <option value="40">40</option>
                                    <option value="45">45</option>
                                    <option value="50">50</option>
                                    <option value="55">55</option>
                                </select>
                                </div>
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
                            <button id="btnConfirmaModalExcluirEquipe" type="button" class="btn btn-success pull-right" onclick="fExcluirEquipe()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaModalAlterarHoraEquipe" type="button" class="btn btn-success pull-right" onclick="fAlterarHoraEquipe()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaModalIncluirProfessorEquipe" type="button" class="btn btn-success pull-right" onclick="fIncluirEquipeOferecimento()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaModalIncluirTecnicoEquipe" type="button" class="btn btn-success pull-right" onclick="fIncluirEquipeOferecimento()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Matricular Aluno -->
    <div class="modal fade" id="divModalAlunosOferecimento" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;&nbsp;Matricular Alunos nesse Oferecimento</h4>
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
                                            <span>Matrícula</span><br />
                                            <input class="form-control input-sm" onchange="fLimpaGridMatriculaAluno()" id="txtMatriculaAluno" type="number" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" onchange="fLimpaGridMatriculaAluno()" id="txtNomeAluno" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaAlunoDisponivelOferecimento()" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>
                                        </div>

                                    </div>
                                    <br />

                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <span>Tipo Curso</span><br />
                                                    <asp:DropDownList runat="server" ID="ddlTipoCursoFiltro" onchange="fMostrarProgresso5()" ClientIDMode="Static" class="ddl_fecha_grid_resultados_MatriculaAluno form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoFiltro_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="hidden-lg hidden-md">
                                                    <br />
                                                </div>

                                                <div class="col-md-6">
                                                    <span>Curso</span><br />
                                                    <asp:DropDownList runat="server" ID="ddlNomeCursoFiltro" onchange="fMostrarProgresso5()" ClientIDMode="Static" class="ddl_fecha_grid_resultados_MatriculaAluno form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlNomeCursoFiltro_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="hidden-lg hidden-md">
                                                    <br />
                                                </div>

                                                <div class="col-md-2">
                                                    <span>Turma</span><br />
                                                    <asp:DropDownList runat="server" ID="ddlTurmaFiltro" ClientIDMode="Static" class="ddl_fecha_grid_resultados_MatriculaAluno form-control input-sm select2" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoFiltro" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlNomeCursoFiltro" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
                                                <div id="msgSemResultadosgrdAlunoDisponivel" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <asp:Label runat="server" ID="Label4" Text="Nenhum Aluno disponível encontrado" />
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdAlunoDisponivel" >
                                                    <div class="scroll">
                                                        <table id="grdAlunoDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para Excluir Matrícula do Aluno -->
    <div class="modal fade" id="divModalExcluirMatricula" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Matrícula de Aluno</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir a matrícula do Aluno: <label class="negrito" id="lblNomeAlunoExcluir"></label> - Matrícula: <label class="negrito" id="lblMatriculaAlunoExcluir"></label>
                    <input id="txtIdMatriculaOferecimentoExcluirMatricula" type="text" class="hidden" />
                    <input id="txtIdAlunoExcluirMatricula" type="text" class="hidden" />
                    <input id="txtIdTurmaExcluirMatricula" type="text" class="hidden" />
                    <input id="txtIdNotaExcluirMatricula" type="text" class="hidden" />
                    <input id="txtNomeAlunoExcluirMatricula" type="text" class="hidden" />
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirMatriculaAluno" type="button" name="bntExcluirMatriculaAluno" title="" class="btn btn-success" onclick="fExcluirMatriculaAluno()" >
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

    <!-- Modal para Editar Nota -->
    <div class="modal fade" id="divModalEditarNota" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-edit"></i> Editar Nota</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        
                        <div class="row">
                            <div class="col-md-3">
                                <span>Matrícula</span><br />
                                <input class="form-control input-sm" id="txtIdAlunoNota" type="text" value="" readonly="readonly"/>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-8">
                                <span>Aluno</span><br />
                                <input class="form-control input-sm" id="txtNomeAlunoNota" type="text" value="" readonly="readonly"/>
                                <input class="form-control input-sm" id="txtIdNota" type="text" value="" style="display:none" />
                            </div>
                            
                        </div>
                        <br />

                        <div class="row"> 
                            <div class="col-md-6">
                                <span>Turma</span><br />
                                <input class="form-control input-sm" id="txtTurmaAlunoNota" type="text" value="" readonly="readonly"/>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-2">
                                <span>Conceito</span><br />

                                <select id="ddlConceitoNota" class="form-control input-sm select2 SemPesquisa">
                                    <option value=""> </option>
                                    <option value="A">A</option>
                                    <option value="B">B</option>
                                    <option value="C">C</option>
                                    <option value="D">D</option>
                                    <option value="E">E</option>
                                    <option value="I">I</option>
                                </select>

                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-3">
                                <span>Autorizado <small>(mostrar nota)</small></span><br />

                                <select id="ddlAutorizadoNota" class="form-control input-sm select2 SemPesquisa">
                                    <option value=""> </option>
                                    <option value="N">Não</option>
                                    <option value="S">Sim</option>
                                </select>

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
                            <button id="btnConfirmaEditarNota" type="button" class="btn btn-success pull-right" onclick="fEditarNota()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Associar Professor -->
    <div class="modal fade" id="divModalPresencaAlunos" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-purple">
                    <h4 class="modal-title"><label id="lblTituloModalPresencaAluno"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div id="divComAluno" >
                            <div class="box box-solid">
                                <!-- /.box-header -->
                                <div class="box-body">
                                  <div class="box-group" id="accordion">
                                    <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                                    <div class="panel box box-primary">
                                      <div class="box-header with-border">
                                        <h4 class="box-title">
                                          <a data-toggle="collapse" data-parent="#accordion" href="#tabGerarLista" aria-expanded="false" class="collapsed">
                                               <i class="fa fa-print"></i> Gerar Listas
                                          </a>
                                        </h4>
                                      </div>
                                      <div id="tabGerarLista" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                        <div class="box-body">
                                            <div class="row">
                                                  <div class="col-md-12"> 
                                                      <span>Mensagem na Lista (opcional)</span><br />
                                                      <textarea style="resize: none;font-size:14px" runat="server" class="form-control input-sm" rows="6" id="txtMensagemLista"></textarea>
                                                  </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6 ">
                                                    <button type="button" id="btnGerarListaPresencaAluno" runat="server" class="btn btn-primary" onclick="if (fPreparaRelatorio('A Lista de Presença dos Alunos está sendo preparada.')) return;" onserverclick="btnGerarListaPresencaAluno_Click"> <%--onserverclick="btnNovoAluno_Click"--%>
                                                        <i class="fa fa-calendar-check-o"></i>&nbsp;Gerar Lista de Presença dos Alunos
                                                    </button>
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-6 pull-right">
                                                    
                                                    <button type="button" id="btnGerarListaPresencaProfessor" runat="server" class="btn btn-info pull-right" onclick="if (fPreparaRelatorio('A Lista de Presença de Professor está sendo preparada.')) return;" onserverclick="btnGerarListaPresencaProfessor_Click"> <%--onserverclick="btnNovoAluno_Click"--%>
                                                        <i class="fa fa-calendar-o"></i>&nbsp;Gerar Lista de Presença do Professor
                                                    </button>
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
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                <div id="msgSemResultadosgrdPresencaAluno" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <asp:Label runat="server" ID="Label8" Text="Nenhum Aluno matriculado nesse Oferecimento" />
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdPresencaAluno" >
                                                    <div class="scroll">
                                                        <table id="grdPresencaAluno" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

                        <div id="divSemAluno" class="row">
                            <div class="alert bg-gray col-lg-12">
                                <asp:Label runat="server" ID="Label9" Text="Nenhum Aluno matriculado nesse Oferecimento" />
                            </div>
                            <br />
                            <div class="col-md-12">
                                <button type="button" id="Button1" runat="server" class="btn btn-info pull-right" onclick="if (fPreparaRelatorio('A Lista de Presença de Professor está sendo preparada.')) return;" onserverclick="btnGerarListaPresencaProfessor_Click"> <%--onserverclick="btnNovoAluno_Click"--%>
                                    <i class="fa fa-calendar-o"></i>&nbsp;Gerar Lista de Presença do Professor
                                </button>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="col-xs-3 pull-left">
                        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                    <div class="hidden-lg hidden-md"> 
                        <br />
                    </div>

                    <div class="col-xs-3 center-block">
                        <button id="btnSelecionaTodosPresencaAluno" type="button" class="btn btn-danger center-block" onclick="fDesselecionarTodosPresenca()">
                        <i class="fa fa-square-o"></i>&nbsp;Falta Geral</button>
                    </div>
                    <div class="hidden-lg hidden-md"> 
                        <br />
                    </div>

                    <div class="col-xs-3 center-block">
                        <button id="btnDesselecionaTodosPresencaAluno" type="button" class="btn btn-info center-block" onclick="fSelecionarTodosPresenca()">
                        <i class="fa fa-check-square-o"></i>&nbsp;Presença Geral</button>
                    </div>
                    <div class="hidden-lg hidden-md"> 
                        <br />
                    </div>

                    <div class="col-xs-3 pull-right">
                        <button id="btnConfirmaPresencaAluno" type="button" class="btn btn-success pull-right">
                        <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                    </div>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Confirma Matricula Oferecimento -->
    <div class="modal fade" id="divModalIncluiAlunoOferecimentoConfirmacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-yellow-active">
                    <h4 class="modal-title"><i class="fa fa-info-circle"></i> Atenção</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 ">
                            <span> O Aluno <label class="negrito" id="lblNomeIncluiAlunoOferecimentoConfirmacao"></label> da turma <label class="negrito" id="lblTurmaIncluiAlunoOferecimentoConfirmacao"></label> 
                                <label id="lblQuantoApontamentosIncluiAlunoOferecimentoConfirmacao"></label> 
                            </span>
                            <br /><br />
                        </div>
                        <br />

                        <div class="col-md-12" id="divSituacaoMatricula">
                            <span> Situação da Matrícula: <br /> <label class="negrito" id="lblSituacaoIncluiAlunoOferecimentoConfirmacao"></label>
                            </span>
                            <hr />
                            <br />
                        </div>
                        <br />

                        <div class="col-md-12" id="divRequerimento">
                            <span> Disciplinas que o aluno deveria ter cursado: <br /> <label class="negrito" id="lblRequerimentosIncluiAlunoOferecimentoConfirmacao"></label>
                            </span>
                            <hr />
                            <br />
                        </div>
                        <br />

                        <div class="col-md-12" id="divAlertaLimite">
                            <span> Situação do Aluno em relação à essa disciplina: <br /> <label class="negrito" id="lblAlertaIncluiAlunoOferecimentoConfirmacao"></label>
                            </span>
                            <hr />
                            <br />
                        </div>
                        <br />

                        <div class="col-md-12 ">
                            <span> 
                            </span>
                            <br /><br />
                            <span>Ainda assim deseja realmente matricular o Aluno nesse oferecimento?</span>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-xs-6 pull-left">
                        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                    <div class="hidden-lg hidden-md"> 
                        <br />
                    </div>

                    <div class="col-xs-6 pull-right">
                        <button id="btnConfirmaIncluiAlunoOferecimentoConfirmacao" type="button" class="btn btn-success pull-right">
                        <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                    </div>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Log -->
    <div class="modal fade" id="divModalLog" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-yellow-active">
                    <h4 class="modal-title"><i class="fa fa-info-circle"></i> Log</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-3 ">
                            <span> Data </span> <br />
                            <label  class="negrito" id="lblDataLog"></label> 
                            <br /><br />
                        </div>

                        <div class="col-md-3 ">
                            <span> Hora </span> <br />
                            <label  class="negrito" id="lblHoraLog"></label> 
                            <br /><br />
                        </div>

                        <div class="col-md-3 ">
                            <span> Status </span> <br />
                            <label  class="negrito" id="lblStatusLog"></label> 
                            <br /><br />
                        </div>

                        <div class="col-md-3 ">
                            <span> Responsável </span> <br />
                            <label  class="negrito" id="lblUsuarioLog"></label> 
                            <br /><br />
                        </div>
                        <br />

                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-xs-6 pull-right">
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <a id="divAProcessando" data-toggle="modal" href="#divModalProcessando" class="btn btn-primary hidden">Processando</a>
    <div class="modal fade" id="divModalProcessando" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-body">
                    <div class ="row">
                        <div class ="col-md-12 text-center">
                            Processando... <br />
                            Por favor, aguarde.<br /><br />
                            <img src="img/loader.gif" width="42" height="42" alt="" />
                            <a id="divAFechaProcessando"  href="#" data-dismiss="modal" class="btn hidden">Fechar</a>
                        </div>
                    </div>
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
    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

     <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>

    <script src="Scripts/nicEdit.js" type="text/javascript"></script>

    <script src="https://cdn.datatables.net/plug-ins/1.10.16/sorting/date-euro.js"></script>

    <script>
        var vRowIndex_grdCoordenador;
        
        $('#txtCPFProfessor').mask('999.999.999-99');

        $('#txtCPFTecnico').mask('999.999.999-99');

        function fAProcessando() {
            //alert('entrou');
            document.getElementById('divAProcessando').click();
        }

        function fAFechaProcessando() {
            //alert('entrou');
            document.getElementById('divAFechaProcessando').click();
        }

        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function fMostrarProgresso5()
        {
            document.getElementById('<%=UpdateProgress5.ClientID%>').style.display = "block";
        }

        function fModalAssociarProfessor() {
            document.getElementById("divgrdProfessorDisponivel").style.display = "none";
            $('#divModalAssociarProfessor').modal();
        }

        function fModalAssociarTecnico() {
            document.getElementById("divgrdTecnicoDisponivel").style.display = "none";
            $('#divModalAssociarTecnico').modal();
        }

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
        }


        function fbtnSalvar() {
            //alert("Clicou");
           $(".cBotaoSalvar").attr('disabled','disabled');

            //var table1 = $('#grdProfessor').DataTable();
            document.getElementById('hCodigo').value = "";

            var data = $('#grdProfessor').DataTable().$('input,select,textarea').serialize();
            data = replaceAll("chkResponsavel_", "", data);
            data = replaceAll("chkResponsavel_", "", data);
            data = replaceAll("=on&", ";", data);
            data = replaceAll("=on", "", data);
            if (data == "" && document.getElementById('<%=tabDataAulaOferecimento.ClientID%>').style.display != 'none') {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Selecione um Professor responsável.";
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                $('.cBotaoSalvar').removeAttr('disabled');
                return;
            }

            document.getElementById('hCodigo').value = data;

            var nicEE = new nicEditors.findEditor('<%=txtObjetivoOferecimento.ClientID%>');
            document.getElementById('htxtObjetivoOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtJustificativaOferecimento.ClientID%>');
            document.getElementById('htxtJustificativaOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtEmentaOferecimento.ClientID%>');
            document.getElementById('htxtEmentaOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtFormaAvaliacaoOferecimento.ClientID%>');
            document.getElementById('htxtFormaAvaliacaoOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtMaterialUtilizadoOferecimento.ClientID%>');
            document.getElementById('htxtMaterialUtilizadoOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtMetodologiaOferecimento.ClientID%>');
            document.getElementById('htxtMetodologiaOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtConhecimentosPreviosOferecimento.ClientID%>');
            document.getElementById('htxtConhecimentosPreviosOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtProgramaOferecimento.ClientID%>');
            document.getElementById('htxtProgramaOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtBibliografiaBasicaOferecimento.ClientID%>');
            document.getElementById('htxtBibliografiaBasicaOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtBibliografiaComplementarOferecimento.ClientID%>');
            document.getElementById('htxtBibliografiaComplementarOferecimento').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtObservacaoOferecimento.ClientID%>');
            document.getElementById('htxtObservacaoOferecimento').value = nicEE.getContent();

            document.getElementById('<%=bntSalvar2.ClientID%>').click();

            $('.cBotaoSalvar').removeAttr('disabled');
        }

        bkLib.onDomLoaded(function () {
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtObjetivoOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtJustificativaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtEmentaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtFormaAvaliacaoOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtMaterialUtilizadoOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtMetodologiaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtConhecimentosPreviosOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtProgramaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtBibliografiaBasicaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtBibliografiaComplementarOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtObservacaoOferecimento.ClientID%>');

        });

        function fAtivaTextAreas() {
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtObjetivoOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtJustificativaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtEmentaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtFormaAvaliacaoOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtMaterialUtilizadoOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtMetodologiaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtConhecimentosPreviosOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtProgramaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtBibliografiaBasicaOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtBibliografiaComplementarOferecimento.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtObservacaoOferecimento.ClientID%>');
        }
        
        $(document).ready(function () {
            fPreencheProfessorOferecimento();
            fPreencheTecnicoOferecimento();
            if (document.getElementById("<%=tabDataAulaOferecimento.ClientID%>").style.display != 'none') {
                fPreencheDataAulaOferecimento();
                fPreencheMatriculaOferecimento();
            }
        });

        //================================================================================

        function fLogDataAula(qData, qHora, qStatus, qUsuario) {
            //alert(qData + " " + qHora + " " + qStatus + " " + qUsuario)
            document.getElementById('lblDataLog').innerHTML = qData;
            document.getElementById('lblHoraLog').innerHTML = qHora;
            document.getElementById('lblStatusLog').innerHTML = qStatus;
            document.getElementById('lblUsuarioLog').innerHTML = qUsuario;
            $('#divModalLog').modal();
        }

        //================================================================================
        function fCopyUrl() {
            /* Get the text field */
            var copyText = document.getElementById("<%=txtUrlEmenta.ClientID%>");

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
        
        function fAbreEditarAula(qAula, qId, qData, qHoraInicio, qHoraFim, qSala) {
            document.getElementById("divComPresenca").style.display = "none";
            document.getElementById("txtIdAula").value = qId;
            document.getElementById("txtDataAulaModalEditarAula").value = qData;
            //document.getElementById("txtHoraFimAulaModalEditarAula").value = qHoraFim;
            //document.getElementById("<%=ddlSalaAula.ClientID%>").value = qSala;
            $("#ddlHoraInicioAulaModalEditarAula").val(qHoraInicio.substring(0, 2)).trigger("change");
            $("#ddlMinutoInicioAulaModalEditarAula").val(qHoraInicio.substring(3, 6)).trigger("change");
            $("#ddlHoraFimAulaModalEditarAula").val(qHoraFim.substring(0, 2)).trigger("change");
            $("#ddlMinutoFimAulaModalEditarAula").val(qHoraFim.substring(3, 6)).trigger("change");

            $("#<%=ddlSalaAula.ClientID%>").val(qSala).trigger("change");

            document.getElementById("txtDataAulaModalEditarAula").readOnly = false;

            document.getElementById("ddlHoraInicioAulaModalEditarAula").disabled = false;
            document.getElementById("ddlMinutoInicioAulaModalEditarAula").disabled = false;
            document.getElementById("ddlHoraFimAulaModalEditarAula").disabled = false;
            document.getElementById("ddlMinutoFimAulaModalEditarAula").disabled = false;
            document.getElementById("<%=ddlSalaAula.ClientID%>").disabled = false;

            $("#divCabecModalEditarAula").removeClass("bg-danger");
            $('#divCabecModalEditarAula').addClass('bg-primary');
            document.getElementById("lblTituloModalEditarAula").innerHTML = "<i class=\"fa fa-edit\"></i>&nbsp;Editar Aula n.º " + qAula;
            document.getElementById("btnConfirmaAlteracaoModalEditarAula").style.display = 'block';
            document.getElementById("btnConfirmaExclusaoModalEditarAula").style.display = 'none';

            $('#divModalEditarAula').modal();
        }

        //================================================================================

        function fAbreApagarAula(qAula, qId, qData, qHoraInicio, qHoraFim, qSala, qPresenca) {
            if (qPresenca == "ComPresenca") {
                document.getElementById("divComPresenca").style.display = "block";
            }
            else {
                document.getElementById("divComPresenca").style.display = "none";
            }

            document.getElementById("txtIdAula").value = qId;
            document.getElementById("txtDataAulaModalEditarAula").value = qData;
            $("#ddlHoraInicioAulaModalEditarAula").val(qHoraInicio.substring(0, 2)).trigger("change");
            $("#ddlMinutoInicioAulaModalEditarAula").val(qHoraInicio.substring(3, 6)).trigger("change");
            $("#ddlHoraFimAulaModalEditarAula").val(qHoraFim.substring(0, 2)).trigger("change");
            $("#ddlMinutoFimAulaModalEditarAula").val(qHoraFim.substring(3, 6)).trigger("change");
            $("#<%=ddlSalaAula.ClientID%>").val(qSala).trigger("change");

            document.getElementById("txtDataAulaModalEditarAula").readOnly = true;
            document.getElementById("ddlHoraInicioAulaModalEditarAula").disabled = true;
            document.getElementById("ddlMinutoInicioAulaModalEditarAula").disabled = true;
            document.getElementById("ddlHoraFimAulaModalEditarAula").disabled = true;
            document.getElementById("ddlMinutoFimAulaModalEditarAula").disabled = true;
            document.getElementById("<%=ddlSalaAula.ClientID%>").disabled = true;

            $("#divCabecModalEditarAula").removeClass("bg-primary");
            $('#divCabecModalEditarAula').addClass('bg-danger');
            document.getElementById("lblTituloModalEditarAula").innerHTML = "<i class=\"fa fa-eraser\"></i>&nbsp;Excluir Aula n.º " + qAula;
            document.getElementById("btnConfirmaAlteracaoModalEditarAula").style.display = 'none';
            document.getElementById("btnConfirmaExclusaoModalEditarAula").style.display = 'block';

            $('#divModalEditarAula').modal();
        }

        //================================================================================

        function fPreencheDataAulaOferecimento() {
            var dt = $('#grdDatasAula').DataTable({
                processing: true,
                serverSide: false,
                destroy: true,
                async: false,
                stateSave: false,
                "autoWidth": false,
                error: function (xhr, error, thrown) {
                    alert('Não está logado');
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
                    //alert('Total registros: ' + oSettings.fnRecordsTotal());
                    //alert('Retorno json: ' + json[oSettings.fnRecordsTotal() - 1].P11);
                    document.getElementById('<%=txtHorasProfessor.ClientID%>').value = json[oSettings.fnRecordsTotal() - 1].P11;
                    document.getElementById('<%=txtHorasTecnico.ClientID%>').value = json[oSettings.fnRecordsTotal() - 1].P12;
                    document.getElementById('<%=txtHorasProfessor.ClientID%>').style.color = json[oSettings.fnRecordsTotal() - 1].P13;
                    document.getElementById('<%=txtTotalHorasAulas.ClientID%>').value = json[oSettings.fnRecordsTotal() - 1].P15;

                    if (json[oSettings.fnRecordsTotal() - 1].P13 == "red") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Excedeu o limite de horas do professor.";
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-danger");
                        $('#divCabecalho').addClass('alert-warning');
                        $('#divMensagemModal').modal();
                    }

                    document.getElementById('hMinimoData').value = json[oSettings.fnRecordsTotal() - 1].P15
                    $('#txtDataAulaModalIncluirAula').attr('min', document.getElementById('hMinimoData').value);
                    $('#txtDataAulaModalEditarAula').attr('min', document.getElementById('hMinimoData').value);

                    //alert('Retorno json P11: ' + json[oSettings.fnRecordsTotal() - 1].P11);
                    //alert('Retorno json P12: ' + json[oSettings.fnRecordsTotal() - 1].P12);
                    //alert('Retorno json P13: ' + json[oSettings.fnRecordsTotal() - 1].P13);

                    if (oSettings.fnRecordsTotal() == 0) {
                        document.getElementById("divgrdDatasAula").style.display = "none";
                        document.getElementById("msgSemResultadosDatasAula").style.display = "block";
                    }
                    else {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro") {
                            document.getElementById("divgrdDatasAula").style.display = "none";
                            document.getElementById("msgSemResultadosDatasAula").style.display = "block";
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                            $('#divModalAssociarTamanho').modal('hide');
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            document.getElementById("divgrdDatasAula").style.display = "block";
                            document.getElementById("msgSemResultadosDatasAula").style.display = "none";

                            var table_grdDatasAula = $('#grdDatasAula').DataTable();

                            $('#grdDatasAula').on("click", "tr", function () {
                                vRowIndex_grdDatasAula = table_grdDatasAula.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheDataAulaOferecimento",
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
                        "data": "P1", "title": "Sequência", "orderable": true, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P2", "title": "Data Aula", "orderable": false, "className": "text-center centralizarTH", width: '1140px'
                    },
                    {
                        "data": "P3", "title": "Hora Início", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P4", "title": "Hora Fim", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P5", "title": "Sala", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P6", "title": "Excluir Aula", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P7", "title": "Editar Aula", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P8", "title": "Equipe", "orderable": false, "className": "text-left", width: '100%'
                    },
                    {
                        "data": "P14", "title": "Presença Alunos", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P9", "title": "Incluir Professor", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P10", "title": "Incluir Técnico/Monitor", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    }
                ],
                order: [[1, 'asc']],
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

        function fPreencheMatriculaOferecimento() {
            var dt = $('#grdMatriculaOferecimento').DataTable({
                processing: true,
                serverSide: false,
                destroy: true,
                async: false,
                stateSave: false,
                "autoWidth": false,
                error: function (xhr, error, thrown) {
                    alert('Não está logado');
                },
                searching: false, //Pesquisar
                bPaginate: false, // Paginação
                bInfo: true, //mostrando 1 de x registros
                fnInitComplete: function (oSettings, json) {
                    //alert('Passou');
                    //CallBackReq(oSettings.fnRecordsTotal());
                    //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                    //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                    //    alert(json[i].Item);
                    //} 
                    //alert('Retorno json: ' + json[oSettings.fnRecordsTotal() - 1].P11);
                    //document.getElementById('<%=txtHorasProfessor.ClientID%>').value = json[oSettings.fnRecordsTotal() - 1].P11;
                    //document.getElementById('<%=txtHorasTecnico.ClientID%>').value = json[oSettings.fnRecordsTotal() - 1].P12;
                    //document.getElementById('<%=txtHorasProfessor.ClientID%>').style.color = json[oSettings.fnRecordsTotal() - 1].P13;

                    if (oSettings.fnRecordsTotal() == 0) {
                        document.getElementById("divgrdMatriculaOferecimento").style.display = "none";
                        document.getElementById("msgSemResultadosMatriculaOferecimento").style.display = "block";
                    }
                    else {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro") {
                            document.getElementById("divgrdMatriculaOferecimento").style.display = "none";
                            document.getElementById("msgSemResultadosMatriculaOferecimento").style.display = "block";
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                            $('#divModalAssociarTamanho').modal('hide');
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            document.getElementById("divgrdMatriculaOferecimento").style.display = "block";
                            document.getElementById("msgSemResultadosMatriculaOferecimento").style.display = "none";

                            var table_grdMatriculaOferecimento = $('#grdMatriculaOferecimento').DataTable();

                            $('#grdMatriculaOferecimento').on("click", "tr", function () {
                                vRowIndex_grdMatriculaOferecimento = table_grdMatriculaOferecimento.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheMatriculaOferecimento",
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
                        "data": "P1", "title": "Matrícula", "orderable": true, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P2", "title": "Nome", "orderable": true, "className": "text-left centralizarTH", width: '70%', type: 'locale-compare'
                    },
                    {
                        "data": "P3", "title": "Turma", "orderable": false, "className": "text-center centralizarTH", width: '30%'
                    },
                    {
                        "data": "P4", "title": "Freq.", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P5", "title": "Conceito", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P6", "title": "Resultado", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P9", "title": "Autorizado <br><small>(mostrar)</small>", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P7", "title": "Editar Nota", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    },
                    {
                        "data": "P8", "title": "Excluir Matrícula", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                    }
                ],
                order: [[2, 'asc']],
                dom: 'Blfrtip',
                lengthMenu: [[20, 40, 60, -1], [20, 40, 60, "Todos"]],
                buttons: [

                ],
                "language": {

                    "sEmptyTable": "Nenhum registro encontrado",
                    "sInfo": "Alunos matriculados (_TOTAL_)",
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


        function fModalAtivaInativa(qOperacao) {
            if (qOperacao == 'Ativa') {
                $("#divCabecAtiva").removeClass("bg-danger");
                $('#divCabecAtiva').addClass('bg-info');
                document.getElementById("btnConfirmaAtivar").style.display = 'block';
                document.getElementById("btnConfirmaInativar").style.display = 'none';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Oferecimento';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar o Oferecimento n.º: <strong>' + document.getElementById("<%=txtNumeroOferecimanto.ClientID%>").value + '</strong> da disciplina: <strong>' + document.getElementById("<%=txtCodigoDisciplinaOferecimento.ClientID%>").value + ' - ' + document.getElementById("<%=txtNomeDisciplinaOferecimento.ClientID%>").value + '</strong>?';
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Oferecimento';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar o Oferecimento n.º: <strong>' + document.getElementById("<%=txtNumeroOferecimanto.ClientID%>").value + '</strong> da disciplina: <strong>' + document.getElementById("<%=txtCodigoDisciplinaOferecimento.ClientID%>").value + ' - ' + document.getElementById("<%=txtNomeDisciplinaOferecimento.ClientID%>").value + '</strong>?';
            }
            $('#divModalAtivaInativa').modal();
        }

        //===============================================================
        function fModalAdicionarDataAula() {
            $('#divModalIncluirAula').modal();
        }

        //===============================================================
        function fIncluirAula() {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            var sAux = "";
            if (document.getElementById("txtDataAulaModalIncluirAula").value == "") {
                sAux = "Deve-se preencher a Data da Aula <br><br>"
            }

            if (document.getElementById("<% =ddlSalaAulaIncluirAula.ClientID%>").value == "") {
                sAux = sAux + "Deve-se selecionar uma sala de aula <br><br>"
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

            var qData = document.getElementById('txtDataAulaModalIncluirAula').value;
            var qHoraAulaInicio = document.getElementById('ddlHoraInicioAula').value;
            var qMinutoAulaInicio = document.getElementById('ddlMinutoInicioAula').value;
            var qHoraAulaFim = document.getElementById('ddlHoraFimAula').value;
            var qMinutoAulaFim = document.getElementById('ddlMinutoFimAula').value;
            var qIdSalaAula = document.getElementById("<% =ddlSalaAulaIncluirAula.ClientID%>").value;
            var qIdProfessor = document.getElementById("<% =ddlProfessorIncluirAula.ClientID%>").value;
            var qHoraGasta = document.getElementById('ddlHoraProfessorIncluirAula').value;
            var qMinutoGasta = document.getElementById('ddlMinutoProfessorIncluirAula').value;

            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluirAula?qData=" + qData + "&qHoraAulaInicio=" + qHoraAulaInicio + "&qMinutoAulaInicio=" + qMinutoAulaInicio + "&qHoraAulaFim=" + qHoraAulaFim + "&qMinutoAulaFim=" + qMinutoAulaFim + "&qIdSalaAula=" + qIdSalaAula + "&qIdProfessor=" + qIdProfessor + "&qHoraGasta=" + qHoraGasta + "&qMinutoGasta=" + qMinutoGasta,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Data Aula nesse Ofereciemnto';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão da Data Aula nesse Oferecimento. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //$('#divModalIncluirAula').modal('hide');
                        fPreencheDataAulaOferecimento();
                        fPreencheProfessorOferecimento();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Data no Oferecimento</strong><br /><br />',
                            message: 'Inclusão de Data nesse Oferecimento foi realizada com sucesso.<br />',

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
                                from: "bottom", 
                                align: "right"
                            }
                        });
                        
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert("Houve um erro na inclusão da Aula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão da Aula. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }


        //===============================================================
        function fEditarAula() {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            var sAux = "";
            if (document.getElementById("txtDataAulaModalEditarAula").value == "") {
                sAux = "Deve-se preencher a Data da Aula <br><br>"
            }
            if (document.getElementById("<% =ddlSalaAula.ClientID%>").value == "") {
                sAux = sAux + "Deve-se selecionar uma sala de aula <br><br>"
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

            var qId = document.getElementById('txtIdAula').value;
            var qData = document.getElementById('txtDataAulaModalEditarAula').value;
            var qHoraAulaInicio = document.getElementById('ddlHoraInicioAulaModalEditarAula').value;
            var qMinutoAulaInicio = document.getElementById('ddlMinutoInicioAulaModalEditarAula').value;
            var qHoraAulaFim = document.getElementById('ddlHoraFimAulaModalEditarAula').value;
            var qMinutoAulaFim = document.getElementById('ddlMinutoFimAulaModalEditarAula').value;
            var qIdSalaAula = document.getElementById("<% =ddlSalaAula.ClientID%>").value;

            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fEditarAula?qId=" + qId + "&qData=" + qData + "&qHoraAulaInicio=" + qHoraAulaInicio + "&qMinutoAulaInicio=" + qMinutoAulaInicio + "&qHoraAulaFim=" + qHoraAulaFim + "&qMinutoAulaFim=" + qMinutoAulaFim + "&qIdSalaAula=" + qIdSalaAula,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração da Aula';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na edição da Aula. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalEditarAula').modal('hide');
                        fPreencheDataAulaOferecimento();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração da Aula no Oferecimento</strong><br /><br />',
                            message: 'Alteração da Aula foi realizada com sucesso.<br />',

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
                    alert("Houve um erro na Alteração da Aula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração da Aula. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //===============================================================
        function fExcluirAula() {
            var qId = document.getElementById('txtIdAula').value;
            
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fExcluirAula?qId=" + qId,
                dataType: "json",
                success: function (json) {
                    if (json[0].P0 == "deslogado") {
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão da Aula';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão da Aula. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalEditarAula').modal('hide');
                        fPreencheDataAulaOferecimento();
                        fPreencheProfessorOferecimento();
                        fPreencheTecnicoOferecimento();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão da Aula no Oferecimento</strong><br /><br />',
                            message: 'Exclusão da Aula foi realizada com sucesso.<br />',

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
                error: function (xhr) {
                    alert("Houve um erro na exclusão da Aula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () {
                    alert("Houve um erro na exclusão da Aula. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //===============================================================
        function fSelecionarDisciplina() {
            document.getElementById("divgrdDisciplinaDisponivel").style.display = "none";
            $('#divModalAssociarDisciplina').modal();
        }

        //================================================================================

        function fPerquisaDisciplinaDisponivelOferecimento() {
            fProcessando();
            try {
                var qCodigo = document.getElementById('txtCodigoDisciplina').value;
                var qNome = document.getElementById('txtNomeDisciplina').value;
                var qCurso = $("#ddlCursoOferecimento option:selected").val();
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
                        url: "wsSapiens.asmx/fPerquisaDisciplinaDisponivelOferecimento?qCodigo=" + qCodigo + "&qNome=" + qNome + "&qCurso=" + qCurso,
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
        }

        //============================================================================

        function fIncluiDisciplinaOferecimento(qId, qCodigo, qNome) {
            document.getElementById('hCodigoDisciplina').value = qId;
            document.getElementById('<%=txtIdDisciplinaOferecimento.ClientID%>').value = qId;
            document.getElementById('<%=btnIncluiDisciplina.ClientID%>').click();
            $('#divModalAssociarDisciplina').modal('hide');
        }

        //=======================================

        function fAtivarInativarOferecimento(qOperacao){
            //alert(qOperacao);
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAtivarInativarOferecimento",
                contentType: 'application/json; charset=utf-8',
                data: "{qOperacao:'" + qOperacao + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var vTitulo = '';
                    var vBg = '';
                    var vIcon = '';
                    if (qOperacao == "Ativar") {
                        vTitulo = "Oferecimento Ativado com sucesso";
                        vBg = "info";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    else {
                        vTitulo = "Oferecimento Inativado com sucesso"
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
                            document.getElementById('<%=lblInativadoOferecimento.ClientID%>').style.display='none';
                        }
                        else {
                            document.getElementById('<%=btnAtivar.ClientID%>').style.display='block';
                            document.getElementById('<%=btnInativar.ClientID%>').style.display='none';
                            document.getElementById('<%=lblInativadoOferecimento.ClientID%>').style.display='block';
                        }

                        $('#divModalAtivaInativa').modal('hide');
                    }
                    else if (json[0].Retorno == "deslogado") {
                        window.location.href = "index.html";
                    }
                    else {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Ativação/Inativação do Oferecimento';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].Resposta;
                        $("#divCabecalho").removeClass("alert-warning");
                        $("#divCabecalho").removeClass("alert-primary");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
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


        //================================================================================

        function fPreencheProfessorOferecimento() {
            var dt = $('#grdProfessor').DataTable({
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
                        document.getElementById("divgrdProfessor").style.display = "none";
                        document.getElementById("msgSemResultadosProfessor").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdProfessor").style.display = "none";
                            document.getElementById("msgSemResultadosProfessor").style.display = "block";
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
                            document.getElementById("divgrdProfessor").style.display = "block";
                            document.getElementById("msgSemResultadosProfessor").style.display = "none";

                            var table_grdProfessor = $('#grdProfessor').DataTable();

                            $('#grdProfessor').on("click", "tr", function () {
                                vRowIndex_grdProfessor = table_grdProfessor.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheProfessorOferecimento",
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
                        "data": "P3", "title": "E-mail confirmado", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Responsável", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P5", "title": "Excluir", "orderable": false, "className": "text-center"
                    }
                ],
                order: [[2, 'asc']],
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

        function fPerquisaProfessorDisponivelOferecimento() {
            fProcessando();
            try {
                var qCPF = document.getElementById('txtCPFProfessor').value;
                var qNome = document.getElementById('txtNomeProfessor').value;
                var dt = $('#grdProfessorDisponivel').DataTable({
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
                            document.getElementById("divgrdProfessorDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdProfessorDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdProfessorDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdProfessorDisponivel").style.display = "block";
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
                                document.getElementById("divgrdProfessorDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdProfessorDisponivel").style.display = "none";

                                var table_grdProfessorDisponivel = $('#grdProfessorDisponivel').DataTable();

                                $('#grdProfessorDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdProfessorDisponivel = table_grdProfessorDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaProfessorDisponivelOferecimento?qCPF=" + qCPF + "&qNome=" + qNome,
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

        //========================================================================

        function fIncluiProfessorOferecimento(qId, qCPF, qNome) {
            fProcessando();
           
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiProfessorOferecimento?qId=" + qId,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Professor';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do Professor: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#<%=ddlProfessorIncluirAula.ClientID%>').append($('<option>', { 
                            value: qId,
                            text : qNome 
                        })).select2();
                        fSelect2();
                        fPreencheProfessorOferecimento();
                        fPreencheDataAulaOferecimento();
                        //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.';
                        //$("#divCabecalho").removeClass("alert-warning");
                        //$("#divCabecalho").removeClass("alert-danger");
                        //$('#divCabecalho').addClass('alert-success');
                        //$('#divMensagemModal').modal();
                        $('#grdProfessorDisponivel').DataTable().row(vRowIndex_grdProfessorDisponivel).remove().draw();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Professor</strong><br /><br />',
                            message: 'Inclusão do Professor <strong>' + qNome + '</strong> realizada com sucesso.<br />',

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
                    alert("Houve um erro na inclusão do Professor. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão do Professor. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function AbreModalApagarProfessor(qId, qCPF, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeProfessor').innerHTML = qNome;
            document.getElementById('lblCPFProfessor').innerHTML = qCPF;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirProfessor').modal();
        }

        //============================================================================

        function fExcluiProfessorOferecimento() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiProfessorOferecimento?qId=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Professor';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do Professor: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            fPreencheDataAulaOferecimento();
                            $('#grdProfessor').DataTable().row(vRowIndex_grdProfessor).remove().draw();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Professor</strong><br /><br />',
                                message: 'Exclusão do Professor realizada com sucesso.<br />',

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
                        $('#divModalExcluirProfessor').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão do Professor. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirProfessor').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão do Professor. Por favor tente novamente!");
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }


        //================================================================================

        function fPreencheTecnicoOferecimento() {
            var dt = $('#grdTecnico').DataTable({
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
                        document.getElementById("divgrdTecnico").style.display = "none";
                        document.getElementById("msgSemResultadosTecnico").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdTecnico").style.display = "none";
                            document.getElementById("msgSemResultadosTecnico").style.display = "block";
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
                            document.getElementById("divgrdTecnico").style.display = "block";
                            document.getElementById("msgSemResultadosTecnico").style.display = "none";

                            var table_grdTecnico = $('#grdTecnico').DataTable();

                            $('#grdTecnico').on("click", "tr", function () {
                                vRowIndex_grdTecnico = table_grdTecnico.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheTecnicoOferecimento",
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
                        "data": "P3", "title": "E-mail confirmado", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Excluir", "orderable": false, "className": "text-center"
                    }
                ],
                order: [[2, 'asc']],
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

        function fPerquisaTecnicoDisponivelOferecimento() {
            fProcessando();
            try {
                var qCPF = document.getElementById('txtCPFTecnico').value;
                var qNome = document.getElementById('txtNomeTecnico').value;
                var dt = $('#grdTecnicoDisponivel').DataTable({
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
                            document.getElementById("divgrdTecnicoDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdTecnicoDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdTecnicoDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdTecnicoDisponivel").style.display = "block";
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
                                document.getElementById("divgrdTecnicoDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdTecnicoDisponivel").style.display = "none";

                                var table_grdTecnicoDisponivel = $('#grdTecnicoDisponivel').DataTable();

                                $('#grdTecnicoDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdTecnicoDisponivel = table_grdTecnicoDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaTecnicoDisponivelOferecimento?qCPF=" + qCPF + "&qNome=" + qNome,
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

        //========================================================================

        function fIncluiTecnicoOferecimento(qId, qCPF, qNome) {
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiTecnicoOferecimento?qId=" + qId,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Técnico/Monitor';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do Técnico/Monitor: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        
                        fPreencheTecnicoOferecimento();
                        fPreencheDataAulaOferecimento();
                        //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.';
                        //$("#divCabecalho").removeClass("alert-warning");
                        //$("#divCabecalho").removeClass("alert-danger");
                        //$('#divCabecalho').addClass('alert-success');
                        //$('#divMensagemModal').modal();
                        $('#grdTecnicoDisponivel').DataTable().row(vRowIndex_grdTecnicoDisponivel).remove().draw();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Técnico/Monitor</strong><br /><br />',
                            message: 'Inclusão do Técnico/Monitor<strong>' + qNome + '</strong> realizada com sucesso.<br />',

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
                    alert("Houve um erro na inclusão do Técnico/Monitor. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão do Técnico/Monitor. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function AbreModalApagarTecnico(qId, qCPF, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeTecnico').innerHTML = qNome;
            document.getElementById('lblCPFTecnico').innerHTML = qCPF;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirTecnico').modal();
        }

        //============================================================================

        function fExcluiTecnicoOferecimento() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiTecnicoOferecimento?qId=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Professor';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do Técnico/Monitor: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            fPreencheDataAulaOferecimento();
                            $('#grdTecnico').DataTable().row(vRowIndex_grdTecnico).remove().draw();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Técnico/Monitor</strong><br /><br />',
                                message: 'Exclusão do Técnico/Monitor realizada com sucesso.<br />',

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
                        $('#divModalExcluirTecnico').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão do Técnico/Monitor. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirTecnico').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão do Técnico/Monitor. Por favor tente novamente!");
                        $('#divModalExcluirTecnico').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //=================================================================


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

        function fMostrarProgresso()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function fCheckAssociar(obj) {
            var lista = $('[name^="chkResponsavel_"]')

            for (var i = 0; i < lista.length; i++) {
                if (lista[i] != obj) {
                    lista[i].checked = false;
                }
                
            }
            obj.checked = true;
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalAssociarProfessor').is(':visible')) {
                    fPerquisaProfessorDisponivelOferecimento();
                }
                else if ($('#divModalAssociarTecnico').is(':visible')) {
                    fPerquisaTecnicoDisponivelOferecimento();
                }
                else if ($('#divModalAssociarDisciplina').is(':visible')) {
                    fPerquisaDisciplinaDisponivelOferecimento();
                }
                else if ($('#divModalAlunosOferecimento').is(':visible')) {
                    fPerquisaAlunoDisponivelOferecimento();
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

        function fCheckPresenca(elemento) {
            var sAux = (elemento.name).split("_");
            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fCheckPresenca?qIdAula=" + sAux[1] + "&qIdProfessor=" + sAux[2] + "&qTipoProfessor=" + sAux[3] + "&qSituacao=" + elemento.checked,
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Presença';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração de presença do ' + sAux[3] + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {

                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Alteração de Presença</strong><br /><br />',
                                message: 'Alteração de Presença realizada com sucesso.<br />',

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

        function AbreModalApagarCoordenador(qId, qCPF, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeCoordenador').innerHTML = qNome;
            document.getElementById('lblCPFCoordenador').innerHTML = qCPF;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirCoordenador').modal();
        }

        function fAbreModalExcluirEquipe(qAula, qIdProfessor, qIdAula, qEquipe, qHora, qMinuto, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblTituloEquipe').innerHTML = "<i class='fa fa-eraser'></i> Excluir " + qEquipe + " da Aula n.º " + qAula;
            document.getElementById('txtNomeEquipe').style.display = "block";
            //document.getElementById('ddlNomeEquipe').style.display = "none";
            //$('#ddlNomeEquipe').next(".select2-container").show();
            $('#ddlNomeEquipe').next(".select2-container").hide();
            document.getElementById('txtIdProfessorEquipe').value = qIdProfessor;
            document.getElementById('txtIdAulaEquipe').value = qIdAula;
            document.getElementById('txtTipoEquipe').value = qEquipe;
            document.getElementById('txtNomeEquipe').value = qNome;

            $("#ddlHoraEquipe").val(qHora).trigger("change");
            $("#ddlMinutoEquipe").val(qMinuto).trigger("change");
            document.getElementById('ddlHoraEquipe').disabled = true;
            document.getElementById('ddlMinutoEquipe').disabled = true;

            document.getElementById('btnConfirmaModalExcluirEquipe').style.display = "block";
            document.getElementById('btnConfirmaModalAlterarHoraEquipe').style.display = "none";
            document.getElementById('btnConfirmaModalIncluirProfessorEquipe').style.display = "none";
            document.getElementById('btnConfirmaModalIncluirTecnicoEquipe').style.display = "none";
            $("#divCabecModalEquipe").removeClass("bg-green");
            $("#divCabecModalEquipe").removeClass("bg-yellow");
            $("#divCabecModalEquipe").removeClass("bg-primary");
            $('#divCabecModalEquipe').addClass('bg-danger');
            $('#divModalEquipe').modal();
        }

        function fExcluirEquipe() {
            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluirEquipe?qIdAula=" + document.getElementById('txtIdAulaEquipe').value + "&qIdProfessor=" + document.getElementById('txtIdProfessorEquipe').value + "&qTipoProfessor=" + document.getElementById('txtTipoEquipe').value,
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Excluir ' + document.getElementById('txtTipoEquipe').value;
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do ' + document.getElementById('txtTipoEquipe').value + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            fPreencheDataAulaOferecimento();
                            if ((document.getElementById('txtTipoEquipe').value) == "Professor") {
                                fPreencheProfessorOferecimento();
                            }
                            else {
                                fPreencheTecnicoOferecimento();
                            }
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Exclusão de ' + document.getElementById('txtTipoEquipe').value + '</strong><br /><br />',
                                message: 'Exclusão do ' + document.getElementById('txtTipoEquipe').value + ' <strong>' + document.getElementById('txtNomeEquipe').value + '</strong> realizada com sucesso.<br />',
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
                            $('#divModalEquipe').modal('hide');
                        }
                        fFechaProcessando();
                    },
                    error: function (xhr) {
                        alert("Houve um erro na exclusão do " + document.getElementById('txtTipoEquipe').value + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na exclusão do " + document.getElementById('txtTipoEquipe').value + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        function fAbreModalAlterarEquipe(qAula, qIdProfessor, qIdAula, qEquipe, qHora, qMinuto, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblTituloEquipe').innerHTML = "<i class='fa fa-clock-o'></i> Editar hora do " + qEquipe + " na Aula n.º " + qAula;
            document.getElementById('txtNomeEquipe').style.display = "block";
            //document.getElementById('ddlNomeEquipe').style.display = "none";
            //$('#ddlNomeEquipe').next(".select2-container").show();
            $('#ddlNomeEquipe').next(".select2-container").hide();
            document.getElementById('txtIdProfessorEquipe').value = qIdProfessor;
            document.getElementById('txtIdAulaEquipe').value = qIdAula;
            document.getElementById('txtTipoEquipe').value = qEquipe;
            document.getElementById('txtNomeEquipe').value = qNome;
            $("#ddlHoraEquipe").val(qHora).trigger("change");
            $("#ddlMinutoEquipe").val(qMinuto).trigger("change");
            document.getElementById('ddlHoraEquipe').disabled = false;
            document.getElementById('ddlMinutoEquipe').disabled = false;

            document.getElementById('btnConfirmaModalExcluirEquipe').style.display = "none";
            document.getElementById('btnConfirmaModalAlterarHoraEquipe').style.display = "block";
            document.getElementById('btnConfirmaModalIncluirProfessorEquipe').style.display = "none";
            document.getElementById('btnConfirmaModalIncluirTecnicoEquipe').style.display = "none";
            $("#divCabecModalEquipe").removeClass("bg-green");
            $("#divCabecModalEquipe").removeClass("bg-yellow");
            $("#divCabecModalEquipe").removeClass("bg-danger");
            $('#divCabecModalEquipe').addClass('bg-primary');
            $('#divModalEquipe').modal();
        }

        function fAlterarHoraEquipe() {
            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fAlterarEquipe?qIdAula=" + document.getElementById('txtIdAulaEquipe').value + "&qIdProfessor=" + document.getElementById('txtIdProfessorEquipe').value + "&qTipoProfessor=" + document.getElementById('txtTipoEquipe').value + "&qHora=" + $('#ddlHoraEquipe').val() + "&qMinuto=" + $('#ddlMinutoEquipe').val(),
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alterar a hora do ' + document.getElementById('txtTipoEquipe').value;
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração da hora do ' + document.getElementById('txtTipoEquipe').value + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            fPreencheDataAulaOferecimento();
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Alteração da hora do ' + document.getElementById('txtTipoEquipe').value + '</strong><br /><br />',
                                message: 'Alteração da hora do ' + document.getElementById('txtTipoEquipe').value + ' <strong>' + document.getElementById('txtNomeEquipe').value + '</strong> realizada com sucesso.<br />',
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
                            $('#divModalEquipe').modal('hide');
                        }
                        fFechaProcessando();
                    },
                    error: function (xhr) {
                        alert("Houve um erro na alteração da hora do " + document.getElementById('txtTipoEquipe').value + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na alteração da hora do " + document.getElementById('txtTipoEquipe').value + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        function fAbreModalIncluirEquipe(qAula, qIdAula, qEquipe) {
            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fPreencheEquipeDisponiveisOferecimento?qAula=" + qIdAula + "&qEquipe=" + qEquipe,
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Incluir ' + qEquipe;
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão de ' + qEquipe + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {

                            document.getElementById('lblTituloEquipe').innerHTML = "<i class='fa fa-user-plus'></i> Incluir " + qEquipe + " na Aula n.º " + qAula;
                            document.getElementById('txtNomeEquipe').style.display = "none";
                            $('#ddlNomeEquipe').next(".select2-container").show();
                            //$('#ddlNomeEquipe').next(".select2-container").hide();
                            $("#ddlNomeEquipe").empty().trigger('change');

                            $('#ddlNomeEquipe').select2({ data: json });

                            fSelect2();
                            document.getElementById('txtIdAulaEquipe').value = qIdAula;
                            document.getElementById('txtTipoEquipe').value = qEquipe;
                            $("#ddlHoraEquipe").val("04").trigger("change");
                            $("#ddlMinutoEquipe").val("00").trigger("change");
                            document.getElementById('ddlHoraEquipe').disabled = false;
                            document.getElementById('ddlMinutoEquipe').disabled = false;

                            document.getElementById('btnConfirmaModalExcluirEquipe').style.display = "none";
                            document.getElementById('btnConfirmaModalAlterarHoraEquipe').style.display = "none";
                            if (qEquipe == "Professor") {
                                document.getElementById('btnConfirmaModalIncluirProfessorEquipe').style.display = "block";
                                document.getElementById('btnConfirmaModalIncluirTecnicoEquipe').style.display = "none";
                            }
                            else {
                                document.getElementById('btnConfirmaModalIncluirProfessorEquipe').style.display = "none";
                                document.getElementById('btnConfirmaModalIncluirTecnicoEquipe').style.display = "block";
                            }
                            if (qEquipe == "Professor") {
                                $("#divCabecModalEquipe").removeClass("bg-yellow");
                                $("#divCabecModalEquipe").removeClass("bg-danger");
                                $('#divCabecModalEquipe').removeClass('bg-primary');
                                $('#divCabecModalEquipe').addClass('bg-green');
                            }
                            else {
                                $("#divCabecModalEquipe").removeClass("bg-green");
                                $("#divCabecModalEquipe").removeClass("bg-danger");
                                $('#divCabecModalEquipe').removeClass('bg-primary');
                                $('#divCabecModalEquipe').addClass('bg-yellow');
                            }
                            
                            $('#divModalEquipe').modal();
                        }
                        fFechaProcessando();
                    },
                    error: function (xhr) {
                        alert("Houve um erro na inclusão de " + qEquipe + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na inclusão de " + qEquipe + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        function fIncluirEquipeOferecimento() {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            var sAux = "";
            if ($("#ddlNomeEquipe").val() == "0") {
                sAux = "Deve-se selecionar um " + document.getElementById('txtTipoEquipe').value;
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                return;
            }

            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fIncluirEquipeOferecimento?qIdAula=" + document.getElementById('txtIdAulaEquipe').value + "&qIdProfessor=" + $("#ddlNomeEquipe").val() + "&qTipoProfessor=" + document.getElementById('txtTipoEquipe').value + "&qHora=" + $('#ddlHoraEquipe').val() + "&qMinuto=" + $('#ddlMinutoEquipe').val(),
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de ' + document.getElementById('txtTipoEquipe').value;
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do ' + document.getElementById('txtTipoEquipe').value + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            fPreencheDataAulaOferecimento();
                            if ((document.getElementById('txtTipoEquipe').value) == "Professor") {
                                fPreencheProfessorOferecimento();
                            }
                            else {
                                fPreencheTecnicoOferecimento();
                            }

                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Inclusão de ' + document.getElementById('txtTipoEquipe').value + '</strong><br /><br />',
                                message: 'Inclusão do ' + document.getElementById('txtTipoEquipe').value + ' <strong>' + $("#ddlNomeEquipe option:selected").text() + '</strong> realizada com sucesso.<br />',
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
                            $('#divModalEquipe').modal('hide');
                        }
                        fFechaProcessando();
                    },
                    error: function (xhr) {
                        alert("Houve um erro na inclusão do " + document.getElementById('txtTipoEquipe').value + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na inclusão do " + document.getElementById('txtTipoEquipe').value + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //================================================================================

        function fModalAdicionarMatriculaOferecimento() {
            document.getElementById("divgrdAlunoDisponivel").style.display = "none";
            $('#divModalAlunosOferecimento').modal();
        }

        //================================================================================

        $('.ddl_fecha_grid_resultados_MatriculaAluno').on('select2:select', function (e) {
            fLimpaGridMatriculaAluno();
        });

        //================================================================================

        function fLimpaGridMatriculaAluno() {
            document.getElementById("divgrdAlunoDisponivel").style.display = "none";
        }

        //================================================================================

        function fPerquisaAlunoDisponivelOferecimento() {
            fProcessando();
            try {
                //alert("passou-0");
                var qMatricula = document.getElementById('txtMatriculaAluno').value;
                var qNome = document.getElementById('txtNomeAluno').value;
                var qTipoCurso = $("#<%=ddlTipoCursoFiltro.ClientID%> option:selected").val();
                var qCurso = $("#<%=ddlNomeCursoFiltro.ClientID%> option:selected").val();
                var qTurma = $("#<%=ddlTurmaFiltro.ClientID%> option:selected").val();
                //alert("passou-1");
                var dt = $('#grdAlunoDisponivel').DataTable({
                    stateSave: true,
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
                            document.getElementById("divgrdAlunoDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdAlunoDisponivel").style.display = "block";
                            fFechaProcessando();
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdAlunoDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdAlunoDisponivel").style.display = "block";
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
                                document.getElementById("divgrdAlunoDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdAlunoDisponivel").style.display = "none";

                                var table_grdAlunoDisponivel = $('#grdAlunoDisponivel').DataTable();

                                $('#grdAlunoDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdAlunoDisponivel = table_grdAlunoDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaAlunoDisponivelOferecimento?qMatricula=" + qMatricula + "&qNome=" + qNome + "&qTipoCurso=" + qTipoCurso + "&qCurso=" + qCurso + "&qTurma=" + qTurma,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden centralizarTH", width: "10px"
                        },
                        {
                            "data": "P1", "title": "Matrícula", "orderable": true, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P2", "title": "Nome", "orderable": true, "className": "text-left centralizarTH", width: '55%'
                        },
                        {
                            "data": "P3", "title": "Turma", "orderable": true, "className": "text-lef centralizarTHt", width: '45%'
                        },
                        {
                            "data": "P4", "title": "Adicionar", "orderable": false, "className": "text-center centralizarTH", width: "10px"
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

        //===============================================================
        function fIncluiAlunoOferecimentoConfirmacao(qRequisito, qTurma, qSituacao, qIdTurma, qIdAluno, qNome, qAlerta, qLigaSituacao, qLigaRequerimento, qLigaAlerta) {
            document.getElementById('divSituacaoMatricula').style.display = "none";
            document.getElementById('divRequerimento').style.display = "none";
            document.getElementById('divAlertaLimite').style.display = "none";
            document.getElementById('lblNomeIncluiAlunoOferecimentoConfirmacao').innerHTML = qNome;
            document.getElementById('lblTurmaIncluiAlunoOferecimentoConfirmacao').innerHTML = qTurma;
            document.getElementById('lblSituacaoIncluiAlunoOferecimentoConfirmacao').innerHTML = qSituacao;
            document.getElementById('lblRequerimentosIncluiAlunoOferecimentoConfirmacao').innerHTML = qRequisito;
            document.getElementById('lblAlertaIncluiAlunoOferecimentoConfirmacao').innerHTML = qAlerta;
            if ((qLigaSituacao + qLigaRequerimento + qLigaAlerta) > 1) {
                document.getElementById('lblQuantoApontamentosIncluiAlunoOferecimentoConfirmacao').innerHTML = "tem o seguinte apontamento:";
            }
            else {
                document.getElementById('lblQuantoApontamentosIncluiAlunoOferecimentoConfirmacao').innerHTML = "tem os seguintes apontamentos:";
            }
            document.getElementById('lblQuantoApontamentosIncluiAlunoOferecimentoConfirmacao').innerHTML = "tem o seguinte apontamento:";
            document.getElementById('btnConfirmaIncluiAlunoOferecimentoConfirmacao').setAttribute("onClick", "fIncluiAlunoOferecimento('" + qIdTurma + "','" + qIdAluno + "','" + qNome + "');");

            if (qLigaSituacao == 1) {
                document.getElementById('divSituacaoMatricula').style.display = "block";
            }
            if (qLigaRequerimento == 1) {
                document.getElementById('divRequerimento').style.display = "block";
            }
            if (qLigaAlerta == 1) {
                document.getElementById('divAlertaLimite').style.display = "block";
            }
            
            $('#divModalIncluiAlunoOferecimentoConfirmacao').modal();
        }

        //===============================================================

        function fIncluiAlunoOferecimentoPreRequisitoConfirmacao(qRequisito, qTurma, qSituacao, qIdTurma, qIdAluno, qNome) {
            document.getElementById('lblNomeIncluiAlunoOferecimentoConfirmacao').innerHTML = qNome;
            document.getElementById('lblTurmaIncluiAlunoOferecimentoConfirmacao').innerHTML = qTurma;
            document.getElementById('lblSituacaoIncluiAlunoOferecimentoConfirmacao').innerHTML = qSituacao;
            document.getElementById('lblRequerimentosIncluiAlunoOferecimentoConfirmacao').innerHTML = qRequisito;
            document.getElementById('lblQuantoApontamentosIncluiAlunoOferecimentoConfirmacao').innerHTML = "tem o seguinte apontamento:";
            //document.getElementById('btnConfirmaIncluiAlunoOferecimentoConfirmacao').onclick = 'fIncluiAlunoOferecimento("' + qIdTurma + '","' + qIdAluno + '","' + qNome + '");';
            document.getElementById('btnConfirmaIncluiAlunoOferecimentoConfirmacao').setAttribute( "onClick", "fIncluiAlunoOferecimento('" + qIdTurma + "','" + qIdAluno + "','" + qNome + "');" );
                    
            document.getElementById('divSituacaoMatricula').style.display = "none";
            document.getElementById('divRequerimento').style.display = "block";
            $('#divModalIncluiAlunoOferecimentoConfirmacao').modal();
        }

        //===============================================================


        function fIncluiAlunoOferecimentoPreRequisitoPlusConfirmacao(qRequisito, qTurma, qSituacao, qIdTurma, qIdAluno, qNome) {
            document.getElementById('lblNomeIncluiAlunoOferecimentoConfirmacao').innerHTML = qNome;
            document.getElementById('lblTurmaIncluiAlunoOferecimentoConfirmacao').innerHTML = qTurma;
            document.getElementById('lblSituacaoIncluiAlunoOferecimentoConfirmacao').innerHTML = qSituacao;
            document.getElementById('lblRequerimentosIncluiAlunoOferecimentoConfirmacao').innerHTML = qRequisito;
            document.getElementById('lblQuantoApontamentosIncluiAlunoOferecimentoConfirmacao').innerHTML = "tem os seguintes apontamentos:";
            //document.getElementById('btnConfirmaIncluiAlunoOferecimentoConfirmacao').onclick = 'fIncluiAlunoOferecimento("' + qIdTurma + '","' + qIdAluno + '","' + qNome + '");';
            document.getElementById('btnConfirmaIncluiAlunoOferecimentoConfirmacao').setAttribute("onClick", "fIncluiAlunoOferecimento('" + qIdTurma + "','" + qIdAluno + "','" + qNome + "');");

            document.getElementById('divSituacaoMatricula').style.display = "block";
            document.getElementById('divRequerimento').style.display = "block";
            $('#divModalIncluiAlunoOferecimentoConfirmacao').modal();
        }

        //===============================================================



        function fIncluiAlunoOferecimento(qIdTurma, qIdAluno, qNome) {

            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fIncluiAlunoOferecimento?qIdTurma=" + qIdTurma + "&qIdAluno=" + qIdAluno + "&qNome=" + qNome,
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Matrícula de Alumo no Oferecimento';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na matrícula do aluno ' + qNome + ' nesse oferecimento. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {

                            $('#divModalIncluiAlunoOferecimentoConfirmacao').modal('hide');
                            fPreencheMatriculaOferecimento();

                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Matrícula do Aluno no Oferecimento </strong><br /><br />',
                                message: 'Matrícula do aluno <strong>' + document.getElementById('txtTipoEquipe').value + '</strong> nesse oferecimento realizada com sucesso.<br />',
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
                            fPerquisaAlunoDisponivelOferecimento();
                        }
                        fFechaProcessando();
                    },
                    error: function (xhr) {
                        alert("Houve um erro na matrícula do aluno " + qNome + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na matrícula do aluno " + qNome + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //===============================================================

        function fAbreExcluirMatricula(qId_matricula_oferecimento, qId_aluno, qId_turma, qId_Nota, qNome) {
            document.getElementById('lblNomeAlunoExcluir').innerHTML = qNome;
            document.getElementById('lblMatriculaAlunoExcluir').innerHTML = qId_aluno;
            document.getElementById('txtIdMatriculaOferecimentoExcluirMatricula').value = qId_matricula_oferecimento;
            document.getElementById('txtIdAlunoExcluirMatricula').value = qId_aluno;
            document.getElementById('txtIdTurmaExcluirMatricula').value = qId_turma;
            document.getElementById('txtIdNotaExcluirMatricula').value = qId_Nota;
            document.getElementById('txtNomeAlunoExcluirMatricula').value = qNome;
            $('#divModalExcluirMatricula').modal();
        }

        //===============================================================

        function fExcluirMatriculaAluno() {
            var qId_matricula_oferecimento = document.getElementById('txtIdMatriculaOferecimentoExcluirMatricula').value;
            var qId_aluno = document.getElementById('txtIdAlunoExcluirMatricula').value;
            var qId_turma = document.getElementById('txtIdTurmaExcluirMatricula').value;
            var qId_Nota = document.getElementById('txtIdNotaExcluirMatricula').value;
            var qNome = document.getElementById('txtNomeAlunoExcluirMatricula').value;
            
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fExcluirMatriculaAluno?qId_matricula_oferecimento=" + qId_matricula_oferecimento + "&qId_aluno=" + qId_aluno + "&qId_turma=" + qId_turma + "&qId_Nota=" + qId_Nota + "&qNome=" + qNome,
                dataType: "json",
                success: function (json) {
                    if (json[0].P0 == "deslogado") {
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Matrícula de Aluno';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão de matrícula do Aluno <strong>' + qNome + '</strong>. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalExcluirMatricula').modal('hide');
                        fPreencheMatriculaOferecimento();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão da Matrícula no Oferecimento</strong><br /><br />',
                            message: 'Exclusão da matrícula do aluno <b>' + qNome + '</b> foi realizada com sucesso.<br />',

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
                error: function (xhr) {
                    alert("Houve um erro na exclusão da Matrícula do aluno " + qNome + ". Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () {
                    alert("Houve um erro na exclusão da Matrícula do aluno " + qNome + ". Por favor tente novamente.");
                    fFechaProcessando()
                }
            });
        }

        //================================================================================

        function fAbreEditarNota(qTurma, qConceito, qAutorizado, qid_matricula_oferecimento, qid_aluno, qid_turma, qid_nota, qNome) {
            document.getElementById("txtIdAlunoNota").value = qid_aluno;
            document.getElementById("txtNomeAlunoNota").value = qNome;
            document.getElementById("txtIdNota").value = qid_nota;
            document.getElementById("txtTurmaAlunoNota").value = qTurma;
            $("#ddlConceitoNota").val(qConceito).trigger("change");
            $("#ddlAutorizadoNota").val(qAutorizado).trigger("change");

            $('#divModalEditarNota').modal();
        }

        //===============================================================

        function fEditarNota() {
            //var qId_matricula_oferecimento = document.getElementById('txtIdMatriculaOferecimentoExcluirMatricula').value;
            var qid_aluno = document.getElementById('txtIdAlunoNota').value;
            var qid_nota = document.getElementById('txtIdNota').value;
            var qNome = document.getElementById('txtNomeAlunoNota').value;
            var qConceito = document.getElementById('ddlConceitoNota').value;
            var qAutorizado = document.getElementById('ddlAutorizadoNota').value;
            
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fEditarNota?qid_aluno=" + qid_aluno + "&qid_nota=" + qid_nota + "&qNome=" + qNome + "&qConceito=" + qConceito + "&qAutorizado=" + qAutorizado,
                dataType: "json",
                success: function (json) {
                    if (json[0].P0 == "deslogado") {
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Nota de Aluno';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração da nota do Aluno <strong>' + qNome + '</strong>. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalEditarNota').modal('hide');
                        fPreencheMatriculaOferecimento();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração de Nota de Aluno</strong><br /><br />',
                            message: 'Alteração da nota do aluno <b>' + qNome + '</b> foi realizada com sucesso.<br />',

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
                error: function (xhr) {
                    alert("Houve um erro na alteração da nota do aluno " + qNome + ". Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () {
                    alert("Houve um erro na alteração da nota do aluno " + qNome + ". Por favor tente novamente.");
                    fFechaProcessando()
                }
            });
        }

        //===============================================================

        function fAbrePresencaAlunos(qData, qIdAula, qAchor) {
            //var qId_matricula_oferecimento = document.getElementById('txtIdMatriculaOferecimentoExcluirMatricula').value;
            var qIdAula = qIdAula;
            //fProcessando();
            var dt = $('#grdPresencaAluno').DataTable({
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
                        

                    if (oSettings.fnRecordsTotal() == 0) {
                        document.getElementById('lblTituloModalPresencaAluno').innerHTML = '<i class="fa fa-calendar-check-o"></i>&nbsp;Presença de Alunos - ' + qData;
                        document.getElementById("divgrdPresencaAluno").style.display = "none";
                        document.getElementById("msgSemResultadosgrdPresencaAluno").style.display = "block";
                        document.getElementById("divComAluno").style.display = "none";
                        document.getElementById('hCodigo').value = qIdAula;
                        document.getElementById("divSemAluno").style.display = "block";
                        document.getElementById("btnConfirmaPresencaAluno").style.display = "none";
                        document.getElementById("btnSelecionaTodosPresencaAluno").style.display = "none";
                        document.getElementById("btnDesselecionaTodosPresencaAluno").style.display = "none";

                        $('#divModalPresencaAlunos').modal();
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdPresencaAluno").style.display = "none";
                            document.getElementById("msgSemResultadosgrdPresencaAluno").style.display = "block";
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
                            document.getElementById('lblTituloModalPresencaAluno').innerHTML = '<i class="fa fa-calendar-check-o"></i>&nbsp;Presença de Alunos - ' + qData;
                            document.getElementById("divComAluno").style.display = "block";
                            document.getElementById("divSemAluno").style.display = "none";
                            document.getElementById("btnConfirmaPresencaAluno").style.display = "block";
                            document.getElementById('btnConfirmaPresencaAluno').setAttribute("onClick", "fConfirmaPresencaAluno('" + qIdAula + "','" + qAchor +"');");
                            $("#tabGerarLista").removeClass("in");
                            document.getElementById('<% =txtMensagemLista.ClientID%>').value = "";
                            document.getElementById('hCodigo').value = qIdAula;
                            document.getElementById("btnSelecionaTodosPresencaAluno").style.display = "block";
                            document.getElementById("btnDesselecionaTodosPresencaAluno").style.display = "block";
                            $('#divModalPresencaAlunos').modal();
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreenchePresencaAlunos?qIdAula=" + qIdAula + "&qData=" + qData,
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
                        "data": "P1", "title": "Matrícula", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Nome", "orderable": true, "className": "text-left", type: 'locale-compare'
                    },
                    {
                        "data": "P3", "title": "Presente", "orderable": false, "className": "text-center"
                    }
                ],
                order: [[2, 'asc']],
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

        //===============================================================

        function fConfirmaPresencaAluno(qIdAula, qAchor) {
            //alert("Clicou");
           
            //var table1 = $('#grdProfessor').DataTable();
            document.getElementById('hCodigo').value = "";

            var data = $('#grdPresencaAluno').DataTable().$('input,select,textarea').serialize();

            //alert("qIdAula: " + qIdAula);

            //alert("Data: " + data);

            data = replaceAll("chkAlunoPresenca_", "", data);
            data = replaceAll("chkAlunoPresenca_", "", data);
            data = replaceAll("=on&", ";", data);
            data = replaceAll("=on", "", data);

            //alert("Data alterada: " + data);
            //return;

            var qPresencas = data;
            
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fConfirmaPresencaAluno?qPresencas=" + qPresencas + "&qIdAula=" + qIdAula,
                dataType: "json",
                success: function (json) {
                    if (json[0].P0 == "deslogado") {
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Presenças de Alunos';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração das Presenças de Alunos. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $("#" + qAchor).removeClass("btn-danger");
                        $('#' + qAchor).addClass('btn-purple');
                        fPreencheMatriculaOferecimento();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração de Presenças de Alunos</strong><br /><br />',
                            message: 'A alteração de presenças dos alunos foi realizada com sucesso.<br />',

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
                error: function (xhr) {
                    alert("Houve um erro na alteração de Presenças de Alunos. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () {
                    alert("Houve um erro na alteração de Presenças de Alunos. Por favor tente novamente.");
                    fFechaProcessando()
                }
            });
        }

        //===============================================================

        function fSelecionarTodosPresenca() {
            //var x = document.getElementsByClassName("chkAlunoPresenca");
            var x = document.querySelectorAll('[id^=chkAlunoPresenca]');

            for (i = 0; i < x.length; i++) {
                x[i].checked = true;
                //alert(x[i]);
            }
        }

        //===============================================================

        function fDesselecionarTodosPresenca() {
            //var x = document.getElementsByClassName("chkAlunoPresenca");
            var x = document.querySelectorAll('[id^=chkAlunoPresenca]');

            for (i = 0; i < x.length; i++) {
                x[i].checked = false;
                //alert(x[i]);
            }
        }

        //===============================================================

        function fGerarListaPresencaProfessor(qIdAula) {
            //var x = document.getElementsByClassName("chkAlunoPresenca");
            var x = document.querySelectorAll('[id^=chkAlunoPresenca]');

            for (i = 0; i < x.length; i++) {
                x[i].checked = false;
                //alert(x[i]);
            }
        }

        //===============================================================

        function fGerarListaPresencaAluno(qIdAula) {
            //var x = document.getElementsByClassName("chkAlunoPresenca");
            var x = document.querySelectorAll('[id^=chkAlunoPresenca]');

            for (i = 0; i < x.length; i++) {
                x[i].checked = false;
                //alert(x[i]);
            }
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

        //============================================
        function AbreModalMensagem(qClass) {
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').removeClass('alert-danger');
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
        }

        function AbreMensagem(qClass) {
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').removeClass('alert-danger');
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
        }

        function AbreMensagem_com_parametros(qClass, qTitulo, qMensagem) {
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').removeClass('alert-danger');
            $('#divCabecalho').addClass(qClass);
            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = qTitulo;
            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = qMensagem;
            $('#divMensagemModal').modal();
        }

        //============================================

        //Para desconsiderar o acento em uma coluna na hora de ordenar
        //Colocar  
        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "locale-compare-asc": function (a, b) {
                return a.localeCompare(b, 'da', { sensitivity: 'accent' })
            },
            "locale-compare-desc": function (a, b) {
                return b.localeCompare(a, 'da', { sensitivity: 'accent' })
            }
        });

    </script>

</asp:Content>
