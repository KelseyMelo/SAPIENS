<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_curso.aspx.cs" Inherits="SERPI.UI.WebForms_C.index_curso" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nossos Cursos</title>

    <style>
        @keyframes img-ani {
          from{opacity:0;}
          to{opacity: 1;}
        }

        .bannerRosto_interno {
            background: url('img/homepage/cursos/habitacao.jpg');
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


        
            .card_flutuante {
                position: fixed; /* posição absoluta ao elemento pai, neste caso o BODY */
                /* Posiciona no meio, tanto em relação a esquerda como ao topo */
                left: 80%;
                top: 50%;
                width: 300px; /* Largura da DIV */
                height: 200px; /* Altura da DIV */
                /* A margem a esquerda deve ser menos a metade da largura */
                /* A margem ao topo deve ser menos a metade da altura */
                /* Fazendo isso, centralizará a DIV */
                margin-left: -150px;
                margin-top: -125px;
                background-color: #fbfbfb;
                /*color: #FFF;*/
                /*background-color: #666;*/
                
                z-index: 10; /* Faz com que fique sobre todos os elementos da página */


                box-shadow: 0 6px 12px 0 rgba(0, 0, 0, 0.6);
                transition: 0.3s;
                min-width: 10%;
                border-radius: 2px;
            }

            .card_flutuante p{
                margin: 10px 5px 5px 5px;
                font-family: 'Noto Sans KR', sans-serif;
                font-weight: 100;
                letter-spacing: -0.25px;
                line-height: 1.25;
                font-size: 12px;
                word-break: break-all;
                word-wrap: pre-wrap;
                color: darkslategrey;
    
                -webkit-animation-name: p-show; /* Safari 4.0 - 8.0 */
                -webkit-animation-duration: 1.5s; /* Safari 4.0 - 8.0 */
                animation-name: p-show;
                animation-duration: 1.5s;
                }

            .card_flutuante button{
                position:absolute;
                bottom:0px;
                left: 50%;
                width:100%;
                height:40px;
                transform: translateX(-50%);
                -webkit-transform: translateX(-50%);
                -moz-transform: translateX(-50%);
                border-style:none

            }
    </style>


    <style>

    </style>

    <asp:Literal ID="litGoogle" runat="server"></asp:Literal>
    
</head>
<body>
    
    <form id="form2" runat="server">
        <div class="card_flutuante hidden-xs hidden" >
            <p>
                <span style="color:#016699"><strong>Data: </strong></span>
                <span>07, 14, 15, 21 Prof. Dr. Marcos Bruno (E-mail: marcos.acbruno@gmail.com)</span>
            </p>
            <p>
                <strong style="color:#016699">Carga horária: </strong>20 horas
            </p>
            <p>
                <strong style="color:#016699">Local: </strong>ON LINE pelo Microsoft Teams
            </p>
            <p>
                <strong style="color:#016699">Investimento: </strong>R$ 600,00 
            </p>
            <p>
                <strong style="color:#016699">Coordenador: </strong>Prof. Dr. Marcos Bruno (E-mail: marcos.acbruno@gmail.com)
            </p>


            <button class="btn-primary" type="button" >
                <i class="fa fa-pencil fa-lg"></i>
                <span class="cssCurso_pt"><strong>INSCREVA-SE</strong></span><span class="cssCurso_en"><strong>SIGN UP</strong></span>
            </button>
            <%--style="width:100%; margin-bottom:20px;border-style: none;"--%>
            <%--<p>
                <strong>Data: </strong>07, 14, 15, 21 e 22 de maio de 2021 (sextas das 19h as 22h30 e sábados das 08h30 às 12h30)
            </p>
            <p>
                <strong>Carga horária: </strong>20 horas
            </p>
            <p>
                <strong>Local: </strong>ON LINE pelo Microsoft Teams
            </p>
            <p>
                <strong>Investimento: </strong>R$ 600,00 
            </p>
            <p>
                <strong>Coordenador: </strong>Prof. Dr. Marcos Bruno (E-mail: marcos.acbruno@gmail.com)
            </p>--%>
        </div>

        <input runat="server" id="hTipoCursoRunat" name="hTipoCursoRunat" type="hidden" value="" />
        <div>
            <section id="sectionBanner" runat="server" class="bannerRosto_interno">

                <div id="texto-img" class="text-center">
                    <h1 class="text-center">
                        <strong>
                            <asp:Label ID="lblNomeCurso" runat="server" Text="adipiscing" CssClass="cssCurso_pt"></asp:Label>
                            <asp:Label ID="lblNomeCurso_en" runat="server" Text="APPLIED COMPUTING"  CssClass="cssCurso_en"></asp:Label>
                        </strong>
                    </h1>
                </div>

            </section>

            <div style="background-color:#016699">
                <br />
                <div class="container">
                    <div class="row">
                        <div class="col-sm-8">
                            <h2 style="color:white"><asp:Label ID="lblInscricao" runat="server" Text=" Pós IPT - Faça sua Inscrição nesse curso" CssClass="cssCurso_pt"></asp:Label><asp:Label ID="lblInscricao_en" runat="server" Text=" Post IPT - Register for this course" CssClass="cssCurso_en"></asp:Label> </h2>
                        </div>
                        <div class="col-sm-4 hidden-xs">
                            <br />
                            <a id="ainscricoesDesk" runat="server" href="./homeInscricoes.aspx" target="_blank" class="btn btn-default btn-lg aInscricao"><i class="fa fa-pencil fa-lg" style="color:#016699"></i><strong style="color:#016699"> <span class="cssCurso_pt"><label id="lblIncrevaseDesktop" style="cursor:pointer" runat="server">Inscreva-se</label> </span><span class="cssCurso_en">Sign up</span></strong></a>
                            <a id="aFormulario" runat="server" href="javascript:fModalFormulario()" target="_blank" class="btn btn-default btn-lg"><i class="fa fa-info-circle fa-lg" style="color:#016699"></i><strong style="color:#016699"> <span class="cssCurso_pt">Mais Informações</span><span class="cssCurso_en">More information</span></strong></a>
                        </div>

                        <div class="col-sm-4 hidden-sm hidden-md hidden-lg">
                            <br />
                            <div class="center-block">
                                <a id="ainscricoesmobile" runat="server" href="./homeInscricoes.aspx" target="_blank" class="btn btn-default btn-lg center-block aInscricao"><i class="fa fa-pencil fa-lg" style="color:#016699"></i><strong style="color:#016699"> <span class="cssCurso_pt"><label id="lblIncrevaseMobile" style="cursor:pointer" runat="server">Inscreva-se</label></span><span class="cssCurso_en">Sign up</span></strong></a>
                                <a id="aFormularioMobile" runat="server" href="javascript:fModalFormulario()" target="_blank" class="btn btn-default btn-lg center-block"><i class="fa fa-info-circle fa-lg" style="color:#016699"></i><strong style="color:#016699"> <span class="cssCurso_pt">Mais Informações</span><span class="cssCurso_en">More information</span></strong></a>
                            </div>

                        </div>
                    </div>
                </div>
                <br />
            </div>
            <br />
            <br />
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <%--<div class="image container-video">
                            <iframe width="560" height="315" src="https://www.youtube.com/embed/kTo3zljXMwo" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen=""></iframe>
                        </div>--%>

                        <h2><strong>
                            <asp:Label ID="lblNomeCurso2" runat="server" Text="adipiscing" CssClass="hidden cssCurso_pt"></asp:Label>
                            <asp:Label ID="lblNomeCurso2_en" runat="server" Text="adipiscing" CssClass="hidden cssCurso_en"></asp:Label>
                            </strong></h2>

                        <asp:Literal ID="litDescricao" runat="server"></asp:Literal>

                    </div>
                </div>

            </div>
            <br />
            <div id="divBotoes" class="container cssCurso_pt" runat="server" visible="false">
                <br />
                <hr />
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary btn-lg center-block" onclick="fModalCorpoDocente()">
                            <i class="fa fa-users"></i> <span class="cssCurso_pt">Corpo Docente</span><span class="cssCurso_en">Faculty</span>
                        </button>
                    </div>
                </div>
            </div>
            <div id="divBotoes_en" class="container cssCurso_en" runat="server" visible="false">
                <br />
                <hr />
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary btn-lg center-block" onclick="fModalCorpoDocente()">
                            <i class="fa fa-users"></i> <span class="cssCurso_pt">Corpo Docente</span><span class="cssCurso_en">Faculty</span>
                        </button>
                    </div>
                </div>
            </div>

            <br />
            <hr />
            <br />

            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <button id="btnVoltar" runat="server" type="button" class="btn btn-primary btn-outline btn-lg center-block" href="javascript:fVolta(4)">
                            <i class="fa fa-reply fa-lg"></i><span class="cssCurso_pt">Voltar</span><span class="cssCurso_en">Black</span>
                        </button>
                    </div>
                </div>

            </div>

            <br />
            <br />

            <div class="modal fade" id="divModalCorpoDocente" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title"><i class="fa fa-users fa-lg"></i> <span class="cssCurso_pt">Corpo Docente</span><span class="cssCurso_en">Faculty</span></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblCorpoDocente" runat="server" Text="Label" CssClass="cssCurso_pt"></asp:Label>
                                    <asp:Label ID="lblCorpoDocente_en" runat="server" Text="Label" CssClass="cssCurso_en"></asp:Label>
                                </div>
                            </div>
                            <br />


                            <br />
                            <hr size="100" width="100%" noshade color="#9D9D9D" />
                            <br />
                            <div class="row">
                                <div class="col-lg-12 pull-right">
                                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal"><i class="fa fa-close"></i> <span class="cssCurso_pt">Fechar</span><span class="cssCurso_en">Close</span></button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

        </div>


        <script>
            $(document).ready(function () {
                if (document.getElementById('<%=hTipoCursoRunat.ClientID%>').value == '1') {
                    $(".cssDivLanguage").hide();
                    //Habilitar inglês aqui
                    $(".cssDivLanguage").show();
                    fLanguage(document.getElementById('hLanguage').value);
                }
                else {
                    fLanguage('pt');
                }

                $(".ls-wp-container").remove();

                $(this).scrollTop(0);
            });

            function fVolta(qIdTipo) {
                $('#result').load('index_area.aspx?qIdTipo=' + qIdTipo);
            }

            function fModalCorpoDocente() {
                $('#divModalCorpoDocente').modal();
            }

            function fModalFormulario() {
                $('#divModalFormulario').modal();
            }

            function fAviso(qTitulo, qAviso) {
                document.getElementById('lblTituloMensagemMaster').innerHTML = qTitulo;
                document.getElementById('lblMensagemMaster').innerHTML = qAviso;
                $('#divMensagemModalMaster').modal();
            }

            function fLanguage(qIdioma) {
                //document.getElementById("<%=lblNomeCurso.ClientID%>").style.display = "none";
                //document.getElementById("<%=lblNomeCurso_en.ClientID%>").style.display = "none";
                $('.cssCurso_pt').hide();
                $('.cssCurso_en').hide();
                if (qIdioma == "en") {
                    //document.getElementById("<%=lblNomeCurso_en.ClientID%>").style.display = "block";
                    $('.cssCurso_en').show();
                }
                else {
                    //document.getElementById("<%=lblNomeCurso.ClientID%>").style.display = "block";
                    $('.cssCurso_pt').show();
                }
            }
        </script>

        <%--Modal Forulário Educação Corporativa--%>

        <div class="modal fade" id="divModalFormulario" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header alert-info">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
                <h5 class="modal-title" id="myModalLabel"><i class="fa fa-info-circle fa-lg"></i> Educação Corporativa</h5>
              </div>
                <div class="modal-body">
                    <iframe>
                        <div id="pedidos-de-cursos-orcamentos-perguntas-9320fb362bbe046c45e3"></div>
                        <script type="text/javascript" src="https://d335luupugsy2.cloudfront.net/js/rdstation-forms/stable/rdstation-forms.min.js"></script>
                        <script type="text/javascript"> new RDStationForms('pedidos-de-cursos-orcamentos-perguntas-9320fb362bbe046c45e3', 'UA-154434342-1').createForm();</script>

                    </iframe>
                    
                </div>
              <%--<div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
              </div>--%>
            </div>
          </div>
        </div>
        
    </form>
    

</body>
</html>