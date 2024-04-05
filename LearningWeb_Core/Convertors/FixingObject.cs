namespace LearningWeb_Core.Convertors
{
    public class FixingObject
    {
        public static string FixingEmail(string Email) => Email.Trim().ToLower();
    }
}
