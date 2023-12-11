using UnityEngine;


public class Vendor : NPC, IInteractable
{
    
    [SerializeField]
    private VendorItem[] items = default;

   
    public VendorItem[] MyItems { get => items; }

    [SerializeField] KeyCode InteractionKeyCode = KeyCode.E;

    private bool isInRange;
    private bool isopened;
    [SerializeField] bool showAndHideMouse = true;

    private void Update()
    {

        if (isInRange && Input.GetKeyDown(InteractionKeyCode) && isopened != true)
        {

            isopened = true;
            ShowMouseCursor();
            Interact();
        }
        else if (Input.GetKeyDown(InteractionKeyCode) && isopened)
        {

            StopInteract();
            HideMouseCursor();
            isopened = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject, false);
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
            
        }
    }

    public void ShowMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void HideMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}