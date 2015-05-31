// JScript File
function confirmAction (event, message) {
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

