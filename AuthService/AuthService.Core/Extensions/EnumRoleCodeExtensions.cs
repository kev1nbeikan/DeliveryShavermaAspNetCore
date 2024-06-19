using AuthService.Core.Common;

namespace AuthService.Core.Extensions;

public static class EnumRoleCodeExtensions
{
    public static int ToInt(this RoleCode roleCode)
    {
        return (int)roleCode;
    }
    
    public static string ToIntString(this RoleCode roleCode)
    {
        return ((int)roleCode).ToString();
    }
}