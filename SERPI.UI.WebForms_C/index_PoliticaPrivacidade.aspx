<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_PoliticaPrivacidade.aspx.cs" Inherits="SERPI.UI.WebForms_C.index_PoliticaPrivacidade" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Política de Privacidade</title>

    <style>
        @keyframes img-ani {
          from{opacity:0;}
          to{opacity: 1;}
        }

        .bannerRosto_interno {
            background: url('img/homepage/privacy-policy.jpg');
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
                    <h1 class="text-center"><strong>Política de Privacidade</strong></h1>
                </div>
                   
            </section>
            
            <br />
            <br />
            <div class="container" >
                <div class="row">
                    <div class="col-md-12" style="font-family: sans-serif; text-align: justify; font-size: 1.8rem;">

                        <h2><strong>Política de Privacidade</strong></h2>
                        <br />

                        <p style="line-height:1.7em">
                            <strong>1. Objetivos </strong><br />
                            Estabelecer o compromisso que o Instituto de Pesquisas Tecnológicas do Estado de São Paulo - IPT tem com a privacidade e a proteção dos dados pessoais coletados, de acordo com a Lei Geral de Proteção de Dados (Lei 13.709, de 14/08/2018).
                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            <strong>2. Fundamentação Legal </strong><br />
                            Lei Federal 13.709, de 14/08/2018 (Lei Geral de Proteção de Dados).
                        </p>
                        <br />   
                        <p style="line-height:1.7em">
                            <strong>3. Orientações Gerais </strong><br />
                            Como condição para acesso e uso das funcionalidades exclusivas do site e dos serviços prestados pelo IPT o titular dos dados declara que fez a leitura completa e atenta da presente Política de Privacidade, estando plenamente ciente, conferindo, assim, sua livre e expressa concordância com os termos aqui estipulados, autorizando a obtenção dos dados e informações aqui mencionados, bem como sua utilização para os fins especificados.
                            <br /><br />
                            Caso não esteja de acordo com estas Diretivas, o titular dos dados pessoais deverá descontinuar o acesso ao site ou entrar em contato pelo e-mail <a class="a_faq" href="mailto:privacidade@ipt.br">privacidade@ipt.br</a>.
                        </p>                        
                        <br />
                        <p style="line-height:1.7em">
                            <strong>3.1. Direitos do titular dos dados </strong><br />
                            De acordo com o artigo 18 da Lei federal 13.709/2018 Lei Geral de Proteção de Dados são direitos do titular: 
                            <br /><br />
                            • Confirmação da existência de tratamento;
                            <br /><br />
                            • Acesso aos dados;
                            <br /><br />
                            • Correção de dados incompletos, inexatos ou desatualizados;
                            <br /><br />
                            • Anonimização, bloqueio ou eliminação de dados desnecessários, excessivos ou tratados em desconformidade;
                            <br /><br />
                            • Eliminação dos dados tratados com consentimento do usuário; e
                            <br /><br />
                            • Revogação do consentimento.
                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            <strong>3.2. Como seus dados pessoais são utilizados </strong><br />
                            Os dados pessoais fornecidos voluntariamente para o IPT, ao utilizar o serviço de correio eletrônico ou qualquer formulário de contato, serão tratados minimamente para responder ou atender à solicitação da finalidade específica necessária:
                            <br /> 
                            <table class="table table-striped table-bordered">
                              <thead>
                                <tr>
                                  <th scope="col" style="width:50%">Para que usamos seus dados</th>
                                  <th scope="col" style="width:50%">Nossos motivos</th>
                                </tr>
                              </thead>
                              <tbody>
                                <tr>
                                  <td>Nossos serviços: Assessoria Técnica e Estudos, Ensaios, Análises, Calibrações e Aferições, Atividades Educacionais, Elaboração/Cessão e Licenciamento de Programas, Projetos de Pesquisa e Desenvolvimento.</td>
                                  <td>• Cumprir contratos<br />
                                      • Negociação de diferentes modelos contratuais<br />
                                      • Elaboração de propostas<br />
                                      • Obrigações legais<br />
                                      • Nossos interesses legítimos: coletas de dados em campo, emissão de documentos técnicos
                                   </td>
                                </tr>
                                <tr>
                                  <td>Publicações e patentes</td>
                                  <td>• Transferência de conhecimento</td>
                                </tr>
                                <tr>
                                  <td>Atendimento ao cliente</td>
                                  <td>• Responder questionamentos</td>
                                </tr>
                                <tr>
                                  <td>Eventos</td>
                                  <td>• Workshops, Lives, Congressos, Seminários, “IPT Portas Abertas”<br />
                                      • Cumprir obrigações contratuais<br />
                                      • Fazer divulgação
                                  </td>
                                </tr>
                                <tr>
                                  <td>Parcerias técnicas com Instituições Internacionais</td>
                                  <td>• Relações internacionais<br />
                                      • Programa de Desenvolvimento e Capacitação no Exterior<br />
                                      • Recebimento de Pesquisador Visitante
                                  </td>
                                </tr>
                                <tr>
                                  <td>Redes Sociais</td>
                                  <td>• Responder questionamentos</td>
                                </tr>
                                 <tr>
                                  <td>Websites</td>
                                  <td>• Responder questionamentos<br />
                                      • Elaboração de propostas<br />
                                      • Emissão de documentos técnicos
                                  </td>
                                </tr>
                                 <tr>
                                  <td>Motivos legais</td>
                                  <td>• Quando requerido pela lei em resposta a procedimentos legais<br />
                                      • Em resposta a pedido de autoridade legal<br />
                                      • Proteção de direitos<br />
                                      • Fazer cumprir termos de acordos e contratos
                                  </td>
                                </tr>
                                 <tr>
                                  <td>Operação da área de Recursos Humanos e Benefício</td>
                                  <td>• Cumprir obrigações com os empregados do Instituto<br />
                                      • Concurso público<br />
                                      • Oferta de estágio<br />
                                      • Menor aprendiz<br />
                                      • Creche
                                  </td>
                                </tr>
                                <tr>
                                  <td>Controle de acessos</td>
                                  <td>• Cadastro de visitantes</td>
                                </tr>
                                <tr>
                                  <td>Aquisições</td>
                                  <td>• Licitações, contratos, suprimentos e compras</td>
                                </tr>
                              </tbody>
                            </table>
                            <br />
                            Os dados fornecidos serão acessados somente por profissionais do IPT devidamente autorizados, responsáveis diretos pelos processos de auditoria, de segurança, de estatística e de execução dos pedidos solicitados pelo titular do dado, respeitando os princípios de proporcionalidade, necessidade e relevância, além do compromisso de confidencialidade e preservação da privacidade nos termos desta Política de Privacidade.

                        </p>

                        <br />
                        <p style="line-height:1.7em">
                            <strong>3.3. Como armazenamos os seus dados pessoais eletrônicos </strong><br />
                            Os dados pessoais coletados eletronicamente e os registros de atividades são armazenados nos servidores corporativos locais, e na nuvem do IPT, localizados no Brasil, em ambiente seguro e controlado, pelo menos no prazo exigido por lei de 6 (seis) meses, conforme artigo 15 do Marco Civil da Internet (Lei 12965, de 23.04.2014).
                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            <strong>3.4. Uso de Cookies </strong><br />
                            O IPT utiliza cookies, cabendo ao titular dos dados configurar o seu navegador de Internet caso deseje bloqueá-los, ficando o titular ciente de que algumas funcionalidades do site poderão não estar disponíveis após esse bloqueio. Os dados são coletados a partir da adesão voluntária ao uso do site.
                            <br /><br />
                            <u>Exemplos de utilização de Cookies:</u>
                            <br />
                            <table class="table table-striped table-bordered">
                              <thead>
                                <tr>
                                  <th scope="col" style="width:25%">Cookie usado/tipo e categoria</th>
                                  <th scope="col" style="width:25%">Objetivo</th>
                                  <th scope="col" style="width:25%">Que dados são coletados?</th>
                                  <th scope="col" style="width:25%">Detalhes</th>
                                </tr>
                              </thead>
                              <tbody>
                                <tr>
                                  <td>
                                      Cookies de Desempenho<br /><br />
                                      <u>Tipo:</u> Analíticos<br /><br />
                                      <u>Categoria:</u> Persistentes/ de Sessão/ de Terceiros
                                  </td>
                                  <td>
                                      Ajudam-nos a entender como os visitantes interagem com nosso site fornecendo informações sobre as áreas visitadas, o tempo gasto e quaisquer problemas encontrados, como erros de mensagem. Isso nos ajuda a melhorar o desempenho de nossos sites.
                                  </td>
                                  <td>
                                      Eles não o identificam como indivíduo. Todos os dados são coletados e agregados anonimamente.
                                  </td>
                                  <td>
                                      <a class="a_faq" href="https://developers.google.com/analytics/devguides/collection/analyticsjs/cookie-usage" target="_blank">Google Analytics</a><br /><br />
                                      <a class="a_faq" href="https://support.google.com/adsense/answer/2839090?hl=en" target="_blank"> DoubleClick</a>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                      Cookies de Compartilhamento Social (também conhecidos como Cookies “de Terceiros” ou “widgets Sociais”)<br /><br />
                                      <u>Tipo:</u> Midia Social /Compartilhamento<br /><br />
                                      <u>Categoria:</u> de Terceiros
                                  </td>
                                  <td>
                                      O compartilhamento social oferecido no  site é executado por terceiros. Esses terceiros podem colocar Cookies no seu computador quando você usa recursos de compartilhamento social no Site, ou se você já tiver feito o login neles. Esses Cookies ajudam a melhorar sua experiência no Site. Permitem que você compartilhe comentários / avaliações / páginas / indicadores e ajudam a dar acesso às redes sociais e às ferramentas sociais online com maior facilidade
                                  </td>
                                  <td>
                                      Esses Cookies podem coletar dados pessoais que você divulgou voluntariamente, como seu nome de usuário.
                                  </td>
                                  <td>
                                      <a class="a_faq" href="https://www.ghostery.com/en/apps/facebook_social_plugins" target="_blank">Facebook</a><br /><br />
                                      <a class="a_faq" href="https://www.ghostery.com/en/apps/twitter_button" target="_blank">Twitter</a>
                                  </td>
                                </tr>
                                
                              </tbody>
                            </table>
                            <br />
                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            <strong>3.5. Dados tratados por terceiros </strong><br />
                            Terceiros que realizam processamento de quaisquer dados coletados pelo IPT deverão, obrigatoriamente, respeitar as condições aqui estipuladas e as normas de Segurança da Informação do IPT.
                            <br /><br />
                            O IPT não comercializa, cede ou compartilha seus registros para quaisquer instituições públicas ou privadas, sem que tenha a obrigatoriedade de cumprimentos legais, regulamentares e contratuais, ou sem que tenha o consentimento do titular dos dados pessoais.
                            <br /><br />
                            Outrossim, se tais registros evidenciarem a ocorrência de violação às normas e regulamentos do IPT, ou às Leis e Regulamentos vigentes que ferem os direitos de Privacidade, o IPT oferecerá denúncia às autoridades judiciais, administrativas ou governamentais competentes sempre que houver requerimento, requisição ou ordem judicial. Sendo assim, o IPT prestará toda a cooperação para elucidação da questão, inclusive disponibilizando às autoridades todos os registros e dados de auditoria que se fizerem necessários, compartilhando o mínimo de informações necessárias para a finalidade.
                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            <strong>4. Disposições gerais </strong><br />
                            <strong>4.1. Alterações em nossa Política de Privacidade </strong><br />
                            O IPT se reserva o direito de alterar o teor desta Política de Privacidade a qualquer momento, sempre que julgar necessário, para adequação e conformidade legal, conforme a finalidade ou necessidade, tendo a sua vigência no ato da publicação. A continuação do uso do site após qualquer modificação nesta Política de Privacidade constituirá sua aceitação das modificações realizadas.
                            <br /><br />
                            Ocorrendo atualizações neste documento e que demandem nova coleta de consentimento, o IPT notificará o titular pelos meios de contato por ele fornecidos.
                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            <strong>4.2. Responsabilidade do titular dos dados </strong><br />
                            O titular é corresponsável pelo sigilo de seus dados pessoais. O compartilhamento de senhas e dados de acesso ao site viola esta Política de Privacidade.
                            <br /><br />
                            O titular poderá fazer cumprir quaisquer de seus direitos enviando uma mensagem para <a class="a_faq" href="mailto:privacidade@ipt.br">privacidade@ipt.br</a>. No caso de revogação de consentimento, esta ação poderá impedir o bom funcionamento do site.
                            <br /><br />
                            Em caso de dúvidas com relação às disposições constantes nesta Política de Privacidade, o titular dos dados pessoais poderá entrar em contato a Encarregada de Dados Pessoais, Silvana Betlei Murbak, pelo endereço de e-mail <a class="a_faq" href="mailto:privacidade@ipt.br">privacidade@ipt.br</a>.

                        </p>
                        <br />
                        <p style="line-height:1.7em">
                            <strong>5. Glossário </strong><br />
                            <strong>Banco de dados:</strong> conjunto estruturado de dados pessoais, estabelecido em um ou em vários locais, em suporte eletrônico ou físico.
                            <br /><br />
                            <strong>Cookies:</strong> arquivos de internet que armazenam dados quando você visita um site, para facilitar a sua navegação, como seu endereço de e-mail, preferências de pesquisas no Google, cidade de onde você está conectado, logins e senhas, histórico de navegação, desempenho do computador etc.
                            <br /><br />
                            <strong>Dado pessoal:</strong> informação relacionada a pessoa natural identificada ou identificável, tais como: nome, RG, endereço de e-mail, endereço residencial, telefone, celular, fotografia etc.
                            <br /><br />
                            <strong>Dado pessoal sensível:</strong> dado pessoal sobre origem racial ou étnica, convicção religiosa, opinião política, filiação a sindicato ou a organização de caráter religioso, filosófico ou político, dado referente à saúde ou à vida sexual, dado genético ou biométrico, quando vinculado a uma pessoa natural.
                            <br /><br />
                            <strong>Titular de dados pessoais:</strong> pessoa natural a quem se referem os dados pessoais que são objeto de tratamento. 

                        </p>

                    </div>
                </div>
                <br />
                <hr />
                <br />

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
              'page_title': 'Página Política de Privacidade',
              'page_path': '/index_PoliticaPrivacidade.aspx'
          });
        </script>

    </form>
</body>
</html>