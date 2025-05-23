namespace SharedScripts.ItemGeneratorScripts
{
    public interface IAffix<T> where T : struct
    {
        public        T          Value       { get; }
        public static Range<T>[] Tiers       { get; }
        public        string     Description { get; }
    }
}