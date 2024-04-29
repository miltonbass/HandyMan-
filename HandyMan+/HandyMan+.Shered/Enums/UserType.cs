using Orders.Shared.Enums;
using System.ComponentModel;

namespace Orders.Shared.Enums
{
    public enum UserType
    {
        [Description("Usuario")]
        Usuario,

        [Description("Proveedor")]
        Proveedor
    }
}
public class UserTypeEnumToList
{
    public static List<string> GetList()
    {
        return [.. Enum.GetNames(typeof(UserType))];
    }
}