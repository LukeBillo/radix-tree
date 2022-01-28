using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Collections.RadixTree.Tests;

public class AddTests
{
    [Theory]
    [InlineData("test")]
    [InlineData("word")]
    [InlineData("hello")]
    [InlineData("spaghetti")]
    [InlineData("1")]
    public void WhenInstantiatingARadixTreeWithOneWord_ThenTheWordIsInTheTree(string word)
    {
        var tree = new RadixTree { word };
        Assert.Single(tree, wordInTree => wordInTree == word);
    }

    [Theory]
    [InlineData("shot")]
    [InlineData("colder")]
    [InlineData("hello")]
    public void GivenARadixTreeWithTwoWords_WhenAddingAnExtraWord_ThenItIsAddedSuccessfully(string word)
    {
        var tree = new RadixTree
        {
            "hot", 
            "cold",
        };
        
        tree.Add(word);

        Assert.Equal(3, tree.Count);
        
        Assert.Contains(word, tree);
        Assert.Contains("hot", tree);
        Assert.Contains("cold", tree);
    }
}