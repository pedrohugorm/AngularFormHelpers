using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace AngularFormHelpers.Library.LabTest
{
    public class FormBuilder<TModel>
    {

    }

    //public interface IFormElementCollection : ICollection<IFormElement>
    //{
        
    //}

    public interface IRenderFormElement
    {
        MvcHtmlString Render<TModel>(IFormItemItemElement<TModel> itemItem);
    }

    public interface IFormElement<TModel>
    {
        IFormItemItemElement<TModel> For<TProperty>(Expression<Func<TModel, TProperty>> expression);

        IFormItemCollectionElement<TModel> ForCollection<TProperty>(Expression<Func<TModel, TProperty>> expression)
            where TProperty : IEnumerable;
    }

    public interface ICustomRenderFormItemElement<TModel> : IFormElement<TModel>
    {
        IFormElement<TModel> AsCustomTemplate(string partial);
    }

    public interface IWrapFormItemElement<TModel> : IFormElement<TModel>
    {
        IFormElement<TModel> As<TRenderType>() where TRenderType : IRenderFormElement;
        IFormElement<TModel> AsDefault();
    }

    public interface IBaseElement<TModel>
    {
        IFormItemItemElement<TModel> Class(string className);
    }

    public interface IFormItemItemElement<TModel> : IWrapFormItemElement<TModel>, IBaseElement<TModel>
    {
        IFormItemItemElement<TModel> Placeholder(string placeholder);
        IFormItemItemElement<TModel> Label(string placeholder);
    }

    public interface IFormItemCollectionElement<TModel> : IBaseElement<TModel>
    {
        
    }

    public class FormElement<TModel> : IFormElement<TModel>
    {
        public IFormItemItemElement<TModel> For<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new FormItemItem<TModel, TProperty>(expression, this);
        }

        public IFormItemCollectionElement<TModel> ForCollection<TProperty>(
            Expression<Func<TModel, TProperty>> expression) where TProperty : IEnumerable
        {
            throw new NotImplementedException();
        }
    }

    public class FormItemItem<TModel, TProperty> : IFormItemItemElement<TModel>, ICustomRenderFormItemElement<TModel>, IWrapFormItemElement<TModel>
    {
        private readonly Expression<Func<TModel, TProperty>> _expression;
        private readonly IFormElement<TModel> _formElement;

        public FormItemItem(Expression<Func<TModel, TProperty>> expression, IFormElement<TModel> formElement)
        {
            _expression = expression;
            _formElement = formElement;
        }

        public IFormElement<TModel> As<TRenderType>() where TRenderType : IRenderFormElement
        {
            return _formElement;
        }

        public IFormElement<TModel> AsDefault()
        {
            return _formElement;
        }

        public IFormItemItemElement<TModel> Class(string className)
        {
            return this;
        }

        public IFormItemItemElement<TModel> Placeholder(string placeholder)
        {
            return this;
        }

        public IFormItemItemElement<TModel> Label(string placeholder)
        {
            return this;
        }

        public IFormItemItemElement<TModel> For<TAnotherProperty>(Expression<Func<TModel, TAnotherProperty>> expression)
        {
            return _formElement.For(expression);
        }

        public IFormItemCollectionElement<TModel> ForCollection<TAnotherProperty>(
            Expression<Func<TModel, TAnotherProperty>> expression) where TAnotherProperty : IEnumerable
        {
            throw new NotImplementedException();
        }

        public IFormElement<TModel> AsCustomTemplate(string partial)
        {
            return _formElement;
        }
    }

    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTest { get; set; }

        public IEnumerable<TestModel> Items { get; set; }
    }

    public class Example
    {
        public Example()
        {
            new FormElement<TestModel>()
                .For(r => r.Id).Placeholder("Test").Class("dsasa").As<TextBox>()
                .For(r => r.Name).As<TextBox>()
                .For(r => r.IsTest).As<Checkbox>()
                .ForCollection(r => r.Items)
                ;
        }
    }

    public class TextBox : IRenderFormElement
    {
        public MvcHtmlString Render<TModel>(IFormItemItemElement<TModel> itemItem)
        {
            return new MvcHtmlString("");
        }
    }

    public class Checkbox : IRenderFormElement
    {
        public MvcHtmlString Render<TModel>(IFormItemItemElement<TModel> itemItem)
        {
            throw new NotImplementedException();
        }
    }
}