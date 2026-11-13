using UnityEngine;

public class Walking : MonoBehaviour
{
    public AudioSource AudioSource;

    public AudioClip metal;
    public AudioClip bone;
    public AudioClip Flesh;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;

    public void Footstep()
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask))
        {
            if (hit.collider.CompareTag("bone"))
            {
                PlayFootstepSoundL("bone");
            }
            if (hit.collider.CompareTag("flesh"))
            {
                PlayFootstepSoundL("flesh");
            }
            if (hit.collider.CompareTag("metal"))
            {
                PlayFootstepSoundL("metal");
            }
        }
    }

    void PlayFootstepSoundL(AudioClip audio)
    {
        AudioSource.pitch = Random.Range(0.8f, 1f);
        AudioSource.PlayOneShot(audio)
    }
}
