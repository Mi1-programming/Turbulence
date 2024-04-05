using UnityEngine;

[System.Serializable]
public struct ColliderSound
{
    public float force;
    public float volume;
    public Vector2 pitch;
    public AudioClip snd;
}
public class BounceSounds : MonoBehaviour
{
    [SerializeField] private ColliderSound[] sounds;
    [SerializeField] private AudioSource source;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            for (int i = sounds.Length - 1; i >= 0; i--)
            {
                if (!source.isActiveAndEnabled) break;

                ColliderSound s = sounds[i];
                if (contact.normalImpulse < s.force) continue;

                source.volume = s.volume;
                source.pitch = Random.Range(s.pitch.x, s.pitch.y);
                source.clip = s.snd;
                source.Play();

                break;
            }
        } 
    }
}
