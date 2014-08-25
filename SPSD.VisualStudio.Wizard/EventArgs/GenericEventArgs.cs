using System;

namespace SPSD.VisualStudio.Wizard
{
    public delegate void GenericEventHandler<T>(object sender, GenericEventArgs<T> tArgs);

    public class GenericEventArgs<T> : EventArgs
    {
        private T value;

        public GenericEventArgs()
        {
            value = default(T);
        }

        public GenericEventArgs(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}