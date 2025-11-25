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
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using rzdcxLib;

namespace DCXACCEventsExample
{
    class RzdcxEventsSink : IDCXACCEvents
    {
        public RzdcxEventsSink()
        {
        }
        //[DispId(7)]
        void IDCXACCEvents.OnAssocEnd()
        {

        }
        //[DispId(1)]
        void IDCXACCEvents.OnCommitResult(bool status, string transaction_uid, string succeeded_instances, string failed_instances)
        {

        }
        //[DispId(2)]
        void IDCXACCEvents.OnConnection(string calling_ae_title, string called_ae_title, string calling_host, ref bool accept_connection)
        {

        }
        //[DispId(8)]
        void IDCXACCEvents.OnEcho()
        {

        }
        //[DispId(9)]
        DCXOBJIterator IDCXACCEvents.OnFind(DCXOBJ identifier)
        {
            DCXOBJIterator result = new DCXOBJIterator();
            DCXELM e = new DCXELM();
            e.Init((int)DICOM_TAGS_ENUM.PatientsName);
            e.Value = "P1";
            identifier.insertElement(e);
            result.Insert(identifier);
            e.Value = "P2";
            identifier.insertElement(e);
            result.Insert(identifier);
            return result;
        }
        //[DispId(6)]
        void IDCXACCEvents.OnStorageCommitRequest(string transaction_uid, string instances, ref bool accept_storage_commit)
        {

        }
        //[DispId(4)]
        void IDCXACCEvents.OnStoreDone(string filename, bool storage_status, ref bool accept_storage)
        {

        }
        //[DispId(3)]
        void IDCXACCEvents.OnStoreSetup(ref string filename)
        {

        }
        //[DispId(5)]
        void IDCXACCEvents.OnTimeout()
        {

        }
        //[DispId(10)]
        void IDCXACCEvents.OnMPPSCreate(string affected_sop_instance_uid, rzdcxLib.DCXOBJ dataset) 
        { 
        }
        //[DispId(11)]
        void IDCXACCEvents.OnMPPSSet(string affected_sop_instance_uid, rzdcxLib.DCXOBJ dataset)
        {

        }

     }
}
