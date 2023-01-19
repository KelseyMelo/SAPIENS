﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="aluBoletos.aspx.cs" Inherits="SERPI.UI.WebForms_C.aluBoletos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAlunoBoletoFipt" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liAlunoBoletoFipt" />
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
         #ContentPlaceHolderBody_grdAluno_Boletos td.centralizarTH {
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
            <h3 class=""><i class="fa fa-money fa-lg text-green"></i>&nbsp;<strong >Boletos</strong> (Listagem)</h3>
        </div>
    </div>
    <br />


    <div class="container-fluid">


        <div class="row">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12">
                            <div class="grid-content">
                                <div runat="server" id="msgSemResultados" visible="false">
                                    <div class="alert bg-gray">
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Boleto encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdAluno_Boletos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_alunos_boletos_parcelas"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "alunos_boletos_curso.nome_curso") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Curso" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "alunos_boletos_curso.nome_curso") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Venc." HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem,"data_venc")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Pagto" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem,"data_pagto")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vlr Original" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:C}", DataBinder.Eval(Container.DataItem,"valor_original")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Vlr Corrigido" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:C}", DataBinder.Eval(Container.DataItem,"valor_corrigido")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Vlr Pago" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:C}", DataBinder.Eval(Container.DataItem,"valor_recebido")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Imprimir Boleto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                     <%#setImprimirBoleto(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"data_venc")), Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"data_pagto")), Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"valor_corrigido")), Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"valor_original")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"IDLancamento")), Convert.ToString(DataBinder.Eval(Container.DataItem,"nossonumero")))  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           <%-- <asp:TemplateField HeaderText="Datas Serasa (Inclusão-Exclusão)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                     <%#setDatasSerasa(DataBinder.Eval(Container.DataItem,"data_inclusao_Serasa").ToString(), DataBinder.Eval(Container.DataItem,"data_exclusao_Serasa").ToString())  %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Datas e-mails enviados" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setDataEmailsEnviados(DataBinder.Eval(Container.DataItem,"alunos_inadimpentes_fipt"), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"IDCURSO"))) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ocorrencias FIPT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setOcorrenciasFIPT(DataBinder.Eval(Container.DataItem,"alunos_inadimpentes_fipt.nome").ToString(),DataBinder.Eval(Container.DataItem,"NomeCurso").ToString(),DataBinder.Eval(Container.DataItem,"ocorrencias").ToString())    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           

                                            <%--<asp:TemplateField HeaderText="Data Exclusão Serasa" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem,"data_exclusao_Serasa")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Boleto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setBotaoEmail(DataBinder.Eval(Container.DataItem,"alunos_boletos"), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"id_alunos_boletos")))    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="ShowProgress()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdAluno_Boletos_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                        </Columns>

                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                    </asp:GridView>

                                </div>

                            </div>
                        </div>
                    </div>
                    <br />

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
                    <h4 class="modal-title"><i class="fa fa-calculator fa-2x"></i>&nbsp;Recalcular TODOS os Aluno</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-12">
                                <span>ATENÇÃO</span><br /><br />
                                <span>Deseja realizar um novo processamento com as informações da Base de Dados da FIPT? </span><br />
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
                            <i class="fa fa-calculator fa-2x"></i>&nbsp;Recalcular TODOS os Aluno</button>
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

    <!-- Modal para Informação de processamento (IdAluno não encontrado, email diferente, etc) -->
    <div class="modal fade" id="divModalExibirInformacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-yellow">
                    <h4 class="modal-title"><i class="fa fa-2x fa-info-circle"></i>&nbsp;Informação</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-12">
                                <label style="font-size:large" id="lblInformacao"></label>
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

    <!-- Modal para Emissão de Boleto () -->
    <div class="modal fade" id="divModalEmitirBoleto" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div id="divHeaderClass" class="modal-header bg-danger">
                    <h4 class="modal-title"><i class="fa fa-2x fa-barcode"></i>&nbsp;Emissão de Boleto</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-12">
                                <label style="font-size:large" id="lblInformacaoBoleto"></label>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-4">
                                <span>Data Venc.</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtDataVencOrig" type="text" value=""/>
                                <input readonly="true" class="form-control input-sm" id="txtIDLancamento" type="text" value="" style="display:none"/>
                                <input readonly="true" class="form-control input-sm" id="txtTipoBoleto" type="text" value="" style="display:none"/>
                            </div>
                            <div class="col-md-4">
                                <span>Valor Orig.</span><br />
                                <input readonly="true" class="form-control input-sm" id="TxtValorOriginal" type="text" value="" />
                            </div>
                            <div class="col-md-4" id="divValorCorrigido">
                                <span>Valor Corrigido.</span><br />
                                <input readonly="true" class="form-control input-sm" id="TxtValorCorrigido" type="text" value="" />
                            </div>
                        </div>
                        <br />

                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar</button>
                    </div>

                    <div class="pull-right">
                        <button type="button" class="btn btn-success" onclick="fEmiteBoletoAluno()" >
                            <i class="fa fa-print fa-2x"></i>&nbsp;Gerar Boleto</button>
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

            $('#<%=grdAluno_Boletos.ClientID%>').dataTable({
                stateSave: true,
                "bProcessing": true,
                "columns": [
                    { "orderable": false },
                    { width: "800px"},  // Curso
                    { width: "20px"},  //  Data Venc
                    { width: "20px"},  //Data Pagto
                    { width: "20px" },  //Valor Otiginal
                    { width: "20px"},  //valor corrigido
                    { width: "20px" }, //valor paggo
                    { width: "10px" }, //Imprimir Boleto

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

                }

            });

            $('#<%=grdAluno_Boletos.ClientID%>').on('draw.dt', function() {
                fDesenhaCheckBox();
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
            fRetornoFiltro = "Boletos";
            return fRetornoFiltro;
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                //alert('oi2');
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

        function fInformacao(qInformacao) {
            document.getElementById('lblInformacao').innerHTML = qInformacao;

             $('#divModalExibirInformacao').modal();
        }

        function fEmitirBoleto(qIDLancamento, qValorOriginal, qValorCorrigido, qDataVencimento, qTipoBoleto ) {

            if (qTipoBoleto == "0") {
                $("#divHeaderClass").removeClass("bg-danger");
                $('#divHeaderClass').addClass('bg-primary');
                document.getElementById('lblInformacaoBoleto').innerHTML = "Boleto a Vencer";
                document.getElementById('divValorCorrigido').style.display = "none";
            }
            else {
                $("#divHeaderClass").removeClass("bg-primary");
                $('#divHeaderClass').addClass('bg-danger');
                document.getElementById('lblInformacaoBoleto').innerHTML = "Boleto Vencido";
                document.getElementById('TxtValorCorrigido').value = TxtValorCorrigido;
                document.getElementById('divValorCorrigido').style.display = "block";
            }
            //document.getElementById('lblInformacaoBoleto').innerHTML = "Boleto Vencido";
            document.getElementById('txtDataVencOrig').value = qDataVencimento;
            document.getElementById('txtIDLancamento').value = qIDLancamento;
            document.getElementById('txtTipoBoleto').value = qTipoBoleto;
            document.getElementById('TxtValorOriginal').value = qValorOriginal;
            document.getElementById('TxtValorCorrigido').value = qValorCorrigido;

            $('#divModalEmitirBoleto').modal();
        }

        //============================================================================
        
        function fEmiteBoletoAluno() {
            fProcessando();
            try {
                var qPermissao;
                var formData = new FormData();
                formData.append("qIDLancamento", document.getElementById('txtIDLancamento').value);
                formData.append("qTipoBoleto", document.getElementById('txtTipoBoleto').value);

                    $.ajax({
                    url: "wsSapiens.asmx/fEmiteBoletoAluno",
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
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Erro Emissão Boleto';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na emissão de boleto <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //$('#divModalEditarGrupo').modal('hide');
                            //alert(json[0].P0);
                            if (document.getElementById('txtTipoBoleto').value == 0) {
                                window.open("BoletoBB_print.aspx", '_blank').focus();
                            }
                            else {
                                window.open("boletoBB.aspx", '_blank').focus();
                            }
                            
                            $('#divModalEmitirBoleto').modal('hide');
                        }
                        fFechaProcessando();
                    },
                    error: function(xhr){
                        alert("Houve um erro na emissão de boleto. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        fFechaProcessando();
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na emissão de boleto. Por favor tente novamente!");
                        fFechaProcessando();
                    }
                });
            } catch (e) {
                fFechaProcessando();
            }
        }

        //============================================================================

        function fEnviarEmailIndividual(qEmailAluno, qNomeCurso, qId_aluno,  qId_aluno_FIPT, qId_aluno_Curso, qIdCurso) {
            document.getElementById('<%=txtParaEmail.ClientID%>').value = qEmailAluno;
            document.getElementById('<%=txtCcEmail.ClientID%>').value = "financeiroensino@ipt.br";
            document.getElementById('<%=txtAssuntoEmail.ClientID%>').value = "Pendência - Curso: " + qNomeCurso;
            document.getElementById('<%=txtIdAluno.ClientID%>').value = qId_aluno;
            document.getElementById('<%=txtIdAluno_FIPT.ClientID%>').value = qId_aluno_FIPT;
            document.getElementById('<%=txtIdCurso.ClientID%>').value = qIdCurso;
            document.getElementById('<%=txtIdAlunoCurso.ClientID%>').value = qId_aluno_Curso;
            fBuscaCorpoEmailInadimplentes(qId_aluno_Curso);
        } 

        //============================================================================
        
        function fBuscaCorpoEmailInadimplentes(qId_aluno_Curso) {
            fProcessando();
            try {
                var qPermissao;
                var formData = new FormData();
                formData.append("qId_aluno_Curso", qId_aluno_Curso);

                    $.ajax({
                    url: "wsSapiens.asmx/fBuscaCorpoEmailInadimplentes",
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
            var data = $('#<%=grdAluno_Boletos.ClientID%>').DataTable().$('input').serialize();
            
            data = replaceAll("chkAlunoInadimplente_", "", data);
            data = replaceAll("=on&", ";", data);
            data = replaceAll("=on", "", data);
            data = data.split(";");

            var qListaIds = "";
            var qListaNomes = "";
            var qElemento = "";

            data.forEach(function (pDupla, indice) {
                qElemento = pDupla.split("_");
                //alert("Id: " + qElemento[0] + " - Nome: " + decodeURIComponent(qElemento[1]).replaceAll("+", " "));
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