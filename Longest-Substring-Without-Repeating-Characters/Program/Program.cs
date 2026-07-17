namespace Program;

class Program
{
    static void Main(string[] args)
    {
        Solution solution = new Solution();
        Console.WriteLine(solution.LengthOfLongestSubstring("abcabcbb"));
        Console.WriteLine(solution.LengthOfLongestSubstring("bbbbb"));
        Console.WriteLine(solution.LengthOfLongestSubstring("pwwkew"));
        Console.WriteLine(solution.LengthOfLongestSubstring("a"));
        Console.WriteLine(solution.LengthOfLongestSubstring(" "));
        Console.WriteLine(solution.LengthOfLongestSubstring("au"));
        Console.WriteLine(solution.LengthOfLongestSubstring("dvdf"));
    }
}

public class Solution {
    public int LengthOfLongestSubstring(string s)
    {
        int result = 0;
        int left = 0;
        Dictionary<char, int> lastSeen = new();

        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            if (lastSeen.TryGetValue(c, out int lastIndex) && lastIndex >= left)
            {
                left = lastIndex + 1;
            }
            lastSeen[c] = right;
            result = Math.Max(result, right - left + 1);
        }

        return result;
    }
}