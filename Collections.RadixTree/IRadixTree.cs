namespace Collections.RadixTree;

public interface IRadixTree : ICollection<string>
{
    bool TryAdd(string word);
    bool TryRemove(string word);
}