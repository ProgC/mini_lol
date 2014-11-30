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

            mMain.AddEntity(instancedObject);
            
            mTime = 0;
        }	    
	}
}
