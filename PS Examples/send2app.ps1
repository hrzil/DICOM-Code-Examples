#A template to send a file to a listener example application:

param($fileParam)
$file2send = "./example.dcm" #default name.
if ($fileParam.Length -gt 0)
{    $file2send = $fileParam;}

# network parameters:
$CallingAE = "RZDCXPS";
$CalledAE  = "DSRSCV";
$HostName  = "localhost";
$port      = 104;

#Create a requester:
$REQ = New-Object -com rzdcx.DCXREQ

$succeeded = "";
$failed = "";

#Send the file:
$REQ.Send($CallingAE, $CalledAE, $HostName, $port, $file2send, [ref] $succeeded, [ref] $failed);

"FAILED:"
$failed
"SUCCEEDED:"
$succeeded

#if ($succeeded -eq $file2send -and $failed -eq "")
#{   return 0;}
#else
#{   return 1;}