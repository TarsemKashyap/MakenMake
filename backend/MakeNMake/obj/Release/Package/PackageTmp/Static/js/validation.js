function ValidateNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode >= 48 && charCode <= 57) {
        return true;
    }
    else if (charCode == 8 || charCode == 9) {
        return true;
    }
    else {
        alert('Please input Numbers only');
        return false;
    }
}
function ValidateDecimalNumber(evt,element) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (
        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
        (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function ValidateCharacters(evt) {
    var keyCode = (evt.which) ? evt.which : evt.keyCode
    if (keyCode == 32 || keyCode == 8 || keyCode == 9) {
        return true;
    }
    else if ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122)) {
        return true;
    }
    else {
        alert('Please input Character only');
        return false;
    }
}
function ValidateServiceCharacters(evt) {
    var keyCode = (evt.which) ? evt.which : evt.keyCode
    if (keyCode == 32 || keyCode == 8 || keyCode == 9 || keyCode == 219 || keyCode == 221) {
        return true;
    }
    else if ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122)) {
        return true;
    }
    else {
        alert('Please input Character only');
        return false;
    }
}

function ValidateDate(Val_date) {
    var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    if(Val_date.match(dateformat)){
        var seperator1 = Val_date.split('/');
        var seperator2 = Val_date.split('-');

        if (seperator1.length>1)
        {
            var splitdate = Val_date.split('/');
        }
        else if (seperator2.length>1)
        {
            var splitdate = Val_date.split('-');
        }
        var dd = parseInt(splitdate[0]);
        var mm  = parseInt(splitdate[1]);
        var yy = parseInt(splitdate[2]);
        var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];
        if (mm==1 || mm>2)
        {
            if (dd>ListofDays[mm-1])
            {
                alert('Invalid date format!');
                return false;
            }
        }
        if (mm==2)
        {
            var lyear = false;
            if ( (!(yy % 4) && yy % 100) || !(yy % 400))
            {
                lyear = true;
            }
            if ((lyear==false) && (dd>=29))
            {
                alert('Invalid date format!');
                return false;
            }
            if ((lyear==true) && (dd>29))
            {
                alert('Invalid date format!');
                return false;
            }
        }
    }
    else
    {
        alert("Invalid date format!");
        return false;
    }
}