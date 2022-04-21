using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace task1
{
    internal class Program
    {
        public static List<User> list;
        static void Main(string[] args)
        {

            Menu();
            //Console.WriteLine(Functionality.addUser());

        }

        public static void displayUser()
        {
            Console.WriteLine("----------User List---------");
            foreach (User user in list)
                Console.WriteLine(user);
        }



        public static void Menu()
        {
            string input = "";
            do
            {
                list = Functionality.getAllUsers();
                Console.WriteLine("\n");
                Console.WriteLine("----------Main Menu---------");
                Console.WriteLine("1.Add User");
                Console.WriteLine("2.Update User");
                Console.WriteLine("3.List Users");
                Console.WriteLine("4.Exit");
                Console.WriteLine("Select an option");
                input = Console.ReadLine();
                if (input.Equals("1"))
                    Functionality.addUser();
                else if (input.Equals("2"))
                    Functionality.updateUser();
                else if (input.Equals("3"))
                    displayUser();
                else if (input.Equals("4"))
                    return;

            } while (!input.Equals("4"));
        }


        //public static  delegate void SuccessMessageDelegate(object sender, MyEventArgs myEventArgs);

        //public delegate void MyHandler2(object sender, MyEventArgs e);




    }
    public delegate void MessageDelegate();
    class Functionality
    {
        //public static event SuccessMessageDelegate ProcessCompleted;
        public static void updateUser()
        {
            Console.WriteLine("Enter User Phone No.");
            string phoneNo = Console.ReadLine();
            string projPath = Path.GetFullPath("..\\..\\..\\");
            string path = projPath + "Data/" + phoneNo + ".txt";
            if ((!File.Exists(path)))
            {
                Console.WriteLine("User Dosnt exist");
            }
            else
            {
                Console.WriteLine("Ente First Name (only chars 13 char long)");
                string firstname = Console.ReadLine();

                Console.WriteLine("Enter Last Name (only chars 10 char long)");
                string lastname = Console.ReadLine();

                Console.WriteLine("Enter DOB (pattern yyyy/mm/dd)");
                string DOB = Console.ReadLine();

                Console.WriteLine("Enter Gender (M/F/m/f)");
                string Gender = Console.ReadLine();

                //Console.WriteLine("Enter Phone No. (+923...  13 char long)");
                string phone = phoneNo;

                User user = new User();
                bool check = user.newInstance(firstname, lastname, DOB, phone, Gender);

                if (check)
                {
                    writeUser(user).Wait();
                }

            }
        }

        public static bool checkUserExists(User user)
        {
            string projPath = Path.GetFullPath("..\\..\\..\\");
            string path = projPath + "Data/";
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles("*.txt");
            foreach (var file in files)
            {
                if (file.Name.StartsWith(user.getPhone()))
                    return true;
            }
            return false;
        }


        public static bool addUser()
        {
            Console.WriteLine("Ente First Name (only chars 13 char long)");
            string firstname = Console.ReadLine();

            Console.WriteLine("Enter Last Name (only chars 10 char long)");
            string lastname = Console.ReadLine();

            Console.WriteLine("Enter DOB (pattern yyyy/mm/dd)");
            string DOB = Console.ReadLine();

            Console.WriteLine("Enter Gender (M/F/m/f)");
            string Gender = Console.ReadLine();

            Console.WriteLine("Enter Phone No. (+923...  13 char long)");
            string phone = Console.ReadLine();

            User user = new User();
            bool check = user.newInstance(firstname, lastname, DOB, phone, Gender);

            if (check)
            {
                if (!checkUserExists(user))
                {
                    writeUser(user).Wait();
                    return true;
                }
                else
                {
                    Console.WriteLine("User Exists!");
                }

            }
            else
            {
                Console.WriteLine("Constraints Violated");
            }

            return false;
        }

        public static List<User> getAllUsers()
        {
            List<User> list = new List<User>();
            string projPath = Path.GetFullPath("..\\..\\..\\");
            string path = projPath + "Data/";
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles("*.txt");
            foreach (var file in files)
            {
                User u =  readUser(file.Name).Result;
                list.Add(u);
            }
            return list;
        }

        public static void Message()
        {
            Console.WriteLine("Successfully Saved to txt file");
        }
        public static event MessageDelegate Display;
        public static async Task<bool> writeUser(User u)
        {
            Display += Message;
            string projPath = Path.GetFullPath("..\\..\\..\\");
            string path = projPath + "Data/" + u.getPhone() + ".txt";
            //if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(u.ToString());
                }
            }
            Display.Invoke();
            return true;
        }
        public static async Task<User> readUser(string _fName)
        {
            string projPath = Path.GetFullPath("..\\..\\..\\");
            string path = projPath + "Data/" + _fName;

            string[] lines = File.ReadAllLines(path);
            string[] args = lines[0].Split(',');
            User u = new User();
            u.newInstance(args[0], args[1], args[2], args[3], args[4]);
            return u;
        }
    }
}
