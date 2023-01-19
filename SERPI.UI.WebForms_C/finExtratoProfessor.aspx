<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="finExtratoProfessor.aspx.cs" Inherits="SERPI.UI.WebForms_C.finExtratoProfessor" %>
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
            <h3 class=""><i class="fa fa-circle-o text-green"></i>&nbsp;<strong >Extrato do Professor </strong> (Listagem)</h3>
        </div>

        <div class="col-md-3">

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
                            <span>Identificação</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtIdProfessor" type="number" value="" maxlength="18" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-7">
                            <span>Nome</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtNomeProfessor" type="text" value="" maxlength="150" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-2">
                            <span>CPF</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtCPFProfessor" type="text" value="" maxlength="50" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4">
                            <span>Situação</span><br />
                            <div class="row center-block btn-default form-group">
                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacaoProfessor" ID="optSituacaoProfessorTodos" runat="server"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoProfessorTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-4">                    
                                <asp:RadioButton GroupName="GrupoSituacaoProfessor" ID="optSituacaoProfessorSim" runat="server" Checked="true"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoProfessorSim.ClientID %>">Ativo</label>
                                </div>

                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacaoProfessor" ID="optSituacaoProfessorNao" runat="server" />
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoProfessorNao.ClientID %>">Inativo</label>
                                </div>
                            </div>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2"></div>

                        <div class="col-md-1">
                            <%--<span>&nbsp;</span><br />--%>

                            <div class="hidden-xs hidden-sm">
                                <span>&nbsp;</span><br />
                            </div>

                            <button  runat="server" id="bntPerquisaProfessor" name="bntPerquisaProfessor" onserverclick ="btnPerquisaProfessor_Click" title="" class="btn btn-success pull-right hidden " href="#">
                                <i class="glyphicon glyphicon-ok"></i>&nbsp;OK</button>

                            <a id="aBntPerquisaProfessor" runat ="server" onclick="ShowProgress()" onserverclick="btnPerquisaProfessor_Click" href="#" class ="btn btn-success pull-right"><i class="glyphicon glyphicon-ok"></i><span>&nbsp;OK</span></a>
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Professor encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_professor"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "professor")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Foto" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%# setLinkImagem(DataBinder.Eval(Container.DataItem, "cpf").ToString(), DataBinder.Eval(Container.DataItem, "professor").ToString())%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="id_professor" HeaderText="Identificação" ItemStyle-HorizontalAlign="Center" />

                                            <asp:BoundField DataField="professor" HeaderText="Nome" />

                                            <asp:BoundField DataField="cpf" HeaderText="CPF" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"/>

                                            <%--<asp:BoundField DataField="numero_documento" HeaderText="Doc. de Identificação" ItemStyle-HorizontalAlign="Center" />--%>

                                            <%--<asp:BoundField DataField="email" HeaderText="Email" />--%>

                                            <asp:TemplateField HeaderText="Situação" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "status").ToString() == "inativado" ? "<div class='text-danger'><strong>INATIVO</strong></div>" : "Ativo"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Saldo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH" HeaderStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#setSaldo(Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"plano")), Convert.ToDecimal(DataBinder.Eval(Container.DataItem,"pagamento")))    %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Selecionar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-search-plus btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="ShowProgress()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Selecionar" ID="btnStart" runat="server" Text="" OnCommand="grdResultado_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/dataTables.bootstrap.min.js"></script>
<%--    <script src="Scripts/jquery.datatable.1.10.13.min.js"></script>--%>

    
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
            
        });

        function teclaEnter() {
            if (event.keyCode == "13") {
                //alert('oi');
                document.getElementById("<%=aBntPerquisaProfessor.ClientID%>").click();
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

        $(".fecha_grid_resultados").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });


        $(document).ready(function () {
            $('#<%=txtCPFProfessor.ClientID%>').mask('999.999.999-99');

            $('#<%=grdResultado.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, columnDefs: [{ type: 'locale-compare', targets: 3 }] });

            fechaLoading();

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
            $('#divApagar').hide();
            $('#divCabecalho').toggleClass(qClass);
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
