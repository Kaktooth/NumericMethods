using System.Data;

namespace NumberMethods
{
    public class IterationData
    {
        System.Data.DataSet dataSet;
        public DataTable dt = new DataTable($"Таблиця ітерацій");
        public DataColumn column;
        public DataRow row;


        public IterationData()
        {
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "n";
            column.ReadOnly = false;
            column.Unique = false;
            column.Caption = "n";
            

            dt.Columns.Add(column);
            dt.Columns[0].SetOrdinal(0);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "x";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "f(x)";
            column.AutoIncrement = false;
            column.Caption = "f(x)";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "y";
            column.AutoIncrement = false;
            column.Caption = "y";
            column.ReadOnly = false;
            column.Unique = false;

            dt.Columns.Add(column);

            //DataColumn[] PrimaryKeyColumns = new DataColumn[0];
            //PrimaryKeyColumns[0] = dt.Columns["n"];
            //dt.PrimaryKey = PrimaryKeyColumns;

            dataSet = new DataSet();
            dataSet.Tables.Add(dt);

        }
    }
}
