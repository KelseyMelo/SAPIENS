<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="admTelaGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.admTelaGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAdmSistema" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liCadTela" />

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
            <h3 class=""><i class="fa fa-circle-o text-aqua"></i>&nbsp;<strong >Tela</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            <button type="button"  runat="server" id="btnCriarTela" name="btnCriarTela" class="btn btn-primary " href="#" onclick="" onserverclick="btnCriarTela_Click"> <%--onserverclick="btnCriarTela_Click"--%> 
                    <i class="fa fa-magic"></i>&nbsp;Criar Tela</button>

        </div>

    </div>
    <br />

    <div class="container-fluid">
        <div class="tab-content">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <span style="font-size:14px">Tela (SERPI) </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtTelaSerpi" type="text" value="" maxlength="80"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4">
                                    <span style="font-size:14px">Descrição (SERPI) </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtDescricaoSerpi" type="text" value="" maxlength="80"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4">
                                    <span style="font-size:14px">Módulo (SERPI) </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <asp:DropDownList runat="server" ID="ddlModuloSerpi" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                        <asp:ListItem Text="Selecione um módulo" Value="Selecione um módulo" />
                                        <asp:ListItem Text="Acadêmico" Value="Acadêmico" />
                                        <asp:ListItem Text="Administração Sistema" Value="Administração Sistema" />
                                        <asp:ListItem Text="Financeiro" Value="Financeiro" />
                                        <asp:ListItem Text="Matrícula" Value="Matrícula" />
                                        <asp:ListItem Text="Processo Seletivo" Value="Processo Seletivo" />
                                        <asp:ListItem Text="Relatórios Gerenciais" Value="Relatórios Gerenciais" />
                                        <asp:ListItem Text="Outros" Value="Outros" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-4">
                                    <span style="font-size:14px">Módulo (SAPIENS) </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <asp:DropDownList runat="server" ID="ddlModuloSapiens" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                        <asp:ListItem Text="Selecione um módulo" Value="Selecione um módulo" />
                                        <asp:ListItem Text="Administração Sistema" Value="Administração Sistema" />
                                        <asp:ListItem Text="Processo Seletivo" Value="Processo Seletivo" />
                                        <asp:ListItem Text="Acadêmico" Value="Acadêmico" />
                                        <asp:ListItem Text="Matrícula" Value="Matrícula" />
                                        <asp:ListItem Text="Financeiro" Value="Financeiro" />
                                        <asp:ListItem Text="Monitor" Value="Monitor" />
                                        <asp:ListItem Text="Outros" Value="Outros" />
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4">
                                    <span style="font-size:14px">Descrição (SAPIENS) </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtDescricaoSapiens" type="text" value="" maxlength="80"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span>Situação</span><br />
                                    <div class="row center-block btn-default form-group">
                                        <div class="col-md-6">
                                            <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoSim" runat="server" Checked="true" />
                                            &nbsp;
                                <label class="opt" for="<%=optSituacaoSim.ClientID %>">Ativo</label>
                                        </div>

                                        <div class="col-md-6">
                                            <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoNao" runat="server" />
                                            &nbsp;
                                <label class="opt" for="<%=optSituacaoNao.ClientID %>">Inativo</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-1">
                                    <br />
                                    <button runat="server" type="button" id="btnSalvarTela" name="btnSalvarTela" class="btn btn-success pull-right" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvarTela_Click"> <%----%>
                                        <i class="fa fa-floppy-o"></i>&nbsp;Salvar</button>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Sessão Grupo -->
        <div class="tab-content" id="divGrupos" runat ="server" visible="false">

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="box-title text-bold">Grupos Associados  <i class="fa fa-info-circle fa-lg" style="color:blueviolet" data-toggle="tooltip" title="Grupos associados a essa tela."></i></h5>
                            <br />
                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="row">
                                            <div class="col-md-12">
                                                <div class="grid-content">
                                                 
                                                    <div id="msgSemResultadosGrupo" style="display:block">
                                                        <div class="alert bg-gray"> 
                                                            <asp:Label runat="server" ID="Label6" Text="Nenhum Grupo associado." />
                                                        </div>
                                                    </div>
                                                    <div id="divgrdGrupo" class="table-responsive" style="display:none">
                                                        <div class="scroll">
                                                            <table id="grdGrupo" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                    <tr>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="col-md-3 pull-right">
                                                        <button type="button" id="btnAssociarGrupo" name="btnAssociarGrupo" class="btn btn-warning pull-right" href="#" onclick="fModalAssociarGrupo()">
                                                            <i class="fa fa-plus-square"></i>&nbsp;Associar Grupo</button>
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
                                            <div id="msgSemResultadosgrdGrupoDisponivel" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label7" Text="Nenhum Grupo encontrado" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdGrupoDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdGrupoDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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
                            <span>Tela</span>
                            <input class="form-control input-sm" type="text" id="txtNomeTelaEditar" readonly="true" />

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

                    <div class="row">
                        <div class="col-md-12">
                            <span>Tipo de acesso</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-6">
                                    <asp:RadioButton GroupName="GrupoSituacao" ID="optEscritaTela" runat="server" />
                                    &nbsp;
                        <label class="opt" for="<%=optEscritaTela.ClientID %>">Escrita</label>
                                </div>

                                <div class="col-md-6">
                                    <asp:RadioButton GroupName="GrupoSituacao" ID="optLeituraTela" runat="server" />
                                    &nbsp;
                        <label class="opt" for="<%=optLeituraTela.ClientID %>">Leitura</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    

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

    <!-- Modal para Excluir Tela -->
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
        var vRowIndex_grdGrupo;

        $(document).ready(function () {
            if ($('#<%=divGrupos.ClientID%>').length) {
                fPreencheGrupoTela();
                fSelect2();
            }
            else {
                fSelect2();
            }
            
        });

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

        function fPreencheGrupoTela() {
            var dt = $('#grdGrupo').DataTable({
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
                        document.getElementById("divgrdGrupo").style.display = "none";
                        document.getElementById("msgSemResultadosGrupo").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdGrupo").style.display = "none";
                            document.getElementById("msgSemResultadosGrupo").style.display = "block";
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
                            document.getElementById("divgrdGrupo").style.display = "block";
                            document.getElementById("msgSemResultadosGrupo").style.display = "none";
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheGrupoTela",
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
                        "data": "P0", "title": "Grupo", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P1", "title": "Tipo Acesso", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Editar Acesso", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Excluir", "orderable": false, "className": "text-center"
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
        }

        //================================================================================

        function fPerquisaGrupoDisponivel() {
            fProcessando();
            try {
                var qIdGrupo = document.getElementById('txtCodigoGrupo').value;
                var qNome = document.getElementById('txtNomeGrupo').value;
                var dt = $('#grdGrupoDisponivel').DataTable({
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
                            document.getElementById("divgrdGrupoDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdGrupoDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdGrupoDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdGrupoDisponivel").style.display = "block";
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
                                document.getElementById("divgrdGrupoDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdGrupoDisponivel").style.display = "none";
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
        
        function fEditarGrupo(qNomeGrupo) {
            fProcessando();
            try {
                var qPermissao;
                if (document.getElementById('<%=optEscritaTela.ClientID%>').checked) {
                    qPermissao = "escrita";
                }
                else {
                    qPermissao = "leitura";
                }
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
                            fPreencheGrupoTela();
                            
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
            if (!document.getElementById('<%=optEscritaTela.ClientID%>').checked && !document.getElementById('<%=optLeituraTela.ClientID%>').checked) {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Associação de Grupo';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Deve-se selecionar o Tipo de Acesso.';
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            var qPermissao;
            if (document.getElementById('<%=optEscritaTela.ClientID%>').checked) {
                qPermissao = "escrita";
            }
            else {
                qPermissao = "leitura";
            }

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
                            fPreencheGrupoTela();
                            
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
                            fPreencheGrupoTela();
                            
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

            document.getElementById('txtNomeTelaEditar').value = document.getElementById('<%=txtDescricaoSapiens.ClientID%>').value;
            document.getElementById('txtNomeGrupoEditar').value = qNomeGrupo;
            document.getElementById('hCodigo').value = qIdGrupo;
            if (qTipoAcesso == "Escrita") {
                document.getElementById('<%=optEscritaTela.ClientID%>').checked = true;
                document.getElementById('<%=optLeituraTela.ClientID%>').checked = false;
            }
            else {
                document.getElementById('<%=optEscritaTela.ClientID%>').checked = false;
                document.getElementById('<%=optLeituraTela.ClientID%>').checked = true;    
            }
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

            document.getElementById('txtNomeTelaEditar').value = document.getElementById('<%=txtDescricaoSapiens.ClientID%>').value;
            document.getElementById('txtNomeGrupoEditar').value = qNome;
            document.getElementById('hCodigo').value = qIdGrupo;
            document.getElementById('<%=optEscritaTela.ClientID%>').checked = false;
            document.getElementById('<%=optLeituraTela.ClientID%>').checked = false;
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

            document.getElementById('txtNomeTelaEditar').value = document.getElementById('<%=txtDescricaoSapiens.ClientID%>').value;
            document.getElementById('txtNomeGrupoEditar').value = qNomeGrupo;
            document.getElementById('hCodigo').value = qIdGrupo;
            if (qTipoAcesso == "Escrita") {
                document.getElementById('<%=optEscritaTela.ClientID%>').checked = true;
                document.getElementById('<%=optLeituraTela.ClientID%>').checked = false;
            }
            else {
                document.getElementById('<%=optEscritaTela.ClientID%>').checked = false;
                document.getElementById('<%=optLeituraTela.ClientID%>').checked = true;    
            }
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
