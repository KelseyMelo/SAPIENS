<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadTurmaGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadTurmaGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li7Turmas" />

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

        .negrito
        {
            font-weight: bold !important;
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

    <div class="row"> 
        <div class="col-md-8">
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Turma</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label><asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label></h3>
            <asp:Label  ID="lblId" runat="server" CssClass="hidden"></asp:Label>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            <button type="button" runat="server" id="btnInativar" class="btn btn-danger" href="#" onclick="fModalAtivaInativa('Inativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-off"></i> Inativar Turma
            </button>
            <button type="button" runat="server" id="btnAtivar" class="btn btn-info" href="#" onclick="fModalAtivaInativa('Ativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-on"></i> Ativar Turma
            </button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button"  runat="server" id="btnCriarTurma" name="btnCriarTurma" onserverclick="btnCriarTurma_Click" class="btn btn-primary pull-right" href="#" onclick=""  > <%--onserverclick="btnCriarTurma_Click"--%>
                    <i class="fa fa-magic"></i>&nbsp;Criar Turma</button>
            <button type="button"  runat="server" id="btnImprimirComprovanteMatricula" name="btnImprimirComprovanteMatricula" onserverclick="btnImprimirComprovanteMatricula_Click" class="btn btn-primary pull-right hidden" href="#" onclick=""  >
                    <i class="fa fa-magic"></i>&nbsp;Imprimir Comprovante</button>

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
                                        <div class="col-md-2 ">
                                            <span>Código da Turma </span><br />
                                            <input class="form-control input-sm" runat="server" id="txtNumeroTurma" type="text" value="" readonly="true"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3 ">
                                            <span>Tipo do Curso </span><span style="color:red;">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTipoCurso" onchange="fMostrarProgresso1()"  ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCurso_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-4 ">
                                            <span>Curso </span><span style="color:red;">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlCursoTurma" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCursoTurma_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                         <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3 ">
                                            <span>Data Limite Matrícula Candidatos </span><span style="color:red;">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataTerminoInscricaoTurma" type="date" value="" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-4 ">
                                            <span>Período </span><span style="color:red;">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlPeriodoTurma" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodoTurma_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Data início Período </span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataInicioPeriodoTurma" type="text" value="" readonly="true" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Data Fim Período </span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataFimPeriodoTurma" type="text" value="" readonly="true"/>
                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoCurso" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCursoTurma" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlPeriodoTurma" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li id="tabDetalheTurma" class="active"><a href="#tab_DetalheTurma" id="atab_DetalheTurma" data-toggle="tab"><strong>Detalhe</strong></a></li>
                <li id="tabCoordenadoresTurma" runat="server"><a href="#tab_CoordenadoresTurma" id="atab_CoordenadoresTurma"  data-toggle="tab"><strong>Coordenadores da Turma</strong></a></li>
                <li id="tabDisciplinaTurma" runat="server"><a href="#tab_DisciplinaTurma" id="atab_DisciplinaTurma"  data-toggle="tab"><strong>Disciplinas da Turma</strong></a></li>
                <li id="tabMatriculaTurma" class="" runat="server"><a href="#tab_MatriculaTurma" id="atab_MatriculaTurma"  data-toggle="tab"><strong>Matrícula na Turma</strong></a></li>
            </ul>

            <br />

            <div class="tab-content">
                <div class="tab-pane active" id="tab_DetalheTurma">
                    <%--                                        <b>How to use:</b>--%>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title">Detalhe da Turma</h3>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br /> 
                                </div>

                                <div class="col-md-2">
                                    <br />
                                    <button type="button" runat="server" id="bntSalvar2" name="bntSalvar2" class="btn btn-success pull-right" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvar_ServerClick">
                                                    <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <span>Data início da Turma </span><span style="color:red;">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataInicioTurma" type="date" value=""  />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Data Fim da Turma </span><span style="color:red;">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataFimTurma" type="date" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <%--<div class="col-md-2" style="line-height: 1.9em;">
                                            &nbsp;<br />
                                            <asp:CheckBox ID="chkAtivoTurma" runat="server"/>
                                            &nbsp;
                                            <label style="font-weight:normal" class="opt" for="<%=chkAtivoTurma.ClientID %>">Ativo</label>
                                        </div>--%>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <span>Carga Horária </span><span style="font-size:small">hora(s)</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtCargaHorariaTurma" type="number" value="" min="1" max="999"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Créditos </span><br />
                                            <input class="form-control input-sm" runat="server" id="txtCreditosTurma" type="number" value="" min="1" max="99"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>N.º Máx Disciplinas</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtNumeroMaxDisciplinaTurma" type="number" value="" min="1" max="99"/>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <span>Portaria MEC </span><span id="spanAsterisco_txtPortatiaMEC_Turma" runat="server" style="color:red;display:none">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtPortatiaMEC_Turma" type="number" value="" min="1" max="99999999"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Data Portaria MEC </span><span id="spanAsterisco_txtDataPortatiaMEC_Turma" runat="server" style="color:red;display:none">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataPortatiaMEC_Turma" type="date" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Data Diário Oficial </span><span id="spanAsterisco_txtDataDiarioOficialTurma" runat="server" style="color:red;display:none">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataDiarioOficialTurma" type="date" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Conceito na CAPES </span><span id="spanAsterisco_txtConceitoCapesTurma" runat="server" style="color:red;display:none">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtConceitoCapesTurma" type="number" value="" min="1" max="99"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>N.º na CAPES </span><span id="spanAsterisco_txtNumeroCapesTurma" runat="server" style="color:red;display:none">*</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtNumeroCapesTurma" type="text" value="" maxlength="50"/>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-10">
                                            <span>Observações </span><br />
                                            <textarea style ="resize:vertical; font-size:14px" runat ="server" class="form-control input-sm" rows="2" id="txtObservacaoTurma" maxlength="150"></textarea>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="box-footer">
                            <button type="button" runat="server" id="bntSalvarTurma" name="bntSalvarTurma" class="btn btn-success pull-right" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvar_ServerClick">
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
                        </div>

                    </div>
                </div>

                <div class="tab-pane" id="tab_CoordenadoresTurma">
                    <%--                                        <b>How to use:</b>--%>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3 class="box-title">Coordenadores na Turma</h3>
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
                                                                                            <th>Excluir</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <div class="col-md-3 pull-right">
                                                                            <button type="button" id="btnAssociarCoordenador" name="btnAssociarCoordenador" class="btn btn-info pull-right" href="#" onclick="fModalAssociarCoordenador()">
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

                <div class="tab-pane" id="tab_DisciplinaTurma">
                    <%--                                        <b>How to use:</b>--%>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3 class="box-title">Disciplinas da Turma</h3>
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
                                                                                <asp:Label runat="server" ID="Label6" Text="Nenhuma Disciplina associada." />
                                                                            </div>
                                                                        </div>
                                                                        <div id="divgrdDisciplina" class="table-responsive" style="display:none">
                                                                            <div class="scroll">
                                                                                <table id="grdDisciplina" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                                    <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                                        <tr>
                                                                                            
                                                                                        </tr>
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

                <div class="tab-pane" id="tab_MatriculaTurma">
                    <%--                                        <b>How to use:</b>--%>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3 class="box-title">Matrícula na Turma</h3>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br /> 
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="tab-content">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-4 ">
                                                <span>Área de Concentração </span><br />
                                                <asp:DropDownList runat="server" ID="ddlAreaConcentracaoTurma" onchange="fPreencheMatriculaTurma()" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="grid-content">
                                                    <div id="msgSemResultadosAlunosTurma" style="display:block">
                                                        <div class="alert bg-gray"> 
                                                            <asp:Label runat="server" ID="Label1" Text="Não existem alunos matriculado nesta turma para a área de concentração selecionada." />
                                                        </div>
                                                    </div>

                                                    <div id="divgrdAlunosTurma" class="table-responsive" style="display:none">
                                                        <div class="scroll">
                                                            <table id="grdAlunosTurma" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="col-md-3 pull-right">
                                                        <button type="button" id="btnAlunosTurma" name="btnAlunosTurma" class="btn btn-info pull-right" href="#" onclick="fModalAlunosTurma()">
                                                            <i class="fa fa-user-plus"></i>&nbsp;Matricular Aluno</button>
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
                <div class="modal-header bg-info">
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
                                            <input class="form-control input-sm" id="txtCPFCoordenador" type="text" value="" />
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

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaCoordenadorDisponivelTurma()" >
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
                            <button id="bntExcluirCoordenador" type="button" name="bntExcluirCoordenador" title="" class="btn btn-success" onclick="fExcluiCoordenadorTurma()" >
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
                <div class="modal-header bg-info">
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

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaDisciplinaDisponivelTurma()" >
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
                            <button id="bntExcluirDisciplina" type="button" name="bntExcluirDisciplina" title="" class="btn btn-success" onclick="fExcluiDisciplinaTurma()" >
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

        <!-- Modal para Incluir Aluno -->
    <div class="modal fade" id="divModalAlunosTurma" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Matricular Aluno</h4>
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
                                            <input class="form-control input-sm" id="txtMatriculaAluno" type="number" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeAluno" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaAlunoDisponivelTurma()" >
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
                                                <div id="msgSemResultadosgrdAlunoDisponivel" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <asp:Label runat="server" ID="Label2" Text="Nenhum Aluno disponível encontrado" />
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

        <!-- Modal para Excluir Aluno -->
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

    <!-- Modal trocar Aluno de Área de Concentração -->
    <div class="modal fade" id="divModalMudarAreaTurma" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-blue">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-exchange"></i>&nbsp;&nbsp;Mudar de Área de Concentração</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <span >Realizar a mudança do aluno <label id="lblAlunoMudanca" class="negrito"></label> da área de concentração <label class="negrito" id="lblAreaMudanca"></label> para a área abaixo:</span>
                                <input class="form-control input-sm hidden" id="txtIdMatriculaMudanca" type="text" />
                                <input class="form-control input-sm hidden" id="txtIdAlunoMudanca" type="text" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <select id="ddlAreaMudanca" class="form-control input-sm select2 SemPesquisa"></select>
                                <input class="form-control input-sm hidden" id="txtIdAreaMudanca" type="text" />
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
                            <button id="btnConfirmaMudarArea" type="button" class="btn btn-success pull-right" onclick="fMudarAreaTurma()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- ====================== -->

    <!-- Modal trocar Aluno de Área de Concentração -->
    <div class="modal fade" id="divModalExcluirAlunoTurma" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i>&nbsp;&nbsp;Excluir Matrícula do Aluno</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <span >Deseja realmente excluir a matrícula do aluno <label id="lblAlunoExcluir" class="negrito"></label> dessa turma?</span>
                                <input class="form-control input-sm hidden" id="txtIdMatriculaExcluir" type="text" />
                                <input class="form-control input-sm hidden" id="txtIdAlunoExcluir" type="text" />
                                <input class="form-control input-sm hidden" id="txtNomeAlunoExcluir" type="text" />
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
                            <button id="btnConfirmaExcluirAlunoTurma" type="button" class="btn btn-success pull-right" onclick="fExcluirAlunoTurma()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- ====================== -->


    <!-- Modal para Ativar/Inativar Turma -->
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarTurma('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarTurma('Inativar')">
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


    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>
        var vRowIndex_grdCoordenador;
        var vRowIndex_grdDisciplina;

        $('#txtCPFCoordenador').mask('999.999.999-99');

        $(document).ready(function () {
            fPreencheCoordenadorTurma();
            fPreencheDisciplinaTurma();
            if (document.getElementById("<%=tabMatriculaTurma.ClientID%>").style.display != 'none') {
                fPreencheMatriculaTurma();
            }
        });

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

        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        //================================================================================

        function fPreencheCoordenadorTurma() {
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
                    url: "wsSapiens.asmx/fPreencheCoordenadorTurma",
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

        function fPerquisaCoordenadorDisponivelTurma() {
            fProcessando();
            try {
                var qCPF = document.getElementById('txtCPFCoordenador').value;
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
                        url: "wsSapiens.asmx/fPerquisaCoordenadorDisponivelTurma?qCPF=" + qCPF + "&qNome=" + qNome,
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

        function fIncluiCoordenadorTurma(qId, qCPF, qNome) {
            
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiCoordenadorTurma?qId=" + qId,
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
                        fPreencheCoordenadorTurma();
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

        function fExcluiCoordenadorTurma() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiCoordenadorTurma?qId=" + document.getElementById('hCodigo').value,
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

        function fPreencheDisciplinaTurma() {
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

                            $('#grdDisciplina').on("click", "tr", function () {
                                vRowIndex_grdDisciplina = table_grdDisciplina.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheDisciplinaTurma",
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
                        "data": "P4", "title": "Cadastrado em", "orderable": false, "className": "text-center"
                    }
                    ,
                    {
                        "data": "P5", "title": "Cadastrado por", "orderable": false, "className": "text-center"
                    }
                    ,
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

        function fPerquisaDisciplinaDisponivelTurma() {
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
                        url: "wsSapiens.asmx/fPerquisaDisciplinaDisponivelTurma?qCodigo=" + qCodigo + "&qNome=" + qNome,
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

        function fIncluiDisciplinaTurma(qId, qCodigo, qNome) {
            
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiDisciplinaTurma?qId=" + qId,
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
                        fPreencheDisciplinaTurma();
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

        function fExcluiDisciplinaTurma() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiDisciplinaTurma?qId=" + document.getElementById('hCodigo').value,
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
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Turma';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar a Turma <strong>' + document.getElementById("<%=txtNumeroTurma.ClientID%>").value + '</strong>?' ;
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Turma';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar a Turma <strong>' + document.getElementById("<%=txtNumeroTurma.ClientID%>").value + '</strong>?' ;
            }
            $('#divModalAtivaInativa').modal();
        }

        //================================================================================

        function fPreencheMatriculaTurma() {
           
            var dt = $('#grdAlunosTurma').DataTable({
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
                "autoWidth": false,
                fnInitComplete: function (oSettings, json) {
                    //alert('Passou');
                    //CallBackReq(oSettings.fnRecordsTotal());
                    //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                    //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                    //    alert(json[i].Item);
                    //} 
                    //alert('Retorno json: ' + json);
                        

                    if(oSettings.fnRecordsTotal() == 0){
                        document.getElementById("divgrdAlunosTurma").style.display = "none";
                        document.getElementById("msgSemResultadosAlunosTurma").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdAlunosTurma").style.display = "none";
                            document.getElementById("msgSemResultadosAlunosTurma").style.display = "block";
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
                            document.getElementById("divgrdAlunosTurma").style.display = "block";
                            document.getElementById("msgSemResultadosAlunosTurma").style.display = "none";

                            var table_grdAlunosTurma = $('#grdAlunosTurma').DataTable();

                            $('#grdAlunosTurma').on("click", "tr", function(){
                                vRowIndex_grdAlunosTurma = table_grdAlunosTurma.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheMatriculaTurma?qIdArea=" +  $("#<% =ddlAreaConcentracaoTurma.ClientID%> option:selected").val(),
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
                        "data": "P2", "title": "Aluno", "orderable": true, "className": "text-left", width: '50%'
                    },
                    {
                        "data": "P3", "title": "Data", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Status", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P5", "title": "Situação", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P6", "title": "Imprimir Comprovante", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P7", "title": "Alterar Área", "Alterar Área": false, "className": "text-center"
                    },
                    {
                        "data": "P8", "title": "Excluir Matrícula", "orderable": false, "className": "text-center"
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

        function fModalAlunosTurma() {
            document.getElementById("divgrdAlunoDisponivel").style.display = "none";
            $('#divModalAlunosTurma').modal();
        }

        //================================================================================

        function fPerquisaAlunoDisponivelTurma() {
            fProcessando();
            try {
                var qMatricula = document.getElementById('txtMatriculaAluno').value;
                var qNome = document.getElementById('txtNomeAluno').value;
                var qArea = $("#<% =ddlAreaConcentracaoTurma.ClientID%> option:selected").val();
                var dt = $('#grdAlunoDisponivel').DataTable({
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
                        url: "wsSapiens.asmx/fPerquisaAlunoDisponivelTurma?qMatricula=" + qMatricula + "&qNome=" + qNome + "&qArea=" + qArea,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                        },
                        {
                            "data": "P1", "title": "Matrícula", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P2", "title": "Nome", "orderable": true, "className": "text-left"
                        },
                        {
                            "data": "P3", "title": "Status", "orderable": false, "className": "text-left"
                        },
                        {
                            "data": "P4", "title": "Adicionar", "orderable": false, "className": "text-center"
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

        function fIncluiAlunoTurma(qIdAluno, qIdAluno2, qNome) {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            var sAux = "";
            if ($("#ddlStatusAluno_" + qIdAluno).val() == "0") {
                sAux = "Deve-se selecionar um <strong><i>status</i></strong> para matricular o aluno: <strong>" + qNome + "</strong>";
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

            var qArea = $("#<% =ddlAreaConcentracaoTurma.ClientID%> option:selected").val();

            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fIncluiAlunoTurma?qIdAluno=" + qIdAluno + "&qArea=" + qArea + "&qStatus=" + $("#ddlStatusAluno_" + qIdAluno).val(),
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Matrícula de Aluno';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na matrícula do Aluno ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            $('#grdAlunoDisponivel').DataTable().row(vRowIndex_grdAlunoDisponivel).remove().draw();
                            fPreencheMatriculaTurma();
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong> Matrícula de Aluno</strong><br /><br />',
                                message: 'Matrícula do Aluno <strong>' + qNome + '</strong> realizada com sucesso.<br />',
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
                        alert("Houve um erro na matrícula do Aluno " + qNome + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na matrícula do Aluno " + qNome + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //===============================================================

        function fAbreMudarAreaTurma(qIdMatricula, qIdArea, qIdAluno, qNome) {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            var qArea = $("#<% =ddlAreaConcentracaoTurma.ClientID%> option:selected").val();
            fProcessando();
            try {
                $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fAbreMudarAreaTurma?qArea=" + qArea,
                    dataType: "json",
                    success: function (json) {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Matrícula de Aluno';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na matrícula do Aluno ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            document.getElementById('txtIdMatriculaMudanca').value = qIdMatricula;
                            document.getElementById('txtIdAlunoMudanca').value = qIdAluno;
                            document.getElementById('txtIdAreaMudanca').value = qIdArea;
                            document.getElementById('lblAlunoMudanca').innerHTML = qNome;
                            document.getElementById('lblAreaMudanca').innerHTML = $("#<% =ddlAreaConcentracaoTurma.ClientID%> option:selected").text();
                            $("#ddlAreaMudanca").empty();
                            $('#ddlAreaMudanca').select2({ data: json });
                            fSelect2();
                            $('#divModalMudarAreaTurma').modal();
                        }
                        fFechaProcessando();
                    },
                    error: function (xhr) {
                        alert("Houve um erro na matrícula do Aluno " + qNome + ". Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () {
                        alert("Houve um erro na matrícula do Aluno " + qNome + ". Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //===============================================================

        function fMudarAreaTurma() {
            var qAreaNova = $("#ddlAreaMudanca option:selected").val();
            var qMatricula = document.getElementById('txtIdMatriculaMudanca').value;
            var qAreaAntiga = document.getElementById('txtIdAreaMudanca').value;
            var qAluno = document.getElementById('txtIdAlunoMudanca').value;
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fMudarAreaTurma",
                contentType: 'application/json; charset=utf-8',
                data: "{qMatricula:'" + qMatricula + "', qAreaAntiga:'" + qAreaAntiga + "', qAreaNova:'" + qAreaNova + "', qAluno:'" + qAluno + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var json = $.parseJSON(data.d);
                    //alert(itens[0].NomeEmpresa);
                    if (json[0].Retorno == 'ok') {
                        fPreencheMatriculaTurma();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Mudança de Área de Concentração</strong><br /><br />',
                            message: 'Mudança de Área de Concentração realizada com sucesso.<br />',

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

                        $('#divModalMudarAreaTurma').modal('hide');
                    }
                    else if (json[0].Retorno == "deslogado") {
                        window.location.href = "index.html";
                    }
                    else {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Mudança de Área de Concentração';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].Resposta;
                        $("#divCabecalho").removeClass("alert-warning");
                        $("#divCabecalho").removeClass("alert-primary");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    $('#divModalMudarAreaTurma').modal('hide')

                },
                error: function (xmlHttpRequest, status, err) {
                    document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro para mudança de Área de Concentração';
                    document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro para mudar Área de Concentração <br/> Erro: ' + err + '<br/>Status do erro: ' + status;
                    $('#divModalMudarAreaTurma').modal('hide')
                    $('#divMensagemModal').modal('show');
                }
            });
        }


        //===============================================================

        function fExcluirAluno(qIdMatricula, qIdArea, qIdAluno, qNome) {
            document.getElementById('txtIdMatriculaExcluir').value = qIdMatricula;
            document.getElementById('txtIdAlunoExcluir').value = qIdAluno;
            document.getElementById('txtNomeAlunoExcluir').value = qNome;
            document.getElementById('lblAlunoExcluir').innerHTML = qNome;
            $('#divModalExcluirAlunoTurma').modal();

        }

        //===============================================================

        function fExcluirAlunoTurma() {
            var qMatricula = document.getElementById('txtIdMatriculaExcluir').value;
            var qAluno = document.getElementById('txtNomeAlunoExcluir').value;
            fProcessando();
            try
            {
                $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fExcluirAlunoTurma",
                contentType: 'application/json; charset=utf-8',
                data: "{qMatricula:'" + qMatricula + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var json = $.parseJSON(data.d);
                    //alert(itens[0].NomeEmpresa);
                    if (json[0].Retorno == 'ok') {
                        fPreencheMatriculaTurma();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão de matrícula de aluno</strong><br /><br />',
                            message: 'A exclusão da matrícula do aluno <strong>' + qAluno + '</strong> dessa turma foi realizada com sucesso.<br />',

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
                        $('#divModalExcluirAlunoTurma').modal('hide');
                    }
                    else if (json[0].Retorno == "deslogado") {
                        window.location.href = "index.html";
                    }
                    else {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão da matrícula do aluno';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].Resposta;
                        $("#divCabecalho").removeClass("alert-warning");
                        $("#divCabecalho").removeClass("alert-primary");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    $('#divModalExcluirAlunoTurma').modal('hide');
                    fFechaProcessando();
                },
                error: function (xmlHttpRequest, status, err) {
                    document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro na exclusão da matrícula do aluno';
                    document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro na exclusão da matrícula do aluno' + qAluno + '<br/> Erro: ' + err + '<br/>Status do erro: ' + status;
                    $('#divModalExcluirAlunoTurma').modal('hide')
                    $('#divMensagemModal').modal('show');
                    fFechaProcessando();
                }
            });

            } catch (e) {
                fFechaProcessando();
            }
        }

        function fComprovanteMatricula(qIdMatricula, qIdArea, qIdAluno, qNome) {
            document.getElementById('hCodigo').value = qIdMatricula;
            document.getElementById('<%=btnImprimirComprovanteMatricula.ClientID%>').click();
        }

        //===============================================================
        //=======================================

        function fAtivarInativarTurma(qOperacao){
            //alert(qOperacao);
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAtivarInativarTurma",
                contentType: 'application/json; charset=utf-8',
                data: "{qOperacao:'" + qOperacao + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var vTitulo = '';
                    var vBg = '';
                    var vIcon = '';
                    if (qOperacao == "Ativar") {
                        vTitulo = "Turma Ativado com sucesso";
                        vBg = "info";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    else {
                        vTitulo = "Turma Inativado com sucesso"
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
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Ativação/Inativação da Turma';
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
                document.getElementById('<%=spanAsterisco_txtConceitoCapesTurma.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtDataDiarioOficialTurma.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtDataPortatiaMEC_Turma.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtNumeroCapesTurma.ClientID%>').style.display = "inline-block";
                document.getElementById('<%=spanAsterisco_txtPortatiaMEC_Turma.ClientID%>').style.display = "inline-block";
            }
            else {
                document.getElementById('<%=spanAsterisco_txtConceitoCapesTurma.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtDataDiarioOficialTurma.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtDataPortatiaMEC_Turma.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtNumeroCapesTurma.ClientID%>').style.display = "none";
                document.getElementById('<%=spanAsterisco_txtPortatiaMEC_Turma.ClientID%>').style.display = "none";
            }

        });

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalAssociarCoordenador').is(':visible')) {
                    fPerquisaCoordenadorDisponivelTurma();
                }
                else if ($('#divModalAssociarDisciplina').is(':visible')) {
                    fPerquisaDisciplinaDisponivelTurma();
                }
                else if ($('#divModalAlunosTurma').is(':visible')) {
                    fPerquisaAlunoDisponivelTurma();
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
            //$('#divApagar').hide();
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $("#divCabecalho").removeClass("alert-danger");
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
