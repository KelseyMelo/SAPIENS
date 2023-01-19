<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="cadOferecimento.aspx.cs" Inherits="SERPI.UI.WebForms_C.cadOferecimento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAcademico" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="li8Oferecimentos" />

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

            #ContentPlaceHolderBody_grdResultado td.centralizarTH {
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
        <div class="col-md-9">
            <h3 class=""><i class="fa fa-circle-o text-primary"></i>&nbsp;<strong >Oferecimento</strong> (Listagem)</h3>
        </div>

        <div class="col-md-3">
            <br />
            <div class ="pull-right ">
                <button type="button" runat="server" id="btnCriarOferecimento" name="btnCriarOferecimento" onserverclick="btnCriarOferecimento_Click" class="btn btn-success" href="#" onclick=""  > <%--onserverclick="btnCriarOferecimento_Click"--%>
                        <i class="fa fa-magic"></i>&nbsp;Criar Oferecimento</button>
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
                            <span>Id Ofer.</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtIdOferecimento" type="number" min="0" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span>Código da Disciplina</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtCodigoDisciplinaOferecimento" type="text" value="" maxlength="7" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4">
                            <span>Nome da Disciplina</span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtNomeDisciplinaOferecimento" autocomplete="off" type="text" value="" maxlength="150" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span>Data Início <small>(aula)</small></span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtDataInicioAula" type="date" value="" />
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-2">
                            <span>Data Fim <small>(aula)</small></span><br />
                            <input class="form-control input-sm fecha_grid_resultados" runat="server" id="txtDataFimAula" type="date" value="" />
                        </div>
                        
                    </div>
                    <br />

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3">
                                    <span>Tipo Curso</span><br />
                                    <asp:DropDownList runat="server" ID="ddlTipoCursoOferecimento" onchange="fMostrarProgresso1()" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2 SemPesquisa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCursoOferecimento_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-5">
                                    <span>Curso</span><br />
                                    <asp:DropDownList runat="server" ID="ddlNomeCursoOferecimento" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2">
                                    <span>Período</span><br />
                                    <asp:DropDownList runat="server" ID="ddlPeriodoOferecimento" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-1" style="line-height: 1.9em;">
                                    &nbsp;<br />
                                    <asp:CheckBox ID="chkAtivoOferecimento" runat="server" Checked="true"/>
                                    &nbsp;
                                    <label style="font-weight:normal" class="opt" for="<%=chkAtivoOferecimento.ClientID %>">Ativo</label>
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoCursoOferecimento" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />

                    <div class="row">
                        <div class="col-md-6">
                            <span>Professor</span><br />
                            <asp:DropDownList runat="server" ID="ddlProfessorOferecimento" ClientIDMode="Static" class="ddl_fecha_grid_resultados form-control input-sm select2" AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                        <div class="hidden-lg hidden-md">
                            <br />
                        </div>

                        <div class="col-md-4">
                            <span>Situação</span><br />
                            <div class="row center-block btn-default form-group" style="line-height: 1.9em;">
                                <div class="col-md-4">
                                <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoTodos" runat="server" CssClass="fecha_grid_resultados"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoTodos.ClientID %>">Todos</label>
                                </div>
                                
                                <div class="col-md-4">                    
                                <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoSim" runat="server" Checked="true" CssClass="fecha_grid_resultados"/>
                                &nbsp;
                                <label class="opt" for="<%=optSituacaoSim.ClientID %>">Ativo</label>
                                </div>

                                <div class="col-md-4">
                                    <asp:RadioButton GroupName="GrupoSituacao" ID="optSituacaoNao" runat="server" CssClass="fecha_grid_resultados"/>
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
                            
                            <a id="aBntPerquisaAluno" runat ="server" onclick="fProcessando()" onserverclick="btnPerquisaOferecimento_Click"  href="#" class ="btn btn-success pull-right"><i class="fa fa-check"></i><span>&nbsp;OK</span></a> <%--onserverclick="bntPerquisaAluno_Click"--%>
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
                                        <asp:Label runat="server" ID="lblMsgSemResultados" Text="Nenhum Oferecimento encontrado" />
                                    </div>
                                </div>
                                <div class="table-responsive ">

                                    <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        AllowPaging="True" PageSize="1000000" AllowSorting="true" DataKeyNames="id_Oferecimento"
                                        SkinID="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <%--<AlternatingRowStyle BackColor="#000022" />--%>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Ordem" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "quadrimestre") + DataBinder.Eval(Container.DataItem, "disciplinas.codigo").ToString() + DataBinder.Eval(Container.DataItem, "num_oferecimento").ToString() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <span style="position: relative;">
                                                        <i class="fa fa-edit btn btn-primary btn-circle"></i>
                                                        <asp:Button OnClientClick="fProcessando()" HorizontalAlign="Center" CssClass="movedown" HeaderText="Editar" ID="btnStart" runat="server" Text="" OnCommand="grdResultado_Command" CommandName="StartService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="id_Oferecimento" HeaderText="Id Ofer." ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="quadrimestre" HeaderText="Período" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="centralizarTH"/>

                                            <%--<asp:BoundField DataField="disciplinas.areas_concentracao.cursos.tipos_curso.tipo_curso" HeaderText="Tipo Curso" ItemStyle-HorizontalAlign="left"/>--%>

                                            <asp:TemplateField HeaderText="Tipo Curso - Sigla" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# setTipoMestrado(DataBinder.Eval(Container.DataItem, "disciplinas.cursos_disciplinas"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="disciplinas.codigo" HeaderText="Código"  ItemStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="disciplinas.nome" HeaderText="Disciplina"  ItemStyle-CssClass="centralizarTH"/>

                                            <asp:BoundField DataField="num_oferecimento" HeaderText="Oferecimento" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH"/>

                                            <asp:TemplateField HeaderText="Ativo" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ativo")) == 1 ? "Sim" : "Não"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Situação" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="centralizarTH">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "status").ToString() == "inativado" ? "<div class='text-danger'><strong>INATIVO</strong></div>" : "Ativo"%>
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
        $('.ddl_fecha_grid_resultados').on('select2:select', function (e) {
            $('#<%=divResultados.ClientID%>').hide();
        });

        $(document).ready(function () {

            fSelect2();

            $('input').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue',
                increaseOferecimento: '20%' // optional
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

            $('#<%=ddlTipoCursoOferecimento.ClientID%>').on("select2:select", function(e) { 
                 $('#<%=divResultados.ClientID%>').hide();
            });

            $('#<%=ddlNomeCursoOferecimento.ClientID%>').on("select2:select", function(e) { 
                 $('#<%=divResultados.ClientID%>').hide();
            });

            $('#<%=ddlPeriodoOferecimento.ClientID%>').on("select2:select", function(e) { 
                 $('#<%=divResultados.ClientID%>').hide();
            });

            $('#<%=ddlProfessorOferecimento.ClientID%>').on("select2:select", function(e) { 
                 $('#<%=divResultados.ClientID%>').hide();
            });
        }

        function teclaEnter() {
            if (event.keyCode == "13") {
                //funcPesquisar();
                //alert('oi');

                document.getElementById("<%=aBntPerquisaAluno.ClientID%>").click();
                
               <%-- alert('oi2');
               $('#<%=aBntPerquisaAluno.ClientID%>').click();
                alert('oi3');

                $('#<%=aBntPerquisaAluno.ClientID%>').trigger("click")
                alert('oi4');--%>

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

        //}


        $(".alteracao").keydown(function () {
            //alert("The text has been changed.");
            $('#<%=divResultados.ClientID%>').hide();
        });


        $(document).ready(function () {

            $('#<%=grdResultado.ClientID%>').dataTable({ stateSave: true, "bProcessing": true, order: [[0, 'desc']], });

            fechaLoading();


            //var table = document.getElementById(<%//=grdEvidencia.ClientID%>);
            //var rows = table.getElementsByTagName("tr");
            //for (i = 0; i < rows.length; i++) {
            //    //manipulate rows 
            //    if (i % 2 == 0) {
            //        
            //        rows[i].className = "even";
            //    } else {
            //        rows[i].className = "odd";
            //    }
            //}

        });


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

    </script>

</asp:Content>
