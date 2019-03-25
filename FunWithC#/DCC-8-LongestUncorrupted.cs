int[] longestUncorruptedSegment(int[] sourceArray, int[] destinationArray) {

    int[] result = {0, 0};
    List<int> sequence = new List<int>(){0};
    List<int> locationBrk = new List<int>(){0};
    int count = 0;
    
    for(int i = 0; i < destinationArray.Length; i++)
    {
        if (sourceArray[i] == destinationArray[i])
        {
            sequence[count]++;
        }
        else if (sourceArray[i] != destinationArray[i])
        {
            sequence.Add(0);
            locationBrk.Add(0);
            count++;
            
            if (sequence[count] == 0)
            {
                locationBrk[count] = i + 1;
            }
        }
    }
    
    result[0] = sequence.Max();
    result[1] = locationBrk[sequence.IndexOf(sequence.Max())];
    
    return result;
}

//compare between:
//sourceArray = [33531593, 96933415, 28506400, 39457872, 29684716, 86010806]
//destinationArray = [33531593, 96913415, 28506400, 39457872, 29684716, 86010806]