using System.Collections;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Collections.RadixTree.Tests")]
namespace Collections.RadixTree;

public struct RadixTreeNode : IEnumerable<RadixTreeNode>
{
    public RadixTreeNode()
    {
        Value = null;
        Children = new SortedSet<RadixTreeNode>();
        IsValidWord = false;
    }
    
    public RadixTreeNode(string? value, SortedSet<RadixTreeNode> children, bool isValidWord)
    {
        Value = value;
        Children = children;
        IsValidWord = isValidWord;
    }

    public string? Value { get; private set; }
    public SortedSet<RadixTreeNode> Children { get; private set; }
    public bool IsValidWord { get; private set; }
    
    public bool IsLeaf => Children.Count is 0;
    
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
}