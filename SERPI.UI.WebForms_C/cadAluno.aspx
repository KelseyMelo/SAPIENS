<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadAluno.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadAluno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>

<%--    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />--%>
<link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css" rel="stylesheet" />
<link href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.bootstrap.min.css" rel="stylesheet" />


<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigoAluno"  name="hCodigoAluno" value="value" />
    <%--<input type="hidden" id ="hTituloPagina"  name="hTituloPagina" value="Aluno (Listagem)" />--%>

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/minimal/blue.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>
      
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
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Aluno</strong> (Listagem)</h3>
        </div>

        <div class="col-md-3">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarAluno" name="btnCriarAluno" class="btn btn-success" href="#" onclick="" onserverclick="btnCriarAluno_Click" >
                        <i class="fa fa-magic"></i>&nbsp;Cadastrar Aluno</button>
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
                            <input class="form-control input-sm alteracao" runat="server" id="txtMatriculaAluno" type="number" value="" maxlength="18" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-8">
                            <span>Nome</span><br />
                            <input class="form-control input-sm alteracao" runat="server" id="txtNomeAluno" type="text" value="" maxlength="150" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span>CPF</span><br />
                            <input class="form-control input-sm alteracao" runat="server" id="txtCPFAluno" type="text" value="" maxlength="50" />
                        </div>

                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>Doc. de Identificação</span><br />
                            <input class="form-control input-sm alteracao" runat="server" id="txtRGAluno" type="text" value="" maxlength="20" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4">
                            <span>Email</span><br />
                            <div class="hidden">
                                    <input name="txtLogin3" id="txtLogin3" type="text" class="form-control" value="" />
                                </div>
                            <input class="form-control input-sm alteracao" runat="server" id="txtEmailAluno" type="email" value="" maxlength="100" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-6">
                            <span>Empresa</span><br />
                            <input class="form-control input-sm alteracao" runat="server" id="txtEmpresaAluno" type="text" value="" maxlength="200" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-3">
                            <span>Refazer Prova Inglês</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoProvaProficienciaIngles" ID="optProficienciaInglesTodos" runat="server" Checked="true"/>
                                &nbsp;
                                <label class="opt" for="<%=optProficienciaInglesTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-4">                    
                                <asp:RadioButton GroupName="GrupoProvaProficienciaIngles" ID="optProficienciaInglesSim" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optProficienciaInglesSim.ClientID %>">Sim</label>
                                </div>

                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoProvaProficienciaIngles" ID="optProficienciaInglesNao" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optProficienciaInglesNao.ClientID %>">Não</label>
                                </div>
                            </div>
