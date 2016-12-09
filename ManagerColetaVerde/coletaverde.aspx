<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coletaverde.aspx.cs" Inherits="ManagerColetaVerde.coletaverde" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coleta Verde</title>
     <!--JQUERY-->
            
            <script src="js/jquery.js" type="text/javascript"></script>
            <script src="js/jquery-ui.js" type="text/javascript"></script>
            <link href="css/jquery-ui.css" rel="Stylesheet" type="text/css" />
            <!--script src="js/jquery.maskedinput.js" type="text/javascript"></script-->
            <script src="js/jquery.maskMoney.js" type="text/javascript"></script>
            <script src="js/jQuery-Mask-Plugin-master/src/jquery.mask.js" type="text/javascript"></script>
            <script src="bootstrap/js/bootstrap-select.js" type="text/javascript"></script>
           <link href=" bootstrap/css/bootstrap.css" rel="Stylesheet" type="text/css" />
            <link href=" bootstrap/css/bootstrap-select.css" rel="Stylesheet" type="text/css" />
            <script src="bootstrap/js/bootstrap.js"></script>
          <link rel="stylesheet" href="http://cdn.leafletjs.com/leaflet/v0.7.7/leaflet.css" />
        <script src="http://cdn.leafletjs.com/leaflet/v0.7.7/leaflet.js"></script>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
        <%--<script src="Scripts/jquery.js" type="text/javascript"></script>--%>
        <script type="text/javascript" src="../dist/Leaflet.Coordinates-0.1.5.min.js"></script>
	    <link rel="stylesheet" href="../dist/Leaflet.Coordinates-0.1.5.css"/>
        <script src="js/server.js" type="text/javascript"></script>
    <style>
        html, body {
        height: 100%;
        margin: 0;
        padding: 0;
      }
      #map {
        
       
        height: 95%;
        width: 100%;
        vertical-align:bottom;        
        bottom: 0px;
        left: 0px;
      }
       #manager {
 
        border-radius: 3px;
        height: 5%;
        width: 100%;     
        top: 0px;
        left: 0px;
      }
       /* navbar */
.navbar-default {
    background-color: #74c76b;
    border-color: #74c76b;
    height:100%;
}
/* title */
.navbar-default .navbar-brand {
    color: #fff;
}
.navbar-default .navbar-brand:hover,
.navbar-default .navbar-brand:focus {
    color: #5E5E5E;
}
/* link */
.navbar-default .navbar-nav > li > a {
    color: #ffffff;
}
.navbar-default .navbar-nav > li > a:hover,
.navbar-default .navbar-nav > li > a:focus {
    color: #333;
}
.navbar-default .navbar-form{
    width:90%;
}
.navbar-default .navbar-nav > .active > a, 
.navbar-default .navbar-nav > .active > a:hover, 
.navbar-default .navbar-nav > .active > a:focus {
    color: #555;
    background-color: #5a9a52;
}
.navbar-default .navbar-nav > .open > a, 
.navbar-default .navbar-nav > .open > a:hover, 
.navbar-default .navbar-nav > .open > a:focus {
    color: #555;
    background-color: #D5D5D5;
}
/* caret */
.navbar-default .navbar-nav > .dropdown > a .caret {
    border-top-color: #777;
    border-bottom-color: #777;
}
.navbar-default .navbar-nav > .dropdown > a:hover .caret,
.navbar-default .navbar-nav > .dropdown > a:focus .caret {
    border-top-color: #333;
    border-bottom-color: #333;
}
.navbar-default .navbar-nav > .open > a .caret, 
.navbar-default .navbar-nav > .open > a:hover .caret, 
.navbar-default .navbar-nav > .open > a:focus .caret {
    border-top-color: #555;
    border-bottom-color: #555;
}
/* mobile version */
.navbar-default .navbar-toggle {
    border-color: #DDD;
}
.navbar-default .navbar-toggle:hover,
.navbar-default .navbar-toggle:focus {
    background-color: #DDD;
}
.navbar-default .navbar-toggle .icon-bar {
  background-color: #CCC;
}
@media (max-width: 767px) {
  .navbar-default .navbar-nav .open .dropdown-menu > li > a {
      color: #ffffff;
    }
  .navbar-default .navbar-nav .open .dropdown-menu > li > a:hover,
  .navbar-default .navbar-nav .open .dropdown-menu > li > a:focus {
          color: #333;
    }
}

    </style> 
