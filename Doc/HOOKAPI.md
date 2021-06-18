# HOOKAPI

```teleport(string:uuid,int:x,int:y,int:z,int:did)```

 - Hook内容需要随版本更新
 - 传送到未加载的区块有概率摔死，BDS自身原因
 
 
 
# 自主适配方法
## 1.寻找相对内存地址

在导出的pdb信息中搜索如下语句

```?teleport@TeleportCommand@@SAXAEAVActor@@VVec3@@PEAV3@V?$AutomaticID@VDimension@@H@@VRelativeFloat@@4HAEBUActorUniqueID@@@Z```

会得到这样的信息

![NPP 图标](https://raw.githubusercontent.com/Sbaoor-fly/CSR-IronLuaLoader/master/Doc/pic/nppp-search.png)

## 2.适配

打开解决方案第236行，照着已有项填入新版本的版本号与内存地址

```cs
{BDS版本号, 内存地址 }
```
例：1.16.210.06的适配如下
```cs
{"1.16.210.06", 0x007B1D20 }
```

## 3.编译