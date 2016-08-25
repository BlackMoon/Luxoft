using System.Xml.Linq;

namespace client.DynamicXml
{
    /// <summary>
    /// Static class with extensions methods for creating dynamic wrappers for XElement, XDocument and XAttribute
    /// </summary>
    public static class DynamicExtensions
    {
        /// <summary>
        /// Helper method that creates dynamic wrapper around XElement
        /// </summary>
        public static dynamic AsDynamic(this XElement element)
        {
            return DynamicXElement.CreateInstance(element);
        }

        /// <summary>
        /// Creates dynamic wrapper around XDocument.Root
        /// </summary>
        public static dynamic AsDynamic(this XDocument document)
        {
            return document.Root.AsDynamic();
        }

        /// <summary>
        /// Creates dynamic wrapper around XAttribute
        /// </summary>
        public static dynamic AsDynamic(this XAttribute attribute)
        {
            return DynamicXAttribute.CreateInstance(attribute);
        }

    }
}