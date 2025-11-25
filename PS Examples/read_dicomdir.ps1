# Send DICOMDIR Patients to PACS

param($rootFolder);


# Look for a DICOMDIR file in the provided path and scan it for all referenced file ID's
# return an array of referenced file id's (relative path to root dir)
Function GetDICOMDIRRefFileIDs($rooDir)
{
    # Initialize he return string
    $files = @(); 

    $dir = New-Object -ComObject rzdcx.DCXDICOMDIR;
    $dir.Init($rootFolder + "DICOMDIR");

    $patients=$dir.getPatientIterator();
    while (-Not $patients.AtEnd()) {
        $p=$patients.Get();
        $patname = $p.GetElement(0x100010).value;
        $patid = $p.GetElement(0x100020).value;

        # "Patient Name: " + $patname + ", Patient ID: " + $patid

        $studies = $dir.getStudyIterator($patid);
        while (-Not $studies.AtEnd()) {
            $s=$studies.Get();
            $suid = $s.GetElement(0x0020000d).value;
            $sdate = $s.GetElement(0x00080020).value;
            
            # "Study Instance UID: " + $suid + ", Study Date: " + $sdate;
      
            $series = $dir.getSeriesIterator($patid, $suid);
            while (-Not $series.AtEnd()) {
                $e = $series.Get();
                $seriesUid = $e.GetElement(0x0020000e).value;
                
                # "Series Instance UID: " + $seriesUid;

                $instances = $dir.getSeriesLeafIterator($patid, $suid, $seriesUid);
                while (-Not $instances.AtEnd()) {
                    $i = $instances.Get();
                    $sopClassUid = $i.GetElement(0x00080016).value;
                    $sopInstanceUid = $i.GetElement(0x00080018).value;
                    $refFileId = $i.GetElement(0x00041500).value

                    # "SOP Class UID: " + $sopClassUid + ", SOP Instance UID: " + $sopInstanceUid + ", Referenced File ID: " + $refFileId

                    if (Join-Path -Path $rootFolder -ChildPath $refFileId | Test-Path) {
                        $files += $refFileId;
                    } 
                    else
                    {
                        "File referenced in DICOMDIR not found on disk: " + $refFileId;
                    }
                     
                    $instances.Next();
                }
                $series.Next();
            }
            $studies.Next();
        }
        $patients.Next();
    }

    return $files;
}

if (Join-Path -Path $rootFolder -ChildPath "DICOMDIR" | Test-Path)
{
    $fileRefIDs = GetDICOMDIRRefFileIDs($rootFolder);

    # concatenate it to full pathes ; separated as used in DCXREQ.Send
    $files = "";
    foreach($f in $fileRefIDs) {
        $files += Join-Path -Path $rootFolder -ChildPath $f;
        $files += ";";
    }

    "Found " + $fileRefIDs.Count.ToString() + " files to send."

    $r = New-Object -ComObject rzdcx.DCXREQ;
    $passed = "";
    $failed = "";
    $r.Send("MY_AE_TITLE", "THEIR_AE_TITLE", "localhost", 5108, $files, [ref]$passed, [ref]$failed);

    if ($failed.Length -gt 0) {
        "Some files didn't go through."
        "Failed files: " + $failed;
    } 
    else 
    {
        "All files sent.";
    } 
}
else
{
    "No DICOMDIR here";
    exit;
}

