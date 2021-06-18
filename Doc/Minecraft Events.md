# Listeners
## 监听器列表

事件 | 描述 | 是否可拦截
-|-|-
onServerCmd | 后台指令监听 | 是
onServerCmdOutput | 后台指令输出信息监听 | 是
onFormSelect | 玩家选择GUI表单监听 | 是
onUseItem | 使用物品监听 | 是
onPlacedBlock | 放置方块监听 | 是
onDestroyBlock | 破坏方块监听 | 是
onStartOpenChest | 开箱监听 | 是
onStartOpenBarrel | 开桶监听 | 否
onStopOpenChest | 关箱监听 | 否
onStopOpenBarrel | 关桶监听 | 否
onSetSlot | 放入取出物品监听 | 否
onChangeDimension | 切换维度监听 | 否
onMobDie | 生物死亡监听 | 否
onMobHurt | 生物受伤监听 | 是
onRespawn | 玩家重生监听 | 否
onChat | 聊天监听 | 否
onInputText | 玩家输入文本监听 | 是
onCommandBlockUpdate | 玩家更新命令方块监听 | 是
onInputCommand | 玩家输入指令监听 | 是
onBlockCmd | 命令方块(矿车)执行指令监听 | 是
onNpcCmd | NPC执行指令监听 | 是
onLoadName |  加载名字监听 | 否
onPlayerLeft | 离开游戏监听 | 否
onMove | 移动监听 | 否
onAttack | 攻击监听 | 是
onLevelExplode | 爆炸监听 | 是
onEquippedArmor | 玩家切换护甲监听 | 否
onLevelUp | 玩家升级 | 是
onPistonPush | 活塞推方块事件 | 是
onChestPair | 箱子合并事件 | 是
onMobSpawnCheck | 生物生成检查事件 | 是