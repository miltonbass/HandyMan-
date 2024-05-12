using Orders.Shared.Enums;
using System.ComponentModel;

namespace Orders.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,

        [Description("Usuario")]
        User,

        [Description("Usuario Premium")]
        UserPremium,

        [Description("Especialista")]
        Specialist

        
    }
}
public class UserTypeEnumToList
{
    public static List<string> GetList()
    {
        return [.. Enum.GetNames(typeof(UserType))];
    }
}