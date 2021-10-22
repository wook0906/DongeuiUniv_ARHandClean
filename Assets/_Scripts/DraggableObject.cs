using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private static float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //Vector3 xzDirection = Vector3.forward * ARCamController.S.horizontal.Vertical + Vector3.right * ARCamController.S.horizontal.Horizontal;
        Vector3 xzDirection = (ARCamController.S.transform.forward.normalized * -1f) * ARCamController.S.horizontal.Vertical + (ARCamController.S.transform.right.normalized * -1f) * ARCamController.S.horizontal.Horizontal;
        if (xzDirection.magnitude > 0f)
        {
            transform.position += xzDirection * Time.deltaTime * speed;
        }
        Vector3 yDirection = (Vector3.up * -1f) * ARCamController.S.vertical.Vertical;// + Vector3.down * ARCamController.S.vertical.Horizontal;
        if(yDirection.magnitude > 0f)
        {
            transform.position += yDirection * Time.deltaTime * speed;
        }
    }
}
