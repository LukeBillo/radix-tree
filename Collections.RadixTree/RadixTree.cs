using System.Collections;
using System.Text;

namespace Collections.RadixTree;

public class RadixTree : IRadixTree
{
    internal RadixTreeNode Root { get; private set; } = new();

    public int Count => GetValidWords().Count();
    public bool IsReadOnly => false;

    public void Add(string word)
    {
        if (!TryAdd(word))
        {
            throw new InvalidOperationException($"Failed to add word \"{word}\" to radix tree");
        }
    }

    public void Clear() => Root = new RadixTreeNode();

    public bool Contains(string word) => GetValidWords().Contains(word);

    public void CopyTo(string[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(string word)
    {
        throw new NotImplementedException();
    }
    
    public bool TryAdd(string word)
    {
        var rootHasValue = !string.IsNullOrEmpty(Root.Value);
        
        if (Root.IsLeaf && !rootHasValue)
        {
            Root = Root with { Value = word, IsValidWord = true };
            return true;
        }
        
        if (rootHasValue && !Root.IsValuePartOfWord(word))
        {
            var children = new SortedSet<RadixTreeNode>
            {
                new(Root.Value, new SortedSet<RadixTreeNode>(), true),
                new(word, new SortedSet<RadixTreeNode>(), true)
            };
            
            Root = new RadixTreeNode(string.Empty, children, false);
            return true;
        }

        return TryAddRecursive(word, Root, new StringBuilder());
    }

    public bool TryRemove(string word)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<string> GetEnumerator() => GetValidWords().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IEnumerable<string> GetValidWords() => GetValidWordsRecursive(Root, new StringBuilder());

    private static IEnumerable<string> GetValidWordsRecursive(RadixTreeNode node, StringBuilder word)
    {
        if (!string.IsNullOrEmpty(node.Value))
        {
            word = word.Append(node.Value);
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
                     .Select(child => GetValidWordsRecursive(child, new StringBuilder(word.ToString())))
                     .SelectMany(words => words))
        {
            yield return wordFromChild;
        }
    }
    
    private static bool TryAddRecursive(string word, RadixTreeNode node, StringBuilder wordSoFar)
    {
        wordSoFar = wordSoFar.Append(node.Value);
        
        if (wordSoFar.ToString() == word)
        {
            return false;
        }

        var restOfWord = word[wordSoFar.Length..];
        var parentNodeForWord = node.Children.SingleOrDefault(child => child.IsValuePartOfWord(restOfWord));

        if (parentNodeForWord is not null)
        {
            return TryAddRecursive(word, parentNodeForWord, wordSoFar);
        }
        
        var newNode = new RadixTreeNode(restOfWord, new SortedSet<RadixTreeNode>(), true);
        node.Children.Add(newNode);
        
        return true;
    }
}