bool[] delivery(int[] order, int[][] shoppers) {
    
    double shopperWait;
    bool[] results = new bool[shoppers.Length];
    
    for (int i = 0; i < shoppers.Length; i++)
    {
        shopperWait = (double)(order[0] + shoppers[i][0]) / (double)shoppers[i][1] + (double)shoppers[i][2];
        
        if (order[1] - shopperWait > 0)
        {
            results[i] = false;
        }
        else if (order[2] + order[1] >= shopperWait)
        {
            results[i] = true;
        }
        else
        {
            results[i] = false;
        }
    }
    return results;
}
