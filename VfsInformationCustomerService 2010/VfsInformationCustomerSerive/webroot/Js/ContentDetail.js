var xmlHttpContentDetail;
var browserType = "IE";
var targetID;

function showContentTemplateDetail(source, contentTemplateDetailID)
{    
    var str;
    str = source.value;
    targetID = contentTemplateDetailID
    xmlHttpContentDetail = GetXmlHttpObjectDetail();
    
    if (xmlHttpContentDetail == null) {
        alert ("Your browser does not support AJAX!");
        return;
    } 
    //alert(xmlHttpContentDetail.onreadystatechange);
    var url = "AjaxGetContentTemplateDetails.aspx";
    url = url + "?contentTemplateID=" + str;
    xmlHttpContentDetail.onreadystatechange = DisplayContentDetail;
    xmlHttpContentDetail.open("GET",url,true);
    xmlHttpContentDetail.send(null);
} 

function DisplayContentDetail() 
{
    if (xmlHttpContentDetail.readyState == 4 || xmlHttpContentDetail.readyState == "complete") { 
        var contentDetailObj = document.getElementById(targetID);       
        contentDetailObj.innerHTML = xmlHttpContentDetail.responseText;        
	}
}

function GetXmlHttpObjectDetail() {
    var xmlHttp = null;
    try {
    // Firefox, Opera 8.0+, Safari
        xmlHttp = new XMLHttpRequest();
        browserType = "Others";
    }
    catch (e) {
       // Internet Explorer
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
            browserType = "IE";
        }
        catch (e){
           xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            browserType = "IE";
        }
     }
     
     if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)){ //test for MSIE x.x;
     var ieversion=new Number(RegExp.$1) // capture x.x portion and store as a number
     if (ieversion>=8)
        //document.write("You're using IE8 or above")
        browserType = "IE";
     else if (ieversion>=7)
        //document.write("You're using IE7.x")
        browserType = "IE";
     else if (ieversion>=6)
        //document.write("You're using IE6.x")
         browserType = "IE";
     else if (ieversion>=5)
        browserType = "IE";
        //document.write("You're using IE5.x")
}

     return xmlHttp;
}

function on_mouse_over(el) {
}

function on_mouse_out() {
	var popup = document.getElementById("popup");
	popup.style.display = "none";
}

function find_position(obj) {
	var curleft = curtop = 0;
	curleft = obj.offsetLeft;
	curtop = obj.offsetTop;

	if (obj.offsetParent) {
		while (obj = obj.offsetParent) {
			curleft += obj.offsetLeft;
			curtop += obj.offsetTop;
		}
	}
    return [curleft,curtop];
}
