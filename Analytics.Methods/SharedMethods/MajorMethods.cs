using System.Globalization;
using System.Text.RegularExpressions;

namespace Analytics.Methods.SharedMethods
{
    public partial class MajorMethods
    {
        /// <summary>
        /// Optimization of regular expressions
        /// </summary>
        /// <param name="negativeRegex">IsMatch Denial</param>
        private bool SetRegex(string value, string pattern)
        {
            return new Regex(pattern).IsMatch(value);
        }

        /// <summary>
        /// Checks <paramref name="value"/> for compliance with email
        /// </summary>
        public bool Mail(string value) =>
            SetRegex(value, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

        /// <summary>
        /// Checks <paramref name="value"/> for compliance with the zip code
        /// </summary>
        public bool Index(string value) =>
            SetRegex(value, @"\b\d{6}\b");

        /// <summary>
        /// Checks <paramref name="value"/> for imei compliance.
        /// Checks for the presence of 15 digits in a row
        /// </summary>
        public bool Imei(string value) =>
            SetRegex(value, @"^\d{15}(,\d{15})*$");

        /// <summary>
        /// Checks <paramref name="value"/> for file schema
        /// </summary>
        public bool File(string value) =>
            SetRegex(value, @"(\w+:|\.*)\\(\w+\\)*(\w*\.\w*)");

        /// <summary>
        /// Checks <paramref name="value"/> for compliance with the hex color code
        /// </summary>
        public bool Hex(string value) =>
            SetRegex(value, @"^#(?:[0-9a-fA-F]{3}){1,2}$");

        /// <summary>
        /// Checks <paramref name="value"/> for Imsi compliance.
        /// Imsi usually contains 15 numbers.
        /// Currently being processed for the ru region is 250 +13 numbers
        /// </summary>
        public bool Imsi(string value) =>
            SetRegex(value, @"\b250\d{12}\b");

        /// <summary>
        /// Checks <paramref name="value"/> for matching coordinates
        /// </summary>
        public bool Coordinate(string value) =>
            SetRegex(value, @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$");

        /// <summary>
        /// Checks <paramref name="value"/> for matching the address.
        /// Checks in <paramref name="value"/> the presence of: st., d., g., region.
        /// </summary>
        public bool Address(string value) =>
            SetRegex(value, @"(ул\.)|(д\.)|(г\.)|(обл\.)");

        /// <summary>
        /// Checks <paramref name="value"/> for msisdn compliance
        /// </summary>
        public bool Msisdn(string value) =>
            SetRegex(value, @"^[1-9]\d{6,12}$");

        /// <summary>
        /// Checks <paramref name="value"/> for compliance with the text
        /// </summary>
        public bool Str(string value) =>
            !SetRegex(value, @"^(\d+|\d+\.\d+)$");

        /// <summary>
        /// Checks <paramref name="value"/> for compliance with the ip address
        /// </summary>
        public bool Ip(string value) =>
            SetRegex(value, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

        /// <summary>
        /// Checks <paramref name="value"/> for matching an integer
        /// </summary>
        public bool Int(string value) =>
            long.TryParse(value, out _);

        /// <summary>
        /// Checks <paramref name="value"/> for compliance with a fractional number
        /// </summary>
        public bool Dbl(string value) =>
            double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

        /// <summary>
        /// Checks <paramref name="value"/> for common date and time formats
        /// </summary>
        public bool Datetime(string value)
        {
            var date_time = value.Split(' ');

            if (date_time.Length != 2) return false;

            string[] formats = {
                "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "MM/dd/yyyy", "yyyy-MM-dd",
                "dd.MM.yyyy", "dd.M.yyyy", "d.M.yyyy", "d.MM.yyyy",
                "dd.MM.yy", "dd.M.yy", "d.M.yy", "d.MM.yy", "MM.dd.yyyy", "yyyy-MM-dd",
                "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy",
                "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy", "MM-dd-yyyy", "yyyy-MM-dd"
            };
            var _date = DateTime.TryParseExact(date_time[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            var _time = TimeSpan.TryParse(date_time[1], out _);

            return _date & _time;
        }

        /// <summary>
        /// Checks <paramref name="value"/> for common time formats
        /// </summary>
        public bool Time(string value)
        {
            bool isMatch = SetRegex(value, @"\b(\d{1,2}\s\w+)\b");

            string[] formats =
                {
                "d.hh:mm", "hh:mm", "h:m", "d.hh:mm:ss", @"s\.fff", @"ss\.fff", @"ss\.f", @"ss\.ff",
                "hh':'mm':'ss", @"hh\:mm\:ss", @"hh\:mm\:ss", @"d\.h\:mm\:ss", @"hh\:mm\:ss",
                "hh", @"h\:mm", @"hh\:mm", @"d\.hh\:mm\:ss", "hhmm", @"hh\:mm\:ss",
                @"h\:m\:s",
                "d.hh/mm", "hh/mm", "h/m", "d.hh/mm/ss", @"s\.fff", @"ss\.fff", @"ss\.f", @"ss\.ff",
                "hh'/'mm'/'ss", @"hh\/mm\/ss", @"hh\/mm\/ss", @"d\.h\/mm\/ss", @"hh\/mm\/ss",
                "hh", @"h\/mm", @"hh\/mm", @"d\.hh\/mm\/ss", "hhmm", @"hh\/mm\/ss",
                @"h\/m\/s",
                "d.hh.mm", "hh.mm", "h.m", "d.hh.mm.ss", @"s\.fff", @"ss\.fff", @"ss\.f", @"ss\.ff",
                "hh'.'mm'.'ss", @"hh\.mm\.ss", @"hh\.mm\.ss", @"d\.h\.mm\.ss", @"hh\.mm\.ss",
                "hh", @"h\.mm", @"hh\.mm", @"d\.hh\.mm\.ss", "hhmm", @"hh\.mm\.ss",
                @"h\.m\.s"
            };

            return isMatch | TimeSpan.TryParseExact(value, formats, CultureInfo.InvariantCulture, out _);
        }

        /// <summary>
        /// Checks <paramref name="value"/> for common date formats
        /// </summary>
        public bool Date(string value)
        {
            string[] formats = {
                "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "MM/dd/yyyy", "yyyy-MM-dd",
                "dd.MM.yyyy", "dd.M.yyyy", "d.M.yyyy", "d.MM.yyyy",
                "dd.MM.yy", "dd.M.yy", "d.M.yy", "d.MM.yy", "MM.dd.yyyy", "yyyy-MM-dd",
                "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy",
                "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy", "MM-dd-yyyy", "yyyy-MM-dd"
            };

            return DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}
