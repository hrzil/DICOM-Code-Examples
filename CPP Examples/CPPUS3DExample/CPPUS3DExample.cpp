/*
 * 
 * Copyright (c) 2015, H.R.Z. SOftware Services LTD
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions 
 * are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
 *    in the documentation and/or other materials provided with the distribution.
 * 
 * 3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from 
 *    this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
 * OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; 
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

/// \page CPPTestApplications C++ Test Applications
/// The C++ test applications can be downloaded from www.roniza.com/downloads
/// \section CPPUS3DExample C++ Creating Enhanced Ultrasound 3D Object Example
/// Creating Enhanced Ultrasound 3D Object Example
/// This example shows how to use the DCXOBJ in the
/// rzdcx.dll to create enhanced ultrasound 3D object
/// \example CPPUS3DExample.cpp 

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

/*

IE			Module								Reference	Usage
Patient		Patient 							C.7.1.1		M
			Clinical Trial Subject				C.7.1.3		U

Study		General Study						C.7.2.1		M
			Patient Study 						C.7.2.2		U
			Clinical Trial Study				C.7.2.3		U

Series		General Series						C.7.3.1		M
			Enhanced US Series					C.8.24.1	M
			Clinical Trial Series				C.7.3.2		U

Frame of Reference	
			Frame of Reference					C.7.4.1		M
			Ultrasound Frame of Reference		C.8.24.2	M
			Synchronization						C.7.4.2		M

Equipment	General Equipment 					C.7.5.1		M
			Enhanced General Equipment			C.7.5.2		M

Image		General Image						C.7.6.1		M
			Image Pixel 						C.7.6.3		M
			Enhanced Contrast/Bolus				C.7.6.4	b	C - Required if contrast media was applied. See Section A.59.3.1.2 for baseline context group ID.
			Multi-frame Functional Groups		C.7.6.16	M
			Multi-frame Dimension				C.7.6.17	M
			Cardiac Synchronization				C.7.6.18.1	C - Required if cardiac synchronization was applied.
			Respiratory Synchronization			C.7.6.18.2	C - Required if respiratory synchronization was applied.
			Device								C.7.6.12	U
			Acquisition Context					C.7.6.14	M
			Specimen							C.7.6.22	U
			Enhanced Palette Color Lookup Table	C.7.6.23	U
			Enhanced US Image 					C.8.24.3	M
			IVUS Image							C.8.24.4	C - Required if Modality = IVUS
			Excluded Intervals					C.8.24.5	U
			SOP Common							C.12.1		M
			Frame Extraction					C.12.3		C - Required if the SOP Instance was created in response to a Frame-Level retrieve request
 
 */

static int ROWS=512;
static int COLUMNS=512;
static int BITS_ALLOCATED=12;
static int BITS_STORED=16;
static int RESCALE_INTERCEPT=0;
static int SAMPLES_PER_PIXEL=1;
static int NUMBER_OF_FRAMES=10;
static char* PHOTOMETRIC_INTERPRETATION="MONOCHROME2";

void CreateAndInsertElement(IDCXOBJPtr& obj, DICOM_TAGS_ENUM tag)
{
	obj->insertElement(obj->createElement(tag));
}

template<typename T>
void CreateAndInsertElement(IDCXOBJPtr& obj, DICOM_TAGS_ENUM tag, T value)
{
	IDCXELMPtr e(obj->createElement(tag));
	e->Value = value;
	obj->insertElement(e);
}

template<typename T>
void CreateAndInsertElement(IDCXOBJPtr& obj, DICOM_TAGS_ENUM tag, T* valuePtr, int vm)
{
	IDCXELMPtr e(obj->createElement(tag));
	e->Length = vm;
	e->Value = (unsigned long)valuePtr;
	obj->insertElement(e);
}

template<>
void CreateAndInsertElement<IDCXOBJIteratorPtr>(IDCXOBJPtr& obj, DICOM_TAGS_ENUM tag, IDCXOBJIteratorPtr value)
{
	IDCXELMPtr e(obj->createElement(tag));
	e->PutValue( &_variant_t( (IUnknown*)value, false ) );
	obj->insertElement(e);
}


// C.7.1.1
static void Patient_Patient(IDCXOBJPtr &obj) 
{
	CreateAndInsertElement(obj, patientName, "US3D Volume^Example"); // PatientsName
	CreateAndInsertElement(obj, patientID, "123456789"); // PatientID
	CreateAndInsertElement(obj, PatientBirthDate, "20101120");// PatientsBirthDate
	CreateAndInsertElement(obj, PatientSex, "O"); // PatientSex
}

// C.7.2.1
static void Study_GeneralStudy(IDCXOBJPtr &obj) 
{
	CreateAndInsertElement(obj, studyInstanceUID, "999.999.999.999");	// Study Instance UID	(0020,000D)				1	Unique identifier for the Study.
	CreateAndInsertElement(obj, StudyDate);								//	Study Date	(0008,0020)						2	Date the Study started.
	CreateAndInsertElement(obj, StudyTime);								//	Study Time	(0008,0030)						2	Time the Study started.
	CreateAndInsertElement(obj, ReferringPhysicianName);				//	Referring Physician's Name	(0008,0090)		2	Name of the patient's referring physician
}

static void Series_GeneralSeries(IDCXOBJPtr &obj) 
{
	/*
		Modality			(0008,0060)		1	Type of equipment that originally acquired the data used to create the images in this Series. See C.7.3.1.1.1 for Defined Terms. 
		Series Instance UID	(0020,000E)		1	Unique identifier of the Series.
		Series Number		(0020,0011)		2	A number that identifies this Series.
	*/
	CreateAndInsertElement(obj, Modality, "US");
	CreateAndInsertElement(obj, seriesInstanceUID, "999.999.999.999.999");
	CreateAndInsertElement(obj, SeriesNumber);
}

static void Series_EnhancedUSSeries(IDCXOBJPtr &obj) 
{
	/*
		Nothing mandatory here!!!
	*/
}

// Frame of Reference			C.7.4.1		M
static void FrameOfReference_FrameOfReference(IDCXOBJPtr &obj) 
{
	/*
		Frame of Reference UID			(0020,0052)	1	Uniquely identifies the frame of reference for a Series. See C.7.4.1.1.1 for further explanation.
		Position Reference Indicator	(0020,1040)	2	Part of the patient's anatomy used as a reference, such as the iliac crest, orbital-medial, sternal notch, symphysis pubis, xiphoid, lower coastal margin, external auditory meatus. See C.7.4.1.1.2 for further explanation.
	*/
	CreateAndInsertElement(obj, FrameOfReferenceUID, "999.999.999.999.999.1");
	CreateAndInsertElement(obj, PositionReferenceIndicator);

}

