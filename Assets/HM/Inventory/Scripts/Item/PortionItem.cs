using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-28 PM 11:07:23
// 작성자 : Rito

namespace Rito.InventorySystem
{
    /// <summary> 수량 아이템 - 포션 아이템 </summary>
    public class PortionItem : CountableItem, IUsableItem
    {
        public PortionItemData PortionItemData { get; private set; }

        public PortionItem(PortionItemData data, int amount = 1) : base(data, amount)
        {
            PortionItemData = data;
        }

        public bool Use()
        {
            // 회복량을 디버그 로그로 출력
            Debug.Log($"체력 회복량 : {PortionItemData.Value}");

            // 임시 : 개수 하나 감소
            Amount--;
            return true;
        }

        protected override CountableItem Clone(int amount)
        {
            return new PortionItem(CountableData as PortionItemData, amount);
        }
    }
}