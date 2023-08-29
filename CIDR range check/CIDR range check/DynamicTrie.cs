using System;
using System.Collections.Generic;

public class TrieNode
{
    public bool IsWord { get; set; }
    public Dictionary<char, TrieNode> Children { get; }

    public TrieNode()
    {
        IsWord = false;
        Children = new Dictionary<char, TrieNode>();
    }
}

public class Trie
{
    private TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    public void Insert(List<char> word)
    {
        TrieNode current = root;

        foreach (char c in word)
        {
            if (!current.Children.ContainsKey(c))
            {
                current.Children[c] = new TrieNode();
            }

            current = current.Children[c];
        }

        current.IsWord = true;
    }

    public bool Search(List<char> word)
    {
        TrieNode current = root;

        foreach (char c in word)
        {
            if (!current.Children.ContainsKey(c))
            {
                return false;
            }

            current = current.Children[c];
        }

        return current.IsWord;
    }
}

public class DynamicTrie
{
    public static void Main(string[] args)
    {
        List<List<char>> words = new List<List<char>>
        {
            new List<char> { 'c', 'a', 'r' },
            new List<char> { 'c', 'a', 't' },
            new List<char> { 'c', 'a', 't', 's' },
            new List<char> { 'd', 'o', 'g' },
            new List<char> { 'd', 'o', 'g', 's' }
        };

        Trie trie = new Trie();

        foreach (var word in words)
        {
            trie.Insert(word);
        }

        Console.WriteLine(trie.Search(new List<char> { 'c', 'a', 'r' }));    // Output: True
        Console.WriteLine(trie.Search(new List<char> { 'c', 'a', 't' }));    // Output: True
        Console.WriteLine(trie.Search(new List<char> { 'c', 'a', 't', 's' }));    // Output: True
        Console.WriteLine(trie.Search(new List<char> { 'd', 'o', 'g' }));    // Output: True
        Console.WriteLine(trie.Search(new List<char> { 'd', 'o', 'g', 's' }));    // Output: True
        Console.WriteLine(trie.Search(new List<char> { 'c', 'a', 'r', 't' }));    // Output: False
        Console.WriteLine(trie.Search(new List<char> { 'd', 'o' }));    // Output: False
    }
}
