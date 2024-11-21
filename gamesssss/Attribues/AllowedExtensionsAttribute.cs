//namespace gamesssss.Attribues
//{
//    public class AllowedExtensionsAttribute : ValidationAttribute
//    {
//        private readonly string _allowedExtensions;
//        public AllowedExtensionsAttribute(string allowedExtensions)
//        {
//            _allowedExtensions = allowedExtensions;
//        }
//        protected override ValidationResult? IsValid
//            (object? value, ValidationContext validationContext)
//        {
//            var file = value as IFormFile;

//            if (file != null)
//            {
//                var extention = Path.GetExtension(file.FileName);
//                var isallowed = !_allowedExtensions.Split(separator: ',').Contains(extention, StringComparer.OrdinalIgnoreCase);
//                if (!isallowed)
//                {
//                    return new ValidationResult(errorMessage: $"Only{_allowedExtensions} are allowed");
//                }
//            }
//            return ValidationResult.Success;
//        }
//    }
//}
namespace gamesssss.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string extensions)
        {
            _extensions = extensions.Split(',').Select(e => e.Trim().ToLower()).ToArray();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult($"Not allowed extension. Allowed extensions: {string.Join(", ", _extensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
