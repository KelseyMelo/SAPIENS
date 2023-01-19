<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finRelBoleto.aspx.cs" Inherits="SERPI.UI.WebForms_C.finRelBoleto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liRelatorioBoletos" />

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigoProfessor"  name="hCodigoProfessor" value="value" />
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
        <div class="col-md-9">
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Relatório de Boletos</strong></h3>
        </div>

        <div class="col-md-3">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarProfessor" name="btnCriarProfessor" class="btn btn-success hidden" href="#" onclick="" onserverclick="btnCriarProfessor_Click" >
                        <i class="fa fa-magic"></i>&nbsp;Cadastrar Professor</button>
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

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <span>Período</span><br />
                                    <asp:DropDownList runat="server" ID="ddlPeriodoBoleto" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodoBoleto_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <span>Curso</span><br />
                                    <asp:DropDownList runat="server" ID="ddlCursoBoleto" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlPeriodoBoleto" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />

                    <div class="row">
                        <div class="col-md-6">
                            <span>Situação</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacaoBoleto" Checked="true" ID="optSituacaoBoletoTodos" runat="server"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoBoletoTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-4">                    
                                <asp:RadioButton GroupName="GrupoSituacaoBoleto" ID="optSituacaoBoletoPagos" runat="server"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoBoletoPagos.ClientID %>">Pagos</label>
                                </div>

                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacaoBoleto" ID="optSituacaoBoletoNaoPagos" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoBoletoNaoPagos.ClientID %>">Não Pagos</label>
                                </div>
                            </div>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2"></div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button type="button" id="bntPerquisaBoleto" name="bntPerquisaBoleto" onclick="fPreencheGradeBoleto();" title="" class="btn btn-success pull-right ">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                        </div>

                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="panel panel-primary">

            <div class="panel-body">

                <div class="row">

                    <div class="col-md-12">
                        <div class="grid-content">
                            <div id="msgSemResultadosgrdCandidato" style="display:none">
                                <div class="alert bg-gray"> 
                                    <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum candidato inscrito." />
                                </div>
                            </div>

                            <div id="divgrdCandidato" class="table-responsive" style="display:none">
                                <div class="scroll">
                                    <table id="grdCandidato" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
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

    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header bg-blue">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
            <h4 class="modal-title" id="myModalLabel"><label id="labelNomeExibeImagem">test</label></h4>
          </div>
          <div class="modal-body text-center">
            <img src="" id="imagepreview" class="img-responsive center-block"  > <%--style="width: 400px; height: 300px;"--%>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal para Boleto -->
    <div class="modal fade" id="divModalBoleto" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div id="divCabecModalBoleto" class="modal-header">
                    <h4 class="modal-title"><i class="fa fa-money"></i>&nbsp;<label id="lblTituloModalBoleto"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <label class="text-bold" style="font-size:large" id="lblMensagemModalBoleto"></label>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-5">
                                <span>Nome</span><br />
                                <input class="form-control input-sm" id="txtNomeModalBoleto" type="text" value="" readonly="true" />
                                <input class="form-control input-sm" id="txtIdBoletoModalBoleto" type="text" style="display:none" />
                                <input class="form-control input-sm" id="txtIdInscricaoModalBoleto" type="text" style="display:none" />
                                <input class="form-control input-sm" id="txtEmailModalBoleto" type="text" style="display:none" />
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-5">
                                <span>Curso</span><br />
                                <input class="form-control input-sm" id="txtCursoModalBoleto" type="text" value="" readonly="true" />
                            </div>
                        </div>
                        <br />

                        <div class="row"> 
                            <div class="col-md-3">
                                <span>Boleto</span><br />
                                <input class="form-control input-sm" id="txtRefTransModalBoleto" type="text" value="" readonly="true" />
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-3">
                                <span>Data Pagamento</span><br />
                                <input class="form-control input-sm" id="txtDataModalBoleto" type="date" value="" />
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
                            <button id="btnConfirmarPagamento" type="button" class="btn btn-success pull-right" onclick="fConfirmarDataPagamento()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnExcluirPagamento" type="button" class="btn btn-success pull-right" onclick="fExcluirDataPagamento()">
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
                //alert('oi');
                document.getElementById('bntPerquisaBoleto').click();
            }
        }

        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
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
            $('#divgrdCandidato').hide();
        });


        $(document).ready(function () {

            //$('#grdCandidato').dataTable({ stateSave: true, "bProcessing": true, });
            fechaLoading();
            
        }); 
        
        function fPreencheGradeBoleto() {
            if (document.getElementById('<%=ddlPeriodoBoleto.ClientID%>').value == "") {
                document.getElementById("divgrdCandidato").style.display = "none";
                document.getElementById("msgSemResultadosgrdCandidato").style.display = "block";
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Selecione um Período";
                $('#divModalAssociarTamanho').modal('hide');
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                return;
            }
            
            var qIdPeriodo = document.getElementById('<%=ddlPeriodoBoleto.ClientID%>').value;
            var qIdCurso = document.getElementById('<%=ddlCursoBoleto.ClientID%>').value;
            var qSituacao = 0;
            if (document.getElementById('<%=optSituacaoBoletoPagos.ClientID%>').checked) {
                qSituacao = 1;
            }
            else if (document.getElementById('<%=optSituacaoBoletoNaoPagos.ClientID%>').checked) {
                qSituacao = 2;
            }
            var dt = $('#grdCandidato').DataTable({
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
                        document.getElementById("divgrdCandidato").style.display = "none";
                        document.getElementById("msgSemResultadosgrdCandidato").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdCandidato").style.display = "none";
                            document.getElementById("msgSemResultadosgrdCandidato").style.display = "block";
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
                            document.getElementById("divgrdCandidato").style.display = "block";
                            document.getElementById("msgSemResultadosgrdCandidato").style.display = "none";

                            var table_grdCandidato = $('#grdCandidato').DataTable();

                            $('#grdCandidato').on("click", "tr", function () {
                                vRowIndex_grdCandidato = table_grdCandidato.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheGradeBoleto?qIdPeriodo=" + qIdPeriodo + "&qIdCurso=" + qIdCurso + "&qSituacao=" + qSituacao,
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
                        "data": "P0", "title": "Nome", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P1", "title": "Curso", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P2", "title": "Boleto", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Data Pagamento", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Ação", "orderable": false, "className": "text-center"
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

        function fModalExcluirPagamento(qIdInscricao, qNome, qCurso, qRefTran, qData, qIdBoleto) {
            $("#divCabecModalBoleto").removeClass("bg-primary");
            $('#divCabecModalBoleto').addClass('bg-danger');
            document.getElementById('lblTituloModalBoleto').innerHTML = "Excluir Data de Pagamento";
            document.getElementById('lblMensagemModalBoleto').innerHTML = "Deseja excluir a data de pagamento do candidato abaixo?";
            document.getElementById('txtNomeModalBoleto').value = qNome;
            document.getElementById('txtCursoModalBoleto').value = qCurso;
            document.getElementById('txtRefTransModalBoleto').value = qRefTran;
            document.getElementById('txtDataModalBoleto').value = qData;
            document.getElementById('txtIdBoletoModalBoleto').value = qIdBoleto;
            document.getElementById('txtIdInscricaoModalBoleto').value = qIdInscricao;

            document.getElementById('txtDataModalBoleto').readOnly = true;

            document.getElementById('btnConfirmarPagamento').style.display = "none";
            document.getElementById('btnExcluirPagamento').style.display = "block";

            $('#divModalBoleto').modal();
        }

        function fExcluirDataPagamento() {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            var sAux = "";
            if (document.getElementById("txtDataModalBoleto").value == "") {
                sAux = "Deve-se preencher a Data de Pagamento <br><br>"
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
            var qIdInscricao = document.getElementById('txtIdInscricaoModalBoleto').value;
            var qNome = document.getElementById('txtNomeModalBoleto').value;
            var qRefTran = document.getElementById('txtRefTransModalBoleto').value;
            var qData = document.getElementById('txtDataModalBoleto').value;
            var qIdBoleto = document.getElementById('txtIdBoletoModalBoleto').value;

            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fExcluirDataPagamento?qIdInscricao=" + qIdInscricao + "&qNome=" + qNome + "&qRefTran=" + qRefTran + "&qData=" + qData + "&qIdBoleto=" + qIdBoleto,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Data de Pagamento';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Exclusão de Data de Pagamento. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalBoleto').modal('hide');
                        fPreencheGradeBoleto();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Exclusão de Data de Pagamento</strong><br /><br />',
                            message: 'Exclusão de Data de Pagamento foi realizada com sucesso.<br />',

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
                    alert("Houve um erro na Exclusão de Data de Pagamento. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Exclusão de Data de Pagamento. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }


        //================================================================================

        function fModalConfirmarPagamento(qEmail, qIdInscricao, qNome, qCurso, qRefTran, qData, qIdBoleto) {
            $("#divCabecModalBoleto").removeClass("bg-danger");
            $('#divCabecModalBoleto').addClass('bg-primary');
            document.getElementById('lblTituloModalBoleto').innerHTML = "Inserir Data de Pagamento";
            document.getElementById('lblMensagemModalBoleto').innerHTML = "Deseja inserir a data de pagamento do candidato abaixo?";
            document.getElementById('txtNomeModalBoleto').value = qNome;
            document.getElementById('txtCursoModalBoleto').value = qCurso;
            document.getElementById('txtRefTransModalBoleto').value = qRefTran;
            document.getElementById('txtDataModalBoleto').value = qData;
            document.getElementById('txtIdBoletoModalBoleto').value = qIdBoleto;
            document.getElementById('txtIdInscricaoModalBoleto').value = qIdInscricao;
            document.getElementById('txtEmailModalBoleto').value = qEmail;

            document.getElementById('txtDataModalBoleto').readOnly = false;

            document.getElementById('btnConfirmarPagamento').style.display = "block";
            document.getElementById('btnExcluirPagamento').style.display = "none";

            $('#divModalBoleto').modal();
        }

        function fConfirmarDataPagamento() {
            //alert('oi: ' + document.getElementById("txtDataAulaModalIncluirAula").value);
            var sAux = "";
            if (document.getElementById("txtDataModalBoleto").value == "") {
                sAux = "Deve-se preencher a Data de Pagamento <br><br>"
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
            var qIdInscricao = document.getElementById('txtIdInscricaoModalBoleto').value;
            var qNome = document.getElementById('txtNomeModalBoleto').value;
            var qRefTran = document.getElementById('txtRefTransModalBoleto').value;
            var qData = document.getElementById('txtDataModalBoleto').value;
            var qIdBoleto = document.getElementById('txtIdBoletoModalBoleto').value;
            var qEmail = document.getElementById('txtEmailModalBoleto').value;

            fProcessando();
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fConfirmarDataPagamento?qIdInscricao=" + qIdInscricao + "&qNome=" + qNome + "&qRefTran=" + qRefTran + "&qData=" + qData + "&qIdBoleto=" + qIdBoleto + "&qEmail=" + qEmail,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Data de Pagamento';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Inclusão de Data de Pagamento. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalBoleto').modal('hide');
                        fPreencheGradeBoleto();
                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Data de Pagamento</strong><br /><br />',
                            message: 'Inclusão de Data de Pagamento foi realizada com sucesso.<br />',

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
                    alert("Houve um erro na Inclusão de Data de Pagamento. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Inclusão de Data de Pagamento. Por favor tente novamente!");
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

        function fExibeImagem(qId, qNome) {
            $('#imagepreview').attr('src', "img\\pessoas\\" + qId + "?" + new Date()); // here asign the image to the modal when the user click the enlarge link
            document.getElementById('labelNomeExibeImagem').innerHTML = qNome;
            $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
        }

    </script>

</asp:Content>
