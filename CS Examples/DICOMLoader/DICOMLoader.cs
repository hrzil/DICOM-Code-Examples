using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using rzdcxLib;

namespace DICOMLoader
{
    static class Utils
    {
        /// <summary>
        /// Retrieve from DCXOBJ instance given DICOM tag value as string
        /// If tag doesn't exist - return empty string
        /// </summary>
        /// <param name="obj">DICOM object</param>
        /// <param name="ElementTag">Tag to extract</param>
        /// <returns>Tag value</returns>
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
    }
    /// <summary>
    /// Method that must be called by GUI or other class on each DICOM file loading
    /// </summary>
    /// <param name="obj">Opened DICOM object</param>
    public delegate void FileLoadHandler(DCXOBJ obj);

    /// <summary>
    /// Fired when trying to open a non-DICOM file
    /// </summary>
    /// <param name="fileInfo"></param>
    public delegate void NotDICOMFileHandler(FileInfo fileInfo, int errCode, string message);

    public delegate void LoadFileErrorHandler(FileInfo fileInfo, Exception e);

    /// <summary>
    /// Fired when a DICOMDIR file is found and used
    /// </summary>
    public delegate void DICOMDIRFoundHandler();

    public delegate void DICOMDIR_PatientNodeHandler (DCXOBJ patientObj);
    public delegate void DICOMDIR_StudyNodeHandler   (DCXOBJ patientObj, DCXOBJ studyObj);
    public delegate void DICOMDIR_SeriesNodeHandler  (DCXOBJ patientObj, DCXOBJ studyObj, DCXOBJ seriesObj);
    public delegate void DICOMDIR_ImageNodeHandler   (DCXOBJ patientObj, DCXOBJ studyObj, DCXOBJ seriesObj, DCXOBJ imageObj);

    public interface DICOMLoaderEvents
    {
        event FileLoadHandler             OnFileLoad;
        event NotDICOMFileHandler         OnNotDICOMFile;
        event LoadFileErrorHandler        OnLoadFileError;
        event DICOMDIRFoundHandler        OnDICOMDIRFound;
        event DICOMDIR_PatientNodeHandler OnPatientNode;
        event DICOMDIR_StudyNodeHandler   OnStudyNode;
        event DICOMDIR_SeriesNodeHandler  OnSeriesNode;
        event DICOMDIR_ImageNodeHandler   OnImageNode;
    }

    /// <summary>
    /// Class that might load DICOM objects by given list of the files paths,
    /// or by scanning given directory, or by extracting list of the files from DICOMDIR
    /// </summary>
    public class DICOMLoader : DICOMLoaderEvents
    {
        public event FileLoadHandler OnFileLoad;
        public event NotDICOMFileHandler OnNotDICOMFile;
        public event LoadFileErrorHandler OnLoadFileError;
        public event DICOMDIRFoundHandler OnDICOMDIRFound;
        public event DICOMDIR_PatientNodeHandler OnPatientNode;
        public event DICOMDIR_StudyNodeHandler OnStudyNode;
        public event DICOMDIR_SeriesNodeHandler OnSeriesNode;
        public event DICOMDIR_ImageNodeHandler OnImageNode;

        /// <summary>
        /// Try to load files from given folder
        /// </summary>
        /// <param name="Folder">Folder to scan for DICOM files</param>
        /// <param name="updateEvent">Event to inform GUI or other class about opened DICOM file</param>
        public void LoadDirectory(string Folder)
        {
            string DDIR = Path.Combine(Folder, "DICOMDIR");
            bool ReadFilesAsList = true;
            if (File.Exists(DDIR))
            {
                this.OnDICOMDIRFound?.Invoke();

                //Folder contains DICOMDIR file - open as DICOMDIR
                List<string> DDfiles = LoadDICOMDIR(DDIR, Folder);
                if (DDfiles.Count > 0)
                {
                    //DICOMDIR contains required data about files, load them
                    LoadFiles(DDfiles.ToArray());
                    ReadFilesAsList = false;
                }
            }

            if (ReadFilesAsList)
            {
                //Folder doesn't contain valid DICOMDIR - scan folder for all files and open DICOMs
                string[] allFiles = Directory.GetFiles(Folder, "*.*", SearchOption.AllDirectories);
                LoadFiles(allFiles.ToArray());
            }
        }

