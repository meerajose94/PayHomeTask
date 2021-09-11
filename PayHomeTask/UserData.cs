using System;
using System.Collections.Generic;
using System.Text;

namespace PayHomeTask
{
   public class UserData
    {
            
            public string userId { get; set; }
            public string Id { get; set; }
            public string Title { get; set; }
            public Boolean Completed { get; set; }
            public override string ToString()
            {
            return $" {Id,5} : {Title}";
            }
    }
}
