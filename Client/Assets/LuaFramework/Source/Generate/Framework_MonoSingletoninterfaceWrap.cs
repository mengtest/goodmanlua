﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Framework_MonoSingletoninterfaceWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Framework.MonoSingletoninterface), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("Launch", Launch);
		L.RegFunction("MonoSingletoninterfaceOnInitialize", MonoSingletoninterfaceOnInitialize);
		L.RegFunction("MonoSingletoninterfaceOnUninitialize", MonoSingletoninterfaceOnUninitialize);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Launch(IntPtr L)
	{
#if UNITY_EDITOR
        ToluaProfiler.AddCallRecord("Framework.MonoSingletoninterface.Launch");
#endif
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Framework.MonoSingletoninterface obj = (Framework.MonoSingletoninterface)ToLua.CheckObject<Framework.MonoSingletoninterface>(L, 1);
			obj.Launch();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MonoSingletoninterfaceOnInitialize(IntPtr L)
	{
#if UNITY_EDITOR
        ToluaProfiler.AddCallRecord("Framework.MonoSingletoninterface.MonoSingletoninterfaceOnInitialize");
#endif
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Framework.MonoSingletoninterface obj = (Framework.MonoSingletoninterface)ToLua.CheckObject<Framework.MonoSingletoninterface>(L, 1);
			obj.MonoSingletoninterfaceOnInitialize();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MonoSingletoninterfaceOnUninitialize(IntPtr L)
	{
#if UNITY_EDITOR
        ToluaProfiler.AddCallRecord("Framework.MonoSingletoninterface.MonoSingletoninterfaceOnUninitialize");
#endif
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Framework.MonoSingletoninterface obj = (Framework.MonoSingletoninterface)ToLua.CheckObject<Framework.MonoSingletoninterface>(L, 1);
			obj.MonoSingletoninterfaceOnUninitialize();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
#if UNITY_EDITOR
        ToluaProfiler.AddCallRecord("Framework.MonoSingletoninterface.op_Equality");
#endif
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

