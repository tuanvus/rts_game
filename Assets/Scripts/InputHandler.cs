using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Units.Player;

namespace RTS.InputManager
{
    public class InputHandler : Singleton<InputHandler>
    {
        private RaycastHit hit;
        private List<Transform> selectUnits = new List<Transform>();

        private bool isDragging = false;
        private Vector3 mousePos;

        void Start()
        {


        }
        private void OnGUI()
        {
            if(isDragging)
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

                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {

                        case 8:
                            SelectUnit(hit.transform,Input.GetKey(KeyCode.LeftShift));
                            break;
                        default:
                            isDragging = true;
                            DeselectUnits();
                            break;

                    }
                }

            }
            if(Input.GetMouseButtonUp(0))
            {
                foreach(Transform child in Player.PlayerManager.Instance.playerUnits)
                {
                    foreach(Transform unit in child)
                    {
                        if(isWithinSelectionBounds(unit))
                        {
                            SelectUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if(Input.GetMouseButtonDown(1))
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
                            foreach(Transform unit in selectUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.MoveUnit(hit.point);
                            }
                            break;

                    }
                }
            }


        }

        private void SelectUnit(Transform unit, bool canMutiselect = false)
        {
            if(!canMutiselect)
            {
                DeselectUnits();
            }
            selectUnits.Add(unit);
            unit.Find("Highlight").gameObject.SetActive(true);

        }

        private void DeselectUnits()
        {
            for(int i = 0; i < selectUnits.Count;i++)
            {
                selectUnits[i].Find("Highlight").gameObject.SetActive(false);
            }
            selectUnits.Clear();

        }
        private bool isWithinSelectionBounds(Transform tf)
        {
            if(!isDragging)
            {
                return false;
            }
            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
            return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));


        }

        private bool HaveSelectedUnits()
        {
            if(selectUnits.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}