</head>
<body>
   
        
    
        <div id="manager" >
            <nav class="navbar navbar-default">
              <div class="container-fluid">
                <div class="navbar-header">
                  <img title="Coleta Verde" style="width:50px;height:45px;" src="img/coletaverde.png" />
                </div>
              
                <form class="navbar-form navbar-left" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
                  <div class="input-group">
                      <div style="height:100%; width:100%;">
                          <select style="width:20%;" class="form-control" id="estado">
                            <option value="0">Selecione o Estado</option>                               
                            <option>Minas Gerais</option>                          
                          </select>
                           <select style="width:20%;" class="form-control" id="cidade">
                            <option>Selecione o Cidade</option>
                            <option value ="1">Belo Horizonte</option>                       
                          </select>
                          <input style="width:20%;" id="bairro" type="text" class="form-control"  placeholder="Bairro">
                          <input  style="width:20%;"id="logradouro" type="text" class="form-control"  placeholder="Logradouro">
                          <input  style="width:10%;"id="numero" type="text" class="form-control"  placeholder="Numero">
                           <button type="button" class="btn btn-default" onclick="geocode();">
                                 <i class="glyphicon glyphicon-search"></i>
                           </button>
                      </div>
                      
                       
            
                    
                  </div>
                </form>
              </div>
            </nav>
                

        </div>
         <div id="map"style="width:100%;"></div>
   
    
 
    <script>
        var map;
        var URLBASE = "http://localhost:52253/";
        window.onload = inicial();
        var ilum = L.layerGroup();
        var select = L.layerGroup();
        var markeratual;//pega o marker que vai ser cadastrado
        var imagematual;//imagem que vai ser cadastrada
        var coletaatual=[];//array de string de tipos de coletas que vao ser cadastrado
        var locationposition;// a latitude e longitude que vai ser cadastrada
        var endEstadoatual;
        var endCidadeAtual;
        var endBairroAtual;
        var endLogradouroAtual;
        var endNumeroAtual;
        var placeSearch, autocomplete;
        var componentForm = {
            street_number: 'short_name',
            route: 'long_name',
            locality: 'long_name',
            administrative_area_level_1: 'short_name',
            country: 'long_name',
            postal_code: 'short_name'
        };
        var redIcon;
        function inicial()
        {
            
            var grayscale = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
                maxZoom: 18
            });

            //inicializa o mapa com o mapabase openstreetmao variavel grayscale
             map = L.map('map', {
                layers: grayscale,

            }).setView([-19.9700, -43.9270], 9);//seta visualização inicial e zoom inicial respectivamente

            //colcoa scala no maoa
             L.control.scale().addTo(map);
            //coloca mouse move coordenadas no maoa
             L.control.coordinates().addTo(map);
             redIcon= L.icon({
                 iconUrl: 'img/map-marker-icon.png',                    
                 iconSize: [35, 35], // size of the icon
             });
            
        }
        function getBaseUrl() {
            return window.location.href.match(/^.*\//);
        }
        function changebox(obj)
        {
            var boxes = $('div.cadastro_checkbox input[type=checkbox]:checked');
            coletaatual = [];
            $.each(boxes, function (i, check) {
               coletaatual.push([check.value]);
            });

          

        }
        function insert()
        {
            if (coletaatual.length > 0)
            {
                var resp = insertPonto(markeratual.getLatLng().lat, markeratual.getLatLng().lng, imagematual, endEstadoatual, endCidadeAtual, endBairroAtual, endLogradouroAtual, endNumeroAtual, coletaatual.toString());//chamar função que esta no arquivo js/server.js        
                if (resp) {
                    var result = selectPontos();
                    var json = $.parseJSON(result);
                    if (json && markeratual) {
                        ilum.removeLayer(markeratual);
                    }

                    $.each(json, function (i, jsondata) {

                        SelectMarkerMap(jsondata.id_posto, jsondata.lat, jsondata.lng, jsondata.foto, jsondata.hr_atendimento,coletaatual);
                    })

                }

            }
            else {
                alert("Por favor escolha o tipo de material e tente novamente.")
            }
          
            


            

        }

        function deletar(lat,lng,id)
        {
            deletarPontos(lat, lng, id);
            var teste = "";
            var markerremove;
            for (i in select._layers)
            {
                if(id==select._layers[i].options.id)
                {
                    markerremove = select._layers[i];
                }
            }
            if (markerremove)
            {
                select.removeLayer(markerremove);
            }
           
                



            //var markerremove = select.getLayerId(id);
            //alert(markerremove)
            //select.removeLayer(markerremove);

        }
        $(document).ready(function () {
                var materiais = [];
                var result = selectPontos();            
                var json = $.parseJSON(result);
                if (json && markeratual)
                {
                    if(ilum)
                     ilum.removeLayer(markeratual);
                }

                $.each(json, function (i, jsondata) {

                    var retorno = selectmaterial(jsondata.id_posto);
                    var jsonmaterial = $.parseJSON(retorno);
                    
                    if (retorno)
                    {
                        $.each(jsonmaterial, function (j, jsonm) {

                            materiais.push(
                               [
                                   jsonm.nom_material,
                                   jsonm.id_material,
                                   jsonm.id_posto


                               ]);

                        });
                        SelectMarkerMap(jsondata.id_posto, jsondata.lat, jsondata.lng, jsondata.foto, jsondata.hr_atendimento, materiais);
                    }
                   

                    
                   
                })
            
          
            
            
        });
           

        

        function setMarkerMap(lat,lng)
        {
            

            //boto~es checkbox para tipo de cole do posto
            var checkbox = '<br><div class="cadastro_checkbox"><label><input type="checkbox" onclick="changebox(this)" value="1">Papel</label>&ensp;<label><input type="checkbox" onclick="changebox(this)" value="2">Plástico</label>&ensp;<label><input  onclick="changebox(this)" type="checkbox" value="3">Vidro</label>&ensp;<label><input onclick="changebox(this)" type="checkbox" value="4">Metal</label></div>';
            //botão de cadastro do posto
            var btnconfirma = '<button type="button" style="width:45%; margin:5px;" class="btn btn-success" onclick="insert()">Cadastrar</button>';
            //botão de remover o posto
            var btncancela = '<button type="button"  style="width:45%; margin:5px;" class="btn btn-danger" onclick="">Remover</button>';
            //input para upload da imagem
            var btnimagem = '<input type="file" onchange="previewFile(this)" name="filename" accept="image/gif, image/jpeg, image/png">';
            //visualizador da imagem
            var preview = '<img class="cadastroimg" style="width:200px;height:100px;top: 50%;left: 50%;" src="' + URLBASE + 'img/posto_de_coleta.png"/>';
            //input dia/hora funcionamneto
            var diahora = "";

            if (markeratual) {
                ilum.removeLayer(markeratual);
            }
            
            
            //criando um ponto no mapa e adicionando elementos dentro da popup
            markeratual = L.marker([lat, lng],{icon:redIcon}).bindPopup('<strong>Ponto</strong><br>Mais um teste.<br>' + btnimagem + '<br><div style="width:150px;height:60px;">' + preview + '</div><br><br>' + checkbox + '<br><br><div class="row">' + btnconfirma + ' '  + '</div></div>');
            
                ilum.addLayer(markeratual);
                map.addLayer(ilum);
                map.setView([lat, lng], 18);//visualização do mapa para o marker e o zoom 18

            }
            function SelectMarkerMap(pk,lat, lng,urlimagem,hr_funcionamento,materiais) {
                var checkbox = '<br><div class="' + pk + '_checkbox">';
                for (var i = 0; i < materiais.length; i++)
                {
                   
                    if (materiais[i][2] != null)
                    {
                        checkbox += '<label><input type="checkbox" value="' + materiais[i][1] + '" checked="true">' + materiais[i][0] + '</label>&ensp;';
                    }
                    else {
                        checkbox += '<label><input type="checkbox" value="' + materiais[i][1] + '" checked="false">' + materiais[i][0] + '</label>&ensp;';
                    }
                   
                }
                checkbox += '</div>';
                //boto~es checkbox para tipo de cole do posto
                // checkbox = '<br><div class="' + pk + '_checkbox"><label><input type="checkbox" value="1" >Papel</label>&ensp;<label><input type="checkbox" value="2">Plástico</label>&ensp;<label><input type="checkbox" value="3">Vidro</label>&ensp;<label><input type="checkbox" value="4">Metal</label></div>';
                //botão de cadastro do posto
                var btnconfirma = '<button type="button" style="width:45%; margin:5px;" class="btn btn-success" onclick="">Atualizar</button>';
                //botão de remover o posto
                var btncancela = '<button type="button"  style="width:45%; margin:5px;" class="btn btn-danger" onclick="deletar(' + lat + ',' + lng + ',' + pk + ')">Remover</button>';
                //input para upload da imagem
                var btnimagem = '<input type="file" onchange="previewFileSelect(this,' + pk + ')" name="filename" accept="image/gif, image/jpeg, image/png">';
                //visualizador da imagem
              
                if (urlimagem != "null" && urlimagem != "" && urlimagem != null)
                {
                    var preview = '<img id="' + pk + '_img" style="width:200px;height:100px;top: 50%;left: 50%;" src=" ' + URLBASE + urlimagem + '"/>';
                }
                else {
                    var preview = '<img id="' + pk + '_img" style="width:200px;height:100px;top: 50%;left: 50%;" src=""/>';
                }
                
                //criando um ponto no mapa e adicionando elementos dentro da popup
                var marker = L.marker([lat, lng], { id: pk }).addTo(map).bindPopup('<strong>Ponto</strong><br>Mais um teste.<br>' + btnimagem + '<br><div style="width:150px;height:60px;">' + preview + '</div><br><br>' + checkbox + '<br><br><div class="row">'  + ' ' + btncancela + '</div></div>');
                
                var boxes = $('#' + pk + '_checkbox input[type=checkbox]');
                
                //$.each(boxes, function (i, check) {
                //    alert(check.checked);
                //    for (var i = 0; i < materiais.length; i++)
                //    {
                //        if(materiais[i]==check.value)
                //        {
                           
                //            check.checked = true;
                //        }
                //    }
                   
                //});
                select.addLayer(marker);
                map.addLayer(select);
                map.setView([-19.9700, -43.9270], 9);//visualização do mapa para o marker e o zoom 18

            }
            //seta a imagem no visualizador da popup e coloca dentro da variavel
            function previewFile(obj) {
          
                var file = obj.files[0]; //sames as here
                var reader = new FileReader();
            
            
                reader.onloadend = function () {
                    $('.cadastroimg').attr('src', reader.result);
                    imagematual = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file); //reads the data as a URL
                } else {
                    $('.cadastroimg').attr('src', '');
                }
            }
            function previewFileSelect(obj,id) {

                var file = obj.files[0]; //sames as here
                var reader = new FileReader();


                reader.onloadend = function () {
                    $('#'+id+'_img').attr('src', reader.result);//cria a visualização dpara os marker ja cadastrados
                    // imagematual = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file); //reads the data as a URL
                } else {
                    $('#' + id + '_img').attr('src', '');
                }
            }
      

          
           
            function geocode()
            {
                //concatena os valores digitados nos campos
                var address = $("#estado option:selected").text() + " " + $('#cidade option:selected').text() + " " + $('#bairro').val() + " " + $('#logradouro').val() + " " + $('#numero').val();
                //monta a url para geocode do google
                var url = "http://maps.google.com/maps/api/geocode/json?address=" + address;
                //limpa as variaveis
                endEstadoatual = "";
                endCidadeAtual = "";
                endBairroAtual = "";
                endLogradouroAtual = "";
                endNumeroAtual = "";
                //request url para retorna lat e long
                $.getJSON(url, function(data) {
                    if (data.status == "OK")//verifica se geocode deu certo
                    {                      
                            var lng = data.results[0].geometry.location.lng;//pega a longitude
                            var lat = data.results[0].geometry.location.lat;//pega a latitude  
                            //seta os dados de endereço em variaveis separadas para cadastrar no banco
                            endEstadoatual = $('#estado').val();
                            endCidadeAtual = $('#cidade').val();
                            endBairroAtual = $('#bairro').val();
                            endLogradouroAtual = $('#logradouro').val();
                            endNumeroAtual = $('#numero').val();

                            setMarkerMap(lat, lng);  //coloca o marker no mapa                     

                    }
                    else {
                        alert("Endereço não encontrado.");
                    }
                    


                });
            }
       
            

    </script>
      
  
</body>
</html>
