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
    /// Type of graphic annotaion to draw on DICOM image
    /// </summary>
    public enum GraphicObjectType
    {
        Point = 0,
        Polyline,
        Circle,
        Ellipse,
        Interpolated
    }

    /// <summary>
    /// Parent class off all types of annotations (text, graphics)
    /// </summary>
    public class BasicAnnotation
    {
        /// <summary>
        /// Create DICOM string representation of the points list to store in DICOM files
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static string GetStringForValueArray(PointF[] points)
        {
            string res = "";

            foreach (PointF p in points)
            {
                res = res + p.X.ToString() + @"\" + p.Y.ToString() + @"\";
            }

            return res.Substring(0, res.Length - 1);
        }

        /// <summary>
        /// Draw one annotation (text or graphic) on the given GDI Graphics object by given data (type, units etc.)
        /// If units is "DISPLAY" (relative position inside displayed area) - calculate correspondent positions in pixels
        /// </summary>
        /// <param name="g"></param>
        /// <param name="units"></param>
        /// <param name="type"></param>
        /// <param name="Filled"></param>
        /// <param name="da"></param>
        /// <param name="points"></param>
        /// <param name="text"></param>
        protected void Draw(Graphics g, string units, string type, bool Filled, DisplayedArea da, PointF[] points, string text)
        {
            float Width = 1;
            float Height = 1;

            DisplayedAreaDrawingInfo di = da.GetMyDrawingInfo();
            if (units == "DISPLAY")
            {
                //DISPLAY - fractions
                Width = di.Width;
                Height = di.Height;
            }

            switch (type)
            {
                case "POLYLINE":
                    this.DrawPolyline(g, Filled, units == "DISPLAY" ? di.StartPoint : new PointF(0,0) , Width, Height, points);
                    break;
                case "POINT":
                    this.DrawPoint(g, units == "DISPLAY" ? di.StartPoint : new PointF(0, 0), Width, Height, points[0]);
                    break;
                case "CIRCLE":
                    this.DrawCircle(g, Filled, units == "DISPLAY" ? di.StartPoint : new PointF(0, 0), Width, Height, points);
                    break;
                case "ELLIPSE":
                    this.DrawEllipse(g, Filled, units == "DISPLAY" ? di.StartPoint : new PointF(0, 0), Width, Height, points);
                    break;
                case "TEXT":
                    this.DrawText(g, units == "DISPLAY" ? di.StartPoint : new PointF(0, 0), Width, Height, points, text);
                    break;
                case "INTERPOLATED":
                    this.DrawBezier(g, Filled, units == "DISPLAY" ? di.StartPoint : new PointF(0, 0), Width, Height, points);
                    break;
            }
        }

        /// <summary>
        /// Draw/fill polygone figure inside displayed area boundaries on the given Graphics object using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="Filled"></param>
        /// <param name="StartPoint"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="points"></param>
        private void DrawPolyline(Graphics g, bool Filled, PointF StartPoint, float Width, float Height, PointF[] points)
        {
            PointF[] drawingPoints = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                drawingPoints[i].X = StartPoint.X + Width * points[i].X;
                drawingPoints[i].Y = StartPoint.Y + Height * points[i].Y;
            }

            if (!Filled)
            {
                for (int i = 1; i < drawingPoints.Length; i++)
                {
                    g.DrawLine(new Pen(Color.Yellow),
                               drawingPoints[i - 1].X,
                               drawingPoints[i - 1].Y,
                               drawingPoints[i].X,
                               drawingPoints[i].Y);
                }
            }
            else
                g.FillPolygon(new SolidBrush(Color.Yellow), drawingPoints, System.Drawing.Drawing2D.FillMode.Winding);
        }

        /// <summary>
        /// Draw opne point inside displayed area boundaries on the given Graphics object using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="StartPoint"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="point"></param>
        private void DrawPoint(Graphics g, PointF StartPoint, float Width, float Height, PointF point)
        {
            g.FillRectangle(new SolidBrush(Color.Yellow), StartPoint.X + Width * point.X, StartPoint.Y + Height * point.Y, 1, 1);
        }

        /// <summary>
        /// Draw/fill circle figure inside displayed area boundaries on the given Graphics object using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="Filled"></param>
        /// <param name="StartPoint"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="points"></param>
        private void DrawCircle(Graphics g, bool Filled, PointF StartPoint, float Width, float Height, PointF[] points)
        {
            //Calculate radius
            double radius = DCXFunctions.CalculateDistanceBetweenTwoPoints(Width * points[0].X, Height * points[0].Y,
                                                                        Width * points[1].X, Height * points[1].Y);

            if (!Filled)
                g.DrawEllipse(new Pen(Color.Yellow), (int)(StartPoint.X + Width * points[0].X - radius),
                                                     (int)(StartPoint.Y + Height * points[0].Y - radius),
                                                     (int)(radius + radius), (int)(radius + radius));
            else
                g.FillEllipse(new SolidBrush(Color.Yellow), (int)(StartPoint.X + Width * points[0].X - radius),
                                                 (int)(StartPoint.Y + Height * points[0].Y - radius),
                                                 (int)(radius + radius), (int)(radius + radius));
        }

        /// <summary>
        /// Draw/fill ellipse figure inside displayed area boundaries on the given Graphics object using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="Filled"></param>
        /// <param name="StartPoint"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="points"></param>
        private void DrawEllipse(Graphics g, bool Filled, PointF StartPoint, float Width, float Height, PointF[] points)
        {
            //Build bounding rectangle
            RectangleF bounds = new RectangleF(StartPoint.X + Width * points[0].X, StartPoint.Y + Height * points[2].Y,
                                               Math.Abs(Width * points[1].X - Width * points[0].X),
                                               Math.Abs(Height * points[3].Y - Height * points[2].Y));
            if (!Filled)
                g.DrawEllipse(new Pen(Color.Yellow), bounds);
            else
                g.FillEllipse(new SolidBrush(Color.Yellow), bounds);
        }

        /// <summary>
        /// Draw/fill curve lines set inside displayed area boundaries on the given Graphics object using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="Filled"></param>
        /// <param name="StartPoint"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="points"></param>
        private void DrawBezier(Graphics g, bool Filled, PointF StartPoint, float Width, float Height, PointF[] points)
        {
            PointF[] drawingPoints = new PointF[points.Length];
            for(int i = 0; i < points.Length; i++)
            {
                drawingPoints[i].X = StartPoint.X + Width * points[i].X;
                drawingPoints[i].Y = StartPoint.Y + Height * points[i].Y;
            }
            if (!Filled)
                g.DrawBeziers(new Pen(Color.Yellow), drawingPoints);
            else
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddBeziers(drawingPoints);
                g.FillPath(new SolidBrush(Color.Yellow), path);
            }
        }

        /// <summary>
        /// Draw text inside displayed area boundaries on the given Graphics object using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="StartPoint"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="points"></param>
        /// <param name="text"></param>
        private void DrawText(Graphics g, PointF StartPoint, float Width, float Height, PointF[] points, string text)
        {
            RectangleF drawArea = new RectangleF(StartPoint.X + Width * points[0].X,
                      StartPoint.Y + Height * points[0].Y,
                      StartPoint.X + Width * points[1].X,
                      StartPoint.Y + Height * points[1].Y);
            g.DrawString(text, new Font("Verdana", 10, FontStyle.Regular), new SolidBrush(Color.Yellow), drawArea);
        }
    }

    /// <summary>
    /// Class storing data of one text annotation to draw on the image
    /// </summary>
    public class TextAnnotation : BasicAnnotation
    {
        /// <summary>
        /// Text data that is unformatted and whose manner of display within the defined bounding box is implementation dependent.
        /// </summary>
        private string _UnformattedTextValue;

        /// <summary>
        /// "DISPLAY" or "PIXEL"
        /// </summary>
        private string _BoundingBoxAnnotationUnits;

        /// <summary>
        /// Top left point of text boundaries inside displayed area
        /// </summary>
        private PointF _BoundingBoxTopLeftHandCorner;

        /// <summary>
        /// Bottom right point of text boundaries inside displayed area
        /// </summary>
        private PointF _BoundingBoxBottomRightHandCorner;

        /// <summary>
        /// Location of the text relative to the vertical edges of the bounding box.
        /// </summary>
        private string _BoundingBoxTextHorizontalJustification;

        /// <summary>
        /// Constructor for reading data from DICOM file
        /// </summary>
        /// <param name="obj"></param>
        public TextAnnotation(DCXOBJ obj)
        {
            this._UnformattedTextValue = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.UnformattedTextValue);
            this._BoundingBoxAnnotationUnits = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.BoundingBoxAnnotationUnits);
            this._BoundingBoxTextHorizontalJustification = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.BoundingBoxTextHorizontalJustification);
            DCXELM el = obj.GetElement((int)DICOM_TAGS_ENUM.BoundingBoxTopLeftHandCorner);
            this._BoundingBoxTopLeftHandCorner = DCXFunctions.GetPointFromValueArray(el);
            el = obj.GetElement((int)DICOM_TAGS_ENUM.BoundingBoxBottomRightHandCorner);
            this._BoundingBoxBottomRightHandCorner = DCXFunctions.GetPointFromValueArray(el);
        }

        /// <summary>
        /// Constructor for writing data into DICOM file
        /// </summary>
        /// <param name="text"></param>
        /// <param name="TopLeftCorner"></param>
        /// <param name="BottomRightCorner"></param>
        public TextAnnotation(string text, PointF TopLeftCorner, PointF BottomRightCorner)
        {
            this._UnformattedTextValue = text;
            this._BoundingBoxTopLeftHandCorner = TopLeftCorner;
            this._BoundingBoxBottomRightHandCorner = BottomRightCorner;

            //???
            this._BoundingBoxAnnotationUnits = "PIXEL";
            this._BoundingBoxTextHorizontalJustification = "LEFT";
        }

        /// <summary>
        /// Build DCXOBJ instance containing info about text annotation
        /// </summary>
        /// <returns></returns>
        public DCXOBJ GetMyDICOM()
        {
            DCXOBJ o = new DCXOBJ();
            DCXELM elToIns = new DCXELM();

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.UnformattedTextValue, this._UnformattedTextValue);
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.BoundingBoxAnnotationUnits, this._BoundingBoxAnnotationUnits);
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.BoundingBoxTextHorizontalJustification, this._BoundingBoxTextHorizontalJustification);

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.BoundingBoxTopLeftHandCorner, BasicAnnotation.GetStringForValueArray(new PointF[] { this._BoundingBoxTopLeftHandCorner }));
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.BoundingBoxBottomRightHandCorner, BasicAnnotation.GetStringForValueArray(new PointF[] { this._BoundingBoxBottomRightHandCorner }));

            return o;
        }

        /// <summary>
        /// Draw text annotations inside given displayed area using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="da"></param>
        public void DrawMyText(Graphics g, DisplayedArea da)
        {
            PointF[] points = new PointF[2];
            points[0] = this._BoundingBoxTopLeftHandCorner;
            points[1] = this._BoundingBoxBottomRightHandCorner;

            base.Draw(g, this._BoundingBoxAnnotationUnits, "TEXT", false, da, points, this._UnformattedTextValue);

        }
    }

    /// <summary>
    /// Class storing data of one graphics annotation to draw on the image
    /// </summary>
    public class GraphicObjectAnnotation : BasicAnnotation
    {
        /// <summary>
        /// "DISPLAY" or "PIXEL"
        /// </summary>
        private string _GraphicAnnotationUnits;

        /// <summary>
        /// The shape of graphic that is to be drawn.
        /// POINT, POLYLINE, INTERPOLATED, CIRCLE, ELLIPSE
        /// </summary>
        private string _GraphicType;

        /// <summary>
        /// "Y" or "N"
        /// </summary>
        private string _GraphicFilled;

        /// <summary>
        /// Set of the points to draw figure
        /// </summary>
        private PointF[] _GraphicData;

        /// <summary>
        /// Always 2 (by standard)
        /// </summary>
        private int _GraphicDimensions;

        /// <summary>
        /// Number of data points in this graphic
        /// </summary>
        private int _NumberOfGraphicPoints;

        /// <summary>
        /// Constructor for writing data into DICOM file
        /// </summary>
        /// <param name="type"></param>
        /// <param name="points"></param>
        /// <param name="radius"></param>
        /// <param name="Filled"></param>
        public GraphicObjectAnnotation(GraphicObjectType type, PointF[] points, float radius, bool Filled)
        {
            this._GraphicData = points;
            this._NumberOfGraphicPoints = points.Length;

            switch (type)
            {
                case GraphicObjectType.Polyline:
                    this._GraphicType = "POLYLINE";
                    break;
                case GraphicObjectType.Point:
                    this._GraphicType = "POINT";
                    break;
                case GraphicObjectType.Circle:
                    this._GraphicType = "CIRCLE";
                    break;
                case GraphicObjectType.Ellipse:
                    this._GraphicType = "ELLIPSE";
                    break;
                case GraphicObjectType.Interpolated:
                    this._GraphicType = "INTERPOLATED";
                    break;
            }
            this._GraphicFilled = Filled ? "Y" : "N";

            //???
            this._GraphicAnnotationUnits = "PIXEL";
            this._GraphicDimensions = 2;
        }

        /// <summary>
        /// Constructor for reading data from DICOM file
        /// </summary>
        /// <param name="obj"></param>
        public GraphicObjectAnnotation(DCXOBJ obj)
        {
            this._GraphicAnnotationUnits = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.GraphicAnnotationUnits);
            this._GraphicType = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.GraphicType);
            this._GraphicFilled = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.GraphicFilled);
            this._GraphicDimensions = Convert.ToInt32(DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.GraphicDimensions));
            this._NumberOfGraphicPoints = Convert.ToInt32(DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.NumberOfGraphicPoints));

            DCXELM el = obj.GetElement((int)DICOM_TAGS_ENUM.GraphicData);
            float[] points = el.ValueArray as float[];
            this._GraphicData = new PointF[points.Length / 2];
            int pointsCount = 0;
            for (int i = 0; i < points.Length - 1; i = i + 2)
            {
                this._GraphicData[pointsCount].X = points[i];
                this._GraphicData[pointsCount].Y = points[i + 1];
                pointsCount++;
            }
        }

        /// <summary>
        /// Build DCXOBJ instance containing info about graphic annotation
        /// </summary>
        /// <returns></returns>
        public DCXOBJ GetMyDICOM()
        {
            DCXOBJ o = new DCXOBJ();
            DCXELM elToIns = new DCXELM();

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicAnnotationUnits, this._GraphicAnnotationUnits);

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicData, BasicAnnotation.GetStringForValueArray(this._GraphicData));

            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicType, this._GraphicType);
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicFilled, this._GraphicFilled);
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.GraphicDimensions, this._GraphicDimensions);
            DCXFunctions.InsertElement(o, DICOM_TAGS_ENUM.NumberOfGraphicPoints, this._NumberOfGraphicPoints);

            return o;
        }

        /// <summary>
        /// Draw corresponding figure inside given displayed area using GDI methods
        /// </summary>
        /// <param name="g"></param>
        /// <param name="da"></param>
        public void DrawMyGraphic(Graphics g, DisplayedArea da)
        {
            base.Draw(g, this._GraphicAnnotationUnits, this._GraphicType, this._GraphicFilled == "Y", da, this._GraphicData, "");
        }
    }
}
