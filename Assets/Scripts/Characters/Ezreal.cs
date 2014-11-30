using UnityEngine;
using System.Collections;

public class Ezreal : MonoBehaviour 
{
    public Main mMain;

	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        mMain = gameObject.GetComponent<Player>().mMain;
        Player player = gameObject.GetComponent<Player>();
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 worldPos = Main.GetMousePositionInWorld();
            worldPos.y = transform.position.y;
            Vector3 dir = worldPos - transform.position;
            
            GameObject obj = (GameObject)GameObject.Instantiate(mMain.mEzreal_Q);
            obj.GetComponent<ezreal_q>().mTeam = player.mTeamID;
            obj.transform.position = transform.position;
            obj.transform.LookAt(transform.position + dir.normalized * 3);

            player.SetDirection(transform.position + dir.normalized);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 worldPos = Main.GetMousePositionInWorld();
            worldPos.y = transform.position.y;
            Vector3 dir = worldPos - transform.position;
                       

            GameObject obj = (GameObject)GameObject.Instantiate(mMain.mEzreal_W);
            obj.GetComponent<ezreal_w>().mTeam = player.mTeamID;

            obj.transform.position = transform.position;
            obj.transform.LookAt(transform.position + dir.normalized * 3);
            player.SetDirection(transform.position + dir.normalized);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 이즈리얼은 역시 앞비전.           
            Vector3 worldPos = Main.GetMousePositionInWorld();
            worldPos.y = transform.position.y;
            Vector3 dir = worldPos - transform.position;

            gameObject.GetComponent<Player>().SetPosition(transform.position + dir.normalized * 3);         
            
            // 이펙트 붙여 보자 빠빠빰.
            GameObject effect = (GameObject)GameObject.Instantiate(mMain.mEff_VisionMove);            
            effect.transform.parent = gameObject.transform;
            effect.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
	}
}
