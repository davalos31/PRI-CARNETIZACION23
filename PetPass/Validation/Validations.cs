using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetPass.Validation
{

    public class Validations
    {
        private static string capturedImageBase64;
        public (bool, string) ValidateName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return (false, "El nombre es obligatorio");
            }

            foreach (char c in value)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return (false, "Solo puede contener letras");
                }
            }

            return (true, null);
        }
        public (bool, string) ValidateFirstName(string value)
        {
            if (string.IsNullOrEmpty(value)) return (false, "el primer Apellido es obligatorio");

            foreach (char c in value)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return (false, "Solo puede contener letras");
                }
            }

            return (true, null);
        }
        public (bool, string) ValidateLastName(string value)
        {
            if (string.IsNullOrEmpty(value)) return (false, "el segundo Apellido es obligatorio");

            foreach (char c in value)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return (false, "Solo puede contener letras");
                }
            }

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

        public (bool, string) ValidateGender(string gender)
        {
            if (string.IsNullOrEmpty(gender))
                return (false, "El género es obligatorio");

            // Puedes agregar más validaciones específicas del género si es necesario

            return (true, null);
        }

        public (bool, string) ValidateImage(string image)
        {
            if (string.IsNullOrEmpty(image))
                return (false, "La imagen es obligatoria");

            // Puedes agregar más validaciones específicas de la imagen si es necesario

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
        public static void SetCapturedImage(string imageSource)
        {
            capturedImageBase64 = imageSource;
        }

        public static string GetCapturedImageBase64()
        {
            return capturedImageBase64;
        }



        public static string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }


        public (bool, string) ValidateCampaignName(string campaignName)
        {
            if (string.IsNullOrWhiteSpace(campaignName))
            {
                return (false, "Debe de insertar un dato en el nombre de la campaña.");
            }
            return (true, null);
        }

        public (bool, string) ValidateEndDate(DateTime endDate)
        {
            if (endDate <= DateTime.Now)
            {
                return (false, "La fecha de finalización debe ser posterior a la fecha actual.");
            }
            return (true, null);
        }

        public (bool, string) ContainsSpecialCharacters(string input)
        {

            // Validar que el nombre de la campaña no esté vacío
            if (string.IsNullOrEmpty(input))
            {
                return (false, "El nombre de la campaña no puede estar vacío.");
            }

            // Validar la presencia de caracteres especiales en el nombre
            string specialCharacters = "!@#$%^&*()_+[]{}|;:'\",.<>?~";
            foreach (char c in specialCharacters)
            {
                if (input.Contains(c))
                {
                    return (false, "El nombre de la campaña contiene caracteres especiales.");
                }
            }

            // Si no se encontraron caracteres especiales y el nombre no está vacío, la validación es exitosa
            return (true, null);
        }

        public (bool, string) ValidateFields(int selectedZone, int selectedCampaign)
        {
            if (string.IsNullOrWhiteSpace(selectedZone.ToString()) || string.IsNullOrWhiteSpace(selectedCampaign.ToString()))
            {
                return (false, "Por favor, selecciona una zona o una campaña.");
            }
            return (true, null);
        }

        public (bool, string) ValidateDate(DateTime selectedDate)
        {
            if (selectedDate < DateTime.Now.Date)
            {
                return (false, "La fecha de la patrulla debe ser igual o posterior a la fecha actual.");
            }
            return (true, null);
        }

        public (bool, string) ValidateAgeOver18(DateTime birthDate)
        {
            DateTime today = DateTime.Now;
            DateTime eighteenYearsAgo = today.AddYears(-18);

            if (birthDate > eighteenYearsAgo)
            {
                return (false, "La persona debe ser mayor de 18 años.");
            }

            return (true, null);
        }

    }
}

