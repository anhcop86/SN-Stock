
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

    function CheckAllForNewsList(selfObj)
    {
        var objElements = document.getElementsByTagName('input');        
        var objElement;
        
        for(i=0; i<objElements.length; i++)
        {
            objElement = objElements[i];                        
            
            if(objElement.type == "checkbox" && ((objElement.id.substring(0,12)) == 'selectedItem'))            
            {                
                objElement.checked = selfObj.checked;
            }
        }
    }
    function CheckAllForHome(selfObj)
    {
        var objElements = document.getElementsByTagName('input');        
        var objElement;
        for(i=0; i<objElements.length; i++)
        {
            objElement = objElements[i];                        
            if(objElement.type == "checkbox" && objElement.id == 'selectedGroupNewsItem')
            {                
                if(selfObj.checked == true){
                    document.getElementById('selectedItem_' + objElement.value).checked = true;
                    document.getElementById('checkAll').checked = true;
                }
                objElement.checked = selfObj.checked;
            }
        }
    }
    function SingleCheckForNewsList(selfObj)
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
                if(objElement.type == "checkbox" && (objElement.id.substring(0,12) == 'selectedItem') && objElement.checked == false && objElement.id != 'checkAll'){//check
                    checkAll = false; 
                }
           }
        }
        document.getElementById('checkAll').checked = checkAll;       
    }
    function SingleCheckItemGroupNews(selfObj)
    {
        var objElements = document.getElementsByTagName('input');
        var objElement;
        var checkAll;
        checkAll = true;
        var checkedItem = document.getElementById('selectedItem_' + selfObj.value);        
        
        if(checkedItem.checked == false && selfObj.checked == true){
            document.getElementById('selectedItem_' + selfObj.value).checked = true;            
        }        
        if(selfObj.checked == false){
            document.getElementById('CheckAllHome').checked = false;
            return;
        }        
        if(selfObj.checked == true){
            for(i=0; i<objElements.length; i++){
                objElement = objElements[i];
                
                if(objElement.type == "checkbox" &&objElement.id == 'selectedGroupNewsItem' && objElement.checked == false && objElement.id != 'CheckAllHome'){                                        
                    checkAll = false;
                }
           }
        }
        document.getElementById('CheckAllHome').checked = checkAll;      
    }