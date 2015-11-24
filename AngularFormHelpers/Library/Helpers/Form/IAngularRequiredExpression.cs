using System.Runtime.Serialization;

namespace AngularFormHelpers.Library.Helpers.Form
{
    public interface IAngularRequiredExpression
    {
        [DataMember(Name = "data-ng-required")]
        string RequiredExpression { set; get; }
    }
}