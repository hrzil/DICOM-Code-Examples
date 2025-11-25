MODALIZER SDK Code Examples
===========================

Disclaimer:
This repository supplies code samples of MODALIZER-SDK usage in C++, C# and Powershell (in their respective folders).
All of the samples below require the MODALIZER-SDK to run. 
The SDK itself is proprietary and not included in this repository.
Get the SDK only from our downloads site: https://downloads.hrzkit.com/MODALIZER-SDK/

MODALIZER SDK C++ Examples
==========================

All MODALIZER-SDK C++ examples imports the TLB file that is embeded in rzdcx.dll and assume that it is located in a folder parallel to the examples folder.
You can see this line in all examples:

#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")

To build the examples in this archive please do the following:

1. Download MODALIZER-SDK latest release from https://downloads.hrzkit.com/MODALIZER-SDK/

2. Extract the zip MODALIZER-SDK archive to a folder next to the examples. 

3. Run "install.bat" as an administrator (right click "install.bat" and select "Run as aministrator")
   
You should have the following directory structure:

<...>\MODALIZER-SDK
<...>\MODALIZER-SDK\win32
...
<...>\ExportCPP\CPPRZDCXExamples.sln
<...>\ExportCPP\CPPCreateDICOMDIR
<...>\ExportCPP\CPPCreateImageExample
...

3. Extract the zip CppExampleData archive to the folder next to the examples (so subfolders of the zip merge with the projects folders)

4.Open the solution file CPPRZDCXExamples.sln using Visual Studio (2019) and build it using the default configuration (Debug|Win32).

MODALIZER SDK C# Examples
=========================

To build the examples in this archive please do the following:

1. Download MODALIZER-SDK latest release from https://downloads.hrzkit.com/MODALIZER-SDK/

2.Run "install.bat" as an administrator (right click "install.bat" and select "Run as aministrator"). 

You should have the following directory structure:

<...>\MODALIZER-SDK
<...>\MODALIZER-SDK\win32
...
<...>\ExportCPP\CPPRZDCXExamples.sln
<...>\ExportCPP\CPPCreateDICOMDIR
<...>\ExportCPP\CPPCreateImageExample
...

3. Extract the zip CsExampleData archive to the folder next to the examples (so subfolders of the zip merge with the projects folders)

4. Open the solution file CSharpRZDCXExamples.sln using Visual Studio (2019) and build it using the default configuration (Debug|Win32).

MODALIZER SDK PowerShell Examples
=================================

To run the examples in this archive please do the following:

1. Download MODALIZER-SDK latest release from https://downloads.hrzkit.com/MODALIZER-SDK/

2. Run "install.bat" as an administrator (right click "install.bat" and select "Run as aministrator")

3. Open the scripts using PowerShell ISE, change parameters according to your needs and run it.
