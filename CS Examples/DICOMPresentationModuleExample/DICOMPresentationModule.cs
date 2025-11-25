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
using System.Drawing;
using rzdcxLib;

namespace DICOMPresentationModuleExample
{
    /// <summary>
    /// Contains all info requires to read/build DICOM Referenced Image Sequence object:
    /// series UID, sequence tag and list of all RelationshipSOPInstance belonging to an image sequence
    /// inside GSPS module
    /// </summary>
    public class ReferencedImagesSeries : RelationshipSeries
    {
        /// <summary>
        /// Constructor that creates series for storing in DICOM file
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        public ReferencedImagesSeries(string pSeriesInstanceUID)
            : base(pSeriesInstanceUID)
        {
            this._MySequenceTag = DICOM_TAGS_ENUM.ReferencedImageSequence;
        }

        /// <summary>
        /// Consructor which read all data from ReferencedImageSequence object
        /// loaded from DICOM file
        /// </summary>
        /// <param name="obj"></param>
        public ReferencedImagesSeries(DCXOBJ obj) : base (obj, DICOM_TAGS_ENUM.ReferencedImageSequence)
        {
        }

        /// <summary>
        /// Add some specific tag on writing series into DICOM object (MPPS tags for example)
        /// To be overriden in the inherited classes
        /// </summary>
        /// <param name="series_item"></param>
        protected override void InsertAdditionalSeriesLevelTags(DCXOBJ series_item)
        {
        }

        /// <summary>
        /// Check all SOP instances belonging to sequence for matching SOP instance UID
        /// and referenced frame (if defined)
        /// </summary>
        /// <param name="SOPInstanceUID"></param>
        /// <param name="FrameNum"></param>
        /// <returns></returns>
        public bool IsMySOPAndFrame(string SOPInstanceUID, int FrameNum)
        {
            bool Valid = false;
            foreach (RelationshipSOPInstance inst in this._MyInstances)
            {
                if (inst.SOPInstanceUID != SOPInstanceUID)
                {
                    continue;
                }
                if (inst.ReferencedFrameNumber != null && inst.ReferencedFrameNumber != "")
                {
                    if (inst.ReferencedFrameNumber == FrameNum.ToString())
                    {
                        Valid = true;
                        break;
                    }
                }
                else
                {
                    Valid = true;
                    break;
                }
            }
            return Valid;
        }
    }

    /// <summary>
    /// Info about DICOM Referenced Image Sequence belonging to GSPS module
    /// </summary>
    public class ReferencedSeriesModule : RelationshipModule
    {
        /// <summary>
        /// Constructor for reading/writing info about DICOM Referenced Image Sequence
        /// </summary>
        public ReferencedSeriesModule() 
            : base(DICOM_TAGS_ENUM.ReferencedSeriesSequence)
        {
        }

        protected override void InsertAdditionalModuleLevelTags(DCXOBJ output)
        { 
        }

        /// <summary>
        /// Create new Referenced Image Sequence for writing into GSPS DICOM file
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <returns></returns>
        protected override RelationshipSeries GetNewSeries(string pSeriesInstanceUID)
        {
            return new ReferencedImagesSeries(pSeriesInstanceUID);
        }

        /// <summary>
        /// Create new Referenced Image Sequence for reading from GSPS DICOM file
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <returns></returns>
        protected override RelationshipSeries GetNewSeries(DCXOBJ obj)
        {
            return new ReferencedImagesSeries(obj);
        }
    }

    /// <summary>
    /// Graphic presentation of GSPS displayd area (all values in pixels)
    /// For drawing annotations on DICOM image
    /// </summary>
    public class DisplayedAreaDrawingInfo
    {
        /// <summary>
        /// Left top point of displayed area inside DICOM image
        /// </summary>
        public PointF StartPoint;
        
        /// <summary>
        /// Width of displayed area in pixels
        /// </summary>
        public float Width;

        /// <summary>
        /// Height of displayed area in pixels
        /// </summary>
        public float Height;
    }

    /// <summary>
    /// Class containing all onfo about GSPS displayed area from DICOM file
    /// (on which part of the image to draw annotations)
    /// </summary>
    public class DisplayedArea
    {
        /// <summary>
        /// The subset of images and frames listed in the Presentation State Relationship Module, to which this displayed area selection applies.
        /// One or more Items shall be included in this Sequence.
        /// Required if the displayed area selection in this Item does not apply to all the images and frames listed in the Presentation State Relationship Module.
        /// </summary>
        private ReferencedImagesSeries _ReferencedImages = null;

        public ReferencedImagesSeries ReferencedImages
        {
            get { return _ReferencedImages; }
            set { _ReferencedImages = value; }
        }

        /// <summary>
        /// Manner of selection of display size.
        /// If Presentation Size Mode is specified as SCALE TO FIT, then the specified area shall be displayed as large as possible within the available area on the display or window, i.e., magnified or minified if necessary to fit the display or window space available.
        /// If it's specified as TRUE SIZE, then the physical size of the rendered image pixels shall be the same on the screen as specified in Presentation Pixel Spacing (0070,0101).
        /// If it's specified as MAGNIFY, then the factor that shall be used to spatially interpolate image pixels to create pixels on the display is defined.
        /// </summary>
        private string _PresentationSizeMode;

        /// <summary>
        /// The top left (after spatial transformation) pixel in the referenced image to be displayed,given as column\row. 
        /// Column is the horizontal (before spatial transformation) offset(X) and row is the vertical (before spatial transformation) offset (Y) 
        /// relative to the origin of the pixel data before spatial transformation, which is 1\1.
        /// </summary>
        private PointF _DisplayedAreaTopLeftHandCorner;

        /// <summary>
        /// The bottom right (after spatial transformation) pixel in the referenced image to bedisplayed, given as column\row.
        /// Column is the horizontal (before spatialt ransformation) offset (X) and row is the vertical (before spatial transformation) offset(Y) 
        /// relative to the origin of the pixel data before spatial transformation, which is 1\1.
        /// </summary>
        private PointF _DisplayedAreaBottomRightHandCorner;

