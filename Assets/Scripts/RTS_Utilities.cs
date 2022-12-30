namespace RTS
{
    public class RTS_Utilities
    {


    }
    public enum TypeUnit
    {
        Archer,//xa thu
        Commander,//Chỉ huy
        Crossbowman,//xa thu nỏ
        Halberdier, //giáo
        Knight,//hiệp sĩ
        Mage,//pháp sư

        HeavyCavalry, //kị binh hạng nặng
        HeavyInfantry,//bộ binh hạng nặng
        HeavySwordman,//kiếm sĩ hạng nặng

        HighPriest,//thầy tế thượng phẩm

        LightCavalry,//kị binh hạng nhẹ
        LightInfantry,// bộ binh hạng nhẹ


        King,//vua
        MountedKing,// vua cưỡi ngựa
        MountedKnight,//kị sĩ cưỡi ngựa
        MountedMage,// phù thủy cưỡi ngựa
        MountedPaladin,//vua cưỡi ngựa nâng cấp
        MountedPriest,//thầy tế cưỡi ngựa
        MountedScout,//cung thủ cưỡi ngựa
        Paladin,//vua nâng cấp
        Peasant,//nông dân 
        Priest,//thầy tế 
        Scout,//xa thu nâng cấp 
        Settler,//xe chở hàng
        Spearman,// bộ binh giáo
        Swordman // bộ binh
    }
    public enum StateUnit
    {
        Idle,
        Move,
        Attack,
        Dead,
        Hit,
        MovingToResour,
        GatheringResources,
        MovingToStorage,
        FindNodeResources


    }
    public enum ResourcesType
    {
        Food,
        Wood,
        Gold,
        Stone
    }
}


