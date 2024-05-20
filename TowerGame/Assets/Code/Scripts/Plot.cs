using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject hoverTower; // Rename to hoverTower to distinguish it from the actual tower
    private GameObject placedTower; // Store the placed tower separately

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        hoverTower = Instantiate(towerToBuild.hoverPrefab, transform.position, Quaternion.identity);
    }

    private void OnMouseExit()
    {
        if (hoverTower != null)
        {
            Destroy(hoverTower);
            hoverTower = null;
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (hoverTower != null)
        {
            Destroy(hoverTower);
            hoverTower = null;

            Tower towerToBuild = BuildManager.main.GetSelectedTower();

            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("You can't afford this tower");
                return;
            }

            LevelManager.main.SpendCurrency(towerToBuild.cost);

            placedTower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        }
    }
}
