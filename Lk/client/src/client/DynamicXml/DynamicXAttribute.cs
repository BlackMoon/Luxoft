using System;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Globalization;
using System.Xml.Linq;

namespace client.DynamicXml
{
    public class DynamicXAttribute : DynamicObject
    {
        private readonly XAttribute _attribute;
        
        private DynamicXAttribute(XAttribute attribute)
        {
            Contract.Requires(attribute != null);
            _attribute = attribute;
        }

        public static dynamic CreateInstance(XAttribute attribute)
        {
            Contract.Requires(attribute != null);
            Contract.Ensures(Contract.Result<object>() != null);
            return new DynamicXAttribute(attribute);
        }
        
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.ReturnType == typeof(XAttribute))
            {
                result = _attribute;
                return true;
            }
            string underlyingValue = _attribute.Value;
            
            // We shoult treat TimeSpan separately, because we should call Parse method
            // instead of Convert.ChangeType
            if (binder.ReturnType == typeof(TimeSpan))
            {
                result = TimeSpan.Parse(underlyingValue);
                return true;
            }
            
            // For the rest of the types we could use Convert.ChangeType             
            // If this conversion succeeded ok, otherwise we'll have exception
            result = Convert.ChangeType(underlyingValue, binder.ReturnType, CultureInfo.InvariantCulture);
            return true;
        }

        public string Value
        {
            get
            {
                return _attribute.Value;
            }
        }    
    }
}