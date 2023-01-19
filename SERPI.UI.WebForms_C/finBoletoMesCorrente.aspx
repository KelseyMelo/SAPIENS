<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finBoletoMesCorrente.aspx.cs" Inherits="SERPI.UI.WebForms_C.finBoletoMesCorrente" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liBoletoMesCorrente" />
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
         #ContentPlaceHolderBody_grdBoletosMesCorrente td.centralizarTH {
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
        <div class="col-md-8">
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Emissão Boleto Mês Corrente </strong>(Listagem)</h3>
        </div>

        <div class="col-md-4">
            <br />
            <div class ="pull-right ">
                <button type="button" class="btn btn-success" onclick="fCalcularLote();" > <%--onserverclick="btnCriarAluno_Click"--%>
                        <i class="fa fa-calculator"></i>&nbsp;Processar Boletos Mês Corrente (FIPT)</button>

                <button type="button"  runat="server" id="btnNovaConsultaFIPT" name="btnNovaConsultaFIPT" class="btn btn-success hidden" onclick="if (fProcessando()) return;" onserverclick="btnNovaConsultaFIPT_Click" > <%--onserverclick="btnCriarAluno_Click"--%>
                        <i class="fa fa-calculator"></i>&nbsp;Recalcular Consulta FIPT</button>

                <button type="button"  runat="server" id="btnCalcularAluno" name="btnCalcularAluno" class="btn btn-success hidden" onserverclick="btnCalcularAluno_Click" > <%--onserverclick="btnCriarAluno_Click"--%>
                        <i class="fa fa-calculator"></i>&nbsp;Recalcular Consulta FIPT</button>
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

                        <div class="col-md-4">
                            <span>Nome</span><br />
                            <input class="form-control input-sm alteracao" runat="server" id="txtNomeAluno" type="text" value="" maxlength="150" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span style="font-size:14px">Mês</span><br />
                            <asp:DropDownList runat="server" ID="ddlMesBoleto" ClientIDMode="Static" class="fecha_grid_resultados form-control input-sm select2 SemPesquisa" AutoPostBack="false">
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
                            <input class="form-control alteracao" runat="server" id="txtAnoBoleto" type="number" min="2000" />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button  runat="server" id="btnPesquisaListaInadimplenteFIPT" name="btnPesquisaListaInadimplenteFIPT" title="" class="btn btn-success pull-right hidden " href="#">  <%--onserverclick ="btnPesquisaBoletoMesCorrente_Click" --%>
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                            <a id="abtnPesquisaListaInadimplenteFIPT" name="abtnPesquisaListaInadimplenteFIPT" runat ="server" onclick="if (fProcessando()) return;" class ="btn btn-success pull-right" onserverclick="btnPesquisaBoletoMesCorrente_Click"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a> <%--onserverclick="btnPesquisaBoletoMesCorrente_Click"--%>
                        </div>

                    </div>
                    <br />

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

                                    <asp:GridView ID="grdBoletosMesCorrente" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_boleto_email"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "nome")  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="idaluno" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="email" HeaderText="E-mail" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:TemplateField HeaderText="Data Vencimento" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem,"data_vencimento")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:C2}", DataBinder.Eval(Container.DataItem,"valor")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="nosso_numero" HeaderText="Nosso Número" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:TemplateField HeaderText="Datas e-mail enviado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem,"data_envio")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Enviar E-mail (individual)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setBotaoEmail(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_boleto_email")), DataBinder.Eval(Container.DataItem,"email") != null ? DataBinder.Eval(Container.DataItem,"email").ToString() : "", DataBinder.Eval(Container.DataItem,"nosso_numero") != null ? DataBinder.Eval(Container.DataItem,"nosso_numero").ToString() : "", DataBinder.Eval(Container.DataItem,"data_envio") != null ?  Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"data_envio")) : Convert.ToDateTime("01/01/0001")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Enviar E-mail (lote)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setCheckEmail(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_boleto_email")), (DataBinder.Eval(Container.DataItem,"nome") != null ? DataBinder.Eval(Container.DataItem,"nome").ToString() : ""), (DataBinder.Eval(Container.DataItem,"email") != null ? DataBinder.Eval(Container.DataItem,"email").ToString() : ""), DataBinder.Eval(Container.DataItem,"nosso_numero") != null ? DataBinder.Eval(Container.DataItem,"nosso_numero").ToString() : "", DataBinder.Eval(Container.DataItem,"data_envio") != null ?  Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"data_envio")) : Convert.ToDateTime("01/01/0001")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Recalcular (individual)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setRecalcularAluno(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_boleto_email")), DataBinder.Eval(Container.DataItem,"nome").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                    </asp:GridView>

                                </div>

                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-12">
                            <button id="btnLocalizaEmailsLote" runat="server" type="button" class="btn btn-success" onclick="fLocalizaEmailsLote()">
                            <i class="fa fa-send-o"></i>&nbsp;Enviar Email em LOTE</button>
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

    <!-- Modal Enviar Email individual-->
    <div class="modal fade" id="divModalEnviarEmail" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-envelope"></i>&nbsp;&nbsp;E-Mail (Individual)</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-3">
                                <span >De</span><br />
                                <input class="form-control input-sm" runat="server" id="txtDeEmail" type="text" readonly="readonly" value="financeiroensino@ipt.br" />
                                <input class="form-control input-sm hidden" runat="server" id="txtIdAluno" type="text" readonly="readonly" value="" />
                                <input class="form-control input-sm hidden" runat="server" id="txtIdAluno_FIPT" type="text" readonly="readonly" value="" />
                                <input class="form-control input-sm hidden" runat="server" id="txtIdCurso" type="text" readonly="readonly" value="" />
                                <input class="form-control input-sm hidden" runat="server" id="txtIdAlunoCurso" type="text" readonly="readonly" value="" />
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                            <div class="col-md-4">
                                <span >Para</span><br />
                                <input class="form-control input-sm" runat="server" id="txtParaEmail" type="text" value="" />
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                            <div class="col-md-4">
                                <span >Cc</span><br />
                                <input class="form-control input-sm" runat="server" id="txtCcEmail" type="text" value="" readonly="true"/>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span >Assunto</span><br />
                                <input class="form-control input-sm" runat="server" id="txtAssuntoEmail" type="text" value="Pendência - Curso:" readonly="readonly" />
                            </div>
                        </div>
                        <br />

                        <div class="row ">
                            <div class="col-md-12">
                                <span>Mensagem</span><br />
                                <textarea style="resize: vertical" runat="server" id="txtCorpoEmail" name="txtCorpoEmail" class="form-control input-block-level" rows="5"></textarea>
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
                            <button type="button" runat="server" id="btnEnviarEmailUnitario" class="btn btn-primary pull-right" onclick="if (fProcessando()) return;" onserverclick="btnEnviarEmailUnitario_Click">  <%----%>
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                            <button type="button" class="btn btn-primary pull-right hidden" onclick="fEnviaEmailCertificado()" >
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para apresentar Ocorrências -->
    <div class="modal fade" id="divModalOcorrencias" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-newspaper-o fa-2x"></i>&nbsp;Ocorrências</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-8">
                                <span>Aluno</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtAlunoOcorrencia" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-10">
                                <span>Curso</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtCursoOcorrencia" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span>Ocorrêcias</span><br />
                                <textarea style="resize: vertical;font-size:14px" class="form-control input-sm" rows="5" id="txtOcorrencia"></textarea>
                            </div>
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

    <!-- Modal para Reacalcular Lote -->
    <div class="modal fade" id="divModalCalcularLote" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <h4 class="modal-title"><i class="fa fa-calculator fa-2x"></i>&nbsp;Processar Boletos do Mês Corrente</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-12">
                                <span>ATENÇÃO</span><br /><br />
                                <span>Deseja realizar um processamento dos boletos do mês corrente dentro da Base de Dados da FIPT? </span><br />
                            </div>
                        </div>
                        <br />

                        <%--<div class="row">
                            <div class="col-md-10">
                                <span>Curso</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtCursoOcorrencia1" type="text" value="" />
                            </div>
                        </div>
                        <br />--%>

                    </div>

                </div>
                <div class="modal-footer">

                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-success" onclick="fBotaoCalcularLote()">
                            <i class="fa fa-calculator fa-2x"></i>&nbsp;Processar Boletos do Mês Corrente</button>
                    </div>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Reacalcular individual -->
    <div class="modal fade" id="divModalCalcularIndividual" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-calculator"></i>&nbsp;Recalcular Aluno</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-8">
                                <span>Aluno</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtAlunoCalculo" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <%--<div class="row">
                            <div class="col-md-10">
                                <span>Curso</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtCursoOcorrencia1" type="text" value="" />
                            </div>
                        </div>
                        <br />--%>

                    </div>

                </div>
                <div class="modal-footer">

                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-success" onclick="fProcessando();fBotaoCalcularAluno()">
                            <i class="fa fa-calculator"></i>&nbsp;Recalcular Aluno</button>
                    </div>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Informação de provcessamento (IdAluno não encontrado, email diferente, etc) -->
    <div class="modal fade" id="divModalExibirInformacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <h4 class="modal-title"><i class="fa fa-info-circle"></i>&nbsp;Informação de Processamento do Aluno</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-8">
                                <span>Aluno</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtAlunoInformacao" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span>Informação</span><br />
                                <label id="lblInformacao"></label>
                            </div>
                        </div>
                        <br />

                        <%--<div class="row">
                            <div class="col-md-10">
                                <span>Curso</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtCursoOcorrencia1" type="text" value="" />
                            </div>
                        </div>
                        <br />--%>

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

    <!-- Modal para enviar email LOTE -->
    <div class="modal fade" id="divModalEnviarEmailLote" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <h4 class="modal-title"><i class="fa fa-envelope fa-2x"></i>&nbsp;Envio de E-mails (em Lote)</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-12">
                                <span>Lista de alunos que receberão e-mails</span><br />
                                <textarea style="resize: vertical;font-size:14px" class="form-control input-sm" rows="10" id="txtListaEmailsLote" readonly="true"></textarea>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="modal-footer">

                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" runat="server" class="btn btn-success" onclick="if (fProcessando()) return;" onserverclick="btnEnviarEmailLote_Click" >
                            <i class="fa fa-send fa-2x"></i>&nbsp;Enviar Emails (em Lote)</button>
                    </div>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
