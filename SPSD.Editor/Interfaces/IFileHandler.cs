namespace SPSD.Editor.Interfaces
{
    internal interface IFileHandler<T>
    {
        void SetDefault();
        void LoadEnv(T node);
        T SaveEnv(T node);
    }
}