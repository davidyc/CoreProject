// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#UserMainInfoDiv > a').hide()   
    $('#UserAddInfoDiv > a').hide()
    var checked = $('#EditURLDIVCB').is(':checked');
    if (checked) {
        $('#URLINPUTDETAIL').show();
    }
    else {
        $('#URLINPUTDETAIL').hide();
    }
    $('#URLInput').hide()

    
});

$(document).ready(function () {
    $("#UserMainInfoDiv").click(function () {
        $('#UserMainInfoDiv > a').slideToggle(500)
    });
});

$(document).ready(function () {
    $("#UserAddInfoDiv").click(function () {
        $('#UserAddInfoDiv > a').slideToggle(500)
    });
});


$(document).ready(function () {
    $("#ExternalInput").click(function () {
        $('#URLInput').slideToggle(500)
    });
});


$(document).ready(function () {
    $("#EditURLDIVCB").click(function () {
        $('#URLINPUTDETAIL').slideToggle(500)
        var checked = $('#EditURLDIVCB').is(':checked');
        if (!checked) {
            $('#URLINPUTDETAILinput').val("");
        }       
    });
});


