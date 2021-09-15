using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTS.InputManager
{
    public class InputHandler : Singleton<InputHandler>
    {
        private RaycastHit hit;
        private List<Transform> selectUnits = new List<Transform>();
        void Start()
        {
        }

        void Update()
        {

        }
        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
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
                            DeselectUnits();
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

    }
}