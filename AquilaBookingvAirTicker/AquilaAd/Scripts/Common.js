// JScript File
function confirmAction(event, message) {
    if (confirm(message)) {
        return;
    } else {
        if (event.preventDefault) {
            event.preventDefault();
        } else {
            event.returnValue = false;
        }
    }
}

function currency(which) {

    currencyValue = which.value;
    currencyValue = currencyValue.replace(",", "");
    fullPart = currencyValue;
    newStr = "";
    for (i = 0; i < fullPart.length; i++) {
        newStr = fullPart.substring(fullPart.length - i - 1, fullPart.length - i) + newStr;
        if (((i + 1) % 3 == 0) & ((i + 1) > 0)) {
            if ((i + 1) < fullPart.length) {
                newStr = "," + newStr;
            }
        }
    }
    which.value = newStr;
}

function normalize(which) {
    alert("Normal");
    val = which.value;
    val = val.replace(",", "");
    which.value = val;
}

