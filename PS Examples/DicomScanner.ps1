#!PowerShell // ha! this doesn't work on widows ;-) 

# Well, this is something very useful (I think) for anyone managing a PACS.
# Lets say you have a directory full with DICOM files you have no idea what in them (you do, right?) and you want to scan through them.
# So, here's a little powershell script for this:

# Ok, so first we get the directory to scan as a command line parameter
param ($ScanPath)

# This is a function. It extract one element from a DICOM object and return an empty string if the element doesn't exist or doesn't have a value 
function Get-Value($obj, $tag) 
{
  $e = $obj.GetElement($tag)
  if ($e -ne $null)
  {
    if ($e.Value -ne $null)
    { 
      return $e.Value
    }
    else
    {
      return ""
    }
  }
  else
  {
    return "";
  }
}

# Modify this array to add more tags. The pair items are used for headers (and readabity).
$tags = @(
    0x00100010, "patient name",
    0x00100020, "patient id",
    0x00100030, "patient birth date",
    0x00100040, "patient sex",
    0x00080016, "sop class uid",
    0x00080018, "sop instance uid",
    0x00080020, "study date",
    0x00080030, "study time",
    0x00080050, "accession number",
    0x0020000d, "study Instance UID",
    0x0020000e, "Series Instance UID")

# This prints a nice CSV header file. 
# Note all the '","' to enclose values in double quotes and separate them with commas. 
# This repeats all over and is not very fancy :-/
function Print-Header
{
    $line = '"Filename","Status'
    for (($i=1); ($i -lt $tags.Count); ($i+=2))
    {
        $line += '","'
        $line += $tags[$i]
    }
    $line += '"'
    $line
}

# This is where the element values are extracted and one line is retured. 
# The filename is inn the first column and a status is on the second. Then the values from the tags array.
# The status  can be "OK" or "ERROR". You will also see errors in stderr.
function Parse-DicomFile($filename)
{
    $line = '"'
    $line += $filename
    $line += '","OK'
    try {
        $obj = New-Object -ComObject rzdcx.DCXOBJ
        $obj.openFile($filename)
        for (($i=0); ($i -lt $tags.Count); ($i+=2))
        {
            $line += '","'
            $line += Get-Value $obj $tags[$i]
        }
        $line+='"'
    } catch { $line = '"' + $filename+ '","ERROR"' }
    return $line
}

# Here it starts. We first print the header and then scan through the folder and extract the data from every DICOM file. 
# Note ignoring directories
Print-Header
Get-ChildItem -Recurse -Path $ScanPath | Foreach-Object {    
    if ($_.Attributes -ne "Directory") {
        Parse-DicomFile $_.FullName
    }
} 

# Enjoy!!!
