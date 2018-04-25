#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
#Warn  ; Recommended for catching common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

oSC := ComObjCreate("ScriptControl")

; define the Language
oSC.Language := "VBScript"

; define the VBScript
script =
(
Const CSIDL_COMMON_PROGRAMS = &H17
Set objShell = CreateObject("Shell.Application") 
Set objAllUsersProgramsFolder = objShell.NameSpace(CSIDL_COMMON_PROGRAMS) 
strAllUsersProgramsPath = objAllUsersProgramsFolder.Self.Path 
Set objFolder = objShell.Namespace(strAllUsersProgramsPath & "\BXSofts\Fingerprint Information System") 
Set objFolderItem = objFolder.ParseName("Fingerprint Information System.lnk") 
Set colVerbs = objFolderItem.Verbs 
For Each objVerb in colVerbs 
    If Replace(objVerb.name, "&", "") = "Pin to Start Menu" Then objVerb.DoIt 
	If Replace(objVerb.name, "&", "") = "Pin to Taskbar" Then objVerb.DoIt

	
Next
)

oSC.ExecuteStatement(script)