﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenLisp.Core.Kernel.OS.System.Security;

namespace OpenLisp.Core.Kernel.OS.System.Users
{
    class Users
    {

        #region UserDirs
        public static void InitUserDirs(string user)
        {
            if (user == "root")
            {
                return;
            }
            else
            {
                string[] DefaultDirectories =
                {
                    @"0:\Users\" + user + @"\Desktop",
                    @"0:\Users\" + user + @"\Documents",
                    @"0:\Users\" + user + @"\Downloads",
                    @"0:\Users\" + user + @"\Music",
                };
                foreach (string dirs in DefaultDirectories)
                    if (!Directory.Exists(dirs))
                        Directory.CreateDirectory(dirs);
            }
        }
        #endregion UserDirs

        public static string[] users;
        static string[] reset;
        static List<string> usersfile = new List<string>();

        /// <summary>
        /// Method to create an user.
        /// </summary>
        public void Create(string username, string password, string type = "standard")
        {
            try
            {
                password = Sha256.hash(password);
                LoadUsers();
                if (GetUser("user").StartsWith(username))
                {
                    Console.WriteLine(username + " already exists.");
                    return;
                }
                PutUser("user:" + username, password + "|" + type);
                PushUsers();
                Console.WriteLine(username + " has been created!");

                InitUserDirs(username);
                Console.WriteLine("Personal directories has been created!");
            }
            catch
            {
                Console.WriteLine("Error while creating user.");
            }
        }

        /// <summary>
        /// Method to remove an user.
        /// </summary>
        public void Remove(string username)
        {
            if (GetUser("user").StartsWith(username))
            {
                LoadUsers();
                DeleteUser(username);
                //Directory.Delete(@"0:\Users\" + username, true);
                Console.WriteLine("User has been remnoved.");
            }
            else
            {
                Console.WriteLine("User does not exist.");
            }
        }

        /// <summary>
        /// Method to change the password of an user.
        /// </summary>
        public void ChangePassword(string username, string password)
        {

            LoadUsers();
            EditUser(username, password);
            File.Delete(@"0:\System\passwd");
            File.Create(@"0:\System\passwd");
            PushUsers();
            //Directory.Delete(@"0:\Users\" + username, true);
            Console.WriteLine("Password has been changed.");

        }

        public static void DeleteUser(string user)
        {

            foreach (string line in users)
            {
                usersfile.Add(line);
            }

            int counter = -1;
            int index = 0;

            bool exists = false;

            foreach (string element in usersfile)
            {
                counter = counter + 1;
                if (element.Contains(user))
                {
                    index = counter;
                    exists = true;
                }
            }
            if (exists)
            {
                usersfile.RemoveAt(index);

                users = usersfile.ToArray();

                usersfile.Clear();

                File.Delete(@"0:\System\passwd");

                PushUsers();
            }
        }

        public static void EditUser(string username, string password)
        {
            foreach (string line in users)
            {
                usersfile.Add(line);
            }

            int counter = -1;
            int index = 0;

            bool exists = false;

            foreach (string element in usersfile)
            {
                counter = counter + 1;
                if (element.Contains(username))
                {
                    index = counter;
                    exists = true;
                }
            }
            if (exists)
            {
                password = Sha256.hash(password);

                usersfile[index] = "user:" + username + ":" + password + "|" + Kernel.userLevelLogged;

                users = usersfile.ToArray();

                usersfile.Clear();
            }
        }

        public static string GetUser(string parameter)
        {
            string value = "null";

            foreach (string line in users)
            {
                usersfile.Add(line);
            }

            foreach (string element in usersfile)
            {
                if (element.StartsWith(parameter))
                {
                    value = element.Remove(0, parameter.Length + 1);
                }
            }

            usersfile.Clear();

            return value;
        }

        public static void PutUser(string parameter, string value)
        {
            bool contains = false;

            foreach (string line in users)
            {
                usersfile.Add(line);
                if (line.StartsWith(parameter))
                {
                    contains = true;
                }
            }

            if (!contains)
            {
                usersfile.Add(parameter + ":" + value);
            }

            users = usersfile.ToArray();

            usersfile.Clear();
        }

        public static void PushUsers()
        {
            File.WriteAllLines(@"0:\System\passwd", users);
        }

        public static void LoadUsers()
        {
            //reset of users string array in memory if there is "something"
            users = reset;
            //load
            users = File.ReadAllLines(@"0:\System\passwd");
        }

    }
}
