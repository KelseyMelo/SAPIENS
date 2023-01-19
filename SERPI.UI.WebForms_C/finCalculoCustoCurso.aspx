<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finCalculoCustoCurso.aspx.cs" Inherits="SERPI.UI.WebForms_C.finCalculoCustoCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liCalculoCustos" />

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

        #ContentPlaceHolderBody_grdValorHoraAula td.centralizarTH {
                vertical-align: middle;  
            }

        #grdDetalheHoraAula td.centralizarTH {
                vertical-align: middle;  
            }

        #grdValorOrientacao td.centralizarTH1 {
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
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Relatório Pagamento de Docentes</strong></h3><%--Cálculo de Custos--%>
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
                                <div class="col-md-3">
                                    <span style="font-size:14px">Tipo de Curso</span><br />
                                    <asp:DropDownList runat="server" ID="ddlTipoCursoCalculoCusto" ClientIDMode="Static" class="fecha_grid_resultados form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoCalculoCusto_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-6">
                                    <span style="font-size:14px">Curso</span><br />
                                    <asp:DropDownList runat="server" ID="ddlCursoCalculoCusto" ClientIDMode="Static" class="fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoCalculoCusto" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />

                    <div class="row">
                        <div class="col-md-3">
                            <span style="font-size:14px">Mês</span><br />
                            <asp:DropDownList runat="server" ID="ddlMesCalculoCusto" ClientIDMode="Static" class="fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
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
                        
                        <div class="col-md-2">
                            <span style="font-size:14px">Ano </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <input class="form-control alteracao" runat="server" id="txtAnoCalculoCusto" type="number" min="2000" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span style="font-size:14px">Ordenar por </span> <%--&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />--%>
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-6">
                                    <asp:RadioButton GroupName="GrupoOrdenar" ID="optOrdenarProfessor" runat="server" Checked="true"/>
                                    &nbsp;
                                    <label class="opt" for="<%=optOrdenarProfessor.ClientID %>">Professor</label>
                                </div>
                                
                                <div class="col-md-6">                    
                                    <asp:RadioButton GroupName="GrupoOrdenar" ID="optOrdenarEmpresa" runat="server" />
                                    &nbsp;
                                    <label class="opt" for="<%=optOrdenarEmpresa.ClientID %>">Empresa</label>
                                </div>
                            </div>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button type="button" runat="server" id="bntPerquisaCalculoCusto" name="bntPerquisaCalculoCusto" onclick="if (fProcessando()) return;" title="" class="btn btn-success pull-right" onserverclick="bntPerquisaCalculoCusto_Click" > <%--onserverclick="bntPerquisaCalculoCusto_Click" --%>
                                <i class="fa fa-calculator"></i>&nbsp;Calcular</button>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div id="divgrdHoraAula" class="row" runat="server" style="display:none">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                            <div class="col-md-12 clearfix" style="">
                            <div class="grid-content">
                                <h3><asp:Label runat="server" ID="Label1" Text="CUSTOS HORA-AULA" /></h3>

                                <div runat="server" id="msgSemResultadosHoraAula" visible="true">
                                    <div class="alert bg-gray">
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum resultado encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdValorHoraAula" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" 
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "professor")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="col_Professor" HeaderText="Professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="NomeCurso" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="BotaoDetalhe" HeaderText="Detalhe" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>

                                            <asp:TemplateField HeaderText="Total Horas" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                     <%#(DataBinder.Eval(Container.DataItem, "col_Professor").ToString() == "<strong>TOTAL:</strong>") ? "" : set_ValorHora(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "col_TotalHoras")))%>                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor Hora" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                     <%# (DataBinder.Eval(Container.DataItem, "col_Professor").ToString() == "<strong>TOTAL:</strong>") ? "" : Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "valor_hora")).ToString("#,###,###,##0.00")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "col_Total")).ToString("<strong>#,###,###,##0.00</strong>")%>
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

        <br />

        <div id="divgrdBanca" class="row" runat="server" style="display:none">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12 clearfix" style="">
                            <div class="grid-content">
                                <h3><asp:Label runat="server" ID="Label2" Text="CUSTOS BANCA" /></h3>

                                <div runat="server" id="msgSemResultadosBanca" visible="true">
                                    <div class="alert bg-gray">
                                        <asp:Label runat="server" ID="Label3" Text="Nenhum resultado encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdValorBanca" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" 
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "col_Id_Professor")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="col_Professor" HeaderText="Professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_FormaRecebimento" HeaderText="Forma Recebimento" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_TipoBanca" HeaderText="Tipo Banca" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_DataHora" HeaderText="Data/Hora" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_Aluno" HeaderText="Aluno" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                     <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "col_Total")).ToString("<strong>#,###,###,##0.00</strong>")%>
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

        <br />

        <div id="divgrdOrientacao" class="row" runat="server" style="display:none">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12 clearfix" style="">
                            <div class="grid-content">
                                <h3><asp:Label runat="server" ID="Label4" Text="CUSTOS ORIENTAÇÃO" /></h3>

                                <div runat="server" id="msgSemResultadosOrientacao" visible="true">
                                    <div class="alert bg-gray">
                                        <asp:Label runat="server" ID="Label5" Text="Nenhum resultado encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdValorOrientacao" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" 
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "col_Id_Professor")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="col_Professor" HeaderText="Professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_FormaRecebimento" HeaderText="Forma Recebimento" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH hidden notexport" HeaderStyle-CssClass="centralizarTH hidden notexport" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_Empresa" HeaderText="Empresa PJ" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH hidden notexport" HeaderStyle-CssClass="centralizarTH hidden notexport" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_Curso" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH1" HeaderStyle-CssClass="centralizarTH1" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_TipoBanca" HeaderText="Tipo Banca" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH1" HeaderStyle-CssClass="centralizarTH1" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_DataHora" HeaderText="Data/Hora" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH1" HeaderStyle-CssClass="centralizarTH1" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_Aluno" HeaderText="Aluno" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH1" HeaderStyle-CssClass="centralizarTH1" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="centralizarTH1" HeaderStyle-CssClass="centralizarTH1" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                     <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "col_Total")).ToString("<strong>#,###,###,##0.00</strong>")%>
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

        <br />

        <div id="divgrdCoordenacao" class="row" runat="server" style="display:none">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12 clearfix" style="">
                            <div class="grid-content">
                                <h3><asp:Label runat="server" ID="Label7" Text="CUSTOS COORDENAÇÃO" /></h3>

                                <div runat="server" id="msgSemResultadosCoordenacao" visible="true">
                                    <div class="alert bg-gray">
                                        <asp:Label runat="server" ID="Label8" Text="Nenhum resultado encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdCoordenacao" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" 
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "col_Id_Professor")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="col_Professor" HeaderText="Professor" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_Curso" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_TipoCoordenacao" HeaderText="Tipo Coordenação" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:BoundField DataField="col_Turma" HeaderText="Turma(s) Aberta(s)" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false" HtmlEncode="false"/>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                     <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "col_Total")).ToString("<strong>#,###,###,##0.00</strong>")%>
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

        <br />

        <div id="divgrdTotal" class="row" runat="server" style="display:none">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">
                        <div class="col-md-8 clearfix" style="">
                            <h3>Total Custos para o mês de <strong><asp:Label runat="server" ID="lblMesAno" Text="Novembro/2017:" /></strong></h3>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4 clearfix text-right" style="">
                            <h3><strong><asp:Label runat="server" ID="lblTotalCusto" Text="R$ 68.390,00" /></strong></h3>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-6 clearfix center-block" style="">
                            <button type="button" runat="server" id="btnImprimirCustos" name="btnImprimirCustos" onclick="if (fPreparaRelatorio('O relatório PDF de Cáculo de Custos está sendo preparado.')) return;" title="" class="center-block btn btn-success" onserverclick="btnImprimirCustos_Click" > <%--onserverclick="bntPerquisaCalculoCusto_Click" --%>
                                <i class="fa fa-file-pdf-o"></i>&nbsp;Imprimir PDF
                            </button>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>
                        <div class="col-md-6 clearfix center-block" style="">
                            <button type="button" runat="server" id="btnImprimirCustos_excel" name="btnImprimirCustos_excel" onclick="if (fPreparaRelatorio('O relatório Excel de Cáculo de Custos está sendo preparado.')) return;" title="" class="center-block btn btn-info" onserverclick="btnImprimirCustosExcel_Click" > <%--onserverclick="bntPerquisaCalculoCusto_Click" --%>
                                <i class="fa fa-file-excel-o"></i>&nbsp;Imprimir Excel
                            </button>
                        </div>

                    </div>


                </div>

            </div>

        </div>

        <div id="divSemValores" class="row" runat="server" style="display:none">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12 clearfix" style="">
                            <h3 class="text-center">Não há movimentação financeira para o período solicitado</h3>
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

    <!-- Modal para detalhe HoraAula-->
    <div class="modal fade" id="divModalHoraAula" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-sticky-note fa-lg"></i> Detalhe Hora-Aula</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            Professor: <br /><h4><label class="negrito" id="lblNomeProfessorDetalhe"></label></h4>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-12">
                            Curso: <br /><h4><label class="negrito" id="lblNomeCursoDetalhe"></label></h4>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-12">
                            Mês/Ano: <br /><h4><label class="negrito" id="lblMesAnoDetalhe"></label></h4>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="tab-content">
                                <div class="panel panel-default">
                                    <div class="panel-body">

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="grid-content">
                                                    <div id="msgSemResultadosgrdDetalheHoraAula" style="display:block">
                                                        <div class="alert bg-gray">
                                                            <asp:Label runat="server" ID="Label6" Text="Nenhum resultado encontrado" />
                                                        </div>
                                                    </div>
                                                    <div id="divgrdDetalheHoraAula" class="" style="display: none">
                                                        <div class="">
                                                            <table id="grdDetalheHoraAula" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
                                                                <caption>Detalhes</caption>
                                                                <thead style="color: White; background-color: #507CD1; font-weight: bold;">
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

                    <hr size="100" width="100%" noshade color="#9D9D9D" />
                    <br />
                    <div class="row">
                        <div class="col-lg-12 pull-right">
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal"><i class="fa fa-close"></i> Fechar</button>
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

    <div id="divPogress" class="loading" align="center">
        Processando... <br />Por favor, aguarde.
        <br />
        <img src="img/loader.gif" width="42" height="42" alt="" />
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

        $('input').on('ifChecked', function (event) {
            document.getElementById("<%=divgrdHoraAula.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdBanca.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdOrientacao.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdCoordenacao.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdTotal.ClientID%>").style.display = "none";
            document.getElementById("<%=divSemValores.ClientID%>").style.display = "none";
        });
        ///GrupoOrdenar" ID="optOrdenarEmpresa" 

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

        

        function teclaEnter() {
            if (event.keyCode == "13") {
                //alert('oi');
                document.getElementById("<%=bntPerquisaCalculoCusto.ClientID%>").click();
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

        
        $('.fecha_grid_resultados').on("select2:selecting", function (e) {
            document.getElementById("<%=divgrdHoraAula.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdBanca.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdOrientacao.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdCoordenacao.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdTotal.ClientID%>").style.display = "none";
            document.getElementById("<%=divSemValores.ClientID%>").style.display = "none";
        });

        $(".alteracao").keydown(function () {
            document.getElementById("<%=divgrdHoraAula.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdBanca.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdOrientacao.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdCoordenacao.ClientID%>").style.display = "none";
            document.getElementById("<%=divgrdTotal.ClientID%>").style.display = "none";
            document.getElementById("<%=divSemValores.ClientID%>").style.display = "none";
        });

        $(document).ready(function () {

           <%-- $('#<%=grdValorHoraAula.ClientID%>').dataTable({
                stateSave: false,
                "bProcessing": true,
                columnDefs: [{ type: 'date-euro', targets: 2 }, { type: 'date-euro', targets: 3 }],
                order: [[2, 'desc']],
                "paging": false,
                "ordering": false,
                "info": false,
                "bFilter": false

            });

            fechaLoading();--%>

        });

        function fAbreModalDetalheHoraAula() {
            $('#divModalHoraAula').modal();
            //alert("Hello world");
        }

        //================================================================================

        function fPreencheDetalheHoraAula(qIdCurso, qIdProfessor, qData, qMesAno) {
            try {
                var dt = $('#grdDetalheHoraAula').DataTable({
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
                            document.getElementById("divgrdDetalheHoraAula").style.display = "none";
                            document.getElementById("msgSemResultadosgrdDetalheHoraAula").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdDetalheHoraAula").style.display = "none";
                                document.getElementById("msgSemResultadosgrdDetalheHoraAula").style.display = "block";
                                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                                $("#divCabecalho").removeClass("alert-success");
                                $("#divCabecalho").removeClass("alert-warning");
                                $('#divCabecalho').addClass('alert-danger');
                                $('#divMensagemModal').modal();
                            } 
                            else
                            {
                                document.getElementById("lblNomeProfessorDetalhe").innerHTML = json[0].P0;
                                document.getElementById("lblNomeCursoDetalhe").innerHTML = json[0].P1;
                                document.getElementById("lblMesAnoDetalhe").innerHTML = qMesAno;

                                document.getElementById("divgrdDetalheHoraAula").style.display = "block";
                                document.getElementById("msgSemResultadosgrdDetalheHoraAula").style.display = "none";

                                $('#divModalHoraAula').modal();

                            }
                        }
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPreencheDetalheHoraAula?qIdCurso=" + qIdCurso + "&qIdProfessor=" + qIdProfessor + "&qData=" + qData,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P2", "title": "Disciplina", "orderable": false, "className": "text-center centralizarTH", width: "25%"
                        },
                        {
                            "data": "P3", "title": "Período", "orderable": false, "className": "text-center centralizarTH", width: "25%"
                        },
                        {
                            "data": "P4", "title": "Data", "orderable": false, "className": "text-center centralizarTH", width: "25%"
                        },
                        {
                            "data": "P5", "title": "Horas", "orderable": false, "className": "text-center centralizarTH", width: '25%'
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

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        //===============================================================

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

    </script>

</asp:Content>
