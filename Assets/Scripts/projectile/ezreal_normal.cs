using UnityEngine;
using System.Collections;

public class ezreal_normal : MonoBehaviour 
{
    public float mSpeed = 0.3f;
    public TeamIDs mTeam;
    public GameObject mTargetObject;

	// Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mTargetObject == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (Vector3.Distance(transform.position, mTargetObject.transform.position) > 1)
            {
                Vector3 dir = mTargetObject.transform.position - transform.position;
                transform.position += dir.normalized * mSpeed;
                transform.LookAt(mTargetObject.transform.position);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
