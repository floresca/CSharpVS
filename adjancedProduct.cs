int adjacentElementsProduct(int[] inputArray) {
    
    int product = inputArray[0] * inputArray[1];
    
    for (int i = 0; i < inputArray.Length - 1; i++)
    {
        if (inputArray[i] * inputArray[i + 1] > product)
        {
            product = inputArray[i] * inputArray[i + 1];
        }
    }
    
    return product;
}
