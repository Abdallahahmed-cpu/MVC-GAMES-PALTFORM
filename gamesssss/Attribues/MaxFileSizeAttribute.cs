//namespace gamesssss.Attribues
//{
//    public class MaxFileSizeAttribute : ValidationAttribute
//    {
//        private readonly int _maxFileSize;
//        public MaxFileSizeAttribute(int maxFileSize)
//        {
//            _maxFileSize = maxFileSize;
//        }
//        protected override ValidationResult? IsValid
//            (object? value, ValidationContext validationContext)
//        {
//            var file = value as IFormFile;

//            if (file != null)
//            {

//                if (file.Length>_maxFileSize)
//                {
//                    return new ValidationResult(errorMessage: $"Maximum allowed file size{_maxFileSize}byetes ");
//                }
//            }
//            return ValidationResult.Success;
//        }
//    }
//}
namespace gamesssss.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var file = value as IFormFile;
            //if (file != null && file.Length > _maxFileSize)
            //{
            //    return new ValidationResult($"Maximum allowed file size is {_maxFileSize / (1024 * 1024)} MB.");
            //}
            return ValidationResult.Success;
        }
    }
}

