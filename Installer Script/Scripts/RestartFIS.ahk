#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

Process, Exist, Fingerprint Information System.exe ; check if 'FileName' is running
If (ErrorLevel = 0) ;if not running
	run Fingerprint Information System.exe
Else ; file already running
		;traytip, Message, %FileName% already running, 5, 1
Process, Close, %ErrorLevel%  
	Process, WaitClose, %ErrorLevel% 
	sleep 5000
run Fingerprint Information System.exe