<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadRelProfessorOrientacao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadRelProfessorOrientacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liCadRelProfessor" />
    <input type="hidden" id ="hItemMenu2"  name="hItemMenu2" value="liCadRelProfessorOrientacoes" />

<%--    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />--%>
<link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css" rel="stylesheet" />
<link href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.bootstrap.min.css" rel="stylesheet" />

    <%--Summer Note--%>
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.11/summernote.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.11/summernote.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.11/lang/summernote-pt-BR.js"></script>--%>
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.6.9/summernote.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.6.9/summernote.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.11/lang/summernote-pt-BR.js"></script>
    

<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigoAluno"  name="hCodigoAluno" value="value" />
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
         #ContentPlaceHolderBody_grdAluno td.centralizarTH {
            vertical-align: middle;  
        }

         div.dt-buttons{
position:relative;
float:left;
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
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Relatório de Orientações do Professor</strong></h3>
        </div>

        <div class="col-md-3">
            
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
                        <div class="col-md-3">
                            <span style="font-size:14px">CPF </span>
                            <input class="form-control input-sm alteracao" runat="server" id="txtCpfProfessor" type="text" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-5">
                            <span style="font-size:14px">Nome </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <input class="form-control input-sm alteracao" runat="server" id="txtNomeProfessor" type="text" value="" maxlength="150" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button  runat="server" id="btnPerquisaProfessor" name="btnPerquisaProfessor" onclick="if (ShowProgress()) return;" onserverclick ="btnPerquisaProfessor_Click" title="" class="btn btn-success pull-right">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divResultados" class="row" runat="server" visible="false">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12">
                            <div class="grid-content">
                                <div runat="server" id="msgSemResultados" visible="false">
                                    <div class="alert bg-gray">
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum registro encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdProfessorOrientacao" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="grdProfessorOrientacao_RowDataBound">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "professores.nome")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="id_professor" HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="professores.cpf" HeaderText="CPF" ItemStyle-CssClass ="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>
                                            
                                            <asp:BoundField DataField="professores.nome" HeaderText="Professor" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />

                                            <asp:TemplateField HeaderText="Data Início" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "matricula_turma.turmas.data_inicio") != null ? String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "matricula_turma.turmas.data_inicio")) : "" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Qualificação" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%#SetDataQualificacao(DataBinder.Eval(Container.DataItem,"matricula_turma.banca") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Defesa" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%#SetDataDefesa(DataBinder.Eval(Container.DataItem,"matricula_turma.banca") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="matricula_turma.alunos.nome" HeaderText="Aluno" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="titulo" HeaderText="Título" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="true"/>

                                            <asp:BoundField DataField="tipo_orientacao" HeaderText="Papel" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                        </Columns>

                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                    </asp:GridView>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>

    <!-- Modal Mensagens -->
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

    <div id="divPogress" class="loading" align="center">
        Processando... <br />Por favor, aguarde.
        <br />
        <img src="img/loader.gif" width="42" height="42" alt="" />
    </div>
