using System.Linq;
using Xunit;

namespace Collections.RadixTree.Tests;

public class NodeTests
{
    [Theory]
    [InlineData("random")]
    [InlineData("words")]
    [InlineData("root")]
    public void GivenAnEmptyRadixTree_WhenAddingOneWord_ThenTheWordIsTheRootValue(string word)
    {
        var tree = new RadixTree { word };
        Assert.Equal(tree.Root.Value, word);
    }
    
    [Theory]
    [InlineData("unrelated")]
    [InlineData("words")]
    [InlineData("children")]
    public void GivenARadixTreeWithOneWord_WhenAddingANewWordThatIsUnrelatedToTheExistingWord_ThenTheRootIsAssignedAnEmptyValue_AndTheWordsAreAddedAsChildren(string word)
    {
        var tree = new RadixTree { "test" };
        tree.Add(word);
        
        Assert.Equal(string.Empty, tree.Root.Value);
        Assert.Equal(2, tree.Root.Children.Count);
        
        Assert.Contains(tree.Root.Children, node => node.Value == word);
        Assert.Contains(tree.Root.Children, node => node.Value == "test");
    }

    [Theory]
    [InlineData("testing")]
    [InlineData("testify")]
    [InlineData("testers")]
    public void GivenARadixTreeWithOneWord_WhenAddingANewWordThatStartsWithTheExistingWord_ThenTheRootKeepsTheExistingWordAsValue_AndTheNewWordIsAddedAsAChild(string word)
    {
        var tree = new RadixTree { "test" };
        tree.Add(word);
        
        Assert.Equal("test", tree.Root.Value);
        Assert.Single(tree.Root.Children, child => child.Value == word["test".Length..]);
    }

    [Theory]
    [InlineData("testing", "test")]
    [InlineData("hello", "he")]
    public void GivenARadixTreeWithTwoUnrelatedWords_WhenAddingARelatedWord_ThenANewChildNodeIsCreatedUnderTheRelatedWord(string newWord, string relatedWord)
    {
        var tree = new RadixTree { "test", "he" };
        tree.Add(newWord);
        
        Assert.Equal(2, tree.Root.Children.Count);

        var expectedParentNode = tree.Root.Children.Single(child => child.Value == relatedWord);
        Assert.Single(expectedParentNode.Children, node => node.Value == newWord[relatedWord.Length..]);
        
        var expectedSolitaryNode = tree.Root.Children.Single(child => child.Value != relatedWord);
        Assert.Empty(expectedSolitaryNode.Children);
    }
}