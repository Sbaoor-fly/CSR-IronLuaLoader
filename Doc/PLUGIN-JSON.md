```json
// 这里是IronLuaLoader的插件配置文件，支持斜杠“//”注释。
// 请在此处配置插件信息，请将此文件与插件放在一起加载
{
    "LUALOADER": {
        "configure_version": 100,   //配置文件版本，请勿修改
        "enabled": true             //是否开启此插件
    },
    "PLUGIN":{
        "name": "TEST",  //插件名称
        "guid": "368590ed-e3bf-4ae4-9f35-eb196333632c", //插件唯一标识符
        "author": "Sbaoor",                           //插件作者
	"describe":["ILD示例插件已加载"],             //插件描述
        "version": [2,0,0]                          //插件版本
    },
    "DEPENGING": {
        "Library": [ //此处为依赖的Lua库列表
            "dkjson"
        ],
        "Plugins": [], //此处填写依赖的ILL插件列表，只接受对应标识符 //xuiddb.lua
        "IronLuaLoader": 200 //ILR最低版本
    }
}
```
