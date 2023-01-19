<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_contato.aspx.cs" Inherits="SERPI.UI.WebForms_C.index_contato" %>

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
            background: url('img/homepage/contato.jpg');
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
            background-color: rgba(0,0,0, 0.3);
             
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
    </style>
    <script src="Scripts/jquery.mask.min.js"></script>

</head>
<body>
    <form id="form2" runat="server">

        <div>
            <section class="bannerRosto_interno">
                
                <div id="texto-img" class="text-center">
                    <h1 class="text-center"><strong>Fale Conosco</strong></h1>
                </div>
                   
            </section>
            
            <br />
            <br />
            <div style="background-color:#f8f8f8">
                <div class="container">
                    <div class="row" >
                        <div class="col-md-12">
                            <h2 class="text-center">Informações</h2>
                            <br />
                            <div class="row">
                                <div class="col-md-5">
                                    <h4>Endereço</h4>
                                    <div class="widget widget-address">
                                        <p class="widget-address__text">
                                            Instituto de Pesquisas Tecnológicas – IPT<br>
                                            Avenida Professor Almeida Prado, 532 – Cidade Universitária, Butantã, São Paulo - SP<br>
                                            Ensino Tecnológico - Prédio 56 – térreo.
                                        </p>
                                    </div>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-4">
                                    <h4>Contato</h4>
                                    <p>
                                        E-mail: cursos@ipt.br | mestrado@ipt.br<br />
                                        Telefone: (11) 3767-4226 | 4068<br />
                                        <span class="hidden">WhatsApp: (11) 95994-7506</span>
                                    </p>
                                </div>
                                <div class="hidden-lg hidden-md">
                                    <br />
                                </div>

                                <div class="col-md-3">
                                    <h4>Horário de Funcionamento</h4>
                                    <p>
                                        Segunda à Sexta das 09h às 21h
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
            <br />
            <br />

            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <h2 class="text-center">Mensagem</h2>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-5">
                        <div class="row">
                            <div class="col-md-12">
                                <span>Nome </span><span class="text-danger"><strong>*</strong></span><br />
                                <input class="form-control input-sm" id="txtNome" type="text" value="" maxlength="350" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span>Email </span><span class="text-danger"><strong>*</strong></span><br />
                                <input class="form-control input-sm" id="txtEmail" type="email" value="" maxlength="350" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span>Assunto </span><span class="text-danger"><strong>*</strong></span><br />
                                <input class="form-control input-sm" id="txtAssunto" type="text" value="" maxlength="350" />
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <span>Celular </span><span class="text-danger"><strong>*</strong></span><br />
                                <input class="form-control input-sm" id="txtCelular" type="text" value="" maxlength="350" />
                            </div>
                        </div>
                        
                    </div>
                    <div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-7">
                        <div class="row">
                            <div class="col-md-12 ">
                                <span>Mensagem </span><span class="text-danger"><strong>*</strong></span><br />
                                <textarea style ="resize:vertical;font-size:14px" runat ="server" class="form-control input-sm" rows="11" id="txtMensagem"></textarea>
                            </div>
                        </div>  
                    </div>

                </div>
                <br />
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-outline btn-primary btn-lg center-block" href="#" onclick="fEnviaEmail()">
                            <i class="fa fa-envelope"></i> Enviar Mensagem
                        </button>
                        <a id="aEnviar" class="hidden" href="#">
                        </a>
                    </div>
                </div>

                <br />
                <br />
                <br />
            </div>

        </div>


        <script>
            $('#txtCelular').mask('(99) 9.9999-9999');

            $(document).ready(function () {
                $(".ls-wp-container").remove();

                $(this).scrollTop(0);
            });




            function fEnviaEmail() {

                var sAux = "";
                if (document.getElementById('txtNome').value.trim() == '') {
                    sAux = "Deve-se digitar o Nome.<br/>";
                }

                if (document.getElementById('txtEmail').value.trim() == '') {
                    sAux = sAux + "Deve-se digitar o Email.<br/>";
                }

                if (document.getElementById('txtAssunto').value.trim() == '') {
                    sAux = sAux + "Deve-se digitar o Assunto.<br/>";
                }

                if (document.getElementById('txtCelular').value.trim() == '') {
                    sAux = sAux + "Deve-se digitar o Celular.<br/>";
                }

                if (document.getElementById('txtMensagem').value.trim() == '') {
                    sAux = sAux + "Deve-se digitar a Mensagem.<br/>";
                }

                if (sAux != "") {
                    document.getElementById('lblTituloMensagemMaster').innerHTML = 'Atenção';
                    document.getElementById('lblMensagemMaster').innerHTML = sAux;
                    $("#divCabecalhoMaster").removeClass("bg-info");
                    $("#divCabecalhoMaster").removeClass("bg-success");
                    $("#divCabecalhoMaster").removeClass("bg-primary");
                    $("#divCabecalhoMaster").removeClass("bg-danger");
                    $("#divCabecalhoMaster").addClass("bg-warning");
                    $('#divMensagemModalMaster').modal();
                    return;
                }

                var sMensagem;
                sMensagem = replaceAll(document.getElementById("txtMensagem").value, "\n", "%0D%0A") + "%0D%0A%0D%0A" + document.getElementById("txtNome").value + "%0D%0A" + document.getElementById("txtEmail").value + "%0D%0A" + document.getElementById("txtCelular").value

                var link = document.getElementById("aEnviar");
                link.href = "mailto:cursos@ipt.br;mestrado@ipt.br?subject=" + document.getElementById("txtAssunto").value + "&body=" + sMensagem;
                link.click();

            }

            function replaceAll(str, find, replace) {
                return str.replace(new RegExp(find, 'g'), replace);
            }

        </script>

        <!-- Global site tag (gtag.js) - Google Analytics -->
        <script async src="https://www.googletagmanager.com/gtag/js?id=UA-154434342-1"></script>
        <script>
          window.dataLayer = window.dataLayer || [];
          function gtag(){dataLayer.push(arguments);}
          gtag('js', new Date());

          gtag('config', 'UA-154434342-1', {
              'page_title': 'Página Fale Conosco',
              'page_path': '/contato.aspx'
          });
        </script>


    </form>
</body>
</html>