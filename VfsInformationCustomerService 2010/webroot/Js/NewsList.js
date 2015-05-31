
    function CheckAll(selfObj)
    {
        var objElements = document.getElementsByTagName('input');
        var objElement;
        for(i=0; i<objElements.length; i++){
            objElement = objElements[i];
            if(objElement.type == "checkbox"){
                objElement.checked = selfObj.checked;
            }
       }        
    }
    
    function SingleCheck(selfObj)
    {
        var objElements = document.getElementsByTagName('input');
        var objElement;
        var checkAll;
        checkAll = true;
        if(selfObj.checked == false){        
            document.getElementById('checkAll').checked = false;
            return;            
        }
        
        if(selfObj.checked == true){        
            for(i=0; i<objElements.length; i++){
                objElement = objElements[i];
                if(objElement.type == "checkbox" && objElement.checked == false && objElement.id != 'checkAll'){
                    checkAll = false; 
                }
           }
        }
        document.getElementById('checkAll').checked = checkAll;       
    }    


