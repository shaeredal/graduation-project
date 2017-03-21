using NUnit.Framework;
using OnlinerNotifier.BLL.Validators;

namespace OnlinerNotifier.BLL_Tests.Validators
{
    [TestFixture]
    public class EmailValidatorTest
    {
        [TestCase("OnlinerNotifier@gmail.com", ExpectedResult = true)]
        [TestCase("OnlinerNotifier@gmailcom", ExpectedResult = false)]
        public bool IsValid_Email_Validity(string email)
        {
            var emailValidator = new EmailValidator();

            var result = emailValidator.IsValid(email);

            return result;
        }
    }
}
