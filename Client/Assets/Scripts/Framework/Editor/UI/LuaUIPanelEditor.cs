/********************************************************************************
** auth:  https://github.com/HushengStudent
** date:  2018/01/17 23:41:41
** desc:  编辑器扩展;
*********************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Framework
{
    [CustomEditor(typeof(LuaUIPanel))]
    public class LuaUIPanelEditor : Editor
    {
        private readonly static string _panel =
            "Client/Assets/LuaFramework/Lua/Panel/";

        private readonly static string _ctrlPath =
            EnvVariableHelper.GameFrameworkPath + _panel + "Controller/";

        private readonly static string _panelPath =
            EnvVariableHelper.GameFrameworkPath + _panel + "View/";

        private readonly static string _luaCom =
            "       self.panel.ComName = l_ctrl.luaUIComArray[index]\r\n";

        private StringBuilder _ctrlBuilder = new StringBuilder()
            .AppendLine("-- -")
            .AppendLine("---This code was generated by a tool.")
            .AppendLine("---To coding to do what u want to do.")
            .AppendLine("---")
            .AppendLine(" ")

            .AppendLine("module(\"UI\", package.seeall)")
            .AppendLine(" ")

            .AppendLine("require \"Panel.View.ModuleNamePanel\"")
            .AppendLine(" ")

            .AppendLine("local super = UI.BaseCtrl")
            .AppendLine(" ")
            .AppendLine("ModuleNameCtrl = class(\"ModuleNameCtrl\",BaseCtrl)")
            .AppendLine(" ")
            .AppendLine("function ModuleNameCtrl:ctor()")
            .AppendLine("       super.ctor(self)")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:OnInitialize(args)")
            .AppendLine("       super.OnInitialize(self,args)")
            .AppendLine("       local l_panel = ModuleNamePanel.new()")
            .AppendLine("       self.panel = l_panel:BindLuaCom(self.go)")
            .AppendLine("       self:OnInitializeEx(args)")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:OnUpdate(interval)")
            .AppendLine("       super.OnUpdate(self,interval)")
            .AppendLine("       self:OnUpdateEx(interval)")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:OnHide()")
            .AppendLine("       super.OnHide(self)")
            .AppendLine("       self:OnHideEx()")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:OnResume()")
            .AppendLine("       super.OnResume(self)")
            .AppendLine("       self:OnResumeEx()")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:UnInitialize()")
            .AppendLine("       super.UnInitialize(self)")
            .AppendLine("       self:UnInitializeEx()")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("-----------------------------///beautiful line///-----------------------------")

            .AppendLine(" ")
            .AppendLine(" ")
            .AppendLine(" ")
            .AppendLine("function ModuleNameCtrl:OnInitializeEx(args)")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:OnUpdateEx(interval)")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:OnHideEx()")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:OnResumeEx()")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNameCtrl:UnInitializeEx()")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")
            ;

        private StringBuilder _panelBuilder = new StringBuilder()
            .AppendLine("---")
            .AppendLine("---This code was generated by a tool.")
            .AppendLine("---Forbid To coding.")
            .AppendLine("---")
            .AppendLine(" ")

            .AppendLine("module(\"UI\", package.seeall)")
            .AppendLine(" ")

            .AppendLine("ModuleNamePanel = class(\"ModuleNamePanel\")")
            .AppendLine(" ")

            .AppendLine("function ModuleNamePanel:ctor()")
            .AppendLine(" ")
            .AppendLine("end")
            .AppendLine(" ")

            .AppendLine("function ModuleNamePanel:BindLuaCom(gameObject)")
            .AppendLine("       local l_ctrl = gameObject:GetComponent(\"LuaUIPanel\")")
            .AppendLine("       self.panel = {}")
            .AppendLine("#List#")
            .AppendLine("       return self.panel")
            .AppendLine("end");

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var originalColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("\r\n" + "更新Prefab信息" + "\r\n"))
            {
                LuaUIPanel ctrl = target as LuaUIPanel;
                if (null == ctrl) return;
                LuaUICom[] comArray = ctrl.gameObject.GetComponentsInChildren<LuaUICom>();
                Dictionary<string, List<int>> repeatNameDict = new Dictionary<string, List<int>>();
                for (int i = 0; i < comArray.Length; i++)
                {
                    comArray[i].LuaUIPanel = ctrl;
                    if (string.IsNullOrEmpty(comArray[i].LuaUIComName))
                    {
                        comArray[i].LuaUIComName = checkInvalidFormatName(comArray[i].gameObject.name);
                    }
                    var name = comArray[i].LuaUIComName;
                    for (int j = i + 1; j < comArray.Length; j++)
                    {
                        var str = comArray[j].LuaUIComName;
                        if (name == str)
                        {
                            List<int> list;
                            if (!repeatNameDict.TryGetValue(name, out list))
                            {
                                list = new List<int>();
                                repeatNameDict[name] = list;
                                list.Add(j);
                            }
                            list.Add(i);
                        }
                    }
                }
                if (repeatNameDict.Count > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var temp in repeatNameDict)
                    {
                        if (temp.Value.Count > 0)
                        {
                            temp.Value.Sort();

                            for (int i = 0; i < temp.Value.Count; i++)
                            {
                                builder.Append(temp.Value[i].ToString() + ",");
                            }
                            builder.Append("name repeat:" + temp.Key);
                        }
                        builder.AppendLine(" ");
                    }
                    EditorUtility.DisplayDialog("Dialog", builder.ToString(), "确认");
                    return;
                }
                ctrl.luaUIComArray = new LuaUICom[comArray.Length];
                for (int i = 0; i < ctrl.luaUIComArray.Length; i++)
                {
                    ctrl.luaUIComArray[i] = comArray[i];
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("\r\n" + "创建Controller" + "\r\n"))
            {
                LuaUIPanel ctrl = target as LuaUIPanel;
                if (null == ctrl)
                {
                    LogHelper.PrintError("[LuaUIPanelEditor]LuaUIPanel is null.");
                    return;
                }
                string name = ctrl.gameObject.name;
                string fileName = _ctrlPath + name + "Ctrl.lua";
                string allText = _ctrlBuilder.ToString().Replace("ModuleName", name);
                if (File.Exists(fileName))
                {
                    GUIUtility.systemCopyBuffer = allText;
                    EditorUtility.DisplayDialog("Dialog", "file:" + name + "Ctrl.lua" + " 已存在,成功复制到剪切板!", "确认");
                    return;
                }
                TextWriter tw = new StreamWriter(fileName);
                tw.Close();
                File.WriteAllText(fileName, allText);
                EditorUtility.DisplayDialog("Dialog", "file:" + name + "Ctrl.lua" + " 生成成功!", "确认");
                AssetDatabase.Refresh();
            }
            GUI.backgroundColor = Color.yellow;
            if (GUILayout.Button("\r\n" + "更新Panel" + "\r\n"))
            {
                LuaUIPanel ctrl = target as LuaUIPanel;
                if (null == ctrl)
                {
                    LogHelper.PrintError("[LuaUIPanelEditor]LuaUIPanel is null.");
                    return;
                }
                string name = ctrl.gameObject.name;
                string fileName = _panelPath + name + "Panel.lua";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                TextWriter tw = new StreamWriter(fileName);
                tw.Close();
                string tempStr = _panelBuilder.ToString().Replace("ModuleName", name);
                string comStr = "";
                for (int i = 0; i < ctrl.luaUIComArray.Length; i++)
                {
                    comStr = comStr + _luaCom.Replace("ComName", ctrl.luaUIComArray[i].LuaUIComName).Replace("index", i.ToString());
                }
                string allText = tempStr.Replace("#List#", comStr);
                File.WriteAllText(fileName, allText);
                EditorUtility.DisplayDialog("Dialog", "file:" + name + "Panel.lua" + " 生成成功!", "确认");
                AssetDatabase.Refresh();
            }
            GUI.backgroundColor = Color.blue;

            GUI.backgroundColor = originalColor;
        }

        private string checkInvalidFormatName(string str)
        {
            return str.Replace(" ", "").Replace(".", "")
                .Replace("(", "").Replace("（", "")
                .Replace(")", "").Replace("）", "")
                .Replace("{", "").Replace("}", "")
                .Replace("[", "").Replace("]", "")
                .Replace("【", "").Replace("】", "");
        }
    }
}
