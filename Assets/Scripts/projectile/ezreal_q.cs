using UnityEngine;
using System.Collections;

public class ezreal_q : MonoBehaviour 
{
    public float mTime;
    public TeamIDs mTeam;
    
	// Use this for initialization
	void Start ()
    {
        mTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        mTime += Time.deltaTime;

        if (mTime > 0.2f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.position += transform.forward * 0.7f;
        }
	}
}
