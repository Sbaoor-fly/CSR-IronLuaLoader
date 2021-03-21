# GUIBuilder

GUIBuilder是一个可以让您快速创建自定义表单的API

通过短暂的学习我相信您可以轻易上手

```CreateGUI(string:title)```
 
 - 初始化一个表单对象
 
注意：下列API均需要一个已初始化的表单对象来执行

```AddLabel(string:msg)```

 - 为表单添加一个描述文本
 
```AddInput(string:msg1,string:msg2)```

 - msg1：输入框描述
 
 - msg2：水印文本

```AddSlider(string:msg,int:index,int:max)```

 - 为表单添加一个游标滑块
 
 - index：滑块初始位置
 
 - max：最大格数
 
```AddToggle(string:msg)```

 - 为表单添加一个开关
 
 - msg：描述文本
 
```AddStepSlider(string:msg,int:index,string:Array)```

 - 为表单添加一个矩阵滑块
 
 - mag：描述文本
 
 - index：初始选项
 
 - Array：表单数组，格式为["第一个选项","第二个选项","第三个选项"]
 
 ```AddDropdown(string:msg,int:index,string:Array)```
 
  - 为表单添加一个下拉框
  
  - msg：描述文本
  
  - index：初始选项
  
  - Array：同上
  
 ```SendToPlayer(string:uuid)```
 
 - 将表单发送给UUID对应玩家
 
 - 返回表单ID