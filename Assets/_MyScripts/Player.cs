using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Self { get; private set; }
    public bool isVR;

    [SerializeField] private Canvas canvas;
    [SerializeField] private ParticleSystem Fog;
    [SerializeField] private HeadBehaviour head;

    private void Awake()
    {
        if (Self == null)
            Self = this;
        else
        {
            Destroy(this);
            return;
        }
        transform.GetChild(isVR ? 0 : 1).gameObject.SetActive(true);
        canvas.gameObject.SetActive(!isVR);
        Fog.transform.SetParent(transform.GetChild(isVR ? 0 : 1));
        head = GetComponentInChildren<HeadBehaviour>();
    }

    public Transform Transfrom { get { return isVR ? head.transform : transform.GetChild(1); } }
}
