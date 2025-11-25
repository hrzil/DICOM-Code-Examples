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

using System;
using System.Collections.Generic;
using System.Text;
using rzdcxLib;
using System.Runtime.InteropServices;

namespace DicomObjectExample
{
    /// <summary>
    /// \example SequenceExample.cs
    /// This Example show how to create a sequence tag and add it to a DICOM Object.
    /// The example creates a referenced SOP Instances Sequence, that is used in many
    /// DICOM services such as Storage Commitment and others.
    /// </summary>
    class SequenceExample
    {
        DCXOBJ CreateReferencedImageSequence()
        {
            ///  First create an object iterator to hold the sequence items
            DCXOBJIterator it = new DCXOBJIterator();
            
            /// Create an element
            DCXELM e = new DCXELM();
     
            /// Then Create an object for the first item.
            DCXOBJ currObj = new DCXOBJ();

            for (int i = 1; i<=10; i++)
            {
                /// Set the SOP Class UID, for example CT
                e.Init(0x00081150);
                e.Value = "1.2.840.10008.5.1.4.1.1.2";
                currObj.insertElement(e);

                /// Set the SOP Instance UID
                e.Init(0x00081155);
                e.Value = "1.2.3.4.5.6." + i.ToString();
                currObj.insertElement(e);

                /// Add the item to the sequence
                it.Insert(currObj);
            }

            /// Assign the sequence to a Referenced Image Sequence Element
            e.Init(0x00081199);
            e.Value = it;
            DCXOBJ theObj = new DCXOBJ();
            theObj.insertElement(e);

            Marshal.ReleaseComObject(it);
            Marshal.ReleaseComObject(e);
            Marshal.ReleaseComObject(currObj);

            return theObj;
        }
    }
}
