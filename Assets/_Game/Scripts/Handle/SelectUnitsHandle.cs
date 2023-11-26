using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectUnitsHandle : MonoBehaviour
{
    public LayerMask unitLayerMask;
    public LayerMask terrainLayerMask;
    public LayerMask resourceLayerMask;


    public List<Transform> selectUnits = new List<Transform>();
    private bool isDragging = false;
    private Vector3 mousePos;
    private RaycastHit hit;
    public Transform poinArrow;

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
                    // Debug.Log(" vao =");

                    // Clicked on a unit
                    SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                }
                else
                {
                    // Debug.Log("ko vao =" +hit.transform.name);
                    DeselectUnits();
                }
            }
            else
            {
                //  isDragging = true;
                DeselectUnits();
            }
        }

        MoveUnit();

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

    void MoveUnit()
    {
        if (HaveSelectedUnits() && Input.GetMouseButtonUp(1) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("vao =" + hit.transform.name);
            
                if (LayerMaskUtility.IsInLayerMask(hit.transform.gameObject, unitLayerMask))
                {
                    poinArrow = hit.transform;

                }
                else if (LayerMaskUtility.IsInLayerMask(hit.transform.gameObject, resourceLayerMask))
                {
                    poinArrow = hit.transform;

                }
                else if (LayerMaskUtility.IsInLayerMask(hit.transform.gameObject, terrainLayerMask))
                {
                    poinArrow.transform.position = hit.point;
                }


                foreach (var unit in selectUnits)
                {
                    unit.GetComponent<UnitsBase>().Movement(poinArrow);
                }
            }


            DeselectUnits();
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
            selectUnits.Add(unit);
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
}