#create a query object

param($QueryFile)
$logFile = ".\getwithqueryfile.log"

$QRY = New-Object -com rzdcx.DCXOBJ

if ($QueryFile.Length -gt 0)
{ 
$QueryFile

    $QRY.openFile($QueryFile);
}
else 
{
    $ELM = New-Object -com rzdcx.DCXELM

    $ELM.Init(0x00100010) # patient name
    $ELM.Value = "*"
    $QRY.insertElement($ELM);

 #   $ELM.Init(0x00100020) # patient ID
 #   $ELM.Value = "c"
 #   $QRY.insertElement($ELM);

    $ELM.Init(0x00080050) # acc #
    $ELM.Value = "d"
    $QRY.insertElement($ELM);


    $ELM.Init(0x00080052) # QueryRetreive Level
    $ELM.Value = "PATIENT"
    $QRY.insertElement($ELM)
    $QRY.saveFile("query.dcm")
}

$APP = new-object -ComObject rzdcx.DCXAPP
$APP.StartLogging($logFile)
$APP.LogLevel = 5; # debug
$REQ = New-Object -com rzdcx.DCXREQ

#long timeout for debug
$REQ.AssociationRequestTimeout = 1000
$REQ.DimseCommandTimeout       = 1000
$REQ.Get("BARESH", "COMMON", "localhost", 1104, $QRY)

# Print results
$filename
$dcmfile
#"Succeeded: " + $succeeded
#"Failed: " + $failed

# Stop logging
$APP.WriteToLog(5,"Test Script finished")
$App.StopLogging()

"END OF TEST SCRIPT"
