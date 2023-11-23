using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : Singleton<InputHandler>
{
    // public LayerMask obstacleLayerMask;
    public LayerMask unitLayerMask;
    public LayerMask objectLayerMask;

    //building
    public GameObject prefabToPlace;

    public List<Transform> selectUnits = new List<Transform>();
    public List<Transform> selectBuilding = new List<Transform>();

    bool hasObstacle;
    Vector3 prefabPosition;

    private RaycastHit hit;
    private bool isDragging = false;
    private Vector3 mousePos;

    void Start()
    {
    }

    private void OnGUI()
    {
        if (isDragging)
        {
            Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
            MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
            MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);
        }
    }

    private void Update()
    {
        //SelectBuilding();
        SelectUnit();
    }

    public void SelectBuilding()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        // Raycast from the mouse position to find the position on the ground where the prefab should be placed
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the position of the prefab
            prefabPosition = hit.point;

            // Check if the prefab intersects with any other colliders in the scene
            // hasObstacle = Physics.CheckBox(prefabPosition, prefabToPlace.GetComponent<Collider>().bounds.size,
            //     Quaternion.identity, obstacleLayerMask);

            // Only place the prefab if it does not intersect with any other colliders
            //if (!hasObstacle)
            //{
            //    // Instantiate the prefab at the calculated position
            //    GameObject prefabInstance = Instantiate(prefabToPlace, prefabPosition, Quaternion.identity);
            //}
        }
        //  }
    }

    public void SelectUnit()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isDragging = true;
            if (Physics.Raycast(ray, out hit))
            {

                LayerMask layerHit = hit.transform.gameObject.layer;

                if (LayerMaskUtility.IsInLayerMask(hit.transform.gameObject, unitLayerMask))
                {
                    // Clicked on a unit
                    Debug.Log(" vao");

                    SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                }
                else if (LayerMaskUtility.IsInLayerMask(hit.transform.gameObject, objectLayerMask))
                {
                    Debug.Log("object");
                }
                else
                {
                    Debug.Log("ko vao");
                    // Clicked on something else
                    // isDragging = false;
                    DeselectUnits();
                }
            }
            else
            {
              //  isDragging = true;

                Debug.Log("ko vao ray");
                DeselectUnits();
            }
        }

        // if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        // {
        //     if (isDragging)
        //     {
        //         foreach (Transform child in PlayerManager.Instance.playerUnits)
        //         {
        //             if (isWithinSelectionBounds(child))
        //             {
        //                 SelectUnit(child, true);
        //             }
        //             else
        //             {
        //                 child.GetComponent<InteractionObject>().highlightRing.Hide();
        //             }
        //         }
        //     }
        // }
        if (HaveSelectedUnits() && Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Debug.Log("vao");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                foreach (var unit in selectUnits)
                {
                    unit.GetComponent<UnitsBase>().Movement(hit.point);
                }
            }

   
            DeselectUnits();
        }

        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            foreach (Transform child in PlayerManager.Instance.playerUnits)
            {
                if (isWithinSelectionBounds(child))
                {
                    SelectUnit(child, true);
                }
            }

            isDragging = false;
        }
    }

    private void SelectUnit(Transform unit, bool canMutiselect = false)
    {
        if (!canMutiselect)
        {
            DeselectUnits();
        }

        if (!selectUnits.Contains(unit))
        {
            Debug.Log("ck 1");
            selectUnits.Add(unit);
        }
        else
        {
            Debug.Log("ck 2");

        }

        selectUnits.DoIfNotNull(() =>
        {
            selectUnits.ForEach(tf =>
            {
                tf.GetComponent<InteractionObject>().highlightRing.gameObject.SetActive(true);
            });
        });


        //unit.Find("Highlight").gameObject.SetActive(true);
    }

    private void DeselectUnits()
    {
        this.selectUnits.ForEach(unit => { unit.GetComponent<InteractionObject>().highlightRing.Hide(); });

        selectUnits.Clear();
    }

    private bool isWithinSelectionBounds(Transform tf)
    {
        if (!isDragging)
        {
            return false;
        }

        Camera cam = Camera.main;
        Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
        return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));
    }

    private bool HaveSelectedUnits()
    {
        if (selectUnits.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(prefabPosition, prefabToPlace.GetComponent<Collider>().bounds.size);
    }
}