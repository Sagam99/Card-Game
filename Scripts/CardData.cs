using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "cardGame/card")]
public class CardData : ScriptableObject
{
    public enum Damagetype
    {

        Fire,
        Ice,
        Both
    }

    public string CardTitle;
    public string description;

    public int cost;

    public int damage;

    public Damagetype damageType;
    public Sprite cardImage;
    public Sprite frameImage;
    public int numberInDeck;

    public bool isDefenseCard = false;
    public bool isMirrorCard = false;
    public bool isMulti = false;

}
