public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1. Create a new array of doubles with the size equal to 'length'.
        // 2. Use a loop that runs from index 0 up to length - 1.
        // 3. For each index i, calculate the multiple by multiplying 'number' by (i + 1).
        //    Example:
        //      i = 0 → number * 1
        //      i = 1 → number * 2
        //      i = 2 → number * 3
        // 4. Store the calculated value in the array at index i.
        // 5. After the loop finishes, return the completed array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1. Determine how many elements from the end of the list need to move to the front.
        //    This number is 'amount'.
        // 2. Find the index where the split should happen:
        //      splitIndex = data.Count - amount
        // 3. Get the last 'amount' elements using GetRange(splitIndex, amount).
        //    These elements will move to the front.
        // 4. Remove those elements from their original position using RemoveRange(splitIndex, amount).
        // 5. Insert the saved elements at the beginning of the list using InsertRange(0, savedList).
        // 6. The list will now be rotated to the right.

        int splitIndex = data.Count - amount;

        List<int> movedItems = data.GetRange(splitIndex, amount);

        data.RemoveRange(splitIndex, amount);

        data.InsertRange(0, movedItems);
    }
}