using UnityEngine;

public class DoorsEvent : MonoBehaviour
{
    [SerializeField] GameObject entranceDoor;
    [SerializeField] private Animator animatorEntranceDoor;
    private int _idOpenDoor;

    
    void OnEnable()
    {
        _idOpenDoor = Animator.StringToHash("Open");
        entranceDoor = GameObject.FindGameObjectWithTag("EntranceDoor");
        animatorEntranceDoor = entranceDoor.GetComponent<Animator>();
    }

    public void DoorOut()
    {
        if(!GameManager.Instance.hasCheckPointActive)
        animatorEntranceDoor.SetTrigger(_idOpenDoor);
    }
}
