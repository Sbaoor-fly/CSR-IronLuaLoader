using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronLuaLoader
{
    public class LUALOADER
    {
        /// <summary>
        /// 配置文件版本，请勿修改
        /// </summary>
        public int configure_version { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enabled { get; set; }
    }

    public class PLUGIN
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 插件唯一标识符
        /// </summary>
        public string guid { get; set; }
        /// <summary>
        /// 插件作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 插件版本
        /// </summary>
        public List<int> version { get; set; }
        /// <summary>
        /// 插件描述
        /// </summary>
        public List<string> describe { get; set; }
    }

    public class DEPENGING
    {
        /// <summary>
        /// 插件依赖库列表
        /// </summary>
        public List<string> Library { get; set; }
        /// <summary>
        /// 依赖的ILL插件列表，只接受对应标识符
        /// </summary>
        public List<string> Plugins { get; set; }
        /// <summary>
        /// ILL最低版本
        /// </summary>
        public int IronLuaLoader { get; set; }
    }

    public class PLINFO
    {
        /// <summary>
        /// 
        /// </summary>
        public LUALOADER LUALOADER { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PLUGIN PLUGIN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DEPENGING DEPENGING { get; set; }
    }

}
