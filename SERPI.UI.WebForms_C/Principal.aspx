<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="SERPI.UI.WebForms_C.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="" />

    <link href="https://fonts.googleapis.com/css?family=Geo" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css?family=Londrina+Shadow|Russo+One" rel="stylesheet"/>
    
    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
    
    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet" />

    <script src="Scripts/moment-with-locales.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <style>
        #ContentPlaceHolderBody_grdNovidadesSapiens td.centralizarTH {
            vertical-align: middle;  
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

        div.dt-buttons{
            position:relative;
            float:left;
        }

        /*body {
            font-family: 'liquid_crystalregular', sans-serif;
            user-select: none;
            user-drag: none;
        }

        .clock {
            height: 100px;
            width: 70%;
            line-height: 100px;
            margin: 150px auto 0;
            padding: 0 50px;
            background: #222;
            color: #eee;
            text-align: center;
            border-radius: 15px;
            box-shadow: 0 0 7px #222;
            text-shadow: 0 0 3px #fff;
        }*/
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


    <asp:UpdateProgress ID="UpdateProgress2" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel2"  >
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

    <asp:UpdateProgress ID="UpdateProgress3" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel3"  >
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

    <asp:UpdateProgress ID="UpdateProgress4" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel4"  >
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
        <div class="col-lg-12">
            <H1 class="page-header">SAPIENS - Sistema de Apoio Integrado ao Ensino </H1>
            <h3><i class="fa fa-smile-o"></i> Bem-Vindo
                <div class="hidden-lg hidden-md"></div>
                <small></small></h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <br />

    <div class="row" id="divRowAluno" runat="server">
        <div class="col-md-1 col-lg-1 hidden-sm hidden-xs">
            
        </div>

        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3 text-justify well text-primary">
            <h3><span ><div id="clock" class="clock text-center" style="font-family: 'Russo One', sans-serif; font-size:xx-large;">carregando ...</div></span></h3>
        </div>
        <!-- /.col-lg-12 -->
    </div>

    <div id="divRowQuadro" runat="server" visible="false">


        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li id="li_Quadro" runat="server" class="active"><a href="#<%=tab_Quadro.ClientID%>" data-toggle="tab">Quadro de Horários</a></li>
                <li id="li_Matricula" runat="server" ><a href="#<%=tab_Matricula.ClientID%>" data-toggle="tab">Alunos com Situação de Matrícula Pendente</a></li>
                <li id="li_Documentos" runat="server" class="" ><a href="#<%=tab_Documentos.ClientID%>" data-toggle="tab">Alunos com Documentos Pendentes</a></li>
                <li id="li_AprovacaoOrientador" runat="server" class="" ><a href="#<%=tab_AprovacaoOrientador.ClientID%>" data-toggle="tab">Alunos com Defesa aprovada e sem "Data Aprovação Orientador"</a></li>
                <li id="li_EntregaArtigo" runat="server" class="" ><a href="#<%=tab_EntregaArtigo.ClientID%>" data-toggle="tab">Alunos com Data de Aprovação Orientador e sem "Data Entrega Artigo"</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="tab_Quadro" runat="server">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h4>Quadro de horários</h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-12">

                                    <br />

                                    <div class="row">
                                        <div class="col-md-3">
                                            <span>Data Evento <small><b>(início)</b></small> </span>
                                            <br />
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input class="form-control input-sm" runat="server" id="txtDataQuadro" type="date" value="" onchange="fFechaGrid()" />
                                            </div>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-3">
                                            <span>Data Evento <small><b>(fim)</b></small> </span>
                                            <br />
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input class="form-control input-sm" runat="server" id="txtDataQuadroFim" type="date" value="" onchange="fFechaGrid()" />
                                            </div>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <br />
                                            <a id="aBntPerquisaAluno" runat="server" onserverclick="btnImprimirPresencaAluno_Click" onclick="fProcessando()" href="#" class="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="grid-content">
                                        <div runat="server" id="divMsgQuadro" visible="false">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="Label1" Text="Nenhuma reserva encontrada" />
                                            </div>
                                        </div>
                                        <div id="divQuadro" class="table-responsive">
                                            <asp:GridView ID="grdQuadro" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_monitor"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><%--ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"--%>
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "dataeventoinicio").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Data" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "Dataeventomonitor").ToString().Replace (" ","<br>")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="desceventomonitor" HeaderText="Evento" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="Localeventomonitor" HeaderText="Local" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:TemplateField HeaderText="Horário" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "Horarioeventomonitor").ToString().Replace (" as","<br>as")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Responsaveleventomonitor" HeaderText="Responsável" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="Coffee" HeaderText="Coffee" ItemStyle-HorizontalAlign="Left" />

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
                <!-- /.tab-pane -->
                <div class="tab-pane" id="tab_Matricula" runat="server">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h4>Alunos com Situação de Matrícula Pendente</h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-12">

                                    <br />
                                    
                                    <div class="row">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                            
                                                    <div class="col-md-3">
                                                        <span>Tipo Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlTipoCursoPrincipal" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoPrincipal_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br />
                                                    </div>

                                                    <div class="col-md-5">
                                                        <span>Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlNomeCursoPrincipal" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                            
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoPrincipal" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <br />
                                            <a id="aBntPesquisaMatricula" runat="server" onserverclick="btnPesquisaMatricula_Click" onclick="fProcessando()" href="#" class="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
                                        </div>
                                    </div>
                                    <br />

                                    <br />

                                    <div class="grid-content" id="divGradeMatricula" runat="server" style="display:none">
                                        <div runat="server" id="divMsgMatricula" visible="true">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="Label2" Text="Nenhum aluno com a Situação de Matrícula Pendente encontrado" />
                                            </div>
                                        </div>
                                        <div id="divMatricula" class="table-responsive">
                                            <asp:GridView ID="grdMatricula" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_aluno"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <asp:TemplateField HeaderText="aluno.nome" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><%--ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"--%>
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "alunos.nome").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="id_aluno" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:BoundField DataField="alunos.nome" HeaderText="Aluno" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cursos.nome" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cod_turma" HeaderText="Turma" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                        <ItemTemplate>
                                                            <span style="position: relative;">
                                                                <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                                <asp:Button OnClientClick="fProcessando()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdMatricula_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                <!-- /.tab-pane -->
                <div class="tab-pane" id="tab_Documentos" runat="server">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h4>Alunos com Documentos Pendentes (Documentos Obrigatórios ou Contratos Assinados)</h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-12">

                                    <br />
                                    
                                    <div class="row">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                            
                                                    <div class="col-md-3">
                                                        <span>Tipo Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlTipoCursoDocumentos" onchange="fMostrarProgresso2()" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoDocumentos_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br />
                                                    </div>

                                                    <div class="col-md-5">
                                                        <span>Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlCursoDocumentos" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                            
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoDocumentos" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <br />
                                            <a id="btnPesquisaDocumento" runat="server" onserverclick="btnPesquisaDocumento_Click" onclick="fProcessando()" href="#" class="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
                                        </div>
                                    </div>
                                    <br />

                                    <br />

                                    <div class="grid-content" id="divGradeDocumento" runat="server" style="display:none">
                                        <div runat="server" id="divMsgDocumento" visible="true">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="Label3" Text="Nenhum aluno Documentos Pendentes encontrado" />
                                            </div>
                                        </div>
                                        <div id="divDocumento" class="table-responsive">
                                            <asp:GridView ID="grdDocumento" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_aluno"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <asp:TemplateField HeaderText="aluno.nome" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><%--ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"--%>
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "alunos.nome").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="id_aluno" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:BoundField DataField="alunos.nome" HeaderText="Aluno" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cursos.nome" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cod_turma" HeaderText="Turma" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                        <ItemTemplate>
                                                            <span style="position: relative;">
                                                                <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                                <asp:Button OnClientClick="fProcessando()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdDocumento_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                <!-- /.tab-pane -->
                <div class="tab-pane" id="tab_AprovacaoOrientador" runat="server">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h4>Alunos com Defesa aprovada e sem "Data Aprovação Orientador"</h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-12">

                                    <br />
                                    
                                    <div class="row">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                            
                                                    <div class="col-md-3">
                                                        <span>Tipo Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlTipoCursoAprovacaoOrientador" onchange="fMostrarProgresso3()" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoAprovacaoOrientador_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br />
                                                    </div>

                                                    <div class="col-md-5">
                                                        <span>Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlCursoAprovacaoOrientador" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                            
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoAprovacaoOrientador" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <br />
                                            <a id="btnPesquisaAprovacaoOrientador" runat="server" onserverclick="btnPesquisaAprovacaoOrientador_Click" onclick="fProcessando()" href="#" class="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
                                        </div>
                                    </div>
                                    <br />

                                    <br />

                                    <div class="grid-content" id="divGradeAprovacaoOrientador" runat="server" style="display:none">
                                        <div runat="server" id="divMsgAprovacaoOrientador" visible="true">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="Label4" Text="Nenhum aluno com Defesa aprovada e sem 'Data Aprovação Orientador' encontrado" />
                                            </div>
                                        </div>
                                        <div id="divAprovacaoOrientador" class="table-responsive">
                                            <asp:GridView ID="grdAprovacaoOrientador" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_aluno"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <asp:TemplateField HeaderText="aluno.nome" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><%--ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"--%>
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "alunos.nome").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="id_aluno" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:BoundField DataField="alunos.nome" HeaderText="Aluno" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cursos.nome" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cod_turma" HeaderText="Turma" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                        <ItemTemplate>
                                                            <span style="position: relative;">
                                                                <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                                <asp:Button OnClientClick="fProcessando()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdAprovacaoOrientador_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                <!-- /.tab-pane -->
                <div class="tab-pane" id="tab_EntregaArtigo" runat="server">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h4>Alunos com Data de Aprovação Orientador e sem "Data Entrega Artigo"</h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-12">

                                    <br />
                                    
                                    <div class="row">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                            
                                                    <div class="col-md-3">
                                                        <span>Tipo Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlTipoCursoEntregaArtigo" onchange="fMostrarProgresso4()" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoEntregaArtigo_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="hidden-lg hidden-md">
                                                        <br />
                                                    </div>

                                                    <div class="col-md-5">
                                                        <span>Curso</span><br />
                                                        <asp:DropDownList Width="100%" runat="server" ID="ddlCursoEntregaArtigo" ClientIDMode="Static" class="ddl_fecha_grid_resultados_Matricula form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                            
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoEntregaArtigo" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <br />
                                            <a id="btnPesquisaEntregaArtigo" runat="server" onserverclick="btnPesquisaEntregaArtigo_Click" onclick="fProcessando()" href="#" class="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
                                        </div>
                                    </div>
                                    <br />

                                    <br />

                                    <div class="grid-content" id="divGradeEntregaArtigo" runat="server" style="display:none">
                                        <div runat="server" id="divMsgEntregaArtigo" visible="true">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="Label5" Text="Nenhum aluno com Defesa aprovada e sem 'Data Aprovação Orientador' encontrado" />
                                            </div>
                                        </div>
                                        <div id="divEntregaArtigo" class="table-responsive">
                                            <asp:GridView ID="grdEntregaArtigo" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_aluno"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <asp:TemplateField HeaderText="aluno.nome" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"><%--ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"--%>
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "alunos.nome").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="id_aluno" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:BoundField DataField="alunos.nome" HeaderText="Aluno" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cursos.nome" HeaderText="Curso" ItemStyle-HorizontalAlign="Left" />

                                                    <asp:BoundField DataField="turmas.cod_turma" HeaderText="Turma" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                        <ItemTemplate>
                                                            <span style="position: relative;">
                                                                <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                                <asp:Button OnClientClick="fProcessando()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdEntregaArtigo_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                <!-- /.tab-pane -->
            </div>
            <!-- /.tab-content -->
        </div>


        
    </div>
    <br />

    <div class="row" id="divRowSecretaria" runat="server">
        <div class="col-lg-3 col-md-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><label id="lblQtdNovidades" runat="server"></label></h3>

              <p>Novidades Sapiens</p>
            </div>
            <div class="icon">
              <i id="iIconeNovidades" runat="server" class="fa fa-newspaper-o"></i>
            </div>
            <a href="#" data-toggle="collapse" data-target="#especificacao" class="small-box-footer collapsed">Mais informações <i class="fa fa-arrow-circle-right" aria-hidden="true"></i></a>
                
              
          </div>
            <div id="especificacao" class="collapse" style="height: 0px;">
                <div class="grid-content">
                    <div runat="server" id="msgSemResultadosgrdNovidadesSapiens" visible="false">
                        <div class="alert bg-gray">
                            <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhuma Novidade encontrada" />
                        </div>
                    </div>
                    <div class="">
                        <asp:GridView ID="grdNovidadesSapiens" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                            AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="idNovidadesSistema"
                            SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <%--<AlternatingRowStyle BackColor="#000022" />--%>
                            <Columns>

                                <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "idNovidadesSistema").ToString()%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Título" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem,"titulo").ToString().Replace("\\\\","\"") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Data" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                    <ItemTemplate>
                                        <%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem,"dataOcorrencia")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Detalhe" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                    <ItemTemplate>
                                        <%#setVisualizarNovidades(DataBinder.Eval(Container.DataItem,"titulo").ToString(), DataBinder.Eval(Container.DataItem,"detalhe").ToString(), Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"dataInicio")),  Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"dataFim")),  Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"dataOcorrencia"))) %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <HeaderStyle BackColor="#00c0ef" Font-Bold="True" ForeColor="White" />

                        </asp:GridView>

                    </div>

                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="divAvisatrocaSenha" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <h4 class="modal-title" id="H2"><i class="fa fa-key"></i>&nbsp;Alteração de Senha
                    </h4>
                </div>
                <div id="Div3" class="modal-body">
                    <h4>Prezado Usuário</h4>
                    <br />
                    Você ainda está com a Senha Padrão.
                    <br /><br />
                    É aconselhável que você altere sua senha.<br /> <asp:Label ID="lblMensagemDesenv" runat="server" Text=""></asp:Label>
                </div>
                <div class="modal-footer">

                    <div class="pull-right">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar
                        </button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="divQservidor" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <h4 class="modal-title" id="H1"><i class="fa fa-key"></i>&nbsp;Desenvolvimento
                    </h4>
                </div>
                <div id="Div2" class="modal-body">
                    <h4>Servidor de Desenvolvimento</h4>
                    <br /><br />
                    <asp:Label ID="lblMensagem" runat="server" Text="Label"></asp:Label>
                    <br /><br />
                </div>
                <div class="modal-footer">

                    <div class="pull-right">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Fechar
                        </button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal modal-info fade" tabindex="-1" role="dialog" id="divModalVisualizarNovidades" aria-hidden="true" style="display: none;">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">×</span></button>
                <h4 class="modal-title"><i class="fa fa-newspaper-o"></i>  Implementações Sapiens</h4>
              </div>
              <div class="modal-body">
                <p><label id="lblTituloNovidades" style="font-size:x-large"></label></p>
                <br />

                <p><label id="lblDetalheNovidades" style="font-size:large"></label></p>
                <br />

                <p><label id="lblDataNovidades" style="font-size:large"></label></p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline" data-dismiss="modal"><i class="fa fa-close"></i> Fechar</button>
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
                    <asp:Label runat="server" ID="lblMensagemClone" Text="" />

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

    

    <script>
        function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }

        function fMostrarProgresso2()
        {
            document.getElementById('<%=UpdateProgress2.ClientID%>').style.display = "block";
        }

        function fMostrarProgresso3()
        {
            document.getElementById('<%=UpdateProgress3.ClientID%>').style.display = "block";
        }

        function fMostrarProgresso4()
        {
            document.getElementById('<%=UpdateProgress4.ClientID%>').style.display = "block";
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

            $('#<%=ddlTipoCursoPrincipal.ClientID%>').on("select2:select", function(e) { 
                 $('#<%=divGradeMatricula.ClientID%>').hide();
            });

            $('#<%=ddlNomeCursoPrincipal.ClientID%>').on("select2:select", function(e) { 
                 $('#<%=divGradeMatricula.ClientID%>').hide();
            });
        }

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

        $('.ddl_fecha_grid_resultados_Matricula').on('select2:select', function (e) {
            $('#<%=divGradeMatricula.ClientID%>').hide();
        });

        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "locale-compare-asc": function (a, b) {
                return a.localeCompare(b, 'da', { sensitivity: 'accent' })
            },
            "locale-compare-desc": function (a, b) {
                return b.localeCompare(a, 'da', { sensitivity: 'accent' })
            }
        });

        

        $(document).ready(function () {
            fSelect2(); 

            $('#<%=grdNovidadesSapiens.ClientID%>').dataTable(
                {
                    stateSave: false,
                    "bProcessing": true,
                    columnDefs: [{ type: 'date-euro', targets: 2 }, { "orderable": false, targets: 3 }],
                    order: [[2, 'desc']],
                    searching: false, //Pesquisar
                    bPaginate: true, // Paginação
                    bLengthChange: false, // Mostar 10, 20 50 registros por página
                    bInfo: true, //mostrando 1 de x registros
                }
            );

            if (document.getElementById("<%=grdQuadro.ClientID%>") != null) {
                $('#<%=grdQuadro.ClientID%>').dataTable(
                    {
                        stateSave: true,
                        "bProcessing": true,
                        columnDefs: [
                            //{ type: 'date-euro', targets: 0 },
                            { type: 'date-euro', targets: [0] },
                            { width: '10%', targets: [1,4] },
                            { "orderable": false, targets: [1,4,6] }
                            //width: '10px'
                        ],
                        order: [[0, 'asc']],
                        searching: false, //Pesquisar
                        bPaginate: false, // Paginação
                        bLengthChange: false, // Mostar 10, 20 50 registros por página
                        bInfo: false, //mostrando 1 de x registros
                        dom: "<'row'<'col-md-6'l><'col-md-6'Bf>>" + "<'row'<'col-md-6'><'col-md-6'>>" + "<'row'<'col-md-12't>><'row'<'col-md-6'i><'col-md-6'p>>", //l = Mosatra 20 reg por pág; f = pesquisa; 'Blfrtip'
                        buttons: [
                    {
                        extend: 'pdf',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6],
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
                                    newdata = replaceAll('<br>', '\n', newdata);
                                    newdata = replaceAll('<span style="line-height: 2.2em;">', '', newdata);
                                    newdata = replaceAll('</span>', '', newdata);
                                    return newdata;
                                }
                            }
                        },
                        //orientation: 'landscape',
                        title: function () {
                                var qPulaLinha = "\n";
                                var fRetornoFiltro = "Quadro de Horários";
                                if (document.getElementById("<%=txtDataQuadro.ClientID%>").value != "") {
                                    fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Data: de " + moment(document.getElementById("<%=txtDataQuadro.ClientID%>").value).format('DD/MM/YYYY') + " a " + moment(document.getElementById("<%=txtDataQuadroFim.ClientID%>").value).format('DD/MM/YYYY');
                                }
                                return fRetornoFiltro;
                            },
                        filename: 'Quadro de Horário ' + moment(document.getElementById("<%=txtDataQuadro.ClientID%>").value).format('DD-MM-YYYY'),
                        text: '<i class="fa fa-file-pdf-o fa-lg" title="Pdf"><br></i>',
                        className: 'btn btn-info btn-circle'
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6],
                            format: {
                                body: function (data, row, column, node) {
                                    var newdata = data;
                                    //newdata = replaceAll('<br>', '<br>', newdata);
                                    return newdata;
                                }
                            }
                        },
                        //orientation: 'landscape',
                        title: function () {
                                var qPulaLinha = "<br />";
                                var fRetornoFiltro = "Quadro de Horários";
                                if (document.getElementById("<%=txtDataQuadro.ClientID%>").value != "") {
                                    fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Data: de " + moment(document.getElementById("<%=txtDataQuadro.ClientID%>").value).format('DD/MM/YYYY') + " a " + moment(document.getElementById("<%=txtDataQuadroFim.ClientID%>").value).format('DD/MM/YYYY');
                                }
                                return fRetornoFiltro;
                            },
                        filename: 'Quadro de Horário ' + moment(document.getElementById("<%=txtDataQuadro.ClientID%>").value).format('DD-MM-YYYY'),
                        text: '<i class="fa fa-print fa-lg" title="Imprimir"><br></i>',
                        className: 'btn btn-default btn-circle'
                    },
                    {
                        extend: 'excel',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6],
                            format: {
                                body: function (data, row, column, node) {
                                    var newdata = data;
                                    newdata = replaceAll('<br>', ' \r\n', newdata);
                                    newdata = replaceAll('<span style="line-height: 2.2em;">', '', newdata);
                                    newdata = replaceAll('</span>', '', newdata);
                                    return newdata;
                                }
                            }
                        },
                        title: function () {
                            var qPulaLinha = ' -';
                                var fRetornoFiltro = "Quadro de Horários";
                                if (document.getElementById("<%=txtDataQuadro.ClientID%>").value != "") {
                                    fRetornoFiltro = fRetornoFiltro + qPulaLinha + " Data: de " + moment(document.getElementById("<%=txtDataQuadro.ClientID%>").value).format('DD/MM/YYYY') + " a " + moment(document.getElementById("<%=txtDataQuadroFim.ClientID%>").value).format('DD/MM/YYYY');
                                }
                                return fRetornoFiltro;
                            },
                        filename: 'Quadro de Horário ' + moment(document.getElementById("<%=txtDataQuadro.ClientID%>").value).format('DD-MM-YYYY'),
                        text: '<i class="fa fa-file-excel-o fa-lg" title="Excel"></i>',
                        className: 'btn btn-success  btn-circle'
                    }],
                    }
                );
            }


            //===============================================

            if (document.getElementById("<%=grdMatricula.ClientID%>") != null) {
                $('#<%=grdMatricula.ClientID%>').dataTable(
                    {
                        stateSave: false,
                        "bProcessing": true,
                        columnDefs: [
                            { type: 'locale-compare', targets: [2,3] },
                            { "orderable": false, targets: [0, 5] }
                            //width: '10px'
                        ],
                        order: [[0, 'asc']],
                        searching: true, //Pesquisar
                        bPaginate: true, // Paginação
                        bLengthChange: true, // Mostar 10, 20 50 registros por página
                        bInfo: true, //mostrando 1 de x registros

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

                    }
                );
            }

            //===============================================

            if (document.getElementById("<%=grdDocumento.ClientID%>") != null) {
                $('#<%=grdDocumento.ClientID%>').dataTable(
                    {
                        stateSave: false,
                        "bProcessing": true,
                        columnDefs: [
                            { type: 'locale-compare', targets: [2,3] },
                            { "orderable": false, targets: [0, 5] }
                            //width: '10px'
                        ],
                        order: [[0, 'asc']],
                        searching: true, //Pesquisar
                        bPaginate: true, // Paginação
                        bLengthChange: true, // Mostar 10, 20 50 registros por página
                        bInfo: true, //mostrando 1 de x registros

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

                    }
                );
            }

            //===============================================

            if (document.getElementById("<%=grdAprovacaoOrientador.ClientID%>") != null) {
                $('#<%=grdAprovacaoOrientador.ClientID%>').dataTable(
                    {
                        stateSave: false,
                        "bProcessing": true,
                        columnDefs: [
                            { type: 'locale-compare', targets: [2,3] },
                            { "orderable": false, targets: [0, 5] }
                            //width: '10px'
                        ],
                        order: [[0, 'asc']],
                        searching: true, //Pesquisar
                        bPaginate: true, // Paginação
                        bLengthChange: true, // Mostar 10, 20 50 registros por página
                        bInfo: true, //mostrando 1 de x registros

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

                    }
                );
            }

            //===============================================


            if (document.getElementById("<%=grdEntregaArtigo.ClientID%>") != null) {
                $('#<%=grdEntregaArtigo.ClientID%>').dataTable(
                    {
                        stateSave: false,
                        "bProcessing": true,
                        columnDefs: [
                            { type: 'locale-compare', targets: [2,3] },
                            { "orderable": false, targets: [0, 5] }
                            //width: '10px'
                        ],
                        order: [[0, 'asc']],
                        searching: true, //Pesquisar
                        bPaginate: true, // Paginação
                        bLengthChange: true, // Mostar 10, 20 50 registros por página
                        bInfo: true, //mostrando 1 de x registros

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

                    }
                );
            }

            //===============================================

            function update() {
                moment.locale("pt-br");
                $('#clock').html(moment().format('dddd') + '<br><br>' + moment().format('D MMMM YYYY') + '<br><br>' + moment().format('H:mm:ss'));
            }

            setInterval(update, 1000);

            fechaLoading();

        });

        //===========================

        function fFechaGrid() {
            document.getElementById('divQuadro').style.display = "none";
        }

        //window.onload = digitized();

        function fExibeVisualizarNovidades(qTitulo, qDetalhe, qDataOcorrencia) {
            document.getElementById("lblTituloNovidades").innerHTML = replaceAll("\\","\"",qTitulo);
            document.getElementById("lblDetalheNovidades").innerHTML = replaceAll("\\", "\"", qDetalhe);
            document.getElementById("lblDataNovidades").innerHTML = "Data ocorrência: <strong>" + qDataOcorrencia + "</strong>";
            $('#divModalVisualizarNovidades').modal();
        }

        function AvisoTrocaSenha() {
            $('#divAvisatrocaSenha').modal();
            //$('#divErroLogin').modal();
            //alert("Hello world");
        }

        function qServidor() {
            $('#divQservidor').modal();
            //$('#divErroLogin').modal();
            //alert("Hello world");
        }

        function AbreModalMensagem(qClass) {
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
