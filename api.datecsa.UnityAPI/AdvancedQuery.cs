using System;
using System.Collections.Generic;
using System.Data;
using Hyland.Unity;

namespace api.datecsa.UnityAPI
{
    public class AdvancedQuery
    {
        private Hyland.Unity.Application app = null;
        DataTable resultsTable = new DataTable("QueryResults");
        DataTable resultsTable2 = new DataTable("QueryResults");

        public DataTable AdvancedDocumentQuery(string CustomQueryName, List<Keyword> keywordList)
        {
            try
            {
                // Find the custom query and check for null.
                CustomQuery custQuery = app.Core.CustomQueries.Find(CustomQueryName);
                if (custQuery == null)
                {
                    throw new Exception("Custom Query not found");
                }

                // Create the document query.
                DocumentQuery dq = app.Core.CreateDocumentQuery();
                // Add the custom query to the document query.
                dq.AddCustomQuery(custQuery);
                // Loop through the keywords in the list and add them to the query. These keywords have the same type so be sure to use the Or relation.
                foreach (Keyword key in keywordList)
                {
                    dq.AddKeyword(key, KeywordOperator.Equal, KeywordRelation.Or);
                }
                // Execute the Query
                QueryResult queryResult = dq.ExecuteQueryResults(1000);

                // Loop through display column configurations and add columns to data table.
                foreach (DisplayColumnConfiguration dispColConfig in queryResult.DisplayColumnConfigurations)
                {
                    string heading = dispColConfig.Heading; // Get Heading Value
                    resultsTable.Columns.Add(heading);
                }

                // Loop through the results to add rows to the data table.
                foreach (QueryResultItem resultItem in queryResult.QueryResultItems)
                {
                    DataRow row = resultsTable.NewRow();
                    foreach (DisplayColumn dispCol in resultItem.DisplayColumns)
                    {
                        string rowValue = dispCol.ToString(); // Get Row Value
                        row[dispCol.Configuration.Heading] = rowValue;
                    }
                    resultsTable.Rows.Add(row);
                }
                return resultsTable;
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

        public DataTable AdvancedDocumentQuery(string DocumentTypeName, List<Keyword> StandAloneKeywords, List<Keyword> Signer1Info, List<Keyword> Signer2Info)
        {
            try
            {
                // Find the DocumentType and check for null. (DocumentTypeName is passed into this method.)
                DocumentType docType = app.Core.DocumentTypes.Find(DocumentTypeName);
                if (docType == null)
                {
                    throw new Exception("Document Type not found");
                }
                // Create the document query.
                DocumentQuery dq = app.Core.CreateDocumentQuery();
                // Add the DocType to the document query.
                dq.AddDocumentType(docType);
                // Loop through the standalone keywords in the list and add them to the query. These keywords have the same type so be sure to use the Or relation.
                foreach (Keyword keyword in StandAloneKeywords)
                {
                    dq.AddKeyword(keyword, KeywordOperator.Equal, KeywordRelation.Or);
                }
                // Find the KeywordRecordType for "Signer Information". Check for null.
                KeywordRecordType keyRecType = app.Core.KeywordRecordTypes.Find("Signer Information");
                if (keyRecType == null)
                {
                    throw new Exception("The keyword record type could not be found.");
                }
                // Create the QueryKeywordRecord from the KeywordRecordType.
                QueryKeywordRecord queryKeyRecSigner1 = keyRecType.CreateQueryKeywordRecord();
                // Loop through Signer1Info and add the keywords to the QueryKeyword record.
                foreach (Keyword keyword in Signer1Info)
                {
                    queryKeyRecSigner1.AddKeyword(keyword);
                }
                // Add the query keyword record to the document query. Be sure to use OR operation.
                dq.AddQueryKeywordRecord(queryKeyRecSigner1, KeywordRelation.Or);
                // Create a second instance of QueryKeywordRecord from the KeywordRecordType.
                QueryKeywordRecord queryKeyRecSigner2 = keyRecType.CreateQueryKeywordRecord();
                // Loop through Signer2Info and add the keywords to the second instance of our QueryKeyword record.
                foreach (Keyword keyword in Signer2Info)
                {
                    queryKeyRecSigner2.AddKeyword(keyword);
                }
                // Add the second query keyword record to the document query. There are no more query keyword records so an operator is not required.
                dq.AddQueryKeywordRecord(queryKeyRecSigner2);
                // Add DisplayColumn to doc query for Document ID.
                dq.AddDisplayColumn(DisplayColumnType.DocumentID);
                // Add DisplayColumn to doc query for Document Type Name.
                dq.AddDisplayColumn(DisplayColumnType.DocumentTypeName);
                // Add DisplayColumn to doc query for Document Name.
                dq.AddDisplayColumn(DisplayColumnType.DocumentName);
                // Find the keyword type for "Loan Account #". Check for null.
                KeywordType loanAccountKeyType = app.Core.KeywordTypes.Find("Loan Account #");
                if (loanAccountKeyType == null)
                {
                    throw new Exception("Could not find keyword type: Loan Account #");
                }
                // Add the keyword type display column to the document query.
                dq.AddDisplayColumn(loanAccountKeyType); //add a keyword display column

                //Execute Query
                QueryResult queryResult = dq.ExecuteQueryResults(1000);

                // Loop through display column configuration and add columns to data table.
                foreach (DisplayColumnConfiguration dispColConfig in queryResult.DisplayColumnConfigurations)
                {
                    string heading = dispColConfig.Heading; // Get Heading Value
                    resultsTable2.Columns.Add(heading);
                }

                // Loop through the results to add rows to the data table.
                foreach (QueryResultItem resultItem in queryResult.QueryResultItems)
                {
                    DataRow row = resultsTable2.NewRow();
                    foreach (DisplayColumn dispCol in resultItem.DisplayColumns)
                    {
                        string rowValue = dispCol.ToString(); // Get Row Value
                        row[dispCol.Configuration.Heading] = rowValue;
                    }
                    resultsTable2.Rows.Add(row);
                }
                return resultsTable2;
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

        public AdvancedQuery(Hyland.Unity.Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
            }
            this.app = app;
        }
    }
}
