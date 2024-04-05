namespace LearningWeb_Core.Generator
{
    public static class UniqCode
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