//Ultrasound Frame of Reference		C.8.24.2	M
// US Frame Of Reference Module defines a transformation from trancducer coordinate system to the volume coordinate system
static void FrameOfReference_UltrasoundFrameOfReference(IDCXOBJPtr &obj) 
{
	/*
		Volume Frame of Reference UID		(0020,9312)	1	Uniquely identifies this Volume Frame of Reference.
	*/
	CreateAndInsertElement(obj, VolumeFrameOfReferenceUID, "999.999.999.999.999.2");

	/*
		Ultrasound Acquisition Geometry 	(0020,9307)	1	Characteristic of the ultrasound acquisition geometry.  Defined Terms:
			APEX = 	there exists an apex of the scan lines from which the volume data was acquired.
	*/
	CreateAndInsertElement(obj, UltrasoundAcquisitionGeometry, "APEX");

	/*
		Apex Position						(0020,9308)	1C	Position of the apex (or phase center) of the acquisition geometry, encoded as xA, yA, and zA in mm units in the Volume Frame of Reference. The apex (xA, yA, zA) may be located in the volume or exterior to it.
			Required if value of Ultrasound Acquisition Geometry (0020,9307) is APEX.
	*/
	CreateAndInsertElement(obj, ApexPosition, "0.0\\0.0\\-10.0");

	/*
		Volume to Transducer Mapping Matrix	(0020,9309)	1	A 4x4 homogeneous transformation matrix that maps the Volume Frame of Reference coordinate system (XV, YV, ZV) to the Transducer Frame of Reference coordinate system (XX,YX, ZX). Matrix elements shall be listed in row-major order. See Section C.8.24.2.1 for details.
	*/
	CreateAndInsertElement(obj, VolumeToTransducerMappingMatrix, 
		"1.0\\0.0\\0.0\\1.0"
		"0.0\\1.0\\0.0\\1.0"
		"0.0\\0.0\\1.0\\1.0"
		"0.0\\0.0\\0.0\\1.0");

	/*
		Patient Frame of Reference Source	(0020,930C)	1C	Indicates how the supplied Image Position (Patient) (0020,0032) and Image Orientation (Patient) (0020,0037) values are obtained.
			Enumerated Values:
				TABLE 
			A positioning device, such as a gantry, was used to generate these values.
				ESTIMATED
			Estimated patient position / orientation (eg, estimated by the user), or if reliable information is not available.
				REGISTRATION
			Acquisition has been spatially registered to a prior image set.
			Required if either Image Position (Patient) (0020,0032) or Image Orientation (Patient) (0020,0037) is present.
	*/
	CreateAndInsertElement(obj, PatientFrameOfReferenceSource, "ESTIMATED");

	/*
		Table Frame of Reference UID		(0020,9313)	1C	Uniquely identifies this Table Frame of Reference.
		Required if Patient Frame of Reference Source (0020,930C) is TABLE.

		Volume to Table Mapping Matrix		(0020,930A)	1C	A 4x4 homogeneous transformation matrix that maps the Volume Frame of Reference coordinate system (XV,YV, ZV) to the Table Frame of Reference coordinate system (XT,YT, ZT). Matrix elements shall be listed in row-major order. See Section C.8.24.2.2 for details. 
			Required if Patient Frame of Reference Source (0020,930C) is TABLE.
	*/
}


// Synchronization						C.7.4.2		M
static void FrameOfReference_Synchronization(IDCXOBJPtr &obj) {
	/*
		Synchronization Frame of Reference UID	(0020,0200)	1	UID of common synchronization environment. See C.7.4.2.1.1.
	*/
	CreateAndInsertElement(obj, SynchronizationFrameOfReferenceUID, "1.2.840.10008.15.1.1"); /// UTC, see C.7.4.2.1.1 Synchronization Frame of Reference UID

	/*
Synchronization Trigger					(0018,106A)	1	Data acquisition synchronization with external equipment
Enumerated Values:
SOURCE - this equipment provides synchronization channel or trigger to other equipment
EXTERNAL - this equipment receives synchronization channel or trigger from other equipment
PASSTHRU - this equipment receives synchronization channel or trigger and forwards it 
NO TRIGGER - data acquisition not synchronized by common channel or trigger
*/
	CreateAndInsertElement(obj, SynchronizationTrigger, "NO TRIGGER");

	/*
Trigger Source or Type					(0018,1061)	3	Specifies equipment ID of trigger source and/or type of trigger
Synchronization Channel					(0018,106C)	1C	Identifier of waveform channel that records the synchronization channel or trigger, see C.7.4.2.1.3. 
Required if synchronization channel or trigger is encoded in a waveform in this SOP Instance

Acquisition Time Synchronized			(0018,1800)	1	Acquisition DateTime (0008,002A) synchronized with external time reference. 
Enumerated Values: Y, N
See C.7.4.2.1.4
	*/
	CreateAndInsertElement(obj, AcquisitionTimeSynchronized, "Y");
}

// General Equipment 					C.7.5.1		M
//static void Equipment_GeneralEquipment() 
//{
//	/*
//	Manufacturer	(0008,0070)	2	Manufacturer of the equipment that produced the composite instances.
//	*/
//	CreateAndInsertElement("RZ Software Services");
//}

// Enhanced General Equipment			C.7.5.2		M
static void Equipment_EnhancedGeneralEquipment(IDCXOBJPtr &obj) 
{
/*
Manufacturer				(0008,0070)	1	Manufacturer of the equipment that produced the composite instances.
Manufacturer’s Model Name	(0008,1090)	1	Manufacturer’s model name of the equipment that produced the composite instances.
Device Serial Number		(0018,1000)	1	Manufacturer’s serial number of the equipment that produced the composite instances. 
Software Versions			(0018,1020)	1	Manufacturer’s designation of software version of the equipment that produced the composite instances. See Section C.7.5.1.1.3.
*/
	CreateAndInsertElement(obj, Manufacturer, "RZ Software Services");
	CreateAndInsertElement(obj, ManufacturerModelName, "RZDCX");
	CreateAndInsertElement(obj, DeviceSerialNumber, "999999999");
	CreateAndInsertElement(obj, SoftwareVersions, "1.0.2.1");
}

//General Image						C.7.6.1		M
static void Image_GeneralImage(IDCXOBJPtr &obj) 
{
/*
Instance Number				(0020,0013)	2	A number that identifies this image.
	Note:	This Attribute was named Image Number in earlier versions of this Standard.
*/
	CreateAndInsertElement(obj, InstanceNumber);
}

