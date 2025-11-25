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
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using rzdcxLib;

namespace DICOMPresentationModuleExample
{
    /// <summary>
    /// Interface common for all classes which content can be inserted into DICOM file
    /// </summary>
    public interface IDicomizable
    {
        void ToDICOM(DCXOBJ output);
    }

    /// <summary>
    /// Common functions for working with DCX objects
    /// </summary>
    public static class DCXFunctions
    {
        /// <summary>
        /// Insert one DXCELM object into given DCXOBJ
        /// If Value parameter is not empty string - set it as element value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tag"></param>
        /// <param name="Value"></param>
        public static void InsertElement(DCXOBJ obj, DICOM_TAGS_ENUM tag, string Value = "")
        {
            DCXELM el = new DCXELM();

            el.Init((int)tag);

            if (Value != "")
                el.Value = Value;

            obj.insertElement(el);

        }

        /// <summary>
        /// Insert one DXCELM object into given DCXOBJ
        /// If Value parameter is not null - set it as element value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tag"></param>
        /// <param name="Value"></param>
        public static void InsertElement(DCXOBJ obj, DICOM_TAGS_ENUM tag, object Value)
        {
            DCXELM el = new DCXELM();

            el.Init((int)tag);

            if (Value != null)
                el.Value = Value;

            obj.insertElement(el);

        }

        /// <summary>
        /// Retrive from DCXOBJ instance given DICOM tag value as string
        /// If tag doesn't exist - return empty string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ElementTag"></param>
        /// <returns></returns>
        public static string GetElementValueAsString(DCXOBJ obj, int ElementTag)
        {
            DCXELM e = obj.GetElement(ElementTag);
            if (e == null)
            {
                //Tag doesn't exist
                return String.Empty;
            }
            if (e.Value != null)
                return e.Value.ToString();
            else
            {
                return String.Empty;
            }
        }

        public static System.Drawing.PointF GetPointFromValueArray(DCXELM el)
        {
            if (el.ValueRepresentation == VR_CODE.VR_CODE_FL)
            {
                float[] coord1 = el.ValueArray as float[];
                return new System.Drawing.PointF(coord1[0], coord1[1]);
            }

            int[] coord = el.ValueArray as int[];
            return new System.Drawing.PointF(coord[0], coord[1]);
        }

        /// <summary>
        /// Calculate distance betseen two given point ([x,y] pair for each point)
        /// </summary>
        /// <param name="xPos1"></param>
        /// <param name="yPos1"></param>
        /// <param name="xPos2"></param>
        /// <param name="yPos2"></param>
        /// <returns></returns>
        public static double CalculateDistanceBetweenTwoPoints(double xPos1, double yPos1,
                                                               double xPos2, double yPos2)
        {
            if (yPos1 == yPos2)
            {
                return (Math.Abs(xPos1 - xPos2));
            }
            else
            {
                if (xPos1 == xPos2)
                {
                    return (Math.Abs(yPos1 - yPos2));
                }
                else
                {
                    return (Math.Round(Math.Sqrt(Math.Pow(Math.Abs(xPos1 - xPos2), 2) +
                             Math.Pow(Math.Abs(yPos1 - yPos2), 2)), 1));
                }
            }
        }
    }

    /// <summary>
    /// Contains all info requires to read/build DICOM series object:
    /// series UID, series sequence tag and list of all RelationshipSOPInstance belonging to a series
    /// </summary>
    abstract public class RelationshipSeries
    {
        /// <summary>
        /// Info about one DICOM instance of the series
        /// </summary>
        protected class RelationshipSOPInstance
        {
            public string SOPClassUID { set; get; }
            public string SOPInstanceUID { set; get; }
            public string ReferencedFrameNumber { set; get; }

