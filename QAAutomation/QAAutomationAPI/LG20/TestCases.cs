using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace QA.Automation.APITests.LG20
{
    [TestFixture]
    public class TestCases : ApiTestBase
    {
        private List<IApiPage> _items = new List<IApiPage>();

        public TestCases(string userUrl, string userName, string userPassword) : base(userName, userPassword)
        {
            //this.url = userUrl;
        }

        public TestCases()
        {
        }

        [SetUp]
        public void Init()
        {
        }


        //[TestCaseSource("items")]
        [Category("TestCase")]
        public void TCPlayList1(KeyValuePair<string, string> item)
        {
            //VSTS ID: 46 - Use Case 1:  Create a playlist

            // Login
            // Select Make PlayList
            // Enter playlist name
            // Select a channel
            // Save
            // Verify Channel is complete. 
        }

        public void TCAssestList2(KeyValuePair<string, string> item)
        {
            //VSTS ID: 74 - Use Case 4:  Uploading images and videos from Assets

            LGMPlayListsService playlist = new LGMPlayListsService(null);

            // Get data from data directory


            // Login
            // Select Assets from like
            // SElect UploadFiles
            // Select browser you computer
            // Select image or video
            // SElect Open
            // Close Upload files widget
            // Verify Channel is complete. 
        }

        public void TCAssestList3(KeyValuePair<string, string> item)
        {
            //VSTS ID: 75 = Use Case 5: Configuring Uploaded Image and Video Assets

            // Login
            // Select Assets from like
            // Select Edit icon fro any asset
            // Change the Asset name
            // Change the Asset description
            // Change/ add Asset Tag
            // Change the Asset Start Date
            // Change the Asset Expiration Date
            // SElect save
            // verify the assets has been changed. 
            // Select browser you computer
            // Select image or video
            // SElect Open
            // Close Upload files widget
            // Verify Channel is complete. 
        }

        [TearDown]
        public void WriteOut()
        {
            foreach (IApiPage api in _items)
            {
                api.DeleteItemsFromApi();
            }
        }
    }
}
