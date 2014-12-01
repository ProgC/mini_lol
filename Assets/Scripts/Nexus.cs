using UnityEngine;
using System.Collections;

public class Nexus : MonoBehaviour
{
    public TeamIDs mTeam;
    public Main mMain;
    public float mTime;
    
	// Use this for initialization
	void Start () 
    {
        mTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        mTime += Time.deltaTime;

        if (mTime > 3)
        {
            GameObject obj = mMain.GetPrefabMinions(mTeam);

            GameObject instancedObject = (GameObject)GameObject.Instantiate(obj, transform.position, Quaternion.identity);
            MinionsAI ai = instancedObject.GetComponent<MinionsAI>();
            ai.mTeam = mTeam;
            ai.mMyTeam = this;
            ai.mOpponentTeam = mMain.GetOpponentTeam(mTeam);

            // 현재는 미니언만 들어가고 플레이어 챔프들은 이 리스트에 들어가지 않는다. 그렇기 때문에
            // select entity함수에서 로직을 작성하지 않아도 된다. (추후에 바꿀 것임)
            mMain.AddEntity(instancedObject);
            
            mTime = 0;
        }	    
	}
}
