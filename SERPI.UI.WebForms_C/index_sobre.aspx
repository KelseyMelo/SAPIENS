<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_sobre.aspx.cs" Inherits="SERPI.UI.WebForms_C.index_sobre" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sobre</title>

    <style>
        @keyframes img-ani {
          from{opacity:0;}
          to{opacity: 1;}
        }

        .bannerRosto_interno {
            background: url('img/homepage/sobre.jpg');
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
    </style>

</head>
<body>
    <form id="form2" runat="server">

        <div>
            <section class="bannerRosto_interno">
                
                <div id="texto-img" class="text-center">
                    <h1 class="text-center"><strong>Pós-Graduação</strong></h1>
                </div>
                   
            </section>
            
            <br />
            <br />
            <div class="container" >
                <div class="row">
                    <div class="col-md-12" style="font-family: sans-serif; text-align: justify; font-size: 1.8rem;">

                        <h2><strong>Ensino Tecnológico</strong></h2>
                        <br />

                        <p style="line-height:1.7em">
                            As atividades de ensino do IPT têm como objetivo formar profissionais com sólida capacitação para o desenvolvimento e aplicação de metodologias, técnicas e processos, tendo em vista as mudanças tecnológicas e as transformações econômicas e socioambientais que afetam o desenvolvimento nacional.
                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            Busca formar docentes e pesquisadores visando à geração e difusão de conhecimentos vinculados aos complexos científico-tecnológicos, enfatizando a sua interação com a realidade econômica e socioambiental brasileira.
                        </p>
                        <br />   
                        <p style="line-height:1.7em">
                            Além de fomentar a pesquisa e a inovação tecnológica e contribuir para agregar competitividade e aumentar a produtividade em empresas, organizações públicas e privadas.
                        </p>                        

                    </div>
                </div>
                <br />
                <hr />
                <br />

                <div class="row">
                    <div class="col-md-12" style="font-family: sans-serif; text-align: justify; font-size: 1.8rem;">

                        <h2><strong>Infraestrutura</strong></h2>
                        <br />

                        <p style="line-height:1.7em">
                            O Instituto de Pesquisas Tecnológicas (IPT) se destaca como um dos maiores institutos de pesquisas do Brasil, com mais de 100.000 m2 de instalações, organizadas em 11 centros tecnológicos que compreendem 37 laboratórios e equipe multidisciplinar com mais de 1000 profissionais,  pesquisadores e técnicos altamente qualificados, atuando em quatro grandes frentes: inovação, pesquisa & desenvolvimento, serviços tecnológicos, desenvolvimento & apoio metrológico e informação & educação em tecnologia.
                        </p>
                        <br />
                                              
                        <div class="row">
                            <div class="col-md-12">
                                <img src="img/Homepage/predio_2.png?v=0" class="center-block img-responsive" />
                            </div>
                        </div>
                        <hr />
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <img src="img/Homepage/sala_1.JPG?v=0" class="center-block img-responsive" />
                            </div>
                        </div>
                        <hr />
                        <br />

                        <div class="row">
                            <div class="col-md-6 center-block">
                                <img src="img/Homepage/secretaria_1.png?v=0" class="center-block img-responsive" />
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>
                            <div class="col-md-6 center-block">
                                <img src="img/Homepage/secretaria_2.png?v=0" class="center-block img-responsive" />
                            </div>
                        </div>
                        <hr />
                        <br />

                        <p style="line-height:1.7em">
                            <strong>O Ensino Tecnológico</strong> fica localizado no prédio 56 do Instituto de Pesquisas Tecnológicas. Neste prédio encontram-se as salas de aula e os laboratórios de informática. <br />O IPT possui um grande acervo em sua Biblioteca cujo o catálogo pode ser acessado pelo seguinte link: <a target="_blank" href="https://www.ipt.br/consultas_online/biblioteca">biblioteca</a><br /> O aluno ingressante nos cursos de mestrado profissional e especialização terá acesso a cerca de 50 mil títulos de revistas acadêmicas e científicas por meio dos convênios que o IPT mantém com universidades e instituições de pesquisas nacionais e internacionais.
                        </p>
                        <br />

                    </div>
                </div>
                <br />
                <hr />
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <h2><strong>Formatura</strong></h2>
                        <div class="row">
                            <div class="col-md-12">
                                <img src="img/Homepage/formatura.png?v=0" class="center-block img-responsive" />
                                <h4 class="text-center"> Entrega de diplomas em 2019. O IPT cuida de seu sonho.</h4>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
                <br />
                <hr />
                <br />

                <div class="row hidden">
                    <div class="col-md-12">
                        <h2><strong>Confira nosso calendário</strong></h2>
                        <%--<h2><strong>Confira nosso calendário para os meses de abril/maio/junho de 2020</strong></h2>--%>
                        <br />
                    </div>
                </div>

                <div class="row hidden">
                    <div class="col-md-3">
                        <span><strong>Julho de 2020</strong></span>
                        <a href="javascript:fExibeImagem('Julho_R8.jpg','Julho de 2020',)">
                            <img class="img-responsive" src="img\Homepage\Julho_R8.jpg?v=1" />
                        </a>
                    </div>
                    <div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-3">
                        <span><strong>Agosto de 2020</strong></span>
                        <a href="javascript:fExibeImagem('Agosto_R8.jpg','Agosto de 2020',)">
                            <img class="img-responsive" src="img\Homepage\Agosto_R8.jpg?v=0" />
                        </a>
                    </div>
                    <div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-3">
                        <span><strong>Setembro de 2020</strong></span>
                        <a href="javascript:fExibeImagem('Setembro_R8.jpg','Setembro de 2020',)">
                            <img class="img-responsive" src="img\Homepage\Setembro_R8.jpg?v=0" />
                        </a>
                    </div>
                    <div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-3">
                        <span><strong>Outubro de 2020</strong></span>
                        <a href="javascript:fExibeImagem('Outubro_R7.jpg','Outubro de 2020',)">
                            <img class="img-responsive" src="img\Homepage\Outubro_R7.jpg?v=0" />
                        </a>
                    </div>
                </div>
                <br />

                <div class="row">
                   
                </div>

               <%-- <div class="row">
                    <div class="col-md-12">
                        <h3><strong>Abril de 2020</strong></h3>
                        <br />
                        <img src="img/Homepage/04_ABRIL-min.JPG?v=0" class="center-block img-responsive" />
                    </div>
                </div>
                <hr />
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <h3><strong>Maio de 2020</strong></h3>
                        <br />
                        <img src="img/Homepage/20.05.07.maio_R2.JPG?v=0" class="center-block img-responsive" />
                    </div>
                </div>

                <hr />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <h3><strong>Junho de 2020</strong></h3>
                        <br />
                        <img src="img/Homepage/junho_2020_R3.jpg?v=0" class="center-block img-responsive" />
                    </div>
                </div>

                 <hr />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <h3><strong>Julho de 2020</strong></h3>
                        <br />
                        <img src="img/Homepage/julho20.jpg?v=0" class="center-block img-responsive" />
                    </div>
                </div>

                <hr />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <h3><strong>Agosto de 2020</strong></h3>
                        <br />
                        <img src="img/Homepage/agosto20.jpg?v=0" class="center-block img-responsive" />
                    </div>
                </div>--%>
                <br />

            </div>

            <br />
            <br />

        </div>

        <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header bg-blue">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Fechar</span></button>
                <h5 class="modal-title" id="myModalLabel"><label id="labelNomeExibeImagem">test</label></h5>
              </div>
              <div class="modal-body text-center">
                <img src="" id="imagepreview" class="img-responsive center-block"  > <%--style="width: 400px; height: 300px;"--%>
              </div>
              <%--<div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
              </div>--%>
            </div>
          </div>
        </div>

        <script>
            $(document).ready(function () {
                $(".ls-wp-container").remove();

                $(this).scrollTop(0);

                //$('#divModalInicio').modal();

            });

            function fExibeImagem(qImagem, qMes) {
                $('#imagepreview').attr('src', "img\\Homepage\\" + qImagem + "?" + new Date()); // here asign the image to the modal when the user click the enlarge link
                document.getElementById('labelNomeExibeImagem').innerHTML = qMes;
                $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
            }

        </script>

        <!-- Global site tag (gtag.js) - Google Analytics -->
        <script async src="https://www.googletagmanager.com/gtag/js?id=UA-154434342-1"></script>
        <script>
              window.dataLayer = window.dataLayer || [];
              function gtag(){dataLayer.push(arguments);}
              gtag('js', new Date());

              gtag('config', 'UA-154434342-1', {
                  'page_title': 'Página Sobre',
                  'page_path': '/sobre.aspx'
              });
        </script>

    </form>
</body>
</html>
