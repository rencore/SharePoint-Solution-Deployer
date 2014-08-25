using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace SPSD.VisualStudio.Wizard
{
    internal class GenericConverter<T> : TypeConverter
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
            return true;
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
            return TypeDescriptor.GetProperties(typeof(T), attributes);
        }

        ///<summary>
        ///Converts the given object to the type of this converter, using the specified context and culture information.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Object"></see> that represents the converted value.
        ///</returns>
        ///
        ///<param name="culture">The <see cref="T:System.Globalization.CultureInfo"></see> to use as the current culture. </param>
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        ///<param name="value">The <see cref="T:System.Object"></see> to convert. </param>
        ///<exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string text = value as string;
            if (text == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            string text2 = text.Trim();
            //if (context.PropertyDescriptor.Attributes[typeof(SerializableAttribute)] != null)
            //{
            //    StreamReader reader = new StreamReader(text2);
            //    XmlSerializer serializer = new XmlSerializer(typeof(T));
            //    return serializer.Deserialize(reader);
            //}

            if (context == null)
            {
                return null;
            }
            if (text2.Length == 0)
            {
                return null;
            }
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            char ch = culture.TextInfo.ListSeparator[0];
            string[] textArray = text2.Split(new char[] { ch });
            PropertyInfo[] properties = typeof(T).GetProperties();
            object instance = Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[0]);
            int current = 0;
            if (constructor != null)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    if (!properties[i].CanWrite)
                    {
                        continue;
                    }
                    string s = TypeDescriptor.GetConverter(properties[i].PropertyType).ConvertToString(context, culture, properties[i].GetValue(instance, null));
                    int count = s.Split(new char[] { ch }).Length;
                    string tmpString = string.Empty;
                    for (int j = 0; j < count; j++)
                    {
                        tmpString += textArray[current + j] + ch;
                    }
                    current += count;
                    string[] parts = tmpString.Trim(new char[] { ch }).Split(new string[] { "=" }, StringSplitOptions.None);
                    if (TypeDescriptor.GetConverter(properties[i].PropertyType).CanConvertFrom(typeof(string)))
                    {

                        object val = TypeDescriptor.GetConverter(properties[i].PropertyType).ConvertFromString(parts.Length == 2 ? parts[1].Trim(new char[] { '[', ']' }) : string.Empty);
                        properties[i].SetValue(instance, val, new object[0]);
                    }
                    else
                    {
                        object val = properties[i].GetValue(context.PropertyDescriptor.GetValue(context.Instance), null);
                        properties[i].SetValue(instance, val, new object[0]);
                    }
                }
            }
            return instance;
        }

        ///<summary>
        ///Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter"></see> is associated with, using the specified context, given a set of property values for the object.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Object"></see> representing the given <see cref="T:System.Collections.IDictionary"></see>, or null if the object cannot be created. This method always returns null.
        ///</returns>
        ///
        ///<param name="propertyValues">An <see cref="T:System.Collections.IDictionary"></see> of new property values. </param>
        ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            object instance = Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (!properties[i].CanWrite)
                {
                    continue;
                }
                if (propertyValues[properties[i].Name] == null)
                {
                    continue;
                }
                properties[i].SetValue(instance, propertyValues[properties[i].Name], new object[0]);
            }
            return instance;
        }

        //public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        //{
        //    if (destinationType == null)
        //    {
        //        throw new ArgumentNullException("destinationType");
        //    }
        //    if (value is T)
        //    {
        //        return value.ToString();
        //    }
        //    return base.ConvertTo(context, culture, value, destinationType);
        //}

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
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if (value is T)
            {
                if (destinationType == typeof(string))
                {
                    T tVal = (T)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    PropertyInfo[] properties = tVal.GetType().GetProperties();
                    string separator = culture.TextInfo.ListSeparator[0].ToString();
                    string[] textArray = new string[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        if (properties[i].CanWrite)
                        {
                            textArray[i] = properties[i].Name + "=[" + TypeDescriptor.GetConverter(properties[i].PropertyType).ConvertToString(context, culture, properties[i].GetValue(value, null)) + "]";
                        }
                    }
                    string retVal = string.Empty;
                    for (int i = 0; i < textArray.Length; i++)
                    {
                        if (textArray[i] != null)
                        {
                            retVal += textArray[i] + separator;
                        }
                    }
                    return retVal.TrimEnd(new char[] { separator[0] });
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    return new InstanceDescriptor(typeof(T).GetConstructor(new Type[0]), null, false);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
