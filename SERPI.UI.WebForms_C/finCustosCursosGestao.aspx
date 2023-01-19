<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finCustosCursosGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.finCustosCursosGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liCustosCurso" />

<%--    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />--%>
<link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css" rel="stylesheet" />
<link href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.bootstrap.min.css" rel="stylesheet" />
    
    <%--maskMoney--%>
    <script src="https://cdn.rawgit.com/plentz/jquery-maskmoney/master/dist/jquery.maskMoney.min.js"></script>


<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="value" />
    <input type="hidden" id ="h_grdValoresHoraAula"  name="h_grdValoresHoraAula" value="value" />
    <input type="hidden" id ="h_grdValoresBancas"  name="h_grdValoresBancas" value="value" />
    <input type="hidden" id ="h_grdValoresOrientacao"  name="h_grdValoresOrientacao" value="value" />
    <input type="hidden" id ="h_grdValoresCoordenacao"  name="h_grdValoresCoordenacao" value="value" />

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
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Custos por Curso</strong> (Gestão)</h3>
        </div>

        <div class="col-md-3">
            
        </div>
    </div>
    <br />

    <div class="container-fluid">

        <div class="panel panel-primary">
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-3">
                        <span style="font-size:14px">Tipo de Curso</span><br />
                        <input class="form-control input-sm" runat="server" id="txtTipoCursoGestao" type="text" value="" readonly="true" />
                    </div>
                    <div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-2">
                        <span style="font-size:14px">Código</span><br />
                        <input class="form-control input-sm" runat="server" id="txtCodigoCursoGestao" type="text" value="" readonly="true" />
                    </div>
                    <div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-3">
                        <span style="font-size:14px">Curso</span><br />
                        <input class="form-control input-sm" runat="server" id="txtNomeCursoGestao" type="text" value="" readonly="true" />
                    </div>
                </div>

            </div>
        </div>

        <div class="panel panel-primary">

            <div class="panel-body">

                <div class="row">

                    <div class="col-md-8">
                        <div class="grid-content">

                        <h3><asp:Label runat="server" ID="lblMsgSemResultados" Text="Valores Hora Aula" /></h3>

                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdValoresHoraAula" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                            AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="P1"
                                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "P1")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="P2" HeaderText="Forma de Recebimento" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>

                                                <asp:BoundField DataField="P3" HeaderText="Doutor" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P4" HeaderText="Mestre" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P5" HeaderText="Graduado" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P6" HeaderText="Técnico" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P8" HeaderText="Monitor" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P7" HeaderText="Editar Valores" ItemStyle-CssClass="" HeaderStyle-CssClass=""  ItemStyle-Width="5%" HtmlEncode="false" ItemStyle-HorizontalAlign="Center"/>

                                            </Columns>

                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                        </asp:GridView>

                                        <asp:Button ID="btnMatricular" CssClass="hidden" runat="server" Text="Button" OnClick="btnMatricular_Click" />
                                        <asp:Button ID="btnExcluir" CssClass="hidden" runat="server" Text="Button" OnClick="btnExcluir_Click" />

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnMatricular" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>

                <hr />
                <br />

                <div class="row">
                    <div class="col-md-8">
                        <div class="grid-content">
                        <h3><asp:Label runat="server" ID="lblMsgSemResultadosValoresBancas" Text="Valores Bancas" /></h3>

                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdValoresBancas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                            AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="P1"
                                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "P1")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="P2" HeaderText="Local" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>

                                                <asp:BoundField DataField="P3" HeaderText="Valor" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P4" HeaderText="Editar Valor" ItemStyle-CssClass="" HeaderStyle-CssClass=""  ItemStyle-Width="5%" HtmlEncode="false" ItemStyle-HorizontalAlign="Center"/>

                                            </Columns>

                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                        </asp:GridView>

                                    </ContentTemplate>
                                    <Triggers>

                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>

                <hr />
                <br />

                <div class="row">
                    <div class="col-md-8">
                        <div class="grid-content">
                        <h3><asp:Label runat="server" ID="Label1" Text="Valores Orientação" /></h3>

                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdValoresOrientacao" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                            AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="P1"
                                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "P1")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="P2" HeaderText="Forma de Recebimento" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>

                                                <asp:BoundField DataField="P3" HeaderText="Qualificação" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P4" HeaderText="Defesa" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P5" HeaderText="Editar Valor" ItemStyle-CssClass="" HeaderStyle-CssClass=""  ItemStyle-Width="5%" HtmlEncode="false" ItemStyle-HorizontalAlign="Center"/>

                                            </Columns>

                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                        </asp:GridView>

                                    </ContentTemplate>
                                    <Triggers>

                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>

                <hr />
                <br />

                <div class="row">
                    <div class="col-md-8">
                        <div class="grid-content">
                        <h3><asp:Label runat="server" ID="lblMsgSemResultadosValoresCoordenacao" Text="Valores Coordenação" /></h3>

                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdValoresCoordenacao" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                            AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="P1"
                                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "P1")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="P2" HeaderText="Tipo Coordenador" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>

                                                <asp:BoundField DataField="P3" HeaderText="Valor" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false"/>
                                                <asp:BoundField DataField="P4" HeaderText="Editar Valor" ItemStyle-CssClass="" HeaderStyle-CssClass=""  ItemStyle-Width="5%" HtmlEncode="false" ItemStyle-HorizontalAlign="Center"/>

                                            </Columns>

                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                        </asp:GridView>

                                    </ContentTemplate>
                                    <Triggers>

                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>

            </div>

            <div class="panel-footer">
                <div class="row">
                    <div class="col-xs-12">
                        <button type="button" runat="server" id="btnSalvarDados_Cover" name="btnSalvarDados_Cover" class="btn btn-success pull-right" onclick="fReadLista()" > <%--onclick="window.history.back()"--%>
                                <i class="fa fa-save fa-lg"></i>&nbsp;Salvar Dados</button>
                        <button type="button" style="visibility:hidden" runat="server" id="btnSalvarDados" name="btnSalvarDados" class="btn btn-success pull-right" onclick="if (fProcessando()) return;"  onserverclick="btnSalvarDados_Click" > <%--onclick="window.history.back()"--%>
                                <i class="fa fa-save fa-lg"></i>&nbsp;Salvar Dados</button>
                    </div>
                </div>
            </div>

        </div>

    </div>

    <div class="row">
        <div class="col-xs-2">
            <button type="button" runat="server" id="btnVoltar" name="btnVoltar" class="btn btn-default hidden" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                    <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            <button type="button" id="btnVoltarCover" name="btnVoltarCover" class="btn btn-default" onclick="fVoltar()"> <%--onclick="window.history.back()"--%>
                    <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
        </div>
    </div>

    <!-- Modal Hora Aula  -->
    <div class="modal fade" id="divModalHoraAula" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        <span>Valores Hora Aula</span></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-8">
                            <span>Forma de Recebimento</span><br />
                            <input class="form-control input-sm" id="txtIdForma" type="text" style="display:none" />
                            <input class="form-control input-sm" id="txtNomeForma" type="text" readonly="true" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>Doutor</span><br />
                            <input class="form-control input-sm" id="txtDoutorForma" type="text" />
                        </div>

                        <div class="col-md-2">
                            <span>Mestre</span><br />
                            <input class="form-control input-sm" id="txtMestreForma" type="text" />
                        </div>

                        <div class="col-md-2">
                            <span>Graduado</span><br />
                            <input class="form-control input-sm" id="txtGraduadoForma" type="text" />
                        </div>

                        <div class="col-md-2">
                            <span>Técnico</span><br />
                            <input class="form-control input-sm" id="txtTecnicoForma" type="text" />
                        </div>

                        <div class="col-md-2">
                            <span>Monitor</span><br />
                            <input class="form-control input-sm" id="txtMonitorForma" type="text" />
                        </div>

                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" id="btnEditaHoraAula" name="btnEditaHoraAula" class="btn btn-success" href="#" onclick="fAlteraHoraAula()">
                        <i class="fa fa-check"></i>&nbsp;Confirmar</button>

                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Valores Bancas  -->
    <div class="modal fade" id="divModalValoresBancas" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        <span>Valores Bancas</span>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10">
                            <input class="form-control input-sm" id="idValorBanca" type="text" style="display:none" />
                            <span><label id="lblValorBanca"></label></span><br />
                            <input class="form-control input-sm" id="txtValorBanca" type="text" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" id="btnEditaValorBanca" name="btnEditaValorBanca" class="btn btn-success" href="#" onclick="fAlteraValorBanca()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>

                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

        <!-- Modal Valores Orientação  -->
    <div class="modal fade" id="divModalValoresOrientacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        <span>Valores Orientação</span></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-8">
                            <span>Forma de Recebimento</span><br />
                            <input class="form-control input-sm" id="txtIdFormaOrientacao" type="text" style="display:none" />
                            <input class="form-control input-sm" id="txtNomeFormaOrientacao" type="text" readonly="true" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-6">
                            <span>Qualificação</span><br />
                            <input class="form-control input-sm" id="txtQualificacao" type="text" />
                        </div>

                        <div class="col-md-6">
                            <span>Defesa</span><br />
                            <input class="form-control input-sm" id="txtDefesa" type="text" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" id="btnEditaOrientacao" name="btnEditaOrientacao" class="btn btn-success" href="#" onclick="fAlteraValoresOrientacao()">
                        <i class="fa fa-check"></i>&nbsp;Confirmar</button>

                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Valores Coordenacao  -->
    <div class="modal fade" id="divModalValoresCoordenacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        <span>Valores Coordenação</span>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10">
                            <input class="form-control input-sm" id="idValorCoordenacao" type="text" style="display:none" />
                            <span><label id="lblValorCoordenacao"></label></span><br />
                            <input class="form-control input-sm" id="txtValorCoordenacao" type="text" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" id="btnEditaValorCoordenacao" name="btnEditaValorCoordenacao" class="btn btn-success" onclick="fAlteraValorCoordenacao()">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>

                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Mensagens -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="divModalVoltar" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        Atenção</h4>
                </div>
                <div class="modal-body">
                    Foram realizadas alterações em alguns valores!<br /><br />Se você voltar sem "Salvar" esses valores "Não" serão atualizados.<br /><br />Deseja voltar mesmo assim?

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Não</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-success" onclick="fVoltarConfirma()">
                            <i class="fa fa-check"></i>&nbsp;Sim</button>
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

    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>
        var $row;
        var $rowValorBanca;
        var $rowValorOrientacao;
        var $rowValorCoordenacao;

        $(document).ready(function () {

            document.getElementById('<%=btnSalvarDados_Cover.ClientID%>').disabled = true;
            //document.getElementById('<%=btnSalvarDados.ClientID%>').style.visibility = 'hidden';

            fSelect2();

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });

            $("#txtDoutorForma").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtMestreForma").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtGraduadoForma").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtTecnicoForma").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtMonitorForma").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtValorBanca").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtQualificacao").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtDefesa").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

            $("#txtValorCoordenacao").maskMoney({
                prefix: "",
                decimal: ",",
                thousands: ".",
                precision: 2,
                allowZero: true,               //Permitir que os usuários entrem zero
                allowNegative: false,            //Permitir os usuários de inserir valores negativos
            });

        });

        //==============================================================================

        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        //==============================================================================

        function fVoltar() {
            if (document.getElementById("<%=btnSalvarDados_Cover.ClientID%>").disabled == false)
            {
                $('#divModalVoltar').modal();
            }
            else {
                document.getElementById("<%=btnVoltar.ClientID%>").click();
            }
        }

        //==============================================================================

        function fVoltarConfirma() {
            document.getElementById("<%=btnVoltar.ClientID%>").click();
        }

        //==============================================================================

        function fReadLista() {
            document.getElementById("h_grdValoresHoraAula").value = "";
            document.getElementById("h_grdValoresBancas").value = "";
            document.getElementById("h_grdValoresOrientacao").value = "";
            document.getElementById("h_grdValoresCoordenacao").value = "";
            var sAux = '';

            var table = document.getElementById("<%=grdValoresHoraAula.ClientID%>");
            for (var i = 1, row; row = table.rows[i]; i++) {
                //alert("table.rows: " + table.rows.count);
                if (row.cells[0]) {
                    //alert("i: " + i);
                    //alert("row: " + row);
                    //alert("row.cells[0]: " + row.cells[0].innerHTML);
                    if (sAux != "") {
                        sAux = sAux + "||";
                    }
                    sAux = sAux + row.cells[0].innerHTML + "~~" + row.cells[2].innerHTML + "~~" + row.cells[3].innerHTML + "~~" + row.cells[4].innerHTML + "~~" + row.cells[5].innerHTML + "~~" + row.cells[6].innerHTML;
                    //alert(sAux);
                }
                //else {
                //    alert("erro");
                //}
                        
            }

            //alert("Passou: " + sAux);

            document.getElementById("h_grdValoresHoraAula").value = sAux;

            //========================================================================================

            var tableValoresBancas = document.getElementById("<%=grdValoresBancas.ClientID%>");
            sAux = '';

            for (var i = 1, row; row = tableValoresBancas.rows[i]; i++) {
                //alert("table.rows: " + table.rows.count);
                if (row.cells[0]) {
                    //alert("i: " + i);
                    //alert("row: " + row);
                    //alert("row.cells[0]: " + row.cells[0].innerHTML);
                    if (sAux != "") {
                        sAux = sAux + "||";
                    }
                    sAux = sAux + row.cells[0].innerHTML + "~~" + row.cells[2].innerHTML;
                    //alert(sAux);
                }
                //else {
                //    alert("erro");
                //}
                        
            }

            document.getElementById("h_grdValoresBancas").value = sAux;

            //========================================================================================

            var tableValoresOrientacao = document.getElementById("<%=grdValoresOrientacao.ClientID%>");
            sAux = '';

            for (var i = 1, row; row = tableValoresOrientacao.rows[i]; i++) {
                //alert("table.rows: " + table.rows.count);
                if (row.cells[0]) {
                    //alert("i: " + i);
                    //alert("row: " + row);
                    //alert("row.cells[0]: " + row.cells[0].innerHTML);
                    if (sAux != "") {
                        sAux = sAux + "||";
                    }
                    sAux = sAux + row.cells[0].innerHTML + "~~" + row.cells[2].innerHTML + "~~" + row.cells[3].innerHTML;
                    //alert(sAux);
                }
                //else {
                //    alert("erro");
                //}
                        
            }

            //alert("Passou: " + sAux);

            document.getElementById("h_grdValoresOrientacao").value = sAux;

            //========================================================================================

            var tableValoresCoordenacao = document.getElementById("<%=grdValoresCoordenacao.ClientID%>");
            sAux = '';

            for (var i = 1, row; row = tableValoresCoordenacao.rows[i]; i++) {
                //alert("table.rows: " + table.rows.count);
                if (row.cells[0]) {
                    //alert("i: " + i);
                    //alert("row: " + row);
                    //alert("row.cells[0]: " + row.cells[0].innerHTML);
                    if (sAux != "") {
                        sAux = sAux + "||";
                    }
                    sAux = sAux + row.cells[0].innerHTML + "~~" + row.cells[2].innerHTML;
                    //alert(sAux);
                }
                //else {
                //    alert("erro");
                //}
                        
            }

            document.getElementById("h_grdValoresCoordenacao").value = sAux;

            //========================================================================================

            //return;

            document.getElementById("<%=btnSalvarDados.ClientID%>").click();
        }

        //==============================================================================

        $('body').on('click', 'a.classEditaHoraAula', function () {
            $row = $(this).closest("tr");    // Find the row
            document.getElementById("txtIdForma").value = $row.find("td:nth-child(1)").text();
            document.getElementById("txtNomeForma").value = $row.find("td:nth-child(2)").text();
            document.getElementById("txtDoutorForma").value = $row.find("td:nth-child(3)").text();
            document.getElementById("txtMestreForma").value = $row.find("td:nth-child(4)").text();
            document.getElementById("txtGraduadoForma").value = $row.find("td:nth-child(5)").text();
            document.getElementById("txtTecnicoForma").value = $row.find("td:nth-child(6)").text();
            document.getElementById("txtMonitorForma").value = $row.find("td:nth-child(7)").text();
            $('#divModalHoraAula').modal();
        });

        //==============================================================================

        $('body').on('click', 'a.classEditaValorBanca', function () {
            $rowValorBanca = $(this).closest("tr");    // Find the row

            document.getElementById("idValorBanca").value = $rowValorBanca.find("td:nth-child(1)").text();
            document.getElementById("lblValorBanca").innerHTML = $rowValorBanca.find("td:nth-child(2)").text();
            document.getElementById("txtValorBanca").value = $rowValorBanca.find("td:nth-child(3)").text();
            $('#divModalValoresBancas').modal();
        });

        //==============================================================================

        $('body').on('click', 'a.classEditaValoresOrientacao', function () {
            $rowValorOrientacao = $(this).closest("tr");    // Find the row

            document.getElementById("txtIdFormaOrientacao").value = $rowValorOrientacao.find("td:nth-child(1)").text();
            document.getElementById("txtNomeFormaOrientacao").value = $rowValorOrientacao.find("td:nth-child(2)").text();
            document.getElementById("txtQualificacao").value = $rowValorOrientacao.find("td:nth-child(3)").text();
            document.getElementById("txtDefesa").value = $rowValorOrientacao.find("td:nth-child(4)").text();
            $('#divModalValoresOrientacao').modal();
        });

        //==============================================================================

        $('body').on('click', 'a.classEditaValorCoordenacao', function () {
            $rowValorCoordenacao = $(this).closest("tr");    // Find the row

            document.getElementById("idValorCoordenacao").value = $rowValorCoordenacao.find("td:nth-child(1)").text();
            document.getElementById("lblValorCoordenacao").innerHTML = $rowValorCoordenacao.find("td:nth-child(2)").text();
            document.getElementById("txtValorCoordenacao").value = $rowValorCoordenacao.find("td:nth-child(3)").text();
            $('#divModalValoresCoordenacao').modal();
        });

        //==============================================================================

        function fAlteraHoraAula() {
            $row.find("td:nth-child(1)").text(document.getElementById("txtIdForma").value);
            $row.find("td:nth-child(2)").text(document.getElementById("txtNomeForma").value);
            $row.find("td:nth-child(3)").text(document.getElementById("txtDoutorForma").value);
            $row.find("td:nth-child(4)").text(document.getElementById("txtMestreForma").value);
            $row.find("td:nth-child(5)").text(document.getElementById("txtGraduadoForma").value);
            $row.find("td:nth-child(6)").text(document.getElementById("txtTecnicoForma").value);
            $row.find("td:nth-child(7)").text(document.getElementById("txtMonitorForma").value);
            document.getElementById('<%=btnSalvarDados_Cover.ClientID%>').disabled = false;
            $('#divModalHoraAula').modal('hide');
        }

        //==============================================================================

        function fAlteraValorBanca() {
            $rowValorBanca.find("td:nth-child(3)").text(document.getElementById("txtValorBanca").value);
            document.getElementById('<%=btnSalvarDados_Cover.ClientID%>').disabled = false;
            $('#divModalValoresBancas').modal('hide');
        }

        //==============================================================================

        function fAlteraValoresOrientacao() {
            $rowValorOrientacao.find("td:nth-child(1)").text(document.getElementById("txtIdFormaOrientacao").value);
            $rowValorOrientacao.find("td:nth-child(2)").text(document.getElementById("txtNomeFormaOrientacao").value);
            $rowValorOrientacao.find("td:nth-child(3)").text(document.getElementById("txtQualificacao").value);
            $rowValorOrientacao.find("td:nth-child(4)").text(document.getElementById("txtDefesa").value);
            document.getElementById('<%=btnSalvarDados_Cover.ClientID%>').disabled = false;
            $('#divModalValoresOrientacao').modal('hide');
        }

        //==============================================================================

        function fAlteraValorCoordenacao() {
            $rowValorCoordenacao.find("td:nth-child(3)").text(document.getElementById("txtValorCoordenacao").value);
            document.getElementById('<%=btnSalvarDados_Cover.ClientID%>').disabled = false;
            $('#divModalValoresCoordenacao').modal('hide');
        }

        //==============================================================================

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


        function teclaEnter() {
            if (event.keyCode == "13") {
                //funcPesquisar();
                //alert('oi');
                if (!$('#divModalEnviarEmail').is(':visible')) {
                    <%--document.getElementById("<%=btnPerquisaProfessor.ClientID%>").click()--%>;
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
