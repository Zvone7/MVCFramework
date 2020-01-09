using System;
using System.Collections.Generic;
using System.Text;

namespace MvcFrameworkCml.Transfer
{
    public class ContentError
    {
        public String Description { get; }
        public Exception Exception { get; }

        public ContentError(Exception e, String description)
        {
            Exception = e;
            Description = description;
        }
    }
}
