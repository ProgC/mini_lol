using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour 
{
    public GameObject[] mPrefabsMinions;
    public Nexus[] mNexuses;

    public GameObject mEff_VisionMove;
    public GameObject mEzreal_Q;
    public GameObject mEzreal_W;
    public GameObject mEzreal_Normal;
    
    List<GameObject> mEntities = new List<GameObject>();
        
	// Use this for initialization
	void Start () {
	
	}
	
	// 플레이어를 따라 다니게 만들자.
	void Update ()
    {        
	}

    public void AddEntity(GameObject obj)
    {
        mEntities.Add(obj);
    }

    public void RemoveEntity(GameObject obj)
    {
        mEntities.Remove(obj);        
    }

    public List<GameObject> SelectEntities(TeamIDs teamId, Vector3 mousePos)
    {
        List<GameObject> objects = new List<GameObject>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit[] hit = Physics.RaycastAll(ray);

        for (int i = 0; i < hit.Length; ++i)
        {
            EntityData data = hit[i].collider.gameObject.GetComponent<EntityData>();

            if (data != null)
            {
                if (data.mTeamIds != teamId)
                {
                    objects.Add(hit[i].collider.gameObject);
                }
            }
        }
                
        return objects;
    }

    public GameObject GetPrefabMinions(TeamIDs teamIds)
    {
        if (teamIds == TeamIDs.PLAYER_A)
        {
            return mPrefabsMinions[0];
        }
        else if (teamIds == TeamIDs.PLAYER_B)
        {
            return mPrefabsMinions[1];
        }
        
        return mPrefabsMinions[0];
    }

    public Nexus GetOpponentTeam(TeamIDs teamIds)
    {
        if (teamIds == TeamIDs.PLAYER_A)
        {
            return mNexuses[1];
        }
        else if (teamIds == TeamIDs.PLAYER_B)
        {
            return mNexuses[0];
        }

        return mNexuses[0];        
    }

    public static Vector3 GetMousePositionInWorld()
    {
        Vector3 worldPos = Vector3.zero;
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float d;
        if (plane.Raycast(ray, out d))
        {
            worldPos = ray.origin + ray.direction * d;            
        }
        return worldPos;
    }
     

}