// a function to create and allocate a safe array of bytes from the pixel array (char[])

static void pixels2safeArray(/*[In]*/unsigned char* pArr, /*[Out]*/SAFEARRAY*& sArr, /*[In]*/ULONG size) {
	HRESULT hr = S_OK;
	sArr = SafeArrayCreateVector(VT_UI1, 0, size);
	char* pdata;
	hr = SafeArrayAccessData(sArr, (void**)&pdata);
	for (UINT i = 0; i < size; i++) {
		pdata[i] = pArr[i];
	}
	hr = SafeArrayUnaccessData(sArr);
}
//Image Pixel 						C.7.6.3		M
static void Image_ImagePixel(IDCXOBJPtr& obj) 
{
/*
Table C.7-11b
IMAGE PIXEL MACRO ATTRIBUTES
Attribute Name	Tag	Type	Attribute Description
Samples per Pixel	(0028,0002)
1	Number of samples (planes) in this image. See C.7.6.3.1.1 for further explanation. */
	CreateAndInsertElement(obj,SamplesPerPixel, 1);
	/*
Photometric Interpretation	(0028,0004)
1	Specifies the intended interpretation of the pixel data. See C.7.6.3.1.2 for further explanation. */

	CreateAndInsertElement(obj, PhotometricInterpretation, "MONOCHROME2");
	/*
Rows	(0028,0010)
1	Number of rows in the image. */

	CreateAndInsertElement(obj, Rows, 128);
	/*
Columns	(0028,0011) 
1	Number of columns in the image */
	CreateAndInsertElement(obj, Columns, 128);

/*
Bits Allocated	(0028,0100)

1	Number of bits allocated for each pixel sample. Each sample shall have the same number of bits allocated. See PS 3.5 for further explanation.*/
	CreateAndInsertElement(obj, BitsAllocated, 8);

/*
Bits Stored	(0028,0101)
1	Number of bits stored for each pixel sample. Each sample shall have the same number of bits stored. See PS 3.5 for further explanation.*/
	CreateAndInsertElement(obj, BitsStored, 8);

	/*
	High Bit	(0028,0102)
1	Most significant bit for pixel sample data. Each sample shall have the same high bit. See PS 3.5  for further explanation.*/
	CreateAndInsertElement(obj, HighBit, 8);
	/*
Pixel Representation	(0028,0103)
1	Data representation of the pixel samples. Each sample shall have the same pixel representation. Enumerated Values: 
	0000H = unsigned integer.
	0001H = 2's complement
	*/
	CreateAndInsertElement(obj, PixelRepresentation, 0);

	/*
Pixel Data	(7FE0,0010)
1C	A data stream of the pixel samples that comprise the Image. See C.7.6.3.1.4 for further explanation.
Required if Pixel Data Provider URL (0028,7FE0) is not present.*/
	{
		IDCXELMPtr el(__uuidof(DCXELM));
		el->Init(PixelData);
		ULONG len = ROWS * COLUMNS * SAMPLES_PER_PIXEL * NUMBER_OF_FRAMES;
			el->Length = len;
		unsigned char* pixels=new unsigned char[ROWS*COLUMNS*NUMBER_OF_FRAMES];
		for (int n=0; n<NUMBER_OF_FRAMES; n++) {
			for (int y=0; y<ROWS;y++){
				for (int x=0;x<COLUMNS;x++){
					int i=x+COLUMNS*y*n;
					pixels[i]=((i)%(1<<BITS_STORED))-RESCALE_INTERCEPT;
				}
			}
		}
		if (sizeof(unsigned char)==1 || sizeof(unsigned char) == 3)
			el->ValueRepresentation = VR_CODE_OB;
		SAFEARRAY* newArr;
		pixels2safeArray(pixels, newArr, len);
		variant_t newVar(newArr);
		VariantInit(&newVar);
		newVar.vt = VT_UI1 | VT_ARRAY;
		newVar.parray = newArr;
		el->Value = newVar;
		obj->insertElement(el);
	}
/*
Planar Configuration	(0028,0006)
1C	Indicates whether the pixel data are sent color-by-plane or color-by-pixel. Required if Samples per Pixel (0028,0002) has a value greater than 1. See C.7.6.3.1.3 for further explanation.
Pixel Aspect Ratio	(0028,0034)
1C	Ratio of the vertical size and horizontal size of the pixels in the image specified by a pair of integer values where the first value is the vertical pixel size, and the second value is the horizontal pixel size. Required if the aspect ratio values do not have a ratio of 1:1 and the physical pixel spacing is not specified by Pixel Spacing (0028,0030), or Imager Pixel Spacing (0018,1164) or Nominal Scanned Pixel Spacing (0018,2010), either for the entire Image or per-frame in a Functional Group Macro. See C.7.6.3.1.7.
Smallest Image Pixel Value	(0028,0106)
3	The minimum actual pixel value encountered in this image.
Largest Image Pixel Value	(0028,0107)
3	The maximum actual pixel value encountered in this image.
Red Palette Color Lookup Table Descriptor 	(0028,1101)
1C	Specifies the format of the Red Palette Color Lookup Table Data (0028,1201) Required if Photometric Interpretation (0028,0004) has a value of PALETTE COLOR or Pixel Presentation (0008,9205) at the image level equals COLOR or MIXED. See  C.7.6.3.1.5 for further explanation.
Green Palette Color Lookup Table Descriptor 	(0028,1102)
1C	Specifies the format of the Green Palette Color Lookup Table Data (0028,1202) Required if Photometric Interpretation (0028,0004) has a value of PALETTE COLOR or Pixel Presentation (0008,9205) at the image level equals COLOR or MIXED. See C.7.6.3.1.5 for further explanation.
Blue Palette Color Lookup Table Descriptor 	(0028,1103)
1C	Specifies the format of the Blue Palette Color Lookup Table Data (0028,1203) Required if Photometric Interpretation (0028,0004) has a value of PALETTE COLOR or Pixel Presentation (0008,9205) at the image level equals COLOR or MIXED. See C.7.6.3.1.5 for further explanation.
Red Palette Color Lookup Table Data	(0028,1201)
1C	Red Palette Color Lookup Table Data. 
Required if Photometric Interpretation (0028,0004) has a value of PALETTE COLOR or Pixel Presentation (0008,9205) at the image level equals COLOR or MIXED. See C.7.6.3.1.6 for further explanation.
Green Palette Color Lookup Table Data	(0028,1202)
1C	Green Palette Color Lookup Table Data. Required if Photometric Interpretation (0028,0004) has a value of PALETTE COLOR or Pixel Presentation (0008,9205) at the image level equals COLOR or MIXED. See C.7.6.3.1.6 for further explanation.
Blue Palette Color Lookup Table Data	(0028,1203)
1C	Blue Palette Color Lookup Table Data. Required if Photometric Interpretation (0028,0004) has a value of PALETTE COLOR or Pixel Presentation (0008,9205) at the image level equals COLOR or MIXED. See C.7.6.3.1.6 for further explanation.
ICC Profile	(0028,2000)
3	An ICC Profile encoding the transformation of device-dependent color stored pixel values into PCS-Values.
See Section C.11.15.1.1.1.
When present, defines the color space of color Pixel Data (7FE0,0010) values, and the output of Palette Color Lookup Table Data (0028,1201-1203).
Note:	The profile applies only to the Pixel Data (7FE0,0010) attribute at the same level of the dataset and not to any icons nested within sequences, which may or may not have their own ICC profile specified.

*/
}