            /// <summary>
            /// Constructor that reads one instance from DICOM file
            /// Input DCXOBJ is one object extracted from series iterator
            /// </summary>
            /// <param name="obj"></param>
            public RelationshipSOPInstance(DCXOBJ obj)
            {
                this.SOPClassUID = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.ReferencedSOPClassUID);
                this.SOPInstanceUID = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.ReferencedSOPInstanceUID);
                this.ReferencedFrameNumber = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.ReferencedFrameNumber);
            }

            /// <summary>
            /// Constructor that creates one instance for storing in DICOM file
            /// Instance will not contain ReferencedFrameNumber tag 
            /// (for example one MPPS file instance or GSPS module referenced instance)
            /// </summary>
            /// <param name="pSOPInstanceUID"></param>
            /// <param name="pSOPClassUID"></param>
            public RelationshipSOPInstance(string pSOPInstanceUID, string pSOPClassUID)
            {
                this.SOPClassUID = pSOPClassUID;
                this.SOPInstanceUID = pSOPInstanceUID;
            }

            /// <summary>
            /// Constructor that creates one instance for storing in DICOM file
            /// Instance will contain ReferencedFrameNumber tag 
            /// (for example referenced instance in graphic annotation sequence)
            /// </summary>
            /// <param name="pSOPInstanceUID"></param>
            /// <param name="pSOPClassUID"></param>
            /// <param name="FrameNo"></param>
            public RelationshipSOPInstance(string pSOPInstanceUID, string pSOPClassUID, string FrameNo)
            {
                this.SOPClassUID = pSOPClassUID;
                this.SOPInstanceUID = pSOPInstanceUID;
                this.ReferencedFrameNumber = FrameNo;
            }

            /// <summary>
            /// Create DICOM object containing instance info to insert into series iterator
            /// </summary>
            /// <returns></returns>
            public DCXOBJ GetMyDICOM()
            {
                DCXOBJ o = new DCXOBJ();
                DCXELM elToIns = new DCXELM();

                DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.ReferencedSOPClassUID, this.SOPClassUID);
                DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.ReferencedSOPInstanceUID, this.SOPInstanceUID);

                if (this.ReferencedFrameNumber != null && this.ReferencedFrameNumber != "")
                    DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.ReferencedFrameNumber, this.ReferencedFrameNumber);

                return o;
            }
        }

        public string SeriesInstanceUID { set; get; }

        /// <summary>
        /// DICOM tag to use on creating series element in DICOM file
        /// and on reading series from DICOM file
        /// </summary>
        protected DICOM_TAGS_ENUM _MySequenceTag;

        /// <summary>
        /// Can be used for setting exact tag of the series sequence after series creation
        /// (for example various types of MPPS series)
        /// </summary>
        public DICOM_TAGS_ENUM MySequenceTag
        {
            set { _MySequenceTag = value; }
        }

        /// <summary>
        /// List of all SOP instances belonging to a series
        /// </summary>
        protected List<RelationshipSOPInstance> _MyInstances = new List<RelationshipSOPInstance>();

        /// <summary>
        /// Consructor which read all data from series object
        /// loaded from DICOM file
        /// </summary>
        /// <param name="obj"></param>
        public RelationshipSeries(DCXOBJ obj, DICOM_TAGS_ENUM serTag)
        {
            this._MySequenceTag = serTag;
            this.SeriesInstanceUID = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.seriesInstanceUID);

            DCXELM el = obj.GetElement((int)serTag);
            if (el != null)
            {
                this.LoadDICOM(el);
            }
        }

        /// <summary>
        /// Constructor that creates series for storing in DICOM file
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        public RelationshipSeries(string pSeriesInstanceUID)
        {
            SeriesInstanceUID = pSeriesInstanceUID;
        }

        /// <summary>
        /// Search for SOP instance in the inner list by SOP Instance UID
        /// If instance found - return true
        /// If not found - return false
        /// </summary>
        /// <param name="pSOPInstanceUID"></param>
        /// <returns></returns>
        public bool IsMyInstance(string pSOPInstanceUID)
        {
            foreach (RelationshipSOPInstance inst in this._MyInstances)
            {
                if (inst.SOPInstanceUID == pSOPInstanceUID)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Read SOP instances from DCXELM object containg series iterator
        /// </summary>
        /// <param name="elReg"></param>
        public void LoadDICOM(DCXELM elReg)
        {
            DCXOBJIterator objIt = elReg.Value as DCXOBJIterator;
            if (objIt == null)
                throw new ApplicationException("Invalid or empty series sequence " + _MySequenceTag.ToString());

            DCXOBJ Obj = null;

            for (; !objIt.AtEnd(); objIt.Next())
            {
                Obj = objIt.Get();
                this._MyInstances.Add(new RelationshipSOPInstance(Obj));
            }
            if (Obj != null)
                Marshal.ReleaseComObject(Obj);
            Marshal.ReleaseComObject(objIt);
        }

        /// <summary>
        /// Insert one instance without ReferencedFrameNumber into inner list
        /// </summary>
        /// <param name="pSOPClassUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        public void AddInstance(string pSOPInstanceUID, string pSOPClassUID)
        {
            if (!this.IsMyInstance(pSOPInstanceUID))
            {
                //Don't add twice
                _MyInstances.Add(new RelationshipSOPInstance(pSOPInstanceUID, pSOPClassUID));
            }
        }

        /// <summary>
        /// Insert one instance with ReferencedFrameNumber into inner list
        /// </summary>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddInstance(string pSOPInstanceUID, string pSOPClassUID, string FrameNo)
        {
            _MyInstances.Add(new RelationshipSOPInstance(pSOPInstanceUID, pSOPClassUID, FrameNo));
        }

        /// <summary>
        /// Build DCXOBJ instance containing series info (series UID and list of SOP instances)
        /// </summary>
        /// <returns></returns>
        public DCXOBJ BuildMySeries()
        {
            DCXOBJ series_item = new DCXOBJ();

            DCXFunctions.InsertElement(series_item, DICOM_TAGS_ENUM.seriesInstanceUID, this.SeriesInstanceUID);

            this.InsertAdditionalSeriesLevelTags(series_item);

            //Insert instances UIDs
            DCXOBJIterator files = new DCXOBJIterator();

            foreach (RelationshipSOPInstance inst in this._MyInstances)
            {
                files.Insert(inst.GetMyDICOM());
            }

            DCXELM e = new DCXELM();
            e.Init((int)this._MySequenceTag);
            e.Value = files;
            series_item.insertElement(e);

            return series_item;
        }

        /// <summary>
        /// Add some specific tag on writing series into DICOM object (MPPS tags for example)
        /// To be overriden in the inherited classes
        /// </summary>
        /// <param name="series_item"></param>
        abstract protected void InsertAdditionalSeriesLevelTags(DCXOBJ series_item);
    }

    /// <summary>
    /// Info about DICOM relationship module 
    /// (for example list of series for MPPS "study completed" command or
    /// list of referenced series in GSPS module)
    /// Contains main sequence tag and list of RelationshipSeries instances
    /// </summary>
    abstract public class RelationshipModule : IDicomizable
    {
        /// <summary>
        /// DICOM tag to use on creating relationship module sequence element in DICOM file
        /// and on reading relationship module sequence from DICOM file
        /// Defines main relationship sequence
        /// </summary>
        protected DICOM_TAGS_ENUM _serSeqTag;

        //Sequence tag of one series can not be define on this level
        //because module might contain series of various types
        //(for example MPPS relationship module can contain series of type ReferencedImageSequence
        //and of type ReferencedNonImageCompositeSOPInstanceSequence)

        /// <summary>
        /// List of all series belonging to a relationship sequence
        /// </summary>
        private List<RelationshipSeries> _MySeries = new List<RelationshipSeries>();

        protected RelationshipModule(DICOM_TAGS_ENUM seriesSeqTag)
        {
            _serSeqTag = seriesSeqTag;
        }

        /// <summary>
        /// Search SOP instance in all series by series UID and SOP instance UID
        /// If instance found - return true
        /// If not found - return false
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <returns></returns>
        public bool IfInstanceBelongsToMe(string pSeriesInstanceUID, string pSOPInstanceUID)
        {
            RelationshipSeries ser = this.FindSeries(pSeriesInstanceUID);
            if (ser != null)
            {
                return (ser.IsMyInstance(pSOPInstanceUID));
            }

            return false;
        }

        /// <summary>
        /// Read relationship sequence from DCXOBJ containing all tags of input DICOM file
        /// </summary>
        /// <param name="input"></param>
        public void FromDICOM(DCXOBJ input)
        {
            DCXELM el = input.GetElement((int)_serSeqTag);

            ReadSequence(el);
        }

        /// <summary>
        /// Write into given DCXOBJ instance all info about relationship module:
        /// all DICOM tags required by specific type (MPPS, GSPS module etc.),
        /// relationship module sequence tag and list of the series
        /// </summary>
        /// <param name="output"></param>
        public void ToDICOM(DCXOBJ output)
        {
            this.InsertAdditionalModuleLevelTags(output);

            DCXOBJIterator series_sq = new DCXOBJIterator();

            foreach (RelationshipSeries si in this._MySeries)
            {
                series_sq.Insert(si.BuildMySeries());
            }

            DCXELM e = new DCXELM();
            e.Init((int)this._serSeqTag);
            e.Value = series_sq;
            output.insertElement(e);
        }

        /// <summary>
        /// Open given DICOM file, extract required tags and insert new instance into corresponding series
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RelationshipSeries AddFile(string path)
        {
            //Extract data from the file
            DCXOBJ obj = new DCXOBJ();
            obj.openFile(path);
            string pSeriesInstanceUID = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.seriesInstanceUID);
            string pSOPInstanceUID = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.sopInstanceUID);
            string pSOPClassUID = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.sopClassUid);

            return this.AddInstance(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);
        }

        /// <summary>
        /// Find or create series by given SeriesInstanceUID and
        /// insert new instance into this series
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <returns></returns>
        public RelationshipSeries AddInstance(string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID)
        {
            bool NewSeries = false;
            RelationshipSeries ser = this.FindSeries(pSeriesInstanceUID);

            if (ser == null)
            {
                ser = this.GetNewSeries(pSeriesInstanceUID);
                this._MySeries.Add(ser);
                NewSeries = true;
            }

            ser.AddInstance(pSOPInstanceUID, pSOPClassUID);
            return (NewSeries ? ser : null);
        }

        /// <summary>
        /// Add some specific tag on writing main sequence into DICOM object (MPPS tags for example)
        /// To be overriden in the inherited classes
        /// </summary>
        /// <param name="output"></param>
        abstract protected void InsertAdditionalModuleLevelTags(DCXOBJ output);

        /// <summary>
        /// Create specific relationship series in the inherited classes (for example MPPS or GSPS module)
        /// for reading its SOP Instances from the given DCXOBJ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        abstract protected RelationshipSeries GetNewSeries(DCXOBJ obj);

        /// <summary>
        /// Create specific relationship series in the inherited classes (for example MPPS or GSPS module)
        /// and set its series instance UID (for further writing into DICOM file) 
        /// </summary>
        /// <param name="SeriesInstanceUID"></param>
        /// <returns></returns>
        abstract protected RelationshipSeries GetNewSeries(string SeriesInstanceUID);

        /// <summary>
        /// Try to find series in the inner list by given SeriesInstanceUID
        /// Return null if not found
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <returns></returns>
        private RelationshipSeries FindSeries(string pSeriesInstanceUID)
        {
            foreach (RelationshipSeries ser in this._MySeries)
            {
                if (ser.SeriesInstanceUID == pSeriesInstanceUID)
                    return ser;
            }
            return null;
        }

        /// <summary>
        /// Extract list of series from DCXELM containig relationship module sequence iterator
        /// </summary>
        /// <param name="elReg"></param>
        private void ReadSequence(DCXELM elReg)
        {
            DCXOBJIterator objIt = elReg.Value as DCXOBJIterator;
            if (objIt == null)
                throw new ApplicationException("Invalid relationship module sequence value of tag " + _serSeqTag.ToString());

            DCXOBJ Obj = null;

            for (; !objIt.AtEnd(); objIt.Next())
            {
                Obj = objIt.Get();

                //Create new series (specific type by calling children GetNewSeries method
                RelationshipSeries ser = this.GetNewSeries(Obj);
                this._MySeries.Add(ser);
            }
            if (Obj != null)
                Marshal.ReleaseComObject(Obj);
            Marshal.ReleaseComObject(objIt);
        }
    }
}
