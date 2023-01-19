<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finInadimplente.aspx.cs" Inherits="SERPI.UI.WebForms_C.finInadimplente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liControleInadimplentes" />

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="value" />
    <%--<input type="hidden" id ="hTituloPagina"  name="hTituloPagina" value="Professor (Listagem)" />--%>

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
        <div class="col-md-9">
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Controle de Inadimplentes </strong></h3>
        </div>

        <div class="col-md-3 hidden">
            <br />
            <div class ="pull-right ">
                <button type="button"  id="btnAdicionarAluno" name="btnAdicionarAluno" class="btn btn-success" href="#" onclick="fModalPesquisaAlunoInadimplente()" >
                        <i class="fa fa-magic"></i>&nbsp;Adicionar Aluno</button>
            </div>
        </div>
    </div>
    <br />

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
                            <input class="form-control input-sm fecha_grid_resultados" id="txtMatriculaInadimplente" type="number" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-6">
                            <span>Nome</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" id="txtNomeInadimplente" type="text" value="" maxlength="100" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button type="button" id="bntPerquisaInadimplente" name="bntPerquisaInadimplente" onclick="fPreencheGradeInadimplente();" title="" class="btn btn-success pull-right ">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                        </div>
                    </div>

                </div>
            </div>
        </div>


        <div class="row" id="idRowResultado" style="display:none">
            <div class="panel panel-primary">

            <div class="panel-body">

                <div class="row">

                    <div class="col-md-12">
                        <div class="grid-content">
                            <div id="msgSemResultadosgrdInadimplente" style="display:none">
                                <div class="alert bg-gray"> 
                                    <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum registro encontrado." />
                                </div>
                            </div>

                            <div id="divgrdInadimplente" class="table-responsive" style="display:none">
                                <div class="scroll">
                                    <table id="grdInadimplente" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
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
        <br />
    </div>

    <div id="divPogress" class="loading" align="center">
        Processando... <br />Por favor, aguarde.
        <br />
        <img src="img/loader.gif" width="42" height="42" alt="" />
    </div>

    <!-- Modal para pesquisar Aluno Disponíveis na Lista de Inadimplentes -->
    <div class="modal fade" id="divModalPesquisaAlunoInadimplente" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-user-circle-o"></i>&nbsp;&nbsp;Inclusão de aluno na lista de Inadimplência </h4>
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
                                            <input class="form-control input-sm" id="txtMatriculaAlunoNaLista" type="number" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeAlunoNaLista" type="text" value="" maxlength="100" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button id="btnPesquisaAlunoDisponivelInadimplente" type="button" title="" class="btn btn-success" onclick="fPesquisaAlunoDisponivelInadimplente()" >
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
                                                <div id="msgSemResultadosgrdAlunoDisponivelInadimplente" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <asp:Label runat="server" ID="Label1" Text="Nenhum Aluno disponível encontrado" />
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdAlunoDisponivelInadimplente" >
                                                    <div class="scroll">
                                                        <table id="grdAlunoDisponivelInadimplente" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para excluir Aluno da Lista de Inadimplentes -->
    <div class="modal fade" id="divModalExcluirInadimplente" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <h4 class="modal-title"><i class="fa fa-eraser"></i>&nbsp;Excluir aluno da lista de Inadimplente</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">

                        <div class="row">
                            <div class="col-md-12">
                                <span>
                                    <p>
                                        Deseja exluir da lista de inadimplentes o <br />
                                        aluno: <label style="font-weight:bold !important" id="lblNomeAlunoExcluir"></label>
                                        <br />
                                        matrícula: <strong><label style="font-weight:bold !important" id="lblMatriculaAlunoExcluir"></label></strong>
                                    </p>
                                </span>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-xs-6">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Cancelar</button>
                        </div>
                        <div class="col-xs-6">
                            <button id="btnExcluirLista" type="button" class="btn btn-success pull-right" onclick="fExcluirAlunoListaInadimplente()">
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
    
    <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>

    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>

    <script src="https://cdn.datatables.net/plug-ins/1.10.16/sorting/date-euro.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

    
    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>

        $(document).ready(function () {
            fSelect2();
           
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

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                if ($('#divModalPesquisaAlunoInadimplente').is(':visible')) {
                    fPesquisaAlunoDisponivelInadimplente();
                }
                else {
                    document.getElementById('bntPerquisaInadimplente').click();
                }
                
            }
        }

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

        $(".fecha_grid_resultados").keydown(function () {
            //alert("The text has been changed.");
            $('#idRowResultado').hide();
        });


        $(document).ready(function () {

            //$('#grdInadimplente').dataTable({ stateSave: true, "bProcessing": true, });
            fechaLoading();
            
        }); 
        
        function fPreencheGradeInadimplente() {
            fProcessando();
            var qMatricula = document.getElementById("txtMatriculaInadimplente").value;
            var qNome = document.getElementById("txtNomeInadimplente").value;
            var dt = $('#grdInadimplente').DataTable({
                processing: true,
                serverSide: false,
                destroy: true,
                async: false,
                error: function (xhr, error, thrown) {
                    alert( 'Não está logado' );
                },
                searching: true, //Pesquisar
                bPaginate: true, // Paginação
                bInfo: true, //mostrando 1 de x registros
                fnInitComplete: function (oSettings, json) {
                    //alert('Passou');
                    //CallBackReq(oSettings.fnRecordsTotal());
                    //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                    //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                    //    alert(json[i].Item);
                    //} 
                    document.getElementById("idRowResultado").style.display = "block";

                    if(oSettings.fnRecordsTotal() == 0){
                        document.getElementById("divgrdInadimplente").style.display = "none";
                        document.getElementById("msgSemResultadosgrdInadimplente").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdInadimplente").style.display = "none";
                            document.getElementById("msgSemResultadosgrdInadimplente").style.display = "block";
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
                            document.getElementById("divgrdInadimplente").style.display = "block";
                            document.getElementById("msgSemResultadosgrdInadimplente").style.display = "none";

                            var table_grdInadimplente = $('#grdInadimplente').DataTable();

                            $('#grdInadimplente').on("click", "tr", function () {
                                vRowIndex_grdInadimplente = table_grdInadimplente.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheGradeInadimplente?qMatricula=" + qMatricula + "&qNome=" + qNome,
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
                        "data": "P1", "title": "Nome", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P2", "title": "Data", "orderable": true, "className": "text-center", type: 'date-euro'
                    },
                    {
                        "data": "P3", "title": "Exclusão", "orderable": false, "className": "text-center"
                    }
                ],
                order: [[1, 'asc']],
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
            fFechaProcessando();
        }

        //================================================================================

        function fModalExcluirAluno(qMatricula, qNome) {
            document.getElementById('lblMatriculaAlunoExcluir').innerHTML = qMatricula;
            document.getElementById('lblNomeAlunoExcluir').innerHTML = qNome;
            $('#divModalExcluirInadimplente').modal();
        }

        function fExcluirAlunoListaInadimplente() {
            
            var qMatricula = document.getElementById('lblMatriculaAlunoExcluir').innerHTML;
            var qNome = document.getElementById('lblNomeAlunoExcluir').innerHTML;

            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fExcluirAlunoListaInadimplente?qMatricula=" + qMatricula + "&qNome=" + qNome,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Aluno da lista de inadimplentes';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Exclusão de Aluno da lista de inadimplentes. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalExcluirInadimplente').modal('hide');
                        fPreencheGradeInadimplente();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão de Aluno da lista de inadimplentes</strong><br /><br />',
                            message: 'A exclusão do Aluno da lista de inadimplentes foi realizada com sucesso.<br />',

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
                    alert("Houve um erro na Exclusão de Aluno da lista de inadimplentes. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Exclusão de Aluno da lista de inadimplentes. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }


        //================================================================================

        function fModalPesquisaAlunoInadimplente() {
            document.getElementById('txtMatriculaAlunoNaLista').value = "";
            document.getElementById('txtNomeAlunoNaLista').value = "";

            document.getElementById("divgrdAlunoDisponivelInadimplente").style.display = "none";
            document.getElementById("msgSemResultadosgrdAlunoDisponivelInadimplente").style.display = "none";

            $('#divModalPesquisaAlunoInadimplente').modal();
        }

        //================================================================================

         //==========================================================

        function fPesquisaAlunoDisponivelInadimplente() {
            fProcessando();
            try {
                var qMatricula = document.getElementById('txtMatriculaAlunoNaLista').value;
                var qNome = document.getElementById('txtNomeAlunoNaLista').value;

                var dt = $('#grdAlunoDisponivelInadimplente').DataTable({
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
                            document.getElementById("divgrdAlunoDisponivelInadimplente").style.display = "none";
                            document.getElementById("msgSemResultadosgrdAlunoDisponivelInadimplente").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdAlunoDisponivelInadimplente").style.display = "none";
                                document.getElementById("msgSemResultadosgrdAlunoDisponivelInadimplente").style.display = "block";
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
                                document.getElementById("divgrdAlunoDisponivelInadimplente").style.display = "block";
                                document.getElementById("msgSemResultadosgrdAlunoDisponivelInadimplente").style.display = "none";

                                var table_grdAlunoDisponivelInadimplente = $('#grdAlunoDisponivelInadimplente').DataTable();

                                $('#grdAlunoDisponivelInadimplente').on("click", "tr", function () {
                                    vRowIndex_grdAlunoDisponivelInadimplente = table_grdAlunoDisponivelInadimplente.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPesquisaAlunoDisponivelInadimplente?qMatricula=" + qMatricula + "&qNome=" + qNome,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "Matrícula", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left"
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
            
        }

        //============================================

        function fAdicionarAlunoInadimplente(qMatricula, qNome) {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            
            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fAdicionarAlunoInadimplente?qMatricula=" + qMatricula + "&qNome=" + qNome,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de aluno na lista de inadimplentes';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Inclusão de aluno na lista de inadimplentes. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalPesquisaAlunoInadimplente').modal('hide');
                        fPreencheGradeInadimplente();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de aluno na lista de inadimplentes</strong><br /><br />',
                            message: 'Inclusão de aluno na lista de inadimplentes foi realizada com sucesso.<br />',

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
                    alert("Houve um erro na Inclusão de aluno na lista de inadimplentes. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Inclusão de aluno na lista de inadimplentes. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //===============================================================

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
