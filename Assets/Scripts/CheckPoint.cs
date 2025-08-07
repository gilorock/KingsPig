using Unity.Cinemachine;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private static readonly int IsActive = Animator.StringToHash("isActive");
    [SerializeField] private Animator m_Animator;
    [SerializeField] private bool isActive;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive) return;
        if (other.CompareTag("Player")) ActiveCheckPoint();
        GameManager.Instance.hasCheckPointActive = true;
        GameManager.Instance.CheckPointRespawnPosition = transform.position;
    }

    private void ActiveCheckPoint()
    {
        isActive = true;
        m_Animator.SetTrigger(IsActive);
    }
}
