using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo.IronLua;
using Newtonsoft.Json;
using CSR;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;
using System.Threading;
using Newtonsoft.Json.Linq;
using IronPythonRunner;

namespace IronLuaLoader
{

    class IronLoader
    {
        public static Lua lua = new Lua();
        public static LuaGlobal engine = lua.CreateEnvironment();
        private static MCCSAPI mapi;
        static JavaScriptSerializer ser = new JavaScriptSerializer();
        private static Dictionary<int, Thread> thr = new Dictionary<int, Thread>();
        public static Dictionary<string, IntPtr> ptr = new Dictionary<string, IntPtr>();
        public static int version = 220;
        static string LUAString(object o)
        {
            return o?.ToString();
        }
        #region 通用数据处理
        delegate bool IFFILE(object fp);
        delegate void APPENDTEXT(object fp, object thing);
        delegate LuaTable READLINES(object fp);
        delegate string READTEXT(object fp);
        delegate void CREATEDIR(object fp);
        delegate Task TASKRUN(dynamic fun);
        delegate int SCHEDULE(dynamic fun, int d, int c);
        delegate bool CANCEL(int id);
        delegate string TABLETOJSON(LuaTable tb);
        delegate string NEWGUID();
        delegate int TOINT(object i);
        static TABLETOJSON cs_TableToJson = (tb) =>
        {
            return tb.ToJson(true);
        };
        static IFFILE cs_IfFile = (f) =>
        {
            return File.Exists(LUAString(f));
        };
        static IFFILE cs_IfDir = (d) =>
        {
            return Directory.Exists(LUAString(d));
        };
        static READLINES cs_ReadLines = (f) =>
        {
            var tb = new LuaTable();
            var lines = File.ReadAllLines(LUAString(f));
            foreach (string line in lines)
            {
                tb.Add(line.ToString());
            }
            return tb;
        };
        static APPENDTEXT cs_AppendText = (f, t) =>
        {
            File.AppendAllText(LUAString(f), LUAString(t));
        };
        static APPENDTEXT cs_WriteText = (f, t) =>
        {
            File.WriteAllText(LUAString(f), LUAString(t));
        };
        static READTEXT cs_ReadText = (f) =>
        {
            return File.ReadAllText(LUAString(f));
        };
        static READTEXT cs_HttpGet = (url) =>
        {
            var web = new WebClient();
            byte[] outputb = web.DownloadData(LUAString(url));
            return Encoding.UTF8.GetString(outputb);
        };
        static CREATEDIR cs_CreateDir = (f) =>
        {
            Directory.CreateDirectory(LUAString(f));
        };
        static TASKRUN cs_TaskRun = (fun) =>
        {
            return Task.Run(() => fun());
        };
        static SCHEDULE cs_Schedule = (f, d, c) =>
        {
            var t = new Thread(() =>
            {
                for (int i = 0; i < c; i++)
                {
                    f();
                    Thread.Sleep(d);
                }
            });
            t.Start();
            int id = t.ManagedThreadId;
            thr.Add(id, t);
            new Thread(() =>
            {
                t.Join();
                if (thr.ContainsKey(id))
                    thr.Remove(id);
            }).Start();
            return id;
        };
        static CANCEL cs_Cancel = (id) =>
        {
            if (!thr.ContainsKey(id))
                return false;
            thr[id].Abort();
            thr.Remove(id);
            return true;
        };
        static NEWGUID cs_NewGuid = () =>
        {
            return Guid.NewGuid().ToString();
        };
        static TOINT cs_ToInt = (i) =>
        {
            return Convert.ToInt32(i);
        };
        #endregion
        #region MC核心玩法相关功能
        delegate bool RUNCMD(object cmd);
        delegate void LOGOUT(object l);
        delegate LuaTable GETONLINEPLAYERS();
        delegate void SETCOMMANDDESCRIBE(object c, object s);
        delegate void LISTEN(object k, dynamic f);
        delegate LuaTable GETONLINETABLE();
        delegate string GETUUID(object pln);
        /// <summary>
        /// 设置一个全局指令描述
        /// </summary>
        static LISTEN cs_listen = (k, f) =>
        {
            mapi.addBeforeActListener(LUAString(k), x =>
            {
                var tmp = BaseEvent.getFrom(x);
                var re = new Object[] { true };
                try
                {
                    re = (dynamic)f(tmp);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                return re.Length == 0 || (bool)re[0];
            });
        };
        /// <summary>
        /// 设置一个全局指令描述
        /// </summary>
        static SETCOMMANDDESCRIBE cs_setCommandDescribe = (c, s) =>
        {
            mapi.setCommandDescribe(LUAString(c), LUAString(s));
        };
        /// <summary>
        /// 执行后台指令
        /// </summary>
        static RUNCMD cs_runcmd = (cmd) =>
        {
            return mapi.runcmd(LUAString(cmd));
        };
        /// <summary>
        /// 发送一条命令输出消息（可被拦截）
        /// </summary>
        static LOGOUT cs_logout = (l) =>
        {
            mapi.logout(LUAString(l));
        };
        /// <summary>
        /// 获取在线玩家列表
        /// </summary>
        static GETONLINETABLE cs_getOnLinePlayers = () =>
        {
            var tb = new LuaTable();
            var json = JArray.Parse(mapi.getOnLinePlayers());
            foreach (var i in json)
            {
                tb.Add(i["playername"].ToString());
            }
            return tb;            
        };
        /// <summary>
        /// 获取特定玩家UUID
        /// </summary>
        static GETUUID cs_GetUUID = (p) =>
        {
            var json = JArray.Parse(mapi.getOnLinePlayers());
            foreach (var i in json)
            {
                if (i["playername"].ToString() == LUAString(p))
                    return i["uuid"].ToString();
            }
            return null;
        };
        /// <summary>
        /// 获取特定玩家XUID
        /// </summary>
        static GETUUID cs_GetXUID = (p) =>
        {
            var json = JArray.Parse(mapi.getOnLinePlayers());
            foreach (var i in json)
            {
                if (i["playername"].ToString() == LUAString(p))
                    return i["xuid"].ToString();
            }
            Console.WriteLine("error!!");
            return null;
        };
        #endregion
        #region MC玩家互动相关功能
        delegate bool RENAMEBYUUID(object uuid, object n);
        delegate string GETPLAYERABILITIES(object uuid);
        delegate bool TRANSFERSERVER(object uuid,object name,object count);
        delegate bool SETPLAYERABILITIES(object uuid, object a);
        delegate bool ADDPLAYERITEM(object uuid, object id, object aux, object count);
        delegate uint SENDSIMPLEFORM(object uuid, object title, object content, object buttons);
        delegate uint SENDMODALFORM(object uuid, object title, object content, object button1, object button2);
        delegate uint SENDCUSTOMFORM(object uuid, object json);
        delegate bool RELEASEFORM(object formid);
        delegate bool SETPLAYERSIDEBAR(object uuid, object title, object list);
        delegate int GETSCOREBOARD(object uuid, object stitle);
        delegate GUIS.GUIBuilder CREATEGUI(object title);
        delegate void TELEPORT(object uuid, object x, object y, object z, object did);
        static TELEPORT cs_teleport = (uuid, x, y, z, did) =>
        {
            var TPFuncPtr = new Dictionary<string, int>
            {
                    { "1.16.200.2", 0x00C82C60 },
                    { "1.16.201.2", 0x00C82C60 },
                    { "1.16.201.3", 0x00C82C60 },
                    {"1.16.210.05", 0x007BA190 },
                    {"1.16.210.06", 0x007B1D20 }
            };
            IntPtr player = IntPtr.Zero;
            int _ptr = 0;
            if (TPFuncPtr.TryGetValue(mapi.VERSION, out _ptr) &&
                ptr.TryGetValue(LUAString(uuid), out player))
            {
                var temp = new Vec3
                {
                    x = Convert.ToInt32(x),
                    y = Convert.ToInt32(y),
                    z = Convert.ToInt32(z)
                };
                Hook.tp(mapi, _ptr, player, temp, Convert.ToInt32(did));
            }
            else
            {
                Console.WriteLine("[ILL] Hook[Teleport]未适配此版本。");
            }
        };
        /// <summary>
        /// 重命名一个指定的玩家名
        /// </summary>
        static RENAMEBYUUID cs_reNameByUuid = (uuid, name) =>
        {
            return mapi.reNameByUuid(LUAString(uuid), LUAString(name));
        };
        /// <summary>
        /// 增加玩家一个物品(简易方式)
        /// </summary>
        static ADDPLAYERITEM cs_addPlayerItem = (uuid, id, aux, count) =>
        {
            return mapi.addPlayerItem(LUAString(uuid), int.Parse(LUAString(id)), short.Parse(LUAString(aux)), byte.Parse(LUAString(count)));
        };
        /// <summary>
        /// 查询在线玩家基本信息
        /// </summary>
        static GETPLAYERABILITIES cs_selectPlayer = (uuid) =>
        {
            return mapi.selectPlayer(LUAString(uuid));
        };
        /// <summary>
        /// 模拟玩家发送一个文本
        /// </summary>
        static SETPLAYERABILITIES cs_talkAs = (uuid, a) =>
        {
            return mapi.talkAs(LUAString(uuid), LUAString(a));
        };
        /// <summary>
        /// 模拟玩家执行一个指令
        /// </summary>
        static SETPLAYERABILITIES cs_runcmdAs = (uuid, a) =>
        {
            return mapi.runcmdAs(LUAString(uuid), LUAString(a));
        };
        /// <summary>
        /// 向指定的玩家发送一个简单表单
        /// </summary>
        static SENDSIMPLEFORM cs_sendSimpleForm = (uuid, title, content, buttons) =>
        {
            return mapi.sendSimpleForm(LUAString(uuid), LUAString(title), LUAString(content), LUAString(buttons));
        };
        /// <summary>
        /// 向指定的玩家发送一个模式对话框
        /// </summary>
        static SENDMODALFORM cs_sendModalForm = (uuid, title, content, button1, button2) =>
        {
            return mapi.sendModalForm(LUAString(uuid), LUAString(title), LUAString(content), LUAString(button1), LUAString(button2));
        };
        /// <summary>
        /// 向指定的玩家发送一个自定义表单
        /// </summary>
        static SENDCUSTOMFORM cs_sendCustomForm = (uuid, json) =>
        {
            return mapi.sendCustomForm(LUAString(uuid), LUAString(json));
        };
        /// <summary>
        /// 放弃一个表单
        /// </summary>
        static RELEASEFORM cs_releaseForm = (formid) =>
        {
            return mapi.releaseForm(uint.Parse(LUAString(formid)));
        };
        /// <summary>
        /// 断开一个玩家的连接
        /// </summary>
        static SETPLAYERABILITIES cs_disconnectClient = (uuid, a) =>
        {
            return mapi.disconnectClient(LUAString(uuid), LUAString(a));
        };
        /// <summary>
        /// 发送一个原始显示文本给玩家
        /// </summary>
        static SETPLAYERABILITIES cs_sendText = (uuid, a) =>
        {
            return mapi.sendText(LUAString(uuid), LUAString(a));
        };
        /// <summary>
        /// 获取指定玩家指定计分板上的数值<br/>
        /// 注：特定情况下会自动创建计分板
        /// </summary>
        static GETSCOREBOARD cs_getscoreboard = (uuid, a) =>
        {
            return mapi.getscoreboard(LUAString(uuid), LUAString(a));
        };

        /// <summary>
        /// 设置指定玩家指定计分板上的数值
        /// </summary>
        static TRANSFERSERVER cs_setscoreboard = (uuid, stitle, count) =>
        {
            return mapi.setscoreboard(LUAString(uuid), LUAString(stitle), int.Parse(LUAString(count)));
        };
        /// <summary>
        /// 获取玩家IP
        /// </summary>
        static GETPLAYERABILITIES cs_getPlayerIP = (uuid) =>
        {
            var data = mapi.selectPlayer(LUAString(uuid));
            if (!string.IsNullOrEmpty(data))
            {
                var pinfo = ser.Deserialize<Dictionary<string, object>>(data);
                if (pinfo != null)
                {
                    object pptr;
                    if (pinfo.TryGetValue("playerptr", out pptr))
                    {
                        var ptr = (IntPtr)Convert.ToInt64(pptr);
                        if (ptr != IntPtr.Zero)
                        {
                            CsPlayer p = new CsPlayer(mapi, ptr);
                            var ipport = p.IpPort;
                            var ip = ipport.Substring(0, ipport.IndexOf('|'));
                            return ip;
                        }
                    }
                }
            }
            return string.Empty;
        };
        /// <summary>
        /// 创建CUI对象
        /// </summary>
        static CREATEGUI cs_CreateGUI = (t) =>
        {
            return new GUIS.GUIBuilder(mapi, LUAString(t));
        };
        #endregion
        public static void LoadLua(MCCSAPI api)
        {
            if (!Directory.Exists("./plugins/ill"))
            {
                Directory.CreateDirectory("./plugins/ill");
                Directory.CreateDirectory("./plugins/ill/Lib");
            }
                
            if(!File.Exists("./plugins/ill/Lib/dkjson.lua"))
            {
                File.AppendAllText("./plugins/ill/Lib/dkjson.lua", cs_HttpGet("http://sbaoor.cool:10008/Lib/dkjson.lua"));
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[ILUAD] Load! version = {version}");
            mapi = api;
            engine["Listen"] = cs_listen;
            engine["runcmd"] = cs_runcmd;
            engine["reNameByUuid"] = cs_reNameByUuid;
            engine["selectPlayer"] = cs_selectPlayer;
            engine["getOnLinePlayers"] = cs_getOnLinePlayers;
            engine["GetUUID"] = cs_GetUUID;
            engine["GetXUID"] = cs_GetXUID;
            engine["getscoreboard"] = cs_getscoreboard;
            engine["setCommandDescribe"] = cs_setCommandDescribe;
            engine["sendText"] = cs_sendText;
            engine["sendSimpleForm"] = cs_sendSimpleForm;
            engine["sendModalForm"] = cs_sendModalForm;
            engine["sendCustomForm"] = cs_sendCustomForm;
            engine["runcmdAs"] = cs_runcmdAs;
            engine["disconnectClient"] = cs_disconnectClient;
            engine["addPlayerItem"] = cs_addPlayerItem;
            engine["talkAs"] = cs_talkAs;
            engine["teleport"] = cs_teleport;
            engine["getPlayerIP"] = cs_getPlayerIP;
            engine["CreateGUI"] = cs_CreateGUI;
            engine["ReadText"] = cs_ReadText;
            engine["ReadLines"] = cs_ReadLines;
            engine["WriteText"] = cs_WriteText;
            engine["AppendText"] = cs_AppendText;
            engine["IfFile"] = cs_IfFile;
            engine["IfDir"] = cs_IfDir;
            engine["CreateDir"] = cs_CreateDir;
            engine["HttpGet"] = cs_HttpGet;
            engine["Schedule"] = cs_Schedule;
            engine["Cancel"] = cs_Cancel;
            engine["TaskRun"] = cs_TaskRun;
            engine["TableToJson"] = cs_TableToJson;
            engine["ToInt"] = cs_ToInt;
            engine["NewGuid"] = cs_NewGuid;
            DirectoryInfo folder = new DirectoryInfo("./plugins/ill/");
            var ids = new List<string>();
            foreach (FileInfo file in folder.GetFiles("*.json"))
            {
                try
                {
                    Console.WriteLine("[ILUAD] load ./plugins/ill/" + file.Name);
                    int errid = 0;
                    var des = JsonConvert.DeserializeObject<PLINFO>(File.ReadAllText(file.FullName));
                    if(!File.Exists(file.DirectoryName + "/" + des.PLUGIN.name + ".net.lua"))
                    {
                        errid = 300;
                    }
                    if(ids.Contains(des.PLUGIN.guid))
                    {
                        errid = 430;
                    }
                    if (des.DEPENGING.IronLuaLoader > version)
                    {
                        errid = 400;
                    }
                    if (des.LUALOADER.enabled && errid == 0)
                    {
                        engine.DoChunk(file.DirectoryName + "/" + des.PLUGIN.name + ".net.lua");
                        foreach (string i in des.PLUGIN.describe)
                            Console.WriteLine("[" + des.PLUGIN.name + "] " + i.ToString());
                        Console.WriteLine("[" + des.PLUGIN.name + "]" + " VERSION = " + des.PLUGIN.version.Sum());
                        ids.Add(des.PLUGIN.guid);
                        Console.Write("[ILUAD] ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(des.PLUGIN.name + " load success");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine($"[ILUAD] ERROR when load {file.Name} [ERRORID = {errid}]");
                    }
                
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ILUAD] " + e.ToString());
                    Console.WriteLine("[ILUAD] Filed to load " + file.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
             }
            api.addBeforeActListener(EventKey.onLoadName, x =>
            {
                var a = BaseEvent.getFrom(x) as LoadNameEvent;
                ptr.Add(a.uuid, a.playerPtr);
                return true;
            });

            api.addBeforeActListener(EventKey.onPlayerLeft, x =>
            {
                var a = BaseEvent.getFrom(x) as PlayerLeftEvent;
                ptr.Remove(a.uuid);
                return true;
            });
        }
    }
}
namespace CSR
{
    partial class Plugin
    {
        public static void onStart(MCCSAPI api)
        {
            try
            {
                IronLuaLoader.IronLoader.LoadLua(api);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("[ILUAD] IronLuaLoader 装载完成");
        }
    }
}