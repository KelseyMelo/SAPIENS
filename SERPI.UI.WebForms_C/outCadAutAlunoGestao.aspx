<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="outCadAutAlunoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.outCadAutAlunoGestao" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liMenuOutrosGrupo" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liOutrosCadAutAluno" />

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
        <div class="col-md-8">
            <h3 class=""><i class="fa fa-circle-o text-purple"></i>&nbsp;<strong >Cadastro Automático de Alunos</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(novo)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-4 pull-right text-right">
            <br />
            <button type="button"  runat="server" id="btnCriarCadAutAlunosAlunos" name="btnCriarCadAutAlunosAlunos" class="btn btn-primary " href="#" onclick="" onserverclick="btnCriarCadAutAlunosAlunos_Click"> <%--onserverclick="btnCriarCadAutAlunosAlunos_Click"--%> 
                    <i class="fa fa-magic faa-shake animated"></i>&nbsp;Importar Novo Lote de Cadastro de Alunos</button>

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
                                
                            </div>
                        
                            <br />
                        </div>                        

                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-10 ">
                                    <span>Descrição do Lote de Alunos</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDescricao" type="text" value = "" maxlength="200"/>
                                </div>
                                
                            </div>

                            <br />

                            <div class="row">
                                <div class="col-md-10 ">
                                    <span>Curso Alvo</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDescricao_curso" type="text" value = "" maxlength="300"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-1">
                                    <br />
                                    <button runat="server" type="button" id="btnSalvarCadAutAlunosAlunos" name="btnSalvarCadAutAlunosAlunos" class="btn btn-success pull-right" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvarCadAutAlunosAlunos_Click">
                                        <i class="fa fa-floppy-o"></i>&nbsp;Salvar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />

                </div>
            </div>
        </div>
    </div>

    <br />

    <!-- Sessão Grupo -->
    <div class="tab-content" id="divTurma" runat ="server" visible="false">

        <div class="panel panel-default">

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <h5 class="box-title text-bold">Alunos Cadastrados na Importação  <i class="fa fa-info-circle fa-lg" style="color:blueviolet" data-toggle="tooltip" title="Alunos cadastrados nessa importação."></i></h5>
                        <br />
                        <div class="row">
                            <div class="col-md-12 ">
                                <div class="row">
                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                 
                                                <div id="msgSemResultadosTurma" style="display:block">
                                                    <div class="alert bg-gray"> 
                                                        <asp:Label runat="server" ID="Label6" Text="Nenhum Aluno Importado." />
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
                                                    <div class="col-md-4 center-block">
                                                        <button type="button" id="btnLocalizarPlanilha" name="btnLocalizarPlanilha" class="btn btn-warning" onclick="fLocalizaArquivoExcel()">
                                                            <i class="fa fa-upload"></i>&nbsp;Importar Planilha com Alunos</button>
                                                    </div>

                                                    <div class="col-md-4 center-block">
                                                        <%--fa fa-plus-square--%>
                                                        <button type="button" runat="server" id="btnGerarMatriculaAlunos" name="btnGerarMatriculaAlunos" class="btn btn-success" onclick="if (fProcessando()) return;" onserverclick="btnGerarMatriculaAlunos_Click">
                                                            <i class="fa fa-print"></i>&nbsp;Gerar Matrícula de Alunos</button>
                                                    </div>

                                                    <div class="col-md-4 center-block">
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
                                <input class="form-control input-sm" runat="server" id="txtAssuntoEmail" type="text" maxlength="100" value="Cadastro Plataforma SAPIENS" readonly="readonly" />
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
                            <button type="button" class="btn btn-primary pull-right" onclick="fEnviaEmailCadAutAlunosAlunos()" >
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
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
                   <%-- O processo de uma "nova" importação irá recriar <strong>todos os registros</strong> dos participantes.<br /><br />--%>
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
        <asp:FileUpload ID="fileArquivoParaImportar" runat="server" accept=".xls,.xlsx" onchange="javascript:fSelecionouExcelCadAutAlunosAlunos(this);"  />
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
                fPreencheTurmaCadAutAlunosAlunos();
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

        function fLocalizaArquivoExcel() {
            if (document.getElementById("btnLocalizarPlanilha").style.display == "block") {
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

        function fSelecionouExcelCadAutAlunosAlunos(idFile) {
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
                                url: "wsSapiens.asmx/fSelecionouExcelCadAutAlunosAlunos",
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
                                        fPreencheTurmaCadAutAlunosAlunos();
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
                                        fPreencheTurmaCadAutAlunosAlunos();
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

        function fPreencheTurmaCadAutAlunosAlunos() {
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

                    document.getElementById("btnLocalizarPlanilha").style.display = "none";
                    document.getElementById("<%=btnGerarMatriculaAlunos.ClientID%>").style.display = "none";
                    document.getElementById("btnEnviarEmailLote").style.display = "none";

                    if (oSettings.fnRecordsTotal() == 0) {
                        document.getElementById("divgrdTurma").style.display = "none";
                        document.getElementById("msgSemResultadosTurma").style.display = "block";
                        document.getElementById("btnLocalizarPlanilha").style.display = "block";
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
                            //document.getElementById("<%=btnGerarMatriculaAlunos.ClientID%>").style.display = "block";

                            if (json[0].P0 == "") {
                                document.getElementById("<%=btnGerarMatriculaAlunos.ClientID%>").style.display = "block";
                                //json.forEach(function(elemento){
                                //    if (elemento.P6 != "" && elemento.P6 != null) {
                                //        //alert(elemento.P6);
                                //        document.getElementById("btnEnviarEmailLote").style.display = "block";
                                //    }
                                //});
                            }
                            else {
                                 document.getElementById("btnEnviarEmailLote").style.display = "block";
                            }

                            //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {

                            //}
                            
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheTurmaCadAutAlunosAlunos",
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
                        "data": "P0", "title": "Matrícula", "orderable": true, "className": "text-center", "width": "10px"
                    },
                    {
                        "data": "P1", "title": "Aluno", "orderable": true, "className": "text-left", "width": "50%"
                    },
                     {
                        "data": "P2", "title": "CPF", "orderable": true, "className": "text-center", "width": "15px"
                    },
                    {
                        "data": "P3", "title": "E-mail", "orderable": true, "className": "text-left", "width": "10px"
                    },
                    {
                        "data": "P4", "title": "Data Envio de e-mail", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P5", "title": "Enviar e-mail", "orderable": false, "className": "text-center"
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


       //============================================================================
        
        function fBaixarPdfCadAutAlunosAlunos() {
            fProcessando();
            try {
                var qPermissao;
                
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fBaixarPdfCadAutAlunosAlunos",
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
                            //fPreencheTurmaCadAutAlunosAlunos();
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
                        alert("Houve um erro no download dos CadAutAlunosAlunoss. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro no download dos CadAutAlunosAlunoss. Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }


//============================================================================
        
        function fEnviaEmailCadAutAlunosAlunos() {
            fProcessando();
            try {
                
                var formData = new FormData();
                formData.append("qDestinatario", document.getElementById('<%=txtParaEmail.ClientID%>').value);
                formData.append("qAssunto", document.getElementById('<%=txtAssuntoEmail.ClientID%>').value);
                formData.append("qCorpo", document.getElementById('<%=txtCorpoEmail.ClientID%>').value);
                //formData.append("pasta_to", pasta_to);
                //formData.append("arquivo", file);
               
                $.ajax({
                url: "wsSapiens.asmx/fEnviaEmailCadAutAlunosAlunos",
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
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Envio de e-mail de CadAutAlunosAlunoss';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro no envio de e-mail de CadAutAlunosAlunoss. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    } else if (json[0].P0 == "nok") {
                        $('#divModalEnviarEmail').modal('hide');
                        fPreencheTurmaCadAutAlunosAlunos();
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Envio de e-mail de CadAutAlunosAlunoss';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Problema no envio de e-mail.<br><br>Pessoa(s) com e-mail(s) não enviado(s):<br><strong>' + json[0].P1 + '</strong>';
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $("#divCabecalho").removeClass("alert-danger");
                        $('#divCabecalho').addClass('alert-warning');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        $('#divModalEnviarEmail').modal('hide');
                        fPreencheTurmaCadAutAlunosAlunos();
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
                    alert("Houve um erro no envio do E-mail dos CadAutAlunosAlunoss. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando();
                },
                failure: function () 
                {
                    alert("Houve um erro no envio do E-mail dos CadAutAlunosAlunoss. Por favor tente novamente!");
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
                            fPreencheTurmaCadAutAlunosAlunos();
                            
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
            if (!document.getElementById('optEscritaCadAutAlunosAlunos').checked) {
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
                            fPreencheTurmaCadAutAlunosAlunos();
                            
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
                            fPreencheTurmaCadAutAlunosAlunos();
                            
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