<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finPessoaJuridicaGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.finPessoaJuridicaGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liPessoaJuridica" />

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
        <div class="col-md-6">
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Fornecedor</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(nova)"></asp:Label><asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label></h3>
        </div>
         <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2 ">
            <br />
            <button type="button" runat="server" id="btnInativar" class="btn btn-danger hidden" href="#" onclick="fModalAtivaInativa('Inativa')">
                <i class="fa fa-toggle-off"></i> Inativar Fornecedor
            </button>
            <button type="button" runat="server" id="btnAtivar" class="btn btn-info hidden" href="#" onclick="fModalAtivaInativa('Ativa')">
                <i class="fa fa-toggle-on"></i> Ativar Fornecedor
            </button>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button" runat="server" id="btnCriarFornecedor" name="btnCriarFornecedor" onserverclick="btnCriarFornecedor_Click" class="btn btn-primary" href="#" onclick="">
                    <i class="fa fa-magic"></i>&nbsp;Criar Fornecedor</button>

        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button" runat="server" id="bntSalvar2" name="bntSalvar2" class="btn btn-success pull-right" href="#" onclick="if (ShowProgress()) return;" onserverclick="btnSalvar_ServerClick">
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>
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
                                    <div class="col-md-2 ">
                                        <span>Data de Cadastro</span><br />
                                        <input class="form-control input-sm" runat="server" id="txtDataCadastro" type="text" readonly="true"/>
                                    </div>
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>

                                    <div class="col-md-2 ">
                                        <span>Status</span><br />
                                        <input class="form-control input-sm" runat="server" id="txtStatus" type="text" readonly="true"/>
                                    </div>
                                    <div class="hidden-lg hidden-md">
                                        <br />
                                    </div>

                                    <div class="col-md-2 ">
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
                                <div class="col-md-4 ">
                                    <span>Empresa </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNomeFornecedor" type="text" value="" maxlength="200"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>CNPJ </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCNPJFornecedor" type="text" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Inscrição Estadual </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtIEFornecedor" type="text" value=""/>
                                </div>
                                
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-4 ">
                                    <span>Logradouro </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtLogradouroFornecedor" type="text" value="" maxlength="200"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Número </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNumeroFornecedor" type="text" value="" maxlength="10"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3 ">
                                    <span>Complemento </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtComplementoFornecedor" type="text" value="" maxlength="100"/>
                                </div>
                                
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-4 ">
                                    <span>Bairro </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtBairroFornecedor" type="text" value="" maxlength="100"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4 ">
                                    <span>Cidade </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCidadeFornecedor" type="text" value="" maxlength="100"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Estado </span><br />
                                    <asp:DropDownList runat="server" ID="ddlEstadoFornecedor" ClientIDMode="Static" class="form-control input-sm select2">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <br />

                            <div class="row">
                                <div class="col-md-2 ">
                                    <span>CEP </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCepFornecedor" type="text" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4 ">
                                    <span>Nome Contato </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNomeContatoFornecedor" type="text" value="" maxlength="100"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3 ">
                                    <span>Cargo </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCargoFornecedor" type="text" value="" maxlength="100"/>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-2 ">
                                    <span>Telefone </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtTelefoneFornecedor" type="text" value=""/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Fax </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtFaxFornecedor" type="text" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Celular </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtCelularFornecedor" type="text" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4 ">
                                    <span>Email </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtEmailFornecedor" type="email" value="" maxlength="200"/>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-3 ">
                                    <span>Nome do Banco </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNomeBancoFornecedor" type="text" value="" maxlength="50"/>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>N.º do Banco </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNumeroBancoFornecedor" type="number" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>N.º Agência </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtAgenciaFornecedor" type="text" value="" maxlength="50" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4 ">
                                    <span>N.º Conta </span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNumeroContaFornecedor" type="text" value="" maxlength="50"/>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">

            <div class="col-xs-2">
                <button type="button" runat="server" id="btnVoltar" name="btnVoltar" class="btn btn-default" onclick="" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>
            </div>

            <button type="button" runat="server" id="bntSalvarNoticia" name="bntSalvar" class="btn btn-success pull-right" href="#" onclick="if (ShowProgress()) return;" onserverclick="btnSalvar_ServerClick">
                            <i class="fa fa-floppy-o"></i>&nbsp;Salvar Dados</button>

        </div>
    </div>

    <!-- Modal para Ativar/Inativar Fornecedor -->
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
                            <button id="btnConfirmaAtivar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarFornecedor('Ativar')">
                            <i class="fa fa-check"></i>&nbsp;Confirmar</button>
                            <button id="btnConfirmaInativar" type="button" class="btn btn-success pull-right" onclick="fAtivarInativarFornecedor('Inativar')">
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

        $(document).ready(function () {
            $("#<%=txtCNPJFornecedor.ClientID%>").mask("99.999.999/9999-99");
            $("#<%=txtCepFornecedor.ClientID%>").mask("99999-999");
            $("#<%=txtTelefoneFornecedor.ClientID%>").mask("99-9999-9999");
            $("#<%=txtFaxFornecedor.ClientID%>").mask("99-9999-9999");
            $("#<%=txtCelularFornecedor.ClientID%>").mask("99-99999-9999");
            //Temporariamente habilitado 
<%--            $("#<%=ddlTipoCurso.ClientID%>").val("1").trigger("change");
            $('#<%=ddlTipoCurso.ClientID%>').prop("disabled", true)--%>
            //Temporariamente habilitado
        });

        //============================================================================

        function fModalAtivaInativa(qOperacao) {
            if (qOperacao == 'Ativa') {
                $("#divCabecAtiva").removeClass("bg-danger");
                $('#divCabecAtiva').addClass('bg-info');
                document.getElementById("btnConfirmaAtivar").style.display = 'block';
                document.getElementById("btnConfirmaInativar").style.display = 'none';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-on"></i>&nbsp;Ativar Fornecedor';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja ativar o Fornecedor <strong>' + document.getElementById("<%=lblTituloPagina.ClientID%>").innerHTML + '</strong>?';
            }
            else {
                $("#divCabecAtiva").removeClass("bg-info");
                $('#divCabecAtiva').addClass('bg-danger');
                document.getElementById("btnConfirmaAtivar").style.display = 'none';
                document.getElementById("btnConfirmaInativar").style.display = 'block';
                document.getElementById("lblTituloAtiva").innerHTML = '<i class="fa fa-toggle-off"></i>&nbsp;Inativar Fornecedor';
                document.getElementById("lblCorpoAtiva").innerHTML = 'Deseja inativar o Fornecedor <strong>' + document.getElementById("<%=lblTituloPagina.ClientID%>").innerHTML + '</strong>?';
            }
            $('#divModalAtivaInativa').modal();
        }

        //===============================================================

        //=======================================

        function fAtivarInativarFornecedor(qOperacao){
            //alert(qOperacao);
            $.ajax({
                type: "post",
                url: "wsSapiens.asmx/fAtivarInativarFornecedor",
                contentType: 'application/json; charset=utf-8',
                data: "{qOperacao:'" + qOperacao + "'}",
                dataType: 'json',
                success: function (data, status) {
                    var vTitulo = '';
                    var vBg = '';
                    var vIcon = '';
                    if (qOperacao == "Ativar") {
                        vTitulo = "Fornecedor Ativado com sucesso";
                        vBg = "info";
                        vIcon = "fa fa-thumbs-o-up fa-2x";
                    }
                    else {
                        vTitulo = "Fornecedor Inativado com sucesso"
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
                        document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Problema na Ativação/Inativação do Fornecedor';
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
                        document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro para Ativar Fornecedor';
                        document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro para ativar do Fornecedor <br/> Erro: ' + err + '<br/>Status do erro: ' + status;
                    }
                    else {
                        document.getElementById('<%=lblTituloMensagem.ClientID%>').innerHTML = 'Erro para Inativar Fornecedor';
                        document.getElementById('<%=lblMensagem.ClientID%>').innerHTML = 'Erro para Inativar do Fornecedor <br/> Erro: ' + err + '<br/>Status do erro: ' + status;
                    }
                    
                    $('#divModalAtivaInativa').modal('hide')
                    $('#divMensagemModal').modal('show');
                }
            });

                
        }

        //=======================================

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

        function fModalAssociarDisciplina() {
            document.getElementById("divgrdDisciplinaDisponivel").style.display = "none";
            $('#divModalAssociarDisciplina').modal();
        }

        function AbreModalApagarCoordenador(qId, qCPF, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeCoordenador').innerHTML = qNome;
            document.getElementById('lblCPFCoordenador').innerHTML = qCPF;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirCoordenador').modal();
        }

        function AbreModalApagarDisciplina(qId, qCodigo, qNome) {
            //alert(qId + ' ' + qCPF + ' ' + qNome);
            document.getElementById('lblNomeDisciplina').innerHTML = qNome;
            document.getElementById('lblCodigoDisciplina').innerHTML = qCodigo;
            document.getElementById('hCodigo').value = qId;
            $('#divModalExcluirDisciplina').modal();
        }

        function AbreModalMensagem(qClass) {
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

    </script>

</asp:Content>
