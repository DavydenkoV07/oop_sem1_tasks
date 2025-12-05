using System;

namespace TaskManagerApp.Models
{
    /**
    * @class User
    * @brief Represents a system user.
    */
    public class User
    {
        public int UserId { get; }
        public string Username { get; set; }
        public string Email { get; set; }
        private static int _nextId = 1;

        /**
         * @brief Initializes a new user with a unique ID.
         * @param username User's chosen username.
         * @param email User's email address.
         */
        public User(string username, string email)
        {
            UserId = _nextId++;
            Username = username;
            Email = email;
        }

        /**
         * @brief Checks if the email is a valid format (simple check).
         * @return True if the email contains '@' and '.', false otherwise.
         */
        public bool IsValidEmailFormat()
        {
            return Email != null && Email.Contains('@') && Email.Contains('.');
        }
    }
}