<%--    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>--%>

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

        function fDesenhaCheckBox() {
            $('input').iCheck({
                    checkboxClass: 'icheckbox_minimal-blue',
                    radioClass: 'iradio_minimal-blue',
                    increaseArea: '20%' // optional
                });
        }
        
        $(document).ready(function () {

            $('#<%=grdBoletosMesCorrente.ClientID%>').dataTable({
                stateSave: true,
                "bProcessing": true,
                "columns": [
                    { "orderable": false },
                    { width: "10px"},  //idMatricula
                    { width: "1500px"},  // Nome
                    { width: "1500px"},  // Email
                    { width: "10px"},  //  Data Vencimento
                    { width: "20px" },  //Valor 
                    { width: "1500px"},  // Nosso_Numero
                    { width: "10px" }, //Data Envio Email
                    { width: "5px", "orderable": false }, // Envia Enmail
                    { width: "5px", "orderable": false },  // Checkbox Lote
                    { width: "5px", "orderable": false },  // Recalcula
                ],
                //dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                buttons: [
                {
                    extend: 'pdf',
                    exportOptions: {
                        columns: [2, 3, 4, 6, 7, 8, 9, 10 ],
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
                                newdata = replaceAll('&nbsp;', '', newdata);
                                return newdata;
                            }
                        }
                    },
                    orientation: 'landscape',
                    title: function () {
                            var qPulaLinha = "\n";
                            var fRetornoFiltro = "Alunos Inadimplentes";
                            if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=ddlMesBoleto.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + document.getElementById("<%=ddlMesBoleto.ClientID%>").text;
                            }
                            if (document.getElementById("<%=txtAnoBoleto.ClientID%>").value != "") {
                                var sAux = document.getElementById("<%=txtAnoBoleto.ClientID%>").value;
                                //2015-02-01
                                sAux = sAux.substr(8,2) + "/" + sAux.substr(5,2) + "/" + sAux.substr(0,4) 
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " A partir de: " + sAux;
                            }
                            
                            return fRetornoFiltro;
                        },
                    filename: 'Alunos_inadimplentes',
                    text: '<i class="fa fa-file-pdf-o fa-lg" title="Pdf"><br></i>',
                    className: 'btn btn-info btn-circle'
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: [2, 3, 4, 5, 6, 7, 8, 9, 10],
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
                            var fRetornoFiltro = "Alunos Inadimplentes";
                             if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=ddlMesBoleto.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + document.getElementById("<%=ddlMesBoleto.ClientID%>").text;
                            }
                            if (document.getElementById("<%=txtAnoBoleto.ClientID%>").value != "") {
                                var sAux = document.getElementById("<%=txtAnoBoleto.ClientID%>").value;
                                //2015-02-01
                                sAux = sAux.substr(8,2) + "/" + sAux.substr(5,2) + "/" + sAux.substr(0,4) 
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " A partir de: " + sAux;
                            }
                            
                            return fRetornoFiltro;
                        },
                    filename: 'Alunos_inadimplentes',
                    text: '<i class="fa fa-print fa-lg" title="Imprimir"><br></i>',
                    className: 'btn btn-default btn-circle'
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [2, 3, 4, 5, 6, 7, 8, 9, 10],
                        format: {
                            body: function (data, row, column, node) {
                                var newdata = data;
                                newdata = replaceAll('<hr>', '; \r\n', newdata);
                                newdata = replaceAll('<span style="line-height: 2.2em;">', '', newdata);
                                newdata = replaceAll('</span>', '', newdata);
                                newdata = replaceAll('&nbsp;', '', newdata);
                                newdata = replaceAll('- ;', '', newdata);
                                return newdata;
                            }
                        }
                    },
                    title: function () {
                        var qPulaLinha = ' -';
                            var fRetornoFiltro = "Alunos Inadimplentes";
                            if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
                            }
                            if (document.getElementById("<%=ddlMesBoleto.ClientID%>").value != "") {
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + document.getElementById("<%=ddlMesBoleto.ClientID%>").text;
                            }
                        if (document.getElementById("<%=txtAnoBoleto.ClientID%>").value != "") {
                                var sAux = document.getElementById("<%=txtAnoBoleto.ClientID%>").value;
                                //2015-02-01
                                sAux = sAux.substr(8,2) + "/" + sAux.substr(5,2) + "/" + sAux.substr(0,4) 
                                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " A partir de: " + sAux;
                            }
                            
                            return fRetornoFiltro;
                        },
                    filename: 'Alunos_inadimplente',
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

            $('#<%=grdBoletosMesCorrente.ClientID%>').on('draw.dt', function() {
                fDesenhaCheckBox();
            });

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });

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
            fRetornoFiltro = "Alunos Inadimplentes";
            if (document.getElementById("<%=txtMatriculaAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Matrícula: " + document.getElementById("<%=txtMatriculaAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=txtNomeAluno.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome: " + document.getElementById("<%=txtNomeAluno.ClientID%>").value;
            }
            if (document.getElementById("<%=ddlMesBoleto.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " CPF: " + document.getElementById("<%=ddlMesBoleto.ClientID%>").text;
            }
            if (document.getElementById("<%=txtAnoBoleto.ClientID%>").value != "") {
                fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Nome Curso: " + document.getElementById("<%=txtAnoBoleto.ClientID%>").value;
            }
            

            return fRetornoFiltro;
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                //alert('oi2');
                document.getElementById("<%=abtnPesquisaListaInadimplenteFIPT.ClientID%>").click();
            }
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;

        function fOcorrenciasFIPT(qAluno, qCurso, qocorrencia) {
             document.getElementById('txtAlunoOcorrencia').value = qAluno;
            //alert('chegou2');
            document.getElementById('txtCursoOcorrencia').value = qCurso;
            //alert('chegou3');
           // alert('qocorrencia:' + qocorrencia);
            qocorrencia = qocorrencia.replaceAll("aspasimples", "'");
            qocorrencia = qocorrencia.replaceAll("aspadupla", "\"");
            document.getElementById('txtOcorrencia').value = qocorrencia;
            //alert('chegou4');
           
            $('#divModalOcorrencias').modal();
            //alert('chegou5');
        }

        function fCalcularAluno(pNome, pIdAluno) {
            document.getElementById('txtAlunoCalculo').value = pNome;
            document.getElementById('hCodigoAluno').value = pIdAluno;
            //alert('chegou4');
           
            $('#divModalCalcularIndividual').modal();
        }

        function fCalcularLote() {
            //alert('chegou4');
            $('#divModalCalcularLote').modal();
        }

        function fBotaoCalcularLote() {
            //alert("entrou");
            $('#divModalCalcularLote').modal('hide');
            document.getElementById('<%=btnNovaConsultaFIPT.ClientID%>').click();
        }

        function fBotaoCalcularAluno() {
            //alert("entrou");
            document.getElementById('<%=btnCalcularAluno.ClientID%>').click();
        }

        function fInformacao(qNome,qObservacao) {
            document.getElementById('txtAlunoInformacao').value = qNome;
            document.getElementById('lblInformacao').innerHTML = qObservacao;

             $('#divModalExibirInformacao').modal();
        }

        function fEnviarEmailIndividual(qEmailAluno, qId_Boleto, qNomeCurso, qId_aluno, qId_aluno_Curso, qIdCurso) {
            var dt = new Date();
            var currentMonth = dt.getMonth() + 1; 
            var currentYear = dt.getFullYear(); 

            document.getElementById('<%=txtParaEmail.ClientID%>').value = qEmailAluno;
            document.getElementById('<%=txtCcEmail.ClientID%>').value = "financeiroensino@ipt.br";
            document.getElementById('<%=txtAssuntoEmail.ClientID%>').value = "SAPIENS/IPT - Mensalidade ref.: " + currentMonth + "/" + currentYear;
            document.getElementById('<%=txtIdAluno.ClientID%>').value = qId_aluno;
            document.getElementById('<%=txtIdAluno_FIPT.ClientID%>').value = qId_Boleto;
            document.getElementById('<%=txtIdCurso.ClientID%>').value = qIdCurso;
            document.getElementById('<%=txtIdAlunoCurso.ClientID%>').value = qId_aluno_Curso;
            fBuscaCorpoEmailIAlunoBoleto(qId_Boleto);
        } 

        //============================================================================
        
        function fBuscaCorpoEmailIAlunoBoleto(qId_Boleto) {
            fProcessando();
            try {
                var qPermissao;
                var formData = new FormData();
                formData.append("qId_Boleto", qId_Boleto);

                    $.ajax({
                    url: "wsSapiens.asmx/fBuscaCorpoEmailIAlunoBoleto",
                    data: formData,
                    type: 'POST',
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (json) 
                    {
                        if (json[0].P0 == "deslogado") {
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro Elaboração Email';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na elaboração do email. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //$('#divModalEditarGrupo').modal('hide');
                            $('#<%=txtCorpoEmail.ClientID%>').summernote('destroy')
                            document.getElementById('<%=txtCorpoEmail.ClientID%>').value = json[0].P1;
                            //alert(json[0].P1);
                            fTransformaSummer('<%=txtCorpoEmail.ClientID%>');
                            $('#divModalEnviarEmail').modal();
                        }
                        fFechaProcessando();
                    },
                    error: function(xhr){
                        alert("Houve um erro na elaboração do email. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na elaboração do email. Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //============================================================================
        function fLocalizaEmailsLote() {
            var data = $('#<%=grdBoletosMesCorrente.ClientID%>').DataTable().$('input').serialize();
            
            data = replaceAll("chkAlunoBoleto_", "", data);
            data = replaceAll("=on&", ";", data);
            data = replaceAll("=on", "", data);
            data = data.split(";");

            var qListaIds = "";
            var qListaNomes = "";
            var qElemento = "";
            var j = 0;


            data.forEach(function (pDupla, indice) {
                qElemento = pDupla.split("__");
                //j++;
                //if (j<2) {
                //    alert("Id: " + qElemento[0] + " - Nome: " + decodeURIComponent(qElemento[1]).replaceAll("+", " "));
                //}
                
                qListaIds = qListaIds + qElemento[0] + ";";
                qListaNomes = qListaNomes + decodeURIComponent(qElemento[1]).replaceAll("+", " ") + "\n";
                }             
            )   

            document.getElementById('hCodigoAluno').value = qListaIds;
            document.getElementById('txtListaEmailsLote').value = qListaNomes;

            $('#divModalEnviarEmailLote').modal();
            
            return;
            
        }

//============================================================================


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


        //SUMMERNOTE =========================================================================================================
        function fTransformaSummer(qObjeto) {
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

            var $summernote = $('#' + qObjeto);
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
                height: 700, minHeight: 700, maxHeight: 1000,         // set maximum height of editor
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
