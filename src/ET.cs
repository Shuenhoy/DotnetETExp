
namespace ET.ETG
{
    public interface MatrixExpression
    {
        public double this[int row, int col] { get; }
        public int Rows { get; }
        public int Cols { get; }

        public static MatVecMul<T1, T2> Mul<T1, T2>(in T1 a, in T2 b)
            where T1 : MatrixExpression
            where T2 : VectorExpression
            => new(a, b);
    }


    public interface VectorExpression
    {
        public double this[int row] { get; }
        public int Rows { get; }
        public Eval Eval() => new(this);

        public static VecAdd<T1, T2> Add<T1, T2>(in T1 a, in T2 b)
            where T1 : VectorExpression
            where T2 : VectorExpression
            => new(a, b);

    }

    public struct Eval
    {
        internal readonly VectorExpression _data;
        public Eval(in VectorExpression data) => _data = data;

        public static implicit operator Common.Vector(in Eval data)
        {
            Common.Vector result = new(data._data.Rows);
            for (int i = 0; i < result.Rows; i++)
            {
                result[i] = data._data[i];
            }
            return result;
        }

    }

    public struct Matrix : MatrixExpression
    {
        internal readonly Common.Matrix _data;

        public Matrix(in Common.Matrix data) => _data = data;

        double MatrixExpression.this[int row, int col] => _data[row, col];
        int MatrixExpression.Rows => _data.Rows;
        int MatrixExpression.Cols => _data.Cols;


    }

    public struct Vector : VectorExpression
    {
        internal readonly Common.Vector _data;

        public Vector(in Common.Vector data) => _data = data;


        double VectorExpression.this[int row] => _data[row];

        int VectorExpression.Rows => _data.Rows;
    }

    public struct VecAdd<T1, T2> : VectorExpression
        where T1 : VectorExpression
        where T2 : VectorExpression
    {
        internal T1 _a;
        internal T2 _b;

        public VecAdd(in T1 a, in T2 b) => (_a, _b) = (a, b);

        double VectorExpression.this[int row] => (_a[row] + _b[row]);
        int VectorExpression.Rows => _a.Rows;


    }

    public struct MatVecMul<T1, T2> : VectorExpression
        where T1 : MatrixExpression
        where T2 : VectorExpression
    {
        internal T1 _a;
        internal T2 _b;

        public MatVecMul(in T1 a, in T2 b) => (_a, _b) = (a, b);

        double VectorExpression.this[int row]
        {
            get
            {
                double result = 0;
                for (int i = 0; i < _a.Cols; i++)
                {
                    result += _a[row, i] * _b[i];
                }
                return result;
            }
        }
        int VectorExpression.Rows => _a.Rows;
    }

    public static class ETHelper
    {
        public static Matrix ET(this Common.Matrix data)
            => new Matrix(in data);
        public static Vector ET(this Common.Vector data)
            => new Vector(in data);

        public static Common.Vector Eval<T>(this T data)
            where T : VectorExpression
            => data.Eval();

        public static void EvalTo<T>(this T data, Common.Vector result)
            where T : VectorExpression
        {
            for (int i = 0; i < result.Rows; i++)
            {
                result[i] = data[i];
            }
        }

    }
}