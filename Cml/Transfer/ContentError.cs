using System;

namespace Cml.Transfer
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
