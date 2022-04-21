using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    internal class User
    {

        private string _firstName;
        private string _lastName;
        private DateTime _DOB;  
        private char _gender;
        private string _phone;


        public bool newInstance(string FirsrtName,string LastName,string DOB,string Phone, string Gender)
        {
            if (setFirstName(FirsrtName) && setLastName(LastName) && setDOB(DOB) && setGender(Gender) && setPhone(Phone))
            {
                return true;
            }
            return false;
        }


        public string getGender()
        {
            return this._gender.ToString();
        }
        public bool setGender(string gen)
        {
            if(gen.Length==1)
            {
                char g = gen.ToCharArray()[0];
                if (g == 'M' || g == 'm' || g == 'F' || g == 'f')
                {
                    this._gender = g;
                    return true;
                }
            }

            return false;   
        }


        public string getDOB()
        {
            return this._DOB.ToString();
        }
        public bool setDOB(string date)
        {
            try
            {
                this._DOB = Convert.ToDateTime(date);
                //DateTime myDateTime = DateTime.Now;
                //string myDateTimeString = myDateTime.ToString("yyyy-MM-dd");
            }
            catch(System.FormatException e)
            {
                return false;
            }
            return true;
        }

        

        public string getPhone()
        {
            return this._phone;
        }
        public bool setPhone(string num)
        {
            if(num.StartsWith("+923")&&num.Length==13)
            {
                this._phone = num;
                return true;
            }
            return false;
        }



        public string getFirstName()
        {
            return _firstName;
        }
        public bool setFirstName(string FirstName)
        {
            if (FirstName.Length > 15)
                return false;
            for (int i = 0; i < FirstName.Length; i++)
            {
                if(!Char.IsLetter(FirstName[i]))
                {
                    return false;
                }
            }
            this._firstName = FirstName;
            return true;
        }


        public string getLastName()
        {
            return _lastName;
        }
        public bool setLastName(string LastName)
        {
            if (LastName.Length > 15)
                return false;
            for (int i = 0; i < LastName.Length; i++)
            {
                if (!Char.IsLetter(LastName[i]))
                {
                    return false;
                }
            }
            this._lastName = LastName;
            return true;
        }

        public override string ToString()
        {
            return getFirstName()+","+getLastName()+","+this._DOB.ToString("yyyy-MM-dd") + ","+getPhone()+","+getGender();
        }
    }
}
