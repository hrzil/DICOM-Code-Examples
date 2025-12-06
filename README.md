# DICOM Networking and Data Handling Code Examples

This repository contains essential, working source code examples for **DICOM networking** and **data manipulation** using the **[MODALIZER-SDK DICOM Toolkit](https://www.hrzkit.com/products/modalizer-sdk/)** by H.R.Z.

The examples cover C++, C#, and PowerShell, demonstrating how to implement key DICOM features like Storage SCU, Query/Retrieve, and object creation.

In fact, the **MODALIZER+ DICOM Viewer & Converter** is built entirely using the MODALIZER-SDK.

Need a viewer for the DICOM files you create? Easily inspect, convert, and validate your DICOM Objects using the **[MODALIZER+ DICOM Viewer & Converter](www.dicomizer.com)**.

The supporting data as well as zip archives with the C# and C++ source code can be downloaded from [here](https://downloads.hrzkit.com/MODALIZER-SDK/Examples/).

---

## Disclaimer

This repository contains code examples built with [DICOM](https://www.dicomstandard.org). The examples in this repo come with an [MIT license](/LICENSE), but MODALIZER-SDK itself is a proprietary commercial software and not included in this repository. 

These examples are useful because the SDK allows **100 API calls** before enforcing license restrictions, so you will be able to build and run the examples right away.

For more information, check [the complete MODALIZER-SDK DICOM Library reference manual](https://modalizer-sdk.hrzkit.com).

---

## C++ DICOM Examples Build Instructions
=====================================

All MODALIZER-SDK C++ examples import the TLB file that is embedded in rzdcx.dll and assume that it is located in a folder parallel to the examples folder.
You can see this line in all examples:

#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")

To build and run the examples in this archive please do the following:

1. **Download MODALIZER-SDK latest release** from https://downloads.hrzkit.com/MODALIZER-SDK/
2. **Download the Supporting Data** as zip archives from **[https://downloads.hrzkit.com/MODALIZER-SDK/Examples/](https://downloads.hrzkit.com/MODALIZER-SDK/Examples/)**
3. Extract the zip MODALIZER-SDK archive to a folder next to the examples. 
4. Run "install.bat" as an administrator (right click "install.bat" and select "Run as aministrator")
   
You should have the following directory structure:

<...>\MODALIZER-SDK

<...>\MODALIZER-SDK\win32

...

<...>\ExportCPP\CPPRZDCXExamples.sln

<...>\ExportCPP\CPPCreateDICOMDIR

<...>\ExportCPP\CPPCreateImageExample

...


5. Extract the zip **CppExampleData** archive to the folder next to the examples (so subfolders of the zip merge with the projects folders)

6. Open the solution file CPPRZDCXExamples.sln using Visual Studio (2019) and build it using the default configuration (Debug|Win32).

---

## C# DICOM Examples Build Instructions
====================================

To build and run the examples in this archive please do the following:

1. **Download MODALIZER-SDK latest release** from https://downloads.hrzkit.com/MODALIZER-SDK/
2. **Download the C# Supporting Data** as zip archives from **[https://downloads.hrzkit.com/MODALIZER-SDK/Examples/](https://downloads.hrzkit.com/MODALIZER-SDK/Examples/)**
3. Run "install.bat" as an administrator (right click "install.bat" and select "Run as aministrator"). 

You should have the following directory structure:

<...>\MODALIZER-SDK

<...>\MODALIZER-SDK\win32

...

<...>\ExportCPP\CPPRZDCXExamples.sln

<...>\ExportCPP\CPPCreateDICOMDIR

<...>\ExportCPP\CPPCreateImageExample

...


4. Extract the zip **CsExampleData** archive to the folder next to the examples (so subfolders of the zip merge with the projects folders)

5. Open the solution file CSharpRZDCXExamples.sln using Visual Studio (2019) and build it using the default configuration (Debug|Win32).

---

## MODALIZER SDK PowerShell Examples
=================================

To run the examples in this archive please do the following:

1. **Download MODALIZER-SDK latest release** from https://downloads.hrzkit.com/MODALIZER-SDK/
2. Run "install.bat" as an administrator (right click "install.bat" and select "Run as aministrator")
3. Run the scripts from PowerShell or Console or PowerShell ISE.

