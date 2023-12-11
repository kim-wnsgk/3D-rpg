using UnityEngine;



[CreateAssetMenu(fileName = "LevelConfig", menuName = "Level/LevelConfig", order = 1)]

    
public class LevelConfig : ScriptableObject{
        [Header("Animation Curve")]
        public AnimationCurve animationCurve;
        public int MaxLevel;
        public int MaxRequiredExp;
        
       


        //To be called when a entity levels up
        public int GetRequiredExp(int level)
        {
            int requiredExperience = Mathf.RoundToInt(animationCurve.Evaluate(Mathf.InverseLerp(0, MaxLevel, level)) * MaxRequiredExp);
            return requiredExperience;
        }

        public int GetStat(int level)
        {
            int Stat = Mathf.RoundToInt(animationCurve.Evaluate(Mathf.InverseLerp(0, MaxLevel, level)) * MaxRequiredExp / 10);

            return Stat;
        }

    public int GetSP(int level)
    {
        int SP = Mathf.CeilToInt(animationCurve.Evaluate(Mathf.InverseLerp(0, MaxLevel, level)) * MaxRequiredExp / MaxLevel);

        return SP;
    }
}

