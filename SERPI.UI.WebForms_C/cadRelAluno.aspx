<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadRelAluno.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadRelAluno" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li9RelatorioAlunos" />

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
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Relatório de Alunos</strong></h3>
        </div>

        <div class="col-md-3">
            
        </div>
    </div>
    <br />

    <input class="form-control input-sm" runat="server" id="txtqColunas" type="text" value="" style="display:none"/>

    <div class="container-fluid">

        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <b><i class="fa fa-filter"></i>&nbsp;Filtro</b><br />
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-md-2">
                            <span style="font-size:14px">Matrícula </span>&nbsp;<input type="checkbox" id="chkMatriculaAluno" runat="server" checked="checked" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtMatriculaAluno" type="number" value="" maxlength="18" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4">
                            <span style="font-size:14px">Nome </span>&nbsp;<input type="checkbox" id="chkNomeAluno" runat="server" checked="checked" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtNomeAluno" type="text" value="" maxlength="150" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">CPF </span>&nbsp;<input type="checkbox" id="chkCpfAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtCPFAluno" type="text" value="" maxlength="50" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">RG </span>&nbsp;<input type="checkbox" id="chkRgAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtRGAluno" type="text" value="" maxlength="20" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span style="font-size:14px">Telefone </span>&nbsp;<input type="checkbox" id="chkTelefoneAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtTelefoneAluno" type="text" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Celular </span>&nbsp;<input type="checkbox" id="chkCelularAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtCelularAluno" type="text" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span style="font-size:14px">Email </span>&nbsp;<input type="checkbox" id="chkEmailAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtEmailAluno" type="email" value="" maxlength="100" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span style="font-size:14px">Endereço </span>&nbsp;<input type="checkbox" id="chkEnderecoAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtEnderecoAluno" type="text" value="" maxlength="200" />
                        </div>
                    </div>
                    <br />

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2">
                                    <span>Tipo Curso</span>&nbsp;<input type="checkbox" id="chkTipoCursoAluno" runat="server" style="display:inline-block" /><br />
                                    <asp:DropDownList runat="server" ID="ddlTipoCursoAluno" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoAluno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-5">
                                    <span>Curso</span>&nbsp;<input type="checkbox" id="chkCursoAluno" runat="server" style="display:inline-block" /><br />
                                    <asp:DropDownList runat="server" ID="ddlNomeCursoAluno" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlNomeCursoAluno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2">
                                    <span>Turma</span>&nbsp;<input type="checkbox" id="chkTurmaAluno" runat="server" style="display:inline-block" /><small>&nbsp;&nbsp;(Início-Fim)</small>&nbsp;<input type="checkbox" id="chkInicioFimTurma" runat="server" style="display:inline-block" /><br />
                                    <asp:DropDownList runat="server" ID="ddlTurmaAluno" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlTurmaAluno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoAluno" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlNomeCursoAluno" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlTurmaAluno" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />

                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="col-md-4">
                                    <span>Oferecimento</span>&nbsp;<input type="checkbox" id="chkOferecimentoAluno" runat="server" style="display:inline-block" /><small>&nbsp;&nbsp;(Todos)</small>&nbsp;<input type="checkbox" id="chkTodosOferecimentos" runat="server" style="display:inline-block" /><br />
                                    <asp:DropDownList runat="server" ID="ddlOferecimentoAluno" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Formação </span>&nbsp;<input type="checkbox" id="chkFormacaoAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtFormacaoAluno" type="text" value="" maxlength="100" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Ano Formação </span>&nbsp;<input type="checkbox" id="chkAnoFormacaoAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtAnoFormacao" type="number" value="" maxlength="100" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span style="font-size:14px">Empresa </span>&nbsp;<input type="checkbox" id="chkEmpresaAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtEmpresaAluno" type="text" value="" maxlength="200" />
                        </div>
                    </div>

                    <br />

                            
                    <div class="row">
                        <div class="col-md-3">
                            <span style="font-size:14px">Cargo </span>&nbsp;<input type="checkbox" id="chkCargoAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtCargoAluno" type="text" value="" maxlength="200" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-3">
                            <span style="font-size:14px">Área de Concentração </span>&nbsp;<input type="checkbox" id="chkAreaConcentracaoAluno" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtAreaConcentracaoAluno" type="text" value="" maxlength="200" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Situação </span>&nbsp;<input type="checkbox" id="chkSituacaoAluno" runat="server" style="display:inline-block" />
                            <asp:DropDownList runat="server" ID="ddlSituacaoAluno" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa">
                                <asp:ListItem Text="Todas" Value="" />
                                <asp:ListItem Text="Cursando" Value="Cursando" />
                                <asp:ListItem Text="Não cursando" Value="Não cursando" />
                                <asp:ListItem Text="Matriculado" Value="Matriculado" />
                                <asp:ListItem Text="Qualificação" Value="Qualificação" />
                                <asp:ListItem Text="Titulado" Value="Titulado" />
                                <asp:ListItem Text="Prorrogação CPG" Value="Prorrogação CPG" />
                                <asp:ListItem Text="Trancado" Value="Trancado" />
                                <asp:ListItem Text="Trancamento Especial" Value="Trancamento Especial" />
                                <asp:ListItem Text="Desligado" Value="Desligado" />
                                <asp:ListItem Text="Abandonou" Value="Abandonou" />
                            </asp:DropDownList>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Tipo Matrícula </span>&nbsp;<input type="checkbox" id="chkTipoMatricula" runat="server" style="display:inline-block" />
                            <asp:DropDownList runat="server" ID="ddlTipoMatriculaAluno" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa">
                                <asp:ListItem Text="Todos" Value="" />
                                <asp:ListItem Text="Regular" Value="Regular" />
                                <asp:ListItem Text="Especial" Value="Especial" />
                            </asp:DropDownList>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button type="button" runat="server" id="bntPerquisaAluno" name="bntPerquisaAluno" onserverclick ="bntPerquisaAluno_Click" title="" class="btn btn-success pull-right hidden " href="#">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                            <a id="aBntPerquisaAluno" runat ="server" onclick="fProcessando()" onserverclick="bntPerquisaAluno_Click" href="#" class ="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
                        </div>

                    </div>
                    <br />

                    <div class="row" >
                        <div class="col-md-2">
                            <span style="font-size:14px">Orientador </span>&nbsp;<input type="checkbox" id="chkOrientador" runat="server" style="display:inline-block" />
                            <br />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span style="font-size:14px">Data Nascimento </span>&nbsp;<input type="checkbox" id="chkDatNascimento" runat="server" style="display:inline-block" />
                            <br />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

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
                                <asp:RadioButton GroupName="GrupoProvaProficienciaPortugues" ID="optProficienciaPortuguesTodos" runat="server" Checked="true"/>
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

                    </div>

                    <br />

                    <div class="row" >
                        <div class="col-md-4">
                            <span style="font-size:14px">Palavra-Chave </span>&nbsp;<input type="checkbox" id="chkPalavraChave" runat="server" style="display:inline-block" />
                            <input class="form-control input-sm alteracao" runat="server" id="txtPalavraChave" type="text" value="" maxlength="100" />
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
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="grdAluno_RowDataBound">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden notexport" HeaderStyle-CssClass="hidden notexport">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "nome")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" >
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="ShowProgress()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdAluno_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:BoundField DataField="idaluno" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />

                                            <asp:BoundField DataField="cpf" ItemStyle-CssClass ="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderText="CPF" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>

                                            <asp:TemplateField HeaderText="RG" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "numero_documento") != null ? (setRG(DataBinder.Eval(Container.DataItem, "numero_documento").ToString(), DataBinder.Eval(Container.DataItem, "digito_num_documento") == null ? "" : DataBinder.Eval(Container.DataItem, "digito_num_documento").ToString())) : "" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="telefone_res" HeaderText="Telefone" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="celular_res" HeaderText="Celular" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:TemplateField HeaderText="Endereço" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "logradouro_res") != null ? (setEndereco(DataBinder.Eval(Container.DataItem, "logradouro_res").ToString(), DataBinder.Eval(Container.DataItem, "numero_res").ToString(),DataBinder.Eval(Container.DataItem, "complemento_res").ToString(),DataBinder.Eval(Container.DataItem, "bairro_res").ToString(),DataBinder.Eval(Container.DataItem, "cidade_res").ToString(),DataBinder.Eval(Container.DataItem, "uf_res").ToString(),DataBinder.Eval(Container.DataItem, "pais_res") == null? "": DataBinder.Eval(Container.DataItem, "pais_res").ToString(),DataBinder.Eval(Container.DataItem, "cep_res").ToString())) : ""   %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Curso" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setTipoCurso(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Curso" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setCurso(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Turma" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="true" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setTurma(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

<%--                                            <asp:TemplateField HeaderText="Período" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setPeriodo(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Oferecimento" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setOferecimento(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--DataBinder.Eval(Container.DataItem, "matricula_turma")--%>
                                            
                                            <asp:BoundField DataField="formacao" HeaderText="Formação" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="ano_graduacao" HeaderText="Ano Formação" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="empresa" HeaderText="Empresa" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:BoundField DataField="cargo" HeaderText="Cargo" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                            <asp:TemplateField HeaderText="Área de Concentração" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setArea(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Situação" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setSituacao(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Matrícula" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setTipoMatricula(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Orientador" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#setOrientacao(DataBinder.Eval(Container.DataItem,"matricula_turma") )    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data Nascimento" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"data_nascimento")).ToString("dd/MM/yyyy")    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="palavra_chave" HeaderText="Palavra-Chave" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                                        </Columns>

                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                    </asp:GridView>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>

                <div class="panel-footer" id="divBotaoPreparaEmail" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <button type="button" class="btn btn-success pull-right" onclick="fAbreModalEmail()">
                                                        <i class="fa fa-envelope-o"></i>&nbsp;Preparar email</button>
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

    <!-- Modal Enviar Email -->
    <div class="modal fade" id="divModalEnviarEmail" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-blue">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-envelope"></i>&nbsp;&nbsp;E-Mail</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <span >De</span><br />
                                <input class="form-control input-sm" runat="server" id="txtDeEmail" type="text" readonly="readonly" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span >Para</span><br />
                                <textarea style="resize: none;font-size:14px" runat="server" class="form-control input-sm" rows="3" id="txtParaEmail" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span >Cc</span><br />
                                <textarea style="resize: none;font-size:14px" runat="server" class="form-control input-sm" rows="2" id="txtCcEmail"></textarea>
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

                        <div class="row ">
                            <div class="col-md-12">
                                <span>Corpo do Email</span><br />
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
                            <button type="button" runat="server" id="btnEnviarEmail" class="btn btn-primary pull-right hidden" onclick="if (fProcessando()) return;" onserverclick="btnEnviarEmail_Click">
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                            <button type="button" class="btn btn-primary pull-right" onclick="fEnviaEmail()" >
                            <i class="fa fa-send"></i>&nbsp;Enviar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
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

        function fEnviaEmail() {
            var sAux = "";

            if (document.getElementById('<%=txtAssuntoEmail.ClientID%>').value.trim() == "") {
                sAux = "Deve-se digitar um Assunto do Email <br><br>";
            }

            var sTemp;
            sTemp = replaceAll("&nbsp;", "", $('#txtCorpoEmail').code());

            if ($('#txtCorpoEmail').code().trim() == "" ) {
                sAux = sAux + "Deve-se digitar um Texto no corpo do Email ";
            }
            else if (sTemp.trim() == "") {
                sAux = sAux + "Deve-se digitar um Texto no corpo do Email ";
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

            document.getElementById('hCodigoAluno').value = $('#txtCorpoEmail').code();

            document.getElementById('<%=btnEnviarEmail.ClientID%>').click();

        }

        function fEscondeColunas() {
            var aColunas = document.getElementById('<%=txtqColunas.ClientID%>').value;
            var qColunas = aColunas.split(";");
            var tbl = $('#<%=grdAluno.ClientID%>');

            for (var i = 0; i < qColunas.length; i++) {
                //alert('i: ' + i + " qColunas[i]: " + qColunas[i]);
                tbl.DataTable().column(qColunas[i]).visible(false);
                //tbl.fnSetColumnVis(qColunas[i], false);
            }
            
        }

        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
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

            $(".select_multiplo").select2({
                multiple: true
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

            $('#<%=txtTelefoneAluno.ClientID%>').mask('99-9999-9999');
            $('#<%=txtCelularAluno.ClientID%>').mask('99-99999-9999');

            $('#<%=grdAluno.ClientID%>').dataTable({
                stateSave: false,
                "initComplete": function(settings, json) {
                    //alert('DataTables has finished its initialisation.');
                    fEscondeColunas();
                },
                "bProcessing": true,
                "columns": [
                    { "orderable": false }, //Ordenação
                    { width: "10px" },  //Matrícula
                    { width: "50px" },  //Nome
                    { width: "10px" },  //CPF
                    { width: "10px" },  //RG
                    { width: "10px" },  //Telefone
                    { width: "10px" },  //Celular
                    { width: "15px" },  //Email
                    { width: "50px" },  //Endereço
                    { width: "10px" },  //Tipo Curso
                    { width: "50px" },  //Curso
                    { width: "10px" },  //Turma
                    { width: "50px" },  //Oferecimento
                    { width: "20px" },  //Formação
                    { width: "10px" },  //Ano - Formação
                    { width: "20px" },  //Empresa
                    { width: "20px" },  //Cargo
                    { width: "20px" },  //Area
                    { width: "20px" },  //Situação
                    { width: "10px" },   //Tipo Matrícula
                    { width: "50px" },   //Orientação
                    { width: "50px", type: 'date-euro'},   //Data Nascimento
                    { width: "50px" }   //Palavra-Chave

                ],
                dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                buttons: [
                {
                    extend: 'pdf',
                    exportOptions: {
                        columns: ':not(.notexport)',
                        format: {
                            body: function (data, row, column, node) {
                                var newdata = data;
                                newdata = replaceAll('<hr>', '\n', newdata);
                                newdata = replaceAll('<span style="line-height: 2.2em;">', '', newdata);
                                newdata = replaceAll('</span>', '', newdata);
                                return newdata;
                            }
                        }
                    },
                    orientation: 'landscape',
                    title: function () {
                        //var qPulaLinha = "\n";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Aluno";
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
                        if (document.getElementById("<%=txtTelefoneAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Telefone: " + document.getElementById("<%=txtTelefoneAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtCelularAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Celular: " + document.getElementById("<%=txtCelularAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEmailAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Email: " + document.getElementById("<%=txtEmailAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEnderecoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Endereço: " + document.getElementById("<%=txtEnderecoAluno.ClientID%>").value;
                        }
                        if ($("#<%=ddlTipoCursoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Tipo Curso: " + $("#<%=ddlTipoCursoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlNomeCursoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + $("#<%=ddlNomeCursoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlTurmaAluno.ClientID%> option:selected").val() != "" ) {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Turma: " + $("#<%=ddlTurmaAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlOferecimentoAluno.ClientID%> option:selected").val() != "" && $("#<%=ddlOferecimentoAluno.ClientID%> option:selected").text() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Oferecimento: " + $("#<%=ddlOferecimentoAluno.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=txtFormacaoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Formação: " + document.getElementById("<%=txtFormacaoAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtAnoFormacao.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Ano Formação: " + document.getElementById("<%=txtAnoFormacao.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEmpresaAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Empresa: " + document.getElementById("<%=txtEmpresaAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtCargoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Cargo: " + document.getElementById("<%=txtCargoAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtAreaConcentracaoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Área de Concentração: " + document.getElementById("<%=txtAreaConcentracaoAluno.ClientID%>").value;
                        }
                        if ($("#<%=ddlSituacaoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Situação: " + $("#<%=ddlSituacaoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlTipoMatriculaAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Tipo Matrícula: " + $("#<%=ddlTipoMatriculaAluno.ClientID%> option:selected").text();
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
                        columns: ':not(.notexport)',
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
                        //var qPulaLinha = "<br/>";
                        var qPulaLinha = " - ";
                        var fRetornoFiltro = "Relatório de Aluno";
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
                        if (document.getElementById("<%=txtTelefoneAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Telefone: " + document.getElementById("<%=txtTelefoneAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtCelularAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Celular: " + document.getElementById("<%=txtCelularAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEmailAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Email: " + document.getElementById("<%=txtEmailAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEnderecoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Endereço: " + document.getElementById("<%=txtEnderecoAluno.ClientID%>").value;
                        }
                        if ($("#<%=ddlTipoCursoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Tipo Curso: " + $("#<%=ddlTipoCursoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlNomeCursoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + $("#<%=ddlNomeCursoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlTurmaAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Turma: " + $("#<%=ddlTurmaAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlOferecimentoAluno.ClientID%> option:selected").val() != "" && $("#<%=ddlOferecimentoAluno.ClientID%> option:selected").text() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Oferecimento: " + $("#<%=ddlOferecimentoAluno.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=txtFormacaoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Formação: " + document.getElementById("<%=txtFormacaoAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtAnoFormacao.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Ano Formação: " + document.getElementById("<%=txtAnoFormacao.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEmpresaAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Empresa: " + document.getElementById("<%=txtEmpresaAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtCargoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Cargo: " + document.getElementById("<%=txtCargoAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtAreaConcentracaoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Área de Concentração: " + document.getElementById("<%=txtAreaConcentracaoAluno.ClientID%>").value;
                        }
                        if ($("#<%=ddlSituacaoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Situação: " + $("#<%=ddlSituacaoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlTipoMatriculaAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Tipo Matrícula: " + $("#<%=ddlTipoMatriculaAluno.ClientID%> option:selected").text();
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
                        columns: ':not(.notexport)',
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
                        var fRetornoFiltro = "Relatório de Aluno";
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
                        if (document.getElementById("<%=txtTelefoneAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Telefone: " + document.getElementById("<%=txtTelefoneAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtCelularAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Celular: " + document.getElementById("<%=txtCelularAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEmailAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Email: " + document.getElementById("<%=txtEmailAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEnderecoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Endereço: " + document.getElementById("<%=txtEnderecoAluno.ClientID%>").value;
                        }
                        if ($("#<%=ddlTipoCursoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Tipo Curso: " + $("#<%=ddlTipoCursoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlNomeCursoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Curso: " + $("#<%=ddlNomeCursoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlTurmaAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Turma: " + $("#<%=ddlTurmaAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlOferecimentoAluno.ClientID%> option:selected").val() != "" && $("#<%=ddlOferecimentoAluno.ClientID%> option:selected").text() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Oferecimento: " + $("#<%=ddlOferecimentoAluno.ClientID%> option:selected").text();
                        }
                        if (document.getElementById("<%=txtFormacaoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Formação: " + document.getElementById("<%=txtFormacaoAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtAnoFormacao.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Ano Formação: " + document.getElementById("<%=txtAnoFormacao.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtEmpresaAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Empresa: " + document.getElementById("<%=txtEmpresaAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtCargoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Cargo: " + document.getElementById("<%=txtCargoAluno.ClientID%>").value;
                        }
                        if (document.getElementById("<%=txtAreaConcentracaoAluno.ClientID%>").value != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Área de Concentração: " + document.getElementById("<%=txtAreaConcentracaoAluno.ClientID%>").value;
                        }
                        if ($("#<%=ddlSituacaoAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Situação: " + $("#<%=ddlSituacaoAluno.ClientID%> option:selected").text();
                        }
                        if ($("#<%=ddlTipoMatriculaAluno.ClientID%> option:selected").val() != "") {
                            fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Tipo Matrícula: " + $("#<%=ddlTipoMatriculaAluno.ClientID%> option:selected").text();
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

            $('#<%=chkMatriculaAluno.ClientID%>, #<%=chkNomeAluno.ClientID%>').iCheck('disable');

            $("#<%=chkInicioFimTurma.ClientID%>").on('ifChecked', function (e) {
                $('#<%=chkTurmaAluno.ClientID%>').iCheck('check'); //To check the radio button
            });

            $("#<%=chkTodosOferecimentos.ClientID%>").on('ifChecked', function (e) {
                $('#<%=chkOferecimentoAluno.ClientID%>').iCheck('check'); //To check the radio button
            });

            $("#<%=chkTurmaAluno.ClientID%>").on('ifUnchecked', function (e) {
                $('#<%=chkInicioFimTurma.ClientID%>').iCheck('uncheck'); //To check the radio button
            });

            $("#<%=chkOferecimentoAluno.ClientID%>").on('ifUnchecked', function (e) {
                $('#<%=chkTodosOferecimentos.ClientID%>').iCheck('uncheck'); //To check the radio button
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
                if (!$('#divModalEnviarEmail').is(':visible')) {
                    document.getElementById("<%=aBntPerquisaAluno.ClientID%>").click();
                }

                
                
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



        $(".alteracao").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });

        $('.ddl_fecha_grid_resultados').on('select2:select', function (e) {
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


        // =========================================================================================================

        function fAbreModalEmail() {
            //$('#divModalEnviarEmail').modal();
            var sAux = "";
            var table2 = $('#<%=grdAluno.ClientID %>').DataTable();
            table2.cells().every(function (row, col) {
                //alert('row: ' + row );
                //alert('col: ' + col );
                
                if (col == 7) {
                    //alert('Novamente: ' + this.data() );
                    if (sAux != "") {
                        sAux = sAux + ";"
                    }
                    sAux = sAux + this.data();
                }
            });
            //document.getElementById('<%=txtDeEmail.ClientID %>').value = 'mestrado@ipt.br';
            document.getElementById('<%=txtParaEmail.ClientID %>').value = sAux;
            document.getElementById('<%=txtCcEmail.ClientID %>').value = document.getElementById('<%=txtCcEmailHidden.ClientID %>').value;
            document.getElementById('<%=txtAssuntoEmail.ClientID %>').value = "";
            //alert($('#txtCorpoEmail').code());
            $('#txtCorpoEmail').code('');
            //alert($('#txtCorpoEmail').code());
            $('#divModalEnviarEmail').modal();
        }

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
