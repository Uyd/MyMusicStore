window.onload = function () {
    function $(id) {
        return document.getElementById(id);
    }

    var ul = $('ad_ul'),
        ol = $('ad_ol');

    var ollis = ol.children;

    var leader = 0;//偏移的结果
    var target = 0;//目标位置

    for (var i = 0; i < ollis.length; i++) {
        //每个li的索引号
        ollis[i].index = i;
        ollis[i].onmouseover = function () {
            for (var j = 0; j < ollis.length; j++) {
                ollis[j].className = ''; //去掉所有的current类名;
            }
            this.className = 'current';

            target = -this.index * 490; //目标位置=当前index乘以图片宽度
        }
    }
//动画 每20ms left的值如何变化
    setInterval(function () {
        leader = leader + (target - leader) / 10;
        ul.style.left = leader + 'px';
        console.log(ul.style.left);
    }, 20)
}