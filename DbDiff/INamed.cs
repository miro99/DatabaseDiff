namespace DbDiff
{
    public interface INamed
    {
        string Name { get; }

        bool Equals(object obj);
        int GetHashCode();
    }
}