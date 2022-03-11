// RectangularArrays: This class includes methods to convert 
// Java rectangular arrays (jagged arrays with inner arrays of the same length).

namespace NfcSample
{
    public static class RectangularArrays
    {
        public static sbyte[][] RectangularSbyteArray(int size1, int size2)
        {
            sbyte[][] newArray = new sbyte[size1][];
            for (int array1 = 0; array1 < size1; array1++)
            {
                newArray[array1] = new sbyte[size2];
            }

            return newArray;
        }
    }
}