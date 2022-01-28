using System.Collections;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Collections.RadixTree.Tests")]
namespace Collections.RadixTree;

internal record RadixTreeNode : IEnumerable<RadixTreeNode>, IComparable<RadixTreeNode>
{
    internal RadixTreeNode()
    {
        Value = null;
        Children = new SortedSet<RadixTreeNode>();
        IsValidWord = false;
    }
    
    internal RadixTreeNode(string? value, SortedSet<RadixTreeNode> children, bool isValidWord)
    {
        Value = value;
        Children = children;
        IsValidWord = isValidWord;
    }

    internal string? Value { get; init; }
    internal SortedSet<RadixTreeNode> Children { get; init; }
    internal bool IsValidWord { get; init; }
    
    internal bool IsLeaf => Children.Count is 0;

    internal bool IsValuePartOfWord(string word) => !string.IsNullOrEmpty(Value) && word.StartsWith(Value);

    public IEnumerator<RadixTreeNode> GetEnumerator()
    {
        foreach (var child in Children)
        {
            yield return child;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int CompareTo(RadixTreeNode? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        
        var valueComparison = string.Compare(Value, other.Value, StringComparison.Ordinal);
        return valueComparison != 0 ? valueComparison : IsValidWord.CompareTo(other.IsValidWord);
    }
}