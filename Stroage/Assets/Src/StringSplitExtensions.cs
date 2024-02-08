using System;


public static class StringSplitExtensions
{
    public static ReadOnlySplitEnumerator SplitReadOnlySpan(this string str, char[] separator)
    {
        return new ReadOnlySplitEnumerator(str.AsSpan(), separator);
    }
    public static SplitEnumerator SplitSpan(this string str, char[] separator,out char[] result)
    {
        result = str.ToCharArray();
        return new SplitEnumerator(result, separator);
    }

}

public ref struct ReadOnlySplitEntry
{
    public ReadOnlySpan<char> first { get; private set; }
    public ReadOnlySplitEntry(ReadOnlySpan<char> first)
    {
        this.first = first;
    }
    public static implicit operator ReadOnlySpan<char>(ReadOnlySplitEntry entry) => entry.first;
}

public ref struct SplitEntry
{
    public Span<char> first { get; private set; }
    public bool isEnd;
    public SplitEntry(Span<char> first, bool isEnd)
    {
        this.first = first;
        this.isEnd = isEnd;
    }
    
    public static implicit operator Span<char>(SplitEntry entry) => entry.first;
}

public ref struct ReadOnlySplitEnumerator
{
    private ReadOnlySpan<char> _str;
    private ReadOnlySpan<char> _separator;
    public ReadOnlySplitEntry Current { get; private set; }
    
    public ReadOnlySplitEnumerator GetEnumerator() => this;
    public ReadOnlySplitEnumerator(ReadOnlySpan<char> str, char[] separator)
    {
        _str = str;
        _separator = separator;
        Current = default;
    }
    
    public bool MoveNext()
    {
        var s = _str;
        if (s.Length == 0)
            return false;
        var index = _str.IndexOf(_separator);
        if (index == -1)
        {
            _str = ReadOnlySpan<char>.Empty;
            Current = new ReadOnlySplitEntry(s);
            return true;
        }
        Current = new ReadOnlySplitEntry(s.Slice(0, index));
        _str = s.Slice(index + 1);
        return true;
    }
}
public ref struct SplitEnumerator
{
    private Span<char> _str;
    private ReadOnlySpan<char> _separator;
    public SplitEntry Current { get; private set; }
    
    public SplitEnumerator GetEnumerator() => this;
    public SplitEnumerator(char[] str, char[] separator)
    {
        _str = str;
        _separator = separator;
        Current = default;
    }
    
    public bool MoveNext()
    {
        var s = _str;
        if (s.Length == 0)
            return false;
        int index = 0;
        for (int i = 0 ; i < s.Length; i++)
        {
            char c = s[i];
            for (int j = 0; j < _separator.Length; j++)
            {
                if (_separator[j] == c)
                {
                    index = i;
                    break;
                }
            }
        }
        //var index = _str.IndexOf(_separator);
        if (index == -1)
        {
            _str = Span<char>.Empty;
            Current = new SplitEntry(s,true);
            return true;
        }
        Current = new SplitEntry(s.Slice(0, index), false);
        _str = s.Slice(index + 1);
        return true;
    }
}
