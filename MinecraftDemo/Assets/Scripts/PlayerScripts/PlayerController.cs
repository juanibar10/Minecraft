using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    public float jumpSpeed = 8;
    public float gravity = 20;
    public float sensivility = 4;
    public float maxRange = 3;
    [HideInInspector]
    public bool canInteract;

    private Vector3 moveDir = Vector3.zero;
    private float yRotation = 0;
    private float yRot;
    private float xRot;

    public Camera camara;

    private Inventory inv;
    
    private void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        canInteract = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (canInteract)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            MouseLook();

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray(camara.transform.position, camara.transform.forward);
                RaycastHit hit;
                BlockScript block;

                if (Physics.Raycast(ray, out hit, maxRange))
                {
                    if (block = hit.transform.GetComponent<BlockScript>())
                    {
                        block.BreackBlock();
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if(inv.items[inv.hoverIndex].Placeable == true)
                {
                    Ray ray = new Ray(camara.transform.position, camara.transform.forward);
                    RaycastHit hit;
                    BlockScript block;

                    if (Physics.Raycast(ray, out hit, maxRange))
                    {
                        Vector3 spawnPos = Vector3.zero;

                        float xDiff = hit.point.x - hit.transform.position.x;
                        float yDiff = hit.point.y - hit.transform.position.y;
                        float zDiff = hit.point.z - hit.transform.position.z;

                        if(Mathf.Abs(yDiff) == 0.5f)
                        {
                            spawnPos = hit.transform.position + (Vector3.up * yDiff) * 2;
                        }
                        else if (Mathf.Abs(xDiff) == 0.5f)
                        {
                            spawnPos = hit.transform.position + (Vector3.right * xDiff) * 2;
                        }
                        else if (Mathf.Abs(zDiff) == 0.5f)
                        {
                            spawnPos = hit.transform.position + (Vector3.forward * zDiff) * 2;
                        }

                        Instantiate(inv.items[inv.hoverIndex].Object, spawnPos, Quaternion.identity).transform.SetParent(GameObject.Find("Chunk").transform);
                        inv.RemoveItem();

                    }
                }
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        
    }

    void MovePlayer()
    {
        CharacterController character = GetComponent<CharacterController>();

        if (character.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = jumpSpeed;
            }
        }

        moveDir.y -= gravity * Time.deltaTime;
        character.Move(moveDir * Time.deltaTime);
    }

    public void MouseLook()
    {
        yRot = -Input.GetAxis("Mouse Y") * sensivility;
        xRot = Input.GetAxis("Mouse X") * sensivility;
        yRotation += yRot;
        yRotation = Mathf.Clamp(yRotation, -80, 80);

        if(xRot != 0)
        {
            transform.eulerAngles += new Vector3(0, xRot, 0);
        }
        if(yRot != 0)
        {
            camara.transform.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y, 0);
        }
    }
}
