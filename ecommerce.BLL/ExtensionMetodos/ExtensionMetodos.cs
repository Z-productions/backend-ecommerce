using ecommerce.DTO.Registration;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace ecommerce.BLL.ExtensionMetodos
{
    public static class ExtensionMetodos
    {
        // Encriptación de contraseña
        public static string EncryptPassword(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        // Validar URL
        public static bool IsValidUrl(this string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }

        // Validar campos vacíos o todos los campos vacíos
        public static void ValidateDto<T>(this T dto, bool validateAllFieldsEmpty = false) where T : class
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El formulario no puede estar vacío. Por favor, complete los campos.");

            var type = typeof(T);
            var properties = type.GetProperties();

            bool allFieldsEmpty = true;

            foreach (var property in properties)
            {
                var value = property.GetValue(dto) as string;

                // Verificar si el campo es string y está vacío
                if (!string.IsNullOrWhiteSpace(value))
                {
                    allFieldsEmpty = false;  // Si al menos un campo tiene valor, no están todos vacíos
                }
                else if (validateAllFieldsEmpty == false && string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"El campo '{property.Name}' es obligatorio. Por favor, complete este campo.");
                }
            }

            // Si se especifica validar si todos los campos están vacíos
            if (validateAllFieldsEmpty && allFieldsEmpty)
            {
                throw new ArgumentException("No ha completado ningún campo. Por favor, rellene al menos uno.");
            }
        }
        // Validar formato de correo electrónico
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
