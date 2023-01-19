<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finSolicitacaoPagtoProfessor.aspx.cs" Inherits="SERPI.UI.WebForms_C.finSolicitacaoPagtoProfessor" ValidateRequest ="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liSolicitacaoPagamentoProfessor" />

    <input type="hidden" id ="hIdProfessor"  name="hIdProfessor" value="" />
    <input type="hidden" id ="hBotaoCalculo"  name="hBotaoCalculo" value="" />
    <input type="hidden" id ="hMesInicio"  name="hMesInicio" value="" />
    <input type="hidden" id ="hMesFim"  name="hMesFim" value="" />

    <input type="hidden" id ="hIdPlano"  name="hIdPlano" value="" />
    <input type="hidden" id ="hValorSolicitar"  name="hValorSolicitar" value="" />

    <input type="hidden" id ="hIdSolicitacao"  name="hIdSolicitacao" value="" />

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hTextoEmail"  name="hTextoEmail" value="value" />
    <%--<input type="hidden" id ="hTituloPagina"  name="hTituloPagina" value="Professor (Listagem)" />--%>

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/minimal/blue.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>

    <!-- summernote -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote.css" rel="stylesheet"/>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.11/lang/summernote-pt-BR.js"></script>
      
    <style type="text/css">
        input[type="checkbox"] { position: absolute; opacity: 0; z-index: -1; }
        input[type="checkbox"]+span { font: 16pt sans-serif; color: #0E76A8; }
        input[type="checkbox"]+span:before { font: 16pt FontAwesome; content: '\00f096'; display: inline-block; width: 16pt; padding: 2px 0 0 3px; margin-right: 0.5em; }
        input[type="checkbox"]:checked+span:before { content: '\00f046'; }
        input[type="checkbox"]:focus+span:before { outline: 1px dotted #aaa; }
        input[type="checkbox"]:disabled+span { color: #999; }
        input[type="checkbox"]:not(:disabled)+span:hover:before { text-shadow: 0 1px 2px #77F; }
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

        tr {
        border-bottom: 1pt solid #808080;
        }

        #grdRecebimentoNF td.centralizarTH, #grdVisualizarSolicitacoes td.centralizarTH  {
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

    <div class="row"> 
        <div class="col-md-9">
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Solicitação Pagamento Professor </strong> (Listagem)</h3>
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
                        <div class="col-md-2">
                            <span>Identificação</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtIdProfessor" type="number" value="" maxlength="18" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-7">
                            <span>Nome</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtNomeProfessor" type="text" value="" maxlength="150" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>CPF</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtCPFProfessor" type="text" value="" maxlength="50" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4">
                            <span>Situação</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacaoProfessor" ID="optSituacaoProfessorTodos" runat="server"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoProfessorTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-4">                    
                                <asp:RadioButton GroupName="GrupoSituacaoProfessor" ID="optSituacaoProfessorSim" runat="server" Checked="true"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoProfessorSim.ClientID %>">Ativo</label>
                                </div>

                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacaoProfessor" ID="optSituacaoProfessorNao" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoProfessorNao.ClientID %>">Inativo</label>
                                </div>
                            </div>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-5">
                            <span>Saldo</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-3">
                                <asp:RadioButton GroupName="GrupoSaldoProfessor" ID="optSaldoTodos" runat="server" Checked="true"/>
                                &nbsp;
                                <label class="opt" for="<%=optSaldoTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-3">                    
                                <asp:RadioButton GroupName="GrupoSaldoProfessor" ID="optSaldoCom" runat="server"/>
                                &nbsp;
                                <label class="opt" for="<%=optSaldoCom.ClientID %>">Com Saldo</label>
                                </div>

                                <div class="col-md-3">
                                <asp:RadioButton GroupName="GrupoSaldoProfessor" ID="optSaldoSem" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optSaldoSem.ClientID %>">Sem Saldo</label>
                                </div>

                                <div class="col-md-3">
                                <asp:RadioButton GroupName="GrupoSaldoProfessor" ID="optSaldoSolicitacao" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optSaldoSolicitacao.ClientID %>">Com Solicitação</label>
                                </div>
                            </div>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button  runat="server" id="bntPerquisaProfessor" name="bntPerquisaProfessor" onclick="fProcessando()" onserverclick ="btnPerquisaProfessor_Click" title="" class="btn btn-success pull-right hidden " href="#">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                            <a id="aBntPerquisaProfessor" runat ="server" onclick="fProcessando()" onserverclick="btnPerquisaProfessor_Click" href="#" class ="btn btn-success pull-right"><i class="fa fa-check"></i><span>&nbsp;OK</span></a>
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Professor encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "professor")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Foto" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%# setLinkImagem(DataBinder.Eval(Container.DataItem, "cpf").ToString(), DataBinder.Eval(Container.DataItem, "professor").ToString())%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" DataField="id_professor" HeaderText="Identificação" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px" ItemStyle-Width="10px" />

                                            <asp:BoundField DataField="professor" HeaderText="Nome" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />

                                            <asp:BoundField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" DataField="cpf" HeaderText="CPF" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>

                                            <%--<asp:BoundField DataField="numero_documento" HeaderText="Doc. de Identificação" ItemStyle-HorizontalAlign="Center" />--%>

                                            <%--<asp:BoundField DataField="email" HeaderText="Email" />--%>

                                            <asp:TemplateField HeaderText="Situação" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "status").ToString() == "inativado" ? "<div class='text-danger'><strong>INATIVO</strong></div>" : "Ativo"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Saldo a Solicitar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setSaldo(Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"saldo_a_solicitar")), Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"pagamento")))    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Solicitado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setSaldo(Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"solicitado")), Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"pagamento")))    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Recalcular Plano" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%#setRecalcular(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_professor")), DataBinder.Eval(Container.DataItem,"professor").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Visualizar Extrato" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH hidden" HeaderStyle-CssClass="centralizarTH hidden" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%#setExtrato(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_professor")), DataBinder.Eval(Container.DataItem,"professor").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Solicitar Pagamento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%#setPagamento(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_professor")), DataBinder.Eval(Container.DataItem,"professor").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Preparar Email" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%#setEmail(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_professor")), DataBinder.Eval(Container.DataItem,"professor").ToString(), Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"solicitado"))) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Recebimento Nota Fiscal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%#setNotaFiscal(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_professor")), DataBinder.Eval(Container.DataItem,"professor").ToString(), Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"solicitado"))) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Visualizar Solicitações Pagamento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <%#setVisualizarSolicicacao(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_professor")), DataBinder.Eval(Container.DataItem,"professor").ToString()) %>
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

    <!-- Modal imagem Recalcular -->
    <div class="modal fade" id="divModalRecalcular" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-refresh"></i>&nbsp;&nbsp;Recalcular Plano</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            Professor:
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label id="lblProfessorReclacular"></label>
                        </div>
                    </div>
                    <hr />
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <strong>Cálculo por mês:</strong>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Mês/Ano
                            <input class="form-control input-sm" id="txtMesAno1" type="text" value="" />
                            <small><em>(dd/aaaa)</em></small>
                        </div>
                        <div class="hidden-md hidden-lg"><br /></div>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <br />
                            <button type="button" class="btn btn-success pull-right" onclick="fRecalcular('mes')">
                                <i class="fa fa-calculator fa-lg"></i>&nbsp;Calcular Mês</button>

                            <button runat="server" id="btnCalcularMes" name="btnCalcularMes" onclick="if (fProcessando()) return;" onserverclick ="btnCalcularMes_Click" title="" class="btn btn-success pull-right hidden " href="#">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>
                        </div>
                    </div>

                    <br />
                    <hr />
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <strong>Cálculo por período:</strong>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Mês/Ano Início
                            <input class="form-control input-sm" id="txtMesAno2" type="text" value="" />
                            <small><em>(dd/aaaa)</em></small>
                        </div>
                        <div class="hidden-md hidden-lg"><br /></div>
                        <div class="col-md-4">
                            Mês/Ano Fim
                            <input class="form-control input-sm" id="txtMesAno3" type="text" value="" />
                            <small><em>(dd/aaaa)</em></small>
                        </div>
                        <div class="hidden-md hidden-lg"><br /></div>
                        <div class="col-md-4">
                            <br />
                            <button type="button" class="btn btn-purple pull-right" onclick="fRecalcular('periodo')">
                                <i class="fa fa-calculator fa-lg"></i>&nbsp;Calcular Período</button>
                        </div>
                    </div>

                    <br />
                    <hr />
                    <br />

                    <div class="row hidden">
                        <div class="col-md-12">
                            <strong>Cálculo Total:</strong>
                        </div>
                    </div>
                    <div class="row hidden">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <br />
                            <button type="button" class="btn btn-orange pull-right" onclick="fRecalcular('total')">
                                <i class="fa fa-calculator fa-lg"></i>&nbsp;Calcular Total</button>
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

    <!-- Modal Solicitar Pagamento -->
    <div class="modal fade" id="divModalSolicitarPagto" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-green">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-money"></i>&nbsp;&nbsp;Solicitar Pagamento</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            Professor:
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label id="lblProfessorSolicitarPagto"></label>
                        </div>
                    </div>
                    <hr />
                    <br />

                    <div class="container-fluid">

                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                <div id="msgSemResultadosgrdSolicitarPagto" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <label>Nenhum Valor disponível para ser solicitado.</label>
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdSolicitarPagto" >
                                                    <div class="scroll">
                                                        <table id="grdSolicitarPagto" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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
                        <hr />
                        <br />

                        <div class="row">
                            <div class="col-xs-6 col-md-3">
                                <span>Data Solicitação</span>
                                <input class="form-control input-sm" id="txtDataSolicitacao" runat="server" type="date" />
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-xs-6 col-md-3">
                                <span>Valor a Solicitar</span>
                                <input class="form-control input-sm" id="txtValorSolicitacao" type="text" readonly />
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-xs-6 col-md-3">
                                <button id="btnSalvarSolicitacaoPagto" runat="server" type="button" class="btn btn-success pull-right" onserverclick="btnSalvarSolicitacaoPagto_Click">
                                 <i class="fa fa-save"></i>&nbsp;Cadastrar</button>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="col-xs-12 pull-right">
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                    

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
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
                                <input class="form-control input-sm" runat="server" id="txtDeEmail" type="text" readonly="readonly" />
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                            <div class="col-md-6">
                                <span >Para</span><br />
                                <textarea style="resize: none;font-size:14px" runat="server" class="form-control input-sm" rows="1" id="txtParaEmail"></textarea>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                            <div class="col-md-3">
                                <span >Cópia</span><br />
                                <textarea style="resize: none;font-size:14px" runat="server" class="form-control input-sm" rows="1" id="txtCcEmail"></textarea>
                                <input class="form-control input-sm" runat="server" id="txtCcEmailHidden" type="text" style="display:none"  />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span >Assunto</span><br />
                                <input class="form-control input-sm" runat="server" id="txtAssuntoEmail" type="text" maxlength="100"  />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-xs-4 text-right">
                                <span >Data limite para o envio da NF</span><br />
                            </div>
                            <div class="col-md-3">
                                <input class="form-control input-sm" runat="server" id="txtDataLimite" type="date"  />
                            </div>
                        </div>
                        <br />

                        <div class="row ">
                            <div class="col-md-12">
                                <span>Mensagem</span><br />
                                <textarea style="resize: vertical" id="txtCorpoEmail" name="txtCorpoEmail" class="form-control input-block-level" rows="5"></textarea>
                            </div>
                        </div>
                         <br />

                        <div class="row ">
                            <div class="col-md-12">
                                <button type="submit" class="btn btn-primary btn-sm hidden"><i class="fa fa-send"></i>&nbsp;ENVIAR</button>
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
                            <button type="button" runat="server" id="btnEnviarEmail" class="btn btn-primary pull-right hidden" onclick="if (fProcessando()) return;" onserverclick="btnEnviarEmail_Click">  <%--onserverclick="btnEnviarEmail_Click"--%>
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                            <button type="button" class="btn btn-primary pull-right" onclick="fProcessando();fEnviaEmail();" >
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Recebimento NF -->
    <div class="modal fade" id="divModalRecebimentoNF" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-purple">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-edit fa-lg"></i>&nbsp;&nbsp;Recebimento de Nota Fiscal</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            Professor:
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label id="lblProfessorRecebimentoNF"></label>
                        </div>
                    </div>
                    <hr />
                    <br />

                    <div class="container-fluid">

                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                <div id="msgSemResultadosgrdRecebimentoNF" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <label>Nenhum Valor solicitado para recebimento de Nota Fiscal.</label>
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdRecebimentoNF" >
                                                    <div class="scroll">
                                                        <table id="grdRecebimentoNF" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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
                        <hr />
                        <br />

                        <div class="row">
                            <div class="col-xs-6 col-md-3">
                                <span>Total Valor Solicitado</span>
                                <input class="form-control" id="txtTotalSolicitado" runat="server" type="text" readonly />
                            </div>
                            
                        </div>
                        <br />
                        <hr />
                        <br />

                        <div class="row">
                            <div class="col-xs-6 col-md-3">
                                <span>Nota Fiscal</span>
                                <input class="form-control input-sm" id="txtNotaFiscal" type="text" runat="server" />
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-xs-6 col-md-3">
                                <span>Data Recebimento</span>
                                <input class="form-control input-sm" id="txtDataRecebimentoNF" type="date" runat="server"/>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-xs-6 col-md-3">
                                <span>Data Pagamento</span>
                                <input class="form-control input-sm" id="txtDataPagtoNF" type="date" runat="server"/>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-xs-6 col-md-3">
                                <br />
                                <button id="btnSalvarRecebimentoNF" runat="server" type="button" class="btn btn-success pull-right hidden" onserverclick="btnSalvarRecebimentoNF_Click">
                                 <i class="fa fa-save fa-lg"></i>&nbsp;&nbsp;Cadastrar</button>
                                <button type="button" class="btn btn-success pull-right" onclick="fSalvarRecebimentoNF()">
                                 <i class="fa fa-save fa-lg"></i>&nbsp;&nbsp;Cadastrar</button>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer hidden">
                    <div class="col-xs-12 pull-right">
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                    

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Visualizar Solicitações -->
    <div class="modal fade" id="divModalVisualizarSolicitacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-yellow">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-search-plus fa-lg"></i>&nbsp;&nbsp;Visualizar Solicitações de Pagamentos</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            Professor:
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label id="lblProfessorVisualizarSolicitacoes"></label>
                        </div>
                    </div>
                    <hr />
                    <br />

                    <div class="container-fluid">

                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="grid-content">
                                                <div id="msgSemResultadosgrdVisualizarSolicitacoes" style="display:none">
                                                    <div class="alert bg-gray">
                                                        <label>Nenhum Valor de Solicitação de Pagamento encontrado.</label>
                                                    </div>
                                                </div>
                                                <div class="table-responsive" id="divgrdVisualizarSolicitacoes" >
                                                    <div class="scroll">
                                                        <table id="grdVisualizarSolicitacoes" class="table table-striped table-bordered table-condensed table-hover" role="grid" width="100%">
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
                    <div class="col-xs-12 pull-right">
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>
                    

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Visualizar Solicitações -->
    <div class="modal fade" id="divModalExcluirSolicitacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser fa-lg"></i>&nbsp;&nbsp;Excluir Solicitação</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">

                        <div class="row">

                            <div class="col-md-12">
                                <label>Deseja excluir a Solicitação abaixo?</label>
                                <input class="form-control input-sm hidden" runat="server" id="txtIdSolicitacao" type="text" />      
                            </div>

                        </div>
                        <br />

                        <div class="row">

                            <div class="col-xs-6">
                                <span>Data Solicitação</span><br />
                                <label id="lblDataSolicitacao"></label>      
                            </div>

                            <div class="col-xs-6">
                                <span>Valor Solicitado</span><br />
                                <label id="lblValorSolicitacao"></label>      
                            </div>

                        </div>


                    </div>

                </div>
                <div class="modal-footer">
                    <div class="col-xs-6 pull-left">
                        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="col-xs-6 pull-right">
                        <button id="btnExcluirSolicitacao" runat="server" type="button" class="btn btn-success pull-right" onserverclick="btnExcluirSolicitacao_Click">
                                 <i class="fa fa-eraser fa-lg"></i>&nbsp;&nbsp;Confirmar Exclusão</button>
                    </div>
                    

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

     <!-- Modal imagem Profesor -->
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

    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

    <script src="https://cdn.datatables.net/plug-ins/1.10.16/sorting/date-euro.js"></script>

    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>
        var dTotalSolicitacaoPagto = 0;

        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "locale-compare-asc": function (a, b) {
                return a.localeCompare(b, 'da', { sensitivity: 'accent' })
            },
            "locale-compare-desc": function (a, b) {
                return b.localeCompare(a, 'da', { sensitivity: 'accent' })
            }
        });



        $(document).ready(function () {

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });
            
        });

        function fFormataCheck() {
            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                //alert('oi');
                if (!$('#divModalEnviarEmail').is(':visible')) {
                    document.getElementById("<%=aBntPerquisaProfessor.ClientID%>").click();
                }
                
            }
        }

        function fEnviaEmail() {
            //alert($('#txtCorpoEmail').summernote('code'));
            
            document.getElementById("hTextoEmail").value = $('#txtCorpoEmail').summernote('code');
            document.getElementById("<%=btnEnviarEmail.ClientID%>").click();
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
            $('#txtMesAno1').mask('99/9999');
            $('#txtMesAno2').mask('99/9999');
            $('#txtMesAno3').mask('99/9999');

            $('#<%=txtCPFProfessor.ClientID%>').mask('999.999.999-99');

            $('#<%=grdResultado.ClientID%>').dataTable({
                stateSave: true,
                "bProcessing": true,
                columnDefs: [
                        {
                            "targets": [7, 8, 9, 10, 11, 12, 13],
                            "orderable": false
                        },
                        {
                            "targets": [7, 8, 9, 10, 11, 12, 13],
                            "width": '10px'
                        }
                ],
            });

            fechaLoading();

        });

        //=====================================
        function fExibeRecalcular(qId, qNome) {
            document.getElementById('lblProfessorReclacular').innerHTML = '<strong>' + qNome + '</strong>';

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            //var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var mm = String(today.getMonth()).padStart(2, '0'); //January is 0!
            if (mm == "00") {
                mm = "01";
            }
            var yyyy = today.getFullYear();
            document.getElementById('txtMesAno1').value = mm + "/" + yyyy;
            document.getElementById('txtMesAno2').value = "01/" + yyyy;
            document.getElementById('txtMesAno3').value = mm + "/" + yyyy;

            document.getElementById('hIdProfessor').value = qId;
            $('#divModalRecalcular').modal();
            //alert("Hello world");
        }

        //=====================================
        function fRecalcular(qBotao) {
            //alert('oi');
            document.getElementById('hBotaoCalculo').value = qBotao;
            var mes1 = document.getElementById('txtMesAno1').value;
            var mes2 = document.getElementById('txtMesAno2').value;
            var mes3 = document.getElementById('txtMesAno3').value;
            //alert('mes1-1: ' + mes1.substring(0, 2) + 'mes1-2: ' + mes1.substring(3, 7));
            //alert(!isNaN(mes1.substring(3,7)));
            //return;
            if (qBotao == 'mes') {
                if (!isNaN(mes1.substring(0, 2)) && !isNaN(mes1.substring(3, 7)) && mes1.substring(3, 7).length==4) {
                    document.getElementById('hMesInicio').value = mes1;
                }
                else {
                    document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                    document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "O campo Mês/Ano deve estar no formato mm/aaaa";
                    $("#divCabecalho").removeClass("alert-info");
                    $("#divCabecalho").removeClass("alert-success");
                    $("#divCabecalho").removeClass("alert-danger");
                    $("#divCabecalho").removeClass("alert-warning");
                    $('#divCabecalho').addClass('alert-danger');
                    $('#divMensagemModal').modal();
                    return;
                }
                
            }
            else if (qBotao == 'periodo') {
                if (!isNaN(mes2.substring(0, 2)) && !isNaN(mes2.substring(3, 7)) && !isNaN(mes3.substring(0, 2)) && !isNaN(mes3.substring(3, 7)) && mes2.substring(3, 7).length==4 && mes3.substring(3, 7).length==4) {
                    document.getElementById('hMesInicio').value = mes2;
                    document.getElementById('hMesFim').value = mes3;
                }
                else {
                    document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                    document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "Os campos Mês/Ano (início e fim) devem estar no formato mm/aaaa";
                    $("#divCabecalho").removeClass("alert-info");
                    $("#divCabecalho").removeClass("alert-success");
                    $("#divCabecalho").removeClass("alert-danger");
                    $("#divCabecalho").removeClass("alert-warning");
                    $('#divCabecalho').addClass('alert-danger');
                    $('#divMensagemModal').modal();
                    return;
                }

            }
            document.getElementById('<%=btnCalcularMes.ClientID%>').click();
            //alert("Hello world");
        }

        //=====================================
        function fExibePagamento2(qId, qNome) {
            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Solicitar Pagamento';
            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "A rotina 'Solicitar Pagamento' está em desenvolvimento.";
            $("#divCabecalho").removeClass("alert-info");
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-danger");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').addClass('alert-success');
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        //=====================================
        function fExibeEmail(qId, qNome) {
            fMontaEmailSolicitacaoPagamento(qId);
            //alert("Hello world");
        }

        //=====================================
        function fExibeNotaFiscal2(qId, qNome) {
            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Recebimento Nota Fiscal';
            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "A rotina 'Recebimento Nota Fiscal' está em desenvolvimento.";
            $("#divCabecalho").removeClass("alert-info");
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-danger");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').addClass('alert-success');
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        //=====================================
        function fExibeVisualizarSolicicacao2(qId, qNome) {
            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Visualizar Solicitações Pagamento';
            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = "A rotina 'Visualizar Solicitações Pagamento' está em desenvolvimento.";
            $("#divCabecalho").removeClass("alert-info");
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-danger");
            $("#divCabecalho").removeClass("alert-warning");
            $('#divCabecalho').addClass('alert-warning');
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }
        //=====================================

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

        //===============================================================

        function fExibePagamento(qIdProfessor, qNome) {
            fProcessando();
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            document.getElementById('hIdProfessor').value = qIdProfessor;
            document.getElementById('<%=txtDataSolicitacao.ClientID%>').value = yyyy + "-" + mm + "-" + dd;
            document.getElementById('txtValorSolicitacao').value = "0";
            document.getElementById('lblProfessorSolicitarPagto').innerHTML = '<strong>' + qNome + '</strong>';
            var qIdAula = qIdAula;
            //fProcessando();
            //$('#divModalSolicitarPagto').modal();
            //return;
            var dt = $('#grdSolicitarPagto').DataTable({
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
                        
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdSolicitarPagto").style.display = "none";
                            document.getElementById("msgSemResultadosgrdSolicitarPagto").style.display = "block";
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
                            //document.getElementById("btnConfirmaPresencaAluno").style.display = "block";
                            //document.getElementById('btnConfirmaPresencaAluno').setAttribute("onClick", "fConfirmaPresencaAluno('" + qIdAula + "','" + qAchor +"');");
                            //document.getElementById('hCodigo').value = qIdAula;
                            //$(document).on('click', '.classCheck', function () { fSomaSolicitacaoPagto(this) });
                            //fFormataCheck();
                            document.getElementById('<%=btnSalvarSolicitacaoPagto.ClientID%>').style.display = 'none';
                            document.getElementById('hIdPlano').value = "";
                            document.getElementById('hValorSolicitar').value = "";
                            dTotalSolicitacaoPagto = 0;
                            document.getElementById("txtValorSolicitacao").value = "0";
                            $('#divModalSolicitarPagto').modal();
                        }
                },
                ajax: {
                    url: "wsSapiens.asmx/fSolicitarPagto?qIdProfessor=" + qIdProfessor,
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
                        "data": "P0", "title": "Mês/Ano", "orderable": false, "className": "hidden", type: 'date-euro'
                    },
                    {
                        "data": "P1", "title": "Mês/Ano", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Grupo", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Valor", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Selecionar", "orderable": false, "className": "text-center"
                    }
                ],
                order: [[0, 'asc']],
                dom: 'Blfrtip',
                lengthMenu: [[20, 40, 60, -1], [20, 40, 60, "Todos"]],
                buttons: [

                ],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
                },
                fixedHeader: true
            });
            fFechaProcessando();
        }

        //===============================================================

        function fSomaSolicitacaoPagto(elemento) {
            var sAux = (elemento.name).split("_");
            //alert(sAux[1] + " " + sAux[2]);
            if (elemento.checked) {
                dTotalSolicitacaoPagto = parseFloat(dTotalSolicitacaoPagto) + parseFloat(sAux[2])
                document.getElementById('hIdPlano').value = document.getElementById('hIdPlano').value + sAux[1] + "=" + sAux[2] + "-";
            }
            else {
                document.getElementById('hIdPlano').value = (document.getElementById('hIdPlano').value).replace(sAux[1] + "=" + sAux[2] + "-", "");
                dTotalSolicitacaoPagto = parseFloat(dTotalSolicitacaoPagto) - parseFloat(sAux[2])
            }
            document.getElementById('txtValorSolicitacao').value = dTotalSolicitacaoPagto.toLocaleString('pt-br', { minimumFractionDigits: 2 });

            if (parseFloat(dTotalSolicitacaoPagto) > parseFloat(0)) {
                document.getElementById('<%=btnSalvarSolicitacaoPagto.ClientID%>').style.display = 'block';
            }
            else {
                document.getElementById('<%=btnSalvarSolicitacaoPagto.ClientID%>').style.display = 'none';
            }
            document.getElementById('hValorSolicitar').value = document.getElementById('txtValorSolicitacao').value;

            //alert("plano: " + document.getElementById('hIdPlano').value + " Valor: " + document.getElementById('hValorSolicitar').value + " idprof: " + document.getElementById('hIdProfessor').value)
        }

        //===============================================================

        function fMontaEmailSolicitacaoPagamento(qId) {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fMontaEmailSolicitacaoPagamento?qIdProfessor=" + qId,
                    dataType: "json",
                    success: function(json)
                    {
                        //alert("passou");
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na montagem do e-mail do professor solicitado. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            $('#txtCorpoEmail').summernote('code', json[0].P1);
                            document.getElementById('<% =txtDeEmail.ClientID%>').value = json[0].P2;
                            document.getElementById('<% =txtParaEmail.ClientID%>').value = json[0].P3;
                            document.getElementById('<% =txtCcEmail.ClientID%>').value = json[0].P4;
                            document.getElementById('<% =txtAssuntoEmail.ClientID%>').value = json[0].P5;
                            document.getElementById('<% =txtDataLimite.ClientID%>').value = json[0].P6;
                            $('#divModalEnviarEmail').modal();
                        
                        }
                        fFechaProcessando();
                    },
                    error: function(xhr){
                        alert("Houve um erro na preparação do Email de Pagamento do Professor. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na preparação do Email de Pagamento do Professor. Por favor tente novamente!");
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //===============================

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

        function fExibeNotaFiscal(qIdProfessor, qNome) {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            document.getElementById('hIdProfessor').value = qIdProfessor;
            document.getElementById('<%=txtNotaFiscal.ClientID%>').value = "";
            document.getElementById('<%=txtDataRecebimentoNF.ClientID%>').value = yyyy + "-" + mm + "-" + dd;
            document.getElementById('<%=txtDataPagtoNF.ClientID%>').value = "";
            document.getElementById('txtValorSolicitacao').value = "0";
            document.getElementById('lblProfessorRecebimentoNF').innerHTML = '<strong>' + qNome + '</strong>';
            var qIdAula = qIdAula;
            //fProcessando();
            //$('#divModalRecebimentoNF').modal();
            //return;
            var dt = $('#grdRecebimentoNF').DataTable({
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
                        
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdRecebimentoNF").style.display = "none";
                            document.getElementById("msgSemResultadosgrdRecebimentoNF").style.display = "block";
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        } 
                        else
                        {
                            document.getElementById('<%=txtTotalSolicitado.ClientID%>').value = json[0].P5;
                            document.getElementById('<%=btnSalvarSolicitacaoPagto.ClientID%>').style.display = 'none';
                            document.getElementById('hIdProfessor').value = qIdProfessor;
                            document.getElementById('hIdSolicitacao').value = json[oSettings.fnRecordsTotal() - 1].P6;
                            dTotalSolicitacaoPagto = 0;
                            document.getElementById("txtValorSolicitacao").value = "0";
                            $('#divModalRecebimentoNF').modal();
                        }
                },
                ajax: {
                    url: "wsSapiens.asmx/fRecebimentoNF?qIdProfessor=" + qIdProfessor + "&qSolicitado=true",
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
                        "data": "P0", "title": "Data Solicitação", "orderable": false, "className": "text-center centralizarTH", type: 'date-euro'
                    },
                    {
                        "data": "P1", "title": "Mês/Ano", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Grupo", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Valor Grupo", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Valor Solicitado", "orderable": false, "className": "text-center centralizarTH"
                    }
                ],
                order: [[0, 'asc']],
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

        //===============================================================

        function fSalvarRecebimentoNF() {
            var sAux="";

            if (document.getElementById('<%=txtNotaFiscal.ClientID%>').value == "") {
                sAux = "Deve-se digitar o número da Nota Fiscal. <br><br>";
            }

            if (document.getElementById('<%=txtDataRecebimentoNF.ClientID%>').value == "") {
                sAux = sAux + "Deve-se digitar a Data de Recebimento. <br><br>";
            }

            if (document.getElementById('<%=txtDataPagtoNF.ClientID%>').value == "") {
                sAux = sAux + "Deve-se digitar a Data de Pagamento. <br><br>";
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-info");
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-danger");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-warning');
                $('#divMensagemModal').modal();
                return;
            }

            document.getElementById('<%=btnSalvarRecebimentoNF.ClientID%>').click();
        }

        //===============================================================

        function fExibeVisualizarSolicicacao(qIdProfessor, qNome) {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            document.getElementById('hIdProfessor').value = qIdProfessor;
            document.getElementById('lblProfessorVisualizarSolicitacoes').innerHTML = '<strong>' + qNome + '</strong>';
            var qIdAula = qIdAula;
            //fProcessando();
            //$('#divModalRecebimentoNF').modal();
            //return;
            var dt = $('#grdVisualizarSolicitacoes').DataTable({
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
                    //alert('Retorno json: ' + json);
                    document.getElementById("divgrdVisualizarSolicitacoes").style.display = "block";
                    document.getElementById("msgSemResultadosgrdVisualizarSolicitacoes").style.display = "none";

                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdVisualizarSolicitacoes").style.display = "none";
                            document.getElementById("msgSemResultadosgrdVisualizarSolicitacoes").style.display = "block";
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        } 
                        else if (json[0].P0 == null)
                        {
                            //alert(json[0].P0);
                            document.getElementById("divgrdVisualizarSolicitacoes").style.display = "none";
                            document.getElementById("msgSemResultadosgrdVisualizarSolicitacoes").style.display = "block";
                            $('#divModalVisualizarSolicitacao').modal();
                        }
                        else
                        {
                            document.getElementById('<%=txtTotalSolicitado.ClientID%>').value = json[0].P5;
                            document.getElementById('<%=btnSalvarSolicitacaoPagto.ClientID%>').style.display = 'none';
                            document.getElementById('hIdProfessor').value = qIdProfessor;
                            document.getElementById('hIdSolicitacao').value = json[oSettings.fnRecordsTotal() - 1].P6;
                            dTotalSolicitacaoPagto = 0;
                            document.getElementById("txtValorSolicitacao").value = "0";
                            $('#divModalVisualizarSolicitacao').modal();
                        }

                },
                ajax: {
                    url: "wsSapiens.asmx/fRecebimentoNF?qIdProfessor=" + qIdProfessor + "&qSolicitado=false",  
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
                        "data": "P0", "title": "Data Solicitação", "orderable": false, "className": "text-center centralizarTH", type: 'date-euro'
                    },
                    {
                        "data": "P1", "title": "Mês/Ano", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Grupo", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P3", "title": "Valor Grupo", "orderable": false, "className": "text-center"
                    },
                    {
                        "data": "P4", "title": "Valor Solicitado", "orderable": false, "className": "text-center centralizarTH"
                    },
                    {
                        "data": "P7", "title": "Mais Informações", "orderable": false, "className": "text-center centralizarTH"
                    }
                    ,
                    {
                        "data": "P8", "title": "Excluir Solicitação", "orderable": false, "className": "text-center centralizarTH"
                    }
                ],
                order: [[0, 'asc']],
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

        //===============================================================

        function fMaisInformacoes(qNota,qData_recebimento,qData_pagamento) {
            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Mais Detalhes';
            var sAux;
            sAux = "<b>Nota Fiscal: </b> " + qNota + "<br>";
            sAux = sAux + "<b>Data Recebimento: </b> " + qData_recebimento + "<br>";
            sAux = sAux + "<b>Data Pagamento: </b> " + qData_pagamento + "<br>";
            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
            $('#divModalAssociarTamanho').modal('hide');
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $("#divCabecalho").removeClass("alert-danger");
            $("#divCabecalho").removeClass("alert-primary");
            $("#divCabecalho").removeClass("alert-info");
            $('#divCabecalho').addClass('alert-warning');
            $('#divMensagemModal').modal();
        }

        //===============================================================

        function fExcluirSolicitacao(qIdSolicitacao, qData_solicitacao, qValorSolicitado) {
            document.getElementById('<% =txtIdSolicitacao.ClientID%>').value = qIdSolicitacao;
            document.getElementById('lblDataSolicitacao').innerHTML = qData_solicitacao;
            document.getElementById('lblValorSolicitacao').innerHTML = qValorSolicitado;
            ////document.getElementById('lblProfessorExcluirSolicitacoes').value = qNome;
            $('#divModalExcluirSolicitacao').modal();
        }

        //===============================================================

    </script>

</asp:Content>