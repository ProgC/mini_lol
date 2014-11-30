using UnityEngine;
using System.Collections;

public class Outline : MonoBehaviour 
{
    public MeshRenderer mMeshRenderer;
    
	// Use this for initialization
	void Start () 
    {
        mMeshRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*Vector3 worldPos = Vector3.zero;
                
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float d;
        if (plane.Raycast(ray, out d))
        {
            worldPos = ray.origin + ray.direction * d;            
        }
        return worldPos;
    

        Vector3 worldPos = Main.GetMousePositionInWorld();
        worldPos.y = transform.position.y;
        Vector3 dir = worldPos - transform.position;*/
	
	}

    void OnMouseOver()
    {
        mMeshRenderer.enabled = true;
    }

    void OnMouseExit()
    {
        mMeshRenderer.enabled = false;
    }


}
