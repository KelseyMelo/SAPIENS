<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="telaVideoGestao.aspx.cs" Inherits="SERPI.UI.WebForms_C.telaVideoGestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liMenuMonitorGrupo" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liMonitorVideo" />

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
        <div class="col-md-8">
            <h3 class=""><i class="fa fa-circle-o text-red"></i>&nbsp;<strong >Vídeo</strong> <asp:Label ID="lblTituloPagina" runat="server" Text="(novo)"></asp:Label><asp:Label ID="lblInativado" ForeColor="Red" runat="server" Text=" (Inativado)"></asp:Label></h3>
            <asp:Label  ID="lblId" runat="server" CssClass="hidden"></asp:Label>
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button"  runat="server" id="btnCriarVideo" name="btnCriarVideo" class="btn btn-primary pull-right" href="#" onclick="" onserverclick="btnCriarVideo_Click" > <%--onserverclick="btnCriarVideo_Click"--%>
                    <i class="fa fa-magic"></i>&nbsp;Criar novo Vídeo</button>

        </div>
        <div class="hidden-lg hidden-md">
            <br /> 
        </div>

        <div class="col-md-2">
            <br />
            <button type="button" runat="server" id="btnSalvar" name="btnSalvar" class="btn btn-success pull-right" href="#" onclick="if (fProcessando()) return;" onserverclick="btnSalvar_ServerClick"><%--onserverclick="btnSalvar_ServerClick"--%>
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
                                <div class="col-md-10 ">
                                    <span>Descrição </span><span style="color:red;">*</span><br />
                                    <textarea style ="resize:vertical; font-size:14px" runat ="server" id="txtDescVideoMonitor" class="form-control input-sm" rows="2" maxlength="100"></textarea>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-6 ">
                                    <span>Nome do Arquivo </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtNomeArquivoVideoMonitor" type="text" value="" readonly="true" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Ordem de Apresentação (decrescente) </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtOrdemVideoMonitor" type="number" min="1" />
                                </div>

                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-2 ">
                                    <span>Data Início Vídeo </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDataInicioVideo" type="date"  value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Hora Início Vídeo </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtHoraInicioVideo" type="time" value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Data Fim Vídeo </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtDataFimVideo" type="date"  value="" />
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-2 ">
                                    <span>Hora Fim Vídeo </span><span style="color:red;">*</span><br />
                                    <input class="form-control input-sm" runat="server" id="txtHoraFimVideo" type="time" value="" />
                                </div>

                            </div>
                            <br />
                            <br />

                            <div class="row">
                                <div class="col-md-10 text-center">
                                    <span>Video </span><span style="color:red;">*</span><br />
                                    <video id="idVideo" width="700" height="400" runat="server" controls><%--controls autoplay--%>
<%--                                <source src="https://www.w3schools.com/html/mov_bbb.mp4" type="video/mp4"/>
                                        <source src="./videos/Vídeo institucional do IPT 2014.mp4" type="video/mp4"/>--%>
                                        <%--<source src="./videos/Pós-Graduação em Gestão da Inovação Tecnológica e Negócio.mp4" type="video/mp4"/>--%>
                                        <%--Your browser does not support the video tag.--%>
                                    </video>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-10 ">
                                    <button type="button" runat="server"  id="btnNovoVideo" name="btnNovoVideo" class="btn btn-primary center-block " onclick="javascript:fLocalizaVideo()" > <%--onserverclick="btnVoltar_Click"--%>
                                        <i class="fa fa-video-camera"></i>&nbsp;Localizar novo Vídeo
                                    </button>
                                </div>
                            </div>
                        </div>
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
        <asp:FileUpload ID="fileArquivoParaGravar" runat="server" accept=".mp4,.ogg" onchange="javascript:fSelecionouArquivo(this);"  />
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

        });

        function fLocalizaVideo() {
            document.getElementById("<%=fileArquivoParaGravar.ClientID%>").click();
        }

        function fSelecionouArquivo(idFile) {
            var vFileExt = idFile.value.split('.').pop();
            if (vFileExt.toUpperCase() == "MP4" || vFileExt.toUpperCase() == "OGG") {

                if (idFile.files && idFile.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('#<%=idVideo.ClientID%>').attr('src', e.target.result);
                        document.getElementById('<%=txtNomeArquivoVideoMonitor.ClientID%>').value = idFile.files[0].name;
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
