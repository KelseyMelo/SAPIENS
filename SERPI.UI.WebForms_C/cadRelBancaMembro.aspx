<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadRelBancaMembro.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadRelBancaMembro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liCadRelBanca" />
    <input type="hidden" id ="hItemMenu2"  name="hItemMenu2" value="liCadRelBancaMembros" />

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


        .larguraTdTitulo td:nth-child(5) {
           width: 25%;  
        }

        .larguraTdProfessores td:nth-child(6) {
           width: 20%;  
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
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Relatório de Membros de Bancas de Qualificação/Defesa</strong></h3>
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
                        <div class="col-md-4">
                            <span style="font-size:14px">Curso </span>
                            <asp:DropDownList runat="server" ID="ddlCurso" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Mês </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <asp:DropDownList runat="server" ID="ddlMes" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa">
                                <asp:ListItem Text="Todos" Value="0" />
                                <asp:ListItem Text="Janeiro" Value="1" />
                                <asp:ListItem Text="Fevereiro" Value="2" />
                                <asp:ListItem Text="Março" Value="3" />
                                <asp:ListItem Text="Abril" Value="4" />
                                <asp:ListItem Text="Maio" Value="5" />
                                <asp:ListItem Text="Junho" Value="6" />
                                <asp:ListItem Text="Julho" Value="7" />
                                <asp:ListItem Text="Agosto" Value="8" />
                                <asp:ListItem Text="Setembro" Value="9" />
                                <asp:ListItem Text="Outubro" Value="10" />
                                <asp:ListItem Text="Novembro" Value="11" />
                                <asp:ListItem Text="Dezembro" Value="12" />
                            </asp:DropDownList>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <span style="font-size:14px">Ano </span>
                            <input class="form-control input-sm alteracao" runat="server" id="txtAno" type="text" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Condição </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <asp:DropDownList runat="server" ID="ddlCondicao" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa">
                                <asp:ListItem Text="Todos" Value="" />
                                <asp:ListItem Text="Aprovado" Value="Aprovado" />
                                <asp:ListItem Text="Reprovado" Value="Reprovado" />
                            </asp:DropDownList>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Tipo </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <asp:DropDownList runat="server" ID="ddlTipoQualificacaoDefesa" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa">
                                <asp:ListItem Text="Qualificação" Value="Qualificação" />
                                <asp:ListItem Text="Defesa" Value="Defesa" />
                            </asp:DropDownList>
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

                                    <asp:GridView ID="grdBancaMembros" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_banca"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="grdBancaQualificacaoDefesa_RowDataBound">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "matricula_turma.alunos.nome")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="matricula_turma.alunos.idaluno" HeaderText="Matrícula" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="matricula_turma.alunos.nome" HeaderText="Aluno" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="matricula_turma.turmas.cursos.nome" HeaderText="Curso" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="matricula_turma.turmas.cod_turma" HeaderText="Turma" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>
                                            
                                            <asp:BoundField DataField="titulo" HeaderText="Título" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="true"/>

                                            <asp:TemplateField HeaderText="Professor" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%# SetProfessores(DataBinder.Eval(Container.DataItem, "banca_professores")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Papel" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%# SetPapel(DataBinder.Eval(Container.DataItem, "banca_professores")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "horario") != null ? String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "horario")) : "" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="resultado" HeaderText="Resultado" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>



                                            <%--<asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "banca.matricula_turma.alunos.nome")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="banca.matricula_turma.alunos.idaluno" HeaderText="Matrícula" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="banca.matricula_turma.alunos.nome" HeaderText="Aluno" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="banca.matricula_turma.turmas.cursos.nome" HeaderText="Curso" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="banca.matricula_turma.turmas.cod_turma" HeaderText="Turma" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>
                                            
                                            <asp:BoundField DataField="banca.titulo" HeaderText="Título" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="true"/>

                                            <asp:BoundField DataField="professores.nome" HeaderText="Professor" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />

                                            <asp:BoundField DataField="tipo_professor" HeaderText="Papel" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="true"/>
                                            
                                            <asp:TemplateField HeaderText="Data" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "banca.horario") != null ? String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "banca.horario")) : "" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="banca.resultado" HeaderText="Resultado" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>--%>



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

            $('#<%=txtAno.ClientID%>').mask('9999');

            $('#<%=grdBancaMembros.ClientID%>').dataTable({
                stateSave: false,
                "initComplete": function(settings, json) {
                    //alert('DataTables has finished its initialisation.');
                    //fEscondeColunas();
                },
                "bProcessing": true,
                "columns": [
                    { "orderable": false }, //Ordenação
                    { width: "10px" },  //Matricula
                    { width: "50px" },  //Aluno
                    { width: "50px" },  //Curso
                    { width: "10px" },  //Turma
                    { width: "50px" },  //Titulo
                    { width: "50px" },  //Professor
                    { width: "10px" },  //Papel
                    { width: "10px", type: 'date-euro' },  //Data
                    { width: "10px" },  //Resultado

                ],
                dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                buttons: [
                {
                    extend: 'pdfHtml5',
                    exportOptions: {
                        columns: ':not(.notexport)',
                        stripNewlines: false
                    },
                    customize : function(doc) {
                        //doc.styles['td:nth-child(6)'] = { 
                        //    width: '100px',
                        //    'max-width': '100px'
                        //}
                        doc.content[1].table.widths = [ '5%', '14%', '14%', '5%', '18%', '20%', '7%', '9%', '8%'];
                        
                    },
                    orientation: 'landscape',
                    title: function () {
                        //var qPulaLinha = "\n";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Membros de Bancas de " + document.getElementById("<%=ddlTipoQualificacaoDefesa.ClientID%>").value;
                        if (document.getElementById("<%=ddlCurso.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + $("#<%=ddlCurso.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=ddlMes.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Mês: " + $("#<%=ddlMes.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=txtAno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Ano: " + document.getElementById("<%=txtAno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=ddlCondicao.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Condição: " + $("#<%=ddlCondicao.ClientID%> option:selected").text();
                        }
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Membros_Banca_' + document.getElementById("<%=ddlTipoQualificacaoDefesa.ClientID%>").value,
                    text: '<i class="fa fa-file-pdf-o fa-lg" title="Pdf"><br></i>',
                    className: 'btn btn-info btn-circle'
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: ':not(.notexport)',
                        stripHtml: false
                    },
                    customize: function ( win ) {
                        $(win.document.body)
                            .css( 'font-size', '10pt' );
                            //.prepend('<img src="http://datatables.net/media/images/logo-fade.png" style="position:absolute; top:0; left:0;" />');
 
                        $(win.document.body).find('table')
                            .addClass('larguraTdProfessores')
                            .addClass('larguraTdTitulo')
                            .css('font-size', '8pt');
                    },
                    orientation: 'landscape',
                    title: function () {
                        //var qPulaLinha = "\n";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Membros de Bancas de " + document.getElementById("<%=ddlTipoQualificacaoDefesa.ClientID%>").value;
                        if (document.getElementById("<%=ddlCurso.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + $("#<%=ddlCurso.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=ddlMes.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Mês: " + $("#<%=ddlMes.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=txtAno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Ano: " + document.getElementById("<%=txtAno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=ddlCondicao.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Condição: " + $("#<%=ddlCondicao.ClientID%> option:selected").text();
                        }
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Membros_Banca_' + document.getElementById("<%=ddlTipoQualificacaoDefesa.ClientID%>").value,
                    text: '<i class="fa fa-print fa-lg" title="Imprimir"><br></i>',
                    className: 'btn btn-default btn-circle'
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: ':not(.notexport)',
                        stripNewlines: false
                    },
                    title: function () {
                        //var qPulaLinha = "\n";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Membros de Bancas de " + document.getElementById("<%=ddlTipoQualificacaoDefesa.ClientID%>").value;
                        if (document.getElementById("<%=ddlCurso.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + $("#<%=ddlCurso.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=ddlMes.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Mês: " + $("#<%=ddlMes.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=txtAno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Ano: " + document.getElementById("<%=txtAno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=ddlCondicao.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Condição: " + $("#<%=ddlCondicao.ClientID%> option:selected").text();
                        }
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Membros_Banca_' + document.getElementById("<%=ddlTipoQualificacaoDefesa.ClientID%>").value,
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

            //MergeGridCells();

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
            var fRetornoFiltro = "Relatório de Membros de Bancas de " + document.getElementById("<%=ddlTipoQualificacaoDefesa.ClientID%>").value;
            if (document.getElementById("<%=ddlCurso.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + $("#<%=ddlCurso.ClientID%> option:selected").text();
            }
            if (document.getElementById("<%=ddlMes.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Mês: " + $("#<%=ddlMes.ClientID%> option:selected").text();
            }
            if (document.getElementById("<%=txtAno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Ano: " + document.getElementById("<%=txtAno.ClientID%>").value;
            }
            if (document.getElementById("<%=ddlCondicao.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Condição: " + $("#<%=ddlCondicao.ClientID%> option:selected").text();
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
            $('#<%=txtAno.ClientID%>').mask('9999');
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

        function MergeGridCells() {
            var dimension_cells = new Array();
            var dimension_col = null;
            var columnCount = $("#<%=grdBancaMembros.ClientID%> tr:first th").length;
            for (dimension_col = 0; dimension_col < columnCount; dimension_col++) {
                // first_instance holds the first instance of identical td
                var first_instance = null;
                var rowspan = 1;
                // iterate through rows
                $("#<%=grdBancaMembros.ClientID%>").find('tr').each(function () {

                    // find the td of the correct column (determined by the dimension_col set above)
                    var dimension_td = $(this).find('td:nth-child(' + dimension_col + ')');

                    if (first_instance == null) {
                        // must be the first row
                        first_instance = dimension_td;
                    } else if (dimension_td.text() == first_instance.text()) {
                        // the current td is identical to the previous
                        // remove the current td
                        dimension_td.remove();
                        ++rowspan;
                        // increment the rowspan attribute of the first instance
                        first_instance.attr('rowspan', rowspan);
                    } else {
                        // this cell is different from the last
                        first_instance = dimension_td;
                        rowspan = 1;
                    }
                });
            }
        }

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
