using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PetPass.Model;

namespace PetPass.Resources.Tools
{
    public class ValidationForm
    {

        public static List<string> ValidatePerson(Person person)
        {
            List<string> errores = new List<string>();

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrWhiteSpace(person.Name))
            {
                errores.Add("El campo Nombre es obligatorio.");
            }
            else if (!Regex.IsMatch(person.Name, @"^[a-zA-Z\s]+$"))
            {
                errores.Add("El campo Nombre no debe contener números ni caracteres especiales.");
            }

            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                errores.Add("El campo Primer Nombre es obligatorio.");
            }
            else if (!Regex.IsMatch(person.FirstName, @"^[a-zA-Z\s]+$"))
            {
                errores.Add("El campo Primer Nombre no debe contener números ni caracteres especiales.");
            }

            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                errores.Add("El campo Apellido es obligatorio.");
            }
            else if (!Regex.IsMatch(person.LastName, @"^[a-zA-Z\s]+$"))
            {
                errores.Add("El campo Apellido no debe contener números ni caracteres especiales.");
            }

            if (string.IsNullOrWhiteSpace(person.CI))
            {
                errores.Add("El campo CI (Cédula de Identidad) es obligatorio.");
            }
            //else
            //{
            //    // Utiliza una expresión regular para validar el formato específico (n números seguidos de n letras)
            //    string ciPattern = @"^\d{1,}\p{L}{1,}$";

            //    if (!Regex.IsMatch(person.CI, ciPattern))
            //    {
            //        errores.Add("El campo CI (Cédula de Identidad) no tiene el formato válido.");
            //    }
            //}

            if (string.IsNullOrWhiteSpace(person.Gender))
            {
                errores.Add("Seleccione un género.");
            }

            if (string.IsNullOrWhiteSpace(person.Email))
            {
                errores.Add("El campo Email es obligatorio.");
            }
            else if (!Regex.IsMatch(person.Email, pattern))
            {
                errores.Add("El formato del correo electrónico no es válido.");
            }

            return errores;
        }
    }
}
