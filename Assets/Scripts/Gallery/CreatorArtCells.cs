using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorArtCells : Factory<ArtButton>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private GridLayoutGroup gridGroup;
    [SerializeField] private string baseUrl;
    private Vector2 cellSize;
    private Vector2 spacing;
    private float sizeCanvasY;
    private int amountVisibleCells;
    private List<ArtButton> cells;


    public void Init()
    {
        cells = new List<ArtButton>();
        scroll.onValueChanged.AddListener(OnScrollbarValueChanged);
        amountVisibleCells = GetAmountVisibleCells();
        CreateArtCells();
    }

    private int GetAmountVisibleCells()
    {
        cellSize = gridGroup.cellSize;
        spacing = gridGroup.spacing;
        sizeCanvasY = canvas.GetComponent<RectTransform>().sizeDelta.y;
        return (Mathf.CeilToInt(((sizeCanvasY - spacing.y) / (cellSize.y + spacing.y))) * gridGroup.constraintCount);
    }
    private void OnScrollbarValueChanged(Vector2 value)
    {
        float scrollValue = scroll.normalizedPosition.y;
        if (scrollValue < 0.1f)
        {
            CreateArtCells();
        }
    }
    private void CreateArtCells()
    {
        for (int artName = 1; artName <= amountVisibleCells; artName++)
        {
            var art = Create();
            art.Init(baseUrl, artName);
            cells.Add(art);
        }
    }
}
