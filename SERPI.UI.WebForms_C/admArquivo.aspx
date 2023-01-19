<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="admArquivo.aspx.cs" Inherits="SERPI.UI.WebForms_C.admArquivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAdmSistema" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liCadArquivos" />

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
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
            <h3 class=""><i class="fa fa-circle-o text-aqua"></i>&nbsp;<strong >Arquivos para Portal</strong> (Listagem)</h3>
        </div>

        <div class="col-md-3">
            <br />
            <div class ="pull-right ">
                <button type="button"  runat="server" id="btnCriarArquivo" name="btnCriarArquivo" onserverclick="btnCriarArquivo_Click" class="btn btn-success" href="#" onclick=""  > <%--onserverclick="btnCriarArquivo_Click"--%>
                        <i class="fa fa-magic"></i>&nbsp;Cadastrar Arquivo</button>
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
                        <div class="col-md-4">
                            <span>Descrição</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtDescricaoArquivo" type="text" maxlength="200"/>
                            <input id="txtCopy" type="text" style="display:none"/>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4">
                            <span>Nome do Arquivo</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtNomeArquivo" type="text" maxlength="200"/>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span>Tipo Arquivo</span><br />
                            <%--<div class="row center-block btn-default form-group">
                                <asp:RadioButton GroupName="Sexo" ID="optSexoMasculino" runat="server"/>
                                &nbsp;
                                <label class="opt" for="<%=optSexoMasculino.ClientID %>">Masculino</label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    
                                <asp:RadioButton GroupName="Sexo" ID="optSexoFeminino" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optSexoFeminino.ClientID %>">Feminino</label>
                            </div>--%>
                            <asp:DropDownList runat="server" ID="ddlTipoArquivo" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                <asp:ListItem Text="Todos" Value="" />
                                <asp:ListItem Text="jpg" Value="jpg" />
                                <asp:ListItem Text="png" Value="png" />
                                <asp:ListItem Text="pdf" Value="pdf" />
                            </asp:DropDownList>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

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

                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>
                            
                            <a id="aBntPerquisaArquivo" runat ="server" onclick="fProcessando()"  href="#" onserverclick="btnPerquisaArquivo_Click" class ="btn btn-success pull-right"><i class="fa fa-check"></i><span>&nbsp;OK</span></a> <%----%>
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Arquivo encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_arquivo"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "id_arquivo").ToString()%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="id_arquivo" HeaderText="Código" ItemStyle-HorizontalAlign="Center" />

                                            <asp:BoundField DataField="descricao" HeaderText="Descrição" ItemStyle-HorizontalAlign="Left" />

                                            <asp:TemplateField HeaderText="Nome do arquivo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# setNomeArquivo(DataBinder.Eval(Container.DataItem, "nome_arquivo").ToString(), Convert.ToDateTime((DataBinder.Eval(Container.DataItem, "data_alteracao"))))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="tipo_arquivo" HeaderText="Tipo do arquivo" ItemStyle-HorizontalAlign="Center" />

                                            <asp:TemplateField HeaderText="Copiar link" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# setCopiarLink(DataBinder.Eval(Container.DataItem, "nome_arquivo").ToString(), Convert.ToDateTime((DataBinder.Eval(Container.DataItem, "data_alteracao"))))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Apagar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# setApagarArquivo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id_arquivo").ToString()), DataBinder.Eval(Container.DataItem, "nome_arquivo").ToString())%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="fProcessando()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdResultado_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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

        <div class="row">
            <div class="col-md-12">
            </div>
        </div>
        <br />
    </div>

    <!-- Modal para Apagar arquivo -->
    <div class="modal fade" id="divModalApagarArquivo" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <h4 class="modal-title" id="H1"><i class="fa fa-eraser"></i>&nbsp;Apagar Arquivo
                    </h4>
                </div>
                <div id="Div2" class="modal-body">
                    <strong>Deseja Apagar o Arquivo:</strong> <label id="lblNomeArquivo">test</label> (<label id="lblIdArquivo">test</label>)?
                </div>
                <div class="modal-footer">

                    <div class="pull-left">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-close"></i>&nbsp;Cancelar
                        </button>
                    </div>
                    
                    <div class="pull-right">
                        
                        <span class="icon-input-btn"><span class="glyphicon glyphicon-check"></span>
                            <button runat="server" id="btnApagarArquivo" name="btnApagarArquivo" class="btn btn-success" href="#" onserverclick="btnApagarArquivo_Click">
                                <i class="fa fa-check"></i>&nbsp;Confirma</button>
                        </span>

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
                increaseVideo: '20%' // optional
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

                document.getElementById("<%=aBntPerquisaArquivo.ClientID%>").click();
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

        function fAbreModalApagarArquivo(qId, qNome) {
            //$('#divCabecalho').toggleClass(qClass);
            
            document.getElementById('lblNomeArquivo').innerHTML = qNome;
            document.getElementById('lblIdArquivo').innerHTML = qId;
            document.getElementById('hCodigo').value = qId;
            $('#divModalApagarArquivo').modal();
            //alert("Hello world");
        }

        function AbreModalAtivar(qId, qDescricao) {
            //$('#divCabecalho').toggleClass(qClass);
            $(".csslblVideo").html(qDescricao);
            $(".csslblIdVideo").html(qId);
            document.getElementById('hCodigo').value = qId;
            $('#divModalAtivar').modal();
            //alert("Hello world");
        }


        $(".alteracao").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });


        $(document).ready(function () {
            $('#<%=grdResultado.ClientID%>').dataTable({ stateSave: false, "bProcessing": true, order: [[0, 'desc']], });
            fechaLoading();
        });

        function AbreModalMensagem(qClass) {
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $("#divCabecalho").removeClass("alert-primary");
            $('#divCabecalho').removeClass('alert-danger');
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
        }

        //=======================================

        function fCopyUrl(qURL) {
            /* Get the text field */
            document.getElementById("txtCopy").value = qURL;

            document.getElementById("txtCopy").style.display = 'block';
            //alert(document.getElementById("txtCopy").value);
            var copyText = document.getElementById("txtCopy");
            //alert(copyText.value);
            /* Select the text field */
            copyText.select();

            /* Copy the text inside the text field */
            document.execCommand("copy");

            document.getElementById("txtCopy").style.display = 'none';

            /* Alert the copied text */
            $.notify({
                icon: 'fa fa-check',
                title: '<strong>Atenção! </strong><br /><br />',
                message: 'O Link foi copiado com êxito.<br />',
            
            }, {
                type: 'info',
                delay: 1000,
                timer: 1000,
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

        //================================================================================

    </script>

</asp:Content>
