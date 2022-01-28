using System.Collections.Generic;
using Xunit;

namespace Collections.RadixTree.Tests;

public class EnumerationTests
{
    [Fact]
    public void GivenARadixTreeWith8Words_WhenEnumerating_ThenAll8WordsAreReturned()
    {
        var expectedWords = new SortedSet<string>
        {
            "fin", "find", "finish", "happy", "hell", "hello", "hi", "him"
        };

        var tree = new RadixTree();
        foreach (var word in expectedWords)
        {
            tree.Add(word);
        }

        var actualWords = new List<string>();
        foreach (var word in tree)
        {
            actualWords.Add(word);
        }

        Assert.Equal(expectedWords, actualWords);
    }
}