using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace client.DynamicXml
{
    /// <summary>
    /// "Dynamic wrapper" around XElement for reading xml content dynamically
    /// </summary>
    /// 
    public class DynamicXElement : DynamicObject
    {
        private readonly XElement _element;

        private DynamicXElement(XElement element)
        {
            Contract.Requires(element != null);
            _element = element;
        }

        /// <summary>
        /// Factory made intended usage more clear. We "should" use object of this class dynamically.
        /// </summary>
        public static dynamic CreateInstance(XElement element)
        {
            Contract.Requires(element != null);
            Contract.Ensures(Contract.Result<object>() != null);
            return new DynamicXElement(element);
        }

        #region System.Object overrides
        // All overrides for System.Object methods simply delegates they calls to underlying element
        public sealed override string ToString()
        {
            return _element.ToString();
        }
        public sealed override bool Equals(object obj)
        {
            var rhs = obj as DynamicXElement;
            if (ReferenceEquals(rhs, null))
                return false;
            return _element == rhs._element;
        }
        public sealed override int GetHashCode()
        {
            return _element.GetHashCode();
        }
        #endregion System.Object overrides

        /// <summary>
        /// Converting dynamic XElement wrapper to any other type means converting (or extracting) underlying element.Value
        /// </summary>
        public override sealed bool TryConvert(ConvertBinder binder, out object result)
        {
            // We're returning underlying XElement if required
            if (binder.ReturnType == typeof(XElement))
            {
                result = _element;
                return true;
            }
            
            // We could treat dynamic wrapper as collection of it subelements
            if (binder.ReturnType.IsAssignableFrom(typeof(IEnumerable<XElement>)))
            {
                result = _element.Elements();
                return true;
            }
            
            string underlyingValue = _element.Value;
            // We shoult treat TimeSpan separately, because we should call Parse method instead of Convert.ChangeType
            if (binder.ReturnType == typeof(TimeSpan))
            {
                result = TimeSpan.Parse(underlyingValue);
                return true;
            }

            // For the rest of the types we could use Convert.ChangeType
            // If this conversion succeeded ok, otherwise we'll have exception
            result = Convert.ChangeType(underlyingValue, binder.ReturnType, System.Globalization.CultureInfo.InvariantCulture);
            return true;
        }

        /// <summary>
        /// This method called during access to underlying subelement
        /// </summary>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string binderName = binder.Name;
            Contract.Assume(binderName != null);
            // Finding apprpopriate subelement and creating dynamic wrapper if this subelement exists
            XElement subelement = _element.Element(binderName);
            if (subelement != null)
            {
                result = CreateInstance(subelement);
                return true;
            }
            // Calling base implementation leads to runtime exception
            return base.TryGetMember(binder, out result);
        }

        /// <summary>
        /// Indexer that returns XAttribute wrapper by XNode
        /// </summary>
        public dynamic this[XName name]
        {
            get
            {
                Contract.Requires(name != null);
                XAttribute attribute = _element.Attribute(name);
                if (attribute == null)
                    throw new InvalidOperationException("Attribute not found. Name: " + name.LocalName);
                return attribute.AsDynamic();
            }
        }

        /// <summary>
        /// Indexer that returns subelement by element index
        /// </summary>
        public dynamic this[int idx]
        {
            get
            {
                // For 0 index we returning current element
                if (idx == 0) 
                    return this;
                // For non-zero index we'll return appropriate peer.
                // We should take parent and then access to appropriate child element
                var parent = _element.Parent;
                Contract.Assume(parent != null);
                XElement subElement = parent.Elements().ElementAt(idx);
                // subElement can't be null. If we don't have an element with appropriate index, we'll have ArgumentOutOfRangeException. Lets suggest this to Static Code analyzer
                Contract.Assume(subElement != null);
                return CreateInstance(subElement);
            }
        }

        public sealed override IEnumerable<string> GetDynamicMemberNames()
        {
            return _element.Elements()
                .Select(x => x.Name.LocalName)
                .Distinct()
                .OrderBy(x => x);
        }

        /// <summary>
        /// Check if member exists
        /// </summary>
        public bool HasMember(string member)
        {
            return GetDynamicMemberNames().Contains(member);
        }

        public XElement XElement
        {
            get
            {
                return _element;
            }
        }
    }
}