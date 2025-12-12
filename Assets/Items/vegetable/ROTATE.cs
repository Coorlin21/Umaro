using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("旋轉速度 (度/秒)")]
    public float rotateSpeed = 90f;

    [Header("旋轉軸向")]
    public Vector3 rotateAxis = new Vector3(0, 1, 0);

    void Update()
    {
        // 在自己的座標軸方向旋轉（真正的自轉）
        transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime, Space.Self);
    }
}