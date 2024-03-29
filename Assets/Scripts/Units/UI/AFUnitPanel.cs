﻿using AFSInterview.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AFSInterview.Units.UI
{
    public class AFUnitPanel : AFObserverMonoBehaviour<AFUnitInstance>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TextMeshProUGUI unitName;
        
        [SerializeField]
        private TextMeshProUGUI unitDamage;
                
        [SerializeField]
        private TextMeshProUGUI unitArmor;
        
        [SerializeField]
        private Image panelFill;

        [SerializeField]
        private Image panelBorder;

        [SerializeField]
        private Color selectedBorderColor;

        [SerializeField]
        private Color normalBorderColor;

        [SerializeField]
        private Color hoveredBorderColor;
        
        protected override void RegisterEvents()
        {
            ObservedObject.OnUnitSelected.AddListener(ReactToChanges);
            ObservedObject.OnUnitDeselected.AddListener(ReactToChanges);
            ObservedObject.OnUnitHovered.AddListener(ReactToChanges);
            ObservedObject.OnUnitUnhovered.AddListener(ReactToChanges);
        }

        protected override void UnregisterEvents()
        {
            ObservedObject.OnUnitSelected.RemoveListener(ReactToChanges);
            ObservedObject.OnUnitDeselected.RemoveListener(ReactToChanges);
            ObservedObject.OnUnitHovered.RemoveListener(ReactToChanges);
            ObservedObject.OnUnitUnhovered.RemoveListener(ReactToChanges);
        }

        protected override void OnReactToChanges()
        {
            unitName.text = ObservedObject.UnitData.name;
            unitDamage.text = $"D: {ObservedObject.AttackDamage}";
            unitArmor.text = $"A: {ObservedObject.ArmorPoints}";
            panelFill.color = ObservedObject.UnitArmy.ArmyColor;

            if (ObservedObject.IsHovered)
            {
                panelBorder.color = hoveredBorderColor;
            }
            else
            {
                panelBorder.color = ObservedObject.IsSelected ? selectedBorderColor : normalBorderColor;
            }
        }

        private void ReactToChanges(AFUnitInstance unitInstance)
        {
            ReactToChanges();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ObservedObject.HoverUnit();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ObservedObject.UnhoverUnit();
        }
    }
}