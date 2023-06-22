using Analytics.Methods.SharedMethods;
using Analytics.Shared;

namespace Analytics.Core
{
    public class MajorFactory : BaseFactory<MajorMethods, MajorFactoryMethodInfo>
    {
        public MajorFactory(MajorMethods methods) : base(methods)
        {
        }

        public MajorFactory Mail()
        {
            AddMethod(_methods.Mail);
            return this;
        }

        public MajorFactory Index()
        {
            AddMethod(_methods.Index);
            return this;
        }

        public MajorFactory Imei()
        {
            AddMethod(_methods.Imei);
            return this;
        }

        public MajorFactory Imsi()
        {
            AddMethod(_methods.Imsi);
            return this;
        }

        public MajorFactory File() { 
            AddMethod(_methods.File);
            return this;
        }

        public MajorFactory Hex() 
        {
            AddMethod(_methods.Hex);
            return this;
        }

        public MajorFactory Coordinate() 
        {
            AddMethod(_methods.Coordinate);
            return this;
        }

        public MajorFactory Address()
        {
            AddMethod(_methods.Address);
            return this;
        }

        public MajorFactory Msisdn()
        {
            AddMethod(_methods.Msisdn);
            return this;
        }

        public MajorFactory Str()
        {
            AddMethod(_methods.Str);
            return this;
        }

        public MajorFactory Ip()
        {
            AddMethod(_methods.Ip);
            return this;
        }

        public MajorFactory Int()
        {
            AddMethod(_methods.Int);
            return this;
        }

        public MajorFactory Dbl()
        {
            AddMethod(_methods.Dbl);
            return this;
        }

        public MajorFactory Datetime()
        {
            AddMethod(_methods.Datetime);
            return this;
        }

        public MajorFactory Time()
        {
            AddMethod(_methods.Time);
            return this;
        }

        public MajorFactory Date()
        {
            AddMethod(_methods.Date);
            return this;
        }

        protected void AddMethod(Func<string, bool> func)
        {
            _selectedMethods.Add(new MajorFactoryMethodInfo(func.Method.Name, func));
        }
    }
}
