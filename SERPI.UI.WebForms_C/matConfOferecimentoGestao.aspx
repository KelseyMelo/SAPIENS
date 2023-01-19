<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="matConfOferecimentoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.matConfOferecimentoGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liMatricula" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liConfirmacaoOferecimento" />

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
        <div class="col-md-6">
            <h3 class=""><i class="fa fa-circle-o text-yellow"></i>&nbsp;<strong >Confirmação de Oferecimento</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label></h3>
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
                                <div class="col-md-7 ">
                                    <span style="font-size:14px">Período de Matrícula</span><br />
                                    <input class="form-control input-sm alteracao" runat="server" id="txtPeriodoConfirmacaoMatricula" type="text" value="" readonly="true"/>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtIdPreOferecimento" type="text" value="" style="display:none"/>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-2 ">
                                    <span style="font-size:14px">Código</span><br />
                                    <input class="form-control input-sm alteracao" runat="server" id="txtCodigoConfirmacaoMatricula" type="text" value="" readonly="true"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-5">
                                    <span style="font-size:14px">Nome </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                                    <input class="form-control input-sm alteracao" runat="server" id="txtNomeConfirmacaoMatricula" type="text" value="" readonly="true"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Sessão Disciplina -->
        <div class="row">
            <div class="col-xs-12">
        <div class="panel panel-default">

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <h5 class="box-title text-bold">Alunos</h5>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-primary">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="grid-content">
                                                    <div id="msgSemResultadosgrdAlunoConfirmacaoMatricula" style="display:block">
                                                        <div class="alert bg-gray">
                                                            <asp:Label runat="server" ID="Label8" Text="Nenhum Aluno inscrito" />
                                                        </div>
                                                    </div>
                                                    <div class="table-responsive" id="divgrdAlunoConfirmacaoMatricula" style="display:block">
                                                        <div class="scroll">
                                                            <table id="grdAlunoConfirmacaoMatricula" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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
                                    <div class="panel-footer" id="divFooter">
                                        <div class="row">
 
                                            <div class="col-xs-3 center-block">
                                                <button id="btnDesselecionaTodosConfMatricula" type="button" class="btn btn-danger center-block" onclick="fDesselecionarTodosConfMatricula()">
                                                <i class="fa fa-square-o"></i>&nbsp;Desmarcar Todos</button>
                                            </div>
                                            <div class="hidden-lg hidden-md"> 
                                                <br />
                                            </div>

                                            <div class="col-xs-3 center-block">
                                                <button id="btnSelecionaTodosConfMatricula" type="button" class="btn btn-primary center-block" onclick="fSelecionarTodosConfMatricula()">
                                                <i class="fa fa-check-square-o"></i>&nbsp;Marcar Todos</button>
                                            </div>
                                            <div class="hidden-lg hidden-md"> 
                                                <br />
                                            </div>

                                            <div class="col-xs-3 pull-right">
                                                <button id="btnConfirmaConfMatricula" type="button" class="btn btn-success pull-right" onclick="fConfirmaConfMatricula()">
                                                <i class="fa fa-check"></i>&nbsp;Confirmar</button>
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
                <button type="button" runat="server" id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

        </div>
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

                                            <button type="button" title="" class="btn btn-success" onclick="fPerquisaDisciplinaDisponivelPeridodoMatricula()" >
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

    <!-- Modal para Alterar Disciplina -->
    <div class="modal fade" id="divModalIncluirDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-plus-circle"></i> Inclusão de Disciplina</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <span>Disciplina</span>
                            <input class="form-control input-sm" type="text" id="txtNomeDisciplinaIncluir" readonly="true" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>Vagas</span>
                            <input class="form-control input-sm" type="number" id="txtVagaDisciplinaIncluir" min="1"  />
                        </div>
                        <div class="hidden-lg hidden-md"> 
                            <br />
                        </div>

                        <div class="col-md-4"> 
                            <span>Dia da Semana</span>
                            <select id="ddlDiaDisciplinaIncluir" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                <option selected value="">não especificado</option> 
                                <option value="segunda-feira">segunda-feira</option> 
                                <option value="terça-feira">terça-feira</option> 
                                <option value="quarta-feira">quarta-feira</option> 
                                <option value="quinta-feira">quinta-feira</option> 
                                <option value="sexta-feira">sexta-feira</option> 
                                <option value="sábado">sábado</option> 
                                <option value="domingo">domingo</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntIncluirDisciplina" type="button" name="bntIncluirDisciplina" title="" class="btn btn-success" onclick="fIncluiDisciplinaPeriodoMatricula()" >
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

    <!-- Modal para Alterar Disciplina -->
    <div class="modal fade" id="divModalEditarDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-edit"></i> Editar de Disciplina</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <span>Disciplina</span>
                            <input class="form-control input-sm" type="text" id="txtNomeDisciplinaEditar" readonly="true" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>Vagas</span>
                            <input class="form-control input-sm" type="number" id="txtVagaDisciplinaEditar"  />
                        </div>
                        <div class="hidden-lg hidden-md"> 
                            <br />
                        </div>

                        <div class="col-md-4"> 
                            <span>Dia da Semana</span>
                            <select id="ddlDiaDisciplinaEditar" class="form-control input-sm select2 SemPesquisa" style="display:inline-block">
                                <option selected value="">não especificado</option> 
                                <option value="segunda-feira">segunda-feira</option> 
                                <option value="terça-feira">terça-feira</option> 
                                <option value="quarta-feira">quarta-feira</option> 
                                <option value="quinta-feira">quinta-feira</option> 
                                <option value="sexta-feira">sexta-feira</option> 
                                <option value="sábado">sábado</option> 
                                <option value="domingo">domingo</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntAlterarDisciplina" type="button" name="bntAlterarDisciplina" title="" class="btn btn-success" onclick="fAlteraDisciplinaPeriodoMatricula()" >
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

    <!-- Modal para Excluir Disciplina -->
    <div class="modal fade" id="divModalExcluirDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Disciplina</h4>
                </div>
                <div class="modal-body">
                    Disciplina: <label class="negrito" id="lblCodigoDisciplina"></label> - <label class="negrito" id="lblNomeDisciplina"></label><br />Vagas: <label class="negrito" id="lblVagasDisciplina"></label><br />Dia: <label class="negrito" id="lblDiaDisciplina"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirDisciplina" type="button" name="bntExcluirDisciplina" title="" class="btn btn-success" onclick="fExcluiDisciplinaPeriodoMatricula()" >
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

    <!-- Modal para Excluir Período de Matrícula -->
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

        $(document).ready(function () {
            fPreencheAlunoConfirmacaoMatricula();

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

        function fPreencheAlunoConfirmacaoMatricula() {
            var dt = $('#grdAlunoConfirmacaoMatricula').DataTable({
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
                        document.getElementById("divgrdAlunoConfirmacaoMatricula").style.display = "none";
                        document.getElementById("msgSemResultadosgrdAlunoConfirmacaoMatricula").style.display = "block";
                        document.getElementById("divFooter").style.display = "none";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdAlunoConfirmacaoMatricula").style.display = "none";
                            document.getElementById("msgSemResultadosgrdAlunoConfirmacaoMatricula").style.display = "block";
                            document.getElementById("divFooter").style.display = "none";
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
                            document.getElementById("divgrdAlunoConfirmacaoMatricula").style.display = "block";
                            document.getElementById("msgSemResultadosgrdAlunoConfirmacaoMatricula").style.display = "none";
                            document.getElementById("divFooter").style.display = "block";
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheAlunoConfirmacaoMatricula",
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
                        "data": "P0", "title": "Matrícula", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P1", "title": "Aluno", "orderable": true, "className": "text-left", type: 'locale-compare'
                    },
                    {
                        "data": "P2", "title": "Matriculado", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Ação", "orderable": false, "className": "text-center"
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

        function fSelecionarTodosConfMatricula() {
            //var x = document.getElementsByClassName("chkAlunoPresenca");
            var x = document.querySelectorAll('[id^=chkAlunoConfMatricula]');

            for (i = 0; i < x.length; i++) {
                x[i].checked = true;
                //alert(x[i]);
            }
        }

        //===============================================================

        function fDesselecionarTodosConfMatricula() {
            //var x = document.getElementsByClassName("chkAlunoPresenca");
            var x = document.querySelectorAll('[id^=chkAlunoConfMatricula]');

            for (i = 0; i < x.length; i++) {
                x[i].checked = false;
                //alert(x[i]);
            }
        }

        //===============================================================

        function fConfirmaConfMatricula() {
            var x = document.querySelectorAll('[id^=chkAlunoConfMatricula]');
            var qMatricular = "";
            var qDesmatricular = "";
            for (i = 0; i < x.length; i++) {
                if (x[i].classList.contains('sim') && x[i].checked == false) {
                    if (qDesmatricular != "") {
                        qDesmatricular = qDesmatricular + ";";
                    }
                    qDesmatricular = qDesmatricular + x[i].name.replace("chkAlunoConfMatricula_", "");
                }
                if (x[i].classList.contains('nao') && x[i].checked == true) {
                    if (qMatricular != "") {
                        qMatricular = qMatricular + ";";
                    }
                    qMatricular = qMatricular + x[i].name.replace("chkAlunoConfMatricula_", "");
                }
            }
            
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fConfirmaConfMatricula?qMatricular=" + qMatricular + "&qDesmatricular=" + qDesmatricular,
                dataType: "json",
                success: function (json) {
                    if (json[0].P0 == "deslogado") {
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Confirmação de Matrícula de Alunos';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Confirmação de Matrícula de Alunos. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheAlunoConfirmacaoMatricula();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Confirmação de Matrícula de Alunos</strong><br /><br />',
                            message: 'A Confirmação de Matrícula dos alunos foi realizada com sucesso.<br />',

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
                    alert("Houve um erro na Confirmação de Matrícula de Alunos. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () {
                    alert("Houve um erro na Confirmação de Matrícula de Alunos. Por favor tente novamente.");
                    fFechaProcessando()
                }
            });
        }

        //================================================================================

        function fPerquisaDisciplinaDisponivelPeridodoMatricula() {
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
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaDisciplinaDisponivelPeridodoMatricula?qCodigo=" + qCodigo + "&qNome=" + qNome,
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

        function fIncluiDisciplinaPeriodoMatricula() {
            
            var sAux = "";
            if (document.getElementById("txtVagaDisciplinaIncluir").value == "" || document.getElementById("txtVagaDisciplinaIncluir").value <= "0" || isNaN(document.getElementById("txtVagaDisciplinaIncluir").value)) {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Deve-se digitar um número de vagas";
                $('#divModalAssociarTamanho').modal('hide');
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            var qVaga = document.getElementById("txtVagaDisciplinaIncluir").value;
            var qId = document.getElementById('hCodigo').value;
            var qNome = document.getElementById("txtNomeDisciplinaIncluir").value;

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiDisciplinaPeriodoMatricula?qId=" + qId + "&qVaga=" + qVaga + "&qDia=" + $('#ddlDiaDisciplinaIncluir').val(),
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
                        $('#divModalIncluirDisciplina').modal('hide');
                        fPreencheDisciplinaPeriodoMatricula();

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

        function fAlteraDisciplinaPeriodoMatricula() {
            
            var sAux = "";
            if (document.getElementById("txtVagaDisciplinaEditar").value == "" || document.getElementById("txtVagaDisciplinaEditar").value <= "0" || isNaN(document.getElementById("txtVagaDisciplinaEditar").value)) {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Deve-se digitar um número de vagas";
                $('#divModalAssociarTamanho').modal('hide');
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();
                return;
            }

            var qVaga = document.getElementById("txtVagaDisciplinaEditar").value;
            var qId = document.getElementById('hCodigo').value;
            var qNome = document.getElementById("txtNomeDisciplinaEditar").value;

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fAlteraDisciplinaPeriodoMatricula?qId=" + qId + "&qVaga=" + qVaga + "&qDia=" + $('#ddlDiaDisciplinaEditar').val(),
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Alteração de Disciplina';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na alteração da Disciplina: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalEditarDisciplina').modal('hide');
                        fPreencheDisciplinaPeriodoMatricula();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Alteração de Disciplina</strong><br /><br />',
                            message: 'Alteração da Disciplina <strong>' + qNome + '</strong> realizada com sucesso.<br />',

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
                    alert("Houve um erro na Alteração da Disciplina. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Alteração da Disciplina. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //============================================================================

        function fExcluiDisciplinaPeriodoMatricula() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiDisciplinaPeriodoMatricula?qId=" + document.getElementById('hCodigo').value,
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
                            $('#divModalExcluirDisciplina').modal('hide');
                            fPreencheDisciplinaPeriodoMatricula();
                            
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

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalAssociarDisciplina').is(':visible')) {
                    fPerquisaDisciplinaDisponivelPeridodoMatricula();
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

        function fModalAssociarDisciplina() {
            document.getElementById("divgrdDisciplinaDisponivel").style.display = "none";
            $('#divModalAssociarDisciplina').modal();
        }

        function AbreModalIncluirDisciplina(qIdDisciplina, qCodigo, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('txtNomeDisciplinaIncluir').value = qCodigo + " - " + qNome;
            document.getElementById('txtVagaDisciplinaIncluir').value = "20";
            $("#ddlDiaDisciplinaIncluir").val("").trigger("change");
            document.getElementById('hCodigo').value = qIdDisciplina;
            $('#divModalIncluirDisciplina').modal();
        }

        function AbreModalEditarDisciplina(qId_pre_oferecimento, qIdDisciplina, qCodigo, qNome, qVaga, qDia) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('txtNomeDisciplinaEditar').value = qCodigo + " - " + qNome;
            document.getElementById('txtVagaDisciplinaEditar').value = qVaga;
            $("#ddlDiaDisciplinaEditar").val(qDia).trigger("change");
            document.getElementById('hCodigo').value = qId_pre_oferecimento;
            $('#divModalEditarDisciplina').modal();
        }

        function AbreModalApagarDisciplina(qId_pre_oferecimento, qIdDisciplina, qCodigo, qNome, qVaga, qDia) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeDisciplina').innerHTML = qNome;
            document.getElementById('lblCodigoDisciplina').innerHTML = qCodigo;
            document.getElementById('lblVagasDisciplina').innerHTML = qVaga;
            document.getElementById('lblDiaDisciplina').innerHTML = qDia;
            document.getElementById('hCodigo').value = qId_pre_oferecimento;
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
