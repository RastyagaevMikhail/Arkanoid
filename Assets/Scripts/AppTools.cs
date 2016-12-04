using UnityEngine;
using System.Collections;

public class AppTools
{

    private static AppTools _instance = null;

    public static AppTools Instatnce
    {
        get
        {
            if (_instance == null)
                _instance = new AppTools();
            return _instance;
        }
    }

    public Vector2 ScreenSize;
    public Bounds CameraBounds;


    AppTools()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 TopLeftPointWorld = cam.ScreenToWorldPoint(Vector3.zero);
        Vector3 TopRigthPointWorld = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0,0)); 
        Vector3 BotRigthPointWorld = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0)); 
        Vector3 BotLeftPointWorld = cam.ScreenToWorldPoint(new Vector3(0, Screen.height,0)); 

        ScreenSize = new Vector2(Vector2.Distance(TopLeftPointWorld, TopRigthPointWorld), Vector2.Distance(TopRigthPointWorld, BotRigthPointWorld));
        Vector3 camPos = Camera.main.transform.position;
        camPos.z = 0;
        CameraBounds = new Bounds(camPos, new Vector3(ScreenSize.x, ScreenSize.y,0));
    }


    public void  DrawCameraBouns()
    {
        DrawBounds(CameraBounds);

    }


    public void DrawBounds(Bounds bounds)
    {
        DrawBounds(bounds, Color.red);
    }

    public void DrawBounds(Bounds bounds, Color clr)
    {
        Vector3 TopLeft = new Vector3(bounds.min.x, bounds.max.y);
        Vector3 BotRigth = new Vector3(bounds.max.x, bounds.min.y);
        Debug.DrawLine(bounds.min, TopLeft, clr);
        Debug.DrawLine(TopLeft, bounds.max, clr);
        Debug.DrawLine(bounds.max, BotRigth, clr);
        Debug.DrawLine(BotRigth, bounds.min, clr);
    }



    public bool OvrevapCameraBounds(Bounds bounds)
    {

        return OverlapBounds(CameraBounds, bounds);
    }

    //
    public bool OverlapBounds(Bounds boundsA, Bounds boundsB)
    {
       
        DrawBounds(boundsA, Color.blue);
        DrawBounds(boundsB, Color.green);

        bool result = false;
        //top left point A bounds;
        Vector3 TL_A = new Vector2(boundsA.min.x, boundsA.max.y);
        Vector3 TR_A = boundsA.max;
        Vector3 BR_A = new Vector2(boundsA.max.x, boundsA.min.y);
        Vector3 BL_A = boundsA.min;

        //Debug.DrawRay(BR_A, Vector3.down * 100);

        //top left point B bounds
        Vector3 TL_B = new Vector2(boundsB.min.x, boundsB.max.y);
        Vector3 TR_B = boundsB.max;
        Vector3 BR_B = new Vector2(boundsB.max.x, boundsB.min.y);
        Vector3 BL_B = boundsB.min;
       // Debug.DrawRay(BR_B, Vector3.down * 100);

        //Debug.Log(boundsA.Contains(BR_B) );
       // Debug.Log(boundsA.center.z +  " " + BR_B.z);

        // B in A
        bool BinA = (boundsA.Contains(TL_B) ||
                    boundsA.Contains(TR_B) ||
                    boundsA.Contains(BR_B) ||
                    boundsA.Contains(BL_B));
        // A in B
        bool AinB = (boundsB.Contains(TL_A) ||
                    boundsB.Contains(TR_A) ||
                    boundsB.Contains(BR_A) ||
                    boundsB.Contains(BL_A));
        result = BinA || AinB;


      //  Debug.Log("Overalp = " + result.ToString());
        return result;
    }
}
