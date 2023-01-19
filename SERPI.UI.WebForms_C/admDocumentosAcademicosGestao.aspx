<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="admDocumentosAcademicosGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.admDocumentosAcademicosGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAdmSistema" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liCadDocumentosAcademicos" />

    <script src="Scripts/jquery.mask.min.js"></script>

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
    
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

        label.opt {
            cursor: pointer;
        }

        .negrito
        {
            font-weight: bold !important;
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

            .nicEdit-main {
            overflow: auto !important;
            height: 5.5em;
        }

        .a_faq {
            color:dimgrey !important;
            text-decoration:none !important;
            transition:all 0.5s;
        }

        .a_faq:hover {
            color:#3588CC !important;
            text-decoration:none !important;
            transition:all 0.5s;
        }

        .rotate{
            -moz-transition: all 0.5s linear;
            -webkit-transition: all 0.5s linear;
            transition: all 0.5s linear;
        }

        .rotate.down{
            -moz-transform:rotate(-90deg);
            -webkit-transform:rotate(-90deg);
            transform:rotate(-90deg);
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
    <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server"  AssociatedUpdatePanelID="UpdatePanel1"  >
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
    </asp:UpdateProgress>--%>

    <div class="row"> 
        <div class="col-md-10">
            <h3 class=""><i class="fa fa-circle-o text-aqua"></i>&nbsp;<strong >Documento Acadêmico</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(novo)"></asp:Label></h3>
            <asp:Label  ID="lblId" runat="server" CssClass="hidden"></asp:Label>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>
        

        <div class="col-md-2">
            <br />
            <button type="button"  runat="server" id="btnCriarDocumentoAcademico" name="btnCriarDocumentoAcademico" class="btn btn-primary pull-right" href="#" onclick="" onserverclick="btnCriarDocumentoAcademico_Click" > <%--onserverclick="btnCriarVideo_Click"--%>
                    <i class="fa fa-magic fa-lg"></i>&nbsp;Cadastrar novo Documento</button>
        </div>
    </div>
    <br />

    <div class="container-fluid">
        <div class="tab-content">
            <div class="panel panel-default">
                <div class="panel-body">



                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab_Preview" data-toggle="tab"><em>Preview</em></a></li>
                            <li id="tabPublicado" runat="server"><a href="#tab_Publicado" data-toggle="tab">Publicado</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_Preview">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">

                                            <div class="col-md-6 text-center">
                                                <button type="button" id="btnEnviarAprovacaoOffLine" name="btnEnviarAprovacaoOffLine" runat="server" class="btn btn-warning " onclick="fModalEnviarAprovacao('EnviarAprovacao')">
                                                    <i class="fa fa-mail-forward fa-lg"></i>&nbsp;Enviar para Aprovação
                                                </button>
                                                <button type="button" id="btnAprovarOffLine" name="btnAprovarOffLine" runat="server" class="btn btn-success " onclick="fModalEnviarAprovacao('Aprovar')">
                                                    <i class="fa fa-thumbs-o-up fa-lg"></i>&nbsp;Aprovar
                                                </button>
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br /> 
                                            </div>

                                            <div class="col-md-6 text-center">
                                                <button type="button" runat="server" id="btnSalvar" name="btnSalvar" class="btn btn-success" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvar_ServerClick">
                                                    <i class="fa fa-floppy-o fa-lg"></i>&nbsp;Salvar Dados
                                                </button>
                                                <button type="button" id="btnReprovarOffLine" name="btnReprovarOffLine" runat="server" class="btn btn-danger " onclick="fModalEnviarAprovacao('Reprovar')">
                                                    <i class="fa fa-thumbs-o-down fa-lg"></i>&nbsp;Reprovar
                                                </button>
                                            </div>
                                        </div>
                                        <br />

                                        <div runat="server" id="divEdicao">
                                            <div class="row">
                                                <div class="col-md-2 ">
                                                    <span>Data de Cadastro</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtDataCadastro" type="text" readonly="true" />
                                                </div>
                                                <div class="hidden-lg hidden-md">
                                                    <br />
                                                </div>

                                                <div class="col-md-2 ">
                                                    <span>Status</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtStatus" type="text" readonly="true" />
                                                </div>
                                                <div class="hidden-lg hidden-md">
                                                    <br />
                                                </div>

                                                <div class="col-md-2 ">
                                                    <span>Última Alteração</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtDataAlteracao" type="text" readonly="true" />
                                                </div>
                                                <div class="hidden-lg hidden-md">
                                                    <br />
                                                </div>

                                                <div class="col-md-3 ">
                                                    <span>Responsável</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtResponsavel" type="text" readonly="true" />
                                                </div>

                                            </div>
                                            <br />
                                        </div>

                                        <label id="lblTextoHomeAlterado" class="text-red text-center piscante" runat="server"></label>
                                        <div class="row">
                                            <div class="col-md-12 ">
                                                <span>Título </span><span style="color: red;">*</span><br />
                                                <input class="form-control" runat="server" id="txtTituloDocumentoPreview" type="text" value="" maxlength="200" />
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-12 ">
                                                <span>Descrição </span><span style="color: red;">*</span><br />
                                                <textarea style="resize: vertical; font-size: 14px" runat="server" id="txtDescricaoDocumentoPreview" class="form-control" rows="4" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Nome do Arquivo </span><span style="color: red;">*</span><br />
                                                <input class="form-control" runat="server" id="txtNomeArquivoPreview" type="text" value="" readonly="true" />
                                            </div>
                                            <div class="hidden-lg hidden-md">
                                                <br />
                                            </div>

                                            <div class="col-md-2 ">
                                                <br />
                                                <button type="button" runat="server" id="btnLocalizarArquivo" name="btnLocalizarArquivo" class="btn btn-primary center-block " onclick="javascript:fLocalizaArquivo()">
                                                    <i class="fa fa-search fa-lg"></i>&nbsp;Localizar Arquivo
                                                </button>
                                            </div>
                                        </div>

                                        <div runat="server" id="divObservacao">
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12 ">
                                                    <span>Histórico de Observações</span><br />
                                                    
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="grid-content">
                                                                <div runat="server" id="msgSemResultados" visible="false">
                                                                    <div class="alert bg-gray">
                                                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhuma Observação encontrada" />
                                                                    </div>
                                                                </div>
                                                                <div class="table-responsive ">

                                                                    <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_documentos_academicos_obs"
                                                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <%#DataBinder.Eval(Container.DataItem, "DataObs").ToString()%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:BoundField DataField="DataObs" HeaderText="Data/Hora Observação" ItemStyle-HorizontalAlign="Center" />

                                                                            <asp:BoundField DataField="Observacao" HeaderText="Observação" ItemStyle-HorizontalAlign="Left" />

                                                                            <asp:BoundField DataField="usuario" HeaderText="Usuário" ItemStyle-HorizontalAlign="Left" />

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

                                    </div>
                                </div>
                                <asp:Literal ID="litExemploPreview" runat="server"></asp:Literal>

                            </div>
                            <!-- /.tab-pane -->
                            <div class="tab-pane" id="tab_Publicado">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div runat="server" id="divEdicaoAprovado">
                                            <div class="row">
                                                <div class="col-md-2 ">
                                                    <span>Data de Aprovação</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtDataAprovacao" type="text" readonly="true" />
                                                </div>
                                                <div class="hidden-lg hidden-md">
                                                    <br />
                                                </div>
                                                
                                                <div class="col-md-3 ">
                                                    <span>Responsável</span><br />
                                                    <input class="form-control input-sm" runat="server" id="txtusuarioAprovacao" type="text" readonly="true" />
                                                </div>

                                            </div>
                                            <br />
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12 ">
                                                <span>Título </span><span style="color: red;">*</span><br />
                                                <input class="form-control" runat="server" id="txtTituloDocumento" type="text" value="" maxlength="200"  readonly="true"/>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-12 ">
                                                <span>Descrição </span><span style="color: red;">*</span><br />
                                                <textarea style="resize: vertical; font-size: 14px" runat="server" id="txtDescricaoDocumento" class="form-control" rows="4" maxlength="1000" readonly="true"></textarea>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <span>Nome do Arquivo </span><span style="color: red;">*</span><br />
                                                <input class="form-control" runat="server" id="txtNomeArquivo" type="text" value="" readonly="true" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <asp:Literal ID="litExemploPublicado" runat="server"></asp:Literal>
                                
                            </div>
                            <!-- /.tab-pane -->
                        </div>
                        <!-- /.tab-content -->
                    </div>
                    
                </div>
            </div>
        </div>

        <div class="row">

            <div class="col-xs-2">
                <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click"> <%--onserverclick="btnVoltar_Click"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

        </div>
    </div>

    <!-- Modal para aprovações -->
    <div class="modal fade" id="divAprovacao" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div id="divCabecAprovacao" class="modal-header bg-yellow">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><i id="iconCabecEnviar" class="fa fa-mail-forward fa-lg"></i>&nbsp;<label id="lblCabecAprovacao">Associar Grupo</label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 ">
                            <label id="lblCorpoAprovacao">Enviar para Aprovação</label>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-12 ">
                            <div id="divLabelObs"><span>Observação </span><span style="color: red;">*</span><br /></div>
                            <textarea style="resize: vertical; font-size: 14px" runat="server" id="txtObsAprovacao" class="form-control" rows="4" maxlength="1000"></textarea>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-xs-6 ">
                            <div class="pull-left">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    <i class="fa fa-close"></i>&nbsp;Fechar</button>
                            </div>
                        </div>
                        <div class="col-xs-6 ">
                            <div class="pull-right">
                                <button type="button" runat="server" id="btnEnviarAprovacao" name="btnEnviarAprovacao" class="btn btn-warning hidden" onclick="if (fProcessando()) return;" onserverclick="btnEnviarAprovacao_ServerClick">
                                    <i class="fa fa-mail-forward fa-lg"></i>&nbsp;Enviar para Aprovação
                                </button>
                                <button type="button" id="btnEnviarAprovacaoFake" name="btnEnviarAprovacaoFake" class="btn btn-warning " onclick="fEnviarAprovacao()">
                                    <i class="fa fa-mail-forward fa-lg"></i>&nbsp;Enviar para Aprovação
                                </button>

                                <button type="button" runat="server" id="btnAprovar" name="btnAprovar" class="btn btn-success" onclick="if (fProcessando()) return;" onserverclick="btnAprovar_ServerClick">
                                    <i class="fa fa-thumbs-o-up fa-lg"></i>&nbsp;Ok
                                </button>

                                <button type="button" runat="server" id="btnReprovar" name="btnReprovar" class="btn btn-danger hidden" onclick="if (fProcessando()) return;" onserverclick="btnReprovar_ServerClick">
                                    <i class="fa fa-thumbs-o-down fa-lg"></i>&nbsp;Ok
                                </button>
                                <button type="button" id="btnReprovarFake" name="btnReprovarFake" class="btn btn-danger" onclick="fReprovar()">
                                    <i class="fa fa-thumbs-o-down fa-lg"></i>&nbsp;Ok
                                </button>
                            </div>
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

    <div class="hidden">
        <asp:FileUpload ID="fileArquivoParaGravar" runat="server" accept=".doc,.docx,.pdf" onchange="javascript:fSelecionouArquivo(this);"  />
    </div>
    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

     <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.16/sorting/date-euro.js"></script>

    <style>
        /*.even {
            background-color: #dff0d8;
        }*/
    </style>

    <script>

        $(document).ready(function () {
            $('#<%=grdResultado.ClientID%>').dataTable(
                {
                    stateSave: false,
                    "bProcessing": true,
                    order: [[0, 'desc']],
                    columnDefs: [{ type: 'date-euro', targets: 0 }, { type: 'date-euro', targets: 1 }]
                });
            fechaLoading();
        });

        $(".collapsed").click(function () {
            //alert($(this).get(0).id);
            $("#i" + $(this).get(0).id).toggleClass("down");
        })

        function fModalEnviarAprovacao(qEvento) {
            document.getElementById('<%=txtObsAprovacao.ClientID%>').value = "";
            document.getElementById('<%=txtObsAprovacao.ClientID%>').style.display = "none";
            $("#iconCabecEnviar").removeClass("fa-mail-forward");
            $("#iconCabecEnviar").removeClass("fa-thumbs-o-up");
            $("#iconCabecEnviar").removeClass("fa-thumbs-o-down");
            //document.getElementById('lblCorpoAprovacao').style.display = "block";
            document.getElementById('divLabelObs').style.display = "none";
            document.getElementById('btnEnviarAprovacaoFake').style.display = "none";
            document.getElementById('<%=btnAprovar.ClientID%>').style.display = "none";
            document.getElementById('btnReprovarFake').style.display = "none";
            $("#divCabecAprovacao").removeClass("bg-yellow");
            $("#divCabecAprovacao").removeClass("bg-success");
            $("#divCabecAprovacao").removeClass("bg-danger");

            if (qEvento == 'EnviarAprovacao') {
                $('#divCabecAprovacao').addClass("bg-yellow");
                $("#iconCabecEnviar").addClass("fa-mail-forward");
                document.getElementById("lblCabecAprovacao").innerHTML = "Enviar para aprovação";
                document.getElementById('lblCorpoAprovacao').innerHTML = 'Preencha a observação para enviar para aprovação.<br /> (assim ajuda o "aprovador" a identificar o que foi incluído/alterado)';
                document.getElementById('divLabelObs').style.display = "block";
                document.getElementById('<%=txtObsAprovacao.ClientID%>').style.display = "block";
                document.getElementById('btnEnviarAprovacaoFake').style.display = "block";
                $('#divAprovacao').modal();
            }
            else if (qEvento == 'Aprovar') {
                $('#divCabecAprovacao').addClass("bg-success");
                $("#iconCabecEnviar").addClass("fa-thumbs-o-up");
                document.getElementById("lblCabecAprovacao").innerHTML = "Aprovar";
                document.getElementById('lblCorpoAprovacao').innerHTML = 'Deseja Aprovar a publicação do Documento?';
                document.getElementById('<%=btnAprovar.ClientID%>').style.display = "block";
                $('#divAprovacao').modal();
            }
            else if (qEvento == 'Reprovar') {
                $('#divCabecAprovacao').addClass("bg-danger");
                $("#iconCabecEnviar").addClass("fa-thumbs-o-down");
                document.getElementById("lblCabecAprovacao").innerHTML = "Reprovar";
                document.getElementById('divLabelObs').style.display = "block";
                document.getElementById('lblCorpoAprovacao').innerHTML = 'Preencha a observação para reprovação.<br /> (assim ajuda o "usuário" a identificar o porquê foi Reprovado)';
                document.getElementById('<%=txtObsAprovacao.ClientID%>').style.display = "block";
                document.getElementById('btnReprovarFake').style.display = "block";
                $('#divAprovacao').modal();
            }
        }

        function fEnviarAprovacao() {
           
            if (document.getElementById('<%=txtObsAprovacao.ClientID%>').value == "") {
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass("alert-danger");
                document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = "ATENÇÃO"
                document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = "Deve-se preencher campo 'Observação' para descrever a alteração realizada."
                $('#divMensagemModal').modal();
                return;
            }
            document.getElementById('<%=btnEnviarAprovacao.ClientID%>').click();
        }

        function fReprovar() {
           
            if (document.getElementById('<%=txtObsAprovacao.ClientID%>').value == "") {
                $("#divCabecalho").removeClass("alert-success");
                $("#divCabecalho").removeClass("alert-warning");
                $("#divCabecalho").removeClass("alert-danger");
                $('#divCabecalho').addClass("alert-danger");
                document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = "ATENÇÃO"
                document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = "Deve-se preencher campo 'Observação' para descrever o porquê da Reprovação."
                $('#divMensagemModal').modal();
                return;
            }
            document.getElementById('<%=btnReprovar.ClientID%>').click();
        }

        function fLocalizaArquivo() {
            document.getElementById("<%=fileArquivoParaGravar.ClientID%>").click();
        }

        function fSelecionouArquivo(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "DOCX" || vFileExt.toUpperCase() == "DOC" || vFileExt.toUpperCase() == "PDF") {

                if (idFile.files && idFile.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        document.getElementById('<%=txtNomeArquivoPreview.ClientID%>').value = idFile.files[0].name;
                    }

                    reader.readAsDataURL(idFile.files[0]);
                }

                $("#<%=fileArquivoParaGravar.ClientID%>").change(function () {
                    fSelecionouImagem(this);
                });

            } else {
                document.getElementById("divTamanhoNovaUnidade").innerHTML = "";
                document.getElementById('divExtencaoUnidade').style.display = 'none';
                document.getElementById('divTamanhoUnidade').style.display = 'block';
                document.getElementById('idRowMensagensArquivoInvalidoUnidade').style.display = 'block';
            }
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
        }

       <%-- function fMostrarProgresso1()
        {
            document.getElementById('<%=UpdateProgress1.ClientID%>').style.display = "block";
        }--%>

        //================================================================================
        
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

        function isValidDate(dateString) {
            // First check for the pattern
            var regex_date = /^\d{4}\-\d{1,2}\-\d{1,2}$/;

            if (!regex_date.test(dateString)) {
                return false;
            }

            // Parse the date parts to integers
            var parts = dateString.split("-");
            var day = parseInt(parts[2], 10);
            var month = parseInt(parts[1], 10);
            var year = parseInt(parts[0], 10);

            // Check the ranges of month and year
            if (year < 1000 || year > 3000 || month == 0 || month > 12)

                return false;

            var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            // Adjust for leap years
            if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                monthLength[1] = 29;

            // Check the range of the day
            return day > 0 && day <= monthLength[month - 1];
        };

        function fDiaSemana(elemento) {
            var semana = ["domingo", "segunda-feira", "terça-feira", "quarta-feira", "quinta-feira", "sexta-feira", "sábado"];

                var data = elemento.value;
                var arr = data.split("-");
                var teste = new Date(arr[0], arr[1] - 1, arr[2]);
                var dia = teste.getDay();
                //alert(arr[0]);
                //alert(arr[1]);
                //alert(arr[2]);
                return semana[dia];
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                
            }
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;

        function AbreModalMensagem(qClass) {
            //$('#divApagar').hide();
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $("#divCabecalho").removeClass("alert-danger");
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
