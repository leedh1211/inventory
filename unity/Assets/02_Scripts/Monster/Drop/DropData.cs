namespace _02_Scripts.Monster.Drop
{
    public class DropData
    {
        public int monsterId;
        public int userSeq;
        public float dropRateModifier;

        public DropData(int monsterId, int userSeq, float dropRateModifier)
        {
            this.monsterId = monsterId;
            this.userSeq = userSeq;
            this.dropRateModifier = dropRateModifier;
        }
    }
}