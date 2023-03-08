using System;

using Hyland.Unity;

namespace api.datecsa.UnityAPI
{
    public class ValidateKeywords
    {
        private Hyland.Unity.Application app = null;

        public bool AmountAboveThreshold(long DocumentID)
        {
            try
            {
                bool isAmountAboveThreshold = false;

                // Get the Document using the ID that is passed into the method. (Core). Check for null.
                Document doc = app.Core.GetDocumentByID(DocumentID);
                if (doc == null)
                {
                    throw new Exception("Could not find document with id: " + DocumentID);
                }
                // Get all of the Keyword Records from the document.
                KeywordRecordList keyRecList = doc.KeywordRecords;
                // Loop through the KeywordRecords on the document.
                foreach (KeywordRecord rec in keyRecList)
                {
                    // Loop through each keyword in the keyword record
                    foreach (Keyword key in rec.Keywords)
                    {
                        // Check if the KeywordType name for the current keyword is "Loan Amount".
                        if (key.KeywordType.Name == "Loan Amount")
                        {
                            // If so, check if the value of loan amount is greater than 10,000. (x>10000)
                            if (key.CurrencyValue > 10000)
                            {
                                // If so, set isAmountAboveThreshold to true. 
                                isAmountAboveThreshold = true;
                            }
                        }
                    }
                }

                return isAmountAboveThreshold;
            }


            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("The Unity API session could not be found, please reconnect.", ex);
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("There was a Unity API exception.", ex);
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("There was an unknown exception.", ex);
            }


        }

        public int SignerInfoMissing(long DocumentID)
        {
            try
            {
                int numRecordsInvalid = 0;
                // Get the Document from the Core. Check for null.
                Document doc = app.Core.GetDocumentByID(DocumentID);
                if (doc == null)
                {
                    throw new Exception("Could not find document with id: " + DocumentID);
                }
                // Loop through the Keyword Records on the document.
                foreach (KeywordRecord keyRec in doc.KeywordRecords)
                {
                    // Check if the KeywordRecordType RecordType is MultiInstance and check that KeywordRecordType name is "Signer Information".
                    if ((keyRec.KeywordRecordType.RecordType == RecordType.MultiInstance) && (keyRec.KeywordRecordType.Name == "Signer Information"))
                    {
                        // If so, loop through each Keyword in the record. 
                        foreach (Keyword key in keyRec.Keywords)
                        {
                            // Check if the keyword IsBlank.
                            if (key.IsBlank)
                            {
                                // If so, increment the numRecordsInvalid.
                                numRecordsInvalid++;
                            }
                        }
                    }
                }
                return numRecordsInvalid;
            }
            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("The Unity API session could not be found, please reconnect.", ex);
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("There was a Unity API exception.", ex);
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("There was an unknown exception.", ex);
            }
        }


        public ValidateKeywords(Hyland.Unity.Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
            }
            this.app = app;

        }
    }
}