static void Image_EnhancedUSImage(IDCXOBJPtr& obj) 
{
	//Attribute Name	Tag	Type	Attribute Description
	//Image Type	(0008,0008)
	//1	Image identification characteristics.
	//See C.8.24.3.2 for specialization.
	CreateAndInsertElement(obj, ImageType, "ORIGINAL\\PRIMARY");

	//Samples Per Pixel	(0028,0002)
	//1	Number of samples per pixel in this image.
	//Enumerated value:  1
	//Photometric Interpretation	(0028,0004)
	//1	Specifies the intended interpretation of the pixel data.
	//Enumerated value:  MONOCHROME2
	//Bits Allocated	(0028,0100)
	//1	Number of bits allocated for each pixel sample.
	//Enumerated values:  8, 16
	//Bits Stored	(0028,0101)
	//1	Number of bits stored for each pixel sample.
	//Enumerated values:  8, 16
	//High Bit	(0028,0102)
	//1	Most significant bit for pixel sample data.
	//Enumerated values:  7, 15
	//Pixel Representation	(0028,0103)
	//1	Data representation of pixel samples.  The constrained enumerated value implies the VR of all Palette Color Lookup Table Descriptors be US (and not SS).
	//Enumerated value:
	//    0000H = unsigned integer
	//See C.8.24.3.1 for specialization.

	// All the above are already inserted in the Image_ImagePixel above

	//Dimension Organization Type	(0020,9311)
	//1	Dimension organization of the instance.
	//Enumerated Values:
	//3D
	//3D_TEMPORAL
	//See C.8.24.3.3 for details.

	// The above already inserted in The dimension sequence module

	//Acquisition Datetime 	(0008,002A)
	//1	The date and time that the acquisition of data that resulted in this image started.
	CreateAndInsertElement(obj, AcquisitionDatetime, "20101118120000");

	//Acquisition Duration	(0018,9073)
	//1	Duration of the image acquisition in ms.
	CreateAndInsertElement(obj, AcquisitionDuration, 10); // 10 ms

	//Pixel Spacing	(0028,0030)
	//1	Physical distance in the patient between the centers of adjacent pixels, specified by a numeric pair - adjacent row spacing (delimiter) adjacent column spacing, in mm.
	//See 10.7.1.3 for further explanation of the value order.
	CreateAndInsertElement(obj, PixelSpacing, "1.5\\1.5"); // 1.5 mm x 1.5 mm pixels

	//Position Measuring Device Used	(0018,980C)
	//1C	Indicates the position measuring device used in the acquisition of the image, which implies the degree of precision of the Pixel Spacing (0028,0030) and Spacing Between Slices (0018,0088) values.  Enumerated Values:
	//RIGID
	//The image was acquired with a position measuring device.
	//FREEHAND
	//The image was acquired without a position measuring device.
	//Required if Volumetric Properties (0008,9206) is VOLUME and Volume Based Calculation Technique (0008,9207) is NONE.  May be present otherwise.
	CreateAndInsertElement(obj, PositionMeasuringDeviceUsed, "FREEHAND");

	//Lossy Image Compression	(0028,2110)
	//1C	Specifies whether an Image has undergone lossy compression.
	//Enumerated Values:
	//00
	//Image has NOT been subjected to lossy compression.
	//01
	//Image has been subjected to lossy compression.
	//See C.7.6.1.1.5
	//Required if Lossy Compression has been performed on the Image.
	//Lossy Image Compression Ratio	(0028,2112)
	//1C	See C.7.6.1.1.5 for further explanation.
	//Required if Lossy Image Compression (0028,2110) equals 01.
	//
	//Lossy Image Compression Method	(0028,2114)
	//1C	A label for the lossy compression method(s) that have been applied to this image.
	//See C.7.6.1.1.5 for further explanation.
	//May be multi valued if successive lossy compression steps have been applied; the value order shall correspond to the values of Lossy Image Compression Ratio (0028,2112).
	//Note:  For historical reasons, the lossy compression method may also be described in Derivation Description (0008,2111).
	//Required if Lossy Image Compression (0028,2110) equals 01.
	//
	//Presentation LUT Shape	(2050,0020)
	//1	Specifies an identity transformation for the Presentation LUT, such that the output of all grayscale transformations defined in the IOD containing this Module are defined to be P-Values.
	//Enumerated Values:
	//IDENTITY - output is in P-Values.
	CreateAndInsertElement(obj, PresentationLUTShape, "IDENTITY");

	//Rescale Intercept	(0028,1052)
	//1	The value b in relationship between stored values (SV) and the output units. 
	//Output units = m*SV + b.
	//Enumerated value 0
	CreateAndInsertElement(obj, RescaleIntercept, 0);

	//Rescale Slope	(0028,1053)
	//1	m in the equation specified by Rescale Intercept (0028,1052).
	//Enumerated value 1
	CreateAndInsertElement(obj, RescaleSlope, 1);

	//Source Image Sequence	(0008,2112)
	//1C	A Sequence that identifies the set of Image SOP Class/Instance pairs of the Images that were used to derive this Image. Zero or more Items may be included in this Sequence.
	//See C.7.6.1.1.4 for further explanation.
	//Required if Image Type (0008,0008) Value 1 is DERIVED.
	//>Include ‘Image SOP Instance Reference Macro’ Table 10-3
	//>Purpose of Reference Code Sequence	(0040,A170)
	//1	Describes the purpose for which the reference is made.
	//Only a single item shall be permitted in this sequence.
	//>>Include ‘Code Sequence Macro’ Table 8.8-1	Defined CID 7202
	//
	//Referenced Image Sequence	(0008,1140)
	//3	A sequence that references other images significantly related to this image. (e.g., an image containing spatially related frames)
	//One or more Items may be included in this sequence.
	//>Include ‘Image SOP Instance Reference Macro’ Table 10-3
	//>Purpose of Reference Code Sequence	(0040,A170)
	//1	Describes the purpose for which the reference is made.
	//Only a single item shall be permitted in this sequence.
	//>>Include ‘Code Sequence Macro’ Table 8.8-1	Defined CID 7201
	//
	//Referenced Raw Data Sequence	(0008,9121)
	//3	A sequence that identifies the set of Raw Data SOP Class/Instance pairs of the raw data that were used to derive this Image.
	//One or more Items may be included in this Sequence.
	//>Include ‘'Hierarchical SOP Instance Reference Macro’ Table C.17-3
	//Referenced Instance Sequence	(0008,114A)
	//1C	A sequence which provides reference to a set of non-image SOP Class/Instance pairs significantly related to this Image, including waveforms that may or may not be temporally synchronized with this image.
	//One or more Items may be included in this sequence.
	//Required if waveforms are acquired in conjunction with image acquisition. May be present otherwise.
	//>Include ‘SOP Instance Reference Macro’ Table 10-11
	//>Purpose of Reference Code Sequence	(0040,A170)
	//1	Code describing the purpose of the reference to the Instance(s). Only a single Item shall be permitted in this sequence.
	//>>Include 'Code Sequence Macro' Table 8.8-1	Defined CID 7004 for referenced waveforms.
	//Number of Stages	(0008,2124)
	//1C	Number of stages in this protocol.
	//Required if Performed Protocol Type (0040,0261) is present with value STAGED.
	//
	//Stage Number	(0008,2122)
	//1C	A number that identifies the Stage. Stage Number starts at one.
	//Required if Performed Protocol Type (0040,0261) is present with value STAGED.
	//
	//Stage Code Sequence	(0040,000A)
	//1C	Sequence describing the performed Ultrasound Protocol Stage.
	//Only a single item shall be present in this sequence.
	//Required if Performed Protocol Type (0040,0261) is present with value STAGED.
	//
	//>Include ‘Code Sequence Macro’ Table 8.8-1	Baseline CID 12002
	//
	//View Code Sequence	(0054,0220)
	//1	Sequence that describes the view of the patient anatomy in this image.
	//Only a single Item shall be permitted in this Sequence.
	//See Section C.8.5.6.1.19.
	//>Include ‘Code Sequence Macro’ Table 8.8-1	See Section C.8.5.6.1.19 for Context Group ID’s
	//>View Modifier Code Sequence	(0054,0222)
	//3	Sequence that provides modifiers for the view of the patient anatomy.
	//One or more Items may be included in this Sequence.
	//See Section C.8.5.6.1.19.
	//>>Include ‘Code Sequence Macro’ Table 8.8-1	See Section C.8.5.6.1.19 for Context Group ID’s
	
	// For now insert empty sequence
	CreateAndInsertElement(obj, ViewCodeSequence); /// \todo fill values

	//Event Timer Sequence	(0008,2133)
	//3	Sequence of time intervals of significance to this image. Each item describes one time interval either beginning or ending at Acquisition Datetime (0008,002A).
	//One or more Items may be included in this Sequence.
	//>Event Time Offset	(0008,2134)
	//1	Signed value of the time between Acquisition Datetime (0008,002A) and the event, in milliseconds.  Positive values indicate the event occurs after Acquisition Datetime (0008,002A).
	//
	//>Event Code Sequence	(0008,2135)
	//1	Type of event.
	//Only a single item shall be present in this sequence.
	//>Include ‘Code Sequence Macro’ Table 8.8-1	Defined CID 12031
	//
	//>Event Timer Name(s)	(0008,2132)
	//3	Name that identifies the event timer.  May be used in addition to Event Time Code Sequence to offer site-specific user-readable event time names.
	//Only a single value shall be permitted.
	//Include 'General Anatomy Mandatory Macro' Table 10-5	No Context IDs are specified.
	//Burned In Annotation	(0028,0301)
	//1	Indicates whether or not image contains sufficient burned in annotation to identify the patient and date the image was acquired.
	//Enumerated Value:
	//NO
	CreateAndInsertElement(obj, BurnedInAnnotation, "NO");

	//Icon Image Sequence	(0088,0200)
	//3	This icon image is representative of the image.
	//>Include ‘Image Pixel Macro’ Table C.7-11b	See C.7.6.1.1.6 for further explanation.
	//Transducer Data	(0018,5010)
	//3	Manufacturer defined code or description

	//Transducer Scan Pattern Code Sequence	(0018,9809)
	//1	The scan pattern the transducer is capable of.
	//Only a single item shall be permitted in this sequence.
	//See C.8.24.3.4 for further explanation.
	//>Include ‘Code Sequence Macro’ Table 8.8-1	Defined CID 12032.
	
	CreateAndInsertElement(obj, TransducerScanPatternCodeSequence); /// \todo insert values

	//
	//Transducer Geometry Code Sequence	(0018,980D)
	//1	Geometric structure of the transducer.
	//Only a single item shall be permitted in this sequence. 
	//See C.8.24.3.4 for further explanation.
	//>Include ‘Code Sequence Macro’ Table 8.8-1	Defined CID 12033.
	//
	CreateAndInsertElement(obj, TransducerGeometryCodeSequence); /// \todo insert values

	//Transducer Beam Steering Code Sequence	(0018,980E)
	//1	Technique used by the transducer for beam steering.
	//One or more items shall be permitted in this sequence.  If more than one item is present, the order is significant from plane-forming technique to volume-forming technique. 
	//See C.8.24.3.4 for further explanation.
	//>Include ‘Code Sequence Macro’ Table 8.8-1	Defined CID 12034.
	//
	CreateAndInsertElement(obj, TransducerBeamSteeringCodeSequence); /// \todo insert values

	//Transducer Application Code Sequence	(0018,980F)
	//1	The primary clinical application of the transducer.
	//Only a single Item shall be permitted in this sequence. 
	//See C.8.24.3.4 for further explanation.
	//>Include ‘Code Sequence Macro’ Table 8.8-1	Defined CID 12035.
	//
	CreateAndInsertElement(obj, TransducerApplicationCodeSequence); /// \todo insert values

	//Preprocessing Function	(0018,5020)
	//3	Manufacturer defined description of processing of echo information. Data may include code or description of gain (initial, overall, TGC, dynamic range, etc.), pre-processing, post-processing, Doppler processing parameters, e.g. cutoff filters, etc., as used in generating a given image.

	//Mechanical Index	(0018,5022)
	//1	See C.8.5.6.1.8 for Description.
	
	//Bone Thermal Index	(0018,5024)
	//1	See C.8.5.6.1.8 for Description.
	
	//Cranial Thermal Index	(0018,5026)
	//1	See C.8.5.6.1.8 for Description.
	
	//Soft Tissue Thermal Index	(0018,5027)
	//1	See C.8.5.6.1.8 for Description.
	
	//Depth(s) of Focus	(0018,9801)
	//1	The depth or depths from the transducer face, of the manufacturer defined beam focus points used for the image, in mm.
	
	//Depth of Scan Field	(0018,5050)
	//1	The depth, in mm, from the transducer face to the deepest point included in the image- the field of view.
	//

}


