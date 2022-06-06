namespace FallingSandSimulation.Items {
    public static class SandTool {
        /// <summary>
        /// 比较沙粒重量
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>-1表示first较轻,1表示second较轻,0表示等重</returns>
        public static int CompareWeight(BaseSandItem first, BaseSandItem second) {
            return ((int) first.sandTypeEnum).CompareTo((int) second.sandTypeEnum);
        }

        public static byte GetUpdateMask(int value) {
            return (byte) (value & (long) 1);
        }
    }

    public enum SandTypeEnum {
        Empty = 1 << 0,
        Water = 1 << 1,
        Sand = 1 << 2,
    }
}