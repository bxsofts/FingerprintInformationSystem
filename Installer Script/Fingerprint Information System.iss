
[Setup]
AppName=Fingerprint Information System
AppVerName=Fingerprint Information System V 12.0.0.0
VersionInfoVersion=12.0.0.0
VersionInfoCompany=BXSofts
VersionInfoProductName= Fingerprint Information System
VersionInfoProductVersion=12.0.0.0
VersionInfoDescription=Manage SDFPB Registers. C@P Baiju Xavior, Fingerprint Expert.
VersionInfoTextVersion=12.0.0.0
VersionInfoCopyright=C@P BXSofts
MinVersion=0,5
AppComments=Manage SDFPB Registers. C@P Baiju Xavior, Fingerprint Expert.
AppCopyright=Copyright © 2007-2018 BXSofts, Inc
AppPublisher=BXSofts, Inc.
AppMutex=Fingerprint_Information_System_APPMUTEX
AppID={{9af41670-6375-4975-91f8-05c8d73d3f48}
DefaultDirName={pf}\BXSofts\Fingerprint Information System
DefaultGroupName=BXSofts\Fingerprint Information System
OutputDir=.
UsePreviousAppDir=yes
UsePreviousGroup=yes
UsePreviousSetupType=yes
OutputBaseFilename=Fingerprint Information System V12.0 Setup
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
WizardImageFile=.\Icons\WizardImage.bmp
WizardSmallImageFile=.\Icons\WizardImageSmall.bmp
SetupIconFile=.\Icons\Setup Icon.ico


[Icons]
Name: {commonprograms}\BXSofts\Fingerprint Information System\Fingerprint Information System; Filename: {app}\Fingerprint Information System.exe
Name: {commonprograms}\BXSofts\Fingerprint Information System\Fingerprint Information System Help File; Filename: {app}\Help.chm
Name: {commonprograms}\BXSofts\Fingerprint Information System\{cm:UninstallProgram,Fingerprint Information System}; Filename: {uninstallexe}
Name: {commondesktop}\Fingerprint Information System; Filename: {app}\Fingerprint Information System.exe; Tasks: desktopicon; Comment: "A program to automate SDFP Bureau Registers. Designed and programmed by Baiju Xavior, Fingerprint Expert."; HotKey: "ctrl+alt+f"
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\Fingerprint Information System; Filename: {app}\Fingerprint Information System.exe; Tasks: quicklaunchicon ; Comment: "A program to automate SDFP Bureau Registers. Designed and programmed by Baiju Xavior, Fingerprint Expert."; OnlyBelowVersion: 6.1


[CustomMessages]
dotnetmissing=Setup has detected that Microsoft .NET Framework v3.5 is not installed in your system. The application will not work correctly until you install it. Do you want to install the application any way?



[code]
function InitializeSetup(): Boolean;
var
    NetFrameWorkInstalled : Boolean;
    Result1 : Boolean;
begin

	NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5');
	if NetFrameWorkInstalled =true then
	  begin
		Result := true;
	end;

	if NetFrameWorkInstalled =false then
		begin
			Result1 := MsgBox(ExpandConstant('{cm:dotnetmissing}'),mbConfirmation, MB_YESNO) = idYes;
			if Result1 =true then
				begin
					Result:=true;
			    end
	    end;
  end;

[Files]

Source: .\Fonts\segoeui.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Segoe UI
Source: .\Fonts\segoeuib.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Segoe UI Bold
Source: .\Fonts\SEGOEUII.TTF; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Sego UI Italic
Source: .\Fonts\SEGOEUIZ.TTF; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Sego UI BoldItalic
Source: .\Fonts\Rupee_Foradian.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Rupee Foradian
Source: .\Fonts\Keralite.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Keralite

Source: ..\Fingerprint Information System\bin\Release\*; DestDir: {app}\; Flags: ignoreversion


Source: .\WordTemplates\*.docx; DestDir: {userdocs}\BXSofts\Fingerprint Information System\WordTemplates; Flags: ignoreversion
Source: .\Database\FingerPrint.mdb; DestDir: {userdocs}\BXSofts\Fingerprint Information System\Database\; Flags: onlyifdoesntexist uninsneveruninstall
Source: .\Report Viewer\wiaaut.dll; DestDir: {sys}; Flags: onlyifdoesntexist uninsneveruninstall sharedfile regserver noregerror
Source: .\Report Viewer\ReportViewer2010.exe; DestDir: {app}\; Flags: ignoreversion
Source: .\Scripts\PinToTaskbar.exe; DestDir: {app}\; Flags: ignoreversion

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

Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\RecentSOCRecords; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\RecentDARecords; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\RecentFPARecords; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\RecentCDRecords; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\RecentIDRecords; Flags: uninsdeletekey noerror
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\RecentACRecords; Flags: uninsdeletekey noerror

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
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: FirstRun; ValueData: 1;  Flags: noerror uninsdeletekey 
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: PdlFPAttestation; ValueData: ;  Flags: noerror uninsdeletekey   createvalueifdoesntexist

Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: RibbonVisible; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AutoCapitalize; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: IgnoreAllCaps; ValueData: 1; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: NilPrintText; ValueData: No action pending; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: PrintRemainsText; ValueData: Search continuing; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: NoPrintRemainsText; ValueData: Fully eliminated or unfit; Flags: noerror uninsdeletekey   createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: SaveDefaultWidth; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: CreateTable; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: UpdateNullFields; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: HideFields; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: HighLightColor; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AutoBackup; ValueData: 1; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: AutoBackupTime; ValueData: 7; Flags: noerror uninsdeletekey  createvalueifdoesntexist
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\General Settings; ValueType: string; ValueName: RenameOldBackupFiles; ValueData: 1; Flags: noerror uninsdeletekey
Root: HKCU; Subkey: Software\BXSofts\Fingerprint Information System\TabColorSettings; ValueType: string; ValueName: TabStyle; ValueData: 9; Flags: noerror uninsdeletekey createvalueifdoesntexist

[Run]
Filename: {app}\PinToTaskbar.exe; Tasks: Pintotaskbar
Filename: "{app}\ReportViewer2010.exe"; StatusMsg: "Installing Report Viewer. Please wait..."; Parameters: "/q"
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

