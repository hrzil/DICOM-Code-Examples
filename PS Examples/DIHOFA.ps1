# DIHOFA on PowerShell
# Dicom Hot Folder App

$TAG_StudyUID = 0x0020000D;

# Extracts a study UID from a given DICOM file.
function Get-StudyUID{
    param($path);

    #file doesn't exist
    if (($path -eq $null )-or(-not (Test-Path -Path $path -PathType Leaf))){
        return "";
    }
    
    $obj = New-Object -ComObject rzdcx.dcxobj;
    try{
        $obj.openFile($path);
    }
    catch [System.Runtime.InteropServices.COMException]{
        # Not a DICOM
        Write-Host "Non-Dicom File detected: [ " $path " ]";
        return $null;
    }
    $elm = $obj.GetElement($TAG_StudyUID);
 
    if(($elm.Length) -gt 0){
        #Write-Host $elm.Value;
        return $elm.Value;
    }
    else{
        # studyUID Element was empty or absent
        Write-Host "No Study UID was found.";
        return "";
    }
}

# Recursivly deletes folders which are empty of files, 
# Including the input path, if it turnes to be empty of files.
function Remove-Empties{
    param($path);

    if(-not (Test-Path -Path $path -PathType Container)){
        return;
    }
    # Remove sub-folders
    foreach($sub in (Get-ChildItem -Path $path -Directory)){
        
        Remove-Empties($path + "\" + $sub);
    }
    # Remove self if empty
    if((Get-ChildItem -Path $path -File) -eq $null){
        Remove-Item -Path $path -Force;
    }
    return;
}
# Cleans a given path of the subfolders empty of files
function Remove-Empty-SubFoldesrs{
    param($path);

    foreach($sub in (Get-ChildItem -Path $path -Directory)){
        Remove-Empties -path ($path+"\"+$sub);
    }
}

# Reads the given folder content and returns the whole-files list
function Scan-Folder{
    param($inputFolder);

    if (-not (Test-Path -Path $inputFolder -PathType Container)){
        Write-Host "Input folder doesn't exist. Exiting..."
        return $null;
    }
    Write-Host "Scanning input folder [" $inputFolder "] :";
    $allFiles = Get-ChildItem -Path $inputFolder -Recurse -File;
    Write-Host "Found " $allFiles.Count "files in total";
    
    return $allFiles;
}

# Transfers the failed files to the given reject folder
function Move-Rejects{
    param($rejectFolder, $study, $failed);

    $currFailedFolder = $rejectFolder + "/" + $study;
    if (-not (Test-Path -Path $rejectFolder -PathType Container)){
        New-Item -Path $currFailedFolder -ItemType Directory > $null;
    }
    Move-Item -Path $failed -Destination $currFailedFolder -Force;
}
# Moves a given file to the "Not-DICOM" folder
function Move-NotDICOM{
    param($file,$rejectFolder);

    Move-Rejects -rejectFolder $rejectFolder -study "Not-DICOM" -failed $file;
}

# Deletes a list of files
function Remove-Files{
    param($pathList);

    $files = $pathList.Split(";");
    foreach($f in $files){
        if($f.Length -gt 0){
            if(Test-Path -Path $f -PathType Leaf){
                Remove-Item -Path $f -Force;
            }
        }
    }
}

function Send-DICOMs{
    param(   $DihofaAET, $TargetAET, $TargetHost, $TargetPort
                        ,$fileBatch, $succeeded, $failed  );

    if($fileBatch.Length -eq 0){ return; }

    try{
        $req.Send(   $DihofaAET, $TargetAET, $TargetHost, $TargetPort
                        ,$fileBatch, [ref] $succeeded, [ref] $failed  );
    }
    catch [System.Runtime.InteropServices.COMException]{
        # Timeout
        Write-Host "An Error occoured while sending DICOM files: " $Error[0]; 
        #log...
        return;
    }
}

#The main operational loop
function DIHOFA-Loop{
    param(   $inputFolder, $rejectFolder, $DihofaAET = "DIHOFA",
             $TargetAET, $TargetHost, $TargetPort, $groupbyStudy = $False,
             $MAX_FILES = 100);
    
    Write-Host "Welcome";
    
    # Initialize loop
    $req = New-Object -ComObject rzdcx.dcxreq;

    $fileBatch = "";
    $currJobStudy = "";
    
    $counter = 0;
    $succeeded = "";
    $failed = "";

    # Loop through the files
    Get-ChildItem -Path $inputFolder -File -Recurse | ForEach-Object -Process {
        $currFileName = $_.FullName;
        $currFileStudy = Get-StudyUID($currFileName);
        
        # Not a DICOM or no StudyUID
        if($currFileStudy.Length -eq 0){
            Write-Host "Moving file to: " $rejectFolder;
            Move-NotDICOM -file $currFileName -rejectFolder $rejectFolder;
            return;
        }

        if($count -le $MAX_FILES -and (($currFileStudy -eq $currJobStudy) -or (-not $groupbyStudy))){
            $fileBatch+=";" + $currFileName;
            # Append file to next to be sent batch 
            $counter++;
            return;
            }
        
        # Send batch
        Write-Host "Sending batch of " $counter " files";
        Send-DICOMs ( $DihofaAET, $TargetAET, $TargetHost, $TargetPort, $fileBatch, $succeeded, $failed  );
        Send-DICOMs -DihofaAET $DihofaAET -TargetAET $TargetAET -TargetHost $TargetHost -TargetPort $TargetPort -fileBatch $fileBatch -succeeded $succeeded -failed $failed;

        # Remove succeeded if any
        if($succeeded.Length -gt 0){
            Remove-Files -Path $succeeded;
            $succeeded = "";
        }

        # Move failed if any
        if ($failed.Length -gt 0){
            Move-Rejects -rejectFolder $rejectFolder -study $currJobStudy;
            $failed = "";
        }

        # Reset batch
        if($i -ne $allFiles.Count - 1){
            $counter = 0;
            $currJobStudy = $currFileStudy;
            $fileBatch = $currFileName;
            Write-Host "Starting new job."
            Write-Host "Batch's study UID : " $currJobStudy;
        }
        # Send last batch!!!
        Send-DICOMs ( $DihofaAET, $TargetAET, $TargetHost, $TargetPort, $fileBatch, $succeeded, $failed  );
    }
    
    # Clean Input folder of empty folders
    Write-Host "Cleaning input folder";
    Remove-Empty-SubFoldesrs -path $inputFolder;

    Write-Host "Finished";
}

function dihofa_test {
    DIHOFA-Loop -inputFolder C:\DIHOFA\InputFolder -rejectFolder C:\DIHOFA\Rejects -DihofaAET "DIHOFA" -TargetAET "DSRSVC" -TargetHost "localhost" -TargetPort 104 -MAX_FILES 100 -groupbyStudy $True;
}

dihofa_test;