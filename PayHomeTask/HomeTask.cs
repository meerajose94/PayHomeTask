using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
/*PayHomeTask project will fetch todo details of given user from API using http://jsonplaceholder.typicode.com.
 * This programs allows the user to enter the userid as input and provides total number of open and closed todo and details 
 * of all open todo task(title and id number) for the userid. 
 */

namespace PayHomeTask
{
    class HomeTask
    {
        static void Main(string[] args)
        {
            int closedTodoCount = 0; //counts total no of completed todo for user
            int openTodoCount = 0;  // counts total no of incomplete todo for user
            var openTodoDetails = new List<UserData>();// for storing the details of all open todos of the user
            Console.WriteLine("Please enter the userid you want to fetch detail");

            string userinputid = Console.ReadLine();
            int num = -1;
            if (!int.TryParse(userinputid, out num))
            {
                Console.WriteLine("User Input is Not a valid number \n Program Exiting Now");
                return;
            }
            else
            {
                try
                {

                    WebRequest request = WebRequest.Create($"https://jsonplaceholder.typicode.com/users/{userinputid}/todos");
                    WebResponse response = request.GetResponse();
                    // Display the status of request
                    Console.WriteLine("Status Of The Request is ");
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing data returned by the API JSON.
                    // The using block ensures the stream is automatically closed.
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        // Checks if its no todo task for user
                        if (responseFromServer == "[]")
                        {
                            Console.WriteLine("Userid is not valid or User doesnot have any completed/incomplete Todo Tasks, Program exits");
                            response.Close();
                            return;
                        }
                        // fetch all todo datas for tht particuar userid
                        var userTodoData = JsonConvert.DeserializeObject<List<UserData>>(responseFromServer);
                        // to count open and close todo task for userid
                        // and add all open todo to openTodoDetails list
                        foreach (var demoResult in userTodoData)
                        {
                            if (demoResult.Completed == true)
                                closedTodoCount++;
                            else
                            {
                                openTodoCount++;
                                openTodoDetails.Add(demoResult);
                            }

                        }
                        Console.WriteLine("Displaying todo details for requested user-id {0}", userinputid);
                        Console.WriteLine("======================================");
                        Console.WriteLine("Total number closed todos : {0} \n open todos  : {1}", openTodoCount, closedTodoCount);
                        //checks if user has no open todo task
                        if (openTodoDetails != null)
                        {
                            Console.WriteLine("Open ToDos for the user are : ");
                            Console.WriteLine("\nToDo-ID: Title-Name");
                            Console.WriteLine("===========================================");
                            openTodoDetails.ForEach(Console.WriteLine);
                        }
                        else
                            Console.WriteLine("None");
                    }
                    // Close the response.
                    response.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Exception hits***   Link is not found and details are :");
                    Console.WriteLine(exception);

                }
            }
        }
    }
}
