function insertPonto(lat,lng,imagem,estado,cidade,bairro,logradouro,numero,tipocoleta)
{
    var result;
        if (imagem)
        {
            imagem = imagem.split(",")[1];
        }
        else {
            imagem = "";
        }
    
        if (imagem.length < 20000)
        {
            //função oara chamar função webservice interno inserir posto  WEB
            $.ajax({
                async: false,
                url: '/service.asmx/InsertPonto',//url da web service
                type: "POST",//tipo de envio
                data: "{ 'lat':'" + lat + "','lng':'" + lng + "','imagem':'" + imagem + "','estado':'" + estado + "','cidade':'" + cidade + "','bairro':'" + bairro + "','logradouro':'" + logradouro + "','numero':'" + numero + "','tipocoleta':'" + tipocoleta + "'}",//parametros da função
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data2) {
                    alert(data2.d);//retorno da função quando não ocorrer exceções
                    result = true;
                },
                failure: function (response) {
                    alert("Falha: " + response.d);
                },
                error: function (response) {
                    alert("Erro: " + response.d);
                }
            });

        }
        else {
            alert("Imagem muito grande.")
        }

    return result
   
}
function selectPontos()
{
    var json;
    
   //função oara chamar função webservice interno inserir posto  WEB
    $.ajax({
        async: false,
        url: '/service.asmx/GetPontos',//url da web service
        type: "POST",//tipo de envio      
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data2) {
            json =data2.d;//retorno da função quando não ocorrer exceções
        },
        failure: function (response) {
            alert("Falha: " + response.d);
        },
        error: function (response) {
            alert("Erro: " + response.d);
        }
    });

    return json;

    
}
function selectmaterial(id) {
    var json;

    //função oara chamar função webservice interno inserir posto  WEB
    $.ajax({
        async: false,
        url: '/service.asmx/GetMaterial',//url da web service
        data: "{ 'id_posto':'" + id + "'}",//parametros da função
        type: "POST",//tipo de envio      
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data2) {
            json = data2.d;//retorno da função quando não ocorrer exceções
        },
        failure: function (response) {
            alert("Falha: " + response.d);
        },
        error: function (response) {
            alert("Erro: " + response.d);
        }
    });

    return json;


}
function deletarPontos(lat,lng,id)
{
    $.ajax({
        async: false,
        url: '/service.asmx/DeletarPonto',//url da web service
        type: "POST",//tipo de envio
        data: "{ 'lat':'" + lat + "','lng':'" + lng + "','id':'" + id + "'}",//parametros da função
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data2) {
            alert(data2.d);//retorno da função quando não ocorrer exceções
        },
        failure: function (response) {
            alert("Falha: " + response.d);
        },
        error: function (response) {
            alert("Erro: " + response.d);
        }
    });



}
