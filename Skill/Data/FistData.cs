namespace Platformer2D
{
    public class FistData : SkillData
    {
        protected override bool CancelAttackCondition()
        {
            return true;
        }

        protected override bool HoldAttackCondition()
        {
            return true;
        }

        protected override bool StartAttackCondition()
        {
            return true;
        }
    }
}
