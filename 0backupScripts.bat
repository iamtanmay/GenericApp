mkdir ScriptsBckup
robocopy ./Scripts ./ScriptsBckup/Scripts /E
cd ScriptsBckup
del /s *.meta
powershell.exe Compress-Archive ./Scripts ../Scripts_%date:~7,2%-%date:~4,2%-%date:~10,4%.zip
cd ..
rmdir /s /q ScriptsBckup
git add Scripts/\\*
git commit -m "."
git push