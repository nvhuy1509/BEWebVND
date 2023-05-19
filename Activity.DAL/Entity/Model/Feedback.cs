using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minxtu.DAL.Entity
{
    public class Feedback
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nameGuess;

        public string NameGuess
        {
            get { return _nameGuess; }
            set { _nameGuess = value; }
        }


        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        private System.DateTime _createTime;

        public System.DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }


    }
}