        /// <summary>
        /// Loads all patients/studies/series/images data from DICOMDIR file
        /// </summary>
        /// <param name="DDIR">Full path to DICOMDIR file</param>
        /// <param name="Folder">Folder which contains DICOMDIR file (for building of full path to each image file)</param>
        private List<string> LoadDICOMDIR(string DDIR, string Folder)
        {
            List<String> files = new List<string>();

            DCXDICOMDIR dir = new DCXDICOMDIR();
            dir.Init(DDIR);

            DCXOBJIterator studIt = null;
            DCXOBJ ObjSt = null;

            DCXOBJIterator serIt = null;
            DCXOBJ ObjSer = null;

            DCXOBJIterator imgIt = null;
            DCXOBJ ObjImg = null;

            DCXOBJ ObjPat = null;

            //Get patients from DICOMDIR
            DCXOBJIterator patIt = dir.getPatientIterator();
            string PatientID;
            string StudyInstanceUID;
            string SeriesInstanceUID;
            for (; !patIt.AtEnd(); patIt.Next())
            {
                ObjPat = patIt.Get();
                this.OnPatientNode?.Invoke(ObjPat);
                PatientID = Utils.GetElementValueAsString(ObjPat, (int)DICOM_TAGS_ENUM.patientID);

                //Get studies by patient from DICOMDIR
                studIt = dir.getStudyIterator(PatientID);
                for (; !studIt.AtEnd(); studIt.Next())
                {
                    ObjSt = studIt.Get();
                    this.OnStudyNode?.Invoke(ObjPat, ObjSt);
                    StudyInstanceUID = Utils.GetElementValueAsString(ObjSt, (int)DICOM_TAGS_ENUM.studyInstanceUID);

                    //Get series by patient/study from DICOMDIR
                    serIt = dir.getSeriesIterator(PatientID, StudyInstanceUID);
                    for (; !serIt.AtEnd(); serIt.Next())
                    {
                        ObjSer = serIt.Get();
                        this.OnSeriesNode?.Invoke(ObjPat, ObjSt, ObjSer);
                        SeriesInstanceUID = Utils.GetElementValueAsString(ObjSer, (int)DICOM_TAGS_ENUM.seriesInstanceUID);

                        //Get images by patient/study/series from DICOMDIR
                        imgIt = dir.getSeriesLeafIterator(PatientID, StudyInstanceUID, SeriesInstanceUID);
                        for (; !imgIt.AtEnd(); imgIt.Next())
                        {
                            ObjImg = imgIt.Get();
                            this.OnImageNode?.Invoke(ObjPat, ObjSt, ObjSer, ObjImg);

                            string filePath = Folder + Path.DirectorySeparatorChar + Utils.GetElementValueAsString(ObjImg, (int)DICOM_TAGS_ENUM.ReferencedFileID);
                            if (filePath.Substring(filePath.Length - 3, 3).ToUpper() == "DCM")
                            {
                                //Patch for SR reports
                                filePath = filePath.Substring(0, filePath.Length - 3) + ".DCM";
                            }
                            if (!files.Contains(filePath))
                                files.Add(filePath);
                        }
                    }

                }
            }

            // Free memory (if you don't want to wait for the Garbage Collector to do this)
            if (patIt != null)
                Marshal.ReleaseComObject(patIt);
            if (ObjPat != null)
                Marshal.ReleaseComObject(ObjPat);
            if (imgIt != null)
                Marshal.ReleaseComObject(imgIt);
            if (ObjImg != null)
                Marshal.ReleaseComObject(ObjImg);
            if (serIt != null)
                Marshal.ReleaseComObject(serIt);
            if (ObjSer != null)
                Marshal.ReleaseComObject(ObjSer);
            if (studIt != null)
                Marshal.ReleaseComObject(studIt);
            if (ObjSt != null)
                Marshal.ReleaseComObject(ObjSt);
            if (dir != null)
                Marshal.ReleaseComObject(dir);

            //Return list of all DICOM files listed in the DICOMDIR
            return files;
        }

        /// <summary>
        /// Open each file from the list and call some update event to inform GUI or other class
        /// </summary>
        /// <param name="listOfFiles">List of the full paths</param>
        /// <param name="updateEvent">Update event</param>
        public void LoadFiles(string[] listOfFiles)
        {
            FileInfo finf;
            string file;
            for (int i = 0; i < listOfFiles.Length; i++)
            {
                file = listOfFiles[i];
                finf = new FileInfo(file);
                if (File.Exists(file))
                {
                    //Open DICOM file and read all required data from it
                    DCXOBJ dicomObject = new DCXOBJ();
                    try
                    {
                        dicomObject.openFile(file);
                        this.OnFileLoad?.Invoke(dicomObject);
                    }
                    catch(COMException exep)
                    {
                        this.OnNotDICOMFile?.Invoke(finf, exep.ErrorCode, exep.Message);
                    }
                    catch(Exception exep)
                    {
                        this.OnLoadFileError?.Invoke(finf, exep);
                    }
                }
            }
        }
    }
}
