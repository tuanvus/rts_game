public class CostHolder
{
	private int food;

	private int wood;

	private int gold;

	public CostHolder(int f, int w, int g)
	{
		food = f;
		wood = w;
		gold = g;
	}

	public int GetFood()
	{
		return food;
	}

	public int GetWood()
	{
		return wood;
	}

	public int GetGold()
	{
		return gold;
	}
}
