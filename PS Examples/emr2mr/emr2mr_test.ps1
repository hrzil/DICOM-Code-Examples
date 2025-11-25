 #get arguments:
 #Multi-frame file name:
 $mf_file = $args[0];
 
 #Requested frame-number:
 $maxFrame = [int32]$args[1];
 $frameNum*1;
 $broken = $false
 for($frameNum =0; $frameNum -lt $maxFrame; $frameNum++){
    $frameNum*1;
    $sf_file = $mf_file + "_" + $frameNum +".dcm"
    $sf_file ;
    $mf_obj = New-Object -com rzdcx.DCXOBJ;
    $mf_obj.openFile($mf_file);
    
    $sf_obj = New-Object -com rzdcx.DCXOBJ;
    $sf_obj.openFile($sf_file);
    
    $mfPixels = $mf_obj.GetElement(0x7FE00010).ValueArray;
    $sfPixels = $sf_obj.GetElement(0x7FE00010).ValueArray;
    $mfPixels.Count;
    $sfPixels.Count;
    $frameSize = 256*256;
    for($i=0; $i -lt $frameSize; $i++)
    {
        if ($sfPixels[$i] -ne $mfPixels[$i+$frameNum*$frameSize])
        {
            "Broken! at";
            $i;
            "SF:";
            $sfPixels[$i];
            "MF:";
            $mfPixels[$i+$frameNum*$frameSize]
            $broken = $true;
            break;
        }
    }
}
"Broken: "+$broken;
