using UnityEngine;
using System.Collections;

public class Unit2D : MonoBehaviour
{

    private Transform myTransform = null;
    public  Transform MyTransform
    {
        get
        {
            if (myTransform == null)
            {
                myTransform = GetComponent<Transform>();
            }
            return myTransform;
        }
    }

    public Vector3 position
    {
        get
        { 
            return MyTransform.position;    
        }

        set
        { 
            MyTransform.position = value;
        }
    }

    public float x
    {
        get
        {
            return position.x;
        }
        set
        { 
            position = new Vector3(value, position.y, position.z);
        }
    }
    public float y
    {
        get
        {
            return position.y;
        }
        set
        { 
            position = new Vector3(position.x, value, position.z);
        }
    } 
    public float z
    {
        get
        {
            return position.z;
        }
        set
        { 
            position = new Vector3(position.x, position.y, value);
        }
    }

    private SpriteRenderer mySpriteRenderer = null;
    public SpriteRenderer MySpriterenderer
    {
        get
        { 
            if (mySpriteRenderer == null)
                mySpriteRenderer = GetComponent<SpriteRenderer>();
            return mySpriteRenderer;
        }
    }

    public Bounds MyBounds
    {
        get
        {
            Bounds mbounds = MySpriterenderer.bounds;
            Vector3 tmp =  mbounds.center;
            tmp.z = 0;
            mbounds.center = tmp;

            tmp = mbounds.max;
            tmp.z = 0;
            mbounds.max = tmp;

            tmp = mbounds.min;
            tmp.z = 0;
            mbounds.min = tmp;
            return mbounds;
            }
    }

    private Collider2D myCollider2D;
    public Collider2D MyCollider2D
    {
        get
        {
            if (myCollider2D == null)
                myCollider2D = GetComponent<Collider2D>();
            return myCollider2D;
        }
    }
    private Rigidbody2D myRigidbody2D;
    public Rigidbody2D MyRigidbody2D
    {
        get
        {
            if (myRigidbody2D == null)
                myRigidbody2D = GetComponent<Rigidbody2D>();
            return myRigidbody2D;
        }
    }

 
}
