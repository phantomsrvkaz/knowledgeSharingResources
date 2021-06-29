using System.Collections.Generic;

namespace AWSLambdaSecond
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }

        public List<User> GetUserList()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = "1",
                    City = "Dhaka",
                    Name = "Mosfiq vai",
                    Phone = "019821323123"
                },
                new User()
                {
                    Id = "2",
                    City = "Chittagong",
                    Name = "Aman",
                    Phone = "018123213452"
                }
            };
        }
    }
}
