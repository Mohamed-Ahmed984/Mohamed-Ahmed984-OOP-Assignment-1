using System;

public class Solution
{
    public int MaxOperations(int[] nums, int k)
    {
        Array.Sort(nums);

        int left = 0;
        int right = nums.Length - 1;
        int count = 0;

        while (left < right)
        {
            int sum = nums[left] + nums[right];

            if (sum == k)
            {
                count++;
                left++;
                right--;
            }
            else if (sum < k)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return count;
    }
}
