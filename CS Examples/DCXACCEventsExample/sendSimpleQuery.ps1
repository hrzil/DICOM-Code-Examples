$req        = New-Object -ComObject RZDCX.DCXREQ
$identifier = New-Object -ComObject RZDCX.DCXOBJ
$el         = New-Object -ComObject RZDCX.DCXELM

$el.Init(0x00100010)
$el.Value = "TEST^NAME"
$identifier.InsertElement($el)

$el.Init(0x00100020)
$el.Value = "*"
$identifier.InsertElement($el)

 $el.Init(0x00080052) # QueryRetreive Level
 $el.Value = "PATIENT"
 $identifier.insertElement($el)

$req.Query("RZDCXPS","RZDCXTEST", "localhost", 1104, "1.2.840.10008.5.1.4.31",$identifier)