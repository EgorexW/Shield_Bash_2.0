using UnityEngine;

class Player : MonoBehaviour
{
    public void Teleport(Vector3 transformPosition)
    {
        transform.position = transformPosition;
    }
}