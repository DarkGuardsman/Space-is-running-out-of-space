@echo off

SET /P AREYOUSURE=Are you sure (Y/[N])?
IF /I "%AREYOUSURE%" NEQ "Y" GOTO END

butler push builds/Space_is_running_out_of_space_windows_64 darkguardsman/space-is-running-out-of-space:win-64 --userversion-file version.txt
butler push builds/Space_is_running_out_of_space_windows_32 darkguardsman/space-is-running-out-of-space:win-32 --userversion-file version.txt
butler push builds/Space_is_running_out_of_space_linux darkguardsman/space-is-running-out-of-space:linux --userversion-file version.txt
butler push builds/Space_is_running_out_of_space_osx darkguardsman/space-is-running-out-of-space:osx --userversion-file version.txt

PAUSE



