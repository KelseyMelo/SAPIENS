/**
 * @author
 * Máscara do inputs
 */
$(document).ready(function() {
    $("#cep").mask("99999-999");
    $("#cpf").mask("999.999.999-99");
    $("#data_nascimento").mask("99/99/9999");
    $("#tel").mask("(00)0000.00009");
    $("#tel_celular").mask("(00)0000.00009");
    $("#tel_comercial").mask("(00)0000.00009");
    $("#tel_res").mask("(00)0000.00009");
    $("#tel_cel").mask("(00)0000.00009");
    $("#tel_com").mask("(00)0000.00009");
});
/**
 * @author
 * Escolhe investimento através da cidade e curso
 */
function escolhe_inscricao(cidade_id, curso_id, turma_id, nome_cidade) {
    var base_url = $("#base_url").val();
    var token = $("#_token").val();
    $("#conteudo_investimento").load("/escolhe-investimento/" + cidade_id + "/" + curso_id + "/" + turma_id);
    $("#conteudo_investimento").fadeOut();
    $("#conteudo_investimento").fadeIn();
    // $(".panel").css("backgroundColor", "#F5F5F5");
    // $(".panel").css("boxShadow", "0px 0px 0px #FFFFFF");
    //$(".panel-" + cidade_id + "-" + curso_id).css("backgroundColor", "#DDDDDD");
    // $(".panel-" + cidade_id + "-" + curso_id).css("boxShadow", "1px 1px 3px #333333");
    if (nome_cidade) {
        $("#cidade_escolhida_invest").html('Cidade Escolhida: ' + nome_cidade);
    }
}

function preInscricao(id_turma) {
    var base_url = $("#base_url").val();
    var token = $("#_token").val();
    $.ajax({
        url: base_url + '/go-pre-cadastro',
        type: 'post',
        dataType: 'json',
        data: {
            id_turma: id_turma,
            _token: token
        },
        success: function(json) {
            if (json.resp == '1') {
                location = base_url + '/inscricao/pre-inscricao';
            } else if (json.resp == '0') {
                alert('oi');
            }
        }
    });
}
/**
 * author
 * Localizar endereço a partir do cep PF
 */
$('#cep').change(function() {
    var value = $(this).val();
    var cep = value.replace("-", "")
    $.ajax({
        url: 'http://api.common.admcsc.com.br/ceps/' + cep,
        type: 'GET',
        dataType: 'json',
        success: function(json) {
            $("#endereco").val(json[0].Tipo + " " + json[0].Logradouro).css("background-color", "#e5fee1");
            $("#bairro").val(json[0].Bairro).css("background-color", "#e5fee1");
            $("#estado").val(json[0].UF).css("background-color", "#e5fee1").attr('readonly', 'readonly');
            $("#cidade").val(json[0].Municipio).css("background-color", "#e5fee1").attr('readonly', 'readonly');
        },
    });
});
$("#bto_convenio").click(function() {
    var base_url = $("#base_url").val();
    var token = $("#_token").val();
    var id_convenio = $("#id_convenio").val();

    if(!id_convenio.trim()) {
        return false;
    }
    id_convenio = id_convenio.toUpperCase(); //Deixar maiuscula
    $.ajax({
        url: '/convenio/valida',
        type: 'GET',
        dataType: 'json',
        data: {
            _token: token,
            chave: 'codigo',
            id_convenio: id_convenio
        },
        success: function(data) {
            if(data.convenio_codigo){
                location = '/inscricao/forma-pagamento';
            }else{
                $("#codigo_invalido").fadeIn().css('display', 'block');
                setTimeout(function() {
                    $("#codigo_invalido").fadeOut().css('display', 'none');
                    //location = '/inscricao/forma-pagamento';
                }, 5000);
            }
        }
    });
});

// test property
// $('.auto-height').matchHeight({
//     property: 'height'
// });

// $(document).ready(function() {
//     "use strict";
//     var dropdownmenu = $('.dropdown-menu');
//     $(".dropdown").hover(
//         function() {
//             dropdownmenu, $(this).stop(true, true).slideDown("fast");
//             $(this).toggleClass('open');
//         },
//         function() {
//             //dropdownmenu, $(this).stop(true, true).slideUp("fast");
//             $(this).toggleClass('open');
//         }
//     );

//     $('.carousel').carousel({
//         interval: 5000 //changes the speed
//     })
// });

function verificaCodigoPromocional(){
    if($("#id_convenio").val()){
        var pergunta = confirm("Você digitou o código promocional e não validou, deseja continuar?");
        if(pergunta == true){
            return true;
        } else{
            $("#id_convenio").focus();
            return false;
        }
    }
}

$(document).ready(function() {
    $("#select-estado").change(function(){
        $(".panel").hide();
        $(".option-cidade").hide();

        $("#select-cidade").val("");

        $(".estado-"+$(this).val()).show();
        $(".option-"+$(this).val()).show();
    });
    $("#select-cidade").change(function(){
        if($(this).val()){
            $(".panel").hide();
            $(".cidade-"+$(this).val()).show();
        } else{
            $(".panel").hide();
            $(".option-cidade").hide();

            $("#select-cidade").val("");

            $(".estado-"+$("#select-estado").val()).show();
            $(".option-"+$("#select-estado").val()).show();
        }
    });
    $(".option-cidade").hide();
});

$(document).ready(function() {
    $("#gera-boleto").click(function(){
        var idTurma = $("#idTurma").val();
        var cpf = $("#cpf").val();

        $.ajax({
            url: '/verifica-status-envio-boleto',
            type: 'GET',
            data: {idTurma: idTurma, cpf: cpf},
            dataType: 'json',
            success: function(data) {
                //console.log(data);
                if(data.StatusEnvio_ContaReceber == 0){
                    $("#tp-pagamento").val("2");
                } else if(data.status == 1){
                    $("#tp-pagamento").val("21");
                }
            },
        });

        return false;

    });
});