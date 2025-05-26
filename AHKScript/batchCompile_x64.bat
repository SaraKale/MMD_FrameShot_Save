@echo off
set AHK_COMPILER="AutoHotkey_2.0.19\Ahk2Exe.exe"
set BASE="AutoHotkey_2.0.19\AutoHotkey64.exe"

for %%F in (*.ahk) do (
    echo Compiling %%F ...
    %AHK_COMPILER% /in "%%F" /out "%%~nF.exe" /base %BASE%
)
echo All done!
pause
