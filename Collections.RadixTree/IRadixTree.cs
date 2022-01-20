namespace Collections.RadixTree;

public interface IRadixTree : ICollection<string>
{
    bool TryAdd(string value);
    bool TryRemove(string value);
}