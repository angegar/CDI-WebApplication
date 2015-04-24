var map;
var geocoder;
var coords = new Array();
var clubs;

function InitMap(targetDiv) {
    geocoder = new google.maps.Geocoder();
    var mapOptions = {
        zoom: 12
    };

    map = new google.maps.Map(document.getElementById(targetDiv), mapOptions);
}

function DisplayInfo(info, res) {
    if (info) {
        info = info.toLowerCase();
        res = res + info;
        res = res + "</br>";
    }

    return res;
}

function CreateInfoBulle(club) {
    var res = "";

    res = "<b>" + club.NomClub + "</b>";
    res = res + "</br>";

    if (club.SiteWeb) {
        club.SiteWeb = club.SiteWeb.toLowerCase();
        res = "<a href='" + club.SiteWeb + "'>" + res + "</a>";
    }
    /*
    if (club.Addresse) {
    club.Addresse = club.Addresse.toLowerCase();
    club.Ville = club.Ville.toLowerCase();
    res = res + club.Addresse + ' ' + club.Ville;
    res = res + "</br>";
    }
    */
    res = DisplayInfo(club.Adr_Dojo, res);
    res = DisplayInfo(club.Ville, res);
    //res = displayInfo(club.Horaires, res);
    res = DisplayInfo(club.Telephone, res);

    return res;
}

function CreateMarker(club) {
    var markerOptions = {
        map: map,
        position: new google.maps.LatLng(club.Latitude, club.Longitude),
        title: club.NomClub
        //icon: "../Img/karat.jpg"

    };

    var marker = new google.maps.Marker(markerOptions);
    return marker;
}

function CreateInfoWin(club, marker) {
    var contentString = CreateInfoBulle(club);
    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });

    /* google.maps.event.addListener(marker, 'mouseover', function () {
    infowindow.open(map, marker);
    });*/

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    })

    /*google.maps.event.addListener(marker, 'mouseout', function () {
    infowindow.close();
    });*/

    return infowindow;
}

function DisplayClubs(clubsInfo) {
    var town;
    var addr;
    var codePostal;
    clubs = clubsInfo;

    var bounds = new google.maps.LatLngBounds();
    var point = [];
    for (var i = 0; i < clubs.length; i++) {
        clubs[i].marker = CreateMarker(clubs[i]);
        clubs[i].infoBulle = CreateInfoWin(clubs[i], clubs[i].marker);
        var lat = clubs[i].Latitude;
        var long = clubs[i].Longitude;
        if (lat != 0 && long != 0 && lat != undefined && long != undefined) {
            bounds.extend( new google.maps.LatLng(lat, long));
        }
    }

    var center = bounds.getCenter();
    map.setCenter(center);
    
    //map.panTo(center);
    map.fitBounds(bounds);
}
