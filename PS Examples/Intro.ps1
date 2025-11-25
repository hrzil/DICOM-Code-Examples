
# download rzdcx
Invoke-WebRequest -Uri http://downloads.roniza.com/rzdcx/2.0.8.7/RZDCX_2087.zip -OutFile ./rzdcx.zip

# unzip it
Expand-Archive ./rzdcx.zip -DestinationPath ./rzdcx

# regsvr32 win32 version
$rzdcx32 = Resolve-Path .\rzdcx\win32\rzdcx.dll
Start-Process regsvr32 -verb runAs -argumentlist $rzdcx32

$rzdcx64 = Resolve-Path .\rzdcx\win32\rzdcx.dll
Start-Process regsvr32 -verb runAs -argumentlist $rzdcx64

# Create DICOM Object
$DCM = New-Object -com rzdcx.DCXOBJ

$files = @(Get-ChildItem ".\X\*.dcm")

# Write headers
"filename, Patient Name, Patient ID"

# For each file extract and print patient name and patient id
foreach ($file in $files) {
  $DCM.openFile($file)
  $pname = $DCM.GetElememnt([int32]::Parse(00100010,'HexNumber)).Value
  $pid = $DCM.GetElememnt([int32]::Parse(00100020,'HexNumber)).Value
  Write-Host $file "," $pname "," $pid
}
