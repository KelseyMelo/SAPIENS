<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadTipoCurso.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadTipoCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li4TipoCursos" />

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
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
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Tipo Curso</strong> (Listagem)</h3>
        </div>

        <div class="col-md-3 hidden">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarTipoCurso" name="btnCriarTipoCurso" class="btn btn-success" href="#" onclick=""  > <%--onserverclick="btnCriarTipoCurso_Click" --%>
                        <i class="fa fa-magic"></i>&nbsp;Criar Tipo de Curso</button>
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
                            <span>Código</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtCodigoTipoCurso" type="text" value="" maxlength="10" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-5">
                            <span>Nome</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtNomeTipoCurso" type="text" value="" maxlength="350" />
                        </div>
                        
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-4 hidden">
                            <span>Situação</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoTodos" runat="server"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-4">                    
                                <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoSim" runat="server" Checked="true"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoSim.ClientID %>">Ativo</label>
                                </div>

                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoNao" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoNao.ClientID %>">Inativo</label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-7">
                            <span>Status da HomePage</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-3">
                                <asp:RadioButton GroupName="GrupoStatusHome" ID="optStatusHomeTodos" runat="server" Checked="true"/>
                                &nbsp;
                                <label class="opt" for="<%=optStatusHomeTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-3">                    
                                <asp:RadioButton GroupName="GrupoStatusHome" ID="optStatusHomeAprovado" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optStatusHomeAprovado.ClientID %>">Aprovado</label>
                                </div>

                                <div class="col-md-3">
                                <asp:RadioButton GroupName="GrupoStatusHome" ID="optStatusHomeAguardando" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optStatusHomeAguardando.ClientID %>">Aguardando</label>
                                </div>

                                <div class="col-md-3">
                                <asp:RadioButton GroupName="GrupoStatusHome" ID="optStatusHomeReprovado" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optStatusHomeReprovado.ClientID %>">Reprovado</label>
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
                            
                            <a id="aBntPerquisaTipoCurso" runat ="server" onclick="ShowProgress()" onserverclick="btnPerquisaTipoCurso_Click"  href="#" class ="btn btn-success pull-right"><i class="fa fa-check"></i><span>&nbsp;OK</span></a> <%--onserverclick="bntPerquisaAluno_Click"--%>
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Curso encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_tipo_curso"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "id_tipo_curso")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="ShowProgress()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdResultado_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="id_tipo_curso" HeaderText="Código" ItemStyle-HorizontalAlign="Center"/>

                                            <asp:BoundField DataField="tipo_curso" HeaderText="Nome" />

                                            <asp:TemplateField HeaderText="Exibir na Homepage" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# set_Sim_Nao(Convert.ToInt16(DataBinder.Eval(Container.DataItem, "statusHomepage")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Exibir botões (Calendário e Processo Seletivo)" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# set_Sim_Nao(Convert.ToInt16(DataBinder.Eval(Container.DataItem, "statusBotoes")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status da HomePage" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# set_Status_Homepage(Convert.ToInt16(DataBinder.Eval(Container.DataItem, "statusAprovado")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Situação" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "status").ToString() == "inativado" ? "<div class='text-danger'><strong>INATIVO</strong></div>" : "Ativo"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

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

        $(document).ready(function () {

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseCurso: '20%' // optional
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


        function teclaEnter() {
            if (event.keyCode == "13") {
                //funcPesquisar();
                //alert('oi');
                document.getElementById("<%=aBntPerquisaTipoCurso.ClientID%>").click();
            }
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;

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

        $(".alteracao").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });


        $(document).ready(function () {

            $('#<%=grdResultado.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, });

        });

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
