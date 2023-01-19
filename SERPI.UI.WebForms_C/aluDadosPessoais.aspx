<%@ Page Title="" Language="C#" MasterPageFile="~/SERPI.Master" AutoEventWireup="true" CodeBehind="aluDadosPessoais.aspx.cs" Inherits="SERPI.UI.WebForms_C.aluDadosPessoais" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
    <input type="hidden" id ="hGrupoMenu"  name="hGrupoMenu" value="liAlunoDadosCadastrais" /> 
    <input type="hidden" id ="hItemMenu"  name="hItemMenu" value="liAlunoDadosCadastrais" />
    <input type="hidden" id ="hEscolheuFoto"  name="hEscolheuFoto" value="false" />

    <script src="Scripts/jquery.mask.min.js"></script>
            
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <input type="hidden" id ="hCodigoAluno"  name="hCodigoAluno" value="value" />

    <!-- Select2 -->
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="plugins/select2/select2-bootstrap.min.css" rel="stylesheet"/>
      
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

    </style>

    <div class="row"> 
        <div class="col-md-9">
            <h3 class="">&nbsp;&nbsp;&nbsp;<i class="fa fa-user-circle-o"></i>&nbsp;Perfil do aluno</h3>
        </div>

    </div>

    <div class="container-fluid">

        <div class="row">
            <div class="col-lg-10">
                <div class="box box-widget widget-user-2">
                    <div class="widget-user-header bg-aqua-active">
                        <div class="widget-user-image">
                            <img id="imgAluno" onerror="fImagemDefaultMaster(this);" runat="server" style="border: 2px solid white" class="img-redondo2" src="img/pessoas/40708.jpg?26/03/2019 10:02:06" alt="Imagem do aluno"/>
                        </div>
                        <h3 class="widget-user-username"><asp:Label ID="lblNomeAluno" runat="server" Text=""></asp:Label></h3>
                        <h5 class="widget-user-desc"><asp:Label ID="lblCargoAluno" runat="server" Text=""></asp:Label></h5>
                    </div>
                
                    <div class="box-footer">
                        <div class="row">
                            <div class="col-sm-6 border-right">
                                <div class="description-block">
                                <h5 class="description-header">Matrícula</h5>
                                <span class="description-text"><asp:Label ID="lblMatriculaAluno" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-6 border-right">
                                <div class="description-block">
                                <h5 class="description-header">CPF</h5>
                                <span class="description-text"><asp:Label ID="lblCPFAluno" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>

                        </div>

                        <h4 style="color:black"><strong>Dados Pessoais</strong></h4>
                        <hr style="border-bottom:2px solid #3c8dbc" />

                        <div class="row">
                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">CEP</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtCepAluno" type="text" /></span>
                                </div>
                            </div>

                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Logradouro</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtLogradouroAluno" type="text" maxlength="100"/></span>
                                </div>
                            </div>

                            <div class="col-sm-1 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Número</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtNumeroAluno" type="text" maxlength="20"/></span>
                                </div>
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Complemento</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtComplementoAluno" type="text" maxlength="50"/></span>
                                </div>
                            </div>

                             <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Bairro</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtBairroAluno" type="text" maxlength="50"/></span>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">País</h5>
                                    <span class="">
                                        <asp:DropDownList runat="server" ID="ddlPaisAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false" onchange="fPaisResidencia()">
                                        </asp:DropDownList>
                                    </span>
                                </div>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlEstadoAluno" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-sm-2 border-right">
                                        <div class="description-block">
                                            <h5 class="description-header text-left">Estado</h5>
                                            <span class="">
                                                <div runat="server" id ="divddlEstadoAluno">
                                                    <asp:DropDownList runat="server" ID="ddlEstadoAluno" ClientIDMode="Static" class="form-control input-sm select2" OnSelectedIndexChanged="ddlEstadoAluno_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div runat="server" id ="divtxtEstadoAluno">
                                                    <input class="form-control input-sm" runat="server" id="txtEstadoAluno" type="text" value="" maxlength="50" />
                                                </div>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 border-right">
                                        <div class="description-block">
                                            <h5 class="description-header text-left">Cidade</h5>
                                            <span class="">
                                                <div runat="server" id ="divddlCidadeAluno">
                                                    <asp:DropDownList runat="server" ID="ddlCidadeAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </div>
                                                <div runat="server" id ="divtxtCidadeAluno">
                                                    <input class="form-control input-sm" runat="server" id="txtCidadeAluno" type="text" value="" maxlength="50"/>
                                                </div>
                                            </span>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Data Nascimento</h5>
                                    <input class="form-control input-sm" runat="server" id="txtDataNascimento" type="date" />
                                </div>
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Gênero</h5>
                                    <asp:DropDownList runat="server" ID="ddlSexoAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                        <asp:ListItem Text="Informe o Gênero" Value="" />
                                        <asp:ListItem Text="Masculino" Value="m" />
                                        <asp:ListItem Text="Feminino" Value="f" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Celular</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtCelularAluno" type="text" /></span>
                                </div>
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Telefone</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtTelefone" type="text" /></span>
                                </div>
                            </div>

                            <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Email principal</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtEmailPrincipalAluno" type="email" maxlength="100"/></span>
                                </div>
                            </div>

                            <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Email Secundário</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtEmailSecundarioAluno" type="email" maxlength="100"/></span>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Nacionalidade</h5>
                                    <asp:DropDownList runat="server" ID="ddlNacionalidadeAluno" ClientIDMode="Static" class="form-control select2 input-sm " AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Profissão</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtProfissaoAlunoAluno" type="text" maxlength="200"/></span>
                                </div>
                            </div>

                            <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Estado Civil</h5>
                                    <asp:DropDownList runat="server" ID="ddlEstadoCivilAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                        <asp:ListItem Text="Selecionar o Estado Civil" Value="" />
                                        <asp:ListItem Text="Indefinido" Value="Indefinido" />
                                        <asp:ListItem Text="Solteiro" Value="Solteiro" />
                                        <asp:ListItem Text="Casado" Value="Casado" />
                                        <asp:ListItem Text="Separado" Value="Separado" />
                                        <asp:ListItem Text="Divorciado" Value="Divorciado" />
                                        <asp:ListItem Text="Viuvo" Value="Viuvo" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>

                        <br />

                        <br />

                        <h4 style="color:black"><strong>Documento de identificação</strong></h4>
                        <hr style="border-bottom:2px solid #3c8dbc" />

                        <div class="row">
                            <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Tipo Documento</h5>
                                    <asp:DropDownList runat="server" ID="ddlTipoDoctoAluno" ClientIDMode="Static" class="form-control input-sm select2 SemPesquisa" AutoPostBack="false">
                                        <asp:ListItem Text="Selecione Tipo de Docto" Value="" />
                                        <asp:ListItem Text="Registro Geral (Cédula de Identidade)" Value="Registro Geral (Cédula de Identidade)" />
                                        <asp:ListItem Text="Registro Geral de Estrangeiro" Value="Registro Geral de Estrangeiro" />
                                        <asp:ListItem Text="Protocolo do Registro Nacional de Estrangeiro" Value="Protocolo do Registro Nacional de Estrangeiro" />
                                        <asp:ListItem Text="Carteira de Identidade Militar" Value="Carteira de Identidade Militar" />
                                        <asp:ListItem Text="Passaporte" Value="Passaporte" />
                                        <asp:ListItem Text="Carteira de Identidade Especial p/ Estrangeiros" Value="Carteira de Identidade Especial p/ Estrangeiros" />
                                        <asp:ListItem Text="Certidão de Nascimento" Value="Certidão de Nascimento" />
                                        <asp:ListItem Text="Termo de Guarda e Responsabilidade" Value="Termo de Guarda e Responsabilidade" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Número</h5>
                                    <input class="form-control input-sm" runat="server" id="txtNumeroDoctoAluno" type="text" value="" maxlength="20"/>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-sm-1 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Dígito</h5>
                                    <input class="form-control input-sm" runat="server" id="txtDigitoDoctoAluno" type="text" value="" maxlength="1"  onkeypress="return soNumeroeX(event)"/>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Órgão Expedidor</h5>
                                    <input class="form-control input-sm" runat="server" id="txtOrgaoExpeditorAluno" type="text" value="" maxlength="10"/>
                                </div>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Data Expedição</h5>
                                    <%--<div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>--%>
                                        <input class="form-control input-sm" runat="server" id="txtDataExpedicaoAluno" type="date" value=""/>
                                    </div>
                                <%--</div>--%>
                            </div>
                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Data Validade</h5>
                                <%--<div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>--%>
                                    <input class="form-control input-sm" runat="server" id="txtDataValidadeDoctoAluno" type="date" value=""/>
                                <%--</div>--%>
                                </div>
                            </div>                    
                        </div>                                                    



                        <br />

                        <br />

                        <h4 style="color:black"><strong>Formação</strong></h4>
                        <hr style="border-bottom:2px solid #3c8dbc" />

                        <div class="row">
                            <div class="col-sm-5 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Formação</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtFormacaoAluno" type="text" value="" maxlength="100"/></span>
                                </div>
                            </div>

                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-sm-5 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Instituição</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtInstituicaoAluno" type="text" value="" maxlength="100"/></span>
                                </div>
                            </div>

                            <div class="hidden-lg hidden-md">
                                <br />
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Ano de Graduação</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtAnoFormacaoAluno" type="number" value="" maxlength="4" min="0"/></span>
                                </div>
                            </div>

                        </div>

                        <br />

                        <br />

                        <h4 style="color:black"><strong>Dados Comerciais</strong></h4>
                        <hr style="border-bottom:2px solid #3c8dbc" />

                        <div class="row">
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Empresa</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtEmpresaAluno" type="text" maxlength="200"/></span>
                                </div>
                            </div>

                            <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Cargo</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtCargoAluno" type="text" maxlength="100"/></span>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">CEP</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtCepEmpresaAluno" type="text" /></span>
                                </div>
                            </div>

                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Logradouro</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtLogradouroEmpresaAluno" type="text" maxlength="100"/></span>
                                </div>
                            </div>

                            <div class="col-sm-1 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Número</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtNumeroEmpresaAluno" type="text" maxlength="20"/></span>
                                </div>
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Complemento</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtComplementoEmpresaAluno" type="text" maxlength="50"/></span>
                                </div>
                            </div>

                             <div class="col-sm-3 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Bairro</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtBairroEmpresaAluno" type="text" maxlength="50"/></span>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">País</h5>
                                    <span class="">
                                        <asp:DropDownList runat="server" ID="ddlPaisEmpresaAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false" onchange="fPaisEmpresa()">
                                        </asp:DropDownList>
                                    </span>
                                </div>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlEstadoEmpresaAluno" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-sm-2 border-right">
                                        <div class="description-block">
                                            <h5 class="description-header text-left">Estado</h5>
                                            <span class="">
                                                <div runat="server" id ="divddlEstadoEmpresaAluno">
                                                    <asp:DropDownList runat="server" ID="ddlEstadoEmpresaAluno" ClientIDMode="Static" class="form-control input-sm select2" OnSelectedIndexChanged="ddlEstadoEmpresaAluno_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div runat="server" id ="divtxtEstadoEmpresaAluno">
                                                    <input class="form-control input-sm" runat="server" id="txtEstadoEmpresaAluno" type="text" value="" maxlength="50" />
                                                </div>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 border-right">
                                        <div class="description-block">
                                            <h5 class="description-header text-left">Cidade</h5>
                                            <span class="">
                                                <div runat="server" id ="divddlCidadeEmpresaAluno">
                                                    <asp:DropDownList runat="server" ID="ddlCidadeEmpresaAluno" ClientIDMode="Static" class="form-control input-sm select2" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </div>
                                                <div runat="server" id ="divtxtCidadeEmpresaAluno">
                                                    <input class="form-control input-sm" runat="server" id="txtCidadeEmpresaAluno" type="text" value="" maxlength="50"/>
                                                </div>
                                            </span>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="row">
                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Telefone</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtTelefoneEmpresaAluno" type="text" /></span>
                                </div>
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">Ramal</h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtRamalEmpresaAluno" type="number" maxlength="15"/></span>
                                </div>
                            </div>

                            <div class="col-sm-6 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left"><span class="piscante">Palavras-chave</span> <button type="button" title="informações" class="btn btn-circle-xs btn-purple" style="padding: 2px 3px;" onclick="fAbrePalavra()"><i class="fa fa-info fa-lg" style="margin-top:-2px" ></i></button> </h5>
                                    <span class="description-text"><input class="form-control input-sm" runat="server" id="txtPalavraChaveAluno" type="text" maxlength="2000"/></span>
                                </div>
                            </div>

                            <div class="col-sm-2 border-right">
                                <div class="description-block">
                                    <h5 class="description-header text-left">&nbsp;</h5>
                                    <button type="button" runat="server" id="btnSalvarDadosCadastrais" name="btnSalvarDadosCadastrais" onserverclick="btnSalvarDadosCadastrais_Click" class="btn btn-primary pull-right hidden" href="#" onclick="">
                                        <i class="fa fa-save"></i>&nbsp;Salvar Dados
                                    </button>
                                    <button type="button" runat="server" id="btnSalvarDadosCadastraisClone" class="btn btn-primary pull-right" onclick="fSalvarDadosCadastrais();">
                                        <i class="fa fa-save"></i>&nbsp;Salvar Dados
                                    </button>
                                </div>
                            </div>

                        </div>

                    </div>
              </div>
            </div>
        </div>
    </div>

    <asp:FileUpload ID="fileArquivoParaGravarOld" runat="server" CssClass="btn btn-primary btn-file hidden" Style="font-size: 9pt; font-family: Verdana" Width="622px" onchange="javascript:imagePreview(this);" />

    <div id="divPogress" class="loading" align="center">
        Processando... <br />Por favor, aguarde.
        <br />
        <img src="img/loader.gif" width="42" height="42" alt="" />
    </div>

    <div class="modal fade" id="divModalPalavra" tabindex="-1" role="dialog" aria-labelledby="CabecalhoMensagem" aria-hidden="true" style="display: none;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header alert-info">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">
                        Palabra-Chave</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Palavra-chave</b> é uma <b>palavra</b> que resume os temas principais de um texto. 
                        <br />
                        Identifica idéias e temas importantes e o objetivo principal, servir de referência às pesquisas e oferecimento de cursos do IPT. 
                        <br /><br />
                        INSTRUÇÕES:<br />
                        Indique as palavras chaves referente às áreas de conhecimento, interesse profissional e/ou acadêmica. 
                        <br /><br />
                        Não escrever de forma abreviada, separar as palavras por ponto e virgula (;) e escrever no singular.
                    </p>
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


        $('#<%=txtCepAluno.ClientID%>').mask('99999-999');
        $('#<%=txtCepEmpresaAluno.ClientID%>').mask('99999-999');

        $('#<%=txtCelularAluno.ClientID%>').mask('99-99999-9999');
        $('#<%=txtTelefone.ClientID%>').mask('99-9999-9999');

        $('#<%=txtTelefoneEmpresaAluno.ClientID%>').mask('99-9999-9999');

        //=========================================================
        function fSalvarDadosCadastrais() {
            document.getElementById('<%=btnSalvarDadosCadastrais.ClientID%>').click();
            return;
             if ((document.getElementById('<%=imgAluno.ClientID%>').src).indexOf("avatarunissex") != -1) {
                 document.getElementById('<% =lblTituloMensagem.ClientID%>').innerHTML = 'Atenção';
                 document.getElementById('<% =lblMensagem.ClientID%>').innerHTML = 'Por favor troque sua foto';
                 $("#divCabecalho").removeClass("alert-success");
                 $("#divCabecalho").removeClass("alert-danger");
                 $('#divCabecalho').addClass('alert-warning');
                 $('#divMensagemModal').modal();
             }
             else {
                 document.getElementById('<%=btnSalvarDadosCadastrais.ClientID%>').click();
             }
        }

        //=========================================================
        function fAbrePalavra() {
            $('#divModalPalavra').modal();
            
        }
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

        function fPaisResidencia() {
            var display = document.getElementById('<%=ddlPaisAluno.ClientID%>').value;
            if(display == "Brasil")
            {
                document.getElementById('<%=divddlEstadoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divddlCidadeAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divtxtEstadoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divtxtCidadeAluno.ClientID%>').style.display = 'none';
                fSelect2();
            }   
            else
            { 
                document.getElementById('<%=divddlEstadoAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divddlCidadeAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divtxtEstadoAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divtxtCidadeAluno.ClientID%>').style.display = 'block';
                fSelect2();
            }
        }

        function fPaisEmpresa() {
            var display = document.getElementById('<%=ddlPaisEmpresaAluno.ClientID%>').value;
            if(display == "Brasil")
            {
                document.getElementById('<%=divddlEstadoEmpresaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divddlCidadeEmpresaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divtxtEstadoEmpresaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divtxtCidadeEmpresaAluno.ClientID%>').style.display = 'none';
                fSelect2();
            }   
            else
            { 
                document.getElementById('<%=divddlEstadoEmpresaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divddlCidadeEmpresaAluno.ClientID%>').style.display = 'none';
                document.getElementById('<%=divtxtEstadoEmpresaAluno.ClientID%>').style.display = 'block';
                document.getElementById('<%=divtxtCidadeEmpresaAluno.ClientID%>').style.display = 'block';
                fSelect2();
            }
        }

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

        function fAlteracao(qRelatorio) {
            document.getElementById('<%=lblCargoAluno.ClientID%>').innerHTML = document.getElementById('<%=txtCargoAluno.ClientID%>').value;

            $.notify({
                icon: 'fa fa-thumbs-o-up fa-2x',
                title: '<strong>Alteração de Dados</strong><br /><br />',
                message: qRelatorio,

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
                    from: "top",
                    align: "center"
                }
            });
        }

    </script>

</asp:Content>
