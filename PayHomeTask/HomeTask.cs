using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
/*
 * 
 *                 ****************This is the main program of the PayHomeTask*************
 *
 * PayHomeTask project will fetch todo details of given user from API using http://jsonplaceholder.typicode.com.
 * This programs allows the user to enter the userid as input and provides total number of open and closed todo and details 
 * of all open todo task(title and id number) for the userid. 
 */

namespace PayHomeTask
{
    public class HomeTask
    {
        public static int closedTodoCount; //for total true completed todo
        public static int openTodoCount;// for total false complete todo 
        public static List<UserData> openTodoDetails = new List<UserData>();

        static void Main(string[] args)
        {
        


        //calling GetUserInput method for getting user input
        string userinputid = GetUserInput();

            // calls GetDataFromAPI method for fetching data from JSONAPI
            string checkDataFromServer = GetDataFromAPI(userinputid);


            // Checks if it has no todo task for user or userid not updated
            if (checkDataFromServer == "[]")
            {
                Console.WriteLine("Userid is not valid or User doesnot have any completed/incomplete Todo Tasks, Program exits");
                return;
            }
            // fetch all todo datas for tht particuar userid
            var userTodoData = JsonConvert.DeserializeObject<List<UserData>>(checkDataFromServer);

            //FinalResult is a method to calculate the counts of open and close todo and printing all opentodo task for user
            FinalResult(userTodoData,userinputid);


        }
        // method for getting user-id as input from user
        public static string GetUserInput()
        {
            Console.WriteLine("Please enter the userid you want to fetch detail");
            string userinput;
            int num = -1;
            while (true)
            {
                userinput = Console.ReadLine();
                if (!int.TryParse(userinput, out num))
                    Console.WriteLine("User Input is Not a valid number \n Please try again any userid between 1 and 10");
                else
                    break;
            }
            return userinput;
        }

        //method for fetching data from JSON API
        public static string GetDataFromAPI(string userinputid)
        {

            string responseFromServer;
            string userid = userinputid;
            string path = $"https://jsonplaceholder.typicode.com/users/{userid}/todos";
            WebRequest request = WebRequest.Create(path);
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
                responseFromServer = reader.ReadToEnd();
            }
            //closing response
            response.Close();
            return responseFromServer;

        }

        //method will calculates count of open and close todo and prints all opentodo details
        public static void FinalResult(List<UserData> userToDoData, string userinputid)
         {

            openTodoCount = 0;
            closedTodoCount = 0;
            openTodoDetails.Clear();
            
        List<UserData> TodoData = userToDoData;
            //var openTodoDetails = new List<UserData>();// for storing the details of all open todos of the user

            
            // to count open and close todo task for userid
            // and add all open todo to openTodoDetails list
            foreach (var demoResult in TodoData)
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
            Console.WriteLine("Total number closed todos : {0} \n open todos  : {1}", closedTodoCount, openTodoCount);
            Console.WriteLine("Open ToDos for the user are : ");
            //checks if user has no open todo task
            if (openTodoDetails.Count !=0)
            {
                
                Console.WriteLine("\nToDo-ID: Title-Name");
                Console.WriteLine("===========================================");
                openTodoDetails.ForEach(Console.WriteLine);
            }
            else
                Console.WriteLine("None");
        }



    }
}
