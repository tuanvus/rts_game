using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class InputHandler : Singleton<InputHandler>
    {
        private RaycastHit hit;
        public List<Transform> selectUnits = new List<Transform>();


        private bool isDragging = false;
        private Vector3 mousePos;

        [Header("Building")]
        //building
        public GameObject prefabToPlace;

        public LayerMask obstacleLayerMask;
        public List<Transform> selectBuilding = new List<Transform>();
        bool hasObstacle;
        Vector3 prefabPosition;
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
            SelectBuilding();
            //SelectUnit();

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
                    hasObstacle = Physics.CheckBox(prefabPosition, prefabToPlace.GetComponent<Collider>().bounds.size, Quaternion.identity, obstacleLayerMask);

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

                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;
                    switch (layerHit.value)
                    {

                        case 8:
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;
                        default:
                            isDragging = true;
                            DeselectUnits();
                            break;

                    }
                }

            }
            if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                // foreach (Transform child in Player.PlayerManager.Instance.playerUnits)
                // {
                //     foreach (Transform unit in child)
                //     {
                //         if (isWithinSelectionBounds(unit))
                //         {
                //             SelectUnit(unit, true);
                //         }
                //     }
                // }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false)
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
                            // foreach (Transform unit in selectUnits)
                            // {
                            //     UnitBase pU = unit.gameObject.GetComponent<UnitBase>();
                            //     pU.SetTarget(hit.transform);
                            // }
                            break;

                    }
                }
            }


        }

        private void SelectUnit(Transform unit, bool canMutiselect = false)
        {
            if (!canMutiselect)
            {
                DeselectUnits();
            }
            selectUnits.Add(unit);
            //unit.Find("Highlight").gameObject.SetActive(true);

        }

        private void DeselectUnits()
        {
            for (int i = 0; i < selectUnits.Count; i++)
            {
                //selectUnits[i].Find("Highlight").gameObject.SetActive(false);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(prefabPosition, prefabToPlace.GetComponent<Collider>().bounds.size);

        }

    }
