/**
* @file User.cs
* @brief Contains the User class to represent a system user.
* @namespace TaskManagerApp.Models
*/
using System;

namespace TaskManagerApp.Models
{
    /**
    * @class User
    * @brief Represents a system user.
    */
    public class User
    {
        /// @property UserId The unique identifier of the user, generated automatically.
        public int UserId { get; }
        /// @property Username The user's chosen name.
        public string Username { get; set; }
        /// @property Email The user's email address.
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