using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CousorManager : MonoBehaviour
{
//     private Vector3 _mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
//     [SerializeField] private bool _canClick;


//     void Update()
//     {
//         if(ObjectAtMousePosition().Length > 0) _canClick = true;
//         else _canClick = false;

//         if (_canClick && Input.GetMouseButtonDown(0))
//         {
//             ClickAction(ObjectAtMousePosition()[0].gameObject);
//         }
//     }


//     private void ClickAction(GameObject clickObject)
//     {
//         switch (clickObject.tag)
//         {
//             case "Item" :
//                 Debug.Log("pick Item");
//                 break;
//         }
//     }   


//     private Collider[] ObjectAtMousePosition()
//     {
//         return Physics.OverlapSphere(_mouseWorldPos, 3f);
//     }
}
