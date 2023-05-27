using System.Globalization;
using System.Text.RegularExpressions;

namespace Analyzer.Methods
{
    public partial class MajorMethods
    {
        /// <summary>
        /// Для оптимизации регулярных выражений
        /// </summary>
        /// <param name="negativeRegex">Отрицание IsMatch</param>
        private bool SetRegex(string value, string pattern)
        {
            return new Regex(pattern).IsMatch(value);
        }

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие электронной почте
        /// </summary>
        public bool Mail(string value) =>
            SetRegex(value, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие почтовому индексу
        /// </summary>
        public bool Index(string value) =>
            SetRegex(value, @"\b\d{6}\b");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие imei.
        /// Проверяет на наличие 15 цифр подряд
        /// </summary>
        public bool Imei(string value) =>
            SetRegex(value, @"^\d{15}(,\d{15})*$");

        /// <summary>
        /// Проверяет <paramref name="value"/> на наличие схемы файла
        /// </summary>
        public bool File(string value) =>
            SetRegex(value, @"(\w+:|\.*)\\(\w+\\)*(\w*\.\w*)");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие цветовому hex коду
        /// </summary>
        public bool Hex(string value) =>
            SetRegex(value, @"^#(?:[0-9a-fA-F]{3}){1,2}$");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие Imsi.
        /// Imsi обычно содержит 15 чисел.
        /// Сечас обрабатыается для ру региона - это 250 +13 чисел
        /// </summary>
        public bool Imsi(string value) =>
            SetRegex(value, @"\b250\d{12}\b");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие координатам
        /// </summary>
        public bool Coordinate(string value) =>
            SetRegex(value, @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие адресу.
        /// Проверяет в <paramref name="value"/> наличие: ул., д., г., обл.
        /// </summary>
        public bool Address(string value) =>
            SetRegex(value, @"(ул\.)|(д\.)|(г\.)|(обл\.)");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие msisdn
        /// </summary>
        public bool Msisdn(string value) =>
            SetRegex(value, @"^[1-9]\d{6,12}$");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие тексту
        /// </summary>
        public bool Str(string value) =>
            !SetRegex(value, @"^(\d+|\d+\.\d+)$");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие ip адресу
        /// </summary>
        public bool Ip(string value) =>
            SetRegex(value, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие целому числу
        /// </summary>
        public bool Int(string value) =>
            long.TryParse(value, out _);

        /// <summary>
        /// Проверяет <paramref name="value"/> на соответствие дробному числу
        /// </summary>
        public bool Dbl(string value) =>
            double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _);

        /// <summary>
        /// Проверяет <paramref name="value"/> на распространенные форматы даты и времени
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
        /// Проверяет <paramref name="value"/> на распространенные форматы времени
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
        /// Проверяет <paramref name="value"/> на распространенные форматы даты
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
