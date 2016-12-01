app.controller('MainController', ['$scope', function ($scope) {
    /*getProps.success(function (data) {
        $scope.proprietarios = data;

    });*/
    $scope.CNPJ = '17.317.421/0001-43';
    $scope.proprietarios = [];
    
    $scope.getProps = function () {
        $.ajax({
            type: "POST",
            url: "/Service.asmx/GetProps",
            data: "{ 'prefix': '" + $scope.CNPJ + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success:
                
                function (data) {
                alert(data.d)
                var parsed = $.parseJSON(data.d);

                $.each(parsed, function (i, jsondata) {
                    $scope.proprietarios.push({

                        id: jsondata.COD_PROPRIETARIO_PK,
                        nome: jsondata.NOME,
                        CPF: jsondata.CPF
                    });
                    
                });

            },
            error: function (XHR, errStatus, errorThrown) {
                var err = JSON.parse(XHR.responseText);
                errorMessage = err.Message;
                alert(errorMessage);
            }
        });
    };

    $scope.removeProp = function () {
        alert("Id do usuário é " + $scope.item.id);
    }

}]);
