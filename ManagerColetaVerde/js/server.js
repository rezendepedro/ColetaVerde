function insertPonto(lat,lng,imagem)
{
   //função oara chamar função webservice interno inserir posto  WEB
    $.ajax({
        url: '/service.asmx/InsertPonto',//url da web service
        type: "POST",//tipo de envio
        data: "{ 'lat':'"+lat+"','lng':'"+lng+"','imagem':'"+imagem.split(",")[1]+"'}",//parametros da função
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data2) {
            alert(data2.d);//retorno da função quando não ocorrer exceções
        },
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });
}
