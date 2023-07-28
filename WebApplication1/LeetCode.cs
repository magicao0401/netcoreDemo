namespace WebApplication1
{
    public static class LeetCode
    {
        public static long FindTheArrayConcVal(int[] nums)
        {
            long result = 0;
            var mid = nums.Length / 2 ;
           
            for (int i = 0;i<mid;i++)
            {
                result += Int64.Parse($"{nums[i]}{nums[nums.Length -1 - i]}");
            }
          if(nums.Length % 2 != 0)
            {
                result += nums[nums.Length / 2];
            }
            return result;
        }

        public static bool SumOfNumberAndReverse(int num)
        {
            for (int i = 0; i < num; i++)
            {
                if(i + int.Parse(i.ToString().ToCharArray().Reverse().ToArray()) == num)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
