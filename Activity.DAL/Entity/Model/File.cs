using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minxtu.DAL.Entity
{
    public class File
    {
        public File()
        { }

        public String _nameFile;
        public string NameFile { get { return _nameFile; } set { _nameFile = value; } }

        public String _urlFile;
        public string UrlFile { get { return _urlFile; } set { _urlFile = value; } }

        public String _sizeFile;
        public String SizeFile { get { return _sizeFile; } set { _sizeFile = value; } }

        public DateTime _timeUpload;
        public DateTime TimeUpload { get { return _timeUpload; } set { _timeUpload = value; } }

        public String _desciption;
        public string Desciption { get { return _desciption; } set { _desciption = value; } }
        public int _id;
        public int Id { get { return _id; } set { _id = value; } }
       
    }
}