        /// <summary>
        /// Ratio of the vertical size and the horizontal size of the pixels in the referenced image,
        /// to be used to display the referenced image, specified by a pair of integer values
        /// where the first value is the vertical pixel size and the second value is the horizontal pixel size.
        /// </summary>
        private string _PresentationPixelAspectRatio;


        /// <summary>
        /// Constructor for reading info about displayed area from GSPS DICOM file
        /// </summary>
        /// <param name="obj"></param>
        public DisplayedArea(DCXOBJ Obj)
        {
            //DCXOBJIterator objIt = obj.Value as DCXOBJIterator;

            //DCXOBJ Obj = objIt.Get();
            this._PresentationPixelAspectRatio = DCXFunctions.GetElementValueAsString(Obj, (int)DICOM_TAGS_ENUM.PresentationPixelAspectRatio);
            this._PresentationSizeMode = DCXFunctions.GetElementValueAsString(Obj, (int)DICOM_TAGS_ENUM.PresentationSizeMode);

            DCXELM el = Obj.GetElement((int)DICOM_TAGS_ENUM.DisplayedAreaTopLeftHandCorner);
            if (el != null)
                this._DisplayedAreaTopLeftHandCorner = DCXFunctions.GetPointFromValueArray(el);

            el = Obj.GetElement((int)DICOM_TAGS_ENUM.DisplayedAreaBottomRightHandCorner);
            if (el != null)
                this._DisplayedAreaBottomRightHandCorner = DCXFunctions.GetPointFromValueArray(el);

            el = Obj.GetElement((int)DICOM_TAGS_ENUM.ReferencedImageSequence);
            if (el != null)
            {
                //Add referenced images
                this._ReferencedImages = new ReferencedImagesSeries(Obj);
            }

            if (Obj != null)
                Marshal.ReleaseComObject(Obj);
            //Marshal.ReleaseComObject(objIt);
        }

        /// <summary>
        /// Constructor for writing info about displayed area into GSPS DICOM file
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public DisplayedArea(PointF TopLeftCorner, PointF BottomRightCorner)
        {
            this._DisplayedAreaTopLeftHandCorner = TopLeftCorner;
            this._DisplayedAreaBottomRightHandCorner = BottomRightCorner;

            //???
            this._PresentationPixelAspectRatio = "1/1";
            this._PresentationSizeMode = "SCALE TO FIT";
        }

        /// <summary>
        /// Calculate values required for drawing annotations on the image
        /// </summary>
        /// <returns></returns>
        public DisplayedAreaDrawingInfo GetMyDrawingInfo()
        {
            DisplayedAreaDrawingInfo res = new DisplayedAreaDrawingInfo();
            res.StartPoint = this._DisplayedAreaTopLeftHandCorner;
            res.Width = this._DisplayedAreaBottomRightHandCorner.X - this._DisplayedAreaTopLeftHandCorner.X;
            res.Height = this._DisplayedAreaBottomRightHandCorner.Y - this._DisplayedAreaTopLeftHandCorner.Y;
            return res;
        }

