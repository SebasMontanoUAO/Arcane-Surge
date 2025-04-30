using UnityEngine;

public class ModelCorrection : MonoBehaviour
{

    public void CorrectModel(Quaternion playerRotation)
    {
        if(transform.rotation != playerRotation)
        {
            transform.rotation = playerRotation;
        }
    }
}
