sc stop HoshinoEmail
sc delete HoshinoEmail
ping -n 5 127.0.0.1>nul
ping -n 5 127.0.0.1>nul
ping -n 5 127.0.0.1>nul
%~d0
cd %~dp0
Hoshino.Email.Services.exe Install
sc start HoshinoEmail
pause