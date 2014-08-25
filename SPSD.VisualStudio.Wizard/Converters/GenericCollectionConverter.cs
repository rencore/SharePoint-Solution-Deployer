using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Globalization;

namespace SPSD.VisualStudio.Wizard
{
    [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
    internal class GenericCollectionConverter<T> : TypeConverter
    {
        ///<summary>
        ///Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        ///</summary>
        ///
        ///<returns>
        ///true if this converter can perform the conversion; otherwise, false.
        ///</returns>
        ///
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        ///<param name="sourceType">A <see cref="T:System.Type"></see> that represents the type you want to convert from. </param>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        ///<summary>
        ///Returns whether this converter can convert the object to the specified type, using the specified context.
        ///</summary>
        ///
        ///<returns>
        ///true if this converter can perform the conversion; otherwise, false.
        ///</returns>
        ///
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        ///<param name="destinationType">A <see cref="T:System.Type"></see> that represents the type you want to convert to. </param>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        ///<summary>
        ///Converts the given value object to the specified type, using the specified context and culture information.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Object"></see> that represents the converted value.
        ///</returns>
        ///
        ///<param name="culture">A <see cref="T:System.Globalization.CultureInfo"></see>. If null is passed, the current culture is assumed. </param>
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        ///<param name="destinationType">The <see cref="T:System.Type"></see> to convert the value parameter to. </param>
        ///<param name="value">The <see cref="T:System.Object"></see> to convert. </param>
        ///<exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        ///<exception cref="T:System.ArgumentNullException">The destinationType parameter is null. </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is GenericCollection<T>)
            {
                return "(Items)";
            }
            if (value is CollectionBase)
            {
                return "(Items)";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        ///<summary>
        ///Returns whether this object supports properties, using the specified context.
        ///</summary>
        ///
        ///<returns>
        ///true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)"></see> should be called to find the properties of this object; otherwise, false.
        ///</returns>
        ///
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        ///<summary>
        ///Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)"></see> to create a new value, using the specified context.
        ///</summary>
        ///
        ///<returns>
        ///true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)"></see> to create a new value; otherwise, false.
        ///</returns>
        ///
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        ///<summary>
        ///Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> with the properties that are exposed for this data type, or null if there are no properties.
        ///</returns>
        ///
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        ///<param name="attributes">An array of type <see cref="T:System.Attribute"></see> that is used as a filter. </param>
        ///<param name="value">An <see cref="T:System.Object"></see> that specifies the type of array for which to get properties. </param>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            GenericCollection<T> collection = value as GenericCollection<T>;
            if (collection != null)
            {
                PropertyDescriptor[] properties = new PropertyDescriptor[collection.Count];
                for (int i = 0; i < collection.Count; i++)
                {
                    try
                    {
                        properties[i] = (new PDesc<T>(collection[i]));
                    }
                    catch (Exception)
                    {
                    }
                }
                return new PropertyDescriptorCollection(properties);
            }
            return base.GetProperties(context, value, attributes);
        }
    }
}
