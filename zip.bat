CD builds
for /d %%X in (*) do (for /d %%a in (%%X) do ( "C:\Program Files\7-Zip\7z.exe" a -tzip "Space_is_running_out_of_space_%%X.zip" ".\%%a\" ))
PAUSE