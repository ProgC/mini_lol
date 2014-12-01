using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{    
    public Main mMain;

    public TeamIDs mTeamID;
        
	// Use this for initialization
    Vector3 mTargetPos = Vector3.zero;
    float mSpeed = 0.1f;
    float mY = 0.5f;

    EntityData mEntityData;
    
    // 
    GameObject mTargetObject = null;

    public CursorManager mCursorMgr;
        
    enum PlayerState
    {
        IDLE,
        MOVE,
        ATTACK
    }
        
    enum MouseButtonState
    {
        NORMAL,
        ATTACK
    }
        
    PlayerState mPlayerState = PlayerState.IDLE;
    
    // 오르쪽 마우스 용도만이 아니기 때문에 이름을 바꾸었다.
    MouseButtonState mMouseButtonState = MouseButtonState.NORMAL;
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
        mTargetPos = newPos;

        //mPlayerState = PlayerState.IDLE;
    }

    public void SetDirection(Vector3 newDir)
    {
        transform.LookAt(newDir);
    }

    static float AMOUNT_OF_MOUSEMOVE = 0.6f; 
    static int SIZE_OF_BORDER = 10;

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

        // 마우스가 화면 외곽에 있는지 검사한다.                
        if (Input.mousePosition.x < SIZE_OF_BORDER)
        {
            mMain.MoveCameraX(-AMOUNT_OF_MOUSEMOVE);
        }
        else if (Mathf.Abs(Screen.width - Input.mousePosition.x) < SIZE_OF_BORDER)
        {
            mMain.MoveCameraX(AMOUNT_OF_MOUSEMOVE);
        }

        if (Input.mousePosition.y < SIZE_OF_BORDER)
        {
            mMain.MoveCameraZ(-AMOUNT_OF_MOUSEMOVE);            
        }
        else if (Mathf.Abs(Screen.height - Input.mousePosition.y) < SIZE_OF_BORDER)
        {
            mMain.MoveCameraZ(AMOUNT_OF_MOUSEMOVE);
        }


        if (Input.GetButtonDown("ActionA") )
        {
            mMouseButtonState = MouseButtonState.ATTACK;
            mCursorMgr.SetAttack();
        }

        // 왼쪽 버튼을 눌렀을 때 
        // 상태에 따라서 버튼 행동이 다르다.
        // 예를 들어 A키를 한번 누른 후에 왼쪽 버튼을 클릭 하는 것은 
        // 공격 행위이고 그렇지 않은 상태에서는 단순히 해당 유닛을 선택하는 (정보를 표시) 용도다.
        if (Input.GetMouseButtonDown(0))
        {
            List<GameObject> entities = mMain.SelectEntities(mTeamID, Input.mousePosition);

            if (mMouseButtonState == MouseButtonState.NORMAL)
            {
                // 단순 유닛 선택
                if (entities.Count > 0)
                {
                    // 해당 유닛의 정보를 왼쪽 상단에 띄워 준다. 물론 유닛이 사라지면 해당 이벤트를 받아서 HUD를 삭제 시켜야 한다.                    

                }
            }
            else if (mMouseButtonState == MouseButtonState.ATTACK)
            {
                bool isAbleToAttack = false;

                // 해당 유닛 공격 혹은 그라운드 어택 (가장 가까운 적을 자동으로 타겟으로 삼아 공격한다.)
                if (entities.Count > 0)
                {                    
                    mTargetObject = entities[0];                        
                    isAbleToAttack = true;                    
                }
                else
                {
                    // 그라운드 어택
                    // 해당 마우스 위치에서 가장 가까운 적을 공격한다. (물론 롤에서는 이전에 공격했던 유닛이 아직 살아 있으면
                    // 가까운것 상관없이 계속해서 이전 타겟을 공격한다. 이것으로 알 수 있는 것은 이전 타겟의 정보를 저장하고 있다는 의미)                                        
                    GameObject cloestEntity = mMain.SelectCloestEntity(mTeamID, Main.GetMousePositionInWorld());

                    if ( cloestEntity != null )
                    {
                        mTargetObject = cloestEntity;                        
                        isAbleToAttack = true;
                    }
                }

                if (isAbleToAttack)
                {
                    if (Vector3.Distance(mTargetObject.transform.position, transform.position) <= (10 / mEntityData.mRange))
                    {
                        mPlayerState = PlayerState.ATTACK;
                    }
                    else
                    {
                        mTargetObject = null;
                        mPlayerState = PlayerState.MOVE;
                    }
                }

                if (!isAbleToAttack)
                {
                    // 그냥 이동 처리를 한다.
                    mPlayerState = PlayerState.MOVE;
                    mTargetPos = new Vector3(Main.GetMousePositionInWorld().x, mY, Main.GetMousePositionInWorld().z);
                }
            }

            mCursorMgr.SetNormal();
        }


        if (Input.GetMouseButtonDown(1))
        {
            //if ( mMouseButtonState == MouseButtonState.NORMAL )
            {
                List<GameObject> entities = mMain.SelectEntities(mTeamID, Input.mousePosition);

                if (entities.Count > 0)
                {
                    // on unit                    
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
                        mTargetPos = ray.origin + ray.direction * d;
                        mTargetPos.y = mY;
                        mPlayerState = PlayerState.MOVE;
                    }

                    mCursorMgr.SetNormal();
                }
            }
            /*else if (mMouseButtonState == MouseButtonState.ATTACK)
            {
                // todo
                
            }*/
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
                
        Vector3 dir = mTargetPos - transform.position;

        if (mPlayerState == PlayerState.MOVE)
        {
            if (Vector3.Distance(transform.position, mTargetPos) > 1)
            {
                transform.position += dir.normalized * (10 / mEntityData.mSpeed); //mSpeed;
                transform.LookAt(mTargetPos);
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
