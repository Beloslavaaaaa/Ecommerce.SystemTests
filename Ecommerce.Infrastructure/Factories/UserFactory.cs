namespace Ecommerce.Infrastructure.Factories
{
    using Ecommerce.Infrastructure.DTOs;
    using System;

    public static class UserFactory
    {
        public static UserDto GetStandardUser()
        {
            var email = Environment.GetEnvironmentVariable("SAUCE_USER");
            var pass = Environment.GetEnvironmentVariable("SAUCE_PASS");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                throw new Exception("Environment Variables SAUCE_USER or SAUCE_PASS are not set on this machine!");
            }

            return new UserDto
            {
                Email = email,
                Password = pass
            };
        }

        public static UserDto GetInvalidUser()
        {
            return new UserDto
            {
                Email = "nonexistent@example.com",
                Password = "wrongpassword"
            };
        }
        public static UserDto GetExistingUser()
        {
            return new UserDto
            {
                Name = "Existing User",
                Email = "testuser@example.com",
                Password = "password123"
            };
        }
        public static UserDto GetValidUser()
        {
            return new UserDto
            {
                Email = "testuser@example.com",
                Password = "password123"
            };
        }
        public static UserDto GetRegistrationUser()
        {
            string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmss");

            return new UserDto
            {
                Name = "testuser123", 
                Email = $"bella789bella@bella.com",
                Password = "testpass123",
                Title = "Mrs",
                BirthDay = "1",
                BirthMonth = "January",
                BirthYear = "2001",
                FirstName = "test",
                LastName = "test",
                Address = "test",
                Country = "India",
                State = "test",
                City = "test",
                ZipCode = "90001",
                MobileNumber = "1234567890"
            };
        }
    }
}