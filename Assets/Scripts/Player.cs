using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{    
    public Main mMain;

    public TeamIDs mTeamID;
        
	// Use this for initialization
    Vector3 mTarget = Vector3.zero;
    float mSpeed = 0.1f;
    float mY = 0.5f;

    EntityData mEntityData;

    // 
    GameObject mTargetObject = null;

    enum PlayerState
    {
        IDLE,
        MOVE,
        ATTACK
    }

    enum RightButtonState
    {
        NORMAL,
        ATTACK
    }
        
    PlayerState mPlayerState = PlayerState.IDLE;
    RightButtonState mRightButtonState = RightButtonState.NORMAL;
    float mAttackTime;
    
	void Start () 
    {
        mEntityData = gameObject.GetComponent<EntityData>();        	    

        Debug.Log(mEntityData.mHP);
        Debug.Log(mEntityData.mAttackSpeed);
	}

    // hack
    public void SetPosition(Vector3 newPos)
    {
        // update direction
        transform.LookAt(newPos);

        //Vector3 dir = newPos - transform.position;
        transform.position = newPos;        
        mTarget = newPos;

        //mPlayerState = PlayerState.IDLE;
    }

    public void SetDirection(Vector3 newDir)
    {
        transform.LookAt(newDir);
    }
	
	// Update is called once per frame
	void Update () 
    {
        // right button
        //  right button mode
        //    -> normal
        //      -> on unit
        //             -> attack
        //      -> on ground
        //             -> move
        //    -> attacking mode
        //      -> on unit
        //             -> attack
        //      -> on ground
        //             -> attack on ground

        if (Input.GetMouseButtonDown(1))
        {
            if ( mRightButtonState == RightButtonState.NORMAL )
            {
                List<GameObject> entities = mMain.SelectEntities(mTeamID, Input.mousePosition);

                if (entities.Count > 0)
                {
                    // on unit
                    //Debug.Log("attack on unit in normal mode");
                    //entities[0]
                    mTargetObject = entities[0];
                    mPlayerState = PlayerState.ATTACK;
                    //mAttackTime = 0;
                }
                else
                {
                    // on ground so move the player                
                    Plane plane = new Plane(Vector3.up, Vector3.zero);
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    float d;
                    if (plane.Raycast(ray, out d))
                    {
                        mTarget = ray.origin + ray.direction * d;
                        mTarget.y = mY;
                        mPlayerState = PlayerState.MOVE;
                    }
                }
            }
            else if (mRightButtonState == RightButtonState.ATTACK)
            {
                // todo
                
            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    GameObject obj = mMain.SelectEntity(SelectionMode.Attack_On_Unit
        //        GetTarget(Input.mousePosition);

        //    if ( obj != null )
        //    {                
        //        //mTargetObject
                
        //    }
        //}
                
        Vector3 dir = mTarget - transform.position;

        if (mPlayerState == PlayerState.MOVE)
        {
            if (Vector3.Distance(transform.position, mTarget) > 1)
            {
                transform.position += dir.normalized * mSpeed;
                transform.LookAt(mTarget);
            }
            else
            {
                mPlayerState = PlayerState.IDLE;
            }
        }

        mAttackTime += Time.deltaTime;
        if (mAttackTime >= (1.0f / mEntityData.mAttackSpeed))
        {
            //mAttackTime = 0;
        }

        if ( mPlayerState == PlayerState.ATTACK )
        {
            if (mAttackTime >= (1.0f / mEntityData.mAttackSpeed))
            {
                // Attack to target object
                GameObject obj = (GameObject)GameObject.Instantiate(mMain.mEzreal_Normal);
                obj.GetComponent<ezreal_normal>().mTeam = mTeamID;
                obj.GetComponent<ezreal_normal>().mTargetObject = mTargetObject;
                
                // bug
                obj.transform.position = transform.position;
                obj.transform.LookAt(transform.position + dir.normalized * 3);                                
                SetDirection(mTargetObject.transform.position);
                
                mAttackTime = 0;
            }
        }

        

        transform.position = new Vector3(transform.position.x, mY, transform.position.z);
	}
}
