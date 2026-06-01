namespace ARVTech.Shared.Security.Implementations
{
    using ARVTech.Shared.Security.Interfaces;

    public class PepperProvider : IPepperProvider
    {
        public string GetPepper()
        {
            return "e7fcdedfd13e0a83848d21a24d0ff40e";
        }
    }
}