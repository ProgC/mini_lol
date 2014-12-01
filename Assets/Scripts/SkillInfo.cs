using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillInfo
{
    public float mDistance;
    public float mCurCoolTime;
    public float mMaxCoolTime;
    public UISprite mSprite;
    
    public bool IsAvailable()
    {
        return mCurCoolTime >= mMaxCoolTime;
    }

    public void Used()
    {
        mCurCoolTime = 0;
    }

    public void Update()
    {
        mCurCoolTime += Time.deltaTime;
        
        if (mCurCoolTime >= mMaxCoolTime)
        {
            mCurCoolTime = mMaxCoolTime;
        }

        mSprite.fillAmount = mCurCoolTime / mMaxCoolTime;
    }
}
