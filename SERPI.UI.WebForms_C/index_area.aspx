<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_area.aspx.cs" Inherits="SERPI.UI.WebForms_C.index_area" %>

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
            background: url('img/homepage/cursos/mestrado.jpg');
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

    </style>

    <asp:Literal ID="litGoogle" runat="server"></asp:Literal>

</head>
<body>
    <form id="form2" runat="server">
        <div>
            <section id="sectionBanner" runat="server" class="bannerRosto_interno">

                <div id="texto-img" class="text-center">
                    <h1 class="text-center">
                        <strong>
                            <asp:Label ID="lblTipoCurso" runat="server" Text="adipiscing"></asp:Label>
                            <asp:Label ID="lblTipoCurso_en" runat="server" Text="Master's Degree"></asp:Label>
                        </strong>
                    </h1>

                    <%--<a id="btn-inscrevase" class="btn btn btn-success" href="#" onclick=""><strong>Inscreva-se</strong> </a>--%>
                </div>

            </section>

            <br />
            <br />
            <div class="container">
                <%--<div>
                    <h3 class="widget-title" style="color:#016699">MESTRADOS PROFISSIONAIS</h3>
                </div>--%>

                <div class="row">
                    <div class="col-md-4 pull-right hidden">
                        <img class="img-rounded img-responsive" src="img/Homepage/MESTRADO.jpg" />
                        <div>
                            <br />
                            <br />
                        </div>
                    </div>


                    <div class="col-md-12" > <%--style="font-family: sans-serif; text-align: justify; font-size: 1.8rem;"--%>
                        <asp:Literal ID="litDescricao" runat="server"></asp:Literal>
                        <%--<p>
                            O Instituto de Pesquisas Tecnológicas em já formou mais de 1.200 mestres, por meio do seu ensino superior de Mestrado Profissional.
                            <br /><br />
                            O mestrado tem como objetivo formar profissionais aptos a planejar e a desenvolver projetos inovadores e de base tecnológica, dirigidos para a solução prática.
                            <br /><br />
                            Os profissionais que ingressam no curso, tem como vantagem a experiência dos docentes, profissionais do IPT, que vivenciam a prática de pesquisas, estudos e projetos, voltados à tecnologia e inovação.
                            <br /><br />

                            O IPT oferece o curso de Mestrado Profissional nas seguintes áreas:
                            <br /><br />
                        </p>--%>
                    </div>
                </div>

            </div>

            <br />

            <div class="container">
                <div class="row">
                    <asp:Literal ID="litCursos" runat="server"></asp:Literal>

                    <div class="col-md-4 hidden">
                        <div class="thumbnail" onclick="fGoToCurso(1)">
                            <div class="caption text-center">
                                <br />
                                <img style="width: 280px; height: 200px" class="center-block img-rounded img-responsive" src="img/Homepage/cursos/habitacao.jpg" title="Habitação: Planejamento e Tecnologia" alt="Habitação: Planejamento e Tecnologia" />
                                <br />
                                <h4 id="thumbnail-label" style="color: #016699">Habitação: Planejamento e Tecnologia</h4>
                                <%--<p><i class="glyphicon glyphicon-user light-red lighter bigger-120"></i>&nbsp;&nbsp;Auditor</p>--%>
                                <%--<div class="thumbnail-description smaller">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin sed mi ut leo vulputate eleifend sit amet in lacus. Suspendisse vehicula erat sit amet porttitor ullamcorper. Nulla sed dignissim ipsum. Sed ut sapien auctor, consectetur odio quis, commodo diam. Pellentesque malesuada quam sit amet ullamcorper ornare. Etiam pretium velit et nisi commodo gravida. Curabitur sagittis justo tellus, ac scelerisque tortor aliquam in.</div>--%>
                            </div>
                            <div class="caption card-footer text-center">
                                <ul class="list-inline">
                                    <li>Saiba mais...</li>
                                </ul>
                            </div>
                        </div>


                        <%--<div class="thumbnail">
                            <div class="caption text-center" onclick="location.href='https://flow.microsoft.com/en-us/connectors/shared_slack/slack/'">
                                <div class="position-relative">
                                    <img src="https://az818438.vo.msecnd.net/icons/slack.png" style="width: 72px; height: 72px;" />
                                </div>
                                <h4 id="thumbnail-label"><a href="https://flow.microsoft.com/en-us/connectors/shared_slack/slack/" target="_blank">Microsoft Slack</a></h4>
                                <p><i class="glyphicon glyphicon-user light-red lighter bigger-120"></i>&nbsp;Auditor</p>
                                <div class="thumbnail-description smaller">Slack is a team communication tool, that brings together all of your team communications in one place, instantly searchable and available wherever you go.</div>
                            </div>
                            <div class="caption card-footer text-center">
                                <ul class="list-inline">
                                    <li><i class="people lighter"></i>&nbsp;7 Active Users</li>
                                    <li></li>
                                    <li><i class="glyphicon glyphicon-envelope lighter"></i><a href="#">&nbsp;Help</a></li>
                                </ul>
                            </div>
                        </div>--%>



                        <%--<div class="service-item">
                            <a href="/pos-graduacao/seguranca-de-barragens/8/1">
                                <figure class="effect-apollo">
                                    <img src="img/Homepage/68417-seguranca_em_barragens.jpg" title="Habitação: Planejamento e Tecnologia" alt="Habitação: Planejamento e Tecnologia" />
                                    <div class="effect-apollo__overlay"></div>
                                </figure>
                            </a>
                            <h4 class="service-item__title titulo-cursos-mais-procurados"><a href="/pos-graduacao/seguranca-de-barragens/8/1">Habitação: Planejamento e Tecnologia</a></h4>

                        </div>--%>
                    </div>

                    <%--<div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-4">
                        <div class="thumbnail" onclick="fGoToCurso(1)">
                            <br />
                            <div class="caption text-center" >
                                <img style="width:280px;height:200px" class="center-block img-rounded img-responsive" src="img/Homepage/cursos/processos industriais.jpg" title="Processos Industriais" alt="Processos Industriais" />
                                <br />
                                <h4 style="color:#016699">Processos Industriais</h4>
                            </div>
                            <div class="caption card-footer text-center">
                                <ul class="list-inline">
                                    <li>Saiba mais...</li>
                                </ul>
                            </div>
                        </div>

                    </div>--%>
                </div>
            </div>

            <div id="divBotoes" class="container" runat="server" visible="false">
                <br />
                <hr />
                <br />

                <div class="row">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-lg center-block" href="#" onclick="fModalProcessoSeletivo()">
                            <i class="fa fa-address-book"></i> <span class="cssCurso_pt">Processo Seletivo</span><span class="cssCurso_en">Selection Process</span>
                        </button>
                    </div>
                    <div class="hidden-lg hidden-md">
                        <br />
                    </div>

                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary btn-lg center-block" href="#" onclick="fModalCalendario()">
                            <i class="fa fa-calendar"></i> <span class="cssCurso_pt">Calendário</span><span class="cssCurso_en">Calendar</span>
                        </button>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="modal fade" id="divModalProcessoSeletivo" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title"><i class="fa fa-address-book fa-lg"></i> <span id="divModProcesso_Language">Processo Seletivo</span></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblProcessoSeletivo" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="lblProcessoSeletivo_en" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>

                            <hr size="100" width="100%" noshade color="#9D9D9D" />

                            <div class="row">
                                <div class="col-lg-12 pull-right">
                                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal"><i class="fa fa-close"></i>Fechar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal fade" id="divModalCalendario" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title"><i class="fa fa-calendar fa-lg"></i> <span id="divModCalendario_Language">Calendário</span></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblCalendario" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="lblCalendario_en" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <hr size="100" width="100%" noshade color="#9D9D9D" />

                            <div class="row">
                                <div class="col-lg-12 pull-right">
                                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal"><i class="fa fa-close"></i>Fechar</button>
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
            if (document.getElementById('hTipoCurso').value == '1') {
                    fLanguage(document.getElementById('hLanguage').value);
            }
            else {
                fLanguage('pt');
            }

            $(document).ready(function () {
                //alert('entrou')
                //if (document.getElementById('hTipoCurso').value == '1') {
                //    fLanguage(document.getElementById('hLanguage').value);
                //}
                //else {
                //    fLanguage('pt');
                //}

                $(".ls-wp-container").remove();

                $(this).scrollTop(0);
            });

            function fLanguage(qIdioma) {
                document.getElementById("<%=lblTipoCurso.ClientID%>").style.display = "none";
                document.getElementById("<%=lblTipoCurso_en.ClientID%>").style.display = "none";
                document.getElementById("idDescricao").style.display = "none";
                document.getElementById("idDescricao_en").style.display = "none";
                document.getElementById("<%=lblCalendario.ClientID%>").style.display = "none";
                document.getElementById("<%=lblCalendario_en.ClientID%>").style.display = "none";
                document.getElementById("<%=lblProcessoSeletivo.ClientID%>").style.display = "none";
                document.getElementById("<%=lblProcessoSeletivo_en.ClientID%>").style.display = "none";
                $('.cssCurso_pt').hide();
                $('.cssCurso_en').hide();
                if (qIdioma == "en") {
                    document.getElementById("<%=lblTipoCurso_en.ClientID%>").style.display = "block";
                    document.getElementById("idDescricao_en").style.display = "block";
                    //document.getElementById("lblProcesso_Language").innerHTML = "Selection Process";
                    document.getElementById("divModProcesso_Language").innerHTML = "Selection Process";
                    //document.getElementById("lblCalendario_Language").innerHTML = "Calendar";
                    document.getElementById("divModCalendario_Language").innerHTML = "Calendar";
                    document.getElementById("<%=lblCalendario_en.ClientID%>").style.display = "block";
                    document.getElementById("<%=lblProcessoSeletivo_en.ClientID%>").style.display = "block";
                    $('.cssCurso_en').show();
                }
                else {
                    document.getElementById("<%=lblTipoCurso.ClientID%>").style.display = "block";
                    document.getElementById("idDescricao").style.display = "block";
                    //document.getElementById("lblProcesso_Language").innerHTML = "Processo Seletivo";
                    document.getElementById("divModProcesso_Language").innerHTML = "Processo Seletivo";
                    //document.getElementById("lblCalendario_Language").innerHTML = "Calendário";
                    document.getElementById("divModCalendario_Language").innerHTML = "Calendário";
                    document.getElementById("<%=lblCalendario.ClientID%>").style.display = "block";
                    document.getElementById("<%=lblProcessoSeletivo.ClientID%>").style.display = "block";
                    $('.cssCurso_pt').show();
                }
            }

            function fGoToCurso(qIdCurso) {
                $('#result').load('index_curso.aspx?qIdCurso=' + qIdCurso);
            }

            function fModalProcessoSeletivo() {
                $('#divModalProcessoSeletivo').modal();
            }

            function fModalCalendario() {
                $('#divModalCalendario').modal();
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
    </form>
</body>
</html>