//Multi-frame Functional Groups		C.7.6.16	M
// Every multi frame image object has a set of mandatory and optional functional groups

/*
	Table C.7.6.16-3
FRAME CONTENT MACRO ATTRIBUTES
	Attribute Name		Tag		Type		Attribute Description
Frame Content Sequence	(0020,9111)
1	Identifies general characteristics of this frame. Only a single Item shall be permitted in this sequence.
>Frame Acquisition Number	(0020,9156)
3	A number identifying the single continuous gathering of data over a period of time that resulted in this frame.
	>Frame Reference DateTime		(0018,9151)
	1C		The point in time that is most representative of when data was acquired for this frame. See C.7.6.16.2.2.1 and C.7.6.16.2.2.2 for further explanation.
Note:	The synchronization of this time with an external clock is specified in the synchronization Module in Acquisition Time synchronized (0018,1800). 
Required if Frame Type (0008,9007) Value 1 of this frame is ORIGINAL. May be present otherwise.
	>Frame Acquisition DateTime		(0018,9074)
	1C	The date and time that the acquisition of data that resulted in this frame started. See C.7.6.16.2.2.1 for further explanation. 
Required if Frame Type (0008,9007) Value 1 of this frame is ORIGINAL. May be present otherwise.
	>Frame Acquisition Duration		(0018,9220)
	1C	The actual amount of time [in milliseconds] that was used to acquire data for this frame. See C.7.6.16.2.2.1 and C.7.6.16.2.2.3 for further explanation.
	Required if Frame Type (0008,9007) Value 1 of this frame is ORIGINAL. May be present otherwise.
>Cardiac Cycle Position	(0018,9236)
3	Description of the position in the cardiac cycle that is most representative of this frame.
Defined Terms:
	END_SYSTOLE
	END_DIASTOLE
	UNDETERMINED
>Respiratory Cycle Position	(0018,9214)
3	Description of the position in the respiratory cycle that is most representative of this frame.
Defined Terms:
	START _RESPIR
	END_RESPIR
	UNDETERMINED
>Dimension Index Values	(0020,9157)
1C	Contains the values of the indices defined in the Dimension Index Sequence (0020,9222) for this multi-frame header frame. The number of values is equal to the number of Items of the Dimension Index Sequence and shall be applied in the same order.
See section C.7.6.17.1 for a description.
Required if the value of the Dimension Index Sequence (0020,9222) exists.

	>Temporal Position Index	(0020,9128)
	1C		Ordinal number (starting from 1) of the frame in the set of frames with different temporal positions.
	Required if the value of SOP Class UID (0008,0016) equals “1.2.840.10008.5.1.4.1.1.130”. May be present otherwise. See C.7.6.16.2.2.6.
>Stack ID	(0020,9056)
1C	Identification of a group of frames, with different positions and/or orientations that belong together, within a dimension organization.
See C.7.6.16.2.2.4 for further explanation. 
Required if the value of SOP Class UID (0008,0016) equals “1.2.840.10008.5.1.4.1.1.130”. May be present otherwise. See C.7.6.16.2.2.7.
>In-Stack Position Number	(0020,9057)
1C	The ordinal number of a frame in a group of frames, with the same Stack ID 
Required if Stack ID (0020,9056) is present.
See section C.7.6.16.2.2.4 for further explanation.
>Frame Comments	(0020,9158)
3	User-defined comments about the frame.
>Frame Label	(0020,9453)
3	Label corresponding to a specific dimension index value. Selected from a set of dimension values defined by the application.
This attribute may be referenced by the Dimension Index Pointer (0020,9165) attribute in the Multi-frame Dimension Module.
See C.7.6.16.2.2.5 for further explanation.
 */


