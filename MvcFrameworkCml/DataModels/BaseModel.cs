using System;

namespace MvcFrameworkCml.DataModels
{
    public abstract class BaseModel
    {
        public Int32 Id;
        public BaseModel(Int32 id)
        {
            Id = id;
        }
    }
}
