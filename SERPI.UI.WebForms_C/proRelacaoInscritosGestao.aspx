<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="proRelacaoInscritosGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.proRelacaoInscritosGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liProcessoSeletivo" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liRelInscritos" />

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
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="value" />
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

         #ContentPlaceHolderBody_grdRelacaoInscritosGestao td.centralizarTH {
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
            <h3 class=""><i class="fa fa-circle-o text-fuchsia"></i>&nbsp;<strong >Relação de Inscritos</strong> (Gestão)</h3>
        </div>

        <div class="col-md-3">
            
        </div>
    </div>
    <br />

    <div class="container-fluid">

        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-body">

                    <div class="row">
                        <div class="col-md-4">
                            <span style="font-size:14px">Período de Inscrição</span><br />
                            <input class="form-control input-sm" runat="server" id="txtPeriodoInscricaoGestao" type="text" value="" readonly="true" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <span style="font-size:14px">Curso</span><br />
                            <input class="form-control input-sm" runat="server" id="txtCursoPeriodoInscricaoGestao" type="text" value="" readonly="true" />
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhuma inscrição encontrada" />
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdRelacaoInscritosGestao" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_inscricao"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" ><%--onrowdatabound="grdRelacaoInscritosGestao_RowDataBound"--%>
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "id_inscricao")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                                    <asp:TemplateField HeaderText="RG" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                        <ItemTemplate>
                                                             <%# setRg(DataBinder.Eval(Container.DataItem, "rg_rne").ToString(),DataBinder.Eval(Container.DataItem, "id_inscricao").ToString())%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CPF" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                        <ItemTemplate>
                                                            <%# Convert.ToUInt64(DataBinder.Eval(Container.DataItem, "cpf")).ToString(@"000\.000\.000\-00") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="email_res" HeaderText="Email" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                                    <asp:TemplateField HeaderText="Celular" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "celular_res").ToString() == "" ? "" :  Convert.ToUInt64(DataBinder.Eval(Container.DataItem, "celular_res")).ToString(@"00 0\.0000\-0000") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Área de concentração" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# setAreaConcentracao( Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id_area_concentracao")))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Data Inscrição" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "data_inscricao") != null ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", DataBinder.Eval(Container.DataItem, "data_inscricao")) : "" %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Conhec. do Curso" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH hidden notexport" HeaderStyle-CssClass="centralizarTH hidden notexport" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "pesquisamala").ToString() != "Outros" ? DataBinder.Eval(Container.DataItem, "pesquisamala").ToString() : DataBinder.Eval(Container.DataItem, "pesquisamala").ToString() + "/" + DataBinder.Eval(Container.DataItem, "pesquisaoutros").ToString() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Status Atual" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# setStatusAtual(DataBinder.Eval(Container.DataItem, "historico_inscricao"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Matriculado" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# setStatusMatriculado(DataBinder.Eval(Container.DataItem, "historico_inscricao"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Visualizar Inscrição" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH notexport" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# setBotaoVisualizar( Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id_inscricao")), DataBinder.Eval(Container.DataItem, "nome").ToString())%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Matricular na Turma" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH notexport" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# setBotaoMatricular(DataBinder.Eval(Container.DataItem, "historico_inscricao"), Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id_inscricao")), DataBinder.Eval(Container.DataItem, "nome").ToString(),  Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id_area_concentracao")))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Excluir Inscrição" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH notexport" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" >
                                                        <ItemTemplate>
                                                            <%# setBotaoExcluir( Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id_inscricao")), DataBinder.Eval(Container.DataItem, "nome").ToString())%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

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

                </div>
            </div>

        </div>
    </div>

        <div class="row">
            <div class="col-xs-2">
                <button type="button" runat="server" id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="if (fProcessando()) return;" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>
        </div>

    <!-- Modal Visualizar  -->
    <div class="modal fade" id="divModalVisualizarInscricao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        <label>Dados da Inscrição</label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class ="col-md-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-pencil"></i>&nbsp;Dados do Curso</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                    
                                        <div class="col-md-6">
                                            <span>Curso</span><br />
                                            <input class="form-control input-sm" id="txtNomeCurso" type="text" value="" readonly="true" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Área de Concentração </span><br />
                                            <input class="form-control input-sm" id="txtAreaConcentracaoCurso" type="text" value="" readonly="true" />
                                        </div>
                                    </div>
                                    <br />
                                </div>

                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class ="col-md-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-user"></i>&nbsp;Dados Pessoais</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <span>CPF</span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtCPFAluno" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Nome </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtNomeAluno" type="text" value="" maxlength="350" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Data Nasc. </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtNascimentoAluno" type="text" value="" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Sexo </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtSexoAluno" type="text" value="" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2">
                                            <span>CEP</span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtCEPAluno" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Endereço </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtEnderecoAluno" type="text" value="" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Número </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtEnderecoNumeroAluno" type="text" value="" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Complemento </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtEnderecoComplementoAluno" type="text" value="" maxlength="20" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-2">
                                            <span>Bairro</span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtBairroAluno" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Cidade </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtCidadeAluno" type="text" value="" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span>Estado </span><br />
                                                <input readonly="true" class="form-control input-sm" id="txtEstadoAluno" type="text" value="" />
                                        </div>

                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-4">
                                            <span>Email </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtEmailAluno" type="text" value="" />
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-3">
                                            <table align="center" width="100%" cellpadding="0" cellspacing="0" border="0"> 
                                                <tr>
                                                    <td><span style="font-size:14px">Rg </span><br /><input readonly="true" class="form-control input-sm" id="txtRg" type="text" value=""  /></td>
                                                    <td><span style="font-size:14px">Dígito </span><br /><input readonly="true" class="form-control input-sm" id="txtDigitoRg" type="text" value=""/></td>
                                                </tr> 
                                            </table>

                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span>Telefone</span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtTelefone" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span>Celular </span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtCelular" type="text" value=""/>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class ="col-md-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-edit"></i>&nbsp;Como tomou conhecimento do Curso</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <input readonly="true" class="form-control input-sm" id="txtPesquisa" type="text" value=""/>

                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-6">
                                            <span>Qual(is) outro(s)</span><br />
                                            <input readonly="true" class="form-control input-sm" id="txtOutros" type="text" value="" />
                                        </div>
                                    </div>
                                    
                                </div>

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

    <!-- Modal para Matricular na Turma -->
    <div class="modal fade" id="divModalMatricular" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-edit"></i>&nbsp;Matricular na Turma</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-8">
                                <span>Nome</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtNomeMatricula" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-10">
                                <span>Curso</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtCursoMatricula" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-10">
                                <span>Área de Concentração</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtAreaMatricula" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-6">
                                <span>Turma</span><br />
                                <asp:DropDownList runat="server" ID="ddlTurmaMatricula" style="width:100%" ClientIDMode="Static" class="form-control select2" AutoPostBack="false" >
                                </asp:DropDownList>
                            </div>
                            <div class="hidden-lg hidden-md"> 
                                <br />
                            </div>

                            <div class="col-md-3">
                                <span>Status</span><br />
                                <asp:DropDownList runat="server" ID="ddlStatusMatricula" style="width:100%" ClientIDMode="Static" class="form-control select2 SemPesquisa" AutoPostBack="false" >
                                    <asp:ListItem Text="Selecione" Value="" />
                                    <asp:ListItem Text="Especial" Value="Especial" />
                                    <asp:ListItem Text="Regular" Value="Regular" />
                                </asp:DropDownList>
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
                        <button type="button" class="btn btn-success" onclick="fBotaoMatricular()">
                            <i class="fa fa-save"></i>&nbsp;Matricular</button>
                    </div>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Matricular na Turma -->
    <div class="modal fade" id="divModalExcluirInscrito" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-red">
                    <h4 class="modal-title"><i class="fa fa-eraser"></i>&nbsp;Excluir Inscrição</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                     
                        <div class="row">
                            <div class="col-md-8">
                                <span>Nome do Candidato</span><br />
                                <input readonly="true" class="form-control input-sm" id="txtNomeIncricaoExcluir" type="text" value="" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-10">
                                <span style="font-size:large">Deseja excluir a Inscrição desse candidato?</span><br />
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
                        <button type="button" class="btn btn-success" onclick="fBotaoExcluir()">
                            <i class="fa fa-check"></i>&nbsp;Sim</button>
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
        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function fVisualizarInscrito(qIdInscricao) {
            fProcessando();
            var formData = new FormData();
            formData.append("qIdInscricao", qIdInscricao);
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fVisualizarInscrito",
                dataType: "json",
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Visualizar Inscrição';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na Visualização da Inscrição. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        document.getElementById('txtNomeCurso').value = json[0].P0;
                        document.getElementById('txtAreaConcentracaoCurso').value = json[0].P1;
                        document.getElementById('txtCPFAluno').value = json[0].P2;
                        document.getElementById('txtNomeAluno').value = json[0].P3;
                        document.getElementById('txtNascimentoAluno').value = json[0].P4;
                        document.getElementById('txtSexoAluno').value = json[0].P5;
                        document.getElementById('txtCEPAluno').value = json[0].P6;
                        document.getElementById('txtEnderecoAluno').value = json[0].P7;
                        document.getElementById('txtEnderecoNumeroAluno').value = json[0].P8;
                        document.getElementById('txtEnderecoComplementoAluno').value = json[0].P9;
                        document.getElementById('txtBairroAluno').value = json[0].P10;
                        document.getElementById('txtCidadeAluno').value = json[0].P11;
                        document.getElementById('txtEstadoAluno').value = json[0].P12;
                        document.getElementById('txtEmailAluno').value = json[0].P13;
                        document.getElementById('txtRg').value = json[0].P14;
                        document.getElementById('txtDigitoRg').value = json[0].P15;
                        document.getElementById('txtTelefone').value = json[0].P16;
                        document.getElementById('txtCelular').value = json[0].P17;
                        document.getElementById('txtPesquisa').value = json[0].P18;
                        document.getElementById('txtOutros').value = json[0].P19;

                        $('#divModalVisualizarInscricao').modal();
                    }
                    fFechaProcessando();
                },
                error: function(xhr){
                    alert("Houve um erro na Visualização da Inscrição. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na Visualização da Inscrição. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }

        //===========================

        function fMatricularInscrito(qIdInscricao, qNome, qArea) {
            document.getElementById('hCodigo').value = qIdInscricao;
            document.getElementById('txtNomeMatricula').value = qNome;
            document.getElementById('txtCursoMatricula').value = document.getElementById('<%=txtCursoPeriodoInscricaoGestao.ClientID%>').value;
            document.getElementById('txtAreaMatricula').value = qArea;
            $('#<%=ddlTurmaMatricula.ClientID%>').val('').trigger('change');
            $('#<%=ddlStatusMatricula.ClientID%>').val('').trigger('change');

            $('#divModalMatricular').modal();
        }

        //===========================

         function fExcluirInscrito(qIdInscricao, qNome) {
            document.getElementById('hCodigo').value = qIdInscricao;
            document.getElementById('txtNomeIncricaoExcluir').value = qNome;

            $('#divModalExcluirInscrito').modal();
        }

        //===========================

        function fBotaoMatricular() {
            var sAux = "";
            if ($('#<%=ddlTurmaMatricula.ClientID%>').val() == "") {
                sAux = "Deve-se selecionar uma Turma <br><br>"
            }

            if ($('#<%=ddlStatusMatricula.ClientID%>').val() == "") {
                sAux = sAux + "Deve-se selecionar um Status <br><br>"
            }

            if (sAux != "") {
                document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = sAux;
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $('#divCabecalho').addClass('alert-danger');
                $('#divMensagemModal').modal();

                return;
            }
            fMostrarProgresso1();
            document.getElementById('<%=btnMatricular.ClientID%>').click();

        }

        //==============================================

        function fBotaoExcluir() {
            var sAux = "";

            fMostrarProgresso1();
            document.getElementById('<%=btnExcluir.ClientID%>').click();

        }

        //===========================

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

            fConfiguraGrade();

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });

        });

        function fConfiguraGrade() {
            $('#<%=grdRelacaoInscritosGestao.ClientID%>').dataTable({
                stateSave: false,
                "initComplete": function(settings, json) {
                    //alert('DataTables has finished its initialisation.');
                    //fEscondeColunas();
                },
                order: [[1, 'asc']],
                "bProcessing": true,
                "columns": [
                    { "orderable": false }, //Ordenação
                    { width: "70px", "className": "text-left centralizarTH" },  //Nome
                    { width: "10px", "className": "text-center centralizarTH" },  //RG
                    { width: "10px", "className": "text-center centralizarTH" },  //CPF
                    { width: "50px", "className": "text-center centralizarTH" },  //email
                    { width: "10px", "className": "text-center centralizarTH" },  //Celular
                    { width: "50px" },  //Área concentração
                    { width: "10px", type: 'date-euro', "className": "text-center centralizarTH" },  //Data Inscriçao
                    //{ width: "15px", "className": "text-left centralizarTH" },  //Conhec. do Curso
                    { width: "10px", "className": "text-center centralizarTH" },  //Status
                    { width: "10px", "className": "text-center centralizarTH" },  //Status Matricula
                    { width: "10px", orderable: false, "className": "text-center centralizarTH" },  //Visualizar Inscrição
                    { width: "10px", orderable: false, "className": "text-center centralizarTH" },  //Matricula na turma
                    { width: "10px", orderable: false, "className": "text-center centralizarTH" }  //Excluir inscrição

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
                        var fRetornoFiltro = "Relatório de Relação de Inscritos";
                        fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Período de Inscrição: " + document.getElementById("<%=txtPeriodoInscricaoGestao.ClientID%>").value;
                        fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + document.getElementById("<%=txtCursoPeriodoInscricaoGestao.ClientID%>").value;
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Relacao_Inscritos',
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
                        var fRetornoFiltro = "Relatório de Relação de Inscritos";
                        fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Período de Inscrição: " + document.getElementById("<%=txtPeriodoInscricaoGestao.ClientID%>").value;
                        fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + document.getElementById("<%=txtCursoPeriodoInscricaoGestao.ClientID%>").value;
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Relacao_Inscritos',
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
                        var fRetornoFiltro = "Relatório de Relação de Inscritos";
                        fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Período de Inscrição: " + document.getElementById("<%=txtPeriodoInscricaoGestao.ClientID%>").value;
                        fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + document.getElementById("<%=txtCursoPeriodoInscricaoGestao.ClientID%>").value;
                        return fRetornoFiltro;
                    },
                    filename: 'Relatorio_Relacao_Inscritos',
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
        }

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
            fRetornoFiltro = "Relatório de Relação de Inscritos";
            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Período de Inscrição: " + document.getElementById("<%=txtPeriodoInscricaoGestao.ClientID%>").value;
            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + document.getElementById("<%=txtCursoPeriodoInscricaoGestao.ClientID%>").value;

            return fRetornoFiltro;
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

        $(".alteracao").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });

        $('.ddl_fecha_grid_resultados').on('select2:select', function (e) {
            $('#<%=divResultados.ClientID%>').hide();
        });
        

        $(document).ready(function () {
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
