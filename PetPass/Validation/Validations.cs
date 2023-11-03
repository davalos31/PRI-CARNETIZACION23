using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetPass.Validation
{
    internal class Validations
    {
        public (bool, string) ValidateName(string value)
        {
            if (string.IsNullOrEmpty(value)) return (false, "es obligatorio");

            foreach (char c in value)
                if (!char.IsLetter(c))
                    if (!c.Equals(" "))
                        return (false, "solo puede contener letras");

            return (true, null);
        }
        public (bool, string) ValidateDate(DateTime? date)
        {
            if (date == null) return (false, "es obligatorio");

            int edad = DateTime.Today.Year - date.Value.Year;
            if (date.Value.Date > DateTime.Today.AddYears(-edad))
                edad--;

            if (edad < 18) return (false, "debe ser mayor de edad");
            else return (true, null);
        }
        public (bool, string) ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return (false, "el telefono es obligatorio");



            int max = 8;
            int min = 6;
            if (phone.Length > max || phone.Length < min) return (false, $"el telefono debe tener de {min} a {max} caracteres");

            if (!phone.All(char.IsDigit)) return (false, "el telefono solo puede tener numeros");

            return (true, null);
        }
        public (bool, string) ValidateCI(string ci)
        {
            if (string.IsNullOrEmpty(ci)) return (false, "el CI es obligatorio");



            int min = 6;
            if (ci.Length < min) return (false, $"el CI debe tener mas de {min} caracteres");

            if (!ci.Substring(0, ci.Length - 2).All(char.IsDigit)) return (false, "el formato del CI no es valido");


            return (true, null);
        }
        public (bool, string) ValidateEmail(string email)
        {
            if (email == null) return (false, "el correo es obligatorio");

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Match match = Regex.Match(email, pattern);

            if (!match.Success) return (false, "el correo no es valido");
            else return (true, null);
        }
        private static List<string> base64Images = new List<string>();

        public static void ProcessImages(List<string> images)
        {
            // Aquí puedes procesar las imágenes base64 como desees.
            // Por ejemplo, puedes guardarlas en una base de datos o realizar cualquier otro tipo de procesamiento.
            base64Images.AddRange(images);
        }

        public static List<string> GetImages()
        {
            // Este método estático devuelve la lista de imágenes base64.
            return base64Images;
        }
    }
}
