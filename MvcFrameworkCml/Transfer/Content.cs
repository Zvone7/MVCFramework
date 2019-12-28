using System;
using System.Collections.Generic;
using System.Text;

namespace MvcFrameworkCml.Transfer
{
    public class Content<T>
    {
        public T Data { get; private set; }
        public Boolean HasError { get; private set; }
        public List<ContentError> Errors { get; private set; }

        public Content()
        {
            Errors = new List<ContentError>();
        }

        public void AppendError(Exception e, String description = default)
        {
            HasError = true;
            if (Errors == null) Errors = new List<ContentError>();
            if (description == default) description = e.Message;
            Errors.Add(new ContentError(e, description));
        }
        public void AppendError(Content<T> content)
        {
            HasError = true;
            if (Errors == null) Errors = new List<ContentError>();
            Errors.AddRange(content.Errors);
        }

        public void SetData(T data)
        {
            HasError = false;
            Data = data;
        }
    }
}
