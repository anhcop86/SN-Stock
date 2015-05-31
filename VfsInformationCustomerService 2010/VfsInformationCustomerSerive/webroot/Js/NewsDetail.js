var xmlHttpNewsDetail;
var propertyID;
var browserType = "IE";

function showNewDetail(str)
{
    propertyID = str;
    
    xmlHttpNewsDetail = GetXmlHttpObjectDetail();
    if (xmlHttpNewsDetail == null) {
        alert ("Your browser does not support AJAX!");
        return;
    } 
    //alert(xmlHttpNewsDetail.onreadystatechange);
    var url = "AjaxGetNewDetails.aspx";
    url = url + "?newId=" + str;
    xmlHttpNewsDetail.onreadystatechange = DisplayNewDetail;
    xmlHttpNewsDetail.open("GET",url,true);
    xmlHttpNewsDetail.send(null);
} 

function DisplayNewDetail() 
{
    if (xmlHttpNewsDetail.readyState == 4 || xmlHttpNewsDetail.readyState == "complete") { 
        var newTitle = document.getElementById("newTitle");
        var popup = document.getElementById("newContent");
        pos = xmlHttpNewsDetail.responseText.indexOf('#');
        newTitle.innerHTML = xmlHttpNewsDetail.responseText.substring(0,pos);
        popup.innerHTML = xmlHttpNewsDetail.responseText.substr(pos + 1);
        
       //document.getElementById("txtHint").innerHTML=xmlHttp.responseText;
        //var textobj = document.getElementById("P" + propertyID);
        /*var xmlObj= xmlHttpNewsDetail.responseXML.documentElement;
        var districtNode = xmlHttpNewsDetail.responseXML.documentElement.childNodes;
        
        var titleNode = districtNode[0].childNodes[0]; 
        var contentNode = districtNode[1].childNodes[0];
                        
        var newTitle = document.getElementById("newTitle");
        var popup = document.getElementById("newContent");
        
        if(browserType == "Others")
        {    
            newTitle.innerHTML = titleNode.textContent;
            popup.innerHTML = contentNode.textContent;       
        }
        else
        {
            newTitle.innerHTML = titleNode.xml;
            popup.innerHTML = contentNode.xml; 
        }*/
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
