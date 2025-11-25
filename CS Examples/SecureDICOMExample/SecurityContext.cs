using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecureDICOMExample
{
    /// <summary>
    /// TLS certificates can be stored in windows in one of two store locations:
    /// + the Local Machine store, available for all users.
    /// + the Current User store, available for the current user only.
    /// </summary>
    internal enum StoreType
    {
        LocalMachine=0,
        CurrentUser=1
    }

    /// <summary>
    /// Besides encryption and decryption of the communication,
    /// We can verify the other side (make sure we know the cert holder)
    /// by one of the following methods:
    /// </summary>
    internal enum VerificationMethod
    {
        None=0,         // Don't verify at all
        TrustChain=1,   // Check if the cert issuer is trusted
        Thumbprints=2   // Compare other side cert to a list of allowed certs
    }

    /// <summary>
    /// This class demonstrates how to load a certificate to a DCXSEC object 
    /// </summary>
    internal class SecurityContext
    {
        internal StoreType Store
        {
            get;
            set;
        }
        internal VerificationMethod VerificationMethod
        {
            get;
            set;
        }
        internal string SubjectName
        {
            get;
            set;
        }
        internal string Thumbprint
        {
            get;
            set;
        }
        internal string AcceptedThumbprints
        {
            get;
            set;
        }
        internal bool MutualAuthentication
        {
            get;
            set;
        }

        /// <summary>
        /// This function translates a SecurityContext object into DCXSEC
        /// This is a full example of configuring a DCXSEC object.
        /// </summary>
        /// <returns></returns>
        internal rzdcxLib.DCXSEC ToDCXSEC()
        {
            // create a DCXSEC object
            rzdcxLib.DCXSEC sec = new rzdcxLib.DCXSEC();

            // select a certificate store:
            // Local Machine cert store - available for every user that logs on the machine
            if (this.Store == StoreType.LocalMachine)
                sec.CertStore = rzdcxLib.DCXSEC_CERT_STORE.DCXSEC_CERT_STORE_LOCAL_COMPUTER;
            // Current User cert store - available only for the currently logged user on the machine
            else
                sec.CertStore = rzdcxLib.DCXSEC_CERT_STORE.DCXSEC_CERT_STORE_CURRENT_USER;

            // select a verification Method:
            // Trust Chain Verification checks whether the issuer is trusted
            // (available in Trusted Root Certification Authority)
            if (this.VerificationMethod == VerificationMethod.TrustChain)
                sec.VerificationMethod = rzdcxLib.DCXSEC_VERIFICATION_METHOD.DCXSEC_VERIFICATION_METHOD_TRUST_CHAIN;
            // Thumbprint verification matches the thumbprint of the certificate
            // of the other side to a list of known cert thumbprint.
            else if (this.VerificationMethod == VerificationMethod.Thumbprints)
                sec.VerificationMethod = rzdcxLib.DCXSEC_VERIFICATION_METHOD.DCXSEC_VERIFICATION_METHOD_THUMBPRINT;
            // No verifixcation will be done, just encription
            else
                sec.VerificationMethod = rzdcxLib.DCXSEC_VERIFICATION_METHOD.DCXSEC_VERIFICATION_METHOD_NONE;

            // If thumbprint verification was selected,
            // a ';' seperated list of accepted thumbprints
            // should be provided to the DCXSEC
            if (this.VerificationMethod == VerificationMethod.Thumbprints 
                && this.AcceptedThumbprints != null && 
                this.AcceptedThumbprints.Length > 0)
                sec.AcceptedThumbprints = this.AcceptedThumbprints;

            // A certificate can be selected and loaded to a DCXSEC in two ways:
            // Either by the subject name (CN property of the Subject attribute).
            // Or by the certificate's thumbprint (aka signature).
            // There is no need to set both of them, once the thumbprint is set the subject name
            // is automatically loaded to the DCXSEC and vice versa
            if (this.SubjectName != null && this.SubjectName.Trim().Length > 0)
                sec.CertSubjectName = this.SubjectName.Replace("CN=","");
            else
                sec.CertThumbprint = this.Thumbprint;

            // A client (the initiator of the call) should always verify cert of the server it called.
            // A server may (or may not) attempt to verify the cert presented by the client.
            // If a SERVER needs to verify the client, Mutual Authentication should be set to true
            sec.MutualAuthentication = this.MutualAuthentication;

            return sec;
        }
    }
}
