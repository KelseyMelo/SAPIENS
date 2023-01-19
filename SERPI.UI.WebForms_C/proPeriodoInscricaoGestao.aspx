<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="proPeriodoInscricaoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.proPeriodoInscricaoGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liProcessoSeletivo" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liPeriodosIncricao" />

    <script src="Scripts/jquery.mask.min.js"></script>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="https://cdn.rawgit.com/plentz/jquery-maskmoney/master/dist/jquery.maskMoney.min.js"></script>
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
        <div class="col-md-6">
            <h3 class=""><i class="fa fa-circle-o text-fuchsia"></i>&nbsp;<strong >Período de Inscrição</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            <button type="button" runat="server" id="btnExcluirPeriodoInscricao" class="btn btn-danger" href="#" onserverclick="btnExcluirPeriodoInscricao_ServerClick"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-eraser"></i> Excluir Período de Inscrição
            </button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button"  runat="server" id="btnCriarPeriodoInscricao" name="btnCriarPeriodoInscricao" onserverclick="btnCriarPeriodoInscricao_Click" class="btn btn-primary" href="#" onclick=""  > 
                    <i class="fa fa-magic"></i>&nbsp;Criar Período de Inscrição</button>
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
                                <div class="col-md-4 ">
                                    <span style="font-size:14px">Descrição do Período</span><br />
                                    <input class="form-control input-sm alteracao" runat="server" id="txtPeriodoInscricao" type="text" value="" maxlength="50"/>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtIdPeriodoInscricao" type="text" value="" style="display:none"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span style="font-size:14px">Data Início </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtDataInicioPeriodoInscricaoGestao" type="date" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span style="font-size:14px">Data Fim </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtDataFimPeriodoInscricaoGestao" type="date" value="" />
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-3">
                                    <span style="font-size:14px">Data Limíte de Pagamento </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtDataLimitePagtoPeriodoInscricaoGestao" type="date" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <span style="font-size:14px">Data da Prova </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtDataProvaPeriodoInscricaoGestao" type="date" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-6 pull-right">
                                    <br />
                                    <button type="button" runat="server" id="btnSalvarPeriodoInscricao" name="btnSalvarPeriodoInscricao" class="btn btn-success pull-right" href="#" onclick="if (ShowProgress()) return;" onserverclick="btnSalvarPeriodoInscricao_ServerClick">
                                                    <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Sessão Cursos -->
        <div class="tab-content" id="divCursos" runat ="server">

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="box-title text-bold">Cursos</h5>
                            <br />
                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="row">
                                            <div class="col-md-12">
                                                <div class="grid-content">
                                                 
                                                    <div id="msgSemResultadosCurso" style="display:block">
                                                        <div class="alert bg-gray"> 
                                                            <asp:Label runat="server" ID="Label6" Text="Nenhum Curso associado." />
                                                        </div>
                                                    </div>
                                                    <div id="divgrdCursoInscricao" class="table-responsive" style="display:none">
                                                        <div class="scroll">
                                                            <table id="grdCursoInscricao" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                    <tr>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="col-md-3 pull-right">
                                                        <button type="button" id="btnAssociarCurso" name="btnAssociarCurso" class="btn btn-warning pull-right" onclick="fModalAssociarCurso()">
                                                            <i class="fa fa-plus-square"></i>&nbsp;Incluir Curso</button>
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
                <button type="button" runat="server" id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

        </div>
    </div>

    <!-- Modal para Associar Curso -->
    <div class="modal fade" id="divModalAssociarCurso" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus-square"></i>&nbsp;Associar Curso</h4>
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
                                            <span>Sigla</span><br />
                                            <input class="form-control input-sm" id="txtSiglaCursoInscricao" type="text" value="" maxlength="10" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeCursoInscricao" type="text" value="" maxlength="350" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaCursoDisponivelPeridodoInscricao()" >
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
                                            <div id="msgSemResultadosgrdCursoDisponivelPeriodoInscricao" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label7" Text="Nenhum Curso encontrado" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdCursoDisponivelPeriodoInscricao" >
                                                <div class="scroll">
                                                    <table id="grdCursoDisponivelPeriodoInscricao" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para Incluir Curso -->
    <div class="modal fade" id="divModalIncluirCursoInscricao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-plus-circle"></i> Inclusão de Curso</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <span>Curso</span>
                            <input class="form-control input-sm" type="text" id="txtNomeCursoIncluir" readonly="true" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>Valor</span>
                            <input class="form-control input-sm" type="text" id="txtValorCursoIncluir" value="" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntIncluirCursoInscricao" type="button" name="bntIncluirCursoInscricao" title="" class="btn btn-success" onclick="fIncluiCursoPeriodoInscricao()" >
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

    <!-- Modal para Alterar Curso -->
    <div class="modal fade" id="divModalEditarCursoInscricao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-edit"></i> Editar Curso</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <span>Curso</span>
                            <input class="form-control input-sm" type="text" id="txtNomeCursoEditar" readonly="true" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>Valor</span>
                            <input class="form-control input-sm" type="text" id="txtValorCursoEditar" value="" />
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntAlterarCursoInscricao" type="button" name="bntAlterarCursoInscricao" title="" class="btn btn-success" onclick="fAlteraCursoPeriodoInscricao()" >
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

    <!-- Modal para Excluir Curso -->
    <div class="modal fade" id="divModalExcluirCursoInscricao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Curso</h4>
                </div>
                <div class="modal-body">
                    Curso: <label class="negrito" id="lblNomeCurso"></label><br />Valor: <label class="negrito" id="lblValorCurso"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirCursoInscricao" type="button" name="bntExcluirCursoInscricao" title="" class="btn btn-success" onclick="fExcluiCursoPeriodoInscricao()" >
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

    <!-- Modal para Excluir Período de Inscricao -->
    <div class="modal fade" id="divModalExcluirPeriodoInscricao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
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
        //var vRowIndex_grdDisciplina;

        $(document).ready(function () {
            fPreencheCursoPeriodoInscricao();

            $("#txtValorCursoIncluir").maskMoney({
                    prefix: "",
                    decimal: ",",
                    thousands: ".",
                    precision: 2,
                    allowZero: true,               //Permitir que os usuários entrem zero
                    allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtValorCursoEditar").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

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
        });

        //=============================================================================

        function fPreencheCursoPeriodoInscricao() {
            var dt = $('#grdCursoInscricao').DataTable({
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
                        document.getElementById("divgrdCursoInscricao").style.display = "none";
                        document.getElementById("msgSemResultadosCurso").style.display = "block";
                        document.getElementById('<%=btnExcluirPeriodoInscricao.ClientID%>').disabled = false;
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdCursoInscricao").style.display = "none";
                            document.getElementById("msgSemResultadosCurso").style.display = "block";
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        } 
                        else
                        {
                            document.getElementById("divgrdCursoInscricao").style.display = "block";
                            document.getElementById("msgSemResultadosCurso").style.display = "none";
                            document.getElementById('<% =btnExcluirPeriodoInscricao.ClientID%>').disabled = true;
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheCursoPeriodoInscricao",
                    "type": "POST",
                    "dataSrc": "",
                    error: function (xhr, error, thrown) {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Houve um erro no processamento.<br/> <br/>Descrição do Erro: " + JSON.stringify(xhr, null, 2);
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
                        "data": "P1", "title": "Sigla", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Nome Curso", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P3", "title": "Valor", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Editar", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P5", "title": "Excluir", "orderable": false, "className": "text-center"
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

        function fPerquisaCursoDisponivelPeridodoInscricao() {
            fProcessando();
            try {
                var qSigla = document.getElementById('txtSiglaCursoInscricao').value;
                var qNome = document.getElementById('txtNomeCursoInscricao').value;
                var dt = $('#grdCursoDisponivelPeriodoInscricao').DataTable({
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
                            document.getElementById("divgrdCursoDisponivelPeriodoInscricao").style.display = "none";
                            document.getElementById("msgSemResultadosgrdCursoDisponivelPeriodoInscricao").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdCursoDisponivelPeriodoInscricao").style.display = "none";
                                document.getElementById("msgSemResultadosgrdCursoDisponivelPeriodoInscricao").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdCursoDisponivelPeriodoInscricao").style.display = "block";
                                document.getElementById("msgSemResultadosgrdCursoDisponivelPeriodoInscricao").style.display = "none";
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaCursoDisponivelPeridodoInscricao?qSigla=" + qSigla + "&qNome=" + qNome,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                        },
                        {
                            "data": "P1", "title": "Sigla", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P2", "title": "Curso", "orderable": true, "className": "text-left"
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

        function fIncluiCursoPeriodoInscricao() {
           
            var qValor = document.getElementById("txtValorCursoIncluir").value;
            var qId = document.getElementById('hCodigo').value;
            var qNome = document.getElementById("txtNomeCursoIncluir").value;

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiCursoPeriodoInscricao?qId=" + qId + "&qValor=" + qValor,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Curso';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do Curso: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalIncluirCursoInscricao').modal('hide');
                        fPreencheCursoPeriodoInscricao();
                        fPerquisaCursoDisponivelPeridodoInscricao();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Curso</strong><br /><br />',
                            message: 'Inclusão do Curso <strong>' + qNome + '</strong> realizada com sucesso.<br />',

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

        function fAlteraCursoPeriodoInscricao() {

            var qValor = document.getElementById("txtValorCursoEditar").value;
            var qId = document.getElementById('hCodigo').value;
            var qNome = document.getElementById("txtNomeCursoEditar").value;

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fAlteraCursoPeriodoInscricao?qId=" + qId + "&qValor=" + qValor,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Curso';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração do Curso: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalEditarCursoInscricao').modal('hide');
                        fPreencheCursoPeriodoInscricao();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração de Curso</strong><br /><br />',
                            message: 'Alteração do Curso <strong>' + qNome + '</strong> realizada com sucesso.<br />',

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
                    alert("Houve um erro na Alteração do Curso. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração do Curso. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function fExcluiCursoPeriodoInscricao() {
            fProcessando();
            var qNome = document.getElementById("lblNomeCurso").innerHTML;
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiCursoPeriodoInscricao?qId=" + document.getElementById('hCodigo').value + "&qNome=" + qNome,
                    dataType: "json",
                    success: function(json)
                    {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Curso';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do Curso: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            $('#divModalExcluirCursoInscricao').modal('hide');
                            fPreencheCursoPeriodoInscricao();
                            
                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Curso</strong><br /><br />',
                                message: 'Exclusão do Curso realizada com sucesso.<br />',

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
                        $('#divModalExcluirCursoInscricao').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão do Curso. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirCursoInscricao').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão do Curso. Por favor tente novamente!");
                        $('#divModalExcluirCursoInscricao').hide();
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
                if ($('#divModalAssociarCurso').is(':visible')) {
                    fPerquisaCursoDisponivelPeridodoInscricao();
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

        function fModalAssociarCurso() {
            document.getElementById("divgrdCursoDisponivelPeriodoInscricao").style.display = "none";
            $('#divModalAssociarCurso').modal();
        }

        function AbreModalIncluirCurso(qIdCurso, qSigla, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('txtNomeCursoIncluir').value = qSigla + " - " + qNome;
            document.getElementById('txtValorCursoIncluir').value = "0";
            document.getElementById('hCodigo').value = qIdCurso;
            $('#divModalIncluirCursoInscricao').modal();
        }

        function AbreModalEditarCurso(qIdCurso, qSigla, qNome, qValor) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('txtNomeCursoEditar').value = qSigla + " - " + qNome;
            document.getElementById('txtValorCursoEditar').value = qValor;
            document.getElementById('hCodigo').value = qIdCurso;
            $('#divModalEditarCursoInscricao').modal();
        }

        function AbreModalApagarCurso(qIdCurso, qSigla, qNome, qValor) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeCurso').innerHTML = qSigla + " - " + qNome;
            document.getElementById('lblValorCurso').innerHTML = qValor;
            document.getElementById('hCodigo').value = qIdCurso;
            $('#divModalExcluirCursoInscricao').modal();
        }

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>