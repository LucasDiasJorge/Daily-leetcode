using System.Globalization;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        Solution solution = new Solution();

        solution.TwoSum(new int[] { 3, 3 }, 6);
    }
}

public class Solution
{

    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> seen = new Dictionary<int, int>(); // valor -> índice

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (seen.TryGetValue(complement, out int j))
            {
                return new int[] { j, i };
            }

            seen[nums[i]] = i; // só insere depois de checar
        }

        return Array.Empty<int>(); // nenhum par encontrado
    }

}