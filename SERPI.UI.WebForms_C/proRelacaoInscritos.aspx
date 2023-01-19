<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="proRelacaoInscritos.aspx.cs" Inherits="SERPI.UI.WebForms_C.proRelacaoInscritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liProcessoSeletivo" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liRelInscritos" />

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

        #ContentPlaceHolderBody_grdRelacaoInscrito td.centralizarTH {
            vertical-align: middle;  
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
            <h3 class=""><i class="fa fa-circle-o text-fuchsia"></i>&nbsp;<strong>Relação de Inscritos</strong> (Listagem)</h3>
        </div>

        <div class="col-md-3">
            <br />

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
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-6">
                                    <span style="font-size:14px">Período de Inscrição</span><br />
                                    <asp:DropDownList runat="server" ID="ddlPeriodoInscritos" ClientIDMode="Static" onchange="fMostrarProgresso1()" class="ddl_fecha_grid_resultados form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodoInscritos_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-5">
                                    <span style="font-size:14px">Curso</span><br />
                                    <asp:DropDownList runat="server" ID="ddlCursoInscritos" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control select2 SemPesquisa" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlPeriodoInscritos" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button type="button" runat="server" id="bntPerquisaInscritos" name="bntPerquisaInscritos" onserverclick="bntPerquisaInscritos_Click" onclick="if (fProcessando()) return;" title="" class="btn btn-success pull-right ">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                            <button type="button" runat="server" id="btnEditaInscritos" name="btnEditaInscritos" onserverclick="btnEditaInscritos_Click" onclick="if (fProcessando()) return;" title="" class="btn btn-success pull-right hidden">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;EditaInscritos</button>
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Curso nesse Período." />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdRelacaoInscrito" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_periodo"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "id_periodo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="cursos.nome" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" />

                                            <asp:TemplateField HeaderText="Área de Concentração" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#setAreaConcentracao(DataBinder.Eval(Container.DataItem, "fichas_inscricao"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total por área" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#setAreaConcentracaoTotal(DataBinder.Eval(Container.DataItem, "fichas_inscricao"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="fichas_inscricao.count" HeaderText="Total Inscritos" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" />

                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setBotao(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id_periodo")), Convert.ToInt32(DataBinder.Eval(Container.DataItem, "cursos.id_curso")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                    </asp:GridView>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <br />

            <div id="divPesquisa" runat="server" visible="false" class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-6">
                            <div class="grid-content">
                                <div class="table-responsive ">
                                    <span><strong> Pesquisa IPT</strong></span><br />
                                    <asp:GridView ID="grdPesquisa" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>
                                            <asp:BoundField DataField="P1" HeaderText="Resposta" ItemStyle-HorizontalAlign="Left" />

                                            <asp:BoundField DataField="P2" HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center" />

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
        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

         $('#<%=ddlPeriodoInscritos.ClientID%>').on("select2:select", function(e) { 
            //alert($(this).val());
             $('#<%=divPesquisa.ClientID%>').hide();
             $('#<%=divResultados.ClientID%>').hide();
         });

        $('#<%=ddlCursoInscritos.ClientID%>').on("select2:select", function(e) { 
            //alert($(this).val());
             $('#<%=divPesquisa.ClientID%>').hide();
             $('#<%=divResultados.ClientID%>').hide();
         });
        
        function fEditarInscritos(qIdPeriodo, qIdCurso) {
            document.getElementById('hCodigo').value = qIdCurso;
            document.getElementById('<%=btnEditaInscritos.ClientID%>').click();
        }

        $(document).ready(function () {

            $('#<%=grdRelacaoInscrito.ClientID%>').dataTable({ stateSave: false, "bProcessing": true, order: [[2, 'asc']] });
            $('#<%=grdPesquisa.ClientID%>').dataTable({
                stateSave: false,
                searching: false, //Pesquisar
                bPaginate: false, // Paginação
                bLengthChange: false, // Mostar 10, 20 50 registros por página
                bInfo: false, //mostrando 1 de x registros
                "bProcessing": true, order: [[0, 'asc']]
            });

            fechaLoading();

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
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
                document.getElementById("<%=bntPerquisaInscritos.ClientID%>").click();
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
            $('#<%=divResultados.ClientID%>').hide();
        });

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
