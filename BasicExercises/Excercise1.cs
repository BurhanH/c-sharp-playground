namespace BasicExercises
{
    public static class Excercise1
    {
        /// <summary>
        /// Swap the elements in the array at index1 and index2
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public static int[] Swap(int[] array, int index1, int index2)
        {
            if (index1 == index2)
                {
                    return array;
                }
            if (index1 < 0 || index2 < 0)
                {
                    return array;
                }
            if (array.Length > 1)
                {
                    int tempStorage = array[index1];
                    array[index1] = array[index2];
                    array[index2] = tempStorage;
                }
            return array;
        }

        /// <summary>
        /// Sort the array from smallest to largest
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] Sort(int[] array)
        {
            if (array.Length > 1)
            {
                for (int i = 0; i < array.Length; i++)
                    {
                        int j = i;
                        while((j > 0) && (array[j] < array[j - 1]))
                            {
                                Swap(array, j, j - 1);
                                j = j - 1;
                            }
                    }
            }
            return array;
        }
    }
}
