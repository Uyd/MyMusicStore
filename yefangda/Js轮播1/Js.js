// var up=document.getElementById('picUp');
// console.log(up);
// up.onmouseover=function() {
//     console.log(up);
// }
window.onload = function () {
    function $(id) {
        return document.getElementById(id);
    }

    var pic_top = 0; //控制图片的top
    var timer = null; //定时器

    $('picUp').onmousemove = function () {
        clearInterval(timer);
        timer = setInterval(function () {
            pic_top--;
            pic_top >= -100 ? $('pic').style.top = pic_top : clearInterval(timer);
        }, 100)
    }
    $('picDown').onmouseover = function () {
        clearInterval(timer);
        timer = setInterval(function () {
            pic_top++;
            pic_top < 0 ? $('pic').style.top = pic_top + "px" : clearInterval(timer);
        }, 100)
    }

    $("box").onmouseout = function () {
        clearInterval(timer);
    }
}