static IDCXOBJPtr FG_FrameContent_Item(int frameAcqNum)
{
	IDCXOBJPtr obj(__uuidof(DCXOBJ));
	CreateAndInsertElement(obj, FrameAcquisitionNumber, frameAcqNum);
	CreateAndInsertElement(obj, FrameReferenceDatetime, "20101118115959");
	CreateAndInsertElement(obj, FrameAcquisitionDatetime, "20101118120000");
	CreateAndInsertElement(obj, FrameAcquisitionDuration, 10);
	CreateAndInsertElement(obj, DimensionIndexValues, /*TODO*/ 0); /// \todo check what should be the value
	/// Temporal position index not required (0020,9128)
	/// Stack ID - not required
	/// In stack pos - not required
	/// Frame Comments - optional
	/// Frame Label - optional
	return obj;
}

static IDCXOBJIteratorPtr FG_FrameContentSequence()
{
	IDCXOBJIteratorPtr it(__uuidof(DCXOBJIterator));
	for (int i = 0; i<NUMBER_OF_FRAMES; i++)
	{
		IDCXOBJPtr obj(FG_FrameContent_Item(i));
		it->Insert(obj);
	}
	return it;
}

// Plane Position Volume - C.7.6.16.2.21 - M
static void FG_PlanePositionVolume(IDCXOBJPtr& obj)
{
	IDCXOBJIteratorPtr it(__uuidof(DCXOBJIterator));
	double imagePos[3] = {.0,.0,.0};

	for (int i = 0; i<NUMBER_OF_FRAMES; i++, imagePos[2]+=1.5)
	{
		IDCXOBJPtr item(__uuidof(DCXOBJ));
		CreateAndInsertElement(item, ImagePositionVolume, imagePos, 3);
		it->Insert(item);
	}
	CreateAndInsertElement(obj, PlanePositionVolumeSequence, it);
}

