using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragger : MonoBehaviour, IDragHandler
{
    public float rotationSpeed = 2.0f; // 旋转速度
    private Vector3 lastMousePosition;

    public void OnDrag(PointerEventData eventData)
    {
        // 计算鼠标移动的增量
        Vector3 deltaMouse = Input.mousePosition - lastMousePosition;

        // 通过增量来旋转模型
        transform.Rotate(Vector3.up * deltaMouse.x * rotationSpeed, Space.World);

        // 更新鼠标位置
        lastMousePosition = Input.mousePosition;
    }

    public void StartDrag()
    {
        // 记录初始鼠标位置
        lastMousePosition = Input.mousePosition;
    }
}
