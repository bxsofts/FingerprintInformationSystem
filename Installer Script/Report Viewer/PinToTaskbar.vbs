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