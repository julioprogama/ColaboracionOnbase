using System;
using Hyland.Unity;
using Hyland.Unity.Extensions;

namespace api.datecsa.UnityAPI
{
    public class UpdateKeywords
    {
        private Hyland.Unity.Application app = null;

        public void Approve(long DocumentID)
        {
            string keywordValue = "Approved";
            try
            {
                // Get the document from the core using ID. The Document ID is passed in. Check for null.
                Document doc = app.Core.GetDocumentByID(DocumentID);
                if (doc == null)
                {
                    throw new Exception("Could not find document by id: " + DocumentID);
                }
                // Get the document type from the document object.
                DocumentType docType = doc.DocumentType;
                // Find the "Status" keyword type from the document type HINT: Use the FindKeywordType method from the KeywordRecordTypeList. Check for null.
                KeywordType statusKeyType = docType.KeywordRecordTypes.FindKeywordType("Status");
                if (statusKeyType == null)
                {
                    throw new Exception("The Keyword Type 'Status' could not be found or is not on the document type with name of: " + docType.Name);
                }
                // Create a keyword of this type with the value "Approved". Use the TryCreateKeyword extension method.
                Keyword statusKeyword = null;
                if (!statusKeyType.TryCreateKeyword(keywordValue, out statusKeyword))
                {
                    throw new Exception("Account number keyword could not be created.");
                }
                // Create the KeywordModifier from the document.
                KeywordModifier keyMod = doc.CreateKeywordModifier();
                // Find the keyword record that contains our keyword type.
                KeywordRecord keyRec = doc.KeywordRecords.Find(statusKeyType);
                // If the keyword record is not null.
                if (keyRec != null)
                {
                    // Find the old keyword from the record of the keyword type.
                    Keyword oldStatus = keyRec.Keywords.Find(statusKeyType);
                    // Update the keyword.
                    keyMod.UpdateKeyword(oldStatus, statusKeyword);
                }
                // Else if the keyword record is null.
                else
                {
                    // Add the new keyword.
                    keyMod.AddKeyword(statusKeyword);
                }
                // Lock the document. (Use a using statement to clean up lock.)
                using (DocumentLock documentLock = doc.LockDocument())
                {
                    // Check if the lock status is Already Locked (DocumentLockStatus enum).
                    if (documentLock.Status == DocumentLockStatus.AlreadyLocked)
                    {
                        throw new Exception("Document lock could not be obtained, lock obtained by another user or process.");
                    }
                    // If not locked, apply the changes to the database.
                    keyMod.ApplyChanges();
                }
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




        public void UpdateSignerInfo(long DocumentID, string SSNValue, string FieldToUpdate, string NewValue)
        {
            try
            {
                // Find the KeywordRecordType for "Signer Information" from the Core. Check for null.
                KeywordRecordType keyRecType = app.Core.KeywordRecordTypes.Find("Signer Information");
                if (keyRecType == null)
                {
                    throw new Exception("Keyword Record Type 'Signer Information' could not be found.");
                }
                // Find the KeywordType for FieldToUpdate from the KeywordRecordType. Check for null.
                KeywordType keyType = keyRecType.KeywordTypes.Find(FieldToUpdate);
                if (keyType == null)
                {
                    throw new Exception("The Keyword Type '" + FieldToUpdate + "' could not be found on the Keyword Record Type 'Singer Information'");
                }
                // Create a keyword of this type. Use the TryCreateKeyword extension method.(NewValue is passed in).
                Keyword newKeywordValue = null;
                if (!keyType.TryCreateKeyword(NewValue, out newKeywordValue))
                {
                    throw new Exception("Keyword for " + NewValue + " could not be created.");
                }
                // Get the document from the core. Check for null.
                Document doc = app.Core.GetDocumentByID(DocumentID);
                if (doc == null)
                {
                    throw new Exception("The document was not found.");
                }
                // Create the KeywordModifier from the document.
                KeywordModifier keywordMod = doc.CreateKeywordModifier();
                // Find all of the Keyword Records on the document that have the keyword record type. (KeywordRecordList)(FindAll)
                KeywordRecordList keyRecList = doc.KeywordRecords.FindAll(keyRecType);
                // Loop through all of the keyword records in this list.
                foreach (KeywordRecord keyRec in keyRecList)
                {
                    // Find the keyword in the record for "SSN".
                    Keyword key = keyRec.Keywords.Find("SSN");
                    // If the keyword is not null AND the keyword is not blank AND the keyword alphanumeric value is equal to SSN (passed in from method).
                    if ((key != null) && (!key.IsBlank) && (key.AlphaNumericValue == SSNValue))
                    {
                        // Create the EditableKeywordRecord from the KeywordRecord.
                        EditableKeywordRecord editableKeyRec = keyRec.CreateEditableKeywordRecord();
                        // Find the old keyword for the keyword type from the record.
                        Keyword oldKeyword = keyRec.Keywords.Find(keyType);
                        // Update the keyword within the editablekeywordrecord.
                        editableKeyRec.UpdateKeyword(oldKeyword, newKeywordValue);
                        // Update the Keyword Record for the Keyword Modifier.
                        keywordMod.UpdateKeywordRecord(editableKeyRec);
                    }
                }
                // Lock the document. (Use a using statement to clean up lock.)
                using (DocumentLock documentLock = doc.LockDocument())
                {
                    // Check if the lock status is Already Locked (DocumentLockStatus enum).
                    if (documentLock.Status == DocumentLockStatus.AlreadyLocked)
                    {
                        throw new Exception("Document lock could not be obtained, lock obtained by another user or process.");
                    }
                    // If not locked, apply the changes to the database.
                    keywordMod.ApplyChanges();
                }

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


        public UpdateKeywords(Hyland.Unity.Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
            }
            this.app = app;
        }
    }
}