// Plane Position Orientation - C.7.6.16.2.22 - M
// Shared FG, only one entry with default value "1\0\0\0\1\0"
static void FG_PlaneOrientationVolume(IDCXOBJPtr& obj)
{
	IDCXOBJIteratorPtr it(__uuidof(DCXOBJIterator));
	IDCXOBJPtr item(__uuidof(DCXOBJ));
	CreateAndInsertElement(item, ImageOrientationVolume, "1\0\0\0\1\0");
	it->Insert(item);
	CreateAndInsertElement(obj, PlaneOrientationVolumeSequence, it);
}
static void Image_MultiFrameFunctionalGroups_PerFrame(IDCXOBJPtr& obj)
{
	/// Frame Content 
	CreateAndInsertElement(obj, PerFrameFunctionalGroupsSequence, FG_FrameContentSequence());
	
	/// Plane Position (Volume)
	FG_PlanePositionVolume(obj);
}

// Frame VOI LUT Module C.7.16.2.10
// We can add it for the whole frames
static void FG_FrameVOILUT(IDCXOBJPtr& obj)
{
	IDCXOBJPtr item(__uuidof(DCXOBJ));
	CreateAndInsertElement(item, WindowCenter, "127");
	CreateAndInsertElement(item, WindowWidth, "255");
	IDCXOBJIteratorPtr seq(__uuidof(DCXOBJIterator));
	seq->Insert(item);
	CreateAndInsertElement(obj, FrameVOILUTSequence, seq);
}

// Image Data Type 	C.7.6.16.2.24	M
static void FG_ImageDataType(IDCXOBJPtr& obj)
{
	IDCXOBJPtr item(__uuidof(DCXOBJ));
	CreateAndInsertElement(item, DataType, "TISSUE_INTENSITY");
	CreateAndInsertElement(item, AliasedDataType, "NO");
	IDCXOBJIteratorPtr seq(__uuidof(DCXOBJIterator));
	seq->Insert(item);
	CreateAndInsertElement(obj, ImageDataTypeSequence, seq);
}

// US Image Description	C.8.24.6.1	M - May not be used as a Per-Frame Functional Group
static void FG_USImageDescription(IDCXOBJPtr& obj)
{
	IDCXOBJPtr item(__uuidof(DCXOBJ));
	CreateAndInsertElement(item, FrameType, "ORIGINAL\\PRIMARY"); // C.8.24.3.2	Image Type
	CreateAndInsertElement(item, VolumetricProperties, "VOLUME"); // C.8.16.2.1.2	Volumetric Properties
	CreateAndInsertElement(item, VolumeBasedCalculationTechnique, "NONE"); // C.8.16.2.1.3	Volume Based Calculation Technique Attribute
	IDCXOBJIteratorPtr seq(__uuidof(DCXOBJIterator));
	seq->Insert(item);
	CreateAndInsertElement(obj, USImageDescriptionSequence, seq);
}

static void Image_MultiFrameFunctionalGroups_Shared(IDCXOBJPtr& obj)
{
	// Insert frame voi lut module
	FG_FrameVOILUT(obj);

	// Plane Orientation (Volume)
	FG_PlaneOrientationVolume(obj);

	// Data Type - just one type for this object
	FG_ImageDataType(obj);

	// US Image Description	C.8.24.6.1	M - May not be used as a Per-Frame Functional Group
	FG_USImageDescription(obj);
}


static void Image_MultiFrameFunctionalGroups(IDCXOBJPtr& obj) 
{
/*
	Table C.7.6.16-1
MULTI-FRAME FUNCTIONAL GROUPS MODULE ATTRIBUTES
	Attribute Name		Tag		Type		Attribute Description
	Shared Functional Groups Sequence		(5200,9229)
	2		Sequence that contains the Functional Group Macros that are shared for all frames in this SOP Instance and Concatenation.
Note:	The contents of this sequence are the same in all SOP Instances that comprise a Concatenation.
	Zero or one Item may be included in this sequence.
	See section C.7.6.16.1.1 for further explanation.
	>Include one or more Functional Group Macros that are shared by all frames. The selected Functional Group Macros shall not be present in the Per-frame Functional Groups Sequence (5200,9230).
			See section C.7.6.16.1.1 for further explanation.
	For each IOD that includes this module, a table is defined in which the permitted Functional Group Macros and their usage is specified.
	Per-frame Functional Groups Sequence		(5200,9230)
	1		Sequence that contains the Functional Group Macros corresponding to each frame of the Multi-frame Image. The first Item corresponds with the first frame, and so on.
	Each Item shall contain the same set of Functional Group Macros.
	This Sequence shall contain the same number of Items as the number of frames in the Multi-frame image. See Section C.7.6.16.1.2 for further explanation.
	>Include one or more Functional Group Macros.		For each IOD that includes this module, a table is defined in which the permitted Functional Group Macros and their usage is specified.
Instance Number	(0020,0013)
1	A number that identifies this instance. The value shall be the same for all SOP Instances of a Concatenation, and different for each separate Concatenation and for each SOP Instance not within a Concatenation in a series.
Content Date	(0008,0023)
1	The date the data creation was started.
Note:	For instance, this is the date the pixel data is created, not the date the data is acquired.
Content Time	(0008,0033)
1	The time the data creation was started.
Note:	For instance, this is the time the pixel data is created, not the time the data is acquired.
Number of Frames	(0028,0008)
1	Number of frames in a multi-frame image. See C.7.6.6.1.1 for further explanation.
Concatenation Frame Offset Number	(0020,9228)
1C	Offset of the first frame in a multi-frame image of a concatenation. Logical frame numbers in a concatenation can be used across all its SOP instances. This offset can be applied to the implicit frame number to find the logical frame number in a concatenation. The offset is numbered from zero; i.e., the instance of a concatenation that begins with the first frame of the concatenation has a Concatenation Frame Offset Number (0020,9228) of zero.
Required if Concatenation UID (0020,9161) is present.
Representative Frame Number	(0028,6010)
3	The frame number selected for use as a pictorial representation (e.g. icon) of the multi-frame Image.
	Concatenation UID	(0020,9161)
1C		Identifier of all SOP Instances that belong to the same concatenation.
Required if a group of multi-frame image SOP Instances within a Series are part of a Concatenation.
	SOP Instance UID of Concatenation Source	(0020,0242)
1C		The SOP Instance UID of the single composite SOP Instance of which the Concatenation is a part. All SOP Instances of a concatenation shall use the same value for this attribute, see C.7.6.16.1.3.
Note: 	May be used to reference the entire instance rather than individual instances of the concatenation, which may be transient  (e.g., from a presentation state). 
	Required if Concatenation UID (0020,9161) is present. 
In-concatenation Number	(0020,9162)
1C	Identifier for one SOP Instance belonging to a concatenation. See C.7.6.16.2.2.4 for further specification. The first instance in a concatentation (that with the lowest Concatenation Frame Offset Number (0020,9228) value) shall have an In-concatenation Number (0020,9162) value of 1, and subsequent instances shall have values monotonically increasing by 1.
Required if Concatenation UID (0020,9161) is present. 
In-concatenation Total Number	(0020,9163)
3	The number of SOP Instances sharing the same Concatenation UID.
*/

	// Add Sequence Element: Shared FG (5200,9229)
	Image_MultiFrameFunctionalGroups_Shared(obj);

	// Add Sequence Element: Per-Frame FG (5200,9230)
	Image_MultiFrameFunctionalGroups_PerFrame(obj);
}

