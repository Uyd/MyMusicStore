window.onload = function () {
    //文章上交的时间
    var ArticleTime = document.querySelector(".top>span.time>span");
    var date = new Date();
    ArticleTime.innerText = date.toLocaleString();
    //提交按钮
    var sm = document.querySelector("input:nth-child(2)");
    //文本框
    var txt = document.querySelector(".textarea");
    sm.onclick = function () {
        // alert("是否提交？");
        //是否为空
        if (txt.value.length == 0)
            return false;
        if (window.confirm("是否提交？")) {
            return true;
        } else {
            return false;
        }
    }
    //清除内容
    var rs = document.querySelector("input:nth-child(1)");
    var ta = document.querySelector(".textarea");
    rs.onclick = function () {
        ta.value = "";
    }

    //小图标点评
    //评论盒子
    var rc = document.querySelector(".ri_comment");
    var mc = document.querySelector(".M_comment_icon");
    //加内边距显示下面的内容
    var dpBox = document.querySelector(".dp_Content");

    // 768以下移动设备
    var Width = window.screen.width;
    var height = window.screen.height;

    mc.style.bottom = height / 2 - 20 + "px";

    console.log(mc.style.top);
    if (Width < 768) {
        mc.style.display = "block";
        rc.style.display = "none";
    } else {
        rc.style.display = "block";
        mc.style.display = "none";
    }
    //点击事件
    var bool = false; //单次点击
    mc.onclick = function () {
        if (!bool) {
            rc.style.display = "block";
            rc.style.position = "fixed";
            rc.style.left = "0";
            rc.style.bottom = "0";

            mc.innerText = "X";

            rch = rc.offsetHeight;
            // dpBox.style.paddingBottom = rch + "px";

            bool = true;
        } else {
            rc.style.display = "none";
            mc.innerText = "点评";

            // dpBox.style.paddingBottom = 0 + "px";

            bool = false;
        }
    }
};