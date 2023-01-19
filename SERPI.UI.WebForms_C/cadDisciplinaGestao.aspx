<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadDisciplinaGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadDisciplinaGestao" validateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li3Disciplinas" />

    <input type="hidden" id ="htxtObjetivoDisciplina"  name="htxtObjetivoDisciplina" value="" />
    <input type="hidden" id ="htxtJustificativaDisciplina"  name="htxtJustificativaDisciplina" value="" />
    <input type="hidden" id ="htxtEmentaDisciplina"  name="htxtEmentaDisciplina" value="" />
    <input type="hidden" id ="htxtFormaAvaliacaoDisciplina"  name="htxtFormaAvaliacaoDisciplina" value="" />
    <input type="hidden" id ="htxtMaterialUtilizadoDisciplina"  name="htxtMaterialUtilizadoDisciplina" value="" />
    <input type="hidden" id ="htxtMetodologiaDisciplina"  name="htxtMetodologiaDisciplina" value="" />
    <input type="hidden" id ="htxtConhecimentosPreviosDisciplina"  name="htxtConhecimentosPreviosDisciplina" value="" />
    <input type="hidden" id ="htxtProgramaDisciplina"  name="htxtProgramaDisciplina" value="" />
    <input type="hidden" id ="htxtBibliografiaBasicaDisciplina"  name="htxtBibliografiaBasicaDisciplina" value="" />
    <input type="hidden" id ="htxtBibliografiaComplementarDisciplina"  name="htxtBibliografiaComplementarDisciplina" value="" />
    <input type="hidden" id ="htxtObservacaoDisciplina"  name="htxtObservacaoDisciplina" value="" />

    <script src="Scripts/jquery.mask.min.js"></script>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
    
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="" />
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

            /*input[type=checkbox] {
                transform: scale(0.8);
            }

            input[type=checkbox] {
                width: 20px;
                height: 20px;
                margin-right: 8px;
                cursor: pointer;
                font-size: 18px; 
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
            }

            input[type=checkbox]:checked:after {
                content: "\2714";
                padding: -5px;
                font-weight: bold;
            }*/

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
    <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel2"  >
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

    <div class="row"> 
        <div class="col-md-4">
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Disciplina</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label><asp:Label ID="lblInativadoDisciplina" ForeColor="Red" runat="server" Text=" (Inativada)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button" runat="server" id="btnInativar" class="btn btn-danger" href="#" onclick="fModalAtivaInativa('Inativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-off"></i> Inativar Disciplina
            </button>
            <button type="button" runat="server" id="btnAtivar" class="btn btn-info" href="#" onclick="fModalAtivaInativa('Ativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-on"></i> Ativar Disciplina
            </button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            <button type="button" runat="server" id="btnImprimirEmenta" class="btn btn-warning" href="#" onclick="" onserverclick="btnImprimirEmenta_Click" > <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-print"></i>&nbsp;Imprimir Ementa</button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarDisciplina" name="btnCriarDisciplina" onserverclick="btnCriarDisciplina_Click" class="btn btn-primary" href="#" onclick=""  > <%--onserverclick="btnCriarDisciplina_Click"--%>
                        <i class="fa fa-magic"></i>&nbsp;Criar Disciplina</button>
            </div>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button" runat="server" id="bntSalvar2" name="bntSalvar2" class="btn btn-success pull-right hidden" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvar_ServerClick1">
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
            <button type="button" class="btn btn-success pull-right" onclick="fbtnSalvar()">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Salvar dados</button>
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
                                    <input class="form-control input-sm" runat="server" id="txtCodigoDisciplina" type="text" value="" maxlength="7"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-6 ">
                                    <span>Nome </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNomeDisciplina" type="text" value="" maxlength="150"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>N.º Máx Alunos</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNumeroMaxAlunosDisciplina" type="number" value="" min="1" max="99"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2">
                                    <span>Obrigatório</span><br />
                                    <asp:DropDownList runat="server" ID="ddlObrigatorioDisciplina" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                        <asp:ListItem Text="Não" Value="false" />
                                        <asp:ListItem Text="Sim" Value="true" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-4 ">
                                    <span>Área de Concentração </span><%--<span style="color:red;">*</span>--%><br />
                                    <asp:DropDownList runat="server" ID="ddlAreaConcentracaoDisciplina" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Data Criação</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDataCriacaoDisciplina" type="date" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Data Última Alteração</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDataUltimaAlteracaoDisciplina" type="date" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Créditos </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCreditosDisciplina" type="number" value="" min="1" max="99"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Carga Horária </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCargaHorariaDisciplina" type="number" value="" min="1" max="99"/>
                                </div>

                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <span>Substituindo</span><br />
                                    <asp:DropDownList runat="server" ID="ddlDisciplinaSubstituta" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div id="divUrl" runat="server">
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>

                                    <div class="col-md-3">
                                        <span>URL da Ementa</span><br />
                                        <input class="form-control input-sm" runat="server" id="txtUrlEmenta" type="text" value="" readonly="true"/>
                                    </div>
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>

                                    <div class="col-md-2">
                                        <br />
                                        <button type="button" id="btnCopyURL" class="btn btn-default pull-right" href="#" onclick="fCopyUrl()"> 
                                        <i class="fa fa-copy"></i>&nbsp;Copiar Link</button>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab_Dados" data-toggle="tab"><strong>Dados Disciplina</strong></a></li>
                <li id="tabProfessores" runat="server"><a href="#tab_Professores" data-toggle="tab"><strong>Professores</strong></a></li>
                <li id="tabTecnicos" runat="server"><a href="#tab_Tecnicos" data-toggle="tab"><strong>Técnicos</strong></a></li>
                <li id="tabRequisitadas" runat="server"><a href="#tab_Requisitadas" data-toggle="tab"><strong>Disciplinas Pré-requisitadas</strong></a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="tab_Dados">
                    <!-- Sessão Ementa -->
                    <div class="tab-content">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Objetivos</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtObjetivoDisciplina"></textarea>
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-6 ">
                                                <span>Justificativa</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtJustificativaDisciplina"></textarea>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Ementa </span><span style="color: red;">*</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtEmentaDisciplina" htmlencode="true"></textarea>
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-6 ">
                                                <span>Forma de Avaliação</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtFormaAvaliacaoDisciplina" htmlencode="false"></textarea>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Material Utilizado</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtMaterialUtilizadoDisciplina" htmlencode="false"></textarea>
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-6 ">
                                                <span>Metodologia</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtMetodologiaDisciplina" htmlencode="false"></textarea>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Conhecimentos Prévios</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtConhecimentosPreviosDisciplina" htmlencode="false"></textarea>
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-6 ">
                                                <span>Programa da Disciplina</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtProgramaDisciplina" htmlencode="false"></textarea>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Bibliografia Básica </span><span style="color: red;">*</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtBibliografiaBasicaDisciplina" htmlencode="false"></textarea>
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-6 ">
                                                <span>Bibliografia Complementar</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtBibliografiaComplementarDisciplina" htmlencode="false"></textarea>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Observação</span><br />
                                                <textarea style="resize: none; font-size: 14px" runat="server" class="form-control input-sm" rows="6" id="txtObservacaoDisciplina" htmlencode="false"></textarea>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.tab-pane -->
                <div class="tab-pane" id="tab_Professores">
                    <!-- Sessão Professor -->
                    <div class="tab-content" id="divProfessor" runat="server" style="display: none">

                        <div class="panel panel-default">

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
                                                                                            <th>id</th>
                                                                                            <th>CPF</th>
                                                                                            <th>Nome</th>
                                                                                            <th>Responsavel</th>
                                                                                            <th>Excluir</th>
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

                    <div class="tab-content" id="divProfessoresAdicionados" runat="server">
                        <div class="tab-content">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h5 class="box-title text-bold">Professores Adicionados</h5>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <button type="button" id="btnAdicionarProfessores" class="btn btn-warning" href="#" onclick="fModalAdicionarProfessor()">
                                                <i class="fa fa-user-plus"></i>&nbsp;Adicionar Professor</button>
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
                                                                                <div runat="server" id="msgSemResultadogrdProfessorAdicionado">
                                                                                    <div class="alert bg-gray">
                                                                                        <asp:Label runat="server" ID="Label1" Text="Não existem Professores para esta disciplina." />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="table-responsive ">

                                                                                    <asp:GridView ID="grdProfessorAdicionado" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                                                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                        <Columns>

                                                                                            <asp:BoundField DataField="id_professor" HeaderText="id_professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                            <asp:BoundField DataField="cpf" HeaderText="CPF" ItemStyle-HorizontalAlign="Left" />

                                                                                            <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                            <%--<asp:BoundField DataField="DisciplinaCodigo" HeaderText="Disciplina" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />--%>

                                                                                            <asp:TemplateField HeaderText="Responsável" ItemStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox OnClick="fCheckResponsavel(this)" Checked='<%# (bool) DataBinder.Eval(Container.DataItem, "responsavel") ? true : false%>' ID="chkResponsavel" CssClass="checkbox text-center" runat="server"></asp:CheckBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <span style="position: relative;">
                                                                                                        <i class="fa fa-close btn btn-danger btn-circle"></i>
                                                                                                        <asp:Button HorizontalAlign="Center" ToolTip="Desassociar Professor" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdProfessorAdicionado_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                </div>
                <!-- /.tab-pane -->
                <div class="tab-pane" id="tab_Tecnicos">
                    <!-- Sessão Técnico -->
                    <div class="tab-content" id="divTecnico" runat="server">

                        <div class="panel panel-default">

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h5 class="box-title text-bold">Técnicos Adicionados</h5>

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
                                                                                <asp:Label runat="server" ID="Label6" Text="Nenhum Técnico associado." />
                                                                            </div>
                                                                        </div>
                                                                        <div id="divgrdTecnico" class="table-responsive" style="display: none">
                                                                            <div class="scroll">
                                                                                <table id="grdTecnico" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
                                                                                    <thead style="color: White; background-color: #507CD1; font-weight: bold;">
                                                                                        <tr>
                                                                                            <th>id</th>
                                                                                            <th>CPF</th>
                                                                                            <th>Nome</th>
                                                                                            <th>Excluir</th>
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
                                                <button type="button" id="btnAssociarTecnico" name="btnAssociarTecnico" class="btn btn-warning pull-right" href="#" onclick="fModalAssociarTecnico()">
                                                    <i class="fa fa-user-plus"></i>&nbsp;Incluir Técnico</button>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="tab-content" id="divTecnicoAdicionados" runat="server">
                        <div class="tab-content">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h5 class="box-title text-bold">Técnicos Adicionados</h5>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <button type="button" id="btnAdicionarTecnico" class="btn btn-warning" href="#" onclick="fModalAdicionarTecnico()">
                                                <i class="fa fa-user-plus"></i>&nbsp;Adicionar Técnico</button>
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
                                                                                <div runat="server" id="msgSemResultadogrdTecnicoAdicionado">
                                                                                    <div class="alert bg-gray">
                                                                                        <asp:Label runat="server" ID="Label3" Text="Não existem Técnicos para esta disciplina." />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="table-responsive ">

                                                                                    <asp:GridView ID="grdTecnicoAdicionado" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                                                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                        <Columns>

                                                                                            <asp:BoundField DataField="id_professor" HeaderText="id_professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                            <asp:BoundField DataField="cpf" HeaderText="CPF" ItemStyle-HorizontalAlign="Left" />

                                                                                            <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                            <%--<asp:BoundField DataField="DisciplinaCodigo" HeaderText="Disciplina" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />--%>

                                                                                            <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <span style="position: relative;">
                                                                                                        <i class="fa fa-close btn btn-danger btn-circle"></i>
                                                                                                        <asp:Button HorizontalAlign="Center" CssClass="movedown" ToolTip="Desassociar Técnico" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdTecnicoAdicionado_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                </div>
                <!-- /.tab-pane -->
                <div class="tab-pane" id="tab_Requisitadas">
                    <!-- Sessão Disciplinas Pré-requisitadas -->
                    <div class="tab-content" id="divPreRequisito" runat="server">

                        <div class="panel panel-default">

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h5 class="box-title text-bold">Disciplinas Pré-requisitadas</h5>

                                        <div class="tab-content">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12 ">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="grid-content">

                                                                        <div id="msgSemResultadosPrerequisito" style="display: block">
                                                                            <div class="alert bg-gray">
                                                                                <asp:Label runat="server" ID="Label8" Text="Nenhuma Disciplina Pré-requisitada." />
                                                                            </div>
                                                                        </div>
                                                                        <div id="divgrdPrerequisito" class="table-responsive" style="display: none">
                                                                            <div class="scroll">
                                                                                <table id="grdPrerequisito" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
                                                                                    <thead style="color: White; background-color: #507CD1; font-weight: bold;">
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
                                                <button type="button" id="btnAssociarPrerequisito" name="btnAssociarTecnico" class="btn btn-warning pull-right" onclick="fModalAssociarPrerequisito()">
                                                    <i class="fa fa-plus"></i>&nbsp;Incluir Pré-requisito</button>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.tab-pane -->
            </div>
            <!-- /.tab-content -->
        </div>

        <div class="row">

            <div class="col-xs-2">
                <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

            <button type="button" runat="server" id="bntSalvarNoticia" name="bntSalvar" class="btn btn-success pull-right hidden" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvar_ServerClick1">
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
            <button type="button" class="btn btn-success pull-right" onclick="fbtnSalvar()">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Salvar dados</button>

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

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaProfessorDisponivelDisciplina()" >
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
                            <button id="bntExcluirProfessor" type="button" name="bntExcluirProfessor" title="" class="btn btn-success" onclick="fExcluiProfessorDisciplina()" >
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
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Associar Técnico</h4>
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

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaTecnicoDisponivelDisciplina()" >
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
                                                    <asp:Label runat="server" ID="Label7" Text="Nenhum Técnico disponível encontrado" />
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
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Técnico</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir o Técnico: <label id="lblNomeTecnico"></label> - CPF: <label id="lblCPFTecnico"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirTecnico" type="button" name="bntExcluirTecnico" title="" class="btn btn-success" onclick="fExcluiTecnicoDisciplina()" >
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

    <!-- Modal para Associar Pre-requisito -->
    <div class="modal fade" id="divModalAssociarPrerequisito" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Associar Pré-requisito</h4>
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
                                            <input class="form-control input-sm" id="txtCodigoPrerequisito" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomePrerequisito" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaPrerequisitoDisponivelDisciplina()" >
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
                                            <div id="msgSemResultadosgrdPrerequisitoDisponivel" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label9" Text="Nenhum Técnico disponível encontrado" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdPrerequisitoDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdPrerequisitoDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

        <!-- Modal para Excluir Pre-requisito -->
    <div class="modal fade" id="divModalExcluirPrerequisito" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Pré-requisito</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir o Pré-requisito:<br /> <label class="negrito" id="lblCodigoPrerequisito"></label> - <label class="negrito"  id="lblNomePrerequisito"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirPrerequisito" type="button" name="bntExcluirPrerequisito" title="" class="btn btn-success" onclick="fExcluiPrerequisitoDisciplina()" >
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

    <!--======================================================= -->

    <!-- Modal para Associar Professor -->
    <div class="modal fade" id="divModalAdicionarProfessor" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-user-plus"></i>&nbsp;Adicionar Professor</h4>
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
                                                    <input class="form-control input-sm" runat="server" id="txtCPFPerquisaProfessor" type="text" value="" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-8">
                                                    <span>Nome</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtNomePerquisaProfessor" type="text" value="" maxlength="70" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-1">
                                                    <div class="hidden-xs hidden-sm">
                                                        <br />
                                                    </div>

                                                    <button type="button" onclick="if (fMostrarProgresso3()) return;" id="bntPerquisaProfessor" runat="server" name="bntPerquisaProfessor" title="" class="btn btn-success" onserverclick="bntPerquisaProfessor_Click" >
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
                                                            <div class="tab-content" runat="server" id="divResultadoListaProfessorDisponivel" style="display:none">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="grid-content">
                                                                            <div runat="server" id="divgrdProfessoresDisponiveis" style="display:none">
                                                                                <div class="alert bg-gray">
                                                                                    <asp:Label runat="server" ID="Label2" Text="Não existem Professores disponíveis." />
                                                                                </div>
                                                                            </div>
                                                                            <div class="table-responsive ">

                                                                                <asp:GridView ID="grdProfessoresDisponiveis" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                    AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                                                                    SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                    <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                    <Columns>

                                                                                        <asp:BoundField DataField="id_professor" HeaderText="id_professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                        <asp:BoundField DataField="cpf" HeaderText="CPF" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:TemplateField HeaderText="Incluir" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <span style="position: relative;">
                                                                                                    <i class="fa fa-check btn btn-success btn-circle"></i>
                                                                                                    <asp:Button OnClientClick="if (fMostrarProgresso3()) return;" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdProfessoresDisponiveis_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Associar Técnico -->
    <div class="modal fade" id="divModalAdicionarTecnico" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-user-plus"></i>&nbsp;Adicionar Técnico</h4>
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
                                                    <span>CPF</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtCPFPerquisaTecnico" type="text" value="" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-8">
                                                    <span>Nome</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtNomePerquisaTecnico" type="text" value="" maxlength="70" />
                                                </div>
                                                <div class="hidden-lg hidden-md"> 
                                                    <br />
                                                </div>

                                                <div class="col-md-1">
                                                    <div class="hidden-xs hidden-sm">
                                                        <br />
                                                    </div>

                                                    <button type="button" id="bntPerquisaTecnico" runat="server" name="bntPerquisaTecnico" title="" class="btn btn-success" onserverclick="bntPerquisaTecnico_Click" >
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
                                                                <div class="row" runat="server" id="divResultadoListaTecnicoDisponivel" style="display:none">
                                                                    <div class="col-md-12">
                                                                        <div class="grid-content">
                                                                            <div runat="server" id="divgrdTecnicoDisponiveis" style="display:none">
                                                                                <div class="alert bg-gray">
                                                                                    <asp:Label runat="server" ID="Label4" Text="Não existem Técnicos disponíveis." />
                                                                                </div>
                                                                            </div>
                                                                            <div class="table-responsive ">

                                                                                <asp:GridView ID="grdTecnicoDisponiveis" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                                    AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                                                                    SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                    <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                                    <Columns>

                                                                                        <asp:BoundField DataField="id_professor" HeaderText="id_professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                                        <asp:BoundField DataField="cpf" HeaderText="CPF" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                                        <asp:TemplateField HeaderText="Incluir" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <span style="position: relative;">
                                                                                                    <i class="fa fa-check btn btn-success btn-circle"></i>
                                                                                                    <asp:Button HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdTecnicoDisponiveis_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                <div class="modal-header bg-warning">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Coordenador</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir o Coordenador: <label id="lblNomeCoordenador"></label> - CPF: <label id="lblCPFCoordenador"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-2 pull-right">
                            <button id="bntExcluirCoordenador" type="button" name="bntExcluirCoordenador" title="" class="btn btn-success" onclick="fExcluiCoordenador()" >
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>
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

    <!-- Modal para Ativar/Inativar Disciplina -->
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarDisciplina('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarDisciplina('Inativar')">
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
    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

     <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>

    <script src="Scripts/nicEdit.js" type="text/javascript"></script>

    <script>
        var vRowIndex_grdCoordenador;
        
        $('#txtCPFProfessor').mask('999.999.999-99');

        $('#txtCPFTecnico').mask('999.999.999-99');

        $('#<%=txtCPFPerquisaProfessor.ClientID%>').mask('999.999.999-99');

        $('#<%=txtCPFPerquisaTecnico.ClientID%>').mask('999.999.999-99');

        function fModalAssociarProfessor() {
            document.getElementById("divgrdProfessorDisponivel").style.display = "none";
            $('#divModalAssociarProfessor').modal();
        }

        function fModalAssociarTecnico() {
            document.getElementById("divgrdTecnicoDisponivel").style.display = "none";
            $('#divModalAssociarTecnico').modal();
        }

        function fModalAssociarPrerequisito() {
            document.getElementById("divgrdPrerequisitoDisponivel").style.display = "none";
            $('#divModalAssociarPrerequisito').modal();
        }

        function fbtnSalvar() {
            //alert("Clicou");
           
            //var table1 = $('#grdProfessor').DataTable();
            document.getElementById('hCodigo').value = "";

            var data = $('#grdProfessor').DataTable().$('input,select,textarea').serialize();
            data = replaceAll("chkResponsavel_", "", data);
            data = replaceAll("chkResponsavel_", "", data);
            data = replaceAll("=on&", ";", data);
            data = replaceAll("=on", "", data);

            //alert("data: " + data);

            document.getElementById('hCodigo').value = data;

            var nicEE = new nicEditors.findEditor('<%=txtObjetivoDisciplina.ClientID%>');
            document.getElementById('htxtObjetivoDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtJustificativaDisciplina.ClientID%>');
            document.getElementById('htxtJustificativaDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtEmentaDisciplina.ClientID%>');
            document.getElementById('htxtEmentaDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtFormaAvaliacaoDisciplina.ClientID%>');
            document.getElementById('htxtFormaAvaliacaoDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtMaterialUtilizadoDisciplina.ClientID%>');
            document.getElementById('htxtMaterialUtilizadoDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtMetodologiaDisciplina.ClientID%>');
            document.getElementById('htxtMetodologiaDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtConhecimentosPreviosDisciplina.ClientID%>');
            document.getElementById('htxtConhecimentosPreviosDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtProgramaDisciplina.ClientID%>');
            document.getElementById('htxtProgramaDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtBibliografiaBasicaDisciplina.ClientID%>');
            document.getElementById('htxtBibliografiaBasicaDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtBibliografiaComplementarDisciplina.ClientID%>');
            document.getElementById('htxtBibliografiaComplementarDisciplina').value = nicEE.getContent();

            nicEE = new nicEditors.findEditor('<%=txtObservacaoDisciplina.ClientID%>');
            document.getElementById('htxtObservacaoDisciplina').value = nicEE.getContent();

            document.getElementById('<%=bntSalvar2.ClientID%>').click();
        }

        function fMostrarProgresso3()
        {
            document.getElementById('<%=UpdateProgress3.ClientID%>').style.display = "block";
        }

        

        bkLib.onDomLoaded(function () {
            new nicEditor({ iconsPath: 'img/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtObjetivoDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtJustificativaDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtEmentaDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtFormaAvaliacaoDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtMaterialUtilizadoDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtMetodologiaDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtConhecimentosPreviosDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtProgramaDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtBibliografiaBasicaDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtBibliografiaComplementarDisciplina.ClientID%>');
            new nicEditor({ iconsPath: 'https://cdnjs.cloudflare.com/ajax/libs/NicEdit/0.93/nicEditorIcons.gif', buttonList: ['bold', 'italic', 'underline', 'left', 'center', 'right', 'justify', 'ol', 'ul', 'fontSize', 'fontFamily', 'fontFormat', 'indent', 'outdent', 'subscript', 'superscript', 'strikethrough', 'removeformat', 'xhtml'] }).panelInstance('<%=txtObservacaoDisciplina.ClientID%>');

        });

        //============================================================================
        function fModalAtivaInativa(qOperacao) {
            if (qOperacao == 'Ativa') {
                $("#divCabecAtiva").removeClass("bg-danger");
                $('#divCabecAtiva').addClass('bg-info');
                document.getElementById("btnConfirmaAtivar").style.display = 'block';
                document.getElementById("btnConfirmaInativar").style.display = 'none';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Disciplina';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar a disciplina <strong>' + document.getElementById("<%=txtNomeDisciplina.ClientID%>").value + '</strong>?' ;
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Disciplina';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar a disciplina <strong>' + document.getElementById("<%=txtNomeDisciplina.ClientID%>").value + '</strong>?' ;
            }
            $('#divModalAtivaInativa').modal();
        }

        //===============================================================

        //=======================================

        function fAtivarInativarDisciplina(qOperacao){
            //alert(qOperacao);
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAtivarInativarDisciplina",
                contentType: 'application/json; charset=utf-8',
                data: "{qOperacao:'" + qOperacao + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var vTitulo = '';
                    var vBg = '';
                    var vIcon = '';
                    if (qOperacao == "Ativar") {
                        vTitulo = "Disciplina Ativada com sucesso";
                        vBg = "info";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    else {
                        vTitulo = "Disciplina Inativada com sucesso"
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
                            document.getElementById('<%=lblInativadoDisciplina.ClientID%>').style.display='none';
                        }
                        else {
                            document.getElementById('<%=btnAtivar.ClientID%>').style.display='block';
                            document.getElementById('<%=btnInativar.ClientID%>').style.display='none';
                            document.getElementById('<%=lblInativadoDisciplina.ClientID%>').style.display='block';
                        }

                        $('#divModalAtivaInativa').modal('hide');
                    }
                    else if (json[0].Retorno == "deslogado") {
                        window.location.href = "index.html";
                    }
                    else {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Ativação/Inativação da Disciplina';
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

        //=======================================



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


        $(document).ready(function () {
            if (document.getElementById("<%=divProfessor.ClientID%>").style.display == 'block') {
                fPreencheProfessorDisciplina();
                fPreencheTecnicoDisciplina();
                fPreenchePrerequisitoDisciplina();
            }
        });

        //================================================================================

        function fPreencheProfessorDisciplina() {
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
                    url: "wsSapiens.asmx/fPreencheProfessorDisciplina",
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
                        "data": "P3", "title": "Responsável", "orderable": true, "className": "text-center"
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

        function fPerquisaProfessorDisponivelDisciplina() {
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
                        url: "wsSapiens.asmx/fPerquisaProfessorDisponivelDisciplina?qCPF=" + qCPF + "&qNome=" + qNome,
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

        function fIncluiProfessorDisciplina(qId, qCPF, qNome) {
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiProfessorDisciplina?qId=" + qId,
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
                        fPreencheProfessorDisciplina();
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

        function fExcluiProfessorDisciplina() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiProfessorDisciplina?qId=" + document.getElementById('hCodigo').value,
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

        function fPreencheTecnicoDisciplina() {
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
                    url: "wsSapiens.asmx/fPreencheTecnicoDisciplina",
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
                        "data": "P3", "title": "Excluir", "orderable": false, "className": "text-center"
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

        function fPerquisaTecnicoDisponivelDisciplina() {
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
                        url: "wsSapiens.asmx/fPerquisaTecnicoDisponivelDisciplina?qCPF=" + qCPF + "&qNome=" + qNome,
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

        function fIncluiTecnicoDisciplina(qId, qCPF, qNome) {
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiTecnicoDisciplina?qId=" + qId,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Técnico';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do Técnico: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheTecnicoDisciplina();
                        //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.';
                        //$("#divCabecalho").removeClass("alert-warning");
                        //$("#divCabecalho").removeClass("alert-danger");
                        //$('#divCabecalho').addClass('alert-success');
                        //$('#divMensagemModal').modal();
                        $('#grdTecnicoDisponivel').DataTable().row(vRowIndex_grdTecnicoDisponivel).remove().draw();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Técnico</strong><br /><br />',
                            message: 'Inclusão do Técnico <strong>' + qNome + '</strong> realizada com sucesso.<br />',

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
                    alert("Houve um erro na inclusão do Técnico. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão do Técnico. Por favor tente novamente!");
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

        function fExcluiTecnicoDisciplina() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiTecnicoDisciplina?qId=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Técnico';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do Técnico: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            $('#grdTecnico').DataTable().row(vRowIndex_grdTecnico).remove().draw();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Técnico</strong><br /><br />',
                                message: 'Exclusão do Técnico realizada com sucesso.<br />',

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
                        alert("Houve um erro na exclusão do Técnico. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirTecnico').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão do Técnico. Por favor tente novamente!");
                        $('#divModalExcluirTecnico').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //================================================================================

        function fPreenchePrerequisitoDisciplina() {
            var dt = $('#grdPrerequisito').DataTable({
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
                        document.getElementById("divgrdPrerequisito").style.display = "none";
                        document.getElementById("msgSemResultadosPrerequisito").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdPrerequisito").style.display = "none";
                            document.getElementById("msgSemResultadosPrerequisito").style.display = "block";
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                            $('#divModalAssociarPrerequisito').modal('hide');
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        } 
                        else
                        {
                            document.getElementById("divgrdPrerequisito").style.display = "block";
                            document.getElementById("msgSemResultadosPrerequisito").style.display = "none";

                            var table_grdPrerequisito = $('#grdPrerequisito').DataTable();

                            $('#grdPrerequisito').on("click", "tr", function () {
                                vRowIndex_grdPrerequisito = table_grdPrerequisito.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreenchePrerequisitoDisciplina",
                    "type": "POST",
                    "dataSrc": "",
                    error: function (xhr, error, thrown) {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Houve um erro no processamento.<br/> <br/>Descrição do Erro: " + JSON.stringify(xhr, null, 2);
                        $('#divModalAssociarPrerequisito').modal('hide');
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
                        "data": "P3", "title": "Excluir", "orderable": false, "className": "text-center"
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

        function fPerquisaPrerequisitoDisponivelDisciplina() {
            fProcessando();
            try {
                var qCodigo = document.getElementById('txtCodigoPrerequisito').value;
                var qNome = document.getElementById('txtNomePrerequisito').value;
                var dt = $('#grdPrerequisitoDisponivel').DataTable({
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
                            document.getElementById("divgrdPrerequisitoDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdPrerequisitoDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdPrerequisitoDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdPrerequisitoDisponivel").style.display = "block";
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
                                document.getElementById("divgrdPrerequisitoDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdPrerequisitoDisponivel").style.display = "none";

                                var table_grdPrerequisitoDisponivel = $('#grdPrerequisitoDisponivel').DataTable();

                                $('#grdPrerequisitoDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdPrerequisitoDisponivel = table_grdPrerequisitoDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaPrerequisitoDisponivelDisciplina?qCodigo=" + qCodigo + "&qNome=" + qNome,
                        "type": "POST",
                        "dataSrc": "",
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

        function fIncluiPrerequisitoDisciplina(qId, qCodigo, qNome) {
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiPrerequisitoDisciplina?qId=" + qId,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Pré-requisito';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do Pré-requisito: ' + qCodigo + ' - ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreenchePrerequisitoDisciplina();
                        //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.';
                        //$("#divCabecalho").removeClass("alert-warning");
                        //$("#divCabecalho").removeClass("alert-danger");
                        //$('#divCabecalho').addClass('alert-success');
                        //$('#divMensagemModal').modal();
                        $('#grdPrerequisitoDisponivel').DataTable().row(vRowIndex_grdPrerequisitoDisponivel).remove().draw();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Pré-requisito</strong><br /><br />',
                            message: 'Inclusão do Pré-requisito <strong>' + qCodigo + ' - ' + qNome + '</strong> realizada com sucesso.<br />',

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
                    alert("Houve um erro na inclusão do Pré-requisito. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão do  Pré-requisito. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function AbreModalApagarPrerequisito(qId, qCodigo, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomePrerequisito').innerHTML = qNome;
            document.getElementById('lblCodigoPrerequisito').innerHTML = qCodigo;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirPrerequisito').modal();
        }

        //============================================================================

        function fExcluiPrerequisitoDisciplina() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiPrerequisitoDisciplina?qId=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Pré-requisito';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do Pré-requisito. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            $('#grdPrerequisito').DataTable().row(vRowIndex_grdPrerequisito).remove().draw();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Pré-requisito</strong><br /><br />',
                                message: 'Exclusão do Pré-requisito realizada com sucesso.<br />',

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
                        $('#divModalExcluirPrerequisito').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão do Pré-requisito. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirTecnico').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão do Pré-requisito. Por favor tente novamente!");
                        $('#divModalExcluirTecnico').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //=================================================================

        function fAtiva_grdProfessoresDisponiveis() {
            $('#<%=grdProfessoresDisponiveis.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
        }

        function fAtiva_grdTecnicoDisponiveis() {
            $('#<%=grdTecnicoDisponiveis.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
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

        function fModalAdicionarProfessor() {
            document.getElementById('<%=divResultadoListaProfessorDisponivel.ClientID%>').style.display = "none";
            $('#divModalAdicionarProfessor').modal();
        }

        function fModalAdicionarTecnico() {
            document.getElementById('<%=divResultadoListaTecnicoDisponivel.ClientID%>').style.display = "none";
            $('#divModalAdicionarTecnico').modal();
        }

        function fMostrarProgresso()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function fCheckObrigatoria(obj) {
            if (obj.checked) {
                var id = obj.getAttribute("id").split("_");
                document.getElementById("ContentPlaceHolderBody_grdDisciplinas_chkAssociar_" + id[3]).checked = true;
            }
        }

        function fCheckAssociar(obj) {
            if (!obj.checked) {
                var id = obj.getAttribute("id").split("_");
                document.getElementById("ContentPlaceHolderBody_grdDisciplinas_chkObrigatoria_" + id[3]).checked = false;
            }
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalAdicionarProfessor').is(':visible')) {
                    document.getElementById("<%=bntPerquisaProfessor.ClientID%>").click();
                }
                else if ($('#divModalAdicionarTecnico').is(':visible')) {
                    document.getElementById("<%=bntPerquisaTecnico.ClientID%>").click();
                }
                else if ($('#divModalAssociarProfessor').is(':visible')) {
                    fPerquisaProfessorDisponivelDisciplina();
                }
                else if ($('#divModalAssociarTecnico').is(':visible')) {
                    fPerquisaTecnicoDisponivelDisciplina();
                }
                else if ($('#divModalAssociarPrerequisito').is(':visible')) {
                    fPerquisaPrerequisitoDisponivelDisciplina();
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

        function AbreModalApagarCoordenador(qId, qCPF, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeCoordenador').innerHTML = qNome;
            document.getElementById('lblCPFCoordenador').innerHTML = qCPF;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirCoordenador').modal();
        }

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
