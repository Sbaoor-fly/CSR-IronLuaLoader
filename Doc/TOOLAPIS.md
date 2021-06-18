#TOOLAPIS

```ReadText(string:filepath)```

 - 读取文件所有内容并返回

```ReadLines(string:filepath)```

 - 读取并返回文件的每一行
 - 返回值为table

```WriteText(string:filepath,string:texts)```

 - 覆盖式写入文件
 - 文件不存在时会自动创建

```AppendText(string:filepath,string:texts)```

 - 向文件中追加
 - 文件不存在时会自动创建

```IfFile(string:filepath)```

 - 返回一个bool值判断文件是否存在

```IfDir(string:dirpath)```

 - 返回一个bool值判断文件夹是否存在

```CreateDir(string:dirpath)```

 - 创建一个文件夹
 - 注意：不可创建多级文件夹

```HttpGet(string:url)```

 - 发起一个远程HttpGet请求
 - 注意：此方法非异步

```Schedule(function:func,int:delay,int:cycle)```

 - 设置一个定时器
 - delay：执行间隔，单位为毫秒
 - cycle：执行次数
 - 返回一个int类型的任务id，可用于中止任务

```Cancel(int:id)```

 - 用任务id中止一个任务
 - 返回一个bool为是否成功

```TaskRun(function:func)```

 - 异步执行一个函数

```TableToJson(table:tb)```

 - 用于转化table为json
 - 不能转换复杂table
 - 复杂table请用dkjson或者cjson库

```ToInt(string:number)```

 - 转化number为无符号整数

```NewGuid()```

 - 返回一个Guid