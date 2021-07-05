using System;
using System.Linq;
using Xunit;

namespace BasicExercises.Tests
{
    public class Exercise1Tests
    {

        [Fact]
        public void Excercise1_swap()
        {
            Random rand = new Random();
            //Given
            //an array with 4 random values.
            int[] array = new[] { rand.Next(), rand.Next(), rand.Next(), rand.Next() };
            //A random index from the array
            int first = rand.Next(0, 4);
            //A the next or first element in the array
            int second = (first + 1) % 4;
            var expected2nd = array[first];
            var expected1st = array[second];
            //When
            //We do a swap
            Excercise1.Swap(array, first, second);
            //Then
            Assert.Equal(expected1st, array[first]);
            Assert.Equal(expected2nd, array[second]);
        }

        [Fact]
        public void Excercise1_sort()
        {
            Random rand = new Random();
            //Given
            //an array with 1000 random values.
            int[] array = Enumerable.Range(0, 1000).Select(i => rand.Next()).ToArray();
            //Do not use the built in sorting, that's cheating.
            var expectedResult = array.OrderBy(t => t).ToArray();
            //When
            //We do a swap
            int[] result = Excercise1.Sort(array);

            for(var i=0; i< result.Length; ++i)
            {
                Assert.Equal(expectedResult[i], result[i]);
            }

        }
    }
}
