using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using AngularFormHelpers.Library.Extension;

namespace AngularFormHelpers.Library.Helpers.Form
{
    public static class AngularInputHelper
    {
        public static MvcHtmlString NgTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string placeholder = "", bool required = false)
        {
            var attributes = new AngularInputAttributes<TModel, TProperty>
            {
                IsRequired = required,
                Placeholder = placeholder,
                Model = expression,
                Type = InputType.Text
            };

            return htmlHelper.TextBoxFor(expression, attributes.ToDictionary<TModel, TProperty>());
        }
    }
}