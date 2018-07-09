using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Automation.APITests.LG20
{
    interface IApiPage
    {
        string GetAllDocuments(IDictionary<string, string> parms);
        string DeleteDocumentInfo(IDictionary<string, string> parms);
        string GetDoesDocumentExists(IDictionary<string, string> parms);
        string GetDocumentInfoModifiedById(IDictionary<string, string> parms);
        string GetDocumentInfoById(IDictionary<string, string> parms);
        string PutDocumentInfoById(IDictionary<string, string> parms);
        string PostDocumentInfoById(IDictionary<string, string> parms);
        string DeleteDocumentInfoById(IDictionary<string, string> parms);
        string PutBatchReplaceDocuments(IDictionary<string, string> parms);
        string PostBatchUpdatesDocuments(IDictionary<string, string> parms);
        string GetPerformSql(IDictionary<string, string> parms);
        string GetPerformSqlExToken(IDictionary<string, string> parms);


    }
}
