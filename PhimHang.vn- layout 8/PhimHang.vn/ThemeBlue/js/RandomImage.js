window.onload = choosePic;
var myPix = new Array("/img/1.jpg", "/img/2.jpg", "/img/3.jpg", "/img/4.png", "/img/5.jpg", "/img/6.jpg", "/img/7.jpg", "/img/8.jpg", "/img/9.jpg", "/img/10.jpg");
function choosePic() {
    var randomNum = Math.floor(Math.random() * myPix.length);
    document.body.style.background = "url('" + myPix[randomNum] + "') fixed 50% / cover";
}