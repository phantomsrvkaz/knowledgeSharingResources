using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaSecond
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> UserFunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var userService = new DynamoDbService(new AmazonDynamoDBClient());
            dynamic response = await userService.GetUsers();

            if (request.HttpMethod == "POST")
            {
                var user = JsonConvert.DeserializeObject<User>(request.Body);
                if(user == null) { return new APIGatewayProxyResponse(){StatusCode = 400};}
                response = await new DynamoDbService(new AmazonDynamoDBClient()).AddItem(user);
            }

            return new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(response)
            };
        }
    }
}
