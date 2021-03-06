
[Setup]
AppName=Fingerprint Information System
AppVerName=Fingerprint Information System V 16.7.0.0
VersionInfoVersion=16.7.0.0
VersionInfoCompany=BXSofts
VersionInfoProductName= Fingerprint Information System
VersionInfoProductVersion=16.7.0.0
VersionInfoDescription=Manage SDFPB Registers. C@P Baiju Xavior, Fingerprint Expert.
VersionInfoTextVersion=16.7.0.0
VersionInfoCopyright=C@P BXSofts
MinVersion=0,6
AppComments=Manage SDFPB Registers. C@P Baiju Xavior, Fingerprint Expert.
AppCopyright=Copyright � 2007-2021 BXSofts, Inc
AppPublisher=BXSofts, Inc.
AppMutex=Fingerprint_Information_System_APPMUTEX
AppID={{9af41670-6375-4975-91f8-05c8d73d3f48}
DefaultDirName={pf}\BXSofts\Fingerprint Information System
DefaultGroupName=BXSofts\Fingerprint Information System
OutputDir=.
UsePreviousAppDir=yes
UsePreviousGroup=yes
UsePreviousSetupType=yes
OutputBaseFilename=Fingerprint Information System V16.7
SolidCompression=true
PrivilegesRequired=admin
ChangesAssociations=true
TerminalServicesAware=yes
AllowNoIcons=yes
AllowRootDirectory=No
AlwaysShowDirOnReadyPage=yes
AlwaysShowGroupOnReadyPage=yes
AlwaysUsePersonalGroup=yes
Uninstallable=yes
UninstallDisplayIcon={app}\Fingerprint Information System.exe
UninstallFilesDir={app}
WizardImageBackColor=clRed
WindowVisible=false
WizardImageFile=.\Styles\Office2007.bmp
WizardSmallImageFile=.\Styles\WizardImageSmall.bmp
SetupIconFile=.\Styles\Setup Icon.ico
DisableWelcomePage=no

[Icons]
Name: {commonprograms}\BXSofts\Fingerprint Information System\Fingerprint Information System; Filename: {app}\Fingerprint Information System.exe
Name: {commonprograms}\BXSofts\Fingerprint Information System\Fingerprint Information System Help File; Filename: {app}\Help.chm
Name: {commonprograms}\BXSofts\Fingerprint Information System\{cm:UninstallProgram,Fingerprint Information System}; Filename: {uninstallexe}
Name: {commondesktop}\Fingerprint Information System; Filename: {app}\Fingerprint Information System.exe; Tasks: desktopicon; Comment: "A program to automate SDFP Bureau Registers. Designed and programmed by Baiju Xavior, Fingerprint Expert."; HotKey: "ctrl+alt+f"
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\Fingerprint Information System; Filename: {app}\Fingerprint Information System.exe; Tasks: quicklaunchicon ; Comment: "A program to automate SDFP Bureau Registers. Designed and programmed by Baiju Xavior, Fingerprint Expert."; OnlyBelowVersion: 6.1


[CustomMessages]
dotnetmissing=Setup has detected that Microsoft .NET Framework v4.5 or higher is not installed in your system. Do you want to install the application any way?



[code]

// Importing LoadSkin API from ISSkin.DLL
procedure LoadSkin(lpszPath: String; lpszIniFileName: String);
external 'LoadSkin@files:isskin.dll stdcall';

// Importing UnloadSkin API from ISSkin.DLL
procedure UnloadSkin();
external 'UnloadSkin@files:isskin.dll stdcall';

// Importing ShowWindow Windows API from User32.DLL
function ShowWindow(hWnd: Integer; uType: Integer): Integer;
external 'ShowWindow@user32.dll stdcall';

function InitializeSetup(): Boolean;
var
    NetFrameworkVersion: Cardinal;
    NetFrameWorkInstalled : Boolean;
    
begin
   ExtractTemporaryFile('Office2007.cjstyles');
  LoadSkin(ExpandConstant('{tmp}\Office2007.cjstyles'), '');
  Result := True;

	//NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full');
  NetFrameWorkInstalled := RegQueryDWordValue(HKLM,'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', NetFrameworkVersion);

	if NetFrameWorkInstalled =true then
	  begin
		Result := true;
	end;

	if NetFrameWorkInstalled =false then
		begin
			Result:= MsgBox(ExpandConstant('{cm:dotnetmissing}'),mbConfirmation, MB_YESNO)= idyes
		end;
  end;

procedure DeinitializeSetup();
begin
  // Hide Window before unloading skin so user does not get
  // a glimpse of an unskinned window before it is closed.
  ShowWindow(StrToInt(ExpandConstant('{wizardhwnd}')), 0);
  UnloadSkin();
end;

[Files]
Source: .\Styles\ISSkin.dll; DestDir: {app}; Flags: dontcopy
Source: .\Styles\Office2007.cjstyles; DestDir: {tmp}; Flags: dontcopy

Source: .\Fonts\segoeui.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Segoe UI
Source: .\Fonts\segoeuib.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Segoe UI Bold
Source: .\Fonts\SEGOEUII.TTF; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Sego UI Italic
Source: .\Fonts\SEGOEUIZ.TTF; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Sego UI BoldItalic
Source: .\Fonts\Rupee_Foradian.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Rupee Foradian
Source: .\Fonts\Keralite.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Keralite

Source: ..\Fingerprint Information System\bin\Release\*; DestDir: {app}\; Flags: ignoreversion
Source: ..\BackupDatabase\bin\Debug\BackupDatabase.exe; DestDir: {app}\; Flags: ignoreversion

Source: .\WordTemplates\*.docx; DestDir: {userdocs}\BXSofts\Fingerprint Information System\WordTemplates; Flags: ignoreversion
Source: .\WordTemplates\ModusOperandi-PreDefined.txt; DestDir: {userdocs}\BXSofts\Fingerprint Information System\WordTemplates; Flags: ignoreversion
Source: .\WordTemplates\ModusOperandi-UserDefined.txt; DestDir: {userdocs}\BXSofts\Fingerprint Information System\WordTemplates; Flags: onlyifdoesntexist
Source: .\Database\HolidayList.mdb; DestDir: {userdocs}\BXSofts\Fingerprint Information System\WordTemplates; Flags: onlyifdoesntexist
Source: .\Database\FingerPrint.mdb; DestDir: {userdocs}\BXSofts\Fingerprint Information System\Database\; Flags: onlyifdoesntexist uninsneveruninstall
Source: .\Database\WeeklyDiary.mdb; DestDir: {userdocs}\BXSofts\Fingerprint Information System\Database\; Flags: onlyifdoesntexist
Source: .\WIA\wiaaut.dll; DestDir: {sys}; Flags: onlyifdoesntexist uninsneveruninstall sharedfile regserver noregerror
Source: .\Scripts\PinToTaskbar.exe; DestDir: {app}\; Flags: ignoreversion
Source: .\Dependency Files\SQLSysClrTypes32bit.msi; DestDir: {app}\; Flags: ignoreversion
Source: .\Dependency Files\SQLSysClrTypes64bit.msi; DestDir: {app}\; Flags: ignoreversion
;Source: .\Dependency Files\ReportViewer2012.msi; DestDir: {app}\; Flags: ignoreversion
Source: .\VersionHistory\NewVersionFeatures.rtf; DestDir: {userdocs}\BXSofts\Fingerprint Information System; Flags: ignoreversion


[Registry]

Root: HKCR; Subkey: .fpbbk; ValueType: string; ValueName: ; ValueData: Access.MDBFile; Flags: uninsdeletekey noerror

Root: HKCU; Subkey: Software\BXSofts; Flags: noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; Flags: uninsdeletekey noerror

Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\SOCDatagrid; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\DADatagrid; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\FPADatagrid; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\CDDatagrid; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\PSDatagrid; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\ACDatagrid; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\IDDatagrid; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\IODatagrid; Flags: uninsdeletekey noerror

Root: HKCU; Subkey: Control Panel\Desktop; ValueType: dword; ValueName: ForegroundLockTimeout; ValueData: 0; Flags: noerror

Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: Style; ValueData: Office2016; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: BaseColor; ValueData: -16682041; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: ShowPopups; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: PlaySound; ValueData: 1; Flags: noerror uninsdeletekey    createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: LoadAutoTextFromDB; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AutoCompleteMode; ValueData: 3; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: LoadRecordsAtStartup; ValueData: 1; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: LoadCurrentYearRecordsOnly; ValueData: 1; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AppendSOCYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AppendDAYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AppendCDYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AppendFPAYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: TwoDigitSOCYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: TwoDigitDAYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: TwoDigitFPAYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: TwoDigitCDYear; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: LoadDAtoID; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: LoadDAtoAC; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist

Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: QTBarLayout; ValueData: 0,btnNewEntry,btnOpen,btnEdit,btnDelete; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: FirstRun; ValueData: 1;  Flags: noerror uninsdeletekey  createvalueifdoesntexist


Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: RibbonVisible; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AutoCapitalize; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: IgnoreAllCaps; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist

Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: SaveDefaultWidth; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: CreateTable; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: UpdateNullFields; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: HideFields; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: HighLightColor; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\BackupSettings; ValueType: string; ValueName: AutoBackup; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\BackupSettings; ValueType: string; ValueName: AutoBackupTime; ValueData: 15; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\TabColorSettings; ValueType: string; ValueName: TabStyle; ValueData: 9; Flags: noerror uninsdeletekey createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: ShowNewVersionInfo; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: HeadOfAccount; ValueData: 0055-00-501-99; Flags: noerror uninsdeletekey

[Run]
Filename: {app}\PinToTaskbar.exe; Tasks: Pintotaskbar  ; Flags: runhidden
Filename: "msiexec.exe"; Parameters: "/i ""{app}\SQLSysClrTypes64bit.msi"" /quiet"; StatusMsg: "Installing Microsoft SQL System CLR Types 64bit...";  Check: IsWin64;
Filename: "msiexec.exe"; Parameters: "/i ""{app}\SQLSysClrTypes32bit.msi"" /quiet"; StatusMsg: "Installing Microsoft SQL System CLR Types 32bit...";  Check: "not IsWin64";
Filename: {app}\Fingerprint Information System.exe; Description: {cm:LaunchProgram,Fingerprint Information System}; Flags: nowait postinstall skipifsilent runascurrentuser; WorkingDir: {app}

[UninstallDelete]
Name: {app}\DevComponents.DotNetBar2.dll; Type: files
Name: {app}\Fingerprint Information System.pdb; Type: files
Name: {app}\Fingerprint Information System.xml; Type: files
Name: {app}\Fingerprint Information System.exe.config; Type: files
Name: {app}\Fingerprint Information System.exe; Type: files
Name: {app}\Fingerprint Information System.exe.manifest; Type: files
Name: {app}\Fingerprint Information System.application; Type: files
Name: {app}\Microsoft.Office.Interop.Word.dll; Type: files
Name: {app}\Microsoft.Vbe.Interop.dll; Type: files
Name: {app}\office.dll; Type: files
Name: {app}\Interop.WIA.dll; Type: files
Name: {app}\iViewCore.dll; Type: files
Name: {app}; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Backups; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Scanned FP Slips\DA Slips; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Chance Prints; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Scanned FP Slips\Identified Slips; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Scanned FP Slips\Active Criminal Slips; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Performance Statements\Monthly Statement; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Performance Statements\Quarterly Statement; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System\Performance Statements; Type: dirifempty
Name: {userdocs}\BXSofts\Fingerprint Information System; Type: dirifempty
Name: {userdocs}\BXSofts; Type: dirifempty


[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; OnlyBelowVersion: 6.1
Name: pintotaskbar; Description: Add shortcuts to Windows 7 Taskbar and Start Menu ; GroupDescription: {cm:AdditionalIcons}; MinVersion: 6.1

[Dirs]
Name: {userdocs}\BXSofts\Fingerprint Information System\Database
Name: {userdocs}\TA Bills

