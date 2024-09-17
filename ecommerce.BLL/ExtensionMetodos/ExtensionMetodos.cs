namespace ecommerce.BLL.ExtensionMetodos
{
    public static class ExtensionMetodos
    {
        // Validar campos vacíos o todos los campos vacíos
        public static void ValidateDto<T>(this T dto, params string[] propertiesToIgnore) where T : class
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El objeto DTO no puede ser nulo.");

            var type = typeof(T);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                // Verificar si la propiedad debe ser ignorada en la validación
                if (propertiesToIgnore.Contains(property.Name))
                    continue;

                var value = property.GetValue(dto) as string;

                // Verificar campos vacíos
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{property.Name} no puede estar vacío.");
                }
            }
        }

        // Encriptación de contraseña
        public static string EncryptPassword(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            return BCrypt.Net.BCrypt.HashPassword(password); 
        }

        // Extensión para verificar una contraseña
        public static bool VerifyPassword(this string plainTextPassword, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(plainTextPassword) || string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Las contraseñas no pueden estar vacías.");

            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }

        // Validar URL
        public static bool IsValidUrl(this string url)
        {
            // Verificar si la URL es nula o vacía
            if (string.IsNullOrWhiteSpace(url))
            {
                return true;
            }

            if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            return false;
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

        // Método de extensión para verificar si una cadena solo contiene números
        public static bool IsNumeric(this string value)
        {
            return value.All(char.IsDigit);
        }
    }
}
