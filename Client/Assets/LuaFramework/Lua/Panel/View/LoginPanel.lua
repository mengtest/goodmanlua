---
---This code was generated by a tool.
---Forbid To coding.
---
 
module("UI", package.seeall)
 
LoginPanel = class("LoginPanel")
 
function LoginPanel:ctor()
 
end
 
function LoginPanel:BindLuaComponent(gameObject)
       local l_ctrl = gameObject:GetComponent("LuaController")
       self.ComponentList = {}
       self.ComponentList.Login = l_ctrl.componentArray[0]
       self.ComponentList.GameObject = l_ctrl.componentArray[1]
       self.ComponentList.GameObject1 = l_ctrl.componentArray[2]
       self.ComponentList.GameObject2 = l_ctrl.componentArray[3]
       self.ComponentList.GameObject3 = l_ctrl.componentArray[4]
       self.ComponentList.GameObject4 = l_ctrl.componentArray[5]
       self.ComponentList.GameObject5 = l_ctrl.componentArray[6]
       self.ComponentList.GameObject6 = l_ctrl.componentArray[7]
       self.ComponentList.GameObject7 = l_ctrl.componentArray[8]
       self.ComponentList.GameObject8 = l_ctrl.componentArray[9]
       self.ComponentList.GameObject9 = l_ctrl.componentArray[10]

       return self.ComponentList
end
