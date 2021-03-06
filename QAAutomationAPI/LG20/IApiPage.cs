﻿using System;
using System.Collections.Generic;

namespace QA.Automation.APITests.LG20
{
    public interface IApiPage : IDisposable
    {
        string GetAuthInfo(string serviceUrl, string userName, string passWord);
        string GetAuthInfo(string serviceUrl);
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

        void DeleteItemsFromApi();

        string ServiceName { get; }

    }
}
