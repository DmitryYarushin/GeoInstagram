function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 12,
        center: {lat: 55.160658, lng: 61.403528}
    });

    markers = [];
    $.each(data, function () {
        var icon = new google.maps.MarkerImage(
            this.Thumbnail.Url,
            null, /* size is determined at runtime */
            null, /* origin is 0,0 */
            null, /* anchor is bottom center of the scaled image */
            new google.maps.Size(70, 70)
        );

        var marker = new google.maps.Marker({
            position: {lat: parseFloat(this.Location.Latitude), lng: parseFloat(this.Location.Longitude)},
            map: map,
            icon: icon
        });
        markers.push(marker);
    });

    var markerCluster = new MarkerClusterer(map, markers,
        {imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m'});

}

$(document).ready(function () {
    initMap();
});