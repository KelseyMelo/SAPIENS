<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_tcc.aspx.cs" Inherits="SERPI.UI.WebForms_C.index_tcc" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monografias</title>

    <style>
        @keyframes img-ani {
          from{opacity:0;}
          to{opacity: 1;}
        }

        .bannerRosto_interno {
            background: url('img/homepage/tcc.jpg');
            background-repeat: no-repeat;
            background-position: right -80px;
            overflow: hidden;
            background-size: cover;
            height: 50vh;
            background-attachment: fixed;
            animation-name:img-ani;
            animation-duration: 1s;   
            animation-timing-function: ease-in;  
        }

        #texto-img {
            /*text-align: center;*/
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 58vh;
            font-size: 0.5rem;
            text-transform: uppercase;
            color: white;
            text-shadow: 2px 2px 4px #000;
            /*background-color: rgba(0,0,0, 0.5);*/
             
        }

        .titulo-index {
            padding: 30px;
            margin: auto;
            text-transform: uppercase;
            color: white;
            background-color: rgba(0,0,0, 0.5);
        }

        .thumbnail {
           box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.5);
           transition: 0.3s;
           min-width: 40%;
           border-radius: 5px;
           height:380px !important;
         }

         .thumbnail-description {
           min-height: 40px;
         }

         .thumbnail:hover {
           cursor: pointer;
           box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 1);
         }

        /* Extra small devices (phones, 600px and down) */
        @media only screen and (max-width: 600px) {
            #container_principal {
                margin-top:-800px;
            }

          .bannerRosto_interno {
                height: 55vh;
                margin-top:65px;
                background-attachment:unset;
            }

            #texto-img {
                height: 42vh;
            }
        }

        /* Small devices (portrait tablets and large phones, 600px and up) */
        @media only screen and (min-width: 600px) {
          .bannerRosto_interno {
                height: 55vh;
                margin-top:65px;
                background-attachment:unset;
            }

            #texto-img {
                height: 42vh;
            }
        }
        
        /* Medium devices (landscape tablets, 768px and up) */
        @media only screen and (min-width: 768px) {
          .bannerRosto_interno {
                height: 55vh;
            }

            #texto-img {
                height: 55vh;
            }
        } 

        /* Large devices (laptops/desktops, 992px and up) */
        @media only screen and (min-width: 992px) {
          .bannerRosto_interno {
                height: 65vh;
            }

            #texto-img {
                height: 55vh;
            }
        } 

        /* Extra large devices (large laptops and desktops, 1200px and up) */
        @media only screen and (min-width: 1200px) {
          .bannerRosto_interno {
                height: 55vh;
                /*margin-top:90px;*/
            }

            #texto-img {
                height: 55vh;
            }
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

        .centralizar_1 {
          vertical-align: middle;
        }

        .anchor {
            position: relative;
            top: -170px;
            margin: 0;
            padding: 0;
            float: left;
        }

    </style>

    <asp:Literal ID="litGoogle" runat="server"></asp:Literal>

