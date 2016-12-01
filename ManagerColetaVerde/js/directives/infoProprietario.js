/*app.directive('infoProprietario', function () {
    return {
        restrict: 'E',
        scope: {
            info: '='
        },
        templateUrl: 'js/directives/infoProprietario.html'
    };
});*/

app.directive('infoProprietario', function () {

    return {
        scope: {
            item: '=infoProprietario'
        },
        restrict: 'EA',
        template:
            
            "<td style='white-space: nowrap;padding-left: 10px; padding-right: 10px; border-right: 1px solid #cccccc;'>"+
                "{{ item.nome }}"+
            "</td>"+
            "<td style='white-space: nowrap;padding-left: 10px; padding-right: 10px; border-right: 1px solid #cccccc;'>" +
                '{{ item.CPF }}'+
            "</td>"+
            "<td style='white-space: nowrap;padding-left: 0px; padding-right: 0px; text-align:center;'>"+
                "<span style='cursor: pointer;' ng-click='removeProp();' class='glyphicon glyphicon-remove'></span>" +
            "</td>"
    };
})
