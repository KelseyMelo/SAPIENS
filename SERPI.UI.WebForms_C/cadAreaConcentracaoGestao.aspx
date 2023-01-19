<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadAreaConcentracaoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadAreaConcentracaoGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li5AreaConcentracao" />

    <script src="Scripts/jquery.mask.min.js"></script>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
    
    <input type="hidden" id ="hCodigo"  name="hCodigo" value="" />
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

        .select2 {
            width: 100% !important;
        }

        label.opt {
            cursor: pointer;
        }

        caption {
            color: white;
            background-color: #507CD1;
            font-weight: bold;
            text-align: center;
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

            input[type=checkbox] {
                transform: scale(0.8);
            }

            input[type=checkbox] {
                width: 20px;
                height: 20px;
                margin-right: 8px;
                cursor: pointer;
                font-size: 18px; /*Tamanho do check interno*/
                visibility: hidden;
                margin-top:-12px;
            }

            input[type=checkbox]:hover:after {
                border-color: #0E76A8;
            }

            input[type=checkbox]:after {
                content: " ";
                background-color: #fff;
                display: inline-block;
                margin-left: 10px;
                padding-bottom: 5px;
                color: #0E76A8;
                width: 22px;
                height: 25px;
                visibility: visible;
                border: 1px solid #D2D6DE;
                padding-left: 3px;
                
                /*border-radius: 5px;*/
            }

            input[type=checkbox]:checked:after {
                content: "\2714";
                padding: -5px;
                font-weight: bold;
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
    <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel2"  >
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
        <div class="col-md-4">
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Área de Concentração</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label><asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label></h3>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-3 ">
            <br />
            <button type="button" runat="server" id="btnInativar" class="btn btn-danger" href="#" onclick="fModalAtivaInativa('Inativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-off"></i> Inativar Área de Concentração
            </button>
            <button type="button" runat="server" id="btnAtivar" class="btn btn-info" href="#" onclick="fModalAtivaInativa('Ativa')"> <%--onserverclick="btnNovoAluno_Click"--%>
                <i class="fa fa-toggle-on"></i> Ativar Área de Concentração
            </button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-3">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarArea" name="btnCriarArea" onserverclick="btnCriarArea_Click" class="btn btn-primary" href="#" onclick=""  > <%--onserverclick="btnCriarArea_Click"--%>
                        <i class="fa fa-magic"></i>&nbsp;Criar Área de Concentração</button>
            </div>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <a id="A1" runat="server" onclick="ShowProgress()" onserverclick="btnSalvar_ServerClick1" href="#" class ="btn btn-success pull-right hidden"><i class="fa fa-save"></i><span>&nbsp;&nbsp;Salvar dados</span></a> <%--onserverclick="bntPerquisaAluno_Click"--%>
                <button type="button" class="btn btn-success pull-right" onclick="fbtnSalvar()">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Salvar dados</button>
        </div>

    </div>
    <br />

    <div class="container-fluid">
        <div class="tab-content">

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">

                                    <div runat="server" id="divEdicao">
                                    <div class="row">
                                        <div class="col-md-3 ">
                                            <span>Data de Cadastro</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataCadastro" type="text" readonly="true"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3 ">
                                            <span>Status</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtStatus" type="text" readonly="true"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3 ">
                                            <span>Última Alteração</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtDataAlteracao" type="text" readonly="true"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3 ">
                                            <span>Responsável</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtResponsavel" type="text" readonly="true"/>
                                        </div>

                                    </div>
                                    <br />
                                    </div>

                                    <div class="row">
                                        <div class="col-md-5 ">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtNomeArea" type="text" value="" maxlength="250"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2 ">
                                            <span>N.º Eletivas</span><br />
                                            <input class="form-control input-sm" runat="server" id="txtNumeroEletivasArea" type="number" value="" min="0"/>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            &nbsp;<br />
                                            <asp:CheckBox ID="chkDisponivelArea" runat="server"/>
                                            &nbsp;
                                            <label style="font-weight:normal" class="opt" for="<%=chkDisponivelArea.ClientID %>">Disponível p/ Inscrição</label>
                                        </div>
                                    </div>
                                        

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="tab-content" id="divCoordenadores" runat ="server">

            <div class="panel panel-default">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="box-title text-bold">Coordenadores</h5>
                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="row">
                                            <div class="col-md-12">
                                                <div class="grid-content">
                                                 
                                                    <div id="msgSemResultadosCoordenador" style="display:block">
                                                        <div class="alert bg-gray"> 
                                                            <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Coordenador associado." />
                                                        </div>
                                                    </div>
                                                    <div id="divgrdCoordenador" class="table-responsive" style="display:none">
                                                        <div class="scroll">
                                                            <table id="grdCoordenador" class="table table-striped table-bordered table-condensed" role="grid" width="100%" >
                                                                <thead style="color:White;background-color:#507CD1;font-weight:bold;">
                                                                    <tr>
                                                                        <th>id</th>
                                                                        <th>CPF</th>
                                                                        <th>Nome</th>
                                                                        <th>Excluir</th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="col-md-3 pull-right">
                                                        <button type="button" id="btnAssociarCoordenador" name="btnAssociarCoordenador" class="btn btn-warning pull-right" href="#" onclick="fModalAssociarCoordenador()">
                                                            <i class="fa fa-plus"></i>&nbsp;Incluir Coordenador</button>
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
        </div>

        <div class="tab-content">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="box-title text-bold">Curso Associado</h5>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="tab-content">
                                            <div class ="row">
                                                <div class="col-md-8">
                                                    <%--<span>Curso</span><br />--%>
                                                    <asp:DropDownList runat="server" ID="ddlCodigoCursoArea" onchange="fMostrarProgresso()" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCodigoCursoArea_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<div class="hidden-lg hidden-md">
                                                    <br />
                                                </div>

                                                <div class="col-md-6">
                                                    <span>Nome Curso</span><br />
                                                    <asp:DropDownList runat="server" ID="ddlNomeCursoArea" onchange="fMostrarProgresso()" CausesValidation="false" ClientIDMode="Static" class="form-control input-sm " AutoPostBack="true" OnSelectedIndexChanged="ddlNomeCursoArea_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>--%>
                            
                                            </div>
                                            <br />
                                            <hr />
                                            <br />

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="grid-content">
                                                        <div runat="server" id="msgSemResultadosDisciplina" visible="false">
                                                            <div class="alert bg-gray">
                                                                <asp:Label runat="server" ID="Label1" Text="Nenhuma disciplina associada ao curso" />
                                                            </div>
                                                        </div>
                                                        <div class="table-responsive ">

                                                            <asp:GridView ID="grdDisciplinas" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                AllowPaging="True" PageSize="1000000" AllowSorting="true"
                                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" Caption="RELAÇÃO DE DISCIPLINAS DO CURSO">
                                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                                <Columns>

                                                                    <asp:BoundField DataField="id_disciplina" HeaderText="id_disciplina" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="" HeaderStyle-CssClass="" />

                                                                    <asp:BoundField DataField="codigo" HeaderText="Código" ItemStyle-HorizontalAlign="Left" />

                                                                    <asp:BoundField DataField="nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />

                                                                    <%--<asp:BoundField DataField="DisciplinaCodigo" HeaderText="Disciplina" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />--%>

                                                                    <asp:templatefield HeaderText="Associar à A. C." ItemStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" >
                                                                        <itemtemplate>
                                                                            <asp:checkbox OnClick="fCheckAssociar(this)" Checked='<%# DataBinder.Eval(Container.DataItem, "associado").ToString() == "1" ? true : false%>' ID="chkAssociar" CssClass='checkbox text-center' runat="server"></asp:checkbox>
                                                                        </itemtemplate>
                                                                    </asp:templatefield>

                                                                    <asp:templatefield HeaderText="Obrigatória" ItemStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" >
                                                                        <itemtemplate>
                                                                            <asp:checkbox OnClick="fCheckObrigatoria(this)" Checked='<%# DataBinder.Eval(Container.DataItem, "obrigatoria").ToString() == "1" ? true : false%>'  ID="chkObrigatoria" CssClass="checkbox text-center" runat="server"></asp:checkbox>
                                                                        </itemtemplate>
                                                                    </asp:templatefield>
                                                                

                                                                </Columns>

                                                            </asp:GridView>
                                                                                       
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCodigoCursoArea" EventName="SelectedIndexChanged" />
                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlNomeCursoArea" EventName="SelectedIndexChanged" />--%>
                                    </Triggers>
                            </asp:UpdatePanel>
                            </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-2">
                <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

            <div class="col-xs-2 pull-right">
                <a id="btnSalvar" runat="server" onclick="ShowProgress()" onserverclick="btnSalvar_ServerClick1" href="#" class ="btn btn-success pull-right hidden"><i class="fa fa-save"></i><span>&nbsp;&nbsp;Salvar dados</span></a> <%--onserverclick="bntPerquisaAluno_Click"--%>
                <button type="button" class="btn btn-success pull-right" onclick="fbtnSalvar()">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Salvar dados</button>
            </div>
        </div>
    </div>

    <!-- Modal para Associar Coordenador -->
    <div class="modal fade" id="divModalAssociarCoordenador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Associar Coordenador</h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <b><i class="fa fa-filter"></i>&nbsp;Filtro</b><br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                    
                                        <div class="col-md-2">
                                            <span>CPF</span><br />
                                            <input class="form-control input-sm" id="txtCPF" type="text" value="" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-8">
                                            <span>Nome</span><br />
                                            <input class="form-control input-sm" id="txtNomeCoordenador" type="text" value="" maxlength="70" />
                                        </div>
                                        <div class="hidden-lg hidden-md"> 
                                            <br />
                                        </div>

                                        <div class="col-md-1">
                                            <div class="hidden-xs hidden-sm">
                                                <br />
                                            </div>

                                            <button id="bntPerquisaCoordenador" type="button" name="bntPerquisaCoordenador" title="" class="btn btn-success" onclick="fPerquisaCoordenador()" >
                                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>
                                        </div>

                                    </div>
                                    <br />
                                </div>

                            </div>

                        </div>
                        <br />

                        <div class="row">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="grid-content">
                                            <div id="msgSemResultadosgrdCoordenadorDisponivel" style="display:none">
                                                <div class="alert bg-gray">
                                                    <asp:Label runat="server" ID="Label2" Text="Nenhum Coordenador encontrado" />
                                                </div>
                                            </div>
                                            <div class="table-responsive" id="divgrdCoordenadorDisponivel" >
                                                <div class="scroll">
                                                    <table id="grdCoordenadorDisponivel" class="table table-striped table-bordered table-condensed" role="grid" width="100%">
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

    <!-- Modal para Excluir Coordenador -->
    <div class="modal fade" id="divModalExcluirCoordenador" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i class="fa fa-eraser"></i> Exclusão de Coordenador</h4>
                </div>
                <div class="modal-body">
                    Deseja excluir o Coordenador: <label id="lblNomeCoordenador"></label> - CPF: <label id="lblCPFCoordenador"></label>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-3 pull-right">
                            <button id="bntExcluirCoordenador" type="button" name="bntExcluirCoordenador" title="" class="btn btn-success" onclick="fExcluiCoordenador()" >
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;Confirmar</button>
                        </div>
                        <div class="hidden-md hidden-lg">
                            <br />
                        </div>

                        <div class="col-md-2 pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    <i class="fa fa-close"></i>&nbsp;Fechar</button>
                        </div>
                    </div>



                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal para Ativar/Inativar Área de Concentração -->
    <div class="modal fade" id="divModalAtivaInativa" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="divCabecAtiva" class="modal-header bg-danger">
                    <h4 class="modal-title"><label id="lblTituloAtiva"></label></h4>
                </div>
                <div class="modal-body">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <label id="lblCorpoAtiva"></label>
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarArea('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarArea('Inativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
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
        var vIdDisciplina;
        var vAssociar;
        var vObrigatoria;
        var vhCodigo;


        var vRowIndex_grdCoordenador;
        $('#txtCPF').mask('999.999.999-99');


        $(document).ready(function () {
            fAtiva_grdDisciplina();
            fPreencheCoordenador();
            fSelect2();
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
        }

        function fAtiva_grdDisciplina() {
             if ( ! $.fn.DataTable.isDataTable( '#<%=grdDisciplinas.ClientID%>' ) ) {
                $('#<%=grdDisciplinas.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });
            }
        }

        function fbtnSalvar() {
            //alert("Clicou");
           
            var table2 = $('#<%=grdDisciplinas.ClientID%>').DataTable();

            var data = $('#<%=grdDisciplinas.ClientID%>').DataTable().$('input,select,textarea').serialize();
            var data2 = replaceAll("%24", "$", data);

            //alert("data: " + data);

            //return;
            vhCodigo = "";

            document.getElementById('hCodigo').value = "";

            table2.cells().every(function (row, col) {
                //alert('row: ' + row + ' col: ' + col );
                //alert('col: ' + col );
                //if (row == 0 && col == 0) {
                //    vhCodigo = "";
                //    alert('Zerou o vhCodigo na row: ' + row + ' col: ' + col);
                //}
                if (col == 0) {
                    vIdDisciplina = this.data();
                    vAssociar = "0";
                    //alert('vIdDisciplina: ' + vIdDisciplina);
                }
                if (col == 3) {
                    var itens = this.data().split("=");
                    //alert("Entrou na 3" + itens[2]);
                    //alert("this.data(): " + this.data() + "\r\n" + "itens: " + itens[4] + "\r\n" + "data2: " + data2);
                    itens[4] = itens[4].replace("checked", "").trim();
                    itens[4] = itens[4].replace("onclick", "").trim();
                    itens[4] = itens[4].replace('"', '');
                    itens[4] = itens[4].replace('"', '');
                    //alert("this.data(): " + this.data() + "\r\n" + "itens: " + itens[4] + "\r\n" + "data2: " + data2);


                    if (data2.indexOf(itens[4]) != -1) {
                        vAssociar = "1";
                    }
                    else {
                        vAssociar = "0";
                    }

                    //alert("vAssociar: " + vAssociar);

                }
                if (col == 4 && vAssociar == "1") {
                    var itens = this.data().split("=");
                    //alert("this.data(): " + this.data() + "\r\n" + "itens: " + itens[4] + "\r\n" + "data2: " + data2);
                    itens[4] = itens[4].replace("checked", "").trim();
                    itens[4] = itens[4].replace("onclick", "").trim();
                    itens[4] = itens[4].replace('"', '');
                    itens[4] = itens[4].replace('"', '');
                    //alert("this.data(): " + this.data() + "\r\n" + "itens: " + itens[4] + "\r\n" + "data2: " + data2);
                    if (data2.indexOf(itens[4]) != -1) {
                        vObrigatoria = "1";
                        //alert("Associou vObrigatoria:" + data2.indexOf(itens[4]));
                    }
                    else {
                        vObrigatoria = "0";
                        //alert("NÃO Associou vObrigatoria:" + data2.indexOf(itens[4]));
                    }

                    if (vAssociar == "1") {
                        //alert('Disciplina: ' + vIdDisciplina);
                        //alert('vAssociar: ' + vAssociar);
                        //alert('vObrigatoria: ' + vObrigatoria);

                        if (vhCodigo != "") {
                            vhCodigo = vhCodigo + ";"
                        }
                        vhCodigo = vhCodigo + vIdDisciplina + ";" + vAssociar + ";" + vObrigatoria;
                        document.getElementById('hCodigo').value = vhCodigo;
                        //alert("vhCodigo: " + vhCodigo);
                    }
                }
            });
            //alert("vhCodigo: " + vhCodigo);
            document.getElementById('<%=btnSalvar.ClientID%>').click();
        }

        function fMostrarProgresso()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function fMostrarProgresso()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function fCheckObrigatoria(obj) {
            if (obj.checked) {
                var id = obj.getAttribute("id").split("_");
                document.getElementById("ContentPlaceHolderBody_grdDisciplinas_chkAssociar_" + id[3]).checked = true;
            }
        }

        function fCheckAssociar(obj) {
            if (!obj.checked) {
                var id = obj.getAttribute("id").split("_");
                document.getElementById("ContentPlaceHolderBody_grdDisciplinas_chkObrigatoria_" + id[3]).checked = false;
            }
        }

        function teclaEnter() {
            if (event.keyCode == "13") {

                if ($('#divModalAssociarCoordenador').is(':visible')) {
                    document.getElementById("bntPerquisaCoordenador").click();
                }
                
                else {
                    //alert('não');
                }

            }
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;

        function fModalAssociarCoordenador() {
            document.getElementById("divgrdCoordenadorDisponivel").style.display = "none";
            $('#divModalAssociarCoordenador').modal();
        }

        function AbreModalApagarCoordenador(qId, qCPF, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeCoordenador').innerHTML = qNome;
            document.getElementById('lblCPFCoordenador').innerHTML = qCPF;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirCoordenador').modal();
        }

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

        //================================================================================

        function fPreencheCoordenador() {
            var dt = $('#grdCoordenador').DataTable({
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
                        

                    if(oSettings.fnRecordsTotal() == 0){
                        document.getElementById("divgrdCoordenador").style.display = "none";
                        document.getElementById("msgSemResultadosCoordenador").style.display = "block";
                    }
                    else {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        }
                        else if (json[0].P0 == "Erro")
                        {
                            document.getElementById("divgrdCoordenador").style.display = "none";
                            document.getElementById("msgSemResultadosCoordenador").style.display = "block";
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
                            document.getElementById("divgrdCoordenador").style.display = "block";
                            document.getElementById("msgSemResultadosCoordenador").style.display = "none";

                            var table_grdCoordenador = $('#grdCoordenador').DataTable();

                            $('#grdCoordenador').on("click", "tr", function(){
                                vRowIndex_grdCoordenador = table_grdCoordenador.row(this).index()
                            });
                        }
                    }
                },
                ajax: {
                    url: "wsSapiens.asmx/fPreencheCoordenador",
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
                        "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                    },
                    {
                        "data": "P1", "title": "CPF", "orderable": true, "className": "text-center"
                    },
                    {
                        "data": "P2", "title": "Nome", "orderable": true, "className": "text-left"
                    },
                    {
                        "data": "P3", "title": "Excluir", "orderable": false, "className": "text-center"
                    }
                ],
                order: [[1, 'asc']],
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


        //================================================================================

        function fPerquisaCoordenador() {
            fProcessando();
            try {
                var qCPF = document.getElementById('txtCPF').value;
                var qNome = document.getElementById('txtNomeCoordenador').value;
                var dt = $('#grdCoordenadorDisponivel').DataTable({
                    processing: true,
                    serverSide: false,
                    destroy: true,
                    async: false,
                    searching: true, //Pesquisar
                    bPaginate: true, // Paginação
                    bInfo: true, //mostrando 1 de x registros
                    fnInitComplete: function (oSettings, json) {
                        //CallBackReq(oSettings.fnRecordsTotal());
                        //alert('total de registros: ' + oSettings.fnRecordsTotal()); //Total de registos
                        //for (var i = 0; i < oSettings.fnRecordsTotal(); i++) {   //Cada Registro
                        //    alert(json[i].Item);
                        //} 
                        //alert('Retorno json: ' + json);
                        

                        if(oSettings.fnRecordsTotal() == 0){
                            document.getElementById("divgrdCoordenadorDisponivel").style.display = "none";
                            document.getElementById("msgSemResultadosgrdCoordenadorDisponivel").style.display = "block";
                        }
                        else {
                            if(json[0].P0 == "deslogado" ){
                                window.location.href = "index.html";
                            }
                            else if (json[0].P0 == "Erro")
                            {
                                document.getElementById("divgrdCoordenadorDisponivel").style.display = "none";
                                document.getElementById("msgSemResultadosgrdCoordenadorDisponivel").style.display = "block";
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
                                document.getElementById("divgrdCoordenadorDisponivel").style.display = "block";
                                document.getElementById("msgSemResultadosgrdCoordenadorDisponivel").style.display = "none";

                                var table_grdCoordenadorDisponivel = $('#grdCoordenadorDisponivel').DataTable();

                                $('#grdCoordenadorDisponivel').on("click", "tr", function () {
                                    vRowIndex_grdCoordenadorDisponivel = table_grdCoordenadorDisponivel.row(this).index()
                                });
                            }
                        }
                        fFechaProcessando();
                    },
                    ajax: {
                        url: "wsSapiens.asmx/fPerquisaCoordenador?qCPF=" + qCPF + "&qNome=" + qNome,
                        "type": "POST",
                        "dataSrc": "",
                    },
                    columns: [
                        {
                            "data": "P0", "title": "id", "orderable": false, "className": "hidden"
                        },
                        {
                            "data": "P1", "title": "Nome", "orderable": true, "className": "text-left"
                        },
                        {
                            "data": "P2", "title": "CPF", "orderable": true, "className": "text-center"
                        },
                        {
                            "data": "P3", "title": "Adicionar", "orderable": false, "className": "text-center"
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
                fFechaProcessando();
            }
        }

        //=======================================

        function fIncluiCoordenador(qId, qCPF, qNome) {
            
            $.ajax({
                type: "POST",
                url: "wsSapiens.asmx/fIncluiCoordenador?qId=" + qId,
                dataType: "json",
                success: function(json)
                {
                    if(json[0].P0 == "deslogado" ){
                        window.location.href = "index.html";
                    } else if (json[0].P0 == "Erro") {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na inclusão do Coordenador: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                        $("#divCabecalho").removeClass("alert-success");
                        $("#divCabecalho").removeClass("alert-warning");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();
                    }
                    else {
                        fPreencheCoordenador();
                        //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Inclusão de Coordenador';
                        //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.';
                        //$("#divCabecalho").removeClass("alert-warning");
                        //$("#divCabecalho").removeClass("alert-danger");
                        //$('#divCabecalho').addClass('alert-success');
                        //$('#divMensagemModal').modal();
                        $('#grdCoordenadorDisponivel').DataTable().row(vRowIndex_grdCoordenadorDisponivel).remove().draw();

                        $.notify({
                            icon: 'fa fa-thumbs-o-up fa-lg',
                            title: '<strong>Inclusão de Coordenador</strong><br /><br />',
                            message: 'Inclusão do Coordenador <strong>' + qNome + '</strong> realizado com sucesso.<br />',

                        }, {
                            type: 'success',
                            delay: 1500,
                            timer: 1500,
                            z_index: 5000,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });
                        
                    }
                },
                error: function(xhr){
                    alert("Houve um erro na inclusão do Coordenador. Por favor tente novamente.");
                    alert(xhr.statusText + ' - ' + xhr.responseText);
                    fFechaProcessando()
                },
                failure: function () 
                {
                    alert("Houve um erro na inclusão do Coordenador. Por favor tente novamente!");
                    fFechaProcessando()
                }
            });
        }


        //=======================================

        function fExcluiCoordenador() {
            fProcessando();
            try {
                    $.ajax({
                    type: "POST",
                    url: "wsSapiens.asmx/fExcluiCoordenador?qId=" + document.getElementById('hCodigo').value,
                    dataType: "json",
                    success: function(json)
                    {
                        if(json[0].P0 == "deslogado" ){
                            window.location.href = "index.html";
                        } else if (json[0].P0 == "Erro") {
                            document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Coordenador';
                            document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Houve um erro na exclusão do Coordenador: ' + qNome + '. <br /><br /><strong>Erro: </strong>' + json[0].P1;
                            $("#divCabecalho").removeClass("alert-success");
                            $("#divCabecalho").removeClass("alert-warning");
                            $('#divCabecalho').addClass('alert-danger');
                            $('#divMensagemModal').modal();
                        }
                        else {
                            //fPreencheCoordenador();
                            $('#grdCoordenador').DataTable().row(vRowIndex_grdCoordenador).remove().draw();

                            $.notify({
                                icon: 'fa fa-thumbs-o-up fa-2x',
                                title: '<strong>  Exclusão de Coordenador</strong><br /><br />',
                                message: 'Exclusão do Coordenador realizado com sucesso.<br />',

                            }, {
                                type: 'danger',
                                delay: 1500,
                                timer: 1500,
                                z_index: 5000,
                                animate: {
                                    enter: 'animated flipInY',
                                    exit: 'animated flipOutX'
                                },
                                placement: {
                                    from: "top",
                                    align: "center"
                                }
                            });

                            //document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Exclusão de Coordenador';
                            //document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Exclusão do Coordenador realizado com sucesso.';
                            //$("#divCabecalho").removeClass("alert-warning");
                            //$("#divCabecalho").removeClass("alert-danger");
                            //$('#divCabecalho').addClass('alert-primary');
                            //$('#divMensagemModal').modal();
                        
                        }
                        fFechaProcessando();
                        $('#divModalExcluirCoordenador').modal('hide');
                    },
                    error: function(xhr){
                        alert("Houve um erro na exclusão do Coordenador. Por favor tente novamente.");
                        alert(xhr.statusText + ' - ' + xhr.responseText);
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando()
                    },
                    failure: function () 
                    {
                        alert("Houve um erro na exclusão do Coordenador. Por favor tente novamente!");
                        $('#divModalExcluirCoordenador').hide();
                        fFechaProcessando()
                    }
                });
            } catch (e) {
                fFechaProcessando()
            }
        }

        //===========================================================================================

        function fModalAtivaInativa(qOperacao) {
            if (qOperacao == 'Ativa') {
                $("#divCabecAtiva").removeClass("bg-danger");
                $('#divCabecAtiva').addClass('bg-info');
                document.getElementById("btnConfirmaAtivar").style.display = 'block';
                document.getElementById("btnConfirmaInativar").style.display = 'none';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Curso';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar a área de concentração <strong>' + document.getElementById("<%=txtNomeArea.ClientID%>").value + '</strong>?' ;
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Curso';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar a área de concentração <strong>' + document.getElementById("<%=txtNomeArea.ClientID%>").value + '</strong>?';
            }
            $('#divModalAtivaInativa').modal();
        }

        //============================================================================

        //=======================================

        function fAtivarInativarArea(qOperacao){
            //alert(qOperacao);
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAtivarInativarArea",
                contentType: 'application/json; charset=utf-8',
                data: "{qOperacao:'" + qOperacao + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var vTitulo = '';
                    var vBg = '';
                    var vIcon = '';
                    if (qOperacao == "Ativar") {
                        vTitulo = "Área de Concentração Ativada com sucesso";
                        vBg = "info";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    else {
                        vTitulo = "Área de Concentração Inativada com sucesso"
                        vBg = "danger";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    //alert('sucesso');
                    //Tratando o retorno com parseJSON
                    var json = $.parseJSON(data.d);
                    //alert(itens[0].NomeEmpresa);
                    if (json[0].Retorno == 'ok') {
                        $.notify({
                            icon: vIcon,
                            title: '<br /><br /><strong> Atenção! </strong><br /><br />',
                            message: vTitulo,
                        },{
                            type: vBg,
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            },
                            placement: {
                                from: "top",
                                align: "center"
                            }
                        });

                        if (qOperacao == "Ativar") {
                            document.getElementById('<%=btnAtivar.ClientID%>').style.display='none';
                            document.getElementById('<%=btnInativar.ClientID%>').style.display='block';
                            document.getElementById('<%=lblInativado.ClientID%>').style.display='none';
                        }
                        else {
                            document.getElementById('<%=btnAtivar.ClientID%>').style.display='block';
                            document.getElementById('<%=btnInativar.ClientID%>').style.display='none';
                            document.getElementById('<%=lblInativado.ClientID%>').style.display='block';
                        }

                        $('#divModalAtivaInativa').modal('hide');
                    }
                    else if (json[0].Retorno == "deslogado") {
                        window.location.href = "index.html";
                    }
                    else {
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Problema na Ativação/Inativação da Área de Concentração';
                        document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = json[0].Resposta;
                        $("#divCabecalho").removeClass("alert-warning");
                        $("#divCabecalho").removeClass("alert-primary");
                        $('#divCabecalho').addClass('alert-danger');
                        $('#divMensagemModal').modal();

                    }
                    $('#divModalAtivaInativa').modal('hide')

                },
                error: function (xmlHttpRequest, status, err) {
                    if (qOperacao == "Ativar") {
                        document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro para Ativar Área de Concentração';
                        document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro para ativar o professor <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;
                    }
                    else {
                        document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro para Inativar Área de Concentração';
                        document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro para Inativar o professor <br/> Erro: ' + err + '<br/>Status do erro: ' + status ;
                    }
                    
                    $('#divModalAtivaInativa').modal('hide')
                    $('#divMensagemModal').modal('show');
                }
            });
        }

        //=======================================

    </script>

</asp:Content>
