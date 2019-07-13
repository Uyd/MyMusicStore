window.onload = function () {
    //为页面家载键盘事件
    document.body.onkeydown = function (evt) {

        // 定义小球的步长
        var step = 10;
        //获取球台和小球的元素
        var ball = document.getElementById('ball');
        var box = document.getElementById('box');
        //转换数值类型
        var left = ball.offsetLeft;
        var top = ball.offsetTop;
        console.log("球的位置" + left + ',' + top);

        //画布大小
        var box_width = box.offsetWidth;
        var box_height = box.offsetHeight;
        console.log("画布大小" + box_height + ',' + box_width);

        //小球的高和宽
        var ball_width = 30;
        var ball_height = 30;
        console.log("球大小" + ball_height + ',' + ball_width);

        //防止小球超出画布
        var x_max = box_width - ball_width - step;
        var y_max = box_height - ball_height - step;
        console.log("最大距离" + x_max + ',' + y_max);

        //创建键盘事件的对象
        var e = evt ? evt : window.event;
        //左37
        if (e.keyCode == 37 && (left - step)>=0) {
            ball.style.left = left - step + 'px';
            console.log("当前位置左"+"left");
        }
        //上38
        if (e.keyCode == 38 && (top - step)>=0) {
            ball.style.top = top - step + 'px';
            console.log("当前位置上" + top);
        }
        //右
        if (e.keyCode == 39 && left <=x_max) {
            ball.style.left = left + step + 'px';
            console.log("当前位置右" + left);
        }
        //下
        if (e.keyCode == 40 && top <=y_max) {
            ball.style.top = top + step + 'px';
            console.log("当前位置下" + top);
        }


    }
}