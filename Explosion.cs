using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject , 2f);
    }

}
