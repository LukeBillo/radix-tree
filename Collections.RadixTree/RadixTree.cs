using System.Collections;
using System.Text;

namespace Collections.RadixTree;

public class RadixTree : IRadixTree
{
    private RadixTreeNode _root = new();
    
    public int Count => GetValidWords().Count();
    public bool IsReadOnly => false;

    public void Add(string item)
    {
        throw new NotImplementedException();
    }

    public void Clear() => _root = new RadixTreeNode();

    public bool Contains(string item) => GetValidWords().Contains(item);

    public void CopyTo(string[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(string item)
    {
        throw new NotImplementedException();
    }
    
    public bool TryAdd(string value)
    {
        throw new NotImplementedException();
    }

    public bool TryRemove(string value)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<string> GetEnumerator() => GetValidWords().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IEnumerable<string> GetValidWords() => GetValidWordsRecursive(_root, new StringBuilder());

    private static IEnumerable<string> GetValidWordsRecursive(RadixTreeNode node, StringBuilder word)
    {
        if (!string.IsNullOrEmpty(node.Value))
        {
            word.Append(node.Value);
        }

        if (node.IsValidWord)
        {
            yield return word.ToString();
        }

        if (node.IsLeaf)
        {
            yield break;
        }

        foreach (var wordFromChild in node.Children
                     .Select(child => GetValidWordsRecursive(child, word))
                     .SelectMany(words => words))
        {
            yield return wordFromChild;
        }
    }
}