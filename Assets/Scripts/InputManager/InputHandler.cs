using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Units.Player;

namespace RTS.InputManager
{
    public class InputHandler : Singleton<InputHandler>
    {
        private RaycastHit hit;
        public List<Transform> selectUnits = new List<Transform>();
        public Transform selectBuilding = null;
        public LayerMask interactorMask = new LayerMask();
        private bool isDragging = false;
        private Vector3 mousePos;
        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);


            }
        }
        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100, interactorMask))
                {
                    if (AddUnit(hit.transform,Input.GetKey(KeyCode.LeftShift)))
                    {

                    }
                    else if (AddBuilding(hit.transform))
                    {

                    }
                }
                else
                {
                    isDragging = true;
                    DeselectUnits();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.Instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (isWithinSelectionBounds(unit))
                        {
                            AddUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && HaveSelectedUnits())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {
                        case 8:
                            break;
                        case 9:
                            break;
                        default:
                            foreach (Transform unit in selectUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.MoveUnit(hit.point);
                            }
                            break;
                    }
                }

            }


        }


        private void DeselectUnits()
        {
            if (selectBuilding)
            {
                selectBuilding.gameObject.GetComponent<RTS.Interactables.IBuilding>().OnInteractExit();
                selectBuilding = null;
            }

            for (int i = 0; i < selectUnits.Count; i++)
            {
                selectUnits[i].gameObject.GetComponent<RTS.Interactables.IUnit>().OnInteractExit();
            }
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

        private Interactables.IUnit AddUnit(Transform tf, bool canMultiselect = false)
        {
            Interactables.IUnit iUnit = tf.GetComponent<RTS.Interactables.IUnit>();
            if (iUnit)
            {
                if (!canMultiselect)
                {
                    DeselectUnits();
                }
                selectUnits.Add(iUnit.gameObject.transform);
                iUnit.OnInteractEnter();
                return iUnit;
            }
            else
            {
                return null;
            }

        }
        private Interactables.IBuilding AddBuilding(Transform tf)
        {
            Interactables.IBuilding iBuilding = tf.GetComponent<RTS.Interactables.IBuilding>();
            if (iBuilding)
            {
                DeselectUnits();
                selectUnits.Add(iBuilding.gameObject.transform);
                iBuilding.OnInteractEnter();
                return iBuilding;
            }
            else
            {
                return null;
            }

        }
    }
}