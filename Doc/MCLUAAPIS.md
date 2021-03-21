#MCUAAPI

```listen(string:key,function:fun)```

 - 注册一个监听器
 - 如果key不正确则会注册失败
 
```runcmd(string:cmd)```

 - 执行后台命令

```reNameByUuid(string:uuid,string:name)```

 - 重命名UUID对应玩家

```selectPlayer(string:uuid)```

 - 查询UUID对应玩家基本信息

```getOnLinePlayers()```

 - 获取在线玩家列表
 - 返回值为一个table
 - 可以用```TableToJson(table)```函数转为string:Array

```GetUUID(string:playername)```

 - 获取玩家对应UUID
 - 查询失败时返回Null

```GetXUID(string:playername)```

 - 获取玩家对应XUID
 - 查询失败时返回Null

```getscoreboard(string:uuid,string:objname)```

 - 获取uuid对应玩家特定计分板上的数值
 - 若计分板不存在则会自动创建

```setCommandDescribe(string:key,string:describe)```

 - 设置一个全局指令描述

```sendText(string:uuid,string:msg)```
 
 - 向UUID对应玩家发送文本信息

```sendSimpleForm(string:uuid,string:title,string:context,string:Array)```

 - 向UUID对应玩家发送一个普通表单
 - Array格式: ["我是第一行","我是第二行,"我是第三行"]

```sendModalForm(string:uuid,string:title,string:context,string:botton1,string:botton2)```

 - 向UUID对应玩家发送一个模式对话框
 - botton1和botton2为按钮所显示的文本

```sendCustomForm(string:uuid,string:json)```

 - 向UUID对应玩家发送一个自定义表单
 - json格式如下
``` json
{"content":[{"type":"label","text":"这是一个文本标签"},{"placeholder":"水印文本","default":"","type":"input","text":""},{"default":true,"type":"toggle","text":"开关~或许是吧"},{"min":0.0,"max":10.0,"step":2.0,"default":3.0,"type":"slider","text":"游标滑块！？"},{"default":1,"steps":["Step 1","Step 2","Step 3"],"type":"step_slider","text":"矩阵滑块？!"},{"default":1,"options":["Option 1","Option 2","Option 3"],"type":"dropdown","text":"如你所见，下拉框"}], "type":"custom_form","title":"这是一个自定义窗体"}
```

```runcmdAs(string:uuid,string:cmd)```

 - 模拟UUID对应玩家执行一个指令
 - 原版指令可以不用加/,插件指令需要加

```disconnectClient(string:uuid,string:tips)```

 - 断开UUID对应玩家的连接
 - tips为断开连接时的提示
 - 如果在LoadName事件断开，可能不会有自定义提示

```addPlayerItem(string:uuid,int:itemid,int:itemaux,int:count)```

 - 为UUID对应玩家增加一个物品
 - 在玩家背包已满的状态下可能不会生效


```talkAs(string:uuid,string:msg)```

 - 模拟UUID对应玩家说一句话

```getPlayerIP(string:uuid)```

 - 获取玩家所属的IP地址
