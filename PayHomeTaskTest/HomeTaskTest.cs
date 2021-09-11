using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PayHomeTask;
using System.Collections.Generic;
/*
 * 
 *                 ****************This is the unit test of the PayHomeTask*************
 *
 * PayHomeTask project will fetch todo details of given user from API using http://jsonplaceholder.typicode.com.
 * This programs allows the user to enter the userid as input and provides total number of open and closed todo and details 
 * of all open todo task(title and id number) for the userid.
 * 
 * PayHomeTaskTest will do the unit testing on PayHomeTask
 */
namespace PayHomeTaskTest
{
    [TestClass]
    public class HomeTaskTest
    {
       //This test will test for zero value for true todo 
        [TestMethod]
        public void VerifyOpenCloseTodoCount()
        {
            var SampleDataAsJson = new List<UserData>(); //test data. A sample JSON like data having no open and 2 closed todo
            var ExpectedOpenTodoCount = 2; //  no false status of complete
            var ExpectedClosedTodoCount = 0; // 2 for only 2 true status of complete
            string userid = "1";
            SampleDataAsJson.Add(new UserData() { userId = "1", Id = "281", Title = "East of Eden", Completed = false });
            SampleDataAsJson.Add(new UserData() { userId = "1", Id = "282", Title = "The Sun Also Rises by Ernest Hemingway.", Completed = false });
            HomeTask.FinalResult(SampleDataAsJson,userid);
            Assert.AreEqual(ExpectedOpenTodoCount, HomeTask.openTodoCount);
            Assert.AreEqual(ExpectedClosedTodoCount, HomeTask.closedTodoCount);


        }

        ////This test will test for user has zero open todo details task, the openTodoDetails should provide be empty and nothing to be added as no false todo.
       [TestMethod]
        public void VerifyOpenDetailsForNoOpenTodoForAUser()
        {
            var SampleDataAsJson1 = new List<UserData>(); //test data. A sample JSON like data having no open and 2 closed todo
            List<UserData> ExpectedData = new List<UserData>();
            string userid = "1";
            SampleDataAsJson1.Add(new UserData() { userId = "1", Id = "281", Title = "East of Eden", Completed = true });
            SampleDataAsJson1.Add(new UserData() { userId = "1", Id = "282", Title = "The Sun Also Rises by Ernest Hemingway.", Completed = true });
           // resultopenTodoDetials.Add(new UserData() { userId = "1", Id = "281", Title = "East of Eden", Completed = false });
            HomeTask.FinalResult(SampleDataAsJson1, userid);

            Assert.AreEqual(ExpectedData.Count, HomeTask.openTodoDetails.Count);
           
        }
       
        ////This test will test for user has some open details todo task
        [TestMethod]
        public void VerifyOpenDetailsForOpenTodoForAUser()
        {
            var SampleDataAsJson3 = new List<UserData>(); //test data. A sample JSON like data having no open and 2 closed todo
            List<UserData> ExpectedData1 = new List<UserData>();
            string userid = "1";
            SampleDataAsJson3.Add(new UserData() { userId = "1", Id = "281", Title = "East of Eden", Completed = false });
            SampleDataAsJson3.Add(new UserData() { userId = "1", Id = "282", Title = "East of Hawai", Completed = false });
            SampleDataAsJson3.Add(new UserData() { userId = "1", Id = "283", Title = "The Sun  Rises by Ernest Hemingway.", Completed = true });
            SampleDataAsJson3.Add(new UserData() { userId = "1", Id = "284", Title = "The Sun Also Rises by Ernest Hemingway.", Completed = false });
            
            //adding only false open todo details to expected opentododetails
            ExpectedData1.Add(new UserData() { userId = "1", Id = "281", Title = "East of Eden", Completed = false });
            ExpectedData1.Add(new UserData() { userId = "1", Id = "282", Title = "East of Hawai", Completed = false });
            ExpectedData1.Add(new UserData() { userId = "1", Id = "284", Title = "The Sun Also Rises by Ernest Hemingway.", Completed = false });


            // resultopenTodoDetials.Add(new UserData() { userId = "1", Id = "281", Title = "East of Eden", Completed = false });
            HomeTask.FinalResult(SampleDataAsJson3, userid);

            Assert.AreEqual(ExpectedData1.Count, HomeTask.openTodoDetails.Count);

        }

    }

}