<%--    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>--%>

    <%--<script src="Scripts/jquery.dataTables.min.js"></script>--%>

    <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>

    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.colVis.min.js"></script>
    
    <script src="https://cdn.datatables.net/plug-ins/1.10.16/sorting/date-euro.js"></script>

    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>

        function replaceAll(find, replace, str) {
            
            while (str.indexOf(find) > -1) {
                str = str.replace(find, replace);
            }
            return str;
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

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });
        }

        $(document).ready(function () {
            //alert('ready');
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
            });

            fSelect2();

            $('#<%=txtCpfProfessor.ClientID%>').mask('999.999.999-62');

            $('#<%=grdProfessorOrientacao.ClientID%>').dataTable({
                stateSave: false,
                "initComplete": function(settings, json) {
                    //alert('DataTables has finished its initialisation.');
                    //fEscondeColunas();
                },
                "bProcessing": true,
                "columns": [
                    { "orderable": false }, //Ordenação
                    { width: "10px" },  //Id
                    { width: "10px" },  //CPF
                    { width: "50px" },  //Professor
                    { width: "10px", type: 'date-euro'},  //Data Inicio
                    { width: "10px", type: 'date-euro' },  //Data Qualificação
                    { width: "10px", type: 'date-euro' },  //Data Defesa
                    { width: "50px" },  //Aluno
                    { width: "50px" },  //Titulo
                    { width: "10px" },  //Papel

                ],
                dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                buttons: [
                {
                    extend: 'pdf',
                    exportOptions: {
                        columns: ':not(.notexport)'
                    },
                    orientation: 'landscape',
                    title: function () {
                        //var qPulaLinha = "\n";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Orientações do Professor";
                        if (document.getElementById("<%=txtCpfProfessor.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCpfProfessor.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtNomeProfessor.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Professor: " + document.getElementById("<%=txtNomeProfessor.ClientID%>").value;
                        }
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Orientacao_Professor',
                    text: '<i class="fa fa-file-pdf-o fa-lg" title="Pdf"><br></i>',
                    className: 'btn btn-info btn-circle'
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: ':not(.notexport)'
                    },
                    orientation: 'landscape',
                    title: function () {
                        //var qPulaLinha = "\n";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Orientações do Professor";
                        if (document.getElementById("<%=txtCpfProfessor.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCpfProfessor.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtNomeProfessor.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Professor: " + document.getElementById("<%=txtNomeProfessor.ClientID%>").value;
                        }
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Orientacao_Professor',
                    text: '<i class="fa fa-print fa-lg" title="Imprimir"><br></i>',
                    className: 'btn btn-default btn-circle'
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: ':not(.notexport)'
                    },
                    title: function () {
                        //var qPulaLinha = "\n";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Orientações do Professor";
                        if (document.getElementById("<%=txtCpfProfessor.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCpfProfessor.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtNomeProfessor.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Professor: " + document.getElementById("<%=txtNomeProfessor.ClientID%>").value;
                        }
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Orientacao_Professor',
                    text: '<i class="fa fa-file-excel-o fa-lg" title="Excel"></i>',
                    className: 'btn btn-success btn-circle'
                }],
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
                }
            });

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });

        });

        function fRetornoFiltro(qFormato) {
            var qPulaLinha;
            if (qFormato = 'pdf') {
                qPulaLinha = "\n";
            }
            else if (qFormato = 'print') {
                qPulaLinha = "<br />";
            }
            else {
                qPulaLinha = "<br />";
            }
            fRetornoFiltro = "Relatório de Orientações do Professor";
            if (document.getElementById("<%=txtCpfProfessor.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCpfProfessor.ClientID%>").value;
            }
            if (document.getElementById("<%=txtNomeProfessor.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Professor: " + document.getElementById("<%=txtNomeProfessor.ClientID%>").value;
            }

            return fRetornoFiltro;
        }
        function teclaEnter() {
            if (event.keyCode == "13") {
                //funcPesquisar();
                //alert('oi');
                if (!$('#divModalEnviarEmail').is(':visible')) {
                    document.getElementById("<%=btnPerquisaProfessor.ClientID%>").click();
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

        $(".alteracao").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });

        $('.ddl_fecha_grid_resultados').on('select2:select', function (e) {
            $('#<%=divResultados.ClientID%>').hide();
        });
        

        $(document).ready(function () {
            $('#<%=txtCpfProfessor.ClientID%>').mask('999.999.999-99');
            fechaLoading();

        });

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

            var $summernote = $('#txtCorpoEmail');
            $summernote.summernote({
                airMode: false, focus: false,
                lang: 'pt-BR',
                dialogsInBody: true,
                height: 300, minHeight: 300, maxHeight: 600,         // set maximum height of editor
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




        //====Exemplo para versão 0.8.8
        //$(document).ready(function () {
        //    $('#summernote').summernote({
        //        height: "300px",
        //        dialogsInBody: true,
        //        callbacks: {
        //            onImageUpload: function (files) {
        //                uploadFile(files[0]);
        //            }
        //        }
        //    });
        //});

        //function uploadFile(file) {
        //    data = new FormData();
        //    data.append("file", file);

        //    $.ajax({
        //        data: data,
        //        type: "POST",
        //        url: "upload_url_path", //replace with your url
        //        cache: false,
        //        contentType: false,
        //        processData: false,
        //        success: function (url) {
        //            $('#summernote').summernote("insertImage", url);
        //        }
        //    });
        //}

        function AbreModalMensagem(qClass) {
            //$('#divApagar').hide();
            $("#divCabecalho").removeClass("alert-info");
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-danger");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
