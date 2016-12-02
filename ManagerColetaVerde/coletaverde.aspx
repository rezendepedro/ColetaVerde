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
    <style>
        html, body {
        height: 100%;
        margin: 0;
        padding: 0;
      }
      #map {
        
       
        height: 94%;
        width: 100%;
        vertical-align:bottom;        
        bottom: 0px;
        left: 0px;
      }
       #manager {
 
        border-radius: 3px;
        height: 6%;
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
   
        
    
        <div id="manager">
            <nav class="navbar navbar-default">
              <div class="container-fluid">
                <div class="navbar-header">
                  <img title="Coleta Verde" style="width:50px;height:50px;" src="img/coletaverde.png" />
                </div>
              
                <form class="navbar-form navbar-left" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
                  <div class="input-group">
                    <input id="autocomplete" type="text" class="form-control" onFocus="geolocate()" placeholder="Search">                   
            
                    <div class="input-group-btn">
                      <button class="btn btn-default" type="submit">
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
        window.onload = inicial();
        var markeratual;
        var locationposition;
        var placeSearch, autocomplete;
        var componentForm = {
            street_number: 'short_name',
            route: 'long_name',
            locality: 'long_name',
            administrative_area_level_1: 'short_name',
            country: 'long_name',
            postal_code: 'short_name'
        };
        var greenIcon = L.icon({
            iconUrl: 'lib/images/ckpoint_verde.png',
            shadowUrl: 'lib/images/marker-shadow.png',

            iconSize: [20, 35], // size of the icon
            shadowSize: [25, 40], // size of the shadow
            iconAnchor: [20, 35], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 40],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });
        var redIcon = L.icon({
            iconUrl: 'lib/images/ckpoint_vermelho.png',
            shadowUrl: 'lib/images/marker-shadow.png',

            iconSize: [20, 35], // size of the icon
            shadowSize: [25, 40], // size of the shadow
            iconAnchor: [20, 35], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 40],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });
        var blueIcon = L.icon({
            iconUrl: 'lib/images/ckpoint_azul.png',
            shadowUrl: 'lib/images/marker-shadow.png',

            iconSize: [20, 35], // size of the icon
            shadowSize: [25, 40], // size of the shadow
            iconAnchor: [20, 35], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 40],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });

        function inicial()
        {
            var grayscale = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
                maxZoom: 18
            });

             map = L.map('map', {
                layers: grayscale,

            }).setView([-19.9700, -43.9270], 12);


            L.control.scale().addTo(map);
            L.control.coordinates().addTo(map);

        }
        function alerta(messagem)
        {
            alert("teste");
        }

        function setMarkerMap(lat,lng,icon)
        {
            var checkbox = '<div class="checkbox"><label><input type="checkbox" value="papel">Papel</label>  <label><input type="checkbox" value="metal">Metal</label><label><input type="checkbox" value="plastico">Plástico|</label><label><input type="checkbox" value="organico">Organico</label></div>';
            var btnconfirma = '<button type="button" style="width:45%; margin:5px;" class="btn btn-success" onclick="alerta()">Cadastrar</button>';
            var btncancela = '<button type="button"  style="width:45%; margin:5px;" class="btn btn-danger" onclick="alerta()">Remover</button>';
            markeratual = L.marker([lat, lng]).addTo(map).bindPopup('<strong>Ponto</strong><br>Mais um teste.<br><br>' + checkbox + '<br><br><div class="row">' + btnconfirma + ' ' + btncancela + '</div></div>');
            map.setView([lat,lng],18);

        }
      

        /////////////////////////////////////google geocode
        function geolocate() {
           
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {                    
                    locationposition = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };                  
                   
                });
            }
        }
        function fillInAddress() {
            // Get the place details from the autocomplete object.
           
            var place = autocomplete.getPlace();
            setMarkerMap(place.geometry.location.lat(), place.geometry.location.lng(), blueIcon);
           
         
            

            
        }
       
        function initAutocomplete() {
            // Create the autocomplete object, restricting the search to geographical
            // location types.
            autocomplete = new google.maps.places.Autocomplete(
                /** @type {!HTMLInputElement} */(document.getElementById('autocomplete')),
                { types: ['geocode'] });

            // When the user selects an address from the dropdown, populate the address
            // fields in the form.
            autocomplete.addListener('place_changed', fillInAddress);
        }

    </script>
      
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCBAHeOuZu1IdAYuywncZ5vB5vzG9K-QtY&libraries=places&callback=initAutocomplete"
        async defer></script>
     
</body>
</html>
