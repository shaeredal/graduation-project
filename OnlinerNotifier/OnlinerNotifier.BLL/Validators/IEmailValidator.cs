namespace OnlinerNotifier.BLL.Validators
{
    public interface IEmailValidator
    {
        bool IsValid(string email);
    }
}