// Multi-frame Dimension	C.7.6.17	M
static void Image_MultiFrameDimension(IDCXOBJPtr& obj) 
{
			/*
		Dimension Organization Sequence	(0020,9221)
		1	Sequence that lists the Dimension Organization UIDs referenced by the containing SOP Instance. See section C.7.6.17.2 for further explanation. One or more Items may be included in this Sequence. 
		>Dimension Organization UID	(0020,9164)
		1	Uniquely identifies a set of dimensions referenced within the containing SOP Instance. See section C.7.6.17.2 for further explanation. 
		*/

			IDCXOBJPtr dimOrgSeqItem(__uuidof(DCXOBJ));
			CreateAndInsertElement(dimOrgSeqItem, DimensionOrganizationUID, "999.999.999.999.999.3");
			IDCXOBJIteratorPtr dimOrgSeq(__uuidof(DCXOBJIterator));
			dimOrgSeq->Insert(dimOrgSeqItem);
			CreateAndInsertElement(obj, DimensionOrganizationSequence, dimOrgSeq);

		/*
		Dimension Organization Type	(0020,9311)
		3	Dimension organization of the instance.
		Defined Terms:
		3D
		Spatial Multi-frame image of parallel planes (3D volume set)
		3D_TEMPORAL
		Temporal loop of parallel-plane 3D volume sets.
		*/
			CreateAndInsertElement(obj, DimensionOrganizationType, "3D");
		//Dimension Index Sequence	(0020,9222)
		//1	Identifies the sequence containing the indices used to specify the dimension of the multi-frame object. 
		//One or more Items may be included in this sequence.
			IDCXOBJIteratorPtr dimIndexSeq(__uuidof(DCXOBJIterator));
			IDCXOBJPtr dimIndexSeqItem(__uuidof(DCXOBJ));

		//>Dimension Index Pointer	(0020,9165)
		//1	Contains the Data Element Tag that is used to identify the Attribute connected with the index. See section C.7.6.17.1 for further explanation.
			CreateAndInsertElement(dimIndexSeqItem, DimensionIndexPointer, ImagePositionVolume); // We order by image position volume in 3D Position FG

		//>Dimension Index Private Creator	(0020,9213)
		//1C	Identification of the creator of a group of private data elements.
		//Required if the Dimension Index Pointer (0020,9165) value is the Data Element Tag of a Private Attribute.
		//>Functional Group Pointer	(0020,9167)
		//1C	Contains the Data Element Tag of the Functional Group Sequence that contains the Attribute that is referenced by the Dimension Index Pointer (0020,9165).
		//See section C.7.6.17.1 for further explanation.
		//Required if the value of the Dimension Index Pointer (0020,9165) is the Data Element Tag of an Attribute that is contained within a Functional Group Sequence.
			CreateAndInsertElement(dimIndexSeqItem, FunctionalGroupPointer, PlanePositionVolumeSequence);

		//>Functional Group Private Creator	(0020,9238)
		//1C	Identification of the creator of a group of private data elements.
		//Required if the Functional Group Pointer 0020,9167) value is the Data Element Tag of a Private Attribute.
		//>Dimension Organization UID	(0020,9164)
		//1C	Uniquely identifies a set of dimensions referenced within the containing SOP Instance. In particular the dimension described by this sequence item is associated with this Dimension Organization UID. See section C.7.6.17.2 for further explanation. 
		//Required if the value of the Dimension Organization Sequence (0020,9221) contains Items
			CreateAndInsertElement(dimIndexSeqItem, DimensionOrganizationUID, "999.999.999.999.999.3");
		//>Dimension Description Label	(0020,9421)
		//3	Free text description that explains the meaning of the dimension.
			CreateAndInsertElement(dimIndexSeqItem, DimensionDescriptionLabel, "Sorting by Image Position (Volume) in Plane Position Volume Functional Group");

			dimIndexSeq->Insert(dimIndexSeqItem);
			CreateAndInsertElement(obj, DimensionIndexSequence, dimIndexSeq);
}

// Acquisition Context	C.7.6.14	M
static void Image_AcquisitionContext(IDCXOBJPtr& obj) 
{
	// No additional info - insert empty sequence
	CreateAndInsertElement(obj, AcquisitionContextSequence);
}

// SOP Common	C.12.1	M
static void Image_SOPCommon(IDCXOBJPtr& obj) 
{
	CreateAndInsertElement(obj, sopClassUid, "1.2.840.10008.5.1.4.1.1.6.2");
	CreateAndInsertElement(obj, sopInstanceUID, "999.999.999.999.999.1.1");
}



/// \example CPPDicomObjectExample.cpp
/// This is an example of how to use the IDCXELM interface and the IDCXOBJ interface to
/// load, save, create and modify DICOM Images.
int main(int argc, char* argv[])
{
    /// Initialize the dll (has to be performed in every thread)
    HRESULT hr = ::CoInitialize(NULL); 

    if (SUCCEEDED(hr)) {
    
		IDCXOBJPtr obj(__uuidof(DCXOBJ));
		Patient_Patient(obj);

		Study_GeneralStudy(obj);

		Series_GeneralSeries(obj);
		Series_EnhancedUSSeries(obj);

		FrameOfReference_FrameOfReference(obj);
		FrameOfReference_UltrasoundFrameOfReference(obj);
		FrameOfReference_Synchronization(obj);

		//Equipment_GeneralEquipment(obj);
		Equipment_EnhancedGeneralEquipment(obj);

		Image_GeneralImage(obj);
		Image_ImagePixel(obj);
		Image_MultiFrameFunctionalGroups(obj);
		Image_MultiFrameDimension(obj);
		Image_SOPCommon(obj);
	
		obj->saveFile(L"us3d.dcm");
	}


	::CoUninitialize();

	return 0; 
}

