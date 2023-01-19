<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_faq.aspx.cs" Inherits="SERPI.UI.WebForms_C.index_faq" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FAQ</title>

    <style>
        @keyframes img-ani {
          from{opacity:0;}
          to{opacity: 1;}
        }

        .bannerRosto_interno {
            background: url('img/homepage/faq.jpg');
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


    </style>

</head>
<body>
    <form id="form2" runat="server">

        <div>
            <section class="bannerRosto_interno">
                
                <div id="texto-img" class="text-center">
                    <h1 class="text-center"><strong>Perguntas Frequentes</strong></h1>
                </div>
                   
            </section>
            
            <br />
            <br />
            <div class="container" >
                <div class="row">
                    <div class="col-md-12">

                        <div id="faq" role="tablist" aria-multiselectable="true">

                            <h3 style="color:firebrick">Curso de Mestrado Profissional</h3>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="p_1">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq" href="#r_1" aria-expanded="false" aria-controls="r_1">
                                            <strong><i class="fa fa-angle-double-right"></i> 1 - Os cursos do mestrado são em que horário?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="r_1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="p_1">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. Sempre a noite das 19h as 22h30.</span>
                                    </div>
                                </div>
                            </div>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="p_2">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq" href="#r_2" aria-expanded="false" aria-controls="r_2">
                                            <strong><i class="fa fa-angle-double-right"></i> 2 - Em quanto tempo eu consigo terminar o curso?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="r_2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="p_2">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. O prazo máximo é 30 meses. É possível completar em tempo menor.</span>
                                    </div>
                                </div>
                            </div>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="p_3">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq" href="#r_3" aria-expanded="true" aria-controls="r_3">
                                            <strong><i class="fa fa-angle-double-right"></i> 3 - Qual o valor do curso de mestrado?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="r_3" class="panel-collapse collapse" role="tabpanel" aria-labelledby="p_3">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. 30 parcelas de R$ 1.600,00 (com reajuste anual pelo IPC/FIPE).</span>
                                    </div>
                                </div>
                            </div>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="p_4">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq" href="#r_4" aria-expanded="true" aria-controls="r_4">
                                            <strong><i class="fa fa-angle-double-right"></i> 4 - Tem bolsas de estudo?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="r_4" class="panel-collapse collapse" role="tabpanel" aria-labelledby="p_4">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. Não.</span>
                                    </div>
                                </div>
                            </div>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="p_5">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq" href="#r_5" aria-expanded="true" aria-controls="r_5">
                                            <strong><i class="fa fa-angle-double-right"></i> 5 - Quantas vezes abrem inscrições para início dos cursos de Mestrado?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="r_5" class="panel-collapse collapse" role="tabpanel" aria-labelledby="p_5">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. Inscrições abertas nos períodos de fevereiro a abril, junho a agosto e outubro a dezembro.</span>
                                    </div>
                                </div>
                            </div>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="p_6">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq" href="#r_6" aria-expanded="true" aria-controls="r_6">
                                            <strong><i class="fa fa-angle-double-right"></i> 6 - Pagamento total à vista tem desconto?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="r_6" class="panel-collapse collapse" role="tabpanel" aria-labelledby="p_6">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. Sim, desconto de 10%.</span>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <br />

                        <div id="faq_2" role="tablist" aria-multiselectable="true">

                            <h3 style="color:firebrick">Outros Cursos</h3>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="f_2_p_1">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq_2" href="#f_2_r_1" aria-expanded="false" aria-controls="f_2_r_1">
                                            <strong><i class="fa fa-angle-double-right"></i> 1 - Tem desconto?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="f_2_r_1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="f_2_p_1">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. Desconto de 10% nas mensalidades se você trouxer mais duas pessoas para fazer o curso.</span>
                                    </div>
                                </div>
                            </div>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="f_2_p_2">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq_2" href="#f_2_r_2" aria-expanded="false" aria-controls="f_2_r_2">
                                            <strong><i class="fa fa-angle-double-right"></i> 2 - Tem curso a distância?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="f_2_r_2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="f_2_p_2">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. No momento não.</span>
                                    </div>
                                </div>
                            </div>

                            <div class="panel">
                                <div class="panel-heading" role="tab" id="f_2_p_3">
                                    <h5 class="panel-title">
                                        <a class="collapsed a_faq" data-toggle="collapse" data-parent="#faq_2" href="#f_2_r_3" aria-expanded="true" aria-controls="f_2_r_3">
                                            <strong><i class="fa fa-angle-double-right"></i> 3 - Vocês oferecem curso <em>in company</em>?</strong>
                                        </a>
                                    </h5>
                                </div>
                                <div id="f_2_r_3" class="panel-collapse collapse" role="tabpanel" aria-labelledby="f_2_p_3">
                                    <div class="panel-body">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#3588CC">R. É possível realizar curso <em>in company</em>. É preciso enviar uma proposta com informações sobre o conteúdo e prazo desejado.</span>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>

            </div>

            <br />
            <br />

        </div>


        <script>
            $(document).ready(function () {
                $(".ls-wp-container").remove();

                $(this).scrollTop(0);
            });

        </script>

        <!-- Global site tag (gtag.js) - Google Analytics -->
        <script async src="https://www.googletagmanager.com/gtag/js?id=UA-154434342-1"></script>
        <script>
          window.dataLayer = window.dataLayer || [];
          function gtag(){dataLayer.push(arguments);}
          gtag('js', new Date());

          gtag('config', 'UA-154434342-1', {
              'page_title': 'Página FAQ',
              'page_path': '/faq.aspx'
          });
        </script>

    </form>
</body>
</html>
