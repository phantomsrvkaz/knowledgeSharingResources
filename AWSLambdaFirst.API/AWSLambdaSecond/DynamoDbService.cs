using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaSecond
{
    public class DynamoDbService
    {
        private readonly IAmazonDynamoDB _dynamoDbService;

        public DynamoDbService(IAmazonDynamoDB dynamoDbService)
        {
            _dynamoDbService = dynamoDbService;
        }

        public async Task<User[]> GetUsers()
        {
            var result = await _dynamoDbService.ScanAsync(new ScanRequest()
            {
                TableName = "user"
            });

            if (result != null && result.Items != null)
            {
                var users = new List<User>();
                foreach (var item in result.Items)
                {
                    item.TryGetValue("Id", out var id);
                    item.TryGetValue("Name", out var name);
                    item.TryGetValue("Phone", out var phone);
                    item.TryGetValue("City", out var city);

                    users.Add(new User()
                    {
                        Id = id?.S,
                        Name = name?.S,
                        City = city?.S,
                        Phone = phone?.S
                    });
                }
                return users.ToArray();
            }
            return Array.Empty<User>();
        }

        public async Task<string> AddItem(User user)
        {
            var request = new PutItemRequest
            {
                TableName = "user",
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue(user.Id)},
                    {"Name", new AttributeValue(user.Name)},
                    {"Phone", new AttributeValue(user.Phone)},
                    {"City", new AttributeValue(user.City)},

                }
            };
            var response = await _dynamoDbService.PutItemAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
                return "Succesfully Added";
            else
                return "Failed";
        }
    }
}
