using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using rzdcxLib;

namespace InsertPrivateSequences
{
    class Program
    {
        private static List<ProceededSequence> ProceededSequence;

        static void Main(string[] args)
        {
            string ExeDir = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;
            string PathToSrcXML = Path.Combine(ExeDir, "DICOMSource.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(PathToSrcXML);

            string PathToWriteDICOM = Path.Combine(ExeDir, "DICOMOutput.dcm");

            SaveStructure(doc, PathToWriteDICOM);
        }

        public static void SaveStructure(XmlDocument doc, string PathToDICOM)
        {
            ProceededSequence = new List<ProceededSequence>();

            DCXOBJ obj = new DCXOBJ();

            int TagtoInsert;
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "Item" && (GetAttributeText(node, "IsMeta") != "true"))
                {
                    TagtoInsert = Convert.ToInt32(GetAttributeText(node, "Tag"));
                    if (TagtoInsert != 2145386512) // pixel data
                    {
                        InsertElement(obj, node);
                    }
                }
                if (node.Name == "Sequence")
                {
                    InsertSequenceToDICOM(node, obj, null, doc);
                }

            }
            obj.saveFile(PathToDICOM);
        }

        private static void InsertSequenceToDICOM(XmlNode SeqNode, DCXOBJ obj, XmlNode ParentNode, XmlDocument doc)
        {
            string tag = GetAttributeText(SeqNode, "Tag");

            //Check that it's not second etc. sequence of the same parent and with the same tag, they were inserted already
            string ParId = ParentNode == null ? "Main" : GetAttributeText(ParentNode, "ID");
            foreach (ProceededSequence sc in ProceededSequence)
            {
                if (sc.ID == ParId && sc.Tag == tag)
                    return;
            }


            //Find all sequences with the same tag to insert them into one iterator
            List<XmlNode> allSeqNodes = new List<XmlNode>();
            XmlNodeList parChilds;
            if (ParentNode != null)
            {
                parChilds = ParentNode.ChildNodes;
            }
            else
            {
                parChilds = doc.DocumentElement.ChildNodes;
            }
            foreach (XmlNode node in parChilds)
            {
                if (node.Name == "Sequence" && GetAttributeText(node, "Tag") == tag)
                {
                    allSeqNodes.Add(node);
                }
            }

            ProceededSequence newSeq = new ProceededSequence();
            newSeq.ID = ParId;
            newSeq.Tag = tag;
            ProceededSequence.Add(newSeq);

            if (allSeqNodes.Count == 0)
                return;

            DCXOBJIterator objects_sq = new DCXOBJIterator();

            foreach (XmlNode sNode in allSeqNodes)
            {
                InsertSequenceNode(objects_sq, sNode);
            }

            ////Remove comment to check that it works if replace private tag by one from our enum (GraphicLayerSequence here)
            //string NodeName;
            //try
            //{
            //    NodeName = ((DICOM_TAGS_ENUM)Convert.ToInt32(tag)).ToString();

            //    if (Char.IsDigit(NodeName[0]))
            //        tag = "7340128";
            //}
            //catch (Exception)
            //{
            //    tag = "7340128";
            //}

            DCXELM e = new DCXELM();
            e.Init(Convert.ToInt32(tag));
            e.ValueRepresentation = VR_CODE.VR_CODE_SQ;
            e.Value = objects_sq;
            obj.insertElement(e);

        }

        private static void InsertSequenceNode(DCXOBJIterator objects_sq, XmlNode sNode)
        {
            DCXOBJ seq_item = new DCXOBJ();

            foreach (XmlNode node in sNode.ChildNodes)
            {
                if (node.Name == "Item")
                {
                    InsertElement(seq_item, node);

                }
                if (node.Name == "Sequence")
                {
                    InsertSequenceToDICOM(node, seq_item, sNode, null);
                }

            }

            objects_sq.Insert(seq_item);
        }

        private static string GetAttributeText(XmlNode inXmlNode, string name)
        {
            XmlAttribute attr = (inXmlNode.Attributes == null ? null : inXmlNode.Attributes[name]);
            return attr == null ? null : attr.Value;
        }

        private static void InsertElement(DCXOBJ obj, XmlNode node)
            
        {
            //DICOM_TAGS_ENUM tag, string Value = "")

            //(DICOM_TAGS_ENUM)Convert.ToInt32(GetAttributeText(node, "Tag")),
            //                                     node.InnerText.Trim());

            DCXELM el = new DCXELM();

            el.Init(Convert.ToInt32(GetAttributeText(node, "Tag")));
            
            string CaseOriginCode = "VR_CODE_" + GetAttributeText(node, "VR");
            el.ValueRepresentation = (VR_CODE)Enum.Parse(typeof(VR_CODE), CaseOriginCode);

            el.Value = node.InnerText.Trim();

            obj.insertElement(el);

        }
    }

    public class ProceededSequence
    {
        public string ID;
        public string Tag;
    }
}
