//自定义myChildren函数
Element.prototype.myChildren=function(){
	var child=this.childNodes;
	var length=child.length;
	var mychild=[];
	for(var i=0;i<length;i++){
		if(child[i].nodeType==1){
			mychild.push(child[i]);
		}
	}
	return mychild;
}
var qiXi=function(){
    var confi = {
    	//音乐参数
        audio: {
	        enable: true, // 是否开启音乐
	        audio1:document.getElementById('audio1'), // 正常播放地址
	        audio2:document.getElementById('audio2') // 正常循环播放地址
        },
        //时间控制
        setTime: {
            walkToThird: 6000,
            walkToMiddle: 6500,
            walkToEnd: 6500,
            walkTobridge: 1200,
            bridgeWalk: 1800,
            walkToShop: 1500,
            walkOutShop: 1500,
            openDoorTime: 800,
            shutDoorTime: 500,
            waitRotate: 1800
        },
        //花瓣链接数组
        snowflakeURl: ["/img/type1/snow1.png", 
				       "/img/type1/snow2.png",
				       "/img/type1/snow3.png", 
				       "/img/type1/snow4.png", 
				       "/img/type1/snow5.png",
				       "/img/type1/snow6.png"]
    };
	var instanceX;
	var container=document.getElementById('content');
	var visualWidth=container.offsetWidth,
		visualHeight=container.offsetHeight;
	var getValue=function(className){
		var elem=document.getElementsByClassName(className)[0];
		return{
			height:elem.offsetHeight,
			top:elem.offsetTop
		};
	};
	//路的Y轴
	var pathY=function(){
		var data=getValue('a_background_middle');
		return data.top+data.height/2;
	}();
	//桥的Y轴
	var bridgeY=function(){
		var data=getValue('c_background_middle');
		return data.top;
	}();
    if (confi.audio.enable) {
        var audio1 = Html5Audio(confi.audio.audio1);
        audio1.end(function() {
            Html5Audio(confi.audio.audio2)
        })
    }
    var swipe = Swipe(container);
	function scrollTo(time,proportionX){
		var distX=visualWidth*proportionX;
		swipe.scrollTo(distX,time);
	}
	//小女孩
	var girl={
		elem:document.getElementsByClassName('girl')[0],
		getHeight: function() {
   			 return this.elem.offsetHeight;
		},
		rotate:function(){
			this.elem.classList.add('girl-rotate');
		},
		setOffset:function(){
			this.elem.style.cssText+="left:"+(visualWidth/2)+"px;top:"+(bridgeY-this.getHeight())+"px;"
		},
		getOffset:function(){
			return this.elem.getBoundingClientRect()
		},
		getWidth:function(){
			return this.elem.offsetWidth;
		}
	};
	//小鸟
	var bird={
		elemBird:document.getElementsByClassName('bird')[0],
		fly:function(){
			this.elemBird.classList.add('birdFly');
			this.elemBird.style.cssText+="transition: right 15000ms linear;";
			this.elemBird.style.right=visualWidth+"px";
		}
	};
	//logo动画
	var logo={
		elem:document.getElementsByClassName('logo')[0],
		run:function(){
			this.elem.classList.add('logolightSpeedIn')
		}
	}
    var boy = BoyWalk();
    boy.walkTo(confi.setTime.walkToThird, 0.6).then(function() {
        scrollTo(confi.setTime.walkToMiddle, 1);
        return boy.walkTo(confi.setTime.walkToMiddle, 0.5)
    }).then(function() {
        bird.fly()
    }).then(function() {
        boy.stopWalk();
        return BoyToShop(boy)
    }).then(function() {
        girl.setOffset();
        scrollTo(confi.setTime.walkToEnd, 2);
        return boy.walkTo(confi.setTime.walkToEnd, 0.15)
    }).then(function() {
        return boy.walkTo(confi.setTime.walkTobridge, 0.25, (bridgeY - girl.getHeight()) / visualHeight)
    }).then(function() {
        var proportionX = (girl.getOffset().left - boy.getWidth()+girl.getWidth()/2-instanceX) / visualWidth;
        return boy.walkTo(confi.setTime.bridgeWalk, proportionX)
    }).then(function() {
        boy.resetOriginal();
        setTimeout(function() {
            girl.rotate();
            boy.rotate(function() {
                logo.run();
                snowflake();
            })
        },
        confi.setTime.waitRotate)
    });
function BoyWalk(){
	var boy=document.getElementById('boy');
	var boyHeight=boy.offsetHeight;
	var boyWidth=boy.offsetWidth;
	//修正小男孩的位置
	boy.style.cssText+="top:"+(pathY-boyHeight+25)+"px";
	//暂停
    function pauseWalk() {
        boy.classList.add("pauseWalk");
    }
    //恢复
    function restoreWalk() {
        boy.classList.remove("pauseWalk");
    }	
	//用transition做动画
	function startRun(options,runTime){
		var p=new Promise(function(resolve,reject){ //解决了异步回调函数层层嵌套的问题
		boy.classList.remove('pauseWalk');
		var keys=""; //空字符串
		for(var key in options){
			keys+=key+",";
		}
		keys=keys.substring(0,keys.length-1); //去掉最后的分号
		boy.style.transitionProperty=keys;
		boy.style.transitionDuration=runTime+"ms";
		boy.style.transitionTimingFunction="linear";
		boy.style.left=options.left;
		boy.style.top=options.top;
		boy.style.opacity=options.opacity; 
		boy.style.transform=options.transform;
		setTimeout(function(){
			resolve();
		},runTime)
		});
		return p;
	}
	//开始走路
	function walkRun(time,distX,distY){
		time=time||3000;
		boy.classList.add('slowWalk');
		var d1=startRun({
			'left':distX+"px",
			'top':(distY?distY:undefined)+"px"
		},time);
		return d1;
	}
	//走进商店
	function walkToShop(runTime){
		var p1=new Promise(function(resolve,reject){
			var doorObj=document.getElementsByClassName('door')[0];
			var doorPos=doorObj.getBoundingClientRect(); //相对于窗口的位置
			var boyPos=boy.getBoundingClientRect();
			var doorLeft=doorPos.left;
			var doorWidth=doorPos.width;
			var boyLeft=boyPos.left;
			var boyWidth=boyPos.width;
			//当前需要移动的座标
            instanceX = (doorLeft+doorWidth/2) - (boyLeft + boyWidth/ 2);
            var walkPlay=startRun({
                transform: 'translateX(' + instanceX + 'px) scale(0.3,0.3)',
                opacity: 0.1
            }, runTime);
            walkPlay.then(function(){
            	boy.style.opacity=0;
            	resolve();
            })
		});
		return p1;
	}
	//走出商店
    function walkOutShop(runTime) {
        var p1=new Promise(function(resolve,reject){
        	boy.classList.remove("pauseWalk");
        	startRun({
 				transform: 'translateX(' + instanceX + 'px) scale(1,1)',
  				opacity: 1       		
        	},runTime);
			setTimeout(function(){
				resolve();
			},runTime);
        });
        return p1;
    }
	//计算移动距离
	function calculateDistance(direction,proportion){
		return(direction=="x"?visualWidth:visualHeight)*proportion;
	}
	return{
		//开始走路
		walkTo:function(time,proportionX,proportionY){
			var distX=calculateDistance('x',proportionX);
			var distY=calculateDistance('y',proportionY);	
			return walkRun(time,distX,distY);
		},
		toShop:function(runTime){
            return walkToShop(runTime);
		},
        outShop: function(runTime) {
            return walkOutShop(runTime);
        }, 
		stopWalk:function(){
			boy.classList.add('pauseWalk');
		},
        setColor:function(value){
            boy.style.cssText+="background-color: "+value+";";
        },
        getWidth: function() {
            return boyWidth;
        },
        getDistance: function() {
            return boy.getBoundingClientRect().left;
        },        
        resetOriginal:function(){
        	this.stopWalk();
        	boy.classList.remove('slowWalk');
        	boy.classList.remove('slowFlowerWalk');
        	boy.classList.add('boyOriginal');
        },
        takeFlower:function(){
        	boy.classList.add('slowFlowerWalk');
        },
        rotate:function(callback){
        	boy.classList.remove('pauseWalk');
        	boy.classList.add('boy-rotate');
        	if(callback){
        		callback();
        	}
        }
	}
}
var BoyToShop = function(boyObj) {
	var p2= new Promise(function(resolve,reject){
	var door=document.getElementsByClassName('door')[0],
		doorLeft=document.getElementsByClassName('door-left')[0],
		doorRight=document.getElementsByClassName('door-right')[0];
	function doorAction(left,right,time){
	var p= new Promise(function(resolve,reject){
		doorLeft.style.transitionProperty="left";
		doorLeft.style.transitionDuration=time+"ms";
		doorLeft.style.left=left;
		doorRight.style.transitionProperty="left";
		doorRight.style.transitionDuration=time+"ms";
		doorRight.style.left=right;
		setTimeout(function(){
			resolve();
		},time)
	});
	return p;
	}
	function openDoor(time){
		return doorAction("-50%","100%",time);
	}
	function shutDoor(time){
		return doorAction("0%","50%",time);
	}

    //取花
    function takeFlower(){
    	var p1=new Promise(function(resolve,reject){
    		setTimeout(function(){
    			var boy=document.getElementById('boy');
    			boy.classList.add('slowFlowerWalk');
    			resolve();
    		},1500)
    	});
    	return p1;
    }
    var lamp = {
        elem: document.getElementsByClassName('b_background')[0],
        bright: function() {
            this.elem.classList.add("lamp-bright")
        },
        dark: function() {
            this.elem.classList.remove("lamp-bright")
        }
    };
    var waitOpen = openDoor(confi.setTime.openDoorTime);
    waitOpen.then(function() {
        lamp.bright();
        return boyObj.toShop(confi.setTime.walkToShop)
    }).then(function() {
        return takeFlower()
    }).then(function() {
        return boyObj.outShop(confi.setTime.walkOutShop)
    }).then(function() {
        shutDoor(confi.setTime.shutDoorTime);
        lamp.dark();
        resolve();
    });
	});

    return p2;
};
//飘雪花
function snowflake(){
	//雪花容器
	var snowContainer=document.getElementById('snowflake');
	//随机六张图
	function getImagesName(){
        return confi.snowflakeURl[Math.floor(Math.random() * 6)]
	}
	//创建一个雪花元素
	function createSnowBox(){
		var url=getImagesName();
		var elem=document.createElement('div');
		elem.style.backgroundImage="url("+url+")";
		elem.classList.add('snowRoll');
		return elem;
	}
	//开始飘花
	setInterval(function(){
		//运动的轨迹
    	var startPositionLeft = Math.random() * visualWidth - 100,
    		startOpacity = 1,
    		endPositionTop = visualHeight - 40,
        	endPositionLeft = startPositionLeft - 100 + Math.random() * 500,
       		duration =  Math.random() * 5 + 5 + "s";
       	//随机透明度,不小于0.5
        var randomStart = Math.random();
        randomStart = randomStart < 0.5 ? startOpacity: randomStart;
       	//创建一个雪花
       	var newSnowItem=createSnowBox();
       	//设计起点位置
       	newSnowItem.style.left=startPositionLeft+"px";
       	newSnowItem.style.opacity=randomStart;
       	//加入到容器
       	snowContainer.appendChild(newSnowItem);
       	//开始执行动画
       	newSnowItem.style.transitionProperty="top,opacity";
       	newSnowItem.style.transitionDuration=duration;
       	setTimeout(function(){
           	newSnowItem.style.left = endPositionLeft +"px";
    		newSnowItem.style.top = endPositionTop +"px";
    		newSnowItem.style.opacity = 1;
       	},100)
        setTimeout(function(){
        	snowContainer.removeChild(newSnowItem);
        },10000)
       	},100);
	}
	function Html5Audio(audioobj) {
		var audio=audioobj;
		audio.play();
		return{
			end:function(callback){
				audio.addEventListener('ended',function(){
					callback();
				},false);
			}
		};
	}
}
window.onload=function(){
	qiXi()
};
//页面滑动
function Swipe(container,options){
	//获取第一个子节点
	var ele=document.getElementsByClassName('content_wrap')[0];
	//滑动对象
	var swipe={};
	//li页面数量
	var slides=ele.myChildren();
	//获取容器尺寸
	var width=container.offsetWidth,
		height=container.offsetHeight;
	//设置Li页面总宽度
	ele.style.cssText="height:"+height+"px;width:"+width*slides.length+"px;";
	//设置每一个页面li的宽度
	for(var i=0;i<slides.length;i++){
		slides[i].style.cssText+="width:"+width+"px;height:"+height+"px";
	}
	//监控完成与移动
	swipe.scrollTo=function(x,speed){
		ele.style.cssText+="transition-timing-function: linear;transition-duration:"+speed+"ms;transform: translate3d(-"+x+"px,0px,0px)";
		return this;
	}
	return swipe;
}
