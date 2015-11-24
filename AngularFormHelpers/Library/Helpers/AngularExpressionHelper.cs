using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace AngularFormHelpers.Library.Helpers
{
    /// <summary>
    /// Angular JS Helpers for ASP.NET MVC. Instead of using strings, which is not maintanable, use Lambda expressions. 
    /// With that you can refactor your code without messing your angular js code ;)
    /// Soon to add support for filters
    /// </summary>
    public static class AngularExpressionHelper
    {
        public static string NgExpression<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            string varName,
            Expression<Func<TModel, TProperty>> expression,
            params object[] filters)
        {
            var data = AngularExpressionData.FromExpression(expression, varName, filters);

            return data.ToString();
        }

        public static string NgExpression<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, params object[] filters)
        {
            var data = AngularExpressionData.FromExpression(expression, filters);

            return data.ToString();
        }

        public static string Ng<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            string varName,
            Expression<Func<TModel, TProperty>> expression,
            params object[] filters)
        {
            var data = AngularExpressionData.FromExpression(expression, varName, filters);

            return data.GetExpressionString();
        }

        public static string Ng<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, params object[] filters)
        {
            var data = AngularExpressionData.FromExpression(expression, filters);

            return data.GetExpressionString();
        }
    }
}