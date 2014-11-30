using UnityEngine;
using System.Collections;

public class MinionsAI : MonoBehaviour 
{
    public TeamIDs mTeam;
    public Nexus mMyTeam;
    public Nexus mOpponentTeam;
    public float mSpeed = 0.01f;

    EntityData mEntityData;
    

    // Use this for initialization
	void Start () {
        mEntityData = gameObject.GetComponent<EntityData>();

        mSpeed = mEntityData.mSpeed / 1000.0f / 30.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 dir = mOpponentTeam.transform.position - transform.position;

        if (dir.magnitude > 3)
        {
            transform.position += dir.normalized * mSpeed;
        }
        else
        {
            mMyTeam.mMain.RemoveEntity(this.gameObject);

            Destroy(this.gameObject);
        }
	}
}
