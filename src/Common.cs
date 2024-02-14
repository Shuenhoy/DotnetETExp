namespace ET.Common
{
    public class Matrix
    {
        internal double[] _data;
        internal int _rows;
        internal int _cols;

        public Matrix(int rows, int cols)
        {
            _data = new double[rows * cols];
            _rows = rows;
            _cols = cols;
        }

        public void SetZero()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = 0;
            }
        }

        public void SetRandom(int seed = 0)
        {
            Random random = new Random(seed);
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = random.NextDouble();
            }
        }

        public ref double this[int row, int col]
            => ref _data[row * _cols + col];
        public int Rows => _rows;
        public int Cols => _cols;

        public static Vector operator *(Matrix a, Vector b)
        {
            Vector result = new(a.Rows);
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    result[i] += a[i, j] * b[j];
                }
            }
            return result;
        }
    }

    public sealed class Vector
    {
        internal double[] _data;
        internal int _rows;
        public Vector(int rows)
        {
            _rows = rows;
            _data = new double[rows];
        }
        public ref double this[int row] => ref _data[row];
        public void SetZero()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = 0;
            }
        }
        public void SetRandom(int seed = 0)
        {
            Random random = new Random(seed);
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = random.NextDouble();
            }
        }
        public int Rows => _rows;

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Rows; i++)
            {
                result += $"{this[i]} ";
            }
            return result;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            Vector result = new(a.Rows);
            for (int i = 0; i < a.Rows; i++)
            {
                result[i] = a[i] + b[i];
            }
            return result;
        }
    }
}