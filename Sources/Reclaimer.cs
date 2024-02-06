using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Reclaimer.Sources
{
    public class Reclaimer : CardData
    {
        public override bool DetermineCanHaveCardsWhenIsRoot => true;

        protected override bool CanHaveCard(CardData otherCard)
        {
            ReclaimerMod.L.Log(otherCard.Id);

            return otherCard is Equipable && (otherCard as Equipable).blueprint != null;
        }

        public override void UpdateCard()
        {
            var toBeSalvaged = MyGameCard.GetChildCards().FirstOrDefault();
            if (toBeSalvaged != null)
            {
                var equipment = toBeSalvaged.CardData as Equipable;
                var ingredients = equipment.blueprint.Subprints[0].RequiredCards.ToList();
                var salvaged = ingredients.Select(i => WorldManager.instance.GameDataLoader.GetCardFromId(i)).OrderByDescending(i => i.Value).First();

                ReclaimerMod.L.Log($"Reclaimer: Salvaging {equipment.Name}:{Environment.NewLine} {salvaged.Name}");

                toBeSalvaged.DestroyCard(true);
                CardData cardData = WorldManager.instance.CreateCard(MyGameCard.transform.position, salvaged.Id, faceUp: true, checkAddToStack: false);
                cardData.MyGameCard.SendIt();
            }

            base.UpdateCard();
        }
    }
}
