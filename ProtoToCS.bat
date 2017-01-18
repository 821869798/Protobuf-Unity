REM Generate client protobuf files.
REM Generate cs files to Assets\Scripts\ProtoBuf.

REM Change DIR if necessary.
set CLIENT_DIR=.

set PROTOGEN=%CLIENT_DIR%\Tools\ProtoGen\protogen.exe
set OUT_DIR=%CLIENT_DIR%\Assets\Scripts\ProtoBuf

cd /d proto
for %%f in (*.proto)       do ..\%PROTOGEN% -i:%%f -o:..\%OUT_DIR%\%%f.cs
for %%f in (tanks\*.proto) do ..\%PROTOGEN% -i:%%f -o:..\%OUT_DIR%\%%f.cs
cd ..\..\%CLIENT_DIR%

pause



