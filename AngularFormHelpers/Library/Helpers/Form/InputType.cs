using System.Runtime.Serialization;

namespace AngularFormHelpers.Library.Helpers.Form
{
    public enum InputType
    {
        [EnumMember(Value = "text")]
        Text,
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "password")]
        Password
    }
}