using System;

namespace Pgh.Common.Common
{
    public static class Common
    {
        public static bool IsGuid(this Guid? guid)
        {
            return (!guid.HasValue || guid.Value == Guid.Empty);
        }
    }
}
