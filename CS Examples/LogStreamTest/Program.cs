using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using rzdcxLib;
using System.Runtime.InteropServices;
using System.Threading;

namespace LogStreamTest
{
    class StreamWrapper : IStream
    {
        public StreamWrapper(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Can't wrap null stream.");
            this.stream = stream;
        }

        private Stream stream;

        public void Clone(out IStream ppstm)
        {
            throw new NotImplementedException();
        }
        public void Commit(uint grfCommitFlags)
        {
            throw new NotImplementedException();
        }
        public void LockRegion(_ULARGE_INTEGER libOffset, _ULARGE_INTEGER cb, uint dwLockType)
        {
            throw new NotImplementedException();
        }
        public void RemoteCopyTo(IStream pstm, _ULARGE_INTEGER cb, out _ULARGE_INTEGER pcbRead, out _ULARGE_INTEGER pcbWritten)
        {
            throw new NotImplementedException();
        }
        public void RemoteRead(out byte pv, uint cb, out uint pcbRead)
        {
            throw new NotImplementedException();
        }
        public void RemoteSeek(_LARGE_INTEGER dlibMove, uint dwOrigin, out _ULARGE_INTEGER plibNewPosition)
        {
            throw new NotImplementedException();
        }
        public unsafe void RemoteWrite(ref byte pv, uint cb, out uint pcbWritten)
        {
            fixed (byte* b = &pv)
            {
                for (int i = 0; i < cb; i++)
                    stream.WriteByte(b[i]);
                pcbWritten = cb;
                //throw new NotImplementedException(); 
            }
        }
        public void Revert()
        {
            throw new NotImplementedException();
        }
        public void SetSize(_ULARGE_INTEGER libNewSize)
        {
            throw new NotImplementedException();
        }
        public void Stat(out tagSTATSTG pstatstg, uint grfStatFlag)
        {
            throw new NotImplementedException();
        }
        public void UnlockRegion(_ULARGE_INTEGER libOffset, _ULARGE_INTEGER cb, uint dwLockType)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TestECHOLogStream();
            TestECHOFileLog();

            TestQUERYFileLog();
            TestQUERYLogStream();

            TestRETRIEVEFileLog();
            TestRETRIEVELogStream();
        }
        static void TestECHOLogStream()
        {
            FileStream fs = new FileStream("TestECHOLogStream.log", FileMode.Create);
            StreamWrapper w = new StreamWrapper(fs);
            DCXAPP app = new DCXAPP();
            app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
            app.LogStream = w;

            for (int j = 0; j < 50; j++)
            {
                string timestamp = DateTime.Now.ToString();
                app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "\r\n\r\nECHO test (" + j + ") - [" + timestamp + "]");
                DCXREQ r = new DCXREQ();
                r.Echo("TEST", "DSRSVC", "localhost", 104);
            }

            app.FlushLog();
            app.StopLogging();
            fs.Close();
            fs.Dispose();
        }

        static void TestECHOFileLog()
        {
            DCXAPP app = new DCXAPP();
            app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
            app.StartLogging("TestECHOFileLog.log");

            for (int j = 0; j < 50; j++)
            {
                string timestamp = DateTime.Now.ToString();
                app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "\r\n\r\nECHO test (" + j + ") - [" + timestamp + "]");
                DCXREQ r = new DCXREQ();
                r.Echo("TEST", "DSRSVC", "localhost", 104);
            }

            app.StopLogging();
        }

        static void TestQUERYLogStream()
        {
            FileStream fs = new FileStream("TestQUERYLogStream.log", FileMode.Create);
            StreamWrapper w = new StreamWrapper(fs);
            DCXAPP app = new DCXAPP();
            app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
            app.LogStream = w;

            app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "\r\n\r\nQUERY test - [" + DateTime.Now.ToString() + "]");

            DCXREQ requester = new DCXREQ();
            requester.Query("TEST", "DSRSVC", "localhost", 104, "1.2.840.10008.5.1.4.1.2.1.1", BuildSearchQueryObject());

            app.FlushLog();
            app.StopLogging();
            fs.Close();
            fs.Dispose();
        }

        static void TestQUERYFileLog()
        {
            DCXAPP app = new DCXAPP();
            app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
            app.StartLogging("TestQUERYFileLog.log");

            app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "\r\n\r\nQUERY test - [" + DateTime.Now.ToString() + "]");

            DCXREQ requester = new DCXREQ();
            requester.Query("TEST", "DSRSVC", "localhost", 104, "1.2.840.10008.5.1.4.1.2.1.1", BuildSearchQueryObject());

            app.StopLogging();
        }

        static private DCXOBJ BuildSearchQueryObject()
        {
            //Retrieve all patients, 
            //change patient name or patient id in the query to get less data

            DCXOBJ o = new DCXOBJ();
            DCXELM e = new DCXELM();
            e.Init((int)DICOM_TAGS_ENUM.QueryRetrieveLevel);
            e.Value = "PATIENT";
            o.insertElement(e);
            e.Init((int)DICOM_TAGS_ENUM.PatientsName);
            e.Value = "*";
            o.insertElement(e);
            e.Init((int)DICOM_TAGS_ENUM.patientID);
            e.Value = "*";
            o.insertElement(e);
            return o;
        }

        static void TestRETRIEVELogStream()
        {
            FileStream fs = new FileStream("TestRETRIEVELogStream.log", FileMode.Create);
            StreamWrapper w = new StreamWrapper(fs);
            DCXAPP app = new DCXAPP();
            app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
            app.LogStream = w;

            app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "\r\n\r\nRETRIEVE test - [" + DateTime.Now.ToString() + "]");

            MovePatient();

            app.FlushLog();
            app.StopLogging();
            fs.Close();
            fs.Dispose();
        }

        static void TestRETRIEVEFileLog()
        {
            DCXAPP app = new DCXAPP();
            app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
            app.StartLogging("TestRETRIEVEFileLog.log");

            app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "\r\n\r\nRETRIEVE test - [" + DateTime.Now.ToString() + "]");

            MovePatient();

            app.StopLogging();
        }

        static private void MovePatient()
        {
            //For testing we just retrieve files of some patient existing in our PACS DB
            //using running DICOM listener of our MODALIZER+ as accepter 
            //(defined as DICOMApplication in our PACS DB)

            //You of course need to use your accepter instead of "MODALIZERPlus"

            DCXREQ r = new DCXREQ();
            r.MovePatient("RZDCXTEST", "DSRSVC", "localhost", 104, "BARMPLUS",
                "*", "123*");
        }
    }
}
