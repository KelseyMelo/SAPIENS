<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="matPeriodoMatricula.aspx.cs" Inherits="SERPI.UI.WebForms_C.matPeriodoMatricula" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liMatricula" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liPeriodoMatricula" />

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
            <div class="progress">
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
            <h3 class=""><i class="fa fa-circle-o text-yellow"></i>&nbsp;<strong >Período de Matrícula</strong> (Listagem)</h3>
        </div>

        <div class="col-md-3">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarPeriodo" name="btnCriarPeriodo" class="btn btn-success" href="#" onserverclick="btnCriarPeridoMatricula_Click" >
                        <i class="fa fa-magic"></i>&nbsp;Criar Período de Matrícula</button>
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-md-2">
                                <span style="font-size:14px">Tipo de Curso</span><br />
                                <asp:DropDownList runat="server" ID="ddlTipoCursoPeriodoMatricula" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoPeriodoMatricula_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-md-2">
                                <span style="font-size:14px">Período</span><br />
                                <asp:DropDownList runat="server" ID="ddlPeriodoPeriodoMatricula" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoPeriodoMatricula" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                        <div class="col-md-3">
                            <span style="font-size:14px">Data Início </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <input class="form-control input-sm alteracao" runat="server" id="txtDataInicioPeriodoMatricula" type="date" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span style="font-size:14px">Data Fim </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <input class="form-control input-sm alteracao" runat="server" id="txtDataFimPeriodoMatricula" type="date" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button type="button" runat="server" id="bntPerquisaMatricula" name="bntPerquisaMatricula" onserverclick="bntPerquisaMatricula_Click" onclick="if (ShowProgress()) return;" title="" class="btn btn-success pull-right ">
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Período de Matrícula encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdPeriodoMatricula" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_periodo"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "quadrimestre")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="quadrimestre" HeaderText="Período" ItemStyle-HorizontalAlign="Center" />

                                            <asp:TemplateField HeaderText="Data Início" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "data_inicio"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Fim" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "data_termino"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="ShowProgress()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdPeriodoMatricula_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </span>
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
    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.16/sorting/date-euro.js"></script>
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
            fSelect2();
        });

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

        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                //alert('oi');
                document.getElementById("<%=bntPerquisaMatricula.ClientID%>").click();
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


        $(document).ready(function () {

            $('#<%=grdPeriodoMatricula.ClientID%>').dataTable({
                stateSave: false,
                "bProcessing": true,
                columnDefs: [{ type: 'date-euro', targets: 2 }, { type: 'date-euro', targets: 3 }],
                order: [[2, 'desc']]
            });

            fechaLoading();

        });


        function AbreModalDesativaProfessorOffLine(qProfessor, qNome) {
            //$('#divCabecalho').toggleClass(qClass);
            document.getElementById('labelCodigoProfessor').innerHTML = qProfessor;
            document.getElementById('labelNomeProfessor').innerHTML = qNome;
            document.getElementById('hCodigoProfessor').value = qProfessor;
            $('#divModalDesativaProfessorOffline').modal();
            //alert("Hello world");
        }

        function AbreModalAtivaProfessorOffLine(qProfessor, qNome) {
            //$('#divCabecalho').toggleClass(qClass);
            document.getElementById('labelCodigoProfessorReativar').innerHTML = qProfessor;
            document.getElementById('labelNomeProfessorReativar').innerHTML = qNome;
            document.getElementById('hCodigoProfessor').value = qProfessor;
            $('#divModalAtivaProfessorOffline').modal();
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

        function fExibeImagem(qId, qNome) {
            $('#imagepreview').attr('src', "img\\pessoas\\" + qId + "?" + new Date()); // here asign the image to the modal when the user click the enlarge link
            document.getElementById('labelNomeExibeImagem').innerHTML = qNome;
            $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
        }

    </script>

</asp:Content>
