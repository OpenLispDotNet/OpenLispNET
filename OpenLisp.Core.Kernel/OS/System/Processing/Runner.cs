﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Kernel.OS.System.Processing
{
    public class ExecutableRunner
    {
        public void Run(Executable executable, List<string> args)
        {
            try
            {
                LuaFile.VirtualFiles = new Dictionary<string, byte[]>();

                // create Lua VM instance
                var Lua = LuaAPI.NewState();

                // load base libraries
                Lua.L_OpenLibs();

                // args
                Lua.NewTable();
                Lua.PushValue(-1);
                Lua.SetGlobal("arg");

                Lua.PushString("main.lua");
                Lua.RawSetI(-2, 0);

                for (int i = 0; i < args.Count; i++)
                {
                    Lua.PushString(args[i]);
                    Lua.RawSetI(-2, i + 1);
                }

                foreach (var source in executable.LuaSources.Keys)
                {
                    if (source == "main.lua")
                    {
                        continue;
                    }

                    LoadLuaFile(source);
                }

                LoadLuaFile("main.lua");

                void LoadLuaFile(string fileName)
                {
                    var status = Lua.L_LoadBytes(executable.LuaSources[fileName], fileName);

                    // capture errors
                    if (status != ThreadStatus.LUA_OK)
                    {
                        throw new Exception(Lua.ToString(-1));
                    }
                }

                var status = Lua.PCall(0, LuaDef.LUA_MULTRET, 0);
                if (status != ThreadStatus.LUA_OK)
                {
                    throw new Exception(Lua.ToString(-1));
                }

                LuaFile.VirtualFiles.Clear();
                LuaFile.VirtualFiles = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
