<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="aluDocumentosAcademicos.aspx.cs" Inherits="SERPI.UI.WebForms_C.aluDocumentosAcademicos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAlunoDocumentosAcademicos" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liAlunoDocumentosAcademicos" />
    <input type="hidden" id ="hEscolheuFoto"  name="hEscolheuFoto" value="false" />

    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigoAluno"  name="hCodigoAluno" value="value" />

    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet" />
      
    <style type="text/css">

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

        input{
            font-size:14px;
        }

        input[type=email] {
           font-size:14px;
        }

        .img-redondo2{
            border-radius: 50% !important;
            width: 70px !important;
            height: 70px !important;
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

    <div class="row"> 
        <div class="col-md-9">
            <h3 class="">&nbsp;&nbsp;&nbsp;<i class="fa fa-book"></i>&nbsp;Informações Acadêmicas</h3>
        </div>

    </div>

    <div class="container-fluid">

        <div class="row">
            
            <br />
            <asp:Literal ID="litDocumentos" runat="server"></asp:Literal>

            <%--<div class="col-md-6">
                <div class="panel panel-success">
                    <div class="panel-heading" role="tab">
                        <h5 class="panel-title"><a class="collapsed a_faq" id="cab_0" data-toggle="collapse" href="#res_0" aria-expanded="false" onclick="fGoToVisualizacao(0,2731)">
                            <h6><i class="fa fa-file-word-o" style="color: #3588CC"></i> Documento Word</h6>
                            <h5 style="line-height: 1.5em"><strong>Previsão da tendência do preço de criptomoedas utilizando ensemble de  LSTMs</strong></h5>
                            <i id="icab_0" style="margin-top: -25px; color: #3588CC" class="fa fa-chevron-left pull-right rotate"></i>
                        </a></h5>
                    </div>
                    <div id="res_0" class="panel-collapse collapse" role="tabpanel" aria-labelledby="cab_0">
                        <div class="panel-body">
                            <p style="font-size: 1.3rem; line-height: 1.5em">
                                Orientação: <strong>REZENDE, Marcelo Novaes de</strong><br>
                                <br>
                            </p>
                            <div class="row">
                                <div class="col-xs-6">Ano: <strong>2019</strong>                    </div>
                                <div class="col-xs-6 text-right">
                                    visualizações: <strong>
                                        <label id="lblQtdVisualizacao_0">158</label></strong>
                                </div>
                                <div class="col-xs-12 text-right">
                                    <em>downloads</em>: <strong>
                                        <label id="lblQtdDownload_0">48</label></strong>
                                </div>
                            </div>
                            <br>
                            &nbsp;Algoritmos de machine learning têm se destacado atualmente em várias indústrias, tendo como exemplos: previsão em bolsa de valores, carros autônomos e reconhecimento de voz. Recentemente, a criptomoeda Bitcoin passou a ser um ativo negociado em corretoras e, dada sua natureza volátil e não linear, as redes neurais são geralmente indicadas para aprender seu padrão. Entre os vários tipos de redes neurais, o potencial da long short-term memory tem sido pesquisado na academia para previsão da direção do preço do Bitcoin, pois, além de ser desenhada para receber dados sequenciais, ela apresenta funcionalidades como a de memorizar eventos importantes ou esquecê-los quando não tão relevantes, e tais características são promissoras e podem trazer bons resultados. Um método que possibilita aumentar a acurácia de machine learning é o ensemble, que trabalha com um conjunto de algoritmos, em vez de apenas um, proporcionando, dessa maneira, uma opinião mais generalizada durante a predição. Essa abordagem já foi explorada para a previsão da tendência do preço do Bitcoin e melhorou a acurácia em um ensemble de redes multilayer perceptron. Os indicadores técnicos, como features, foram muito estudados para o mercado de ações, entretanto, para Bitcoin, ainda foram pouco explorados. Assim, neste estudo, é apresentado um método de ensemble de LSTMs para previsão da tendência do preço do Bitcoin que utiliza 15 indicadores técnicos como features. O método proposto foi implementado e obteve 64% de acurácia na previsão de 50 dias do dataset de back-testing, portanto mostrando-se atrativo, uma vez que o melhor modelo individual LSTM conseguiu 62% de acurácia, o que evidencia a vantagem do ensemble. O potencial preditivo dos indicadores técnicos também foi avaliado pelos modelos, os quais obtiveram de 52% a 62% de acurácia durante o back-testing. Por outro lado, o modelo com as métricas do preço do Bitcoin, isto é, sem indicadores técnicos, conseguiu apenas 50% de acurácia.               
                                <br>
                            <br>
                            <a class="btn btn-primary btn-outline" target="_blank" onclick="fGoToDownload(0,2731)" href="Teses\2019_EC_Ricardo_Brito.pdf"><b>Faça aqui o <em>download</em> da dissertação</b></a>
                            <p></p>
                        </div>
                    </div>
                </div>
            </div>--%>

        </div>
    </div>


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
        </div>
    </div>
    
    <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <script src="plugins/select2/i18n/pt-BR.js"></script>

    <script>

        $(".collapsed").click(function () {
            //alert($(this).get(0).id);
            $("#i" + $(this).get(0).id).toggleClass("down");
        })

        //=========================================================
        function fSelect2() {

            $(".select2").select2({
                theme: "bootstrap",
                language: "pt-BR",
            });

            $(".SemPesquisa").select2({
                theme: "bootstrap",
                minimumResultsForSearch: Infinity
            });
        }

        //=====================================       

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


        $(document).ready(function () {
            
        });

        function AbreModalMensagem(qClass) {
            $("#divCabecalho").removeClass("alert-warning");
            $("#divCabecalho").removeClass("alert-primary");
            $('#divCabecalho').removeClass('alert-danger');
            $('#divCabecalho').removeClass('alert-success');
            $('#divCabecalho').addClass(qClass);
            $('#divMensagemModal').modal();
        }

        function fAguarde() {

            $.notify({
                icon: 'fa fa-thumbs-o-up fa-2x',
                title: '<strong>Preparação do <em>download</em></strong><br /><br />',
                message: 'AGUARDE...',

            }, {
                type: 'info',
                delay: 1500,
                timer: 1500,
                z_index: 5000,
                animate: {
                    enter: 'animated flipInY',
                    exit: 'animated flipOutX'
                },
                placement: {
                    from: "bottom",
                    align: "left"
                }
            });
        }

    </script>

</asp:Content>
