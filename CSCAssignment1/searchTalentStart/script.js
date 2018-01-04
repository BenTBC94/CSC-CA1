$('#search').keyup(function () {
    //get data from json file
    var urlForJson = "data.json";


    //get data from Restful web Service in development environment
    //var urlForJson = "https://localhost:44317/api/Talents";

    //get data from Restful web Service in production environment
    //var urlForJson= "http://cscassignment120171126092912.azurewebsites.net";

    //Url for the Cloud image hosting
    var urlForCloudImage = "http://res.cloudinary.com/dkkiaa5wq/image/upload/v1511699298/talents/";

    var searchField = $('#search').val();
    var myExp = new RegExp(searchField, "i");
    $.getJSON('data.json', function (data) { // changed urlForJson to "data.json"
        var output = '<ul class="searchresults">';
        $.each(data, function (key, val) {
            //for debug
            console.log(data);
            if ((val.Name.search(myExp) != -1) ||
			(val.Bio.search(myExp) != -1)) {
                output += '<li>';
                output += '<h2>' + val.Name + '</h2>';
                //get the absolute path for local image
                //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                //get the image from cloud hosting
                output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                output += '<p>' + val.Bio + '</p>';
                output += '</li>';
            }
        });
        output += '</ul>';
        $('#update').html(output);
    }); //get JSON
});
