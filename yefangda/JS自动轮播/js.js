window.onload = function () {
    function $(id) {
        return document.getElementById(id);
    }
    var ul = document.getElementById("ul_img");
    var ulList = $("ul_img").children;
    var ollis = $('ad_ol').children;
    var timer = null; //
    var num = 500;
    var p = 0; //页数

    //获得当前li
    for (var i = 0; i < ollis.length; i++) {
        //每个li的索引号
        ollis[i].index = i;
        //如果鼠标经过，去掉所有样式
        ollis[i].onmouseover = function () {
            for (var j = 0; j < ollis.length; j++) {
                ollis[j].className = '';
            }
            //当前的设置特定样式
            this.className = 'current';

            animate(ul, -this.index*num);
            // 去掉定时器
            // clearInterval(timer);

            // timer = setInterval(page, 20);
            p = this.index;
            // console.log(p);
            // clearInterval(test);
        }
        ollis[i].onmouseout = function () {
            for (var j = 0; j < ollis.length; j++) {
                ollis[j].className = '';
            }
            //当前的设置特定样式
            this.className = 'current';
        }

        // test = setInterval(autoplay, 1000);
    }

    // timer = setInterval(page, 20);
    timer = setInterval(autoplay, 1000);
    var key=0;
    //动画
    function autoplay() {
        key++;
        p++;
        p < 4 ? p : p = 0;
        // $('ul_img').style.left = 0;
        if (key > ulList.length - 1) // 后判断
        {
            ul.style.left = 0; // 迅速调回
            key = 1; // 因为第6张就是第一张  第6张播放 下次播放第2张
        }
        animate($('ul_img'), -key * num)

        for (var j = 0; j < ollis.length; j++) {
            ollis[j].className = '';
        }
        //方块当前的设置特定样式
        ollis[p].className = 'current';
    }
    $('box').onmouseover = function () {
        clearInterval(timer);
    }
    $('box').onmouseout = function () {
        timer = setInterval(autoplay, 1000);
    }
}
//公共动画
function animate(obj, target) {
    clearInterval(obj.timer); // 先清除定时器
    var speed = obj.offsetLeft < target ? 15 : -15; // 用来判断 应该 +  还是 -
    obj.timer = setInterval(function () {
        var result = target - obj.offsetLeft; // 因为他们的差值不会超过15
        obj.style.left = obj.offsetLeft + speed + "px";
        console.log(obj.style.left);
        if (Math.abs(result) <= 15) // 如果差值不小于 15 说明到位置了
        {
            clearInterval(obj.timer);
            obj.style.left = target + "px"; // 有5像素差距   我们直接跳转目标位置
        }
    }, 10)
}
