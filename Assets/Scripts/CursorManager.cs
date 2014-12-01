using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour 
{
    // test
    public Texture2D mCursorNormal;
    public Texture2D mCursorAttack;
        
	// Use this for initialization
	void Start () 
    {
     
	}
	
	// Update is called once per frame
	void Update () 
    {        
        
	}

    public void SetNormal()
    {
        Cursor.SetCursor(mCursorNormal, Vector2.zero, CursorMode.Auto);
    }

    public void SetAttack()
    {
        Cursor.SetCursor(mCursorAttack, Vector2.zero, CursorMode.Auto);
    }
}
