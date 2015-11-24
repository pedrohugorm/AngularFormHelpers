using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace AngularFormHelpers.Library.Helpers.Form
{
    public interface IAngularModelExpression<TModel, TProperty>
    {
        [DataMember(Name = "data-ng-model")]
        Expression<Func<TModel, TProperty>> Model { set; get; }
    }
}