        /// <summary>
        /// Build DCXOBJ object containing info about displayed area
        /// </summary>
        /// <returns></returns>
        public DCXOBJ GetMyDICOM()
        {
            DCXOBJ o = new DCXOBJ();
            DCXELM elToIns = new DCXELM();

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.PresentationPixelAspectRatio, this._PresentationPixelAspectRatio);
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.PresentationSizeMode, this._PresentationSizeMode);

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.DisplayedAreaTopLeftHandCorner, BasicAnnotation.GetStringForValueArray(new PointF[] { this._DisplayedAreaTopLeftHandCorner }));
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.DisplayedAreaBottomRightHandCorner, BasicAnnotation.GetStringForValueArray(new PointF[] { this._DisplayedAreaBottomRightHandCorner }));

            return o;
        }
    }

    /// <summary>
    /// This Module defines the characteristics of the layers in which overlays, graphic and text may be rendered.
    /// Layers group together graphics that are related.
    /// It is recommended that a layer be displayed such that it may be distinguished from
    /// other layers that have a different value for Graphic Layer Order (0070,0062).
    /// Displaying by layers is not implemented yet
    /// </summary>
    public class GraphicLayerModule
    {
        /// <summary>
        /// A string that identifies the layer.
        /// This identifier may be used by other Attributes within the same presentation state instance to reference this layer. 
        /// There is no requirement for the same identifiers to be used in different presentation states, 
        /// and there is no mechanism for referencing layers in other presentation states. That is, a UID is not required.
        /// </summary>
        private string _GraphicLayer = "LAYER 1";

        /// <summary>
        /// An integer indicating the order in which it is recommended that the layer be rendered, 
        /// if the display is capable of distinguishing. Lower numbered layers are to be rendered first.
        /// </summary>
        private int _GraphicLayerOrder = 1;

        /// <summary>
        /// A default single gray unsigned value in which it is recommended that the layer be rendered on a monochrome display. 
        /// The units are specified in P-Values from a minimum of 0000H (black) up to a maximum of FFFFH(white). 
        /// The maximum P-Value for this Attribute may be different from the maximum P-Value from the output of the Presentation LUT, 
        /// which may be less than 16 bits in depth.
        /// </summary>
        private uint _GraphicLayerRecommendedDisplayGrayscaleValue = 65535;

        /// <summary>
        /// A free text description of the contents of this layer.
        /// </summary>
        private string _GraphicLayerDescription = "For annotations";

        /// <summary>
        /// Constructor for writing default graphic layer into DICOM file
        /// </summary>
        public GraphicLayerModule()
        {
        }

        /// <summary>
        /// Constructor for reading info about Graphic Layer Module from GSPS DICOM file
        /// </summary>
        /// <param name="obj"></param>
        public GraphicLayerModule(DCXOBJ Obj)
        {
            this._GraphicLayer = DCXFunctions.GetElementValueAsString(Obj, (int)DICOM_TAGS_ENUM.GraphicLayer);
            this._GraphicLayerDescription = DCXFunctions.GetElementValueAsString(Obj, (int)DICOM_TAGS_ENUM.GraphicLayerDescription);

            string elVal = DCXFunctions.GetElementValueAsString(Obj, (int)DICOM_TAGS_ENUM.GraphicLayerOrder);
            Int32.TryParse(elVal, out this._GraphicLayerOrder);

            elVal = DCXFunctions.GetElementValueAsString(Obj, (int)DICOM_TAGS_ENUM.GraphicLayerRecommendedDisplayGrayscaleValue);
            UInt32.TryParse(elVal, out this._GraphicLayerRecommendedDisplayGrayscaleValue);
        }

        /// <summary>
        /// Build DCXOBJ object containing info about Graphic Layer Module
        /// </summary>
        /// <returns></returns>
        public DCXOBJ GetMyDICOM()
        {
            DCXOBJ o = new DCXOBJ();
            DCXELM elToIns = new DCXELM();

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicLayer, this._GraphicLayer);
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicLayerDescription, this._GraphicLayerDescription);

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicLayerOrder, this._GraphicLayerOrder.ToString());
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicLayerRecommendedDisplayGrayscaleValue, this._GraphicLayerRecommendedDisplayGrayscaleValue.ToString());

            return o;
        }
    }

    /// <summary>
    /// Class containing all data of one GraphicAnnotationSequence item:
    /// 1. Referenced Image Sequence (0008,1140) (optional)
    /// Required if graphic annotations in this Item do not apply to all the images and frames listed in the Presentation State Relationship Module.
    /// 2. Text Object Sequence (0070,0008) (optional)
    /// 3. Graphic ObjectSequence (0070,0009 (optional)
    /// Either one or both of Text Object Sequence (0070,0008) or Graphic ObjectSequence (0070,0009) are required.
    /// </summary>
    public class AnnotationSet
    {
        private string _GraphicLayer = "LAYER 1";

        public string GraphicLayer
        {
            get { return _GraphicLayer; }
            set { _GraphicLayer = value; }
        }

        /// <summary>
        /// The subset of images and frames listed in the Presentation State Relationship Module, 
        /// to which this graphic annotation applies.
        /// Required if graphic annotations in this Item do not apply to all the imagesand frames listed in the Presentation State Relationship Module.
        /// </summary>
        private ReferencedImagesSeries _ReferencedImages = null;

        public ReferencedImagesSeries ReferencedImages
        {
            get { return _ReferencedImages; }
            set { _ReferencedImages = value; }
        }

        /// <summary>
        /// A sequence of Items each of which represents a group of annotations composed of graphics
        /// </summary>
        private List<GraphicObjectAnnotation> _GraphicObjectAnnotations = null;

        public List<GraphicObjectAnnotation> GraphicObjectAnnotations
        {
            get { return _GraphicObjectAnnotations; }
            set { _GraphicObjectAnnotations = value; }
        }

        /// <summary>
        /// A sequence of Items each of which represents a group of annotations composed of text
        /// </summary>
        private List<TextAnnotation> _TextAnnotations = null;

        public List<TextAnnotation> TextAnnotations
        {
            get { return _TextAnnotations; }
            set { _TextAnnotations = value; }
        }
        
        /// <summary>
        /// Create one GraphicAnnotationSequence object to insert into output DICOM file
        /// </summary>
        /// <param name="objects_sq"></param>
        public void InsertMyObjectsToDICOM(DCXOBJIterator objects_sq)
        {
            DCXOBJ seq_item = new DCXOBJ();
            DCXOBJIterator items = new DCXOBJIterator();

            //Insert Referenced Image Sequence
            if (this._ReferencedImages != null)
            {
                seq_item = this._ReferencedImages.BuildMySeries();
            }

            //Insert graphic layer
            DCXFunctions.InsertElement(seq_item, DICOM_TAGS_ENUM.GraphicLayer, this._GraphicLayer);

            //Insert text annotations
            if (this._TextAnnotations != null && this._TextAnnotations.Count > 0)
                this.InsertSequenceToDICOM(seq_item, DICOM_TAGS_ENUM.TextObjectSequence);

            //Insert graphic annotations
            if (this._GraphicObjectAnnotations != null && this._GraphicObjectAnnotations.Count > 0)
                this.InsertSequenceToDICOM(seq_item, DICOM_TAGS_ENUM.GraphicObjectSequence);


            objects_sq.Insert(seq_item);
        }

        /// <summary>
        /// Draw all graphics and text annotations on the given bitmap inside
        /// boundaries defined by displayed area
        /// If Referenced Image Sequences is not empty - check that annotations
        /// must applied on given SOP instance and frame 
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="SOPInstanceUID"></param>
        /// <param name="FrameNum"></param>
        public void DrawOnBitmap(Bitmap bm, DisplayedArea da, string SOPInstanceUID, int FrameNum)
        {
            bool Valid = true;

            //Check inner referenced list if exists
            if (this._ReferencedImages != null)
            {
                Valid = this._ReferencedImages.IsMySOPAndFrame(SOPInstanceUID, FrameNum);
            }

            if (!Valid)
                return;

            using (Graphics g = Graphics.FromImage(bm))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                if (this._TextAnnotations != null)
                {
                    foreach (TextAnnotation ta in this._TextAnnotations)
                    {
                        ta.DrawMyText(g, da);
                    }
                }

                if (this._GraphicObjectAnnotations != null)
                {
                    foreach (GraphicObjectAnnotation ga in this._GraphicObjectAnnotations)
                    {
                        ga.DrawMyGraphic(g, da);
                    }
                }
            }
        }

        /// <summary>
        /// Read one Graphic Object Annotation from the given DCXOBJ and add it to the inner list
        /// </summary>
        /// <param name="obj"></param>
        public void ReadGraphicAnnotation(DCXOBJ obj)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();
            this._GraphicObjectAnnotations.Add(new GraphicObjectAnnotation(obj));
        }

        /// <summary>
        /// Read one Text Annotation from the given DCXOBJ and add it to the inner list
        /// </summary>
        /// <param name="obj"></param>
        public void ReadTextAnnotation(DCXOBJ obj)
        {
            if (this._TextAnnotations == null)
                this._TextAnnotations = new List<TextAnnotation>();
            this._TextAnnotations.Add(new TextAnnotation(obj));
        }

        /// <summary>
        /// Insert corresponding DCXOBJ objects into given DCXOBJ
        /// Define what data to be inserted (text annotations etc.) by DICOM tag of the parent sequence
        /// </summary>
        /// <param name="objects_sq"></param>
        /// <param name="seqTag"></param>
        private void InsertSequenceToDICOM(DCXOBJ seq_item, DICOM_TAGS_ENUM seqTag)
        {
            DCXOBJIterator items = new DCXOBJIterator();

            switch (seqTag)
            {
                case DICOM_TAGS_ENUM.TextObjectSequence:
                    foreach (TextAnnotation ta in this._TextAnnotations)
                    {
                        items.Insert(ta.GetMyDICOM());
                    }
                    break;
                case DICOM_TAGS_ENUM.GraphicObjectSequence:
                    foreach (GraphicObjectAnnotation ga in this._GraphicObjectAnnotations)
                    {
                        items.Insert(ga.GetMyDICOM());
                    }
                    break;
            }

            DCXELM e = new DCXELM();
            e.Init((int)seqTag);
            e.Value = items;
            seq_item.insertElement(e);
        }

        /// <summary>
        /// Create one referenced image object and add to a correspondent series
        /// to insert into GSPS DICOM file
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddInnerReferencedImage(string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (this._ReferencedImages == null)
                this._ReferencedImages = new ReferencedImagesSeries(pSeriesInstanceUID);

            if (FrameNo != -1)
               this._ReferencedImages.AddInstance(pSOPInstanceUID, pSOPClassUID, FrameNo.ToString());
            else
               this._ReferencedImages.AddInstance(pSOPInstanceUID, pSOPClassUID);
        }

        /// <summary>
        /// Create text annotation object to insert into GSPS DICOM file
        /// Receives boundaries of the annotation inside an image in pixels
        /// and text of the annotation
        /// </summary>
        /// <param name="text"></param>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public void AddText(string text, PointF TopLeftCorner, PointF BottomRightCorner)
        {
            if (this._TextAnnotations == null)
                this._TextAnnotations = new List<TextAnnotation>();

            TextAnnotation ta = new TextAnnotation(text, TopLeftCorner, BottomRightCorner);
            this._TextAnnotations.Add(ta);
        }

        /// <summary>
        /// Create graphic annotation object of type "POINT" to insert into GSPS DICOM file
        /// Receives position of the point inside an image in pixels
        /// </summary>
        /// <param name="Position"></param>
        public void AddPoint(PointF Position)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();

            PointF[] points = new PointF[1];
            points[0] = Position;

            GraphicObjectAnnotation ga = new GraphicObjectAnnotation(GraphicObjectType.Point, points, -1, false);

            this._GraphicObjectAnnotations.Add(ga);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels (left/top and bottom/rigth points of the line)
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public void AddLine(PointF TopLeftCorner, PointF BottomRightCorner)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();

            PointF[] points = new PointF[2];
            points[0] = TopLeftCorner;
            points[1] = BottomRightCorner;

            GraphicObjectAnnotation ga = new GraphicObjectAnnotation(GraphicObjectType.Polyline, points, -1, false);

            this._GraphicObjectAnnotations.Add(ga);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels (left/top and bottom/rigth points of the rectangle)
        /// Parameter "Filled" indicates whether or not the rectangle is displayed as filled 
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="Filled"></param>
        public void AddRectangle(PointF TopLeftCorner, PointF BottomRightCorner, bool Filled)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();

            PointF[] points = new PointF[5];
            points[0] = new PointF(TopLeftCorner.X, TopLeftCorner.Y);
            points[1] = new PointF(BottomRightCorner.X, TopLeftCorner.Y);
            points[2] = new PointF(BottomRightCorner.X, BottomRightCorner.Y);
            points[3] = new PointF(TopLeftCorner.X, BottomRightCorner.Y);
            points[4] = new PointF(TopLeftCorner.X, TopLeftCorner.Y);

            GraphicObjectAnnotation ga = new GraphicObjectAnnotation(GraphicObjectType.Polyline, points, -1, Filled);

            this._GraphicObjectAnnotations.Add(ga);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file
        /// Receives any number of points inside an image in pixels 
        /// (if polygone is closed - last point must be the same as first)
        /// Parameter "Filled" indicates whether or not the closed polygone is displayed as filled 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="Filled"></param>
        public void AddPolyline(PointF[] points, bool Filled)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();

            GraphicObjectAnnotation ga = new GraphicObjectAnnotation(GraphicObjectType.Polyline, points, -1, Filled);
            this._GraphicObjectAnnotations.Add(ga);
        }

        /// <summary>
        /// Create graphic annotation object of type "CIRCLE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels 
        /// (the first point is to be interpreted as the center and the second point as a point on the circumference of a circle)
        /// Parameter "Filled" indicates whether or not the circle is displayed as filled 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="pointOnCircumference"></param>
        /// <param name="Filled"></param>
        public void AddCircle(PointF center, PointF pointOnCircumference, bool Filled)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();

            PointF[] points = new PointF[2];
            points[0] = center;
            points[1] = pointOnCircumference;

            GraphicObjectAnnotation ga = new GraphicObjectAnnotation(GraphicObjectType.Circle, points, -1, Filled);

            this._GraphicObjectAnnotations.Add(ga);
        }

        /// <summary>
        /// Create graphic annotation object of type "ELLIPSE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels 
        /// (left/top and bottom/rigth points of the ellipse boundaries)
        /// Parameter "Filled" indicates whether or not the ellipse is displayed as filled 
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="Filled"></param>
        public void AddEllipse(PointF TopLeftCorner, PointF BottomRightCorner, bool Filled)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();

            PointF[] points = new PointF[4];
            points[0] = new PointF(TopLeftCorner.X, TopLeftCorner.Y + (BottomRightCorner.Y - TopLeftCorner.Y) / 2.0f);
            points[1] = new PointF(BottomRightCorner.X, points[0].Y);
            points[2] = new PointF(TopLeftCorner.X + (BottomRightCorner.X - TopLeftCorner.X) / 2.0f, TopLeftCorner.Y);
            points[3] = new PointF(points[2].X, BottomRightCorner.Y);

            GraphicObjectAnnotation ga = new GraphicObjectAnnotation(GraphicObjectType.Ellipse, points, -1, Filled);

            this._GraphicObjectAnnotations.Add(ga);
        }

        /// <summary>
        /// Create graphic annotation object of type "INTERPOLATED" to insert into GSPS DICOM file
        /// Receives any number of points inside an image in pixels 
        /// Points are to be interpreted as an n-tuple list of end points between which curved lines are to be drawn.
        /// Parameter "Filled" indicates whether or not the path is displayed as filled 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="Filled"></param>
        public void AddInterpolated(PointF[] points, bool Filled)
        {
            if (this._GraphicObjectAnnotations == null)
                this._GraphicObjectAnnotations = new List<GraphicObjectAnnotation>();

            GraphicObjectAnnotation ga = new GraphicObjectAnnotation(GraphicObjectType.Interpolated, points, -1, Filled);
            this._GraphicObjectAnnotations.Add(ga);
        }
    }


    /// <summary>
    /// Class containing attributes of vector graphics and text annotations that shall be made available by a display device 
    /// to be applied to a DICOM image.
    /// </summary>
    public class GraphicAnnotationModule : IDicomizable
    {
        /// <summary>
        /// A sequence of Items each of which represents a group of annotations composed of graphics or text or both.
        /// One or more Items shall be included in this Sequence.
        /// </summary>
        private List<AnnotationSet> _AnnotationSets = new List<AnnotationSet>();

        /// <summary>
        /// Areas in the DICOM image to draw annotations
        /// </summary>
        private List<DisplayedArea> _DisplayedAreas = new List<DisplayedArea>();

        /// <summary>
        /// Graphic Layers of the current DICOM presentation module
        /// </summary>
        private List<GraphicLayerModule> _GraphicLayerModules = new List<GraphicLayerModule>();

        /// <summary>
        /// Create displayed area object to insert into GSPS DICOM file.
        /// Receives boundaries of displayed area inside an image in pixels.
        /// Displayed area will be created without Referenced Image Sequence for now 
        /// so only one area must be added (containing boundaries of entire image)
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public void AddDisplaydArea(PointF TopLeftCorner, PointF BottomRightCorner)
        {
            this._DisplayedAreas.Add(new DisplayedArea(TopLeftCorner, BottomRightCorner));
        }

        /// <summary>
        /// Create text annotation object to insert into GSPS DICOM file
        /// Receives boundaries of the annotation inside an image in pixels
        /// and text of the annotation
        /// </summary>
        /// <param name="text"></param>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public void AddText(string text, PointF TopLeftCorner, PointF BottomRightCorner,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddText(text, TopLeftCorner, BottomRightCorner);
        }

        /// <summary>
        /// Create graphic annotation object of type "POINT" to insert into GSPS DICOM file
        /// Receives position of the point inside an image in pixels
        /// </summary>
        /// <param name="Position"></param>
        public void AddPoint(PointF Position,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddPoint(Position);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels (left/top and bottom/rigth points of the line)
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public void AddLine(PointF TopLeftCorner, PointF BottomRightCorner,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddLine(TopLeftCorner, BottomRightCorner);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels (left/top and bottom/rigth points of the rectangle)
        /// Parameter "Filled" indicates whether or not the rectangle is displayed as filled 
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="Filled"></param>
        public void AddRectangle(PointF TopLeftCorner, PointF BottomRightCorner, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddRectangle(TopLeftCorner, BottomRightCorner, Filled);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file
        /// Receives any number of points inside an image in pixels 
        /// (if polygone is closed - last point must be the same as first)
        /// Parameter "Filled" indicates whether or not the closed polygone is displayed as filled 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="Filled"></param>
        public void AddPolyline(PointF[] points, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddPolyline(points, Filled);
        }

        /// <summary>
        /// Create graphic annotation object of type "CIRCLE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels 
        /// (the first point is to be interpreted as the center and the second point as a point on the circumference of a circle)
        /// Parameter "Filled" indicates whether or not the circle is displayed as filled 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="pointOnCircumference"></param>
        /// <param name="Filled"></param>
        public void AddCircle(PointF center, PointF pointOnCircumference, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddCircle(center, pointOnCircumference, Filled);
        }

        /// <summary>
        /// Create graphic annotation object of type "ELLIPSE" to insert into GSPS DICOM file
        /// Receives two points inside an image in pixels 
        /// (left/top and bottom/rigth points of the ellipse boundaries)
        /// Parameter "Filled" indicates whether or not the ellipse is displayed as filled 
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="Filled"></param>
        public void AddEllipse(PointF TopLeftCorner, PointF BottomRightCorner, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddEllipse(TopLeftCorner, BottomRightCorner, Filled);
        }

        /// <summary>
        /// Create graphic annotation object of type "INTERPOLATED" to insert into GSPS DICOM file
        /// Receives any number of points inside an image in pixels 
        /// Points are to be interpreted as an n-tuple list of end points between which curved lines are to be drawn.
        /// Parameter "Filled" indicates whether or not the path is displayed as filled 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="Filled"></param>
        public void AddInterpolated(PointF[] points, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet setToAdd = this.FindSetToInsertAnnotation(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
            setToAdd.AddInterpolated(points, Filled);
        }

        /// <summary>
        /// Write into given DCXOBJ instance all info about Graphic Annotation Module:
        /// displayed area, Referenced Image Sequence, graphics and text annotations sequences
        /// </summary>
        /// <param name="output"></param>
        public void ToDICOM(DCXOBJ output)
        {
            DCXOBJIterator objects_sq = new DCXOBJIterator();

            //Insert all Graphic Annotation Sequence objects
            foreach (AnnotationSet set in this._AnnotationSets)
            {
                set.InsertMyObjectsToDICOM(objects_sq);
            }

            DCXELM e = new DCXELM();
            e.Init((int)DICOM_TAGS_ENUM.GraphicAnnotationSequence);
            e.Value = objects_sq;
            output.insertElement(e);

            //Insert displayed area (only one, we don't support multiple displayed areas now)
            if (this._DisplayedAreas.Count > 0)
            {
                objects_sq = new DCXOBJIterator();
                objects_sq.Insert(this._DisplayedAreas[0].GetMyDICOM());
            }

            e.Init((int)DICOM_TAGS_ENUM.DisplayedAreaSelectionSequence);
            e.Value = objects_sq;
            output.insertElement(e);

            //Insert graphic layer (only one, we don't support multiple graphic layers now)
            if (this._GraphicLayerModules.Count == 0)
                this._GraphicLayerModules.Add(new GraphicLayerModule());
            objects_sq = new DCXOBJIterator();
            objects_sq.Insert(this._GraphicLayerModules[0].GetMyDICOM());


            e.Init((int)DICOM_TAGS_ENUM.GraphicLayerSequence);
            e.Value = objects_sq;
            output.insertElement(e);
        }

        /// <summary>
        /// Draw all graphics and text annotations on the given bitmap inside
        /// boundaries defined by displayed area
        /// If Referenced Image Sequences is not empty - check that annotations
        /// must applied on given SOP instance and frame 
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="SOPInstanceUID"></param>
        /// <param name="FrameNum"></param>
        public void DrawOnBitmap(Bitmap bm, string SOPInstanceUID, int FrameNum)
        {
            DisplayedArea da = null;
            DisplayedArea entireDa = null;

            foreach (DisplayedArea ar in this._DisplayedAreas)
            {
                if (ar.ReferencedImages == null)
                {
                    entireDa = ar;
                    continue;
                }
                if (ar.ReferencedImages.IsMySOPAndFrame(SOPInstanceUID, FrameNum))
                {
                    da = ar;
                    continue;
                }
            }

            if (da == null)
                da = entireDa;

            if (da != null)
            {
                foreach (AnnotationSet set in this._AnnotationSets)
                {
                    set.DrawOnBitmap(bm, da, SOPInstanceUID, FrameNum);
                }
            }
        }

        /// <summary>
        /// Read Graphic Annotation Module data from DCXOBJ containing all tags of input DICOM file
        /// </summary>
        /// <param name="input"></param>
        public void FromDICOM(DCXOBJ input)
        {
            DCXELM el = null;

            el = input.GetElement((int)DICOM_TAGS_ENUM.GraphicAnnotationSequence);
            if (el != null)
            {
                //File contains Graphic annotation sequence
                this.LoadFromDCXIterator(el.Value as DCXOBJIterator, DICOM_TAGS_ENUM.GraphicAnnotationSequence);
            }

            el = input.GetElement((int)DICOM_TAGS_ENUM.DisplayedAreaSelectionSequence);
            if (el != null && el.Value != null)
            {
                //File contains DisplayedArea Selection Sequence
                this.LoadFromDCXIterator(el.Value as DCXOBJIterator, DICOM_TAGS_ENUM.DisplayedAreaSelectionSequence);
            }
            else
            {
                //Create "dummy" displayed area
                this._DisplayedAreas.Add(new DisplayedArea(new PointF(0, 0), new PointF(1024, 768)));
            }

            el = input.GetElement((int)DICOM_TAGS_ENUM.GraphicLayerSequence);
            if (el != null)
            {
                //File contains Graphic Layer Sequence
                this.LoadFromDCXIterator(el.Value as DCXOBJIterator, DICOM_TAGS_ENUM.GraphicLayerSequence);
            }
        }

        /// <summary>
        /// Read DCXOBJ objects from given DCXOBJIterator 
        /// Define what data has to be loaded (Graphic Annotation Sequence, text annotations etc.) 
        /// by DICOM tag of the parent sequence (for example TextObjectSequence)
        /// </summary>
        /// <param name="objIt"></param>
        /// <param name="seqTag"></param>
        private void LoadFromDCXIterator(DCXOBJIterator objIt, DICOM_TAGS_ENUM seqTag)
        {
            DCXOBJ Obj = null;
            for (; !objIt.AtEnd(); objIt.Next())
            {
                Obj = objIt.Get();
                switch (seqTag)
                {
                    case DICOM_TAGS_ENUM.GraphicObjectSequence:
                        this._AnnotationSets[this._AnnotationSets.Count - 1].ReadGraphicAnnotation(Obj);
                        break;
                    case DICOM_TAGS_ENUM.TextObjectSequence:
                        this._AnnotationSets[this._AnnotationSets.Count - 1].ReadTextAnnotation(Obj);
                        break;
                    case DICOM_TAGS_ENUM.DisplayedAreaSelectionSequence:
                        this._DisplayedAreas.Add(new DisplayedArea(Obj));
                        break;
                    case DICOM_TAGS_ENUM.GraphicLayerSequence:
                        this._GraphicLayerModules.Add(new GraphicLayerModule(Obj));
                        break;
                    default:
                        if (seqTag == DICOM_TAGS_ENUM.GraphicAnnotationSequence)
                        {
                            //Start first Annotation set
                            this._AnnotationSets.Add(new AnnotationSet());
                        }
                        this.LoadFromDCXObject(Obj);
                        break;
                }
            }
            if (Obj != null)
                Marshal.ReleaseComObject(Obj);
        }

        /// <summary>
        /// Receives one object loaded earlier from DCXOBJIterator,
        /// find its child DCXELM of sequence type and load data this sequence
        /// For example - load Text Object Sequence from parent Graphic Annotation Sequence
        /// </summary>
        /// <param name="obj"></param>
        private void LoadFromDCXObject(DCXOBJ obj)
        {
            DCXELMIterator elIt = obj.iterator();
            DCXELM el = null;
            for (; !elIt.AtEnd(); elIt.Next())
            {
                el = elIt.Get();
                if (el.ValueRepresentation == VR_CODE.VR_CODE_SQ)
                {
                    //Element is some sequence
                    if (el.Tag == (int)DICOM_TAGS_ENUM.ReferencedImageSequence)
                    {
                        //Loading Referenced Image Sequence
                        //Check whether or not this is first of them
                        //If it's the first - ReferencedImages object in the current annotation set is still null
                        if (this._AnnotationSets[this._AnnotationSets.Count - 1].ReferencedImages != null)
                        {
                            //ReferencedImages object in the current annotation set is not null 
                            //It means this is a new Annotation set (new Graphic Annotation Sequence item)
                            this._AnnotationSets.Add(new AnnotationSet());
                        }
                        this._AnnotationSets[this._AnnotationSets.Count - 1].ReferencedImages = new ReferencedImagesSeries(obj);
                    }
                    else
                    {
                        //Load all other that referenced images sequences (text annotations, graphic objects etc.)
                        this.LoadFromDCXIterator(el.Value as DCXOBJIterator, (DICOM_TAGS_ENUM)el.Tag);
                    }
                }
                else
                {
                    //Elements is plain DICOM tag, for example Graphic layer property
                    if (el.Tag == (int)DICOM_TAGS_ENUM.GraphicLayer)
                        this._AnnotationSets[this._AnnotationSets.Count - 1].GraphicLayer = el.Value.ToString();
                }
            }
            if (elIt != null)
                Marshal.ReleaseComObject(elIt);
        }

        /// <summary>
        /// When inserting new annotation - try to find annotation set containing the same instance info in 
        /// its referenced images list. If found - add new annotation to this set. 
        /// If not found - create new set and add instance info to its referenced images list. 
        /// If instance info is not defined ("study level" annotation) - find/create set withiout referenced images list
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        /// <returns></returns>
        private AnnotationSet FindSetToInsertAnnotation(string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            AnnotationSet res = null;
            if (this._AnnotationSets.Count != 0)
            {
                //Try to find set by instance info
                foreach (AnnotationSet set in this._AnnotationSets)
                {
                    if (pSOPInstanceUID == "")
                    {
                        //"Study level" annotation - search for set with null referenced images object
                        if (set.ReferencedImages == null)
                        {
                            res = set;
                            break;
                        }
                    }
                    else
                    {
                        //"Instance/frame level" annotation - search for set containing the same instance/frame info
                        //If found - annotation will be added to this set
                        if (set.ReferencedImages != null && set.ReferencedImages.IsMySOPAndFrame(pSOPInstanceUID, FrameNo))
                        {
                            res = set;
                            break;
                        }
                    }
                }
            }

            if (res == null)
            {
                //First annotation of not found
                res = new AnnotationSet();
                if (pSOPInstanceUID != "")
                {
                    //Add instance info if needed to referenced images list of the new set
                    res.AddInnerReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
                }
                this._AnnotationSets.Add(res);
            }


            return res;
        }



    }

    /// <summary>
    /// Class containing The Grayscale Softcopy Presentation State Information Object Definition (IOD) 
    /// which specifies information about some graphic annotations that may be used to present(display) images 
    /// that are referenced from within the IOD.
    /// </summary>
    public class DICOMPresentationModule : IDicomizable
    {
        private string _SeriesInstanceUID = "";

        /// <summary>
        /// Series of the presentation module (all pr.modules of the study might belong to one series)
        /// </summary>
        public string SeriesInstanceUID
        {
            get { return _SeriesInstanceUID; }
            set { _SeriesInstanceUID = value; }
        }

        private string _StudyInstanceUID = "";

        /// <summary>
        /// Defines study UID to identify DICOM files to apply annotations
        /// </summary>
        public string StudyInstanceUID
        {
            get { return _StudyInstanceUID; }
            set { _StudyInstanceUID = value; }
        }

        private string _PatientID = "";

        /// <summary>
        /// Defines patient ID to identify DICOM files to apply annotations
        /// </summary>
        public string PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        /// <summary>
        /// Attributes that identify and describe a Presentation Series
        /// </summary>
        private ReferencedSeriesModule _ReferencedSeriesModule = new ReferencedSeriesModule();

        /// <summary>
        /// Attributes of vector graphics and text annotation that shall be applied to an image
        /// </summary>
        private GraphicAnnotationModule _GraphicAnnotationModule = new GraphicAnnotationModule();

        /// <summary>
        /// Constructor to create Presentation Module for read/write in DICOM presentation file
        /// </summary>
        public DICOMPresentationModule()
        {
        }

        /// <summary>
        /// Check whether study UID and patient ID from DICOM file is equal to given parameters
        /// For finding presentation modules belonging to one study
        /// </summary>
        /// <param name="patInfo"></param>
        /// <param name="studyInstanceUID"></param>
        /// <returns></returns>
        public bool IsMyStudy(string patientID, string studyInstanceUID)
        {
            return (this.PatientID == patientID &&
                    this.StudyInstanceUID == studyInstanceUID);
        }

        /// <summary>
        /// Search SOP instance in all series of Referenced Series Module 
        /// by series UID and SOP instance UID
        /// If instance found - return true
        /// If not found - return false
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <returns></returns>
        public bool IsMySOPInstance(string seriesInstanceUID, string sopInstanceUID)
        {
            return this._ReferencedSeriesModule.IfInstanceBelongsToMe(seriesInstanceUID, sopInstanceUID);
        }

        /// <summary>
        /// Draw all graphics and text annotations on the given bitmap inside
        /// boundaries defined by displayed area
        /// If Referenced Image Sequences is not empty - check that annotations
        /// must applied on given SOP instance and frame 
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="SOPInstanceUID"></param>
        /// <param name="FrameNum"></param>
        public void DrawOnBitmap(Bitmap bm, string SOPInstanceUID, int FrameNum)
        {
            this._GraphicAnnotationModule.DrawOnBitmap(bm, SOPInstanceUID, FrameNum);
        }

        /// <summary>
        /// Read Presentation Module data from DCXOBJ containing all tags of input DICOM file
        /// </summary>
        /// <param name="input"></param>
        public void FromDICOM(DCXOBJ input)
        {
            if (DCXFunctions.GetElementValueAsString(input, (int)DICOM_TAGS_ENUM.Modality) != "PR")
                throw new Exception("Not DICOM presentation");

            this._StudyInstanceUID = DCXFunctions.GetElementValueAsString(input, (int)DICOM_TAGS_ENUM.studyInstanceUID);
            this._PatientID = DCXFunctions.GetElementValueAsString(input, (int)DICOM_TAGS_ENUM.patientID);

            _ReferencedSeriesModule.FromDICOM(input);

            _GraphicAnnotationModule.FromDICOM(input);
        }

        /// <summary>
        /// Write into given DCXOBJ instance all info about Presentation Module:
        /// modality "PR", study instance UID, patient ID,
        /// Referenced Series Module and Graphic Annotation Module sequences
        /// </summary>
        /// <param name="output"></param>
        public void ToDICOM(DCXOBJ output)
        {
            DCXFunctions.InsertElement(output, DICOM_TAGS_ENUM.Modality, "PR");
            DCXFunctions.InsertElement(output, DICOM_TAGS_ENUM.studyInstanceUID, this._StudyInstanceUID);
            DCXFunctions.InsertElement(output, DICOM_TAGS_ENUM.patientID, this._PatientID);
            DCXFunctions.InsertElement(output, DICOM_TAGS_ENUM.seriesInstanceUID, this._SeriesInstanceUID);
            DCXFunctions.InsertElement(output, DICOM_TAGS_ENUM.MediaStorageSOPClassUID, "1.2.840.10008.5.1.4.1.1.11.1");
            DCXFunctions.InsertElement(output, DICOM_TAGS_ENUM.sopClassUid, "1.2.840.10008.5.1.4.1.1.11.1");
            DCXFunctions.InsertElement(output, DICOM_TAGS_ENUM.sopInstanceUID, (new DCXUID()).CreateUID(UID_TYPE.UID_TYPE_INSTANCE));

            this._ReferencedSeriesModule.ToDICOM(output);

            this._GraphicAnnotationModule.ToDICOM(output);
        }

        /// <summary>
        /// Insert new Referenced Image instance into correspondent series
        /// on "study level" i.e. these are all images of the study participating in displaying annotations
        /// To be called on inserting of each "instance/frame" annotation
        /// If there is annotation of "study level" this method must be called in the loop on all study images
        /// </summary>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <returns></returns>
        public void AddReferencedImage(string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID)
        {
            this._ReferencedSeriesModule.AddInstance(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);
        }

        /// <summary>
        /// Create displayed area object to insert into GSPS DICOM file.
        /// Receives boundaries of displayed area inside an image in pixels.
        /// Displayed area will be created without Referenced Image Sequence for now 
        /// so only one area must be added (containing boundaries of entire image)
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public void AddDisplaydArea(PointF TopLeftCorner, PointF BottomRightCorner)
        {
            this._GraphicAnnotationModule.AddDisplaydArea(TopLeftCorner, BottomRightCorner);
        }

        /// <summary>
        /// Create text annotation object to insert into GSPS DICOM file.
        /// Receives boundaries of the annotation inside an image in pixels 
        /// and text of the annotation.
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list        
        /// </summary>
        /// <param name="text"></param>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddText(string text, PointF TopLeftCorner, PointF BottomRightCorner,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddText(text, TopLeftCorner, BottomRightCorner, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }

        /// <summary>
        /// Create graphic annotation object of type "POINT" to insert into GSPS DICOM file.
        /// Receives position of the point inside an image in pixels.
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddPoint(PointF Position,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddPoint(Position, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file.
        /// Receives two points inside an image in pixels (left/top and bottom/rigth points of the line).
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddLine(PointF TopLeftCorner, PointF BottomRightCorner,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddLine(TopLeftCorner, BottomRightCorner, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file.
        /// Receives two points inside an image in pixels (left/top and bottom/rigth points of the rectangle).
        /// Parameter "Filled" indicates whether or not the rectangle is displayed as filled.
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="Filled"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddRectangle(PointF TopLeftCorner, PointF BottomRightCorner, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddRectangle(TopLeftCorner, BottomRightCorner, Filled, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }

        /// <summary>
        /// Create graphic annotation object of type "POLYLINE" to insert into GSPS DICOM file.
        /// Receives any number of points inside an image in pixels 
        /// (if polygone is closed - last point must be the same as first).
        /// Parameter "Filled" indicates whether or not the closed polygone is displayed as filled.
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list
        /// </summary>
        /// <param name="points"></param>
        /// <param name="Filled"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddPolyline(PointF[] points, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddPolyline(points, Filled, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }

        /// <summary>
        /// Create graphic annotation object of type "CIRCLE" to insert into GSPS DICOM file.
        /// Receives two points inside an image in pixels  
        /// (the first point is to be interpreted as the center and the second point as a point on the circumference of a circle).
        /// Parameter "Filled" indicates whether or not the circle is displayed as filled.
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list
        /// </summary>
        /// <param name="center"></param>
        /// <param name="pointOnCircumference"></param>
        /// <param name="Filled"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddCircle(PointF center, PointF pointOnCircumference, bool Filled,
                            string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddCircle(center, pointOnCircumference, Filled, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }

        /// <summary>
        /// Create graphic annotation object of type "ELLIPSE" to insert into GSPS DICOM file.
        /// Receives two points inside an image in pixels  
        /// (left/top and bottom/rigth points of the ellipse boundaries)
        /// Parameter "Filled" indicates whether or not the ellipse is displayed as filled. 
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list
        /// </summary>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        /// <param name="Filled"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddEllipse(PointF TopLeftCorner, PointF BottomRightCorner, bool Filled,
                               string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddEllipse(TopLeftCorner, BottomRightCorner, Filled, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }

        /// <summary>
        /// Create graphic annotation object of type "INTERPOLATED" to insert into GSPS DICOM file.
        /// Receives any number of points inside an image in pixels. 
        /// Points are to be interpreted as an n-tuple list of end points between which curved lines are to be drawn.
        /// Parameter "Filled" indicates whether or not the path is displayed as filled 
        /// If pSOPInstanceUID parameter is not empty - add instance info to "study level" referenced instances list
        /// </summary>
        /// <param name="points"></param>
        /// <param name="Filled"></param>
        /// <param name="pSeriesInstanceUID"></param>
        /// <param name="pSOPInstanceUID"></param>
        /// <param name="pSOPClassUID"></param>
        /// <param name="FrameNo"></param>
        public void AddInterpolated(PointF[] points, bool Filled,
                                    string pSeriesInstanceUID, string pSOPInstanceUID, string pSOPClassUID, int FrameNo)
        {
            if (pSOPInstanceUID != "")
                this.AddReferencedImage(pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID);

            this._GraphicAnnotationModule.AddInterpolated(points, Filled, pSeriesInstanceUID, pSOPInstanceUID, pSOPClassUID, FrameNo);
        }


    }
}
