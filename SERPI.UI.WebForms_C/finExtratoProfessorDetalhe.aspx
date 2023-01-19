<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finExtratoProfessorDetalhe.aspx.cs" Inherits="SERPI.UI.WebForms_C.finExtratoProfessorDetalhe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liFinanceiro" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liExtratoProfessor" />

    <link href="Content/dataTables.bootstrap.css" rel="stylesheet" />
<%--    <link href="Content/jquery.datatable.1.10.13.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigoProfessor"  name="hCodigoProfessor" value="value" />
    <%--<input type="hidden" id ="hTituloPagina"  name="hTituloPagina" value="Professor (Listagem)" />--%>

    <!-- iCheck for checkboxes and radio inputs -->
    <link href="plugins/iCheck/minimal/blue.css" rel="stylesheet" />
    <script src="plugins/iCheck/icheck.min.js"></script>
      
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

        #ContentPlaceHolderBody_grdResultadoOcorrencia td.centralizarTH {
            vertical-align: middle;  
        }

        #ContentPlaceHolderBody_grdResultadoSolicitacao td.centralizarTH {
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

    <div class="row"> 
        <div class="col-md-9">
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Extrato do Professor </strong> (Detalhe)</h3>
        </div>

        <div class="col-md-3">

        </div>
    </div>
    <br />

    <div class="container-fluid">

        <br />
        <hr />
        <br />

        <div class="row">
            <div class="col-md-1 center-block">
                <%--<a href='javascript:fExibeImagem()'>--%>
                    <img runat="server" id="imgProfessor" src="img/pessoas/avatarunissex.jpg" class="img-rounded center-block" alt="Imagem do Professor" style="width: 80px; height: 80px;" />
                <%--</a>--%>
                <hr style="height:3pt; visibility:hidden;" />
            </div>
            <div class="hidden-lg hidden-md">
                <br />
            </div>

            <div class="col-md-2">
                <h3 class="">
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloCodigo" runat="server" Text="Código"></asp:Label><br /> </span>&nbsp;<asp:Label ID="lblNumeroCodigo" runat="server" Text="0000"></asp:Label>
                </h3>
            </div>

            <div class="col-md-5">
                <h3 class="">
                    &nbsp;<span class ="text-sm"> <asp:Label ID="lblTituloProfessor_a" runat="server" Text="Professor"></asp:Label> </span><br />&nbsp;<asp:Label ID="lblTituloNomeProfessor" runat="server" Text="Label"></asp:Label>
                </h3>
            </div>

            <div class="col-md-4">
                <h3 class="">
                    <br />
                    &nbsp;<asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label>
                </h3>
            </div>

            <%--<div class="col-md-1">
                
            </div>--%>
        </div>
        <br />
        <hr />
        <br />

        <div class="row">
            <div class="panel panel-primary">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4 text-center">
                                            <h3>Saldo Ocorrências</h3><br />
                                            <label runat="server" id="lblTotalOcorrencia" style="font-size:x-large;color:royalblue"></label>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-4 text-center">
                                            <h3>Saldo Pagamentos</h3><br />
                                            <label runat="server" id="lblTotalPagamento" style="font-size:x-large;color:orangered;"></label>
                                        </div>

                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-4 text-center">
                                            <h3>Saldo Total</h3><br />
                                            <label runat="server" id="lblTotalGeral" style="font-size:xx-large;font-weight:bold"></label>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <span>Saldo Total = </span>(Horas Aula + Orientações) - (Solicitações com status "pago")<br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>
                        <div class="col-md-6">

                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <span>Observações</span><br />
                                            <textarea runat="server" style="resize: vertical; font-size: 14px" class="form-control" rows="6" maxlength="5500" id="txtObservacaoExtratoProfessor"></textarea>
                                        </div>
                                        <div class="hidden-lg hidden-md">
                                            <br />
                                        </div>

                                        <div class="col-md-2">
                                            <span></span>
                                            <br />
                                            <button type="button" runat="server" id="btnObservacoesExtratoProfessor" name="btnObservacoesExtratoProfessor" class="btn btn-success" onserverclick="btnObservacoesExtratoProfessor_Click">
                                                <%--onclick="window.history.back()"--%>
                                                <i class="fa fa-check"></i>&nbsp;Salvar <br />Observações</button>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divResultados" class="row" runat="server" visible="false">

            <div class="panel panel-primary">

                <div class="panel-body">

                    <div class="row">
                        <div class="col-md-6">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h2>Ocorrências</h2>
                                        </div>
                                        <div class="col-md-6 hidden">
                                            <span></span><br />
                                            <div class="row center-block btn-default form-group">
                                                <div class="col-md-4">
                                                <asp:RadioButton GroupName="GrupoProvaProficienciaIngles" ID="optSemBanca" runat="server" Checked="true"/>
                                                &nbsp;
                                                <label class="opt" for="<%=optSemBanca.ClientID %>">sem Banca</label>
                                                </div>
                                
                                                <div class="col-md-4">                    
                                                <asp:RadioButton GroupName="GrupoProvaProficienciaIngles" ID="optComBanca" runat="server" />
                                                &nbsp;
                                                <label class="opt" for="<%=optComBanca.ClientID %>">com Banca</label>
                                                </div>

                                                <div class="col-md-4">
                                                <asp:RadioButton GroupName="GrupoProvaProficienciaIngles" ID="optSoBanca" runat="server" />
                                                &nbsp;
                                                <label class="opt" for="<%=optSoBanca.ClientID %>">só Banca</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 hidden">
                                            <span></span><br />
                                            <button type="button" runat="server"  id="btnOkBanca" name="btnVoltar" class="btn btn-success" onserverclick="btnOkBanca_Click"> <%--onclick="window.history.back()"--%>
                                                <i class="fa fa-check"></i>&nbsp;Ok</button>
                                        </div>
                                    </div>
                                    
                                    <div class="grid-content">
                                        <div runat="server" id="msgSemResultadosOcorrencia" visible="true">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhuma Ocorrência encontrada" />
                                            </div>
                                        </div>
                                        <div class="table-responsive ">

                                            <asp:GridView ID="grdResultadoOcorrencia" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <asp:BoundField DataField="id_plano" HeaderText="N.º Ocorrência" ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:BoundField DataField="mes" HeaderText="Mês"  ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" />

                                                    <asp:BoundField DataField="motivo" HeaderText="Ocorrência"  ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Left"/>

                                                    <asp:TemplateField HeaderText="Valor Atual" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" >
                                                        <ItemTemplate>
                                                             <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "valor_atual")).ToString("#,###,###,##0.00")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="valor_atual" HeaderText="Valor Atual"  ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center"/>--%>

                                                    <asp:BoundField DataField="valor_solicitado" HeaderText="Valor Solicitado" ItemStyle-HorizontalAlign="Center" HtmlEncode="false"/>

                                                    <asp:BoundField DataField="data_solicitacao" HeaderText="Data Solicitação" ItemStyle-HorizontalAlign="Center" HtmlEncode="false"/>

                                                </Columns>

                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                            </asp:GridView>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <h2>Solicitações/Pagamentos</h2>
                                    <div class="grid-content">
                                        <div runat="server" id="msgSemResultadosSolicitacao" visible="true">
                                            <div class="alert bg-gray">
                                                <asp:Label runat="server" ID="Label1" Text="Nenhuma Solicitação encontrada" />
                                            </div>
                                        </div>
                                        <div class="table-responsive ">

                                            <asp:GridView ID="grdResultadoSolicitacao" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                                SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                                <Columns>

                                                    <%--<asp:BoundField DataField="id_solicitacao" HeaderText="N.º Solicitação" ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" />--%>

                                                    <asp:BoundField DataField="id_plano" HeaderText="N.º Ocorrência" ItemStyle-HorizontalAlign="Center" HtmlEncode="false"/>

                                                    <asp:BoundField DataField="motivo" HeaderText="Ocorrência" ItemStyle-HorizontalAlign="Left" HtmlEncode="false"/>

                                                    <%--<asp:BoundField DataField="data_solicitacao" HeaderText="Data Solicitação"  ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" />--%>

                                                    <asp:TemplateField HeaderText="Data Solicitação" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" >
                                                        <ItemTemplate>
                                                             <%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "data_solicitacao"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="valor" HeaderText="Valor"  ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center"/>--%>

                                                    <asp:TemplateField HeaderText="Valor Solicitado" ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center" >
                                                        <ItemTemplate>
                                                             <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "valor")).ToString("#,###,###,##0.00")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="nota_fiscal" HeaderText="Nota Fiscal"  ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Center"/>

                                                    <asp:BoundField DataField="status" HeaderText="Status"  ItemStyle-CssClass="centralizarTH" ItemStyle-HorizontalAlign="Left"/>

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

        

        <br />

        <div class="row">
            <div class="pull-left">
                <div class="col-md-12">

                    <button type="button" runat="server"  id="btnVoltar" name="btnVoltar" class="btn btn-default" onserverclick="btnVoltar_Click" > <%--onclick="window.history.back()"--%>
                        <i class="glyphicon glyphicon-step-backward"></i>&nbsp;Voltar</button>

                </div>
            </div>
        </div>
        <br />
    </div>

    <div id="divPogress" class="loading" align="center">
        Processando... <br />Por favor, aguarde.
        <br />
        <img src="img/loader.gif" width="42" height="42" alt="" />
    </div>

    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header bg-blue">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
            <h4 class="modal-title" id="myModalLabel"><label id="labelNomeExibeImagem">test</label></h4>
          </div>
          <div class="modal-body text-center">
            <img src="" id="imagepreview" class="img-responsive center-block"  > <%--style="width: 400px; height: 300px;"--%>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
          </div>
        </div>
      </div>
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
    <script src="https://cdn.datatables.net/plug-ins/1.10.16/sorting/date-euro.js"></script>
    
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

        $(document).ready(function () {

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseArea: '20%' // optional
            });

            $('#<%=grdResultadoOcorrencia.ClientID%>').dataTable(
                {
                    stateSave: false,
                    "bProcessing": true,
                    columnDefs: [
                        {
                            "targets": [ 0, 1, 2, 3, 4, 5],
                            "orderable": false
                        }
                    ]
                });

            $('#<%=grdResultadoSolicitacao.ClientID%>').dataTable(
                {
                    stateSave: false,
                    "bProcessing": true,
                    columnDefs: [
                        {
                            "targets": [ 0, 1, 3, 4],
                            "orderable": false
                        },
                        {
                                "targets": [2],
                                type: 'date-euro'
                        }
                    ],
                    order: [[2, 'asc']]
                });
            
        });


        $(".fecha_grid_resultados").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });

        function AbreModalDesativaProfessorOffLine(qProfessor, qNome) {
            //$('#divCabecalho').toggleClass(qClass);
            document.getElementById('labelCodigoProfessor').innerHTML = qProfessor;
            document.getElementById('labelNomeProfessor').innerHTML = qNome;
            document.getElementById('hCodigoProfessor').value = qProfessor;
            $('#divModalDesativaProfessorOffline').modal();
            //alert("Hello world");
        }

        function AbreModalAtivaProfessorOffLine(qProfessor, qNome) {
            //$('#divCabecalho').toggleClass(qClass);
            document.getElementById('labelCodigoProfessorReativar').innerHTML = qProfessor;
            document.getElementById('labelNomeProfessorReativar').innerHTML = qNome;
            document.getElementById('hCodigoProfessor').value = qProfessor;
            $('#divModalAtivaProfessorOffline').modal();
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
            //$('#divApagar').hide();
            $("#divCabecalho").removeClass("alert-success");
            $("#divCabecalho").removeClass("alert-warning");
            $("#divCabecalho").removeClass("alert-danger");
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
            //alert("Hello world");
        }

        function fExibeImagem(qId, qNome) {
            $('#imagepreview').attr('src', "img\\pessoas\\" + qId + "?" + new Date()); // here asign the image to the modal when the user click the enlarge link
            document.getElementById('labelNomeExibeImagem').innerHTML = qNome;
            $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
        }

    </script>

</asp:Content>
