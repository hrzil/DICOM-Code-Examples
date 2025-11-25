MODALIZER SDK C++ Examples
==========================

All MODALIZER-SDK C++ examples imports the TLB file that is embeded in rzdcx.dll and assume that it is located in a folder parallel to the examples folder.
You can see this line in all examples:

#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")

To build the examples in this archive please do the following:

1. Download MODALIZER-SDK latest release from http://downloads.roniza.com/modalizer-sdk

2. Extract the zip MODALIZER-SDK archive to a folder next to the examples. 

You should have the following directory structure:

<...>\MODALIZER-SDK
<...>\MODALIZER-SDK\win32
...
<...>\ExportCPP\CPPRZDCXExamples.sln
<...>\ExportCPP\CPPCreateDICOMDIR
<...>\ExportCPP\CPPCreateImageExample
...

3. Extract the zip CppExampleData archive to the folder next to the examples (so subfolders of the zip merge with the projects folders)

4. regsvr32 the SDK: 
From a command line with admin rights run the following:

regsvr32 <...>\MODALIZER-SDK\win32\rzdcx.dll
regsvr32 <...>\MODALIZER-SDK\x64\rzdcx.dll

5. Open the solution file CPPRZDCXExamples.sln using Visual Studio (2019) and build it using the default configuration (Debug|Win32).

