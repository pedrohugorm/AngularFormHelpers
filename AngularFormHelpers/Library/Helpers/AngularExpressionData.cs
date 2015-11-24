using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using WebGrease.Css.Extensions;

namespace AngularFormHelpers.Library.Helpers
{
    public class AngularExpressionData
    {
        public const string ExpressionFormat = "{{{{ {0} }}}}";
        public const string FilterSeparator = " | ";
        public const string FilterWithParam = "{0} : {1}";
        public const string FilterWithoutParam = "{0}";
        public const string PropertySeparator = ".";

        public string LeftName { set; get; }
        public string[] RightName { set; get; }

        public RouteValueDictionary Filters { set; get; }

        private AngularExpressionData()
        {
            Filters = new RouteValueDictionary();
        }

        private AngularExpressionData(string properties, string varName = null)
            : this()
        {
            LeftName = varName;
            RightName = properties.Split(new[] { PropertySeparator }, StringSplitOptions.None);
        }

        public static AngularExpressionData FromExpression<T>(Expression<T> expression, string varName = null)
        {
            return new AngularExpressionData(ExpressionHelper.GetExpressionText(expression), varName);
        }

        public static AngularExpressionData FromExpression<T>(Expression<T> expression, string varName = null, params object[] filters)
        {
            var ang = new AngularExpressionData(ExpressionHelper.GetExpressionText(expression), varName);
            ang.GetFilters(filters);

            return ang;
        }

        public static AngularExpressionData FromExpression<T>(Expression<T> expression, params object[] filters)
        {
            var ang = new AngularExpressionData(ExpressionHelper.GetExpressionText(expression));
            ang.GetFilters(filters);

            return ang;
        }

        private void GetFilters(params object[] filters)
        {
            foreach (var filter in filters)
            {
                if (filter is string)
                    Filters.Add(filter as string, null);
                else if (filter is AngularExpressionData)
                {
                    var ang = filter as AngularExpressionData;
                    Filters.Add(ang.GetExpressionString(), null);
                }
                else
                {
                    new RouteValueDictionary(filter).ForEach(r =>
                    {
                        if (r.Value is string)
                            Filters.Add(r.Key, string.Format((string) "'{0}'", (object) r.Value));
                    });
                }
            }
        }

        public string GetFiltersAsString()
        {
            return string.Join(FilterSeparator, Filters.Select(r =>
            {
                if (r.Value == null)
                    return string.Format((string) FilterWithoutParam, (object) r.Key);

                return string.Format(FilterWithParam, r.Key, r.Value);
            }));
        }

        public string GetExpressionString()
        {
            var list = new List<string>();

            if (!string.IsNullOrEmpty(LeftName))
                list.Add(LeftName);

            if (RightName.Any())
                list.Add(string.Join(PropertySeparator, RightName));

            return string.Join(PropertySeparator, list);
        }

        public override string ToString()
        {
            var list = new List<string> {GetExpressionString()};

            if (Filters.Any())
                list.Add(GetFiltersAsString());

            return string.Format(ExpressionFormat, string.Join(FilterSeparator, list));
        }
    }
}