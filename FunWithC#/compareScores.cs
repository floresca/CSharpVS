static List<int> compareTriplets(List<int> a, List<int> b) {
        
        int scoreA = 0;
        int scoreB = 0;
        
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] > b[i])
            {
                scoreA += 1;
            }
            else if (a[i] < b[i])
            {
                scoreB += 1;
            }
        }
        
        int[] scores = new int[2];
        scores[0] = scoreA;
        scores[1] = scoreB;

        return scores[];
        
    }