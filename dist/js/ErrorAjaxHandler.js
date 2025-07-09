var wasSubmitted = false;
var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_initializeRequest(InitializeRequest);
prm.add_endRequest(EndRequest);
var postBackElement;
var intervalCheckCookieDW;
function InitializeRequest(sender, args) {
    $('#TBlock').modal({
        backdrop: 'static',
        keyboard: false
    });
    intervalCheckCookieDW = setInterval(checkCookieDW, 1000);
}

function EndRequest(sender, args) {
    wasSubmitted = false;
    $("#TBlock").modal('hide');
    if (args.get_error() != undefined) {
        var errorMessage;
        if (args.get_response().get_statusCode() == '200') {
            errorMessage = args.get_error().message;
        }
        else {
            errorMessage = 'Error: Error no especificado. ';
        }
        args.set_errorHandled(true);
        var n = errorMessage.split(":");
        var msjFinal = "";
        for (var i = 1; i < n.length; i++) {
            msjFinal = msjFinal.concat(n[i]);
        }
        $("#textoError").html(msjFinal.replace(/\$\$/g, "<br/ >"));
        $('#TError').modal();
    }
    clearInterval(intervalCheckCookieDW);
}