</head>
<body>
    <form id="form2" runat="server">
        <div>
            <section id="sectionBanner" runat="server" class="bannerRosto_interno">

                <div id="texto-img" class="text-center">
                    <h1 class="text-center"><strong>
                        <asp:Label ID="lblTipoCurso" runat="server" Text="Monografias"></asp:Label></strong></h1>
                    <%--<a id="btn-inscrevase" class="btn btn btn-success" href="#" onclick=""><strong>Inscreva-se</strong> </a>--%>
                </div>

            </section>

            <div class="hidden-xs">
                <br /><br /><br />
            </div>
            
            <div class="container">
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <div class="row">
                            <div class="col-md-12 hidden" style="font-family:Helvetica; text-align: left; font-size: 1.9rem;">
                                <h1>
                                    Monografias
                                </h1>
                            </div>
                    
                            <div class="col-md-12" style="font-family: Helvetica,Arial,Lucida,sans-serif; text-align: left; font-size: 1.8rem;">
                                <p style="line-height:1.8em">
                                    Neste espaço você encontra o acervo de Monografias defendidas nos Programas de Especialização do IPT. O acesso ao texto completo das Monografias em formato digital é disponibilizado livremente, com o objetivo de difundir o conhecimento produzido na instituição para a sociedade.
                                </p>
                            </div>
                        </div>
                        <br />
                        <hr />

                        <div class="row">
                            <div class="col-md-12">
                                <p style="text-transform:uppercase">
                                    <strong>Ano de publicação das Monografias</strong>
                                </p>
                                <div class="row">
                                    <asp:Literal ID="litAno" runat="server"></asp:Literal>

                                    <%--<div class="col-md-2 col-xs-4">
                                        <button type="button" class="btn btn-primary btn-block btn-lg btn-outline" onclick="fGoToAno(0)">Todos</button>
                                    </div>
                                    <div class="col-md-2 col-xs-4">
                                        <button type="button" class="btn btn-primary btn-block btn-lg" onMouseOver="this.style.cursor='context-menu'">2020</button>
                                    </div>
                                    <div class="col-md-2 col-xs-4">
                                        <button type="button" class="btn btn-primary btn-block btn-lg btn-outline" onclick="fGoToAno(2019)">2019</button>
                                    </div>
                                    <div class="hidden-md hidden-lg">
                                        <br /><br /><br />
                                    </div>

                                    <div class="col-md-2 col-xs-4">
                                        <button type="button" class="btn btn-primary btn-block btn-lg btn-outline" onclick="fGoToAno(2018)">2018</button>
                                    </div>
                                    <div class="col-md-2 col-xs-4">
                                        <button type="button" class="btn btn-primary btn-block btn-lg btn-outline" onclick="fGoToAno(2018)">2018</button>
                                    </div>
                                    <div class="col-md-2 col-xs-4">
                                        <button type="button" class="btn btn-primary btn-block btn-lg btn-outline" onclick="fGoToAno(2017)">2017</button>
                                    </div>
                                    <br /><br /><br />

                                    <div class="col-md-2 col-xs-4">
                                        <button type="button" class="btn btn-primary btn-block btn-lg btn-outline" onclick="fGoToAno(2016)">2016</button>
                                    </div>--%>
                                </div>
                                <br /><br />

                                <div class="row">
                                    <div class="col-md-10">
                                        <span>Palavra-chave</span><br />
                                        <input type="text" class="form-control" id="txtPalavraChave" value="" maxlength="100" />
                                    </div>
                                    <div class="col-md-2">
                                        <br />
                                        <a class="btn btn-primary" href="javascript:fGoToPesquisar();"><i class="fa fa-search fa-lg"></i> Pesquisar</a>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <hr />
                        <br />

                        <asp:Literal ID="litChamadas" runat="server"></asp:Literal>

                        <div class="row">
                            <div class="col-md-12">
                                <%--<div class="panel panel-success">
                                    <div class="panel-heading" role="tab">
                                        <h5 class="panel-title">
                                            <a class="collapsed a_faq" id="cab_1" data-toggle="collapse" href="#res_1" aria-expanded="false" onclick="fGoToVisualizacao(1)">
                                                <h6><i class="fa fa-square" style="color: #3588CC""></i> Habitação</h6>
                                                <h6><strong>Acompanhamento das atividades de produção de obra utilizando sistema de monitoramento em tempo real</strong></h6>
                                                <i id="icab_1" style="margin-top: -25px; color: #3588CC" class="fa fa-chevron-left pull-right rotate"></i>
                                                <h6>por <strong>SANTOS, Victor de Castro</strong></h6>
                                            </a>
                                        </h5>
                                    </div>
                                    <div id="res_1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="cab_1">
                                        <div class="panel-body">
                                            <p style="font-size: 1.3rem; line-height: 1.5em">
                                                Orientação: <strong>IOSHIMOTO, Eduardo</strong><br /><br />
                                                <div class ="row">
                                                    <div class ="col-xs-6">
                                                        Ano: <strong>2019</strong>
                                                    </div>
                                                    <div class ="col-xs-6 text-right">
                                                        visualizações: <strong><label id="lblQtdVisualizacao_1">50</label></strong>
                                                    </div>
                                                    <div class ="col-xs-12 text-right">
                                                        <em>downloads</em>: <strong><label id="lblQtdDownload_1">32</label></strong>
                                                    </div>
                                                </div>
                                                <br />
                                                Melhorar a gestão do dia a dia da obra é fundamental para garantir o desempenho operacional planejado, face aos diversos imprevistos que podem ocorrer num canteiro de obras, por meio da eliminação ou minimização de desperdícios e tempo de trabalho consumido com atividades que não agregam valor ao cliente e da melhoria no fluxo do trabalho, com intuito de alcançar ou superar os resultados planejados pelas empresas e aumentar sua competitividade no mercado. Um fator importante para o gestor da obra, além de analisar comparativamente o realizado com o planejado, é ser capaz de atuar em casos de anormalidades na produção e direcionar esforços para cumprir as metas estabelecidas pelo planejamento da obra. O Sistema de Execução de Manufatura (MES) é um sistema de tecnologia da informação e comunicação a ser utilizado para o gerenciamento da produção. Por meio da geração de informações precisas e em tempo real, o MES serve como base para adoção de ações corretivas imediatas, promovendo o controle e melhoria dos aspectos que influenciam no processo de produção. Embora tenha a manufatura como um modelo para melhoria dos processos produtivos, o MES é pouco conhecido no setor da construção civil. Neste trabalho são abordados os conceitos do MES e apresentada uma metodologia de implantação e utilização do sistema para monitorar uma obra. É analisado no estudo de caso o impacto da utilização do MES para a melhoria da gestão da produção e tomada de decisão para corrigir os desvios ocorridos no canteiro de obras. Verificou-se no estudo melhorias dos índices de produtividade e aumento do percentual das atividades concluídas, permitindo o cumprimento dos prazos planejados e otimização da mão de obra alocada na produção, além da mudança nos procedimentos de controle da produção e influência positiva do MES em critérios competitivos da construtora da obra.
                                                <br />
                                                <br />
                                                <a class="btn btn-primary btn-outline" target="_blank" onclick="fGoToDownload(1)" href="\Teses\Resposta Anexo 1.pdf"><b>Faça aqui o <em>download</em> do Trabalho de Conclusão de Curso</b></a>
                                            </p>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>


                            <div id="divDissertacoes">

                            </div>
                        
                            <div id="divLoading" class="text-center" style="display:none;">
                                <br />
                                <img src="img/homepage/carregando.gif" title="" alt=""/>
                            </div>

                            <div id="dialog" style="display: none"></div>

                            <div id="divBotaoCarregaDissertacoes" class="row">
                                <div class="col-md-12 text-center">
                                    <br />
                                    <button type="button" class="btn btn-default btn-sm" onclick="fMaisDissertacoes()">
                                        <i class="fa fa-refresh fa-spin"></i>&nbsp;&nbsp;carregar mais Monografias...</button>
                                </div>
                            </div>

                            <div id="divFimDissertacoes" class="row" style="display:none">
                                <br />
                                <div class="col-md-12 text-center alert-warning">
                                    <br />
                                    <p>Fim das Monografias</p>
                                    <br />
                                </div>
                            </div>

                        </div>

                        <br />
                        <br />

                    </div>
                </div>
            </div>
            
            <br />
            <br />

        </div>

        <a id="aAncora" href ="#" class="hidden" ></a>
            
        <script>
            var qRegistroInicio = 0;
            var qQuantosRegistros = 10;
            var qAno = 0;
            var qObjeto = 0;
            var qRodou = false;

            //===========================

            $(document).ready(function () {
                $(".ls-wp-container").remove();

                fMaisDissertacoes();

                $('#aAncora').click(function () {
                    $('html, body').animate({
                        scrollTop: $($(this).attr('href')).offset().top
                    }, 'slow');
                    return false;
                });

                $(this).scrollTop(0);

                fFechaProcessando();
            });

            //===========================
            function fAncoras() {
                $(".collapsed").click(function () {
                    //alert($(this).get(0).id);
                    $("#i" + $(this).get(0).id).toggleClass("down");
                })

                $(".cqBotao").click(function () {
                    //alert($(this).get(0).style.cursor);
                    $(".cqBotao").css('cursor', 'pointer');
                    $(".cqBotao").addClass('btn-outline');
                    $(this).get(0).style.cursor = "context-menu";
                    $(this).get(0).classList.remove('btn-outline');

                });

            }
            $(".collapsed").click(function () {
                //alert($(this).get(0).id);
                $("#i" + $(this).get(0).id).toggleClass("down");
            })

            //===========================

            function fGoToVisualizacao(qElemento, qIdBanca) {
                //alert(document.getElementById('lblQtdVisualizacao_' + qElemento).innerHTML);
                if (!document.getElementById('res_' + qElemento).classList.contains('in')) {
                    var iTotal = parseInt(document.getElementById('lblQtdVisualizacao_' + qElemento).innerHTML) + 1;
                    document.getElementById('lblQtdVisualizacao_' + qElemento).innerHTML = iTotal;
                    fAtualizaVisualizacao(qIdBanca, iTotal);
                }

            }

            //===========================

            function fGoToDownload(qElemento, qIdBanca) {
                var iTotal = parseInt(document.getElementById('lblQtdDownload_' + qElemento).innerHTML) + 1;
                document.getElementById('lblQtdDownload_' + qElemento).innerHTML = iTotal;
                fAtualizaDownload(qIdBanca, iTotal);
            }

            //===========================

            $(".cqBotao").click(function () {
                //alert($(this).get(0).style.cursor);
                $(".cqBotao").css('cursor','pointer');
                $(".cqBotao").addClass('btn-outline');
                $(this).get(0).style.cursor = "context-menu";
                $(this).get(0).classList.remove('btn-outline');
                
            });

            //===========================

            function fGoToAno(qpAno) {
                qAno = qpAno;
                //  $('#result').load('index_chamadas.aspx?qAno=' + qAno);
            }

            //===========================

            function fGoToPesquisar() {
                qRegistroInicio = 0;
                qQuantosRegistros = 10;
                qObjeto = 0;
                $('#divDissertacoes').empty();
                qRodou = false;
                document.getElementById("divBotaoCarregaDissertacoes").style.display = "block";
                document.getElementById("divFimDissertacoes").style.display = "none";

                fMaisDissertacoes();
            }

            
            //===========================

            function fMaisDissertacoes() {

                var formData = new FormData();
                formData.append("qRegistroInicio", qRegistroInicio);
                formData.append("qQuantosRegistros", qQuantosRegistros);
                formData.append("qAno", qAno);
                formData.append("qPalavra", document.getElementById('txtPalavraChave').value.trim());

                try {
                    $.ajax({
                        url: "wsInscricao.asmx/fMaisTCCs",
                        data: formData,
                        type: 'POST',
                        cache: false,
                        contentType: false,
                        processData: false,
                        async: true,
                        beforeSend: function (xhr) {
                            document.getElementById("divLoading").style.display = "block";
                        },
                        success: function (json) {
                            //alert('sucesso: Regs ' + json.length);
                            qRegistroInicio = qRegistroInicio + qQuantosRegistros;
                            var sAux = "";

                            for (var i = 0; i < json.length; i++) {
                                if (json[i].P0 == "Fim") {
                                    document.getElementById("divBotaoCarregaDissertacoes").style.display = "none";
                                    document.getElementById("divFimDissertacoes").style.display = "block";
                                }
                                else
                                {
                                    if (sAux != "") {
                                        sAux = sAux + "<br>";
                                    }
                                    else {
                                        sAux = "<div class=\"anchor\" id=\"divAncora" + qRegistroInicio + "\"></div>";
                                    }

                                    sAux = sAux + "<div class=\"panel panel-success\">";
                                    sAux = sAux + "    <div class=\"panel-heading\" role=\"tab\">";
                                    sAux = sAux + "        <h5 class=\"panel-title\">";
                                    sAux = sAux + "            <a class=\"collapsed a_faq\" id=\"cab_" + qObjeto + "\" data-toggle=\"collapse\" href=\"#res_" + qObjeto + "\" aria-expanded=\"false\" onclick=\"fGoToVisualizacao(" + qObjeto + "," + json[i].P9 + ")\">";
                                    sAux = sAux + "                <h6><i class=\"fa fa-square\" style=\"color: #3588CC\"></i> " + json[i].P0 + "</h6>";
                                    sAux = sAux + "                <h5 style=\"line-height: 1.5em\"><strong>" + json[i].P1 + "</strong></h5>";
                                    sAux = sAux + "                <i id=\"icab_" + qObjeto + "\" style=\"margin-top: -25px; color: #3588CC\" class=\"fa fa-chevron-left pull-right rotate\"></i>";
                                    sAux = sAux + "                <h6>por <strong>" + json[i].P2 + "</strong></h6>";
                                    sAux = sAux + "            </a>";
                                    sAux = sAux + "        </h5>";
                                    sAux = sAux + "    </div>";
                                    sAux = sAux + "    <div id=\"res_" + qObjeto + "\" class=\"panel-collapse collapse\" role=\"tabpanel\" aria-labelledby=\"cab_" + qObjeto + "\">";
                                    sAux = sAux + "        <div class=\"panel-body\">";
                                    sAux = sAux + "            <p style=\"font-size: 1.3rem; line-height: 1.5em\">";
                                    sAux = sAux + "                Orientação: <strong>" + json[i].P3 + "</strong><br /><br />";
                                    sAux = sAux + "                <div class =\"row\">";
                                    sAux = sAux + "                    <div class =\"col-xs-6\">";
                                    sAux = sAux + "                        Ano: <strong>" + json[i].P4 + "</strong>";
                                    sAux = sAux + "                    </div>";
                                    sAux = sAux + "                    <div class =\"col-xs-6 text-right\">";
                                    sAux = sAux + "                        visualizações: <strong><label id=\"lblQtdVisualizacao_" + qObjeto + "\">" + json[i].P6 + "</label></strong>";
                                    sAux = sAux + "                    </div>";
                                    sAux = sAux + "                    <div class =\"col-xs-12 text-right\">";
                                    sAux = sAux + "                        <em>downloads</em>: <strong><label id=\"lblQtdDownload_" + qObjeto + "\">" + json[i].P7 + "</label></strong>";
                                    sAux = sAux + "                    </div>";
                                    sAux = sAux + "                </div>";
                                    sAux = sAux + "                <br />";
                                    sAux = sAux + "                " + json[i].P5 + "";
                                    sAux = sAux + "                <br />";
                                    sAux = sAux + "                <br />";
                                    sAux = sAux + "                <a class=\"btn btn-primary btn-outline\" target=\"_blank\" onclick=\"fGoToDownload(" + qObjeto + "," + json[i].P9 + ")\" href=\"\Teses\\" + json[i].P8 + "\"><b>Faça aqui o <em>download</em> da Monografia</b></a>";
                                    sAux = sAux + "            </p>";
                                    sAux = sAux + "        </div>";
                                    sAux = sAux + "    </div>";
                                    sAux = sAux + "</div>";

                                    qObjeto++;

                                }
                            }

                            $('<div style="display: none;" class="new-link" name="link[]"> <br >' + sAux + '</div>').appendTo($('#divDissertacoes')).slideDown("fast");

                            if (qRodou) {
                                document.getElementById("aAncora").href = '#divAncora' + qRegistroInicio;
                                document.getElementById("aAncora").click();
                            }
                            qRodou = true;
                            //alert('response: ' + response[0].Destino);
                            fAncoras();
                        },

                        error: function (xhr, errorType, exception) {
                            var responseText;
                            //$("#dialog").html("");
                            try {
                                responseText = jQuery.parseJSON(xhr.responseText);
                                alert("<div><b>" + errorType + " " + exception + "</b></div>");
                                alert("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
                                alert("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
                                alert("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
                            } catch (e) {
                                responseText = xhr.responseText;
                                alert(responseText);
                            }
                            document.getElementById("divLoading").style.display = "none";
                        },
                        complete: function () {
                            document.getElementById("divLoading").style.display = "none";
                        }
                    });
                } catch (e) {
                    document.getElementById("divLoading").style.display = "none";
                }
            }

            //===========================

            function fAtualizaVisualizacao(qIdBanca, qTotal) {

                var formData = new FormData();
                formData.append("qIdBanca", qIdBanca);
                formData.append("qTotal", qTotal);
                try {
                    $.ajax({
                        url: "wsInscricao.asmx/fAtualizaVisualizacao",
                        data: formData,
                        type: 'POST',
                        cache: false,
                        contentType: false,
                        processData: false,
                        async: true,
                        success: function (json) {
                            //alert('sucesso: Regs ');
                        },

                        error: function (xhr, errorType, exception) {
                            var responseText;
                            //$("#dialog").html("");
                            try {
                                responseText = jQuery.parseJSON(xhr.responseText);
                                alert("<div><b>" + errorType + " " + exception + "</b></div>");
                                alert("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
                                alert("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
                                alert("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
                            } catch (e) {
                                responseText = xhr.responseText;
                                alert(responseText);
                            }
                            document.getElementById("divLoading").style.display = "none";
                        },
                        complete: function () {
                            document.getElementById("divLoading").style.display = "none";
                        }
                    });
                } catch (e) {
                    document.getElementById("divLoading").style.display = "none";
                }
            }

            //===========================

            function fAtualizaDownload(qIdBanca, qTotal) {

                var formData = new FormData();
                formData.append("qIdBanca", qIdBanca);
                formData.append("qTotal", qTotal);
                try {
                    $.ajax({
                        url: "wsInscricao.asmx/fAtualizaDownload",
                        data: formData,
                        type: 'POST',
                        cache: false,
                        contentType: false,
                        processData: false,
                        async: true,
                        success: function (json) {
                            //alert('sucesso: Regs ');
                        },

                        error: function (xhr, errorType, exception) {
                            var responseText;
                            //$("#dialog").html("");
                            try {
                                responseText = jQuery.parseJSON(xhr.responseText);
                                alert("<div><b>" + errorType + " " + exception + "</b></div>");
                                alert("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
                                alert("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
                                alert("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
                            } catch (e) {
                                responseText = xhr.responseText;
                                alert(responseText);
                            }
                            document.getElementById("divLoading").style.display = "none";
                        },
                        complete: function () {
                            document.getElementById("divLoading").style.display = "none";
                        }
                    });
                } catch (e) {
                    document.getElementById("divLoading").style.display = "none";
                }
            }

            //===========================


            function setModalMaxHeight(element) {
                this.$element = $(element);
                this.$content = this.$element.find('.modal-content');
                var borderWidth = this.$content.outerHeight() - this.$content.innerHeight();
                var dialogMargin = $(window).width() > 767 ? 60 : 20;
                var contentHeight = $(window).height() - (dialogMargin + borderWidth);
                var headerHeight = this.$element.find('.modal-header').outerHeight() || 0;
                var footerHeight = this.$element.find('.modal-footer').outerHeight() || 0;
                var maxHeight = contentHeight - (headerHeight + footerHeight);

                this.$content.css({
                    'overflow': 'hidden'
                });

                this.$element
                  .find('.modal-body').css({
                      'max-height': maxHeight,
                      'overflow-y': 'auto'
                  });
            }

            $('.modal').on('show.bs.modal', function () {
                $(this).show();
                setModalMaxHeight(this);
            });

            $(window).resize(function () {
                if ($('.modal.in').length != 0) {
                    setModalMaxHeight($('.modal.in'));
                }
            });

        </script>

        <!-- Global site tag (gtag.js) - Google Analytics -->
        <script async src="https://www.googletagmanager.com/gtag/js?id=UA-154434342-1"></script>
        <script>
              window.dataLayer = window.dataLayer || [];
              function gtag(){dataLayer.push(arguments);}
              gtag('js', new Date());

              gtag('config', 'UA-154434342-1', {
                  'page_title': 'Página Monografia',
                  'page_path': '/index_tcc.aspx'
              });
        </script>

    </form>
</body>
</html>
