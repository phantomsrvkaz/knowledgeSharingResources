using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaFirst
{
    public class TestFunction
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<User> TestFunctionHandler(ILambdaContext context)
        {
            var userList = new User().GetUserList();
            context.Logger.Log("User list successfully get. Input -> ");
            return userList;
        }
    }
}
