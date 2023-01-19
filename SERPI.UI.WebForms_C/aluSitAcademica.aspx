<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="aluSitAcademica.aspx.cs" Inherits="SERPI.UI.WebForms_C.aluSitAcademica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAlunoSitAcademica" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liAlunoSitAcademica" />
    <input type="hidden" id ="hEscolheuFoto"  name="hEscolheuFoto" value="false" />

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigoAluno"  name="hCodigoAluno" value="value" />

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/all.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>

    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet" />
      
    <style type="text/css">

        .select2 {
            width: 100% !important;
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

        input{
            font-size:14px;
        }

        .nav>li.active>a {
            background-color: #fff !important;
            color: #444 !important;
        }

        td.highlight {
            font-weight: bold;
            color: red;
        }

        .img-redondo2{
            border-radius: 50% !important;
            width: 70px !important;
            height: 70px !important;
        }

    </style>

    <div class="row"> 
        <div class="col-md-9">
            <h3 class="">&nbsp;&nbsp;&nbsp;<i class="fa fa-graduation-cap"></i>&nbsp;Situação acadêmica</h3>
        </div>

    </div>

    <div class="container-fluid">

        <div class="row">
            <div class="col-lg-10">
                <div class="box box-widget widget-user-2">
                    <div class="widget-user-header bg-aqua-active">
                        <div class="widget-user-image">
                            <img id="imgAluno" runat="server" onerror="fImagemDefaultMaster(this);" style="border: 2px solid white" class="img-redondo2" src="img/pessoas/40708.jpg?26/03/2019 10:02:06" alt="Imagem do aluno"/>
                        </div>
                        <h3 class="widget-user-username"><asp:Label ID="lblNomeAluno" runat="server" Text="Kelsey Magalhães Melo"></asp:Label></h3>
                        <h5 class="widget-user-desc"><asp:Label ID="lblCargoAluno" runat="server" Text="Analista de Sistemas Sênior"></asp:Label></h5>
                    </div>
                
                    <div class="box-footer">
                        <div class="row">
                            <div class="col-sm-6 border-right">
                                <div class="description-block">
                                <h5 class="description-header">Matrícula</h5>
                                <span class="description-text"><asp:Label ID="lblMatriculaAluno" runat="server" Text="40708"></asp:Label></span>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-6 border-right">
                                <div class="description-block">
                                <h5 class="description-header">CPF</h5>
                                <span class="description-text"><asp:Label ID="lblCPFAluno" runat="server" Text="111.111111-11"></asp:Label></span>
                                </div>
                                <!-- /.description-block -->
                            </div>

                        </div>

                        <h4 style="color:black"><strong>Turma</strong></h4>
                         <hr style="border-bottom:2px solid #3c8dbc" />
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <div id="divTurmaDiversasNew" style="display:none">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <span>Outras Turmas</span><br />
                                            <select id="ddlTurmaAlunoNew" class="form-control input-sm select2 SemPesquisa">
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                </div>

                                <div id="divTurmaTemNew" style="display:none">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <span>Código Turma</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtCodTurmaAlunoNew" type="text" value="" readonly="readonly" />
                                            <input class="form-control input-sm" runat="server" id="txtIdTurmaAlunoNew" type="text" value="" readonly="readonly" style="display:none" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Período</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtQuadrimestreAlunoNew" type="text" value="" readonly="readonly" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Data Início</span><br />
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input class="form-control input-sm" runat="server" id="txtDataInicioCursoAlunoNew" type="text" value="" readonly="readonly" />
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
                                                <input class="form-control input-sm" runat="server" id="txtDataFimCursoAlunoNew" type="text" value="" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>Data de Término <i class="fa fa-info-circle" style="color:blueviolet" data-toggle="tooltip" title="O cálculo da 'Data de Término' é feito somando-se à 'Data Fim' original da Turma a diferença entre a 'Data Início' e a 'Data Fim' de todos os 'Trancamentos' e 'Prorrogações CPG' aprovados."></i></span><br />
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input class="form-control input-sm" runat="server" id="txtDataTerminoCursoAlunoNew" type="text" value="" readonly="readonly" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <span>Tipo Curso</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtTipoCursoAlunoNew" type="text" value="" readonly="readonly" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-4 ">
                                            <span>Curso</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtCursoAlunoNew" type="text" value="" readonly="readonly" />
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-4 ">
                                            <span>Área de Concentração</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtAreaConcentracaoAlunoNew" type="text" value="" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>

                                <div id="divTurmaNaoTemNew">
                                    <div class="row">
                                        <br />
                                        <div class="col-md-12 ">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="lblMsgSemResultadosNew" Text="Sem Turma associada" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <br />

                                <div id="divTabs" class="row">
                                    <div class="col-lg-12">
                                        <br />
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs">
                                                <li id="tabHistoricoAlunoNew" runat="server" class="active"><a href="#<%=tab_HistoricoAlunoNew.ClientID %>" data-toggle="tab"><strong>Histórico Escolar</strong></a></li>
                                                <li id="tabMatriculaAlunoNew" runat="server" class=""><a href="#<%=tab_MatriculaAlunoNew.ClientID %>" data-toggle="tab"><strong>Matrícula <em>(on-line)</em></strong></a></li>
                                            </ul>

                                            <div class="tab-content">
                                                <%--Histórico do Aluno--%>
                                                <div class="tab-pane active" runat="server" id="tab_HistoricoAlunoNew">
                                                    <div class="tab-content">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body">

                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="grid-content">
                                                                                <div id="msgSemResultadosgrdHistoricoAlunoNew" style="display:block">
                                                                                    <div class="alert bg-gray">
                                                                                        <asp:Label runat="server" ID="Label4" Text="Nenhum Histórico encontrado" />
                                                                                    </div>
                                                                                </div>
                                                                                <div id="divgrdHistoricoAlunoNew" class="table-responsive" style="display:none">
                                                                                    <div class="">
                                                                                        <table id="grdHistoricoAlunoNew" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                                            <caption>RELAÇÃO DE DISCIPLINAS</caption>
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


                                                    <div id="divBotoesImpressaoHistoricoNew" class="row">
                                                        <div class="col-md-12">
                                                            <button runat="server" type="button" id="btnImprimirHitoricoOffNew" name="btnImprimirHitoricoOff" class="btn btn-primary center-block" onclick="if (funcClicaImprimirHistorico()) return;" onserverclick="btnImprimir_Click">
                                                                <i class="fa fa-print"></i>&nbsp;Imprimir Histórico</button>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
                                                        </div>

                                                    </div>

                                                </div>

                                                <%--Matrícula do Aluno--%>
                                                <div class="tab-pane" runat="server" id="tab_MatriculaAlunoNew">
                                                    <div class="tab-content">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body">
                                                            
                                                                <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="grid-content">
                                                                                <div id="msgSemResultadosgrdMatricula" style="display:block">
                                                                                    <div class="alert bg-gray">
                                                                                        <asp:Label runat="server" ID="Label1" Text="Nenhuma disciplina disponível" />
                                                                                    </div>
                                                                                </div>
                                                                                <div id="divgrdMatricula" class="table-responsive" style="display:none">
                                                                                    <div class="">
                                                                                        <table id="grdMatricula" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                                            <caption>DISCIPLINAS DISPONÍVEIS PARA SEREM CURSADAS NO PERÍODO</caption>
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

                                                    <div id="divBotaoConfirmarMatricula" class="row" runat="server">
                                                        <div class="col-md-12">
                                                            <button type="button" class="btn btn-primary pull-right" onclick="fConfirmaMatricula()">
                                                                <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                                                        </div>
                                                        <div class="hidden-lg hidden-md">
                                                            <br />
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



    <div id="divPogress" class="loading" align="center">
        Processando... <br />Por favor, aguarde.
        <br />
        <img src="img/loader.gif" width="42" height="42" alt="" />
    </div>

    <div class="modal fade" id="divModalPresenca" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-info ">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="">
                        <div class="row text-center">
                            <span class="text-center">RELAÇÃO DE PRESENÇA</span><br />
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                            Disciplina: <strong><label id="lblCodigoDisciplinaModalPresenca">&nbsp;</label></strong><br />
                            Nome: <strong><label id="lblNomeDisciplinaModalPresenca">&nbsp;</label></strong>
                            </div>
                        </div>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="Label3" Text="" />
                    <div class="container-fluid text-center">
                        <div class="row text-center">
                            <div class="col-md-12">
                                <table  class="display table table-striped table-bordered table-condensed table-hover" id="tabPresenca" cellspacing="0" width="100%">
                                    <thead>
                                        <tr style="color:White;background-color:#507CD1;font-weight:bold;" >
                                            <th>Nº Aula</th>
                                            <th>Data da Aula</th>
                                            <th>Hora de Início</th>
                                            <th>Hora de Término</th>
                                            <th>Situação</th>
                                        </tr>
                                    </thead>
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
        </div>
    </div>

    <div class="modal fade" id="divModalDisciplina" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" id="">
                        <div class="row text-center">
                            <span class="text-center">Dados do Oferecimento da Turma</span><br />
                        </div>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="Label5" Text="" />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Nome</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblNomeDisciplinaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Código</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblCodigoDisciplinaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Quadrimestre</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblQuadrimestreModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>nº do Oferec.</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblNoOferecimentoModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Objetivo</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblObjetivoModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Ementa</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblEmentaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Bibliografia Básica</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblBibliografiaBasicaModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Bibliografia Complementar</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblBibliografiaComplementarModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo1">
                                    <label>Forma de Avaliação</label>
                                </div>
                                <div class="col-md-10 corCorpo1">
                                    <label id="lblFormaAvaliacaoModalDisciplina">&nbsp;</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 corTitulo2">
                                    <label>Observação</label>
                                </div>
                                <div class="col-md-10 corCorpo2">
                                    <label id="lblObservacaoModalDisciplina">&nbsp;</label>
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
        </div>
    </div>

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
        </div>
    </div>
    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

    <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>

    <script>

        $(document).ready(function () {
            fPreencheGrupoTurmaAluno();
        });

        //=========================================================

        function fPreencheGrupoTurmaAluno() {

            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheGrupoTurmaAluno",
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Rotina de carregamnto do grupo da Turma ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na rotina de carregamnto do grupo da Turma. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        //alert(json.length)
                        if (json.length > 1) {
                            document.getElementById('divTurmaDiversasNew').style.display = 'block';
                            document.getElementById('divTurmaTemNew').style.display = 'block';
                            document.getElementById('divTabs').style.display = 'block';
                            document.getElementById('divTurmaNaoTemNew').style.display = 'none';
                            $("#ddlTurmaAlunoNew").empty();
                            $('#ddlTurmaAlunoNew').select2({ data: json });
                            fSelect2();
                            $("#ddlTurmaAlunoNew").val(json[0].id).trigger("change");
                            fPreencheTurmaAluno(json[0].id);
                            document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value = json[0].id;
                        }
                        else if (json[0].id != "Nada") {
                            document.getElementById('divTurmaDiversasNew').style.display = 'none';
                            document.getElementById('divTurmaTemNew').style.display = 'block';
                            document.getElementById('divTabs').style.display = 'block';
                            document.getElementById('divTurmaNaoTemNew').style.display = 'none';
                            fPreencheTurmaAluno(json[0].id);
                            document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value = json[0].id;
                        }
                        else {
                            document.getElementById('divTurmaDiversasNew').style.display = 'none';
                            document.getElementById('divTurmaTemNew').style.display = 'none';
                            document.getElementById('divTabs').style.display = 'none';
                            document.getElementById('divTurmaNaoTemNew').style.display = 'block';
                        }
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na rotina de carregamnto do grupo da Turma. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na rotina de carregamnto do grupo da Turma. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
        }

        //============================

        function fPreencheTurmaAluno(qIdTurma) {
            fProcessando();
            try {
                $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fPreencheTurmaAluno?qIdTurma=" + qIdTurma,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                        fFechaProcessando();
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Preenchimnto de dados da Turma ';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro no preenchimnto de dados da Turma. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                        fFechaProcessando();
                    }
                    else {
                        //document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value = qIdTurma;
                        document.getElementById('<% =txtCodTurmaAlunoNew.ClientID%>').value = json[0].P0;
                        document.getElementById('<% =txtQuadrimestreAlunoNew.ClientID%>').value = json[0].P1;
                        document.getElementById('<% =txtTipoCursoAlunoNew.ClientID%>').value = json[0].P2;
                        document.getElementById('<% =txtDataInicioCursoAlunoNew.ClientID%>').value = json[0].P3;
                        document.getElementById('<% =txtDataFimCursoAlunoNew.ClientID%>').value = json[0].P4;
                        document.getElementById('<% =txtDataTerminoCursoAlunoNew.ClientID%>').value = json[0].P5;
                        document.getElementById('<% =txtCursoAlunoNew.ClientID%>').value = json[0].P6;
                        document.getElementById('<% =txtAreaConcentracaoAlunoNew.ClientID%>').value = json[0].P7;
                        
                        if (document.getElementById('divTabs').style.display != 'none') {
                            //alert("entrou Histório Aluno");
                            fPreencheHistoricoAluno(qIdTurma);
                            fPreenchePreMatricula(qIdTurma);
                            //fPreencheMatriculaAluno(qIdTurma);
                        }

                        fFechaProcessando();
                    }
                },
                error: function(xhr){
                    alert("Houve um erro no preenchimnto de dados da Turma. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro no preenchimnto de dados da Turma. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
    
            } catch (e) {
                fFechaProcessando();
            }
        }

        //================================================================================

        function fPreencheHistoricoAluno(qIdTurma) {
            try {
                var dt = $('#grdHistoricoAlunoNew').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdHistoricoAlunoNew").style.display = "none";
                            document.getElementById("msgSemResultadosgrdHistoricoAlunoNew").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdHistoricoAlunoNew").style.display = "none";
                                document.getElementById("msgSemResultadosgrdHistoricoAlunoNew").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("divgrdHistoricoAlunoNew").style.display = "block";
                                document.getElementById("msgSemResultadosgrdHistoricoAlunoNew").style.display = "none";

                                var table_grdHistoricoAlunoNew = $('#grdHistoricoAlunoNew').DataTable();

                                $('#grdHistoricoAlunoNew').on("click", "tr", function () {
                                    vRowIndex_grdHistoricoAlunoNew = table_grdHistoricoAlunoNew.row(this).index()
                                });
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheHistoricoAluno?qIdTurma=" + qIdTurma,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Início", "orderable": false, "className": "text-center centralizarTH", width: "10px", type: 'date-euro'
                        },
                        {
                            "data": "P1", "title": "Período", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P2", "title": "Disciplina", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P3", "title": "Nome", "orderable": false, "className": "text-left centralizarTH", width: '55%'
                        },
                        {
                            "data": "P4", "title": "Duração", "orderable": false, "className": "text-lef centralizarTHt", width: '10px'
                        },
                        {
                            "data": "P5", "title": "Frequência", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P6", "title": "Conceito", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P7", "title": "Resultado", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P8", "title": "Detalhe Disciplina", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P9", "title": "Presença Disciplina", "orderable": false, "className": "text-center centralizarTH", width: "10px"
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
                
            }
            
        }

        //================================================================================

        function fPreenchePreMatricula(qIdTurma) {
            try {
                var dt = $('#grdMatricula').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: false, //Pesquisar
                    bPaginate: false, // Paginação
                    bInfo: false, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        document.getElementById('<%=Label1.ClientID%>').innerHTML = 'Nenhuma disciplina disponível';

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdMatricula").style.display = "none";
                            document.getElementById("msgSemResultadosgrdMatricula").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdMatricula").style.display = "none";
                                document.getElementById("msgSemResultadosgrdMatricula").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            }
                            else if (json[0].P0 == "Aviso")
                            {
                                document.getElementById('<%=Label1.ClientID%>').innerHTML = json[0].P1;
                            } 
                            else
                            {
                                fIcheck();
                                document.getElementById("divgrdMatricula").style.display = "block";
                                document.getElementById("msgSemResultadosgrdMatricula").style.display = "none";

                                var table_grdMatricula = $('#grdMatricula').DataTable();

                                $('#grdMatricula').on("click", "tr", function () {
                                    vRowIndex_grdMatricula = table_grdMatricula.row(this).index()
                                });
                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreenchePreMatricula?qIdTurma=" + qIdTurma + "&qTab=" + document.getElementById('hQTab').value,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Código", "orderable": false, "className": "text-center centralizarTH", width: "10px"
                        },
                        {
                            "data": "P1", "title": "Disciplina", "orderable": false, "className": "text-left centralizarTH", width: "50px"
                        },
                        {
                            "data": "P2", "title": "Vagas Disponíveis", "orderable": false, "className": "text-center centralizarTH", width: "55%"
                        },
                        {
                            "data": "P3", "title": "Dia da Semana", "orderable": false, "className": "text-center centralizarTH", width: '10px'
                        },
                        {
                            "data": "P4", "title": " ", "orderable": false, "className": "text-center centralizarTHt", width: '6px'
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
                
            }
            
        }


        //================================================================================

        function fDetalheDisciplina(qDisciplina) {

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
                    document.getElementById('lblErroCorpo').innerHTML = 'Erro para exibir Dados do Oferecimento <br/> Erro: ' + err + '<br/>Status do erro: ' + status;

                    $('#divModalErro').modal('show');
                }
            });


        }

        //================================================================================

        function fDetalhePresenca(qDisciplina, qAluno) {
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/ListaPresenca",
                contentType: 'application/json; charset=utf-8',
                //data: "{idOferecimento:'" + 'SP' + "', n:'" + '5' + "'}",
                data: "{idOferecimento:'" + qDisciplina + "', idAluno:'" + qAluno + "'}",
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
                        }, {
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
                        $('#tabPresenca').DataTable({
                            processing: true,
                            destroy: true,
                            "paging": false,
                            "searching": false,
                            "ordering": false,
                            "info": false,
                            "aaData": itens,
                            "aoColumns": [
                                { "mDataProp": "NumeroAula" },
                                { "mDataProp": "DataAula" },
                                { "mDataProp": "HoraInicio" },
                                { "mDataProp": "HoraFim" },
                                { "mDataProp": "Presente" }
                            ],
                            "createdRow": function (row, data1, index) {
                                if (data1.Presente == 'Ausente') {
                                    $('td', row).eq(4).addClass('highlight');
                                }
                            }
                        });
                        $('#divModalPresenca').modal('show');
                    }
                },
                error: function (xmlHttpRequest, status, err) {
                    document.getElementById('lblErroCabecalho').innerHTML = 'Erro para exibir Dados de Presença';
                    document.getElementById('lblErroCorpo').innerHTML = 'Erro para exibir dados de Presença do Aluno<br/> Erro: ' + err + '<br/>Status do erro: ' + status;

                    $('#divModalErro').modal('show');
                }
            });

        }

        //================================================================================


        function funcClicaImprimirHistorico() {
            fPreparaRelatorio('O relatório do Histórico está sendo preparado.');
        }

        //================================================================================

        function fConfirmaMatricula() {
            var x = document.querySelectorAll('[id^=chkAlunoMatricula]');
            var qMatricular = "";
            var qDesmatricular = "";
            for (i = 0; i < x.length; i++) {
                if (x[i].classList.contains('sim') && x[i].checked == false) {
                    if (qDesmatricular != "") {
                        qDesmatricular = qDesmatricular + ";";
                    }
                    qDesmatricular = qDesmatricular + x[i].name.replace("chkAlunoMatricula_", "");
                }
                if (x[i].classList.contains('nao') && x[i].checked == true) {
                    if (qMatricular != "") {
                        qMatricular = qMatricular + ";";
                    }
                    qMatricular = qMatricular + x[i].name.replace("chkAlunoMatricula_", "");
                }
            }
            
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fConfirmaMatricula?qMatricular=" + qMatricular + "&qDesmatricular=" + qDesmatricular + "&qTab=" + document.getElementById('hQTab').value,
                dataType: "json",
                success: function (json) {
                    if (json[0].P0 == "deslogado") {
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Confirmação de Pré-Matrícula';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Confirmação de Pré-Matrícula. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreenchePreMatricula(document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value);
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Confirmação de Pré-Matrícula</strong><br /><br />',
                            message: 'A Confirmação de Pré-Matrícula foi realizada com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1700,
                            timer: 1700,
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
                    alert("Houve um erro na Confirmação de Pré-Matrícula. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () {
                    alert("Houve um erro na Confirmação de Pré-Matrícula. Por favor tente novamente.");
                    fFechaProcessando()
                }
            });
        }

        //================================================================================



        $('#ddlTurmaAlunoNew').on("select2:select", function(e) { 
            fPreencheTurmaAluno($(this).val());
            document.getElementById('<% =txtIdTurmaAlunoNew.ClientID%>').value = $(this).val();
        });

        function fSelect2() {

            $(".select2").select2({
                theme: "bootstrap",
                language: "pt-BR",
            });

            $(".SemPesquisa").select2({
                theme: "bootstrap",
                minimumResultsForSearch: Infinity
            });
        }

        function fIcheck() {
            $('input').iCheck({
                checkboxClass: 'icheckbox_flat-blue',
                radioClass: 'iradio_flat-blue',
                increasePeriodo: '20%' // optional
            });
        }

        $(document).ready(function () {
            
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

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                //$('body').append(modal);
                //$("#divPogress").css("z-index", "1500");
                $("#divPogress").append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

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

        function AbreModalMensagem(qClass) {
            $("#divCabecalho").removeClass("alert-warning");
            $("#divCabecalho").removeClass("alert-primary");
            $('#divCabecalho').removeClass('alert-danger');
            $('#divCabecalho').removeClass('alert-success');
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
        }

        

    </script>

</asp:Content>
