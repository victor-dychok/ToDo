using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Controllers
{
    public static class Validation
    {
        private const int IntNumericalLimit = 2147483647;
        public static bool IsOkIntIdValue(int value)
        {
            return value >= 0 && value <= IntNumericalLimit;
        }

        public static bool IsPositive(int value)
        {
            return value >= 0;
        }
    }
}
