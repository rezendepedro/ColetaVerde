function insertPonto(lat,lng,imagem)
{
    imagem = imagem.split(",")[1];
    console.log("-latlng: "+lat+","+lng+"\n-image:"+imagem)
   //função oara chamar função webservice interno inserir posto  WEB
    $.ajax({
        url: '/service.asmx/InsertPonto',//url da web service
        type: "POST",//tipo de envio
        data: "{ 'lat':'"+lat+"','lng':'"+lng+"','imagem':'"+imagem+"'}",//parametros da função
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
