using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace AngularFormHelpers.Library.Helpers.Form
{
    [DataContract]
    public class AngularHtmlAttributes
    {
        [DataMember]
        public InputType Type { set; get; }
        [DataMember]
        public string[] Class { set; get; }
        [DataMember]
        public string Placeholder { set; get; }
        
        public bool IsRequired { set; get; }

        [DataMember]
        public string Required
        {
            get { return IsRequired ? "required" : string.Empty; }
        }

        public bool IsReadonly { set; get; }

        [DataMember]
        public string Readonly
        {
            get { return IsReadonly ? "readonly" : string.Empty; }
        }
    }

    public class AngularInputAttributes<TModel, TProperty> : AngularHtmlAttributes, IAngularRequiredExpression,
        IAngularModelExpression<TModel, TProperty>
    {
        public string RequiredExpression { get; set; }
        public Expression<Func<TModel, TProperty>> Model { get; set; }
    }
}