<%--                            <asp:DropDownList runat="server" ID="ddlSexoAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                <asp:ListItem Text="Masculino" Value="m" />
                                <asp:ListItem Text="Feminino" Value="f" />
                            </asp:DropDownList>--%>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span>Refazer Prova Protuguês</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoProvaProficienciaPortugues" ID="optProficienciaPortuguesTodos" runat="server" Checked="true" />
                                    &nbsp;
                                <label class="opt" for="<%=optProficienciaPortuguesTodos.ClientID %>">Todos</label>
                                </div>

                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoProvaProficienciaPortugues" ID="optProficienciaPortuguesSim" runat="server" />
                                    &nbsp;
                                <label class="opt" for="<%=optProficienciaPortuguesSim.ClientID %>">Sim</label>
                                </div>

                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoProvaProficienciaPortugues" ID="optProficienciaPortuguesNao" runat="server" />
                                    &nbsp;
                                <label class="opt" for="<%=optProficienciaPortuguesNao.ClientID %>">Não</label>
                                </div>
                            </div>
                            <%--                            <asp:DropDownList runat="server" ID="ddlSexoAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                <asp:ListItem Text="Masculino" Value="m" />
                                <asp:ListItem Text="Feminino" Value="f" />
                            </asp:DropDownList>--%>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button  runat="server" id="bntPerquisaAluno" name="bntPerquisaAluno" onserverclick ="bntPerquisaAluno_Click" title="" class="btn btn-success pull-right hidden " href="#">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                            <a id="aBntPerquisaAluno" runat ="server" onclick="fProcessando()" onserverclick="bntPerquisaAluno_Click" href="#" class ="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Aluno encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdAluno" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="idaluno"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "nome")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="fProcessando()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdAluno_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="idaluno" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="cpf" ItemStyle-CssClass ="hidden" HeaderStyle-CssClass="hidden" HeaderText="CPF" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>

                                            <asp:BoundField DataField="numero_documento" ItemStyle-CssClass ="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderText="Doc. de Identificação" ItemStyle-HorizontalAlign="Center" />

                                            <asp:TemplateField HeaderText="Turma - Curso" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setTurmaCurso(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--DataBinder.Eval(Container.DataItem, "matricula_turma")--%>
                                            <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="empresa" HeaderText="Empresa" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <%--<asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify">
                                                <ItemTemplate>
                                                    <%# setUnidadeNegocio(DataBinder.Eval(Container.DataItem, "StatusAtivo").ToString())%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

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

        <div class="row">
            <div class="col-md-12">
            </div>
        </div>
        <br />
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
    
    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>
        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "locale-compare-asc": function (a, b) {
                return a.localeCompare(b, 'da', { sensitivity: 'accent' })
            },
            "locale-compare-desc": function (a, b) {
                return b.localeCompare(a, 'da', { sensitivity: 'accent' })
            }
        });

        $(document).ready(function () {
            $('#<%=grdAluno.ClientID%>').dataTable({
                stateSave: true,
                "bProcessing": true,
                "columns": [
                    { "orderable": false },
                    { "orderable": false },
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ],
                dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                buttons: [
                {
                    extend: 'pdf',
                    exportOptions: {
                        columns: [2, 3, 5, 6, 7, 8],
                        format: {
                            body: function (data, row, column, node) {
                                var newdata = data;
                                //if (newdata.indexOf('<hr>') != -1) {
                                //    newdata = replaceAll('<hr>', '\n', newdata);
                                //    return newdata;
                                //}
                                //else {
                                //    return newdata;
                                //}
                                newdata = replaceAll('<hr>', '\n', newdata);
                                newdata = replaceAll('<span style="line-height: 2.2em;">', '', newdata);
                                newdata = replaceAll('</span>', '', newdata);
                                return newdata;
                            }
                        }
                    },
                    orientation: 'landscape',
                    title: function () {
                            var qPulaLinha = "\n";
                            var fRetornoFiltro = "Cadastro Aluno";
                            if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtCPFAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCPFAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtRGAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " RG: " + document.getElementById("<%=txtRGAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtEmailAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Email: " + document.getElementById("<%=txtEmailAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtEmpresaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Empresa: " + document.getElementById("<%=txtEmpresaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=optProficienciaInglesSim.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: sim";
                            }
                            if (document.getElementById("<%=optProficienciaInglesNao.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: não";
                            }
                            if (document.getElementById("<%=optProficienciaPortuguesSim.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: sim";
                            }
                            if (document.getElementById("<%=optProficienciaPortuguesNao.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: não";
                            }
                            return fRetornoFiltro;
                        },
                    filename: 'Cadastro_de_Aluno',
                    text: '<i class="fa fa-file-pdf-o fa-lg" title="Pdf"><br></i>',
                    className: 'btn btn-info btn-circle'
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: [2, 3, 5, 6, 7, 8],
                        format: {
                            body: function (data, row, column, node) {
                                var newdata = data;
                                newdata = replaceAll('<hr>', '<br>', newdata);
                                return newdata;
                            }
                        }
                    },
                    orientation: 'landscape',
                    title: function () {
                            var qPulaLinha = "<br />";
                            var fRetornoFiltro = "Cadastro Aluno";
                            if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtCPFAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCPFAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtRGAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " RG: " + document.getElementById("<%=txtRGAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtEmailAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Email: " + document.getElementById("<%=txtEmailAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtEmpresaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Empresa: " + document.getElementById("<%=txtEmpresaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=optProficienciaInglesSim.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: sim";
                            }
                            if (document.getElementById("<%=optProficienciaInglesNao.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: não";
                            }
                            if (document.getElementById("<%=optProficienciaPortuguesSim.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: sim";
                            }
                            if (document.getElementById("<%=optProficienciaPortuguesNao.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: não";
                            }
                            return fRetornoFiltro;
                        },
                    filename: 'Cadastro_de_Aluno',
                    text: '<i class="fa fa-print fa-lg" title="Imprimir"><br></i>',
                    className: 'btn btn-default btn-circle'
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [2, 3, 5, 6, 7, 8],
                        format: {
                            body: function (data, row, column, node) {
                                var newdata = data;
                                newdata = replaceAll('<hr>', '; \r\n', newdata);
                                newdata = replaceAll('<span style="line-height: 2.2em;">', '', newdata);
                                newdata = replaceAll('</span>', '', newdata);
                                return newdata;
                            }
                        }
                    },
                    title: function () {
                        var qPulaLinha = ' -';
                            var fRetornoFiltro = "Cadastro Aluno";
                            if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtCPFAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCPFAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtRGAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " RG: " + document.getElementById("<%=txtRGAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtEmailAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Email: " + document.getElementById("<%=txtEmailAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtEmpresaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Empresa: " + document.getElementById("<%=txtEmpresaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=optProficienciaInglesSim.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: sim";
                            }
                            if (document.getElementById("<%=optProficienciaInglesNao.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: não";
                            }
                            if (document.getElementById("<%=optProficienciaPortuguesSim.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: sim";
                            }
                            if (document.getElementById("<%=optProficienciaPortuguesNao.ClientID%>").checked) {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: não";
                            }
                            return fRetornoFiltro;
                        },
                    filename: 'Cadastro_de_Aluno',
                    text: '<i class="fa fa-file-excel-o fa-lg" title="Excel"></i>',
                    className: 'btn btn-success  btn-circle'
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
            fRetornoFiltro = "Cadastro Aluno";
            if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=txtCPFAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=txtCPFAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=txtRGAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " RG: " + document.getElementById("<%=txtRGAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=txtEmailAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Email: " + document.getElementById("<%=txtEmailAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=txtEmpresaAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Empresa: " + document.getElementById("<%=txtEmpresaAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=optProficienciaInglesSim.ClientID%>").checked) {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: sim";
            }
            if (document.getElementById("<%=optProficienciaInglesNao.ClientID%>").checked) {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Inglês: não";
            }
            if (document.getElementById("<%=optProficienciaPortuguesSim.ClientID%>").checked) {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: sim";
            }
            if (document.getElementById("<%=optProficienciaPortuguesNao.ClientID%>").checked) {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Refazer Prova Português: não";
            }

            return fRetornoFiltro;
        }
        function teclaEnter() {
            if (event.keyCode == "13") {
                //funcPesquisar();
                //alert('oi');

                document.getElementById("<%=aBntPerquisaAluno.ClientID%>").click();
                
               <%-- alert('oi2');
               $('#<%=aBntPerquisaAluno.ClientID%>').click();
                alert('oi3');

                $('#<%=aBntPerquisaAluno.ClientID%>').trigger("click")
                alert('oi4');--%>

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


        //function funcPesquisar() {
            
        //    alert('oi');
                
        //    var table = $('#example').DataTable({
                
        //            "ajax": "teste.txt",
        //            "columns": [
        //                {
        //                    "className": 'details-control',
        //                    "orderable": false,
        //                    "data": null,
        //                    "defaultContent": ''
        //                },
        //                { "data": "name" },
        //                { "data": "position" },
        //                { "data": "office" },
        //                { "data": "salary" }
        //            ],
        //            "order": [[1, 'asc']]
        //        });

        //        // Add event listener for opening and closing details
        //        $('#example tbody').on('click', 'td.details-control', function () {
        //            var tr = $(this).closest('tr');
        //            var row = table.row(tr);

        //            if (row.child.isShown()) {
        //                // This row is already open - close it
        //                row.child.hide();
        //                tr.removeClass('shown');
        //            }
        //            else {
        //                // Open this row
        //                row.child(format(row.data())).show();
        //                tr.addClass('shown');
        //            }
        //        });



        //    /* Formatting function for row details - modify as you need */
        //    function format(d) {
        //        // `d` is the original data object for the row
        //        return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        //            '<tr>' +
        //                '<td>Full name:</td>' +
        //                '<td>' + d.name + '</td>' +
        //            '</tr>' +
        //            '<tr>' +
        //                '<td>Extension number:</td>' +
        //                '<td>' + d.extn + '</td>' +
        //            '</tr>' +
        //            '<tr>' +
        //                '<td>Extra info:</td>' +
        //                '<td>And any further details here (images etc)...</td>' +
        //            '</tr>' +
        //        '</table>';
        //    }


        //    alert('oi2');

        //}


        $(".alteracao").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });


        $(document).ready(function () {
            $('#<%=txtCPFAluno.ClientID%>').mask('999.999.999-99');
            fechaLoading();
        });



        //function isMobile() {
        //    if (navigator.userAgent.match(/Android/i)
        //         || navigator.userAgent.match(/webOS/i)
        //         || navigator.userAgent.match(/iPhone/i)
        //         || navigator.userAgent.match(/iPad/i)
        //         || navigator.userAgent.match(/iPod/i)
        //         || navigator.userAgent.match(/BlackBerry/i)
        //         || navigator.userAgent.match(/Windows Phone/i)
        //    ) {
        //        return true;
        //    }
        //    else {
        //        return false;
        //    }
        //}


        function AbreModalDesativaAlunoOffLine(qAluno, qNome) {
            //$('#divCabecalho').toggleClass(qClass);
            document.getElementById('labelCodigoAluno').innerHTML = qAluno;
            document.getElementById('labelNomeAluno').innerHTML = qNome;
            document.getElementById('hCodigoAluno').value = qAluno;
            $('#divModalDesativaAlunoOffline').modal();
            //alert("Hello world");
        }

        function AbreModalAtivaAlunoOffLine(qAluno, qNome) {
            //$('#divCabecalho').toggleClass(qClass);
            document.getElementById('labelCodigoAlunoReativar').innerHTML = qAluno;
            document.getElementById('labelNomeAlunoReativar').innerHTML = qNome;
            document.getElementById('hCodigoAluno').value = qAluno;
            $('#divModalAtivaAlunoOffline').modal();
            //alert("Hello world");
        }

        function AbreModalDescricaoEvidencia(qdescricao) {
            //$('#divCabecalho').toggleClass(qClass);
            document.getElementById('labelDescricaoEvidencia').innerHTML = qdescricao;
            $('#divModalDescricaoEvidencia').modal();
            //alert("Hello world");
        }


        function AbreModalApagarEvidencia() {
            //$('#divCabecalho').toggleClass(qClass);
            $('#divApagar').modal();
            //alert("Hello world");
        }

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
